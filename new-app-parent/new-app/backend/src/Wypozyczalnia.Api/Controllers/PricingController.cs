using Microsoft.AspNetCore.Mvc;
using Wypozyczalnia.Dto;
using Wypozyczalnia.Services;

namespace Wypozyczalnia.Controllers;

/// <summary>Wycena wynajmu: GET /api/pricing/quote?equipmentId=&amp;from=&amp;to=[&amp;customerId=].</summary>
[ApiController]
[Route("api/pricing")]
public class PricingController(PricingService pricingService) : ControllerBase
{
    [HttpGet("quote")]
    public QuoteResponse Quote(
        [FromQuery] string? equipmentId,
        [FromQuery] string? from,
        [FromQuery] string? to,
        [FromQuery] string? customerId)
    {
        long equipment = QueryParameters.RequireLong(equipmentId, "equipmentId");
        DateTime issuedAt = QueryParameters.RequireDateTime(from, "from");
        DateTime returnedAt = QueryParameters.RequireDateTime(to, "to");
        long? customer = QueryParameters.OptionalLong(customerId, "customerId");

        return QuoteResponse.FromModel(pricingService.CalculateQuote(equipment, issuedAt, returnedAt, customer));
    }
}
