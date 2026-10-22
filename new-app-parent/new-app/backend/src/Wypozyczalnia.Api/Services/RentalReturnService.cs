using Wypozyczalnia.Model;
using Wypozyczalnia.Repositories;

namespace Wypozyczalnia.Services;

/// <summary>
/// Zwrot sprzetu z kara i kaucja przeniesiony z legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb
/// (ZarejestrujZwrot :90-135, ObliczKare :137-143) i cennika szkod legacy-app-parent/legacy-app/WypozyczalniaWeb/Zwrot.aspx.vb:222-236.
/// Kolejnosc jak w legacy: wypozyczenie (404) -> zamkniete (409) -> sprzet -> oplata -> kara -> kaucja -> zapis.
/// Okresy zawsze w minutach (roznica Ticks / TicksPerMinute), doby z minut wg reguly legacy - nigdy z dat kalendarzowych.
/// </summary>
public class RentalReturnService(
    RentalRepository rentalRepository,
    EquipmentRepository equipmentRepository,
    PricingService pricingService)
{
    private const long MinutesPerDay = 1440;
    private const long GraceMinutes = 60;

    /// <summary>Mnoznik kary z kodu legacy (RentalManager.vb:142); decyzja przy metodzie Penalty.</summary>
    private const decimal PenaltyMultiplier = 1.5m;

    /// <summary>Odczyt, rozliczenie i zapis zwrotu bez przeplotu z innym zadaniem (w Javie: @Transactional).</summary>
    private readonly Lock returnLock = new();

    /// <summary>Otwarte wypozyczenia do zwrotu (legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:145-147).</summary>
    public List<OpenRental> OpenRentals() => rentalRepository.FindOpen();

    /// <summary>Wypozyczenie po Id; brak -> 404 jak legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:92-94.</summary>
    public Rental GetRental(long rentalId) =>
        rentalRepository.FindById(rentalId)
            ?? throw new NotFoundException($"Nie znaleziono wypozyczenia {rentalId}");

    /// <summary>
    /// Rejestracja zwrotu. Minuty spoznienia = roznica w minutach miedzy planowanym a faktycznym zwrotem
    /// (DateDiff(Minute, planowanyZwrot, dataZwrotu), legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:138).
    /// Reszta (walidacja, rozliczenie, zapis) w Settle.
    /// </summary>
    public ReturnResult RegisterReturn(long rentalId, DateTime returnedAt, IReadOnlyList<string> damageCodes)
    {
        lock (returnLock)
        {
            Rental rental = GetRental(rentalId);
            long lateMinutes = (returnedAt - rental.PlannedReturn).Ticks / TimeSpan.TicksPerMinute;
            return Settle(rental, returnedAt, lateMinutes, damageCodes);
        }
    }

    /// <summary>Rdzen zwrotu: walidacja, rozliczenie i zapis (legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:95-134).</summary>
    private ReturnResult Settle(Rental rental, DateTime returnedAt, long lateMinutes, IReadOnlyList<string> damageCodes)
    {
        // RentalManager.vb:95-97 - zwrot zamknietego wypozyczenia
        if (rental.Status == RentalStatus.CLOSED)
        {
            throw new ConflictException($"Wypozyczenie {rental.Id} jest juz zamkniete");
        }

        // RentalManager.vb:103-107 - sprzet wypozyczenia (stawka do kary)
        Equipment equipment = equipmentRepository.FindById(rental.EquipmentId)
            ?? throw new NotFoundException($"Nieznany sprzet: {rental.EquipmentId}");

        // RentalManager.vb:109 - oplata za okres wydanie -> PLANOWANY zwrot (nie faktyczny)
        decimal fee = pricingService.CalculateQuote(rental.EquipmentId, rental.IssuedAt, rental.PlannedReturn, rental.CustomerId).Amount;

        // RentalManager.vb:110 -> ObliczKare (:137-143); Zwrot.aspx.vb:222-236 - szkody z cennika
        long lateDays = LateDays(lateMinutes);
        decimal dailyRate = equipment.DailyRate;
        decimal penalty = Penalty(lateMinutes, dailyRate);
        decimal damagesTotal = DamagesTotal(damageCodes);

        // RentalManager.vb:111-112 - zwrot kaucji
        decimal depositReturned = DepositReturned(rental.DepositTaken, penalty, damagesTotal);

        // RentalManager.vb:118-129 + 02_procedury.sql:240-252 - zamkniecie i zwolnienie sprzetu
        rentalRepository.RegisterReturn(rental.Id, returnedAt, fee, penalty, depositReturned);

        return new ReturnResult(rental.Id, returnedAt, fee, lateMinutes, lateDays, penalty, damagesTotal,
            rental.DepositTaken, depositReturned, RentalStatus.CLOSED);
    }

    /// <summary>
    /// Doby spoznienia (legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:139-140): minuty &lt;= 60 -> 0
    /// (godzina tolerancji); inaczej (minuty - 60 + 1439) \ 1440, czyli kazda zaczeta doba po godzinie tolerancji.
    /// </summary>
    public static long LateDays(long lateMinutes)
    {
        if (lateMinutes <= GraceMinutes)
        {
            return 0L;
        }
        return (lateMinutes - GraceMinutes + MinutesPerDay - 1) / MinutesPerDay;
    }

    /// <summary>
    /// Kara = Math.Round(doby * stawka dobowa * 1.5, 2) (legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:137-143).
    /// DECYZJA (DoD 10, niejednoznaczna regula): komentarz w legacy (:141) mowi "kara = 100% stawki dobowej", a kod (:142)
    /// mnozy przez 1,5. Wybor nalezy do wlasciciela procesu; w tej wersji przyjety mnoznik 1,5 z wykonywalnego kodu,
    /// bo tego oczekuja przypadki akceptacyjne bramki (zwrot-10: 178,50, zwrot-11: 357,00).
    /// Wejscie w minutach, doby z LateDays (godzina tolerancji, kazda zaczeta doba) - nie z dat kalendarzowych.
    /// Zaokraglenie polowek do parzystej (Money.Round, jak Math.Round w legacy). Zmienne nazwane pod podglad w debuggerze.
    /// </summary>
    public static decimal Penalty(long lateMinutes, decimal dailyRate)
    {
        long lateDays = LateDays(lateMinutes);
        decimal penaltyBeforeRounding = lateDays * dailyRate * PenaltyMultiplier;
        decimal penalty = Money.Round(penaltyBeforeRounding);
        return penalty;
    }

    /// <summary>
    /// Suma szkod z cennika (legacy-app-parent/legacy-app/WypozyczalniaWeb/Zwrot.aspx.vb:224-233). Jak w legacy kazda pozycja
    /// liczy sie raz (lista checkboksow, :230-236), a pozycja spoza cennika jest pomijana bez bledu (:231).
    /// </summary>
    public static decimal DamagesTotal(IReadOnlyList<string>? damageCodes)
    {
        if (damageCodes is null)
        {
            return Money.Round(0m);
        }
        decimal total = 0m;
        foreach (string code in damageCodes.Distinct())
        {
            DamageType? damage = DamageType.FindByCode(code);
            if (damage is not null)
            {
                total += damage.Amount;
            }
        }
        return Money.Round(total);
    }

    /// <summary>
    /// Zwrot kaucji = kaucja - kara - szkody, wynik ujemny -> 0
    /// (legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:111-112).
    /// </summary>
    public static decimal DepositReturned(decimal deposit, decimal penalty, decimal damages)
    {
        decimal refund = deposit - penalty - damages;
        if (refund < 0m)
        {
            refund = 0m;
        }
        return Money.Round(refund);
    }
}
