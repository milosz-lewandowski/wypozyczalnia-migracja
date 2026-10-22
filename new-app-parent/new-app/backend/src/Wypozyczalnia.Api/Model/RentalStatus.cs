namespace Wypozyczalnia.Model;

/// <summary>
/// Status wypozyczenia. Kody legacy (kolumna Wypozyczenia.Status, legacy-app-parent/legacy-app/Database/01_schema.sql:92):
/// OPEN = O, CLOSED = Z.
/// </summary>
public enum RentalStatus
{
    OPEN,
    CLOSED
}
