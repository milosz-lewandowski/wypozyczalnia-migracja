using Wypozyczalnia.Model;
using Wypozyczalnia.Repositories;
using Wypozyczalnia.Services;
using Wypozyczalnia.Tests.Support;

namespace Wypozyczalnia.Tests.Characterization;

/// <summary>
/// Testy przeplywu zwrotu przez serwis (RegisterReturn z data zwrotu) na danych z data.sql
/// (= legacy-app-parent/legacy-app/Database/03_seed.sql). Asercje: oplata, status, zwrot kaucji, stan sprzetu.
/// Kary NIE sa tu asertowane (zasada: kwoty kary tylko w RentalReturnServiceTest, wejscie w minutach).
/// Zwrot zmienia baze, wiec kazdy test dostaje wlasna baze w pamieci (TestApplication).
/// Reguly:
/// - OPLATA: wycena za okres wydanie -&gt; PLANOWANY zwrot, nie faktyczny (legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:109);
///   wycena wg legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:31-56 (stawka weekendowa :36, doby k w sb/nd :38-45,
///   rabat klienta :50-54, CInt(prog * 2.5) :58-60);
/// - KAUCJA: kaucja - kara - szkody, minimum 0 (RentalManager.vb:111-112);
/// - ZAPIS: wypozyczenie zamkniete, sprzet wraca do statusu dostepny (legacy-app-parent/legacy-app/Database/02_procedury.sql:240-252).
/// </summary>
public sealed class RentalReturnServiceFlowTest : IDisposable
{
    private readonly TestApplication application = new();
    private readonly RentalReturnService rentalReturnService;
    private readonly EquipmentRepository equipmentRepository;

    public RentalReturnServiceFlowTest()
    {
        rentalReturnService = application.Get<RentalReturnService>();
        equipmentRepository = application.Get<EquipmentRepository>();
    }

    public void Dispose() => application.Dispose();

    /*
     * Z-FLOW-1: wyp. 6 (klient 12 prog 0, sprzet 7 OGR 119,00, wydanie sb 2026-10-17 10:00, plan nd 2026-10-18 10:00),
     * zwrot pn 2026-10-19 12:00, bez szkod.
     * 1. Oplata za wydanie -> plan (RentalManager.vb:109): 1440 min = 1 doba (PricingManager.vb:22-28).
     * 2. Doba k = 0 zaczyna sie w sobote -> stawka weekendowa OGR 2026: Math.Round(119,00 * 1,20, 2) = 142,80
     *    (PricingManager.vb:36-41, taryfa 03_seed.sql:104-113). Prog 0 -> bez rabatu. Oplata 142,80.
     * 3. Zapis: status CLOSED, sprzet 7 -> AVAILABLE (02_procedury.sql:240-252).
     */
    [Fact(DisplayName = "Z-FLOW-1: wyp. 6 -> oplata 142,80 (doba w sobote), zamkniete, sprzet 7 dostepny")]
    public void ZFlow1_Rental6()
    {
        ReturnResult result = rentalReturnService.RegisterReturn(6, At(2026, 10, 19, 12, 0), []);

        Assert.Equal(142.80m, result.Fee);
        Assert.Equal(RentalStatus.CLOSED, result.Status);
        Assert.Equal(RentalStatus.CLOSED, rentalReturnService.GetRental(6).Status);
        Assert.Equal(EquipmentStatus.AVAILABLE, equipmentRepository.FindById(7)!.Status);
    }

    /*
     * Z-FLOW-2: wyp. 5 (klient 9 prog 2, sprzet 12 IMP 60,25, kaucja 200,00, wydanie pt 2026-10-16 09:00,
     * plan pn 2026-10-19 09:00), zwrot 2026-10-19 09:45, szkoda CASING.
     * 1. Oplata: 4320 min = 3 doby: pt, sb, nd (PricingManager.vb:22-28, :38-45).
     * 2. Weekend IMP 2026: Math.Round(60,25 * 1,50, 2) = Math.Round(90,375, 2) = 90,38 (PricingManager.vb:36).
     * 3. Suma 60,25 + 2 * 90,38 = 241,01; 3 \ 7 = 0 -> bez rabatu tygodniowego (:46).
     * 4. Prog 2 -> CInt(5,0) = 5 % (:58-60); Math.Round(241,01 * 95 / 100, 2) = Math.Round(228,9595, 2) = 228,96 (:53).
     * 5. Spoznienie 45 min <= 60 -> kara 0 (RentalManager.vb:139); szkody: obudowa 50 (Zwrot.aspx.vb:224).
     * 6. Zwrot kaucji: 200,00 - 0 - 50,00 = 150,00 (RentalManager.vb:111).
     */
    [Fact(DisplayName = "Z-FLOW-2: wyp. 5 -> oplata 228,96, zwrot kaucji 150,00 (obudowa, 45 min w tolerancji)")]
    public void ZFlow2_Rental5WithCasingDamage()
    {
        ReturnResult result = rentalReturnService.RegisterReturn(5, At(2026, 10, 19, 9, 45), ["CASING"]);

        Assert.Equal(228.96m, result.Fee);
        Assert.Equal(150.00m, result.DepositReturned);
        Assert.Equal(RentalStatus.CLOSED, result.Status);
    }

    /*
     * Z-FLOW-3: wyp. 4 (klient 10 prog 3, sprzet 4 BUD 38,50, kaucja 400,00, wydanie 2026-10-01 08:00,
     * plan 2026-10-15 08:00), zwrot 2026-10-22 08:00, szkoda DIRT.
     * 1. Oplata = wycena W-F (zwrot korzysta z wyceny, RentalManager.vb:109): 14 dob, 4 weekendowe;
     *    weekend BUD 2026 Math.Round(38,50 * 1,25, 2) = Math.Round(48,125, 2) = 48,12; 10 * 38,50 + 4 * 48,12 = 577,48;
     *    rabat tyg. Math.Round(57,748, 2) = 57,75 -> 519,73; prog 3 -> CInt(7,5) = 8 %;
     *    Math.Round(519,73 * 92 / 100, 2) = Math.Round(478,1516, 2) = 478,15 (PricingManager.vb:36-54, :58-60).
     * 2. Kaucja: kara za 7 dob spoznienia (404,25) + szkody 20,00 > 400,00 -> zwrot 0,00 (RentalManager.vb:111-112).
     */
    [Fact(DisplayName = "Z-FLOW-3: wyp. 4 -> oplata 478,15 (= wycena W-F), zwrot kaucji 0,00")]
    public void ZFlow3_Rental4DepositUsedUp()
    {
        ReturnResult result = rentalReturnService.RegisterReturn(4, At(2026, 10, 22, 8, 0), ["DIRT"]);

        Assert.Equal(478.15m, result.Fee);
        Assert.Equal(0.00m, result.DepositReturned);
        Assert.Equal(RentalStatus.CLOSED, result.Status);
    }

    /* Z-ZAMK: wyp. 1 jest zamkniete w seedzie (03_seed.sql:149) -> blad z legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:95-97. */
    [Fact(DisplayName = "Z-ZAMK: zamkniete wypozyczenie -> ConflictException")]
    public void ZZamk_AlreadyClosed()
    {
        var exception = Assert.Throws<ConflictException>(() =>
            rentalReturnService.RegisterReturn(1, At(2026, 10, 19, 12, 0), []));

        Assert.Equal("Wypozyczenie 1 jest juz zamkniete", exception.Message);
    }

    /* Z-404: nieznane wypozyczenie -> blad z legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:92-94. */
    [Fact(DisplayName = "Z-404: nieznane wypozyczenie -> NotFoundException")]
    public void Z404_UnknownRental()
    {
        var exception = Assert.Throws<NotFoundException>(() =>
            rentalReturnService.RegisterReturn(999, At(2026, 10, 19, 12, 0), []));

        Assert.Equal("Nie znaleziono wypozyczenia 999", exception.Message);
    }

    private static DateTime At(int year, int month, int day, int hour, int minute) => new(year, month, day, hour, minute, 0);
}
