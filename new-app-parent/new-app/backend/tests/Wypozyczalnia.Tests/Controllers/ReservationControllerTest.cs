using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Wypozyczalnia.Tests.Support;

namespace Wypozyczalnia.Tests.Controllers;

/// <summary>
/// Kontrakt HTTP dostepnosci i rezerwacji (GET /api/equipment/available, POST i GET /api/reservations).
/// Wartosci i reguly sprawdzaja AvailabilityServiceTest i ReservationServiceTest; tu: kody HTTP, ksztalt odpowiedzi,
/// tresc {"error"}. POST zmienia baze, wiec kazdy test startuje aplikacje z wlasna baza w pamieci (Database:Name).
/// </summary>
public sealed class ReservationControllerTest : IDisposable
{
    private readonly WebApplicationFactory<Program> factory;
    private readonly HttpClient client;

    public ReservationControllerTest()
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

    /* D-1 przez HTTP (wyprowadzenie w AvailabilityServiceTest.D1_CategoryAvailability). */
    [Fact(DisplayName = "D-1: 200, Id [1, 3, 4] z polami sprzetu")]
    public async Task Available_Ok()
    {
        var response = await client.GetAsync("/api/equipment/available?category=BUD&from=2026-10-21T09:00&to=2026-10-21T17:00");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        JsonElement body = await ReadJson(response);
        Assert.Equal([1L, 3L, 4L], body.EnumerateArray().Select(item => item.GetProperty("id").GetInt64()));
        JsonElement first = body[0];
        Assert.Equal("BUD-0001", first.GetProperty("inventoryNumber").GetString());
        Assert.Equal("BUD", first.GetProperty("category").GetString());
        Assert.Equal("45.00", first.GetProperty("dailyRate").GetRawText());
        Assert.Equal("300.00", first.GetProperty("deposit").GetRawText());
    }

    /* D-ZAKRES: to = from (legacy-app-parent/legacy-app/Database/02_procedury.sql:32-36). */
    [Fact(DisplayName = "D-ZAKRES: to = from -> 400 Nieprawidlowy zakres dat")]
    public async Task Available_InvalidRange()
    {
        var response = await client.GetAsync("/api/equipment/available?category=BUD&from=2026-10-21T09:00&to=2026-10-21T09:00");

        await AssertError(response, HttpStatusCode.BadRequest, "Nieprawidlowy zakres dat");
    }

    /* R-OK przez HTTP (wyprowadzenie w ReservationServiceTest.ROk_CreatesNewReservation). */
    [Fact(DisplayName = "R-OK: 201, status NEW, estimatedPrice 0.00 do przeniesienia wyceny, daty yyyy-MM-ddTHH:mm")]
    public async Task Create_Ok()
    {
        var response = await PostReservation(
            """{"customerId":1,"equipmentId":1,"from":"2026-10-26T08:00","to":"2026-10-28T08:00","notes":"odbior rano"}""");

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        JsonElement body = await ReadJson(response);
        Assert.Equal(JsonValueKind.Number, body.GetProperty("id").ValueKind);
        Assert.Equal(1, body.GetProperty("customerId").GetInt64());
        Assert.Equal(1, body.GetProperty("equipmentId").GetInt64());
        Assert.Equal("2026-10-26T08:00", body.GetProperty("from").GetString());
        Assert.Equal("2026-10-28T08:00", body.GetProperty("to").GetString());
        Assert.Equal("NEW", body.GetProperty("status").GetString());
        Assert.Equal("0.00", body.GetProperty("estimatedPrice").GetRawText());
        Assert.Equal("odbior rano", body.GetProperty("notes").GetString());
    }

    /* R-KOLIZJA (legacy-app-parent/legacy-app/WypozyczalniaCore/ReservationManager.vb:34-36). */
    [Fact(DisplayName = "R-KOLIZJA: 409 Sprzet niedostepny w wybranym terminie")]
    public async Task Create_Collision()
    {
        var response = await PostReservation(
            """{"customerId":1,"equipmentId":2,"from":"2026-10-21T08:00","to":"2026-10-23T08:00"}""");

        await AssertError(response, HttpStatusCode.Conflict, "Sprzet niedostepny w wybranym terminie");
    }

    /* R-BLOK (legacy-app-parent/legacy-app/WypozyczalniaCore/ReservationManager.vb:28-31). */
    [Fact(DisplayName = "R-BLOK: 409 Klient zablokowany")]
    public async Task Create_BlockedCustomer()
    {
        var response = await PostReservation(
            """{"customerId":5,"equipmentId":1,"from":"2026-10-26T08:00","to":"2026-10-28T08:00"}""");

        await AssertError(response, HttpStatusCode.Conflict, "Klient zablokowany");
    }

    /* R-DATY (legacy-app-parent/legacy-app/WypozyczalniaCore/ReservationManager.vb:20-22). */
    [Fact(DisplayName = "R-DATY: 400 to = from")]
    public async Task Create_InvalidDates()
    {
        var response = await PostReservation(
            """{"customerId":1,"equipmentId":1,"from":"2026-10-26T08:00","to":"2026-10-26T08:00"}""");

        await AssertError(response, HttpStatusCode.BadRequest, "Data zakonczenia musi byc pozniejsza niz data rozpoczecia");
    }

    /* R-60 granica: 60 dni i 1 minuta (legacy-app-parent/legacy-app/WypozyczalniaCore/ReservationManager.vb:23-25). */
    [Fact(DisplayName = "R-60: 60 dni i 1 minuta -> 400")]
    public async Task Create_TooLong()
    {
        var response = await PostReservation(
            """{"customerId":1,"equipmentId":1,"from":"2026-11-01T08:00","to":"2026-12-31T08:01"}""");

        await AssertError(response, HttpStatusCode.BadRequest, "Rezerwacja moze obejmowac maksymalnie 60 dni");
    }

    /* R-60 granica: dokladnie 60 dni -> 201 (legacy-app-parent/legacy-app/WypozyczalniaCore/ReservationManager.vb:23). */
    [Fact(DisplayName = "R-60: dokladnie 60 dni -> 201")]
    public async Task Create_ExactlySixtyDays()
    {
        var response = await PostReservation(
            """{"customerId":1,"equipmentId":1,"from":"2026-11-01T08:00","to":"2026-12-31T08:00"}""");

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal("NEW", (await ReadJson(response)).GetProperty("status").GetString());
    }

    /* R-404: nieznany sprzet (legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:32-33 przez ReservationManager.vb:39). */
    [Fact(DisplayName = "R-404: nieznany sprzet -> 404")]
    public async Task Create_UnknownEquipment()
    {
        var response = await PostReservation(
            """{"customerId":1,"equipmentId":999,"from":"2026-10-26T08:00","to":"2026-10-28T08:00"}""");

        await AssertError(response, HttpStatusCode.NotFound, "Nieznany sprzet: 999");
    }

    [Fact(DisplayName = "Brak pola from -> 400 z polem error")]
    public async Task Create_MissingField()
    {
        var response = await PostReservation("""{"customerId":1,"equipmentId":1,"to":"2026-10-28T08:00"}""");

        await AssertError(response, HttpStatusCode.BadRequest, "Brak parametru: from");
    }

    /* R-LISTA: klient 4 -> [3] (seed legacy-app-parent/legacy-app/Database/03_seed.sql). */
    [Fact(DisplayName = "R-LISTA: GET ?customerId=4 -> [3], status CONFIRMED")]
    public async Task List_ByCustomer()
    {
        var response = await client.GetAsync("/api/reservations?customerId=4");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        JsonElement body = await ReadJson(response);
        Assert.Equal([3L], body.EnumerateArray().Select(item => item.GetProperty("id").GetInt64()));
        Assert.Equal("CONFIRMED", body[0].GetProperty("status").GetString());
        Assert.Equal("133.12", body[0].GetProperty("estimatedPrice").GetRawText());
    }

    [Fact(DisplayName = "GET bez filtra -> wszystkie rezerwacje z seeda, sort po Id")]
    public async Task List_All()
    {
        var response = await client.GetAsync("/api/reservations");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        JsonElement body = await ReadJson(response);
        Assert.Equal([1L, 2L, 3L, 4L, 5L, 6L], body.EnumerateArray().Select(item => item.GetProperty("id").GetInt64()));
    }

    private Task<HttpResponseMessage> PostReservation(string json) =>
        client.PostAsync("/api/reservations", new StringContent(json, Encoding.UTF8, "application/json"));

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
