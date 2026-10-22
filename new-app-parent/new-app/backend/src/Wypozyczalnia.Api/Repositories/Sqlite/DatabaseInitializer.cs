using System.Text.RegularExpressions;

namespace Wypozyczalnia.Repositories.Sqlite;

/// <summary>
/// Zaladowanie bazy przy starcie: Database/schema.sql, Database/data.sql (katalog aplikacji),
/// potem dodatkowe skrypty z Database:ExtraDataScripts (sciezki bezwzgledne rozdzielone ';', w podanej kolejnosci).
/// Loader usuwa slowa kluczowe literalow H2 TIMESTAMP '...' i DATE '...' (zostaje sam tekst w apostrofach),
/// dzieki czemu skrypty danych w skladni H2 (np. tools/bramka/dane-zwrot.sql) laduja sie bez zmian.
/// </summary>
public sealed partial class DatabaseInitializer(SqliteDatabase database, IConfiguration configuration)
{
    public void Initialize()
    {
        string scriptsDirectory = Path.Combine(AppContext.BaseDirectory, "Database");
        ExecuteScript(Path.Combine(scriptsDirectory, "schema.sql"));
        ExecuteScript(Path.Combine(scriptsDirectory, "data.sql"));

        string extraScripts = configuration["Database:ExtraDataScripts"] ?? string.Empty;
        foreach (string path in extraScripts.Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
        {
            ExecuteScript(path);
        }
    }

    private void ExecuteScript(string path)
    {
        string sql = H2Literal().Replace(File.ReadAllText(path), "'");

        using var connection = database.OpenConnection();
        using var transaction = connection.BeginTransaction();
        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = sql;
        command.ExecuteNonQuery();
        transaction.Commit();
    }

    /// <summary>Slowo kluczowe literalu H2 przed apostrofem: TIMESTAMP '2026-10-19 10:00:00' -> '2026-10-19 10:00:00'.</summary>
    [GeneratedRegex(@"\b(?:TIMESTAMP|DATE)\s+'", RegexOptions.IgnoreCase)]
    private static partial Regex H2Literal();
}
