using Wypozyczalnia.Model;
using Wypozyczalnia.Services;
using Wypozyczalnia.Tests.Support;

namespace Wypozyczalnia.Tests.Characterization;

/// <summary>
/// Testy charakteryzacyjne wyceny. Wartosci oczekiwane policzone z kodu legacy na tych samych danych, nie z nowej implementacji.
/// Reguly (dowodem jest linia kodu, nie komentarz w legacy):
/// - DOBY: minuty \ 1440, reszta &gt; 60 = +1 doba, wynik 0 -&gt; 1, minuty &lt;= 0 -&gt; blad
///   (legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:22-28);
/// - TARYFA: najnowsza taryfa kategorii z ObowiazujeOd &lt;= dzien wydania
///   (legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:34, legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/PricingDAL.vb:14-21,
///   legacy-app-parent/legacy-app/Database/02_procedury.sql:281-290);
/// - WEEKEND: stawka weekendowa Math.Round(stawka * mnoznik, 2); doba k (od dataOd + k dni) w sb/nd po tej stawce
///   (legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:36-45);
/// - TYDZIEN: gdy doby \ 7 &gt;= 1: kwota - Math.Round(kwota * rabatTyg / 100, 2) (legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:46-48);
/// - PROG: prog klienta przyciety do 0..3, brak klienta = 0 (legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:39-50);
///   rabat CInt(prog * 2.5) = 0/2/5/8 % (legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:58-60), kwota
///   Math.Round(kwota * (100 - rabat) / 100, 2) (legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:50-54);
/// - ZAOKR: Math.Round(Decimal, 2) i CInt w .NET zaokraglaja polowki do parzystej (MidpointRounding.ToEven).
/// Dane: new-app-parent/new-app/backend/src/Wypozyczalnia.Api/Database/data.sql = seed legacy-app-parent/legacy-app/Database/03_seed.sql
/// (te same Id i wartosci). Taryfy 2026: BUD 1,25 / 10 %, OGR 1,20 / 10 %, IMP 1,50 / 5 %, TRN 1,00 / 15 % (03_seed.sql:104-113).
/// </summary>
public sealed class PricingServiceTest : IDisposable
{
    private readonly TestApplication application = new();
    private readonly PricingService pricingService;

    public PricingServiceTest()
    {
        pricingService = application.Get<PricingService>();
    }

    public void Dispose() => application.Dispose();

    private static void AssertQuote(Quote quote, decimal expectedAmount, int expectedDays, int expectedWeekendDays,
        int expectedCustomerDiscountPercent)
    {
        Assert.Equal(expectedAmount, quote.Amount);
        Assert.Equal(expectedDays, quote.Days);
        Assert.Equal(expectedWeekendDays, quote.WeekendDays);
        Assert.Equal(expectedCustomerDiscountPercent, quote.CustomerDiscountPercent);
    }

    /*
     * W-A: sprzet 6 (OGR, 28,85), pn 2026-10-12 08:00 -> sb 2026-10-17 08:00, klient 2 (prog 1).
     * 1. Doby: 7200 min; 7200 \ 1440 = 5; reszta 0 -> 5 dob (PricingManager.vb:23-26).
     * 2. Doby k = 0..4: pn, wt, sr, czw, pt -> 0 weekendowych (sobota 08:00 to koniec okresu) (PricingManager.vb:38-45).
     * 3. Suma 5 * 28,85 = 144,25; rabat tygodniowy: 5 \ 7 = 0 -> brak (PricingManager.vb:46).
     * 4. Prog 1 -> CInt(2,5) = 2 (polowka do parzystej) (PricingManager.vb:59).
     * 5. Math.Round(144,25 * 98 / 100, 2) = Math.Round(141,365, 2) = 141,36 (polowka w gore dalaby 141,37) (PricingManager.vb:53).
     * Dowody: legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:22-28, :36-54, :58-60.
     */
    [Fact(DisplayName = "W-A: 5 dob, rabat klienta 2 %, polowka do parzystej -> 141,36")]
    public void WA_HalfEvenOnCustomerDiscount()
    {
        Quote quote = pricingService.CalculateQuote(6, At(2026, 10, 12, 8, 0), At(2026, 10, 17, 8, 0), 2L);

        AssertQuote(quote, 141.36m, 5, 0, 2);
        Assert.Equal(0m, quote.WeeklyDiscountPercent);
    }

    /*
     * W-B: sprzet 2 (BUD, 72,35), wt 2026-10-13 09:00 -> czw 2026-10-15 09:30, klient 1 (prog 0).
     * 1. Doby: 2910 min; 2910 \ 1440 = 2; reszta 30, nie > 60 -> 2 doby (PricingManager.vb:25-26).
     * 2. Doby: wt, sr -> 0 weekendowych. Suma 2 * 72,35 = 144,70. Brak rabatu tygodniowego i progu.
     * Dowod: legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:22-28.
     */
    [Fact(DisplayName = "W-B: reszta 30 min miesci sie w tolerancji -> 2 doby, 144,70")]
    public void WB_RemainderWithinGraceHour()
    {
        Quote quote = pricingService.CalculateQuote(2, At(2026, 10, 13, 9, 0), At(2026, 10, 15, 9, 30), 1L);

        AssertQuote(quote, 144.70m, 2, 0, 0);
    }

    /*
     * W-B60: jak W-B, koniec czw 2026-10-15 10:00.
     * 1. Doby: 2940 min; 2940 \ 1440 = 2; reszta 60; warunek "> 60" nie zachodzi -> 2 doby (PricingManager.vb:26).
     * 2. Suma 2 * 72,35 = 144,70.
     * Dowod: legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:26.
     */
    [Fact(DisplayName = "W-B60: reszta dokladnie 60 min -> nadal 2 doby, 144,70")]
    public void WB60_RemainderExactlySixtyMinutes()
    {
        Quote quote = pricingService.CalculateQuote(2, At(2026, 10, 13, 9, 0), At(2026, 10, 15, 10, 0), 1L);

        AssertQuote(quote, 144.70m, 2, 0, 0);
    }

    /*
     * W-B61: jak W-B, koniec czw 2026-10-15 10:01.
     * 1. Doby: 2941 min; 2941 \ 1440 = 2; reszta 61 > 60 -> 3 doby (PricingManager.vb:26).
     * 2. Doby: wt, sr, czw -> 0 weekendowych. Suma 3 * 72,35 = 217,05.
     * Dowod: legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:26.
     */
    [Fact(DisplayName = "W-B61: reszta 61 min -> 3 doby, 217,05")]
    public void WB61_RemainderSixtyOneMinutes()
    {
        Quote quote = pricingService.CalculateQuote(2, At(2026, 10, 13, 9, 0), At(2026, 10, 15, 10, 1), 1L);

        AssertQuote(quote, 217.05m, 3, 0, 0);
    }

    /*
     * W-C: sprzet 2 (BUD, 72,35), pt 2026-10-09 10:00 -> pt 2026-10-16 10:00, klient 1 (prog 0).
     * 1. Doby: 10080 min = 7 dob. Taryfa BUD na 2026-10-09 = wiersz 2026-01-01 (1,25 / 10 %), nie 2025 (1,20)
     *    (02_procedury.sql:288-290: ObowiazujeOd <= dzien, ORDER BY ObowiazujeOd DESC).
     * 2. Stawka weekendowa Math.Round(72,35 * 1,25, 2) = Math.Round(90,4375, 2) = 90,44 (PricingManager.vb:36).
     * 3. Doby k = 0..6: pt, sb, nd, pn, wt, sr, czw -> 2 weekendowe; 5 * 72,35 + 2 * 90,44 = 361,75 + 180,88 = 542,63.
     * 4. 7 \ 7 = 1 -> rabat tygodniowy Math.Round(542,63 * 10 / 100, 2) = Math.Round(54,263, 2) = 54,26;
     *    542,63 - 54,26 = 488,37 (PricingManager.vb:46-48). Prog 0 -> brak rabatu klienta.
     * Dowody: legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:34-48, legacy-app-parent/legacy-app/Database/02_procedury.sql:281-290.
     */
    [Fact(DisplayName = "W-C: 7 dob z weekendem, taryfa 2026, rabat tygodniowy 10 % -> 488,37")]
    public void WC_WeekendAndWeeklyDiscount()
    {
        Quote quote = pricingService.CalculateQuote(2, At(2026, 10, 9, 10, 0), At(2026, 10, 16, 10, 0), 1L);

        AssertQuote(quote, 488.37m, 7, 2, 0);
        Assert.Equal(90.44m, quote.WeekendRate);
        Assert.Equal(10m, quote.WeeklyDiscountPercent);
    }

    /*
     * W-D: sprzet 2 (BUD, 72,35), wt 2026-10-13 09:00 -> czw 2026-10-15 09:00, klient 4 (prog 3).
     * 1. Doby: 2880 min = 2 doby (wt, sr); suma 2 * 72,35 = 144,70.
     * 2. Prog 3 -> CInt(3 * 2,5) = CInt(7,5) = 8 (polowka do parzystej; "2,5 % za prog" z komentarza :49 dalby 7,5)
     *    (PricingManager.vb:59).
     * 3. Math.Round(144,70 * 92 / 100, 2) = Math.Round(133,124, 2) = 133,12 (PricingManager.vb:53).
     * Dowody: legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:50-54, :58-60.
     */
    [Fact(DisplayName = "W-D: prog 3 -> CInt(7,5) = 8 %, 133,12")]
    public void WD_TopTierRoundsToEight()
    {
        Quote quote = pricingService.CalculateQuote(2, At(2026, 10, 13, 9, 0), At(2026, 10, 15, 9, 0), 4L);

        AssertQuote(quote, 133.12m, 2, 0, 8);
    }

    /*
     * W-F: sprzet 4 (BUD, 38,50), czw 2026-10-01 08:00 -> czw 2026-10-15 08:00, klient 10 (prog 3).
     * 1. Doby: 20160 min = 14 dob; doby k = 0..13 od czwartku -> sb 03, nd 04, sb 10, nd 11 = 4 weekendowe.
     * 2. Stawka weekendowa Math.Round(38,50 * 1,25, 2) = Math.Round(48,125, 2) = 48,12 (polowka w gore dalaby 48,13) (PricingManager.vb:36).
     * 3. Suma 10 * 38,50 + 4 * 48,12 = 385,00 + 192,48 = 577,48.
     * 4. 14 \ 7 = 2 >= 1 -> rabat tygodniowy Math.Round(57,748, 2) = 57,75; 577,48 - 57,75 = 519,73 (PricingManager.vb:46-48).
     * 5. Prog 3 -> 8 %: Math.Round(519,73 * 92 / 100, 2) = Math.Round(478,1516, 2) = 478,15 (PricingManager.vb:53).
     * Dowody: legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:36-54.
     */
    [Fact(DisplayName = "W-F: 14 dob, polowka stawki weekendowej do parzystej, oba rabaty -> 478,15")]
    public void WF_HalfEvenOnWeekendRate()
    {
        Quote quote = pricingService.CalculateQuote(4, At(2026, 10, 1, 8, 0), At(2026, 10, 15, 8, 0), 10L);

        AssertQuote(quote, 478.15m, 14, 4, 8);
        Assert.Equal(48.12m, quote.WeekendRate);
    }

    /*
     * W-MIN: sprzet 15 (TRN, 12,50), wt 2026-10-20 08:00 -> 08:30, bez klienta.
     * 1. Doby: 30 min; 30 \ 1440 = 0; reszta 30 nie > 60 -> 0; wynik 0 -> 1 doba (PricingManager.vb:27).
     * 2. Suma 1 * 12,50 = 12,50 (wt, nie weekend). Bez klienta = prog 0 (ObliczOplateBezKlienta, PricingManager.vb:62-64).
     * Dowod: legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:27.
     */
    [Fact(DisplayName = "W-MIN: 30 min -> minimum 1 doba, 12,50")]
    public void WMin_MinimumOneDay()
    {
        Quote quote = pricingService.CalculateQuote(15, At(2026, 10, 20, 8, 0), At(2026, 10, 20, 8, 30), null);

        AssertQuote(quote, 12.50m, 1, 0, 0);
    }

    /*
     * W-NIEZNANY-KLIENT: jak W-A, klient 999 (brak w bazie).
     * 1. ProgRabatu zwraca 0 dla braku wiersza (CustomerManager.vb:43-45) -> CInt(0) = 0 -> brak rabatu.
     * 2. Suma 5 * 28,85 = 144,25 bez zmian.
     * Dowody: legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:43-45, legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:52.
     */
    [Fact(DisplayName = "W-NIEZNANY-KLIENT: brak klienta w bazie = prog 0 -> 144,25")]
    public void WUnknownCustomer_TierZero()
    {
        Quote quote = pricingService.CalculateQuote(6, At(2026, 10, 12, 8, 0), At(2026, 10, 17, 8, 0), 999L);

        AssertQuote(quote, 144.25m, 5, 0, 0);
    }

    /*
     * W-BLAD: sprzet 2, data zwrotu = data wydania -> 0 minut -> blad "Data zwrotu musi byc pozniejsza niz data wydania".
     * Dowod: legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:24.
     */
    [Fact(DisplayName = "W-BLAD: data zwrotu = data wydania -> blad danych")]
    public void WBlad_ReturnNotAfterIssue()
    {
        var exception = Assert.Throws<InvalidRequestException>(() =>
            pricingService.CalculateQuote(2, At(2026, 10, 13, 9, 0), At(2026, 10, 13, 9, 0), 1L));

        Assert.Equal("Data zwrotu musi byc pozniejsza niz data wydania", exception.Message);
    }

    /*
     * W-404: sprzet 999 (brak w bazie) -> "Nieznany sprzet: 999".
     * Dowod: legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:32-33.
     */
    [Fact(DisplayName = "W-404: nieznany sprzet -> NotFoundException")]
    public void W404_UnknownEquipment()
    {
        var exception = Assert.Throws<NotFoundException>(() =>
            pricingService.CalculateQuote(999, At(2026, 10, 13, 9, 0), At(2026, 10, 15, 9, 0), 1L));

        Assert.Equal("Nieznany sprzet: 999", exception.Message);
    }

    /*
     * W-TARYFA (dodatkowy): sprzet 6 (OGR), wydanie 2025-06-02 - jedyna taryfa OGR
     * obowiazuje od 2026-01-01, wiec brak wiersza z ObowiazujeOd <= 2025-06-02 -> blad z PricingDAL.vb:19.
     * Dowody: legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/PricingDAL.vb:18-19, legacy-app-parent/legacy-app/Database/02_procedury.sql:288-289.
     */
    [Fact(DisplayName = "W-TARYFA: brak taryfy na dzien wydania -> ConflictException")]
    public void WTariff_MissingForIssueDay()
    {
        var exception = Assert.Throws<ConflictException>(() =>
            pricingService.CalculateQuote(6, At(2025, 6, 2, 8, 0), At(2025, 6, 3, 8, 0), null));

        Assert.Equal("Brak taryfy dla kategorii OGR na dzien 2025-06-02", exception.Message);
    }

    /*
     * DOBY - granice funkcji LiczbaDob na samych minutach (legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:22-28):
     * 1 -> 0 + reszta 1 -> 0 -> minimum 1; 1440 -> 1; 1500 (reszta 60) -> 1; 1501 (reszta 61) -> 2; 2880 -> 2; 0 -> blad.
     */
    [Fact(DisplayName = "DOBY: granice tolerancji 60/61 min i minimum 1 doba")]
    public void RentalDays_Boundaries()
    {
        Assert.Equal(1, PricingService.RentalDays(1));
        Assert.Equal(1, PricingService.RentalDays(1440));
        Assert.Equal(1, PricingService.RentalDays(1500));
        Assert.Equal(2, PricingService.RentalDays(1501));
        Assert.Equal(2, PricingService.RentalDays(2880));
        Assert.Throws<InvalidRequestException>(() => PricingService.RentalDays(0));
    }

    /*
     * PROG - RabatProgu = CInt(prog * 2.5), CInt zaokragla polowki do parzystej
     * (legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:58-60): 0 -> 0; 1 -> CInt(2,5) = 2; 2 -> 5; 3 -> CInt(7,5) = 8.
     */
    [Fact(DisplayName = "PROG: CInt(prog * 2,5) = 0 / 2 / 5 / 8 %")]
    public void CustomerDiscountPercent_BankersCInt()
    {
        Assert.Equal(0, PricingService.CustomerDiscountPercent(0));
        Assert.Equal(2, PricingService.CustomerDiscountPercent(1));
        Assert.Equal(5, PricingService.CustomerDiscountPercent(2));
        Assert.Equal(8, PricingService.CustomerDiscountPercent(3));
    }

    /*
     * ZAOKR - Money.Round = Math.Round(Decimal, 2) w .NET (polowki do parzystej)
     * (legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:36, :47, :53): 0,125 -> 0,12; 0,135 -> 0,14; 141,365 -> 141,36.
     */
    [Fact(DisplayName = "ZAOKR: polowki do parzystej (ToEven)")]
    public void MoneyRound_HalfEven()
    {
        Assert.Equal(0.12m, Money.Round(0.125m));
        Assert.Equal(0.14m, Money.Round(0.135m));
        Assert.Equal(141.36m, Money.Round(141.365m));
        Assert.Equal(2, Money.Round(141.365m).Scale);
    }

    private static DateTime At(int year, int month, int day, int hour, int minute) => new(year, month, day, hour, minute, 0);
}
