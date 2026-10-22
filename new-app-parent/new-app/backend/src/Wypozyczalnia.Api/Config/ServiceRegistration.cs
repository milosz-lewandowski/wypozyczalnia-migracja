using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Wypozyczalnia.Controllers;
using Wypozyczalnia.Repositories;
using Wypozyczalnia.Repositories.Sqlite;
using Wypozyczalnia.Services;

namespace Wypozyczalnia.Config;

/// <summary>Rejestracja zaleznosci aplikacji: baza, repozytoria, serwisy, kontrolery, JSON, CORS, obsluga bledow.</summary>
public static class ServiceRegistration
{
    public static IServiceCollection AddWypozyczalnia(this IServiceCollection services, IConfiguration configuration)
    {
        // Baza SQLite w pamieci: jedno trwale polaczenie na czas zycia aplikacji (inaczej baza znika).
        services.AddSingleton<SqliteDatabase>();
        services.AddSingleton<DatabaseInitializer>();
        services.AddHostedService<DatabaseStartup>();

        services.AddSingleton<AvailabilityRepository>();
        services.AddSingleton<CustomerRepository>();
        services.AddSingleton<EquipmentRepository>();
        services.AddSingleton<ReservationRepository>();
        services.AddSingleton<TariffRepository>();
        services.AddSingleton<RentalRepository>();

        services.AddSingleton<AvailabilityService>();
        services.AddSingleton<ReservationService>();
        services.AddSingleton<PricingService>();
        services.AddSingleton<RentalReturnService>();

        services.AddControllers()
            .AddJsonOptions(options =>
            {
                var json = options.JsonSerializerOptions;
                // Jak Jackson w Javie: nazwy pol z rozroznieniem wielkosci liter, liczby w cudzyslowie tez przyjmowane,
                // polskie znaki bez escapowania, enumy jako nazwy (NEW, CONFIRMED...).
                json.PropertyNameCaseInsensitive = false;
                json.NumberHandling = JsonNumberHandling.AllowReadingFromString;
                json.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                json.Converters.Add(new JsonStringEnumConverter());
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                // Bledne dane wejsciowe obsluguja kontrolery same, z komunikatem {"error": "..."}.
                options.SuppressModelStateInvalidFilter = true;
            });

        services.AddExceptionHandler<ApiExceptionHandler>();
        services.AddProblemDetails();

        services.AddFrontendCors(configuration);
        return services;
    }
}
