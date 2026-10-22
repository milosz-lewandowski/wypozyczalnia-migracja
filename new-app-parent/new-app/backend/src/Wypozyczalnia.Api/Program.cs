using Wypozyczalnia.Config;

var builder = WebApplication.CreateBuilder(args);

// Port domyslny 8080 (proxy Vite frontendu); nadpisuje ASPNETCORE_URLS albo --urls.
if (string.IsNullOrEmpty(builder.Configuration["urls"]))
{
    builder.WebHost.UseUrls("http://localhost:8080");
}

builder.Services.AddWypozyczalnia(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();
app.UseCors(CorsConfig.PolicyName);
app.MapControllers();

try
{
    // Start bazy (schema.sql, data.sql, Database:ExtraDataScripts) wykonuje DatabaseStartup przed otwarciem portu.
    app.Run();
    return 0;
}
catch (Exception exception)
{
    // Np. blad skryptu z Database:ExtraDataScripts; szczegoly zalogowal juz host ("Hosting failed to start").
    // Logger jest w tym miejscu zamkniety, wiec komunikat idzie wprost na stderr.
    Console.Error.WriteLine($"Aplikacja nie wystartowala: {exception.Message}");
    return 1;
}

/// <summary>Jawnie publiczna klasa Program - punkt wejscia dla WebApplicationFactory w testach.</summary>
public partial class Program;
