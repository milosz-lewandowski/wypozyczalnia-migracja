-- =============================================================
-- Wypozyczalnia - procedury skladowane
-- Autor: zespol IT | 2014-03-11
-- Historia:
--   2015-02-04 MW: usp_PobierzTaryfe, usp_ZapiszTaryfe (taryfy z data obowiazywania)
--   2016-09-21 JK: usp_WydajSprzet zamyka rezerwacje
--   2018-01-15 PT: usp_WystawFakture - numeracja miesieczna FV/rrrr/mm/nnnn
-- Uruchamiac po 01_schema.sql, przed 03_seed.sql.
-- =============================================================

USE Wypozyczalnia
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- -------------------------------------------------------------
-- usp_SprawdzDostepnosc
-- Zwraca wszystko, co koliduje z terminem dla danego sprzetu.
-- Brak wierszy = sprzet dostepny.
-- -------------------------------------------------------------
IF OBJECT_ID('dbo.usp_SprawdzDostepnosc', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_SprawdzDostepnosc
GO
CREATE PROCEDURE dbo.usp_SprawdzDostepnosc @SprzetId INT, @DataOd DATETIME, @DataDo DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    IF @DataDo <= @DataOd
    BEGIN
        RAISERROR('Nieprawidlowy zakres dat', 16, 1);
        RETURN;
    END

    SELECT CAST('R' AS CHAR(1)) AS Zrodlo,
           r.Id AS Id,
           r.DataOd AS DataOd,
           r.DataDo AS DataDo
    FROM dbo.Rezerwacje r
    WHERE r.SprzetId = @SprzetId
      AND r.Status IN ('N', 'P')
      AND r.DataOd < @DataDo
      AND r.DataDo > @DataOd

    UNION ALL

    SELECT CAST('W' AS CHAR(1)) AS Zrodlo,
           w.Id AS Id,
           w.DataWydania AS DataOd,
           w.PlanowanyZwrot AS DataDo
    FROM dbo.Wypozyczenia w
    WHERE w.SprzetId = @SprzetId
      AND w.Status = 'O'
      AND w.DataWydania < @DataDo
      AND w.PlanowanyZwrot > @DataOd

    UNION ALL

    SELECT CAST('S' AS CHAR(1)) AS Zrodlo,
           s.Id AS Id,
           CAST(NULL AS DATETIME) AS DataOd,
           CAST(NULL AS DATETIME) AS DataDo
    FROM dbo.Sprzet s
    WHERE s.Id = @SprzetId
      AND s.Status IN ('S', 'X')

    ORDER BY Zrodlo, DataOd;
END
GO

-- -------------------------------------------------------------
-- usp_ZapiszRezerwacje
-- -------------------------------------------------------------
IF OBJECT_ID('dbo.usp_ZapiszRezerwacje', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_ZapiszRezerwacje
GO
CREATE PROCEDURE dbo.usp_ZapiszRezerwacje @KlientId INT, @SprzetId INT, @DataOd DATETIME, @DataDo DATETIME, @CenaSzacunkowa DECIMAL(10,2), @Uwagi NVARCHAR(500), @NoweId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM dbo.Klienci WHERE Id = @KlientId)
    BEGIN
        RAISERROR('Nieznany klient %d', 16, 1, @KlientId);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM dbo.Sprzet WHERE Id = @SprzetId)
    BEGIN
        RAISERROR('Nieznany sprzet %d', 16, 1, @SprzetId);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO dbo.Rezerwacje (KlientId, SprzetId, DataOd, DataDo, Status, CenaSzacunkowa, Uwagi, Utworzono)
        VALUES (@KlientId, @SprzetId, @DataOd, @DataDo, 'N', @CenaSzacunkowa, @Uwagi, GETDATE());

        SET @NoweId = CAST(SCOPE_IDENTITY() AS INT);

        INSERT INTO dbo.DziennikOperacji (Kiedy, Operacja, Szczegoly)
        VALUES (GETDATE(), 'REZERWACJA',
                N'Rezerwacja ' + CAST(@NoweId AS NVARCHAR(10)) + N', klient ' + CAST(@KlientId AS NVARCHAR(10))
                + N', sprzet ' + CAST(@SprzetId AS NVARCHAR(10)));

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @Blad NVARCHAR(2048) = ERROR_MESSAGE();
        RAISERROR(@Blad, 16, 1);
    END CATCH
END
GO

-- -------------------------------------------------------------
-- usp_ZmienStatusRezerwacji
-- -------------------------------------------------------------
IF OBJECT_ID('dbo.usp_ZmienStatusRezerwacji', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_ZmienStatusRezerwacji
GO
CREATE PROCEDURE dbo.usp_ZmienStatusRezerwacji @RezerwacjaId INT, @Status CHAR(1)
AS
BEGIN
    SET NOCOUNT ON;

    IF @Status NOT IN ('N', 'P', 'A', 'Z')
    BEGIN
        RAISERROR('Nieprawidlowy status rezerwacji: %s', 16, 1, @Status);
        RETURN;
    END

    DECLARE @StaryStatus CHAR(1);
    SELECT @StaryStatus = Status FROM dbo.Rezerwacje WHERE Id = @RezerwacjaId;

    IF @StaryStatus IS NULL
    BEGIN
        RAISERROR('Nieznana rezerwacja %d', 16, 1, @RezerwacjaId);
        RETURN;
    END

    UPDATE dbo.Rezerwacje
    SET Status = @Status
    WHERE Id = @RezerwacjaId;

    INSERT INTO dbo.DziennikOperacji (Kiedy, Operacja, Szczegoly)
    VALUES (GETDATE(), 'STATUS_REZERWACJI',
            N'Rezerwacja ' + CAST(@RezerwacjaId AS NVARCHAR(10)) + N': ' + @StaryStatus + N' -> ' + @Status);
END
GO

-- -------------------------------------------------------------
-- usp_WydajSprzet
-- -------------------------------------------------------------
IF OBJECT_ID('dbo.usp_WydajSprzet', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_WydajSprzet
GO
CREATE PROCEDURE dbo.usp_WydajSprzet @RezerwacjaId INT = NULL, @KlientId INT, @SprzetId INT, @DataWydania DATETIME, @PlanowanyZwrot DATETIME, @KaucjaPobrana DECIMAL(10,2), @NoweId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    IF @RezerwacjaId = 0 SET @RezerwacjaId = NULL;

    IF @PlanowanyZwrot <= @DataWydania
    BEGIN
        RAISERROR('Planowany zwrot musi byc pozniejszy niz wydanie', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO dbo.Wypozyczenia (RezerwacjaId, KlientId, SprzetId, DataWydania, PlanowanyZwrot, KaucjaPobrana, Status)
        VALUES (@RezerwacjaId, @KlientId, @SprzetId, @DataWydania, @PlanowanyZwrot, @KaucjaPobrana, 'O');

        SET @NoweId = CAST(SCOPE_IDENTITY() AS INT);

        UPDATE dbo.Sprzet
        SET Status = 'W'
        WHERE Id = @SprzetId;

        IF @RezerwacjaId IS NOT NULL
        BEGIN
            UPDATE dbo.Rezerwacje
            SET Status = 'Z'
            WHERE Id = @RezerwacjaId;
        END

        INSERT INTO dbo.DziennikOperacji (Kiedy, Operacja, Szczegoly)
        VALUES (GETDATE(), 'WYDANIE',
                N'Wypozyczenie ' + CAST(@NoweId AS NVARCHAR(10)) + N', sprzet ' + CAST(@SprzetId AS NVARCHAR(10))
                + ISNULL(N', rezerwacja ' + CAST(@RezerwacjaId AS NVARCHAR(10)), N''));

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @Blad NVARCHAR(2048) = ERROR_MESSAGE();
        RAISERROR(@Blad, 16, 1);
    END CATCH
END
GO

-- -------------------------------------------------------------
-- usp_ZarejestrujZwrot
-- -------------------------------------------------------------
IF OBJECT_ID('dbo.usp_ZarejestrujZwrot', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_ZarejestrujZwrot
GO
CREATE PROCEDURE dbo.usp_ZarejestrujZwrot @WypozyczenieId INT, @DataZwrotu DATETIME, @Oplata DECIMAL(10,2), @Kara DECIMAL(10,2), @KaucjaZwrocona DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SprzetId INT;
    DECLARE @Status CHAR(1);

    SELECT @SprzetId = SprzetId, @Status = Status
    FROM dbo.Wypozyczenia
    WHERE Id = @WypozyczenieId;

    IF @SprzetId IS NULL
    BEGIN
        RAISERROR('Nieznane wypozyczenie %d', 16, 1, @WypozyczenieId);
        RETURN;
    END

    IF @Status = 'Z'
    BEGIN
        RAISERROR('Wypozyczenie %d jest juz zamkniete', 16, 1, @WypozyczenieId);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE dbo.Wypozyczenia
        SET DataZwrotu = @DataZwrotu,
            Oplata = @Oplata,
            Kara = @Kara,
            KaucjaZwrocona = @KaucjaZwrocona,
            Status = 'Z'
        WHERE Id = @WypozyczenieId;

        UPDATE dbo.Sprzet
        SET Status = 'D'
        WHERE Id = @SprzetId;

        INSERT INTO dbo.DziennikOperacji (Kiedy, Operacja, Szczegoly)
        VALUES (GETDATE(), 'ZWROT',
                N'Wypozyczenie ' + CAST(@WypozyczenieId AS NVARCHAR(10)) + N', oplata ' + CAST(@Oplata AS NVARCHAR(20))
                + N', kara ' + CAST(@Kara AS NVARCHAR(20)));

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @Blad NVARCHAR(2048) = ERROR_MESSAGE();
        RAISERROR(@Blad, 16, 1);
    END CATCH
END
GO

-- -------------------------------------------------------------
-- usp_PobierzTaryfe
-- Taryfa obowiazujaca w danym dniu (najnowsza z ObowiazujeOd <= dzien).
-- -------------------------------------------------------------
IF OBJECT_ID('dbo.usp_PobierzTaryfe', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_PobierzTaryfe
GO
CREATE PROCEDURE dbo.usp_PobierzTaryfe @Kategoria CHAR(3), @NaDzien DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
           Id,
           Kategoria,
           MnoznikWeekend,
           RabatTygodniowyProc,
           ObowiazujeOd
    FROM dbo.Taryfy
    WHERE Kategoria = @Kategoria
      AND ObowiazujeOd <= @NaDzien
    ORDER BY ObowiazujeOd DESC;
END
GO

-- -------------------------------------------------------------
-- usp_ZapiszTaryfe
-- Upsert po (Kategoria, ObowiazujeOd).
-- -------------------------------------------------------------
IF OBJECT_ID('dbo.usp_ZapiszTaryfe', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_ZapiszTaryfe
GO
CREATE PROCEDURE dbo.usp_ZapiszTaryfe @Kategoria CHAR(3), @MnoznikWeekend DECIMAL(4,2), @RabatTygodniowyProc DECIMAL(5,2), @ObowiazujeOd DATE
AS
BEGIN
    SET NOCOUNT ON;

    IF @MnoznikWeekend <= 0 OR @RabatTygodniowyProc < 0
    BEGIN
        RAISERROR('Nieprawidlowe parametry taryfy', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM dbo.Taryfy WHERE Kategoria = @Kategoria AND ObowiazujeOd = @ObowiazujeOd)
    BEGIN
        UPDATE dbo.Taryfy
        SET MnoznikWeekend = @MnoznikWeekend,
            RabatTygodniowyProc = @RabatTygodniowyProc
        WHERE Kategoria = @Kategoria
          AND ObowiazujeOd = @ObowiazujeOd;
    END
    ELSE
    BEGIN
        INSERT INTO dbo.Taryfy (Kategoria, MnoznikWeekend, RabatTygodniowyProc, ObowiazujeOd)
        VALUES (@Kategoria, @MnoznikWeekend, @RabatTygodniowyProc, @ObowiazujeOd);
    END

    INSERT INTO dbo.DziennikOperacji (Kiedy, Operacja, Szczegoly)
    VALUES (GETDATE(), 'TARYFA',
            @Kategoria + N' od ' + CONVERT(NVARCHAR(10), @ObowiazujeOd, 120));
END
GO

-- -------------------------------------------------------------
-- usp_WystawFakture
-- Numer: FV/rrrr/mm/nnnn, numeracja od 1 w kazdym miesiacu.
-- -------------------------------------------------------------
IF OBJECT_ID('dbo.usp_WystawFakture', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_WystawFakture
GO
CREATE PROCEDURE dbo.usp_WystawFakture @KlientId INT, @WypozyczenieId INT, @Netto DECIMAL(10,2), @Vat DECIMAL(10,2), @Brutto DECIMAL(10,2), @NoweId INT OUTPUT, @Numer VARCHAR(30) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Teraz DATETIME = GETDATE();
    DECLARE @Rok INT = YEAR(@Teraz);
    DECLARE @Miesiac INT = MONTH(@Teraz);
    DECLARE @Kolejny INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        SELECT @Kolejny = COUNT(*) + 1
        FROM dbo.Faktury WITH (UPDLOCK, HOLDLOCK)
        WHERE YEAR(DataWystawienia) = @Rok
          AND MONTH(DataWystawienia) = @Miesiac;

        SET @Numer = 'FV/' + CAST(@Rok AS VARCHAR(4)) + '/'
                   + RIGHT('0' + CAST(@Miesiac AS VARCHAR(2)), 2) + '/'
                   + RIGHT('0000' + CAST(@Kolejny AS VARCHAR(4)), 4);

        INSERT INTO dbo.Faktury (Numer, KlientId, WypozyczenieId, DataWystawienia, Netto, Vat, Brutto, Status)
        VALUES (@Numer, @KlientId, @WypozyczenieId, @Teraz, @Netto, @Vat, @Brutto, 'W');

        SET @NoweId = CAST(SCOPE_IDENTITY() AS INT);

        INSERT INTO dbo.DziennikOperacji (Kiedy, Operacja, Szczegoly)
        VALUES (@Teraz, 'FAKTURA',
                N'Faktura ' + @Numer + N', wypozyczenie ' + CAST(@WypozyczenieId AS NVARCHAR(10)));

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @Blad NVARCHAR(2048) = ERROR_MESSAGE();
        RAISERROR(@Blad, 16, 1);
    END CATCH
END
GO
