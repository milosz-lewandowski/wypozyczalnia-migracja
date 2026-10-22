-- Dodatkowe otwarte wypozyczenia tylko dla przypadkow akceptacyjnych zwrotu (tools/bramka/przypadki-zwrot.json).
-- Ladowane przez akceptacja.py po data.sql aplikacji (backend bramki na osobnym porcie, H2 w pamieci).
-- Dane w 100% syntetyczne. Klient 1 (prog 0), wydanie pn 2026-10-19 10:00, planowany zwrot wt 2026-10-20 10:00.
INSERT INTO rentals (id, reservation_id, customer_id, equipment_id, issued_at, planned_return, returned_at, deposit_taken, fee, penalty, deposit_returned, status) VALUES
 (901, NULL, 1,  2, TIMESTAMP '2026-10-19 10:00:00', TIMESTAMP '2026-10-20 10:00:00', NULL, 500.00, NULL, NULL, NULL, 'OPEN'),
 (902, NULL, 1,  3, TIMESTAMP '2026-10-19 10:00:00', TIMESTAMP '2026-10-20 10:00:00', NULL, 600.00, NULL, NULL, NULL, 'OPEN'),
 (903, NULL, 1,  7, TIMESTAMP '2026-10-19 10:00:00', TIMESTAMP '2026-10-20 10:00:00', NULL, 800.00, NULL, NULL, NULL, 'OPEN'),
 (904, NULL, 1,  7, TIMESTAMP '2026-10-19 10:00:00', TIMESTAMP '2026-10-20 10:00:00', NULL, 800.00, NULL, NULL, NULL, 'OPEN'),
 (905, NULL, 1,  2, TIMESTAMP '2026-10-19 10:00:00', TIMESTAMP '2026-10-20 10:00:00', NULL, 500.00, NULL, NULL, NULL, 'OPEN'),
 (906, NULL, 1,  7, TIMESTAMP '2026-10-19 10:00:00', TIMESTAMP '2026-10-20 10:00:00', NULL, 800.00, NULL, NULL, NULL, 'OPEN'),
 (907, NULL, 1,  7, TIMESTAMP '2026-10-19 10:00:00', TIMESTAMP '2026-10-20 10:00:00', NULL, 800.00, NULL, NULL, NULL, 'OPEN'),
 (908, NULL, 1,  2, TIMESTAMP '2026-10-19 10:00:00', TIMESTAMP '2026-10-20 10:00:00', NULL, 500.00, NULL, NULL, NULL, 'OPEN'),
 (909, NULL, 1, 15, TIMESTAMP '2026-10-19 10:00:00', TIMESTAMP '2026-10-20 10:00:00', NULL, 300.00, NULL, NULL, NULL, 'OPEN');
