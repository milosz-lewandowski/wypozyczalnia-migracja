namespace Wypozyczalnia.Model;

/// <summary>
/// Status rezerwacji. Kody legacy (legacy-app-parent/legacy-app/Database/01_schema.sql:73-85):
/// NEW = N, CONFIRMED = P, CANCELLED = A, COMPLETED = Z.
/// Termin blokuja tylko NEW i CONFIRMED (legacy-app-parent/legacy-app/Database/02_procedury.sql:44).
/// </summary>
public enum ReservationStatus
{
    NEW,
    CONFIRMED,
    CANCELLED,
    COMPLETED
}
