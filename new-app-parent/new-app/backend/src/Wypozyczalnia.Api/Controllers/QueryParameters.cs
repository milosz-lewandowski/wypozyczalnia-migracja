using System.Globalization;
using Wypozyczalnia.Dto;
using Wypozyczalnia.Services;

namespace Wypozyczalnia.Controllers;

/// <summary>
/// Odczyt parametrow zapytania z komunikatami zgodnymi z kontraktem API:
/// brak albo pusta wartosc -> "Brak parametru: X", zly format -> "Niepoprawna wartosc parametru: X" (oba HTTP 400).
/// </summary>
public static class QueryParameters
{
    public static string RequireText(string? value, string name) =>
        value ?? throw new InvalidRequestException($"Brak parametru: {name}");

    public static DateTime RequireDateTime(string? value, string name)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidRequestException($"Brak parametru: {name}");
        }
        if (!IsoDateTime.TryParse(value, out DateTime result))
        {
            throw new InvalidRequestException($"Niepoprawna wartosc parametru: {name}");
        }
        return result;
    }

    /// <summary>Parametr opcjonalny: brak albo pusta wartosc = null.</summary>
    public static long? OptionalLong(string? value, string name)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }
        if (!long.TryParse(value.Trim(), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out long result))
        {
            throw new InvalidRequestException($"Niepoprawna wartosc parametru: {name}");
        }
        return result;
    }
}
