using Microsoft.AspNetCore.Mvc;
using Wypozyczalnia.Dto;
using Wypozyczalnia.Services;

namespace Wypozyczalnia.Controllers;

/// <summary>Dostepnosc sprzetu: GET /api/equipment/available?category=&amp;from=&amp;to=.</summary>
[ApiController]
[Route("api/equipment")]
public class EquipmentController(AvailabilityService availabilityService) : ControllerBase
{
    [HttpGet("available")]
    public List<EquipmentResponse> Available(
        [FromQuery] string? category,
        [FromQuery] string? from,
        [FromQuery] string? to)
    {
        // Kolejnosc sprawdzania parametrow jak w Javie: category, from, to.
        string requiredCategory = QueryParameters.RequireText(category, "category");
        DateTime dateFrom = QueryParameters.RequireDateTime(from, "from");
        DateTime dateTo = QueryParameters.RequireDateTime(to, "to");

        return availabilityService.AvailableInCategory(requiredCategory, dateFrom, dateTo)
            .Select(EquipmentResponse.FromModel)
            .ToList();
    }
}
