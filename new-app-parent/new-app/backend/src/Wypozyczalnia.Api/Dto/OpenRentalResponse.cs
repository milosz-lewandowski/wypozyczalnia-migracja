using System.Text.Json.Serialization;
using Wypozyczalnia.Model;

namespace Wypozyczalnia.Dto;

/// <summary>Pozycja GET /api/rentals (otwarte wypozyczenia). Daty yyyy-MM-ddTHH:mm, kwoty skala 2.</summary>
public sealed record OpenRentalResponse(
    long Id,
    long CustomerId,
    string CustomerName,
    long EquipmentId,
    string EquipmentName,
    [property: JsonConverter(typeof(MinuteDateTimeJsonConverter))] DateTime IssuedAt,
    [property: JsonConverter(typeof(MinuteDateTimeJsonConverter))] DateTime PlannedReturn,
    decimal DepositTaken)
{
    public static OpenRentalResponse FromModel(OpenRental rental) => new(
        rental.Id, rental.CustomerId, rental.CustomerName, rental.EquipmentId, rental.EquipmentName,
        rental.IssuedAt, rental.PlannedReturn, rental.DepositTaken);
}
