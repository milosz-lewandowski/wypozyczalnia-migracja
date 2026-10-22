namespace Wypozyczalnia.Model;

/// <summary>
/// Otwarte wypozyczenie na liscie do zwrotu, z nazwa klienta i sprzetu
/// (legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/RentalDAL.vb:68-78, Otwarte).
/// </summary>
public sealed record OpenRental(
    long Id,
    long CustomerId,
    string CustomerName,
    long EquipmentId,
    string EquipmentName,
    DateTime IssuedAt,
    DateTime PlannedReturn,
    decimal DepositTaken);
