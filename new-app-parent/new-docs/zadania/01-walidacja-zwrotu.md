# 01 Walidacja zwrotu: data zwrotu i rodzaje szkód

Treść zgłoszenia do wklejenia w Issue przypisane do agenta w chmurze; struktura jak w `.github/ISSUE_TEMPLATE/zadanie-dla-agenta.md`.

Warunek wstępny: backend w C# (ASP.NET Core, .NET 10), proces zwrotu jest przeniesiony (wynik M3c): `POST /api/rentals/{id}/return` działa zgodnie z `new-app-parent/new-docs/KONTRAKT-API.md`, a bramka daje `BRAMKA: OK`.

## Cel

Rejestracja zwrotu przyjmuje dziś każdą datę zwrotu i po cichu pomija nieznane kody szkód. Ma odrzucać takie żądania błędem `400` z treścią `{"error": "..."}` (format błędu z kontraktu), tak jak stary system robi to dla daty i jak wymaga tego cennik szkód.

## Stan obecny

- Kontrakt (`new-app-parent/new-docs/KONTRAKT-API.md`, sekcja „Zwrot: `POST /api/rentals/{id}/return`”) wymienia błąd `400` tylko dla braku `returnedAt` i złego formatu; o dacie wcześniejszej niż wydanie i o nieznanym kodzie szkody milczy. Zadanie uzupełnia kontrakt o oba przypadki.
- Legacy odrzuca zwrot z datą wcześniejszą niż wydanie: `legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:99-100` oraz `legacy-app-parent/legacy-app/WypozyczalniaWeb/Zwrot.aspx.vb:218-219`.
- Legacy pomija pozycje spoza cennika szkód bez śladu: `legacy-app-parent/legacy-app/WypozyczalniaWeb/Zwrot.aspx.vb:232`. Tu zmieniamy zachowanie celowo: błędnie wpisany kod ma kończyć się błędem, nie zaniżoną sumą szkód (decyzja właściciela procesu, DoD pkt 10).
- Znane kody szkód (kontrakt): `CASING` 50,00, `ACCESSORIES` 30,00, `DIRT` 20,00.

## Pliki startowe

- serwis zwrotu: `new-app-parent/new-app/backend/src/Wypozyczalnia.Api/Services/RentalReturnService.cs` (metoda rejestracji zwrotu)
- kody szkód: `new-app-parent/new-app/backend/src/Wypozyczalnia.Api/Model/DamageType.cs`
- `new-app-parent/new-app/backend/src/Wypozyczalnia.Api/Services/InvalidRequestException.cs` (wyjątek zamieniany na `400`)
- `new-app-parent/new-app/backend/src/Wypozyczalnia.Api/Controllers/ApiExceptionHandler.cs` (jedno miejsce mapowania wyjątków)
- kontroler zwrotu: `new-app-parent/new-app/backend/src/Wypozyczalnia.Api/Controllers/RentalController.cs`
- testy zwrotu z M3c (wzór testów serwisu i kontraktu HTTP): `new-app-parent/new-app/backend/tests/Wypozyczalnia.Tests/Characterization/RentalReturnServiceTest.cs`, `new-app-parent/new-app/backend/tests/Wypozyczalnia.Tests/Controllers/RentalControllerTest.cs`; wzór kontraktu HTTP także `new-app-parent/new-app/backend/tests/Wypozyczalnia.Tests/Controllers/ReservationControllerTest.cs`, wspólny host testowy `new-app-parent/new-app/backend/tests/Wypozyczalnia.Tests/Support/TestApplication.cs`
- `new-app-parent/new-docs/KONTRAKT-API.md`, `new-app-parent/new-docs/DEFINITION-OF-DONE.md`, instrukcje i skill z `.github/` (te same co w M3)

## Kryteria akceptacji

1. `returnedAt` wcześniejsze niż data wydania → `InvalidRequestException` z komunikatem dokładnie `Data zwrotu wczesniejsza niz data wydania`; API odpowiada `400 {"error":"Data zwrotu wczesniejsza niz data wydania"}`.
2. `returnedAt` równe dacie wydania jest dozwolone (warunek ostry jak `RentalManager.vb:99`) i kończy się `200`.
3. Kod szkody spoza `CASING`, `ACCESSORIES`, `DIRT` → `InvalidRequestException` z komunikatem dokładnie `Nieznany rodzaj szkody: <kod>`; dla `XYZ` API odpowiada `400 {"error":"Nieznany rodzaj szkody: XYZ"}`.
4. Przy odrzuceniu zwrot nie jest zapisany: wypożyczenie zostaje otwarte, sprzęt ma niezmieniony status.
5. Kolejność sprawdzeń: wypożyczenie istnieje (`404`), nie jest zamknięte (`409`), data zwrotu, kody szkód, dopiero rozliczenie.
6. Kwoty, kara i kaucja bez zmian: żaden istniejący test ani przypadek `tools/bramka/przypadki-zwrot.json` nie zmienia wartości oczekiwanej.
7. `KONTRAKT-API.md`: w sekcji zwrotu lista błędów `400` uzupełniona o oba przypadki.
8. `pwsh ./tools/bramka.ps1` kończy się `BRAMKA: OK`.

## Testy

Nowe testy, bez zmian w istniejących. Dane z `new-app-parent/new-app/backend/src/Wypozyczalnia.Api/Database/data.sql`: wypożyczenie 6 otwarte, wydane 2026-10-17T10:00.

| id | wejście (wypożyczenie 6) | oczekiwane |
|---|---|---|
| WZ-1 | `returnedAt` 2026-10-16T08:00 | `400`, `{"error":"Data zwrotu wczesniejsza niz data wydania"}`; wypożyczenie nadal otwarte |
| WZ-2 | `returnedAt` 2026-10-17T10:00 (równe wydaniu) | `200` |
| WZ-3 | `returnedAt` 2026-10-18T10:00, `damages` `["XYZ"]` | `400`, `{"error":"Nieznany rodzaj szkody: XYZ"}`; wypożyczenie nadal otwarte |

- Serwis: wyjątek, komunikat, stan wypożyczenia po odrzuceniu.
- Kontrakt HTTP: status i treść `error`.
- Uruchomienie: `pwsh ./tools/bramka.ps1` (zawiera `dotnet test` na `new-app-parent/new-app/backend/Wypozyczalnia.sln`; ta sama komenda lokalnie, w CI i u agenta w chmurze).

## Poza zakresem

- `new-app-parent/new-app/backend/src/Wypozyczalnia.Api/Database/schema.sql` i `data.sql`.
- `.github/workflows/**`, `tools/bramka.ps1`, `tools/bramka/**` (chronione przez `.github/workflows/pr-zakres.yml`).
- Liczenie kary, opłaty i kaucji.
- Walidacja daty „z przyszłości” (`legacy-app-parent/legacy-app/WypozyczalniaWeb/Zwrot.aspx.vb:207-208`): zależy od bieżącego czasu, nie wprowadzamy.
- `legacy-app-parent/` i frontend (formularz zwrotu pokazuje komunikat `error`).

## Definicja ukończenia

- [ ] Kryteria 1-8 spełnione, testy WZ-1, WZ-2, WZ-3 zielone
- [ ] `BRAMKA: OK`; brak zmian w istniejących wartościach oczekiwanych
- [ ] Zmieniony tylko serwis i kontroler zwrotu, ich testy i sekcja zwrotu w `KONTRAKT-API.md`
- [ ] Opis PR wskazuje to zgłoszenie, dowody legacy i sposób sprawdzenia
