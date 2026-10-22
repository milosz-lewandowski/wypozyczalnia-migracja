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
