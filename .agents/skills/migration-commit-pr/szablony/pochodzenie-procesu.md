# P<n> — <nazwa procesu>: pochodzenie i zgodność z legacy

> Szablon skilla `migration-commit-pr`. Zapisz jako `new-app-parent/new-docs/migracja/P<n>-<slug>.md`. Każde twierdzenie o legacy poprzyj dowodem w backtickach (ścieżka od korzenia repo i linia albo zakres). Reguły wyprowadzaj z wykonywalnego VB/SQL, nie z komentarzy. Po zapisaniu uruchom `python tools\sprawdz_dowody.py`. Oddzielaj fakt, interpretację i niewiadomą.

| Pole | Wartość |
|---|---|
| Proces | P<n> — <nazwa> |
| Status w supervisorze | zmigrowany / częściowo |
| Gałąź | <gałąź> |
| Commit / PR | <uzupełnij po commicie> |
| Źródło prac | <sesja, agent, data> |
| Analiza legacy | [08 — sekcja P<n>](<link względny do 08>) |

## 1. Skąd pochodzi proces w legacy

| Warstwa | Element | Dowód |
|---|---|---|
| UI (handler) | <Strona.aspx.vb, metoda> | <ścieżka>:<linia-od>-<linia-do> |
| Logika | <Manager.metoda> | <ścieżka>:<linia> |
| Dane | <DAL / procedura / SQL inline> | <ścieżka>:<linia> |
| Tabele | <tabele> | <ścieżka schematu>:<linia> |

## 2. Reguły biznesowe

| # | Reguła | Legacy | New-app | Test potwierdzający |
|---|---|---|---|---|
| 1 | <np. tolerancja 60 minut> | <ścieżka>:<linia> | <ścieżka>:<linia> | <klasa testu, DisplayName> |

## 3. Mapowanie komponentów legacy → new-app

| Legacy | New-app |
|---|---|
| <Manager / DAL / strona> | <Service / Repository / Controller / Page / api> |

## 4. Wyjątki, decyzje i niejednoznaczności

- Wyjątki W1–W6, które dotyczą procesu (np. W5: wykonywany mnożnik kary 1,5, nie komentarz 100%), wraz z decyzją.
- Niejednoznaczności do decyzji właściciela procesu (DoD 10): <opis, dowody obu wersji>.

## 5. Kontrakt API

- `<METODA> /api/...`: parametry, odpowiedź, błędy (zgodnie z `new-app-parent/new-docs/KONTRAKT-API.md`).

## 6. Jak potwierdzono zgodność

- testy characterization: <klasy i przypadki>;
- testy kontrolerów: <klasy>;
- przypadki akceptacyjne bramki: `tools/bramka/przypadki-<proces>.json` (<liczba> przypadków, wynik);
- review `migration-reviewer`: <werdykt, rundy poprawek>;
- `.\tools\bramka.ps1`: <wynik>.

## 7. Dotknięte inne procesy

- P<m>: <pliki, co i dlaczego> (wpis `odnotuj` w supervisorze) | brak.

## 8. Fakty, interpretacje, niewiadome

- **Fakty:** ...
- **Interpretacje:** ...
- **Niewiadome:** ...
