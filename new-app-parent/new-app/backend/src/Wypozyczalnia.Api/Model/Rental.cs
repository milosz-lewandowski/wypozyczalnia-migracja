namespace Wypozyczalnia.Model;

/// <summary>
/// Wypozyczenie (tabela rentals; legacy: Wypozyczenia, kolumny jak legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/RentalDAL.vb:14-16).
/// PlannedReturn = planowany zwrot (od niego liczy sie spoznienie i do niego oplata).
/// </summary>
public sealed record Rental(
    long Id,
    long? ReservationId,
    long CustomerId,
    long EquipmentId,
    DateTime IssuedAt,
    DateTime PlannedReturn,
    decimal DepositTaken,
    RentalStatus Status);
