# Bramka i diff (etap 4-5)

Czytaj, gdy uruchamiasz bramkę albo bramka jest czerwona.

## Uruchomienie

- Z korzenia repo: `.\tools\bramka.ps1`; opis: `new-app-parent/new-docs/BRAMKA.md`.
- Bramka startuje własny backend na porcie 8089; zajęty port → `$env:BRAMKA_PORT='<wolny port>'` przed uruchomieniem.
- Cel: ostatnia linia `BRAMKA: OK`.

## Czytanie wyniku

- reguły zespołu: naruszenie wskazuje plik `.cs` i regułę;
- testy: nazwa testu i asercja (oczekiwana kontra otrzymana);
- przypadki akceptacyjne: proces, przypadek, oczekiwana kontra otrzymana wartość;
- "pominięto": proces jeszcze nie przeniesiony; to nie błąd.

## Czerwona bramka

1. `git diff` (i `git status`) przed jakąkolwiek poprawką: co zmieniła ta praca.
2. Przyczyna w kodzie tej zmiany → poprawiasz tylko ten kod.
3. Przyczyna poza zmianą → nie poprawiasz przy okazji; zgłaszasz.
4. Nigdy nie zmieniasz oczekiwanych wartości testów ani przypadków bramki, żeby było zielono.
5. Po poprawce bramka od nowa, aż `BRAMKA: OK`.
