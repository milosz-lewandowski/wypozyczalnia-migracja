---
name: Zadanie dla agenta
about: Mały feature do wykonania przez agenta w chmurze (Copilot coding agent)
title: "[agent] "
labels: ["zadanie-agent"]
---

## Cel

<!-- Jedno, dwa zdania: co ma działać po zmianie i po co. -->

## Stan obecny

<!-- Co dzieje się dziś; dowody `plik:linia` (legacy albo nowa aplikacja). -->

-

## Pliki startowe

<!-- Ścieżki od korzenia repo: kod do zmiany, wzorzec do naśladowania, kontrakt (`new-app-parent/new-docs/KONTRAKT-API.md`). -->

-

## Kryteria akceptacji

<!-- 5-7 punktów, każdy sprawdzalny testem albo poleceniem. Nie umiesz tego spisać = zadanie nie jest gotowe dla chmury. -->

1.
2.
3.

## Testy

<!-- Jakie testy mają powstać, jakie wartości asertują; polecenie uruchomienia. -->

-

## Poza zakresem

<!-- Czego agent nie rusza: pliki chronione (`schema.sql`, `.github/workflows/**`, `tools/bramka.ps1`, `tools/bramka/**`), kod legacy, sąsiednie procesy. -->

-

## Definicja ukończenia

- [ ] Wszystkie kryteria akceptacji spełnione
- [ ] `pwsh ./tools/bramka.ps1` kończy się `BRAMKA: OK`
- [ ] Zmiany tylko w plikach z zakresu; brak zmian w plikach chronionych
- [ ] Opis PR wskazuje to zgłoszenie: co zmieniono, dlaczego, jak sprawdzono (`new-app-parent/new-docs/DEFINITION-OF-DONE.md`)
