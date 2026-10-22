namespace Wypozyczalnia.Model;

/// <summary>
/// Status sprzetu. Kody legacy (kolumna Sprzet.Status, legacy-app-parent/legacy-app/Database/01_schema.sql:46):
/// AVAILABLE = D, RENTED = W, SERVICE = S, RETIRED = X.
/// </summary>
public enum EquipmentStatus
{
    AVAILABLE,
    RENTED,
    SERVICE,
    RETIRED
}
