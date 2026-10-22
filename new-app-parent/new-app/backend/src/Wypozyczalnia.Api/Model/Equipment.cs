namespace Wypozyczalnia.Model;

/// <summary>Sprzet do wypozyczenia (tabela equipment; legacy: Sprzet, legacy-app-parent/legacy-app/Database/01_schema.sql:46-56).</summary>
public sealed record Equipment(
    long Id,
    string InventoryNumber,
    string Name,
    string Category,
    decimal DailyRate,
    decimal Deposit,
    EquipmentStatus Status);
