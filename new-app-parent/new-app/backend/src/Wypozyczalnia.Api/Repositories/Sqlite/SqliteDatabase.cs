using Microsoft.Data.Sqlite;

namespace Wypozyczalnia.Repositories.Sqlite;

/// <summary>
/// Baza SQLite w pamieci, wspoldzielona miedzy polaczeniami (Mode=Memory;Cache=Shared).
/// Nazwa bazy z konfiguracji Database:Name (domyslnie "wypozyczalnia"; testy podaja wlasna, zeby miec osobna baze).
/// Baza w pamieci zyje, dopoki jest otwarte choc jedno polaczenie - dlatego klasa trzyma jedno przez caly czas zycia aplikacji.
/// </summary>
public sealed class SqliteDatabase : IDisposable
{
    private readonly string connectionString;
    private readonly SqliteConnection keepAliveConnection;

    public SqliteDatabase(IConfiguration configuration)
    {
        string name = configuration["Database:Name"] is { Length: > 0 } configured ? configured : "wypozyczalnia";
        connectionString = new SqliteConnectionStringBuilder
        {
            DataSource = name,
            Mode = SqliteOpenMode.Memory,
            Cache = SqliteCacheMode.Shared
        }.ToString();
        keepAliveConnection = new SqliteConnection(connectionString);
        keepAliveConnection.Open();
    }

    /// <summary>Nowe otwarte polaczenie do bazy; wolajacy zamyka je przez using.</summary>
    public SqliteConnection OpenConnection()
    {
        var connection = new SqliteConnection(connectionString);
        connection.Open();
        // Klucze obce jak w H2 (w SQLite trzeba je wlaczyc na kazdym polaczeniu).
        using var pragma = connection.CreateCommand();
        pragma.CommandText = "PRAGMA foreign_keys = ON";
        pragma.ExecuteNonQuery();
        return connection;
    }

    public void Dispose() => keepAliveConnection.Dispose();
}
