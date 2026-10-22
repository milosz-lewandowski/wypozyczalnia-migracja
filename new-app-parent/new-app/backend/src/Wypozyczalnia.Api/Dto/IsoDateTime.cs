using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Wypozyczalnia.Dto;

/// <summary>
/// Daty API: ISO bez strefy. Wejscie "yyyy-MM-ddTHH:mm", sekundy i ulamki sekund opcjonalne
/// (jak LocalDateTime w Javie); data bez godziny jest bledem.
/// </summary>
public static class IsoDateTime
{
    private static readonly string[] InputFormats =
    [
        "yyyy-MM-dd'T'HH:mm",
        "yyyy-MM-dd'T'HH:mm:ss",
        "yyyy-MM-dd'T'HH:mm:ss.FFFFFFF"
    ];

    public static bool TryParse(string? text, out DateTime value) =>
        DateTime.TryParseExact(text, InputFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out value);
}

/// <summary>Odczyt daty z ciala zadania JSON (format jak <see cref="IsoDateTime"/>); zly format = blad odczytu zadania.</summary>
public sealed class IsoDateTimeJsonConverter : JsonConverter<DateTime?>
{
    public override bool HandleNull => true;

    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }
        if (reader.TokenType == JsonTokenType.String && IsoDateTime.TryParse(reader.GetString(), out DateTime value))
        {
            return value;
        }
        throw new JsonException("Niepoprawna data");
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options) =>
        throw new NotSupportedException("Konwerter tylko do odczytu zadan");
}

/// <summary>Zapis daty w odpowiedzi jako "yyyy-MM-ddTHH:mm" (bez sekund, jak w kontrakcie API).</summary>
public sealed class MinuteDateTimeJsonConverter : JsonConverter<DateTime>
{
    private const string Format = "yyyy-MM-dd'T'HH:mm";

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        DateTime.ParseExact(reader.GetString()!, Format, CultureInfo.InvariantCulture);

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToString(Format, CultureInfo.InvariantCulture));
}
