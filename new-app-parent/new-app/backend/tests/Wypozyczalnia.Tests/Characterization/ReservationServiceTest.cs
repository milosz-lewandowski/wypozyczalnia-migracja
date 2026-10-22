using Wypozyczalnia.Model;
using Wypozyczalnia.Services;
using Wypozyczalnia.Tests.Support;

namespace Wypozyczalnia.Tests.Characterization;

/// <summary>
/// Testy charakteryzacyjne rezerwacji. Wartosci oczekiwane z kodu legacy na tych samych danych.
/// Kolejnosc sprawdzen (legacy-app-parent/legacy-app/WypozyczalniaCore/ReservationManager.vb:19-39):
/// daty (:20-22) -&gt; maks. 60 dni, TotalDays ulamkowe (:23-25) -&gt; klient (:27-31, CustomerManager.vb:26-37)
/// -&gt; dostepnosc (:33-36) -&gt; cena szacunkowa = wycena (:38-39) -&gt; zapis ze statusem N (:41-51).
/// Stan s1-start: wyceny jeszcze nie ma w new-app, cena szacunkowa = 0,00; test R-PROG wylaczony do jej przeniesienia.
/// Zapis zmienia baze, wiec kazdy test dostaje wlasna baze w pamieci (TestApplication; w Javie: @Transactional z rollbackiem).
/// </summary>
public sealed class ReservationServiceTest : IDisposable
{
    private readonly TestApplication application = new();
    private readonly ReservationService reservationService;

    public ReservationServiceTest()
    {
        reservationService = application.Get<ReservationService>();
    }

    public void Dispose() => application.Dispose();

    /*
     * R-OK: klient 1 (prog 0), sprzet 1 (BUD, 45,00), pn 2026-10-26 08:00 -> sr 2026-10-28 08:00.
     * 1. 2880 min -> 2 doby (PricingManager.vb:22-28); doby pn, wt -> 0 weekendowych (:38-45).
     * 2. 2 * 45,00 = 90,00; brak rabatu tygodniowego (2 \ 7 = 0) i progu (0 %).
     * 3. Status N = NEW (ReservationManager.vb:46), cena = wycena (:39).
     * Stan s1-start: bez wyceny cena = 0,00; po przeniesieniu wyceny wraca oczekiwanie 90,00.
     */
    [Fact(DisplayName = "R-OK: 2 doby -> NEW, cena 0,00 do przeniesienia wyceny (docelowo 90,00)")]
    public void ROk_CreatesNewReservation()
    {
        Reservation reservation = reservationService.Create(1, 1, At(2026, 10, 26, 8, 0), At(2026, 10, 28, 8, 0), null);

        Assert.Equal(ReservationStatus.NEW, reservation.Status);
        Assert.Equal(0.00m, reservation.EstimatedPrice);
        Assert.Equal(1L, reservation.CustomerId);
        Assert.Equal(1L, reservation.EquipmentId);
        Assert.True(reservation.Id >= 100L);
    }

    /*
     * R-PROG: klient 2 (prog 1), sprzet 6 (OGR, 28,85), pn 2026-11-02 08:00 -> sb 2026-11-07 08:00.
     * Cena = wycena (ReservationManager.vb:39), jak W-A: 5 dob pn-pt, 144,25; rabat CInt(2,5) = 2 %;
     * Math.Round(141,365, 2) = 141,36 (polowka do parzystej) (PricingManager.vb:50-54, :58-60).
     */
    [Fact(DisplayName = "R-PROG: cena szacunkowa = wycena z rabatem klienta -> 141,36",
        Skip = "Wymaga wyceny w new-app; wlaczyc po przeniesieniu wyceny z legacy")]
    public void RTier_PriceIsQuote()
    {
        Reservation reservation = reservationService.Create(2, 6, At(2026, 11, 2, 8, 0), At(2026, 11, 7, 8, 0), null);

        Assert.Equal(141.36m, reservation.EstimatedPrice);
    }

    /*
     * R-KOLIZJA: klient 1, sprzet 2, 2026-10-21 08:00 -> 2026-10-23 08:00 nachodzi na rezerwacje 3 (CONFIRMED,
     * 2026-10-20 09:00 - 2026-10-22 09:00) (02_procedury.sql:44-46) -> ReservationManager.vb:34-36.
     */
    [Fact(DisplayName = "R-KOLIZJA: termin nachodzi na rezerwacje 3 -> Sprzet niedostepny w wybranym terminie")]
    public void RCollision()
    {
        var exception = Assert.Throws<ConflictException>(() =>
            reservationService.Create(1, 2, At(2026, 10, 21, 8, 0), At(2026, 10, 23, 8, 0), null));

        Assert.Equal("Sprzet niedostepny w wybranym terminie", exception.Message);
    }

    /* R-BLOK: klient 5 zablokowany w seedzie (zalegla platnosc) -> ReservationManager.vb:28-31, CustomerManager.vb:32-35. */
    [Fact(DisplayName = "R-BLOK: klient zablokowany -> 409")]
    public void RBlockedCustomer()
    {
        var exception = Assert.Throws<ConflictException>(() =>
            reservationService.Create(5, 1, At(2026, 10, 26, 8, 0), At(2026, 10, 28, 8, 0), null));

        Assert.Equal("Klient zablokowany", exception.Message);
    }

    /* R-BLOK (nieznany klient): CzyMozeWypozyczac zwraca False dla braku klienta (CustomerManager.vb:28-30), ten sam komunikat. */
    [Fact(DisplayName = "R-BLOK: nieznany klient traktowany jak zablokowany")]
    public void RUnknownCustomer()
    {
        var exception = Assert.Throws<ConflictException>(() =>
            reservationService.Create(999, 1, At(2026, 10, 26, 8, 0), At(2026, 10, 28, 8, 0), null));

        Assert.Equal("Klient zablokowany", exception.Message);
    }

    /* R-DATY: koniec = poczatek (ReservationManager.vb:20-22). Sprawdzane przed klientem: klient 5 nie zmienia wyniku. */
    [Fact(DisplayName = "R-DATY: to = from -> 400 przed sprawdzeniem klienta")]
    public void RDates()
    {
        var exception = Assert.Throws<InvalidRequestException>(() =>
            reservationService.Create(5, 1, At(2026, 10, 26, 8, 0), At(2026, 10, 26, 8, 0), null));

        Assert.Equal("Data zakonczenia musi byc pozniejsza niz data rozpoczecia", exception.Message);
    }

    /*
     * R-60: 2026-11-01 08:00 -> 2027-01-01 08:00 = 30 (listopad) + 31 (grudzien) = 61 dni > 60
     * (ReservationManager.vb:17, :23-25).
     */
    [Fact(DisplayName = "R-60: 61 dni -> Rezerwacja moze obejmowac maksymalnie 60 dni")]
    public void R60_TooLong()
    {
        var exception = Assert.Throws<InvalidRequestException>(() =>
            reservationService.Create(1, 1, At(2026, 11, 1, 8, 0), At(2027, 1, 1, 8, 0), null));

        Assert.Equal("Rezerwacja moze obejmowac maksymalnie 60 dni", exception.Message);
    }

    /*
     * R-60 granica: 2026-11-01 08:00 -> 2026-12-31 08:01 = 60 dni i 1 minuta. Legacy porownuje TimeSpan.TotalDays
     * (ulamkowe): 60,0007 > 60 -> blad (ReservationManager.vb:23-25). Liczenie pelnych dni kalendarzowych dawaloby 60 i przepuscilo.
     */
    [Fact(DisplayName = "R-60 granica: 60 dni i 1 minuta -> 400")]
    public void R60_OneMinuteOver()
    {
        var exception = Assert.Throws<InvalidRequestException>(() =>
            reservationService.Create(1, 1, At(2026, 11, 1, 8, 0), At(2026, 12, 31, 8, 1), null));

        Assert.Equal("Rezerwacja moze obejmowac maksymalnie 60 dni", exception.Message);
    }

    /*
     * R-60 granica: 2026-11-01 08:00 -> 2026-12-31 08:00 = dokladnie 60 dni; TotalDays = 60, nie > 60 -> zapis
     * (ReservationManager.vb:23). Sprzet 1 wolny w calym okresie.
     */
    [Fact(DisplayName = "R-60 granica: dokladnie 60 dni -> zapisana")]
    public void R60_ExactlySixtyDaysAllowed()
    {
        Reservation reservation = reservationService.Create(1, 1, At(2026, 11, 1, 8, 0), At(2026, 12, 31, 8, 0), null);

        Assert.Equal(ReservationStatus.NEW, reservation.Status);
    }

    /*
     * R-404: nieznany sprzet. Procedura dostepnosci nie zwraca kolizji dla nieistniejacego Id, wiec blad pada dopiero
     * w wycenie (ReservationManager.vb:39 -> PricingManager.vb:32-33). Stan s1-start: to samo miejsce i komunikat,
     * sprawdza EquipmentRepository.
     */
    [Fact(DisplayName = "R-404: nieznany sprzet -> Nieznany sprzet: 999")]
    public void RUnknownEquipment()
    {
        var exception = Assert.Throws<NotFoundException>(() =>
            reservationService.Create(1, 999, At(2026, 10, 26, 8, 0), At(2026, 10, 28, 8, 0), null));

        Assert.Equal("Nieznany sprzet: 999", exception.Message);
    }

    /* R-UWAGI: uwagi przyciete (Trim) i obciete do 500 znakow (ReservationManager.vb:16, :288-297). */
    [Fact(DisplayName = "R-UWAGI: trim i obciecie do 500 znakow")]
    public void RNotesTrimmedTo500()
    {
        string notes = "  " + new string('x', 520) + "  ";

        Reservation reservation = reservationService.Create(1, 1, At(2026, 10, 26, 8, 0), At(2026, 10, 28, 8, 0), notes);

        Assert.Equal(500, reservation.Notes!.Length);
    }

    /* R-LISTA: klient 4 ma w seedzie jedna rezerwacje (Id 3, CONFIRMED); lista obejmuje kazdy status (ReservationDAL.vb:60-66). */
    [Fact(DisplayName = "R-LISTA: rezerwacje klienta 4 -> [3]")]
    public void RListByCustomer()
    {
        Assert.Equal([3L], reservationService.List(4L).Select(reservation => reservation.Id));
    }

    private static DateTime At(int year, int month, int day, int hour, int minute) => new(year, month, day, hour, minute, 0);
}
