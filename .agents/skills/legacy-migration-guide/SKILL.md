---
name: legacy-migration-guide
description: "Użyj, gdy trzeba przenieść proces biznesowy lub komponent z legacy-app-parent/legacy-app do new-app-parent/new-app z zachowaniem zachowania 1:1 (characterization, parity, backend/frontend, kontrakt API, testy). Wymaga wcześniejszej zgody migration-supervisor (rozpocznij P<n>). Słowa-klucze: migracja legacy, przeniesienie procesu, parity, wycena, rezerwacja, zwrot."
---

# Legacy Migration Guide

Skill do migracji procesów biznesowych z aplikacji legacy (VB.NET WebForms + T-SQL) do nowej aplikacji (ASP.NET Core + React), z naciskiem na **zachowanie 1:1**.

## Kiedy używać

Użyj tego skilla, gdy użytkownik prosi o:
- przeniesienie procesu z legacy do new-app,
- odtworzenie logiki biznesowej bez zmiany reguł,
- migrację endpointu + UI + testów wokół jednego procesu,
- doprowadzenie placeholdera do działającej funkcji.

## Kontekst repo

- Legacy (źródło prawdy procesów): `legacy-app-parent/legacy-app/`
- Materiały analityczne: `legacy-app-parent/analysis/`
- Nowa aplikacja:
  - backend: `new-app-parent/new-app/backend/`
  - frontend: `new-app-parent/new-app/frontend/`
- Dokumentacja nowej aplikacji: `new-app-parent/new-docs/`

## Workflow migracji (end-to-end)

Wykonawcą migracji jest agent `migration-implementer` (`.github/agents/migration-implementer.agent.md`).

0. **Zgoda supervisora (obowiązkowa)**
   - `python .agents/skills/migration-supervisor/scripts/supervisor.py rozpocznij P<n> --agent <nazwa>`
   - Kod `3` oznacza STOP: nic nie zmieniaj, zgłoś powód odmowy. Bez zgody skilla `migration-supervisor` nie wolno migrować.
   - Postęp całej migracji: `python .agents/skills/migration-supervisor/scripts/supervisor.py info`.

1. **Znajdź źródła reguł w legacy**
   - Zacznij od managerów i DAL (`*Manager.vb`, `DAL/*.vb`) oraz procedur SQL.
   - Zanotuj kolejność walidacji, komunikaty błędów i rounding kwot.

2. **Znajdź miejsca integracji w new-app**
   - Backend: `Controllers/`, `Services/`, `Repositories/`, `Dto/`, `Model/`
   - Frontend: `src/pages/`, `src/api/`, komponenty współdzielone
   - Kontrakt: `new-docs/KONTRAKT-API.md`

3. **Przenieś logikę do backendu**
   - Implementuj reguły biznesowe w `Services/`.
   - Dostęp do danych wyłącznie przez `Repositories/`.
   - Mapuj błędy na standard API (`400`, `404`, `409` z `{"error":"..."}`).
   - Zachowaj formaty dat i kwot zgodne z kontraktem.

4. **Podłącz kontroler i DTO**
   - Dodaj endpoint(y) i walidację query/body.
   - Dodaj/uzupełnij DTO odpowiedzi.
   - Rejestruj zależności w konfiguracji DI.

5. **Podepnij proces pod istniejące flow**
   - Jeśli proces wpływa na rezerwację/zwrot/inne flow, podmień zaślepki i TODO.
   - Nie zostawiaj „martwych” ścieżek po migracji.

6. **Przenieś UI (jeśli dotyczy)**
   - Zastąp placeholder w `src/pages/*Page.tsx` działającym formularzem/widokiem.
   - Dodaj klienta API w `src/api/`.
   - Zachowaj istniejące wzorce UX i komunikatów błędów.

7. **Zaktualizuj testy i dokumentację**
   - Characterization testy: wartości oczekiwane z legacy.
   - Testy kontrolerów: kody HTTP, shape odpowiedzi, treść błędów.
   - Uzupełnij `new-docs/KONTRAKT-API.md`.

8. **Walidacja końcowa**
   - Backend: `dotnet test new-app-parent/new-app/backend/Wypozyczalnia.sln`
   - Frontend: `npm run build` (w `new-app-parent/new-app/frontend`)
   - Jeśli wymagane: `.\tools\bramka.ps1`

9. **Finalizacja: commit i PR wyłącznie przez skill `migration-commit-pr`**
   - Skill dzieli zmiany po procesie, tworzy dokument pochodzenia, uruchamia review `migration-reviewer` i bramkę, wywołuje `zakoncz`/`odnotuj` w supervisorze, commituje per proces i przygotowuje PR.
   - Samodzielny commit poza tym skillem jest zakazany; `--no-verify` jest zakazane (hook pre-commit pilnuje stanu migracji).

## Zasady jakości

- Legacy to **jedyny** autorytet reguł biznesowych.
- Nie zmieniaj kodu legacy.
- Najpierw parity, potem ewentualne usprawnienia.
- Brak broad catch i cichych fallbacków.
- Zmiana procesu = zmiana kodu + testów + kontraktu API (gdy dotyczy).

## Definition of Done dla migracji procesu

- Endpoint(y) działają i zwracają poprawne pola/statusy.
- Logika biznesowa zgodna z legacy na danych testowych.
- UI procesu nie jest placeholderem.
- Testy backendu przechodzą.
- Frontend się buduje.
- Kontrakt API jest aktualny.
- Stan procesu w `migration-supervisor` zaktualizowany (`zakoncz`/`odnotuj`), dokument pochodzenia `new-app-parent/new-docs/migracja/P<n>-<slug>.md` istnieje.

