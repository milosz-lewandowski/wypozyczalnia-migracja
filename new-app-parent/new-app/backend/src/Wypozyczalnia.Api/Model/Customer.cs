namespace Wypozyczalnia.Model;

/// <summary>
/// Klient (tabela customers; legacy: Klienci, legacy-app-parent/legacy-app/Database/01_schema.sql:28-42).
/// DiscountTier = ProgRabatu z bazy, bez przyciecia (przycina serwis, jak CustomerManager.vb:47-48).
/// </summary>
public sealed record Customer(
    long Id,
    string FirstName,
    string LastName,
    string? CompanyName,
    int DiscountTier,
    bool Blocked);
