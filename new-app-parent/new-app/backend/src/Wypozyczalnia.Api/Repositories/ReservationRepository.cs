using Microsoft.Data.Sqlite;
using Wypozyczalnia.Model;
using Wypozyczalnia.Repositories.Sqlite;

namespace Wypozyczalnia.Repositories;

/// <summary>Rezerwacje (tabela reservations). Odpowiednik legacy: ReservationDAL (WypozyczalniaCore/DAL/ReservationDAL.vb).</summary>
public class ReservationRepository(SqliteDatabase database)
{
    private const string Columns = "id, customer_id, equipment_id, date_from, date_to, status, estimated_price, notes";

    /// <summary>
    /// Zapis nowej rezerwacji ze statusem NEW i czasem utworzenia z bazy
    /// (legacy-app-parent/legacy-app/Database/02_procedury.sql:100-103, usp_ZapiszRezerwacje). Zwraca nadane Id.
    /// </summary>
    public long Insert(long customerId, long equipmentId, DateTime from, DateTime to, decimal estimatedPrice, string? notes)
    {
        using var connection = database.OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = """
            INSERT INTO reservations (customer_id, equipment_id, date_from, date_to, status,
                                      estimated_price, notes, created_at)
            VALUES (@customerId, @equipmentId, @dateFrom, @dateTo, 'NEW',
                    @estimatedPrice, @notes, CURRENT_TIMESTAMP)
            RETURNING id
            """;
        command.Parameters.AddWithValue("@customerId", customerId);
        command.Parameters.AddWithValue("@equipmentId", equipmentId);
        command.Parameters.AddWithValue("@dateFrom", DbDateTime.ToDb(from));
        command.Parameters.AddWithValue("@dateTo", DbDateTime.ToDb(to));
        command.Parameters.AddWithValue("@estimatedPrice", estimatedPrice);
        command.Parameters.AddWithValue("@notes", (object?)notes ?? DBNull.Value);
        return Convert.ToInt64(command.ExecuteScalar());
    }

    public Reservation? FindById(long id)
    {
        using var connection = database.OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = $"SELECT {Columns} FROM reservations WHERE id = @id";
        command.Parameters.AddWithValue("@id", id);
        using var reader = command.ExecuteReader();
        return reader.Read() ? Map(reader) : null;
    }

    /// <summary>Wszystkie rezerwacje (kazdy status), sortowanie po Id.</summary>
    public List<Reservation> FindAll()
    {
        using var connection = database.OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = $"SELECT {Columns} FROM reservations ORDER BY id";
        return ReadAll(command);
    }

    /// <summary>Rezerwacje klienta (kazdy status, jak ReservationDAL.vb:60-66), sortowanie po Id.</summary>
    public List<Reservation> FindByCustomer(long customerId)
    {
        using var connection = database.OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = $"SELECT {Columns} FROM reservations WHERE customer_id = @customerId ORDER BY id";
        command.Parameters.AddWithValue("@customerId", customerId);
        return ReadAll(command);
    }

    private static List<Reservation> ReadAll(SqliteCommand command)
    {
        using var reader = command.ExecuteReader();
        var result = new List<Reservation>();
        while (reader.Read())
        {
            result.Add(Map(reader));
        }
        return result;
    }

    private static Reservation Map(SqliteDataReader reader) => new(
        reader.GetInt64(reader.GetOrdinal("id")),
        reader.GetInt64(reader.GetOrdinal("customer_id")),
        reader.GetInt64(reader.GetOrdinal("equipment_id")),
        DbDateTime.FromDb(reader.GetString(reader.GetOrdinal("date_from"))),
        DbDateTime.FromDb(reader.GetString(reader.GetOrdinal("date_to"))),
        Enum.Parse<ReservationStatus>(reader.GetString(reader.GetOrdinal("status"))),
        // SQLite nie pamieta skali DECIMAL(10,2) (0.00 zapisuje jako 0) - Money.Round przywraca dwa miejsca
        Money.Round(reader.GetDecimal(reader.GetOrdinal("estimated_price"))),
        reader.IsDBNull(reader.GetOrdinal("notes")) ? null : reader.GetString(reader.GetOrdinal("notes")));
}
