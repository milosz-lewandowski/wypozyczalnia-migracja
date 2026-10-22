namespace Wypozyczalnia.Model;

/// <summary>
/// Taryfa kategorii sprzetu (tabela tariffs; legacy: Taryfy, legacy-app-parent/legacy-app/Database/01_schema.sql).
/// Obowiazuje od ValidFrom do dnia przed nastepna taryfa tej samej kategorii.
/// </summary>
public sealed record Tariff(
    string Category,
    decimal WeekendMultiplier,
    decimal WeeklyDiscountPercent,
    DateOnly ValidFrom);
