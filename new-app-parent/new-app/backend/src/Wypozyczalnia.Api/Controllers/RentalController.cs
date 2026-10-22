using Microsoft.AspNetCore.Mvc;
using Wypozyczalnia.Dto;
using Wypozyczalnia.Services;

namespace Wypozyczalnia.Controllers;

/// <summary>Zwrot sprzetu: GET /api/rentals (otwarte), POST /api/rentals/{id}/return (rozliczenie z kara i kaucja).</summary>
[ApiController]
[Route("api/rentals")]
public class RentalController(RentalReturnService rentalReturnService) : ControllerBase
{
    [HttpGet]
    public List<OpenRentalResponse> OpenRentals() =>
        rentalReturnService.OpenRentals()
            .Select(OpenRentalResponse.FromModel)
            .ToList();

    [HttpPost("{id:long}/return")]
    public ReturnResponse ReturnRental(long id, [FromBody] ReturnRequest? request)
    {
        // Nieczytelne cialo (zly JSON, zly format daty, puste cialo) -> 400
        if (!ModelState.IsValid || request is null)
        {
            throw new InvalidRequestException("Niepoprawne dane zadania");
        }
        if (request.ReturnedAt is null)
        {
            throw new InvalidRequestException("Brak pola: returnedAt");
        }
        var result = rentalReturnService.RegisterReturn(id, request.ReturnedAt.Value, request.DamagesOrEmpty());
        return ReturnResponse.FromModel(result);
    }
}
