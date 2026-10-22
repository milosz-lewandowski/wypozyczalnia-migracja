# Warstwy i kwoty (etap 3)

Czytaj, gdy piszesz kod backendu i testy przenoszonego procesu.

## Wzorzec: moduł rezerwacji

- `ReservationController` → `ReservationService` → `ReservationRepository`; nowy proces powtarza ten kształt.
- Katalogi w `new-app-parent/new-app/backend/src/Wypozyczalnia.Api/`: `Controllers/` → `Services/` → `Repositories/`.
- DTO na granicy API: `*Request` / `*Response` w `Dto/` (przestrzeń nazw `Wypozyczalnia.Dto`).
- Rejestracja serwisu i repozytorium: `Config/ServiceRegistration.cs`, obok istniejących.

## Kwoty, czas, SQL, błędy

- Kwoty wyłącznie `decimal` i `Money.Round` (`MidpointRounding.ToEven`); nigdy `double` ani `float`.
- Okresy z `TimeSpan` w minutach; reguła dób z legacy, nie dni kalendarzowe.
- SQL tylko w `Repositories/`, parametry nazwane, bez sklejania.
- Błąd API w formacie istniejących modułów: `{"error": "..."}` (`ErrorResponse`).
- Ścieżki i pola wg `new-app-parent/new-docs/KONTRAKT-API.md`.

## Testy

- Test serwisu: xUnit, nazwa = nazwa serwisu + `Test` (jak `ReservationServiceTest`).
- Wartości oczekiwane z legacy, nie z nowego kodu.
- Testy architektury (`ArchitectureTest`) muszą przejść dla nowych klas.
