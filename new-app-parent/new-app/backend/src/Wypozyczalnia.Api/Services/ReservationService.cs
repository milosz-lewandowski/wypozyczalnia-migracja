using Wypozyczalnia.Model;
using Wypozyczalnia.Repositories;

namespace Wypozyczalnia.Services;

/// <summary>
/// Rezerwacje przeniesione z legacy-app-parent/legacy-app/WypozyczalniaCore/ReservationManager.vb (UtworzRezerwacje :19-55).
/// Kolejnosc sprawdzen jak w legacy: daty -> 60 dni -> klient -> dostepnosc -> cena.
/// Stan s1-start: wycena nie jest jeszcze przeniesiona, wiec cena szacunkowa to 0,00 (kolumna estimated_price
/// jest NOT NULL), a nieznany sprzet wykrywa EquipmentRepository zamiast wyceny.
/// </summary>
public class ReservationService(
    AvailabilityService availabilityService,
    EquipmentRepository equipmentRepository,
    CustomerRepository customerRepository,
    ReservationRepository reservationRepository)
{
    private const long MaxReservationDays = 60;
    private const long MinutesPerDay = 1440;
    private const int MaxNotesLength = 500;

    /// <summary>Cena szacunkowa do czasu przeniesienia wyceny (kolumna estimated_price nie przyjmuje NULL).</summary>
    private static readonly decimal PriceNotQuoted = 0.00m;

    /// <summary>Sprawdzenie dostepnosci i zapis bez przeplotu z innym zadaniem (w Javie: @Transactional).</summary>
    private readonly Lock createLock = new();

    public Reservation Create(long customerId, long equipmentId, DateTime from, DateTime to, string? notes)
    {
        // ReservationManager.vb:20-22
        if (to <= from)
        {
            throw new InvalidRequestException("Data zakonczenia musi byc pozniejsza niz data rozpoczecia");
        }
        // ReservationManager.vb:23-25 - TotalDays > 60 (ulamkowe): 60 dni i 1 minuta to juz za dlugo
        long minutes = (to - from).Ticks / TimeSpan.TicksPerMinute;
        if (minutes > MaxReservationDays * MinutesPerDay)
        {
            throw new InvalidRequestException($"Rezerwacja moze obejmowac maksymalnie {MaxReservationDays} dni");
        }

        lock (createLock)
        {
            // ReservationManager.vb:27-31 + CustomerManager.vb:26-37 - nieznany albo zablokowany klient nie moze rezerwowac
            Customer? customer = customerRepository.FindById(customerId);
            bool canRent = customer is not null && !customer.Blocked;
            if (!canRent)
            {
                throw new ConflictException("Klient zablokowany");
            }
            // ReservationManager.vb:33-36
            if (!availabilityService.IsAvailable(equipmentId, from, to))
            {
                throw new ConflictException("Sprzet niedostepny w wybranym terminie");
            }
            // ReservationManager.vb:38-39 - w legacy nieznany sprzet konczy sie bledem 404 dopiero w wycenie
            if (equipmentRepository.FindById(equipmentId) is null)
            {
                throw new NotFoundException($"Nieznany sprzet: {equipmentId}");
            }
            // TODO po przeniesieniu wyceny: cena szacunkowa = wycena (ReservationManager.vb:38-39)
            decimal estimatedPrice = PriceNotQuoted;

            // ReservationManager.vb:41-51
            long id = reservationRepository.Insert(customerId, equipmentId, from, to, estimatedPrice, TrimNotes(notes));
            return reservationRepository.FindById(id)
                ?? throw new InvalidOperationException($"Brak zapisanej rezerwacji {id}");
        }
    }

    /// <summary>Wszystkie rezerwacje albo rezerwacje klienta (kazdy status), sortowanie po Id.</summary>
    public List<Reservation> List(long? customerId)
    {
        if (customerId is null)
        {
            return reservationRepository.FindAll();
        }
        return reservationRepository.FindByCustomer(customerId.Value);
    }

    /// <summary>
    /// ReservationManager.vb:288-297 - uwagi przyciete (trim) i obciete do 500 znakow;
    /// puste zapisywane jako NULL (ReservationDAL.vb:26-29).
    /// </summary>
    internal static string? TrimNotes(string? notes)
    {
        if (notes is null)
        {
            return null;
        }
        string trimmed = notes.Trim();
        if (trimmed.Length == 0)
        {
            return null;
        }
        return trimmed.Length > MaxNotesLength ? trimmed[..MaxNotesLength] : trimmed;
    }
}
