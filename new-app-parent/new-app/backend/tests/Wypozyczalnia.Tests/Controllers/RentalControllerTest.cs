using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Wypozyczalnia.Tests.Support;

namespace Wypozyczalnia.Tests.Controllers;

/// <summary>
/// Kontrakt HTTP zwrotu (GET /api/rentals, POST /api/rentals/{id}/return): kody 200/400/404/409, ksztalt odpowiedzi
/// i tresc {"error"}. Kwot kary tu NIE asertujemy (zasada) - kare liczy i sprawdza RentalReturnServiceTest (wejscie w minutach).
/// POST zmienia baze, wiec kazdy test startuje aplikacje z wlasna baza w pamieci (Database:Name).
/// </summary>
public sealed class RentalControllerTest : IDisposable
{
    private readonly WebApplicationFactory<Program> factory;
    private readonly HttpClient client;

    public RentalControllerTest()
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

    /* Otwarte wypozyczenia w seedzie: 4, 5, 6 (status O, legacy-app-parent/legacy-app/Database/03_seed.sql:155-159); 1-3 zamkniete. */
    [Fact(DisplayName = "GET /api/rentals: 200, trzy otwarte (4, 5, 6) z polami listy")]
    public async Task OpenRentals_Ok()
    {
        var response = await client.GetAsync("/api/rentals");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        JsonElement body = await ReadJson(response);
        Assert.Equal([4L, 5L, 6L], body.EnumerateArray().Select(item => item.GetProperty("id").GetInt64()));
        JsonElement third = body[2];
        Assert.Equal(12, third.GetProperty("customerId").GetInt64());
        Assert.Equal("Marcin Grabowski", third.GetProperty("customerName").GetString());
        Assert.Equal(7, third.GetProperty("equipmentId").GetInt64());
        Assert.Equal("Rębak do gałęzi", third.GetProperty("equipmentName").GetString());
        Assert.Equal("2026-10-17T10:00", third.GetProperty("issuedAt").GetString());
        Assert.Equal("2026-10-18T10:00", third.GetProperty("plannedReturn").GetString());
        Assert.Equal("800.00", third.GetProperty("depositTaken").GetRawText());
        Assert.Equal("Firma Przykładowa Alfa", body[1].GetProperty("customerName").GetString());
    }

    /* Zwrot wyp. 6: 200 i komplet pol rozliczenia; wartosci kary sprawdza RentalReturnServiceTest (Z-K1560-119). */
    [Fact(DisplayName = "POST zwrot wyp. 6: 200, pola rozliczenia obecne, status CLOSED")]
    public async Task ReturnRental_Ok()
    {
        var response = await PostReturn(6, """{"returnedAt":"2026-10-19T12:00","damages":[]}""");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        JsonElement body = await ReadJson(response);
        Assert.Equal(6, body.GetProperty("rentalId").GetInt64());
        Assert.Equal("2026-10-19T12:00", body.GetProperty("returnedAt").GetString());
        foreach (string field in new[] { "fee", "lateMinutes", "lateDays", "penalty", "damagesTotal", "depositTaken", "depositReturned" })
        {
            Assert.True(body.TryGetProperty(field, out _), $"brak pola {field}");
        }
        Assert.Equal("CLOSED", body.GetProperty("status").GetString());
    }

    /* Bez pola damages = brak szkod (pole opcjonalne). */
    [Fact(DisplayName = "POST zwrot bez pola damages: 200")]
    public async Task ReturnRental_DamagesOptional()
    {
        var response = await PostReturn(5, """{"returnedAt":"2026-10-19T09:45"}""");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("CLOSED", (await ReadJson(response)).GetProperty("status").GetString());
    }

    /* Nieznane wypozyczenie (legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:92-94). */
    [Fact(DisplayName = "POST zwrot wyp. 999: 404 z komunikatem z legacy")]
    public async Task ReturnRental_NotFound()
    {
        var response = await PostReturn(999, """{"returnedAt":"2026-10-19T12:00","damages":[]}""");

        await AssertError(response, HttpStatusCode.NotFound, "Nie znaleziono wypozyczenia 999");
    }

    /* Zamkniete wypozyczenie 1 (legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:95-97). */
    [Fact(DisplayName = "POST zwrot wyp. 1 (zamkniete): 409")]
    public async Task ReturnRental_AlreadyClosed()
    {
        var response = await PostReturn(1, """{"returnedAt":"2026-10-19T12:00","damages":[]}""");

        await AssertError(response, HttpStatusCode.Conflict, "Wypozyczenie 1 jest juz zamkniete");
    }

    [Fact(DisplayName = "POST zwrot bez returnedAt: 400 z polem error")]
    public async Task ReturnRental_MissingReturnedAt()
    {
        var response = await PostReturn(6, """{"damages":[]}""");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.True((await ReadJson(response)).TryGetProperty("error", out _));
    }

    [Fact(DisplayName = "POST zwrot ze zlym formatem daty: 400 z polem error")]
    public async Task ReturnRental_BadDateFormat()
    {
        var response = await PostReturn(6, """{"returnedAt":"19.10.2026 12:00","damages":[]}""");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.True((await ReadJson(response)).TryGetProperty("error", out _));
    }

    private Task<HttpResponseMessage> PostReturn(long rentalId, string json) =>
        client.PostAsync($"/api/rentals/{rentalId}/return", new StringContent(json, Encoding.UTF8, "application/json"));

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
