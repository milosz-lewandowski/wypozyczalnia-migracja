using Microsoft.Data.Sqlite;
using Wypozyczalnia.Model;
using Wypozyczalnia.Repositories.Sqlite;

namespace Wypozyczalnia.Repositories;

/// <summary>Wypozyczenia (tabela rentals). Odpowiednik legacy: DAL/RentalDAL.vb + procedura usp_ZarejestrujZwrot.</summary>
public class RentalRepository(SqliteDatabase database)
{
    private const string Columns =
        "id, reservation_id, customer_id, equipment_id, issued_at, planned_return, deposit_taken, status";

    /// <summary>
    /// Otwarte wypozyczenia z nazwa klienta i sprzetu (legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/RentalDAL.vb:68-78).
    /// Nazwa klienta: firma, gdy podana, inaczej imie i nazwisko (SqlNazwaKlienta, RentalDAL.vb:18-19; w nowym schemacie
    /// brak kolumny Typ, decyduje company_name). Sortowanie po id (kontrakt API), legacy sortowalo po PlanowanyZwrot.
    /// </summary>
    public List<OpenRental> FindOpen()
    {
        using var connection = database.OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = """
            SELECT r.id, r.customer_id,
                   CASE WHEN c.company_name IS NOT NULL THEN c.company_name
                        ELSE c.first_name || ' ' || c.last_name END AS customer_name,
                   r.equipment_id, e.name AS equipment_name,
                   r.issued_at, r.planned_return, r.deposit_taken
            FROM rentals r
            JOIN customers c ON c.id = r.customer_id
            JOIN equipment e ON e.id = r.equipment_id
            WHERE r.status = 'OPEN'
            ORDER BY r.id
            """;
        using var reader = command.ExecuteReader();
        var result = new List<OpenRental>();
        while (reader.Read())
        {
            result.Add(MapOpen(reader));
        }
        return result;
    }

    /// <summary>
    /// Wypozyczenie po Id (legacy: RentalDAL.PobierzWypozyczenie,
    /// legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/RentalDAL.vb:56-60, kolumny :14-16).
    /// </summary>
    public Rental? FindById(long id)
    {
        using var connection = database.OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = $"SELECT {Columns} FROM rentals WHERE id = @id";
        command.Parameters.AddWithValue("@id", id);
        using var reader = command.ExecuteReader();
        return reader.Read() ? Map(reader) : null;
    }

    /// <summary>
    /// Zapis zwrotu: zamkniecie wypozyczenia z kwotami i zwolnienie sprzetu - dwa UPDATE w jednej transakcji
    /// na jednym polaczeniu, jak legacy-app-parent/legacy-app/Database/02_procedury.sql:240-252 (w Javie: @Transactional).
    /// </summary>
    public void RegisterReturn(long id, DateTime returnedAt, decimal fee, decimal penalty, decimal depositReturned)
    {
        using var connection = database.OpenConnection();
        using var transaction = connection.BeginTransaction();

        using (var closeRental = connection.CreateCommand())
        {
            closeRental.Transaction = transaction;
            closeRental.CommandText = """
                UPDATE rentals
                SET returned_at = @returnedAt,
                    fee = @fee,
                    penalty = @penalty,
                    deposit_returned = @depositReturned,
                    status = 'CLOSED'
                WHERE id = @id
                """;
            closeRental.Parameters.AddWithValue("@returnedAt", DbDateTime.ToDb(returnedAt));
            closeRental.Parameters.AddWithValue("@fee", fee);
            closeRental.Parameters.AddWithValue("@penalty", penalty);
            closeRental.Parameters.AddWithValue("@depositReturned", depositReturned);
            closeRental.Parameters.AddWithValue("@id", id);
            closeRental.ExecuteNonQuery();
        }

        using (var releaseEquipment = connection.CreateCommand())
        {
            releaseEquipment.Transaction = transaction;
            releaseEquipment.CommandText = """
                UPDATE equipment
                SET status = 'AVAILABLE'
                WHERE id = (SELECT equipment_id FROM rentals WHERE id = @id)
                """;
            releaseEquipment.Parameters.AddWithValue("@id", id);
            releaseEquipment.ExecuteNonQuery();
        }

        transaction.Commit();
    }

    private static OpenRental MapOpen(SqliteDataReader reader) => new(
        reader.GetInt64(reader.GetOrdinal("id")),
        reader.GetInt64(reader.GetOrdinal("customer_id")),
        reader.GetString(reader.GetOrdinal("customer_name")),
        reader.GetInt64(reader.GetOrdinal("equipment_id")),
        reader.GetString(reader.GetOrdinal("equipment_name")),
        DbDateTime.FromDb(reader.GetString(reader.GetOrdinal("issued_at"))),
        DbDateTime.FromDb(reader.GetString(reader.GetOrdinal("planned_return"))),
        // SQLite nie pamieta skali DECIMAL(10,2) (800.00 zapisuje jako 800) - Money.Round przywraca dwa miejsca
        Money.Round(reader.GetDecimal(reader.GetOrdinal("deposit_taken"))));

    private static Rental Map(SqliteDataReader reader) => new(
        reader.GetInt64(reader.GetOrdinal("id")),
        reader.IsDBNull(reader.GetOrdinal("reservation_id")) ? null : reader.GetInt64(reader.GetOrdinal("reservation_id")),
        reader.GetInt64(reader.GetOrdinal("customer_id")),
        reader.GetInt64(reader.GetOrdinal("equipment_id")),
        DbDateTime.FromDb(reader.GetString(reader.GetOrdinal("issued_at"))),
        DbDateTime.FromDb(reader.GetString(reader.GetOrdinal("planned_return"))),
        Money.Round(reader.GetDecimal(reader.GetOrdinal("deposit_taken"))),
        Enum.Parse<RentalStatus>(reader.GetString(reader.GetOrdinal("status"))));
}
