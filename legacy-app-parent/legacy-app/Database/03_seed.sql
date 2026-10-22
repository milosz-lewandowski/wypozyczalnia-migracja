-- Modul: 03_seed.sql - dane testowe bazy Wypozyczalnia | Autor: zespol IT | 2014-03-11
-- Historia: 2014-03-11 JK utworzenie
--           2019-02-14 PT odswiezenie danych (klienci firmowi, faktury)
-- TYLKO srodowisko testowe. Dane fikcyjne, numery dokumentow nie przechodza walidacji.

USE Wypozyczalnia
GO

SET NOCOUNT ON
GO

DELETE FROM dbo.PozycjeFaktur
DELETE FROM dbo.Faktury
DELETE FROM dbo.Wypozyczenia
DELETE FROM dbo.Rezerwacje
DELETE FROM dbo.Taryfy
DELETE FROM dbo.Sprzet
DELETE FROM dbo.Klienci
DELETE FROM dbo.DziennikOperacji
GO

-- ------------------------------------------------------------
-- Klienci
-- ------------------------------------------------------------
SET IDENTITY_INSERT dbo.Klienci ON
GO

INSERT INTO dbo.Klienci (Id, Imie, Nazwisko, NazwaFirmy, Email, Telefon, NumerDokumentu, NIP, Typ, ProgRabatu, Zablokowany, PowodBlokady, DataRejestracji)
VALUES (1, N'Anna', N'Nowak', NULL, N'anna.nowak@example.pl', '+48 000 000 001', 'ZZZ000001', NULL, 'I', 0, 0, NULL, '2024-02-12T10:15:00')
INSERT INTO dbo.Klienci (Id, Imie, Nazwisko, NazwaFirmy, Email, Telefon, NumerDokumentu, NIP, Typ, ProgRabatu, Zablokowany, PowodBlokady, DataRejestracji)
VALUES (2, N'Piotr', N'Kowalczyk', NULL, N'piotr.kowalczyk@example.pl', '+48 000 000 002', 'ZZZ000002', NULL, 'I', 1, 0, NULL, '2024-03-05T12:40:00')
INSERT INTO dbo.Klienci (Id, Imie, Nazwisko, NazwaFirmy, Email, Telefon, NumerDokumentu, NIP, Typ, ProgRabatu, Zablokowany, PowodBlokady, DataRejestracji)
VALUES (3, N'Katarzyna', N'Wiśniewska', NULL, N'katarzyna.wisniewska@example.pl', '+48 000 000 003', 'ZZZ000003', NULL, 'I', 2, 0, NULL, '2024-05-21T09:05:00')
INSERT INTO dbo.Klienci (Id, Imie, Nazwisko, NazwaFirmy, Email, Telefon, NumerDokumentu, NIP, Typ, ProgRabatu, Zablokowany, PowodBlokady, DataRejestracji)
VALUES (4, N'Tomasz', N'Zieliński', NULL, N'tomasz.zielinski@example.pl', '+48 000 000 004', 'ZZZ000004', NULL, 'I', 3, 0, NULL, '2024-06-30T16:20:00')
INSERT INTO dbo.Klienci (Id, Imie, Nazwisko, NazwaFirmy, Email, Telefon, NumerDokumentu, NIP, Typ, ProgRabatu, Zablokowany, PowodBlokady, DataRejestracji)
VALUES (5, N'Magdalena', N'Wójcik', NULL, N'magdalena.wojcik@example.pl', '+48 000 000 005', 'ZZZ000006', NULL, 'I', 0, 1, N'zalegla platnosc', '2024-09-11T11:00:00')
INSERT INTO dbo.Klienci (Id, Imie, Nazwisko, NazwaFirmy, Email, Telefon, NumerDokumentu, NIP, Typ, ProgRabatu, Zablokowany, PowodBlokady, DataRejestracji)
VALUES (6, N'Michał', N'Kamiński', NULL, N'michal.kaminski@example.pl', '+48 000 000 006', 'ZZZ000007', NULL, 'I', 1, 0, NULL, '2025-01-17T13:30:00')
INSERT INTO dbo.Klienci (Id, Imie, Nazwisko, NazwaFirmy, Email, Telefon, NumerDokumentu, NIP, Typ, ProgRabatu, Zablokowany, PowodBlokady, DataRejestracji)
VALUES (7, N'Agnieszka', N'Dąbrowska', NULL, N'agnieszka.dabrowska@example.pl', '+48 000 000 007', 'ZZZ000008', NULL, 'I', 0, 0, NULL, '2025-03-08T08:45:00')
INSERT INTO dbo.Klienci (Id, Imie, Nazwisko, NazwaFirmy, Email, Telefon, NumerDokumentu, NIP, Typ, ProgRabatu, Zablokowany, PowodBlokady, DataRejestracji)
VALUES (8, N'Paweł', N'Szymański', NULL, N'pawel.szymanski@example.pl', '+48 000 000 008', 'ZZZ000009', NULL, 'I', 0, 0, NULL, '2025-05-26T17:10:00')
INSERT INTO dbo.Klienci (Id, Imie, Nazwisko, NazwaFirmy, Email, Telefon, NumerDokumentu, NIP, Typ, ProgRabatu, Zablokowany, PowodBlokady, DataRejestracji)
VALUES (9, N'Ewa', N'Mazur', N'Firma Przykładowa Alfa', N'ewa.mazur@example.pl', '+48 000 000 009', 'ZZZ000010', '0000000001', 'F', 2, 0, NULL, '2025-07-02T10:00:00')
INSERT INTO dbo.Klienci (Id, Imie, Nazwisko, NazwaFirmy, Email, Telefon, NumerDokumentu, NIP, Typ, ProgRabatu, Zablokowany, PowodBlokady, DataRejestracji)
VALUES (10, N'Krzysztof', N'Krawczyk', N'Firma Przykładowa Beta', N'krzysztof.krawczyk@example.pl', '+48 000 000 010', 'ZZZ000011', '0000000002', 'F', 3, 0, NULL, '2025-09-15T14:25:00')
INSERT INTO dbo.Klienci (Id, Imie, Nazwisko, NazwaFirmy, Email, Telefon, NumerDokumentu, NIP, Typ, ProgRabatu, Zablokowany, PowodBlokady, DataRejestracji)
VALUES (11, N'Joanna', N'Piotrowska', NULL, N'joanna.piotrowska@example.pl', '+48 000 000 011', 'ZZZ000012', NULL, 'I', 0, 0, NULL, '2026-02-03T09:50:00')
INSERT INTO dbo.Klienci (Id, Imie, Nazwisko, NazwaFirmy, Email, Telefon, NumerDokumentu, NIP, Typ, ProgRabatu, Zablokowany, PowodBlokady, DataRejestracji)
VALUES (12, N'Marcin', N'Grabowski', NULL, N'marcin.grabowski@example.pl', '+48 000 000 012', 'ZZZ000013', NULL, 'I', 0, 0, NULL, '2026-04-19T15:35:00')
GO

SET IDENTITY_INSERT dbo.Klienci OFF
GO

-- ------------------------------------------------------------
-- Sprzet
-- ------------------------------------------------------------
SET IDENTITY_INSERT dbo.Sprzet ON
GO

INSERT INTO dbo.Sprzet (Id, NumerInwentarzowy, Nazwa, Kategoria, StawkaDobowa, Kaucja, Status)
VALUES (1, 'BUD-0001', N'Betoniarka 160 l', 'BUD', 45.00, 300.00, 'D')
INSERT INTO dbo.Sprzet (Id, NumerInwentarzowy, Nazwa, Kategoria, StawkaDobowa, Kaucja, Status)
VALUES (2, 'BUD-0002', N'Zagęszczarka gruntowa', 'BUD', 72.35, 500.00, 'D')
INSERT INTO dbo.Sprzet (Id, NumerInwentarzowy, Nazwa, Kategoria, StawkaDobowa, Kaucja, Status)
VALUES (3, 'BUD-0003', N'Młot wyburzeniowy 15 kg', 'BUD', 89.90, 600.00, 'D')
INSERT INTO dbo.Sprzet (Id, NumerInwentarzowy, Nazwa, Kategoria, StawkaDobowa, Kaucja, Status)
VALUES (4, 'BUD-0004', N'Rusztowanie warszawskie (zestaw)', 'BUD', 38.50, 400.00, 'W')
INSERT INTO dbo.Sprzet (Id, NumerInwentarzowy, Nazwa, Kategoria, StawkaDobowa, Kaucja, Status)
VALUES (5, 'BUD-0005', N'Piła do glazury', 'BUD', 52.15, 300.00, 'S')
INSERT INTO dbo.Sprzet (Id, NumerInwentarzowy, Nazwa, Kategoria, StawkaDobowa, Kaucja, Status)
VALUES (6, 'OGR-0001', N'Glebogryzarka spalinowa', 'OGR', 28.85, 250.00, 'D')
INSERT INTO dbo.Sprzet (Id, NumerInwentarzowy, Nazwa, Kategoria, StawkaDobowa, Kaucja, Status)
VALUES (7, 'OGR-0002', N'Rębak do gałęzi', 'OGR', 119.00, 800.00, 'W')
INSERT INTO dbo.Sprzet (Id, NumerInwentarzowy, Nazwa, Kategoria, StawkaDobowa, Kaucja, Status)
VALUES (8, 'OGR-0003', N'Kosiarka spalinowa', 'OGR', 35.60, 200.00, 'D')
INSERT INTO dbo.Sprzet (Id, NumerInwentarzowy, Nazwa, Kategoria, StawkaDobowa, Kaucja, Status)
VALUES (9, 'OGR-0004', N'Nożyce do żywopłotu', 'OGR', 24.45, 150.00, 'X')
INSERT INTO dbo.Sprzet (Id, NumerInwentarzowy, Nazwa, Kategoria, StawkaDobowa, Kaucja, Status)
VALUES (10, 'IMP-0001', N'Namiot imprezowy 6x12 m', 'IMP', 210.00, 1000.00, 'D')
INSERT INTO dbo.Sprzet (Id, NumerInwentarzowy, Nazwa, Kategoria, StawkaDobowa, Kaucja, Status)
VALUES (11, 'IMP-0002', N'Zestaw nagłośnieniowy 2x500 W', 'IMP', 149.99, 900.00, 'D')
INSERT INTO dbo.Sprzet (Id, NumerInwentarzowy, Nazwa, Kategoria, StawkaDobowa, Kaucja, Status)
VALUES (12, 'IMP-0003', N'Stół cateringowy (komplet 10 szt.)', 'IMP', 60.25, 200.00, 'W')
INSERT INTO dbo.Sprzet (Id, NumerInwentarzowy, Nazwa, Kategoria, StawkaDobowa, Kaucja, Status)
VALUES (13, 'TRN-0001', N'Przyczepa lekka 750 kg', 'TRN', 95.00, 700.00, 'D')
INSERT INTO dbo.Sprzet (Id, NumerInwentarzowy, Nazwa, Kategoria, StawkaDobowa, Kaucja, Status)
VALUES (14, 'TRN-0002', N'Wózek paletowy ręczny', 'TRN', 33.33, 250.00, 'D')
INSERT INTO dbo.Sprzet (Id, NumerInwentarzowy, Nazwa, Kategoria, StawkaDobowa, Kaucja, Status)
VALUES (15, 'TRN-0003', N'Taczka budowlana', 'TRN', 12.50, 50.00, 'D')
GO

SET IDENTITY_INSERT dbo.Sprzet OFF
GO

-- ------------------------------------------------------------
-- Taryfy
-- ------------------------------------------------------------
SET IDENTITY_INSERT dbo.Taryfy ON
GO

INSERT INTO dbo.Taryfy (Id, Kategoria, MnoznikWeekend, RabatTygodniowyProc, ObowiazujeOd)
VALUES (1, 'BUD', 1.20, 10.00, '2025-01-01')
INSERT INTO dbo.Taryfy (Id, Kategoria, MnoznikWeekend, RabatTygodniowyProc, ObowiazujeOd)
VALUES (2, 'BUD', 1.25, 10.00, '2026-01-01')
INSERT INTO dbo.Taryfy (Id, Kategoria, MnoznikWeekend, RabatTygodniowyProc, ObowiazujeOd)
VALUES (3, 'OGR', 1.20, 10.00, '2026-01-01')
INSERT INTO dbo.Taryfy (Id, Kategoria, MnoznikWeekend, RabatTygodniowyProc, ObowiazujeOd)
VALUES (4, 'IMP', 1.50, 5.00, '2026-01-01')
INSERT INTO dbo.Taryfy (Id, Kategoria, MnoznikWeekend, RabatTygodniowyProc, ObowiazujeOd)
VALUES (5, 'TRN', 1.00, 15.00, '2026-01-01')
GO

SET IDENTITY_INSERT dbo.Taryfy OFF
GO

-- ------------------------------------------------------------
-- Rezerwacje
-- ------------------------------------------------------------
SET IDENTITY_INSERT dbo.Rezerwacje ON
GO

INSERT INTO dbo.Rezerwacje (Id, KlientId, SprzetId, DataOd, DataDo, Status, CenaSzacunkowa, Uwagi, Utworzono)
VALUES (1, 2, 6, '2026-10-12T08:00:00', '2026-10-17T08:00:00', 'Z', 141.36, NULL, '2026-10-08T14:00:00')
INSERT INTO dbo.Rezerwacje (Id, KlientId, SprzetId, DataOd, DataDo, Status, CenaSzacunkowa, Uwagi, Utworzono)
VALUES (2, 1, 2, '2026-10-13T09:00:00', '2026-10-15T09:30:00', 'Z', 144.70, N'odbiór osobisty', '2026-10-10T10:20:00')
INSERT INTO dbo.Rezerwacje (Id, KlientId, SprzetId, DataOd, DataDo, Status, CenaSzacunkowa, Uwagi, Utworzono)
VALUES (3, 4, 2, '2026-10-20T09:00:00', '2026-10-22T09:00:00', 'P', 133.12, NULL, '2026-10-14T16:45:00')
INSERT INTO dbo.Rezerwacje (Id, KlientId, SprzetId, DataOd, DataDo, Status, CenaSzacunkowa, Uwagi, Utworzono)
VALUES (4, 3, 10, '2026-10-24T10:00:00', '2026-10-26T10:00:00', 'N', 598.50, N'montaż na miejscu', '2026-10-15T09:10:00')
INSERT INTO dbo.Rezerwacje (Id, KlientId, SprzetId, DataOd, DataDo, Status, CenaSzacunkowa, Uwagi, Utworzono)
VALUES (5, 1, 13, '2026-10-09T10:00:00', '2026-10-16T10:00:00', 'A', 565.25, N'anulowana telefonicznie', '2026-10-01T11:30:00')
INSERT INTO dbo.Rezerwacje (Id, KlientId, SprzetId, DataOd, DataDo, Status, CenaSzacunkowa, Uwagi, Utworzono)
VALUES (6, 6, 11, '2026-10-27T08:00:00', '2026-10-28T08:00:00', 'N', 146.99, NULL, '2026-10-16T13:05:00')
GO

SET IDENTITY_INSERT dbo.Rezerwacje OFF
GO

-- ------------------------------------------------------------
-- Wypozyczenia
-- ------------------------------------------------------------
SET IDENTITY_INSERT dbo.Wypozyczenia ON
GO

INSERT INTO dbo.Wypozyczenia (Id, RezerwacjaId, KlientId, SprzetId, DataWydania, PlanowanyZwrot, DataZwrotu, KaucjaPobrana, Oplata, Kara, KaucjaZwrocona, Status, NumerPrzesylki)
VALUES (1, 1, 2, 6, '2026-10-12T08:00:00', '2026-10-17T08:00:00', '2026-10-17T08:40:00', 250.00, 141.36, 0.00, 250.00, 'Z', NULL)
INSERT INTO dbo.Wypozyczenia (Id, RezerwacjaId, KlientId, SprzetId, DataWydania, PlanowanyZwrot, DataZwrotu, KaucjaPobrana, Oplata, Kara, KaucjaZwrocona, Status, NumerPrzesylki)
VALUES (2, 2, 1, 2, '2026-10-13T09:00:00', '2026-10-15T09:30:00', '2026-10-16T11:30:00', 500.00, 144.70, 217.05, 282.95, 'Z', NULL)
INSERT INTO dbo.Wypozyczenia (Id, RezerwacjaId, KlientId, SprzetId, DataWydania, PlanowanyZwrot, DataZwrotu, KaucjaPobrana, Oplata, Kara, KaucjaZwrocona, Status, NumerPrzesylki)
VALUES (3, NULL, 7, 3, '2026-10-05T07:00:00', '2026-10-07T07:00:00', '2026-10-07T08:01:00', 600.00, 179.80, 134.85, 465.15, 'Z', NULL)
INSERT INTO dbo.Wypozyczenia (Id, RezerwacjaId, KlientId, SprzetId, DataWydania, PlanowanyZwrot, DataZwrotu, KaucjaPobrana, Oplata, Kara, KaucjaZwrocona, Status, NumerPrzesylki)
VALUES (4, NULL, 10, 4, '2026-10-01T08:00:00', '2026-10-15T08:00:00', NULL, 400.00, NULL, NULL, NULL, 'O', NULL)
INSERT INTO dbo.Wypozyczenia (Id, RezerwacjaId, KlientId, SprzetId, DataWydania, PlanowanyZwrot, DataZwrotu, KaucjaPobrana, Oplata, Kara, KaucjaZwrocona, Status, NumerPrzesylki)
VALUES (5, NULL, 9, 12, '2026-10-16T09:00:00', '2026-10-19T09:00:00', NULL, 200.00, NULL, NULL, NULL, 'O', NULL)
INSERT INTO dbo.Wypozyczenia (Id, RezerwacjaId, KlientId, SprzetId, DataWydania, PlanowanyZwrot, DataZwrotu, KaucjaPobrana, Oplata, Kara, KaucjaZwrocona, Status, NumerPrzesylki)
VALUES (6, NULL, 12, 7, '2026-10-17T10:00:00', '2026-10-18T10:00:00', NULL, 800.00, NULL, NULL, NULL, 'O', NULL)
GO

SET IDENTITY_INSERT dbo.Wypozyczenia OFF
GO

-- ------------------------------------------------------------
-- Faktury i pozycje
-- ------------------------------------------------------------
SET IDENTITY_INSERT dbo.Faktury ON
GO

INSERT INTO dbo.Faktury (Id, Numer, KlientId, WypozyczenieId, DataWystawienia, Netto, Vat, Brutto, Status, IdTransakcji)
VALUES (1, 'FV/2026/10/0001', 7, 3, '2026-10-07T09:00:00', 314.65, 72.37, 387.02, 'O', 'TX-DEMO-0001')
INSERT INTO dbo.Faktury (Id, Numer, KlientId, WypozyczenieId, DataWystawienia, Netto, Vat, Brutto, Status, IdTransakcji)
VALUES (2, 'FV/2026/10/0002', 1, 2, '2026-10-16T12:00:00', 361.75, 83.20, 444.95, 'W', NULL)
INSERT INTO dbo.Faktury (Id, Numer, KlientId, WypozyczenieId, DataWystawienia, Netto, Vat, Brutto, Status, IdTransakcji)
VALUES (3, 'FV/2026/10/0003', 2, 1, '2026-10-17T09:00:00', 141.36, 32.51, 173.87, 'O', 'TX-DEMO-0002')
GO

SET IDENTITY_INSERT dbo.Faktury OFF
GO

INSERT INTO dbo.PozycjeFaktur (FakturaId, Lp, Opis, Ilosc, CenaNetto, StawkaVat, WartoscNetto)
VALUES (1, 1, N'Wynajem: Młot wyburzeniowy 15 kg (2 doby)', 1.00, 179.80, 0.23, 179.80)
INSERT INTO dbo.PozycjeFaktur (FakturaId, Lp, Opis, Ilosc, CenaNetto, StawkaVat, WartoscNetto)
VALUES (1, 2, N'Kara za spóźniony zwrot', 1.00, 134.85, 0.23, 134.85)
INSERT INTO dbo.PozycjeFaktur (FakturaId, Lp, Opis, Ilosc, CenaNetto, StawkaVat, WartoscNetto)
VALUES (2, 1, N'Wynajem: Zagęszczarka gruntowa (2 doby)', 1.00, 144.70, 0.23, 144.70)
INSERT INTO dbo.PozycjeFaktur (FakturaId, Lp, Opis, Ilosc, CenaNetto, StawkaVat, WartoscNetto)
VALUES (2, 2, N'Kara za spóźniony zwrot', 1.00, 217.05, 0.23, 217.05)
INSERT INTO dbo.PozycjeFaktur (FakturaId, Lp, Opis, Ilosc, CenaNetto, StawkaVat, WartoscNetto)
VALUES (3, 1, N'Wynajem: Glebogryzarka spalinowa (5 dób)', 1.00, 141.36, 0.23, 141.36)
GO

-- ------------------------------------------------------------
-- DziennikOperacji
-- ------------------------------------------------------------
INSERT INTO dbo.DziennikOperacji (Kiedy, Operacja, Szczegoly)
VALUES ('2026-10-08T14:00:00', 'REZERWACJA', N'Id=1; Klient=2; Sprzet=6')
INSERT INTO dbo.DziennikOperacji (Kiedy, Operacja, Szczegoly)
VALUES ('2026-10-10T10:20:00', 'REZERWACJA', N'Id=2; Klient=1; Sprzet=2')
INSERT INTO dbo.DziennikOperacji (Kiedy, Operacja, Szczegoly)
VALUES ('2026-10-14T16:45:00', 'REZERWACJA', N'Id=3; Klient=4; Sprzet=2')
INSERT INTO dbo.DziennikOperacji (Kiedy, Operacja, Szczegoly)
VALUES ('2026-10-15T09:10:00', 'REZERWACJA', N'Id=4; Klient=3; Sprzet=10')
INSERT INTO dbo.DziennikOperacji (Kiedy, Operacja, Szczegoly)
VALUES ('2026-10-16T13:05:00', 'REZERWACJA', N'Id=6; Klient=6; Sprzet=11')
INSERT INTO dbo.DziennikOperacji (Kiedy, Operacja, Szczegoly)
VALUES ('2026-10-07T08:01:00', 'ZWROT', N'Wypozyczenie=3; Kara=134,85')
INSERT INTO dbo.DziennikOperacji (Kiedy, Operacja, Szczegoly)
VALUES ('2026-10-16T11:30:00', 'ZWROT', N'Wypozyczenie=2; Kara=217,05')
INSERT INTO dbo.DziennikOperacji (Kiedy, Operacja, Szczegoly)
VALUES ('2026-10-17T08:40:00', 'ZWROT', N'Wypozyczenie=1; Kara=0,00')
GO

PRINT 'Dane testowe Wypozyczalnia zaladowane.'
GO
