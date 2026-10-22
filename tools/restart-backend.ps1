# Restart backendu: zatrzymuje proces na porcie 8080 (cale drzewo dotnet watch, przez zatrzymaj.ps1)
# i uruchamia backend ponownie W NOWEJ KARCIE Windows Terminala "backend:8080" (okno "wypozyczalnia";
# bez Windows Terminala: nowe okno PowerShell). Biezace okno zostaje wolne.
# Stara karta backendu zostaje z samym promptem - mozna ja zamknac.
# Opcja -WBiezacymOknie: backend startuje w tym oknie (zajmuje je do Ctrl+C).
#
# Uruchomienie z korzenia repo:
#   .\tools\restart-backend.ps1
#   .\tools\restart-backend.ps1 -WBiezacymOknie
# Kiedy potrzebny: zmiana .csproj, Program.cs, appsettings*.json, Database/*.sql, nowy pakiet NuGet,
# albo backend "zawiesil sie" po hot reload. W karcie backendu to samo robi Ctrl+R.
# Plik celowo tylko ASCII (Windows PowerShell 5.1 czyta skrypty bez BOM jako ANSI).

param(
    [switch]$WBiezacymOknie
)

$repoRoot = Split-Path -Parent $PSScriptRoot
$backendDirectory = Join-Path $repoRoot 'new-app-parent\new-app\backend'
$backendScript = Join-Path $PSScriptRoot 'uruchom-backend.ps1'
$stopScript = Join-Path $PSScriptRoot 'zatrzymaj.ps1'
$terminalWindowName = 'wypozyczalnia'

& $stopScript -Porty 8080
if ($LASTEXITCODE -ne 0) {
    Write-Host 'Port 8080 nadal zajety - restart przerwany (komunikat powyzej mowi, co zrobic).' -ForegroundColor Red
    exit 1
}

if ($WBiezacymOknie) {
    & $backendScript
    exit $LASTEXITCODE
}

function ConvertTo-QuotedArgument([string]$argumentText) {
    if ($argumentText -match '\s') { return '"' + $argumentText + '"' }
    return $argumentText
}

$windowsTerminalCommand = Get-Command wt.exe -ErrorAction SilentlyContinue
if ($windowsTerminalCommand) {
    $terminalArguments = @(
        '-w', $terminalWindowName,
        'new-tab', '--title', 'backend:8080', '--suppressApplicationTitle',
        '-d', (ConvertTo-QuotedArgument $backendDirectory),
        'powershell.exe', '-NoExit', '-ExecutionPolicy', 'Bypass', '-File', (ConvertTo-QuotedArgument $backendScript)
    )
    Start-Process -FilePath $windowsTerminalCommand.Source -ArgumentList $terminalArguments
    Write-Host 'Backend uruchomiony ponownie w nowej karcie Windows Terminala "backend:8080".' -ForegroundColor Cyan
} else {
    $powershellArguments = @('-NoExit', '-ExecutionPolicy', 'Bypass', '-File', (ConvertTo-QuotedArgument $backendScript))
    Start-Process -FilePath 'powershell.exe' -ArgumentList $powershellArguments -WorkingDirectory $backendDirectory
    Write-Host 'Backend uruchomiony ponownie w nowym oknie PowerShell.' -ForegroundColor Cyan
}
Write-Host 'Gotowy po kilkunastu sekundach: http://localhost:8080/api/ping'
