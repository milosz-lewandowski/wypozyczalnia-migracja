-- Dane startowe nowej aplikacji (SQLite w pamieci).
-- Literaly TIMESTAMP '...' i DATE '...' (skladnia H2) zamienia na tekst DatabaseInitializer.
-- Dane w 100% syntetyczne. Te same Id i wartosci co seed legacy legacy-app-parent/legacy-app/Database/03_seed.sql
-- (Klienci :28-51, Sprzet :63-92, Taryfy :104-113, Rezerwacje :125-136, Wypozyczenia :148-159),
-- zeby kazda wartosc oczekiwana w tescie dalo sie policzyc z kodu legacy na tych samych danych.
-- Pominiete kolumny kontaktowe klientow, faktury i dziennik (poza zakresem).
-- Uwaga dla testow: zwrot zamyka wypozyczenie (status CLOSED) - testy mutujace @Transactional,
-- e2e i demo wymagaja swiezego backendu (baza w pamieci = restart przywraca te dane).

-- Klienci (03_seed.sql:28-51); discount_tier = ProgRabatu, blocked = Zablokowany
INSERT INTO customers (id, first_name, last_name, company_name, discount_tier, blocked, block_reason) VALUES
 (1,  'Anna',      'Nowak',       NULL,                      0, FALSE, NULL),
 (2,  'Piotr',     'Kowalczyk',   NULL,                      1, FALSE, NULL),
 (3,  'Katarzyna', 'Wiśniewska',  NULL,                      2, FALSE, NULL),
 (4,  'Tomasz',    'Zieliński',   NULL,                      3, FALSE, NULL),
 (5,  'Magdalena', 'Wójcik',      NULL,                      0, TRUE,  'zalegla platnosc'),
 (6,  'Michał',    'Kamiński',    NULL,                      1, FALSE, NULL),
 (7,  'Agnieszka', 'Dąbrowska',   NULL,                      0, FALSE, NULL),
 (8,  'Paweł',     'Szymański',   NULL,                      0, FALSE, NULL),
 (9,  'Ewa',       'Mazur',       'Firma Przykładowa Alfa',  2, FALSE, NULL),
 (10, 'Krzysztof', 'Krawczyk',    'Firma Przykładowa Beta',  3, FALSE, NULL),
 (11, 'Joanna',    'Piotrowska',  NULL,                      0, FALSE, NULL),
 (12, 'Marcin',    'Grabowski',   NULL,                      0, FALSE, NULL);

-- Sprzet (03_seed.sql:63-92); status D->AVAILABLE, W->RENTED, S->SERVICE, X->RETIRED
INSERT INTO equipment (id, inventory_number, name, category, daily_rate, deposit, status) VALUES
 (1,  'BUD-0001', 'Betoniarka 160 l',                    'BUD',  45.00,  300.00, 'AVAILABLE'),
 (2,  'BUD-0002', 'Zagęszczarka gruntowa',               'BUD',  72.35,  500.00, 'AVAILABLE'),
 (3,  'BUD-0003', 'Młot wyburzeniowy 15 kg',             'BUD',  89.90,  600.00, 'AVAILABLE'),
 (4,  'BUD-0004', 'Rusztowanie warszawskie (zestaw)',    'BUD',  38.50,  400.00, 'RENTED'),
 (5,  'BUD-0005', 'Piła do glazury',                     'BUD',  52.15,  300.00, 'SERVICE'),
 (6,  'OGR-0001', 'Glebogryzarka spalinowa',             'OGR',  28.85,  250.00, 'AVAILABLE'),
 (7,  'OGR-0002', 'Rębak do gałęzi',                     'OGR', 119.00,  800.00, 'RENTED'),
 (8,  'OGR-0003', 'Kosiarka spalinowa',                  'OGR',  35.60,  200.00, 'AVAILABLE'),
 (9,  'OGR-0004', 'Nożyce do żywopłotu',                 'OGR',  24.45,  150.00, 'RETIRED'),
 (10, 'IMP-0001', 'Namiot imprezowy 6x12 m',             'IMP', 210.00, 1000.00, 'AVAILABLE'),
 (11, 'IMP-0002', 'Zestaw nagłośnieniowy 2x500 W',       'IMP', 149.99,  900.00, 'AVAILABLE'),
 (12, 'IMP-0003', 'Stół cateringowy (komplet 10 szt.)',  'IMP',  60.25,  200.00, 'RENTED'),
 (13, 'TRN-0001', 'Przyczepa lekka 750 kg',              'TRN',  95.00,  700.00, 'AVAILABLE'),
 (14, 'TRN-0002', 'Wózek paletowy ręczny',               'TRN',  33.33,  250.00, 'AVAILABLE'),
 (15, 'TRN-0003', 'Taczka budowlana',                    'TRN',  12.50,   50.00, 'AVAILABLE');

-- Taryfy (03_seed.sql:104-113)
INSERT INTO tariffs (id, category, weekend_multiplier, weekly_discount_percent, valid_from) VALUES
 (1, 'BUD', 1.20, 10.00, DATE '2025-01-01'),
 (2, 'BUD', 1.25, 10.00, DATE '2026-01-01'),
 (3, 'OGR', 1.20, 10.00, DATE '2026-01-01'),
 (4, 'IMP', 1.50,  5.00, DATE '2026-01-01'),
 (5, 'TRN', 1.00, 15.00, DATE '2026-01-01');

-- Rezerwacje (03_seed.sql:125-136); status N->NEW, P->CONFIRMED, A->CANCELLED, Z->COMPLETED
INSERT INTO reservations (id, customer_id, equipment_id, date_from, date_to, status, estimated_price, notes, created_at) VALUES
 (1, 2,  6, TIMESTAMP '2026-10-12 08:00:00', TIMESTAMP '2026-10-17 08:00:00', 'COMPLETED', 141.36, NULL,                      TIMESTAMP '2026-10-08 14:00:00'),
 (2, 1,  2, TIMESTAMP '2026-10-13 09:00:00', TIMESTAMP '2026-10-15 09:30:00', 'COMPLETED', 144.70, 'odbiór osobisty',         TIMESTAMP '2026-10-10 10:20:00'),
 (3, 4,  2, TIMESTAMP '2026-10-20 09:00:00', TIMESTAMP '2026-10-22 09:00:00', 'CONFIRMED', 133.12, NULL,                      TIMESTAMP '2026-10-14 16:45:00'),
 (4, 3, 10, TIMESTAMP '2026-10-24 10:00:00', TIMESTAMP '2026-10-26 10:00:00', 'NEW',       598.50, 'montaż na miejscu',       TIMESTAMP '2026-10-15 09:10:00'),
 (5, 1, 13, TIMESTAMP '2026-10-09 10:00:00', TIMESTAMP '2026-10-16 10:00:00', 'CANCELLED', 565.25, 'anulowana telefonicznie', TIMESTAMP '2026-10-01 11:30:00'),
 (6, 6, 11, TIMESTAMP '2026-10-27 08:00:00', TIMESTAMP '2026-10-28 08:00:00', 'NEW',       146.99, NULL,                      TIMESTAMP '2026-10-16 13:05:00');

-- Wypozyczenia (03_seed.sql:148-159); status O->OPEN, Z->CLOSED
-- Otwarte z planowanym zwrotem w przeszlosci (wzgledem 22.10.2026): 4, 5, 6.
INSERT INTO rentals (id, reservation_id, customer_id, equipment_id, issued_at, planned_return, returned_at, deposit_taken, fee, penalty, deposit_returned, status) VALUES
 (1, 1,    2,  6, TIMESTAMP '2026-10-12 08:00:00', TIMESTAMP '2026-10-17 08:00:00', TIMESTAMP '2026-10-17 08:40:00', 250.00, 141.36,   0.00, 250.00, 'CLOSED'),
 (2, 2,    1,  2, TIMESTAMP '2026-10-13 09:00:00', TIMESTAMP '2026-10-15 09:30:00', TIMESTAMP '2026-10-16 11:30:00', 500.00, 144.70, 217.05, 282.95, 'CLOSED'),
 (3, NULL, 7,  3, TIMESTAMP '2026-10-05 07:00:00', TIMESTAMP '2026-10-07 07:00:00', TIMESTAMP '2026-10-07 08:01:00', 600.00, 179.80, 134.85, 465.15, 'CLOSED'),
 (4, NULL, 10, 4, TIMESTAMP '2026-10-01 08:00:00', TIMESTAMP '2026-10-15 08:00:00', NULL,                             400.00, NULL,     NULL,   NULL,   'OPEN'),
 (5, NULL, 9, 12, TIMESTAMP '2026-10-16 09:00:00', TIMESTAMP '2026-10-19 09:00:00', NULL,                             200.00, NULL,     NULL,   NULL,   'OPEN'),
 (6, NULL, 12, 7, TIMESTAMP '2026-10-17 10:00:00', TIMESTAMP '2026-10-18 10:00:00', NULL,                             800.00, NULL,     NULL,   NULL,   'OPEN');

-- Nowe Id z aplikacji od 100 (jak RESTART WITH 100 w H2): licznik AUTOINCREMENT trzyma tabela sqlite_sequence.
UPDATE sqlite_sequence SET seq = 99 WHERE name IN ('reservations', 'rentals');
