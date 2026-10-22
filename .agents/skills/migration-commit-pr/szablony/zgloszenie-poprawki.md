# Szablon zgłoszenia poprawki (orkiestrator → migration-implementer)

Format bazowy: `new-app-parent/new-docs/ZGLOSZENIE-BLEDU.md`, uzupełniony o dane z review.

```markdown
# <ID ustalenia> [<POZIOM>] P<n>: <proces i objaw w jednym zdaniu>

Runda: <N>/3 · Kategoria: <parity | jakość | bezpieczeństwo | edge-case>

## Na czym polega błąd
<co w kodzie działa źle, dlaczego; dowód: <ścieżka>:<linia>>

## Na czym polega potencjalna podatność / usterka
<skutek: rozbieżność kwot z legacy, możliwość ataku, utrata danych, 500, wyścig...; kto i jak może to wywołać>

## Kroki
1. <wywołanie API albo akcja w UI>

## Oczekiwane i faktyczne
- oczekiwane: <wartość, status, komunikat>; źródło: <dowód legacy albo przypadek bramki>
- faktyczne: <wartość>

## Dane
<identyfikatory z danych testowych, wyłącznie syntetyczne>

## Środowisko
gałąź <gałąź>, commit <sha albo drzewo robocze>

## Wpływ na zgodność z legacy
<poprawka przywraca zgodność / nie zmienia zachowania legacy / UWAGA: zmieniłaby zachowanie legacy → decyzja właściciela (DoD 10)>

## Wymagany test regresyjny
<nazwa i asercja testu, który bez poprawki nie przechodzi>
```
