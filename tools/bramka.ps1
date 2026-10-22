# Bramka jakosci nowej aplikacji: jedna komenda, lancuch krokow, wynik "BRAMKA: OK" albo lista "nie" z powodami.
#   1. testy backendu (dotnet test na backend/Wypozyczalnia.sln; tu biegnie tez test architektury)
#   2. typy frontendu (npx tsc -b), tylko gdy sa node_modules
#   3. reguly statyczne (tools/bramka/sprawdz.py)
#   4. przypadki akceptacyjne przez API (tools/bramka/akceptacja.py; proces nieprzeniesiony = pominiety)
# Uruchomienie z dowolnego katalogu: .\tools\bramka.ps1   (Linux: pwsh tools/bramka.ps1)
# Kod wyjscia: 0 = OK, 1 = co najmniej jedno "nie". Szczegoly kazdego "nie" wypisuje krok powyzej podsumowania.
# Plik celowo tylko ASCII (Windows PowerShell 5.1 czyta skrypty bez BOM jako ANSI).

$repoRoot = Split-Path -Parent $PSScriptRoot
$backendDirectory = Join-Path $repoRoot 'new-app-parent/new-app/backend'
$frontendDirectory = Join-Path $repoRoot 'new-app-parent/new-app/frontend'
$gateDirectory = Join-Path $PSScriptRoot 'bramka'
$runningOnWindows = [System.Environment]::OSVersion.Platform -eq [System.PlatformID]::Win32NT
$failedSteps = New-Object System.Collections.Generic.List[string]

function Write-StepHeader([string]$stepTitle) {
    Write-Host ''
    Write-Host "=== $stepTitle ===" -ForegroundColor Cyan
}

function Find-PythonCommand {
    $candidateCommands = @(@('python'), @('python3'), @('py', '-3'))
    foreach ($candidateCommand in $candidateCommands) {
        $executableName = $candidateCommand[0]
        if (-not (Get-Command $executableName -ErrorAction SilentlyContinue)) { continue }
        $extraArguments = @($candidateCommand | Select-Object -Skip 1)
        try {
            & $executableName @extraArguments -c 'import sys; sys.exit(0 if sys.version_info[0] == 3 else 1)' 2>$null | Out-Null
            if ($LASTEXITCODE -eq 0) { return ,$candidateCommand }
        } catch {
            continue
        }
    }
    return $null
}

# --- 1. testy backendu ---
Write-StepHeader '1/4 testy backendu (dotnet test)'
$backendSolutionPath = Join-Path $backendDirectory 'Wypozyczalnia.sln'
if (-not (Get-Command 'dotnet' -ErrorAction SilentlyContinue)) {
    $failedSteps.Add('nie: [1] brak polecenia dotnet (.NET SDK), testow backendu nie uruchomiono')
} elseif (-not (Test-Path $backendSolutionPath)) {
    $failedSteps.Add("nie: [1] brak $backendSolutionPath")
} else {
    Push-Location $backendDirectory
    try {
        & dotnet test $backendSolutionPath --nologo
        if ($LASTEXITCODE -ne 0) { $failedSteps.Add('nie: [1] testy albo kompilacja backendu nie przechodza (szczegoly wyzej)') }
    } finally {
        Pop-Location
    }
}

# --- 2. typy frontendu ---
Write-StepHeader '2/4 typy frontendu (npx tsc -b)'
if (-not (Test-Path (Join-Path $frontendDirectory 'node_modules'))) {
    Write-Host 'pominieto: brak node_modules, uruchom npm ci w new-app-parent/new-app/frontend'
} else {
    if ($runningOnWindows) { $npxCommand = 'npx.cmd' } else { $npxCommand = 'npx' }
    Push-Location $frontendDirectory
    try {
        & $npxCommand tsc -b
        if ($LASTEXITCODE -ne 0) { $failedSteps.Add('nie: [2] bledy typow we frontendzie (szczegoly wyzej)') } else { Write-Host 'typy frontendu: zielono' }
    } finally {
        Pop-Location
    }
}

# --- 3 i 4. skrypty Pythona ---
$pythonCommand = Find-PythonCommand
if ($null -eq $pythonCommand) {
    $failedSteps.Add('nie: [3-4] nie znaleziono Pythona 3 (python, python3, py -3), regul i przypadkow nie sprawdzono')
} else {
    $pythonExecutable = $pythonCommand[0]
    $pythonExtraArguments = @($pythonCommand | Select-Object -Skip 1)

    Write-StepHeader '3/4 reguly statyczne (sprawdz.py)'
    & $pythonExecutable @pythonExtraArguments (Join-Path $gateDirectory 'sprawdz.py') $repoRoot
    if ($LASTEXITCODE -ne 0) { $failedSteps.Add('nie: [3] reguly statyczne (lista naruszen wyzej)') }

    Write-StepHeader '4/4 przypadki akceptacyjne przez API (akceptacja.py)'
    & $pythonExecutable @pythonExtraArguments (Join-Path $gateDirectory 'akceptacja.py') $repoRoot
    if ($LASTEXITCODE -ne 0) { $failedSteps.Add('nie: [4] przypadki akceptacyjne (rozbieznosci z legacy wyzej)') }
}

Write-Host ''
if ($failedSteps.Count -eq 0) {
    Write-Host 'BRAMKA: OK' -ForegroundColor Green
    exit 0
}
Write-Host 'BRAMKA: NIE' -ForegroundColor Red
foreach ($failedStep in $failedSteps) { Write-Host " - $failedStep" -ForegroundColor Red }
exit 1
