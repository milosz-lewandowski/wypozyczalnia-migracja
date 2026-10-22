# Zatrzymuje aplikacje: zwalnia porty 8080 (backend) i 5173 (frontend).
# Dla kazdego portu w stanie Listen znajduje proces-wlasciciela, wchodzi w gore drzewa procesow
# (dotnet watch -> Wypozyczalnia.Api, npm -> cmd -> node vite) i zatrzymuje cale drzewo (taskkill /T /F).
# Domyslnie zatrzymuje TYLKO procesy z tego repo (sciezka repo w linii komend albo w sciezce pliku .exe);
# proces spoza repo zostaje, a skrypt mowi, jak go zatrzymac (-Wymus).
# Idempotentny: wolny port = komunikat "wolny", kod wyjscia 0.
#
# Uruchomienie z korzenia repo:
#   .\tools\zatrzymaj.ps1                  # porty 8080 i 5173
#   .\tools\zatrzymaj.ps1 -Porty 8080      # tylko backend
#   .\tools\zatrzymaj.ps1 -Wymus           # takze procesy spoza tego repo
# Kod wyjscia: 0 = porty wolne, 1 = co najmniej jeden port nadal zajety.
# Plik celowo tylko ASCII (Windows PowerShell 5.1 czyta skrypty bez BOM jako ANSI).

param(
    [int[]]$Porty = @(8080, 5173),
    [switch]$Wymus
)

$repoRoot = Split-Path -Parent $PSScriptRoot
$repoRootMarker = ($repoRoot.TrimEnd('\', '/') + '\').ToLowerInvariant()
# Procesy, przez ktore wolno wejsc w gore drzewa (posrednicy uruchomienia); wszystko inne = granica (np. powershell karty).
$launcherProcessNames = @('dotnet.exe', 'node.exe', 'cmd.exe', 'npm.exe', 'conhost.exe')

function Get-ProcessDetails([int]$processIdentifier) {
    return Get-CimInstance Win32_Process -Filter "ProcessId=$processIdentifier" -ErrorAction SilentlyContinue
}

function Test-BelongsToRepo($processDetails) {
    if (-not $processDetails) { return $false }
    $searchedText = ([string]$processDetails.CommandLine + ' ' + [string]$processDetails.ExecutablePath).Replace('/', '\').ToLowerInvariant()
    return $searchedText.Contains($repoRootMarker)
}

function Get-ListeningProcessIds([int]$portNumber) {
    $listeningConnections = Get-NetTCPConnection -State Listen -LocalPort $portNumber -ErrorAction SilentlyContinue
    return @($listeningConnections | Select-Object -ExpandProperty OwningProcess -Unique | Where-Object { $_ -gt 0 })
}

$portsStillBusy = New-Object System.Collections.Generic.List[int]

foreach ($portNumber in $Porty) {
    $ownerProcessIds = Get-ListeningProcessIds $portNumber
    if ($ownerProcessIds.Count -eq 0) {
        Write-Host "port ${portNumber}: wolny" -ForegroundColor Green
        continue
    }
    foreach ($ownerProcessId in $ownerProcessIds) {
        $ownerDetails = Get-ProcessDetails $ownerProcessId
        $chainOfProcesses = New-Object System.Collections.Generic.List[object]
        if ($ownerDetails) { $chainOfProcesses.Add($ownerDetails) }
        $currentDetails = $ownerDetails
        while ($currentDetails) {
            $parentDetails = Get-ProcessDetails ([int]$currentDetails.ParentProcessId)
            if (-not $parentDetails) { break }
            if ($launcherProcessNames -notcontains $parentDetails.Name.ToLowerInvariant()) { break }
            $chainOfProcesses.Add($parentDetails)
            $currentDetails = $parentDetails
        }
        $belongsToRepo = $false
        foreach ($chainDetails in $chainOfProcesses) { if (Test-BelongsToRepo $chainDetails) { $belongsToRepo = $true } }
        $ownerName = if ($ownerDetails) { $ownerDetails.Name } else { '?' }
        if (-not $belongsToRepo -and -not $Wymus) {
            Write-Host "port ${portNumber}: zajety przez $ownerName (PID $ownerProcessId) spoza tego repo - NIE zatrzymuje." -ForegroundColor Yellow
            if ($ownerDetails) { Write-Host "  linia komend: $($ownerDetails.CommandLine)" }
            Write-Host "  Jesli to na pewno mozna zamknac: .\tools\zatrzymaj.ps1 -Porty $portNumber -Wymus"
            continue
        }
        $topProcess = $chainOfProcesses[$chainOfProcesses.Count - 1]
        $topProcessId = if ($topProcess) { [int]$topProcess.ProcessId } else { [int]$ownerProcessId }
        $stoppedDescription = ($chainOfProcesses | ForEach-Object { "$($_.Name)($($_.ProcessId))" }) -join ' <- '
        & taskkill.exe /PID $topProcessId /T /F 2>&1 | Out-Null
        Write-Host "port ${portNumber}: zatrzymano drzewo procesow: $stoppedDescription" -ForegroundColor Cyan
    }
}

# Sprawdzenie koncowe (taskkill dziala asynchronicznie; czekamy do 10 s).
foreach ($portNumber in $Porty) {
    $waitDeadline = (Get-Date).AddSeconds(10)
    while ((Get-ListeningProcessIds $portNumber).Count -gt 0 -and (Get-Date) -lt $waitDeadline) { Start-Sleep -Milliseconds 300 }
    if ((Get-ListeningProcessIds $portNumber).Count -gt 0) { $portsStillBusy.Add($portNumber) }
}

if ($portsStillBusy.Count -gt 0) {
    Write-Host "Nadal zajete porty: $($portsStillBusy -join ', ')" -ForegroundColor Red
    exit 1
}
Write-Host "Porty $($Porty -join ', ') wolne." -ForegroundColor Green
exit 0
