using Microsoft.AspNetCore.Mvc;

namespace Wypozyczalnia.Controllers;

/// <summary>Sprawdzenie, czy backend dziala: GET /api/ping -> {"status": "ok"}.</summary>
[ApiController]
[Route("api/ping")]
public class PingController : ControllerBase
{
    [HttpGet]
    public IActionResult Ping() => Ok(new Dictionary<string, string> { ["status"] = "ok" });
}
