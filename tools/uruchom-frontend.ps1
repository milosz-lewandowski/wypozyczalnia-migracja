# Uruchamia frontend (React + Vite) w trybie deweloperskim: npm run dev, port 5173.
# Zmiany w plikach frontu przeladowuja sie same w przegladarce (Vite HMR). Zapytania /api ida do backendu na 8080.
# Dziala z dowolnego katalogu (sciezki liczone od polozenia skryptu). W tym oknie: Ctrl+C = stop.
#
# Uruchomienie z korzenia repo:
#   .\tools\uruchom-frontend.ps1
# Zatrzymanie z innego okna: .\tools\zatrzymaj.ps1 -Porty 5173
# Plik celowo tylko ASCII (Windows PowerShell 5.1 czyta skrypty bez BOM jako ANSI).

$repoRoot = Split-Path -Parent $PSScriptRoot
$frontendDirectory = Join-Path $repoRoot 'new-app-parent\new-app\frontend'
$frontendPort = 5173

if (-not (Get-Command npm -ErrorAction SilentlyContinue)) {
    Write-Host 'Brak polecenia npm. Zainstaluj Node.js (https://nodejs.org, wersja LTS) i otworz nowe okno terminala.' -ForegroundColor Red
    exit 1
}
if (-not (Test-Path (Join-Path $frontendDirectory 'node_modules'))) {
    Write-Host 'Brak zaleznosci frontendu (katalog node_modules). Zainstaluj je raz:' -ForegroundColor Yellow
    Write-Host "  Push-Location `"$frontendDirectory`"; npm ci; Pop-Location"
    Write-Host 'i uruchom ten skrypt ponownie.'
    exit 1
}
$listeningConnections = Get-NetTCPConnection -State Listen -LocalPort $frontendPort -ErrorAction SilentlyContinue
if ($listeningConnections) {
    $ownerProcessId = @($listeningConnections)[0].OwningProcess
    $ownerProcess = Get-Process -Id $ownerProcessId -ErrorAction SilentlyContinue
    Write-Host "Port $frontendPort jest zajety (proces: $($ownerProcess.ProcessName), PID $ownerProcessId)." -ForegroundColor Yellow
    Write-Host 'Zatrzymaj go i uruchom ponownie:'
    Write-Host '  .\tools\zatrzymaj.ps1 -Porty 5173'
    Write-Host '  .\tools\uruchom-frontend.ps1'
    exit 1
}

try { $Host.UI.RawUI.WindowTitle = "frontend:$frontendPort" } catch { }
Write-Host "Frontend: http://localhost:$frontendPort  (Ctrl+C = stop)" -ForegroundColor Cyan
Push-Location $frontendDirectory
try {
    & npm run dev -- --strictPort
    $devServerExitCode = $LASTEXITCODE
} finally {
    Pop-Location
}
exit $devServerExitCode
