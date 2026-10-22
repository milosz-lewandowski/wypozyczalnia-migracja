using Wypozyczalnia.Repositories.Sqlite;

namespace Wypozyczalnia.Config;

/// <summary>
/// Start bazy przed otwarciem portu: schema.sql, data.sql i dodatkowe skrypty z Database:ExtraDataScripts.
/// Blad skryptu przerywa start aplikacji (kod wyjscia rozny od 0).
/// </summary>
public sealed class DatabaseStartup(DatabaseInitializer initializer) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        initializer.Initialize();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
