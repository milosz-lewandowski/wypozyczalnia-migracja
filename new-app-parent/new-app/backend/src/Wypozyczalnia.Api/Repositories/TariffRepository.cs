using System.Globalization;
using Microsoft.Data.Sqlite;
using Wypozyczalnia.Model;
using Wypozyczalnia.Repositories.Sqlite;

namespace Wypozyczalnia.Repositories;

/// <summary>Taryfy (tabela tariffs). Odpowiednik legacy: PricingDAL.PobierzTaryfe + usp_PobierzTaryfe.</summary>
public class TariffRepository(SqliteDatabase database)
{
    /// <summary>Kolumna valid_from jest w bazie tekstem "yyyy-MM-dd", wiec parametr dnia ma ten sam format (porownanie tekstowe).</summary>
    private const string DayFormat = "yyyy-MM-dd";

    /// <summary>
    /// Taryfa kategorii obowiazujaca w danym dniu: najnowsza z valid_from &lt;= dzien
    /// (legacy-app-parent/legacy-app/Database/02_procedury.sql:281-290, wolana z WypozyczalniaCore/DAL/PricingDAL.vb:15-17).
    /// </summary>
    public Tariff? FindValidOn(string category, DateOnly day)
    {
        using var connection = database.OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = """
            SELECT category, weekend_multiplier, weekly_discount_percent, valid_from
            FROM tariffs
            WHERE category = @category
              AND valid_from <= @day
            ORDER BY valid_from DESC
            LIMIT 1
            """;
        command.Parameters.AddWithValue("@category", category.Trim());
        command.Parameters.AddWithValue("@day", day.ToString(DayFormat, CultureInfo.InvariantCulture));
        using var reader = command.ExecuteReader();
        return reader.Read() ? Map(reader) : null;
    }

    private static Tariff Map(SqliteDataReader reader) => new(
        reader.GetString(reader.GetOrdinal("category")).Trim(),
        // SQLite nie pamieta skali DECIMAL (1.20 zapisuje jako 1.2) - Money.Round przywraca dwa miejsca
        Money.Round(reader.GetDecimal(reader.GetOrdinal("weekend_multiplier"))),
        Money.Round(reader.GetDecimal(reader.GetOrdinal("weekly_discount_percent"))),
        DateOnly.ParseExact(reader.GetString(reader.GetOrdinal("valid_from")), DayFormat, CultureInfo.InvariantCulture));
}
