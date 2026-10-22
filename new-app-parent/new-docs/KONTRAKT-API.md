# Kontrakt API backendu

Uzgodniony kontrakt między backendem a frontendem. Zmiana ścieżki, parametru albo pola = zmiana kontraktu (do uzgodnienia z frontendem i QA).

## Zasady wspólne

- prefiks `/api`, JSON w UTF-8
- daty i godziny: ISO bez strefy, `yyyy-MM-ddTHH:mm` (w żądaniach sekundy opcjonalne)
- kwoty: liczby dziesiętne, 2 miejsca po przecinku
- błąd: zawsze `{"error": "<komunikat>"}`; kody: `400` złe dane albo brak parametru, `404` brak obiektu, `409` konflikt stanu

## Endpointy istniejące

### `GET /api/ping`

Sprawdzenie, czy backend działa. Odpowiedź `200`: `{"status": "ok"}`.

### `GET /api/equipment/available`

Dostępny sprzęt w kategorii i terminie.

- parametry: `category` (wymagany), `from` (wymagany), `to` (wymagany)
- odpowiedź `200`: lista obiektów `id`, `inventoryNumber`, `name`, `category`, `dailyRate`, `deposit`
- błędy: `400`

### `POST /api/reservations`

Nowa rezerwacja.

- ciało: `customerId`, `equipmentId`, `from`, `to` (wymagane), `notes` (opcjonalne)
- odpowiedź `201`: rezerwacja (pola niżej)
- błędy: `400`, `404`, `409`

### `GET /api/reservations`

Lista rezerwacji.

- parametry: `customerId` (opcjonalny)
- odpowiedź `200`: lista rezerwacji
- pola rezerwacji: `id`, `customerId`, `equipmentId`, `from`, `to`, `status`, `estimatedPrice`, `notes`

## Endpointy zaplanowane (procesy przenoszone z legacy)

### Wycena: `GET /api/pricing/quote`

Wycena wynajmu sprzętu na okres, opcjonalnie dla klienta.

- parametry: `equipmentId` (wymagany), `from` (wymagany, wydanie), `to` (wymagany, zwrot), `customerId` (opcjonalny)
- odpowiedź `200`: `equipmentId`, `from`, `to`, `customerId`, `days`, `weekendDays`, `dailyRate`, `weekendRate`, `weeklyDiscountPercent`, `customerDiscountPercent`, `amount`
- błędy: `400` (brak parametru, zły format, zły zakres dat), `404` (nieznany sprzęt), `409` (brak taryfy na dzień wydania)

### Zwrot: `GET /api/rentals`

Otwarte wypożyczenia do zwrotu.

- odpowiedź `200`: lista obiektów `id`, `customerId`, `customerName`, `equipmentId`, `equipmentName`, `issuedAt`, `plannedReturn`, `depositTaken`

### Zwrot: `POST /api/rentals/{id}/return`

Rejestracja zwrotu z rozliczeniem opłaty, kary, szkód i kaucji.

- ciało: `returnedAt` (wymagane), `damages` (opcjonalne, lista kodów szkód: `CASING`, `ACCESSORIES`, `DIRT`)
- odpowiedź `200`: `rentalId`, `returnedAt`, `fee`, `lateMinutes`, `lateDays`, `penalty`, `damagesTotal`, `depositTaken`, `depositReturned`, `status`
- błędy: `400` (brak `returnedAt`, zły format), `404` (nieznane wypożyczenie), `409` (wypożyczenie już zamknięte)
