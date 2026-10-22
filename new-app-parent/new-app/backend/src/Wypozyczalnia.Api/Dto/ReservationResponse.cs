using System.Text.Json.Serialization;
using Wypozyczalnia.Model;

namespace Wypozyczalnia.Dto;

/// <summary>Rezerwacja w odpowiedziach POST i GET /api/reservations. Daty yyyy-MM-ddTHH:mm, kwota skala 2.</summary>
public sealed record ReservationResponse(
    long Id,
    long CustomerId,
    long EquipmentId,
    [property: JsonConverter(typeof(MinuteDateTimeJsonConverter))] DateTime From,
    [property: JsonConverter(typeof(MinuteDateTimeJsonConverter))] DateTime To,
    ReservationStatus Status,
    decimal EstimatedPrice,
    string? Notes)
{
    public static ReservationResponse FromModel(Reservation reservation) => new(
        reservation.Id, reservation.CustomerId, reservation.EquipmentId,
        reservation.From, reservation.To, reservation.Status, reservation.EstimatedPrice,
        reservation.Notes);
}
