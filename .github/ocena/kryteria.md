# Kryteria oceny PR

Oceń wyłącznie dostarczony diff. Jeśli opis PR wskazuje zadanie z `new-app-parent/new-docs/zadania/`, oceniaj względem jego kryteriów akceptacji. Zwróć wynik dokładnie w poniższym formacie.

| Kryterium | OK / UWAGA / BŁĄD | Uzasadnienie |
| --- | --- | --- |
| Zakres zgłoszenia bez zmian poza nim |  | Czy każda zmiana jest konieczna do zakresu i dotyczy plików z sekcji „Pliki startowe” albo jest uzasadniona w opisie PR (DoD pkt 9). |
| Kryteria akceptacji |  | Kryteria zgłoszenia spełnione, niespełnione i niesprawdzalne z diffu. |
| Zgodność z kontraktem |  | Endpointy, pola, kody i format błędu `{"error": "..."}` zgodne z `new-app-parent/new-docs/KONTRAKT-API.md`; wskaż reguły legacy przeniesione inaczej niż w kontrakcie. |
| Kwoty i daty |  | Kwoty wyłącznie typem `Money` (`decimal`), zaokrąglenie bankierskie (`MidpointRounding.ToEven`); daty i doby przez istniejące metody serwisów. Wskaż `double`, `float`, `MidpointRounding.AwayFromZero`, obcinanie. |
| Bramka i testy |  | Czy `pwsh ./tools/bramka.ps1` może dać `BRAMKA: OK`; czy testy obejmują przypadki brzegowe zadania, nie tylko szczęśliwą ścieżkę; czy żaden istniejący test nie został osłabiony. |
| Instrukcje repo przestrzegane |  | Zgodność z `AGENTS.md`, `.github/copilot-instructions.md`, instrukcjami i skillem z `.github/` oraz `new-app-parent/new-docs/DEFINITION-OF-DONE.md`. |
| Brak zmian chronionych |  | Każda zmiana pliku chronionego: `new-app-parent/new-app/backend/src/Wypozyczalnia.Api/Database/schema.sql`, `.github/workflows/`, `tools/bramka.ps1`, `tools/bramka/`. |
| Czytelność/spójność |  | Nazewnictwo, warstwy (`Controllers` → `Services` → `Repositories`), zgodność z istniejącym stylem. |
| Ryzyka/braki |  | Ryzyka, niezweryfikowane założenia, brakujące testy albo przypadki brzegowe. |

## Poprawki dla agenta

@copilot Podaj jedną konkretną wiadomość gotową do wklejenia jako komentarz PR: najpierw najważniejsze poprawki, potem testy albo ryzyka; jeśli nie ma uwag, napisz wprost, że nie znaleziono problemów.
