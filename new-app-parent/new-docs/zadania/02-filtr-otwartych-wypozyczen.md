# 02 Filtr otwartych wypożyczeń po kliencie

Zakres małego feature'a do flow „z czatu VS Code do agenta w chmurze”. Zwykle powstaje w rozmowie w trybie planowania; ten plik jest wersją zapasową do wklejenia przy starcie zadania z karty `Agents` na GitHubie. Struktura jak w `.github/ISSUE_TEMPLATE/zadanie-dla-agenta.md`.

To nowa funkcja, nie przeniesienie procesu z legacy: legacy pokazuje wszystkie otwarte wypożyczenia bez filtra (`legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/RentalDAL.vb:68-78`). Obowiązuje definicja ukończenia i bramka; skill migracji nie jest tu potrzebny.

## Cel

Na zakładce Zwrot obsługa wybiera klienta i widzi tylko jego otwarte wypożyczenia. Filtr działa w API (`GET /api/rentals?customerId=...`) i na froncie.

## Stan obecny

- `GET /api/rentals` zwraca wszystkie otwarte wypożyczenia posortowane po `id`, bez parametrów (`new-app-parent/new-docs/KONTRAKT-API.md`, sekcja „Zwrot: `GET /api/rentals`”).
- Zakładka Zwrot (`new-app-parent/new-app/frontend/src/pages/ZwrotPage.tsx`) pokazuje całą listę w tabeli.
- Dane startowe (`new-app-parent/new-app/backend/src/Wypozyczalnia.Api/Database/data.sql`): otwarte wypożyczenia 4 (klient 10), 5 (klient 9), 6 (klient 12).

## Pliki startowe

- `new-app-parent/new-app/backend/src/Wypozyczalnia.Api/Controllers/RentalController.cs` (akcja listy otwartych)
- `new-app-parent/new-app/backend/src/Wypozyczalnia.Api/Controllers/QueryParameters.cs` (parametry opcjonalne i komunikaty `400`)
- `new-app-parent/new-app/backend/src/Wypozyczalnia.Api/Services/RentalReturnService.cs`, `new-app-parent/new-app/backend/src/Wypozyczalnia.Api/Repositories/RentalRepository.cs`
- `new-app-parent/new-app/frontend/src/api/rentals.ts`, `new-app-parent/new-app/frontend/src/pages/ZwrotPage.tsx`
- testy: `new-app-parent/new-app/backend/tests/Wypozyczalnia.Tests/Controllers/RentalControllerTest.cs`

## Kryteria akceptacji

1. `GET /api/rentals` bez parametru działa jak dziś (istniejący test bez zmian).
2. `GET /api/rentals?customerId=9` zwraca tylko wypożyczenia tego klienta (w danych startowych: `[5]`).
3. Klient bez otwartych wypożyczeń albo nieistniejący → `200` i pusta lista.
4. `customerId` niebędące liczbą → `400 {"error":"Niepoprawna wartosc parametru: customerId"}` (ten sam komunikat co przy innych parametrach).
5. Warstwy jak w reszcie aplikacji: kontroler → serwis → repozytorium; SQL tylko w `Repositories/`.
6. Front: nad tabelą lista wyboru klienta (opcja „Wszyscy klienci” i klienci z otwartych wypożyczeń); wybór pobiera listę z API z parametrem `customerId`.
7. `KONTRAKT-API.md`: w sekcji `GET /api/rentals` opisany parametr opcjonalny i błąd `400`.
8. `pwsh ./tools/bramka.ps1` kończy się `BRAMKA: OK`.

## Testy

| id | wejście | oczekiwane |
|---|---|---|
| FK-1 | `GET /api/rentals?customerId=9` | `200`, identyfikatory `[5]` |
| FK-2 | `GET /api/rentals?customerId=3` | `200`, `[]` |
| FK-3 | `GET /api/rentals?customerId=abc` | `400`, `{"error":"Niepoprawna wartosc parametru: customerId"}` |

## Poza zakresem

- `schema.sql`, `data.sql`, `.github/workflows/**`, `tools/bramka.ps1`, `tools/bramka/**`.
- Proces zwrotu i jego rozliczenie (`POST /api/rentals/{id}/return`).
- `legacy-app-parent/`.

## Definicja ukończenia

- [ ] Kryteria 1-8 spełnione, testy FK-1, FK-2, FK-3 zielone
- [ ] `BRAMKA: OK`
- [ ] Opis PR: co zmieniono, dlaczego, jak sprawdzono (`new-app-parent/new-docs/DEFINITION-OF-DONE.md`)
