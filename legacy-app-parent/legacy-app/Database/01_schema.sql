-- Modul: 01_schema.sql - schemat bazy Wypozyczalnia | Autor: zespol IT | 2014-03-11
-- Historia: 2014-03-11 JK utworzenie
--           2016-05-20 MW tabele Faktury, PozycjeFaktur
--           2018-09-04 PT Klienci: NazwaFirmy, NIP, Typ
-- Uruchamiac na pustej bazie. Skrypt usuwa istniejace tabele!

USE Wypozyczalnia
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('dbo.PozycjeFaktur', 'U') IS NOT NULL DROP TABLE dbo.PozycjeFaktur
IF OBJECT_ID('dbo.Faktury', 'U') IS NOT NULL DROP TABLE dbo.Faktury
IF OBJECT_ID('dbo.Wypozyczenia', 'U') IS NOT NULL DROP TABLE dbo.Wypozyczenia
IF OBJECT_ID('dbo.Rezerwacje', 'U') IS NOT NULL DROP TABLE dbo.Rezerwacje
IF OBJECT_ID('dbo.Taryfy', 'U') IS NOT NULL DROP TABLE dbo.Taryfy
IF OBJECT_ID('dbo.Sprzet', 'U') IS NOT NULL DROP TABLE dbo.Sprzet
IF OBJECT_ID('dbo.Klienci', 'U') IS NOT NULL DROP TABLE dbo.Klienci
IF OBJECT_ID('dbo.DziennikOperacji', 'U') IS NOT NULL DROP TABLE dbo.DziennikOperacji
GO

-- ------------------------------------------------------------
-- Klienci
-- ------------------------------------------------------------
CREATE TABLE dbo.Klienci (
    Id               INT IDENTITY(1,1) PRIMARY KEY,
    Imie             NVARCHAR(50)  NOT NULL,
    Nazwisko         NVARCHAR(80)  NOT NULL,
    NazwaFirmy       NVARCHAR(120) NULL,
    Email            NVARCHAR(120) NOT NULL,
    Telefon          VARCHAR(20)   NOT NULL,
    NumerDokumentu   VARCHAR(20)   NULL,
    NIP              VARCHAR(13)   NULL,
    Typ              CHAR(1)       NOT NULL CONSTRAINT DF_Klienci_Typ DEFAULT 'I',
    ProgRabatu       TINYINT       NOT NULL CONSTRAINT DF_Klienci_ProgRabatu DEFAULT 0,
    Zablokowany      BIT           NOT NULL CONSTRAINT DF_Klienci_Zablokowany DEFAULT 0,
    PowodBlokady     NVARCHAR(200) NULL,
    DataRejestracji  DATETIME      NOT NULL CONSTRAINT DF_Klienci_DataRejestracji DEFAULT GETDATE()
)
GO

-- ------------------------------------------------------------
-- Sprzet (Status: D dostepny, W wypozyczony, S serwis, X wycofany)
-- ------------------------------------------------------------
CREATE TABLE dbo.Sprzet (
    Id                 INT IDENTITY(1,1) PRIMARY KEY,
    NumerInwentarzowy  VARCHAR(20)    NOT NULL CONSTRAINT UQ_Sprzet_NumerInwentarzowy UNIQUE,
    Nazwa              NVARCHAR(100)  NOT NULL,
    Kategoria          CHAR(3)        NOT NULL,
    StawkaDobowa       DECIMAL(10,2)  NOT NULL,
    Kaucja             DECIMAL(10,2)  NOT NULL,
    Status             CHAR(1)        NOT NULL CONSTRAINT DF_Sprzet_Status DEFAULT 'D'
)
GO

-- ------------------------------------------------------------
-- Taryfy (mnozniki weekendowe i rabaty tygodniowe per kategoria)
-- ------------------------------------------------------------
CREATE TABLE dbo.Taryfy (
    Id                   INT IDENTITY(1,1) PRIMARY KEY,
    Kategoria            CHAR(3)       NOT NULL,
    MnoznikWeekend       DECIMAL(4,2)  NOT NULL,
    RabatTygodniowyProc  DECIMAL(5,2)  NOT NULL,
    ObowiazujeOd         DATE          NOT NULL,
    CONSTRAINT UQ_Taryfy_Kategoria_Od UNIQUE (Kategoria, ObowiazujeOd)
)
GO

-- ------------------------------------------------------------
-- Rezerwacje (Status: N nowa, P potwierdzona, A anulowana, Z zrealizowana)
-- ------------------------------------------------------------
CREATE TABLE dbo.Rezerwacje (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    KlientId        INT            NOT NULL CONSTRAINT FK_Rezerwacje_Klienci REFERENCES dbo.Klienci(Id),
    SprzetId        INT            NOT NULL CONSTRAINT FK_Rezerwacje_Sprzet REFERENCES dbo.Sprzet(Id),
    DataOd          DATETIME       NOT NULL,
    DataDo          DATETIME       NOT NULL,
    Status          CHAR(1)        NOT NULL CONSTRAINT DF_Rezerwacje_Status DEFAULT 'N',
    CenaSzacunkowa  DECIMAL(10,2)  NOT NULL,
    Uwagi           NVARCHAR(500)  NULL,
    Utworzono       DATETIME       NOT NULL CONSTRAINT DF_Rezerwacje_Utworzono DEFAULT GETDATE()
)
GO

CREATE INDEX IX_Rezerwacje_Sprzet ON dbo.Rezerwacje (SprzetId, DataOd, DataDo)
GO

-- ------------------------------------------------------------
-- Wypozyczenia (Status: O otwarte, Z zamkniete)
-- ------------------------------------------------------------
CREATE TABLE dbo.Wypozyczenia (
    Id               INT IDENTITY(1,1) PRIMARY KEY,
    RezerwacjaId     INT            NULL CONSTRAINT FK_Wypozyczenia_Rezerwacje REFERENCES dbo.Rezerwacje(Id),
    KlientId         INT            NOT NULL CONSTRAINT FK_Wypozyczenia_Klienci REFERENCES dbo.Klienci(Id),
    SprzetId         INT            NOT NULL CONSTRAINT FK_Wypozyczenia_Sprzet REFERENCES dbo.Sprzet(Id),
    DataWydania      DATETIME       NOT NULL,
    PlanowanyZwrot   DATETIME       NOT NULL,
    DataZwrotu       DATETIME       NULL,
    KaucjaPobrana    DECIMAL(10,2)  NOT NULL,
    Oplata           DECIMAL(10,2)  NULL,
    Kara             DECIMAL(10,2)  NULL,
    KaucjaZwrocona   DECIMAL(10,2)  NULL,
    Status           CHAR(1)        NOT NULL CONSTRAINT DF_Wypozyczenia_Status DEFAULT 'O',
    NumerPrzesylki   VARCHAR(40)    NULL
)
GO

CREATE INDEX IX_Wypozyczenia_Status ON dbo.Wypozyczenia (Status, SprzetId)
GO

-- ------------------------------------------------------------
-- Faktury (Status: W wystawiona, O oplacona)
-- ------------------------------------------------------------
CREATE TABLE dbo.Faktury (
    Id               INT IDENTITY(1,1) PRIMARY KEY,
    Numer            VARCHAR(30)    NOT NULL CONSTRAINT UQ_Faktury_Numer UNIQUE,
    KlientId         INT            NOT NULL CONSTRAINT FK_Faktury_Klienci REFERENCES dbo.Klienci(Id),
    WypozyczenieId   INT            NOT NULL CONSTRAINT FK_Faktury_Wypozyczenia REFERENCES dbo.Wypozyczenia(Id),
    DataWystawienia  DATETIME       NOT NULL,
    Netto            DECIMAL(10,2)  NOT NULL,
    Vat              DECIMAL(10,2)  NOT NULL,
    Brutto           DECIMAL(10,2)  NOT NULL,
    Status           CHAR(1)        NOT NULL CONSTRAINT DF_Faktury_Status DEFAULT 'W',
    IdTransakcji     VARCHAR(40)    NULL
)
GO

CREATE TABLE dbo.PozycjeFaktur (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    FakturaId     INT            NOT NULL CONSTRAINT FK_PozycjeFaktur_Faktury REFERENCES dbo.Faktury(Id),
    Lp            INT            NOT NULL,
    Opis          NVARCHAR(200)  NOT NULL,
    Ilosc         DECIMAL(10,2)  NOT NULL,
    CenaNetto     DECIMAL(10,2)  NOT NULL,
    StawkaVat     DECIMAL(4,2)   NOT NULL,
    WartoscNetto  DECIMAL(10,2)  NOT NULL
)
GO

-- ------------------------------------------------------------
-- DziennikOperacji (prosty log operacji biznesowych)
-- ------------------------------------------------------------
CREATE TABLE dbo.DziennikOperacji (
    Id         INT IDENTITY(1,1) PRIMARY KEY,
    Kiedy      DATETIME       NOT NULL CONSTRAINT DF_DziennikOperacji_Kiedy DEFAULT GETDATE(),
    Operacja   VARCHAR(40)    NOT NULL,
    Szczegoly  NVARCHAR(400)  NULL
)
GO

PRINT 'Schemat Wypozyczalnia utworzony.'
GO
