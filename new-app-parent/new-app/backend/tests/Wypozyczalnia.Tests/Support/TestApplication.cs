using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wypozyczalnia.Config;
using Wypozyczalnia.Repositories.Sqlite;

namespace Wypozyczalnia.Tests.Support;

/// <summary>
/// Kontener zaleznosci aplikacji z wlasna baza w pamieci (unikalne Database:Name) i danymi z data.sql.
/// Kazdy test dostaje swiezy stan bazy,
/// bo xUnit tworzy nowa instancje klasy testowej (a wiec i nowa baze) dla kazdego testu.
/// </summary>
public sealed class TestApplication : IDisposable
{
    private readonly ServiceProvider provider;

    public TestApplication()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Database:Name"] = UniqueDatabaseName()
            })
            .Build();

        var services = new ServiceCollection();
        services.AddSingleton(configuration);
        services.AddLogging();
        services.AddWypozyczalnia(configuration);
        provider = services.BuildServiceProvider();
        provider.GetRequiredService<DatabaseInitializer>().Initialize();
    }

    public T Get<T>() where T : notnull => provider.GetRequiredService<T>();

    public static string UniqueDatabaseName() => "test-" + Guid.NewGuid().ToString("N");

    public void Dispose() => provider.Dispose();
}
