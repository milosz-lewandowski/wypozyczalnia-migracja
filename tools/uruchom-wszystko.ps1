# Uruchamia cala aplikacje: backend (dotnet watch, port 8080) i frontend (Vite, port 5173)
# w dwoch osobnych kartach Windows Terminala o tytulach "backend:8080" i "frontend:5173"
# (obie w jednym oknie terminala o nazwie "wypozyczalnia"). Bez Windows Terminala: dwa osobne okna PowerShell.
# Kazda karta uruchamia odpowiednio tools\uruchom-backend.ps1 i tools\uruchom-frontend.ps1;
# komunikaty o bledach (np. port zajety) zostaja widoczne w karcie.
#
# Uruchomienie z korzenia repo:
#   .\tools\uruchom-wszystko.ps1
# Potem: frontend http://localhost:5173, backend http://localhost:8080/api/ping
# Zatrzymanie obu: .\tools\zatrzymaj.ps1
# Plik celowo tylko ASCII (Windows PowerShell 5.1 czyta skrypty bez BOM jako ANSI).

$repoRoot = Split-Path -Parent $PSScriptRoot
$backendDirectory = Join-Path $repoRoot 'new-app-parent\new-app\backend'
$frontendDirectory = Join-Path $repoRoot 'new-app-parent\new-app\frontend'
$backendScript = Join-Path $PSScriptRoot 'uruchom-backend.ps1'
$frontendScript = Join-Path $PSScriptRoot 'uruchom-frontend.ps1'
$terminalWindowName = 'wypozyczalnia'

# Start-Process w PowerShell 5.1 skleja tablice argumentow spacjami bez cudzyslowow: sciezki ze spacjami cytujemy sami.
function ConvertTo-QuotedArgument([string]$argumentText) {
    if ($argumentText -match '\s') { return '"' + $argumentText + '"' }
    return $argumentText
}

function Start-ScriptInNewTab([string]$tabTitle, [string]$workingDirectory, [string]$scriptPath) {
    $windowsTerminalCommand = Get-Command wt.exe -ErrorAction SilentlyContinue
    if ($windowsTerminalCommand) {
        # Kazde wywolanie wt = jedna karta; zadnych ';' (wt traktuje srednik jako separator polecen).
        $terminalArguments = @(
            '-w', $terminalWindowName,
            'new-tab', '--title', $tabTitle, '--suppressApplicationTitle',
            '-d', (ConvertTo-QuotedArgument $workingDirectory),
            'powershell.exe', '-NoExit', '-ExecutionPolicy', 'Bypass', '-File', (ConvertTo-QuotedArgument $scriptPath)
        )
        Start-Process -FilePath $windowsTerminalCommand.Source -ArgumentList $terminalArguments
        Write-Host "otwarto karte Windows Terminala: $tabTitle"
    } else {
        $powershellArguments = @('-NoExit', '-ExecutionPolicy', 'Bypass', '-File', (ConvertTo-QuotedArgument $scriptPath))
        Start-Process -FilePath 'powershell.exe' -ArgumentList $powershellArguments -WorkingDirectory $workingDirectory
        Write-Host "otwarto okno PowerShell: $tabTitle (brak Windows Terminala)"
    }
}

Start-ScriptInNewTab 'backend:8080' $backendDirectory $backendScript
# Krotka pauza, zeby wt zdazyl utworzyc okno "wypozyczalnia" - druga karta trafi do tego samego okna.
Start-Sleep -Seconds 2
Start-ScriptInNewTab 'frontend:5173' $frontendDirectory $frontendScript

Write-Host ''
Write-Host 'Backend startuje kilkanascie sekund (pierwszy build), frontend kilka sekund.' -ForegroundColor Cyan
Write-Host '  aplikacja:       http://localhost:5173'
Write-Host '  backend (ping):  http://localhost:8080/api/ping'
Write-Host '  zatrzymanie:     .\tools\zatrzymaj.ps1'
