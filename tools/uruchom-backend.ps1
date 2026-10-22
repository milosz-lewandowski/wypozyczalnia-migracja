# Uruchamia backend (.NET) w trybie hot reload: dotnet watch na src/Wypozyczalnia.Api, port 8080.
# Zmiany w kodzie C# przeladowuja sie same; zmiana nieobslugiwana przez hot reload ("rude edit")
# = automatyczny restart (DOTNET_WATCH_RESTART_ON_RUDE_EDIT=1). Recznie w tym oknie: Ctrl+R = restart, Ctrl+C = stop.
# Dziala z dowolnego katalogu (sciezki liczone od polozenia skryptu).
#
# Uruchomienie z korzenia repo:
#   .\tools\uruchom-backend.ps1
# Zatrzymanie z innego okna: .\tools\zatrzymaj.ps1 -Porty 8080
# Plik celowo tylko ASCII (Windows PowerShell 5.1 czyta skrypty bez BOM jako ANSI).

$repoRoot = Split-Path -Parent $PSScriptRoot
$backendDirectory = Join-Path $repoRoot 'new-app-parent\new-app\backend'
$apiProjectDirectory = Join-Path $backendDirectory 'src\Wypozyczalnia.Api'
$apiProjectFile = Join-Path $apiProjectDirectory 'Wypozyczalnia.Api.csproj'
$backendPort = 8080

if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) {
    Write-Host 'Brak polecenia dotnet. Zainstaluj .NET SDK 10 (https://dotnet.microsoft.com/download) i otworz nowe okno terminala.' -ForegroundColor Red
    exit 1
}
if (-not (Test-Path $apiProjectFile)) {
    Write-Host "Nie znaleziono projektu backendu: $apiProjectFile" -ForegroundColor Red
    exit 1
}
$listeningConnections = Get-NetTCPConnection -State Listen -LocalPort $backendPort -ErrorAction SilentlyContinue
if ($listeningConnections) {
    $ownerProcessId = @($listeningConnections)[0].OwningProcess
    $ownerProcess = Get-Process -Id $ownerProcessId -ErrorAction SilentlyContinue
    Write-Host "Port $backendPort jest zajety (proces: $($ownerProcess.ProcessName), PID $ownerProcessId)." -ForegroundColor Yellow
    Write-Host 'Zatrzymaj go i uruchom ponownie:'
    Write-Host '  .\tools\zatrzymaj.ps1 -Porty 8080'
    Write-Host '  .\tools\uruchom-backend.ps1'
    Write-Host 'albo jednym poleceniem: .\tools\restart-backend.ps1'
    exit 1
}

# Restart bez pytania przy zmianach, ktorych hot reload nie umie zastosowac.
$env:DOTNET_WATCH_RESTART_ON_RUDE_EDIT = '1'
# Bez otwierania przegladarki przy starcie.
$env:DOTNET_WATCH_SUPPRESS_LAUNCH_BROWSER = '1'

try { $Host.UI.RawUI.WindowTitle = "backend:$backendPort" } catch { }
Write-Host "Backend: http://localhost:$backendPort/api/ping  (Ctrl+R = restart, Ctrl+C = stop)" -ForegroundColor Cyan
Push-Location $apiProjectDirectory
try {
    & dotnet watch --project $apiProjectFile
    $watchExitCode = $LASTEXITCODE
} finally {
    Pop-Location
}
exit $watchExitCode
