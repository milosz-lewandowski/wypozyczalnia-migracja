using Wypozyczalnia.Services;

namespace Wypozyczalnia.Tests.Characterization;

/// <summary>
/// Testy charakteryzacyjne kary za spoznienie, szkod i kaucji - metody statyczne serwisu, bez bazy.
/// Wejscie = minuty spoznienia (zasada: kwoty kary asertowane tylko tutaj; kontroler i przeplyw serwisu ich nie sprawdzaja).
/// Wartosci oczekiwane policzone z kodu legacy, nie z nowej implementacji. Dowodem jest linia kodu, nie komentarz:
/// - KARA: minuty &lt;= 60 -&gt; 0 (legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:139);
///   doby = (minuty - 60 + 1439) \ 1440 (:140); kara = Math.Round(doby * stawka * 1.5, 2) (:142);
///   komentarz :141 mowi "100% stawki" - kod mnozy przez 1,5;
/// - ZAOKR: Math.Round(Decimal, 2) w .NET = polowki do parzystej (HALF_EVEN; Money.Round, MidpointRounding.ToEven);
/// - KAUCJA: kaucja - kara - szkody, wynik ujemny -&gt; 0 (RentalManager.vb:111-112);
/// - SZKODY: cennik obudowa 50, akcesoria 30, zabrudzenie 20 (legacy-app-parent/legacy-app/WypozyczalniaWeb/Zwrot.aspx.vb:224-226).
/// </summary>
public class RentalReturnServiceTest
{
    /*
     * Z-K60: 60 min spoznienia, stawka 72,35.
     * 60 <= 60 -> kara 0 bez liczenia dob (RentalManager.vb:139): godzina tolerancji.
     */
    [Fact(DisplayName = "Z-K60: 60 min spoznienia miesci sie w tolerancji -> kara 0,00, doby 0")]
    public void ZK60_WithinGraceHour()
    {
        Assert.Equal(0L, RentalReturnService.LateDays(60));
        Assert.Equal(0.00m, RentalReturnService.Penalty(60, 72.35m));
    }

    /*
     * Z-K61: 61 min, stawka 89,90 (= seed wyp. 3: plan 07:00, zwrot 08:01, legacy-app-parent/legacy-app/Database/03_seed.sql:153).
     * 1. 61 > 60 (RentalManager.vb:139).
     * 2. Doby: (61 - 60 + 1439) \ 1440 = 1440 \ 1440 = 1 (:140).
     * 3. Kara: 1 * 89,90 * 1,5 = 134,85 (:142).
     */
    [Fact(DisplayName = "Z-K61: 61 min -> 1 doba, kara 134,85")]
    public void ZK61_FirstMinuteAfterGrace()
    {
        Assert.Equal(1L, RentalReturnService.LateDays(61));
        Assert.Equal(134.85m, RentalReturnService.Penalty(61, 89.90m));
    }

    /*
     * Z-K1500: 1500 min (25 h), stawka 119,00.
     * 1. Doby: (1500 - 60 + 1439) \ 1440 = 2879 \ 1440 = 1 (RentalManager.vb:140).
     * 2. Kara: 1 * 119,00 * 1,5 = 178,50 (:142).
     */
    [Fact(DisplayName = "Z-K1500: 25 h spoznienia -> 1 doba, kara 178,50")]
    public void ZK1500_OneDay()
    {
        Assert.Equal(1L, RentalReturnService.LateDays(1500));
        Assert.Equal(178.50m, RentalReturnService.Penalty(1500, 119.00m));
    }

    /*
     * Z-K1501: 1501 min, stawka 119,00 (granica 1500/1501).
     * 1. Doby: (1501 - 60 + 1439) \ 1440 = 2880 \ 1440 = 2 (RentalManager.vb:140).
     * 2. Kara: 2 * 119,00 * 1,5 = 357,00 (:142).
     */
    [Fact(DisplayName = "Z-K1501: 1501 min -> 2 doby, kara 357,00 (granica)")]
    public void ZK1501_SecondDayStarts()
    {
        Assert.Equal(2L, RentalReturnService.LateDays(1501));
        Assert.Equal(357.00m, RentalReturnService.Penalty(1501, 119.00m));
    }

    /*
     * Z-K1560: 1560 min (26 h), stawka 72,35 (= seed wyp. 2: plan 15.10 09:30, zwrot 16.10 11:30,
     * legacy-app-parent/legacy-app/Database/03_seed.sql:151).
     * 1. Doby: (1560 - 60 + 1439) \ 1440 = 2939 \ 1440 = 2 (RentalManager.vb:140).
     * 2. Kara: 2 * 72,35 * 1,5 = 217,05 (:142).
     */
    [Fact(DisplayName = "Z-K1560: 26 h spoznienia -> 2 doby, kara 217,05")]
    public void ZK1560_TwoDays()
    {
        Assert.Equal(2L, RentalReturnService.LateDays(1560));
        Assert.Equal(217.05m, RentalReturnService.Penalty(1560, 72.35m));
    }

    /*
     * Z-K1560-119: 1560 min, stawka 119,00.
     * 1. Minuty: DateDiff(Minute, plan, zwrot) = 26 h = 1560 (RentalManager.vb:138).
     * 2. Doby: (1560 - 60 + 1439) \ 1440 = 2939 \ 1440 = 2 (:140).
     * 3. Kara: 2 * 119,00 * 1,5 = 357,00 (:142).
     */
    [Fact(DisplayName = "Z-K1560-119: 1560 min, stawka 119,00 -> 2 doby, kara 357,00")]
    public void ZK1560_119()
    {
        Assert.Equal(2L, RentalReturnService.LateDays(1560));
        Assert.Equal(357.00m, RentalReturnService.Penalty(1560, 119.00m));
    }

    /*
     * Z-K-150: 1440 min, stawka 119,00.
     * 1. Doby: (1440 - 60 + 1439) \ 1440 = 2819 \ 1440 = 1 (RentalManager.vb:140).
     * 2. Kara: 1 * 119,00 * 1,5 = 178,50 (:142). Komentarz :141 ("100% stawki") dalby 119,00 - nie ufamy komentarzom.
     */
    [Fact(DisplayName = "Z-K-150: mnoznik 1,5 z kodu, nie 100 % z komentarza -> 178,50")]
    public void ZK150_MultiplierFromCodeNotComment()
    {
        Assert.Equal(1L, RentalReturnService.LateDays(1440));
        Assert.Equal(178.50m, RentalReturnService.Penalty(1440, 119.00m));
    }

    /*
     * Z-K-HE: 1440 min, stawka 72,35.
     * 1. Doby: 2819 \ 1440 = 1 (RentalManager.vb:140).
     * 2. Kara: Math.Round(1 * 72,35 * 1,5, 2) = Math.Round(108,525, 2) = 108,52 - polowka do parzystej
     *    (HALF_UP dalby 108,53) (:142).
     */
    [Fact(DisplayName = "Z-K-HE: 108,525 -> 108,52 (HALF_EVEN jak Math.Round)")]
    public void ZKHe_HalfEven()
    {
        Assert.Equal(108.52m, RentalReturnService.Penalty(1440, 72.35m));
    }

    /*
     * Z-KAUCJA-0: kaucja 400,00, kara 404,25 (7 dob * 38,50 * 1,5), szkody 20,00.
     * 400,00 - 404,25 - 20,00 = -24,25 < 0 -> 0 (RentalManager.vb:111-112).
     */
    [Fact(DisplayName = "Z-KAUCJA-0: kara i szkody ponad kaucje -> zwrot kaucji 0,00")]
    public void ZKaucja0_NeverNegative()
    {
        decimal refund = RentalReturnService.DepositReturned(400.00m, 404.25m, 20.00m);

        Assert.Equal(0.00m, refund);
    }

    /*
     * Z-KAUCJA: kaucja 800,00 (wyp. 6), kara 357,00 (Z-K1560-119), szkody 0.
     * 800,00 - 357,00 - 0 = 443,00 (RentalManager.vb:111).
     */
    [Fact(DisplayName = "Z-KAUCJA: 800,00 - 357,00 - 0 -> zwrot kaucji 443,00")]
    public void ZKaucja_Remainder()
    {
        decimal refund = RentalReturnService.DepositReturned(800.00m, 357.00m, 0m);

        Assert.Equal(443.00m, refund);
    }

    /*
     * Z-SZKODY: [CASING, ACCESSORIES, DIRT] = obudowa 50 + akcesoria 30 + zabrudzenie 20 = 100,00
     * (legacy-app-parent/legacy-app/WypozyczalniaWeb/Zwrot.aspx.vb:224-226, sumowanie :230-233).
     */
    [Fact(DisplayName = "Z-SZKODY: obudowa + akcesoria + zabrudzenie -> 100,00")]
    public void ZSzkody_PriceList()
    {
        decimal total = RentalReturnService.DamagesTotal(["CASING", "ACCESSORIES", "DIRT"]);

        Assert.Equal(100.00m, total);
    }
}
