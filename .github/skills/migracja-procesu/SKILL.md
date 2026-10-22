---
name: migracja-procesu
description: Przeniesienie procesu biznesowego z legacy (legacy-app-parent/legacy-app/) do nowej aplikacji (new-app-parent/new-app/) albo naprawa czerwonej bramki po takiej zmianie. Użyj, gdy zadanie brzmi "przenieś <proces>" lub "zmigruj <proces>".
---

# Migracja procesu z legacy

Cel: proces w nowej aplikacji daje te same wyniki co legacy; każda reguła ma dowód w kodzie legacy.

## Etapy (kolejność stała)

1. **Rozpoznaj** proces przez router `legacy-app-parent/AGENTS.md`; wypisz reguły z dowodem `plik:linia`, także drugie miejsce liczenia.
2. **Przypadki** z wartości legacy (progi, zaokrąglenia, stawki) jako testy; czerwone przed kodem.
3. **Warstwy** wg modułu-wzorca rezerwacji: kontroler → serwis → repozytorium, DTO na granicy, kwoty przez `Money`.
4. **Bramka**: `.\tools\bramka.ps1` (Linux: `pwsh tools/bramka.ps1`) aż do `BRAMKA: OK`.
5. **Czerwona bramka → diff**: najpierw `git diff`, poprawiasz tylko kod tej zmiany.
6. **Dwuznaczna reguła → stop**: kod kontra komentarz albo dwie formuły = pytanie do człowieka, oczekiwana wartość bez zmian.

## Kiedy czytać

| plik | czytaj, gdy |
|---|---|
| [references/reguly-z-legacy.md](references/reguly-z-legacy.md) | etap 1-2: zbierasz reguły i wartości przypadków |
| [references/warstwy-i-kwoty.md](references/warstwy-i-kwoty.md) | etap 3: piszesz kod backendu i testy |
| [references/bramka-i-diff.md](references/bramka-i-diff.md) | etap 4-5: uruchamiasz bramkę albo jest czerwona |
| [references/stop-przy-niejasnej-regule.md](references/stop-przy-niejasnej-regule.md) | etap 6: dwa źródła mówią co innego |

## Odwołania

- gotowe = `new-app-parent/new-docs/DEFINITION-OF-DONE.md`
- bramka = `new-app-parent/new-docs/BRAMKA.md`
- ścieżki, pola, błędy API = `new-app-parent/new-docs/KONTRAKT-API.md`
- przegląd diffu: agent `recenzent-migracji` (tylko odczyt)

## Czego nie robić

- Nie zmieniaj `legacy-app-parent/` ani oczekiwanej wartości istniejącego testu.
- Nie przenoś kształtu kodu legacy (logika w stronach, sklejany SQL); przenoś regułę.
