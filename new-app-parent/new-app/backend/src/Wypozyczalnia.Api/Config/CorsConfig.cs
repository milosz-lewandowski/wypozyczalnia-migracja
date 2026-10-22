namespace Wypozyczalnia.Config;

/// <summary>CORS dla frontendu (Vite, http://localhost:5173); lista originow z konfiguracji Cors:AllowedOrigins (po przecinku).</summary>
public static class CorsConfig
{
    public const string PolicyName = "frontend";

    public static IServiceCollection AddFrontendCors(this IServiceCollection services, IConfiguration configuration)
    {
        string[] origins = (configuration["Cors:AllowedOrigins"] ?? string.Empty)
            .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        services.AddCors(options => options.AddPolicy(PolicyName, policy => policy
            .WithOrigins(origins)
            .WithMethods("GET", "POST", "PUT", "DELETE")
            .AllowAnyHeader()));
        return services;
    }
}
