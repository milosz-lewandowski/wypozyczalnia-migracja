using Microsoft.Data.Sqlite;
using Wypozyczalnia.Model;
using Wypozyczalnia.Repositories.Sqlite;

namespace Wypozyczalnia.Repositories;

/// <summary>Sprzet (tabela equipment). Odpowiednik legacy: EquipmentManager.PobierzSprzet.</summary>
public class EquipmentRepository(SqliteDatabase database)
{
    private const string Columns = "id, inventory_number, name, category, daily_rate, deposit, status";

    public Equipment? FindById(long id)
    {
        using var connection = database.OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = $"SELECT {Columns} FROM equipment WHERE id = @id";
        command.Parameters.AddWithValue("@id", id);
        using var reader = command.ExecuteReader();
        return reader.Read() ? Map(reader) : null;
    }

    /// <summary>
    /// Sprzet z kategorii bez wycofanego (RETIRED) (legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/EquipmentDAL.vb:41,
    /// EquipmentManager.ListaWKategorii :35-41). Legacy sortuje po numerze inwentarzowym, API po Id
    /// (w danych kolejnosc jest ta sama).
    /// </summary>
    public List<Equipment> FindByCategory(string category)
    {
        using var connection = database.OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = $"SELECT {Columns} FROM equipment WHERE status <> 'RETIRED' AND category = @category ORDER BY id";
        command.Parameters.AddWithValue("@category", category);
        using var reader = command.ExecuteReader();
        var result = new List<Equipment>();
        while (reader.Read())
        {
            result.Add(Map(reader));
        }
        return result;
    }

    private static Equipment Map(SqliteDataReader reader) => new(
        reader.GetInt64(reader.GetOrdinal("id")),
        reader.GetString(reader.GetOrdinal("inventory_number")),
        reader.GetString(reader.GetOrdinal("name")),
        reader.GetString(reader.GetOrdinal("category")).Trim(),
        // SQLite nie pamieta skali DECIMAL(10,2) (45.00 zapisuje jako 45) - Money.Round przywraca dwa miejsca
        Money.Round(reader.GetDecimal(reader.GetOrdinal("daily_rate"))),
        Money.Round(reader.GetDecimal(reader.GetOrdinal("deposit"))),
        Enum.Parse<EquipmentStatus>(reader.GetString(reader.GetOrdinal("status"))));
}
