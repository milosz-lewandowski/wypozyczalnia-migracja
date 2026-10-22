using System.Text.Json.Serialization;
using Wypozyczalnia.Model;

namespace Wypozyczalnia.Dto;

/// <summary>Odpowiedz POST /api/rentals/{id}/return: rozliczenie zwrotu, data yyyy-MM-ddTHH:mm, kwoty skala 2.</summary>
public sealed record ReturnResponse(
    long RentalId,
    [property: JsonConverter(typeof(MinuteDateTimeJsonConverter))] DateTime ReturnedAt,
    decimal Fee,
    long LateMinutes,
    long LateDays,
    decimal Penalty,
    decimal DamagesTotal,
    decimal DepositTaken,
    decimal DepositReturned,
    RentalStatus Status)
{
    public static ReturnResponse FromModel(ReturnResult result) => new(
        result.RentalId, result.ReturnedAt, result.Fee, result.LateMinutes, result.LateDays, result.Penalty,
        result.DamagesTotal, result.DepositTaken, result.DepositReturned, result.Status);
}
