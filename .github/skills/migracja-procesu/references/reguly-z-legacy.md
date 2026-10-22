# Reguły z legacy (etap 1-2)

Czytaj, gdy zbierasz reguły procesu i wartości do przypadków.

## Dowód

- Każda reguła = dowód `plik:linia` w legacy (ścieżka od korzenia repo); bez dowodu reguły nie ma.
- Liczy się wykonywalny kod VB i SQL, nie komentarz; komentarz bywa nieaktualny.
- Mnożnik i próg bierz z kodu, nie z komentarza ani z dokumentu.

## Gdzie szukać

- Start: router `legacy-app-parent/AGENTS.md` (mapa procesów, wyjątki od warstw).
- Reguła bywa liczona dwa razy: szacunek w UI (code-behind) i wynik w Core; sprawdź oba miejsca.
- SQL bywa poza warstwą danych, także w stronach; stałe mają kopie.

## Wartości do przypadków

- Okresy w minutach wg reguły dób z legacy (z tolerancją z legacy); nigdy dni kalendarzowe.
- Zaokrąglenie jak w legacy: połówki do parzystej.
- Granice tolerancji i progów = osobne przypadki (tuż przed i tuż za granicą).
- Wyprowadzenie wartości oczekiwanej w komentarzu testu, z dowodem `plik:linia`.
- Przypadki czerwone przed implementacją: kompilują się, padają na asercjach.
