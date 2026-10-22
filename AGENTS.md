# Repozytorium szkoleniowe: przenoszenie procesów biznesowych

Cel: przenosimy procesy biznesowe wypożyczalni sprzętu z aplikacji legacy do nowej aplikacji tak, żeby działały tak samo jak dotąd.
Dane w repozytorium są w 100% syntetyczne.

## Gdzie co jest

- `legacy-app-parent/legacy-app/` - aplikacja legacy (VB.NET WebForms + T-SQL); jedyne źródło procesów biznesowych; tylko do odczytu, nie kompilujemy jej
- Stary system: [instrukcje legacy](legacy-app-parent/AGENTS.md) (start pracy z legacy) i [wyniki analizy](legacy-app-parent/analysis/results/)
- `legacy-app-parent/analysis/` - rozpoznanie aplikacji legacy: `results/` wyniki sprawdzone, `temp/` hipotezy i robocze wnioski
- `new-app-parent/new-app/` - nowa aplikacja: `backend/` (C#, ASP.NET Core, .NET 10; `Wypozyczalnia.sln`), `frontend/` (React, TypeScript)
- `new-app-parent/new-docs/` - jak uruchomić, bramka jakości, zgłoszenie błędu, definicja ukończenia
- `tools/` - narzędzia zespołu (pomiar kodu, sprawdzanie dowodów, bramka jakości, skrypty uruchamiania)

## Komendy

- testy backendu: `dotnet test new-app-parent/new-app/backend/Wypozyczalnia.sln`
- bramka jakości: `.\tools\bramka.ps1`
- `.\tools\uruchom-backend.ps1` - backend z hot reloadem (`dotnet watch`, port 8080)
- `.\tools\uruchom-frontend.ps1` - frontend (`npm run dev`, Vite, port 5173)
- `.\tools\uruchom-wszystko.ps1` - backend i frontend w osobnych kartach terminala
- `.\tools\restart-backend.ps1` - restart backendu
- `.\tools\zatrzymaj.ps1` - zatrzymanie, zwalnia porty 8080 i 5173
- szczegóły: `new-app-parent/new-docs/URUCHOMIENIE.md`

## Zasady

- kodu legacy nie zmieniamy
- dokumenty i komunikaty piszemy po polsku

## Workflow migracji

Kolejność dla każdego procesu P1–P7 (`legacy-app-parent/analysis/results/08-PRZEPLYWY-BIZNESOWE.md`):

1. skill `migration-supervisor`: postęp `python .agents/skills/migration-supervisor/scripts/supervisor.py info`; zgoda `... rozpocznij P<n> --agent <nazwa>` (kod 3 = odmowa, nie migrujemy)
2. agent `migration-implementer` (`.github/agents/`) + skill `legacy-migration-guide`: implementacja 1:1 z legacy
3. skill `migration-commit-pr`: podzia? zmian po procesie, dokument pochodzenia `new-app-parent/new-docs/migracja/P<n>-<slug>.md`, review agentem `migration-reviewer` (CRITICAL/HIGH wracaj? do implementera, maks. 3 rundy), bramka, `zakoncz`/`odnotuj` w supervisorze, wpis w `new-app-parent/new-docs/CHANGELOG.md`, commit per proces, PR

- hooki: `python .agents/skills/migration-supervisor/scripts/supervisor.py zainstaluj-hooki` (pre-commit blokuje commit procesu bez aktualizacji stanu migracji; globalne hooki działają dalej)
- `git commit --no-verify` i `git push --no-verify` s? zabronione
- stan migracji: `new-app-parent/new-docs/NEW-PRZEPLYWY-BIZNESOWE.md` (generowany, nie edytujemy ręcznie)
