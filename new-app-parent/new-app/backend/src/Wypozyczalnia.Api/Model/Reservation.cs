namespace Wypozyczalnia.Model;

/// <summary>
/// Rezerwacja sprzetu na termin (tabela reservations; legacy: Rezerwacje, legacy-app-parent/legacy-app/Database/01_schema.sql:73-85).
/// EstimatedPrice = CenaSzacunkowa z wyceny przy zapisie (legacy-app-parent/legacy-app/WypozyczalniaCore/ReservationManager.vb:38-39).
/// </summary>
public sealed record Reservation(
    long Id,
    long CustomerId,
    long EquipmentId,
    DateTime From,
    DateTime To,
    ReservationStatus Status,
    decimal EstimatedPrice,
    string? Notes);
