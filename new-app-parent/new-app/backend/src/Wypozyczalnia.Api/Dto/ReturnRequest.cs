using System.Text.Json.Serialization;

namespace Wypozyczalnia.Dto;

/// <summary>
/// Zadanie POST /api/rentals/{id}/return: {"returnedAt", "damages"?}. Data i godzina faktycznego zwrotu
/// (yyyy-MM-ddTHH:mm, sekundy opcjonalne) oraz kody szkod z cennika (CASING, ACCESSORIES, DIRT).
/// ReturnedAt nullowalne, bo brak pola zglasza kontroler komunikatem "Brak pola: returnedAt"; damages opcjonalne, brak = brak szkod.
/// </summary>
public sealed record ReturnRequest(
    [property: JsonConverter(typeof(IsoDateTimeJsonConverter))] DateTime? ReturnedAt,
    List<string>? Damages)
{
    public IReadOnlyList<string> DamagesOrEmpty() => Damages ?? [];
}
