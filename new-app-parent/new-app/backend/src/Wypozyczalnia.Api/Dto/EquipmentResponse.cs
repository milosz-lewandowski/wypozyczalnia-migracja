using Wypozyczalnia.Model;

namespace Wypozyczalnia.Dto;

/// <summary>Pozycja odpowiedzi GET /api/equipment/available. Kwoty skala 2.</summary>
public sealed record EquipmentResponse(
    long Id,
    string InventoryNumber,
    string Name,
    string Category,
    decimal DailyRate,
    decimal Deposit)
{
    public static EquipmentResponse FromModel(Equipment equipment) => new(
        equipment.Id, equipment.InventoryNumber, equipment.Name,
        equipment.Category, equipment.DailyRate, equipment.Deposit);
}
