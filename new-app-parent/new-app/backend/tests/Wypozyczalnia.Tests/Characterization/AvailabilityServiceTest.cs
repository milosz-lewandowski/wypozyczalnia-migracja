using Wypozyczalnia.Model;
using Wypozyczalnia.Services;
using Wypozyczalnia.Tests.Support;

namespace Wypozyczalnia.Tests.Characterization;

/// <summary>
/// Testy charakteryzacyjne dostepnosci sprzetu. Wartosci oczekiwane z kodu legacy na tych samych danych.
/// Reguly (dowodem jest linia kodu, nie komentarz w legacy):
/// - KOLIZJA-R: rezerwacja blokuje tylko w statusie N/P (NEW/CONFIRMED) i gdy DataOd &lt; @DataDo AND DataDo &gt; @DataOd
///   (legacy-app-parent/legacy-app/Database/02_procedury.sql:44-46) - granice ostre;
/// - KOLIZJA-W: otwarte wypozyczenie blokuje od wydania do PLANOWANEGO zwrotu (legacy-app-parent/legacy-app/Database/02_procedury.sql:56-58);
/// - KOLIZJA-S: sprzet w statusie S/X (SERVICE/RETIRED) zawsze niedostepny (legacy-app-parent/legacy-app/Database/02_procedury.sql:66-68);
/// - LISTA: kategoria bez wycofanych (legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/EquipmentDAL.vb:41), pominiety serwis
///   (legacy-app-parent/legacy-app/WypozyczalniaCore/EquipmentManager.vb:58), reszta wg kolizji (:61);
/// - ZAKRES: @DataDo &lt;= @DataOd -&gt; "Nieprawidlowy zakres dat" (legacy-app-parent/legacy-app/Database/02_procedury.sql:32-36).
/// Dane: new-app-parent/new-app/backend/src/Wypozyczalnia.Api/Database/data.sql = seed legacy-app-parent/legacy-app/Database/03_seed.sql.
/// </summary>
public sealed class AvailabilityServiceTest : IDisposable
{
    private readonly TestApplication application = new();
    private readonly AvailabilityService availabilityService;

    public AvailabilityServiceTest()
    {
        availabilityService = application.Get<AvailabilityService>();
    }

    public void Dispose() => application.Dispose();

    /*
     * D-1: kategoria BUD (Id 1-5), sr 2026-10-21 09:00 -> 17:00.
     * 1. Id 1 (AVAILABLE): brak rezerwacji i wypozyczen w terminie -> dostepny.
     * 2. Id 2: rezerwacja 3 CONFIRMED 2026-10-20 09:00 - 2026-10-22 09:00 nachodzi na termin -> kolizja R (02_procedury.sql:44-46).
     * 3. Id 3: wypozyczenie 3 zamkniete (CLOSED) -> nie blokuje (02_procedury.sql:56 tylko Status = 'O') -> dostepny.
     * 4. Id 4: status RENTED, wypozyczenie 4 OPEN z planowanym zwrotem 2026-10-15 08:00, czyli ZALEGLE.
     *    02_procedury.sql:57-58 porownuje termin z PlanowanyZwrot, a nie ze statusem sprzetu W (RENTED):
     *    2026-10-15 08:00 > 2026-10-21 09:00 to falsz -> brak kolizji W. Warunek S (:66-68) sprawdza tylko S i X,
     *    wiec W nie blokuje. Wynik: sprzet fizycznie u klienta jest "dostepny" - osobliwosc legacy utrwalona testem.
     * 5. Id 5: SERVICE -> pominiety w liscie (EquipmentManager.vb:58).
     * Wynik: [1, 3, 4].
     */
    [Fact(DisplayName = "D-1: BUD 21.10 -> [1, 3, 4]; zalegly sprzet 4 (RENTED) dostepny, bo legacy patrzy na PlanowanyZwrot")]
    public void D1_CategoryAvailability()
    {
        List<Equipment> available = availabilityService.AvailableInCategory("BUD", At(2026, 10, 21, 9, 0), At(2026, 10, 21, 17, 0));

        Assert.Equal([1L, 3L, 4L], available.Select(equipment => equipment.Id));
    }

    /*
     * D-GRANICA: sprzet 2, 2026-10-22 09:00 -> 2026-10-23 09:00. Rezerwacja 3 konczy sie 2026-10-22 09:00.
     * Warunek r.DataDo > @DataOd: 09:00 > 09:00 to falsz -> brak kolizji, granice ostre (02_procedury.sql:46).
     */
    [Fact(DisplayName = "D-GRANICA: start rowny koncowi rezerwacji 3 -> dostepny (granice ostre)")]
    public void DBoundary_EndEqualsStartIsFree()
    {
        Assert.True(availabilityService.IsAvailable(2, At(2026, 10, 22, 9, 0), At(2026, 10, 23, 9, 0)));
    }

    /*
     * D-GRANICA (strona przeciwna): sprzet 2, 2026-10-22 08:59 -> 2026-10-23 09:00 nachodzi minuta na rezerwacje 3
     * (2026-10-22 09:00 > 08:59) -> kolizja (02_procedury.sql:45-46).
     */
    [Fact(DisplayName = "D-GRANICA: minuta zakladki z rezerwacja 3 -> niedostepny")]
    public void DBoundary_OneMinuteOverlapCollides()
    {
        Assert.False(availabilityService.IsAvailable(2, At(2026, 10, 22, 8, 59), At(2026, 10, 23, 9, 0)));
    }

    /*
     * D-ANUL: sprzet 13, 2026-10-10 10:00 -> 2026-10-12 10:00. Rezerwacja 5 (2026-10-09 - 2026-10-16) ma status
     * CANCELLED (A); blokuja tylko N i P (02_procedury.sql:44) -> dostepny.
     */
    [Fact(DisplayName = "D-ANUL: anulowana rezerwacja 5 nie blokuje sprzetu 13")]
    public void DCancelled_DoesNotBlock()
    {
        Assert.True(availabilityService.IsAvailable(13, At(2026, 10, 10, 10, 0), At(2026, 10, 12, 10, 0)));
    }

    /*
     * D-X: kategoria OGR (Id 6-9), pn 2026-11-02 08:00 -> wt 2026-11-03 08:00.
     * Id 9 RETIRED (X): pominiety juz w liscie kategorii (EquipmentDAL.vb:41) i niedostepny wg 02_procedury.sql:68.
     * Id 7 RENTED, ale wypozyczenie 6 ma planowany zwrot 2026-10-18 10:00, przed terminem -> brak kolizji (:57-58).
     * Wynik: [6, 7, 8].
     */
    [Fact(DisplayName = "D-X: OGR bez wycofanego sprzetu 9")]
    public void DRetired_Excluded()
    {
        List<Equipment> available = availabilityService.AvailableInCategory("OGR", At(2026, 11, 2, 8, 0), At(2026, 11, 3, 8, 0));

        List<long> ids = available.Select(equipment => equipment.Id).ToList();
        Assert.Equal([6L, 7L, 8L], ids);
        Assert.DoesNotContain(9L, ids);
        Assert.False(availabilityService.IsAvailable(9, At(2026, 11, 2, 8, 0), At(2026, 11, 3, 8, 0)));
    }

    /* D-SERWIS: sprzet 5 SERVICE (S) niedostepny w kazdym terminie (02_procedury.sql:66-68). */
    [Fact(DisplayName = "D-SERWIS: sprzet 5 w serwisie -> niedostepny")]
    public void DService_NeverAvailable()
    {
        Assert.False(availabilityService.IsAvailable(5, At(2026, 11, 2, 8, 0), At(2026, 11, 3, 8, 0)));
    }

    /* D-ZAKRES: koniec = poczatek -> blad "Nieprawidlowy zakres dat" (02_procedury.sql:32-36). */
    [Fact(DisplayName = "D-ZAKRES: to = from -> Nieprawidlowy zakres dat")]
    public void DInvalidRange()
    {
        var exception = Assert.Throws<InvalidRequestException>(() =>
            availabilityService.AvailableInCategory("BUD", At(2026, 10, 21, 9, 0), At(2026, 10, 21, 9, 0)));

        Assert.Equal("Nieprawidlowy zakres dat", exception.Message);
    }

    private static DateTime At(int year, int month, int day, int hour, int minute) => new(year, month, day, hour, minute, 0);
}
