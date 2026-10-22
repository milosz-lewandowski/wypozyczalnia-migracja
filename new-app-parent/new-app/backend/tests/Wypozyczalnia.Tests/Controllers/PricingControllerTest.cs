using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Wypozyczalnia.Tests.Support;

namespace Wypozyczalnia.Tests.Controllers;

/// <summary>
/// Kontrakt HTTP wyceny (GET /api/pricing/quote). Kwoty i reguly sprawdza PricingServiceTest; tu: kody HTTP,
/// ksztalt odpowiedzi i tresc {"error"}. Kazdy test startuje aplikacje z wlasna baza w pamieci (Database:Name).
/// </summary>
public sealed class PricingControllerTest : IDisposable
{
    private readonly WebApplicationFactory<Program> factory;
    private readonly HttpClient client;

    public PricingControllerTest()
    {
        factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder.UseSetting("Database:Name", TestApplication.UniqueDatabaseName()));
        client = factory.CreateClient();
    }

    public void Dispose()
    {
        client.Dispose();
        factory.Dispose();
    }

    /* W-A przez HTTP: 5 dob, rabat klienta 2 %, 141,36 (wyprowadzenie w PricingServiceTest.WA_HalfEvenOnCustomerDiscount). */
    [Fact(DisplayName = "W-A: 200, amount 141.36, days 5, daty w formacie yyyy-MM-ddTHH:mm")]
    public async Task Quote_Ok()
    {
        var response = await client.GetAsync("/api/pricing/quote?equipmentId=6&from=2026-10-12T08:00&to=2026-10-17T08:00&customerId=2");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        JsonElement body = await ReadJson(response);
        Assert.Equal("141.36", body.GetProperty("amount").GetRawText());
        Assert.Equal(5, body.GetProperty("days").GetInt32());
        Assert.Equal(0, body.GetProperty("weekendDays").GetInt32());
        Assert.Equal(2, body.GetProperty("customerDiscountPercent").GetInt32());
        Assert.Equal(6, body.GetProperty("equipmentId").GetInt64());
        Assert.Equal(2, body.GetProperty("customerId").GetInt64());
        Assert.Equal("2026-10-12T08:00", body.GetProperty("from").GetString());
        Assert.Equal("2026-10-17T08:00", body.GetProperty("to").GetString());
        Assert.Equal("28.85", body.GetProperty("dailyRate").GetRawText());
    }

    /* W-BLAD: data zwrotu = data wydania (legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:24). */
    [Fact(DisplayName = "W-BLAD: to = from -> 400 z komunikatem z legacy")]
    public async Task Quote_ReturnNotAfterIssue()
    {
        var response = await client.GetAsync("/api/pricing/quote?equipmentId=2&from=2026-10-13T09:00&to=2026-10-13T09:00");

        await AssertError(response, HttpStatusCode.BadRequest, "Data zwrotu musi byc pozniejsza niz data wydania");
    }

    /* W-404: nieznany sprzet (legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:33). */
    [Fact(DisplayName = "W-404: nieznany sprzet -> 404")]
    public async Task Quote_UnknownEquipment()
    {
        var response = await client.GetAsync("/api/pricing/quote?equipmentId=999&from=2026-10-13T09:00&to=2026-10-15T09:00");

        await AssertError(response, HttpStatusCode.NotFound, "Nieznany sprzet: 999");
    }

    [Fact(DisplayName = "Brak parametru to -> 400 z polem error")]
    public async Task Quote_MissingParameter()
    {
        var response = await client.GetAsync("/api/pricing/quote?equipmentId=6&from=2026-10-12T08:00");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.True((await ReadJson(response)).TryGetProperty("error", out _));
    }

    [Fact(DisplayName = "Zly format daty -> 400 z polem error")]
    public async Task Quote_BadDateFormat()
    {
        var response = await client.GetAsync("/api/pricing/quote?equipmentId=6&from=12.10.2026%2008:00&to=2026-10-17T08:00");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.True((await ReadJson(response)).TryGetProperty("error", out _));
    }

    private static async Task<JsonElement> ReadJson(HttpResponseMessage response)
    {
        using JsonDocument document = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        return document.RootElement.Clone();
    }

    private static async Task AssertError(HttpResponseMessage response, HttpStatusCode expectedStatus, string expectedMessage)
    {
        Assert.Equal(expectedStatus, response.StatusCode);
        Assert.Equal(expectedMessage, (await ReadJson(response)).GetProperty("error").GetString());
    }
}
