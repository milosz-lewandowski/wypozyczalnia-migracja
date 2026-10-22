using System.Text.Json.Serialization;
using Wypozyczalnia.Model;

namespace Wypozyczalnia.Dto;

/// <summary>Odpowiedz GET /api/pricing/quote. Daty ISO bez strefy (yyyy-MM-ddTHH:mm), kwoty skala 2.</summary>
public sealed record QuoteResponse(
    long EquipmentId,
    [property: JsonConverter(typeof(MinuteDateTimeJsonConverter))] DateTime From,
    [property: JsonConverter(typeof(MinuteDateTimeJsonConverter))] DateTime To,
    long? CustomerId,
    int Days,
    int WeekendDays,
    decimal DailyRate,
    decimal WeekendRate,
    decimal WeeklyDiscountPercent,
    int CustomerDiscountPercent,
    decimal Amount)
{
    public static QuoteResponse FromModel(Quote quote) => new(
        quote.EquipmentId, quote.From, quote.To, quote.CustomerId,
        quote.Days, quote.WeekendDays, quote.DailyRate, quote.WeekendRate,
        quote.WeeklyDiscountPercent, quote.CustomerDiscountPercent, quote.Amount);
}
