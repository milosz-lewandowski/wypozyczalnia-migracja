# Szablon wiadomości commita

Nagłówek (do 72 znaków):

- `feat(P<n>-<slug>): <co przeniesiono z legacy>`: migracja procesu
- `fix(P<n>-<slug>): <co naprawiono>`: poprawka w procesie
- `chore(ai): <opis>`: customizacje AI (skille, agenci, hooki)
- `ci(copilot): <opis>`: środowisko Copilot cloud agenta
- `docs(<zakres>): <opis>`: dokumentacja

Treść:

```text
feat(P<n>-<slug>): <opis>

Co:
- <endpointy, serwisy, UI, testy>

Skąd w legacy:
- <ścieżka>:<linia> <element>

Jak sprawdzone:
- dotnet test: <N> testów zielono
- bramka: <wynik, przypadki procesu>
- review migration-reviewer: <werdykt>, rund poprawek: <N>

Dokument pochodzenia: new-app-parent/new-docs/migracja/P<n>-<slug>.md
Stan migracji: P<n> -> <status>; odnotowano: <P<m> albo brak>

Co-authored-by: Copilot <223556219+Copilot@users.noreply.github.com>
```
