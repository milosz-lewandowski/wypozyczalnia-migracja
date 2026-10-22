using System.Globalization;
using Wypozyczalnia.Model;
using Wypozyczalnia.Repositories;

namespace Wypozyczalnia.Services;

/// <summary>
/// Wycena wynajmu przeniesiona z legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb (ObliczOplate, :31-56).
/// Kolejnosc krokow jak w legacy: sprzet -> taryfa -> doby -> suma po dobach -> rabat tygodniowy -> rabat klienta.
/// </summary>
public class PricingService(
    EquipmentRepository equipmentRepository,
    TariffRepository tariffRepository,
    CustomerRepository customerRepository)
{
    private const long MinutesPerDay = 1440;
    private const long GraceMinutes = 60;
    private const int MaxDiscountTier = 3;
    private const decimal TierDiscountStep = 2.5m;
    private const string DayFormat = "yyyy-MM-dd";

    public Quote CalculateQuote(long equipmentId, DateTime from, DateTime to, long? customerId)
    {
        // PricingManager.vb:32-33 - nieznany sprzet
        Equipment equipment = equipmentRepository.FindById(equipmentId)
            ?? throw new NotFoundException($"Nieznany sprzet: {equipmentId}");

        // PricingManager.vb:34 + DAL/PricingDAL.vb:18-19 - taryfa kategorii na dzien wydania
        DateOnly issueDay = DateOnly.FromDateTime(from);
        Tariff tariff = tariffRepository.FindValidOn(equipment.Category, issueDay)
            ?? throw new ConflictException(
                $"Brak taryfy dla kategorii {equipment.Category} na dzien {issueDay.ToString(DayFormat, CultureInfo.InvariantCulture)}");

        // PricingManager.vb:35 -> LiczbaDob (:22-28); minuty jak DateDiff(Minute) w legacy
        long minutes = (to - from).Ticks / TimeSpan.TicksPerMinute;
        int days = RentalDays(minutes);

        // PricingManager.vb:36 - stawka weekendowa zaokraglona przed sumowaniem
        decimal dailyRate = equipment.DailyRate;
        decimal weekendRate = Money.Round(dailyRate * tariff.WeekendMultiplier);

        // PricingManager.vb:38-45 - doba k zaczyna sie od from + k dni; sobota i niedziela po stawce weekendowej
        decimal amount = 0m;
        int weekendDays = 0;
        for (int k = 0; k < days; k++)
        {
            DayOfWeek day = from.AddDays(k).DayOfWeek;
            if (day == DayOfWeek.Saturday || day == DayOfWeek.Sunday)
            {
                amount += weekendRate;
                weekendDays++;
            }
            else
            {
                amount += dailyRate;
            }
        }

        // PricingManager.vb:46-48 - rabat tygodniowy od 7 dob, kwota rabatu zaokraglona osobno
        decimal weeklyDiscountPercent = 0m;
        if (days / 7 >= 1)
        {
            weeklyDiscountPercent = tariff.WeeklyDiscountPercent;
            amount -= Money.Round(amount * weeklyDiscountPercent / 100m);
        }

        // PricingManager.vb:50-54 - rabat progu klienta; prog z CustomerManager.vb:39-50 (brak klienta = 0, przyciecie 0..3)
        int customerDiscountPercent = CustomerDiscountPercent(DiscountTier(customerId));
        if (customerDiscountPercent > 0)
        {
            amount = Money.Round(amount * (100 - customerDiscountPercent) / 100m);
        }

        return new Quote(equipmentId, from, to, customerId, days, weekendDays, dailyRate, weekendRate,
            weeklyDiscountPercent, customerDiscountPercent, Money.Round(amount));
    }

    /// <summary>
    /// Liczba dob z minut (legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:22-28):
    /// pelne doby minuty \ 1440; reszta &gt; 60 min = +1 doba (rowno 60 min miesci sie w tolerancji);
    /// wynik 0 -> 1; minuty &lt;= 0 -> blad.
    /// </summary>
    public static int RentalDays(long minutes)
    {
        if (minutes <= 0)
        {
            // PricingManager.vb:24
            throw new InvalidRequestException("Data zwrotu musi byc pozniejsza niz data wydania");
        }
        int days = (int)(minutes / MinutesPerDay);
        if (minutes % MinutesPerDay > GraceMinutes)
        {
            days++;
        }
        return days == 0 ? 1 : days;
    }

    /// <summary>
    /// Rabat progu klienta w procentach: CInt(prog * 2.5) (legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:58-60).
    /// CInt zaokragla polowki do parzystej: 0 -> 0, 1 -> 2 (2,5), 2 -> 5, 3 -> 8 (7,5).
    /// Komentarz w legacy (:49) mowi o 2,5 % za prog - dowodem jest kod, nie komentarz.
    /// </summary>
    public static int CustomerDiscountPercent(int tier) =>
        (int)Math.Round(tier * TierDiscountStep, 0, MidpointRounding.ToEven);

    /// <summary>CustomerManager.vb:39-50: brak klienta = prog 0 (:43-45), przyciecie do 0..3 (:47-48).</summary>
    private int DiscountTier(long? customerId)
    {
        if (customerId is null)
        {
            return 0;
        }
        int tier = customerRepository.FindById(customerId.Value)?.DiscountTier ?? 0;
        return Math.Clamp(tier, 0, MaxDiscountTier);
    }
}
