using Microsoft.Data.Sqlite;
using Wypozyczalnia.Model;
using Wypozyczalnia.Repositories.Sqlite;

namespace Wypozyczalnia.Repositories;

/// <summary>Klienci (tabela customers). Odpowiednik legacy: CustomerManager.PobierzKlienta / ProgRabatu.</summary>
public class CustomerRepository(SqliteDatabase database)
{
    public Customer? FindById(long id)
    {
        using var connection = database.OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = """
            SELECT id, first_name, last_name, company_name, discount_tier, blocked
            FROM customers
            WHERE id = @id
            """;
        command.Parameters.AddWithValue("@id", id);
        using var reader = command.ExecuteReader();
        return reader.Read() ? Map(reader) : null;
    }

    private static Customer Map(SqliteDataReader reader) => new(
        reader.GetInt64(reader.GetOrdinal("id")),
        reader.GetString(reader.GetOrdinal("first_name")),
        reader.GetString(reader.GetOrdinal("last_name")),
        reader.IsDBNull(reader.GetOrdinal("company_name")) ? null : reader.GetString(reader.GetOrdinal("company_name")),
        reader.GetInt32(reader.GetOrdinal("discount_tier")),
        reader.GetBoolean(reader.GetOrdinal("blocked")));
}
