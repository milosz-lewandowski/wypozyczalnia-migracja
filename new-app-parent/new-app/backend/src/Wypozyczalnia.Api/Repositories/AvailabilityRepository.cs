using Wypozyczalnia.Repositories.Sqlite;

namespace Wypozyczalnia.Repositories;

/// <summary>
/// Kolizje terminu sprzetu. Odpowiednik legacy: procedura usp_SprawdzDostepnosc
/// (legacy-app-parent/legacy-app/Database/02_procedury.sql:27-71), wolana przez ReservationDAL.PobierzKolizje.
/// Trzy zrodla kolizji, jak UNION ALL w procedurze:
/// - R: rezerwacje NEW/CONFIRMED nachodzace na termin (:38-46);
/// - W: otwarte wypozyczenia od wydania do PLANOWANEGO zwrotu (:50-58) - zalegle wypozyczenie po terminie
///   nie blokuje, bo procedura patrzy na PlanowanyZwrot, nie na status sprzetu RENTED;
/// - S: sprzet w serwisie albo wycofany (:62-68).
/// Granice ostre: koniec jednego okresu = poczatek drugiego to brak kolizji (date_to &gt; @dateFrom).
/// </summary>
public class AvailabilityRepository(SqliteDatabase database)
{
    /// <summary>Liczba kolizji (wierszy procedury); 0 = sprzet dostepny w terminie (EquipmentManager.vb:51).</summary>
    public int CountCollisions(long equipmentId, DateTime from, DateTime to)
    {
        using var connection = database.OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = """
            SELECT
              (SELECT COUNT(*) FROM reservations r
                WHERE r.equipment_id = @equipmentId
                  AND r.status IN ('NEW', 'CONFIRMED')
                  AND r.date_from < @dateTo
                  AND r.date_to > @dateFrom)
            + (SELECT COUNT(*) FROM rentals w
                WHERE w.equipment_id = @equipmentId
                  AND w.status = 'OPEN'
                  AND w.issued_at < @dateTo
                  AND w.planned_return > @dateFrom)
            + (SELECT COUNT(*) FROM equipment s
                WHERE s.id = @equipmentId
                  AND s.status IN ('SERVICE', 'RETIRED'))
            """;
        command.Parameters.AddWithValue("@equipmentId", equipmentId);
        command.Parameters.AddWithValue("@dateFrom", DbDateTime.ToDb(from));
        command.Parameters.AddWithValue("@dateTo", DbDateTime.ToDb(to));
        return Convert.ToInt32(command.ExecuteScalar());
    }
}
