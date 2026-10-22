namespace Wypozyczalnia.Model;

/// <summary>
/// Wynik wyceny wynajmu. WeeklyDiscountPercent = rabat tygodniowy zastosowany (0, gdy mniej niz 7 dob),
/// CustomerDiscountPercent = rabat progu klienta (0/2/5/8), CustomerId = null, gdy wycena bez klienta.
/// </summary>
public sealed record Quote(
    long EquipmentId,
    DateTime From,
    DateTime To,
    long? CustomerId,
    int Days,
    int WeekendDays,
    decimal DailyRate,
    decimal WeekendRate,
    decimal WeeklyDiscountPercent,
    int CustomerDiscountPercent,
    decimal Amount);
