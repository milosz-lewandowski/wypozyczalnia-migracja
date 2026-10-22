using Wypozyczalnia.Model;
using Wypozyczalnia.Repositories;

namespace Wypozyczalnia.Services;

/// <summary>
/// Dostepnosc sprzetu w terminie, przeniesiona z legacy-app-parent/legacy-app/WypozyczalniaCore/EquipmentManager.vb
/// (SprawdzDostepnosc :44-52, DostepneWKategorii :55-66) i procedury usp_SprawdzDostepnosc
/// (legacy-app-parent/legacy-app/Database/02_procedury.sql:27-71).
/// </summary>
public class AvailabilityService(AvailabilityRepository availabilityRepository, EquipmentRepository equipmentRepository)
{
    /// <summary>Brak kolizji z rezerwacjami, otwartymi wypozyczeniami i statusem sprzetu (EquipmentManager.vb:44-52).</summary>
    public bool IsAvailable(long equipmentId, DateTime from, DateTime to)
    {
        RequireValidRange(from, to);
        return availabilityRepository.CountCollisions(equipmentId, from, to) == 0;
    }

    /// <summary>
    /// Sprzet z kategorii wolny w terminie (EquipmentManager.vb:55-66): lista bez wycofanych (EquipmentDAL.vb:41),
    /// pominiety sprzet w serwisie (:58), reszta przez sprawdzenie kolizji (:61).
    /// </summary>
    public List<Equipment> AvailableInCategory(string category, DateTime from, DateTime to)
    {
        RequireValidRange(from, to);
        return equipmentRepository.FindByCategory(category.Trim())
            .Where(equipment => equipment.Status != EquipmentStatus.SERVICE)
            .Where(equipment => availabilityRepository.CountCollisions(equipment.Id, from, to) == 0)
            .ToList();
    }

    /// <summary>02_procedury.sql:32-36 - koniec nie pozniej niz poczatek to blad zakresu.</summary>
    private static void RequireValidRange(DateTime from, DateTime to)
    {
        if (to <= from)
        {
            throw new InvalidRequestException("Nieprawidlowy zakres dat");
        }
    }
}
