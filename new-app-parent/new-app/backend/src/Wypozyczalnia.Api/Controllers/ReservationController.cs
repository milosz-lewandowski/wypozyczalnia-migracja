using Microsoft.AspNetCore.Mvc;
using Wypozyczalnia.Dto;
using Wypozyczalnia.Services;

namespace Wypozyczalnia.Controllers;

/// <summary>Rezerwacje: POST /api/reservations (201), GET /api/reservations[?customerId=].</summary>
[ApiController]
[Route("api/reservations")]
public class ReservationController(ReservationService reservationService) : ControllerBase
{
    [HttpPost]
    public IActionResult Create([FromBody] ReservationRequest? request)
    {
        // Nieczytelne cialo (zly JSON, zly typ pola, zly format daty, puste cialo) -> 400
        if (!ModelState.IsValid || request is null)
        {
            throw new InvalidRequestException("Niepoprawne dane zadania");
        }
        long customerId = RequirePresent(request.CustomerId, "customerId");
        long equipmentId = RequirePresent(request.EquipmentId, "equipmentId");
        DateTime from = RequirePresent(request.From, "from");
        DateTime to = RequirePresent(request.To, "to");

        var reservation = reservationService.Create(customerId, equipmentId, from, to, request.Notes);
        return StatusCode(StatusCodes.Status201Created, ReservationResponse.FromModel(reservation));
    }

    [HttpGet]
    public List<ReservationResponse> List([FromQuery] string? customerId)
    {
        long? customerFilter = QueryParameters.OptionalLong(customerId, "customerId");
        return reservationService.List(customerFilter)
            .Select(ReservationResponse.FromModel)
            .ToList();
    }

    private static T RequirePresent<T>(T? value, string name) where T : struct =>
        value ?? throw new InvalidRequestException($"Brak parametru: {name}");
}
