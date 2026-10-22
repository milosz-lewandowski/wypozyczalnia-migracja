using System.Globalization;

namespace Wypozyczalnia.Repositories.Sqlite;

/// <summary>
/// Daty i czasy w bazie jako TEXT "yyyy-MM-dd HH:mm:ss" - ten sam format w skryptach i w zapisie z kodu,
/// bo porownania zakresow (date_from &lt; ...) ida tekstowo.
/// </summary>
public static class DbDateTime
{
    private const string Format = "yyyy-MM-dd HH:mm:ss";

    public static string ToDb(DateTime value) => value.ToString(Format, CultureInfo.InvariantCulture);

    public static DateTime FromDb(string value) => DateTime.ParseExact(value, Format, CultureInfo.InvariantCulture);
}
