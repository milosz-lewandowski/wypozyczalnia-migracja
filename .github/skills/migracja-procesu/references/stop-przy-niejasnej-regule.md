# Stop przy niejasnej regule (etap 6)

Czytaj, gdy dwa źródła reguły mówią co innego.

## Co jest dwuznaczne

- kod liczy inaczej, niż mówi komentarz obok;
- dwa miejsca liczą tę samą wartość różnymi formułami (np. UI kontra Core);
- dokument opisuje regułę inaczej niż kod.

## Co robisz

1. Stop: nie wybierasz wersji sam i nie zmieniasz oczekiwanej wartości.
2. Raportujesz obie wersje z dowodem `plik:linia`.
3. Podajesz skutek: które przypadki lub testy zmienią wynik przy każdej wersji.
4. Pytasz człowieka (właściciela procesu, DoD punkt 10 w `new-app-parent/new-docs/DEFINITION-OF-DONE.md`).

## Po decyzji

- Implementujesz wybraną wersję.
- Decyzja w komentarzu przy metodzie: co wybrano, dlaczego, dowód `plik:linia` obu wersji.
- Domyślnie (bez innej decyzji) obowiązuje wykonywalny kod legacy, nie komentarz.
