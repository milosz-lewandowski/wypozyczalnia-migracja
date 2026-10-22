# Pomiar struktury legacy

Surowy pomiar wykonany bez oceny hipotez H1–H14. Sekcje M1–M10 pochodzą ze skryptu leksykalnego opartego wyłącznie na bibliotece standardowej Pythona. Sekcje M11–M24 odpowiadają kolejno H1–H14 i zawierają dokładne komendy zamrożone w `03-ARCHITEKTURA-HIPOTEZA.md`. Kod wyjścia wszystkich komend wyniósł `0`.

## M1 — Drzewo katalogów z liczbą plików i linii

`python tools\pomiar_struktury_legacy.py M1`

| Katalog | Pliki bezpośrednio | Linie bezpośrednio | Pliki łącznie | Linie łącznie |
|---|---|---|---|---|
| . | 2 | 78 | 52 | 7713 |
| Database | 3 | 749 | 3 | 749 |
| Jobs | 0 | 0 | 3 | 251 |
| Jobs\EksportNocny | 3 | 251 | 3 | 251 |
| WypozyczalniaCore | 7 | 1695 | 22 | 3587 |
| WypozyczalniaCore\DAL | 6 | 992 | 6 | 992 |
| WypozyczalniaCore\Integracje | 3 | 443 | 3 | 443 |
| WypozyczalniaCore\Model | 6 | 457 | 6 | 457 |
| WypozyczalniaWeb | 20 | 2832 | 22 | 3048 |
| WypozyczalniaWeb\Web References | 0 | 0 | 2 | 216 |
| WypozyczalniaWeb\Web References\BramkaPlatnosci | 2 | 216 | 2 | 216 |

## M2 — Klasy i moduły z rolą wyprowadzoną z nazwy

`python tools\pomiar_struktury_legacy.py M2`

| Plik | Rola z nazwy | Deklaracje typów |
|---|---|---|
| Database\01_schema.sql | skrypt SQL | — |
| Database\02_procedury.sql | skrypt SQL | — |
| Database\03_seed.sql | skrypt SQL | — |
| Jobs\EksportNocny\Program.vb | punkt wejścia zadania | Module Program@12 |
| WypozyczalniaCore\CustomerManager.vb | manager | Class CustomerManager@10 |
| WypozyczalniaCore\DAL\DbHelper.vb | helper bazy | Class BladBiznesowyException@9, Class DbHelper@27 |
| WypozyczalniaCore\DAL\EquipmentDAL.vb | DAL | Class EquipmentDAL@12 |
| WypozyczalniaCore\DAL\InvoiceDAL.vb | DAL | Class InvoiceDAL@12 |
| WypozyczalniaCore\DAL\PricingDAL.vb | DAL | Class PricingDAL@12 |
| WypozyczalniaCore\DAL\RentalDAL.vb | DAL | Class RentalDAL@12 |
| WypozyczalniaCore\DAL\ReservationDAL.vb | DAL | Class ReservationDAL@12 |
| WypozyczalniaCore\EquipmentManager.vb | manager | Class EquipmentManager@12 |
| WypozyczalniaCore\Integracje\EksportCsv.vb | eksport | Class EksportCsv@13 |
| WypozyczalniaCore\Integracje\ImportCennikaXml.vb | import | Class ImportCennikaXml@14 |
| WypozyczalniaCore\Integracje\KurierClient.vb | klient integracji | Class KurierClient@15 |
| WypozyczalniaCore\InvoiceManager.vb | manager | Class InvoiceManager@13 |
| WypozyczalniaCore\Model\Faktura.vb | model | Class Faktura@10, Class PozycjaFaktury@74 |
| WypozyczalniaCore\Model\Klient.vb | model | Class Klient@8 |
| WypozyczalniaCore\Model\Rezerwacja.vb | model | Class Rezerwacja@9 |
| WypozyczalniaCore\Model\Sprzet.vb | model | Class Sprzet@9 |
| WypozyczalniaCore\Model\Taryfa.vb | model | Class Taryfa@9 |
| WypozyczalniaCore\Model\Wypozyczenie.vb | model | Class Wypozyczenie@10 |
| WypozyczalniaCore\PricingManager.vb | manager | Class PricingManager@14 |
| WypozyczalniaCore\RentalManager.vb | manager | Class RentalManager@11 |
| WypozyczalniaCore\ReservationManager.vb | manager | Class ReservationManager@12 |
| WypozyczalniaWeb\Cennik.aspx | strona WebForms | — |
| WypozyczalniaWeb\Cennik.aspx.vb | code-behind strony | Class Cennik@14 |
| WypozyczalniaWeb\Faktura.aspx | strona WebForms | — |
| WypozyczalniaWeb\Faktura.aspx.vb | code-behind strony | Class Faktura@13 |
| WypozyczalniaWeb\Global.asax.vb | cykl życia aplikacji | Class Global_asax@9 |
| WypozyczalniaWeb\Klienci.aspx | strona WebForms | — |
| WypozyczalniaWeb\Klienci.aspx.vb | code-behind strony | Class Klienci@11 |
| WypozyczalniaWeb\Rezerwacja.aspx | strona WebForms | — |
| WypozyczalniaWeb\Rezerwacja.aspx.designer.vb | plik projektanta | — |
| WypozyczalniaWeb\Rezerwacja.aspx.vb | code-behind strony | Class Rezerwacja@13 |
| WypozyczalniaWeb\Sprzet.aspx | strona WebForms | — |
| WypozyczalniaWeb\Sprzet.aspx.vb | code-behind strony | Class Sprzet@11 |
| WypozyczalniaWeb\Web References\BramkaPlatnosci\Reference.vb | wygenerowane proxy | — |
| WypozyczalniaWeb\Wypozyczenie.aspx | strona WebForms | — |
| WypozyczalniaWeb\Wypozyczenie.aspx.vb | code-behind strony | Class Wypozyczenie@11 |
| WypozyczalniaWeb\Zwrot.aspx | strona WebForms | — |
| WypozyczalniaWeb\Zwrot.aspx.vb | code-behind strony | Class Zwrot@13 |

## M3 — Macierz zależności per plik

`python tools\pomiar_struktury_legacy.py M3`

| Plik | New ...Manager/DAL/Client() | DbHelper. | Session() |
|---|---|---|---|
| Jobs\EksportNocny\Program.vb | — | 0 | 0 |
| WypozyczalniaCore\CustomerManager.vb | — | 35 | 0 |
| WypozyczalniaCore\DAL\DbHelper.vb | — | 0 | 0 |
| WypozyczalniaCore\DAL\EquipmentDAL.vb | — | 43 | 0 |
| WypozyczalniaCore\DAL\InvoiceDAL.vb | — | 41 | 0 |
| WypozyczalniaCore\DAL\PricingDAL.vb | — | 36 | 0 |
| WypozyczalniaCore\DAL\RentalDAL.vb | — | 48 | 0 |
| WypozyczalniaCore\DAL\ReservationDAL.vb | — | 51 | 0 |
| WypozyczalniaCore\EquipmentManager.vb | EquipmentDAL=10, ReservationDAL=2 | 0 | 0 |
| WypozyczalniaCore\Integracje\EksportCsv.vb | InvoiceManager=1, RentalManager=1 | 0 | 0 |
| WypozyczalniaCore\Integracje\ImportCennikaXml.vb | PricingDAL=1 | 2 | 0 |
| WypozyczalniaCore\Integracje\KurierClient.vb | — | 0 | 0 |
| WypozyczalniaCore\InvoiceManager.vb | CustomerManager=2, EquipmentManager=2, InvoiceDAL=11, RentalManager=3 | 0 | 0 |
| WypozyczalniaCore\Model\Faktura.vb | — | 0 | 0 |
| WypozyczalniaCore\Model\Klient.vb | — | 0 | 0 |
| WypozyczalniaCore\Model\Rezerwacja.vb | — | 0 | 0 |
| WypozyczalniaCore\Model\Sprzet.vb | — | 0 | 0 |
| WypozyczalniaCore\Model\Taryfa.vb | — | 0 | 0 |
| WypozyczalniaCore\Model\Wypozyczenie.vb | — | 0 | 0 |
| WypozyczalniaCore\PricingManager.vb | CustomerManager=2, EquipmentManager=7, PricingDAL=10 | 6 | 0 |
| WypozyczalniaCore\RentalManager.vb | CustomerManager=4, EquipmentManager=7, KurierClient=3, PricingManager=2, RentalDAL=14, ReservationManager=2 | 0 | 0 |
| WypozyczalniaCore\ReservationManager.vb | CustomerManager=1, EquipmentManager=2, PricingManager=3, ReservationDAL=17 | 0 | 0 |
| WypozyczalniaWeb\Cennik.aspx.vb | CustomerManager=1, EquipmentManager=2, PricingManager=1 | 2 | 2 |
| WypozyczalniaWeb\Faktura.aspx.vb | CustomerManager=2, EquipmentManager=1, InvoiceManager=4, RentalManager=1 | 0 | 8 |
| WypozyczalniaWeb\Global.asax.vb | — | 0 | 2 |
| WypozyczalniaWeb\Klienci.aspx.vb | CustomerManager=5 | 1 | 5 |
| WypozyczalniaWeb\Rezerwacja.aspx.designer.vb | — | 0 | 0 |
| WypozyczalniaWeb\Rezerwacja.aspx.vb | CustomerManager=2, EquipmentManager=3, ReservationManager=4 | 0 | 9 |
| WypozyczalniaWeb\Sprzet.aspx.vb | EquipmentManager=5 | 0 | 0 |
| WypozyczalniaWeb\Web References\BramkaPlatnosci\Reference.vb | — | 0 | 0 |
| WypozyczalniaWeb\Wypozyczenie.aspx.vb | CustomerManager=1, EquipmentManager=2, RentalManager=3, ReservationManager=3 | 0 | 2 |
| WypozyczalniaWeb\Zwrot.aspx.vb | EquipmentManager=2, RentalManager=4 | 0 | 1 |

## M4 — Literały SQL poza katalogiem DAL

`python tools\pomiar_struktury_legacy.py M4`

| Plik | Linia | Linia z literałem SQL |
|---|---|---|
| WypozyczalniaCore\CustomerManager.vb | 18 | "SELECT " & KolumnyKlienta & " FROM Klienci WHERE Id = @Id", _ |
| WypozyczalniaCore\CustomerManager.vb | 41 | "SELECT ProgRabatu FROM Klienci WHERE Id = @Id", _ |
| WypozyczalniaCore\CustomerManager.vb | 55 | Dim sql As String = "SELECT TOP 200 Id, Imie, Nazwisko, NazwaFirmy, Email, Telefon FROM Klienci " & _ |
| WypozyczalniaCore\CustomerManager.vb | 56 | "WHERE (@Fraza = '' OR Nazwisko LIKE @Wzorzec OR NazwaFirmy LIKE @Wzorzec OR Email LIKE @Wzorzec) " & _ |
| WypozyczalniaCore\CustomerManager.vb | 57 | "ORDER BY Nazwisko, Imie" |
| WypozyczalniaCore\CustomerManager.vb | 77 | "INSERT INTO Klienci (Imie, Nazwisko, NazwaFirmy, Email, Telefon, NumerDokumentu, NIP, Typ, ProgRabatu) " & _ |
| WypozyczalniaCore\CustomerManager.vb | 79 | "SELECT CAST(SCOPE_IDENTITY() AS INT) AS NoweId", _ |
| WypozyczalniaCore\CustomerManager.vb | 88 | "UPDATE Klienci SET Imie = @Imie, Nazwisko = @Nazwisko, NazwaFirmy = @NazwaFirmy, Email = @Email, " & _ |
| WypozyczalniaCore\CustomerManager.vb | 90 | "WHERE Id = @Id", _ |
| WypozyczalniaCore\CustomerManager.vb | 105 | "UPDATE Klienci SET Zablokowany = 1, PowodBlokady = @Powod WHERE Id = @Id", _ |
| WypozyczalniaCore\CustomerManager.vb | 114 | "UPDATE Klienci SET Zablokowany = 0, PowodBlokady = NULL WHERE Id = @Id", _ |
| WypozyczalniaCore\CustomerManager.vb | 121 | "SELECT Email FROM Klienci WHERE Id = @Id", DbHelper.Parametr("@Id", klientId)) |
| WypozyczalniaCore\CustomerManager.vb | 128 | "SELECT COUNT(*) AS Liczba FROM Wypozyczenia WHERE KlientId = @Id AND Status = 'O'", _ |
| WypozyczalniaCore\CustomerManager.vb | 136 | "SELECT Id, Imie, Nazwisko, NazwaFirmy, Email, Telefon, PowodBlokady FROM Klienci " & _ |
| WypozyczalniaCore\CustomerManager.vb | 137 | "WHERE Zablokowany = 1 ORDER BY Nazwisko, Imie") |
| WypozyczalniaCore\CustomerManager.vb | 143 | "SELECT COUNT(*) AS Liczba FROM Klienci WHERE Email = @Email AND Id <> @Id", _ |
| WypozyczalniaCore\CustomerManager.vb | 188 | "INSERT INTO DziennikOperacji (Operacja, Szczegoly) VALUES (@Operacja, @Szczegoly)", _ |
| WypozyczalniaCore\Integracje\ImportCennikaXml.vb | 141 | Dim dt As DataTable = DbHelper.WykonajZapytanie("SELECT Kaucja FROM Sprzet WHERE NumerInwentarzowy = @Numer", |
| WypozyczalniaCore\PricingManager.vb | 152 | Dim sql As String = "SELECT Id, KlientId, SprzetId, DataOd, DataDo, CenaSzacunkowa FROM Rezerwacje WHERE Status = 'N'" |
| WypozyczalniaCore\PricingManager.vb | 159 | DbHelper.WykonajPolecenie("UPDATE Rezerwacje SET CenaSzacunkowa = @Cena WHERE Id = @Id", |
| WypozyczalniaCore\PricingManager.vb | 292 | Dim dt As DataTable = DbHelper.WykonajZapytanie("SELECT KlientId, SprzetId, DataOd, DataDo, CenaSzacunkowa FROM Rezerwacje WHERE Id = @Id", |
| WypozyczalniaWeb\Cennik.aspx.vb | 58 | Dim sql As String = "UPDATE Sprzet SET StawkaDobowa = " & txtStawka.Text.Replace(",", ".") & " WHERE Id = " & ddlSprzet.SelectedValue |
| WypozyczalniaWeb\Cennik.aspx.vb | 164 | Dim sql As String = "SELECT Id, Kategoria, MnoznikWeekend, RabatTygodniowyProc, ObowiazujeOd FROM Taryfy ORDER BY Kategoria, ObowiazujeOd DESC" |
| WypozyczalniaWeb\Klienci.aspx.vb | 49 | Dim sql As String = "SELECT Id, Imie, Nazwisko, NazwaFirmy, Email, Telefon FROM Klienci WHERE Nazwisko LIKE '%" & txtSzukaj.Text & "%' ORDER BY Nazwisko" |

## M5 — Procedury usp_ zdefiniowane i używane

`python tools\pomiar_struktury_legacy.py M5`

| Procedura | Definicje | Użycia w VB |
|---|---|---|
| usp_pobierztaryfe | Database\02_procedury.sql:276 | WypozyczalniaCore\DAL\PricingDAL.vb:3, WypozyczalniaCore\DAL\PricingDAL.vb:15 |
| usp_sprawdzdostepnosc | Database\02_procedury.sql:27 | WypozyczalniaCore\DAL\ReservationDAL.vb:4, WypozyczalniaCore\DAL\ReservationDAL.vb:18 |
| usp_wydajsprzet | Database\02_procedury.sql:162 | WypozyczalniaCore\DAL\RentalDAL.vb:25 |
| usp_wystawfakture | Database\02_procedury.sql:339 | WypozyczalniaCore\DAL\InvoiceDAL.vb:3, WypozyczalniaCore\DAL\InvoiceDAL.vb:20, WypozyczalniaCore\DAL\InvoiceDAL.vb:29 |
| usp_zapiszrezerwacje | Database\02_procedury.sql:80 | WypozyczalniaCore\DAL\ReservationDAL.vb:3, WypozyczalniaCore\DAL\ReservationDAL.vb:30, WypozyczalniaCore\DAL\ReservationDAL.vb:39 |
| usp_zapisztaryfe | Database\02_procedury.sql:301 | WypozyczalniaCore\DAL\PricingDAL.vb:26 |
| usp_zarejestrujzwrot | Database\02_procedury.sql:215 | WypozyczalniaCore\DAL\RentalDAL.vb:41 |
| usp_zmienstatusrezerwacji | Database\02_procedury.sql:126 | WypozyczalniaCore\DAL\ReservationDAL.vb:54 |

## M6 — Metody w katalogu Model

`python tools\pomiar_struktury_legacy.py M6`

| Plik | Linia | Widoczność | Rodzaj | Nazwa |
|---|---|---|---|---|
| WypozyczalniaCore\Model\Faktura.vb | 24 | Public | Sub | New |
| WypozyczalniaCore\Model\Faktura.vb | 45 | Public | Function | SumaPozycjiNetto |
| WypozyczalniaCore\Model\Faktura.vb | 53 | Public | Function | ZWiersza |
| WypozyczalniaCore\Model\Faktura.vb | 68 | Public | Function | ToString |
| WypozyczalniaCore\Model\Faktura.vb | 83 | Public | Function | ZWiersza |
| WypozyczalniaCore\Model\Klient.vb | 29 | Public | Sub | New |
| WypozyczalniaCore\Model\Klient.vb | 51 | Public | Function | ToString |
| WypozyczalniaCore\Model\Klient.vb | 55 | Public | Function | Equals |
| WypozyczalniaCore\Model\Klient.vb | 61 | Public | Function | GetHashCode |
| WypozyczalniaCore\Model\Rezerwacja.vb | 25 | Public | Sub | New |
| WypozyczalniaCore\Model\Rezerwacja.vb | 61 | Public | Function | ToString |
| WypozyczalniaCore\Model\Sprzet.vb | 24 | Public | Sub | New |
| WypozyczalniaCore\Model\Sprzet.vb | 59 | Public | Function | CzyMoznaWydac |
| WypozyczalniaCore\Model\Sprzet.vb | 63 | Public | Function | ToString |
| WypozyczalniaCore\Model\Taryfa.vb | 19 | Public | Sub | New |
| WypozyczalniaCore\Model\Taryfa.vb | 26 | Public | Sub | New |
| WypozyczalniaCore\Model\Taryfa.vb | 33 | Public | Function | CzyObowiazujeNa |
| WypozyczalniaCore\Model\Taryfa.vb | 37 | Public | Function | CzyZnanaKategoria |
| WypozyczalniaCore\Model\Taryfa.vb | 42 | Public | Function | NazwaKategorii |
| WypozyczalniaCore\Model\Taryfa.vb | 52 | Public | Function | Kopia |
| WypozyczalniaCore\Model\Taryfa.vb | 58 | Public | Function | Domyslna |
| WypozyczalniaCore\Model\Taryfa.vb | 63 | Public | Function | TakaSama |
| WypozyczalniaCore\Model\Taryfa.vb | 69 | Public | Function | ToString |
| WypozyczalniaCore\Model\Wypozyczenie.vb | 26 | Public | Sub | New |
| WypozyczalniaCore\Model\Wypozyczenie.vb | 50 | Public | Function | CzyPoTerminie |
| WypozyczalniaCore\Model\Wypozyczenie.vb | 55 | Public | Function | ZWiersza |
| WypozyczalniaCore\Model\Wypozyczenie.vb | 77 | Public | Function | ToString |

## M7 — Pliki wołające integracje

`python tools\pomiar_struktury_legacy.py M7`

| Integracja | Wołający plik | Linia | Treść |
|---|---|---|---|
| ImportCennikaXml | Jobs\EksportNocny\Program.vb | 62 | Dim import As New ImportCennikaXml() |
| EksportCsv | Jobs\EksportNocny\Program.vb | 76 | Dim eksport As New EksportCsv() |
| KurierClient | WypozyczalniaCore\RentalManager.vb | 77 | Dim numer As String = New KurierClient().ZamowDostawe(noweId, adresDostawy.Trim(), telefon) |
| KurierClient | WypozyczalniaCore\RentalManager.vb | 206 | Return New KurierClient().StatusPrzesylki(w.NumerPrzesylki) |
| KurierClient | WypozyczalniaCore\RentalManager.vb | 227 | Dim numer As String = New KurierClient().ZamowDostawe(wypozyczenieId, adresDostawy, telefon) |
| ImportCennikaXml | WypozyczalniaWeb\Cennik.aspx.vb | 77 | Dim import As New ImportCennikaXml() |
| EksportCsv | WypozyczalniaWeb\Faktura.aspx.vb | 258 | Dim plik As String = New EksportCsv().EksportujDzien(Date.Today) |

## M8 — Math.Round i stałe procentowe

`python tools\pomiar_struktury_legacy.py M8`

| Plik | Linia | Dopasowana linia |
|---|---|---|
| WypozyczalniaCore\InvoiceManager.vb | 17 | Private Const DomyslnaStawkaVat As String = "0,23" |
| WypozyczalniaCore\InvoiceManager.vb | 200 | p.WartoscNetto = Math.Round(ilosc * cenaNetto, 2) |
| WypozyczalniaCore\InvoiceManager.vb | 210 | vat = vat + Math.Round(p.WartoscNetto * p.StawkaVat, 2) |
| WypozyczalniaCore\PricingManager.vb | 36 | Dim stawkaWeekend As Decimal = Math.Round(sprzet.StawkaDobowa * taryfa.MnoznikWeekend, 2) |
| WypozyczalniaCore\PricingManager.vb | 47 | kwota = kwota - Math.Round(kwota * taryfa.RabatTygodniowyProc / 100D, 2) |
| WypozyczalniaCore\PricingManager.vb | 49 | ' rabat 2,5% za kazdy prog stalego klienta (prog 1 = 2,5%) |
| WypozyczalniaCore\PricingManager.vb | 53 | kwota = Math.Round(kwota * (100 - rabatProc) / 100D, 2) |
| WypozyczalniaCore\RentalManager.vb | 141 | ' kara = 100% stawki dobowej za kazda dobe spoznienia |
| WypozyczalniaCore\RentalManager.vb | 142 | Return Math.Round(dobySpoznienia * stawkaDobowa * 1.5D, 2) |
| WypozyczalniaWeb\Rezerwacja.aspx.vb | 318 | Return Math.Round(kwota, 2, MidpointRounding.AwayFromZero) |

## M9 — Klucze sesji

`python tools\pomiar_struktury_legacy.py M9`

| Klucz | Liczba dostępów | Lokalizacje |
|---|---|---|
| OstatnieWypozyczenieId | 5 | WypozyczalniaWeb\Faktura.aspx.vb:72, WypozyczalniaWeb\Faktura.aspx.vb:73, WypozyczalniaWeb\Faktura.aspx.vb:235, WypozyczalniaWeb\Global.asax.vb:23, WypozyczalniaWeb\Zwrot.aspx.vb:256 |
| SzacunekRezerwacji | 5 | WypozyczalniaWeb\Rezerwacja.aspx.vb:27, WypozyczalniaWeb\Rezerwacja.aspx.vb:28, WypozyczalniaWeb\Rezerwacja.aspx.vb:37, WypozyczalniaWeb\Rezerwacja.aspx.vb:61, WypozyczalniaWeb\Rezerwacja.aspx.vb:73 |
| WybranyKlientId | 19 | WypozyczalniaWeb\Cennik.aspx.vb:104, WypozyczalniaWeb\Cennik.aspx.vb:208, WypozyczalniaWeb\Faktura.aspx.vb:110, WypozyczalniaWeb\Faktura.aspx.vb:110, WypozyczalniaWeb\Faktura.aspx.vb:135, WypozyczalniaWeb\Faktura.aspx.vb:135, WypozyczalniaWeb\Faktura.aspx.vb:139, WypozyczalniaWeb\Global.asax.vb:22, WypozyczalniaWeb\Klienci.aspx.vb:40, WypozyczalniaWeb\Klienci.aspx.vb:78, WypozyczalniaWeb\Klienci.aspx.vb:149, WypozyczalniaWeb\Klienci.aspx.vb:150, WypozyczalniaWeb\Klienci.aspx.vb:217, WypozyczalniaWeb\Rezerwacja.aspx.vb:66, WypozyczalniaWeb\Rezerwacja.aspx.vb:78, WypozyczalniaWeb\Rezerwacja.aspx.vb:224, WypozyczalniaWeb\Rezerwacja.aspx.vb:262, WypozyczalniaWeb\Wypozyczenie.aspx.vb:31, WypozyczalniaWeb\Wypozyczenie.aspx.vb:170 |

## M10 — Leksykalne użycia metod publicznych

`python tools\pomiar_struktury_legacy.py M10`

| Deklaracja | Linia | Rodzaj | Nazwa | Wystąpienia leksykalne | Wystąpienia per plik |
|---|---|---|---|---|---|
| WypozyczalniaCore\CustomerManager.vb | 16 | Function | PobierzKlienta | 14 | WypozyczalniaCore\CustomerManager.vb=2; WypozyczalniaCore\InvoiceManager.vb=2; WypozyczalniaCore\RentalManager.vb=3; WypozyczalniaWeb\Cennik.aspx.vb=1; WypozyczalniaWeb\Faktura.aspx.vb=2; WypozyczalniaWeb\Klienci.aspx.vb=2; WypozyczalniaWeb\Rezerwacja.aspx.vb=1; WypozyczalniaWeb\Wypozyczenie.aspx.vb=1 |
| WypozyczalniaCore\CustomerManager.vb | 26 | Function | CzyMozeWypozyczac | 5 | WypozyczalniaCore\CustomerManager.vb=1; WypozyczalniaCore\RentalManager.vb=2; WypozyczalniaCore\ReservationManager.vb=1; WypozyczalniaWeb\Klienci.aspx.vb=1 |
| WypozyczalniaCore\CustomerManager.vb | 39 | Function | ProgRabatu | 21 | WypozyczalniaCore\CustomerManager.vb=12; WypozyczalniaCore\Model\Klient.vb=2; WypozyczalniaCore\PricingManager.vb=2; WypozyczalniaWeb\Cennik.aspx.vb=2; WypozyczalniaWeb\Klienci.aspx.vb=2; WypozyczalniaWeb\Rezerwacja.aspx.vb=1 |
| WypozyczalniaCore\CustomerManager.vb | 52 | Function | Szukaj | 5 | WypozyczalniaCore\CustomerManager.vb=1; WypozyczalniaCore\DAL\EquipmentDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=2; WypozyczalniaWeb\Klienci.aspx.vb=1 |
| WypozyczalniaCore\CustomerManager.vb | 63 | Function | Zapisz | 7 | WypozyczalniaCore\CustomerManager.vb=1; WypozyczalniaCore\DAL\ReservationDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=1; WypozyczalniaCore\PricingManager.vb=1; WypozyczalniaCore\ReservationManager.vb=1; WypozyczalniaWeb\Klienci.aspx.vb=1; WypozyczalniaWeb\Sprzet.aspx.vb=1 |
| WypozyczalniaCore\CustomerManager.vb | 100 | Sub | Zablokuj | 2 | WypozyczalniaCore\CustomerManager.vb=1; WypozyczalniaWeb\Klienci.aspx.vb=1 |
| WypozyczalniaCore\CustomerManager.vb | 112 | Sub | Odblokuj | 1 | WypozyczalniaCore\CustomerManager.vb=1 |
| WypozyczalniaCore\CustomerManager.vb | 119 | Function | PobierzEmail | 1 | WypozyczalniaCore\CustomerManager.vb=1 |
| WypozyczalniaCore\CustomerManager.vb | 126 | Function | LiczbaOtwartychWypozyczen | 1 | WypozyczalniaCore\CustomerManager.vb=1 |
| WypozyczalniaCore\CustomerManager.vb | 134 | Function | KlienciZablokowani | 1 | WypozyczalniaCore\CustomerManager.vb=1 |
| WypozyczalniaCore\CustomerManager.vb | 140 | Function | CzyEmailZajety | 1 | WypozyczalniaCore\CustomerManager.vb=1 |
| WypozyczalniaCore\DAL\DbHelper.vb | 14 | Sub | New | 368 | Jobs\EksportNocny\Program.vb=4; WypozyczalniaCore\CustomerManager.vb=7; WypozyczalniaCore\DAL\DbHelper.vb=20; WypozyczalniaCore\DAL\EquipmentDAL.vb=4; WypozyczalniaCore\DAL\InvoiceDAL.vb=6; WypozyczalniaCore\DAL\PricingDAL.vb=6; WypozyczalniaCore\DAL\RentalDAL.vb=3; WypozyczalniaCore\DAL\ReservationDAL.vb=2; WypozyczalniaCore\EquipmentManager.vb=36; WypozyczalniaCore\Integracje\EksportCsv.vb=7; WypozyczalniaCore\Integracje\ImportCennikaXml.vb=10; WypozyczalniaCore\Integracje\KurierClient.vb=5; WypozyczalniaCore\InvoiceManager.vb=31; WypozyczalniaCore\Model\Faktura.vb=4; WypozyczalniaCore\Model\Klient.vb=1; WypozyczalniaCore\Model\Rezerwacja.vb=1; WypozyczalniaCore\Model\Sprzet.vb=1; WypozyczalniaCore\Model\Taryfa.vb=5; WypozyczalniaCore\Model\Wypozyczenie.vb=2; WypozyczalniaCore\PricingManager.vb=37; WypozyczalniaCore\RentalManager.vb=56; WypozyczalniaCore\ReservationManager.vb=43; WypozyczalniaWeb\Cennik.aspx.vb=10; WypozyczalniaWeb\Faktura.aspx.vb=10; WypozyczalniaWeb\Global.asax.vb=2; WypozyczalniaWeb\Klienci.aspx.vb=6; WypozyczalniaWeb\Rezerwacja.aspx.vb=12; WypozyczalniaWeb\Sprzet.aspx.vb=6; WypozyczalniaWeb\Web References\BramkaPlatnosci\Reference.vb=5; WypozyczalniaWeb\Wypozyczenie.aspx.vb=13; WypozyczalniaWeb\Zwrot.aspx.vb=13 |
| WypozyczalniaCore\DAL\DbHelper.vb | 19 | Sub | New | 368 | Jobs\EksportNocny\Program.vb=4; WypozyczalniaCore\CustomerManager.vb=7; WypozyczalniaCore\DAL\DbHelper.vb=20; WypozyczalniaCore\DAL\EquipmentDAL.vb=4; WypozyczalniaCore\DAL\InvoiceDAL.vb=6; WypozyczalniaCore\DAL\PricingDAL.vb=6; WypozyczalniaCore\DAL\RentalDAL.vb=3; WypozyczalniaCore\DAL\ReservationDAL.vb=2; WypozyczalniaCore\EquipmentManager.vb=36; WypozyczalniaCore\Integracje\EksportCsv.vb=7; WypozyczalniaCore\Integracje\ImportCennikaXml.vb=10; WypozyczalniaCore\Integracje\KurierClient.vb=5; WypozyczalniaCore\InvoiceManager.vb=31; WypozyczalniaCore\Model\Faktura.vb=4; WypozyczalniaCore\Model\Klient.vb=1; WypozyczalniaCore\Model\Rezerwacja.vb=1; WypozyczalniaCore\Model\Sprzet.vb=1; WypozyczalniaCore\Model\Taryfa.vb=5; WypozyczalniaCore\Model\Wypozyczenie.vb=2; WypozyczalniaCore\PricingManager.vb=37; WypozyczalniaCore\RentalManager.vb=56; WypozyczalniaCore\ReservationManager.vb=43; WypozyczalniaWeb\Cennik.aspx.vb=10; WypozyczalniaWeb\Faktura.aspx.vb=10; WypozyczalniaWeb\Global.asax.vb=2; WypozyczalniaWeb\Klienci.aspx.vb=6; WypozyczalniaWeb\Rezerwacja.aspx.vb=12; WypozyczalniaWeb\Sprzet.aspx.vb=6; WypozyczalniaWeb\Web References\BramkaPlatnosci\Reference.vb=5; WypozyczalniaWeb\Wypozyczenie.aspx.vb=13; WypozyczalniaWeb\Zwrot.aspx.vb=13 |
| WypozyczalniaCore\DAL\DbHelper.vb | 34 | Function | PobierzConnectionString | 6 | WypozyczalniaCore\DAL\DbHelper.vb=5; WypozyczalniaCore\DAL\InvoiceDAL.vb=1 |
| WypozyczalniaCore\DAL\DbHelper.vb | 42 | Function | WykonajZapytanie | 65 | WypozyczalniaCore\CustomerManager.vb=8; WypozyczalniaCore\DAL\DbHelper.vb=1; WypozyczalniaCore\DAL\EquipmentDAL.vb=11; WypozyczalniaCore\DAL\InvoiceDAL.vb=9; WypozyczalniaCore\DAL\PricingDAL.vb=9; WypozyczalniaCore\DAL\RentalDAL.vb=12; WypozyczalniaCore\DAL\ReservationDAL.vb=10; WypozyczalniaCore\Integracje\ImportCennikaXml.vb=1; WypozyczalniaCore\PricingManager.vb=2; WypozyczalniaWeb\Cennik.aspx.vb=1; WypozyczalniaWeb\Klienci.aspx.vb=1 |
| WypozyczalniaCore\DAL\DbHelper.vb | 57 | Function | WykonajPolecenie | 24 | WypozyczalniaCore\CustomerManager.vb=4; WypozyczalniaCore\DAL\DbHelper.vb=1; WypozyczalniaCore\DAL\EquipmentDAL.vb=4; WypozyczalniaCore\DAL\InvoiceDAL.vb=3; WypozyczalniaCore\DAL\PricingDAL.vb=4; WypozyczalniaCore\DAL\RentalDAL.vb=3; WypozyczalniaCore\DAL\ReservationDAL.vb=3; WypozyczalniaCore\PricingManager.vb=1; WypozyczalniaWeb\Cennik.aspx.vb=1 |
| WypozyczalniaCore\DAL\DbHelper.vb | 69 | Function | WykonajProcedure | 3 | WypozyczalniaCore\DAL\DbHelper.vb=1; WypozyczalniaCore\DAL\PricingDAL.vb=1; WypozyczalniaCore\DAL\ReservationDAL.vb=1 |
| WypozyczalniaCore\DAL\DbHelper.vb | 84 | Function | WykonajProcedureBezWyniku | 7 | WypozyczalniaCore\DAL\DbHelper.vb=1; WypozyczalniaCore\DAL\InvoiceDAL.vb=1; WypozyczalniaCore\DAL\PricingDAL.vb=1; WypozyczalniaCore\DAL\RentalDAL.vb=2; WypozyczalniaCore\DAL\ReservationDAL.vb=2 |
| WypozyczalniaCore\DAL\DbHelper.vb | 96 | Function | Parametr | 169 | WypozyczalniaCore\CustomerManager.vb=23; WypozyczalniaCore\DAL\DbHelper.vb=1; WypozyczalniaCore\DAL\EquipmentDAL.vb=28; WypozyczalniaCore\DAL\InvoiceDAL.vb=25; WypozyczalniaCore\DAL\PricingDAL.vb=21; WypozyczalniaCore\DAL\RentalDAL.vb=30; WypozyczalniaCore\DAL\ReservationDAL.vb=34; WypozyczalniaCore\Integracje\ImportCennikaXml.vb=1; WypozyczalniaCore\PricingManager.vb=3; WypozyczalniaWeb\Rezerwacja.aspx.vb=3 |
| WypozyczalniaCore\DAL\DbHelper.vb | 103 | Function | ParametrWyjsciowy | 5 | WypozyczalniaCore\DAL\DbHelper.vb=1; WypozyczalniaCore\DAL\InvoiceDAL.vb=2; WypozyczalniaCore\DAL\RentalDAL.vb=1; WypozyczalniaCore\DAL\ReservationDAL.vb=1 |
| WypozyczalniaCore\DAL\EquipmentDAL.vb | 16 | Function | Pobierz | 8 | WypozyczalniaCore\DAL\EquipmentDAL.vb=1; WypozyczalniaCore\DAL\InvoiceDAL.vb=1; WypozyczalniaCore\DAL\RentalDAL.vb=2; WypozyczalniaCore\DAL\ReservationDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=1; WypozyczalniaCore\InvoiceManager.vb=1; WypozyczalniaCore\ReservationManager.vb=1 |
| WypozyczalniaCore\DAL\EquipmentDAL.vb | 25 | Function | PobierzPoNumerze | 6 | WypozyczalniaCore\DAL\EquipmentDAL.vb=1; WypozyczalniaCore\DAL\InvoiceDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=2; WypozyczalniaCore\InvoiceManager.vb=2 |
| WypozyczalniaCore\DAL\EquipmentDAL.vb | 35 | Function | Lista | 23 | WypozyczalniaCore\DAL\EquipmentDAL.vb=2; WypozyczalniaCore\DAL\RentalDAL.vb=3; WypozyczalniaCore\EquipmentManager.vb=2; WypozyczalniaCore\PricingManager.vb=3; WypozyczalniaWeb\Cennik.aspx.vb=3; WypozyczalniaWeb\Faktura.aspx.vb=1; WypozyczalniaWeb\Klienci.aspx.vb=4; WypozyczalniaWeb\Sprzet.aspx.vb=2; WypozyczalniaWeb\Wypozyczenie.aspx.vb=3 |
| WypozyczalniaCore\DAL\EquipmentDAL.vb | 50 | Function | ListaWStatusie | 1 | WypozyczalniaCore\DAL\EquipmentDAL.vb=1 |
| WypozyczalniaCore\DAL\EquipmentDAL.vb | 60 | Function | LiczbaWKategoriach | 2 | WypozyczalniaCore\DAL\EquipmentDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=1 |
| WypozyczalniaCore\DAL\EquipmentDAL.vb | 66 | Function | CzyNumerZajety | 2 | WypozyczalniaCore\DAL\EquipmentDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=1 |
| WypozyczalniaCore\DAL\EquipmentDAL.vb | 74 | Function | Dodaj | 2 | WypozyczalniaCore\DAL\EquipmentDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=1 |
| WypozyczalniaCore\DAL\EquipmentDAL.vb | 88 | Function | Aktualizuj | 2 | WypozyczalniaCore\DAL\EquipmentDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=1 |
| WypozyczalniaCore\DAL\EquipmentDAL.vb | 101 | Function | ZmienStatus | 15 | WypozyczalniaCore\DAL\EquipmentDAL.vb=1; WypozyczalniaCore\DAL\ReservationDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=6; WypozyczalniaCore\RentalManager.vb=3; WypozyczalniaCore\ReservationManager.vb=4 |
| WypozyczalniaCore\DAL\EquipmentDAL.vb | 112 | Function | Szukaj | 5 | WypozyczalniaCore\CustomerManager.vb=1; WypozyczalniaCore\DAL\EquipmentDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=2; WypozyczalniaWeb\Klienci.aspx.vb=1 |
| WypozyczalniaCore\DAL\EquipmentDAL.vb | 128 | Function | HistoriaWypozyczen | 4 | WypozyczalniaCore\DAL\EquipmentDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=2; WypozyczalniaWeb\Sprzet.aspx.vb=1 |
| WypozyczalniaCore\DAL\EquipmentDAL.vb | 136 | Function | ZmienStawke | 4 | WypozyczalniaCore\DAL\EquipmentDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=2; WypozyczalniaCore\PricingManager.vb=1 |
| WypozyczalniaCore\DAL\EquipmentDAL.vb | 144 | Function | MaOtwarteWypozyczenie | 2 | WypozyczalniaCore\DAL\EquipmentDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=1 |
| WypozyczalniaCore\DAL\InvoiceDAL.vb | 17 | Function | ZapiszNaglowek | 2 | WypozyczalniaCore\DAL\InvoiceDAL.vb=1; WypozyczalniaCore\InvoiceManager.vb=1 |
| WypozyczalniaCore\DAL\InvoiceDAL.vb | 36 | Sub | ZapiszPozycje | 2 | WypozyczalniaCore\DAL\InvoiceDAL.vb=1; WypozyczalniaCore\InvoiceManager.vb=1 |
| WypozyczalniaCore\DAL\InvoiceDAL.vb | 49 | Function | Pobierz | 8 | WypozyczalniaCore\DAL\EquipmentDAL.vb=1; WypozyczalniaCore\DAL\InvoiceDAL.vb=1; WypozyczalniaCore\DAL\RentalDAL.vb=2; WypozyczalniaCore\DAL\ReservationDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=1; WypozyczalniaCore\InvoiceManager.vb=1; WypozyczalniaCore\ReservationManager.vb=1 |
| WypozyczalniaCore\DAL\InvoiceDAL.vb | 58 | Function | PobierzDlaWypozyczenia | 2 | WypozyczalniaCore\DAL\InvoiceDAL.vb=1; WypozyczalniaCore\InvoiceManager.vb=1 |
| WypozyczalniaCore\DAL\InvoiceDAL.vb | 68 | Function | PobierzPozycje | 2 | WypozyczalniaCore\DAL\InvoiceDAL.vb=1; WypozyczalniaCore\InvoiceManager.vb=1 |
| WypozyczalniaCore\DAL\InvoiceDAL.vb | 74 | Function | UstawOplacona | 2 | WypozyczalniaCore\DAL\InvoiceDAL.vb=1; WypozyczalniaCore\InvoiceManager.vb=1 |
| WypozyczalniaCore\DAL\InvoiceDAL.vb | 87 | Function | FakturyZDnia | 6 | WypozyczalniaCore\DAL\InvoiceDAL.vb=2; WypozyczalniaCore\Integracje\EksportCsv.vb=1; WypozyczalniaCore\InvoiceManager.vb=2; WypozyczalniaWeb\Faktura.aspx.vb=1 |
| WypozyczalniaCore\DAL\InvoiceDAL.vb | 99 | Function | FakturyKlienta | 3 | WypozyczalniaCore\DAL\InvoiceDAL.vb=1; WypozyczalniaCore\InvoiceManager.vb=2 |
| WypozyczalniaCore\DAL\InvoiceDAL.vb | 105 | Function | Nieoplacone | 3 | WypozyczalniaCore\DAL\InvoiceDAL.vb=1; WypozyczalniaCore\InvoiceManager.vb=2 |
| WypozyczalniaCore\DAL\InvoiceDAL.vb | 111 | Function | PobierzPoNumerze | 6 | WypozyczalniaCore\DAL\EquipmentDAL.vb=1; WypozyczalniaCore\DAL\InvoiceDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=2; WypozyczalniaCore\InvoiceManager.vb=2 |
| WypozyczalniaCore\DAL\InvoiceDAL.vb | 121 | Function | ZestawienieMiesiaca | 3 | WypozyczalniaCore\DAL\InvoiceDAL.vb=1; WypozyczalniaCore\InvoiceManager.vb=2 |
| WypozyczalniaCore\DAL\InvoiceDAL.vb | 132 | Sub | Usun | 2 | WypozyczalniaCore\DAL\InvoiceDAL.vb=1; WypozyczalniaCore\InvoiceManager.vb=1 |
| WypozyczalniaCore\DAL\InvoiceDAL.vb | 153 | Function | CzyIstniejeDlaWypozyczenia | 3 | WypozyczalniaCore\DAL\InvoiceDAL.vb=1; WypozyczalniaCore\InvoiceManager.vb=2 |
| WypozyczalniaCore\DAL\PricingDAL.vb | 14 | Function | PobierzTaryfe | 6 | WypozyczalniaCore\DAL\PricingDAL.vb=2; WypozyczalniaCore\PricingManager.vb=4 |
| WypozyczalniaCore\DAL\PricingDAL.vb | 24 | Sub | ZapiszTaryfe | 4 | WypozyczalniaCore\DAL\PricingDAL.vb=1; WypozyczalniaCore\Integracje\ImportCennikaXml.vb=1; WypozyczalniaCore\PricingManager.vb=2 |
| WypozyczalniaCore\DAL\PricingDAL.vb | 33 | Function | ListaTaryf | 3 | WypozyczalniaCore\DAL\PricingDAL.vb=1; WypozyczalniaCore\PricingManager.vb=2 |
| WypozyczalniaCore\DAL\PricingDAL.vb | 39 | Function | TaryfyKategorii | 2 | WypozyczalniaCore\DAL\PricingDAL.vb=1; WypozyczalniaCore\PricingManager.vb=1 |
| WypozyczalniaCore\DAL\PricingDAL.vb | 50 | Function | UsunTaryfe | 1 | WypozyczalniaCore\DAL\PricingDAL.vb=1 |
| WypozyczalniaCore\DAL\PricingDAL.vb | 55 | Function | ZaktualizujStawkeSprzetu | 3 | WypozyczalniaCore\DAL\PricingDAL.vb=2; WypozyczalniaCore\Integracje\ImportCennikaXml.vb=1 |
| WypozyczalniaCore\DAL\PricingDAL.vb | 64 | Function | StawkaSprzetu | 1 | WypozyczalniaCore\DAL\PricingDAL.vb=1 |
| WypozyczalniaCore\DAL\PricingDAL.vb | 71 | Sub | ZapiszWDzienniku | 5 | WypozyczalniaCore\DAL\PricingDAL.vb=1; WypozyczalniaCore\Integracje\ImportCennikaXml.vb=1; WypozyczalniaCore\PricingManager.vb=3 |
| WypozyczalniaCore\DAL\PricingDAL.vb | 79 | Function | TaryfaIstnieje | 1 | WypozyczalniaCore\DAL\PricingDAL.vb=1 |
| WypozyczalniaCore\DAL\PricingDAL.vb | 87 | Function | TaryfyNaDzien | 4 | WypozyczalniaCore\DAL\PricingDAL.vb=1; WypozyczalniaCore\PricingManager.vb=3 |
| WypozyczalniaCore\DAL\PricingDAL.vb | 99 | Function | ListaSprzetuZeStawkami | 1 | WypozyczalniaCore\DAL\PricingDAL.vb=1 |
| WypozyczalniaCore\DAL\PricingDAL.vb | 110 | Function | HistoriaZmianCennika | 1 | WypozyczalniaCore\DAL\PricingDAL.vb=1 |
| WypozyczalniaCore\DAL\PricingDAL.vb | 117 | Function | ZaktualizujKaucje | 2 | WypozyczalniaCore\DAL\PricingDAL.vb=1; WypozyczalniaCore\PricingManager.vb=1 |
| WypozyczalniaCore\DAL\PricingDAL.vb | 124 | Function | PobierzTaryfePoId | 1 | WypozyczalniaCore\DAL\PricingDAL.vb=1 |
| WypozyczalniaCore\DAL\PricingDAL.vb | 131 | Function | KategorieZTaryfami | 1 | WypozyczalniaCore\DAL\PricingDAL.vb=1 |
| WypozyczalniaCore\DAL\RentalDAL.vb | 21 | Function | Wydaj | 2 | WypozyczalniaCore\DAL\RentalDAL.vb=1; WypozyczalniaCore\RentalManager.vb=1 |
| WypozyczalniaCore\DAL\RentalDAL.vb | 37 | Sub | ZapiszZwrot | 3 | WypozyczalniaCore\DAL\RentalDAL.vb=1; WypozyczalniaCore\RentalManager.vb=2 |
| WypozyczalniaCore\DAL\RentalDAL.vb | 49 | Function | Pobierz | 8 | WypozyczalniaCore\DAL\EquipmentDAL.vb=1; WypozyczalniaCore\DAL\InvoiceDAL.vb=1; WypozyczalniaCore\DAL\RentalDAL.vb=2; WypozyczalniaCore\DAL\ReservationDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=1; WypozyczalniaCore\InvoiceManager.vb=1; WypozyczalniaCore\ReservationManager.vb=1 |
| WypozyczalniaCore\DAL\RentalDAL.vb | 56 | Function | PobierzWypozyczenie | 17 | WypozyczalniaCore\DAL\RentalDAL.vb=1; WypozyczalniaCore\InvoiceManager.vb=3; WypozyczalniaCore\RentalManager.vb=8; WypozyczalniaWeb\Faktura.aspx.vb=1; WypozyczalniaWeb\Wypozyczenie.aspx.vb=1; WypozyczalniaWeb\Zwrot.aspx.vb=3 |
| WypozyczalniaCore\DAL\RentalDAL.vb | 62 | Sub | UstawNumerPrzesylki | 4 | WypozyczalniaCore\DAL\RentalDAL.vb=2; WypozyczalniaCore\RentalManager.vb=2 |
| WypozyczalniaCore\DAL\RentalDAL.vb | 68 | Function | Otwarte | 9 | WypozyczalniaCore\DAL\RentalDAL.vb=1; WypozyczalniaCore\RentalManager.vb=2; WypozyczalniaWeb\Wypozyczenie.aspx.vb=5; WypozyczalniaWeb\Zwrot.aspx.vb=1 |
| WypozyczalniaCore\DAL\RentalDAL.vb | 80 | Function | WypozyczeniaZDnia | 7 | WypozyczalniaCore\DAL\RentalDAL.vb=2; WypozyczalniaCore\Integracje\EksportCsv.vb=1; WypozyczalniaCore\RentalManager.vb=3; WypozyczalniaWeb\Wypozyczenie.aspx.vb=1 |
| WypozyczalniaCore\DAL\RentalDAL.vb | 97 | Function | WypozyczeniaKlienta | 3 | WypozyczalniaCore\DAL\RentalDAL.vb=1; WypozyczalniaCore\RentalManager.vb=2 |
| WypozyczalniaCore\DAL\RentalDAL.vb | 107 | Function | PoTerminie | 8 | WypozyczalniaCore\DAL\RentalDAL.vb=1; WypozyczalniaCore\RentalManager.vb=2; WypozyczalniaWeb\Wypozyczenie.aspx.vb=5 |
| WypozyczalniaCore\DAL\RentalDAL.vb | 117 | Sub | ZmienPlanowanyZwrot | 2 | WypozyczalniaCore\DAL\RentalDAL.vb=1; WypozyczalniaCore\RentalManager.vb=1 |
| WypozyczalniaCore\DAL\RentalDAL.vb | 127 | Function | DoZwrotuWOkresie | 2 | WypozyczalniaCore\DAL\RentalDAL.vb=1; WypozyczalniaCore\RentalManager.vb=1 |
| WypozyczalniaCore\DAL\RentalDAL.vb | 135 | Function | OtwarteKlienta | 5 | WypozyczalniaCore\DAL\RentalDAL.vb=1; WypozyczalniaCore\RentalManager.vb=3; WypozyczalniaWeb\Wypozyczenie.aspx.vb=1 |
| WypozyczalniaCore\DAL\RentalDAL.vb | 142 | Function | OstatnieDlaSprzetu | 2 | WypozyczalniaCore\DAL\RentalDAL.vb=1; WypozyczalniaCore\RentalManager.vb=1 |
| WypozyczalniaCore\DAL\RentalDAL.vb | 158 | Function | CzyOtwarteDlaSprzetu | 2 | WypozyczalniaCore\DAL\RentalDAL.vb=1; WypozyczalniaCore\RentalManager.vb=1 |
| WypozyczalniaCore\DAL\RentalDAL.vb | 165 | Function | LiczbaOtwartych | 1 | WypozyczalniaCore\DAL\RentalDAL.vb=1 |
| WypozyczalniaCore\DAL\RentalDAL.vb | 171 | Function | SumaKaucjiOtwartych | 1 | WypozyczalniaCore\DAL\RentalDAL.vb=1 |
| WypozyczalniaCore\DAL\RentalDAL.vb | 177 | Function | SumaKarZOkresu | 1 | WypozyczalniaCore\DAL\RentalDAL.vb=1 |
| WypozyczalniaCore\DAL\RentalDAL.vb | 186 | Sub | ZapiszDziennik | 10 | WypozyczalniaCore\CustomerManager.vb=4; WypozyczalniaCore\DAL\RentalDAL.vb=1; WypozyczalniaCore\RentalManager.vb=5 |
| WypozyczalniaCore\DAL\ReservationDAL.vb | 17 | Function | PobierzKolizje | 6 | WypozyczalniaCore\DAL\ReservationDAL.vb=3; WypozyczalniaCore\EquipmentManager.vb=3 |
| WypozyczalniaCore\DAL\ReservationDAL.vb | 24 | Function | Zapisz | 7 | WypozyczalniaCore\CustomerManager.vb=1; WypozyczalniaCore\DAL\ReservationDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=1; WypozyczalniaCore\PricingManager.vb=1; WypozyczalniaCore\ReservationManager.vb=1; WypozyczalniaWeb\Klienci.aspx.vb=1; WypozyczalniaWeb\Sprzet.aspx.vb=1 |
| WypozyczalniaCore\DAL\ReservationDAL.vb | 44 | Function | Pobierz | 8 | WypozyczalniaCore\DAL\EquipmentDAL.vb=1; WypozyczalniaCore\DAL\InvoiceDAL.vb=1; WypozyczalniaCore\DAL\RentalDAL.vb=2; WypozyczalniaCore\DAL\ReservationDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=1; WypozyczalniaCore\InvoiceManager.vb=1; WypozyczalniaCore\ReservationManager.vb=1 |
| WypozyczalniaCore\DAL\ReservationDAL.vb | 53 | Sub | ZmienStatus | 15 | WypozyczalniaCore\DAL\EquipmentDAL.vb=1; WypozyczalniaCore\DAL\ReservationDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=6; WypozyczalniaCore\RentalManager.vb=3; WypozyczalniaCore\ReservationManager.vb=4 |
| WypozyczalniaCore\DAL\ReservationDAL.vb | 60 | Function | RezerwacjeKlienta | 6 | WypozyczalniaCore\DAL\ReservationDAL.vb=1; WypozyczalniaCore\ReservationManager.vb=3; WypozyczalniaWeb\Rezerwacja.aspx.vb=1; WypozyczalniaWeb\Wypozyczenie.aspx.vb=1 |
| WypozyczalniaCore\DAL\ReservationDAL.vb | 68 | Function | RezerwacjeSprzetu | 3 | WypozyczalniaCore\DAL\ReservationDAL.vb=1; WypozyczalniaCore\ReservationManager.vb=2 |
| WypozyczalniaCore\DAL\ReservationDAL.vb | 78 | Function | RezerwacjeWOkresie | 3 | WypozyczalniaCore\DAL\ReservationDAL.vb=1; WypozyczalniaCore\ReservationManager.vb=2 |
| WypozyczalniaCore\DAL\ReservationDAL.vb | 88 | Function | LiczbaAktywnychKlienta | 2 | WypozyczalniaCore\DAL\ReservationDAL.vb=1; WypozyczalniaCore\ReservationManager.vb=1 |
| WypozyczalniaCore\DAL\ReservationDAL.vb | 94 | Function | PrzeterminowaneNowe | 2 | WypozyczalniaCore\DAL\ReservationDAL.vb=1; WypozyczalniaCore\ReservationManager.vb=1 |
| WypozyczalniaCore\DAL\ReservationDAL.vb | 103 | Function | PobierzKolizjeBezRezerwacji | 2 | WypozyczalniaCore\DAL\ReservationDAL.vb=1; WypozyczalniaCore\ReservationManager.vb=1 |
| WypozyczalniaCore\DAL\ReservationDAL.vb | 114 | Sub | ZmienTermin | 4 | WypozyczalniaCore\DAL\ReservationDAL.vb=1; WypozyczalniaCore\ReservationManager.vb=3 |
| WypozyczalniaCore\DAL\ReservationDAL.vb | 124 | Function | RezerwacjeNaDzien | 3 | WypozyczalniaCore\DAL\ReservationDAL.vb=1; WypozyczalniaCore\ReservationManager.vb=2 |
| WypozyczalniaCore\DAL\ReservationDAL.vb | 137 | Function | Statystyki | 3 | WypozyczalniaCore\DAL\ReservationDAL.vb=1; WypozyczalniaCore\ReservationManager.vb=2 |
| WypozyczalniaCore\DAL\ReservationDAL.vb | 148 | Function | KalendarzSprzetu | 2 | WypozyczalniaCore\DAL\ReservationDAL.vb=1; WypozyczalniaCore\ReservationManager.vb=1 |
| WypozyczalniaCore\DAL\ReservationDAL.vb | 163 | Function | DoPotwierdzenia | 4 | WypozyczalniaCore\DAL\ReservationDAL.vb=1; WypozyczalniaCore\ReservationManager.vb=3 |
| WypozyczalniaCore\DAL\ReservationDAL.vb | 173 | Sub | ZapiszUwagi | 2 | WypozyczalniaCore\DAL\ReservationDAL.vb=1; WypozyczalniaCore\ReservationManager.vb=1 |
| WypozyczalniaCore\DAL\ReservationDAL.vb | 180 | Sub | DopiszDoDziennika | 4 | WypozyczalniaCore\DAL\ReservationDAL.vb=1; WypozyczalniaCore\ReservationManager.vb=3 |
| WypozyczalniaCore\EquipmentManager.vb | 19 | Function | PobierzSprzet | 26 | WypozyczalniaCore\EquipmentManager.vb=4; WypozyczalniaCore\InvoiceManager.vb=2; WypozyczalniaCore\PricingManager.vb=5; WypozyczalniaCore\RentalManager.vb=5; WypozyczalniaCore\ReservationManager.vb=1; WypozyczalniaWeb\Cennik.aspx.vb=1; WypozyczalniaWeb\Faktura.aspx.vb=1; WypozyczalniaWeb\Rezerwacja.aspx.vb=2; WypozyczalniaWeb\Sprzet.aspx.vb=2; WypozyczalniaWeb\Wypozyczenie.aspx.vb=1; WypozyczalniaWeb\Zwrot.aspx.vb=2 |
| WypozyczalniaCore\EquipmentManager.vb | 27 | Function | PobierzPoNumerze | 6 | WypozyczalniaCore\DAL\EquipmentDAL.vb=1; WypozyczalniaCore\DAL\InvoiceDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=2; WypozyczalniaCore\InvoiceManager.vb=2 |
| WypozyczalniaCore\EquipmentManager.vb | 35 | Function | ListaWKategorii | 8 | WypozyczalniaCore\EquipmentManager.vb=2; WypozyczalniaCore\PricingManager.vb=2; WypozyczalniaWeb\Cennik.aspx.vb=1; WypozyczalniaWeb\Rezerwacja.aspx.vb=1; WypozyczalniaWeb\Sprzet.aspx.vb=1; WypozyczalniaWeb\Wypozyczenie.aspx.vb=1 |
| WypozyczalniaCore\EquipmentManager.vb | 44 | Function | SprawdzDostepnosc | 7 | WypozyczalniaCore\EquipmentManager.vb=3; WypozyczalniaCore\RentalManager.vb=1; WypozyczalniaCore\ReservationManager.vb=2; WypozyczalniaWeb\Rezerwacja.aspx.vb=1 |
| WypozyczalniaCore\EquipmentManager.vb | 55 | Function | DostepneWKategorii | 1 | WypozyczalniaCore\EquipmentManager.vb=1 |
| WypozyczalniaCore\EquipmentManager.vb | 68 | Sub | ZmienStatus | 15 | WypozyczalniaCore\DAL\EquipmentDAL.vb=1; WypozyczalniaCore\DAL\ReservationDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=6; WypozyczalniaCore\RentalManager.vb=3; WypozyczalniaCore\ReservationManager.vb=4 |
| WypozyczalniaCore\EquipmentManager.vb | 81 | Function | Zapisz | 7 | WypozyczalniaCore\CustomerManager.vb=1; WypozyczalniaCore\DAL\ReservationDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=1; WypozyczalniaCore\PricingManager.vb=1; WypozyczalniaCore\ReservationManager.vb=1; WypozyczalniaWeb\Klienci.aspx.vb=1; WypozyczalniaWeb\Sprzet.aspx.vb=1 |
| WypozyczalniaCore\EquipmentManager.vb | 103 | Sub | WyslijDoSerwisu | 2 | WypozyczalniaCore\EquipmentManager.vb=1; WypozyczalniaWeb\Sprzet.aspx.vb=1 |
| WypozyczalniaCore\EquipmentManager.vb | 114 | Sub | Wycofaj | 1 | WypozyczalniaCore\EquipmentManager.vb=1 |
| WypozyczalniaCore\EquipmentManager.vb | 125 | Sub | PrzywrocZSerwisu | 1 | WypozyczalniaCore\EquipmentManager.vb=1 |
| WypozyczalniaCore\EquipmentManager.vb | 136 | Sub | ZmienStawke | 4 | WypozyczalniaCore\DAL\EquipmentDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=2; WypozyczalniaCore\PricingManager.vb=1 |
| WypozyczalniaCore\EquipmentManager.vb | 150 | Function | Szukaj | 5 | WypozyczalniaCore\CustomerManager.vb=1; WypozyczalniaCore\DAL\EquipmentDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=2; WypozyczalniaWeb\Klienci.aspx.vb=1 |
| WypozyczalniaCore\EquipmentManager.vb | 158 | Function | HistoriaWypozyczen | 4 | WypozyczalniaCore\DAL\EquipmentDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=2; WypozyczalniaWeb\Sprzet.aspx.vb=1 |
| WypozyczalniaCore\EquipmentManager.vb | 163 | Function | KolizjeWTerminie | 2 | WypozyczalniaCore\EquipmentManager.vb=1; WypozyczalniaWeb\Rezerwacja.aspx.vb=1 |
| WypozyczalniaCore\EquipmentManager.vb | 167 | Function | CzyMoznaZmienicKategorie | 2 | WypozyczalniaCore\EquipmentManager.vb=1; WypozyczalniaWeb\Sprzet.aspx.vb=1 |
| WypozyczalniaCore\EquipmentManager.vb | 172 | Function | Kategorie | 2 | WypozyczalniaCore\DAL\EquipmentDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=1 |
| WypozyczalniaCore\EquipmentManager.vb | 181 | Function | Podsumowanie | 1 | WypozyczalniaCore\EquipmentManager.vb=1 |
| WypozyczalniaCore\Integracje\EksportCsv.vb | 20 | Function | EksportujDzien | 7 | Jobs\EksportNocny\Program.vb=2; WypozyczalniaCore\Integracje\EksportCsv.vb=4; WypozyczalniaWeb\Faktura.aspx.vb=1 |
| WypozyczalniaCore\Integracje\EksportCsv.vb | 28 | Function | EksportujDzien | 7 | Jobs\EksportNocny\Program.vb=2; WypozyczalniaCore\Integracje\EksportCsv.vb=4; WypozyczalniaWeb\Faktura.aspx.vb=1 |
| WypozyczalniaCore\Integracje\EksportCsv.vb | 104 | Function | EksportujOkres | 1 | WypozyczalniaCore\Integracje\EksportCsv.vb=1 |
| WypozyczalniaCore\Integracje\EksportCsv.vb | 120 | Function | NazwaPlikuDnia | 2 | WypozyczalniaCore\Integracje\EksportCsv.vb=2 |
| WypozyczalniaCore\Integracje\EksportCsv.vb | 124 | Function | CzyWyeksportowano | 3 | Jobs\EksportNocny\Program.vb=1; WypozyczalniaCore\Integracje\EksportCsv.vb=2 |
| WypozyczalniaCore\Integracje\ImportCennikaXml.vb | 22 | Function | ImportujKatalog | 3 | Jobs\EksportNocny\Program.vb=1; WypozyczalniaCore\Integracje\ImportCennikaXml.vb=1; WypozyczalniaWeb\Cennik.aspx.vb=1 |
| WypozyczalniaCore\Integracje\ImportCennikaXml.vb | 43 | Function | ImportujPlik | 2 | WypozyczalniaCore\Integracje\ImportCennikaXml.vb=2 |
| WypozyczalniaCore\Integracje\ImportCennikaXml.vb | 97 | Function | PlikiOczekujace | 2 | WypozyczalniaCore\Integracje\ImportCennikaXml.vb=1; WypozyczalniaWeb\Cennik.aspx.vb=1 |
| WypozyczalniaCore\Integracje\ImportCennikaXml.vb | 107 | Function | SprawdzPlik | 1 | WypozyczalniaCore\Integracje\ImportCennikaXml.vb=1 |
| WypozyczalniaCore\Integracje\KurierClient.vb | 22 | Sub | New | 368 | Jobs\EksportNocny\Program.vb=4; WypozyczalniaCore\CustomerManager.vb=7; WypozyczalniaCore\DAL\DbHelper.vb=20; WypozyczalniaCore\DAL\EquipmentDAL.vb=4; WypozyczalniaCore\DAL\InvoiceDAL.vb=6; WypozyczalniaCore\DAL\PricingDAL.vb=6; WypozyczalniaCore\DAL\RentalDAL.vb=3; WypozyczalniaCore\DAL\ReservationDAL.vb=2; WypozyczalniaCore\EquipmentManager.vb=36; WypozyczalniaCore\Integracje\EksportCsv.vb=7; WypozyczalniaCore\Integracje\ImportCennikaXml.vb=10; WypozyczalniaCore\Integracje\KurierClient.vb=5; WypozyczalniaCore\InvoiceManager.vb=31; WypozyczalniaCore\Model\Faktura.vb=4; WypozyczalniaCore\Model\Klient.vb=1; WypozyczalniaCore\Model\Rezerwacja.vb=1; WypozyczalniaCore\Model\Sprzet.vb=1; WypozyczalniaCore\Model\Taryfa.vb=5; WypozyczalniaCore\Model\Wypozyczenie.vb=2; WypozyczalniaCore\PricingManager.vb=37; WypozyczalniaCore\RentalManager.vb=56; WypozyczalniaCore\ReservationManager.vb=43; WypozyczalniaWeb\Cennik.aspx.vb=10; WypozyczalniaWeb\Faktura.aspx.vb=10; WypozyczalniaWeb\Global.asax.vb=2; WypozyczalniaWeb\Klienci.aspx.vb=6; WypozyczalniaWeb\Rezerwacja.aspx.vb=12; WypozyczalniaWeb\Sprzet.aspx.vb=6; WypozyczalniaWeb\Web References\BramkaPlatnosci\Reference.vb=5; WypozyczalniaWeb\Wypozyczenie.aspx.vb=13; WypozyczalniaWeb\Zwrot.aspx.vb=13 |
| WypozyczalniaCore\Integracje\KurierClient.vb | 28 | Function | ZamowDostawe | 3 | WypozyczalniaCore\Integracje\KurierClient.vb=1; WypozyczalniaCore\RentalManager.vb=2 |
| WypozyczalniaCore\Integracje\KurierClient.vb | 61 | Function | StatusPrzesylki | 2 | WypozyczalniaCore\Integracje\KurierClient.vb=1; WypozyczalniaCore\RentalManager.vb=1 |
| WypozyczalniaCore\Integracje\KurierClient.vb | 72 | Function | AnulujPrzesylke | 1 | WypozyczalniaCore\Integracje\KurierClient.vb=1 |
| WypozyczalniaCore\Integracje\KurierClient.vb | 86 | Function | CzyDostepny | 1 | WypozyczalniaCore\Integracje\KurierClient.vb=1 |
| WypozyczalniaCore\InvoiceManager.vb | 19 | Function | WystawFaktureZaWypozyczenie | 2 | WypozyczalniaCore\InvoiceManager.vb=1; WypozyczalniaWeb\Faktura.aspx.vb=1 |
| WypozyczalniaCore\InvoiceManager.vb | 73 | Function | PobierzFakture | 5 | WypozyczalniaCore\InvoiceManager.vb=3; WypozyczalniaWeb\Faktura.aspx.vb=2 |
| WypozyczalniaCore\InvoiceManager.vb | 84 | Function | PobierzFaktureDlaWypozyczenia | 2 | WypozyczalniaCore\InvoiceManager.vb=1; WypozyczalniaWeb\Faktura.aspx.vb=1 |
| WypozyczalniaCore\InvoiceManager.vb | 95 | Sub | OznaczOplacona | 2 | WypozyczalniaCore\InvoiceManager.vb=1; WypozyczalniaWeb\Faktura.aspx.vb=1 |
| WypozyczalniaCore\InvoiceManager.vb | 115 | Function | FakturyZDnia | 6 | WypozyczalniaCore\DAL\InvoiceDAL.vb=2; WypozyczalniaCore\Integracje\EksportCsv.vb=1; WypozyczalniaCore\InvoiceManager.vb=2; WypozyczalniaWeb\Faktura.aspx.vb=1 |
| WypozyczalniaCore\InvoiceManager.vb | 119 | Function | FakturyKlienta | 3 | WypozyczalniaCore\DAL\InvoiceDAL.vb=1; WypozyczalniaCore\InvoiceManager.vb=2 |
| WypozyczalniaCore\InvoiceManager.vb | 123 | Function | Nieoplacone | 3 | WypozyczalniaCore\DAL\InvoiceDAL.vb=1; WypozyczalniaCore\InvoiceManager.vb=2 |
| WypozyczalniaCore\InvoiceManager.vb | 127 | Function | PobierzPoNumerze | 6 | WypozyczalniaCore\DAL\EquipmentDAL.vb=1; WypozyczalniaCore\DAL\InvoiceDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=2; WypozyczalniaCore\InvoiceManager.vb=2 |
| WypozyczalniaCore\InvoiceManager.vb | 139 | Function | CzyMoznaWystawic | 1 | WypozyczalniaCore\InvoiceManager.vb=1 |
| WypozyczalniaCore\InvoiceManager.vb | 147 | Sub | AnulujNieoplacona | 1 | WypozyczalniaCore\InvoiceManager.vb=1 |
| WypozyczalniaCore\InvoiceManager.vb | 160 | Function | TerminPlatnosci | 1 | WypozyczalniaCore\InvoiceManager.vb=1 |
| WypozyczalniaCore\InvoiceManager.vb | 168 | Function | ZestawienieMiesiaca | 3 | WypozyczalniaCore\DAL\InvoiceDAL.vb=1; WypozyczalniaCore\InvoiceManager.vb=2 |
| WypozyczalniaCore\InvoiceManager.vb | 172 | Function | PodgladFaktury | 2 | WypozyczalniaCore\InvoiceManager.vb=1; WypozyczalniaWeb\Faktura.aspx.vb=1 |
| WypozyczalniaCore\Model\Faktura.vb | 24 | Sub | New | 368 | Jobs\EksportNocny\Program.vb=4; WypozyczalniaCore\CustomerManager.vb=7; WypozyczalniaCore\DAL\DbHelper.vb=20; WypozyczalniaCore\DAL\EquipmentDAL.vb=4; WypozyczalniaCore\DAL\InvoiceDAL.vb=6; WypozyczalniaCore\DAL\PricingDAL.vb=6; WypozyczalniaCore\DAL\RentalDAL.vb=3; WypozyczalniaCore\DAL\ReservationDAL.vb=2; WypozyczalniaCore\EquipmentManager.vb=36; WypozyczalniaCore\Integracje\EksportCsv.vb=7; WypozyczalniaCore\Integracje\ImportCennikaXml.vb=10; WypozyczalniaCore\Integracje\KurierClient.vb=5; WypozyczalniaCore\InvoiceManager.vb=31; WypozyczalniaCore\Model\Faktura.vb=4; WypozyczalniaCore\Model\Klient.vb=1; WypozyczalniaCore\Model\Rezerwacja.vb=1; WypozyczalniaCore\Model\Sprzet.vb=1; WypozyczalniaCore\Model\Taryfa.vb=5; WypozyczalniaCore\Model\Wypozyczenie.vb=2; WypozyczalniaCore\PricingManager.vb=37; WypozyczalniaCore\RentalManager.vb=56; WypozyczalniaCore\ReservationManager.vb=43; WypozyczalniaWeb\Cennik.aspx.vb=10; WypozyczalniaWeb\Faktura.aspx.vb=10; WypozyczalniaWeb\Global.asax.vb=2; WypozyczalniaWeb\Klienci.aspx.vb=6; WypozyczalniaWeb\Rezerwacja.aspx.vb=12; WypozyczalniaWeb\Sprzet.aspx.vb=6; WypozyczalniaWeb\Web References\BramkaPlatnosci\Reference.vb=5; WypozyczalniaWeb\Wypozyczenie.aspx.vb=13; WypozyczalniaWeb\Zwrot.aspx.vb=13 |
| WypozyczalniaCore\Model\Faktura.vb | 45 | Function | SumaPozycjiNetto | 1 | WypozyczalniaCore\Model\Faktura.vb=1 |
| WypozyczalniaCore\Model\Faktura.vb | 53 | Function | ZWiersza | 11 | WypozyczalniaCore\DAL\RentalDAL.vb=2; WypozyczalniaCore\InvoiceManager.vb=4; WypozyczalniaCore\Model\Faktura.vb=3; WypozyczalniaCore\Model\Wypozyczenie.vb=2 |
| WypozyczalniaCore\Model\Faktura.vb | 68 | Function | ToString | 121 | Jobs\EksportNocny\Program.vb=6; WypozyczalniaCore\CustomerManager.vb=10; WypozyczalniaCore\DAL\InvoiceDAL.vb=1; WypozyczalniaCore\DAL\PricingDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=2; WypozyczalniaCore\Integracje\EksportCsv.vb=13; WypozyczalniaCore\Integracje\ImportCennikaXml.vb=1; WypozyczalniaCore\Integracje\KurierClient.vb=6; WypozyczalniaCore\InvoiceManager.vb=1; WypozyczalniaCore\Model\Faktura.vb=5; WypozyczalniaCore\Model\Klient.vb=2; WypozyczalniaCore\Model\Rezerwacja.vb=3; WypozyczalniaCore\Model\Sprzet.vb=1; WypozyczalniaCore\Model\Taryfa.vb=4; WypozyczalniaCore\Model\Wypozyczenie.vb=3; WypozyczalniaCore\PricingManager.vb=9; WypozyczalniaCore\RentalManager.vb=4; WypozyczalniaCore\ReservationManager.vb=4; WypozyczalniaWeb\Cennik.aspx.vb=9; WypozyczalniaWeb\Faktura.aspx.vb=3; WypozyczalniaWeb\Global.asax.vb=2; WypozyczalniaWeb\Klienci.aspx.vb=1; WypozyczalniaWeb\Rezerwacja.aspx.vb=10; WypozyczalniaWeb\Sprzet.aspx.vb=4; WypozyczalniaWeb\Wypozyczenie.aspx.vb=8; WypozyczalniaWeb\Zwrot.aspx.vb=8 |
| WypozyczalniaCore\Model\Faktura.vb | 83 | Function | ZWiersza | 11 | WypozyczalniaCore\DAL\RentalDAL.vb=2; WypozyczalniaCore\InvoiceManager.vb=4; WypozyczalniaCore\Model\Faktura.vb=3; WypozyczalniaCore\Model\Wypozyczenie.vb=2 |
| WypozyczalniaCore\Model\Klient.vb | 29 | Sub | New | 368 | Jobs\EksportNocny\Program.vb=4; WypozyczalniaCore\CustomerManager.vb=7; WypozyczalniaCore\DAL\DbHelper.vb=20; WypozyczalniaCore\DAL\EquipmentDAL.vb=4; WypozyczalniaCore\DAL\InvoiceDAL.vb=6; WypozyczalniaCore\DAL\PricingDAL.vb=6; WypozyczalniaCore\DAL\RentalDAL.vb=3; WypozyczalniaCore\DAL\ReservationDAL.vb=2; WypozyczalniaCore\EquipmentManager.vb=36; WypozyczalniaCore\Integracje\EksportCsv.vb=7; WypozyczalniaCore\Integracje\ImportCennikaXml.vb=10; WypozyczalniaCore\Integracje\KurierClient.vb=5; WypozyczalniaCore\InvoiceManager.vb=31; WypozyczalniaCore\Model\Faktura.vb=4; WypozyczalniaCore\Model\Klient.vb=1; WypozyczalniaCore\Model\Rezerwacja.vb=1; WypozyczalniaCore\Model\Sprzet.vb=1; WypozyczalniaCore\Model\Taryfa.vb=5; WypozyczalniaCore\Model\Wypozyczenie.vb=2; WypozyczalniaCore\PricingManager.vb=37; WypozyczalniaCore\RentalManager.vb=56; WypozyczalniaCore\ReservationManager.vb=43; WypozyczalniaWeb\Cennik.aspx.vb=10; WypozyczalniaWeb\Faktura.aspx.vb=10; WypozyczalniaWeb\Global.asax.vb=2; WypozyczalniaWeb\Klienci.aspx.vb=6; WypozyczalniaWeb\Rezerwacja.aspx.vb=12; WypozyczalniaWeb\Sprzet.aspx.vb=6; WypozyczalniaWeb\Web References\BramkaPlatnosci\Reference.vb=5; WypozyczalniaWeb\Wypozyczenie.aspx.vb=13; WypozyczalniaWeb\Zwrot.aspx.vb=13 |
| WypozyczalniaCore\Model\Klient.vb | 51 | Function | ToString | 121 | Jobs\EksportNocny\Program.vb=6; WypozyczalniaCore\CustomerManager.vb=10; WypozyczalniaCore\DAL\InvoiceDAL.vb=1; WypozyczalniaCore\DAL\PricingDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=2; WypozyczalniaCore\Integracje\EksportCsv.vb=13; WypozyczalniaCore\Integracje\ImportCennikaXml.vb=1; WypozyczalniaCore\Integracje\KurierClient.vb=6; WypozyczalniaCore\InvoiceManager.vb=1; WypozyczalniaCore\Model\Faktura.vb=5; WypozyczalniaCore\Model\Klient.vb=2; WypozyczalniaCore\Model\Rezerwacja.vb=3; WypozyczalniaCore\Model\Sprzet.vb=1; WypozyczalniaCore\Model\Taryfa.vb=4; WypozyczalniaCore\Model\Wypozyczenie.vb=3; WypozyczalniaCore\PricingManager.vb=9; WypozyczalniaCore\RentalManager.vb=4; WypozyczalniaCore\ReservationManager.vb=4; WypozyczalniaWeb\Cennik.aspx.vb=9; WypozyczalniaWeb\Faktura.aspx.vb=3; WypozyczalniaWeb\Global.asax.vb=2; WypozyczalniaWeb\Klienci.aspx.vb=1; WypozyczalniaWeb\Rezerwacja.aspx.vb=10; WypozyczalniaWeb\Sprzet.aspx.vb=4; WypozyczalniaWeb\Wypozyczenie.aspx.vb=8; WypozyczalniaWeb\Zwrot.aspx.vb=8 |
| WypozyczalniaCore\Model\Klient.vb | 55 | Function | Equals | 1 | WypozyczalniaCore\Model\Klient.vb=1 |
| WypozyczalniaCore\Model\Klient.vb | 61 | Function | GetHashCode | 2 | WypozyczalniaCore\Model\Klient.vb=2 |
| WypozyczalniaCore\Model\Rezerwacja.vb | 25 | Sub | New | 368 | Jobs\EksportNocny\Program.vb=4; WypozyczalniaCore\CustomerManager.vb=7; WypozyczalniaCore\DAL\DbHelper.vb=20; WypozyczalniaCore\DAL\EquipmentDAL.vb=4; WypozyczalniaCore\DAL\InvoiceDAL.vb=6; WypozyczalniaCore\DAL\PricingDAL.vb=6; WypozyczalniaCore\DAL\RentalDAL.vb=3; WypozyczalniaCore\DAL\ReservationDAL.vb=2; WypozyczalniaCore\EquipmentManager.vb=36; WypozyczalniaCore\Integracje\EksportCsv.vb=7; WypozyczalniaCore\Integracje\ImportCennikaXml.vb=10; WypozyczalniaCore\Integracje\KurierClient.vb=5; WypozyczalniaCore\InvoiceManager.vb=31; WypozyczalniaCore\Model\Faktura.vb=4; WypozyczalniaCore\Model\Klient.vb=1; WypozyczalniaCore\Model\Rezerwacja.vb=1; WypozyczalniaCore\Model\Sprzet.vb=1; WypozyczalniaCore\Model\Taryfa.vb=5; WypozyczalniaCore\Model\Wypozyczenie.vb=2; WypozyczalniaCore\PricingManager.vb=37; WypozyczalniaCore\RentalManager.vb=56; WypozyczalniaCore\ReservationManager.vb=43; WypozyczalniaWeb\Cennik.aspx.vb=10; WypozyczalniaWeb\Faktura.aspx.vb=10; WypozyczalniaWeb\Global.asax.vb=2; WypozyczalniaWeb\Klienci.aspx.vb=6; WypozyczalniaWeb\Rezerwacja.aspx.vb=12; WypozyczalniaWeb\Sprzet.aspx.vb=6; WypozyczalniaWeb\Web References\BramkaPlatnosci\Reference.vb=5; WypozyczalniaWeb\Wypozyczenie.aspx.vb=13; WypozyczalniaWeb\Zwrot.aspx.vb=13 |
| WypozyczalniaCore\Model\Rezerwacja.vb | 61 | Function | ToString | 121 | Jobs\EksportNocny\Program.vb=6; WypozyczalniaCore\CustomerManager.vb=10; WypozyczalniaCore\DAL\InvoiceDAL.vb=1; WypozyczalniaCore\DAL\PricingDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=2; WypozyczalniaCore\Integracje\EksportCsv.vb=13; WypozyczalniaCore\Integracje\ImportCennikaXml.vb=1; WypozyczalniaCore\Integracje\KurierClient.vb=6; WypozyczalniaCore\InvoiceManager.vb=1; WypozyczalniaCore\Model\Faktura.vb=5; WypozyczalniaCore\Model\Klient.vb=2; WypozyczalniaCore\Model\Rezerwacja.vb=3; WypozyczalniaCore\Model\Sprzet.vb=1; WypozyczalniaCore\Model\Taryfa.vb=4; WypozyczalniaCore\Model\Wypozyczenie.vb=3; WypozyczalniaCore\PricingManager.vb=9; WypozyczalniaCore\RentalManager.vb=4; WypozyczalniaCore\ReservationManager.vb=4; WypozyczalniaWeb\Cennik.aspx.vb=9; WypozyczalniaWeb\Faktura.aspx.vb=3; WypozyczalniaWeb\Global.asax.vb=2; WypozyczalniaWeb\Klienci.aspx.vb=1; WypozyczalniaWeb\Rezerwacja.aspx.vb=10; WypozyczalniaWeb\Sprzet.aspx.vb=4; WypozyczalniaWeb\Wypozyczenie.aspx.vb=8; WypozyczalniaWeb\Zwrot.aspx.vb=8 |
| WypozyczalniaCore\Model\Sprzet.vb | 24 | Sub | New | 368 | Jobs\EksportNocny\Program.vb=4; WypozyczalniaCore\CustomerManager.vb=7; WypozyczalniaCore\DAL\DbHelper.vb=20; WypozyczalniaCore\DAL\EquipmentDAL.vb=4; WypozyczalniaCore\DAL\InvoiceDAL.vb=6; WypozyczalniaCore\DAL\PricingDAL.vb=6; WypozyczalniaCore\DAL\RentalDAL.vb=3; WypozyczalniaCore\DAL\ReservationDAL.vb=2; WypozyczalniaCore\EquipmentManager.vb=36; WypozyczalniaCore\Integracje\EksportCsv.vb=7; WypozyczalniaCore\Integracje\ImportCennikaXml.vb=10; WypozyczalniaCore\Integracje\KurierClient.vb=5; WypozyczalniaCore\InvoiceManager.vb=31; WypozyczalniaCore\Model\Faktura.vb=4; WypozyczalniaCore\Model\Klient.vb=1; WypozyczalniaCore\Model\Rezerwacja.vb=1; WypozyczalniaCore\Model\Sprzet.vb=1; WypozyczalniaCore\Model\Taryfa.vb=5; WypozyczalniaCore\Model\Wypozyczenie.vb=2; WypozyczalniaCore\PricingManager.vb=37; WypozyczalniaCore\RentalManager.vb=56; WypozyczalniaCore\ReservationManager.vb=43; WypozyczalniaWeb\Cennik.aspx.vb=10; WypozyczalniaWeb\Faktura.aspx.vb=10; WypozyczalniaWeb\Global.asax.vb=2; WypozyczalniaWeb\Klienci.aspx.vb=6; WypozyczalniaWeb\Rezerwacja.aspx.vb=12; WypozyczalniaWeb\Sprzet.aspx.vb=6; WypozyczalniaWeb\Web References\BramkaPlatnosci\Reference.vb=5; WypozyczalniaWeb\Wypozyczenie.aspx.vb=13; WypozyczalniaWeb\Zwrot.aspx.vb=13 |
| WypozyczalniaCore\Model\Sprzet.vb | 59 | Function | CzyMoznaWydac | 1 | WypozyczalniaCore\Model\Sprzet.vb=1 |
| WypozyczalniaCore\Model\Sprzet.vb | 63 | Function | ToString | 121 | Jobs\EksportNocny\Program.vb=6; WypozyczalniaCore\CustomerManager.vb=10; WypozyczalniaCore\DAL\InvoiceDAL.vb=1; WypozyczalniaCore\DAL\PricingDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=2; WypozyczalniaCore\Integracje\EksportCsv.vb=13; WypozyczalniaCore\Integracje\ImportCennikaXml.vb=1; WypozyczalniaCore\Integracje\KurierClient.vb=6; WypozyczalniaCore\InvoiceManager.vb=1; WypozyczalniaCore\Model\Faktura.vb=5; WypozyczalniaCore\Model\Klient.vb=2; WypozyczalniaCore\Model\Rezerwacja.vb=3; WypozyczalniaCore\Model\Sprzet.vb=1; WypozyczalniaCore\Model\Taryfa.vb=4; WypozyczalniaCore\Model\Wypozyczenie.vb=3; WypozyczalniaCore\PricingManager.vb=9; WypozyczalniaCore\RentalManager.vb=4; WypozyczalniaCore\ReservationManager.vb=4; WypozyczalniaWeb\Cennik.aspx.vb=9; WypozyczalniaWeb\Faktura.aspx.vb=3; WypozyczalniaWeb\Global.asax.vb=2; WypozyczalniaWeb\Klienci.aspx.vb=1; WypozyczalniaWeb\Rezerwacja.aspx.vb=10; WypozyczalniaWeb\Sprzet.aspx.vb=4; WypozyczalniaWeb\Wypozyczenie.aspx.vb=8; WypozyczalniaWeb\Zwrot.aspx.vb=8 |
| WypozyczalniaCore\Model\Taryfa.vb | 19 | Sub | New | 368 | Jobs\EksportNocny\Program.vb=4; WypozyczalniaCore\CustomerManager.vb=7; WypozyczalniaCore\DAL\DbHelper.vb=20; WypozyczalniaCore\DAL\EquipmentDAL.vb=4; WypozyczalniaCore\DAL\InvoiceDAL.vb=6; WypozyczalniaCore\DAL\PricingDAL.vb=6; WypozyczalniaCore\DAL\RentalDAL.vb=3; WypozyczalniaCore\DAL\ReservationDAL.vb=2; WypozyczalniaCore\EquipmentManager.vb=36; WypozyczalniaCore\Integracje\EksportCsv.vb=7; WypozyczalniaCore\Integracje\ImportCennikaXml.vb=10; WypozyczalniaCore\Integracje\KurierClient.vb=5; WypozyczalniaCore\InvoiceManager.vb=31; WypozyczalniaCore\Model\Faktura.vb=4; WypozyczalniaCore\Model\Klient.vb=1; WypozyczalniaCore\Model\Rezerwacja.vb=1; WypozyczalniaCore\Model\Sprzet.vb=1; WypozyczalniaCore\Model\Taryfa.vb=5; WypozyczalniaCore\Model\Wypozyczenie.vb=2; WypozyczalniaCore\PricingManager.vb=37; WypozyczalniaCore\RentalManager.vb=56; WypozyczalniaCore\ReservationManager.vb=43; WypozyczalniaWeb\Cennik.aspx.vb=10; WypozyczalniaWeb\Faktura.aspx.vb=10; WypozyczalniaWeb\Global.asax.vb=2; WypozyczalniaWeb\Klienci.aspx.vb=6; WypozyczalniaWeb\Rezerwacja.aspx.vb=12; WypozyczalniaWeb\Sprzet.aspx.vb=6; WypozyczalniaWeb\Web References\BramkaPlatnosci\Reference.vb=5; WypozyczalniaWeb\Wypozyczenie.aspx.vb=13; WypozyczalniaWeb\Zwrot.aspx.vb=13 |
| WypozyczalniaCore\Model\Taryfa.vb | 26 | Sub | New | 368 | Jobs\EksportNocny\Program.vb=4; WypozyczalniaCore\CustomerManager.vb=7; WypozyczalniaCore\DAL\DbHelper.vb=20; WypozyczalniaCore\DAL\EquipmentDAL.vb=4; WypozyczalniaCore\DAL\InvoiceDAL.vb=6; WypozyczalniaCore\DAL\PricingDAL.vb=6; WypozyczalniaCore\DAL\RentalDAL.vb=3; WypozyczalniaCore\DAL\ReservationDAL.vb=2; WypozyczalniaCore\EquipmentManager.vb=36; WypozyczalniaCore\Integracje\EksportCsv.vb=7; WypozyczalniaCore\Integracje\ImportCennikaXml.vb=10; WypozyczalniaCore\Integracje\KurierClient.vb=5; WypozyczalniaCore\InvoiceManager.vb=31; WypozyczalniaCore\Model\Faktura.vb=4; WypozyczalniaCore\Model\Klient.vb=1; WypozyczalniaCore\Model\Rezerwacja.vb=1; WypozyczalniaCore\Model\Sprzet.vb=1; WypozyczalniaCore\Model\Taryfa.vb=5; WypozyczalniaCore\Model\Wypozyczenie.vb=2; WypozyczalniaCore\PricingManager.vb=37; WypozyczalniaCore\RentalManager.vb=56; WypozyczalniaCore\ReservationManager.vb=43; WypozyczalniaWeb\Cennik.aspx.vb=10; WypozyczalniaWeb\Faktura.aspx.vb=10; WypozyczalniaWeb\Global.asax.vb=2; WypozyczalniaWeb\Klienci.aspx.vb=6; WypozyczalniaWeb\Rezerwacja.aspx.vb=12; WypozyczalniaWeb\Sprzet.aspx.vb=6; WypozyczalniaWeb\Web References\BramkaPlatnosci\Reference.vb=5; WypozyczalniaWeb\Wypozyczenie.aspx.vb=13; WypozyczalniaWeb\Zwrot.aspx.vb=13 |
| WypozyczalniaCore\Model\Taryfa.vb | 33 | Function | CzyObowiazujeNa | 1 | WypozyczalniaCore\Model\Taryfa.vb=1 |
| WypozyczalniaCore\Model\Taryfa.vb | 37 | Function | CzyZnanaKategoria | 3 | WypozyczalniaCore\Integracje\ImportCennikaXml.vb=1; WypozyczalniaCore\Model\Taryfa.vb=1; WypozyczalniaCore\PricingManager.vb=1 |
| WypozyczalniaCore\Model\Taryfa.vb | 42 | Function | NazwaKategorii | 4 | WypozyczalniaCore\Model\Taryfa.vb=2; WypozyczalniaCore\PricingManager.vb=2 |
| WypozyczalniaCore\Model\Taryfa.vb | 52 | Function | Kopia | 1 | WypozyczalniaCore\Model\Taryfa.vb=1 |
| WypozyczalniaCore\Model\Taryfa.vb | 58 | Function | Domyslna | 1 | WypozyczalniaCore\Model\Taryfa.vb=1 |
| WypozyczalniaCore\Model\Taryfa.vb | 63 | Function | TakaSama | 1 | WypozyczalniaCore\Model\Taryfa.vb=1 |
| WypozyczalniaCore\Model\Taryfa.vb | 69 | Function | ToString | 121 | Jobs\EksportNocny\Program.vb=6; WypozyczalniaCore\CustomerManager.vb=10; WypozyczalniaCore\DAL\InvoiceDAL.vb=1; WypozyczalniaCore\DAL\PricingDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=2; WypozyczalniaCore\Integracje\EksportCsv.vb=13; WypozyczalniaCore\Integracje\ImportCennikaXml.vb=1; WypozyczalniaCore\Integracje\KurierClient.vb=6; WypozyczalniaCore\InvoiceManager.vb=1; WypozyczalniaCore\Model\Faktura.vb=5; WypozyczalniaCore\Model\Klient.vb=2; WypozyczalniaCore\Model\Rezerwacja.vb=3; WypozyczalniaCore\Model\Sprzet.vb=1; WypozyczalniaCore\Model\Taryfa.vb=4; WypozyczalniaCore\Model\Wypozyczenie.vb=3; WypozyczalniaCore\PricingManager.vb=9; WypozyczalniaCore\RentalManager.vb=4; WypozyczalniaCore\ReservationManager.vb=4; WypozyczalniaWeb\Cennik.aspx.vb=9; WypozyczalniaWeb\Faktura.aspx.vb=3; WypozyczalniaWeb\Global.asax.vb=2; WypozyczalniaWeb\Klienci.aspx.vb=1; WypozyczalniaWeb\Rezerwacja.aspx.vb=10; WypozyczalniaWeb\Sprzet.aspx.vb=4; WypozyczalniaWeb\Wypozyczenie.aspx.vb=8; WypozyczalniaWeb\Zwrot.aspx.vb=8 |
| WypozyczalniaCore\Model\Wypozyczenie.vb | 26 | Sub | New | 368 | Jobs\EksportNocny\Program.vb=4; WypozyczalniaCore\CustomerManager.vb=7; WypozyczalniaCore\DAL\DbHelper.vb=20; WypozyczalniaCore\DAL\EquipmentDAL.vb=4; WypozyczalniaCore\DAL\InvoiceDAL.vb=6; WypozyczalniaCore\DAL\PricingDAL.vb=6; WypozyczalniaCore\DAL\RentalDAL.vb=3; WypozyczalniaCore\DAL\ReservationDAL.vb=2; WypozyczalniaCore\EquipmentManager.vb=36; WypozyczalniaCore\Integracje\EksportCsv.vb=7; WypozyczalniaCore\Integracje\ImportCennikaXml.vb=10; WypozyczalniaCore\Integracje\KurierClient.vb=5; WypozyczalniaCore\InvoiceManager.vb=31; WypozyczalniaCore\Model\Faktura.vb=4; WypozyczalniaCore\Model\Klient.vb=1; WypozyczalniaCore\Model\Rezerwacja.vb=1; WypozyczalniaCore\Model\Sprzet.vb=1; WypozyczalniaCore\Model\Taryfa.vb=5; WypozyczalniaCore\Model\Wypozyczenie.vb=2; WypozyczalniaCore\PricingManager.vb=37; WypozyczalniaCore\RentalManager.vb=56; WypozyczalniaCore\ReservationManager.vb=43; WypozyczalniaWeb\Cennik.aspx.vb=10; WypozyczalniaWeb\Faktura.aspx.vb=10; WypozyczalniaWeb\Global.asax.vb=2; WypozyczalniaWeb\Klienci.aspx.vb=6; WypozyczalniaWeb\Rezerwacja.aspx.vb=12; WypozyczalniaWeb\Sprzet.aspx.vb=6; WypozyczalniaWeb\Web References\BramkaPlatnosci\Reference.vb=5; WypozyczalniaWeb\Wypozyczenie.aspx.vb=13; WypozyczalniaWeb\Zwrot.aspx.vb=13 |
| WypozyczalniaCore\Model\Wypozyczenie.vb | 50 | Function | CzyPoTerminie | 3 | WypozyczalniaCore\Model\Wypozyczenie.vb=1; WypozyczalniaCore\RentalManager.vb=1; WypozyczalniaWeb\Wypozyczenie.aspx.vb=1 |
| WypozyczalniaCore\Model\Wypozyczenie.vb | 55 | Function | ZWiersza | 11 | WypozyczalniaCore\DAL\RentalDAL.vb=2; WypozyczalniaCore\InvoiceManager.vb=4; WypozyczalniaCore\Model\Faktura.vb=3; WypozyczalniaCore\Model\Wypozyczenie.vb=2 |
| WypozyczalniaCore\Model\Wypozyczenie.vb | 77 | Function | ToString | 121 | Jobs\EksportNocny\Program.vb=6; WypozyczalniaCore\CustomerManager.vb=10; WypozyczalniaCore\DAL\InvoiceDAL.vb=1; WypozyczalniaCore\DAL\PricingDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=2; WypozyczalniaCore\Integracje\EksportCsv.vb=13; WypozyczalniaCore\Integracje\ImportCennikaXml.vb=1; WypozyczalniaCore\Integracje\KurierClient.vb=6; WypozyczalniaCore\InvoiceManager.vb=1; WypozyczalniaCore\Model\Faktura.vb=5; WypozyczalniaCore\Model\Klient.vb=2; WypozyczalniaCore\Model\Rezerwacja.vb=3; WypozyczalniaCore\Model\Sprzet.vb=1; WypozyczalniaCore\Model\Taryfa.vb=4; WypozyczalniaCore\Model\Wypozyczenie.vb=3; WypozyczalniaCore\PricingManager.vb=9; WypozyczalniaCore\RentalManager.vb=4; WypozyczalniaCore\ReservationManager.vb=4; WypozyczalniaWeb\Cennik.aspx.vb=9; WypozyczalniaWeb\Faktura.aspx.vb=3; WypozyczalniaWeb\Global.asax.vb=2; WypozyczalniaWeb\Klienci.aspx.vb=1; WypozyczalniaWeb\Rezerwacja.aspx.vb=10; WypozyczalniaWeb\Sprzet.aspx.vb=4; WypozyczalniaWeb\Wypozyczenie.aspx.vb=8; WypozyczalniaWeb\Zwrot.aspx.vb=8 |
| WypozyczalniaCore\PricingManager.vb | 22 | Function | LiczbaDob | 8 | WypozyczalniaCore\PricingManager.vb=7; WypozyczalniaWeb\Cennik.aspx.vb=1 |
| WypozyczalniaCore\PricingManager.vb | 31 | Function | ObliczOplate | 12 | WypozyczalniaCore\PricingManager.vb=6; WypozyczalniaCore\RentalManager.vb=2; WypozyczalniaCore\ReservationManager.vb=3; WypozyczalniaWeb\Cennik.aspx.vb=1 |
| WypozyczalniaCore\PricingManager.vb | 58 | Function | RabatProgu | 3 | WypozyczalniaCore\PricingManager.vb=3 |
| WypozyczalniaCore\PricingManager.vb | 62 | Function | ObliczOplateBezKlienta | 1 | WypozyczalniaCore\PricingManager.vb=1 |
| WypozyczalniaCore\PricingManager.vb | 66 | Function | LiczbaDobWeekendowych | 2 | WypozyczalniaCore\PricingManager.vb=2 |
| WypozyczalniaCore\PricingManager.vb | 75 | Function | SzczegolyOplaty | 3 | WypozyczalniaCore\PricingManager.vb=2; WypozyczalniaWeb\Cennik.aspx.vb=1 |
| WypozyczalniaCore\PricingManager.vb | 99 | Function | OpisRabatu | 2 | WypozyczalniaCore\PricingManager.vb=2 |
| WypozyczalniaCore\PricingManager.vb | 104 | Function | PobierzTaryfeDlaSprzetu | 2 | WypozyczalniaCore\PricingManager.vb=2 |
| WypozyczalniaCore\PricingManager.vb | 110 | Function | ListaTaryf | 3 | WypozyczalniaCore\DAL\PricingDAL.vb=1; WypozyczalniaCore\PricingManager.vb=2 |
| WypozyczalniaCore\PricingManager.vb | 114 | Sub | ZapiszTaryfe | 4 | WypozyczalniaCore\DAL\PricingDAL.vb=1; WypozyczalniaCore\Integracje\ImportCennikaXml.vb=1; WypozyczalniaCore\PricingManager.vb=2 |
| WypozyczalniaCore\PricingManager.vb | 131 | Sub | ZmienStawke | 4 | WypozyczalniaCore\DAL\EquipmentDAL.vb=1; WypozyczalniaCore\EquipmentManager.vb=2; WypozyczalniaCore\PricingManager.vb=1 |
| WypozyczalniaCore\PricingManager.vb | 145 | Function | KaucjaDlaSprzetu | 1 | WypozyczalniaCore\PricingManager.vb=1 |
| WypozyczalniaCore\PricingManager.vb | 151 | Function | PrzeliczRezerwacjeNowe | 1 | WypozyczalniaCore\PricingManager.vb=1 |
| WypozyczalniaCore\PricingManager.vb | 172 | Function | CennikKategorii | 1 | WypozyczalniaCore\PricingManager.vb=1 |
| WypozyczalniaCore\PricingManager.vb | 192 | Function | SprawdzTerminy | 1 | WypozyczalniaCore\PricingManager.vb=1 |
| WypozyczalniaCore\PricingManager.vb | 199 | Function | SymulujOplate | 2 | WypozyczalniaCore\PricingManager.vb=2 |
| WypozyczalniaCore\PricingManager.vb | 204 | Function | TabelaOplat | 2 | WypozyczalniaCore\PricingManager.vb=2 |
| WypozyczalniaCore\PricingManager.vb | 224 | Function | TaryfyNaDzien | 4 | WypozyczalniaCore\DAL\PricingDAL.vb=1; WypozyczalniaCore\PricingManager.vb=3 |
| WypozyczalniaCore\PricingManager.vb | 228 | Function | HistoriaTaryf | 1 | WypozyczalniaCore\PricingManager.vb=1 |
| WypozyczalniaCore\PricingManager.vb | 245 | Function | ZmienKaucje | 1 | WypozyczalniaCore\PricingManager.vb=1 |
| WypozyczalniaCore\PricingManager.vb | 257 | Function | BrakujaceTaryfy | 1 | WypozyczalniaCore\PricingManager.vb=1 |
| WypozyczalniaCore\PricingManager.vb | 271 | Function | ZestawienieKategorii | 1 | WypozyczalniaCore\PricingManager.vb=1 |
| WypozyczalniaCore\PricingManager.vb | 291 | Function | CzyCenaAktualna | 1 | WypozyczalniaCore\PricingManager.vb=1 |
| WypozyczalniaCore\RentalManager.vb | 15 | Function | PobierzWypozyczenie | 17 | WypozyczalniaCore\DAL\RentalDAL.vb=1; WypozyczalniaCore\InvoiceManager.vb=3; WypozyczalniaCore\RentalManager.vb=8; WypozyczalniaWeb\Faktura.aspx.vb=1; WypozyczalniaWeb\Wypozyczenie.aspx.vb=1; WypozyczalniaWeb\Zwrot.aspx.vb=3 |
| WypozyczalniaCore\RentalManager.vb | 20 | Function | WydajSprzet | 3 | WypozyczalniaCore\RentalManager.vb=2; WypozyczalniaWeb\Wypozyczenie.aspx.vb=1 |
| WypozyczalniaCore\RentalManager.vb | 90 | Function | ZarejestrujZwrot | 2 | WypozyczalniaCore\RentalManager.vb=1; WypozyczalniaWeb\Zwrot.aspx.vb=1 |
| WypozyczalniaCore\RentalManager.vb | 137 | Function | ObliczKare | 6 | WypozyczalniaCore\RentalManager.vb=5; WypozyczalniaWeb\Zwrot.aspx.vb=1 |
| WypozyczalniaCore\RentalManager.vb | 145 | Function | Otwarte | 9 | WypozyczalniaCore\DAL\RentalDAL.vb=1; WypozyczalniaCore\RentalManager.vb=2; WypozyczalniaWeb\Wypozyczenie.aspx.vb=5; WypozyczalniaWeb\Zwrot.aspx.vb=1 |
| WypozyczalniaCore\RentalManager.vb | 149 | Function | WypozyczeniaZDnia | 7 | WypozyczalniaCore\DAL\RentalDAL.vb=2; WypozyczalniaCore\Integracje\EksportCsv.vb=1; WypozyczalniaCore\RentalManager.vb=3; WypozyczalniaWeb\Wypozyczenie.aspx.vb=1 |
| WypozyczalniaCore\RentalManager.vb | 153 | Function | WypozyczeniaKlienta | 3 | WypozyczalniaCore\DAL\RentalDAL.vb=1; WypozyczalniaCore\RentalManager.vb=2 |
| WypozyczalniaCore\RentalManager.vb | 157 | Function | OtwarteKlienta | 5 | WypozyczalniaCore\DAL\RentalDAL.vb=1; WypozyczalniaCore\RentalManager.vb=3; WypozyczalniaWeb\Wypozyczenie.aspx.vb=1 |
| WypozyczalniaCore\RentalManager.vb | 161 | Function | CzyKlientMaZaleglosci | 1 | WypozyczalniaCore\RentalManager.vb=1 |
| WypozyczalniaCore\RentalManager.vb | 172 | Function | HistoriaSprzetu | 1 | WypozyczalniaCore\RentalManager.vb=1 |
| WypozyczalniaCore\RentalManager.vb | 178 | Function | SzacowanaKara | 4 | WypozyczalniaCore\RentalManager.vb=4 |
| WypozyczalniaCore\RentalManager.vb | 186 | Function | PoTerminie | 8 | WypozyczalniaCore\DAL\RentalDAL.vb=1; WypozyczalniaCore\RentalManager.vb=2; WypozyczalniaWeb\Wypozyczenie.aspx.vb=5 |
| WypozyczalniaCore\RentalManager.vb | 201 | Function | StatusDostawy | 1 | WypozyczalniaCore\RentalManager.vb=1 |
| WypozyczalniaCore\RentalManager.vb | 213 | Function | PonowZamowienieKuriera | 1 | WypozyczalniaCore\RentalManager.vb=1 |
| WypozyczalniaCore\RentalManager.vb | 236 | Function | CzySprzetWydany | 1 | WypozyczalniaCore\RentalManager.vb=1 |
| WypozyczalniaCore\RentalManager.vb | 240 | Function | WydajZRezerwacji | 1 | WypozyczalniaCore\RentalManager.vb=1 |
| WypozyczalniaCore\RentalManager.vb | 248 | Sub | PrzedluzWypozyczenie | 1 | WypozyczalniaCore\RentalManager.vb=1 |
| WypozyczalniaCore\RentalManager.vb | 271 | Function | DoZwrotuWCiagu | 2 | WypozyczalniaCore\RentalManager.vb=2 |
| WypozyczalniaCore\RentalManager.vb | 276 | Function | PodsumowanieDnia | 1 | WypozyczalniaCore\RentalManager.vb=1 |
| WypozyczalniaCore\RentalManager.vb | 295 | Function | Przypomnienia | 1 | WypozyczalniaCore\RentalManager.vb=1 |
| WypozyczalniaCore\RentalManager.vb | 306 | Function | ZamknijBezZwrotu | 1 | WypozyczalniaCore\RentalManager.vb=1 |
| WypozyczalniaCore\ReservationManager.vb | 19 | Function | UtworzRezerwacje | 2 | WypozyczalniaCore\ReservationManager.vb=1; WypozyczalniaWeb\Rezerwacja.aspx.vb=1 |
| WypozyczalniaCore\ReservationManager.vb | 57 | Function | PobierzRezerwacje | 13 | WypozyczalniaCore\RentalManager.vb=2; WypozyczalniaCore\ReservationManager.vb=7; WypozyczalniaWeb\Rezerwacja.aspx.vb=2; WypozyczalniaWeb\Wypozyczenie.aspx.vb=2 |
| WypozyczalniaCore\ReservationManager.vb | 67 | Function | RezerwacjeKlienta | 6 | WypozyczalniaCore\DAL\ReservationDAL.vb=1; WypozyczalniaCore\ReservationManager.vb=3; WypozyczalniaWeb\Rezerwacja.aspx.vb=1; WypozyczalniaWeb\Wypozyczenie.aspx.vb=1 |
| WypozyczalniaCore\ReservationManager.vb | 72 | Function | AktywneRezerwacjeKlienta | 1 | WypozyczalniaCore\ReservationManager.vb=1 |
| WypozyczalniaCore\ReservationManager.vb | 92 | Function | RezerwacjeSprzetu | 3 | WypozyczalniaCore\DAL\ReservationDAL.vb=1; WypozyczalniaCore\ReservationManager.vb=2 |
| WypozyczalniaCore\ReservationManager.vb | 96 | Function | RezerwacjeWOkresie | 3 | WypozyczalniaCore\DAL\ReservationDAL.vb=1; WypozyczalniaCore\ReservationManager.vb=2 |
| WypozyczalniaCore\ReservationManager.vb | 103 | Sub | Potwierdz | 2 | WypozyczalniaCore\ReservationManager.vb=2 |
| WypozyczalniaCore\ReservationManager.vb | 116 | Sub | Anuluj | 2 | WypozyczalniaCore\ReservationManager.vb=1; WypozyczalniaWeb\Rezerwacja.aspx.vb=1 |
| WypozyczalniaCore\ReservationManager.vb | 134 | Sub | OznaczZrealizowana | 2 | WypozyczalniaCore\RentalManager.vb=1; WypozyczalniaCore\ReservationManager.vb=1 |
| WypozyczalniaCore\ReservationManager.vb | 141 | Function | AnulujPrzeterminowane | 1 | WypozyczalniaCore\ReservationManager.vb=1 |
| WypozyczalniaCore\ReservationManager.vb | 160 | Sub | ZmienTermin | 4 | WypozyczalniaCore\DAL\ReservationDAL.vb=1; WypozyczalniaCore\ReservationManager.vb=3 |
| WypozyczalniaCore\ReservationManager.vb | 184 | Function | PrzeliczCene | 1 | WypozyczalniaCore\ReservationManager.vb=1 |
| WypozyczalniaCore\ReservationManager.vb | 199 | Function | RezerwacjeNaDzien | 3 | WypozyczalniaCore\DAL\ReservationDAL.vb=1; WypozyczalniaCore\ReservationManager.vb=2 |
| WypozyczalniaCore\ReservationManager.vb | 204 | Function | Statystyki | 3 | WypozyczalniaCore\DAL\ReservationDAL.vb=1; WypozyczalniaCore\ReservationManager.vb=2 |
| WypozyczalniaCore\ReservationManager.vb | 209 | Function | ZajeteDni | 1 | WypozyczalniaCore\ReservationManager.vb=1 |
| WypozyczalniaCore\ReservationManager.vb | 227 | Function | DoPotwierdzenia | 4 | WypozyczalniaCore\DAL\ReservationDAL.vb=1; WypozyczalniaCore\ReservationManager.vb=3 |
| WypozyczalniaCore\ReservationManager.vb | 231 | Function | PotwierdzWszystkie | 1 | WypozyczalniaCore\ReservationManager.vb=1 |
| WypozyczalniaCore\ReservationManager.vb | 245 | Sub | ZmienUwagi | 1 | WypozyczalniaCore\ReservationManager.vb=1 |
| WypozyczalniaCore\ReservationManager.vb | 254 | Function | CzyKlientMaAktywne | 1 | WypozyczalniaCore\ReservationManager.vb=1 |
| WypozyczalniaCore\ReservationManager.vb | 258 | Function | OpisRezerwacji | 2 | WypozyczalniaCore\ReservationManager.vb=1; WypozyczalniaWeb\Rezerwacja.aspx.vb=1 |
| WypozyczalniaWeb\Web References\BramkaPlatnosci\Reference.vb | 38 | Sub | New | 368 | Jobs\EksportNocny\Program.vb=4; WypozyczalniaCore\CustomerManager.vb=7; WypozyczalniaCore\DAL\DbHelper.vb=20; WypozyczalniaCore\DAL\EquipmentDAL.vb=4; WypozyczalniaCore\DAL\InvoiceDAL.vb=6; WypozyczalniaCore\DAL\PricingDAL.vb=6; WypozyczalniaCore\DAL\RentalDAL.vb=3; WypozyczalniaCore\DAL\ReservationDAL.vb=2; WypozyczalniaCore\EquipmentManager.vb=36; WypozyczalniaCore\Integracje\EksportCsv.vb=7; WypozyczalniaCore\Integracje\ImportCennikaXml.vb=10; WypozyczalniaCore\Integracje\KurierClient.vb=5; WypozyczalniaCore\InvoiceManager.vb=31; WypozyczalniaCore\Model\Faktura.vb=4; WypozyczalniaCore\Model\Klient.vb=1; WypozyczalniaCore\Model\Rezerwacja.vb=1; WypozyczalniaCore\Model\Sprzet.vb=1; WypozyczalniaCore\Model\Taryfa.vb=5; WypozyczalniaCore\Model\Wypozyczenie.vb=2; WypozyczalniaCore\PricingManager.vb=37; WypozyczalniaCore\RentalManager.vb=56; WypozyczalniaCore\ReservationManager.vb=43; WypozyczalniaWeb\Cennik.aspx.vb=10; WypozyczalniaWeb\Faktura.aspx.vb=10; WypozyczalniaWeb\Global.asax.vb=2; WypozyczalniaWeb\Klienci.aspx.vb=6; WypozyczalniaWeb\Rezerwacja.aspx.vb=12; WypozyczalniaWeb\Sprzet.aspx.vb=6; WypozyczalniaWeb\Web References\BramkaPlatnosci\Reference.vb=5; WypozyczalniaWeb\Wypozyczenie.aspx.vb=13; WypozyczalniaWeb\Zwrot.aspx.vb=13 |
| WypozyczalniaWeb\Web References\BramkaPlatnosci\Reference.vb | 76 | Function | ZarejestrujPlatnosc | 4 | WypozyczalniaWeb\Faktura.aspx.vb=1; WypozyczalniaWeb\Web References\BramkaPlatnosci\Reference.vb=3 |
| WypozyczalniaWeb\Web References\BramkaPlatnosci\Reference.vb | 83 | Function | StatusPlatnosci | 3 | WypozyczalniaWeb\Web References\BramkaPlatnosci\Reference.vb=3 |
| WypozyczalniaWeb\Web References\BramkaPlatnosci\Reference.vb | 89 | Sub | CancelAsync | 2 | WypozyczalniaWeb\Web References\BramkaPlatnosci\Reference.vb=2 |

## M11 — surowy wynik H1

`git --no-pager -C 'legacy-app-parent\legacy-app\WypozyczalniaWeb' grep -n -I -e '^' -- '*.aspx' '*.aspx.vb'`

```text
Cennik.aspx:1:<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Cennik.aspx.vb" Inherits="WypozyczalniaWeb.Cennik" %>
Cennik.aspx:2:<!DOCTYPE html>
Cennik.aspx:3:<html xmlns="http://www.w3.org/1999/xhtml">
Cennik.aspx:4:<head runat="server">
Cennik.aspx:5:    <title>Wypożyczalnia - Cennik i taryfy</title>
Cennik.aspx:6:</head>
Cennik.aspx:7:<body>
Cennik.aspx:8:    <form id="form1" runat="server">
Cennik.aspx:9:        <div class="menu">
Cennik.aspx:10:            <a href="Klienci.aspx">Klienci</a> |
Cennik.aspx:11:            <a href="Sprzet.aspx">Sprzęt</a> |
Cennik.aspx:12:            <a href="Rezerwacja.aspx">Rezerwacje</a> |
Cennik.aspx:13:            <a href="Wypozyczenie.aspx">Wydanie</a> |
Cennik.aspx:14:            <a href="Zwrot.aspx">Zwrot</a> |
Cennik.aspx:15:            <a href="Cennik.aspx">Cennik</a> |
Cennik.aspx:16:            <a href="Faktura.aspx">Faktury</a>
Cennik.aspx:17:        </div>
Cennik.aspx:18:        <h2>Cennik i taryfy</h2>
Cennik.aspx:19:        <asp:Label ID="lblWynik" runat="server" ForeColor="DarkRed" EnableViewState="false" />
Cennik.aspx:20:
Cennik.aspx:21:        <h3>Taryfy kategorii</h3>
Cennik.aspx:22:        <asp:GridView ID="gvTaryfy" runat="server" AutoGenerateColumns="false" CellPadding="3" GridLines="Horizontal">
Cennik.aspx:23:            <Columns>
Cennik.aspx:24:                <asp:BoundField DataField="Id" HeaderText="Id" />
Cennik.aspx:25:                <asp:BoundField DataField="Kategoria" HeaderText="Kategoria" />
Cennik.aspx:26:                <asp:BoundField DataField="MnoznikWeekend" HeaderText="Mnożnik weekend" DataFormatString="{0:N2}" />
Cennik.aspx:27:                <asp:BoundField DataField="RabatTygodniowyProc" HeaderText="Rabat tygodniowy %" DataFormatString="{0:N2}" />
Cennik.aspx:28:                <asp:BoundField DataField="ObowiazujeOd" HeaderText="Obowiązuje od" DataFormatString="{0:yyyy-MM-dd}" />
Cennik.aspx:29:            </Columns>
Cennik.aspx:30:            <EmptyDataTemplate>Brak zdefiniowanych taryf.</EmptyDataTemplate>
Cennik.aspx:31:        </asp:GridView>
Cennik.aspx:32:
Cennik.aspx:33:        <h3>Stawka dobowa sprzętu</h3>
Cennik.aspx:34:        <table>
Cennik.aspx:35:            <tr>
Cennik.aspx:36:                <td>Sprzęt:</td>
Cennik.aspx:37:                <td><asp:DropDownList ID="ddlSprzet" runat="server" AutoPostBack="true" Width="320px" /></td>
Cennik.aspx:38:            </tr>
Cennik.aspx:39:            <tr>
Cennik.aspx:40:                <td>Nowa stawka (zł):</td>
Cennik.aspx:41:                <td>
Cennik.aspx:42:                    <asp:TextBox ID="txtStawka" runat="server" Width="100px" />
Cennik.aspx:43:                    <asp:Button ID="btnZapiszStawke" runat="server" Text="Zapisz stawkę" />
Cennik.aspx:44:                </td>
Cennik.aspx:45:            </tr>
Cennik.aspx:46:        </table>
Cennik.aspx:47:
Cennik.aspx:48:        <h3>Import cennika z katalogu</h3>
Cennik.aspx:49:        <p>Pliki XML z katalogu importu zostaną wczytane i przeniesione do podkatalogu „przetworzone”.</p>
Cennik.aspx:50:        <asp:Button ID="btnImportuj" runat="server" Text="Importuj cennik" OnClientClick="return confirm('Zaimportować pliki cennika?');" />
Cennik.aspx:51:
Cennik.aspx:52:        <h3>Kalkulator opłaty</h3>
Cennik.aspx:53:        <asp:Label ID="lblKlient" runat="server" Text="Klient: (nie wybrano)" /><br />
Cennik.aspx:54:        <table>
Cennik.aspx:55:            <tr>
Cennik.aspx:56:                <td>Sprzęt:</td>
Cennik.aspx:57:                <td><asp:DropDownList ID="ddlSprzetKalk" runat="server" Width="320px" /></td>
Cennik.aspx:58:            </tr>
Cennik.aspx:59:            <tr>
Cennik.aspx:60:                <td>Od (rrrr-mm-dd gg:mm):</td>
Cennik.aspx:61:                <td><asp:TextBox ID="txtOd" runat="server" Width="140px" /></td>
Cennik.aspx:62:            </tr>
Cennik.aspx:63:            <tr>
Cennik.aspx:64:                <td>Do (rrrr-mm-dd gg:mm):</td>
Cennik.aspx:65:                <td><asp:TextBox ID="txtDo" runat="server" Width="140px" /></td>
Cennik.aspx:66:            </tr>
Cennik.aspx:67:            <tr>
Cennik.aspx:68:                <td></td>
Cennik.aspx:69:                <td>
Cennik.aspx:70:                    <asp:Button ID="btnOblicz" runat="server" Text="Oblicz" />
Cennik.aspx:71:                    <asp:Label ID="lblKwota" runat="server" Font-Bold="true" />
Cennik.aspx:72:                </td>
Cennik.aspx:73:            </tr>
Cennik.aspx:74:        </table>
Cennik.aspx:75:        <asp:Label ID="lblSzczegoly" runat="server" Font-Size="Small" />
Cennik.aspx:76:    </form>
Cennik.aspx:77:</body>
Cennik.aspx:78:</html>
Cennik.aspx.vb:1:' Modul: Cennik.aspx - taryfy, stawki, import cennika, kalkulator | Autor: zespol IT | 2014-03-11
Cennik.aspx.vb:2:' Historia:
Cennik.aspx.vb:3:'   2015-02-04 MW: lista taryf z data obowiazywania
Cennik.aspx.vb:4:'   2017-06-12 JK: przycisk importu cennika XML
Cennik.aspx.vb:5:'   2019-04-02 PT: kalkulator oplaty dla wybranego klienta
Cennik.aspx.vb:6:
Cennik.aspx.vb:7:Imports System.Data
Cennik.aspx.vb:8:Imports System.Configuration
Cennik.aspx.vb:9:Imports System.Globalization
Cennik.aspx.vb:10:Imports Wypozyczalnia.Core
Cennik.aspx.vb:11:Imports Wypozyczalnia.Core.Model
Cennik.aspx.vb:12:Imports Wypozyczalnia.Core.Integracje
Cennik.aspx.vb:13:
Cennik.aspx.vb:14:Partial Class Cennik
Cennik.aspx.vb:15:    Inherits System.Web.UI.Page
Cennik.aspx.vb:16:
Cennik.aspx.vb:17:    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Cennik))
Cennik.aspx.vb:18:
Cennik.aspx.vb:19:    Protected WithEvents lblWynik As Global.System.Web.UI.WebControls.Label
Cennik.aspx.vb:20:    Protected WithEvents gvTaryfy As Global.System.Web.UI.WebControls.GridView
Cennik.aspx.vb:21:    Protected WithEvents ddlSprzet As Global.System.Web.UI.WebControls.DropDownList
Cennik.aspx.vb:22:    Protected WithEvents txtStawka As Global.System.Web.UI.WebControls.TextBox
Cennik.aspx.vb:23:    Protected WithEvents btnZapiszStawke As Global.System.Web.UI.WebControls.Button
Cennik.aspx.vb:24:    Protected WithEvents btnImportuj As Global.System.Web.UI.WebControls.Button
Cennik.aspx.vb:25:    Protected WithEvents lblKlient As Global.System.Web.UI.WebControls.Label
Cennik.aspx.vb:26:    Protected WithEvents ddlSprzetKalk As Global.System.Web.UI.WebControls.DropDownList
Cennik.aspx.vb:27:    Protected WithEvents txtOd As Global.System.Web.UI.WebControls.TextBox
Cennik.aspx.vb:28:    Protected WithEvents txtDo As Global.System.Web.UI.WebControls.TextBox
Cennik.aspx.vb:29:    Protected WithEvents btnOblicz As Global.System.Web.UI.WebControls.Button
Cennik.aspx.vb:30:    Protected WithEvents lblKwota As Global.System.Web.UI.WebControls.Label
Cennik.aspx.vb:31:    Protected WithEvents lblSzczegoly As Global.System.Web.UI.WebControls.Label
Cennik.aspx.vb:32:
Cennik.aspx.vb:33:    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
Cennik.aspx.vb:34:        If Not IsPostBack Then
Cennik.aspx.vb:35:            ZaladujTaryfy()
Cennik.aspx.vb:36:            ZaladujSprzet()
Cennik.aspx.vb:37:            PokazAktualnaStawke()
Cennik.aspx.vb:38:            txtOd.Text = Date.Today.AddDays(1).ToString("yyyy-MM-dd") & " 08:00"
Cennik.aspx.vb:39:            txtDo.Text = Date.Today.AddDays(2).ToString("yyyy-MM-dd") & " 08:00"
Cennik.aspx.vb:40:        End If
Cennik.aspx.vb:41:        PokazKlienta()
Cennik.aspx.vb:42:    End Sub
Cennik.aspx.vb:43:
Cennik.aspx.vb:44:    Protected Sub ddlSprzet_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlSprzet.SelectedIndexChanged
Cennik.aspx.vb:45:        PokazAktualnaStawke()
Cennik.aspx.vb:46:    End Sub
Cennik.aspx.vb:47:
Cennik.aspx.vb:48:    Protected Sub btnZapiszStawke_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZapiszStawke.Click
Cennik.aspx.vb:49:        If txtStawka.Text.Trim() = "" Then
Cennik.aspx.vb:50:            lblWynik.Text = "Podaj nową stawkę."
Cennik.aspx.vb:51:            Return
Cennik.aspx.vb:52:        End If
Cennik.aspx.vb:53:        If ddlSprzet.SelectedValue = "" Then
Cennik.aspx.vb:54:            lblWynik.Text = "Wybierz sprzęt."
Cennik.aspx.vb:55:            Return
Cennik.aspx.vb:56:        End If
Cennik.aspx.vb:57:        Try
Cennik.aspx.vb:58:            Dim sql As String = "UPDATE Sprzet SET StawkaDobowa = " & txtStawka.Text.Replace(",", ".") & " WHERE Id = " & ddlSprzet.SelectedValue
Cennik.aspx.vb:59:            Dim ile As Integer = DbHelper.WykonajPolecenie(sql)
Cennik.aspx.vb:60:            If ile = 1 Then
Cennik.aspx.vb:61:                lblWynik.Text = "Zapisano stawkę " & txtStawka.Text & " zł dla " & ddlSprzet.SelectedItem.Text & "."
Cennik.aspx.vb:62:                Log.Info("Zmiana stawki z ekranu cennika: " & ddlSprzet.SelectedValue & " = " & txtStawka.Text)
Cennik.aspx.vb:63:            Else
Cennik.aspx.vb:64:                lblWynik.Text = "Nie znaleziono sprzętu."
Cennik.aspx.vb:65:            End If
Cennik.aspx.vb:66:            txtStawka.Text = ""
Cennik.aspx.vb:67:            ZaladujSprzet()
Cennik.aspx.vb:68:            PokazAktualnaStawke()
Cennik.aspx.vb:69:        Catch ex As Exception
Cennik.aspx.vb:70:            Log.Error("Blad zapisu stawki", ex)
Cennik.aspx.vb:71:            lblWynik.Text = "Błąd zapisu stawki: " & ex.Message
Cennik.aspx.vb:72:        End Try
Cennik.aspx.vb:73:    End Sub
Cennik.aspx.vb:74:
Cennik.aspx.vb:75:    Protected Sub btnImportuj_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnImportuj.Click
Cennik.aspx.vb:76:        Dim katalog As String = ConfigurationManager.AppSettings("ImportCennikaKatalog")
Cennik.aspx.vb:77:        Dim import As New ImportCennikaXml()
Cennik.aspx.vb:78:        Try
Cennik.aspx.vb:79:            Dim oczekujace As String() = import.PlikiOczekujace(katalog)
Cennik.aspx.vb:80:            If oczekujace.Length = 0 Then
Cennik.aspx.vb:81:                lblWynik.Text = "Brak plików do importu w katalogu " & katalog & "."
Cennik.aspx.vb:82:                Return
Cennik.aspx.vb:83:            End If
Cennik.aspx.vb:84:            Dim ile As Integer = import.ImportujKatalog(katalog)
Cennik.aspx.vb:85:            lblWynik.Text = "Zaimportowano plików: " & ile & " (" & String.Join(", ", oczekujace) & ")."
Cennik.aspx.vb:86:            ZaladujTaryfy()
Cennik.aspx.vb:87:            ZaladujSprzet()
Cennik.aspx.vb:88:            PokazAktualnaStawke()
Cennik.aspx.vb:89:        Catch ex As System.IO.IOException
Cennik.aspx.vb:90:            Log.Error("Import cennika - blad pliku", ex)
Cennik.aspx.vb:91:            lblWynik.Text = "Błąd dostępu do katalogu importu: " & ex.Message
Cennik.aspx.vb:92:        Catch ex As System.Xml.XmlException
Cennik.aspx.vb:93:            Log.Error("Import cennika - niepoprawny XML", ex)
Cennik.aspx.vb:94:            lblWynik.Text = "Niepoprawny plik XML: " & ex.Message
Cennik.aspx.vb:95:        Catch ex As FormatException
Cennik.aspx.vb:96:            Log.Error("Import cennika - niepoprawna liczba", ex)
Cennik.aspx.vb:97:            lblWynik.Text = "Niepoprawna wartość liczbowa w pliku: " & ex.Message
Cennik.aspx.vb:98:        End Try
Cennik.aspx.vb:99:    End Sub
Cennik.aspx.vb:100:
Cennik.aspx.vb:101:    Protected Sub btnOblicz_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOblicz.Click
Cennik.aspx.vb:102:        lblKwota.Text = ""
Cennik.aspx.vb:103:        lblSzczegoly.Text = ""
Cennik.aspx.vb:104:        Dim klientId As Integer = Session("WybranyKlientId")
Cennik.aspx.vb:105:        Dim dataOd As Date
Cennik.aspx.vb:106:        Dim dataDo As Date
Cennik.aspx.vb:107:        If Not Date.TryParse(txtOd.Text, dataOd) Then
Cennik.aspx.vb:108:            lblWynik.Text = "Niepoprawna data od."
Cennik.aspx.vb:109:            Return
Cennik.aspx.vb:110:        End If
Cennik.aspx.vb:111:        If Not Date.TryParse(txtDo.Text, dataDo) Then
Cennik.aspx.vb:112:            lblWynik.Text = "Niepoprawna data do."
Cennik.aspx.vb:113:            Return
Cennik.aspx.vb:114:        End If
Cennik.aspx.vb:115:        If ddlSprzetKalk.SelectedValue = "" Then
Cennik.aspx.vb:116:            lblWynik.Text = "Wybierz sprzęt do kalkulacji."
Cennik.aspx.vb:117:            Return
Cennik.aspx.vb:118:        End If
Cennik.aspx.vb:119:        Dim pm As New PricingManager()
Cennik.aspx.vb:120:        Try
Cennik.aspx.vb:121:            Dim kwota As Decimal = pm.ObliczOplate(CInt(ddlSprzetKalk.SelectedValue), dataOd, dataDo, klientId)
Cennik.aspx.vb:122:            lblKwota.Text = kwota.ToString("N2") & " zł (" & pm.LiczbaDob(dataOd, dataDo) & " dób)"
Cennik.aspx.vb:123:            lblSzczegoly.Text = Server.HtmlEncode(pm.SzczegolyOplaty(CInt(ddlSprzetKalk.SelectedValue), dataOd, dataDo, klientId))
Cennik.aspx.vb:124:        Catch ex As ArgumentException
Cennik.aspx.vb:125:            lblWynik.Text = ex.Message
Cennik.aspx.vb:126:        Catch ex As BladBiznesowyException
Cennik.aspx.vb:127:            lblWynik.Text = ex.Message
Cennik.aspx.vb:128:        End Try
Cennik.aspx.vb:129:    End Sub
Cennik.aspx.vb:130:
Cennik.aspx.vb:131:    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PreRender
Cennik.aspx.vb:132:        Dim katalog As String = ConfigurationManager.AppSettings("ImportCennikaKatalog")
Cennik.aspx.vb:133:        If String.IsNullOrEmpty(katalog) Then
Cennik.aspx.vb:134:            btnImportuj.Enabled = False
Cennik.aspx.vb:135:            btnImportuj.ToolTip = "Brak klucza ImportCennikaKatalog w konfiguracji"
Cennik.aspx.vb:136:        Else
Cennik.aspx.vb:137:            btnImportuj.ToolTip = "Katalog: " & katalog
Cennik.aspx.vb:138:        End If
Cennik.aspx.vb:139:    End Sub
Cennik.aspx.vb:140:
Cennik.aspx.vb:141:    Private _aktualneKategorie As New List(Of String)()
Cennik.aspx.vb:142:
Cennik.aspx.vb:143:    Protected Sub gvTaryfy_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvTaryfy.RowDataBound
Cennik.aspx.vb:144:        If e.Row.RowType <> DataControlRowType.DataRow Then Return
Cennik.aspx.vb:145:        Dim wiersz As DataRowView = CType(e.Row.DataItem, DataRowView)
Cennik.aspx.vb:146:        Dim kategoria As String = wiersz("Kategoria").ToString().Trim()
Cennik.aspx.vb:147:        Dim od As Date = wiersz("ObowiazujeOd")
Cennik.aspx.vb:148:        If od > Date.Today Then
Cennik.aspx.vb:149:            e.Row.ForeColor = Drawing.Color.Gray
Cennik.aspx.vb:150:            e.Row.ToolTip = "Taryfa przyszła"
Cennik.aspx.vb:151:            Return
Cennik.aspx.vb:152:        End If
Cennik.aspx.vb:153:        If Not _aktualneKategorie.Contains(kategoria) Then
Cennik.aspx.vb:154:            _aktualneKategorie.Add(kategoria)
Cennik.aspx.vb:155:            e.Row.Font.Bold = True
Cennik.aspx.vb:156:            e.Row.ToolTip = "Taryfa obowiązująca"
Cennik.aspx.vb:157:        Else
Cennik.aspx.vb:158:            e.Row.ForeColor = Drawing.Color.DarkGray
Cennik.aspx.vb:159:            e.Row.ToolTip = "Taryfa archiwalna"
Cennik.aspx.vb:160:        End If
Cennik.aspx.vb:161:    End Sub
Cennik.aspx.vb:162:
Cennik.aspx.vb:163:    Private Sub ZaladujTaryfy()
Cennik.aspx.vb:164:        Dim sql As String = "SELECT Id, Kategoria, MnoznikWeekend, RabatTygodniowyProc, ObowiazujeOd FROM Taryfy ORDER BY Kategoria, ObowiazujeOd DESC"
Cennik.aspx.vb:165:        Try
Cennik.aspx.vb:166:            gvTaryfy.DataSource = DbHelper.WykonajZapytanie(sql)
Cennik.aspx.vb:167:            gvTaryfy.DataBind()
Cennik.aspx.vb:168:        Catch ex As Exception
Cennik.aspx.vb:169:            Log.Error("Blad odczytu taryf", ex)
Cennik.aspx.vb:170:            lblWynik.Text = "Nie udało się wczytać taryf."
Cennik.aspx.vb:171:        End Try
Cennik.aspx.vb:172:    End Sub
Cennik.aspx.vb:173:
Cennik.aspx.vb:174:    Private Sub ZaladujSprzet()
Cennik.aspx.vb:175:        Dim wybrany As String = ddlSprzet.SelectedValue
Cennik.aspx.vb:176:        Dim wybranyKalk As String = ddlSprzetKalk.SelectedValue
Cennik.aspx.vb:177:        Dim lista As List(Of Sprzet) = New EquipmentManager().ListaWKategorii("")
Cennik.aspx.vb:178:
Cennik.aspx.vb:179:        ddlSprzet.Items.Clear()
Cennik.aspx.vb:180:        ddlSprzetKalk.Items.Clear()
Cennik.aspx.vb:181:        ddlSprzet.Items.Add(New ListItem("-- wybierz --", ""))
Cennik.aspx.vb:182:        ddlSprzetKalk.Items.Add(New ListItem("-- wybierz --", ""))
Cennik.aspx.vb:183:        For Each s As Sprzet In lista
Cennik.aspx.vb:184:            Dim tekst As String = s.NumerInwentarzowy & " " & s.Nazwa
Cennik.aspx.vb:185:            ddlSprzet.Items.Add(New ListItem(tekst, s.Id.ToString()))
Cennik.aspx.vb:186:            ddlSprzetKalk.Items.Add(New ListItem(tekst, s.Id.ToString()))
Cennik.aspx.vb:187:        Next
Cennik.aspx.vb:188:        If wybrany <> "" AndAlso ddlSprzet.Items.FindByValue(wybrany) IsNot Nothing Then
Cennik.aspx.vb:189:            ddlSprzet.SelectedValue = wybrany
Cennik.aspx.vb:190:        End If
Cennik.aspx.vb:191:        If wybranyKalk <> "" AndAlso ddlSprzetKalk.Items.FindByValue(wybranyKalk) IsNot Nothing Then
Cennik.aspx.vb:192:            ddlSprzetKalk.SelectedValue = wybranyKalk
Cennik.aspx.vb:193:        End If
Cennik.aspx.vb:194:    End Sub
Cennik.aspx.vb:195:
Cennik.aspx.vb:196:    Private Sub PokazAktualnaStawke()
Cennik.aspx.vb:197:        If ddlSprzet.SelectedValue = "" Then
Cennik.aspx.vb:198:            txtStawka.ToolTip = ""
Cennik.aspx.vb:199:            Return
Cennik.aspx.vb:200:        End If
Cennik.aspx.vb:201:        Dim s As Sprzet = New EquipmentManager().PobierzSprzet(CInt(ddlSprzet.SelectedValue))
Cennik.aspx.vb:202:        If s Is Nothing Then Return
Cennik.aspx.vb:203:        txtStawka.ToolTip = "Aktualnie: " & s.StawkaDobowa.ToString("N2") & " zł, kaucja " & s.Kaucja.ToString("N2") & " zł"
Cennik.aspx.vb:204:        txtStawka.Attributes("placeholder") = s.StawkaDobowa.ToString("N2")
Cennik.aspx.vb:205:    End Sub
Cennik.aspx.vb:206:
Cennik.aspx.vb:207:    Private Sub PokazKlienta()
Cennik.aspx.vb:208:        Dim klientId As Integer = Session("WybranyKlientId")
Cennik.aspx.vb:209:        If klientId = 0 Then
Cennik.aspx.vb:210:            lblKlient.Text = "Klient: (nie wybrano - kalkulacja bez rabatu)"
Cennik.aspx.vb:211:            Return
Cennik.aspx.vb:212:        End If
Cennik.aspx.vb:213:        Dim k As Klient = New CustomerManager().PobierzKlienta(klientId)
Cennik.aspx.vb:214:        If k Is Nothing Then
Cennik.aspx.vb:215:            lblKlient.Text = "Klient: (nie znaleziono)"
Cennik.aspx.vb:216:        ElseIf k.Typ = "F" Then
Cennik.aspx.vb:217:            lblKlient.Text = "Klient: " & Server.HtmlEncode(k.NazwaFirmy) & " (próg rabatu " & k.ProgRabatu & ")"
Cennik.aspx.vb:218:        Else
Cennik.aspx.vb:219:            lblKlient.Text = "Klient: " & Server.HtmlEncode(k.Imie & " " & k.Nazwisko) & " (próg rabatu " & k.ProgRabatu & ")"
Cennik.aspx.vb:220:        End If
Cennik.aspx.vb:221:    End Sub
Cennik.aspx.vb:222:
Cennik.aspx.vb:223:End Class
Faktura.aspx:1:<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Faktura.aspx.vb" Inherits="WypozyczalniaWeb.Faktura" %>
Faktura.aspx:2:<!DOCTYPE html>
Faktura.aspx:3:<html xmlns="http://www.w3.org/1999/xhtml">
Faktura.aspx:4:<head runat="server">
Faktura.aspx:5:    <title>Wypożyczalnia - Faktura</title>
Faktura.aspx:6:</head>
Faktura.aspx:7:<body>
Faktura.aspx:8:    <form id="form1" runat="server">
Faktura.aspx:9:        <div class="menu">
Faktura.aspx:10:            <a href="Klienci.aspx">Klienci</a> |
Faktura.aspx:11:            <a href="Sprzet.aspx">Sprzęt</a> |
Faktura.aspx:12:            <a href="Rezerwacja.aspx">Rezerwacje</a> |
Faktura.aspx:13:            <a href="Wypozyczenie.aspx">Wydanie</a> |
Faktura.aspx:14:            <a href="Zwrot.aspx">Zwrot</a> |
Faktura.aspx:15:            <a href="Cennik.aspx">Cennik</a> |
Faktura.aspx:16:            <a href="Faktura.aspx">Faktury</a>
Faktura.aspx:17:        </div>
Faktura.aspx:18:        <h2>Faktura</h2>
Faktura.aspx:19:        <asp:Label ID="lblKomunikat" runat="server" ForeColor="Red" EnableViewState="false" />
Faktura.aspx:20:        <table class="formularz" cellpadding="4" cellspacing="0" border="0">
Faktura.aspx:21:            <tr>
Faktura.aspx:22:                <td>Wypożyczenie:</td>
Faktura.aspx:23:                <td>
Faktura.aspx:24:                    <asp:Label ID="lblWypozyczenie" runat="server" Text="-" />
Faktura.aspx:25:                </td>
Faktura.aspx:26:            </tr>
Faktura.aspx:27:            <tr>
Faktura.aspx:28:                <td>Klient:</td>
Faktura.aspx:29:                <td>
Faktura.aspx:30:                    <asp:Label ID="lblKlient" runat="server" Text="-" />
Faktura.aspx:31:                </td>
Faktura.aspx:32:            </tr>
Faktura.aspx:33:            <tr>
Faktura.aspx:34:                <td>Numer faktury:</td>
Faktura.aspx:35:                <td>
Faktura.aspx:36:                    <asp:Label ID="lblNumer" runat="server" Text="(nie wystawiono)" />
Faktura.aspx:37:                    &nbsp;
Faktura.aspx:38:                    <asp:Label ID="lblStatus" runat="server" />
Faktura.aspx:39:                </td>
Faktura.aspx:40:            </tr>
Faktura.aspx:41:            <tr>
Faktura.aspx:42:                <td>&nbsp;</td>
Faktura.aspx:43:                <td>
Faktura.aspx:44:                    <asp:Button ID="btnWystaw" runat="server" Text="Wystaw fakturę" />
Faktura.aspx:45:                </td>
Faktura.aspx:46:            </tr>
Faktura.aspx:47:        </table>
Faktura.aspx:48:        <h3>Pozycje</h3>
Faktura.aspx:49:        <asp:GridView ID="gvPozycje" runat="server" AutoGenerateColumns="false" CellPadding="4" GridLines="Horizontal" EmptyDataText="Brak pozycji">
Faktura.aspx:50:            <Columns>
Faktura.aspx:51:                <asp:BoundField DataField="Lp" HeaderText="Lp." />
Faktura.aspx:52:                <asp:BoundField DataField="Opis" HeaderText="Opis" />
Faktura.aspx:53:                <asp:BoundField DataField="Ilosc" HeaderText="Ilość" DataFormatString="{0:0.##}" />
Faktura.aspx:54:                <asp:BoundField DataField="CenaNetto" HeaderText="Cena netto" DataFormatString="{0:N2}" />
Faktura.aspx:55:                <asp:BoundField DataField="StawkaVat" HeaderText="VAT" DataFormatString="{0:P0}" />
Faktura.aspx:56:                <asp:BoundField DataField="WartoscNetto" HeaderText="Wartość netto" DataFormatString="{0:N2}" />
Faktura.aspx:57:            </Columns>
Faktura.aspx:58:        </asp:GridView>
Faktura.aspx:59:        <table class="sumy" cellpadding="4" cellspacing="0" border="0">
Faktura.aspx:60:            <tr>
Faktura.aspx:61:                <td>Netto:</td>
Faktura.aspx:62:                <td><asp:Label ID="lblNetto" runat="server" Text="0,00" /> zł</td>
Faktura.aspx:63:            </tr>
Faktura.aspx:64:            <tr>
Faktura.aspx:65:                <td>VAT:</td>
Faktura.aspx:66:                <td><asp:Label ID="lblVat" runat="server" Text="0,00" /> zł</td>
Faktura.aspx:67:            </tr>
Faktura.aspx:68:            <tr>
Faktura.aspx:69:                <td><b>Brutto:</b></td>
Faktura.aspx:70:                <td><b><asp:Label ID="lblBrutto" runat="server" Text="0,00" /> zł</b></td>
Faktura.aspx:71:            </tr>
Faktura.aspx:72:        </table>
Faktura.aspx:73:        <p>
Faktura.aspx:74:            <asp:Button ID="btnZaplac" runat="server" Text="Zapłać przez bramkę" OnClientClick="return confirm('Zarejestrować płatność w bramce?');" />
Faktura.aspx:75:            &nbsp;
Faktura.aspx:76:            <asp:Label ID="lblPlatnosc" runat="server" />
Faktura.aspx:77:        </p>
Faktura.aspx:78:        <div class="separator"></div>
Faktura.aspx:79:        <h3>Faktury z dnia</h3>
Faktura.aspx:80:        <asp:GridView ID="gvFakturyDnia" runat="server" AutoGenerateColumns="false" CellPadding="4" GridLines="Horizontal" EmptyDataText="Brak faktur z dzisiaj">
Faktura.aspx:81:            <Columns>
Faktura.aspx:82:                <asp:BoundField DataField="Numer" HeaderText="Numer" />
Faktura.aspx:83:                <asp:BoundField DataField="Klient" HeaderText="Klient" />
Faktura.aspx:84:                <asp:BoundField DataField="WypozyczenieId" HeaderText="Wypożyczenie" />
Faktura.aspx:85:                <asp:BoundField DataField="Brutto" HeaderText="Brutto" DataFormatString="{0:N2}" />
Faktura.aspx:86:                <asp:BoundField DataField="Status" HeaderText="Status" />
Faktura.aspx:87:            </Columns>
Faktura.aspx:88:        </asp:GridView>
Faktura.aspx:89:        <p>
Faktura.aspx:90:            <asp:Button ID="btnEksportCsv" runat="server" Text="Eksport CSV (dzisiaj)" />
Faktura.aspx:91:            &nbsp;
Faktura.aspx:92:            <asp:Label ID="lblEksport" runat="server" />
Faktura.aspx:93:        </p>
Faktura.aspx:94:    </form>
Faktura.aspx:95:</body>
Faktura.aspx:96:</html>
Faktura.aspx.vb:1:' Modul: Faktura.aspx.vb | Autor: zespol IT | 2014-05-19
Faktura.aspx.vb:2:' Faktura za wypozyczenie: wystawienie, platnosc przez bramke, eksport CSV dnia.
Faktura.aspx.vb:3:' 2015-01-12 MW: platnosc przez bramke SOAP (zamiast strony Platnosc.aspx)
Faktura.aspx.vb:4:' 2018-04-03 PT: lista faktur z dnia i przycisk eksportu CSV
Faktura.aspx.vb:5:
Faktura.aspx.vb:6:Imports System.Data
Faktura.aspx.vb:7:Imports System.Net
Faktura.aspx.vb:8:Imports System.Web.Services.Protocols
Faktura.aspx.vb:9:Imports Wypozyczalnia.Core
Faktura.aspx.vb:10:Imports Wypozyczalnia.Core.Model
Faktura.aspx.vb:11:Imports Wypozyczalnia.Core.Integracje
Faktura.aspx.vb:12:
Faktura.aspx.vb:13:Partial Class Faktura
Faktura.aspx.vb:14:    Inherits System.Web.UI.Page
Faktura.aspx.vb:15:
Faktura.aspx.vb:16:    Protected WithEvents lblKomunikat As Global.System.Web.UI.WebControls.Label
Faktura.aspx.vb:17:    Protected WithEvents lblWypozyczenie As Global.System.Web.UI.WebControls.Label
Faktura.aspx.vb:18:    Protected WithEvents lblKlient As Global.System.Web.UI.WebControls.Label
Faktura.aspx.vb:19:    Protected WithEvents lblNumer As Global.System.Web.UI.WebControls.Label
Faktura.aspx.vb:20:    Protected WithEvents lblStatus As Global.System.Web.UI.WebControls.Label
Faktura.aspx.vb:21:    Protected WithEvents btnWystaw As Global.System.Web.UI.WebControls.Button
Faktura.aspx.vb:22:    Protected WithEvents gvPozycje As Global.System.Web.UI.WebControls.GridView
Faktura.aspx.vb:23:    Protected WithEvents lblNetto As Global.System.Web.UI.WebControls.Label
Faktura.aspx.vb:24:    Protected WithEvents lblVat As Global.System.Web.UI.WebControls.Label
Faktura.aspx.vb:25:    Protected WithEvents lblBrutto As Global.System.Web.UI.WebControls.Label
Faktura.aspx.vb:26:    Protected WithEvents btnZaplac As Global.System.Web.UI.WebControls.Button
Faktura.aspx.vb:27:    Protected WithEvents lblPlatnosc As Global.System.Web.UI.WebControls.Label
Faktura.aspx.vb:28:    Protected WithEvents gvFakturyDnia As Global.System.Web.UI.WebControls.GridView
Faktura.aspx.vb:29:    Protected WithEvents btnEksportCsv As Global.System.Web.UI.WebControls.Button
Faktura.aspx.vb:30:    Protected WithEvents lblEksport As Global.System.Web.UI.WebControls.Label
Faktura.aspx.vb:31:
Faktura.aspx.vb:32:    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Faktura))
Faktura.aspx.vb:33:
Faktura.aspx.vb:34:    Private Const Waluta As String = "PLN"
Faktura.aspx.vb:35:    Private Const FormatDaty As String = "yyyy-MM-dd HH:mm"
Faktura.aspx.vb:36:
Faktura.aspx.vb:37:    Private Property WypozyczenieId As Integer
Faktura.aspx.vb:38:        Get
Faktura.aspx.vb:39:            If ViewState("WypozyczenieId") Is Nothing Then Return 0
Faktura.aspx.vb:40:            Return ViewState("WypozyczenieId")
Faktura.aspx.vb:41:        End Get
Faktura.aspx.vb:42:        Set(ByVal value As Integer)
Faktura.aspx.vb:43:            ViewState("WypozyczenieId") = value
Faktura.aspx.vb:44:        End Set
Faktura.aspx.vb:45:    End Property
Faktura.aspx.vb:46:
Faktura.aspx.vb:47:    Private Property FakturaId As Integer
Faktura.aspx.vb:48:        Get
Faktura.aspx.vb:49:            If ViewState("FakturaId") Is Nothing Then Return 0
Faktura.aspx.vb:50:            Return ViewState("FakturaId")
Faktura.aspx.vb:51:        End Get
Faktura.aspx.vb:52:        Set(ByVal value As Integer)
Faktura.aspx.vb:53:            ViewState("FakturaId") = value
Faktura.aspx.vb:54:        End Set
Faktura.aspx.vb:55:    End Property
Faktura.aspx.vb:56:
Faktura.aspx.vb:57:    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
Faktura.aspx.vb:58:        If Not IsPostBack Then
Faktura.aspx.vb:59:            WypozyczenieId = UstalWypozyczenie()
Faktura.aspx.vb:60:            PokazWypozyczenie()
Faktura.aspx.vb:61:            WczytajFakturyDnia()
Faktura.aspx.vb:62:        End If
Faktura.aspx.vb:63:    End Sub
Faktura.aspx.vb:64:
Faktura.aspx.vb:65:    ' najpierw ?id= z linku po zwrocie, potem sesja
Faktura.aspx.vb:66:    Private Function UstalWypozyczenie() As Integer
Faktura.aspx.vb:67:        Dim id As Integer = 0
Faktura.aspx.vb:68:        Dim zParametru As String = Request.QueryString("id")
Faktura.aspx.vb:69:        If Not String.IsNullOrEmpty(zParametru) Then
Faktura.aspx.vb:70:            Integer.TryParse(zParametru, id)
Faktura.aspx.vb:71:        End If
Faktura.aspx.vb:72:        If id = 0 AndAlso Session("OstatnieWypozyczenieId") IsNot Nothing Then
Faktura.aspx.vb:73:            id = Session("OstatnieWypozyczenieId")
Faktura.aspx.vb:74:        End If
Faktura.aspx.vb:75:        Return id
Faktura.aspx.vb:76:    End Function
Faktura.aspx.vb:77:
Faktura.aspx.vb:78:    Private Sub PokazWypozyczenie()
Faktura.aspx.vb:79:        WyczyscFakture()
Faktura.aspx.vb:80:        If WypozyczenieId = 0 Then
Faktura.aspx.vb:81:            lblWypozyczenie.Text = "nie wybrano (wejdź z ekranu zwrotu)"
Faktura.aspx.vb:82:            PokazWybranegoKlienta()
Faktura.aspx.vb:83:            btnWystaw.Enabled = False
Faktura.aspx.vb:84:            btnZaplac.Enabled = False
Faktura.aspx.vb:85:            Return
Faktura.aspx.vb:86:        End If
Faktura.aspx.vb:87:
Faktura.aspx.vb:88:        Dim w As Wypozyczenie = New RentalManager().PobierzWypozyczenie(WypozyczenieId)
Faktura.aspx.vb:89:        If w Is Nothing Then
Faktura.aspx.vb:90:            lblKomunikat.Text = "Nie znaleziono wypożyczenia nr " & WypozyczenieId
Faktura.aspx.vb:91:            btnWystaw.Enabled = False
Faktura.aspx.vb:92:            btnZaplac.Enabled = False
Faktura.aspx.vb:93:            Return
Faktura.aspx.vb:94:        End If
Faktura.aspx.vb:95:
Faktura.aspx.vb:96:        Dim sprzet As Sprzet = New EquipmentManager().PobierzSprzet(w.SprzetId)
Faktura.aspx.vb:97:        Dim opis As String = "#" & w.Id & " | wydano " & w.DataWydania.ToString(FormatDaty)
Faktura.aspx.vb:98:        If sprzet IsNot Nothing Then opis = opis & " | " & sprzet.Nazwa
Faktura.aspx.vb:99:        If w.DataZwrotu.HasValue Then
Faktura.aspx.vb:100:            opis = opis & " | zwrot " & w.DataZwrotu.Value.ToString(FormatDaty)
Faktura.aspx.vb:101:        Else
Faktura.aspx.vb:102:            opis = opis & " | jeszcze nie zwrócono"
Faktura.aspx.vb:103:        End If
Faktura.aspx.vb:104:        lblWypozyczenie.Text = opis
Faktura.aspx.vb:105:
Faktura.aspx.vb:106:        Dim klient As Klient = New CustomerManager().PobierzKlienta(w.KlientId)
Faktura.aspx.vb:107:        If klient IsNot Nothing Then
Faktura.aspx.vb:108:            lblKlient.Text = NazwaKlienta(klient)
Faktura.aspx.vb:109:        End If
Faktura.aspx.vb:110:        If Session("WybranyKlientId") <> 0 AndAlso Session("WybranyKlientId") <> w.KlientId Then
Faktura.aspx.vb:111:            lblKomunikat.Text = "Uwaga: wypożyczenie należy do innego klienta niż wybrany na liście klientów."
Faktura.aspx.vb:112:        End If
Faktura.aspx.vb:113:
Faktura.aspx.vb:114:        Dim mgr As New InvoiceManager()
Faktura.aspx.vb:115:        Dim f As Wypozyczalnia.Core.Model.Faktura = mgr.PobierzFaktureDlaWypozyczenia(w.Id)
Faktura.aspx.vb:116:        If f IsNot Nothing Then
Faktura.aspx.vb:117:            PokazFakture(f)
Faktura.aspx.vb:118:            btnWystaw.Enabled = False
Faktura.aspx.vb:119:        ElseIf w.Status = "Z" Then
Faktura.aspx.vb:120:            Dim podglad As Wypozyczalnia.Core.Model.Faktura = mgr.PodgladFaktury(w.Id)
Faktura.aspx.vb:121:            If podglad IsNot Nothing Then
Faktura.aspx.vb:122:                PokazPozycje(podglad)
Faktura.aspx.vb:123:                lblNumer.Text = "(podgląd - faktura nie wystawiona)"
Faktura.aspx.vb:124:            End If
Faktura.aspx.vb:125:            btnWystaw.Enabled = True
Faktura.aspx.vb:126:            btnZaplac.Enabled = False
Faktura.aspx.vb:127:        Else
Faktura.aspx.vb:128:            lblKomunikat.Text = "Fakturę można wystawić dopiero po zwrocie sprzętu."
Faktura.aspx.vb:129:            btnWystaw.Enabled = False
Faktura.aspx.vb:130:            btnZaplac.Enabled = False
Faktura.aspx.vb:131:        End If
Faktura.aspx.vb:132:    End Sub
Faktura.aspx.vb:133:
Faktura.aspx.vb:134:    Private Sub PokazWybranegoKlienta()
Faktura.aspx.vb:135:        If Session("WybranyKlientId") Is Nothing OrElse Session("WybranyKlientId") = 0 Then
Faktura.aspx.vb:136:            lblKlient.Text = "-"
Faktura.aspx.vb:137:            Return
Faktura.aspx.vb:138:        End If
Faktura.aspx.vb:139:        Dim klient As Klient = New CustomerManager().PobierzKlienta(Session("WybranyKlientId"))
Faktura.aspx.vb:140:        If klient IsNot Nothing Then
Faktura.aspx.vb:141:            lblKlient.Text = NazwaKlienta(klient) & " (wybrany na liście klientów)"
Faktura.aspx.vb:142:        End If
Faktura.aspx.vb:143:    End Sub
Faktura.aspx.vb:144:
Faktura.aspx.vb:145:    Private Sub PokazFakture(ByVal f As Wypozyczalnia.Core.Model.Faktura)
Faktura.aspx.vb:146:        FakturaId = f.Id
Faktura.aspx.vb:147:        lblNumer.Text = f.Numer & " z dnia " & f.DataWystawienia.ToString("yyyy-MM-dd")
Faktura.aspx.vb:148:        If f.Status = "O" Then
Faktura.aspx.vb:149:            lblStatus.Text = "OPŁACONA (transakcja " & f.IdTransakcji & ")"
Faktura.aspx.vb:150:            lblStatus.ForeColor = Drawing.Color.Green
Faktura.aspx.vb:151:            btnZaplac.Enabled = False
Faktura.aspx.vb:152:        Else
Faktura.aspx.vb:153:            lblStatus.Text = "do zapłaty"
Faktura.aspx.vb:154:            lblStatus.ForeColor = Drawing.Color.DarkOrange
Faktura.aspx.vb:155:            btnZaplac.Enabled = True
Faktura.aspx.vb:156:        End If
Faktura.aspx.vb:157:        PokazPozycje(f)
Faktura.aspx.vb:158:    End Sub
Faktura.aspx.vb:159:
Faktura.aspx.vb:160:    Private Sub PokazPozycje(ByVal f As Wypozyczalnia.Core.Model.Faktura)
Faktura.aspx.vb:161:        gvPozycje.DataSource = f.Pozycje
Faktura.aspx.vb:162:        gvPozycje.DataBind()
Faktura.aspx.vb:163:        lblNetto.Text = f.Netto
Faktura.aspx.vb:164:        lblVat.Text = f.Vat
Faktura.aspx.vb:165:        lblBrutto.Text = f.Brutto
Faktura.aspx.vb:166:    End Sub
Faktura.aspx.vb:167:
Faktura.aspx.vb:168:    Private Sub WyczyscFakture()
Faktura.aspx.vb:169:        FakturaId = 0
Faktura.aspx.vb:170:        lblNumer.Text = "(nie wystawiono)"
Faktura.aspx.vb:171:        lblStatus.Text = ""
Faktura.aspx.vb:172:        lblNetto.Text = "0,00"
Faktura.aspx.vb:173:        lblVat.Text = "0,00"
Faktura.aspx.vb:174:        lblBrutto.Text = "0,00"
Faktura.aspx.vb:175:        lblPlatnosc.Text = ""
Faktura.aspx.vb:176:        gvPozycje.DataSource = Nothing
Faktura.aspx.vb:177:        gvPozycje.DataBind()
Faktura.aspx.vb:178:    End Sub
Faktura.aspx.vb:179:
Faktura.aspx.vb:180:    Private Sub WczytajFakturyDnia()
Faktura.aspx.vb:181:        Try
Faktura.aspx.vb:182:            gvFakturyDnia.DataSource = New InvoiceManager().FakturyZDnia(Date.Today)
Faktura.aspx.vb:183:            gvFakturyDnia.DataBind()
Faktura.aspx.vb:184:        Catch ex As Exception
Faktura.aspx.vb:185:            Log.Error("Blad pobierania faktur z dnia", ex)
Faktura.aspx.vb:186:        End Try
Faktura.aspx.vb:187:    End Sub
Faktura.aspx.vb:188:
Faktura.aspx.vb:189:    Protected Sub btnWystaw_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnWystaw.Click
Faktura.aspx.vb:190:        If WypozyczenieId = 0 Then
Faktura.aspx.vb:191:            lblKomunikat.Text = "Nie wybrano wypożyczenia."
Faktura.aspx.vb:192:            Return
Faktura.aspx.vb:193:        End If
Faktura.aspx.vb:194:        Try
Faktura.aspx.vb:195:            Dim f As Wypozyczalnia.Core.Model.Faktura = New InvoiceManager().WystawFaktureZaWypozyczenie(WypozyczenieId)
Faktura.aspx.vb:196:            PokazFakture(f)
Faktura.aspx.vb:197:            btnWystaw.Enabled = False
Faktura.aspx.vb:198:            lblKomunikat.ForeColor = Drawing.Color.Green
Faktura.aspx.vb:199:            lblKomunikat.Text = "Wystawiono fakturę " & f.Numer
Faktura.aspx.vb:200:            WczytajFakturyDnia()
Faktura.aspx.vb:201:        Catch ex As BladBiznesowyException
Faktura.aspx.vb:202:            lblKomunikat.Text = ex.Message
Faktura.aspx.vb:203:        Catch ex As Exception
Faktura.aspx.vb:204:            Log.Error("Blad wystawiania faktury dla wypozyczenia " & WypozyczenieId, ex)
Faktura.aspx.vb:205:            lblKomunikat.Text = "Błąd wystawiania faktury: " & ex.Message
Faktura.aspx.vb:206:        End Try
Faktura.aspx.vb:207:    End Sub
Faktura.aspx.vb:208:
Faktura.aspx.vb:209:    Protected Sub btnZaplac_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZaplac.Click
Faktura.aspx.vb:210:        If FakturaId = 0 Then
Faktura.aspx.vb:211:            lblKomunikat.Text = "Najpierw wystaw fakturę."
Faktura.aspx.vb:212:            Return
Faktura.aspx.vb:213:        End If
Faktura.aspx.vb:214:        Dim mgr As New InvoiceManager()
Faktura.aspx.vb:215:        Dim f As Wypozyczalnia.Core.Model.Faktura = mgr.PobierzFakture(FakturaId)
Faktura.aspx.vb:216:        If f Is Nothing Then
Faktura.aspx.vb:217:            lblKomunikat.Text = "Nie znaleziono faktury."
Faktura.aspx.vb:218:            Return
Faktura.aspx.vb:219:        End If
Faktura.aspx.vb:220:        If f.Status = "O" Then
Faktura.aspx.vb:221:            lblPlatnosc.Text = "Faktura jest już opłacona."
Faktura.aspx.vb:222:            Return
Faktura.aspx.vb:223:        End If
Faktura.aspx.vb:224:
Faktura.aspx.vb:225:        Dim bramka As New BramkaPlatnosci.BramkaService()
Faktura.aspx.vb:226:        bramka.Timeout = 30000
Faktura.aspx.vb:227:        Try
Faktura.aspx.vb:228:            Dim wynik As BramkaPlatnosci.WynikPlatnosci = bramka.ZarejestrujPlatnosc(f.Numer, f.Brutto, Waluta)
Faktura.aspx.vb:229:            Log.Info("Bramka: faktura " & f.Numer & ", status " & wynik.Status & ", transakcja " & wynik.IdTransakcji)
Faktura.aspx.vb:230:            Select Case wynik.Status
Faktura.aspx.vb:231:                Case "OK"
Faktura.aspx.vb:232:                    mgr.OznaczOplacona(f.Id, wynik.IdTransakcji)
Faktura.aspx.vb:233:                    lblPlatnosc.ForeColor = Drawing.Color.Green
Faktura.aspx.vb:234:                    lblPlatnosc.Text = "Płatność przyjęta, transakcja " & wynik.IdTransakcji
Faktura.aspx.vb:235:                    Session("OstatnieWypozyczenieId") = Nothing
Faktura.aspx.vb:236:                    PokazFakture(mgr.PobierzFakture(f.Id))
Faktura.aspx.vb:237:                    WczytajFakturyDnia()
Faktura.aspx.vb:238:                Case "ODRZUCONA"
Faktura.aspx.vb:239:                    lblPlatnosc.ForeColor = Drawing.Color.Red
Faktura.aspx.vb:240:                    lblPlatnosc.Text = "Bramka odrzuciła płatność: " & wynik.Komunikat
Faktura.aspx.vb:241:                Case Else
Faktura.aspx.vb:242:                    lblPlatnosc.ForeColor = Drawing.Color.Red
Faktura.aspx.vb:243:                    lblPlatnosc.Text = "Błąd bramki: " & wynik.Komunikat
Faktura.aspx.vb:244:            End Select
Faktura.aspx.vb:245:        Catch ex As SoapException
Faktura.aspx.vb:246:            Log.Error("SOAP: blad bramki dla faktury " & f.Numer, ex)
Faktura.aspx.vb:247:            lblPlatnosc.Text = "Bramka zwróciła błąd: " & ex.Message
Faktura.aspx.vb:248:        Catch ex As WebException
Faktura.aspx.vb:249:            Log.Error("Brak polaczenia z bramka platnosci", ex)
Faktura.aspx.vb:250:            lblPlatnosc.Text = "Brak połączenia z bramką płatności. Spróbuj ponownie później."
Faktura.aspx.vb:251:        Finally
Faktura.aspx.vb:252:            bramka.Dispose()
Faktura.aspx.vb:253:        End Try
Faktura.aspx.vb:254:    End Sub
Faktura.aspx.vb:255:
Faktura.aspx.vb:256:    Protected Sub btnEksportCsv_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEksportCsv.Click
Faktura.aspx.vb:257:        Try
Faktura.aspx.vb:258:            Dim plik As String = New EksportCsv().EksportujDzien(Date.Today)
Faktura.aspx.vb:259:            lblEksport.ForeColor = Drawing.Color.Green
Faktura.aspx.vb:260:            lblEksport.Text = "Zapisano: " & plik
Faktura.aspx.vb:261:        Catch ex As Exception
Faktura.aspx.vb:262:            Log.Error("Blad eksportu CSV z ekranu faktur", ex)
Faktura.aspx.vb:263:            lblEksport.ForeColor = Drawing.Color.Red
Faktura.aspx.vb:264:            lblEksport.Text = "Eksport nieudany: " & ex.Message
Faktura.aspx.vb:265:        End Try
Faktura.aspx.vb:266:    End Sub
Faktura.aspx.vb:267:
Faktura.aspx.vb:268:    Private Function NazwaKlienta(ByVal k As Klient) As String
Faktura.aspx.vb:269:        If k.Typ = "F" AndAlso Not String.IsNullOrEmpty(k.NazwaFirmy) Then
Faktura.aspx.vb:270:            Return k.NazwaFirmy & " (" & k.Imie & " " & k.Nazwisko & ")"
Faktura.aspx.vb:271:        End If
Faktura.aspx.vb:272:        Return k.Imie & " " & k.Nazwisko
Faktura.aspx.vb:273:    End Function
Faktura.aspx.vb:274:
Faktura.aspx.vb:275:End Class
Klienci.aspx:1:<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Klienci.aspx.vb" Inherits="WypozyczalniaWeb.Klienci" %>
Klienci.aspx:2:<!DOCTYPE html>
Klienci.aspx:3:<html xmlns="http://www.w3.org/1999/xhtml">
Klienci.aspx:4:<head runat="server">
Klienci.aspx:5:    <title>Wypożyczalnia - Klienci</title>
Klienci.aspx:6:</head>
Klienci.aspx:7:<body>
Klienci.aspx:8:    <form id="form1" runat="server">
Klienci.aspx:9:        <div class="menu">
Klienci.aspx:10:            <a href="Klienci.aspx">Klienci</a> |
Klienci.aspx:11:            <a href="Sprzet.aspx">Sprzęt</a> |
Klienci.aspx:12:            <a href="Rezerwacja.aspx">Rezerwacje</a> |
Klienci.aspx:13:            <a href="Wypozyczenie.aspx">Wydanie</a> |
Klienci.aspx:14:            <a href="Zwrot.aspx">Zwrot</a> |
Klienci.aspx:15:            <a href="Cennik.aspx">Cennik</a> |
Klienci.aspx:16:            <a href="Faktura.aspx">Faktury</a>
Klienci.aspx:17:        </div>
Klienci.aspx:18:        <h2>Klienci</h2>
Klienci.aspx:19:        <asp:Label ID="lblKomunikat" runat="server" ForeColor="Red" EnableViewState="false" />
Klienci.aspx:20:        <table class="szukaj">
Klienci.aspx:21:            <tr>
Klienci.aspx:22:                <td>Nazwisko:</td>
Klienci.aspx:23:                <td><asp:TextBox ID="txtSzukaj" runat="server" Width="250px" /></td>
Klienci.aspx:24:                <td><asp:Button ID="btnSzukaj" runat="server" Text="Szukaj" /></td>
Klienci.aspx:25:                <td><asp:Button ID="btnNowy" runat="server" Text="Nowy klient" CausesValidation="false" /></td>
Klienci.aspx:26:            </tr>
Klienci.aspx:27:        </table>
Klienci.aspx:28:        <asp:GridView ID="gvKlienci" runat="server" AutoGenerateColumns="false" DataKeyNames="Id"
Klienci.aspx:29:            CellPadding="4" GridLines="Horizontal" AllowPaging="true" PageSize="20"
Klienci.aspx:30:            EmptyDataText="Brak klientów spełniających kryteria.">
Klienci.aspx:31:            <Columns>
Klienci.aspx:32:                <asp:BoundField DataField="Id" HeaderText="Nr" />
Klienci.aspx:33:                <asp:BoundField DataField="Imie" HeaderText="Imię" />
Klienci.aspx:34:                <asp:BoundField DataField="Nazwisko" HeaderText="Nazwisko" />
Klienci.aspx:35:                <asp:BoundField DataField="NazwaFirmy" HeaderText="Firma" />
Klienci.aspx:36:                <asp:BoundField DataField="Email" HeaderText="E-mail" />
Klienci.aspx:37:                <asp:BoundField DataField="Telefon" HeaderText="Telefon" />
Klienci.aspx:38:                <asp:CommandField ShowSelectButton="true" SelectText="Wybierz" />
Klienci.aspx:39:                <asp:ButtonField CommandName="Edytuj" Text="Edytuj" />
Klienci.aspx:40:            </Columns>
Klienci.aspx:41:            <SelectedRowStyle BackColor="#FFFFCC" />
Klienci.aspx:42:        </asp:GridView>
Klienci.aspx:43:        <div class="separator"></div>
Klienci.aspx:44:        <h3><asp:Label ID="lblTrybFormularza" runat="server" Text="Nowy klient" /></h3>
Klienci.aspx:45:        <asp:HiddenField ID="hfKlientId" runat="server" Value="0" />
Klienci.aspx:46:        <table class="formularz">
Klienci.aspx:47:            <tr><td>Imię:</td><td><asp:TextBox ID="txtImie" runat="server" MaxLength="50" /></td></tr>
Klienci.aspx:48:            <tr><td>Nazwisko:</td><td><asp:TextBox ID="txtNazwisko" runat="server" MaxLength="80" /></td></tr>
Klienci.aspx:49:            <tr><td>Nazwa firmy:</td><td><asp:TextBox ID="txtNazwaFirmy" runat="server" MaxLength="120" Width="300px" /></td></tr>
Klienci.aspx:50:            <tr><td>E-mail:</td><td><asp:TextBox ID="txtEmail" runat="server" MaxLength="120" /></td></tr>
Klienci.aspx:51:            <tr><td>Telefon:</td><td><asp:TextBox ID="txtTelefon" runat="server" MaxLength="20" /></td></tr>
Klienci.aspx:52:            <tr><td>Nr dokumentu:</td><td><asp:TextBox ID="txtNumerDokumentu" runat="server" MaxLength="20" /></td></tr>
Klienci.aspx:53:            <tr><td>NIP:</td><td><asp:TextBox ID="txtNIP" runat="server" MaxLength="13" /></td></tr>
Klienci.aspx:54:            <tr>
Klienci.aspx:55:                <td>Typ:</td>
Klienci.aspx:56:                <td>
Klienci.aspx:57:                    <asp:DropDownList ID="ddlTyp" runat="server">
Klienci.aspx:58:                        <asp:ListItem Value="I" Text="Osoba prywatna" />
Klienci.aspx:59:                        <asp:ListItem Value="F" Text="Firma" />
Klienci.aspx:60:                    </asp:DropDownList>
Klienci.aspx:61:                </td>
Klienci.aspx:62:            </tr>
Klienci.aspx:63:            <tr>
Klienci.aspx:64:                <td>Próg rabatu:</td>
Klienci.aspx:65:                <td>
Klienci.aspx:66:                    <asp:DropDownList ID="ddlProg" runat="server">
Klienci.aspx:67:                        <asp:ListItem Value="0" Text="0 - brak" />
Klienci.aspx:68:                        <asp:ListItem Value="1" Text="1" />
Klienci.aspx:69:                        <asp:ListItem Value="2" Text="2" />
Klienci.aspx:70:                        <asp:ListItem Value="3" Text="3" />
Klienci.aspx:71:                    </asp:DropDownList>
Klienci.aspx:72:                </td>
Klienci.aspx:73:            </tr>
Klienci.aspx:74:            <tr><td>Powód blokady:</td><td><asp:TextBox ID="txtPowodBlokady" runat="server" MaxLength="200" Width="300px" /></td></tr>
Klienci.aspx:75:        </table>
Klienci.aspx:76:        <asp:Button ID="btnZapisz" runat="server" Text="Zapisz" />
Klienci.aspx:77:        <asp:Button ID="btnZablokuj" runat="server" Text="Zablokuj klienta"
Klienci.aspx:78:            OnClientClick="return confirm('Zablokować klienta?');" />
Klienci.aspx:79:    </form>
Klienci.aspx:80:</body>
Klienci.aspx:81:</html>
Klienci.aspx.vb:1:' Modul: Klienci.aspx.vb - lista, wyszukiwanie i edycja klientow | Autor: zespol IT | 2014-03-11
Klienci.aspx.vb:2:' Historia: 2014-03-11 JK utworzenie strony
Klienci.aspx.vb:3:'           2015-11-23 MW blokowanie klientow, przejscie do rezerwacji
Klienci.aspx.vb:4:'           2018-09-04 PT pole NIP i typ klienta (firmy)
Klienci.aspx.vb:5:Imports System.Data
Klienci.aspx.vb:6:Imports System.Data.SqlClient
Klienci.aspx.vb:7:Imports Wypozyczalnia.Core
Klienci.aspx.vb:8:Imports Wypozyczalnia.Core.Model
Klienci.aspx.vb:9:Imports Wypozyczalnia.Core.DAL
Klienci.aspx.vb:10:
Klienci.aspx.vb:11:Partial Class Klienci
Klienci.aspx.vb:12:    Inherits System.Web.UI.Page
Klienci.aspx.vb:13:
Klienci.aspx.vb:14:    Protected WithEvents lblKomunikat As Global.System.Web.UI.WebControls.Label
Klienci.aspx.vb:15:    Protected WithEvents txtSzukaj As Global.System.Web.UI.WebControls.TextBox
Klienci.aspx.vb:16:    Protected WithEvents btnSzukaj As Global.System.Web.UI.WebControls.Button
Klienci.aspx.vb:17:    Protected WithEvents btnNowy As Global.System.Web.UI.WebControls.Button
Klienci.aspx.vb:18:    Protected WithEvents gvKlienci As Global.System.Web.UI.WebControls.GridView
Klienci.aspx.vb:19:    Protected WithEvents lblTrybFormularza As Global.System.Web.UI.WebControls.Label
Klienci.aspx.vb:20:    Protected WithEvents hfKlientId As Global.System.Web.UI.WebControls.HiddenField
Klienci.aspx.vb:21:    Protected WithEvents txtImie As Global.System.Web.UI.WebControls.TextBox
Klienci.aspx.vb:22:    Protected WithEvents txtNazwisko As Global.System.Web.UI.WebControls.TextBox
Klienci.aspx.vb:23:    Protected WithEvents txtNazwaFirmy As Global.System.Web.UI.WebControls.TextBox
Klienci.aspx.vb:24:    Protected WithEvents txtEmail As Global.System.Web.UI.WebControls.TextBox
Klienci.aspx.vb:25:    Protected WithEvents txtTelefon As Global.System.Web.UI.WebControls.TextBox
Klienci.aspx.vb:26:    Protected WithEvents txtNumerDokumentu As Global.System.Web.UI.WebControls.TextBox
Klienci.aspx.vb:27:    Protected WithEvents txtNIP As Global.System.Web.UI.WebControls.TextBox
Klienci.aspx.vb:28:    Protected WithEvents ddlTyp As Global.System.Web.UI.WebControls.DropDownList
Klienci.aspx.vb:29:    Protected WithEvents ddlProg As Global.System.Web.UI.WebControls.DropDownList
Klienci.aspx.vb:30:    Protected WithEvents txtPowodBlokady As Global.System.Web.UI.WebControls.TextBox
Klienci.aspx.vb:31:    Protected WithEvents btnZapisz As Global.System.Web.UI.WebControls.Button
Klienci.aspx.vb:32:    Protected WithEvents btnZablokuj As Global.System.Web.UI.WebControls.Button
Klienci.aspx.vb:33:
Klienci.aspx.vb:34:    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Klienci))
Klienci.aspx.vb:35:
Klienci.aspx.vb:36:    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
Klienci.aspx.vb:37:        If Not IsPostBack Then
Klienci.aspx.vb:38:            WyczyscFormularz()
Klienci.aspx.vb:39:            ZaladujListe("")
Klienci.aspx.vb:40:            Dim wybrany As Integer = Session("WybranyKlientId")
Klienci.aspx.vb:41:            If wybrany > 0 Then
Klienci.aspx.vb:42:                lblKomunikat.Text = "Aktualnie wybrany klient: nr " & wybrany
Klienci.aspx.vb:43:                lblKomunikat.ForeColor = Drawing.Color.DarkGreen
Klienci.aspx.vb:44:            End If
Klienci.aspx.vb:45:        End If
Klienci.aspx.vb:46:    End Sub
Klienci.aspx.vb:47:
Klienci.aspx.vb:48:    Protected Sub btnSzukaj_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSzukaj.Click
Klienci.aspx.vb:49:        Dim sql As String = "SELECT Id, Imie, Nazwisko, NazwaFirmy, Email, Telefon FROM Klienci WHERE Nazwisko LIKE '%" & txtSzukaj.Text & "%' ORDER BY Nazwisko"
Klienci.aspx.vb:50:        Try
Klienci.aspx.vb:51:            Dim dt As DataTable = DbHelper.WykonajZapytanie(sql)
Klienci.aspx.vb:52:            gvKlienci.PageIndex = 0
Klienci.aspx.vb:53:            gvKlienci.DataSource = dt
Klienci.aspx.vb:54:            gvKlienci.DataBind()
Klienci.aspx.vb:55:            ViewState("OstatniaFraza") = txtSzukaj.Text
Klienci.aspx.vb:56:            If dt.Rows.Count = 0 Then
Klienci.aspx.vb:57:                lblKomunikat.Text = "Nie znaleziono klientow."
Klienci.aspx.vb:58:            End If
Klienci.aspx.vb:59:        Catch ex As SqlException
Klienci.aspx.vb:60:            Log.Error("Blad wyszukiwania klientow", ex)
Klienci.aspx.vb:61:            lblKomunikat.Text = "Blad bazy danych: " & ex.Message
Klienci.aspx.vb:62:        End Try
Klienci.aspx.vb:63:    End Sub
Klienci.aspx.vb:64:
Klienci.aspx.vb:65:    Protected Sub btnNowy_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNowy.Click
Klienci.aspx.vb:66:        WyczyscFormularz()
Klienci.aspx.vb:67:        gvKlienci.SelectedIndex = -1
Klienci.aspx.vb:68:    End Sub
Klienci.aspx.vb:69:
Klienci.aspx.vb:70:    Protected Sub gvKlienci_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gvKlienci.SelectedIndexChanged
Klienci.aspx.vb:71:        Dim klientId As Integer = gvKlienci.SelectedDataKey.Value
Klienci.aspx.vb:72:        Dim cm As New CustomerManager()
Klienci.aspx.vb:73:        If Not cm.CzyMozeWypozyczac(klientId) Then
Klienci.aspx.vb:74:            Dim k As Klient = cm.PobierzKlienta(klientId)
Klienci.aspx.vb:75:            lblKomunikat.Text = "Klient zablokowany: " & If(k Is Nothing, "", k.PowodBlokady)
Klienci.aspx.vb:76:            Return
Klienci.aspx.vb:77:        End If
Klienci.aspx.vb:78:        Session("WybranyKlientId") = klientId
Klienci.aspx.vb:79:        Log.Debug("Wybrano klienta " & klientId & " w sesji " & Session.SessionID)
Klienci.aspx.vb:80:        Response.Redirect("Rezerwacja.aspx")
Klienci.aspx.vb:81:    End Sub
Klienci.aspx.vb:82:
Klienci.aspx.vb:83:    Protected Sub gvKlienci_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles gvKlienci.RowCommand
Klienci.aspx.vb:84:        If e.CommandName <> "Edytuj" Then Return
Klienci.aspx.vb:85:        Dim indeks As Integer = Convert.ToInt32(e.CommandArgument)
Klienci.aspx.vb:86:        Dim klientId As Integer = gvKlienci.DataKeys(indeks).Value
Klienci.aspx.vb:87:        WczytajDoFormularza(klientId)
Klienci.aspx.vb:88:    End Sub
Klienci.aspx.vb:89:
Klienci.aspx.vb:90:    Protected Sub gvKlienci_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvKlienci.PageIndexChanging
Klienci.aspx.vb:91:        gvKlienci.PageIndex = e.NewPageIndex
Klienci.aspx.vb:92:        ZaladujListe(ViewState("OstatniaFraza"))
Klienci.aspx.vb:93:    End Sub
Klienci.aspx.vb:94:
Klienci.aspx.vb:95:    Protected Sub btnZapisz_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZapisz.Click
Klienci.aspx.vb:96:        lblKomunikat.Text = ""
Klienci.aspx.vb:97:        If txtEmail.Text.Trim() = "" OrElse Not txtEmail.Text.Contains("@") Then
Klienci.aspx.vb:98:            lblKomunikat.Text = "Niepoprawny adres e-mail."
Klienci.aspx.vb:99:            Return
Klienci.aspx.vb:100:        End If
Klienci.aspx.vb:101:        Dim blad As String = WalidujFormularz()
Klienci.aspx.vb:102:        If blad <> "" Then
Klienci.aspx.vb:103:            lblKomunikat.Text = blad
Klienci.aspx.vb:104:            Return
Klienci.aspx.vb:105:        End If
Klienci.aspx.vb:106:
Klienci.aspx.vb:107:        Dim k As New Klient()
Klienci.aspx.vb:108:        k.Id = CInt(hfKlientId.Value)
Klienci.aspx.vb:109:        k.Imie = txtImie.Text.Trim()
Klienci.aspx.vb:110:        k.Nazwisko = txtNazwisko.Text.Trim()
Klienci.aspx.vb:111:        k.NazwaFirmy = txtNazwaFirmy.Text.Trim()
Klienci.aspx.vb:112:        k.Email = txtEmail.Text.Trim()
Klienci.aspx.vb:113:        k.Telefon = txtTelefon.Text.Trim()
Klienci.aspx.vb:114:        k.NumerDokumentu = txtNumerDokumentu.Text.Trim().ToUpper()
Klienci.aspx.vb:115:        k.NIP = txtNIP.Text.Replace("-", "").Trim()
Klienci.aspx.vb:116:        k.Typ = ddlTyp.SelectedValue
Klienci.aspx.vb:117:        k.ProgRabatu = ddlProg.SelectedValue
Klienci.aspx.vb:118:        If k.Typ = "I" Then
Klienci.aspx.vb:119:            k.NIP = Nothing
Klienci.aspx.vb:120:            If k.NazwaFirmy = "" Then k.NazwaFirmy = Nothing
Klienci.aspx.vb:121:        End If
Klienci.aspx.vb:122:
Klienci.aspx.vb:123:        Try
Klienci.aspx.vb:124:            Dim cm As New CustomerManager()
Klienci.aspx.vb:125:            Dim noweId As Integer = cm.Zapisz(k)
Klienci.aspx.vb:126:            hfKlientId.Value = noweId
Klienci.aspx.vb:127:            lblTrybFormularza.Text = "Edycja klienta nr " & noweId
Klienci.aspx.vb:128:            lblKomunikat.Text = "Zapisano klienta nr " & noweId
Klienci.aspx.vb:129:            lblKomunikat.ForeColor = Drawing.Color.DarkGreen
Klienci.aspx.vb:130:            ZaladujListe(ViewState("OstatniaFraza"))
Klienci.aspx.vb:131:        Catch ex As BladBiznesowyException
Klienci.aspx.vb:132:            lblKomunikat.Text = ex.Message
Klienci.aspx.vb:133:        Catch ex As SqlException
Klienci.aspx.vb:134:            Log.Error("Blad zapisu klienta", ex)
Klienci.aspx.vb:135:            lblKomunikat.Text = "Nie udalo sie zapisac klienta: " & ex.Message
Klienci.aspx.vb:136:        End Try
Klienci.aspx.vb:137:    End Sub
Klienci.aspx.vb:138:
Klienci.aspx.vb:139:    Protected Sub btnZablokuj_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZablokuj.Click
Klienci.aspx.vb:140:        Dim klientId As Integer = CInt(hfKlientId.Value)
Klienci.aspx.vb:141:        If klientId = 0 Then
Klienci.aspx.vb:142:            lblKomunikat.Text = "Najpierw wybierz klienta do edycji."
Klienci.aspx.vb:143:            Return
Klienci.aspx.vb:144:        End If
Klienci.aspx.vb:145:        Dim powod As String = txtPowodBlokady.Text.Trim()
Klienci.aspx.vb:146:        If powod = "" Then powod = "blokada recznie z ekranu Klienci"
Klienci.aspx.vb:147:        Dim cm As New CustomerManager()
Klienci.aspx.vb:148:        cm.Zablokuj(klientId, powod)
Klienci.aspx.vb:149:        If Session("WybranyKlientId") = klientId Then
Klienci.aspx.vb:150:            Session("WybranyKlientId") = 0
Klienci.aspx.vb:151:        End If
Klienci.aspx.vb:152:        lblKomunikat.Text = "Klient nr " & klientId & " zostal zablokowany."
Klienci.aspx.vb:153:        ZaladujListe(ViewState("OstatniaFraza"))
Klienci.aspx.vb:154:    End Sub
Klienci.aspx.vb:155:
Klienci.aspx.vb:156:    Private Sub ZaladujListe(ByVal fraza As String)
Klienci.aspx.vb:157:        Try
Klienci.aspx.vb:158:            Dim cm As New CustomerManager()
Klienci.aspx.vb:159:            gvKlienci.DataSource = cm.Szukaj(fraza)
Klienci.aspx.vb:160:            gvKlienci.DataBind()
Klienci.aspx.vb:161:        Catch ex As SqlException
Klienci.aspx.vb:162:            Log.Error("Blad ladowania listy klientow", ex)
Klienci.aspx.vb:163:            lblKomunikat.Text = "Blad bazy danych."
Klienci.aspx.vb:164:        End Try
Klienci.aspx.vb:165:    End Sub
Klienci.aspx.vb:166:
Klienci.aspx.vb:167:    Private Sub WczytajDoFormularza(ByVal klientId As Integer)
Klienci.aspx.vb:168:        Dim cm As New CustomerManager()
Klienci.aspx.vb:169:        Dim k As Klient = cm.PobierzKlienta(klientId)
Klienci.aspx.vb:170:        If k Is Nothing Then
Klienci.aspx.vb:171:            lblKomunikat.Text = "Nie znaleziono klienta nr " & klientId
Klienci.aspx.vb:172:            Return
Klienci.aspx.vb:173:        End If
Klienci.aspx.vb:174:        hfKlientId.Value = k.Id
Klienci.aspx.vb:175:        txtImie.Text = k.Imie
Klienci.aspx.vb:176:        txtNazwisko.Text = k.Nazwisko
Klienci.aspx.vb:177:        txtNazwaFirmy.Text = k.NazwaFirmy
Klienci.aspx.vb:178:        txtEmail.Text = k.Email
Klienci.aspx.vb:179:        txtTelefon.Text = k.Telefon
Klienci.aspx.vb:180:        txtNumerDokumentu.Text = k.NumerDokumentu
Klienci.aspx.vb:181:        txtNIP.Text = k.NIP
Klienci.aspx.vb:182:        UstawWybor(ddlTyp, k.Typ)
Klienci.aspx.vb:183:        UstawWybor(ddlProg, k.ProgRabatu.ToString())
Klienci.aspx.vb:184:        txtPowodBlokady.Text = k.PowodBlokady
Klienci.aspx.vb:185:        lblTrybFormularza.Text = "Edycja klienta nr " & k.Id
Klienci.aspx.vb:186:        If k.Zablokowany Then
Klienci.aspx.vb:187:            lblTrybFormularza.Text = lblTrybFormularza.Text & " (ZABLOKOWANY)"
Klienci.aspx.vb:188:            btnZablokuj.Enabled = False
Klienci.aspx.vb:189:        Else
Klienci.aspx.vb:190:            btnZablokuj.Enabled = True
Klienci.aspx.vb:191:        End If
Klienci.aspx.vb:192:    End Sub
Klienci.aspx.vb:193:
Klienci.aspx.vb:194:    Private Function WalidujFormularz() As String
Klienci.aspx.vb:195:        If txtTelefon.Text.Trim() = "" Then
Klienci.aspx.vb:196:            Return "Telefon jest wymagany."
Klienci.aspx.vb:197:        End If
Klienci.aspx.vb:198:        If txtTelefon.Text.Trim().Length > 20 Then
Klienci.aspx.vb:199:            Return "Telefon moze miec maksymalnie 20 znakow."
Klienci.aspx.vb:200:        End If
Klienci.aspx.vb:201:        If ddlTyp.SelectedValue = "F" Then
Klienci.aspx.vb:202:            If txtNazwaFirmy.Text.Trim() = "" Then
Klienci.aspx.vb:203:                Return "Dla firmy podaj nazwe firmy."
Klienci.aspx.vb:204:            End If
Klienci.aspx.vb:205:            Dim nip As String = txtNIP.Text.Replace("-", "").Replace(" ", "").Trim()
Klienci.aspx.vb:206:            If nip.Length <> 10 OrElse Not IsNumeric(nip) Then
Klienci.aspx.vb:207:                Return "NIP musi miec 10 cyfr."
Klienci.aspx.vb:208:            End If
Klienci.aspx.vb:209:        End If
Klienci.aspx.vb:210:        If ddlTyp.SelectedValue = "I" AndAlso txtNumerDokumentu.Text.Trim() = "" Then
Klienci.aspx.vb:211:            Return "Dla osoby prywatnej podaj numer dokumentu."
Klienci.aspx.vb:212:        End If
Klienci.aspx.vb:213:        Return ""
Klienci.aspx.vb:214:    End Function
Klienci.aspx.vb:215:
Klienci.aspx.vb:216:    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PreRender
Klienci.aspx.vb:217:        Dim wybrany As Integer = Session("WybranyKlientId")
Klienci.aspx.vb:218:        If wybrany > 0 AndAlso gvKlienci.Rows.Count > 0 Then
Klienci.aspx.vb:219:            For i As Integer = 0 To gvKlienci.Rows.Count - 1
Klienci.aspx.vb:220:                If gvKlienci.DataKeys(i).Value = wybrany Then
Klienci.aspx.vb:221:                    gvKlienci.Rows(i).Font.Bold = True
Klienci.aspx.vb:222:                End If
Klienci.aspx.vb:223:            Next
Klienci.aspx.vb:224:        End If
Klienci.aspx.vb:225:    End Sub
Klienci.aspx.vb:226:
Klienci.aspx.vb:227:    Private Sub UstawWybor(ByVal lista As DropDownList, ByVal wartosc As String)
Klienci.aspx.vb:228:        Dim pozycja As ListItem = lista.Items.FindByValue(wartosc)
Klienci.aspx.vb:229:        If pozycja IsNot Nothing Then
Klienci.aspx.vb:230:            lista.ClearSelection()
Klienci.aspx.vb:231:            pozycja.Selected = True
Klienci.aspx.vb:232:        End If
Klienci.aspx.vb:233:    End Sub
Klienci.aspx.vb:234:
Klienci.aspx.vb:235:    Private Sub WyczyscFormularz()
Klienci.aspx.vb:236:        hfKlientId.Value = "0"
Klienci.aspx.vb:237:        txtImie.Text = ""
Klienci.aspx.vb:238:        txtNazwisko.Text = ""
Klienci.aspx.vb:239:        txtNazwaFirmy.Text = ""
Klienci.aspx.vb:240:        txtEmail.Text = ""
Klienci.aspx.vb:241:        txtTelefon.Text = ""
Klienci.aspx.vb:242:        txtNumerDokumentu.Text = ""
Klienci.aspx.vb:243:        txtNIP.Text = ""
Klienci.aspx.vb:244:        txtPowodBlokady.Text = ""
Klienci.aspx.vb:245:        ddlTyp.SelectedIndex = 0
Klienci.aspx.vb:246:        ddlProg.SelectedIndex = 0
Klienci.aspx.vb:247:        btnZablokuj.Enabled = False
Klienci.aspx.vb:248:        lblTrybFormularza.Text = "Nowy klient"
Klienci.aspx.vb:249:    End Sub
Klienci.aspx.vb:250:
Klienci.aspx.vb:251:End Class
Rezerwacja.aspx:1:<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Rezerwacja.aspx.vb" Inherits="WypozyczalniaWeb.Rezerwacja" %>
Rezerwacja.aspx:2:<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
Rezerwacja.aspx:3:<!DOCTYPE html>
Rezerwacja.aspx:4:<html xmlns="http://www.w3.org/1999/xhtml">
Rezerwacja.aspx:5:<head runat="server">
Rezerwacja.aspx:6:    <title>Wypożyczalnia - Rezerwacje</title>
Rezerwacja.aspx:7:</head>
Rezerwacja.aspx:8:<body>
Rezerwacja.aspx:9:    <form id="form1" runat="server">
Rezerwacja.aspx:10:        <asp:ScriptManager ID="smRezerwacja" runat="server" />
Rezerwacja.aspx:11:        <div class="menu">
Rezerwacja.aspx:12:            <a href="Klienci.aspx">Klienci</a> |
Rezerwacja.aspx:13:            <a href="Sprzet.aspx">Sprzęt</a> |
Rezerwacja.aspx:14:            <a href="Rezerwacja.aspx">Rezerwacje</a> |
Rezerwacja.aspx:15:            <a href="Wypozyczenie.aspx">Wydanie</a> |
Rezerwacja.aspx:16:            <a href="Zwrot.aspx">Zwrot</a> |
Rezerwacja.aspx:17:            <a href="Cennik.aspx">Cennik</a> |
Rezerwacja.aspx:18:            <a href="Faktura.aspx">Faktury</a>
Rezerwacja.aspx:19:        </div>
Rezerwacja.aspx:20:        <h2>Rezerwacje</h2>
Rezerwacja.aspx:21:        <p>
Rezerwacja.aspx:22:            Klient: <asp:Label ID="lblKlient" runat="server" Font-Bold="true" Text="(nie wybrano)" />
Rezerwacja.aspx:23:        </p>
Rezerwacja.aspx:24:        <table cellpadding="3" cellspacing="0" border="0">
Rezerwacja.aspx:25:            <tr>
Rezerwacja.aspx:26:                <td>Kategoria:</td>
Rezerwacja.aspx:27:                <td>
Rezerwacja.aspx:28:                    <asp:DropDownList ID="ddlKategoria" runat="server" AutoPostBack="true">
Rezerwacja.aspx:29:                        <asp:ListItem Value="" Text="-- wszystkie --" />
Rezerwacja.aspx:30:                        <asp:ListItem Value="BUD" Text="Budowlany" />
Rezerwacja.aspx:31:                        <asp:ListItem Value="OGR" Text="Ogrodowy" />
Rezerwacja.aspx:32:                        <asp:ListItem Value="IMP" Text="Imprezowy" />
Rezerwacja.aspx:33:                        <asp:ListItem Value="TRN" Text="Transportowy" />
Rezerwacja.aspx:34:                    </asp:DropDownList>
Rezerwacja.aspx:35:                </td>
Rezerwacja.aspx:36:            </tr>
Rezerwacja.aspx:37:            <tr>
Rezerwacja.aspx:38:                <td>Sprzęt:</td>
Rezerwacja.aspx:39:                <td>
Rezerwacja.aspx:40:                    <asp:DropDownList ID="ddlSprzet" runat="server" Width="320px" />
Rezerwacja.aspx:41:                </td>
Rezerwacja.aspx:42:            </tr>
Rezerwacja.aspx:43:            <tr>
Rezerwacja.aspx:44:                <td>Od (data, godzina):</td>
Rezerwacja.aspx:45:                <td>
Rezerwacja.aspx:46:                    <asp:TextBox ID="txtDataOd" runat="server" Width="90px" MaxLength="10" />
Rezerwacja.aspx:47:                    <ajaxToolkit:CalendarExtender ID="ceDataOd" runat="server" TargetControlID="txtDataOd" Format="yyyy-MM-dd" FirstDayOfWeek="Monday" />
Rezerwacja.aspx:48:                    <asp:TextBox ID="txtGodzinaOd" runat="server" Width="50px" MaxLength="5" Text="08:00" />
Rezerwacja.aspx:49:                </td>
Rezerwacja.aspx:50:            </tr>
Rezerwacja.aspx:51:            <tr>
Rezerwacja.aspx:52:                <td>Do (data, godzina):</td>
Rezerwacja.aspx:53:                <td>
Rezerwacja.aspx:54:                    <asp:TextBox ID="txtDataDo" runat="server" Width="90px" MaxLength="10" />
Rezerwacja.aspx:55:                    <ajaxToolkit:CalendarExtender ID="ceDataDo" runat="server" TargetControlID="txtDataDo" Format="yyyy-MM-dd" FirstDayOfWeek="Monday" />
Rezerwacja.aspx:56:                    <asp:TextBox ID="txtGodzinaDo" runat="server" Width="50px" MaxLength="5" Text="08:00" />
Rezerwacja.aspx:57:                </td>
Rezerwacja.aspx:58:            </tr>
Rezerwacja.aspx:59:            <tr>
Rezerwacja.aspx:60:                <td>Uwagi:</td>
Rezerwacja.aspx:61:                <td>
Rezerwacja.aspx:62:                    <asp:TextBox ID="txtUwagi" runat="server" TextMode="MultiLine" Rows="2" Columns="40" />
Rezerwacja.aspx:63:                </td>
Rezerwacja.aspx:64:            </tr>
Rezerwacja.aspx:65:            <tr>
Rezerwacja.aspx:66:                <td></td>
Rezerwacja.aspx:67:                <td>
Rezerwacja.aspx:68:                    <asp:Button ID="btnSprawdz" runat="server" Text="Sprawdź dostępność" />
Rezerwacja.aspx:69:                    <asp:Button ID="btnZarezerwuj" runat="server" Text="Zarezerwuj" OnClientClick="return confirm('Zapisać rezerwację?');" />
Rezerwacja.aspx:70:                </td>
Rezerwacja.aspx:71:            </tr>
Rezerwacja.aspx:72:            <tr>
Rezerwacja.aspx:73:                <td>Szacunek:</td>
Rezerwacja.aspx:74:                <td>
Rezerwacja.aspx:75:                    <asp:Label ID="lblSzacunek" runat="server" Text="-" /> zł
Rezerwacja.aspx:76:                </td>
Rezerwacja.aspx:77:            </tr>
Rezerwacja.aspx:78:        </table>
Rezerwacja.aspx:79:        <p>
Rezerwacja.aspx:80:            <asp:Label ID="lblKomunikat" runat="server" ForeColor="Red" EnableViewState="false" />
Rezerwacja.aspx:81:        </p>
Rezerwacja.aspx:82:        <h3>Rezerwacje klienta</h3>
Rezerwacja.aspx:83:        <asp:GridView ID="gvRezerwacje" runat="server" AutoGenerateColumns="false" DataKeyNames="Id"
Rezerwacja.aspx:84:            CellPadding="3" GridLines="Horizontal" EmptyDataText="Brak rezerwacji.">
Rezerwacja.aspx:85:            <Columns>
Rezerwacja.aspx:86:                <asp:CommandField ShowSelectButton="true" SelectText="wybierz" />
Rezerwacja.aspx:87:                <asp:BoundField DataField="Id" HeaderText="Nr" />
Rezerwacja.aspx:88:                <asp:BoundField DataField="Sprzet" HeaderText="Sprzęt" />
Rezerwacja.aspx:89:                <asp:BoundField DataField="DataOd" HeaderText="Od" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
Rezerwacja.aspx:90:                <asp:BoundField DataField="DataDo" HeaderText="Do" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
Rezerwacja.aspx:91:                <asp:BoundField DataField="Status" HeaderText="Status" />
Rezerwacja.aspx:92:                <asp:BoundField DataField="CenaSzacunkowa" HeaderText="Cena" DataFormatString="{0:N2}" />
Rezerwacja.aspx:93:            </Columns>
Rezerwacja.aspx:94:            <SelectedRowStyle BackColor="#FFFFCC" />
Rezerwacja.aspx:95:        </asp:GridView>
Rezerwacja.aspx:96:        <p>
Rezerwacja.aspx:97:            <asp:Button ID="btnAnuluj" runat="server" Text="Anuluj wybraną rezerwację" OnClientClick="return confirm('Anulować rezerwację?');" />
Rezerwacja.aspx:98:        </p>
Rezerwacja.aspx:99:    </form>
Rezerwacja.aspx:100:</body>
Rezerwacja.aspx:101:</html>
Rezerwacja.aspx.vb:1:Option Strict Off
Rezerwacja.aspx.vb:2:' Modul: Rezerwacja.aspx - formularz rezerwacji sprzetu | Autor: zespol IT | 2014-03-11
Rezerwacja.aspx.vb:3:' Historia:
Rezerwacja.aspx.vb:4:'   2014-09-22 JK  szacunek ceny przed zapisem (przycisk Sprawdz)
Rezerwacja.aspx.vb:5:'   2016-05-30 MW  kalendarze AjaxControlToolkit zamiast pol tekstowych
Rezerwacja.aspx.vb:6:'   2019-02-20 MW  anulowanie z listy rezerwacji klienta
Rezerwacja.aspx.vb:7:
Rezerwacja.aspx.vb:8:Imports System.Data
Rezerwacja.aspx.vb:9:Imports System.Globalization
Rezerwacja.aspx.vb:10:Imports Wypozyczalnia.Core
Rezerwacja.aspx.vb:11:Imports Wypozyczalnia.Core.Model
Rezerwacja.aspx.vb:12:
Rezerwacja.aspx.vb:13:Partial Class Rezerwacja
Rezerwacja.aspx.vb:14:    Inherits System.Web.UI.Page
Rezerwacja.aspx.vb:15:
Rezerwacja.aspx.vb:16:    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Rezerwacja))
Rezerwacja.aspx.vb:17:
Rezerwacja.aspx.vb:18:    Private Shared ReadOnly FormatyTerminu() As String = {"yyyy-MM-dd HH:mm", "yyyy-MM-dd H:mm", "dd.MM.yyyy HH:mm", "dd.MM.yyyy H:mm"}
Rezerwacja.aspx.vb:19:
Rezerwacja.aspx.vb:20:    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
Rezerwacja.aspx.vb:21:        If Not IsPostBack Then
Rezerwacja.aspx.vb:22:            PokazKlienta()
Rezerwacja.aspx.vb:23:            WypelnijSprzet()
Rezerwacja.aspx.vb:24:            WybierzSprzetZAdresu()
Rezerwacja.aspx.vb:25:            txtDataOd.Text = Date.Today.AddDays(1).ToString("yyyy-MM-dd")
Rezerwacja.aspx.vb:26:            txtDataDo.Text = Date.Today.AddDays(2).ToString("yyyy-MM-dd")
Rezerwacja.aspx.vb:27:            If Session("SzacunekRezerwacji") IsNot Nothing Then
Rezerwacja.aspx.vb:28:                lblSzacunek.Text = Session("SzacunekRezerwacji")
Rezerwacja.aspx.vb:29:            End If
Rezerwacja.aspx.vb:30:            PokazRezerwacje()
Rezerwacja.aspx.vb:31:        End If
Rezerwacja.aspx.vb:32:    End Sub
Rezerwacja.aspx.vb:33:
Rezerwacja.aspx.vb:34:    Protected Sub ddlKategoria_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlKategoria.SelectedIndexChanged
Rezerwacja.aspx.vb:35:        WypelnijSprzet()
Rezerwacja.aspx.vb:36:        lblSzacunek.Text = "-"
Rezerwacja.aspx.vb:37:        Session("SzacunekRezerwacji") = Nothing
Rezerwacja.aspx.vb:38:    End Sub
Rezerwacja.aspx.vb:39:
Rezerwacja.aspx.vb:40:    Protected Sub btnSprawdz_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSprawdz.Click
Rezerwacja.aspx.vb:41:        Dim dataOd As Date
Rezerwacja.aspx.vb:42:        Dim dataDo As Date
Rezerwacja.aspx.vb:43:        If ddlSprzet.SelectedValue = "" Then
Rezerwacja.aspx.vb:44:            PokazBlad("Wybierz sprzet.")
Rezerwacja.aspx.vb:45:            Return
Rezerwacja.aspx.vb:46:        End If
Rezerwacja.aspx.vb:47:        If Not ParsujTermin(txtDataOd.Text, txtGodzinaOd.Text, dataOd) OrElse Not ParsujTermin(txtDataDo.Text, txtGodzinaDo.Text, dataDo) Then
Rezerwacja.aspx.vb:48:            PokazBlad("Niepoprawny termin. Format: rrrr-mm-dd gg:mm")
Rezerwacja.aspx.vb:49:            Return
Rezerwacja.aspx.vb:50:        End If
Rezerwacja.aspx.vb:51:        If dataDo <= dataOd Then
Rezerwacja.aspx.vb:52:            PokazBlad("Data zakonczenia musi byc pozniejsza niz data rozpoczecia.")
Rezerwacja.aspx.vb:53:            Return
Rezerwacja.aspx.vb:54:        End If
Rezerwacja.aspx.vb:55:
Rezerwacja.aspx.vb:56:        Dim sprzetId As Integer = ddlSprzet.SelectedValue
Rezerwacja.aspx.vb:57:        Dim mgr As New EquipmentManager()
Rezerwacja.aspx.vb:58:        If Not mgr.SprawdzDostepnosc(sprzetId, dataOd, dataDo) Then
Rezerwacja.aspx.vb:59:            PokazBlad("Sprzet niedostepny w wybranym terminie: " & OpisKolizji(mgr.KolizjeWTerminie(sprzetId, dataOd, dataDo)))
Rezerwacja.aspx.vb:60:            lblSzacunek.Text = "-"
Rezerwacja.aspx.vb:61:            Session("SzacunekRezerwacji") = Nothing
Rezerwacja.aspx.vb:62:            Return
Rezerwacja.aspx.vb:63:        End If
Rezerwacja.aspx.vb:64:
Rezerwacja.aspx.vb:65:        Dim sprzet As Sprzet = mgr.PobierzSprzet(sprzetId)
Rezerwacja.aspx.vb:66:        Dim klientId As Integer = Session("WybranyKlientId")
Rezerwacja.aspx.vb:67:        Dim prog As Integer = 0
Rezerwacja.aspx.vb:68:        If klientId > 0 Then
Rezerwacja.aspx.vb:69:            prog = New CustomerManager().ProgRabatu(klientId)
Rezerwacja.aspx.vb:70:        End If
Rezerwacja.aspx.vb:71:        Dim szacunek As Decimal = ObliczSzacunek(sprzet, dataOd, dataDo, prog)
Rezerwacja.aspx.vb:72:        lblSzacunek.Text = szacunek.ToString("N2")
Rezerwacja.aspx.vb:73:        Session("SzacunekRezerwacji") = szacunek
Rezerwacja.aspx.vb:74:        PokazInfo("Sprzet dostepny w wybranym terminie.")
Rezerwacja.aspx.vb:75:    End Sub
Rezerwacja.aspx.vb:76:
Rezerwacja.aspx.vb:77:    Protected Sub btnZarezerwuj_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZarezerwuj.Click
Rezerwacja.aspx.vb:78:        Dim klientId As Integer = Session("WybranyKlientId")
Rezerwacja.aspx.vb:79:        If klientId = 0 Then
Rezerwacja.aspx.vb:80:            PokazBlad("Najpierw wybierz klienta na stronie Klienci.")
Rezerwacja.aspx.vb:81:            Return
Rezerwacja.aspx.vb:82:        End If
Rezerwacja.aspx.vb:83:        If ddlSprzet.SelectedValue = "" Then
Rezerwacja.aspx.vb:84:            PokazBlad("Wybierz sprzet.")
Rezerwacja.aspx.vb:85:            Return
Rezerwacja.aspx.vb:86:        End If
Rezerwacja.aspx.vb:87:
Rezerwacja.aspx.vb:88:        Dim dataOd As Date
Rezerwacja.aspx.vb:89:        Dim dataDo As Date
Rezerwacja.aspx.vb:90:        If Not ParsujTermin(txtDataOd.Text, txtGodzinaOd.Text, dataOd) Then
Rezerwacja.aspx.vb:91:            PokazBlad("Niepoprawna data rozpoczecia.")
Rezerwacja.aspx.vb:92:            Return
Rezerwacja.aspx.vb:93:        End If
Rezerwacja.aspx.vb:94:        If Not ParsujTermin(txtDataDo.Text, txtGodzinaDo.Text, dataDo) Then
Rezerwacja.aspx.vb:95:            PokazBlad("Niepoprawna data zakonczenia.")
Rezerwacja.aspx.vb:96:            Return
Rezerwacja.aspx.vb:97:        End If
Rezerwacja.aspx.vb:98:        If dataOd < Date.Now.AddMinutes(-30) Then
Rezerwacja.aspx.vb:99:            PokazBlad("Nie mozna rezerwowac wstecz.")
Rezerwacja.aspx.vb:100:            Return
Rezerwacja.aspx.vb:101:        End If
Rezerwacja.aspx.vb:102:        If dataDo <= dataOd Then
Rezerwacja.aspx.vb:103:            PokazBlad("Data zakonczenia musi byc pozniejsza niz data rozpoczecia.")
Rezerwacja.aspx.vb:104:            Return
Rezerwacja.aspx.vb:105:        End If
Rezerwacja.aspx.vb:106:        If dataOd.Hour < 7 OrElse dataOd.Hour > 18 Then
Rezerwacja.aspx.vb:107:            PokazBlad("Wydania sprzetu tylko w godzinach 7:00 - 18:00.")
Rezerwacja.aspx.vb:108:            Return
Rezerwacja.aspx.vb:109:        End If
Rezerwacja.aspx.vb:110:        If txtUwagi.Text.Length > 500 Then
Rezerwacja.aspx.vb:111:            txtUwagi.Text = txtUwagi.Text.Substring(0, 500)
Rezerwacja.aspx.vb:112:        End If
Rezerwacja.aspx.vb:113:
Rezerwacja.aspx.vb:114:        Dim rezerwacje As New ReservationManager()
Rezerwacja.aspx.vb:115:        Try
Rezerwacja.aspx.vb:116:            Dim noweId As Integer = rezerwacje.UtworzRezerwacje(klientId, ddlSprzet.SelectedValue, dataOd, dataDo, txtUwagi.Text)
Rezerwacja.aspx.vb:117:            Dim nowa = rezerwacje.PobierzRezerwacje(noweId)
Rezerwacja.aspx.vb:118:            PokazInfo("Zapisano rezerwacje nr " & noweId & ". Cena: " & nowa.CenaSzacunkowa.ToString("N2") & " zl.")
Rezerwacja.aspx.vb:119:            txtUwagi.Text = ""
Rezerwacja.aspx.vb:120:            PokazRezerwacje()
Rezerwacja.aspx.vb:121:        Catch ex As BladBiznesowyException
Rezerwacja.aspx.vb:122:            PokazBlad(ex.Message)
Rezerwacja.aspx.vb:123:        Catch ex As ArgumentException
Rezerwacja.aspx.vb:124:            PokazBlad(ex.Message)
Rezerwacja.aspx.vb:125:        Catch ex As Exception
Rezerwacja.aspx.vb:126:            Log.Error("Blad zapisu rezerwacji, klient " & klientId, ex)
Rezerwacja.aspx.vb:127:            PokazBlad("Nie udalo sie zapisac rezerwacji. Sprobuj ponownie.")
Rezerwacja.aspx.vb:128:        End Try
Rezerwacja.aspx.vb:129:    End Sub
Rezerwacja.aspx.vb:130:
Rezerwacja.aspx.vb:131:    Protected Sub gvRezerwacje_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gvRezerwacje.SelectedIndexChanged
Rezerwacja.aspx.vb:132:        If gvRezerwacje.SelectedIndex < 0 Then
Rezerwacja.aspx.vb:133:            Return
Rezerwacja.aspx.vb:134:        End If
Rezerwacja.aspx.vb:135:        ViewState("WybranaRezerwacja") = gvRezerwacje.SelectedDataKey.Value
Rezerwacja.aspx.vb:136:        Dim rezerwacje As New ReservationManager()
Rezerwacja.aspx.vb:137:        Dim wybrana = rezerwacje.PobierzRezerwacje(gvRezerwacje.SelectedDataKey.Value)
Rezerwacja.aspx.vb:138:        If wybrana Is Nothing Then
Rezerwacja.aspx.vb:139:            PokazBlad("Rezerwacja nie istnieje (odswiez liste).")
Rezerwacja.aspx.vb:140:            btnAnuluj.Enabled = False
Rezerwacja.aspx.vb:141:            Return
Rezerwacja.aspx.vb:142:        End If
Rezerwacja.aspx.vb:143:        btnAnuluj.Enabled = wybrana.CzyAktywna
Rezerwacja.aspx.vb:144:        Dim opis As String = rezerwacje.OpisRezerwacji(wybrana.Id)
Rezerwacja.aspx.vb:145:        If Not String.IsNullOrEmpty(wybrana.Uwagi) Then
Rezerwacja.aspx.vb:146:            opis = opis & ". Uwagi: " & wybrana.Uwagi
Rezerwacja.aspx.vb:147:        End If
Rezerwacja.aspx.vb:148:        PokazInfo(opis)
Rezerwacja.aspx.vb:149:    End Sub
Rezerwacja.aspx.vb:150:
Rezerwacja.aspx.vb:151:    Protected Sub gvRezerwacje_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvRezerwacje.RowDataBound
Rezerwacja.aspx.vb:152:        If e.Row.RowType <> DataControlRowType.DataRow Then
Rezerwacja.aspx.vb:153:            Return
Rezerwacja.aspx.vb:154:        End If
Rezerwacja.aspx.vb:155:        Dim wiersz As DataRowView = CType(e.Row.DataItem, DataRowView)
Rezerwacja.aspx.vb:156:        Dim komorkaStatusu As TableCell = e.Row.Cells(5)
Rezerwacja.aspx.vb:157:        Select Case CStr(wiersz("Status"))
Rezerwacja.aspx.vb:158:            Case "N"
Rezerwacja.aspx.vb:159:                komorkaStatusu.Text = "nowa"
Rezerwacja.aspx.vb:160:            Case "P"
Rezerwacja.aspx.vb:161:                komorkaStatusu.Text = "potwierdzona"
Rezerwacja.aspx.vb:162:                komorkaStatusu.Font.Bold = True
Rezerwacja.aspx.vb:163:            Case "A"
Rezerwacja.aspx.vb:164:                komorkaStatusu.Text = "anulowana"
Rezerwacja.aspx.vb:165:                e.Row.CssClass = "wyszarzony"
Rezerwacja.aspx.vb:166:            Case "Z"
Rezerwacja.aspx.vb:167:                komorkaStatusu.Text = "zrealizowana"
Rezerwacja.aspx.vb:168:                e.Row.CssClass = "wyszarzony"
Rezerwacja.aspx.vb:169:        End Select
Rezerwacja.aspx.vb:170:        If CDate(wiersz("DataDo")) < Date.Now AndAlso CStr(wiersz("Status")) = "N" Then
Rezerwacja.aspx.vb:171:            komorkaStatusu.Text = komorkaStatusu.Text & " (termin minal)"
Rezerwacja.aspx.vb:172:        End If
Rezerwacja.aspx.vb:173:    End Sub
Rezerwacja.aspx.vb:174:
Rezerwacja.aspx.vb:175:    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PreRender
Rezerwacja.aspx.vb:176:        ceDataOd.StartDate = Date.Today
Rezerwacja.aspx.vb:177:        ceDataDo.StartDate = Date.Today
Rezerwacja.aspx.vb:178:        btnSprawdz.Enabled = ddlSprzet.Items.Count > 1
Rezerwacja.aspx.vb:179:    End Sub
Rezerwacja.aspx.vb:180:
Rezerwacja.aspx.vb:181:    ' Rezerwacja.aspx?sprzet=<Id> - link "rezerwuj" z katalogu sprzetu
Rezerwacja.aspx.vb:182:    Private Sub WybierzSprzetZAdresu()
Rezerwacja.aspx.vb:183:        Dim parametr As String = Request.QueryString("sprzet")
Rezerwacja.aspx.vb:184:        Dim sprzetId As Integer
Rezerwacja.aspx.vb:185:        If String.IsNullOrEmpty(parametr) OrElse Not Integer.TryParse(parametr, sprzetId) Then
Rezerwacja.aspx.vb:186:            Return
Rezerwacja.aspx.vb:187:        End If
Rezerwacja.aspx.vb:188:        Dim sprzet As Sprzet = New EquipmentManager().PobierzSprzet(sprzetId)
Rezerwacja.aspx.vb:189:        If sprzet Is Nothing Then
Rezerwacja.aspx.vb:190:            Return
Rezerwacja.aspx.vb:191:        End If
Rezerwacja.aspx.vb:192:        Dim pozycjaKategorii As ListItem = ddlKategoria.Items.FindByValue(sprzet.Kategoria)
Rezerwacja.aspx.vb:193:        If pozycjaKategorii IsNot Nothing Then
Rezerwacja.aspx.vb:194:            ddlKategoria.ClearSelection()
Rezerwacja.aspx.vb:195:            pozycjaKategorii.Selected = True
Rezerwacja.aspx.vb:196:            WypelnijSprzet()
Rezerwacja.aspx.vb:197:        End If
Rezerwacja.aspx.vb:198:        Dim pozycja As ListItem = ddlSprzet.Items.FindByValue(sprzetId.ToString())
Rezerwacja.aspx.vb:199:        If pozycja IsNot Nothing Then
Rezerwacja.aspx.vb:200:            ddlSprzet.ClearSelection()
Rezerwacja.aspx.vb:201:            pozycja.Selected = True
Rezerwacja.aspx.vb:202:        End If
Rezerwacja.aspx.vb:203:    End Sub
Rezerwacja.aspx.vb:204:
Rezerwacja.aspx.vb:205:    Protected Sub btnAnuluj_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAnuluj.Click
Rezerwacja.aspx.vb:206:        If ViewState("WybranaRezerwacja") Is Nothing Then
Rezerwacja.aspx.vb:207:            PokazBlad("Wybierz rezerwacje z listy.")
Rezerwacja.aspx.vb:208:            Return
Rezerwacja.aspx.vb:209:        End If
Rezerwacja.aspx.vb:210:        Dim rezerwacjaId As Integer = ViewState("WybranaRezerwacja")
Rezerwacja.aspx.vb:211:        Try
Rezerwacja.aspx.vb:212:            Dim rezerwacje As New ReservationManager()
Rezerwacja.aspx.vb:213:            rezerwacje.Anuluj(rezerwacjaId)
Rezerwacja.aspx.vb:214:            PokazInfo("Rezerwacja nr " & rezerwacjaId & " anulowana.")
Rezerwacja.aspx.vb:215:            ViewState("WybranaRezerwacja") = Nothing
Rezerwacja.aspx.vb:216:            gvRezerwacje.SelectedIndex = -1
Rezerwacja.aspx.vb:217:            PokazRezerwacje()
Rezerwacja.aspx.vb:218:        Catch ex As BladBiznesowyException
Rezerwacja.aspx.vb:219:            PokazBlad(ex.Message)
Rezerwacja.aspx.vb:220:        End Try
Rezerwacja.aspx.vb:221:    End Sub
Rezerwacja.aspx.vb:222:
Rezerwacja.aspx.vb:223:    Private Sub PokazKlienta()
Rezerwacja.aspx.vb:224:        Dim klientId As Integer = Session("WybranyKlientId")
Rezerwacja.aspx.vb:225:        If klientId = 0 Then
Rezerwacja.aspx.vb:226:            lblKlient.Text = "(nie wybrano - wybierz klienta na stronie Klienci)"
Rezerwacja.aspx.vb:227:            btnZarezerwuj.Enabled = False
Rezerwacja.aspx.vb:228:            Return
Rezerwacja.aspx.vb:229:        End If
Rezerwacja.aspx.vb:230:        Dim klient As Klient = New CustomerManager().PobierzKlienta(klientId)
Rezerwacja.aspx.vb:231:        If klient Is Nothing Then
Rezerwacja.aspx.vb:232:            lblKlient.Text = "(nieznany klient " & klientId & ")"
Rezerwacja.aspx.vb:233:            btnZarezerwuj.Enabled = False
Rezerwacja.aspx.vb:234:            Return
Rezerwacja.aspx.vb:235:        End If
Rezerwacja.aspx.vb:236:        If klient.Typ = "F" AndAlso Not String.IsNullOrEmpty(klient.NazwaFirmy) Then
Rezerwacja.aspx.vb:237:            lblKlient.Text = Server.HtmlEncode(klient.NazwaFirmy & " (" & klient.Imie & " " & klient.Nazwisko & ")")
Rezerwacja.aspx.vb:238:        Else
Rezerwacja.aspx.vb:239:            lblKlient.Text = Server.HtmlEncode(klient.Imie & " " & klient.Nazwisko)
Rezerwacja.aspx.vb:240:        End If
Rezerwacja.aspx.vb:241:        If klient.Zablokowany Then
Rezerwacja.aspx.vb:242:            lblKlient.Text = lblKlient.Text & " - ZABLOKOWANY"
Rezerwacja.aspx.vb:243:            lblKlient.CssClass = "blad"
Rezerwacja.aspx.vb:244:        End If
Rezerwacja.aspx.vb:245:        btnZarezerwuj.Enabled = True
Rezerwacja.aspx.vb:246:    End Sub
Rezerwacja.aspx.vb:247:
Rezerwacja.aspx.vb:248:    Private Sub WypelnijSprzet()
Rezerwacja.aspx.vb:249:        ddlSprzet.Items.Clear()
Rezerwacja.aspx.vb:250:        ddlSprzet.Items.Add(New ListItem("-- wybierz --", ""))
Rezerwacja.aspx.vb:251:        Dim mgr As New EquipmentManager()
Rezerwacja.aspx.vb:252:        For Each s In mgr.ListaWKategorii(ddlKategoria.SelectedValue)
Rezerwacja.aspx.vb:253:            Dim opis As String = s.NumerInwentarzowy & " - " & s.Nazwa & " (" & s.StawkaDobowa.ToString("N2") & " zl/doba)"
Rezerwacja.aspx.vb:254:            If s.Status = "S" Then
Rezerwacja.aspx.vb:255:                opis = opis & " [serwis]"
Rezerwacja.aspx.vb:256:            End If
Rezerwacja.aspx.vb:257:            ddlSprzet.Items.Add(New ListItem(opis, s.Id.ToString()))
Rezerwacja.aspx.vb:258:        Next
Rezerwacja.aspx.vb:259:    End Sub
Rezerwacja.aspx.vb:260:
Rezerwacja.aspx.vb:261:    Private Sub PokazRezerwacje()
Rezerwacja.aspx.vb:262:        Dim klientId As Integer = Session("WybranyKlientId")
Rezerwacja.aspx.vb:263:        If klientId = 0 Then
Rezerwacja.aspx.vb:264:            gvRezerwacje.DataSource = Nothing
Rezerwacja.aspx.vb:265:            gvRezerwacje.DataBind()
Rezerwacja.aspx.vb:266:            btnAnuluj.Enabled = False
Rezerwacja.aspx.vb:267:            Return
Rezerwacja.aspx.vb:268:        End If
Rezerwacja.aspx.vb:269:        Dim tabela As DataTable = New ReservationManager().RezerwacjeKlienta(klientId)
Rezerwacja.aspx.vb:270:        gvRezerwacje.DataSource = tabela
Rezerwacja.aspx.vb:271:        gvRezerwacje.DataBind()
Rezerwacja.aspx.vb:272:        btnAnuluj.Enabled = tabela.Rows.Count > 0
Rezerwacja.aspx.vb:273:    End Sub
Rezerwacja.aspx.vb:274:
Rezerwacja.aspx.vb:275:    Private Function OpisKolizji(ByVal kolizje As DataTable) As String
Rezerwacja.aspx.vb:276:        Dim opisy As New List(Of String)()
Rezerwacja.aspx.vb:277:        For Each wiersz As DataRow In kolizje.Rows
Rezerwacja.aspx.vb:278:            Select Case CStr(wiersz("Zrodlo"))
Rezerwacja.aspx.vb:279:                Case "R"
Rezerwacja.aspx.vb:280:                    opisy.Add("rezerwacja nr " & wiersz("Id") & " (" & CDate(wiersz("DataOd")).ToString("yyyy-MM-dd HH:mm") & _
Rezerwacja.aspx.vb:281:                              " - " & CDate(wiersz("DataDo")).ToString("yyyy-MM-dd HH:mm") & ")")
Rezerwacja.aspx.vb:282:                Case "W"
Rezerwacja.aspx.vb:283:                    opisy.Add("wypozyczenie nr " & wiersz("Id") & " do " & CDate(wiersz("DataDo")).ToString("yyyy-MM-dd HH:mm"))
Rezerwacja.aspx.vb:284:                Case "S"
Rezerwacja.aspx.vb:285:                    opisy.Add("sprzet w serwisie albo wycofany")
Rezerwacja.aspx.vb:286:                Case Else
Rezerwacja.aspx.vb:287:                    opisy.Add("kolizja " & wiersz("Zrodlo"))
Rezerwacja.aspx.vb:288:            End Select
Rezerwacja.aspx.vb:289:        Next
Rezerwacja.aspx.vb:290:        Return String.Join("; ", opisy.ToArray())
Rezerwacja.aspx.vb:291:    End Function
Rezerwacja.aspx.vb:292:
Rezerwacja.aspx.vb:293:    Private Function ParsujTermin(ByVal data As String, ByVal godzina As String, ByRef wynik As Date) As Boolean
Rezerwacja.aspx.vb:294:        If String.IsNullOrEmpty(data) Then
Rezerwacja.aspx.vb:295:            Return False
Rezerwacja.aspx.vb:296:        End If
Rezerwacja.aspx.vb:297:        If String.IsNullOrEmpty(godzina) Then
Rezerwacja.aspx.vb:298:            godzina = "08:00"
Rezerwacja.aspx.vb:299:        End If
Rezerwacja.aspx.vb:300:        Return Date.TryParseExact(data.Trim() & " " & godzina.Trim(), FormatyTerminu, CultureInfo.InvariantCulture, DateTimeStyles.None, wynik)
Rezerwacja.aspx.vb:301:    End Function
Rezerwacja.aspx.vb:302:
Rezerwacja.aspx.vb:303:    Private Sub PokazBlad(ByVal tekst As String)
Rezerwacja.aspx.vb:304:        lblKomunikat.CssClass = "blad"
Rezerwacja.aspx.vb:305:        lblKomunikat.Text = Server.HtmlEncode(tekst)
Rezerwacja.aspx.vb:306:    End Sub
Rezerwacja.aspx.vb:307:
Rezerwacja.aspx.vb:308:    Private Sub PokazInfo(ByVal tekst As String)
Rezerwacja.aspx.vb:309:        lblKomunikat.CssClass = "info"
Rezerwacja.aspx.vb:310:        lblKomunikat.Text = Server.HtmlEncode(tekst)
Rezerwacja.aspx.vb:311:    End Sub
Rezerwacja.aspx.vb:312:
Rezerwacja.aspx.vb:313:    Private Function ObliczSzacunek(ByVal sprzet As Sprzet, ByVal dataOd As Date, ByVal dataDo As Date, ByVal prog As Integer) As Decimal
Rezerwacja.aspx.vb:314:        Dim dni As Long = DateDiff(DateInterval.Day, dataOd.Date, dataDo.Date)
Rezerwacja.aspx.vb:315:        If dni < 1 Then dni = 1
Rezerwacja.aspx.vb:316:        Dim kwota As Decimal = dni * sprzet.StawkaDobowa
Rezerwacja.aspx.vb:317:        kwota = kwota * (100D - prog * 2.5D) / 100D
Rezerwacja.aspx.vb:318:        Return Math.Round(kwota, 2, MidpointRounding.AwayFromZero)
Rezerwacja.aspx.vb:319:    End Function
Rezerwacja.aspx.vb:320:
Rezerwacja.aspx.vb:321:End Class
Sprzet.aspx:1:<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Sprzet.aspx.vb" Inherits="WypozyczalniaWeb.Sprzet" %>
Sprzet.aspx:2:<!DOCTYPE html>
Sprzet.aspx:3:<html xmlns="http://www.w3.org/1999/xhtml">
Sprzet.aspx:4:<head runat="server">
Sprzet.aspx:5:    <title>Wypożyczalnia - Katalog sprzętu</title>
Sprzet.aspx:6:</head>
Sprzet.aspx:7:<body>
Sprzet.aspx:8:    <form id="form1" runat="server">
Sprzet.aspx:9:        <div class="menu">
Sprzet.aspx:10:            <a href="Klienci.aspx">Klienci</a> |
Sprzet.aspx:11:            <a href="Sprzet.aspx">Sprzęt</a> |
Sprzet.aspx:12:            <a href="Rezerwacja.aspx">Rezerwacje</a> |
Sprzet.aspx:13:            <a href="Wypozyczenie.aspx">Wydanie</a> |
Sprzet.aspx:14:            <a href="Zwrot.aspx">Zwrot</a> |
Sprzet.aspx:15:            <a href="Cennik.aspx">Cennik</a> |
Sprzet.aspx:16:            <a href="Faktura.aspx">Faktury</a>
Sprzet.aspx:17:        </div>
Sprzet.aspx:18:        <h2>Katalog sprzętu</h2>
Sprzet.aspx:19:        <p>
Sprzet.aspx:20:            Kategoria:
Sprzet.aspx:21:            <asp:DropDownList ID="ddlKategoria" runat="server" AutoPostBack="true">
Sprzet.aspx:22:                <asp:ListItem Value="" Text="-- wszystkie --" />
Sprzet.aspx:23:                <asp:ListItem Value="BUD" Text="Budowlany" />
Sprzet.aspx:24:                <asp:ListItem Value="OGR" Text="Ogrodowy" />
Sprzet.aspx:25:                <asp:ListItem Value="IMP" Text="Imprezowy" />
Sprzet.aspx:26:                <asp:ListItem Value="TRN" Text="Transportowy" />
Sprzet.aspx:27:            </asp:DropDownList>
Sprzet.aspx:28:        </p>
Sprzet.aspx:29:        <asp:GridView ID="gvSprzet" runat="server" AutoGenerateColumns="false" DataKeyNames="Id"
Sprzet.aspx:30:            CellPadding="3" GridLines="Horizontal" EmptyDataText="Brak sprzętu w kategorii.">
Sprzet.aspx:31:            <Columns>
Sprzet.aspx:32:                <asp:CommandField ShowSelectButton="true" SelectText="edytuj" />
Sprzet.aspx:33:                <asp:BoundField DataField="NumerInwentarzowy" HeaderText="Nr inw." />
Sprzet.aspx:34:                <asp:BoundField DataField="Nazwa" HeaderText="Nazwa" />
Sprzet.aspx:35:                <asp:BoundField DataField="Kategoria" HeaderText="Kat." />
Sprzet.aspx:36:                <asp:BoundField DataField="StawkaDobowa" HeaderText="Stawka / doba" DataFormatString="{0:N2}" />
Sprzet.aspx:37:                <asp:BoundField DataField="Kaucja" HeaderText="Kaucja" DataFormatString="{0:N2}" />
Sprzet.aspx:38:                <asp:BoundField DataField="Status" HeaderText="Status" />
Sprzet.aspx:39:                <asp:HyperLinkField Text="rezerwuj" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="Rezerwacja.aspx?sprzet={0}" />
Sprzet.aspx:40:            </Columns>
Sprzet.aspx:41:            <SelectedRowStyle BackColor="#FFFFCC" />
Sprzet.aspx:42:        </asp:GridView>
Sprzet.aspx:43:        <h3>Edycja sprzętu</h3>
Sprzet.aspx:44:        <asp:HiddenField ID="hfSprzetId" runat="server" Value="0" />
Sprzet.aspx:45:        <table cellpadding="3" cellspacing="0" border="0">
Sprzet.aspx:46:            <tr><td>Numer inwentarzowy:</td><td><asp:TextBox ID="txtNumer" runat="server" MaxLength="20" Width="120px" /></td></tr>
Sprzet.aspx:47:            <tr><td>Nazwa:</td><td><asp:TextBox ID="txtNazwa" runat="server" MaxLength="100" Width="320px" /></td></tr>
Sprzet.aspx:48:            <tr>
Sprzet.aspx:49:                <td>Kategoria:</td>
Sprzet.aspx:50:                <td>
Sprzet.aspx:51:                    <asp:DropDownList ID="ddlKategoriaEdycja" runat="server">
Sprzet.aspx:52:                        <asp:ListItem Value="BUD" Text="Budowlany" />
Sprzet.aspx:53:                        <asp:ListItem Value="OGR" Text="Ogrodowy" />
Sprzet.aspx:54:                        <asp:ListItem Value="IMP" Text="Imprezowy" />
Sprzet.aspx:55:                        <asp:ListItem Value="TRN" Text="Transportowy" />
Sprzet.aspx:56:                    </asp:DropDownList>
Sprzet.aspx:57:                </td>
Sprzet.aspx:58:            </tr>
Sprzet.aspx:59:            <tr><td>Stawka dobowa (zł):</td><td><asp:TextBox ID="txtStawka" runat="server" Width="80px" /></td></tr>
Sprzet.aspx:60:            <tr><td>Kaucja (zł):</td><td><asp:TextBox ID="txtKaucja" runat="server" Width="80px" /></td></tr>
Sprzet.aspx:61:            <tr><td>Status:</td><td><asp:Label ID="lblStatus" runat="server" Text="-" /></td></tr>
Sprzet.aspx:62:        </table>
Sprzet.aspx:63:        <p>
Sprzet.aspx:64:            <asp:Button ID="btnNowy" runat="server" Text="Nowy" CausesValidation="false" />
Sprzet.aspx:65:            <asp:Button ID="btnZapisz" runat="server" Text="Zapisz" />
Sprzet.aspx:66:            <asp:Button ID="btnSerwis" runat="server" Text="Do serwisu" OnClientClick="return confirm('Przekazać sprzęt do serwisu?');" />
Sprzet.aspx:67:        </p>
Sprzet.aspx:68:        <p>
Sprzet.aspx:69:            <asp:Label ID="lblKomunikat" runat="server" ForeColor="Red" EnableViewState="false" />
Sprzet.aspx:70:        </p>
Sprzet.aspx:71:        <h3>Historia wypożyczeń</h3>
Sprzet.aspx:72:        <asp:GridView ID="gvHistoria" runat="server" AutoGenerateColumns="false" CellPadding="3" GridLines="Horizontal"
Sprzet.aspx:73:            EmptyDataText="Brak wypożyczeń.">
Sprzet.aspx:74:            <Columns>
Sprzet.aspx:75:                <asp:BoundField DataField="Id" HeaderText="Nr" />
Sprzet.aspx:76:                <asp:BoundField DataField="Klient" HeaderText="Klient" />
Sprzet.aspx:77:                <asp:BoundField DataField="DataWydania" HeaderText="Wydano" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
Sprzet.aspx:78:                <asp:BoundField DataField="PlanowanyZwrot" HeaderText="Plan. zwrot" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
Sprzet.aspx:79:                <asp:BoundField DataField="DataZwrotu" HeaderText="Zwrot" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
Sprzet.aspx:80:            </Columns>
Sprzet.aspx:81:        </asp:GridView>
Sprzet.aspx:82:    </form>
Sprzet.aspx:83:</body>
Sprzet.aspx:84:</html>
Sprzet.aspx.vb:1:' Modul: Sprzet.aspx - katalog sprzetu, edycja, serwis | Autor: zespol IT | 2014-03-11
Sprzet.aspx.vb:2:' Historia:
Sprzet.aspx.vb:3:'   2015-06-02 MW  pole kaucji w formularzu
Sprzet.aspx.vb:4:'   2018-01-15 PT  historia wypozyczen pod formularzem
Sprzet.aspx.vb:5:
Sprzet.aspx.vb:6:Imports System.Data
Sprzet.aspx.vb:7:Imports System.Globalization
Sprzet.aspx.vb:8:Imports Wypozyczalnia.Core
Sprzet.aspx.vb:9:Imports SprzetModel = Wypozyczalnia.Core.Model.Sprzet
Sprzet.aspx.vb:10:
Sprzet.aspx.vb:11:Partial Class Sprzet
Sprzet.aspx.vb:12:    Inherits System.Web.UI.Page
Sprzet.aspx.vb:13:
Sprzet.aspx.vb:14:    Protected WithEvents form1 As Global.System.Web.UI.HtmlControls.HtmlForm
Sprzet.aspx.vb:15:    Protected WithEvents ddlKategoria As Global.System.Web.UI.WebControls.DropDownList
Sprzet.aspx.vb:16:    Protected WithEvents gvSprzet As Global.System.Web.UI.WebControls.GridView
Sprzet.aspx.vb:17:    Protected WithEvents hfSprzetId As Global.System.Web.UI.WebControls.HiddenField
Sprzet.aspx.vb:18:    Protected WithEvents txtNumer As Global.System.Web.UI.WebControls.TextBox
Sprzet.aspx.vb:19:    Protected WithEvents txtNazwa As Global.System.Web.UI.WebControls.TextBox
Sprzet.aspx.vb:20:    Protected WithEvents ddlKategoriaEdycja As Global.System.Web.UI.WebControls.DropDownList
Sprzet.aspx.vb:21:    Protected WithEvents txtStawka As Global.System.Web.UI.WebControls.TextBox
Sprzet.aspx.vb:22:    Protected WithEvents txtKaucja As Global.System.Web.UI.WebControls.TextBox
Sprzet.aspx.vb:23:    Protected WithEvents lblStatus As Global.System.Web.UI.WebControls.Label
Sprzet.aspx.vb:24:    Protected WithEvents btnNowy As Global.System.Web.UI.WebControls.Button
Sprzet.aspx.vb:25:    Protected WithEvents btnZapisz As Global.System.Web.UI.WebControls.Button
Sprzet.aspx.vb:26:    Protected WithEvents btnSerwis As Global.System.Web.UI.WebControls.Button
Sprzet.aspx.vb:27:    Protected WithEvents lblKomunikat As Global.System.Web.UI.WebControls.Label
Sprzet.aspx.vb:28:    Protected WithEvents gvHistoria As Global.System.Web.UI.WebControls.GridView
Sprzet.aspx.vb:29:
Sprzet.aspx.vb:30:    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Sprzet))
Sprzet.aspx.vb:31:
Sprzet.aspx.vb:32:    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
Sprzet.aspx.vb:33:        If Not IsPostBack Then
Sprzet.aspx.vb:34:            PokazListe()
Sprzet.aspx.vb:35:            WyczyscFormularz()
Sprzet.aspx.vb:36:        End If
Sprzet.aspx.vb:37:    End Sub
Sprzet.aspx.vb:38:
Sprzet.aspx.vb:39:    Protected Sub ddlKategoria_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlKategoria.SelectedIndexChanged
Sprzet.aspx.vb:40:        gvSprzet.SelectedIndex = -1
Sprzet.aspx.vb:41:        PokazListe()
Sprzet.aspx.vb:42:        WyczyscFormularz()
Sprzet.aspx.vb:43:    End Sub
Sprzet.aspx.vb:44:
Sprzet.aspx.vb:45:    Protected Sub gvSprzet_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gvSprzet.SelectedIndexChanged
Sprzet.aspx.vb:46:        Dim sprzetId As Integer = CInt(gvSprzet.SelectedDataKey.Value)
Sprzet.aspx.vb:47:        Dim s As SprzetModel = New EquipmentManager().PobierzSprzet(sprzetId)
Sprzet.aspx.vb:48:        If s Is Nothing Then
Sprzet.aspx.vb:49:            PokazBlad("Sprzet nie istnieje (odswiez liste).")
Sprzet.aspx.vb:50:            WyczyscFormularz()
Sprzet.aspx.vb:51:            Return
Sprzet.aspx.vb:52:        End If
Sprzet.aspx.vb:53:        WypelnijFormularz(s)
Sprzet.aspx.vb:54:        PokazHistorie(s.Id)
Sprzet.aspx.vb:55:    End Sub
Sprzet.aspx.vb:56:
Sprzet.aspx.vb:57:    Protected Sub gvSprzet_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvSprzet.RowDataBound
Sprzet.aspx.vb:58:        If e.Row.RowType <> DataControlRowType.DataRow Then
Sprzet.aspx.vb:59:            Return
Sprzet.aspx.vb:60:        End If
Sprzet.aspx.vb:61:        Dim s As SprzetModel = CType(e.Row.DataItem, SprzetModel)
Sprzet.aspx.vb:62:        e.Row.Cells(6).Text = s.OpisStatusu
Sprzet.aspx.vb:63:        If s.Status = "S" Then
Sprzet.aspx.vb:64:            e.Row.CssClass = "wyszarzony"
Sprzet.aspx.vb:65:        End If
Sprzet.aspx.vb:66:        If s.Status <> "D" AndAlso s.Status <> "W" Then
Sprzet.aspx.vb:67:            e.Row.Cells(7).Text = ""
Sprzet.aspx.vb:68:        End If
Sprzet.aspx.vb:69:    End Sub
Sprzet.aspx.vb:70:
Sprzet.aspx.vb:71:    Protected Sub btnNowy_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNowy.Click
Sprzet.aspx.vb:72:        gvSprzet.SelectedIndex = -1
Sprzet.aspx.vb:73:        WyczyscFormularz()
Sprzet.aspx.vb:74:        txtNumer.Focus()
Sprzet.aspx.vb:75:    End Sub
Sprzet.aspx.vb:76:
Sprzet.aspx.vb:77:    Protected Sub btnZapisz_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZapisz.Click
Sprzet.aspx.vb:78:        Dim stawka As Decimal
Sprzet.aspx.vb:79:        Dim kaucja As Decimal
Sprzet.aspx.vb:80:        If Not Decimal.TryParse(txtStawka.Text.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, stawka) Then
Sprzet.aspx.vb:81:            PokazBlad("Niepoprawna stawka dobowa.")
Sprzet.aspx.vb:82:            Return
Sprzet.aspx.vb:83:        End If
Sprzet.aspx.vb:84:        If txtKaucja.Text.Trim() = "" Then
Sprzet.aspx.vb:85:            kaucja = 0D
Sprzet.aspx.vb:86:        ElseIf Not Decimal.TryParse(txtKaucja.Text.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, kaucja) Then
Sprzet.aspx.vb:87:            PokazBlad("Niepoprawna kaucja.")
Sprzet.aspx.vb:88:            Return
Sprzet.aspx.vb:89:        End If
Sprzet.aspx.vb:90:
Sprzet.aspx.vb:91:        Dim mgr As New EquipmentManager()
Sprzet.aspx.vb:92:        Dim s As SprzetModel
Sprzet.aspx.vb:93:        Dim sprzetId As Integer = CInt(hfSprzetId.Value)
Sprzet.aspx.vb:94:        If sprzetId > 0 Then
Sprzet.aspx.vb:95:            s = mgr.PobierzSprzet(sprzetId)
Sprzet.aspx.vb:96:            If s Is Nothing Then
Sprzet.aspx.vb:97:                PokazBlad("Sprzet nie istnieje.")
Sprzet.aspx.vb:98:                Return
Sprzet.aspx.vb:99:            End If
Sprzet.aspx.vb:100:            If s.Kategoria <> ddlKategoriaEdycja.SelectedValue AndAlso Not mgr.CzyMoznaZmienicKategorie(sprzetId) Then
Sprzet.aspx.vb:101:                PokazBlad("Nie mozna zmienic kategorii wypozyczonego sprzetu.")
Sprzet.aspx.vb:102:                Return
Sprzet.aspx.vb:103:            End If
Sprzet.aspx.vb:104:        Else
Sprzet.aspx.vb:105:            s = New SprzetModel()
Sprzet.aspx.vb:106:        End If
Sprzet.aspx.vb:107:        s.NumerInwentarzowy = txtNumer.Text
Sprzet.aspx.vb:108:        s.Nazwa = txtNazwa.Text.Trim()
Sprzet.aspx.vb:109:        s.Kategoria = ddlKategoriaEdycja.SelectedValue
Sprzet.aspx.vb:110:        s.StawkaDobowa = stawka
Sprzet.aspx.vb:111:        s.Kaucja = kaucja
Sprzet.aspx.vb:112:
Sprzet.aspx.vb:113:        Try
Sprzet.aspx.vb:114:            Dim zapisaneId As Integer = mgr.Zapisz(s)
Sprzet.aspx.vb:115:            hfSprzetId.Value = zapisaneId.ToString()
Sprzet.aspx.vb:116:            PokazInfo("Zapisano sprzet " & s.NumerInwentarzowy & ".")
Sprzet.aspx.vb:117:            PokazListe()
Sprzet.aspx.vb:118:            ZaznaczWiersz(zapisaneId)
Sprzet.aspx.vb:119:        Catch ex As BladBiznesowyException
Sprzet.aspx.vb:120:            PokazBlad(ex.Message)
Sprzet.aspx.vb:121:        Catch ex As Exception
Sprzet.aspx.vb:122:            Log.Error("Blad zapisu sprzetu " & s.NumerInwentarzowy, ex)
Sprzet.aspx.vb:123:            PokazBlad("Nie udalo sie zapisac sprzetu.")
Sprzet.aspx.vb:124:        End Try
Sprzet.aspx.vb:125:    End Sub
Sprzet.aspx.vb:126:
Sprzet.aspx.vb:127:    Protected Sub btnSerwis_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSerwis.Click
Sprzet.aspx.vb:128:        Dim sprzetId As Integer = CInt(hfSprzetId.Value)
Sprzet.aspx.vb:129:        If sprzetId = 0 Then
Sprzet.aspx.vb:130:            PokazBlad("Wybierz sprzet z listy.")
Sprzet.aspx.vb:131:            Return
Sprzet.aspx.vb:132:        End If
Sprzet.aspx.vb:133:        Try
Sprzet.aspx.vb:134:            Dim mgr As New EquipmentManager()
Sprzet.aspx.vb:135:            mgr.WyslijDoSerwisu(sprzetId)
Sprzet.aspx.vb:136:            lblStatus.Text = "serwis"
Sprzet.aspx.vb:137:            PokazInfo("Sprzet przekazany do serwisu.")
Sprzet.aspx.vb:138:            PokazListe()
Sprzet.aspx.vb:139:            ZaznaczWiersz(sprzetId)
Sprzet.aspx.vb:140:        Catch ex As BladBiznesowyException
Sprzet.aspx.vb:141:            PokazBlad(ex.Message)
Sprzet.aspx.vb:142:        End Try
Sprzet.aspx.vb:143:    End Sub
Sprzet.aspx.vb:144:
Sprzet.aspx.vb:145:    Private Sub PokazListe()
Sprzet.aspx.vb:146:        Dim lista As List(Of SprzetModel) = New EquipmentManager().ListaWKategorii(ddlKategoria.SelectedValue)
Sprzet.aspx.vb:147:        gvSprzet.DataSource = lista
Sprzet.aspx.vb:148:        gvSprzet.DataBind()
Sprzet.aspx.vb:149:    End Sub
Sprzet.aspx.vb:150:
Sprzet.aspx.vb:151:    Private Sub PokazHistorie(ByVal sprzetId As Integer)
Sprzet.aspx.vb:152:        Dim tabela As DataTable = New EquipmentManager().HistoriaWypozyczen(sprzetId)
Sprzet.aspx.vb:153:        gvHistoria.DataSource = tabela
Sprzet.aspx.vb:154:        gvHistoria.DataBind()
Sprzet.aspx.vb:155:        gvHistoria.Visible = True
Sprzet.aspx.vb:156:    End Sub
Sprzet.aspx.vb:157:
Sprzet.aspx.vb:158:    Private Sub WypelnijFormularz(ByVal s As SprzetModel)
Sprzet.aspx.vb:159:        hfSprzetId.Value = s.Id.ToString()
Sprzet.aspx.vb:160:        txtNumer.Text = s.NumerInwentarzowy
Sprzet.aspx.vb:161:        txtNazwa.Text = s.Nazwa
Sprzet.aspx.vb:162:        If ddlKategoriaEdycja.Items.FindByValue(s.Kategoria) IsNot Nothing Then
Sprzet.aspx.vb:163:            ddlKategoriaEdycja.SelectedValue = s.Kategoria
Sprzet.aspx.vb:164:        End If
Sprzet.aspx.vb:165:        txtStawka.Text = s.StawkaDobowa.ToString("0.00")
Sprzet.aspx.vb:166:        txtKaucja.Text = s.Kaucja.ToString("0.00")
Sprzet.aspx.vb:167:        lblStatus.Text = s.OpisStatusu
Sprzet.aspx.vb:168:        btnSerwis.Enabled = (s.Status = "D")
Sprzet.aspx.vb:169:        txtNumer.ReadOnly = True
Sprzet.aspx.vb:170:    End Sub
Sprzet.aspx.vb:171:
Sprzet.aspx.vb:172:    Private Sub WyczyscFormularz()
Sprzet.aspx.vb:173:        hfSprzetId.Value = "0"
Sprzet.aspx.vb:174:        txtNumer.Text = ""
Sprzet.aspx.vb:175:        txtNazwa.Text = ""
Sprzet.aspx.vb:176:        txtStawka.Text = ""
Sprzet.aspx.vb:177:        txtKaucja.Text = ""
Sprzet.aspx.vb:178:        lblStatus.Text = "-"
Sprzet.aspx.vb:179:        txtNumer.ReadOnly = False
Sprzet.aspx.vb:180:        btnSerwis.Enabled = False
Sprzet.aspx.vb:181:        If ddlKategoria.SelectedValue <> "" Then
Sprzet.aspx.vb:182:            ddlKategoriaEdycja.SelectedValue = ddlKategoria.SelectedValue
Sprzet.aspx.vb:183:        End If
Sprzet.aspx.vb:184:        gvHistoria.DataSource = Nothing
Sprzet.aspx.vb:185:        gvHistoria.DataBind()
Sprzet.aspx.vb:186:        gvHistoria.Visible = False
Sprzet.aspx.vb:187:    End Sub
Sprzet.aspx.vb:188:
Sprzet.aspx.vb:189:    Private Sub ZaznaczWiersz(ByVal sprzetId As Integer)
Sprzet.aspx.vb:190:        For i As Integer = 0 To gvSprzet.DataKeys.Count - 1
Sprzet.aspx.vb:191:            If CInt(gvSprzet.DataKeys(i).Value) = sprzetId Then
Sprzet.aspx.vb:192:                gvSprzet.SelectedIndex = i
Sprzet.aspx.vb:193:                Return
Sprzet.aspx.vb:194:            End If
Sprzet.aspx.vb:195:        Next
Sprzet.aspx.vb:196:        gvSprzet.SelectedIndex = -1
Sprzet.aspx.vb:197:    End Sub
Sprzet.aspx.vb:198:
Sprzet.aspx.vb:199:    Private Sub PokazBlad(ByVal tekst As String)
Sprzet.aspx.vb:200:        lblKomunikat.CssClass = "blad"
Sprzet.aspx.vb:201:        lblKomunikat.Text = Server.HtmlEncode(tekst)
Sprzet.aspx.vb:202:    End Sub
Sprzet.aspx.vb:203:
Sprzet.aspx.vb:204:    Private Sub PokazInfo(ByVal tekst As String)
Sprzet.aspx.vb:205:        lblKomunikat.CssClass = "info"
Sprzet.aspx.vb:206:        lblKomunikat.Text = Server.HtmlEncode(tekst)
Sprzet.aspx.vb:207:    End Sub
Sprzet.aspx.vb:208:
Sprzet.aspx.vb:209:End Class
Wypozyczenie.aspx:1:<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Wypozyczenie.aspx.vb" Inherits="WypozyczalniaWeb.Wypozyczenie" %>
Wypozyczenie.aspx:2:<!DOCTYPE html>
Wypozyczenie.aspx:3:<html xmlns="http://www.w3.org/1999/xhtml">
Wypozyczenie.aspx:4:<head runat="server">
Wypozyczenie.aspx:5:    <title>Wypożyczalnia - Wydanie sprzętu</title>
Wypozyczenie.aspx:6:</head>
Wypozyczenie.aspx:7:<body>
Wypozyczenie.aspx:8:    <form id="form1" runat="server">
Wypozyczenie.aspx:9:        <div class="menu">
Wypozyczenie.aspx:10:            <a href="Klienci.aspx">Klienci</a> |
Wypozyczenie.aspx:11:            <a href="Sprzet.aspx">Sprzęt</a> |
Wypozyczenie.aspx:12:            <a href="Rezerwacja.aspx">Rezerwacje</a> |
Wypozyczenie.aspx:13:            <a href="Wypozyczenie.aspx">Wydanie</a> |
Wypozyczenie.aspx:14:            <a href="Zwrot.aspx">Zwrot</a> |
Wypozyczenie.aspx:15:            <a href="Cennik.aspx">Cennik</a> |
Wypozyczenie.aspx:16:            <a href="Faktura.aspx">Faktury</a>
Wypozyczenie.aspx:17:        </div>
Wypozyczenie.aspx:18:        <h2>Wydanie sprzętu</h2>
Wypozyczenie.aspx:19:        <div class="klient">
Wypozyczenie.aspx:20:            Klient: <asp:Label ID="lblKlient" runat="server" Font-Bold="true" Text="(nie wybrano)" />
Wypozyczenie.aspx:21:        </div>
Wypozyczenie.aspx:22:        <asp:Label ID="lblKomunikat" runat="server" ForeColor="Red" EnableViewState="false" />
Wypozyczenie.aspx:23:        <table class="formularz" cellpadding="4" cellspacing="0" border="0">
Wypozyczenie.aspx:24:            <tr>
Wypozyczenie.aspx:25:                <td colspan="2"><b>Z rezerwacji</b></td>
Wypozyczenie.aspx:26:            </tr>
Wypozyczenie.aspx:27:            <tr>
Wypozyczenie.aspx:28:                <td>Rezerwacja klienta:</td>
Wypozyczenie.aspx:29:                <td>
Wypozyczenie.aspx:30:                    <asp:DropDownList ID="ddlRezerwacje" runat="server" AutoPostBack="true" Width="420px" />
Wypozyczenie.aspx:31:                </td>
Wypozyczenie.aspx:32:            </tr>
Wypozyczenie.aspx:33:            <tr>
Wypozyczenie.aspx:34:                <td colspan="2"><b>albo bez rezerwacji</b></td>
Wypozyczenie.aspx:35:            </tr>
Wypozyczenie.aspx:36:            <tr>
Wypozyczenie.aspx:37:                <td>Sprzęt dostępny od ręki:</td>
Wypozyczenie.aspx:38:                <td>
Wypozyczenie.aspx:39:                    <asp:DropDownList ID="ddlSprzet" runat="server" AutoPostBack="true" Width="420px" />
Wypozyczenie.aspx:40:                </td>
Wypozyczenie.aspx:41:            </tr>
Wypozyczenie.aspx:42:            <tr>
Wypozyczenie.aspx:43:                <td>Planowany zwrot:</td>
Wypozyczenie.aspx:44:                <td>
Wypozyczenie.aspx:45:                    <asp:TextBox ID="txtPlanowanyZwrot" runat="server" Width="160px" MaxLength="16" />
Wypozyczenie.aspx:46:                    <span class="podpowiedz">(rrrr-mm-dd gg:mm)</span>
Wypozyczenie.aspx:47:                    <asp:RequiredFieldValidator ID="rfvPlanowanyZwrot" runat="server"
Wypozyczenie.aspx:48:                        ControlToValidate="txtPlanowanyZwrot" ErrorMessage="Podaj planowany termin zwrotu"
Wypozyczenie.aspx:49:                        Display="Dynamic" ForeColor="Red" ValidationGroup="wydanie" />
Wypozyczenie.aspx:50:                </td>
Wypozyczenie.aspx:51:            </tr>
Wypozyczenie.aspx:52:            <tr>
Wypozyczenie.aspx:53:                <td>Kaucja do pobrania:</td>
Wypozyczenie.aspx:54:                <td><asp:Label ID="lblKaucja" runat="server" Font-Bold="true" Text="-" /> zł</td>
Wypozyczenie.aspx:55:            </tr>
Wypozyczenie.aspx:56:            <tr>
Wypozyczenie.aspx:57:                <td colspan="2"><b>Dostawa</b></td>
Wypozyczenie.aspx:58:            </tr>
Wypozyczenie.aspx:59:            <tr>
Wypozyczenie.aspx:60:                <td>&nbsp;</td>
Wypozyczenie.aspx:61:                <td>
Wypozyczenie.aspx:62:                    <asp:CheckBox ID="chkDostawa" runat="server" AutoPostBack="true" Text="Dostawa kurierem pod adres klienta" />
Wypozyczenie.aspx:63:                </td>
Wypozyczenie.aspx:64:            </tr>
Wypozyczenie.aspx:65:            <tr>
Wypozyczenie.aspx:66:                <td>Adres dostawy:</td>
Wypozyczenie.aspx:67:                <td>
Wypozyczenie.aspx:68:                    <asp:TextBox ID="txtAdres" runat="server" TextMode="MultiLine" Rows="3" Columns="50" Enabled="false" />
Wypozyczenie.aspx:69:                </td>
Wypozyczenie.aspx:70:            </tr>
Wypozyczenie.aspx:71:            <tr>
Wypozyczenie.aspx:72:                <td>&nbsp;</td>
Wypozyczenie.aspx:73:                <td><asp:Button ID="btnWydaj" runat="server" Text="Wydaj sprzęt" ValidationGroup="wydanie" /></td>
Wypozyczenie.aspx:74:            </tr>
Wypozyczenie.aspx:75:            <tr>
Wypozyczenie.aspx:76:                <td>Numer przesyłki:</td>
Wypozyczenie.aspx:77:                <td><asp:Label ID="lblNumerPrzesylki" runat="server" Text="-" /></td>
Wypozyczenie.aspx:78:            </tr>
Wypozyczenie.aspx:79:        </table>
Wypozyczenie.aspx:80:        <h3>Wydania i zwroty z dnia dzisiejszego</h3>
Wypozyczenie.aspx:81:        <asp:GridView ID="gvWydaneDzis" runat="server" AutoGenerateColumns="false" EmptyDataText="Brak ruchu w dniu dzisiejszym.">
Wypozyczenie.aspx:82:            <Columns>
Wypozyczenie.aspx:83:                <asp:BoundField DataField="Id" HeaderText="Nr" />
Wypozyczenie.aspx:84:                <asp:BoundField DataField="Klient" HeaderText="Klient" />
Wypozyczenie.aspx:85:                <asp:BoundField DataField="Sprzet" HeaderText="Sprzęt" />
Wypozyczenie.aspx:86:                <asp:BoundField DataField="DataWydania" HeaderText="Wydano" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
Wypozyczenie.aspx:87:                <asp:BoundField DataField="PlanowanyZwrot" HeaderText="Zwrot do" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
Wypozyczenie.aspx:88:            </Columns>
Wypozyczenie.aspx:89:        </asp:GridView>
Wypozyczenie.aspx:90:    </form>
Wypozyczenie.aspx:91:</body>
Wypozyczenie.aspx:92:</html>
Wypozyczenie.aspx.vb:1:' Modul: Wypozyczenie.aspx.vb | Autor: zespol IT | 2014-03-11
Wypozyczenie.aspx.vb:2:' Wydanie sprzetu klientowi (z rezerwacji albo od reki), opcjonalnie dostawa kurierem.
Wypozyczenie.aspx.vb:3:' 2015-06-22 MW: dostawa kurierem (KurierClient)
Wypozyczenie.aspx.vb:4:' 2018-02-09 PT: lista wydan i zwrotow z dnia na dole strony
Wypozyczenie.aspx.vb:5:
Wypozyczenie.aspx.vb:6:Imports System.Data
Wypozyczenie.aspx.vb:7:Imports System.Globalization
Wypozyczenie.aspx.vb:8:Imports Wypozyczalnia.Core
Wypozyczenie.aspx.vb:9:Imports Wypozyczalnia.Core.Model
Wypozyczenie.aspx.vb:10:
Wypozyczenie.aspx.vb:11:Partial Class Wypozyczenie
Wypozyczenie.aspx.vb:12:    Inherits System.Web.UI.Page
Wypozyczenie.aspx.vb:13:
Wypozyczenie.aspx.vb:14:    Protected WithEvents lblKlient As Global.System.Web.UI.WebControls.Label
Wypozyczenie.aspx.vb:15:    Protected WithEvents lblKomunikat As Global.System.Web.UI.WebControls.Label
Wypozyczenie.aspx.vb:16:    Protected WithEvents ddlRezerwacje As Global.System.Web.UI.WebControls.DropDownList
Wypozyczenie.aspx.vb:17:    Protected WithEvents ddlSprzet As Global.System.Web.UI.WebControls.DropDownList
Wypozyczenie.aspx.vb:18:    Protected WithEvents txtPlanowanyZwrot As Global.System.Web.UI.WebControls.TextBox
Wypozyczenie.aspx.vb:19:    Protected WithEvents rfvPlanowanyZwrot As Global.System.Web.UI.WebControls.RequiredFieldValidator
Wypozyczenie.aspx.vb:20:    Protected WithEvents lblKaucja As Global.System.Web.UI.WebControls.Label
Wypozyczenie.aspx.vb:21:    Protected WithEvents chkDostawa As Global.System.Web.UI.WebControls.CheckBox
Wypozyczenie.aspx.vb:22:    Protected WithEvents txtAdres As Global.System.Web.UI.WebControls.TextBox
Wypozyczenie.aspx.vb:23:    Protected WithEvents btnWydaj As Global.System.Web.UI.WebControls.Button
Wypozyczenie.aspx.vb:24:    Protected WithEvents lblNumerPrzesylki As Global.System.Web.UI.WebControls.Label
Wypozyczenie.aspx.vb:25:    Protected WithEvents gvWydaneDzis As Global.System.Web.UI.WebControls.GridView
Wypozyczenie.aspx.vb:26:
Wypozyczenie.aspx.vb:27:    Private Const FormatDaty As String = "yyyy-MM-dd HH:mm"
Wypozyczenie.aspx.vb:28:
Wypozyczenie.aspx.vb:29:    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
Wypozyczenie.aspx.vb:30:        If Not IsPostBack Then
Wypozyczenie.aspx.vb:31:            Dim klientId As Integer = Session("WybranyKlientId")
Wypozyczenie.aspx.vb:32:            If klientId = 0 Then
Wypozyczenie.aspx.vb:33:                lblKomunikat.Text = "Najpierw wybierz klienta na stronie Klienci."
Wypozyczenie.aspx.vb:34:                btnWydaj.Enabled = False
Wypozyczenie.aspx.vb:35:            Else
Wypozyczenie.aspx.vb:36:                PokazKlienta(klientId)
Wypozyczenie.aspx.vb:37:                WczytajRezerwacje(klientId)
Wypozyczenie.aspx.vb:38:            End If
Wypozyczenie.aspx.vb:39:            WczytajSprzet()
Wypozyczenie.aspx.vb:40:            WczytajRuchZDnia()
Wypozyczenie.aspx.vb:41:            Dim zAdresu As String = Request.QueryString("rezerwacja")
Wypozyczenie.aspx.vb:42:            If Not String.IsNullOrEmpty(zAdresu) AndAlso ddlRezerwacje.Items.FindByValue(zAdresu) IsNot Nothing Then
Wypozyczenie.aspx.vb:43:                ddlRezerwacje.SelectedValue = zAdresu
Wypozyczenie.aspx.vb:44:                ddlRezerwacje_SelectedIndexChanged(ddlRezerwacje, EventArgs.Empty)
Wypozyczenie.aspx.vb:45:            End If
Wypozyczenie.aspx.vb:46:        End If
Wypozyczenie.aspx.vb:47:    End Sub
Wypozyczenie.aspx.vb:48:
Wypozyczenie.aspx.vb:49:    Protected Sub gvWydaneDzis_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvWydaneDzis.RowDataBound
Wypozyczenie.aspx.vb:50:        If e.Row.RowType <> DataControlRowType.DataRow Then Return
Wypozyczenie.aspx.vb:51:        Dim wiersz As DataRowView = CType(e.Row.DataItem, DataRowView)
Wypozyczenie.aspx.vb:52:        If Not IsDBNull(wiersz("DataZwrotu")) Then
Wypozyczenie.aspx.vb:53:            e.Row.ForeColor = Drawing.Color.Gray
Wypozyczenie.aspx.vb:54:            e.Row.ToolTip = "Zwrocono " & CDate(wiersz("DataZwrotu")).ToString(FormatDaty)
Wypozyczenie.aspx.vb:55:        ElseIf CDate(wiersz("PlanowanyZwrot")) < Date.Now Then
Wypozyczenie.aspx.vb:56:            e.Row.ForeColor = Drawing.Color.DarkRed
Wypozyczenie.aspx.vb:57:            e.Row.ToolTip = "Po terminie zwrotu"
Wypozyczenie.aspx.vb:58:        End If
Wypozyczenie.aspx.vb:59:    End Sub
Wypozyczenie.aspx.vb:60:
Wypozyczenie.aspx.vb:61:    Private Sub PokazKlienta(ByVal klientId As Integer)
Wypozyczenie.aspx.vb:62:        Dim klient As Klient = New CustomerManager().PobierzKlienta(klientId)
Wypozyczenie.aspx.vb:63:        If klient Is Nothing Then
Wypozyczenie.aspx.vb:64:            lblKlient.Text = "(nie znaleziono klienta " & klientId & ")"
Wypozyczenie.aspx.vb:65:            btnWydaj.Enabled = False
Wypozyczenie.aspx.vb:66:            Return
Wypozyczenie.aspx.vb:67:        End If
Wypozyczenie.aspx.vb:68:        If klient.Typ = "F" AndAlso Not String.IsNullOrEmpty(klient.NazwaFirmy) Then
Wypozyczenie.aspx.vb:69:            lblKlient.Text = Server.HtmlEncode(klient.NazwaFirmy & " (" & klient.Imie & " " & klient.Nazwisko & ")")
Wypozyczenie.aspx.vb:70:        Else
Wypozyczenie.aspx.vb:71:            lblKlient.Text = Server.HtmlEncode(klient.Imie & " " & klient.Nazwisko)
Wypozyczenie.aspx.vb:72:        End If
Wypozyczenie.aspx.vb:73:        If klient.Zablokowany Then
Wypozyczenie.aspx.vb:74:            lblKomunikat.Text = "Klient zablokowany: " & Server.HtmlEncode(klient.PowodBlokady)
Wypozyczenie.aspx.vb:75:            btnWydaj.Enabled = False
Wypozyczenie.aspx.vb:76:            Return
Wypozyczenie.aspx.vb:77:        End If
Wypozyczenie.aspx.vb:78:        Dim otwarte As List(Of Model.Wypozyczenie) = New RentalManager().OtwarteKlienta(klientId)
Wypozyczenie.aspx.vb:79:        If otwarte.Count > 0 Then
Wypozyczenie.aspx.vb:80:            Dim poTerminie As Integer = 0
Wypozyczenie.aspx.vb:81:            For Each w As Model.Wypozyczenie In otwarte
Wypozyczenie.aspx.vb:82:                If w.CzyPoTerminie(Date.Now) Then poTerminie = poTerminie + 1
Wypozyczenie.aspx.vb:83:            Next
Wypozyczenie.aspx.vb:84:            lblKlient.Text = lblKlient.Text & " - otwarte wypozyczenia: " & otwarte.Count
Wypozyczenie.aspx.vb:85:            If poTerminie > 0 Then
Wypozyczenie.aspx.vb:86:                lblKomunikat.Text = "Uwaga: klient ma " & poTerminie & " wypozyczen po terminie zwrotu."
Wypozyczenie.aspx.vb:87:            End If
Wypozyczenie.aspx.vb:88:        End If
Wypozyczenie.aspx.vb:89:    End Sub
Wypozyczenie.aspx.vb:90:
Wypozyczenie.aspx.vb:91:    Private Sub WczytajRezerwacje(ByVal klientId As Integer)
Wypozyczenie.aspx.vb:92:        ddlRezerwacje.Items.Clear()
Wypozyczenie.aspx.vb:93:        ddlRezerwacje.Items.Add(New ListItem("-- bez rezerwacji --", "0"))
Wypozyczenie.aspx.vb:94:        Dim tabela As DataTable = New ReservationManager().RezerwacjeKlienta(klientId)
Wypozyczenie.aspx.vb:95:        For Each wiersz As DataRow In tabela.Rows
Wypozyczenie.aspx.vb:96:            Dim status As String = wiersz("Status").ToString()
Wypozyczenie.aspx.vb:97:            If status = "N" OrElse status = "P" Then
Wypozyczenie.aspx.vb:98:                Dim tekst As String = String.Format("#{0} {1} | {2:yyyy-MM-dd HH:mm} - {3:yyyy-MM-dd HH:mm} | {4}",
Wypozyczenie.aspx.vb:99:                    wiersz("Id"), wiersz("Sprzet"), wiersz("DataOd"), wiersz("DataDo"), status)
Wypozyczenie.aspx.vb:100:                ddlRezerwacje.Items.Add(New ListItem(tekst, wiersz("Id").ToString()))
Wypozyczenie.aspx.vb:101:            End If
Wypozyczenie.aspx.vb:102:        Next
Wypozyczenie.aspx.vb:103:    End Sub
Wypozyczenie.aspx.vb:104:
Wypozyczenie.aspx.vb:105:    Private Sub WczytajSprzet()
Wypozyczenie.aspx.vb:106:        ddlSprzet.Items.Clear()
Wypozyczenie.aspx.vb:107:        ddlSprzet.Items.Add(New ListItem("-- wybierz --", "0"))
Wypozyczenie.aspx.vb:108:        Dim lista As List(Of Sprzet) = New EquipmentManager().ListaWKategorii("")
Wypozyczenie.aspx.vb:109:        For Each s As Sprzet In lista
Wypozyczenie.aspx.vb:110:            If s.Status = "D" Then
Wypozyczenie.aspx.vb:111:                ddlSprzet.Items.Add(New ListItem(s.NumerInwentarzowy & " " & s.Nazwa, s.Id.ToString()))
Wypozyczenie.aspx.vb:112:            End If
Wypozyczenie.aspx.vb:113:        Next
Wypozyczenie.aspx.vb:114:    End Sub
Wypozyczenie.aspx.vb:115:
Wypozyczenie.aspx.vb:116:    Private Sub WczytajRuchZDnia()
Wypozyczenie.aspx.vb:117:        gvWydaneDzis.DataSource = New RentalManager().WypozyczeniaZDnia(Date.Today)
Wypozyczenie.aspx.vb:118:        gvWydaneDzis.DataBind()
Wypozyczenie.aspx.vb:119:    End Sub
Wypozyczenie.aspx.vb:120:
Wypozyczenie.aspx.vb:121:    Protected Sub ddlRezerwacje_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlRezerwacje.SelectedIndexChanged
Wypozyczenie.aspx.vb:122:        lblNumerPrzesylki.Text = "-"
Wypozyczenie.aspx.vb:123:        Dim rezerwacjaId As Integer = CInt(ddlRezerwacje.SelectedValue)
Wypozyczenie.aspx.vb:124:        If rezerwacjaId = 0 Then
Wypozyczenie.aspx.vb:125:            ddlSprzet.Enabled = True
Wypozyczenie.aspx.vb:126:            txtPlanowanyZwrot.Text = ""
Wypozyczenie.aspx.vb:127:            lblKaucja.Text = "-"
Wypozyczenie.aspx.vb:128:            Return
Wypozyczenie.aspx.vb:129:        End If
Wypozyczenie.aspx.vb:130:        Dim r As Rezerwacja = New ReservationManager().PobierzRezerwacje(rezerwacjaId)
Wypozyczenie.aspx.vb:131:        If r Is Nothing Then
Wypozyczenie.aspx.vb:132:            lblKomunikat.Text = "Nie znaleziono rezerwacji " & rezerwacjaId
Wypozyczenie.aspx.vb:133:            Return
Wypozyczenie.aspx.vb:134:        End If
Wypozyczenie.aspx.vb:135:        ddlSprzet.SelectedIndex = 0
Wypozyczenie.aspx.vb:136:        ddlSprzet.Enabled = False
Wypozyczenie.aspx.vb:137:        txtPlanowanyZwrot.Text = r.DataDo.ToString(FormatDaty)
Wypozyczenie.aspx.vb:138:        PokazKaucje(r.SprzetId)
Wypozyczenie.aspx.vb:139:    End Sub
Wypozyczenie.aspx.vb:140:
Wypozyczenie.aspx.vb:141:    Protected Sub ddlSprzet_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlSprzet.SelectedIndexChanged
Wypozyczenie.aspx.vb:142:        Dim sprzetId As Integer = CInt(ddlSprzet.SelectedValue)
Wypozyczenie.aspx.vb:143:        If sprzetId = 0 Then
Wypozyczenie.aspx.vb:144:            lblKaucja.Text = "-"
Wypozyczenie.aspx.vb:145:        Else
Wypozyczenie.aspx.vb:146:            PokazKaucje(sprzetId)
Wypozyczenie.aspx.vb:147:            If txtPlanowanyZwrot.Text = "" Then
Wypozyczenie.aspx.vb:148:                txtPlanowanyZwrot.Text = Date.Now.AddDays(1).ToString(FormatDaty)
Wypozyczenie.aspx.vb:149:            End If
Wypozyczenie.aspx.vb:150:        End If
Wypozyczenie.aspx.vb:151:    End Sub
Wypozyczenie.aspx.vb:152:
Wypozyczenie.aspx.vb:153:    Private Sub PokazKaucje(ByVal sprzetId As Integer)
Wypozyczenie.aspx.vb:154:        Dim s As Sprzet = New EquipmentManager().PobierzSprzet(sprzetId)
Wypozyczenie.aspx.vb:155:        If s Is Nothing Then
Wypozyczenie.aspx.vb:156:            lblKaucja.Text = "-"
Wypozyczenie.aspx.vb:157:        Else
Wypozyczenie.aspx.vb:158:            lblKaucja.Text = s.Kaucja.ToString("N2")
Wypozyczenie.aspx.vb:159:        End If
Wypozyczenie.aspx.vb:160:    End Sub
Wypozyczenie.aspx.vb:161:
Wypozyczenie.aspx.vb:162:    Protected Sub chkDostawa_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkDostawa.CheckedChanged
Wypozyczenie.aspx.vb:163:        txtAdres.Enabled = chkDostawa.Checked
Wypozyczenie.aspx.vb:164:        If Not chkDostawa.Checked Then
Wypozyczenie.aspx.vb:165:            txtAdres.Text = ""
Wypozyczenie.aspx.vb:166:        End If
Wypozyczenie.aspx.vb:167:    End Sub
Wypozyczenie.aspx.vb:168:
Wypozyczenie.aspx.vb:169:    Protected Sub btnWydaj_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnWydaj.Click
Wypozyczenie.aspx.vb:170:        Dim klientId As Integer = Session("WybranyKlientId")
Wypozyczenie.aspx.vb:171:        If klientId = 0 Then
Wypozyczenie.aspx.vb:172:            lblKomunikat.Text = "Sesja wygasla - wybierz klienta ponownie."
Wypozyczenie.aspx.vb:173:            Return
Wypozyczenie.aspx.vb:174:        End If
Wypozyczenie.aspx.vb:175:
Wypozyczenie.aspx.vb:176:        Dim rezerwacjaId As Integer = CInt(ddlRezerwacje.SelectedValue)
Wypozyczenie.aspx.vb:177:        Dim sprzetId As Integer
Wypozyczenie.aspx.vb:178:        If rezerwacjaId > 0 Then
Wypozyczenie.aspx.vb:179:            Dim r As Rezerwacja = New ReservationManager().PobierzRezerwacje(rezerwacjaId)
Wypozyczenie.aspx.vb:180:            If r Is Nothing OrElse r.KlientId <> klientId Then
Wypozyczenie.aspx.vb:181:                lblKomunikat.Text = "Rezerwacja nie nalezy do wybranego klienta."
Wypozyczenie.aspx.vb:182:                Return
Wypozyczenie.aspx.vb:183:            End If
Wypozyczenie.aspx.vb:184:            sprzetId = r.SprzetId
Wypozyczenie.aspx.vb:185:        Else
Wypozyczenie.aspx.vb:186:            sprzetId = CInt(ddlSprzet.SelectedValue)
Wypozyczenie.aspx.vb:187:        End If
Wypozyczenie.aspx.vb:188:        If sprzetId = 0 Then
Wypozyczenie.aspx.vb:189:            lblKomunikat.Text = "Wybierz rezerwacje albo sprzet."
Wypozyczenie.aspx.vb:190:            Return
Wypozyczenie.aspx.vb:191:        End If
Wypozyczenie.aspx.vb:192:
Wypozyczenie.aspx.vb:193:        Dim planowanyZwrot As Date
Wypozyczenie.aspx.vb:194:        If Not Date.TryParseExact(txtPlanowanyZwrot.Text.Trim(), FormatDaty, CultureInfo.InvariantCulture, DateTimeStyles.None, planowanyZwrot) Then
Wypozyczenie.aspx.vb:195:            lblKomunikat.Text = "Niepoprawny format daty zwrotu (rrrr-mm-dd gg:mm)."
Wypozyczenie.aspx.vb:196:            Return
Wypozyczenie.aspx.vb:197:        End If
Wypozyczenie.aspx.vb:198:        If planowanyZwrot <= Date.Now Then
Wypozyczenie.aspx.vb:199:            lblKomunikat.Text = "Planowany zwrot musi byc pozniejszy niz chwila wydania."
Wypozyczenie.aspx.vb:200:            Return
Wypozyczenie.aspx.vb:201:        End If
Wypozyczenie.aspx.vb:202:
Wypozyczenie.aspx.vb:203:        Dim adres As String = ""
Wypozyczenie.aspx.vb:204:        If chkDostawa.Checked Then
Wypozyczenie.aspx.vb:205:            adres = txtAdres.Text.Trim()
Wypozyczenie.aspx.vb:206:            If adres = "" Then
Wypozyczenie.aspx.vb:207:                lblKomunikat.Text = "Podaj adres dostawy albo odznacz dostawe."
Wypozyczenie.aspx.vb:208:                Return
Wypozyczenie.aspx.vb:209:            End If
Wypozyczenie.aspx.vb:210:        End If
Wypozyczenie.aspx.vb:211:
Wypozyczenie.aspx.vb:212:        Try
Wypozyczenie.aspx.vb:213:            Dim manager As New RentalManager()
Wypozyczenie.aspx.vb:214:            Dim noweId As Integer = manager.WydajSprzet(rezerwacjaId, klientId, sprzetId, planowanyZwrot, adres)
Wypozyczenie.aspx.vb:215:            Dim w As Model.Wypozyczenie = manager.PobierzWypozyczenie(noweId)
Wypozyczenie.aspx.vb:216:            lblKaucja.Text = w.KaucjaPobrana.ToString("N2")
Wypozyczenie.aspx.vb:217:            If chkDostawa.Checked Then
Wypozyczenie.aspx.vb:218:                If String.IsNullOrEmpty(w.NumerPrzesylki) Then
Wypozyczenie.aspx.vb:219:                    lblNumerPrzesylki.Text = "brak - zamow kuriera telefonicznie"
Wypozyczenie.aspx.vb:220:                Else
Wypozyczenie.aspx.vb:221:                    lblNumerPrzesylki.Text = w.NumerPrzesylki
Wypozyczenie.aspx.vb:222:                End If
Wypozyczenie.aspx.vb:223:            Else
Wypozyczenie.aspx.vb:224:                lblNumerPrzesylki.Text = "odbior osobisty"
Wypozyczenie.aspx.vb:225:            End If
Wypozyczenie.aspx.vb:226:            lblKomunikat.ForeColor = Drawing.Color.Green
Wypozyczenie.aspx.vb:227:            lblKomunikat.Text = "Wydano sprzet, wypozyczenie nr " & noweId & "."
Wypozyczenie.aspx.vb:228:            WczytajRezerwacje(klientId)
Wypozyczenie.aspx.vb:229:            WczytajSprzet()
Wypozyczenie.aspx.vb:230:            WczytajRuchZDnia()
Wypozyczenie.aspx.vb:231:            ddlSprzet.Enabled = True
Wypozyczenie.aspx.vb:232:            txtPlanowanyZwrot.Text = ""
Wypozyczenie.aspx.vb:233:            chkDostawa.Checked = False
Wypozyczenie.aspx.vb:234:            txtAdres.Text = ""
Wypozyczenie.aspx.vb:235:            txtAdres.Enabled = False
Wypozyczenie.aspx.vb:236:        Catch ex As BladBiznesowyException
Wypozyczenie.aspx.vb:237:            lblKomunikat.Text = ex.Message
Wypozyczenie.aspx.vb:238:        Catch ex As Exception
Wypozyczenie.aspx.vb:239:            lblKomunikat.Text = "Blad podczas wydania: " & ex.Message
Wypozyczenie.aspx.vb:240:        End Try
Wypozyczenie.aspx.vb:241:    End Sub
Wypozyczenie.aspx.vb:242:
Wypozyczenie.aspx.vb:243:End Class
Zwrot.aspx:1:<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Zwrot.aspx.vb" Inherits="WypozyczalniaWeb.Zwrot" %>
Zwrot.aspx:2:<!DOCTYPE html>
Zwrot.aspx:3:<html xmlns="http://www.w3.org/1999/xhtml">
Zwrot.aspx:4:<head runat="server">
Zwrot.aspx:5:    <title>Wypożyczalnia - Zwrot sprzętu</title>
Zwrot.aspx:6:</head>
Zwrot.aspx:7:<body>
Zwrot.aspx:8:    <form id="form1" runat="server">
Zwrot.aspx:9:        <div class="menu">
Zwrot.aspx:10:            <a href="Klienci.aspx">Klienci</a> |
Zwrot.aspx:11:            <a href="Sprzet.aspx">Sprzęt</a> |
Zwrot.aspx:12:            <a href="Rezerwacja.aspx">Rezerwacje</a> |
Zwrot.aspx:13:            <a href="Wypozyczenie.aspx">Wydanie</a> |
Zwrot.aspx:14:            <a href="Zwrot.aspx">Zwrot</a> |
Zwrot.aspx:15:            <a href="Cennik.aspx">Cennik</a> |
Zwrot.aspx:16:            <a href="Faktura.aspx">Faktury</a>
Zwrot.aspx:17:        </div>
Zwrot.aspx:18:        <h2>Zwrot sprzętu</h2>
Zwrot.aspx:19:        <asp:Label ID="lblKomunikat" runat="server" ForeColor="Red" EnableViewState="false" />
Zwrot.aspx:20:        <table class="formularz" cellpadding="4" cellspacing="0" border="0">
Zwrot.aspx:21:            <tr>
Zwrot.aspx:22:                <td>Otwarte wypożyczenie:</td>
Zwrot.aspx:23:                <td>
Zwrot.aspx:24:                    <asp:DropDownList ID="ddlOtwarte" runat="server" AutoPostBack="true" Width="460px" />
Zwrot.aspx:25:                </td>
Zwrot.aspx:26:            </tr>
Zwrot.aspx:27:            <tr>
Zwrot.aspx:28:                <td>Szczegóły:</td>
Zwrot.aspx:29:                <td>
Zwrot.aspx:30:                    <asp:Label ID="lblSzczegoly" runat="server" Text="-" />
Zwrot.aspx:31:                </td>
Zwrot.aspx:32:            </tr>
Zwrot.aspx:33:            <tr>
Zwrot.aspx:34:                <td>Data i godzina zwrotu:</td>
Zwrot.aspx:35:                <td>
Zwrot.aspx:36:                    <asp:TextBox ID="txtDataZwrotu" runat="server" Width="160px" MaxLength="16" />
Zwrot.aspx:37:                    <span class="podpowiedz">(rrrr-mm-dd gg:mm, puste = teraz)</span>
Zwrot.aspx:38:                </td>
Zwrot.aspx:39:            </tr>
Zwrot.aspx:40:            <tr>
Zwrot.aspx:41:                <td valign="top">Stwierdzone szkody:</td>
Zwrot.aspx:42:                <td>
Zwrot.aspx:43:                    <asp:CheckBoxList ID="cblSzkody" runat="server" RepeatDirection="Vertical" />
Zwrot.aspx:44:                </td>
Zwrot.aspx:45:            </tr>
Zwrot.aspx:46:            <tr>
Zwrot.aspx:47:                <td>&nbsp;</td>
Zwrot.aspx:48:                <td>
Zwrot.aspx:49:                    <asp:Button ID="btnPodglad" runat="server" Text="Podgląd kary" />
Zwrot.aspx:50:                    &nbsp;
Zwrot.aspx:51:                    <asp:Button ID="btnZwrot" runat="server" Text="Zarejestruj zwrot"
Zwrot.aspx:52:                        OnClientClick="return confirm('Zarejestrować zwrot? Operacji nie można cofnąć.');" />
Zwrot.aspx:53:                </td>
Zwrot.aspx:54:            </tr>
Zwrot.aspx:55:        </table>
Zwrot.aspx:56:        <h3>Rozliczenie</h3>
Zwrot.aspx:57:        <table class="rozliczenie" cellpadding="4" cellspacing="0" border="1">
Zwrot.aspx:58:            <tr>
Zwrot.aspx:59:                <td>Opłata za wynajem:</td>
Zwrot.aspx:60:                <td align="right"><asp:Label ID="lblOplata" runat="server" Text="-" /> zł</td>
Zwrot.aspx:61:            </tr>
Zwrot.aspx:62:            <tr>
Zwrot.aspx:63:                <td>Kara za spóźnienie:</td>
Zwrot.aspx:64:                <td align="right"><asp:Label ID="lblKara" runat="server" Text="-" /> zł</td>
Zwrot.aspx:65:            </tr>
Zwrot.aspx:66:            <tr>
Zwrot.aspx:67:                <td>Kaucja do zwrotu:</td>
Zwrot.aspx:68:                <td align="right"><asp:Label ID="lblKaucjaZwrot" runat="server" Text="-" /> zł</td>
Zwrot.aspx:69:            </tr>
Zwrot.aspx:70:        </table>
Zwrot.aspx:71:        <p>
Zwrot.aspx:72:            <asp:HyperLink ID="lnkFaktura" runat="server" Visible="false" Text="Wystaw fakturę za to wypożyczenie" />
Zwrot.aspx:73:        </p>
Zwrot.aspx:74:        <h3>Wszystkie otwarte wypożyczenia</h3>
Zwrot.aspx:75:        <asp:GridView ID="gvOtwarte" runat="server" AutoGenerateColumns="false" EmptyDataText="Brak otwartych wypożyczeń.">
Zwrot.aspx:76:            <Columns>
Zwrot.aspx:77:                <asp:BoundField DataField="Id" HeaderText="Nr" />
Zwrot.aspx:78:                <asp:BoundField DataField="Klient" HeaderText="Klient" />
Zwrot.aspx:79:                <asp:BoundField DataField="Sprzet" HeaderText="Sprzęt" />
Zwrot.aspx:80:                <asp:BoundField DataField="DataWydania" HeaderText="Wydano" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
Zwrot.aspx:81:                <asp:BoundField DataField="PlanowanyZwrot" HeaderText="Zwrot do" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
Zwrot.aspx:82:                <asp:CommandField ShowSelectButton="true" SelectText="przyjmij" />
Zwrot.aspx:83:            </Columns>
Zwrot.aspx:84:        </asp:GridView>
Zwrot.aspx:85:    </form>
Zwrot.aspx:86:</body>
Zwrot.aspx:87:</html>
Zwrot.aspx.vb:1:Option Strict Off
Zwrot.aspx.vb:2:
Zwrot.aspx.vb:3:' Modul: Zwrot.aspx.vb | Autor: zespol IT | 2014-03-11
Zwrot.aspx.vb:4:' Przyjecie zwrotu sprzetu: kara za spoznienie, szkody, rozliczenie kaucji.
Zwrot.aspx.vb:5:' 2014-09-30 JK: szkody z listy zamiast pola tekstowego
Zwrot.aspx.vb:6:' 2017-05-16 MW: link do faktury po zwrocie
Zwrot.aspx.vb:7:
Zwrot.aspx.vb:8:Imports System.Data
Zwrot.aspx.vb:9:Imports System.Globalization
Zwrot.aspx.vb:10:Imports Wypozyczalnia.Core
Zwrot.aspx.vb:11:Imports Wypozyczalnia.Core.Model
Zwrot.aspx.vb:12:
Zwrot.aspx.vb:13:Partial Class Zwrot
Zwrot.aspx.vb:14:    Inherits System.Web.UI.Page
Zwrot.aspx.vb:15:
Zwrot.aspx.vb:16:    Protected WithEvents lblKomunikat As Global.System.Web.UI.WebControls.Label
Zwrot.aspx.vb:17:    Protected WithEvents ddlOtwarte As Global.System.Web.UI.WebControls.DropDownList
Zwrot.aspx.vb:18:    Protected WithEvents lblSzczegoly As Global.System.Web.UI.WebControls.Label
Zwrot.aspx.vb:19:    Protected WithEvents txtDataZwrotu As Global.System.Web.UI.WebControls.TextBox
Zwrot.aspx.vb:20:    Protected WithEvents cblSzkody As Global.System.Web.UI.WebControls.CheckBoxList
Zwrot.aspx.vb:21:    Protected WithEvents btnPodglad As Global.System.Web.UI.WebControls.Button
Zwrot.aspx.vb:22:    Protected WithEvents btnZwrot As Global.System.Web.UI.WebControls.Button
Zwrot.aspx.vb:23:    Protected WithEvents lblOplata As Global.System.Web.UI.WebControls.Label
Zwrot.aspx.vb:24:    Protected WithEvents lblKara As Global.System.Web.UI.WebControls.Label
Zwrot.aspx.vb:25:    Protected WithEvents lblKaucjaZwrot As Global.System.Web.UI.WebControls.Label
Zwrot.aspx.vb:26:    Protected WithEvents lnkFaktura As Global.System.Web.UI.WebControls.HyperLink
Zwrot.aspx.vb:27:    Protected WithEvents gvOtwarte As Global.System.Web.UI.WebControls.GridView
Zwrot.aspx.vb:28:
Zwrot.aspx.vb:29:    Private Const FormatDaty As String = "yyyy-MM-dd HH:mm"
Zwrot.aspx.vb:30:
Zwrot.aspx.vb:31:    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
Zwrot.aspx.vb:32:        If Not IsPostBack Then
Zwrot.aspx.vb:33:            WypelnijSzkody()
Zwrot.aspx.vb:34:            WczytajOtwarte()
Zwrot.aspx.vb:35:            WyczyscRozliczenie()
Zwrot.aspx.vb:36:            Dim zAdresu As String = Request.QueryString("id")
Zwrot.aspx.vb:37:            If Not String.IsNullOrEmpty(zAdresu) AndAlso ddlOtwarte.Items.FindByValue(zAdresu) IsNot Nothing Then
Zwrot.aspx.vb:38:                ddlOtwarte.SelectedValue = zAdresu
Zwrot.aspx.vb:39:                ddlOtwarte_SelectedIndexChanged(ddlOtwarte, EventArgs.Empty)
Zwrot.aspx.vb:40:            End If
Zwrot.aspx.vb:41:        End If
Zwrot.aspx.vb:42:    End Sub
Zwrot.aspx.vb:43:
Zwrot.aspx.vb:44:    Protected Sub gvOtwarte_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvOtwarte.RowDataBound
Zwrot.aspx.vb:45:        If e.Row.RowType <> DataControlRowType.DataRow Then Return
Zwrot.aspx.vb:46:        Dim wiersz As DataRowView = e.Row.DataItem
Zwrot.aspx.vb:47:        Dim plan As Date = wiersz("PlanowanyZwrot")
Zwrot.aspx.vb:48:        If plan < Date.Now.AddHours(-1) Then
Zwrot.aspx.vb:49:            e.Row.BackColor = Drawing.Color.MistyRose
Zwrot.aspx.vb:50:            e.Row.ToolTip = "Po terminie zwrotu"
Zwrot.aspx.vb:51:        ElseIf plan < Date.Now.AddHours(24) Then
Zwrot.aspx.vb:52:            e.Row.BackColor = Drawing.Color.LightYellow
Zwrot.aspx.vb:53:            e.Row.ToolTip = "Zwrot w ciagu doby"
Zwrot.aspx.vb:54:        End If
Zwrot.aspx.vb:55:    End Sub
Zwrot.aspx.vb:56:
Zwrot.aspx.vb:57:    Protected Sub gvOtwarte_SelectedIndexChanging(ByVal sender As Object, ByVal e As GridViewSelectEventArgs) Handles gvOtwarte.SelectedIndexChanging
Zwrot.aspx.vb:58:        Dim id As String = gvOtwarte.Rows(e.NewSelectedIndex).Cells(0).Text
Zwrot.aspx.vb:59:        If ddlOtwarte.Items.FindByValue(id) IsNot Nothing Then
Zwrot.aspx.vb:60:            ddlOtwarte.SelectedValue = id
Zwrot.aspx.vb:61:            ddlOtwarte_SelectedIndexChanged(ddlOtwarte, EventArgs.Empty)
Zwrot.aspx.vb:62:        End If
Zwrot.aspx.vb:63:    End Sub
Zwrot.aspx.vb:64:
Zwrot.aspx.vb:65:    Private Sub WypelnijSzkody()
Zwrot.aspx.vb:66:        cblSzkody.Items.Clear()
Zwrot.aspx.vb:67:        cblSzkody.Items.Add(New ListItem("Uszkodzona obudowa (50 zł)", "Uszkodzona obudowa"))
Zwrot.aspx.vb:68:        cblSzkody.Items.Add(New ListItem("Brak akcesoriów (30 zł)", "Brak akcesoriów"))
Zwrot.aspx.vb:69:        cblSzkody.Items.Add(New ListItem("Zabrudzenie (20 zł)", "Zabrudzenie"))
Zwrot.aspx.vb:70:    End Sub
Zwrot.aspx.vb:71:
Zwrot.aspx.vb:72:    Private Sub WczytajOtwarte()
Zwrot.aspx.vb:73:        Dim tabela As DataTable = New RentalManager().Otwarte()
Zwrot.aspx.vb:74:        ddlOtwarte.Items.Clear()
Zwrot.aspx.vb:75:        ddlOtwarte.Items.Add(New ListItem("-- wybierz --", "0"))
Zwrot.aspx.vb:76:        For Each wiersz As DataRow In tabela.Rows
Zwrot.aspx.vb:77:            Dim plan As Date = wiersz("PlanowanyZwrot")
Zwrot.aspx.vb:78:            Dim tekst As String = "#" & wiersz("Id") & " " & wiersz("Klient") & " | " & wiersz("Sprzet") & " | do " & plan.ToString(FormatDaty)
Zwrot.aspx.vb:79:            If plan < Date.Now Then
Zwrot.aspx.vb:80:                tekst = tekst & " (po terminie)"
Zwrot.aspx.vb:81:            End If
Zwrot.aspx.vb:82:            ddlOtwarte.Items.Add(New ListItem(tekst, wiersz("Id").ToString()))
Zwrot.aspx.vb:83:        Next
Zwrot.aspx.vb:84:        gvOtwarte.DataSource = tabela
Zwrot.aspx.vb:85:        gvOtwarte.DataBind()
Zwrot.aspx.vb:86:    End Sub
Zwrot.aspx.vb:87:
Zwrot.aspx.vb:88:    Private Sub WyczyscRozliczenie()
Zwrot.aspx.vb:89:        lblOplata.Text = "-"
Zwrot.aspx.vb:90:        lblKara.Text = "-"
Zwrot.aspx.vb:91:        lblKaucjaZwrot.Text = "-"
Zwrot.aspx.vb:92:        lnkFaktura.Visible = False
Zwrot.aspx.vb:93:    End Sub
Zwrot.aspx.vb:94:
Zwrot.aspx.vb:95:    Protected Sub ddlOtwarte_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlOtwarte.SelectedIndexChanged
Zwrot.aspx.vb:96:        WyczyscRozliczenie()
Zwrot.aspx.vb:97:        For Each item As ListItem In cblSzkody.Items
Zwrot.aspx.vb:98:            item.Selected = False
Zwrot.aspx.vb:99:        Next
Zwrot.aspx.vb:100:        Dim id As Integer = ddlOtwarte.SelectedValue
Zwrot.aspx.vb:101:        If id = 0 Then
Zwrot.aspx.vb:102:            lblSzczegoly.Text = "-"
Zwrot.aspx.vb:103:            Return
Zwrot.aspx.vb:104:        End If
Zwrot.aspx.vb:105:        Dim w As Wypozyczenie = New RentalManager().PobierzWypozyczenie(id)
Zwrot.aspx.vb:106:        If w Is Nothing Then
Zwrot.aspx.vb:107:            lblSzczegoly.Text = "-"
Zwrot.aspx.vb:108:            lblKomunikat.Text = "Nie znaleziono wypozyczenia " & id
Zwrot.aspx.vb:109:            Return
Zwrot.aspx.vb:110:        End If
Zwrot.aspx.vb:111:        Dim s As Sprzet = New EquipmentManager().PobierzSprzet(w.SprzetId)
Zwrot.aspx.vb:112:        Dim opis As String = "Wydano " & w.DataWydania.ToString(FormatDaty) & ", zwrot do " & w.PlanowanyZwrot.ToString(FormatDaty) &
Zwrot.aspx.vb:113:            ", kaucja " & w.KaucjaPobrana.ToString("N2") & " zł"
Zwrot.aspx.vb:114:        If s IsNot Nothing Then
Zwrot.aspx.vb:115:            opis = s.NumerInwentarzowy & " " & s.Nazwa & " (" & s.StawkaDobowa.ToString("N2") & " zł/doba). " & opis
Zwrot.aspx.vb:116:        End If
Zwrot.aspx.vb:117:        If Not String.IsNullOrEmpty(w.NumerPrzesylki) Then
Zwrot.aspx.vb:118:            opis = opis & ", przesylka " & w.NumerPrzesylki
Zwrot.aspx.vb:119:        End If
Zwrot.aspx.vb:120:        lblSzczegoly.Text = Server.HtmlEncode(opis)
Zwrot.aspx.vb:121:        If txtDataZwrotu.Text = "" Then
Zwrot.aspx.vb:122:            txtDataZwrotu.Text = Date.Now.ToString(FormatDaty)
Zwrot.aspx.vb:123:        End If
Zwrot.aspx.vb:124:    End Sub
Zwrot.aspx.vb:125:
Zwrot.aspx.vb:126:    Private Function OpisSpoznienia(ByVal plan As Date, ByVal zwrot As Date) As String
Zwrot.aspx.vb:127:        Dim minuty As Long = DateDiff(DateInterval.Minute, plan, zwrot)
Zwrot.aspx.vb:128:        If minuty <= 0 Then
Zwrot.aspx.vb:129:            Return "zwrot w terminie"
Zwrot.aspx.vb:130:        End If
Zwrot.aspx.vb:131:        If minuty <= 60 Then
Zwrot.aspx.vb:132:            Return "spoznienie " & minuty & " min (w tolerancji)"
Zwrot.aspx.vb:133:        End If
Zwrot.aspx.vb:134:        Dim godziny As Long = minuty \ 60
Zwrot.aspx.vb:135:        Dim reszta As Long = minuty Mod 60
Zwrot.aspx.vb:136:        If godziny < 24 Then
Zwrot.aspx.vb:137:            Return "spoznienie " & godziny & " h " & reszta & " min"
Zwrot.aspx.vb:138:        End If
Zwrot.aspx.vb:139:        Return "spoznienie " & (godziny \ 24) & " d " & (godziny Mod 24) & " h " & reszta & " min"
Zwrot.aspx.vb:140:    End Function
Zwrot.aspx.vb:141:
Zwrot.aspx.vb:142:    Private Function OdczytajDateZwrotu(ByRef dataZwrotu As Date) As Boolean
Zwrot.aspx.vb:143:        Dim tekst As String = txtDataZwrotu.Text.Trim()
Zwrot.aspx.vb:144:        If tekst = "" Then
Zwrot.aspx.vb:145:            dataZwrotu = Date.Now
Zwrot.aspx.vb:146:            Return True
Zwrot.aspx.vb:147:        End If
Zwrot.aspx.vb:148:        If Date.TryParseExact(tekst, FormatDaty, CultureInfo.InvariantCulture, DateTimeStyles.None, dataZwrotu) Then
Zwrot.aspx.vb:149:            Return True
Zwrot.aspx.vb:150:        End If
Zwrot.aspx.vb:151:        Return Date.TryParse(tekst, dataZwrotu)
Zwrot.aspx.vb:152:    End Function
Zwrot.aspx.vb:153:
Zwrot.aspx.vb:154:    Protected Sub btnPodglad_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPodglad.Click
Zwrot.aspx.vb:155:        Dim id As Integer = ddlOtwarte.SelectedValue
Zwrot.aspx.vb:156:        If id = 0 Then
Zwrot.aspx.vb:157:            lblKomunikat.Text = "Wybierz wypozyczenie."
Zwrot.aspx.vb:158:            Return
Zwrot.aspx.vb:159:        End If
Zwrot.aspx.vb:160:        Dim dataZwrotu As Date
Zwrot.aspx.vb:161:        If Not OdczytajDateZwrotu(dataZwrotu) Then
Zwrot.aspx.vb:162:            lblKomunikat.Text = "Niepoprawna data zwrotu."
Zwrot.aspx.vb:163:            Return
Zwrot.aspx.vb:164:        End If
Zwrot.aspx.vb:165:        Dim manager As New RentalManager()
Zwrot.aspx.vb:166:        Dim w As Wypozyczenie = manager.PobierzWypozyczenie(id)
Zwrot.aspx.vb:167:        If w Is Nothing Then
Zwrot.aspx.vb:168:            lblKomunikat.Text = "Nie znaleziono wypozyczenia " & id
Zwrot.aspx.vb:169:            Return
Zwrot.aspx.vb:170:        End If
Zwrot.aspx.vb:171:        Dim s As Sprzet = New EquipmentManager().PobierzSprzet(w.SprzetId)
Zwrot.aspx.vb:172:        Dim kara As Decimal = manager.ObliczKare(w.PlanowanyZwrot, dataZwrotu, s.StawkaDobowa)
Zwrot.aspx.vb:173:        Dim szkody As Decimal = 0
Zwrot.aspx.vb:174:        For Each item As ListItem In cblSzkody.Items
Zwrot.aspx.vb:175:            If item.Selected Then
Zwrot.aspx.vb:176:                Select Case item.Value
Zwrot.aspx.vb:177:                    Case "Uszkodzona obudowa"
Zwrot.aspx.vb:178:                        szkody = szkody + 50
Zwrot.aspx.vb:179:                    Case "Brak akcesoriów"
Zwrot.aspx.vb:180:                        szkody = szkody + 30
Zwrot.aspx.vb:181:                    Case "Zabrudzenie"
Zwrot.aspx.vb:182:                        szkody = szkody + 20
Zwrot.aspx.vb:183:                End Select
Zwrot.aspx.vb:184:            End If
Zwrot.aspx.vb:185:        Next
Zwrot.aspx.vb:186:        Dim kaucja As Decimal = w.KaucjaPobrana - kara - szkody
Zwrot.aspx.vb:187:        If kaucja < 0 Then kaucja = 0
Zwrot.aspx.vb:188:        lblOplata.Text = "(przy zwrocie)"
Zwrot.aspx.vb:189:        lblKara.Text = kara
Zwrot.aspx.vb:190:        lblKaucjaZwrot.Text = kaucja
Zwrot.aspx.vb:191:        lblKomunikat.ForeColor = Drawing.Color.Navy
Zwrot.aspx.vb:192:        lblKomunikat.Text = "Podglad (" & OpisSpoznienia(w.PlanowanyZwrot, dataZwrotu) & ") - zwrot nie zostal jeszcze zapisany."
Zwrot.aspx.vb:193:    End Sub
Zwrot.aspx.vb:194:
Zwrot.aspx.vb:195:    Protected Sub btnZwrot_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZwrot.Click
Zwrot.aspx.vb:196:        Dim id As Integer = ddlOtwarte.SelectedValue
Zwrot.aspx.vb:197:        If id = 0 Then
Zwrot.aspx.vb:198:            lblKomunikat.Text = "Wybierz wypozyczenie."
Zwrot.aspx.vb:199:            Return
Zwrot.aspx.vb:200:        End If
Zwrot.aspx.vb:201:
Zwrot.aspx.vb:202:        Dim dataZwrotu As Date
Zwrot.aspx.vb:203:        If Not OdczytajDateZwrotu(dataZwrotu) Then
Zwrot.aspx.vb:204:            lblKomunikat.Text = "Niepoprawna data zwrotu."
Zwrot.aspx.vb:205:            Return
Zwrot.aspx.vb:206:        End If
Zwrot.aspx.vb:207:        If dataZwrotu > Date.Now.AddMinutes(5) Then
Zwrot.aspx.vb:208:            lblKomunikat.Text = "Data zwrotu nie moze byc z przyszlosci."
Zwrot.aspx.vb:209:            Return
Zwrot.aspx.vb:210:        End If
Zwrot.aspx.vb:211:
Zwrot.aspx.vb:212:        Dim manager As New RentalManager()
Zwrot.aspx.vb:213:        Dim przed As Wypozyczenie = manager.PobierzWypozyczenie(id)
Zwrot.aspx.vb:214:        If przed Is Nothing Then
Zwrot.aspx.vb:215:            lblKomunikat.Text = "Nie znaleziono wypozyczenia " & id
Zwrot.aspx.vb:216:            Return
Zwrot.aspx.vb:217:        End If
Zwrot.aspx.vb:218:        If dataZwrotu < przed.DataWydania Then
Zwrot.aspx.vb:219:            lblKomunikat.Text = "Data zwrotu wczesniejsza niz data wydania (" & przed.DataWydania.ToString(FormatDaty) & ")."
Zwrot.aspx.vb:220:            Return
Zwrot.aspx.vb:221:        End If
Zwrot.aspx.vb:222:
Zwrot.aspx.vb:223:        ' slownik szkod: kwoty uzgodnione z kierownikiem punktu (cennik 2014)
Zwrot.aspx.vb:224:        Dim cennikSzkod As New Dictionary(Of String, Decimal)
Zwrot.aspx.vb:225:        cennikSzkod.Add("Uszkodzona obudowa", 50D)
Zwrot.aspx.vb:226:        cennikSzkod.Add("Brak akcesoriów", 30D)
Zwrot.aspx.vb:227:        cennikSzkod.Add("Zabrudzenie", 20D)
Zwrot.aspx.vb:228:
Zwrot.aspx.vb:229:        Dim szkody As Decimal = 0
Zwrot.aspx.vb:230:        Dim opisSzkod As New List(Of String)
Zwrot.aspx.vb:231:        For Each item As ListItem In cblSzkody.Items
Zwrot.aspx.vb:232:            If item.Selected AndAlso cennikSzkod.ContainsKey(item.Value) Then
Zwrot.aspx.vb:233:                szkody = szkody + cennikSzkod(item.Value)
Zwrot.aspx.vb:234:                opisSzkod.Add(item.Value)
Zwrot.aspx.vb:235:            End If
Zwrot.aspx.vb:236:        Next
Zwrot.aspx.vb:237:        If szkody > przed.KaucjaPobrana Then
Zwrot.aspx.vb:238:            lblKomunikat.Text = "Uwaga: szkody przekraczaja kaucje - roznica trafi na fakture."
Zwrot.aspx.vb:239:        End If
Zwrot.aspx.vb:240:
Zwrot.aspx.vb:241:        Dim po As Wypozyczenie
Zwrot.aspx.vb:242:        Try
Zwrot.aspx.vb:243:            po = manager.ZarejestrujZwrot(id, dataZwrotu, szkody)
Zwrot.aspx.vb:244:        Catch ex As BladBiznesowyException
Zwrot.aspx.vb:245:            lblKomunikat.Text = ex.Message
Zwrot.aspx.vb:246:            Return
Zwrot.aspx.vb:247:        Catch ex As Exception
Zwrot.aspx.vb:248:            lblKomunikat.Text = "Blad rejestracji zwrotu: " & ex.Message
Zwrot.aspx.vb:249:            Return
Zwrot.aspx.vb:250:        End Try
Zwrot.aspx.vb:251:
Zwrot.aspx.vb:252:        lblOplata.Text = po.Oplata
Zwrot.aspx.vb:253:        lblKara.Text = po.Kara
Zwrot.aspx.vb:254:        lblKaucjaZwrot.Text = po.KaucjaZwrocona
Zwrot.aspx.vb:255:
Zwrot.aspx.vb:256:        Session("OstatnieWypozyczenieId") = po.Id
Zwrot.aspx.vb:257:        lnkFaktura.NavigateUrl = "Faktura.aspx?id=" & po.Id
Zwrot.aspx.vb:258:        lnkFaktura.Visible = True
Zwrot.aspx.vb:259:
Zwrot.aspx.vb:260:        Dim komunikat As String = "Zwrot zarejestrowany (wypozyczenie nr " & po.Id & ")."
Zwrot.aspx.vb:261:        If po.Kara > 0 Then
Zwrot.aspx.vb:262:            komunikat = komunikat & " Naliczono kare za spoznienie (" & OpisSpoznienia(po.PlanowanyZwrot, dataZwrotu) & ")."
Zwrot.aspx.vb:263:        End If
Zwrot.aspx.vb:264:        If opisSzkod.Count > 0 Then
Zwrot.aspx.vb:265:            komunikat = komunikat & " Szkody: " & String.Join(", ", opisSzkod.ToArray()) & "."
Zwrot.aspx.vb:266:        End If
Zwrot.aspx.vb:267:        If po.KaucjaZwrocona = 0 AndAlso po.KaucjaPobrana > 0 Then
Zwrot.aspx.vb:268:            komunikat = komunikat & " Kaucja w calosci zatrzymana."
Zwrot.aspx.vb:269:        End If
Zwrot.aspx.vb:270:        lblKomunikat.ForeColor = Drawing.Color.Green
Zwrot.aspx.vb:271:        lblKomunikat.Text = Server.HtmlEncode(komunikat)
Zwrot.aspx.vb:272:
Zwrot.aspx.vb:273:        txtDataZwrotu.Text = ""
Zwrot.aspx.vb:274:        For Each item As ListItem In cblSzkody.Items
Zwrot.aspx.vb:275:            item.Selected = False
Zwrot.aspx.vb:276:        Next
Zwrot.aspx.vb:277:        lblSzczegoly.Text = "-"
Zwrot.aspx.vb:278:        WczytajOtwarte()
Zwrot.aspx.vb:279:    End Sub
Zwrot.aspx.vb:280:
Zwrot.aspx.vb:281:End Class
```

## M12 — surowy wynik H2

`git --no-pager -C 'legacy-app-parent\legacy-app\WypozyczalniaWeb' grep -n -I -e '^' -- 'Klienci.aspx.vb' 'Sprzet.aspx.vb' 'Rezerwacja.aspx.vb' 'Wypozyczenie.aspx.vb' 'Cennik.aspx.vb' 'Faktura.aspx.vb'`

```text
Cennik.aspx.vb:1:' Modul: Cennik.aspx - taryfy, stawki, import cennika, kalkulator | Autor: zespol IT | 2014-03-11
Cennik.aspx.vb:2:' Historia:
Cennik.aspx.vb:3:'   2015-02-04 MW: lista taryf z data obowiazywania
Cennik.aspx.vb:4:'   2017-06-12 JK: przycisk importu cennika XML
Cennik.aspx.vb:5:'   2019-04-02 PT: kalkulator oplaty dla wybranego klienta
Cennik.aspx.vb:6:
Cennik.aspx.vb:7:Imports System.Data
Cennik.aspx.vb:8:Imports System.Configuration
Cennik.aspx.vb:9:Imports System.Globalization
Cennik.aspx.vb:10:Imports Wypozyczalnia.Core
Cennik.aspx.vb:11:Imports Wypozyczalnia.Core.Model
Cennik.aspx.vb:12:Imports Wypozyczalnia.Core.Integracje
Cennik.aspx.vb:13:
Cennik.aspx.vb:14:Partial Class Cennik
Cennik.aspx.vb:15:    Inherits System.Web.UI.Page
Cennik.aspx.vb:16:
Cennik.aspx.vb:17:    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Cennik))
Cennik.aspx.vb:18:
Cennik.aspx.vb:19:    Protected WithEvents lblWynik As Global.System.Web.UI.WebControls.Label
Cennik.aspx.vb:20:    Protected WithEvents gvTaryfy As Global.System.Web.UI.WebControls.GridView
Cennik.aspx.vb:21:    Protected WithEvents ddlSprzet As Global.System.Web.UI.WebControls.DropDownList
Cennik.aspx.vb:22:    Protected WithEvents txtStawka As Global.System.Web.UI.WebControls.TextBox
Cennik.aspx.vb:23:    Protected WithEvents btnZapiszStawke As Global.System.Web.UI.WebControls.Button
Cennik.aspx.vb:24:    Protected WithEvents btnImportuj As Global.System.Web.UI.WebControls.Button
Cennik.aspx.vb:25:    Protected WithEvents lblKlient As Global.System.Web.UI.WebControls.Label
Cennik.aspx.vb:26:    Protected WithEvents ddlSprzetKalk As Global.System.Web.UI.WebControls.DropDownList
Cennik.aspx.vb:27:    Protected WithEvents txtOd As Global.System.Web.UI.WebControls.TextBox
Cennik.aspx.vb:28:    Protected WithEvents txtDo As Global.System.Web.UI.WebControls.TextBox
Cennik.aspx.vb:29:    Protected WithEvents btnOblicz As Global.System.Web.UI.WebControls.Button
Cennik.aspx.vb:30:    Protected WithEvents lblKwota As Global.System.Web.UI.WebControls.Label
Cennik.aspx.vb:31:    Protected WithEvents lblSzczegoly As Global.System.Web.UI.WebControls.Label
Cennik.aspx.vb:32:
Cennik.aspx.vb:33:    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
Cennik.aspx.vb:34:        If Not IsPostBack Then
Cennik.aspx.vb:35:            ZaladujTaryfy()
Cennik.aspx.vb:36:            ZaladujSprzet()
Cennik.aspx.vb:37:            PokazAktualnaStawke()
Cennik.aspx.vb:38:            txtOd.Text = Date.Today.AddDays(1).ToString("yyyy-MM-dd") & " 08:00"
Cennik.aspx.vb:39:            txtDo.Text = Date.Today.AddDays(2).ToString("yyyy-MM-dd") & " 08:00"
Cennik.aspx.vb:40:        End If
Cennik.aspx.vb:41:        PokazKlienta()
Cennik.aspx.vb:42:    End Sub
Cennik.aspx.vb:43:
Cennik.aspx.vb:44:    Protected Sub ddlSprzet_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlSprzet.SelectedIndexChanged
Cennik.aspx.vb:45:        PokazAktualnaStawke()
Cennik.aspx.vb:46:    End Sub
Cennik.aspx.vb:47:
Cennik.aspx.vb:48:    Protected Sub btnZapiszStawke_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZapiszStawke.Click
Cennik.aspx.vb:49:        If txtStawka.Text.Trim() = "" Then
Cennik.aspx.vb:50:            lblWynik.Text = "Podaj nową stawkę."
Cennik.aspx.vb:51:            Return
Cennik.aspx.vb:52:        End If
Cennik.aspx.vb:53:        If ddlSprzet.SelectedValue = "" Then
Cennik.aspx.vb:54:            lblWynik.Text = "Wybierz sprzęt."
Cennik.aspx.vb:55:            Return
Cennik.aspx.vb:56:        End If
Cennik.aspx.vb:57:        Try
Cennik.aspx.vb:58:            Dim sql As String = "UPDATE Sprzet SET StawkaDobowa = " & txtStawka.Text.Replace(",", ".") & " WHERE Id = " & ddlSprzet.SelectedValue
Cennik.aspx.vb:59:            Dim ile As Integer = DbHelper.WykonajPolecenie(sql)
Cennik.aspx.vb:60:            If ile = 1 Then
Cennik.aspx.vb:61:                lblWynik.Text = "Zapisano stawkę " & txtStawka.Text & " zł dla " & ddlSprzet.SelectedItem.Text & "."
Cennik.aspx.vb:62:                Log.Info("Zmiana stawki z ekranu cennika: " & ddlSprzet.SelectedValue & " = " & txtStawka.Text)
Cennik.aspx.vb:63:            Else
Cennik.aspx.vb:64:                lblWynik.Text = "Nie znaleziono sprzętu."
Cennik.aspx.vb:65:            End If
Cennik.aspx.vb:66:            txtStawka.Text = ""
Cennik.aspx.vb:67:            ZaladujSprzet()
Cennik.aspx.vb:68:            PokazAktualnaStawke()
Cennik.aspx.vb:69:        Catch ex As Exception
Cennik.aspx.vb:70:            Log.Error("Blad zapisu stawki", ex)
Cennik.aspx.vb:71:            lblWynik.Text = "Błąd zapisu stawki: " & ex.Message
Cennik.aspx.vb:72:        End Try
Cennik.aspx.vb:73:    End Sub
Cennik.aspx.vb:74:
Cennik.aspx.vb:75:    Protected Sub btnImportuj_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnImportuj.Click
Cennik.aspx.vb:76:        Dim katalog As String = ConfigurationManager.AppSettings("ImportCennikaKatalog")
Cennik.aspx.vb:77:        Dim import As New ImportCennikaXml()
Cennik.aspx.vb:78:        Try
Cennik.aspx.vb:79:            Dim oczekujace As String() = import.PlikiOczekujace(katalog)
Cennik.aspx.vb:80:            If oczekujace.Length = 0 Then
Cennik.aspx.vb:81:                lblWynik.Text = "Brak plików do importu w katalogu " & katalog & "."
Cennik.aspx.vb:82:                Return
Cennik.aspx.vb:83:            End If
Cennik.aspx.vb:84:            Dim ile As Integer = import.ImportujKatalog(katalog)
Cennik.aspx.vb:85:            lblWynik.Text = "Zaimportowano plików: " & ile & " (" & String.Join(", ", oczekujace) & ")."
Cennik.aspx.vb:86:            ZaladujTaryfy()
Cennik.aspx.vb:87:            ZaladujSprzet()
Cennik.aspx.vb:88:            PokazAktualnaStawke()
Cennik.aspx.vb:89:        Catch ex As System.IO.IOException
Cennik.aspx.vb:90:            Log.Error("Import cennika - blad pliku", ex)
Cennik.aspx.vb:91:            lblWynik.Text = "Błąd dostępu do katalogu importu: " & ex.Message
Cennik.aspx.vb:92:        Catch ex As System.Xml.XmlException
Cennik.aspx.vb:93:            Log.Error("Import cennika - niepoprawny XML", ex)
Cennik.aspx.vb:94:            lblWynik.Text = "Niepoprawny plik XML: " & ex.Message
Cennik.aspx.vb:95:        Catch ex As FormatException
Cennik.aspx.vb:96:            Log.Error("Import cennika - niepoprawna liczba", ex)
Cennik.aspx.vb:97:            lblWynik.Text = "Niepoprawna wartość liczbowa w pliku: " & ex.Message
Cennik.aspx.vb:98:        End Try
Cennik.aspx.vb:99:    End Sub
Cennik.aspx.vb:100:
Cennik.aspx.vb:101:    Protected Sub btnOblicz_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOblicz.Click
Cennik.aspx.vb:102:        lblKwota.Text = ""
Cennik.aspx.vb:103:        lblSzczegoly.Text = ""
Cennik.aspx.vb:104:        Dim klientId As Integer = Session("WybranyKlientId")
Cennik.aspx.vb:105:        Dim dataOd As Date
Cennik.aspx.vb:106:        Dim dataDo As Date
Cennik.aspx.vb:107:        If Not Date.TryParse(txtOd.Text, dataOd) Then
Cennik.aspx.vb:108:            lblWynik.Text = "Niepoprawna data od."
Cennik.aspx.vb:109:            Return
Cennik.aspx.vb:110:        End If
Cennik.aspx.vb:111:        If Not Date.TryParse(txtDo.Text, dataDo) Then
Cennik.aspx.vb:112:            lblWynik.Text = "Niepoprawna data do."
Cennik.aspx.vb:113:            Return
Cennik.aspx.vb:114:        End If
Cennik.aspx.vb:115:        If ddlSprzetKalk.SelectedValue = "" Then
Cennik.aspx.vb:116:            lblWynik.Text = "Wybierz sprzęt do kalkulacji."
Cennik.aspx.vb:117:            Return
Cennik.aspx.vb:118:        End If
Cennik.aspx.vb:119:        Dim pm As New PricingManager()
Cennik.aspx.vb:120:        Try
Cennik.aspx.vb:121:            Dim kwota As Decimal = pm.ObliczOplate(CInt(ddlSprzetKalk.SelectedValue), dataOd, dataDo, klientId)
Cennik.aspx.vb:122:            lblKwota.Text = kwota.ToString("N2") & " zł (" & pm.LiczbaDob(dataOd, dataDo) & " dób)"
Cennik.aspx.vb:123:            lblSzczegoly.Text = Server.HtmlEncode(pm.SzczegolyOplaty(CInt(ddlSprzetKalk.SelectedValue), dataOd, dataDo, klientId))
Cennik.aspx.vb:124:        Catch ex As ArgumentException
Cennik.aspx.vb:125:            lblWynik.Text = ex.Message
Cennik.aspx.vb:126:        Catch ex As BladBiznesowyException
Cennik.aspx.vb:127:            lblWynik.Text = ex.Message
Cennik.aspx.vb:128:        End Try
Cennik.aspx.vb:129:    End Sub
Cennik.aspx.vb:130:
Cennik.aspx.vb:131:    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PreRender
Cennik.aspx.vb:132:        Dim katalog As String = ConfigurationManager.AppSettings("ImportCennikaKatalog")
Cennik.aspx.vb:133:        If String.IsNullOrEmpty(katalog) Then
Cennik.aspx.vb:134:            btnImportuj.Enabled = False
Cennik.aspx.vb:135:            btnImportuj.ToolTip = "Brak klucza ImportCennikaKatalog w konfiguracji"
Cennik.aspx.vb:136:        Else
Cennik.aspx.vb:137:            btnImportuj.ToolTip = "Katalog: " & katalog
Cennik.aspx.vb:138:        End If
Cennik.aspx.vb:139:    End Sub
Cennik.aspx.vb:140:
Cennik.aspx.vb:141:    Private _aktualneKategorie As New List(Of String)()
Cennik.aspx.vb:142:
Cennik.aspx.vb:143:    Protected Sub gvTaryfy_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvTaryfy.RowDataBound
Cennik.aspx.vb:144:        If e.Row.RowType <> DataControlRowType.DataRow Then Return
Cennik.aspx.vb:145:        Dim wiersz As DataRowView = CType(e.Row.DataItem, DataRowView)
Cennik.aspx.vb:146:        Dim kategoria As String = wiersz("Kategoria").ToString().Trim()
Cennik.aspx.vb:147:        Dim od As Date = wiersz("ObowiazujeOd")
Cennik.aspx.vb:148:        If od > Date.Today Then
Cennik.aspx.vb:149:            e.Row.ForeColor = Drawing.Color.Gray
Cennik.aspx.vb:150:            e.Row.ToolTip = "Taryfa przyszła"
Cennik.aspx.vb:151:            Return
Cennik.aspx.vb:152:        End If
Cennik.aspx.vb:153:        If Not _aktualneKategorie.Contains(kategoria) Then
Cennik.aspx.vb:154:            _aktualneKategorie.Add(kategoria)
Cennik.aspx.vb:155:            e.Row.Font.Bold = True
Cennik.aspx.vb:156:            e.Row.ToolTip = "Taryfa obowiązująca"
Cennik.aspx.vb:157:        Else
Cennik.aspx.vb:158:            e.Row.ForeColor = Drawing.Color.DarkGray
Cennik.aspx.vb:159:            e.Row.ToolTip = "Taryfa archiwalna"
Cennik.aspx.vb:160:        End If
Cennik.aspx.vb:161:    End Sub
Cennik.aspx.vb:162:
Cennik.aspx.vb:163:    Private Sub ZaladujTaryfy()
Cennik.aspx.vb:164:        Dim sql As String = "SELECT Id, Kategoria, MnoznikWeekend, RabatTygodniowyProc, ObowiazujeOd FROM Taryfy ORDER BY Kategoria, ObowiazujeOd DESC"
Cennik.aspx.vb:165:        Try
Cennik.aspx.vb:166:            gvTaryfy.DataSource = DbHelper.WykonajZapytanie(sql)
Cennik.aspx.vb:167:            gvTaryfy.DataBind()
Cennik.aspx.vb:168:        Catch ex As Exception
Cennik.aspx.vb:169:            Log.Error("Blad odczytu taryf", ex)
Cennik.aspx.vb:170:            lblWynik.Text = "Nie udało się wczytać taryf."
Cennik.aspx.vb:171:        End Try
Cennik.aspx.vb:172:    End Sub
Cennik.aspx.vb:173:
Cennik.aspx.vb:174:    Private Sub ZaladujSprzet()
Cennik.aspx.vb:175:        Dim wybrany As String = ddlSprzet.SelectedValue
Cennik.aspx.vb:176:        Dim wybranyKalk As String = ddlSprzetKalk.SelectedValue
Cennik.aspx.vb:177:        Dim lista As List(Of Sprzet) = New EquipmentManager().ListaWKategorii("")
Cennik.aspx.vb:178:
Cennik.aspx.vb:179:        ddlSprzet.Items.Clear()
Cennik.aspx.vb:180:        ddlSprzetKalk.Items.Clear()
Cennik.aspx.vb:181:        ddlSprzet.Items.Add(New ListItem("-- wybierz --", ""))
Cennik.aspx.vb:182:        ddlSprzetKalk.Items.Add(New ListItem("-- wybierz --", ""))
Cennik.aspx.vb:183:        For Each s As Sprzet In lista
Cennik.aspx.vb:184:            Dim tekst As String = s.NumerInwentarzowy & " " & s.Nazwa
Cennik.aspx.vb:185:            ddlSprzet.Items.Add(New ListItem(tekst, s.Id.ToString()))
Cennik.aspx.vb:186:            ddlSprzetKalk.Items.Add(New ListItem(tekst, s.Id.ToString()))
Cennik.aspx.vb:187:        Next
Cennik.aspx.vb:188:        If wybrany <> "" AndAlso ddlSprzet.Items.FindByValue(wybrany) IsNot Nothing Then
Cennik.aspx.vb:189:            ddlSprzet.SelectedValue = wybrany
Cennik.aspx.vb:190:        End If
Cennik.aspx.vb:191:        If wybranyKalk <> "" AndAlso ddlSprzetKalk.Items.FindByValue(wybranyKalk) IsNot Nothing Then
Cennik.aspx.vb:192:            ddlSprzetKalk.SelectedValue = wybranyKalk
Cennik.aspx.vb:193:        End If
Cennik.aspx.vb:194:    End Sub
Cennik.aspx.vb:195:
Cennik.aspx.vb:196:    Private Sub PokazAktualnaStawke()
Cennik.aspx.vb:197:        If ddlSprzet.SelectedValue = "" Then
Cennik.aspx.vb:198:            txtStawka.ToolTip = ""
Cennik.aspx.vb:199:            Return
Cennik.aspx.vb:200:        End If
Cennik.aspx.vb:201:        Dim s As Sprzet = New EquipmentManager().PobierzSprzet(CInt(ddlSprzet.SelectedValue))
Cennik.aspx.vb:202:        If s Is Nothing Then Return
Cennik.aspx.vb:203:        txtStawka.ToolTip = "Aktualnie: " & s.StawkaDobowa.ToString("N2") & " zł, kaucja " & s.Kaucja.ToString("N2") & " zł"
Cennik.aspx.vb:204:        txtStawka.Attributes("placeholder") = s.StawkaDobowa.ToString("N2")
Cennik.aspx.vb:205:    End Sub
Cennik.aspx.vb:206:
Cennik.aspx.vb:207:    Private Sub PokazKlienta()
Cennik.aspx.vb:208:        Dim klientId As Integer = Session("WybranyKlientId")
Cennik.aspx.vb:209:        If klientId = 0 Then
Cennik.aspx.vb:210:            lblKlient.Text = "Klient: (nie wybrano - kalkulacja bez rabatu)"
Cennik.aspx.vb:211:            Return
Cennik.aspx.vb:212:        End If
Cennik.aspx.vb:213:        Dim k As Klient = New CustomerManager().PobierzKlienta(klientId)
Cennik.aspx.vb:214:        If k Is Nothing Then
Cennik.aspx.vb:215:            lblKlient.Text = "Klient: (nie znaleziono)"
Cennik.aspx.vb:216:        ElseIf k.Typ = "F" Then
Cennik.aspx.vb:217:            lblKlient.Text = "Klient: " & Server.HtmlEncode(k.NazwaFirmy) & " (próg rabatu " & k.ProgRabatu & ")"
Cennik.aspx.vb:218:        Else
Cennik.aspx.vb:219:            lblKlient.Text = "Klient: " & Server.HtmlEncode(k.Imie & " " & k.Nazwisko) & " (próg rabatu " & k.ProgRabatu & ")"
Cennik.aspx.vb:220:        End If
Cennik.aspx.vb:221:    End Sub
Cennik.aspx.vb:222:
Cennik.aspx.vb:223:End Class
Faktura.aspx.vb:1:' Modul: Faktura.aspx.vb | Autor: zespol IT | 2014-05-19
Faktura.aspx.vb:2:' Faktura za wypozyczenie: wystawienie, platnosc przez bramke, eksport CSV dnia.
Faktura.aspx.vb:3:' 2015-01-12 MW: platnosc przez bramke SOAP (zamiast strony Platnosc.aspx)
Faktura.aspx.vb:4:' 2018-04-03 PT: lista faktur z dnia i przycisk eksportu CSV
Faktura.aspx.vb:5:
Faktura.aspx.vb:6:Imports System.Data
Faktura.aspx.vb:7:Imports System.Net
Faktura.aspx.vb:8:Imports System.Web.Services.Protocols
Faktura.aspx.vb:9:Imports Wypozyczalnia.Core
Faktura.aspx.vb:10:Imports Wypozyczalnia.Core.Model
Faktura.aspx.vb:11:Imports Wypozyczalnia.Core.Integracje
Faktura.aspx.vb:12:
Faktura.aspx.vb:13:Partial Class Faktura
Faktura.aspx.vb:14:    Inherits System.Web.UI.Page
Faktura.aspx.vb:15:
Faktura.aspx.vb:16:    Protected WithEvents lblKomunikat As Global.System.Web.UI.WebControls.Label
Faktura.aspx.vb:17:    Protected WithEvents lblWypozyczenie As Global.System.Web.UI.WebControls.Label
Faktura.aspx.vb:18:    Protected WithEvents lblKlient As Global.System.Web.UI.WebControls.Label
Faktura.aspx.vb:19:    Protected WithEvents lblNumer As Global.System.Web.UI.WebControls.Label
Faktura.aspx.vb:20:    Protected WithEvents lblStatus As Global.System.Web.UI.WebControls.Label
Faktura.aspx.vb:21:    Protected WithEvents btnWystaw As Global.System.Web.UI.WebControls.Button
Faktura.aspx.vb:22:    Protected WithEvents gvPozycje As Global.System.Web.UI.WebControls.GridView
Faktura.aspx.vb:23:    Protected WithEvents lblNetto As Global.System.Web.UI.WebControls.Label
Faktura.aspx.vb:24:    Protected WithEvents lblVat As Global.System.Web.UI.WebControls.Label
Faktura.aspx.vb:25:    Protected WithEvents lblBrutto As Global.System.Web.UI.WebControls.Label
Faktura.aspx.vb:26:    Protected WithEvents btnZaplac As Global.System.Web.UI.WebControls.Button
Faktura.aspx.vb:27:    Protected WithEvents lblPlatnosc As Global.System.Web.UI.WebControls.Label
Faktura.aspx.vb:28:    Protected WithEvents gvFakturyDnia As Global.System.Web.UI.WebControls.GridView
Faktura.aspx.vb:29:    Protected WithEvents btnEksportCsv As Global.System.Web.UI.WebControls.Button
Faktura.aspx.vb:30:    Protected WithEvents lblEksport As Global.System.Web.UI.WebControls.Label
Faktura.aspx.vb:31:
Faktura.aspx.vb:32:    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Faktura))
Faktura.aspx.vb:33:
Faktura.aspx.vb:34:    Private Const Waluta As String = "PLN"
Faktura.aspx.vb:35:    Private Const FormatDaty As String = "yyyy-MM-dd HH:mm"
Faktura.aspx.vb:36:
Faktura.aspx.vb:37:    Private Property WypozyczenieId As Integer
Faktura.aspx.vb:38:        Get
Faktura.aspx.vb:39:            If ViewState("WypozyczenieId") Is Nothing Then Return 0
Faktura.aspx.vb:40:            Return ViewState("WypozyczenieId")
Faktura.aspx.vb:41:        End Get
Faktura.aspx.vb:42:        Set(ByVal value As Integer)
Faktura.aspx.vb:43:            ViewState("WypozyczenieId") = value
Faktura.aspx.vb:44:        End Set
Faktura.aspx.vb:45:    End Property
Faktura.aspx.vb:46:
Faktura.aspx.vb:47:    Private Property FakturaId As Integer
Faktura.aspx.vb:48:        Get
Faktura.aspx.vb:49:            If ViewState("FakturaId") Is Nothing Then Return 0
Faktura.aspx.vb:50:            Return ViewState("FakturaId")
Faktura.aspx.vb:51:        End Get
Faktura.aspx.vb:52:        Set(ByVal value As Integer)
Faktura.aspx.vb:53:            ViewState("FakturaId") = value
Faktura.aspx.vb:54:        End Set
Faktura.aspx.vb:55:    End Property
Faktura.aspx.vb:56:
Faktura.aspx.vb:57:    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
Faktura.aspx.vb:58:        If Not IsPostBack Then
Faktura.aspx.vb:59:            WypozyczenieId = UstalWypozyczenie()
Faktura.aspx.vb:60:            PokazWypozyczenie()
Faktura.aspx.vb:61:            WczytajFakturyDnia()
Faktura.aspx.vb:62:        End If
Faktura.aspx.vb:63:    End Sub
Faktura.aspx.vb:64:
Faktura.aspx.vb:65:    ' najpierw ?id= z linku po zwrocie, potem sesja
Faktura.aspx.vb:66:    Private Function UstalWypozyczenie() As Integer
Faktura.aspx.vb:67:        Dim id As Integer = 0
Faktura.aspx.vb:68:        Dim zParametru As String = Request.QueryString("id")
Faktura.aspx.vb:69:        If Not String.IsNullOrEmpty(zParametru) Then
Faktura.aspx.vb:70:            Integer.TryParse(zParametru, id)
Faktura.aspx.vb:71:        End If
Faktura.aspx.vb:72:        If id = 0 AndAlso Session("OstatnieWypozyczenieId") IsNot Nothing Then
Faktura.aspx.vb:73:            id = Session("OstatnieWypozyczenieId")
Faktura.aspx.vb:74:        End If
Faktura.aspx.vb:75:        Return id
Faktura.aspx.vb:76:    End Function
Faktura.aspx.vb:77:
Faktura.aspx.vb:78:    Private Sub PokazWypozyczenie()
Faktura.aspx.vb:79:        WyczyscFakture()
Faktura.aspx.vb:80:        If WypozyczenieId = 0 Then
Faktura.aspx.vb:81:            lblWypozyczenie.Text = "nie wybrano (wejdź z ekranu zwrotu)"
Faktura.aspx.vb:82:            PokazWybranegoKlienta()
Faktura.aspx.vb:83:            btnWystaw.Enabled = False
Faktura.aspx.vb:84:            btnZaplac.Enabled = False
Faktura.aspx.vb:85:            Return
Faktura.aspx.vb:86:        End If
Faktura.aspx.vb:87:
Faktura.aspx.vb:88:        Dim w As Wypozyczenie = New RentalManager().PobierzWypozyczenie(WypozyczenieId)
Faktura.aspx.vb:89:        If w Is Nothing Then
Faktura.aspx.vb:90:            lblKomunikat.Text = "Nie znaleziono wypożyczenia nr " & WypozyczenieId
Faktura.aspx.vb:91:            btnWystaw.Enabled = False
Faktura.aspx.vb:92:            btnZaplac.Enabled = False
Faktura.aspx.vb:93:            Return
Faktura.aspx.vb:94:        End If
Faktura.aspx.vb:95:
Faktura.aspx.vb:96:        Dim sprzet As Sprzet = New EquipmentManager().PobierzSprzet(w.SprzetId)
Faktura.aspx.vb:97:        Dim opis As String = "#" & w.Id & " | wydano " & w.DataWydania.ToString(FormatDaty)
Faktura.aspx.vb:98:        If sprzet IsNot Nothing Then opis = opis & " | " & sprzet.Nazwa
Faktura.aspx.vb:99:        If w.DataZwrotu.HasValue Then
Faktura.aspx.vb:100:            opis = opis & " | zwrot " & w.DataZwrotu.Value.ToString(FormatDaty)
Faktura.aspx.vb:101:        Else
Faktura.aspx.vb:102:            opis = opis & " | jeszcze nie zwrócono"
Faktura.aspx.vb:103:        End If
Faktura.aspx.vb:104:        lblWypozyczenie.Text = opis
Faktura.aspx.vb:105:
Faktura.aspx.vb:106:        Dim klient As Klient = New CustomerManager().PobierzKlienta(w.KlientId)
Faktura.aspx.vb:107:        If klient IsNot Nothing Then
Faktura.aspx.vb:108:            lblKlient.Text = NazwaKlienta(klient)
Faktura.aspx.vb:109:        End If
Faktura.aspx.vb:110:        If Session("WybranyKlientId") <> 0 AndAlso Session("WybranyKlientId") <> w.KlientId Then
Faktura.aspx.vb:111:            lblKomunikat.Text = "Uwaga: wypożyczenie należy do innego klienta niż wybrany na liście klientów."
Faktura.aspx.vb:112:        End If
Faktura.aspx.vb:113:
Faktura.aspx.vb:114:        Dim mgr As New InvoiceManager()
Faktura.aspx.vb:115:        Dim f As Wypozyczalnia.Core.Model.Faktura = mgr.PobierzFaktureDlaWypozyczenia(w.Id)
Faktura.aspx.vb:116:        If f IsNot Nothing Then
Faktura.aspx.vb:117:            PokazFakture(f)
Faktura.aspx.vb:118:            btnWystaw.Enabled = False
Faktura.aspx.vb:119:        ElseIf w.Status = "Z" Then
Faktura.aspx.vb:120:            Dim podglad As Wypozyczalnia.Core.Model.Faktura = mgr.PodgladFaktury(w.Id)
Faktura.aspx.vb:121:            If podglad IsNot Nothing Then
Faktura.aspx.vb:122:                PokazPozycje(podglad)
Faktura.aspx.vb:123:                lblNumer.Text = "(podgląd - faktura nie wystawiona)"
Faktura.aspx.vb:124:            End If
Faktura.aspx.vb:125:            btnWystaw.Enabled = True
Faktura.aspx.vb:126:            btnZaplac.Enabled = False
Faktura.aspx.vb:127:        Else
Faktura.aspx.vb:128:            lblKomunikat.Text = "Fakturę można wystawić dopiero po zwrocie sprzętu."
Faktura.aspx.vb:129:            btnWystaw.Enabled = False
Faktura.aspx.vb:130:            btnZaplac.Enabled = False
Faktura.aspx.vb:131:        End If
Faktura.aspx.vb:132:    End Sub
Faktura.aspx.vb:133:
Faktura.aspx.vb:134:    Private Sub PokazWybranegoKlienta()
Faktura.aspx.vb:135:        If Session("WybranyKlientId") Is Nothing OrElse Session("WybranyKlientId") = 0 Then
Faktura.aspx.vb:136:            lblKlient.Text = "-"
Faktura.aspx.vb:137:            Return
Faktura.aspx.vb:138:        End If
Faktura.aspx.vb:139:        Dim klient As Klient = New CustomerManager().PobierzKlienta(Session("WybranyKlientId"))
Faktura.aspx.vb:140:        If klient IsNot Nothing Then
Faktura.aspx.vb:141:            lblKlient.Text = NazwaKlienta(klient) & " (wybrany na liście klientów)"
Faktura.aspx.vb:142:        End If
Faktura.aspx.vb:143:    End Sub
Faktura.aspx.vb:144:
Faktura.aspx.vb:145:    Private Sub PokazFakture(ByVal f As Wypozyczalnia.Core.Model.Faktura)
Faktura.aspx.vb:146:        FakturaId = f.Id
Faktura.aspx.vb:147:        lblNumer.Text = f.Numer & " z dnia " & f.DataWystawienia.ToString("yyyy-MM-dd")
Faktura.aspx.vb:148:        If f.Status = "O" Then
Faktura.aspx.vb:149:            lblStatus.Text = "OPŁACONA (transakcja " & f.IdTransakcji & ")"
Faktura.aspx.vb:150:            lblStatus.ForeColor = Drawing.Color.Green
Faktura.aspx.vb:151:            btnZaplac.Enabled = False
Faktura.aspx.vb:152:        Else
Faktura.aspx.vb:153:            lblStatus.Text = "do zapłaty"
Faktura.aspx.vb:154:            lblStatus.ForeColor = Drawing.Color.DarkOrange
Faktura.aspx.vb:155:            btnZaplac.Enabled = True
Faktura.aspx.vb:156:        End If
Faktura.aspx.vb:157:        PokazPozycje(f)
Faktura.aspx.vb:158:    End Sub
Faktura.aspx.vb:159:
Faktura.aspx.vb:160:    Private Sub PokazPozycje(ByVal f As Wypozyczalnia.Core.Model.Faktura)
Faktura.aspx.vb:161:        gvPozycje.DataSource = f.Pozycje
Faktura.aspx.vb:162:        gvPozycje.DataBind()
Faktura.aspx.vb:163:        lblNetto.Text = f.Netto
Faktura.aspx.vb:164:        lblVat.Text = f.Vat
Faktura.aspx.vb:165:        lblBrutto.Text = f.Brutto
Faktura.aspx.vb:166:    End Sub
Faktura.aspx.vb:167:
Faktura.aspx.vb:168:    Private Sub WyczyscFakture()
Faktura.aspx.vb:169:        FakturaId = 0
Faktura.aspx.vb:170:        lblNumer.Text = "(nie wystawiono)"
Faktura.aspx.vb:171:        lblStatus.Text = ""
Faktura.aspx.vb:172:        lblNetto.Text = "0,00"
Faktura.aspx.vb:173:        lblVat.Text = "0,00"
Faktura.aspx.vb:174:        lblBrutto.Text = "0,00"
Faktura.aspx.vb:175:        lblPlatnosc.Text = ""
Faktura.aspx.vb:176:        gvPozycje.DataSource = Nothing
Faktura.aspx.vb:177:        gvPozycje.DataBind()
Faktura.aspx.vb:178:    End Sub
Faktura.aspx.vb:179:
Faktura.aspx.vb:180:    Private Sub WczytajFakturyDnia()
Faktura.aspx.vb:181:        Try
Faktura.aspx.vb:182:            gvFakturyDnia.DataSource = New InvoiceManager().FakturyZDnia(Date.Today)
Faktura.aspx.vb:183:            gvFakturyDnia.DataBind()
Faktura.aspx.vb:184:        Catch ex As Exception
Faktura.aspx.vb:185:            Log.Error("Blad pobierania faktur z dnia", ex)
Faktura.aspx.vb:186:        End Try
Faktura.aspx.vb:187:    End Sub
Faktura.aspx.vb:188:
Faktura.aspx.vb:189:    Protected Sub btnWystaw_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnWystaw.Click
Faktura.aspx.vb:190:        If WypozyczenieId = 0 Then
Faktura.aspx.vb:191:            lblKomunikat.Text = "Nie wybrano wypożyczenia."
Faktura.aspx.vb:192:            Return
Faktura.aspx.vb:193:        End If
Faktura.aspx.vb:194:        Try
Faktura.aspx.vb:195:            Dim f As Wypozyczalnia.Core.Model.Faktura = New InvoiceManager().WystawFaktureZaWypozyczenie(WypozyczenieId)
Faktura.aspx.vb:196:            PokazFakture(f)
Faktura.aspx.vb:197:            btnWystaw.Enabled = False
Faktura.aspx.vb:198:            lblKomunikat.ForeColor = Drawing.Color.Green
Faktura.aspx.vb:199:            lblKomunikat.Text = "Wystawiono fakturę " & f.Numer
Faktura.aspx.vb:200:            WczytajFakturyDnia()
Faktura.aspx.vb:201:        Catch ex As BladBiznesowyException
Faktura.aspx.vb:202:            lblKomunikat.Text = ex.Message
Faktura.aspx.vb:203:        Catch ex As Exception
Faktura.aspx.vb:204:            Log.Error("Blad wystawiania faktury dla wypozyczenia " & WypozyczenieId, ex)
Faktura.aspx.vb:205:            lblKomunikat.Text = "Błąd wystawiania faktury: " & ex.Message
Faktura.aspx.vb:206:        End Try
Faktura.aspx.vb:207:    End Sub
Faktura.aspx.vb:208:
Faktura.aspx.vb:209:    Protected Sub btnZaplac_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZaplac.Click
Faktura.aspx.vb:210:        If FakturaId = 0 Then
Faktura.aspx.vb:211:            lblKomunikat.Text = "Najpierw wystaw fakturę."
Faktura.aspx.vb:212:            Return
Faktura.aspx.vb:213:        End If
Faktura.aspx.vb:214:        Dim mgr As New InvoiceManager()
Faktura.aspx.vb:215:        Dim f As Wypozyczalnia.Core.Model.Faktura = mgr.PobierzFakture(FakturaId)
Faktura.aspx.vb:216:        If f Is Nothing Then
Faktura.aspx.vb:217:            lblKomunikat.Text = "Nie znaleziono faktury."
Faktura.aspx.vb:218:            Return
Faktura.aspx.vb:219:        End If
Faktura.aspx.vb:220:        If f.Status = "O" Then
Faktura.aspx.vb:221:            lblPlatnosc.Text = "Faktura jest już opłacona."
Faktura.aspx.vb:222:            Return
Faktura.aspx.vb:223:        End If
Faktura.aspx.vb:224:
Faktura.aspx.vb:225:        Dim bramka As New BramkaPlatnosci.BramkaService()
Faktura.aspx.vb:226:        bramka.Timeout = 30000
Faktura.aspx.vb:227:        Try
Faktura.aspx.vb:228:            Dim wynik As BramkaPlatnosci.WynikPlatnosci = bramka.ZarejestrujPlatnosc(f.Numer, f.Brutto, Waluta)
Faktura.aspx.vb:229:            Log.Info("Bramka: faktura " & f.Numer & ", status " & wynik.Status & ", transakcja " & wynik.IdTransakcji)
Faktura.aspx.vb:230:            Select Case wynik.Status
Faktura.aspx.vb:231:                Case "OK"
Faktura.aspx.vb:232:                    mgr.OznaczOplacona(f.Id, wynik.IdTransakcji)
Faktura.aspx.vb:233:                    lblPlatnosc.ForeColor = Drawing.Color.Green
Faktura.aspx.vb:234:                    lblPlatnosc.Text = "Płatność przyjęta, transakcja " & wynik.IdTransakcji
Faktura.aspx.vb:235:                    Session("OstatnieWypozyczenieId") = Nothing
Faktura.aspx.vb:236:                    PokazFakture(mgr.PobierzFakture(f.Id))
Faktura.aspx.vb:237:                    WczytajFakturyDnia()
Faktura.aspx.vb:238:                Case "ODRZUCONA"
Faktura.aspx.vb:239:                    lblPlatnosc.ForeColor = Drawing.Color.Red
Faktura.aspx.vb:240:                    lblPlatnosc.Text = "Bramka odrzuciła płatność: " & wynik.Komunikat
Faktura.aspx.vb:241:                Case Else
Faktura.aspx.vb:242:                    lblPlatnosc.ForeColor = Drawing.Color.Red
Faktura.aspx.vb:243:                    lblPlatnosc.Text = "Błąd bramki: " & wynik.Komunikat
Faktura.aspx.vb:244:            End Select
Faktura.aspx.vb:245:        Catch ex As SoapException
Faktura.aspx.vb:246:            Log.Error("SOAP: blad bramki dla faktury " & f.Numer, ex)
Faktura.aspx.vb:247:            lblPlatnosc.Text = "Bramka zwróciła błąd: " & ex.Message
Faktura.aspx.vb:248:        Catch ex As WebException
Faktura.aspx.vb:249:            Log.Error("Brak polaczenia z bramka platnosci", ex)
Faktura.aspx.vb:250:            lblPlatnosc.Text = "Brak połączenia z bramką płatności. Spróbuj ponownie później."
Faktura.aspx.vb:251:        Finally
Faktura.aspx.vb:252:            bramka.Dispose()
Faktura.aspx.vb:253:        End Try
Faktura.aspx.vb:254:    End Sub
Faktura.aspx.vb:255:
Faktura.aspx.vb:256:    Protected Sub btnEksportCsv_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEksportCsv.Click
Faktura.aspx.vb:257:        Try
Faktura.aspx.vb:258:            Dim plik As String = New EksportCsv().EksportujDzien(Date.Today)
Faktura.aspx.vb:259:            lblEksport.ForeColor = Drawing.Color.Green
Faktura.aspx.vb:260:            lblEksport.Text = "Zapisano: " & plik
Faktura.aspx.vb:261:        Catch ex As Exception
Faktura.aspx.vb:262:            Log.Error("Blad eksportu CSV z ekranu faktur", ex)
Faktura.aspx.vb:263:            lblEksport.ForeColor = Drawing.Color.Red
Faktura.aspx.vb:264:            lblEksport.Text = "Eksport nieudany: " & ex.Message
Faktura.aspx.vb:265:        End Try
Faktura.aspx.vb:266:    End Sub
Faktura.aspx.vb:267:
Faktura.aspx.vb:268:    Private Function NazwaKlienta(ByVal k As Klient) As String
Faktura.aspx.vb:269:        If k.Typ = "F" AndAlso Not String.IsNullOrEmpty(k.NazwaFirmy) Then
Faktura.aspx.vb:270:            Return k.NazwaFirmy & " (" & k.Imie & " " & k.Nazwisko & ")"
Faktura.aspx.vb:271:        End If
Faktura.aspx.vb:272:        Return k.Imie & " " & k.Nazwisko
Faktura.aspx.vb:273:    End Function
Faktura.aspx.vb:274:
Faktura.aspx.vb:275:End Class
Klienci.aspx.vb:1:' Modul: Klienci.aspx.vb - lista, wyszukiwanie i edycja klientow | Autor: zespol IT | 2014-03-11
Klienci.aspx.vb:2:' Historia: 2014-03-11 JK utworzenie strony
Klienci.aspx.vb:3:'           2015-11-23 MW blokowanie klientow, przejscie do rezerwacji
Klienci.aspx.vb:4:'           2018-09-04 PT pole NIP i typ klienta (firmy)
Klienci.aspx.vb:5:Imports System.Data
Klienci.aspx.vb:6:Imports System.Data.SqlClient
Klienci.aspx.vb:7:Imports Wypozyczalnia.Core
Klienci.aspx.vb:8:Imports Wypozyczalnia.Core.Model
Klienci.aspx.vb:9:Imports Wypozyczalnia.Core.DAL
Klienci.aspx.vb:10:
Klienci.aspx.vb:11:Partial Class Klienci
Klienci.aspx.vb:12:    Inherits System.Web.UI.Page
Klienci.aspx.vb:13:
Klienci.aspx.vb:14:    Protected WithEvents lblKomunikat As Global.System.Web.UI.WebControls.Label
Klienci.aspx.vb:15:    Protected WithEvents txtSzukaj As Global.System.Web.UI.WebControls.TextBox
Klienci.aspx.vb:16:    Protected WithEvents btnSzukaj As Global.System.Web.UI.WebControls.Button
Klienci.aspx.vb:17:    Protected WithEvents btnNowy As Global.System.Web.UI.WebControls.Button
Klienci.aspx.vb:18:    Protected WithEvents gvKlienci As Global.System.Web.UI.WebControls.GridView
Klienci.aspx.vb:19:    Protected WithEvents lblTrybFormularza As Global.System.Web.UI.WebControls.Label
Klienci.aspx.vb:20:    Protected WithEvents hfKlientId As Global.System.Web.UI.WebControls.HiddenField
Klienci.aspx.vb:21:    Protected WithEvents txtImie As Global.System.Web.UI.WebControls.TextBox
Klienci.aspx.vb:22:    Protected WithEvents txtNazwisko As Global.System.Web.UI.WebControls.TextBox
Klienci.aspx.vb:23:    Protected WithEvents txtNazwaFirmy As Global.System.Web.UI.WebControls.TextBox
Klienci.aspx.vb:24:    Protected WithEvents txtEmail As Global.System.Web.UI.WebControls.TextBox
Klienci.aspx.vb:25:    Protected WithEvents txtTelefon As Global.System.Web.UI.WebControls.TextBox
Klienci.aspx.vb:26:    Protected WithEvents txtNumerDokumentu As Global.System.Web.UI.WebControls.TextBox
Klienci.aspx.vb:27:    Protected WithEvents txtNIP As Global.System.Web.UI.WebControls.TextBox
Klienci.aspx.vb:28:    Protected WithEvents ddlTyp As Global.System.Web.UI.WebControls.DropDownList
Klienci.aspx.vb:29:    Protected WithEvents ddlProg As Global.System.Web.UI.WebControls.DropDownList
Klienci.aspx.vb:30:    Protected WithEvents txtPowodBlokady As Global.System.Web.UI.WebControls.TextBox
Klienci.aspx.vb:31:    Protected WithEvents btnZapisz As Global.System.Web.UI.WebControls.Button
Klienci.aspx.vb:32:    Protected WithEvents btnZablokuj As Global.System.Web.UI.WebControls.Button
Klienci.aspx.vb:33:
Klienci.aspx.vb:34:    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Klienci))
Klienci.aspx.vb:35:
Klienci.aspx.vb:36:    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
Klienci.aspx.vb:37:        If Not IsPostBack Then
Klienci.aspx.vb:38:            WyczyscFormularz()
Klienci.aspx.vb:39:            ZaladujListe("")
Klienci.aspx.vb:40:            Dim wybrany As Integer = Session("WybranyKlientId")
Klienci.aspx.vb:41:            If wybrany > 0 Then
Klienci.aspx.vb:42:                lblKomunikat.Text = "Aktualnie wybrany klient: nr " & wybrany
Klienci.aspx.vb:43:                lblKomunikat.ForeColor = Drawing.Color.DarkGreen
Klienci.aspx.vb:44:            End If
Klienci.aspx.vb:45:        End If
Klienci.aspx.vb:46:    End Sub
Klienci.aspx.vb:47:
Klienci.aspx.vb:48:    Protected Sub btnSzukaj_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSzukaj.Click
Klienci.aspx.vb:49:        Dim sql As String = "SELECT Id, Imie, Nazwisko, NazwaFirmy, Email, Telefon FROM Klienci WHERE Nazwisko LIKE '%" & txtSzukaj.Text & "%' ORDER BY Nazwisko"
Klienci.aspx.vb:50:        Try
Klienci.aspx.vb:51:            Dim dt As DataTable = DbHelper.WykonajZapytanie(sql)
Klienci.aspx.vb:52:            gvKlienci.PageIndex = 0
Klienci.aspx.vb:53:            gvKlienci.DataSource = dt
Klienci.aspx.vb:54:            gvKlienci.DataBind()
Klienci.aspx.vb:55:            ViewState("OstatniaFraza") = txtSzukaj.Text
Klienci.aspx.vb:56:            If dt.Rows.Count = 0 Then
Klienci.aspx.vb:57:                lblKomunikat.Text = "Nie znaleziono klientow."
Klienci.aspx.vb:58:            End If
Klienci.aspx.vb:59:        Catch ex As SqlException
Klienci.aspx.vb:60:            Log.Error("Blad wyszukiwania klientow", ex)
Klienci.aspx.vb:61:            lblKomunikat.Text = "Blad bazy danych: " & ex.Message
Klienci.aspx.vb:62:        End Try
Klienci.aspx.vb:63:    End Sub
Klienci.aspx.vb:64:
Klienci.aspx.vb:65:    Protected Sub btnNowy_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNowy.Click
Klienci.aspx.vb:66:        WyczyscFormularz()
Klienci.aspx.vb:67:        gvKlienci.SelectedIndex = -1
Klienci.aspx.vb:68:    End Sub
Klienci.aspx.vb:69:
Klienci.aspx.vb:70:    Protected Sub gvKlienci_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gvKlienci.SelectedIndexChanged
Klienci.aspx.vb:71:        Dim klientId As Integer = gvKlienci.SelectedDataKey.Value
Klienci.aspx.vb:72:        Dim cm As New CustomerManager()
Klienci.aspx.vb:73:        If Not cm.CzyMozeWypozyczac(klientId) Then
Klienci.aspx.vb:74:            Dim k As Klient = cm.PobierzKlienta(klientId)
Klienci.aspx.vb:75:            lblKomunikat.Text = "Klient zablokowany: " & If(k Is Nothing, "", k.PowodBlokady)
Klienci.aspx.vb:76:            Return
Klienci.aspx.vb:77:        End If
Klienci.aspx.vb:78:        Session("WybranyKlientId") = klientId
Klienci.aspx.vb:79:        Log.Debug("Wybrano klienta " & klientId & " w sesji " & Session.SessionID)
Klienci.aspx.vb:80:        Response.Redirect("Rezerwacja.aspx")
Klienci.aspx.vb:81:    End Sub
Klienci.aspx.vb:82:
Klienci.aspx.vb:83:    Protected Sub gvKlienci_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles gvKlienci.RowCommand
Klienci.aspx.vb:84:        If e.CommandName <> "Edytuj" Then Return
Klienci.aspx.vb:85:        Dim indeks As Integer = Convert.ToInt32(e.CommandArgument)
Klienci.aspx.vb:86:        Dim klientId As Integer = gvKlienci.DataKeys(indeks).Value
Klienci.aspx.vb:87:        WczytajDoFormularza(klientId)
Klienci.aspx.vb:88:    End Sub
Klienci.aspx.vb:89:
Klienci.aspx.vb:90:    Protected Sub gvKlienci_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvKlienci.PageIndexChanging
Klienci.aspx.vb:91:        gvKlienci.PageIndex = e.NewPageIndex
Klienci.aspx.vb:92:        ZaladujListe(ViewState("OstatniaFraza"))
Klienci.aspx.vb:93:    End Sub
Klienci.aspx.vb:94:
Klienci.aspx.vb:95:    Protected Sub btnZapisz_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZapisz.Click
Klienci.aspx.vb:96:        lblKomunikat.Text = ""
Klienci.aspx.vb:97:        If txtEmail.Text.Trim() = "" OrElse Not txtEmail.Text.Contains("@") Then
Klienci.aspx.vb:98:            lblKomunikat.Text = "Niepoprawny adres e-mail."
Klienci.aspx.vb:99:            Return
Klienci.aspx.vb:100:        End If
Klienci.aspx.vb:101:        Dim blad As String = WalidujFormularz()
Klienci.aspx.vb:102:        If blad <> "" Then
Klienci.aspx.vb:103:            lblKomunikat.Text = blad
Klienci.aspx.vb:104:            Return
Klienci.aspx.vb:105:        End If
Klienci.aspx.vb:106:
Klienci.aspx.vb:107:        Dim k As New Klient()
Klienci.aspx.vb:108:        k.Id = CInt(hfKlientId.Value)
Klienci.aspx.vb:109:        k.Imie = txtImie.Text.Trim()
Klienci.aspx.vb:110:        k.Nazwisko = txtNazwisko.Text.Trim()
Klienci.aspx.vb:111:        k.NazwaFirmy = txtNazwaFirmy.Text.Trim()
Klienci.aspx.vb:112:        k.Email = txtEmail.Text.Trim()
Klienci.aspx.vb:113:        k.Telefon = txtTelefon.Text.Trim()
Klienci.aspx.vb:114:        k.NumerDokumentu = txtNumerDokumentu.Text.Trim().ToUpper()
Klienci.aspx.vb:115:        k.NIP = txtNIP.Text.Replace("-", "").Trim()
Klienci.aspx.vb:116:        k.Typ = ddlTyp.SelectedValue
Klienci.aspx.vb:117:        k.ProgRabatu = ddlProg.SelectedValue
Klienci.aspx.vb:118:        If k.Typ = "I" Then
Klienci.aspx.vb:119:            k.NIP = Nothing
Klienci.aspx.vb:120:            If k.NazwaFirmy = "" Then k.NazwaFirmy = Nothing
Klienci.aspx.vb:121:        End If
Klienci.aspx.vb:122:
Klienci.aspx.vb:123:        Try
Klienci.aspx.vb:124:            Dim cm As New CustomerManager()
Klienci.aspx.vb:125:            Dim noweId As Integer = cm.Zapisz(k)
Klienci.aspx.vb:126:            hfKlientId.Value = noweId
Klienci.aspx.vb:127:            lblTrybFormularza.Text = "Edycja klienta nr " & noweId
Klienci.aspx.vb:128:            lblKomunikat.Text = "Zapisano klienta nr " & noweId
Klienci.aspx.vb:129:            lblKomunikat.ForeColor = Drawing.Color.DarkGreen
Klienci.aspx.vb:130:            ZaladujListe(ViewState("OstatniaFraza"))
Klienci.aspx.vb:131:        Catch ex As BladBiznesowyException
Klienci.aspx.vb:132:            lblKomunikat.Text = ex.Message
Klienci.aspx.vb:133:        Catch ex As SqlException
Klienci.aspx.vb:134:            Log.Error("Blad zapisu klienta", ex)
Klienci.aspx.vb:135:            lblKomunikat.Text = "Nie udalo sie zapisac klienta: " & ex.Message
Klienci.aspx.vb:136:        End Try
Klienci.aspx.vb:137:    End Sub
Klienci.aspx.vb:138:
Klienci.aspx.vb:139:    Protected Sub btnZablokuj_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZablokuj.Click
Klienci.aspx.vb:140:        Dim klientId As Integer = CInt(hfKlientId.Value)
Klienci.aspx.vb:141:        If klientId = 0 Then
Klienci.aspx.vb:142:            lblKomunikat.Text = "Najpierw wybierz klienta do edycji."
Klienci.aspx.vb:143:            Return
Klienci.aspx.vb:144:        End If
Klienci.aspx.vb:145:        Dim powod As String = txtPowodBlokady.Text.Trim()
Klienci.aspx.vb:146:        If powod = "" Then powod = "blokada recznie z ekranu Klienci"
Klienci.aspx.vb:147:        Dim cm As New CustomerManager()
Klienci.aspx.vb:148:        cm.Zablokuj(klientId, powod)
Klienci.aspx.vb:149:        If Session("WybranyKlientId") = klientId Then
Klienci.aspx.vb:150:            Session("WybranyKlientId") = 0
Klienci.aspx.vb:151:        End If
Klienci.aspx.vb:152:        lblKomunikat.Text = "Klient nr " & klientId & " zostal zablokowany."
Klienci.aspx.vb:153:        ZaladujListe(ViewState("OstatniaFraza"))
Klienci.aspx.vb:154:    End Sub
Klienci.aspx.vb:155:
Klienci.aspx.vb:156:    Private Sub ZaladujListe(ByVal fraza As String)
Klienci.aspx.vb:157:        Try
Klienci.aspx.vb:158:            Dim cm As New CustomerManager()
Klienci.aspx.vb:159:            gvKlienci.DataSource = cm.Szukaj(fraza)
Klienci.aspx.vb:160:            gvKlienci.DataBind()
Klienci.aspx.vb:161:        Catch ex As SqlException
Klienci.aspx.vb:162:            Log.Error("Blad ladowania listy klientow", ex)
Klienci.aspx.vb:163:            lblKomunikat.Text = "Blad bazy danych."
Klienci.aspx.vb:164:        End Try
Klienci.aspx.vb:165:    End Sub
Klienci.aspx.vb:166:
Klienci.aspx.vb:167:    Private Sub WczytajDoFormularza(ByVal klientId As Integer)
Klienci.aspx.vb:168:        Dim cm As New CustomerManager()
Klienci.aspx.vb:169:        Dim k As Klient = cm.PobierzKlienta(klientId)
Klienci.aspx.vb:170:        If k Is Nothing Then
Klienci.aspx.vb:171:            lblKomunikat.Text = "Nie znaleziono klienta nr " & klientId
Klienci.aspx.vb:172:            Return
Klienci.aspx.vb:173:        End If
Klienci.aspx.vb:174:        hfKlientId.Value = k.Id
Klienci.aspx.vb:175:        txtImie.Text = k.Imie
Klienci.aspx.vb:176:        txtNazwisko.Text = k.Nazwisko
Klienci.aspx.vb:177:        txtNazwaFirmy.Text = k.NazwaFirmy
Klienci.aspx.vb:178:        txtEmail.Text = k.Email
Klienci.aspx.vb:179:        txtTelefon.Text = k.Telefon
Klienci.aspx.vb:180:        txtNumerDokumentu.Text = k.NumerDokumentu
Klienci.aspx.vb:181:        txtNIP.Text = k.NIP
Klienci.aspx.vb:182:        UstawWybor(ddlTyp, k.Typ)
Klienci.aspx.vb:183:        UstawWybor(ddlProg, k.ProgRabatu.ToString())
Klienci.aspx.vb:184:        txtPowodBlokady.Text = k.PowodBlokady
Klienci.aspx.vb:185:        lblTrybFormularza.Text = "Edycja klienta nr " & k.Id
Klienci.aspx.vb:186:        If k.Zablokowany Then
Klienci.aspx.vb:187:            lblTrybFormularza.Text = lblTrybFormularza.Text & " (ZABLOKOWANY)"
Klienci.aspx.vb:188:            btnZablokuj.Enabled = False
Klienci.aspx.vb:189:        Else
Klienci.aspx.vb:190:            btnZablokuj.Enabled = True
Klienci.aspx.vb:191:        End If
Klienci.aspx.vb:192:    End Sub
Klienci.aspx.vb:193:
Klienci.aspx.vb:194:    Private Function WalidujFormularz() As String
Klienci.aspx.vb:195:        If txtTelefon.Text.Trim() = "" Then
Klienci.aspx.vb:196:            Return "Telefon jest wymagany."
Klienci.aspx.vb:197:        End If
Klienci.aspx.vb:198:        If txtTelefon.Text.Trim().Length > 20 Then
Klienci.aspx.vb:199:            Return "Telefon moze miec maksymalnie 20 znakow."
Klienci.aspx.vb:200:        End If
Klienci.aspx.vb:201:        If ddlTyp.SelectedValue = "F" Then
Klienci.aspx.vb:202:            If txtNazwaFirmy.Text.Trim() = "" Then
Klienci.aspx.vb:203:                Return "Dla firmy podaj nazwe firmy."
Klienci.aspx.vb:204:            End If
Klienci.aspx.vb:205:            Dim nip As String = txtNIP.Text.Replace("-", "").Replace(" ", "").Trim()
Klienci.aspx.vb:206:            If nip.Length <> 10 OrElse Not IsNumeric(nip) Then
Klienci.aspx.vb:207:                Return "NIP musi miec 10 cyfr."
Klienci.aspx.vb:208:            End If
Klienci.aspx.vb:209:        End If
Klienci.aspx.vb:210:        If ddlTyp.SelectedValue = "I" AndAlso txtNumerDokumentu.Text.Trim() = "" Then
Klienci.aspx.vb:211:            Return "Dla osoby prywatnej podaj numer dokumentu."
Klienci.aspx.vb:212:        End If
Klienci.aspx.vb:213:        Return ""
Klienci.aspx.vb:214:    End Function
Klienci.aspx.vb:215:
Klienci.aspx.vb:216:    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PreRender
Klienci.aspx.vb:217:        Dim wybrany As Integer = Session("WybranyKlientId")
Klienci.aspx.vb:218:        If wybrany > 0 AndAlso gvKlienci.Rows.Count > 0 Then
Klienci.aspx.vb:219:            For i As Integer = 0 To gvKlienci.Rows.Count - 1
Klienci.aspx.vb:220:                If gvKlienci.DataKeys(i).Value = wybrany Then
Klienci.aspx.vb:221:                    gvKlienci.Rows(i).Font.Bold = True
Klienci.aspx.vb:222:                End If
Klienci.aspx.vb:223:            Next
Klienci.aspx.vb:224:        End If
Klienci.aspx.vb:225:    End Sub
Klienci.aspx.vb:226:
Klienci.aspx.vb:227:    Private Sub UstawWybor(ByVal lista As DropDownList, ByVal wartosc As String)
Klienci.aspx.vb:228:        Dim pozycja As ListItem = lista.Items.FindByValue(wartosc)
Klienci.aspx.vb:229:        If pozycja IsNot Nothing Then
Klienci.aspx.vb:230:            lista.ClearSelection()
Klienci.aspx.vb:231:            pozycja.Selected = True
Klienci.aspx.vb:232:        End If
Klienci.aspx.vb:233:    End Sub
Klienci.aspx.vb:234:
Klienci.aspx.vb:235:    Private Sub WyczyscFormularz()
Klienci.aspx.vb:236:        hfKlientId.Value = "0"
Klienci.aspx.vb:237:        txtImie.Text = ""
Klienci.aspx.vb:238:        txtNazwisko.Text = ""
Klienci.aspx.vb:239:        txtNazwaFirmy.Text = ""
Klienci.aspx.vb:240:        txtEmail.Text = ""
Klienci.aspx.vb:241:        txtTelefon.Text = ""
Klienci.aspx.vb:242:        txtNumerDokumentu.Text = ""
Klienci.aspx.vb:243:        txtNIP.Text = ""
Klienci.aspx.vb:244:        txtPowodBlokady.Text = ""
Klienci.aspx.vb:245:        ddlTyp.SelectedIndex = 0
Klienci.aspx.vb:246:        ddlProg.SelectedIndex = 0
Klienci.aspx.vb:247:        btnZablokuj.Enabled = False
Klienci.aspx.vb:248:        lblTrybFormularza.Text = "Nowy klient"
Klienci.aspx.vb:249:    End Sub
Klienci.aspx.vb:250:
Klienci.aspx.vb:251:End Class
Rezerwacja.aspx.vb:1:Option Strict Off
Rezerwacja.aspx.vb:2:' Modul: Rezerwacja.aspx - formularz rezerwacji sprzetu | Autor: zespol IT | 2014-03-11
Rezerwacja.aspx.vb:3:' Historia:
Rezerwacja.aspx.vb:4:'   2014-09-22 JK  szacunek ceny przed zapisem (przycisk Sprawdz)
Rezerwacja.aspx.vb:5:'   2016-05-30 MW  kalendarze AjaxControlToolkit zamiast pol tekstowych
Rezerwacja.aspx.vb:6:'   2019-02-20 MW  anulowanie z listy rezerwacji klienta
Rezerwacja.aspx.vb:7:
Rezerwacja.aspx.vb:8:Imports System.Data
Rezerwacja.aspx.vb:9:Imports System.Globalization
Rezerwacja.aspx.vb:10:Imports Wypozyczalnia.Core
Rezerwacja.aspx.vb:11:Imports Wypozyczalnia.Core.Model
Rezerwacja.aspx.vb:12:
Rezerwacja.aspx.vb:13:Partial Class Rezerwacja
Rezerwacja.aspx.vb:14:    Inherits System.Web.UI.Page
Rezerwacja.aspx.vb:15:
Rezerwacja.aspx.vb:16:    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Rezerwacja))
Rezerwacja.aspx.vb:17:
Rezerwacja.aspx.vb:18:    Private Shared ReadOnly FormatyTerminu() As String = {"yyyy-MM-dd HH:mm", "yyyy-MM-dd H:mm", "dd.MM.yyyy HH:mm", "dd.MM.yyyy H:mm"}
Rezerwacja.aspx.vb:19:
Rezerwacja.aspx.vb:20:    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
Rezerwacja.aspx.vb:21:        If Not IsPostBack Then
Rezerwacja.aspx.vb:22:            PokazKlienta()
Rezerwacja.aspx.vb:23:            WypelnijSprzet()
Rezerwacja.aspx.vb:24:            WybierzSprzetZAdresu()
Rezerwacja.aspx.vb:25:            txtDataOd.Text = Date.Today.AddDays(1).ToString("yyyy-MM-dd")
Rezerwacja.aspx.vb:26:            txtDataDo.Text = Date.Today.AddDays(2).ToString("yyyy-MM-dd")
Rezerwacja.aspx.vb:27:            If Session("SzacunekRezerwacji") IsNot Nothing Then
Rezerwacja.aspx.vb:28:                lblSzacunek.Text = Session("SzacunekRezerwacji")
Rezerwacja.aspx.vb:29:            End If
Rezerwacja.aspx.vb:30:            PokazRezerwacje()
Rezerwacja.aspx.vb:31:        End If
Rezerwacja.aspx.vb:32:    End Sub
Rezerwacja.aspx.vb:33:
Rezerwacja.aspx.vb:34:    Protected Sub ddlKategoria_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlKategoria.SelectedIndexChanged
Rezerwacja.aspx.vb:35:        WypelnijSprzet()
Rezerwacja.aspx.vb:36:        lblSzacunek.Text = "-"
Rezerwacja.aspx.vb:37:        Session("SzacunekRezerwacji") = Nothing
Rezerwacja.aspx.vb:38:    End Sub
Rezerwacja.aspx.vb:39:
Rezerwacja.aspx.vb:40:    Protected Sub btnSprawdz_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSprawdz.Click
Rezerwacja.aspx.vb:41:        Dim dataOd As Date
Rezerwacja.aspx.vb:42:        Dim dataDo As Date
Rezerwacja.aspx.vb:43:        If ddlSprzet.SelectedValue = "" Then
Rezerwacja.aspx.vb:44:            PokazBlad("Wybierz sprzet.")
Rezerwacja.aspx.vb:45:            Return
Rezerwacja.aspx.vb:46:        End If
Rezerwacja.aspx.vb:47:        If Not ParsujTermin(txtDataOd.Text, txtGodzinaOd.Text, dataOd) OrElse Not ParsujTermin(txtDataDo.Text, txtGodzinaDo.Text, dataDo) Then
Rezerwacja.aspx.vb:48:            PokazBlad("Niepoprawny termin. Format: rrrr-mm-dd gg:mm")
Rezerwacja.aspx.vb:49:            Return
Rezerwacja.aspx.vb:50:        End If
Rezerwacja.aspx.vb:51:        If dataDo <= dataOd Then
Rezerwacja.aspx.vb:52:            PokazBlad("Data zakonczenia musi byc pozniejsza niz data rozpoczecia.")
Rezerwacja.aspx.vb:53:            Return
Rezerwacja.aspx.vb:54:        End If
Rezerwacja.aspx.vb:55:
Rezerwacja.aspx.vb:56:        Dim sprzetId As Integer = ddlSprzet.SelectedValue
Rezerwacja.aspx.vb:57:        Dim mgr As New EquipmentManager()
Rezerwacja.aspx.vb:58:        If Not mgr.SprawdzDostepnosc(sprzetId, dataOd, dataDo) Then
Rezerwacja.aspx.vb:59:            PokazBlad("Sprzet niedostepny w wybranym terminie: " & OpisKolizji(mgr.KolizjeWTerminie(sprzetId, dataOd, dataDo)))
Rezerwacja.aspx.vb:60:            lblSzacunek.Text = "-"
Rezerwacja.aspx.vb:61:            Session("SzacunekRezerwacji") = Nothing
Rezerwacja.aspx.vb:62:            Return
Rezerwacja.aspx.vb:63:        End If
Rezerwacja.aspx.vb:64:
Rezerwacja.aspx.vb:65:        Dim sprzet As Sprzet = mgr.PobierzSprzet(sprzetId)
Rezerwacja.aspx.vb:66:        Dim klientId As Integer = Session("WybranyKlientId")
Rezerwacja.aspx.vb:67:        Dim prog As Integer = 0
Rezerwacja.aspx.vb:68:        If klientId > 0 Then
Rezerwacja.aspx.vb:69:            prog = New CustomerManager().ProgRabatu(klientId)
Rezerwacja.aspx.vb:70:        End If
Rezerwacja.aspx.vb:71:        Dim szacunek As Decimal = ObliczSzacunek(sprzet, dataOd, dataDo, prog)
Rezerwacja.aspx.vb:72:        lblSzacunek.Text = szacunek.ToString("N2")
Rezerwacja.aspx.vb:73:        Session("SzacunekRezerwacji") = szacunek
Rezerwacja.aspx.vb:74:        PokazInfo("Sprzet dostepny w wybranym terminie.")
Rezerwacja.aspx.vb:75:    End Sub
Rezerwacja.aspx.vb:76:
Rezerwacja.aspx.vb:77:    Protected Sub btnZarezerwuj_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZarezerwuj.Click
Rezerwacja.aspx.vb:78:        Dim klientId As Integer = Session("WybranyKlientId")
Rezerwacja.aspx.vb:79:        If klientId = 0 Then
Rezerwacja.aspx.vb:80:            PokazBlad("Najpierw wybierz klienta na stronie Klienci.")
Rezerwacja.aspx.vb:81:            Return
Rezerwacja.aspx.vb:82:        End If
Rezerwacja.aspx.vb:83:        If ddlSprzet.SelectedValue = "" Then
Rezerwacja.aspx.vb:84:            PokazBlad("Wybierz sprzet.")
Rezerwacja.aspx.vb:85:            Return
Rezerwacja.aspx.vb:86:        End If
Rezerwacja.aspx.vb:87:
Rezerwacja.aspx.vb:88:        Dim dataOd As Date
Rezerwacja.aspx.vb:89:        Dim dataDo As Date
Rezerwacja.aspx.vb:90:        If Not ParsujTermin(txtDataOd.Text, txtGodzinaOd.Text, dataOd) Then
Rezerwacja.aspx.vb:91:            PokazBlad("Niepoprawna data rozpoczecia.")
Rezerwacja.aspx.vb:92:            Return
Rezerwacja.aspx.vb:93:        End If
Rezerwacja.aspx.vb:94:        If Not ParsujTermin(txtDataDo.Text, txtGodzinaDo.Text, dataDo) Then
Rezerwacja.aspx.vb:95:            PokazBlad("Niepoprawna data zakonczenia.")
Rezerwacja.aspx.vb:96:            Return
Rezerwacja.aspx.vb:97:        End If
Rezerwacja.aspx.vb:98:        If dataOd < Date.Now.AddMinutes(-30) Then
Rezerwacja.aspx.vb:99:            PokazBlad("Nie mozna rezerwowac wstecz.")
Rezerwacja.aspx.vb:100:            Return
Rezerwacja.aspx.vb:101:        End If
Rezerwacja.aspx.vb:102:        If dataDo <= dataOd Then
Rezerwacja.aspx.vb:103:            PokazBlad("Data zakonczenia musi byc pozniejsza niz data rozpoczecia.")
Rezerwacja.aspx.vb:104:            Return
Rezerwacja.aspx.vb:105:        End If
Rezerwacja.aspx.vb:106:        If dataOd.Hour < 7 OrElse dataOd.Hour > 18 Then
Rezerwacja.aspx.vb:107:            PokazBlad("Wydania sprzetu tylko w godzinach 7:00 - 18:00.")
Rezerwacja.aspx.vb:108:            Return
Rezerwacja.aspx.vb:109:        End If
Rezerwacja.aspx.vb:110:        If txtUwagi.Text.Length > 500 Then
Rezerwacja.aspx.vb:111:            txtUwagi.Text = txtUwagi.Text.Substring(0, 500)
Rezerwacja.aspx.vb:112:        End If
Rezerwacja.aspx.vb:113:
Rezerwacja.aspx.vb:114:        Dim rezerwacje As New ReservationManager()
Rezerwacja.aspx.vb:115:        Try
Rezerwacja.aspx.vb:116:            Dim noweId As Integer = rezerwacje.UtworzRezerwacje(klientId, ddlSprzet.SelectedValue, dataOd, dataDo, txtUwagi.Text)
Rezerwacja.aspx.vb:117:            Dim nowa = rezerwacje.PobierzRezerwacje(noweId)
Rezerwacja.aspx.vb:118:            PokazInfo("Zapisano rezerwacje nr " & noweId & ". Cena: " & nowa.CenaSzacunkowa.ToString("N2") & " zl.")
Rezerwacja.aspx.vb:119:            txtUwagi.Text = ""
Rezerwacja.aspx.vb:120:            PokazRezerwacje()
Rezerwacja.aspx.vb:121:        Catch ex As BladBiznesowyException
Rezerwacja.aspx.vb:122:            PokazBlad(ex.Message)
Rezerwacja.aspx.vb:123:        Catch ex As ArgumentException
Rezerwacja.aspx.vb:124:            PokazBlad(ex.Message)
Rezerwacja.aspx.vb:125:        Catch ex As Exception
Rezerwacja.aspx.vb:126:            Log.Error("Blad zapisu rezerwacji, klient " & klientId, ex)
Rezerwacja.aspx.vb:127:            PokazBlad("Nie udalo sie zapisac rezerwacji. Sprobuj ponownie.")
Rezerwacja.aspx.vb:128:        End Try
Rezerwacja.aspx.vb:129:    End Sub
Rezerwacja.aspx.vb:130:
Rezerwacja.aspx.vb:131:    Protected Sub gvRezerwacje_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gvRezerwacje.SelectedIndexChanged
Rezerwacja.aspx.vb:132:        If gvRezerwacje.SelectedIndex < 0 Then
Rezerwacja.aspx.vb:133:            Return
Rezerwacja.aspx.vb:134:        End If
Rezerwacja.aspx.vb:135:        ViewState("WybranaRezerwacja") = gvRezerwacje.SelectedDataKey.Value
Rezerwacja.aspx.vb:136:        Dim rezerwacje As New ReservationManager()
Rezerwacja.aspx.vb:137:        Dim wybrana = rezerwacje.PobierzRezerwacje(gvRezerwacje.SelectedDataKey.Value)
Rezerwacja.aspx.vb:138:        If wybrana Is Nothing Then
Rezerwacja.aspx.vb:139:            PokazBlad("Rezerwacja nie istnieje (odswiez liste).")
Rezerwacja.aspx.vb:140:            btnAnuluj.Enabled = False
Rezerwacja.aspx.vb:141:            Return
Rezerwacja.aspx.vb:142:        End If
Rezerwacja.aspx.vb:143:        btnAnuluj.Enabled = wybrana.CzyAktywna
Rezerwacja.aspx.vb:144:        Dim opis As String = rezerwacje.OpisRezerwacji(wybrana.Id)
Rezerwacja.aspx.vb:145:        If Not String.IsNullOrEmpty(wybrana.Uwagi) Then
Rezerwacja.aspx.vb:146:            opis = opis & ". Uwagi: " & wybrana.Uwagi
Rezerwacja.aspx.vb:147:        End If
Rezerwacja.aspx.vb:148:        PokazInfo(opis)
Rezerwacja.aspx.vb:149:    End Sub
Rezerwacja.aspx.vb:150:
Rezerwacja.aspx.vb:151:    Protected Sub gvRezerwacje_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvRezerwacje.RowDataBound
Rezerwacja.aspx.vb:152:        If e.Row.RowType <> DataControlRowType.DataRow Then
Rezerwacja.aspx.vb:153:            Return
Rezerwacja.aspx.vb:154:        End If
Rezerwacja.aspx.vb:155:        Dim wiersz As DataRowView = CType(e.Row.DataItem, DataRowView)
Rezerwacja.aspx.vb:156:        Dim komorkaStatusu As TableCell = e.Row.Cells(5)
Rezerwacja.aspx.vb:157:        Select Case CStr(wiersz("Status"))
Rezerwacja.aspx.vb:158:            Case "N"
Rezerwacja.aspx.vb:159:                komorkaStatusu.Text = "nowa"
Rezerwacja.aspx.vb:160:            Case "P"
Rezerwacja.aspx.vb:161:                komorkaStatusu.Text = "potwierdzona"
Rezerwacja.aspx.vb:162:                komorkaStatusu.Font.Bold = True
Rezerwacja.aspx.vb:163:            Case "A"
Rezerwacja.aspx.vb:164:                komorkaStatusu.Text = "anulowana"
Rezerwacja.aspx.vb:165:                e.Row.CssClass = "wyszarzony"
Rezerwacja.aspx.vb:166:            Case "Z"
Rezerwacja.aspx.vb:167:                komorkaStatusu.Text = "zrealizowana"
Rezerwacja.aspx.vb:168:                e.Row.CssClass = "wyszarzony"
Rezerwacja.aspx.vb:169:        End Select
Rezerwacja.aspx.vb:170:        If CDate(wiersz("DataDo")) < Date.Now AndAlso CStr(wiersz("Status")) = "N" Then
Rezerwacja.aspx.vb:171:            komorkaStatusu.Text = komorkaStatusu.Text & " (termin minal)"
Rezerwacja.aspx.vb:172:        End If
Rezerwacja.aspx.vb:173:    End Sub
Rezerwacja.aspx.vb:174:
Rezerwacja.aspx.vb:175:    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PreRender
Rezerwacja.aspx.vb:176:        ceDataOd.StartDate = Date.Today
Rezerwacja.aspx.vb:177:        ceDataDo.StartDate = Date.Today
Rezerwacja.aspx.vb:178:        btnSprawdz.Enabled = ddlSprzet.Items.Count > 1
Rezerwacja.aspx.vb:179:    End Sub
Rezerwacja.aspx.vb:180:
Rezerwacja.aspx.vb:181:    ' Rezerwacja.aspx?sprzet=<Id> - link "rezerwuj" z katalogu sprzetu
Rezerwacja.aspx.vb:182:    Private Sub WybierzSprzetZAdresu()
Rezerwacja.aspx.vb:183:        Dim parametr As String = Request.QueryString("sprzet")
Rezerwacja.aspx.vb:184:        Dim sprzetId As Integer
Rezerwacja.aspx.vb:185:        If String.IsNullOrEmpty(parametr) OrElse Not Integer.TryParse(parametr, sprzetId) Then
Rezerwacja.aspx.vb:186:            Return
Rezerwacja.aspx.vb:187:        End If
Rezerwacja.aspx.vb:188:        Dim sprzet As Sprzet = New EquipmentManager().PobierzSprzet(sprzetId)
Rezerwacja.aspx.vb:189:        If sprzet Is Nothing Then
Rezerwacja.aspx.vb:190:            Return
Rezerwacja.aspx.vb:191:        End If
Rezerwacja.aspx.vb:192:        Dim pozycjaKategorii As ListItem = ddlKategoria.Items.FindByValue(sprzet.Kategoria)
Rezerwacja.aspx.vb:193:        If pozycjaKategorii IsNot Nothing Then
Rezerwacja.aspx.vb:194:            ddlKategoria.ClearSelection()
Rezerwacja.aspx.vb:195:            pozycjaKategorii.Selected = True
Rezerwacja.aspx.vb:196:            WypelnijSprzet()
Rezerwacja.aspx.vb:197:        End If
Rezerwacja.aspx.vb:198:        Dim pozycja As ListItem = ddlSprzet.Items.FindByValue(sprzetId.ToString())
Rezerwacja.aspx.vb:199:        If pozycja IsNot Nothing Then
Rezerwacja.aspx.vb:200:            ddlSprzet.ClearSelection()
Rezerwacja.aspx.vb:201:            pozycja.Selected = True
Rezerwacja.aspx.vb:202:        End If
Rezerwacja.aspx.vb:203:    End Sub
Rezerwacja.aspx.vb:204:
Rezerwacja.aspx.vb:205:    Protected Sub btnAnuluj_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAnuluj.Click
Rezerwacja.aspx.vb:206:        If ViewState("WybranaRezerwacja") Is Nothing Then
Rezerwacja.aspx.vb:207:            PokazBlad("Wybierz rezerwacje z listy.")
Rezerwacja.aspx.vb:208:            Return
Rezerwacja.aspx.vb:209:        End If
Rezerwacja.aspx.vb:210:        Dim rezerwacjaId As Integer = ViewState("WybranaRezerwacja")
Rezerwacja.aspx.vb:211:        Try
Rezerwacja.aspx.vb:212:            Dim rezerwacje As New ReservationManager()
Rezerwacja.aspx.vb:213:            rezerwacje.Anuluj(rezerwacjaId)
Rezerwacja.aspx.vb:214:            PokazInfo("Rezerwacja nr " & rezerwacjaId & " anulowana.")
Rezerwacja.aspx.vb:215:            ViewState("WybranaRezerwacja") = Nothing
Rezerwacja.aspx.vb:216:            gvRezerwacje.SelectedIndex = -1
Rezerwacja.aspx.vb:217:            PokazRezerwacje()
Rezerwacja.aspx.vb:218:        Catch ex As BladBiznesowyException
Rezerwacja.aspx.vb:219:            PokazBlad(ex.Message)
Rezerwacja.aspx.vb:220:        End Try
Rezerwacja.aspx.vb:221:    End Sub
Rezerwacja.aspx.vb:222:
Rezerwacja.aspx.vb:223:    Private Sub PokazKlienta()
Rezerwacja.aspx.vb:224:        Dim klientId As Integer = Session("WybranyKlientId")
Rezerwacja.aspx.vb:225:        If klientId = 0 Then
Rezerwacja.aspx.vb:226:            lblKlient.Text = "(nie wybrano - wybierz klienta na stronie Klienci)"
Rezerwacja.aspx.vb:227:            btnZarezerwuj.Enabled = False
Rezerwacja.aspx.vb:228:            Return
Rezerwacja.aspx.vb:229:        End If
Rezerwacja.aspx.vb:230:        Dim klient As Klient = New CustomerManager().PobierzKlienta(klientId)
Rezerwacja.aspx.vb:231:        If klient Is Nothing Then
Rezerwacja.aspx.vb:232:            lblKlient.Text = "(nieznany klient " & klientId & ")"
Rezerwacja.aspx.vb:233:            btnZarezerwuj.Enabled = False
Rezerwacja.aspx.vb:234:            Return
Rezerwacja.aspx.vb:235:        End If
Rezerwacja.aspx.vb:236:        If klient.Typ = "F" AndAlso Not String.IsNullOrEmpty(klient.NazwaFirmy) Then
Rezerwacja.aspx.vb:237:            lblKlient.Text = Server.HtmlEncode(klient.NazwaFirmy & " (" & klient.Imie & " " & klient.Nazwisko & ")")
Rezerwacja.aspx.vb:238:        Else
Rezerwacja.aspx.vb:239:            lblKlient.Text = Server.HtmlEncode(klient.Imie & " " & klient.Nazwisko)
Rezerwacja.aspx.vb:240:        End If
Rezerwacja.aspx.vb:241:        If klient.Zablokowany Then
Rezerwacja.aspx.vb:242:            lblKlient.Text = lblKlient.Text & " - ZABLOKOWANY"
Rezerwacja.aspx.vb:243:            lblKlient.CssClass = "blad"
Rezerwacja.aspx.vb:244:        End If
Rezerwacja.aspx.vb:245:        btnZarezerwuj.Enabled = True
Rezerwacja.aspx.vb:246:    End Sub
Rezerwacja.aspx.vb:247:
Rezerwacja.aspx.vb:248:    Private Sub WypelnijSprzet()
Rezerwacja.aspx.vb:249:        ddlSprzet.Items.Clear()
Rezerwacja.aspx.vb:250:        ddlSprzet.Items.Add(New ListItem("-- wybierz --", ""))
Rezerwacja.aspx.vb:251:        Dim mgr As New EquipmentManager()
Rezerwacja.aspx.vb:252:        For Each s In mgr.ListaWKategorii(ddlKategoria.SelectedValue)
Rezerwacja.aspx.vb:253:            Dim opis As String = s.NumerInwentarzowy & " - " & s.Nazwa & " (" & s.StawkaDobowa.ToString("N2") & " zl/doba)"
Rezerwacja.aspx.vb:254:            If s.Status = "S" Then
Rezerwacja.aspx.vb:255:                opis = opis & " [serwis]"
Rezerwacja.aspx.vb:256:            End If
Rezerwacja.aspx.vb:257:            ddlSprzet.Items.Add(New ListItem(opis, s.Id.ToString()))
Rezerwacja.aspx.vb:258:        Next
Rezerwacja.aspx.vb:259:    End Sub
Rezerwacja.aspx.vb:260:
Rezerwacja.aspx.vb:261:    Private Sub PokazRezerwacje()
Rezerwacja.aspx.vb:262:        Dim klientId As Integer = Session("WybranyKlientId")
Rezerwacja.aspx.vb:263:        If klientId = 0 Then
Rezerwacja.aspx.vb:264:            gvRezerwacje.DataSource = Nothing
Rezerwacja.aspx.vb:265:            gvRezerwacje.DataBind()
Rezerwacja.aspx.vb:266:            btnAnuluj.Enabled = False
Rezerwacja.aspx.vb:267:            Return
Rezerwacja.aspx.vb:268:        End If
Rezerwacja.aspx.vb:269:        Dim tabela As DataTable = New ReservationManager().RezerwacjeKlienta(klientId)
Rezerwacja.aspx.vb:270:        gvRezerwacje.DataSource = tabela
Rezerwacja.aspx.vb:271:        gvRezerwacje.DataBind()
Rezerwacja.aspx.vb:272:        btnAnuluj.Enabled = tabela.Rows.Count > 0
Rezerwacja.aspx.vb:273:    End Sub
Rezerwacja.aspx.vb:274:
Rezerwacja.aspx.vb:275:    Private Function OpisKolizji(ByVal kolizje As DataTable) As String
Rezerwacja.aspx.vb:276:        Dim opisy As New List(Of String)()
Rezerwacja.aspx.vb:277:        For Each wiersz As DataRow In kolizje.Rows
Rezerwacja.aspx.vb:278:            Select Case CStr(wiersz("Zrodlo"))
Rezerwacja.aspx.vb:279:                Case "R"
Rezerwacja.aspx.vb:280:                    opisy.Add("rezerwacja nr " & wiersz("Id") & " (" & CDate(wiersz("DataOd")).ToString("yyyy-MM-dd HH:mm") & _
Rezerwacja.aspx.vb:281:                              " - " & CDate(wiersz("DataDo")).ToString("yyyy-MM-dd HH:mm") & ")")
Rezerwacja.aspx.vb:282:                Case "W"
Rezerwacja.aspx.vb:283:                    opisy.Add("wypozyczenie nr " & wiersz("Id") & " do " & CDate(wiersz("DataDo")).ToString("yyyy-MM-dd HH:mm"))
Rezerwacja.aspx.vb:284:                Case "S"
Rezerwacja.aspx.vb:285:                    opisy.Add("sprzet w serwisie albo wycofany")
Rezerwacja.aspx.vb:286:                Case Else
Rezerwacja.aspx.vb:287:                    opisy.Add("kolizja " & wiersz("Zrodlo"))
Rezerwacja.aspx.vb:288:            End Select
Rezerwacja.aspx.vb:289:        Next
Rezerwacja.aspx.vb:290:        Return String.Join("; ", opisy.ToArray())
Rezerwacja.aspx.vb:291:    End Function
Rezerwacja.aspx.vb:292:
Rezerwacja.aspx.vb:293:    Private Function ParsujTermin(ByVal data As String, ByVal godzina As String, ByRef wynik As Date) As Boolean
Rezerwacja.aspx.vb:294:        If String.IsNullOrEmpty(data) Then
Rezerwacja.aspx.vb:295:            Return False
Rezerwacja.aspx.vb:296:        End If
Rezerwacja.aspx.vb:297:        If String.IsNullOrEmpty(godzina) Then
Rezerwacja.aspx.vb:298:            godzina = "08:00"
Rezerwacja.aspx.vb:299:        End If
Rezerwacja.aspx.vb:300:        Return Date.TryParseExact(data.Trim() & " " & godzina.Trim(), FormatyTerminu, CultureInfo.InvariantCulture, DateTimeStyles.None, wynik)
Rezerwacja.aspx.vb:301:    End Function
Rezerwacja.aspx.vb:302:
Rezerwacja.aspx.vb:303:    Private Sub PokazBlad(ByVal tekst As String)
Rezerwacja.aspx.vb:304:        lblKomunikat.CssClass = "blad"
Rezerwacja.aspx.vb:305:        lblKomunikat.Text = Server.HtmlEncode(tekst)
Rezerwacja.aspx.vb:306:    End Sub
Rezerwacja.aspx.vb:307:
Rezerwacja.aspx.vb:308:    Private Sub PokazInfo(ByVal tekst As String)
Rezerwacja.aspx.vb:309:        lblKomunikat.CssClass = "info"
Rezerwacja.aspx.vb:310:        lblKomunikat.Text = Server.HtmlEncode(tekst)
Rezerwacja.aspx.vb:311:    End Sub
Rezerwacja.aspx.vb:312:
Rezerwacja.aspx.vb:313:    Private Function ObliczSzacunek(ByVal sprzet As Sprzet, ByVal dataOd As Date, ByVal dataDo As Date, ByVal prog As Integer) As Decimal
Rezerwacja.aspx.vb:314:        Dim dni As Long = DateDiff(DateInterval.Day, dataOd.Date, dataDo.Date)
Rezerwacja.aspx.vb:315:        If dni < 1 Then dni = 1
Rezerwacja.aspx.vb:316:        Dim kwota As Decimal = dni * sprzet.StawkaDobowa
Rezerwacja.aspx.vb:317:        kwota = kwota * (100D - prog * 2.5D) / 100D
Rezerwacja.aspx.vb:318:        Return Math.Round(kwota, 2, MidpointRounding.AwayFromZero)
Rezerwacja.aspx.vb:319:    End Function
Rezerwacja.aspx.vb:320:
Rezerwacja.aspx.vb:321:End Class
Sprzet.aspx.vb:1:' Modul: Sprzet.aspx - katalog sprzetu, edycja, serwis | Autor: zespol IT | 2014-03-11
Sprzet.aspx.vb:2:' Historia:
Sprzet.aspx.vb:3:'   2015-06-02 MW  pole kaucji w formularzu
Sprzet.aspx.vb:4:'   2018-01-15 PT  historia wypozyczen pod formularzem
Sprzet.aspx.vb:5:
Sprzet.aspx.vb:6:Imports System.Data
Sprzet.aspx.vb:7:Imports System.Globalization
Sprzet.aspx.vb:8:Imports Wypozyczalnia.Core
Sprzet.aspx.vb:9:Imports SprzetModel = Wypozyczalnia.Core.Model.Sprzet
Sprzet.aspx.vb:10:
Sprzet.aspx.vb:11:Partial Class Sprzet
Sprzet.aspx.vb:12:    Inherits System.Web.UI.Page
Sprzet.aspx.vb:13:
Sprzet.aspx.vb:14:    Protected WithEvents form1 As Global.System.Web.UI.HtmlControls.HtmlForm
Sprzet.aspx.vb:15:    Protected WithEvents ddlKategoria As Global.System.Web.UI.WebControls.DropDownList
Sprzet.aspx.vb:16:    Protected WithEvents gvSprzet As Global.System.Web.UI.WebControls.GridView
Sprzet.aspx.vb:17:    Protected WithEvents hfSprzetId As Global.System.Web.UI.WebControls.HiddenField
Sprzet.aspx.vb:18:    Protected WithEvents txtNumer As Global.System.Web.UI.WebControls.TextBox
Sprzet.aspx.vb:19:    Protected WithEvents txtNazwa As Global.System.Web.UI.WebControls.TextBox
Sprzet.aspx.vb:20:    Protected WithEvents ddlKategoriaEdycja As Global.System.Web.UI.WebControls.DropDownList
Sprzet.aspx.vb:21:    Protected WithEvents txtStawka As Global.System.Web.UI.WebControls.TextBox
Sprzet.aspx.vb:22:    Protected WithEvents txtKaucja As Global.System.Web.UI.WebControls.TextBox
Sprzet.aspx.vb:23:    Protected WithEvents lblStatus As Global.System.Web.UI.WebControls.Label
Sprzet.aspx.vb:24:    Protected WithEvents btnNowy As Global.System.Web.UI.WebControls.Button
Sprzet.aspx.vb:25:    Protected WithEvents btnZapisz As Global.System.Web.UI.WebControls.Button
Sprzet.aspx.vb:26:    Protected WithEvents btnSerwis As Global.System.Web.UI.WebControls.Button
Sprzet.aspx.vb:27:    Protected WithEvents lblKomunikat As Global.System.Web.UI.WebControls.Label
Sprzet.aspx.vb:28:    Protected WithEvents gvHistoria As Global.System.Web.UI.WebControls.GridView
Sprzet.aspx.vb:29:
Sprzet.aspx.vb:30:    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Sprzet))
Sprzet.aspx.vb:31:
Sprzet.aspx.vb:32:    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
Sprzet.aspx.vb:33:        If Not IsPostBack Then
Sprzet.aspx.vb:34:            PokazListe()
Sprzet.aspx.vb:35:            WyczyscFormularz()
Sprzet.aspx.vb:36:        End If
Sprzet.aspx.vb:37:    End Sub
Sprzet.aspx.vb:38:
Sprzet.aspx.vb:39:    Protected Sub ddlKategoria_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlKategoria.SelectedIndexChanged
Sprzet.aspx.vb:40:        gvSprzet.SelectedIndex = -1
Sprzet.aspx.vb:41:        PokazListe()
Sprzet.aspx.vb:42:        WyczyscFormularz()
Sprzet.aspx.vb:43:    End Sub
Sprzet.aspx.vb:44:
Sprzet.aspx.vb:45:    Protected Sub gvSprzet_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gvSprzet.SelectedIndexChanged
Sprzet.aspx.vb:46:        Dim sprzetId As Integer = CInt(gvSprzet.SelectedDataKey.Value)
Sprzet.aspx.vb:47:        Dim s As SprzetModel = New EquipmentManager().PobierzSprzet(sprzetId)
Sprzet.aspx.vb:48:        If s Is Nothing Then
Sprzet.aspx.vb:49:            PokazBlad("Sprzet nie istnieje (odswiez liste).")
Sprzet.aspx.vb:50:            WyczyscFormularz()
Sprzet.aspx.vb:51:            Return
Sprzet.aspx.vb:52:        End If
Sprzet.aspx.vb:53:        WypelnijFormularz(s)
Sprzet.aspx.vb:54:        PokazHistorie(s.Id)
Sprzet.aspx.vb:55:    End Sub
Sprzet.aspx.vb:56:
Sprzet.aspx.vb:57:    Protected Sub gvSprzet_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvSprzet.RowDataBound
Sprzet.aspx.vb:58:        If e.Row.RowType <> DataControlRowType.DataRow Then
Sprzet.aspx.vb:59:            Return
Sprzet.aspx.vb:60:        End If
Sprzet.aspx.vb:61:        Dim s As SprzetModel = CType(e.Row.DataItem, SprzetModel)
Sprzet.aspx.vb:62:        e.Row.Cells(6).Text = s.OpisStatusu
Sprzet.aspx.vb:63:        If s.Status = "S" Then
Sprzet.aspx.vb:64:            e.Row.CssClass = "wyszarzony"
Sprzet.aspx.vb:65:        End If
Sprzet.aspx.vb:66:        If s.Status <> "D" AndAlso s.Status <> "W" Then
Sprzet.aspx.vb:67:            e.Row.Cells(7).Text = ""
Sprzet.aspx.vb:68:        End If
Sprzet.aspx.vb:69:    End Sub
Sprzet.aspx.vb:70:
Sprzet.aspx.vb:71:    Protected Sub btnNowy_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNowy.Click
Sprzet.aspx.vb:72:        gvSprzet.SelectedIndex = -1
Sprzet.aspx.vb:73:        WyczyscFormularz()
Sprzet.aspx.vb:74:        txtNumer.Focus()
Sprzet.aspx.vb:75:    End Sub
Sprzet.aspx.vb:76:
Sprzet.aspx.vb:77:    Protected Sub btnZapisz_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZapisz.Click
Sprzet.aspx.vb:78:        Dim stawka As Decimal
Sprzet.aspx.vb:79:        Dim kaucja As Decimal
Sprzet.aspx.vb:80:        If Not Decimal.TryParse(txtStawka.Text.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, stawka) Then
Sprzet.aspx.vb:81:            PokazBlad("Niepoprawna stawka dobowa.")
Sprzet.aspx.vb:82:            Return
Sprzet.aspx.vb:83:        End If
Sprzet.aspx.vb:84:        If txtKaucja.Text.Trim() = "" Then
Sprzet.aspx.vb:85:            kaucja = 0D
Sprzet.aspx.vb:86:        ElseIf Not Decimal.TryParse(txtKaucja.Text.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, kaucja) Then
Sprzet.aspx.vb:87:            PokazBlad("Niepoprawna kaucja.")
Sprzet.aspx.vb:88:            Return
Sprzet.aspx.vb:89:        End If
Sprzet.aspx.vb:90:
Sprzet.aspx.vb:91:        Dim mgr As New EquipmentManager()
Sprzet.aspx.vb:92:        Dim s As SprzetModel
Sprzet.aspx.vb:93:        Dim sprzetId As Integer = CInt(hfSprzetId.Value)
Sprzet.aspx.vb:94:        If sprzetId > 0 Then
Sprzet.aspx.vb:95:            s = mgr.PobierzSprzet(sprzetId)
Sprzet.aspx.vb:96:            If s Is Nothing Then
Sprzet.aspx.vb:97:                PokazBlad("Sprzet nie istnieje.")
Sprzet.aspx.vb:98:                Return
Sprzet.aspx.vb:99:            End If
Sprzet.aspx.vb:100:            If s.Kategoria <> ddlKategoriaEdycja.SelectedValue AndAlso Not mgr.CzyMoznaZmienicKategorie(sprzetId) Then
Sprzet.aspx.vb:101:                PokazBlad("Nie mozna zmienic kategorii wypozyczonego sprzetu.")
Sprzet.aspx.vb:102:                Return
Sprzet.aspx.vb:103:            End If
Sprzet.aspx.vb:104:        Else
Sprzet.aspx.vb:105:            s = New SprzetModel()
Sprzet.aspx.vb:106:        End If
Sprzet.aspx.vb:107:        s.NumerInwentarzowy = txtNumer.Text
Sprzet.aspx.vb:108:        s.Nazwa = txtNazwa.Text.Trim()
Sprzet.aspx.vb:109:        s.Kategoria = ddlKategoriaEdycja.SelectedValue
Sprzet.aspx.vb:110:        s.StawkaDobowa = stawka
Sprzet.aspx.vb:111:        s.Kaucja = kaucja
Sprzet.aspx.vb:112:
Sprzet.aspx.vb:113:        Try
Sprzet.aspx.vb:114:            Dim zapisaneId As Integer = mgr.Zapisz(s)
Sprzet.aspx.vb:115:            hfSprzetId.Value = zapisaneId.ToString()
Sprzet.aspx.vb:116:            PokazInfo("Zapisano sprzet " & s.NumerInwentarzowy & ".")
Sprzet.aspx.vb:117:            PokazListe()
Sprzet.aspx.vb:118:            ZaznaczWiersz(zapisaneId)
Sprzet.aspx.vb:119:        Catch ex As BladBiznesowyException
Sprzet.aspx.vb:120:            PokazBlad(ex.Message)
Sprzet.aspx.vb:121:        Catch ex As Exception
Sprzet.aspx.vb:122:            Log.Error("Blad zapisu sprzetu " & s.NumerInwentarzowy, ex)
Sprzet.aspx.vb:123:            PokazBlad("Nie udalo sie zapisac sprzetu.")
Sprzet.aspx.vb:124:        End Try
Sprzet.aspx.vb:125:    End Sub
Sprzet.aspx.vb:126:
Sprzet.aspx.vb:127:    Protected Sub btnSerwis_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSerwis.Click
Sprzet.aspx.vb:128:        Dim sprzetId As Integer = CInt(hfSprzetId.Value)
Sprzet.aspx.vb:129:        If sprzetId = 0 Then
Sprzet.aspx.vb:130:            PokazBlad("Wybierz sprzet z listy.")
Sprzet.aspx.vb:131:            Return
Sprzet.aspx.vb:132:        End If
Sprzet.aspx.vb:133:        Try
Sprzet.aspx.vb:134:            Dim mgr As New EquipmentManager()
Sprzet.aspx.vb:135:            mgr.WyslijDoSerwisu(sprzetId)
Sprzet.aspx.vb:136:            lblStatus.Text = "serwis"
Sprzet.aspx.vb:137:            PokazInfo("Sprzet przekazany do serwisu.")
Sprzet.aspx.vb:138:            PokazListe()
Sprzet.aspx.vb:139:            ZaznaczWiersz(sprzetId)
Sprzet.aspx.vb:140:        Catch ex As BladBiznesowyException
Sprzet.aspx.vb:141:            PokazBlad(ex.Message)
Sprzet.aspx.vb:142:        End Try
Sprzet.aspx.vb:143:    End Sub
Sprzet.aspx.vb:144:
Sprzet.aspx.vb:145:    Private Sub PokazListe()
Sprzet.aspx.vb:146:        Dim lista As List(Of SprzetModel) = New EquipmentManager().ListaWKategorii(ddlKategoria.SelectedValue)
Sprzet.aspx.vb:147:        gvSprzet.DataSource = lista
Sprzet.aspx.vb:148:        gvSprzet.DataBind()
Sprzet.aspx.vb:149:    End Sub
Sprzet.aspx.vb:150:
Sprzet.aspx.vb:151:    Private Sub PokazHistorie(ByVal sprzetId As Integer)
Sprzet.aspx.vb:152:        Dim tabela As DataTable = New EquipmentManager().HistoriaWypozyczen(sprzetId)
Sprzet.aspx.vb:153:        gvHistoria.DataSource = tabela
Sprzet.aspx.vb:154:        gvHistoria.DataBind()
Sprzet.aspx.vb:155:        gvHistoria.Visible = True
Sprzet.aspx.vb:156:    End Sub
Sprzet.aspx.vb:157:
Sprzet.aspx.vb:158:    Private Sub WypelnijFormularz(ByVal s As SprzetModel)
Sprzet.aspx.vb:159:        hfSprzetId.Value = s.Id.ToString()
Sprzet.aspx.vb:160:        txtNumer.Text = s.NumerInwentarzowy
Sprzet.aspx.vb:161:        txtNazwa.Text = s.Nazwa
Sprzet.aspx.vb:162:        If ddlKategoriaEdycja.Items.FindByValue(s.Kategoria) IsNot Nothing Then
Sprzet.aspx.vb:163:            ddlKategoriaEdycja.SelectedValue = s.Kategoria
Sprzet.aspx.vb:164:        End If
Sprzet.aspx.vb:165:        txtStawka.Text = s.StawkaDobowa.ToString("0.00")
Sprzet.aspx.vb:166:        txtKaucja.Text = s.Kaucja.ToString("0.00")
Sprzet.aspx.vb:167:        lblStatus.Text = s.OpisStatusu
Sprzet.aspx.vb:168:        btnSerwis.Enabled = (s.Status = "D")
Sprzet.aspx.vb:169:        txtNumer.ReadOnly = True
Sprzet.aspx.vb:170:    End Sub
Sprzet.aspx.vb:171:
Sprzet.aspx.vb:172:    Private Sub WyczyscFormularz()
Sprzet.aspx.vb:173:        hfSprzetId.Value = "0"
Sprzet.aspx.vb:174:        txtNumer.Text = ""
Sprzet.aspx.vb:175:        txtNazwa.Text = ""
Sprzet.aspx.vb:176:        txtStawka.Text = ""
Sprzet.aspx.vb:177:        txtKaucja.Text = ""
Sprzet.aspx.vb:178:        lblStatus.Text = "-"
Sprzet.aspx.vb:179:        txtNumer.ReadOnly = False
Sprzet.aspx.vb:180:        btnSerwis.Enabled = False
Sprzet.aspx.vb:181:        If ddlKategoria.SelectedValue <> "" Then
Sprzet.aspx.vb:182:            ddlKategoriaEdycja.SelectedValue = ddlKategoria.SelectedValue
Sprzet.aspx.vb:183:        End If
Sprzet.aspx.vb:184:        gvHistoria.DataSource = Nothing
Sprzet.aspx.vb:185:        gvHistoria.DataBind()
Sprzet.aspx.vb:186:        gvHistoria.Visible = False
Sprzet.aspx.vb:187:    End Sub
Sprzet.aspx.vb:188:
Sprzet.aspx.vb:189:    Private Sub ZaznaczWiersz(ByVal sprzetId As Integer)
Sprzet.aspx.vb:190:        For i As Integer = 0 To gvSprzet.DataKeys.Count - 1
Sprzet.aspx.vb:191:            If CInt(gvSprzet.DataKeys(i).Value) = sprzetId Then
Sprzet.aspx.vb:192:                gvSprzet.SelectedIndex = i
Sprzet.aspx.vb:193:                Return
Sprzet.aspx.vb:194:            End If
Sprzet.aspx.vb:195:        Next
Sprzet.aspx.vb:196:        gvSprzet.SelectedIndex = -1
Sprzet.aspx.vb:197:    End Sub
Sprzet.aspx.vb:198:
Sprzet.aspx.vb:199:    Private Sub PokazBlad(ByVal tekst As String)
Sprzet.aspx.vb:200:        lblKomunikat.CssClass = "blad"
Sprzet.aspx.vb:201:        lblKomunikat.Text = Server.HtmlEncode(tekst)
Sprzet.aspx.vb:202:    End Sub
Sprzet.aspx.vb:203:
Sprzet.aspx.vb:204:    Private Sub PokazInfo(ByVal tekst As String)
Sprzet.aspx.vb:205:        lblKomunikat.CssClass = "info"
Sprzet.aspx.vb:206:        lblKomunikat.Text = Server.HtmlEncode(tekst)
Sprzet.aspx.vb:207:    End Sub
Sprzet.aspx.vb:208:
Sprzet.aspx.vb:209:End Class
Wypozyczenie.aspx.vb:1:' Modul: Wypozyczenie.aspx.vb | Autor: zespol IT | 2014-03-11
Wypozyczenie.aspx.vb:2:' Wydanie sprzetu klientowi (z rezerwacji albo od reki), opcjonalnie dostawa kurierem.
Wypozyczenie.aspx.vb:3:' 2015-06-22 MW: dostawa kurierem (KurierClient)
Wypozyczenie.aspx.vb:4:' 2018-02-09 PT: lista wydan i zwrotow z dnia na dole strony
Wypozyczenie.aspx.vb:5:
Wypozyczenie.aspx.vb:6:Imports System.Data
Wypozyczenie.aspx.vb:7:Imports System.Globalization
Wypozyczenie.aspx.vb:8:Imports Wypozyczalnia.Core
Wypozyczenie.aspx.vb:9:Imports Wypozyczalnia.Core.Model
Wypozyczenie.aspx.vb:10:
Wypozyczenie.aspx.vb:11:Partial Class Wypozyczenie
Wypozyczenie.aspx.vb:12:    Inherits System.Web.UI.Page
Wypozyczenie.aspx.vb:13:
Wypozyczenie.aspx.vb:14:    Protected WithEvents lblKlient As Global.System.Web.UI.WebControls.Label
Wypozyczenie.aspx.vb:15:    Protected WithEvents lblKomunikat As Global.System.Web.UI.WebControls.Label
Wypozyczenie.aspx.vb:16:    Protected WithEvents ddlRezerwacje As Global.System.Web.UI.WebControls.DropDownList
Wypozyczenie.aspx.vb:17:    Protected WithEvents ddlSprzet As Global.System.Web.UI.WebControls.DropDownList
Wypozyczenie.aspx.vb:18:    Protected WithEvents txtPlanowanyZwrot As Global.System.Web.UI.WebControls.TextBox
Wypozyczenie.aspx.vb:19:    Protected WithEvents rfvPlanowanyZwrot As Global.System.Web.UI.WebControls.RequiredFieldValidator
Wypozyczenie.aspx.vb:20:    Protected WithEvents lblKaucja As Global.System.Web.UI.WebControls.Label
Wypozyczenie.aspx.vb:21:    Protected WithEvents chkDostawa As Global.System.Web.UI.WebControls.CheckBox
Wypozyczenie.aspx.vb:22:    Protected WithEvents txtAdres As Global.System.Web.UI.WebControls.TextBox
Wypozyczenie.aspx.vb:23:    Protected WithEvents btnWydaj As Global.System.Web.UI.WebControls.Button
Wypozyczenie.aspx.vb:24:    Protected WithEvents lblNumerPrzesylki As Global.System.Web.UI.WebControls.Label
Wypozyczenie.aspx.vb:25:    Protected WithEvents gvWydaneDzis As Global.System.Web.UI.WebControls.GridView
Wypozyczenie.aspx.vb:26:
Wypozyczenie.aspx.vb:27:    Private Const FormatDaty As String = "yyyy-MM-dd HH:mm"
Wypozyczenie.aspx.vb:28:
Wypozyczenie.aspx.vb:29:    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
Wypozyczenie.aspx.vb:30:        If Not IsPostBack Then
Wypozyczenie.aspx.vb:31:            Dim klientId As Integer = Session("WybranyKlientId")
Wypozyczenie.aspx.vb:32:            If klientId = 0 Then
Wypozyczenie.aspx.vb:33:                lblKomunikat.Text = "Najpierw wybierz klienta na stronie Klienci."
Wypozyczenie.aspx.vb:34:                btnWydaj.Enabled = False
Wypozyczenie.aspx.vb:35:            Else
Wypozyczenie.aspx.vb:36:                PokazKlienta(klientId)
Wypozyczenie.aspx.vb:37:                WczytajRezerwacje(klientId)
Wypozyczenie.aspx.vb:38:            End If
Wypozyczenie.aspx.vb:39:            WczytajSprzet()
Wypozyczenie.aspx.vb:40:            WczytajRuchZDnia()
Wypozyczenie.aspx.vb:41:            Dim zAdresu As String = Request.QueryString("rezerwacja")
Wypozyczenie.aspx.vb:42:            If Not String.IsNullOrEmpty(zAdresu) AndAlso ddlRezerwacje.Items.FindByValue(zAdresu) IsNot Nothing Then
Wypozyczenie.aspx.vb:43:                ddlRezerwacje.SelectedValue = zAdresu
Wypozyczenie.aspx.vb:44:                ddlRezerwacje_SelectedIndexChanged(ddlRezerwacje, EventArgs.Empty)
Wypozyczenie.aspx.vb:45:            End If
Wypozyczenie.aspx.vb:46:        End If
Wypozyczenie.aspx.vb:47:    End Sub
Wypozyczenie.aspx.vb:48:
Wypozyczenie.aspx.vb:49:    Protected Sub gvWydaneDzis_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvWydaneDzis.RowDataBound
Wypozyczenie.aspx.vb:50:        If e.Row.RowType <> DataControlRowType.DataRow Then Return
Wypozyczenie.aspx.vb:51:        Dim wiersz As DataRowView = CType(e.Row.DataItem, DataRowView)
Wypozyczenie.aspx.vb:52:        If Not IsDBNull(wiersz("DataZwrotu")) Then
Wypozyczenie.aspx.vb:53:            e.Row.ForeColor = Drawing.Color.Gray
Wypozyczenie.aspx.vb:54:            e.Row.ToolTip = "Zwrocono " & CDate(wiersz("DataZwrotu")).ToString(FormatDaty)
Wypozyczenie.aspx.vb:55:        ElseIf CDate(wiersz("PlanowanyZwrot")) < Date.Now Then
Wypozyczenie.aspx.vb:56:            e.Row.ForeColor = Drawing.Color.DarkRed
Wypozyczenie.aspx.vb:57:            e.Row.ToolTip = "Po terminie zwrotu"
Wypozyczenie.aspx.vb:58:        End If
Wypozyczenie.aspx.vb:59:    End Sub
Wypozyczenie.aspx.vb:60:
Wypozyczenie.aspx.vb:61:    Private Sub PokazKlienta(ByVal klientId As Integer)
Wypozyczenie.aspx.vb:62:        Dim klient As Klient = New CustomerManager().PobierzKlienta(klientId)
Wypozyczenie.aspx.vb:63:        If klient Is Nothing Then
Wypozyczenie.aspx.vb:64:            lblKlient.Text = "(nie znaleziono klienta " & klientId & ")"
Wypozyczenie.aspx.vb:65:            btnWydaj.Enabled = False
Wypozyczenie.aspx.vb:66:            Return
Wypozyczenie.aspx.vb:67:        End If
Wypozyczenie.aspx.vb:68:        If klient.Typ = "F" AndAlso Not String.IsNullOrEmpty(klient.NazwaFirmy) Then
Wypozyczenie.aspx.vb:69:            lblKlient.Text = Server.HtmlEncode(klient.NazwaFirmy & " (" & klient.Imie & " " & klient.Nazwisko & ")")
Wypozyczenie.aspx.vb:70:        Else
Wypozyczenie.aspx.vb:71:            lblKlient.Text = Server.HtmlEncode(klient.Imie & " " & klient.Nazwisko)
Wypozyczenie.aspx.vb:72:        End If
Wypozyczenie.aspx.vb:73:        If klient.Zablokowany Then
Wypozyczenie.aspx.vb:74:            lblKomunikat.Text = "Klient zablokowany: " & Server.HtmlEncode(klient.PowodBlokady)
Wypozyczenie.aspx.vb:75:            btnWydaj.Enabled = False
Wypozyczenie.aspx.vb:76:            Return
Wypozyczenie.aspx.vb:77:        End If
Wypozyczenie.aspx.vb:78:        Dim otwarte As List(Of Model.Wypozyczenie) = New RentalManager().OtwarteKlienta(klientId)
Wypozyczenie.aspx.vb:79:        If otwarte.Count > 0 Then
Wypozyczenie.aspx.vb:80:            Dim poTerminie As Integer = 0
Wypozyczenie.aspx.vb:81:            For Each w As Model.Wypozyczenie In otwarte
Wypozyczenie.aspx.vb:82:                If w.CzyPoTerminie(Date.Now) Then poTerminie = poTerminie + 1
Wypozyczenie.aspx.vb:83:            Next
Wypozyczenie.aspx.vb:84:            lblKlient.Text = lblKlient.Text & " - otwarte wypozyczenia: " & otwarte.Count
Wypozyczenie.aspx.vb:85:            If poTerminie > 0 Then
Wypozyczenie.aspx.vb:86:                lblKomunikat.Text = "Uwaga: klient ma " & poTerminie & " wypozyczen po terminie zwrotu."
Wypozyczenie.aspx.vb:87:            End If
Wypozyczenie.aspx.vb:88:        End If
Wypozyczenie.aspx.vb:89:    End Sub
Wypozyczenie.aspx.vb:90:
Wypozyczenie.aspx.vb:91:    Private Sub WczytajRezerwacje(ByVal klientId As Integer)
Wypozyczenie.aspx.vb:92:        ddlRezerwacje.Items.Clear()
Wypozyczenie.aspx.vb:93:        ddlRezerwacje.Items.Add(New ListItem("-- bez rezerwacji --", "0"))
Wypozyczenie.aspx.vb:94:        Dim tabela As DataTable = New ReservationManager().RezerwacjeKlienta(klientId)
Wypozyczenie.aspx.vb:95:        For Each wiersz As DataRow In tabela.Rows
Wypozyczenie.aspx.vb:96:            Dim status As String = wiersz("Status").ToString()
Wypozyczenie.aspx.vb:97:            If status = "N" OrElse status = "P" Then
Wypozyczenie.aspx.vb:98:                Dim tekst As String = String.Format("#{0} {1} | {2:yyyy-MM-dd HH:mm} - {3:yyyy-MM-dd HH:mm} | {4}",
Wypozyczenie.aspx.vb:99:                    wiersz("Id"), wiersz("Sprzet"), wiersz("DataOd"), wiersz("DataDo"), status)
Wypozyczenie.aspx.vb:100:                ddlRezerwacje.Items.Add(New ListItem(tekst, wiersz("Id").ToString()))
Wypozyczenie.aspx.vb:101:            End If
Wypozyczenie.aspx.vb:102:        Next
Wypozyczenie.aspx.vb:103:    End Sub
Wypozyczenie.aspx.vb:104:
Wypozyczenie.aspx.vb:105:    Private Sub WczytajSprzet()
Wypozyczenie.aspx.vb:106:        ddlSprzet.Items.Clear()
Wypozyczenie.aspx.vb:107:        ddlSprzet.Items.Add(New ListItem("-- wybierz --", "0"))
Wypozyczenie.aspx.vb:108:        Dim lista As List(Of Sprzet) = New EquipmentManager().ListaWKategorii("")
Wypozyczenie.aspx.vb:109:        For Each s As Sprzet In lista
Wypozyczenie.aspx.vb:110:            If s.Status = "D" Then
Wypozyczenie.aspx.vb:111:                ddlSprzet.Items.Add(New ListItem(s.NumerInwentarzowy & " " & s.Nazwa, s.Id.ToString()))
Wypozyczenie.aspx.vb:112:            End If
Wypozyczenie.aspx.vb:113:        Next
Wypozyczenie.aspx.vb:114:    End Sub
Wypozyczenie.aspx.vb:115:
Wypozyczenie.aspx.vb:116:    Private Sub WczytajRuchZDnia()
Wypozyczenie.aspx.vb:117:        gvWydaneDzis.DataSource = New RentalManager().WypozyczeniaZDnia(Date.Today)
Wypozyczenie.aspx.vb:118:        gvWydaneDzis.DataBind()
Wypozyczenie.aspx.vb:119:    End Sub
Wypozyczenie.aspx.vb:120:
Wypozyczenie.aspx.vb:121:    Protected Sub ddlRezerwacje_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlRezerwacje.SelectedIndexChanged
Wypozyczenie.aspx.vb:122:        lblNumerPrzesylki.Text = "-"
Wypozyczenie.aspx.vb:123:        Dim rezerwacjaId As Integer = CInt(ddlRezerwacje.SelectedValue)
Wypozyczenie.aspx.vb:124:        If rezerwacjaId = 0 Then
Wypozyczenie.aspx.vb:125:            ddlSprzet.Enabled = True
Wypozyczenie.aspx.vb:126:            txtPlanowanyZwrot.Text = ""
Wypozyczenie.aspx.vb:127:            lblKaucja.Text = "-"
Wypozyczenie.aspx.vb:128:            Return
Wypozyczenie.aspx.vb:129:        End If
Wypozyczenie.aspx.vb:130:        Dim r As Rezerwacja = New ReservationManager().PobierzRezerwacje(rezerwacjaId)
Wypozyczenie.aspx.vb:131:        If r Is Nothing Then
Wypozyczenie.aspx.vb:132:            lblKomunikat.Text = "Nie znaleziono rezerwacji " & rezerwacjaId
Wypozyczenie.aspx.vb:133:            Return
Wypozyczenie.aspx.vb:134:        End If
Wypozyczenie.aspx.vb:135:        ddlSprzet.SelectedIndex = 0
Wypozyczenie.aspx.vb:136:        ddlSprzet.Enabled = False
Wypozyczenie.aspx.vb:137:        txtPlanowanyZwrot.Text = r.DataDo.ToString(FormatDaty)
Wypozyczenie.aspx.vb:138:        PokazKaucje(r.SprzetId)
Wypozyczenie.aspx.vb:139:    End Sub
Wypozyczenie.aspx.vb:140:
Wypozyczenie.aspx.vb:141:    Protected Sub ddlSprzet_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlSprzet.SelectedIndexChanged
Wypozyczenie.aspx.vb:142:        Dim sprzetId As Integer = CInt(ddlSprzet.SelectedValue)
Wypozyczenie.aspx.vb:143:        If sprzetId = 0 Then
Wypozyczenie.aspx.vb:144:            lblKaucja.Text = "-"
Wypozyczenie.aspx.vb:145:        Else
Wypozyczenie.aspx.vb:146:            PokazKaucje(sprzetId)
Wypozyczenie.aspx.vb:147:            If txtPlanowanyZwrot.Text = "" Then
Wypozyczenie.aspx.vb:148:                txtPlanowanyZwrot.Text = Date.Now.AddDays(1).ToString(FormatDaty)
Wypozyczenie.aspx.vb:149:            End If
Wypozyczenie.aspx.vb:150:        End If
Wypozyczenie.aspx.vb:151:    End Sub
Wypozyczenie.aspx.vb:152:
Wypozyczenie.aspx.vb:153:    Private Sub PokazKaucje(ByVal sprzetId As Integer)
Wypozyczenie.aspx.vb:154:        Dim s As Sprzet = New EquipmentManager().PobierzSprzet(sprzetId)
Wypozyczenie.aspx.vb:155:        If s Is Nothing Then
Wypozyczenie.aspx.vb:156:            lblKaucja.Text = "-"
Wypozyczenie.aspx.vb:157:        Else
Wypozyczenie.aspx.vb:158:            lblKaucja.Text = s.Kaucja.ToString("N2")
Wypozyczenie.aspx.vb:159:        End If
Wypozyczenie.aspx.vb:160:    End Sub
Wypozyczenie.aspx.vb:161:
Wypozyczenie.aspx.vb:162:    Protected Sub chkDostawa_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkDostawa.CheckedChanged
Wypozyczenie.aspx.vb:163:        txtAdres.Enabled = chkDostawa.Checked
Wypozyczenie.aspx.vb:164:        If Not chkDostawa.Checked Then
Wypozyczenie.aspx.vb:165:            txtAdres.Text = ""
Wypozyczenie.aspx.vb:166:        End If
Wypozyczenie.aspx.vb:167:    End Sub
Wypozyczenie.aspx.vb:168:
Wypozyczenie.aspx.vb:169:    Protected Sub btnWydaj_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnWydaj.Click
Wypozyczenie.aspx.vb:170:        Dim klientId As Integer = Session("WybranyKlientId")
Wypozyczenie.aspx.vb:171:        If klientId = 0 Then
Wypozyczenie.aspx.vb:172:            lblKomunikat.Text = "Sesja wygasla - wybierz klienta ponownie."
Wypozyczenie.aspx.vb:173:            Return
Wypozyczenie.aspx.vb:174:        End If
Wypozyczenie.aspx.vb:175:
Wypozyczenie.aspx.vb:176:        Dim rezerwacjaId As Integer = CInt(ddlRezerwacje.SelectedValue)
Wypozyczenie.aspx.vb:177:        Dim sprzetId As Integer
Wypozyczenie.aspx.vb:178:        If rezerwacjaId > 0 Then
Wypozyczenie.aspx.vb:179:            Dim r As Rezerwacja = New ReservationManager().PobierzRezerwacje(rezerwacjaId)
Wypozyczenie.aspx.vb:180:            If r Is Nothing OrElse r.KlientId <> klientId Then
Wypozyczenie.aspx.vb:181:                lblKomunikat.Text = "Rezerwacja nie nalezy do wybranego klienta."
Wypozyczenie.aspx.vb:182:                Return
Wypozyczenie.aspx.vb:183:            End If
Wypozyczenie.aspx.vb:184:            sprzetId = r.SprzetId
Wypozyczenie.aspx.vb:185:        Else
Wypozyczenie.aspx.vb:186:            sprzetId = CInt(ddlSprzet.SelectedValue)
Wypozyczenie.aspx.vb:187:        End If
Wypozyczenie.aspx.vb:188:        If sprzetId = 0 Then
Wypozyczenie.aspx.vb:189:            lblKomunikat.Text = "Wybierz rezerwacje albo sprzet."
Wypozyczenie.aspx.vb:190:            Return
Wypozyczenie.aspx.vb:191:        End If
Wypozyczenie.aspx.vb:192:
Wypozyczenie.aspx.vb:193:        Dim planowanyZwrot As Date
Wypozyczenie.aspx.vb:194:        If Not Date.TryParseExact(txtPlanowanyZwrot.Text.Trim(), FormatDaty, CultureInfo.InvariantCulture, DateTimeStyles.None, planowanyZwrot) Then
Wypozyczenie.aspx.vb:195:            lblKomunikat.Text = "Niepoprawny format daty zwrotu (rrrr-mm-dd gg:mm)."
Wypozyczenie.aspx.vb:196:            Return
Wypozyczenie.aspx.vb:197:        End If
Wypozyczenie.aspx.vb:198:        If planowanyZwrot <= Date.Now Then
Wypozyczenie.aspx.vb:199:            lblKomunikat.Text = "Planowany zwrot musi byc pozniejszy niz chwila wydania."
Wypozyczenie.aspx.vb:200:            Return
Wypozyczenie.aspx.vb:201:        End If
Wypozyczenie.aspx.vb:202:
Wypozyczenie.aspx.vb:203:        Dim adres As String = ""
Wypozyczenie.aspx.vb:204:        If chkDostawa.Checked Then
Wypozyczenie.aspx.vb:205:            adres = txtAdres.Text.Trim()
Wypozyczenie.aspx.vb:206:            If adres = "" Then
Wypozyczenie.aspx.vb:207:                lblKomunikat.Text = "Podaj adres dostawy albo odznacz dostawe."
Wypozyczenie.aspx.vb:208:                Return
Wypozyczenie.aspx.vb:209:            End If
Wypozyczenie.aspx.vb:210:        End If
Wypozyczenie.aspx.vb:211:
Wypozyczenie.aspx.vb:212:        Try
Wypozyczenie.aspx.vb:213:            Dim manager As New RentalManager()
Wypozyczenie.aspx.vb:214:            Dim noweId As Integer = manager.WydajSprzet(rezerwacjaId, klientId, sprzetId, planowanyZwrot, adres)
Wypozyczenie.aspx.vb:215:            Dim w As Model.Wypozyczenie = manager.PobierzWypozyczenie(noweId)
Wypozyczenie.aspx.vb:216:            lblKaucja.Text = w.KaucjaPobrana.ToString("N2")
Wypozyczenie.aspx.vb:217:            If chkDostawa.Checked Then
Wypozyczenie.aspx.vb:218:                If String.IsNullOrEmpty(w.NumerPrzesylki) Then
Wypozyczenie.aspx.vb:219:                    lblNumerPrzesylki.Text = "brak - zamow kuriera telefonicznie"
Wypozyczenie.aspx.vb:220:                Else
Wypozyczenie.aspx.vb:221:                    lblNumerPrzesylki.Text = w.NumerPrzesylki
Wypozyczenie.aspx.vb:222:                End If
Wypozyczenie.aspx.vb:223:            Else
Wypozyczenie.aspx.vb:224:                lblNumerPrzesylki.Text = "odbior osobisty"
Wypozyczenie.aspx.vb:225:            End If
Wypozyczenie.aspx.vb:226:            lblKomunikat.ForeColor = Drawing.Color.Green
Wypozyczenie.aspx.vb:227:            lblKomunikat.Text = "Wydano sprzet, wypozyczenie nr " & noweId & "."
Wypozyczenie.aspx.vb:228:            WczytajRezerwacje(klientId)
Wypozyczenie.aspx.vb:229:            WczytajSprzet()
Wypozyczenie.aspx.vb:230:            WczytajRuchZDnia()
Wypozyczenie.aspx.vb:231:            ddlSprzet.Enabled = True
Wypozyczenie.aspx.vb:232:            txtPlanowanyZwrot.Text = ""
Wypozyczenie.aspx.vb:233:            chkDostawa.Checked = False
Wypozyczenie.aspx.vb:234:            txtAdres.Text = ""
Wypozyczenie.aspx.vb:235:            txtAdres.Enabled = False
Wypozyczenie.aspx.vb:236:        Catch ex As BladBiznesowyException
Wypozyczenie.aspx.vb:237:            lblKomunikat.Text = ex.Message
Wypozyczenie.aspx.vb:238:        Catch ex As Exception
Wypozyczenie.aspx.vb:239:            lblKomunikat.Text = "Blad podczas wydania: " & ex.Message
Wypozyczenie.aspx.vb:240:        End Try
Wypozyczenie.aspx.vb:241:    End Sub
Wypozyczenie.aspx.vb:242:
Wypozyczenie.aspx.vb:243:End Class
```

## M13 — surowy wynik H3

`git --no-pager -C 'legacy-app-parent\legacy-app\WypozyczalniaCore' grep -n -I -e '^' -- '*Manager.vb'`

```text
CustomerManager.vb:1:' Modul: CustomerManager.vb - obsluga klientow wypozyczalni | Autor: zespol IT | 2014-03-11
CustomerManager.vb:2:' Historia: 2014-03-11 JK utworzenie
CustomerManager.vb:3:'           2015-11-23 MW blokowanie klientow (zalegle platnosci, zgubiony sprzet)
CustomerManager.vb:4:'           2018-09-04 PT klienci firmowi (NIP, NazwaFirmy)
CustomerManager.vb:5:Imports System.Data
CustomerManager.vb:6:Imports System.Data.SqlClient
CustomerManager.vb:7:Imports Wypozyczalnia.Core.Model
CustomerManager.vb:8:Imports Wypozyczalnia.Core.DAL
CustomerManager.vb:9:
CustomerManager.vb:10:Public Class CustomerManager
CustomerManager.vb:11:
CustomerManager.vb:12:    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(CustomerManager))
CustomerManager.vb:13:
CustomerManager.vb:14:    Private Const KolumnyKlienta As String = "Id, Imie, Nazwisko, NazwaFirmy, Email, Telefon, NumerDokumentu, NIP, Typ, ProgRabatu, Zablokowany, PowodBlokady, DataRejestracji"
CustomerManager.vb:15:
CustomerManager.vb:16:    Public Function PobierzKlienta(ByVal klientId As Integer) As Klient
CustomerManager.vb:17:        Dim dt As DataTable = DbHelper.WykonajZapytanie( _
CustomerManager.vb:18:            "SELECT " & KolumnyKlienta & " FROM Klienci WHERE Id = @Id", _
CustomerManager.vb:19:            DbHelper.Parametr("@Id", klientId))
CustomerManager.vb:20:        If dt.Rows.Count = 0 Then
CustomerManager.vb:21:            Return Nothing
CustomerManager.vb:22:        End If
CustomerManager.vb:23:        Return MapujKlienta(dt.Rows(0))
CustomerManager.vb:24:    End Function
CustomerManager.vb:25:
CustomerManager.vb:26:    Public Function CzyMozeWypozyczac(ByVal klientId As Integer) As Boolean
CustomerManager.vb:27:        Dim k As Klient = PobierzKlienta(klientId)
CustomerManager.vb:28:        If k Is Nothing Then
CustomerManager.vb:29:            Log.Warn("Sprawdzenie nieistniejacego klienta: " & klientId)
CustomerManager.vb:30:            Return False
CustomerManager.vb:31:        End If
CustomerManager.vb:32:        If k.Zablokowany Then
CustomerManager.vb:33:            Log.Info("Klient " & klientId & " zablokowany: " & k.PowodBlokady)
CustomerManager.vb:34:            Return False
CustomerManager.vb:35:        End If
CustomerManager.vb:36:        Return True
CustomerManager.vb:37:    End Function
CustomerManager.vb:38:
CustomerManager.vb:39:    Public Function ProgRabatu(ByVal klientId As Integer) As Integer
CustomerManager.vb:40:        Dim dt As DataTable = DbHelper.WykonajZapytanie( _
CustomerManager.vb:41:            "SELECT ProgRabatu FROM Klienci WHERE Id = @Id", _
CustomerManager.vb:42:            DbHelper.Parametr("@Id", klientId))
CustomerManager.vb:43:        If dt.Rows.Count = 0 Then
CustomerManager.vb:44:            Return 0
CustomerManager.vb:45:        End If
CustomerManager.vb:46:        Dim prog As Integer = dt.Rows(0)("ProgRabatu")
CustomerManager.vb:47:        If prog < 0 Then prog = 0
CustomerManager.vb:48:        If prog > 3 Then prog = 3
CustomerManager.vb:49:        Return prog
CustomerManager.vb:50:    End Function
CustomerManager.vb:51:
CustomerManager.vb:52:    Public Function Szukaj(ByVal fraza As String) As DataTable
CustomerManager.vb:53:        If fraza Is Nothing Then fraza = ""
CustomerManager.vb:54:        fraza = fraza.Trim()
CustomerManager.vb:55:        Dim sql As String = "SELECT TOP 200 Id, Imie, Nazwisko, NazwaFirmy, Email, Telefon FROM Klienci " & _
CustomerManager.vb:56:                            "WHERE (@Fraza = '' OR Nazwisko LIKE @Wzorzec OR NazwaFirmy LIKE @Wzorzec OR Email LIKE @Wzorzec) " & _
CustomerManager.vb:57:                            "ORDER BY Nazwisko, Imie"
CustomerManager.vb:58:        Return DbHelper.WykonajZapytanie(sql, _
CustomerManager.vb:59:            DbHelper.Parametr("@Fraza", fraza), _
CustomerManager.vb:60:            DbHelper.Parametr("@Wzorzec", "%" & fraza & "%"))
CustomerManager.vb:61:    End Function
CustomerManager.vb:62:
CustomerManager.vb:63:    Public Function Zapisz(ByVal klient As Klient) As Integer
CustomerManager.vb:64:        If klient Is Nothing Then Throw New ArgumentNullException("klient")
CustomerManager.vb:65:        If String.IsNullOrEmpty(klient.Nazwisko) AndAlso String.IsNullOrEmpty(klient.NazwaFirmy) Then
CustomerManager.vb:66:            Throw New BladBiznesowyException("Podaj nazwisko albo nazwe firmy", "KLI-01")
CustomerManager.vb:67:        End If
CustomerManager.vb:68:        If String.IsNullOrEmpty(klient.Typ) Then klient.Typ = "I"
CustomerManager.vb:69:        If klient.Typ = "F" AndAlso String.IsNullOrEmpty(klient.NIP) Then
CustomerManager.vb:70:            Throw New BladBiznesowyException("Klient firmowy wymaga numeru NIP", "KLI-02")
CustomerManager.vb:71:        End If
CustomerManager.vb:72:        If klient.Imie Is Nothing Then klient.Imie = ""
CustomerManager.vb:73:        If klient.Nazwisko Is Nothing Then klient.Nazwisko = ""
CustomerManager.vb:74:
CustomerManager.vb:75:        If klient.Id = 0 Then
CustomerManager.vb:76:            Dim dt As DataTable = DbHelper.WykonajZapytanie( _
CustomerManager.vb:77:                "INSERT INTO Klienci (Imie, Nazwisko, NazwaFirmy, Email, Telefon, NumerDokumentu, NIP, Typ, ProgRabatu) " & _
CustomerManager.vb:78:                "VALUES (@Imie, @Nazwisko, @NazwaFirmy, @Email, @Telefon, @NumerDokumentu, @NIP, @Typ, @ProgRabatu); " & _
CustomerManager.vb:79:                "SELECT CAST(SCOPE_IDENTITY() AS INT) AS NoweId", _
CustomerManager.vb:80:                ParametryKlienta(klient))
CustomerManager.vb:81:            klient.Id = dt.Rows(0)("NoweId")
CustomerManager.vb:82:            Log.Info("Dodano klienta " & klient.Id)
CustomerManager.vb:83:            ZapiszDziennik("KLIENT_NOWY", "Id=" & klient.Id)
CustomerManager.vb:84:        Else
CustomerManager.vb:85:            Dim parametry As New List(Of SqlParameter)(ParametryKlienta(klient))
CustomerManager.vb:86:            parametry.Add(DbHelper.Parametr("@Id", klient.Id))
CustomerManager.vb:87:            Dim zmienione As Integer = DbHelper.WykonajPolecenie( _
CustomerManager.vb:88:                "UPDATE Klienci SET Imie = @Imie, Nazwisko = @Nazwisko, NazwaFirmy = @NazwaFirmy, Email = @Email, " & _
CustomerManager.vb:89:                "Telefon = @Telefon, NumerDokumentu = @NumerDokumentu, NIP = @NIP, Typ = @Typ, ProgRabatu = @ProgRabatu " & _
CustomerManager.vb:90:                "WHERE Id = @Id", _
CustomerManager.vb:91:                parametry.ToArray())
CustomerManager.vb:92:            If zmienione = 0 Then
CustomerManager.vb:93:                Throw New BladBiznesowyException("Nie znaleziono klienta nr " & klient.Id, "KLI-03")
CustomerManager.vb:94:            End If
CustomerManager.vb:95:            Log.Info("Zaktualizowano klienta " & klient.Id)
CustomerManager.vb:96:        End If
CustomerManager.vb:97:        Return klient.Id
CustomerManager.vb:98:    End Function
CustomerManager.vb:99:
CustomerManager.vb:100:    Public Sub Zablokuj(ByVal klientId As Integer, ByVal powod As String)
CustomerManager.vb:101:        If powod IsNot Nothing AndAlso powod.Length > 200 Then
CustomerManager.vb:102:            powod = powod.Substring(0, 200)
CustomerManager.vb:103:        End If
CustomerManager.vb:104:        DbHelper.WykonajPolecenie( _
CustomerManager.vb:105:            "UPDATE Klienci SET Zablokowany = 1, PowodBlokady = @Powod WHERE Id = @Id", _
CustomerManager.vb:106:            DbHelper.Parametr("@Powod", powod), _
CustomerManager.vb:107:            DbHelper.Parametr("@Id", klientId))
CustomerManager.vb:108:        Log.Warn("Zablokowano klienta " & klientId & ": " & powod)
CustomerManager.vb:109:        ZapiszDziennik("KLIENT_BLOKADA", "Id=" & klientId & "; " & powod)
CustomerManager.vb:110:    End Sub
CustomerManager.vb:111:
CustomerManager.vb:112:    Public Sub Odblokuj(ByVal klientId As Integer)
CustomerManager.vb:113:        DbHelper.WykonajPolecenie( _
CustomerManager.vb:114:            "UPDATE Klienci SET Zablokowany = 0, PowodBlokady = NULL WHERE Id = @Id", _
CustomerManager.vb:115:            DbHelper.Parametr("@Id", klientId))
CustomerManager.vb:116:        ZapiszDziennik("KLIENT_ODBLOKOWANIE", "Id=" & klientId)
CustomerManager.vb:117:    End Sub
CustomerManager.vb:118:
CustomerManager.vb:119:    Public Function PobierzEmail(ByVal klientId As Integer) As String
CustomerManager.vb:120:        Dim dt As DataTable = DbHelper.WykonajZapytanie( _
CustomerManager.vb:121:            "SELECT Email FROM Klienci WHERE Id = @Id", DbHelper.Parametr("@Id", klientId))
CustomerManager.vb:122:        If dt.Rows.Count = 0 Then Return ""
CustomerManager.vb:123:        Return dt.Rows(0)("Email").ToString()
CustomerManager.vb:124:    End Function
CustomerManager.vb:125:
CustomerManager.vb:126:    Public Function LiczbaOtwartychWypozyczen(ByVal klientId As Integer) As Integer
CustomerManager.vb:127:        Dim dt As DataTable = DbHelper.WykonajZapytanie( _
CustomerManager.vb:128:            "SELECT COUNT(*) AS Liczba FROM Wypozyczenia WHERE KlientId = @Id AND Status = 'O'", _
CustomerManager.vb:129:            DbHelper.Parametr("@Id", klientId))
CustomerManager.vb:130:        If dt.Rows.Count = 0 Then Return 0
CustomerManager.vb:131:        Return dt.Rows(0)("Liczba")
CustomerManager.vb:132:    End Function
CustomerManager.vb:133:
CustomerManager.vb:134:    Public Function KlienciZablokowani() As DataTable
CustomerManager.vb:135:        Return DbHelper.WykonajZapytanie( _
CustomerManager.vb:136:            "SELECT Id, Imie, Nazwisko, NazwaFirmy, Email, Telefon, PowodBlokady FROM Klienci " & _
CustomerManager.vb:137:            "WHERE Zablokowany = 1 ORDER BY Nazwisko, Imie")
CustomerManager.vb:138:    End Function
CustomerManager.vb:139:
CustomerManager.vb:140:    Public Function CzyEmailZajety(ByVal email As String, ByVal pominKlientaId As Integer) As Boolean
CustomerManager.vb:141:        If String.IsNullOrEmpty(email) Then Return False
CustomerManager.vb:142:        Dim dt As DataTable = DbHelper.WykonajZapytanie( _
CustomerManager.vb:143:            "SELECT COUNT(*) AS Liczba FROM Klienci WHERE Email = @Email AND Id <> @Id", _
CustomerManager.vb:144:            DbHelper.Parametr("@Email", email.Trim()), _
CustomerManager.vb:145:            DbHelper.Parametr("@Id", pominKlientaId))
CustomerManager.vb:146:        Return CInt(dt.Rows(0)("Liczba")) > 0
CustomerManager.vb:147:    End Function
CustomerManager.vb:148:
CustomerManager.vb:149:    Private Function ParametryKlienta(ByVal k As Klient) As SqlParameter()
CustomerManager.vb:150:        Return New SqlParameter() { _
CustomerManager.vb:151:            DbHelper.Parametr("@Imie", k.Imie), _
CustomerManager.vb:152:            DbHelper.Parametr("@Nazwisko", k.Nazwisko), _
CustomerManager.vb:153:            DbHelper.Parametr("@NazwaFirmy", PustyNaNull(k.NazwaFirmy)), _
CustomerManager.vb:154:            DbHelper.Parametr("@Email", k.Email), _
CustomerManager.vb:155:            DbHelper.Parametr("@Telefon", k.Telefon), _
CustomerManager.vb:156:            DbHelper.Parametr("@NumerDokumentu", PustyNaNull(k.NumerDokumentu)), _
CustomerManager.vb:157:            DbHelper.Parametr("@NIP", PustyNaNull(k.NIP)), _
CustomerManager.vb:158:            DbHelper.Parametr("@Typ", k.Typ), _
CustomerManager.vb:159:            DbHelper.Parametr("@ProgRabatu", k.ProgRabatu)}
CustomerManager.vb:160:    End Function
CustomerManager.vb:161:
CustomerManager.vb:162:    Private Function MapujKlienta(ByVal r As DataRow) As Klient
CustomerManager.vb:163:        Dim k As New Klient()
CustomerManager.vb:164:        k.Id = r("Id")
CustomerManager.vb:165:        k.Imie = r("Imie").ToString()
CustomerManager.vb:166:        k.Nazwisko = r("Nazwisko").ToString()
CustomerManager.vb:167:        k.NazwaFirmy = If(IsDBNull(r("NazwaFirmy")), Nothing, r("NazwaFirmy").ToString())
CustomerManager.vb:168:        k.Email = r("Email").ToString()
CustomerManager.vb:169:        k.Telefon = r("Telefon").ToString()
CustomerManager.vb:170:        k.NumerDokumentu = If(IsDBNull(r("NumerDokumentu")), Nothing, r("NumerDokumentu").ToString())
CustomerManager.vb:171:        k.NIP = If(IsDBNull(r("NIP")), Nothing, r("NIP").ToString())
CustomerManager.vb:172:        k.Typ = r("Typ").ToString()
CustomerManager.vb:173:        k.ProgRabatu = r("ProgRabatu")
CustomerManager.vb:174:        k.Zablokowany = r("Zablokowany")
CustomerManager.vb:175:        k.PowodBlokady = If(IsDBNull(r("PowodBlokady")), Nothing, r("PowodBlokady").ToString())
CustomerManager.vb:176:        k.DataRejestracji = r("DataRejestracji")
CustomerManager.vb:177:        Return k
CustomerManager.vb:178:    End Function
CustomerManager.vb:179:
CustomerManager.vb:180:    Private Function PustyNaNull(ByVal wartosc As String) As String
CustomerManager.vb:181:        If wartosc Is Nothing OrElse wartosc.Trim() = "" Then Return Nothing
CustomerManager.vb:182:        Return wartosc.Trim()
CustomerManager.vb:183:    End Function
CustomerManager.vb:184:
CustomerManager.vb:185:    Private Sub ZapiszDziennik(ByVal operacja As String, ByVal szczegoly As String)
CustomerManager.vb:186:        Try
CustomerManager.vb:187:            DbHelper.WykonajPolecenie( _
CustomerManager.vb:188:                "INSERT INTO DziennikOperacji (Operacja, Szczegoly) VALUES (@Operacja, @Szczegoly)", _
CustomerManager.vb:189:                DbHelper.Parametr("@Operacja", operacja), _
CustomerManager.vb:190:                DbHelper.Parametr("@Szczegoly", szczegoly))
CustomerManager.vb:191:        Catch ex As SqlException
CustomerManager.vb:192:            Log.Error("Nie udalo sie zapisac dziennika operacji", ex)
CustomerManager.vb:193:        End Try
CustomerManager.vb:194:    End Sub
CustomerManager.vb:195:
CustomerManager.vb:196:End Class
EquipmentManager.vb:1:' Modul: EquipmentManager - katalog sprzetu, statusy, dostepnosc | Autor: zespol IT | 2014-03-11
EquipmentManager.vb:2:' TODO 2016: przeniesc sprawdzanie dostepnosci do ReservationManager
EquipmentManager.vb:3:' Historia:
EquipmentManager.vb:4:'   2016-11-08 PT  SprawdzDostepnosc przez ReservationDAL.PobierzKolizje (procedura w bazie)
EquipmentManager.vb:5:'   2018-01-15 PT  status X, lista bez wycofanych
EquipmentManager.vb:6:
EquipmentManager.vb:7:Imports System.Data
EquipmentManager.vb:8:Imports System.Data.SqlClient
EquipmentManager.vb:9:Imports Wypozyczalnia.Core.Model
EquipmentManager.vb:10:Imports Wypozyczalnia.Core.DAL
EquipmentManager.vb:11:
EquipmentManager.vb:12:Public Class EquipmentManager
EquipmentManager.vb:13:
EquipmentManager.vb:14:    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(EquipmentManager))
EquipmentManager.vb:15:
EquipmentManager.vb:16:    Private Shared ReadOnly DozwoloneStatusy() As String = {"D", "W", "S", "X"}
EquipmentManager.vb:17:    Private Shared ReadOnly DozwoloneKategorie() As String = {"BUD", "OGR", "IMP", "TRN"}
EquipmentManager.vb:18:
EquipmentManager.vb:19:    Public Function PobierzSprzet(ByVal sprzetId As Integer) As Sprzet
EquipmentManager.vb:20:        If sprzetId <= 0 Then
EquipmentManager.vb:21:            Return Nothing
EquipmentManager.vb:22:        End If
EquipmentManager.vb:23:        Dim dal As New EquipmentDAL()
EquipmentManager.vb:24:        Return dal.Pobierz(sprzetId)
EquipmentManager.vb:25:    End Function
EquipmentManager.vb:26:
EquipmentManager.vb:27:    Public Function PobierzPoNumerze(ByVal numerInwentarzowy As String) As Sprzet
EquipmentManager.vb:28:        If String.IsNullOrEmpty(numerInwentarzowy) Then
EquipmentManager.vb:29:            Return Nothing
EquipmentManager.vb:30:        End If
EquipmentManager.vb:31:        Return New EquipmentDAL().PobierzPoNumerze(numerInwentarzowy.Trim().ToUpper())
EquipmentManager.vb:32:    End Function
EquipmentManager.vb:33:
EquipmentManager.vb:34:    ' kategoria pusta = wszystkie; wycofane (X) pomijane w DAL
EquipmentManager.vb:35:    Public Function ListaWKategorii(ByVal kategoria As String) As List(Of Sprzet)
EquipmentManager.vb:36:        Dim dal As New EquipmentDAL()
EquipmentManager.vb:37:        If kategoria Is Nothing Then
EquipmentManager.vb:38:            kategoria = ""
EquipmentManager.vb:39:        End If
EquipmentManager.vb:40:        Return dal.Lista(kategoria.Trim())
EquipmentManager.vb:41:    End Function
EquipmentManager.vb:42:
EquipmentManager.vb:43:    ' dostepnosc w terminie: kolizje z rezerwacjami, otwartymi wypozyczeniami i statusem sprzetu
EquipmentManager.vb:44:    Public Function SprawdzDostepnosc(ByVal sprzetId As Integer, ByVal dataOd As Date, ByVal dataDo As Date) As Boolean
EquipmentManager.vb:45:        Dim rezerwacje As New ReservationDAL()
EquipmentManager.vb:46:        Dim kolizje As DataTable = rezerwacje.PobierzKolizje(sprzetId, dataOd, dataDo)
EquipmentManager.vb:47:        If kolizje.Rows.Count > 0 Then
EquipmentManager.vb:48:            Log.Debug("Sprzet " & sprzetId & " niedostepny " & dataOd.ToString("yyyy-MM-dd HH:mm") & " - " & _
EquipmentManager.vb:49:                      dataDo.ToString("yyyy-MM-dd HH:mm") & ", kolizji: " & kolizje.Rows.Count)
EquipmentManager.vb:50:        End If
EquipmentManager.vb:51:        Return kolizje.Rows.Count = 0
EquipmentManager.vb:52:    End Function
EquipmentManager.vb:53:
EquipmentManager.vb:54:    ' sprzet z kategorii wolny w terminie (uzywane przy rezerwacji i wydaniu bez rezerwacji)
EquipmentManager.vb:55:    Public Function DostepneWKategorii(ByVal kategoria As String, ByVal dataOd As Date, ByVal dataDo As Date) As List(Of Sprzet)
EquipmentManager.vb:56:        Dim wynik As New List(Of Sprzet)()
EquipmentManager.vb:57:        For Each s As Sprzet In ListaWKategorii(kategoria)
EquipmentManager.vb:58:            If s.Status = "S" Then
EquipmentManager.vb:59:                Continue For
EquipmentManager.vb:60:            End If
EquipmentManager.vb:61:            If SprawdzDostepnosc(s.Id, dataOd, dataDo) Then
EquipmentManager.vb:62:                wynik.Add(s)
EquipmentManager.vb:63:            End If
EquipmentManager.vb:64:        Next
EquipmentManager.vb:65:        Return wynik
EquipmentManager.vb:66:    End Function
EquipmentManager.vb:67:
EquipmentManager.vb:68:    Public Sub ZmienStatus(ByVal sprzetId As Integer, ByVal nowyStatus As String)
EquipmentManager.vb:69:        If nowyStatus Is Nothing OrElse Array.IndexOf(DozwoloneStatusy, nowyStatus) < 0 Then
EquipmentManager.vb:70:            Throw New ArgumentException("Nieznany status sprzetu: " & nowyStatus, "nowyStatus")
EquipmentManager.vb:71:        End If
EquipmentManager.vb:72:        Dim dal As New EquipmentDAL()
EquipmentManager.vb:73:        Dim ile As Integer = dal.ZmienStatus(sprzetId, nowyStatus)
EquipmentManager.vb:74:        If ile = 0 Then
EquipmentManager.vb:75:            Log.Warn("ZmienStatus: brak sprzetu o Id " & sprzetId)
EquipmentManager.vb:76:        Else
EquipmentManager.vb:77:            Log.Info("Sprzet " & sprzetId & " -> status " & nowyStatus)
EquipmentManager.vb:78:        End If
EquipmentManager.vb:79:    End Sub
EquipmentManager.vb:80:
EquipmentManager.vb:81:    Public Function Zapisz(ByVal sprzet As Sprzet) As Integer
EquipmentManager.vb:82:        If sprzet Is Nothing Then
EquipmentManager.vb:83:            Throw New ArgumentNullException("sprzet")
EquipmentManager.vb:84:        End If
EquipmentManager.vb:85:        Waliduj(sprzet)
EquipmentManager.vb:86:        Dim dal As New EquipmentDAL()
EquipmentManager.vb:87:        If dal.CzyNumerZajety(sprzet.NumerInwentarzowy, sprzet.Id) Then
EquipmentManager.vb:88:            Throw New BladBiznesowyException("Numer inwentarzowy " & sprzet.NumerInwentarzowy & " jest juz uzyty", "SPRZET-NUMER")
EquipmentManager.vb:89:        End If
EquipmentManager.vb:90:        If sprzet.Id = 0 Then
EquipmentManager.vb:91:            sprzet.Id = dal.Dodaj(sprzet)
EquipmentManager.vb:92:            Log.Info("Dodano sprzet " & sprzet.Id & " (" & sprzet.NumerInwentarzowy & ")")
EquipmentManager.vb:93:        Else
EquipmentManager.vb:94:            Dim ile As Integer = dal.Aktualizuj(sprzet)
EquipmentManager.vb:95:            If ile = 0 Then
EquipmentManager.vb:96:                Throw New BladBiznesowyException("Sprzet " & sprzet.Id & " nie istnieje")
EquipmentManager.vb:97:            End If
EquipmentManager.vb:98:            Log.Info("Zaktualizowano sprzet " & sprzet.Id)
EquipmentManager.vb:99:        End If
EquipmentManager.vb:100:        Return sprzet.Id
EquipmentManager.vb:101:    End Function
EquipmentManager.vb:102:
EquipmentManager.vb:103:    Public Sub WyslijDoSerwisu(ByVal sprzetId As Integer)
EquipmentManager.vb:104:        Dim s As Sprzet = PobierzSprzet(sprzetId)
EquipmentManager.vb:105:        If s Is Nothing Then
EquipmentManager.vb:106:            Throw New BladBiznesowyException("Nieznany sprzet: " & sprzetId)
EquipmentManager.vb:107:        End If
EquipmentManager.vb:108:        If s.Status = "W" Then
EquipmentManager.vb:109:            Throw New BladBiznesowyException("Sprzet jest wypozyczony - najpierw zwrot")
EquipmentManager.vb:110:        End If
EquipmentManager.vb:111:        ZmienStatus(sprzetId, "S")
EquipmentManager.vb:112:    End Sub
EquipmentManager.vb:113:
EquipmentManager.vb:114:    Public Sub Wycofaj(ByVal sprzetId As Integer)
EquipmentManager.vb:115:        Dim s As Sprzet = PobierzSprzet(sprzetId)
EquipmentManager.vb:116:        If s Is Nothing Then
EquipmentManager.vb:117:            Throw New BladBiznesowyException("Nieznany sprzet: " & sprzetId)
EquipmentManager.vb:118:        End If
EquipmentManager.vb:119:        If s.Status = "W" Then
EquipmentManager.vb:120:            Throw New BladBiznesowyException("Nie mozna wycofac wypozyczonego sprzetu")
EquipmentManager.vb:121:        End If
EquipmentManager.vb:122:        ZmienStatus(sprzetId, "X")
EquipmentManager.vb:123:    End Sub
EquipmentManager.vb:124:
EquipmentManager.vb:125:    Public Sub PrzywrocZSerwisu(ByVal sprzetId As Integer)
EquipmentManager.vb:126:        Dim s As Sprzet = PobierzSprzet(sprzetId)
EquipmentManager.vb:127:        If s Is Nothing Then
EquipmentManager.vb:128:            Throw New BladBiznesowyException("Nieznany sprzet: " & sprzetId)
EquipmentManager.vb:129:        End If
EquipmentManager.vb:130:        If s.Status <> "S" Then
EquipmentManager.vb:131:            Throw New BladBiznesowyException("Sprzet nie jest w serwisie")
EquipmentManager.vb:132:        End If
EquipmentManager.vb:133:        ZmienStatus(sprzetId, "D")
EquipmentManager.vb:134:    End Sub
EquipmentManager.vb:135:
EquipmentManager.vb:136:    Public Sub ZmienStawke(ByVal sprzetId As Integer, ByVal stawkaDobowa As Decimal, ByVal kaucja As Decimal)
EquipmentManager.vb:137:        If stawkaDobowa <= 0 Then
EquipmentManager.vb:138:            Throw New BladBiznesowyException("Stawka dobowa musi byc dodatnia")
EquipmentManager.vb:139:        End If
EquipmentManager.vb:140:        If kaucja < 0 Then
EquipmentManager.vb:141:            Throw New BladBiznesowyException("Kaucja nie moze byc ujemna")
EquipmentManager.vb:142:        End If
EquipmentManager.vb:143:        Dim dal As New EquipmentDAL()
EquipmentManager.vb:144:        If dal.ZmienStawke(sprzetId, stawkaDobowa, kaucja) = 0 Then
EquipmentManager.vb:145:            Throw New BladBiznesowyException("Nieznany sprzet: " & sprzetId)
EquipmentManager.vb:146:        End If
EquipmentManager.vb:147:        Log.Info("Sprzet " & sprzetId & ": stawka " & stawkaDobowa & ", kaucja " & kaucja)
EquipmentManager.vb:148:    End Sub
EquipmentManager.vb:149:
EquipmentManager.vb:150:    Public Function Szukaj(ByVal fraza As String) As List(Of Sprzet)
EquipmentManager.vb:151:        If fraza Is Nothing OrElse fraza.Trim().Length < 2 Then
EquipmentManager.vb:152:            Return New List(Of Sprzet)()
EquipmentManager.vb:153:        End If
EquipmentManager.vb:154:        Return New EquipmentDAL().Szukaj(fraza)
EquipmentManager.vb:155:    End Function
EquipmentManager.vb:156:
EquipmentManager.vb:157:    ' kolumny: Id, Klient, DataWydania, PlanowanyZwrot, DataZwrotu, Status
EquipmentManager.vb:158:    Public Function HistoriaWypozyczen(ByVal sprzetId As Integer) As DataTable
EquipmentManager.vb:159:        Return New EquipmentDAL().HistoriaWypozyczen(sprzetId)
EquipmentManager.vb:160:    End Function
EquipmentManager.vb:161:
EquipmentManager.vb:162:    ' kolejne kolizje w zadanym okresie (do podgladu na stronie rezerwacji)
EquipmentManager.vb:163:    Public Function KolizjeWTerminie(ByVal sprzetId As Integer, ByVal dataOd As Date, ByVal dataDo As Date) As DataTable
EquipmentManager.vb:164:        Return New ReservationDAL().PobierzKolizje(sprzetId, dataOd, dataDo)
EquipmentManager.vb:165:    End Function
EquipmentManager.vb:166:
EquipmentManager.vb:167:    Public Function CzyMoznaZmienicKategorie(ByVal sprzetId As Integer) As Boolean
EquipmentManager.vb:168:        Dim dal As New EquipmentDAL()
EquipmentManager.vb:169:        Return Not dal.MaOtwarteWypozyczenie(sprzetId)
EquipmentManager.vb:170:    End Function
EquipmentManager.vb:171:
EquipmentManager.vb:172:    Public Function Kategorie() As Dictionary(Of String, String)
EquipmentManager.vb:173:        Dim wynik As New Dictionary(Of String, String)()
EquipmentManager.vb:174:        wynik.Add("BUD", "Budowlany")
EquipmentManager.vb:175:        wynik.Add("OGR", "Ogrodowy")
EquipmentManager.vb:176:        wynik.Add("IMP", "Imprezowy")
EquipmentManager.vb:177:        wynik.Add("TRN", "Transportowy")
EquipmentManager.vb:178:        Return wynik
EquipmentManager.vb:179:    End Function
EquipmentManager.vb:180:
EquipmentManager.vb:181:    Public Function Podsumowanie() As DataTable
EquipmentManager.vb:182:        Return New EquipmentDAL().LiczbaWKategoriach()
EquipmentManager.vb:183:    End Function
EquipmentManager.vb:184:
EquipmentManager.vb:185:    Private Sub Waliduj(ByVal sprzet As Sprzet)
EquipmentManager.vb:186:        If String.IsNullOrEmpty(sprzet.NumerInwentarzowy) Then
EquipmentManager.vb:187:            Throw New BladBiznesowyException("Numer inwentarzowy jest wymagany")
EquipmentManager.vb:188:        End If
EquipmentManager.vb:189:        If String.IsNullOrEmpty(sprzet.Nazwa) Then
EquipmentManager.vb:190:            Throw New BladBiznesowyException("Nazwa sprzetu jest wymagana")
EquipmentManager.vb:191:        End If
EquipmentManager.vb:192:        sprzet.NumerInwentarzowy = sprzet.NumerInwentarzowy.Trim().ToUpper()
EquipmentManager.vb:193:        If sprzet.NumerInwentarzowy.Length > 20 Then
EquipmentManager.vb:194:            Throw New BladBiznesowyException("Numer inwentarzowy za dlugi (max 20)")
EquipmentManager.vb:195:        End If
EquipmentManager.vb:196:        If Array.IndexOf(DozwoloneKategorie, sprzet.Kategoria) < 0 Then
EquipmentManager.vb:197:            Throw New BladBiznesowyException("Nieznana kategoria: " & sprzet.Kategoria)
EquipmentManager.vb:198:        End If
EquipmentManager.vb:199:        If Not sprzet.NumerInwentarzowy.StartsWith(sprzet.Kategoria & "-") Then
EquipmentManager.vb:200:            Throw New BladBiznesowyException("Numer inwentarzowy musi zaczynac sie od kodu kategorii")
EquipmentManager.vb:201:        End If
EquipmentManager.vb:202:        If sprzet.StawkaDobowa <= 0 Then
EquipmentManager.vb:203:            Throw New BladBiznesowyException("Stawka dobowa musi byc dodatnia")
EquipmentManager.vb:204:        End If
EquipmentManager.vb:205:        If sprzet.Kaucja < 0 Then
EquipmentManager.vb:206:            Throw New BladBiznesowyException("Kaucja nie moze byc ujemna")
EquipmentManager.vb:207:        End If
EquipmentManager.vb:208:        If String.IsNullOrEmpty(sprzet.Status) Then
EquipmentManager.vb:209:            sprzet.Status = "D"
EquipmentManager.vb:210:        End If
EquipmentManager.vb:211:        If Array.IndexOf(DozwoloneStatusy, sprzet.Status) < 0 Then
EquipmentManager.vb:212:            Throw New BladBiznesowyException("Nieznany status: " & sprzet.Status)
EquipmentManager.vb:213:        End If
EquipmentManager.vb:214:    End Sub
EquipmentManager.vb:215:
EquipmentManager.vb:216:End Class
InvoiceManager.vb:1:' Modul: InvoiceManager - wystawianie faktur za wypozyczenia, VAT | Autor: zespol IT | 2014-05-19
InvoiceManager.vb:2:' Historia:
InvoiceManager.vb:3:'   2014-11-27 JK  numeracja w bazie, pozycje zapisywane osobno
InvoiceManager.vb:4:'   2016-02-09 MW  stawka VAT z Web.config zamiast stalej w kodzie
InvoiceManager.vb:5:'   2019-03-21 PT  pozycja "Szkody" gdy kaucja nie pokryla rozliczenia
InvoiceManager.vb:6:
InvoiceManager.vb:7:Imports System.Data
InvoiceManager.vb:8:Imports System.Configuration
InvoiceManager.vb:9:Imports System.Globalization
InvoiceManager.vb:10:Imports Wypozyczalnia.Core.Model
InvoiceManager.vb:11:Imports Wypozyczalnia.Core.DAL
InvoiceManager.vb:12:
InvoiceManager.vb:13:Public Class InvoiceManager
InvoiceManager.vb:14:
InvoiceManager.vb:15:    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(InvoiceManager))
InvoiceManager.vb:16:
InvoiceManager.vb:17:    Private Const DomyslnaStawkaVat As String = "0,23"
InvoiceManager.vb:18:
InvoiceManager.vb:19:    Public Function WystawFaktureZaWypozyczenie(ByVal wypozyczenieId As Integer) As Faktura
InvoiceManager.vb:20:        Dim rental As New RentalManager()
InvoiceManager.vb:21:        Dim w As Wypozyczenie = rental.PobierzWypozyczenie(wypozyczenieId)
InvoiceManager.vb:22:        If w Is Nothing Then
InvoiceManager.vb:23:            Throw New BladBiznesowyException("Nie znaleziono wypozyczenia " & wypozyczenieId, "WYP_BRAK")
InvoiceManager.vb:24:        End If
InvoiceManager.vb:25:        If w.Status <> "Z" Then
InvoiceManager.vb:26:            Throw New BladBiznesowyException("Wypozyczenie nie zostalo jeszcze rozliczone (brak zwrotu)", "WYP_OTWARTE")
InvoiceManager.vb:27:        End If
InvoiceManager.vb:28:
InvoiceManager.vb:29:        Dim dal As New InvoiceDAL()
InvoiceManager.vb:30:        If dal.CzyIstniejeDlaWypozyczenia(wypozyczenieId) Then
InvoiceManager.vb:31:            Throw New BladBiznesowyException("Faktura za to wypozyczenie juz istnieje", "FV_ISTNIEJE")
InvoiceManager.vb:32:        End If
InvoiceManager.vb:33:
InvoiceManager.vb:34:        Dim klient As Klient = New CustomerManager().PobierzKlienta(w.KlientId)
InvoiceManager.vb:35:        If klient Is Nothing Then
InvoiceManager.vb:36:            Throw New BladBiznesowyException("Nieznany klient: " & w.KlientId, "KLIENT_BRAK")
InvoiceManager.vb:37:        End If
InvoiceManager.vb:38:        Dim sprzet As Sprzet = New EquipmentManager().PobierzSprzet(w.SprzetId)
InvoiceManager.vb:39:        If sprzet Is Nothing Then
InvoiceManager.vb:40:            Throw New BladBiznesowyException("Nieznany sprzet: " & w.SprzetId, "SPRZET_BRAK")
InvoiceManager.vb:41:        End If
InvoiceManager.vb:42:
InvoiceManager.vb:43:        Dim stawkaVat As Decimal = PobierzStawkeVat()
InvoiceManager.vb:44:        Dim f As New Faktura()
InvoiceManager.vb:45:        f.KlientId = w.KlientId
InvoiceManager.vb:46:        f.WypozyczenieId = w.Id
InvoiceManager.vb:47:        f.DataWystawienia = Date.Now
InvoiceManager.vb:48:
InvoiceManager.vb:49:        Dim doby As Integer = LiczbaDobDoOpisu(w.DataWydania, w.PlanowanyZwrot)
InvoiceManager.vb:50:        DodajPozycje(f, "Wynajem: " & sprzet.Nazwa & " (" & doby & " " & OdmianaDoby(doby) & ")", 1D, w.Oplata, stawkaVat)
InvoiceManager.vb:51:
InvoiceManager.vb:52:        If w.Kara > 0D Then
InvoiceManager.vb:53:            DodajPozycje(f, "Kara za spóźniony zwrot", 1D, w.Kara, stawkaVat)
InvoiceManager.vb:54:        End If
InvoiceManager.vb:55:
InvoiceManager.vb:56:        ' szkody potracone z kaucji; na fakture tylko gdy kaucja zostala wyczerpana
InvoiceManager.vb:57:        Dim szkody As Decimal = w.KaucjaPobrana - w.Kara - w.KaucjaZwrocona
InvoiceManager.vb:58:        If w.KaucjaZwrocona = 0D AndAlso szkody > 0D Then
InvoiceManager.vb:59:            DodajPozycje(f, "Szkody", 1D, szkody, stawkaVat)
InvoiceManager.vb:60:        End If
InvoiceManager.vb:61:
InvoiceManager.vb:62:        PrzeliczSumy(f)
InvoiceManager.vb:63:
InvoiceManager.vb:64:        dal.ZapiszNaglowek(f)
InvoiceManager.vb:65:        For Each p As PozycjaFaktury In f.Pozycje
InvoiceManager.vb:66:            dal.ZapiszPozycje(f.Id, p)
InvoiceManager.vb:67:        Next
InvoiceManager.vb:68:
InvoiceManager.vb:69:        Log.Info("Wystawiono fakture " & f.Numer & " za wypozyczenie " & w.Id & ", brutto " & f.Brutto.ToString("0.00"))
InvoiceManager.vb:70:        Return f
InvoiceManager.vb:71:    End Function
InvoiceManager.vb:72:
InvoiceManager.vb:73:    Public Function PobierzFakture(ByVal fakturaId As Integer) As Faktura
InvoiceManager.vb:74:        Dim dal As New InvoiceDAL()
InvoiceManager.vb:75:        Dim wiersz As DataRow = dal.Pobierz(fakturaId)
InvoiceManager.vb:76:        If wiersz Is Nothing Then
InvoiceManager.vb:77:            Return Nothing
InvoiceManager.vb:78:        End If
InvoiceManager.vb:79:        Dim f As Faktura = Faktura.ZWiersza(wiersz)
InvoiceManager.vb:80:        WczytajPozycje(dal, f)
InvoiceManager.vb:81:        Return f
InvoiceManager.vb:82:    End Function
InvoiceManager.vb:83:
InvoiceManager.vb:84:    Public Function PobierzFaktureDlaWypozyczenia(ByVal wypozyczenieId As Integer) As Faktura
InvoiceManager.vb:85:        Dim dal As New InvoiceDAL()
InvoiceManager.vb:86:        Dim wiersz As DataRow = dal.PobierzDlaWypozyczenia(wypozyczenieId)
InvoiceManager.vb:87:        If wiersz Is Nothing Then
InvoiceManager.vb:88:            Return Nothing
InvoiceManager.vb:89:        End If
InvoiceManager.vb:90:        Dim f As Faktura = Faktura.ZWiersza(wiersz)
InvoiceManager.vb:91:        WczytajPozycje(dal, f)
InvoiceManager.vb:92:        Return f
InvoiceManager.vb:93:    End Function
InvoiceManager.vb:94:
InvoiceManager.vb:95:    Public Sub OznaczOplacona(ByVal fakturaId As Integer, ByVal idTransakcji As String)
InvoiceManager.vb:96:        If String.IsNullOrEmpty(idTransakcji) Then
InvoiceManager.vb:97:            Throw New ArgumentException("Brak identyfikatora transakcji", "idTransakcji")
InvoiceManager.vb:98:        End If
InvoiceManager.vb:99:        Dim f As Faktura = PobierzFakture(fakturaId)
InvoiceManager.vb:100:        If f Is Nothing Then
InvoiceManager.vb:101:            Throw New BladBiznesowyException("Nie znaleziono faktury " & fakturaId, "FV_BRAK")
InvoiceManager.vb:102:        End If
InvoiceManager.vb:103:        If f.Status = "O" Then
InvoiceManager.vb:104:            Log.Warn("Faktura " & f.Numer & " juz oplacona (transakcja " & f.IdTransakcji & "), pomijam " & idTransakcji)
InvoiceManager.vb:105:            Return
InvoiceManager.vb:106:        End If
InvoiceManager.vb:107:        Dim ile As Integer = New InvoiceDAL().UstawOplacona(fakturaId, idTransakcji)
InvoiceManager.vb:108:        If ile = 0 Then
InvoiceManager.vb:109:            Throw New BladBiznesowyException("Nie udalo sie zmienic statusu faktury " & f.Numer, "FV_STATUS")
InvoiceManager.vb:110:        End If
InvoiceManager.vb:111:        Log.Info("Faktura " & f.Numer & " oplacona, transakcja " & idTransakcji)
InvoiceManager.vb:112:    End Sub
InvoiceManager.vb:113:
InvoiceManager.vb:114:    ' kolumny: Id, Numer, KlientId, Klient, WypozyczenieId, DataWystawienia, Netto, Vat, Brutto, Status
InvoiceManager.vb:115:    Public Function FakturyZDnia(ByVal dzien As Date) As DataTable
InvoiceManager.vb:116:        Return New InvoiceDAL().FakturyZDnia(dzien)
InvoiceManager.vb:117:    End Function
InvoiceManager.vb:118:
InvoiceManager.vb:119:    Public Function FakturyKlienta(ByVal klientId As Integer) As DataTable
InvoiceManager.vb:120:        Return New InvoiceDAL().FakturyKlienta(klientId)
InvoiceManager.vb:121:    End Function
InvoiceManager.vb:122:
InvoiceManager.vb:123:    Public Function Nieoplacone() As DataTable
InvoiceManager.vb:124:        Return New InvoiceDAL().Nieoplacone()
InvoiceManager.vb:125:    End Function
InvoiceManager.vb:126:
InvoiceManager.vb:127:    Public Function PobierzPoNumerze(ByVal numer As String) As Faktura
InvoiceManager.vb:128:        If String.IsNullOrEmpty(numer) Then Return Nothing
InvoiceManager.vb:129:        Dim dal As New InvoiceDAL()
InvoiceManager.vb:130:        Dim wiersz As DataRow = dal.PobierzPoNumerze(numer.Trim().ToUpper())
InvoiceManager.vb:131:        If wiersz Is Nothing Then
InvoiceManager.vb:132:            Return Nothing
InvoiceManager.vb:133:        End If
InvoiceManager.vb:134:        Dim f As Faktura = Faktura.ZWiersza(wiersz)
InvoiceManager.vb:135:        WczytajPozycje(dal, f)
InvoiceManager.vb:136:        Return f
InvoiceManager.vb:137:    End Function
InvoiceManager.vb:138:
InvoiceManager.vb:139:    Public Function CzyMoznaWystawic(ByVal wypozyczenieId As Integer) As Boolean
InvoiceManager.vb:140:        Dim w As Wypozyczenie = New RentalManager().PobierzWypozyczenie(wypozyczenieId)
InvoiceManager.vb:141:        If w Is Nothing Then Return False
InvoiceManager.vb:142:        If w.Status <> "Z" Then Return False
InvoiceManager.vb:143:        Return Not New InvoiceDAL().CzyIstniejeDlaWypozyczenia(wypozyczenieId)
InvoiceManager.vb:144:    End Function
InvoiceManager.vb:145:
InvoiceManager.vb:146:    ' anulowanie = usuniecie nieoplaconej faktury (numer zostaje "dziura" w numeracji)
InvoiceManager.vb:147:    Public Sub AnulujNieoplacona(ByVal fakturaId As Integer)
InvoiceManager.vb:148:        Dim f As Faktura = PobierzFakture(fakturaId)
InvoiceManager.vb:149:        If f Is Nothing Then
InvoiceManager.vb:150:            Throw New BladBiznesowyException("Nie znaleziono faktury " & fakturaId, "FV_BRAK")
InvoiceManager.vb:151:        End If
InvoiceManager.vb:152:        If f.Status = "O" Then
InvoiceManager.vb:153:            Throw New BladBiznesowyException("Faktura " & f.Numer & " jest oplacona - anulowanie niemozliwe", "FV_STATUS")
InvoiceManager.vb:154:        End If
InvoiceManager.vb:155:        Dim dal As New InvoiceDAL()
InvoiceManager.vb:156:        dal.Usun(fakturaId)
InvoiceManager.vb:157:        Log.Warn("Usunieto nieoplacona fakture " & f.Numer)
InvoiceManager.vb:158:    End Sub
InvoiceManager.vb:159:
InvoiceManager.vb:160:    Public Function TerminPlatnosci(ByVal f As Faktura) As Date
InvoiceManager.vb:161:        Dim klient As Klient = New CustomerManager().PobierzKlienta(f.KlientId)
InvoiceManager.vb:162:        If klient IsNot Nothing AndAlso klient.Typ = "F" Then
InvoiceManager.vb:163:            Return f.DataWystawienia.Date.AddDays(14)
InvoiceManager.vb:164:        End If
InvoiceManager.vb:165:        Return f.DataWystawienia.Date.AddDays(7)
InvoiceManager.vb:166:    End Function
InvoiceManager.vb:167:
InvoiceManager.vb:168:    Public Function ZestawienieMiesiaca(ByVal rok As Integer, ByVal miesiac As Integer) As DataTable
InvoiceManager.vb:169:        Return New InvoiceDAL().ZestawienieMiesiaca(rok, miesiac)
InvoiceManager.vb:170:    End Function
InvoiceManager.vb:171:
InvoiceManager.vb:172:    Public Function PodgladFaktury(ByVal wypozyczenieId As Integer) As Faktura
InvoiceManager.vb:173:        Dim w As Wypozyczenie = New RentalManager().PobierzWypozyczenie(wypozyczenieId)
InvoiceManager.vb:174:        If w Is Nothing OrElse w.Status <> "Z" Then
InvoiceManager.vb:175:            Return Nothing
InvoiceManager.vb:176:        End If
InvoiceManager.vb:177:        Dim sprzet As Sprzet = New EquipmentManager().PobierzSprzet(w.SprzetId)
InvoiceManager.vb:178:        Dim stawkaVat As Decimal = PobierzStawkeVat()
InvoiceManager.vb:179:        Dim f As New Faktura()
InvoiceManager.vb:180:        f.KlientId = w.KlientId
InvoiceManager.vb:181:        f.WypozyczenieId = w.Id
InvoiceManager.vb:182:        Dim doby As Integer = LiczbaDobDoOpisu(w.DataWydania, w.PlanowanyZwrot)
InvoiceManager.vb:183:        Dim nazwa As String = "?"
InvoiceManager.vb:184:        If sprzet IsNot Nothing Then nazwa = sprzet.Nazwa
InvoiceManager.vb:185:        DodajPozycje(f, "Wynajem: " & nazwa & " (" & doby & " " & OdmianaDoby(doby) & ")", 1D, w.Oplata, stawkaVat)
InvoiceManager.vb:186:        If w.Kara > 0D Then
InvoiceManager.vb:187:            DodajPozycje(f, "Kara za spóźniony zwrot", 1D, w.Kara, stawkaVat)
InvoiceManager.vb:188:        End If
InvoiceManager.vb:189:        PrzeliczSumy(f)
InvoiceManager.vb:190:        Return f
InvoiceManager.vb:191:    End Function
InvoiceManager.vb:192:
InvoiceManager.vb:193:    Private Sub DodajPozycje(ByVal f As Faktura, ByVal opis As String, ByVal ilosc As Decimal, ByVal cenaNetto As Decimal, ByVal stawkaVat As Decimal)
InvoiceManager.vb:194:        Dim p As New PozycjaFaktury()
InvoiceManager.vb:195:        p.Lp = f.Pozycje.Count + 1
InvoiceManager.vb:196:        p.Opis = opis
InvoiceManager.vb:197:        p.Ilosc = ilosc
InvoiceManager.vb:198:        p.CenaNetto = cenaNetto
InvoiceManager.vb:199:        p.StawkaVat = stawkaVat
InvoiceManager.vb:200:        p.WartoscNetto = Math.Round(ilosc * cenaNetto, 2)
InvoiceManager.vb:201:        f.Pozycje.Add(p)
InvoiceManager.vb:202:    End Sub
InvoiceManager.vb:203:
InvoiceManager.vb:204:    ' VAT liczony od kazdej pozycji osobno i sumowany
InvoiceManager.vb:205:    Private Sub PrzeliczSumy(ByVal f As Faktura)
InvoiceManager.vb:206:        Dim netto As Decimal = 0D
InvoiceManager.vb:207:        Dim vat As Decimal = 0D
InvoiceManager.vb:208:        For Each p As PozycjaFaktury In f.Pozycje
InvoiceManager.vb:209:            netto = netto + p.WartoscNetto
InvoiceManager.vb:210:            vat = vat + Math.Round(p.WartoscNetto * p.StawkaVat, 2)
InvoiceManager.vb:211:        Next
InvoiceManager.vb:212:        f.Netto = netto
InvoiceManager.vb:213:        f.Vat = vat
InvoiceManager.vb:214:        f.Brutto = f.Netto + f.Vat
InvoiceManager.vb:215:    End Sub
InvoiceManager.vb:216:
InvoiceManager.vb:217:    Private Sub WczytajPozycje(ByVal dal As InvoiceDAL, ByVal f As Faktura)
InvoiceManager.vb:218:        f.Pozycje.Clear()
InvoiceManager.vb:219:        For Each wiersz As DataRow In dal.PobierzPozycje(f.Id).Rows
InvoiceManager.vb:220:            f.Pozycje.Add(PozycjaFaktury.ZWiersza(wiersz))
InvoiceManager.vb:221:        Next
InvoiceManager.vb:222:    End Sub
InvoiceManager.vb:223:
InvoiceManager.vb:224:    Private Function PobierzStawkeVat() As Decimal
InvoiceManager.vb:225:        Dim tekst As String = ConfigurationManager.AppSettings("StawkaVat")
InvoiceManager.vb:226:        If String.IsNullOrEmpty(tekst) Then
InvoiceManager.vb:227:            Log.Warn("Brak klucza StawkaVat w konfiguracji, uzywam " & DomyslnaStawkaVat)
InvoiceManager.vb:228:            tekst = DomyslnaStawkaVat
InvoiceManager.vb:229:        End If
InvoiceManager.vb:230:        Return CDec(tekst)
InvoiceManager.vb:231:    End Function
InvoiceManager.vb:232:
InvoiceManager.vb:233:    Private Function LiczbaDobDoOpisu(ByVal dataOd As Date, ByVal dataDo As Date) As Integer
InvoiceManager.vb:234:        Dim n As Integer = dataDo.Subtract(dataOd).Days
InvoiceManager.vb:235:        If n < 1 Then n = 1
InvoiceManager.vb:236:        Return n
InvoiceManager.vb:237:    End Function
InvoiceManager.vb:238:
InvoiceManager.vb:239:    Private Function OdmianaDoby(ByVal n As Integer) As String
InvoiceManager.vb:240:        If n = 1 Then Return "doba"
InvoiceManager.vb:241:        Dim reszta10 As Integer = n Mod 10
InvoiceManager.vb:242:        Dim reszta100 As Integer = n Mod 100
InvoiceManager.vb:243:        If reszta10 >= 2 AndAlso reszta10 <= 4 AndAlso (reszta100 < 12 OrElse reszta100 > 14) Then
InvoiceManager.vb:244:            Return "doby"
InvoiceManager.vb:245:        End If
InvoiceManager.vb:246:        Return "dób"
InvoiceManager.vb:247:    End Function
InvoiceManager.vb:248:
InvoiceManager.vb:249:End Class
PricingManager.vb:1:Option Strict Off
PricingManager.vb:2:' Modul: PricingManager - oplata za wynajem, rabaty, taryfy | Autor: zespol IT | 2014-03-11
PricingManager.vb:3:' Historia:
PricingManager.vb:4:'   2015-02-04 MW: mnoznik weekendowy i rabat tygodniowy z tabeli Taryfy
PricingManager.vb:5:'   2016-11-08 JK: rabat stalego klienta wg progu
PricingManager.vb:6:'   2019-04-02 PT: SzczegolyOplaty dla kalkulatora w Cennik.aspx
PricingManager.vb:7:
PricingManager.vb:8:Imports System.Data
PricingManager.vb:9:Imports System.Text
PricingManager.vb:10:Imports System.Globalization
PricingManager.vb:11:Imports Wypozyczalnia.Core.Model
PricingManager.vb:12:Imports Wypozyczalnia.Core.DAL
PricingManager.vb:13:
PricingManager.vb:14:Public Class PricingManager
PricingManager.vb:15:
PricingManager.vb:16:    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(PricingManager))
PricingManager.vb:17:
PricingManager.vb:18:    Private Const MaksMnoznikWeekend As Decimal = 3D
PricingManager.vb:19:    Private Const MaksRabatTygodniowy As Decimal = 50D
PricingManager.vb:20:    Private Const MaksStawkaDobowa As Decimal = 10000D
PricingManager.vb:21:
PricingManager.vb:22:    Public Function LiczbaDob(ByVal dataOd As Date, ByVal dataDo As Date) As Integer
PricingManager.vb:23:        Dim minuty As Long = DateDiff(DateInterval.Minute, dataOd, dataDo)
PricingManager.vb:24:        If minuty <= 0 Then Throw New ArgumentException("Data zwrotu musi byc pozniejsza niz data wydania")
PricingManager.vb:25:        Dim doby As Integer = CInt(minuty \ 1440)
PricingManager.vb:26:        If minuty Mod 1440 > 60 Then doby = doby + 1   ' rozpoczeta doba liczona jako pelna (1 h tolerancji)
PricingManager.vb:27:        If doby = 0 Then doby = 1
PricingManager.vb:28:        Return doby
PricingManager.vb:29:    End Function
PricingManager.vb:30:
PricingManager.vb:31:    Public Function ObliczOplate(ByVal sprzetId As Integer, ByVal dataOd As Date, ByVal dataDo As Date, ByVal klientId As Integer) As Decimal
PricingManager.vb:32:        Dim sprzet As Sprzet = New EquipmentManager().PobierzSprzet(sprzetId)
PricingManager.vb:33:        If sprzet Is Nothing Then Throw New BladBiznesowyException("Nieznany sprzet: " & sprzetId)
PricingManager.vb:34:        Dim taryfa As Taryfa = New PricingDAL().PobierzTaryfe(sprzet.Kategoria, dataOd)
PricingManager.vb:35:        Dim doby As Integer = LiczbaDob(dataOd, dataDo)
PricingManager.vb:36:        Dim stawkaWeekend As Decimal = Math.Round(sprzet.StawkaDobowa * taryfa.MnoznikWeekend, 2)
PricingManager.vb:37:        Dim kwota As Decimal = 0
PricingManager.vb:38:        For k As Integer = 0 To doby - 1
PricingManager.vb:39:            Dim dzien As DayOfWeek = dataOd.AddDays(k).DayOfWeek
PricingManager.vb:40:            If dzien = DayOfWeek.Saturday Or dzien = DayOfWeek.Sunday Then
PricingManager.vb:41:                kwota = kwota + stawkaWeekend
PricingManager.vb:42:            Else
PricingManager.vb:43:                kwota = kwota + sprzet.StawkaDobowa
PricingManager.vb:44:            End If
PricingManager.vb:45:        Next
PricingManager.vb:46:        If doby \ 7 >= 1 Then
PricingManager.vb:47:            kwota = kwota - Math.Round(kwota * taryfa.RabatTygodniowyProc / 100D, 2)
PricingManager.vb:48:        End If
PricingManager.vb:49:        ' rabat 2,5% za kazdy prog stalego klienta (prog 1 = 2,5%)
PricingManager.vb:50:        Dim prog = New CustomerManager().ProgRabatu(klientId)
PricingManager.vb:51:        Dim rabatProc As Integer = RabatProgu(prog)
PricingManager.vb:52:        If rabatProc > 0 Then
PricingManager.vb:53:            kwota = Math.Round(kwota * (100 - rabatProc) / 100D, 2)
PricingManager.vb:54:        End If
PricingManager.vb:55:        Return kwota
PricingManager.vb:56:    End Function
PricingManager.vb:57:
PricingManager.vb:58:    Public Function RabatProgu(ByVal prog As Integer) As Integer
PricingManager.vb:59:        Return CInt(prog * 2.5)
PricingManager.vb:60:    End Function
PricingManager.vb:61:
PricingManager.vb:62:    Public Function ObliczOplateBezKlienta(ByVal sprzetId As Integer, ByVal dataOd As Date, ByVal dataDo As Date) As Decimal
PricingManager.vb:63:        Return ObliczOplate(sprzetId, dataOd, dataDo, 0)
PricingManager.vb:64:    End Function
PricingManager.vb:65:
PricingManager.vb:66:    Public Function LiczbaDobWeekendowych(ByVal dataOd As Date, ByVal dataDo As Date) As Integer
PricingManager.vb:67:        Dim doby As Integer = LiczbaDob(dataOd, dataDo)
PricingManager.vb:68:        Dim ile As Integer = 0
PricingManager.vb:69:        For k As Integer = 0 To doby - 1
PricingManager.vb:70:            If CzyWeekend(dataOd.AddDays(k)) Then ile = ile + 1
PricingManager.vb:71:        Next
PricingManager.vb:72:        Return ile
PricingManager.vb:73:    End Function
PricingManager.vb:74:
PricingManager.vb:75:    Public Function SzczegolyOplaty(ByVal sprzetId As Integer, ByVal dataOd As Date, ByVal dataDo As Date, ByVal klientId As Integer) As String
PricingManager.vb:76:        Dim sprzet As Sprzet = New EquipmentManager().PobierzSprzet(sprzetId)
PricingManager.vb:77:        If sprzet Is Nothing Then Return "Nieznany sprzet: " & sprzetId
PricingManager.vb:78:        Dim taryfa As Taryfa = PobierzTaryfeDlaSprzetu(sprzetId, dataOd)
PricingManager.vb:79:        Dim doby As Integer = LiczbaDob(dataOd, dataDo)
PricingManager.vb:80:        Dim weekendowe As Integer = LiczbaDobWeekendowych(dataOd, dataDo)
PricingManager.vb:81:        Dim prog As Integer = New CustomerManager().ProgRabatu(klientId)
PricingManager.vb:82:        Dim kultura As New CultureInfo("pl-PL")
PricingManager.vb:83:        Dim sb As New StringBuilder()
PricingManager.vb:84:        sb.Append(sprzet.Nazwa).Append(" (").Append(sprzet.NumerInwentarzowy).Append(")")
PricingManager.vb:85:        sb.Append(" | dob: ").Append(doby)
PricingManager.vb:86:        sb.Append(" (w tym weekendowych: ").Append(weekendowe).Append(")")
PricingManager.vb:87:        sb.Append(" | stawka: ").Append(sprzet.StawkaDobowa.ToString("N2", kultura))
PricingManager.vb:88:        sb.Append(" | weekend x").Append(taryfa.MnoznikWeekend.ToString(kultura))
PricingManager.vb:89:        If doby \ 7 >= 1 Then
PricingManager.vb:90:            sb.Append(" | rabat tygodniowy ").Append(taryfa.RabatTygodniowyProc.ToString(kultura)).Append("%")
PricingManager.vb:91:        End If
PricingManager.vb:92:        If prog > 0 Then
PricingManager.vb:93:            sb.Append(" | ").Append(OpisRabatu(prog))
PricingManager.vb:94:        End If
PricingManager.vb:95:        sb.Append(" | razem: ").Append(ObliczOplate(sprzetId, dataOd, dataDo, klientId).ToString("N2", kultura))
PricingManager.vb:96:        Return sb.ToString()
PricingManager.vb:97:    End Function
PricingManager.vb:98:
PricingManager.vb:99:    Public Function OpisRabatu(ByVal prog As Integer) As String
PricingManager.vb:100:        If prog <= 0 Then Return "brak rabatu"
PricingManager.vb:101:        Return "staly klient, prog " & prog & ": " & RabatProgu(prog) & "%"
PricingManager.vb:102:    End Function
PricingManager.vb:103:
PricingManager.vb:104:    Public Function PobierzTaryfeDlaSprzetu(ByVal sprzetId As Integer, ByVal naDzien As Date) As Taryfa
PricingManager.vb:105:        Dim sprzet As Sprzet = New EquipmentManager().PobierzSprzet(sprzetId)
PricingManager.vb:106:        If sprzet Is Nothing Then Throw New BladBiznesowyException("Nieznany sprzet: " & sprzetId)
PricingManager.vb:107:        Return New PricingDAL().PobierzTaryfe(sprzet.Kategoria, naDzien)
PricingManager.vb:108:    End Function
PricingManager.vb:109:
PricingManager.vb:110:    Public Function ListaTaryf() As DataTable
PricingManager.vb:111:        Return New PricingDAL().ListaTaryf()
PricingManager.vb:112:    End Function
PricingManager.vb:113:
PricingManager.vb:114:    Public Sub ZapiszTaryfe(ByVal t As Taryfa)
PricingManager.vb:115:        If t Is Nothing Then Throw New ArgumentNullException("t")
PricingManager.vb:116:        If Not Taryfa.CzyZnanaKategoria(t.Kategoria) Then
PricingManager.vb:117:            Throw New ArgumentException("Nieznana kategoria: " & t.Kategoria)
PricingManager.vb:118:        End If
PricingManager.vb:119:        If t.MnoznikWeekend < 1D Or t.MnoznikWeekend > MaksMnoznikWeekend Then
PricingManager.vb:120:            Throw New ArgumentException("Mnoznik weekendowy poza zakresem 1-" & MaksMnoznikWeekend)
PricingManager.vb:121:        End If
PricingManager.vb:122:        If t.RabatTygodniowyProc < 0D Or t.RabatTygodniowyProc > MaksRabatTygodniowy Then
PricingManager.vb:123:            Throw New ArgumentException("Rabat tygodniowy poza zakresem 0-" & MaksRabatTygodniowy)
PricingManager.vb:124:        End If
PricingManager.vb:125:        Dim dal As New PricingDAL()
PricingManager.vb:126:        dal.ZapiszTaryfe(t)
PricingManager.vb:127:        dal.ZapiszWDzienniku("TARYFA", t.ToString())
PricingManager.vb:128:        Log.Info("Zapisano taryfe " & t.ToString())
PricingManager.vb:129:    End Sub
PricingManager.vb:130:
PricingManager.vb:131:    Public Sub ZmienStawke(ByVal sprzetId As Integer, ByVal nowaStawka As Decimal)
PricingManager.vb:132:        If nowaStawka <= 0D Or nowaStawka > MaksStawkaDobowa Then
PricingManager.vb:133:            Throw New ArgumentException("Nieprawidlowa stawka dobowa: " & nowaStawka)
PricingManager.vb:134:        End If
PricingManager.vb:135:        Dim em As New EquipmentManager()
PricingManager.vb:136:        Dim sprzet As Sprzet = em.PobierzSprzet(sprzetId)
PricingManager.vb:137:        If sprzet Is Nothing Then Throw New BladBiznesowyException("Nieznany sprzet: " & sprzetId)
PricingManager.vb:138:        Dim stara As Decimal = sprzet.StawkaDobowa
PricingManager.vb:139:        sprzet.StawkaDobowa = nowaStawka
PricingManager.vb:140:        em.Zapisz(sprzet)
PricingManager.vb:141:        Call New PricingDAL().ZapiszWDzienniku("STAWKA", sprzet.NumerInwentarzowy & ": " & stara & " -> " & nowaStawka)
PricingManager.vb:142:        Log.Info("Zmiana stawki " & sprzet.NumerInwentarzowy & " z " & stara & " na " & nowaStawka)
PricingManager.vb:143:    End Sub
PricingManager.vb:144:
PricingManager.vb:145:    Public Function KaucjaDlaSprzetu(ByVal sprzetId As Integer) As Decimal
PricingManager.vb:146:        Dim sprzet As Sprzet = New EquipmentManager().PobierzSprzet(sprzetId)
PricingManager.vb:147:        If sprzet Is Nothing Then Return 0D
PricingManager.vb:148:        Return sprzet.Kaucja
PricingManager.vb:149:    End Function
PricingManager.vb:150:
PricingManager.vb:151:    Public Function PrzeliczRezerwacjeNowe() As Integer
PricingManager.vb:152:        Dim sql As String = "SELECT Id, KlientId, SprzetId, DataOd, DataDo, CenaSzacunkowa FROM Rezerwacje WHERE Status = 'N'"
PricingManager.vb:153:        Dim dt As DataTable = DbHelper.WykonajZapytanie(sql)
PricingManager.vb:154:        Dim zmienione As Integer = 0
PricingManager.vb:155:        For Each row As DataRow In dt.Rows
PricingManager.vb:156:            Try
PricingManager.vb:157:                Dim nowa As Decimal = ObliczOplate(row("SprzetId"), row("DataOd"), row("DataDo"), row("KlientId"))
PricingManager.vb:158:                If nowa <> row("CenaSzacunkowa") Then
PricingManager.vb:159:                    DbHelper.WykonajPolecenie("UPDATE Rezerwacje SET CenaSzacunkowa = @Cena WHERE Id = @Id",
PricingManager.vb:160:                        DbHelper.Parametr("@Cena", nowa),
PricingManager.vb:161:                        DbHelper.Parametr("@Id", row("Id")))
PricingManager.vb:162:                    zmienione = zmienione + 1
PricingManager.vb:163:                End If
PricingManager.vb:164:            Catch ex As BladBiznesowyException
PricingManager.vb:165:                Log.Warn("Nie przeliczono rezerwacji " & row("Id") & ": " & ex.Message)
PricingManager.vb:166:            End Try
PricingManager.vb:167:        Next
PricingManager.vb:168:        Log.Info("Przeliczono rezerwacje nowe, zmienionych: " & zmienione)
PricingManager.vb:169:        Return zmienione
PricingManager.vb:170:    End Function
PricingManager.vb:171:
PricingManager.vb:172:    Public Function CennikKategorii(ByVal kategoria As String, ByVal naDzien As Date) As DataTable
PricingManager.vb:173:        Dim wynik As New DataTable("Cennik")
PricingManager.vb:174:        wynik.Columns.Add("Id", GetType(Integer))
PricingManager.vb:175:        wynik.Columns.Add("Nazwa", GetType(String))
PricingManager.vb:176:        wynik.Columns.Add("StawkaDobowa", GetType(Decimal))
PricingManager.vb:177:        wynik.Columns.Add("StawkaWeekend", GetType(Decimal))
PricingManager.vb:178:        wynik.Columns.Add("Kaucja", GetType(Decimal))
PricingManager.vb:179:        Dim taryfa As Taryfa = New PricingDAL().PobierzTaryfe(kategoria, naDzien)
PricingManager.vb:180:        For Each s As Sprzet In New EquipmentManager().ListaWKategorii(kategoria)
PricingManager.vb:181:            Dim r As DataRow = wynik.NewRow()
PricingManager.vb:182:            r("Id") = s.Id
PricingManager.vb:183:            r("Nazwa") = s.Nazwa
PricingManager.vb:184:            r("StawkaDobowa") = s.StawkaDobowa
PricingManager.vb:185:            r("StawkaWeekend") = s.StawkaDobowa * taryfa.MnoznikWeekend
PricingManager.vb:186:            r("Kaucja") = s.Kaucja
PricingManager.vb:187:            wynik.Rows.Add(r)
PricingManager.vb:188:        Next
PricingManager.vb:189:        Return wynik
PricingManager.vb:190:    End Function
PricingManager.vb:191:
PricingManager.vb:192:    Public Function SprawdzTerminy(ByVal dataOd As Date, ByVal dataDo As Date) As String
PricingManager.vb:193:        If dataDo <= dataOd Then Return "Data zwrotu musi byc pozniejsza niz data wydania"
PricingManager.vb:194:        If dataOd < Date.Today.AddDays(-1) Then Return "Data wydania z przeszlosci"
PricingManager.vb:195:        If DateDiff(DateInterval.Day, dataOd, dataDo) > 90 Then Return "Maksymalny okres wynajmu to 90 dni"
PricingManager.vb:196:        Return ""
PricingManager.vb:197:    End Function
PricingManager.vb:198:
PricingManager.vb:199:    Public Function SymulujOplate(ByVal sprzetId As Integer, ByVal dataOd As Date, ByVal liczbaDob As Integer, ByVal klientId As Integer) As Decimal
PricingManager.vb:200:        If liczbaDob < 1 Then Throw New ArgumentException("Liczba dob musi byc dodatnia")
PricingManager.vb:201:        Return ObliczOplate(sprzetId, dataOd, dataOd.AddDays(liczbaDob), klientId)
PricingManager.vb:202:    End Function
PricingManager.vb:203:
PricingManager.vb:204:    Public Function TabelaOplat(ByVal sprzetId As Integer, ByVal dataOd As Date, ByVal klientId As Integer, ByVal maksDob As Integer) As DataTable
PricingManager.vb:205:        Dim wynik As New DataTable("TabelaOplat")
PricingManager.vb:206:        wynik.Columns.Add("Doby", GetType(Integer))
PricingManager.vb:207:        wynik.Columns.Add("DataDo", GetType(Date))
PricingManager.vb:208:        wynik.Columns.Add("Oplata", GetType(Decimal))
PricingManager.vb:209:        wynik.Columns.Add("ZaDobe", GetType(Decimal))
PricingManager.vb:210:        If maksDob < 1 Then maksDob = 1
PricingManager.vb:211:        If maksDob > 30 Then maksDob = 30
PricingManager.vb:212:        For d As Integer = 1 To maksDob
PricingManager.vb:213:            Dim r As DataRow = wynik.NewRow()
PricingManager.vb:214:            Dim kwota As Decimal = SymulujOplate(sprzetId, dataOd, d, klientId)
PricingManager.vb:215:            r("Doby") = d
PricingManager.vb:216:            r("DataDo") = dataOd.AddDays(d)
PricingManager.vb:217:            r("Oplata") = kwota
PricingManager.vb:218:            r("ZaDobe") = kwota / d
PricingManager.vb:219:            wynik.Rows.Add(r)
PricingManager.vb:220:        Next
PricingManager.vb:221:        Return wynik
PricingManager.vb:222:    End Function
PricingManager.vb:223:
PricingManager.vb:224:    Public Function TaryfyNaDzien(ByVal naDzien As Date) As List(Of Taryfa)
PricingManager.vb:225:        Return New PricingDAL().TaryfyNaDzien(naDzien)
PricingManager.vb:226:    End Function
PricingManager.vb:227:
PricingManager.vb:228:    Public Function HistoriaTaryf(ByVal kategoria As String) As String
PricingManager.vb:229:        Dim lista As List(Of Taryfa) = New PricingDAL().TaryfyKategorii(kategoria)
PricingManager.vb:230:        If lista.Count = 0 Then Return "Brak taryf dla kategorii " & kategoria
PricingManager.vb:231:        Dim sb As New StringBuilder()
PricingManager.vb:232:        sb.AppendLine(Taryfa.NazwaKategorii(kategoria) & ":")
PricingManager.vb:233:        Dim poprzednia As Taryfa = Nothing
PricingManager.vb:234:        For Each t As Taryfa In lista
PricingManager.vb:235:            sb.Append("  ").Append(t.ToString())
PricingManager.vb:236:            If poprzednia IsNot Nothing AndAlso poprzednia.MnoznikWeekend <> t.MnoznikWeekend Then
PricingManager.vb:237:                sb.Append(" [zmiana mnoznika: ").Append(t.MnoznikWeekend).Append(" -> ").Append(poprzednia.MnoznikWeekend).Append("]")
PricingManager.vb:238:            End If
PricingManager.vb:239:            sb.AppendLine()
PricingManager.vb:240:            poprzednia = t
PricingManager.vb:241:        Next
PricingManager.vb:242:        Return sb.ToString()
PricingManager.vb:243:    End Function
PricingManager.vb:244:
PricingManager.vb:245:    Public Function ZmienKaucje(ByVal sprzetId As Integer, ByVal nowaKaucja As Decimal) As Boolean
PricingManager.vb:246:        If nowaKaucja < 0D Then Throw New ArgumentException("Kaucja nie moze byc ujemna")
PricingManager.vb:247:        Dim dal As New PricingDAL()
PricingManager.vb:248:        Dim ile As Integer = dal.ZaktualizujKaucje(sprzetId, nowaKaucja)
PricingManager.vb:249:        If ile = 0 Then
PricingManager.vb:250:            Log.Warn("Nie zmieniono kaucji sprzetu " & sprzetId)
PricingManager.vb:251:            Return False
PricingManager.vb:252:        End If
PricingManager.vb:253:        dal.ZapiszWDzienniku("STAWKA", "kaucja sprzetu " & sprzetId & ": " & nowaKaucja)
PricingManager.vb:254:        Return True
PricingManager.vb:255:    End Function
PricingManager.vb:256:
PricingManager.vb:257:    Public Function BrakujaceTaryfy(ByVal naDzien As Date) As List(Of String)
PricingManager.vb:258:        Dim brak As New List(Of String)()
PricingManager.vb:259:        Dim dal As New PricingDAL()
PricingManager.vb:260:        For Each kategoria As String In Taryfa.KategorieZnane
PricingManager.vb:261:            Try
PricingManager.vb:262:                dal.PobierzTaryfe(kategoria, naDzien)
PricingManager.vb:263:            Catch ex As BladBiznesowyException
PricingManager.vb:264:                brak.Add(kategoria)
PricingManager.vb:265:            End Try
PricingManager.vb:266:        Next
PricingManager.vb:267:        If brak.Count > 0 Then Log.Warn("Brak taryf na " & naDzien.ToShortDateString() & ": " & String.Join(",", brak.ToArray()))
PricingManager.vb:268:        Return brak
PricingManager.vb:269:    End Function
PricingManager.vb:270:
PricingManager.vb:271:    Public Function ZestawienieKategorii(ByVal naDzien As Date) As DataTable
PricingManager.vb:272:        Dim wynik As New DataTable("Zestawienie")
PricingManager.vb:273:        wynik.Columns.Add("Kategoria", GetType(String))
PricingManager.vb:274:        wynik.Columns.Add("Nazwa", GetType(String))
PricingManager.vb:275:        wynik.Columns.Add("MnoznikWeekend", GetType(Decimal))
PricingManager.vb:276:        wynik.Columns.Add("RabatTygodniowyProc", GetType(Decimal))
PricingManager.vb:277:        wynik.Columns.Add("LiczbaSprzetu", GetType(Integer))
PricingManager.vb:278:        Dim em As New EquipmentManager()
PricingManager.vb:279:        For Each t As Taryfa In TaryfyNaDzien(naDzien)
PricingManager.vb:280:            Dim r As DataRow = wynik.NewRow()
PricingManager.vb:281:            r("Kategoria") = t.Kategoria
PricingManager.vb:282:            r("Nazwa") = Taryfa.NazwaKategorii(t.Kategoria)
PricingManager.vb:283:            r("MnoznikWeekend") = t.MnoznikWeekend
PricingManager.vb:284:            r("RabatTygodniowyProc") = t.RabatTygodniowyProc
PricingManager.vb:285:            r("LiczbaSprzetu") = em.ListaWKategorii(t.Kategoria).Count
PricingManager.vb:286:            wynik.Rows.Add(r)
PricingManager.vb:287:        Next
PricingManager.vb:288:        Return wynik
PricingManager.vb:289:    End Function
PricingManager.vb:290:
PricingManager.vb:291:    Public Function CzyCenaAktualna(ByVal rezerwacjaId As Integer) As Boolean
PricingManager.vb:292:        Dim dt As DataTable = DbHelper.WykonajZapytanie("SELECT KlientId, SprzetId, DataOd, DataDo, CenaSzacunkowa FROM Rezerwacje WHERE Id = @Id",
PricingManager.vb:293:            DbHelper.Parametr("@Id", rezerwacjaId))
PricingManager.vb:294:        If dt.Rows.Count = 0 Then Return False
PricingManager.vb:295:        Dim r As DataRow = dt.Rows(0)
PricingManager.vb:296:        Try
PricingManager.vb:297:            Return ObliczOplate(r("SprzetId"), r("DataOd"), r("DataDo"), r("KlientId")) = r("CenaSzacunkowa")
PricingManager.vb:298:        Catch ex As BladBiznesowyException
PricingManager.vb:299:            Log.Warn("Nie mozna sprawdzic ceny rezerwacji " & rezerwacjaId & ": " & ex.Message)
PricingManager.vb:300:            Return False
PricingManager.vb:301:        End Try
PricingManager.vb:302:    End Function
PricingManager.vb:303:
PricingManager.vb:304:    Private Function CzyWeekend(ByVal dzien As Date) As Boolean
PricingManager.vb:305:        Return dzien.DayOfWeek = DayOfWeek.Saturday Or dzien.DayOfWeek = DayOfWeek.Sunday
PricingManager.vb:306:    End Function
PricingManager.vb:307:
PricingManager.vb:308:End Class
RentalManager.vb:1:' Modul: RentalManager.vb | Autor: zespol IT | 2014-03-11
RentalManager.vb:2:' Wydania sprzetu, zwroty, kara za spoznienie, rozliczenie kaucji.
RentalManager.vb:3:' 2015-06-22 MW: zamowienie kuriera przy wydaniu
RentalManager.vb:4:' 2016-04-18 JK: kara liczona od planowanego zwrotu z godzina tolerancji
RentalManager.vb:5:
RentalManager.vb:6:Imports System.Data
RentalManager.vb:7:Imports Wypozyczalnia.Core.Model
RentalManager.vb:8:Imports Wypozyczalnia.Core.DAL
RentalManager.vb:9:Imports Wypozyczalnia.Core.Integracje
RentalManager.vb:10:
RentalManager.vb:11:Public Class RentalManager
RentalManager.vb:12:
RentalManager.vb:13:    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(RentalManager))
RentalManager.vb:14:
RentalManager.vb:15:    Public Function PobierzWypozyczenie(ByVal wypozyczenieId As Integer) As Wypozyczenie
RentalManager.vb:16:        If wypozyczenieId <= 0 Then Return Nothing
RentalManager.vb:17:        Return New RentalDAL().PobierzWypozyczenie(wypozyczenieId)
RentalManager.vb:18:    End Function
RentalManager.vb:19:
RentalManager.vb:20:    Public Function WydajSprzet(ByVal rezerwacjaId As Integer, ByVal klientId As Integer, ByVal sprzetId As Integer, ByVal planowanyZwrot As Date, ByVal adresDostawy As String) As Integer
RentalManager.vb:21:        Dim klienci As New CustomerManager()
RentalManager.vb:22:        If Not klienci.CzyMozeWypozyczac(klientId) Then
RentalManager.vb:23:            Log.Warn("Proba wydania dla zablokowanego klienta " & klientId)
RentalManager.vb:24:            Throw New BladBiznesowyException("Klient zablokowany")
RentalManager.vb:25:        End If
RentalManager.vb:26:
RentalManager.vb:27:        Dim sprzety As New EquipmentManager()
RentalManager.vb:28:        Dim s As Sprzet = sprzety.PobierzSprzet(sprzetId)
RentalManager.vb:29:        If s Is Nothing Then
RentalManager.vb:30:            Throw New BladBiznesowyException("Nieznany sprzet: " & sprzetId)
RentalManager.vb:31:        End If
RentalManager.vb:32:        If s.Status <> "D" Then
RentalManager.vb:33:            Throw New BladBiznesowyException("Sprzet " & s.NumerInwentarzowy & " nie jest dostepny (status " & s.Status & ")")
RentalManager.vb:34:        End If
RentalManager.vb:35:
RentalManager.vb:36:        Dim teraz As Date = Date.Now
RentalManager.vb:37:        If planowanyZwrot <= teraz Then
RentalManager.vb:38:            Throw New ArgumentException("Planowany zwrot musi byc pozniejszy niz data wydania")
RentalManager.vb:39:        End If
RentalManager.vb:40:
RentalManager.vb:41:        Dim rezerwacje As New ReservationManager()
RentalManager.vb:42:        If rezerwacjaId > 0 Then
RentalManager.vb:43:            Dim r As Rezerwacja = rezerwacje.PobierzRezerwacje(rezerwacjaId)
RentalManager.vb:44:            If r Is Nothing Then
RentalManager.vb:45:                Throw New BladBiznesowyException("Nie znaleziono rezerwacji " & rezerwacjaId)
RentalManager.vb:46:            End If
RentalManager.vb:47:            If r.KlientId <> klientId OrElse r.SprzetId <> sprzetId Then
RentalManager.vb:48:                Throw New BladBiznesowyException("Rezerwacja " & rezerwacjaId & " dotyczy innego klienta albo sprzetu")
RentalManager.vb:49:            End If
RentalManager.vb:50:            If r.Status <> "N" AndAlso r.Status <> "P" Then
RentalManager.vb:51:                Throw New BladBiznesowyException("Rezerwacja " & rezerwacjaId & " ma status " & r.Status)
RentalManager.vb:52:            End If
RentalManager.vb:53:        End If
RentalManager.vb:54:
RentalManager.vb:55:        Dim w As New Wypozyczenie()
RentalManager.vb:56:        w.RezerwacjaId = rezerwacjaId
RentalManager.vb:57:        w.KlientId = klientId
RentalManager.vb:58:        w.SprzetId = sprzetId
RentalManager.vb:59:        w.DataWydania = teraz
RentalManager.vb:60:        w.PlanowanyZwrot = planowanyZwrot
RentalManager.vb:61:        w.KaucjaPobrana = s.Kaucja
RentalManager.vb:62:        w.Status = "O"
RentalManager.vb:63:
RentalManager.vb:64:        Dim dal As New RentalDAL()
RentalManager.vb:65:        Dim noweId As Integer = dal.Wydaj(w)
RentalManager.vb:66:        Log.Info("Wydano sprzet " & sprzetId & " klientowi " & klientId & ", wypozyczenie " & noweId)
RentalManager.vb:67:
RentalManager.vb:68:        sprzety.ZmienStatus(sprzetId, "W")
RentalManager.vb:69:        If rezerwacjaId > 0 Then
RentalManager.vb:70:            rezerwacje.OznaczZrealizowana(rezerwacjaId)
RentalManager.vb:71:        End If
RentalManager.vb:72:
RentalManager.vb:73:        If Not String.IsNullOrEmpty(adresDostawy) AndAlso adresDostawy.Trim() <> "" Then
RentalManager.vb:74:            Dim klient As Klient = klienci.PobierzKlienta(klientId)
RentalManager.vb:75:            Dim telefon As String = ""
RentalManager.vb:76:            If klient IsNot Nothing Then telefon = klient.Telefon
RentalManager.vb:77:            Dim numer As String = New KurierClient().ZamowDostawe(noweId, adresDostawy.Trim(), telefon)
RentalManager.vb:78:            If numer <> "" Then
RentalManager.vb:79:                dal.UstawNumerPrzesylki(noweId, numer)
RentalManager.vb:80:                Log.Info("Zamowiono kuriera dla wypozyczenia " & noweId & ": " & numer)
RentalManager.vb:81:            Else
RentalManager.vb:82:                Log.Warn("Kurier nie przyjal zlecenia dla wypozyczenia " & noweId)
RentalManager.vb:83:            End If
RentalManager.vb:84:        End If
RentalManager.vb:85:
RentalManager.vb:86:        dal.ZapiszDziennik("WYDANIE", "Wypozyczenie " & noweId & ", sprzet " & s.NumerInwentarzowy & ", klient " & klientId)
RentalManager.vb:87:        Return noweId
RentalManager.vb:88:    End Function
RentalManager.vb:89:
RentalManager.vb:90:    Public Function ZarejestrujZwrot(ByVal wypozyczenieId As Integer, ByVal dataZwrotu As Date, ByVal szkody As Decimal) As Wypozyczenie
RentalManager.vb:91:        Dim w As Wypozyczenie = PobierzWypozyczenie(wypozyczenieId)
RentalManager.vb:92:        If w Is Nothing Then
RentalManager.vb:93:            Throw New BladBiznesowyException("Nie znaleziono wypozyczenia " & wypozyczenieId)
RentalManager.vb:94:        End If
RentalManager.vb:95:        If w.Status = "Z" Then
RentalManager.vb:96:            Throw New BladBiznesowyException("Wypozyczenie " & wypozyczenieId & " jest juz zamkniete")
RentalManager.vb:97:        End If
RentalManager.vb:98:        If szkody < 0 Then szkody = 0
RentalManager.vb:99:        If dataZwrotu < w.DataWydania Then
RentalManager.vb:100:            Throw New ArgumentException("Data zwrotu wczesniejsza niz data wydania")
RentalManager.vb:101:        End If
RentalManager.vb:102:
RentalManager.vb:103:        Dim sprzety As New EquipmentManager()
RentalManager.vb:104:        Dim s As Sprzet = sprzety.PobierzSprzet(w.SprzetId)
RentalManager.vb:105:        If s Is Nothing Then
RentalManager.vb:106:            Throw New BladBiznesowyException("Nieznany sprzet: " & w.SprzetId)
RentalManager.vb:107:        End If
RentalManager.vb:108:
RentalManager.vb:109:        Dim oplata As Decimal = New PricingManager().ObliczOplate(w.SprzetId, w.DataWydania, w.PlanowanyZwrot, w.KlientId)
RentalManager.vb:110:        Dim kara As Decimal = ObliczKare(w.PlanowanyZwrot, dataZwrotu, s.StawkaDobowa)
RentalManager.vb:111:        Dim kaucjaZwrot As Decimal = w.KaucjaPobrana - kara - szkody
RentalManager.vb:112:        If kaucjaZwrot < 0 Then kaucjaZwrot = 0
RentalManager.vb:113:        If kara > 0 Then
RentalManager.vb:114:            Log.Info("Spozniony zwrot wypozyczenia " & wypozyczenieId & ", plan " & w.PlanowanyZwrot.ToString("yyyy-MM-dd HH:mm") &
RentalManager.vb:115:                ", zwrot " & dataZwrotu.ToString("yyyy-MM-dd HH:mm"))
RentalManager.vb:116:        End If
RentalManager.vb:117:        If kara + szkody > w.KaucjaPobrana Then
RentalManager.vb:118:            Log.Warn("Kara i szkody przekraczaja kaucje dla wypozyczenia " & wypozyczenieId)
RentalManager.vb:119:        End If
RentalManager.vb:120:
RentalManager.vb:121:        w.DataZwrotu = dataZwrotu
RentalManager.vb:122:        w.Oplata = oplata
RentalManager.vb:123:        w.Kara = kara
RentalManager.vb:124:        w.KaucjaZwrocona = kaucjaZwrot
RentalManager.vb:125:        w.Status = "Z"
RentalManager.vb:126:
RentalManager.vb:127:        Dim dal As New RentalDAL()
RentalManager.vb:128:        dal.ZapiszZwrot(w)
RentalManager.vb:129:        sprzety.ZmienStatus(s.Id, "D")
RentalManager.vb:130:
RentalManager.vb:131:        Log.Info(String.Format("Zwrot wypozyczenia {0}: oplata {1}, kara {2}, szkody {3}, kaucja zwrocona {4}",
RentalManager.vb:132:            wypozyczenieId, oplata, kara, szkody, kaucjaZwrot))
RentalManager.vb:133:        dal.ZapiszDziennik("ZWROT", "Wypozyczenie " & wypozyczenieId & ", kara " & kara & ", szkody " & szkody)
RentalManager.vb:134:        Return w
RentalManager.vb:135:    End Function
RentalManager.vb:136:
RentalManager.vb:137:    Public Function ObliczKare(ByVal planowanyZwrot As Date, ByVal dataZwrotu As Date, ByVal stawkaDobowa As Decimal) As Decimal
RentalManager.vb:138:        Dim minuty As Long = DateDiff(DateInterval.Minute, planowanyZwrot, dataZwrotu)
RentalManager.vb:139:        If minuty <= 60 Then Return 0D
RentalManager.vb:140:        Dim dobySpoznienia As Long = (minuty - 60 + 1439) \ 1440
RentalManager.vb:141:        ' kara = 100% stawki dobowej za kazda dobe spoznienia
RentalManager.vb:142:        Return Math.Round(dobySpoznienia * stawkaDobowa * 1.5D, 2)
RentalManager.vb:143:    End Function
RentalManager.vb:144:
RentalManager.vb:145:    Public Function Otwarte() As DataTable
RentalManager.vb:146:        Return New RentalDAL().Otwarte()
RentalManager.vb:147:    End Function
RentalManager.vb:148:
RentalManager.vb:149:    Public Function WypozyczeniaZDnia(ByVal dzien As Date) As DataTable
RentalManager.vb:150:        Return New RentalDAL().WypozyczeniaZDnia(dzien)
RentalManager.vb:151:    End Function
RentalManager.vb:152:
RentalManager.vb:153:    Public Function WypozyczeniaKlienta(ByVal klientId As Integer) As DataTable
RentalManager.vb:154:        Return New RentalDAL().WypozyczeniaKlienta(klientId)
RentalManager.vb:155:    End Function
RentalManager.vb:156:
RentalManager.vb:157:    Public Function OtwarteKlienta(ByVal klientId As Integer) As List(Of Wypozyczenie)
RentalManager.vb:158:        Return New RentalDAL().OtwarteKlienta(klientId)
RentalManager.vb:159:    End Function
RentalManager.vb:160:
RentalManager.vb:161:    Public Function CzyKlientMaZaleglosci(ByVal klientId As Integer) As Boolean
RentalManager.vb:162:        Dim teraz As Date = Date.Now
RentalManager.vb:163:        For Each w As Wypozyczenie In OtwarteKlienta(klientId)
RentalManager.vb:164:            If w.CzyPoTerminie(teraz) Then
RentalManager.vb:165:                Log.Debug("Klient " & klientId & " ma zalegle wypozyczenie " & w.Id)
RentalManager.vb:166:                Return True
RentalManager.vb:167:            End If
RentalManager.vb:168:        Next
RentalManager.vb:169:        Return False
RentalManager.vb:170:    End Function
RentalManager.vb:171:
RentalManager.vb:172:    Public Function HistoriaSprzetu(ByVal sprzetId As Integer, ByVal ile As Integer) As List(Of Wypozyczenie)
RentalManager.vb:173:        If ile <= 0 Then ile = 10
RentalManager.vb:174:        If ile > 100 Then ile = 100
RentalManager.vb:175:        Return New RentalDAL().OstatnieDlaSprzetu(sprzetId, ile)
RentalManager.vb:176:    End Function
RentalManager.vb:177:
RentalManager.vb:178:    Public Function SzacowanaKara(ByVal wypozyczenieId As Integer, ByVal naDzien As Date) As Decimal
RentalManager.vb:179:        Dim w As Wypozyczenie = PobierzWypozyczenie(wypozyczenieId)
RentalManager.vb:180:        If w Is Nothing OrElse Not w.CzyOtwarte Then Return 0D
RentalManager.vb:181:        Dim s As Sprzet = New EquipmentManager().PobierzSprzet(w.SprzetId)
RentalManager.vb:182:        If s Is Nothing Then Return 0D
RentalManager.vb:183:        Return ObliczKare(w.PlanowanyZwrot, naDzien, s.StawkaDobowa)
RentalManager.vb:184:    End Function
RentalManager.vb:185:
RentalManager.vb:186:    Public Function PoTerminie(ByVal naDzien As Date) As DataTable
RentalManager.vb:187:        Dim tabela As DataTable = New RentalDAL().PoTerminie(naDzien)
RentalManager.vb:188:        tabela.Columns.Add("SzacowanaKara", GetType(Decimal))
RentalManager.vb:189:        Dim sprzety As New EquipmentManager()
RentalManager.vb:190:        For Each wiersz As DataRow In tabela.Rows
RentalManager.vb:191:            Dim s As Sprzet = sprzety.PobierzSprzet(CInt(wiersz("SprzetId")))
RentalManager.vb:192:            If s Is Nothing Then
RentalManager.vb:193:                wiersz("SzacowanaKara") = 0D
RentalManager.vb:194:            Else
RentalManager.vb:195:                wiersz("SzacowanaKara") = ObliczKare(CDate(wiersz("PlanowanyZwrot")), naDzien, s.StawkaDobowa)
RentalManager.vb:196:            End If
RentalManager.vb:197:        Next
RentalManager.vb:198:        Return tabela
RentalManager.vb:199:    End Function
RentalManager.vb:200:
RentalManager.vb:201:    Public Function StatusDostawy(ByVal wypozyczenieId As Integer) As String
RentalManager.vb:202:        Dim w As Wypozyczenie = PobierzWypozyczenie(wypozyczenieId)
RentalManager.vb:203:        If w Is Nothing Then Return ""
RentalManager.vb:204:        If String.IsNullOrEmpty(w.NumerPrzesylki) Then Return "BRAK"
RentalManager.vb:205:        Try
RentalManager.vb:206:            Return New KurierClient().StatusPrzesylki(w.NumerPrzesylki)
RentalManager.vb:207:        Catch ex As Exception
RentalManager.vb:208:            Log.Error("Status przesylki " & w.NumerPrzesylki & " niedostepny", ex)
RentalManager.vb:209:            Return "NIEZNANY"
RentalManager.vb:210:        End Try
RentalManager.vb:211:    End Function
RentalManager.vb:212:
RentalManager.vb:213:    Public Function PonowZamowienieKuriera(ByVal wypozyczenieId As Integer, ByVal adresDostawy As String) As String
RentalManager.vb:214:        Dim w As Wypozyczenie = PobierzWypozyczenie(wypozyczenieId)
RentalManager.vb:215:        If w Is Nothing Then
RentalManager.vb:216:            Throw New BladBiznesowyException("Nie znaleziono wypozyczenia " & wypozyczenieId)
RentalManager.vb:217:        End If
RentalManager.vb:218:        If Not w.CzyOtwarte Then
RentalManager.vb:219:            Throw New BladBiznesowyException("Wypozyczenie " & wypozyczenieId & " jest zamkniete")
RentalManager.vb:220:        End If
RentalManager.vb:221:        If Not String.IsNullOrEmpty(w.NumerPrzesylki) Then
RentalManager.vb:222:            Return w.NumerPrzesylki
RentalManager.vb:223:        End If
RentalManager.vb:224:        Dim klient As Klient = New CustomerManager().PobierzKlienta(w.KlientId)
RentalManager.vb:225:        Dim telefon As String = ""
RentalManager.vb:226:        If klient IsNot Nothing Then telefon = klient.Telefon
RentalManager.vb:227:        Dim numer As String = New KurierClient().ZamowDostawe(wypozyczenieId, adresDostawy, telefon)
RentalManager.vb:228:        If numer <> "" Then
RentalManager.vb:229:            Dim dal As New RentalDAL()
RentalManager.vb:230:            dal.UstawNumerPrzesylki(wypozyczenieId, numer)
RentalManager.vb:231:            dal.ZapiszDziennik("KURIER", "Wypozyczenie " & wypozyczenieId & ", przesylka " & numer)
RentalManager.vb:232:        End If
RentalManager.vb:233:        Return numer
RentalManager.vb:234:    End Function
RentalManager.vb:235:
RentalManager.vb:236:    Public Function CzySprzetWydany(ByVal sprzetId As Integer) As Boolean
RentalManager.vb:237:        Return New RentalDAL().CzyOtwarteDlaSprzetu(sprzetId)
RentalManager.vb:238:    End Function
RentalManager.vb:239:
RentalManager.vb:240:    Public Function WydajZRezerwacji(ByVal rezerwacjaId As Integer, ByVal adresDostawy As String) As Integer
RentalManager.vb:241:        Dim r As Rezerwacja = New ReservationManager().PobierzRezerwacje(rezerwacjaId)
RentalManager.vb:242:        If r Is Nothing Then
RentalManager.vb:243:            Throw New BladBiznesowyException("Nie znaleziono rezerwacji " & rezerwacjaId)
RentalManager.vb:244:        End If
RentalManager.vb:245:        Return WydajSprzet(rezerwacjaId, r.KlientId, r.SprzetId, r.DataDo, adresDostawy)
RentalManager.vb:246:    End Function
RentalManager.vb:247:
RentalManager.vb:248:    Public Sub PrzedluzWypozyczenie(ByVal wypozyczenieId As Integer, ByVal nowyTermin As Date)
RentalManager.vb:249:        Dim w As Wypozyczenie = PobierzWypozyczenie(wypozyczenieId)
RentalManager.vb:250:        If w Is Nothing Then
RentalManager.vb:251:            Throw New BladBiznesowyException("Nie znaleziono wypozyczenia " & wypozyczenieId)
RentalManager.vb:252:        End If
RentalManager.vb:253:        If Not w.CzyOtwarte Then
RentalManager.vb:254:            Throw New BladBiznesowyException("Wypozyczenie " & wypozyczenieId & " jest zamkniete")
RentalManager.vb:255:        End If
RentalManager.vb:256:        If nowyTermin <= w.PlanowanyZwrot Then
RentalManager.vb:257:            Throw New ArgumentException("Nowy termin musi byc pozniejszy niz obecny planowany zwrot")
RentalManager.vb:258:        End If
RentalManager.vb:259:        If Not New CustomerManager().CzyMozeWypozyczac(w.KlientId) Then
RentalManager.vb:260:            Throw New BladBiznesowyException("Klient zablokowany")
RentalManager.vb:261:        End If
RentalManager.vb:262:        If Not New EquipmentManager().SprawdzDostepnosc(w.SprzetId, w.PlanowanyZwrot, nowyTermin) Then
RentalManager.vb:263:            Throw New BladBiznesowyException("Sprzet zarezerwowany w wybranym terminie - przedluzenie niemozliwe")
RentalManager.vb:264:        End If
RentalManager.vb:265:        Dim dal As New RentalDAL()
RentalManager.vb:266:        dal.ZmienPlanowanyZwrot(wypozyczenieId, nowyTermin)
RentalManager.vb:267:        dal.ZapiszDziennik("PRZEDLUZENIE", "Wypozyczenie " & wypozyczenieId & " do " & nowyTermin.ToString("yyyy-MM-dd HH:mm"))
RentalManager.vb:268:        Log.Info("Przedluzono wypozyczenie " & wypozyczenieId & " do " & nowyTermin.ToString("yyyy-MM-dd HH:mm"))
RentalManager.vb:269:    End Sub
RentalManager.vb:270:
RentalManager.vb:271:    Public Function DoZwrotuWCiagu(ByVal godzin As Integer) As List(Of Wypozyczenie)
RentalManager.vb:272:        Dim teraz As Date = Date.Now
RentalManager.vb:273:        Return New RentalDAL().DoZwrotuWOkresie(teraz, teraz.AddHours(godzin))
RentalManager.vb:274:    End Function
RentalManager.vb:275:
RentalManager.vb:276:    Public Function PodsumowanieDnia(ByVal dzien As Date) As String
RentalManager.vb:277:        Dim tabela As DataTable = WypozyczeniaZDnia(dzien)
RentalManager.vb:278:        Dim wydania As Integer = 0
RentalManager.vb:279:        Dim zwroty As Integer = 0
RentalManager.vb:280:        Dim sumaOplat As Decimal = 0
RentalManager.vb:281:        Dim sumaKar As Decimal = 0
RentalManager.vb:282:        For Each wiersz As DataRow In tabela.Rows
RentalManager.vb:283:            Dim wydano As Date = CDate(wiersz("DataWydania"))
RentalManager.vb:284:            If wydano.Date = dzien.Date Then wydania = wydania + 1
RentalManager.vb:285:            If Not IsDBNull(wiersz("DataZwrotu")) AndAlso CDate(wiersz("DataZwrotu")).Date = dzien.Date Then
RentalManager.vb:286:                zwroty = zwroty + 1
RentalManager.vb:287:                If Not IsDBNull(wiersz("Oplata")) Then sumaOplat = sumaOplat + CDec(wiersz("Oplata"))
RentalManager.vb:288:                If Not IsDBNull(wiersz("Kara")) Then sumaKar = sumaKar + CDec(wiersz("Kara"))
RentalManager.vb:289:            End If
RentalManager.vb:290:        Next
RentalManager.vb:291:        Return String.Format("{0:yyyy-MM-dd}: wydania {1}, zwroty {2}, oplaty {3:N2}, kary {4:N2}",
RentalManager.vb:292:            dzien, wydania, zwroty, sumaOplat, sumaKar)
RentalManager.vb:293:    End Function
RentalManager.vb:294:
RentalManager.vb:295:    Public Function Przypomnienia(ByVal godzin As Integer) As List(Of String)
RentalManager.vb:296:        Dim wynik As New List(Of String)
RentalManager.vb:297:        Dim klienci As New CustomerManager()
RentalManager.vb:298:        For Each w As Wypozyczenie In DoZwrotuWCiagu(godzin)
RentalManager.vb:299:            Dim k As Klient = klienci.PobierzKlienta(w.KlientId)
RentalManager.vb:300:            If k Is Nothing Then Continue For
RentalManager.vb:301:            wynik.Add(String.Format("{0};{1};{2};{3:yyyy-MM-dd HH:mm}", w.Id, k.Email, k.Telefon, w.PlanowanyZwrot))
RentalManager.vb:302:        Next
RentalManager.vb:303:        Return wynik
RentalManager.vb:304:    End Function
RentalManager.vb:305:
RentalManager.vb:306:    Public Function ZamknijBezZwrotu(ByVal wypozyczenieId As Integer, ByVal powod As String) As Wypozyczenie
RentalManager.vb:307:        Dim w As Wypozyczenie = PobierzWypozyczenie(wypozyczenieId)
RentalManager.vb:308:        If w Is Nothing Then
RentalManager.vb:309:            Throw New BladBiznesowyException("Nie znaleziono wypozyczenia " & wypozyczenieId)
RentalManager.vb:310:        End If
RentalManager.vb:311:        If w.Status = "Z" Then
RentalManager.vb:312:            Throw New BladBiznesowyException("Wypozyczenie " & wypozyczenieId & " jest juz zamkniete")
RentalManager.vb:313:        End If
RentalManager.vb:314:        If String.IsNullOrEmpty(powod) Then
RentalManager.vb:315:            Throw New BladBiznesowyException("Podaj powod zamkniecia bez zwrotu")
RentalManager.vb:316:        End If
RentalManager.vb:317:        Dim teraz As Date = Date.Now
RentalManager.vb:318:        Dim s As Sprzet = New EquipmentManager().PobierzSprzet(w.SprzetId)
RentalManager.vb:319:        w.DataZwrotu = teraz
RentalManager.vb:320:        w.Oplata = New PricingManager().ObliczOplate(w.SprzetId, w.DataWydania, w.PlanowanyZwrot, w.KlientId)
RentalManager.vb:321:        w.Kara = ObliczKare(w.PlanowanyZwrot, teraz, s.StawkaDobowa)
RentalManager.vb:322:        w.KaucjaZwrocona = 0
RentalManager.vb:323:        w.Status = "Z"
RentalManager.vb:324:        Dim dal As New RentalDAL()
RentalManager.vb:325:        dal.ZapiszZwrot(w)
RentalManager.vb:326:        New EquipmentManager().ZmienStatus(w.SprzetId, "X")
RentalManager.vb:327:        dal.ZapiszDziennik("UTRATA", "Wypozyczenie " & wypozyczenieId & ": " & powod)
RentalManager.vb:328:        Log.Warn("Zamknieto bez zwrotu wypozyczenie " & wypozyczenieId & ": " & powod)
RentalManager.vb:329:        Return w
RentalManager.vb:330:    End Function
RentalManager.vb:331:
RentalManager.vb:332:End Class
ReservationManager.vb:1:' Modul: ReservationManager - rezerwacje sprzetu na termin | Autor: zespol IT | 2014-03-11
ReservationManager.vb:2:' Historia:
ReservationManager.vb:3:'   2014-09-22 JK  cena szacunkowa liczona przez PricingManager przy zapisie
ReservationManager.vb:4:'   2016-11-08 PT  dostepnosc przez EquipmentManager.SprawdzDostepnosc
ReservationManager.vb:5:'   2019-02-20 MW  anulowanie zrealizowanej rezerwacji zablokowane
ReservationManager.vb:6:
ReservationManager.vb:7:Imports System.Data
ReservationManager.vb:8:Imports System.Data.SqlClient
ReservationManager.vb:9:Imports Wypozyczalnia.Core.Model
ReservationManager.vb:10:Imports Wypozyczalnia.Core.DAL
ReservationManager.vb:11:
ReservationManager.vb:12:Public Class ReservationManager
ReservationManager.vb:13:
ReservationManager.vb:14:    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(ReservationManager))
ReservationManager.vb:15:
ReservationManager.vb:16:    Private Const MaksDlugoscUwag As Integer = 500
ReservationManager.vb:17:    Private Const MaksDniRezerwacji As Integer = 60
ReservationManager.vb:18:
ReservationManager.vb:19:    Public Function UtworzRezerwacje(ByVal klientId As Integer, ByVal sprzetId As Integer, ByVal dataOd As Date, ByVal dataDo As Date, ByVal uwagi As String) As Integer
ReservationManager.vb:20:        If dataDo <= dataOd Then
ReservationManager.vb:21:            Throw New ArgumentException("Data zakonczenia musi byc pozniejsza niz data rozpoczecia")
ReservationManager.vb:22:        End If
ReservationManager.vb:23:        If dataDo.Subtract(dataOd).TotalDays > MaksDniRezerwacji Then
ReservationManager.vb:24:            Throw New ArgumentException("Rezerwacja moze obejmowac maksymalnie " & MaksDniRezerwacji & " dni")
ReservationManager.vb:25:        End If
ReservationManager.vb:26:
ReservationManager.vb:27:        Dim klienci As New CustomerManager()
ReservationManager.vb:28:        If Not klienci.CzyMozeWypozyczac(klientId) Then
ReservationManager.vb:29:            Log.Warn("Proba rezerwacji przez zablokowanego klienta " & klientId)
ReservationManager.vb:30:            Throw New BladBiznesowyException("Klient zablokowany")
ReservationManager.vb:31:        End If
ReservationManager.vb:32:
ReservationManager.vb:33:        Dim sprzetMgr As New EquipmentManager()
ReservationManager.vb:34:        If Not sprzetMgr.SprawdzDostepnosc(sprzetId, dataOd, dataDo) Then
ReservationManager.vb:35:            Throw New BladBiznesowyException("Sprzet niedostepny w wybranym terminie")
ReservationManager.vb:36:        End If
ReservationManager.vb:37:
ReservationManager.vb:38:        Dim cennik As New PricingManager()
ReservationManager.vb:39:        Dim cena As Decimal = cennik.ObliczOplate(sprzetId, dataOd, dataDo, klientId)
ReservationManager.vb:40:
ReservationManager.vb:41:        Dim r As New Rezerwacja()
ReservationManager.vb:42:        r.KlientId = klientId
ReservationManager.vb:43:        r.SprzetId = sprzetId
ReservationManager.vb:44:        r.DataOd = dataOd
ReservationManager.vb:45:        r.DataDo = dataDo
ReservationManager.vb:46:        r.Status = "N"
ReservationManager.vb:47:        r.CenaSzacunkowa = cena
ReservationManager.vb:48:        r.Uwagi = PrzytnijUwagi(uwagi)
ReservationManager.vb:49:
ReservationManager.vb:50:        Dim dal As New ReservationDAL()
ReservationManager.vb:51:        Dim noweId As Integer = dal.Zapisz(r)
ReservationManager.vb:52:        Log.Info("Rezerwacja " & noweId & ": klient " & klientId & ", sprzet " & sprzetId & ", " & _
ReservationManager.vb:53:                 dataOd.ToString("yyyy-MM-dd HH:mm") & " - " & dataDo.ToString("yyyy-MM-dd HH:mm") & ", cena " & cena)
ReservationManager.vb:54:        Return noweId
ReservationManager.vb:55:    End Function
ReservationManager.vb:56:
ReservationManager.vb:57:    Public Function PobierzRezerwacje(ByVal rezerwacjaId As Integer) As Rezerwacja
ReservationManager.vb:58:        Dim dal As New ReservationDAL()
ReservationManager.vb:59:        Dim wiersz As DataRow = dal.Pobierz(rezerwacjaId)
ReservationManager.vb:60:        If wiersz Is Nothing Then
ReservationManager.vb:61:            Return Nothing
ReservationManager.vb:62:        End If
ReservationManager.vb:63:        Return Mapuj(wiersz)
ReservationManager.vb:64:    End Function
ReservationManager.vb:65:
ReservationManager.vb:66:    ' kolumny: Id, SprzetId, Sprzet, DataOd, DataDo, Status, CenaSzacunkowa
ReservationManager.vb:67:    Public Function RezerwacjeKlienta(ByVal klientId As Integer) As DataTable
ReservationManager.vb:68:        Dim dal As New ReservationDAL()
ReservationManager.vb:69:        Return dal.RezerwacjeKlienta(klientId)
ReservationManager.vb:70:    End Function
ReservationManager.vb:71:
ReservationManager.vb:72:    Public Function AktywneRezerwacjeKlienta(ByVal klientId As Integer) As List(Of Rezerwacja)
ReservationManager.vb:73:        Dim wynik As New List(Of Rezerwacja)()
ReservationManager.vb:74:        Dim tabela As DataTable = RezerwacjeKlienta(klientId)
ReservationManager.vb:75:        For Each wiersz As DataRow In tabela.Rows
ReservationManager.vb:76:            Dim status As String = CStr(wiersz("Status"))
ReservationManager.vb:77:            If status = "N" OrElse status = "P" Then
ReservationManager.vb:78:                Dim r As New Rezerwacja()
ReservationManager.vb:79:                r.Id = CInt(wiersz("Id"))
ReservationManager.vb:80:                r.KlientId = klientId
ReservationManager.vb:81:                r.SprzetId = CInt(wiersz("SprzetId"))
ReservationManager.vb:82:                r.DataOd = CDate(wiersz("DataOd"))
ReservationManager.vb:83:                r.DataDo = CDate(wiersz("DataDo"))
ReservationManager.vb:84:                r.Status = status
ReservationManager.vb:85:                r.CenaSzacunkowa = CDec(wiersz("CenaSzacunkowa"))
ReservationManager.vb:86:                wynik.Add(r)
ReservationManager.vb:87:            End If
ReservationManager.vb:88:        Next
ReservationManager.vb:89:        Return wynik
ReservationManager.vb:90:    End Function
ReservationManager.vb:91:
ReservationManager.vb:92:    Public Function RezerwacjeSprzetu(ByVal sprzetId As Integer) As DataTable
ReservationManager.vb:93:        Return New ReservationDAL().RezerwacjeSprzetu(sprzetId, Date.Today)
ReservationManager.vb:94:    End Function
ReservationManager.vb:95:
ReservationManager.vb:96:    Public Function RezerwacjeWOkresie(ByVal dataOd As Date, ByVal dataDo As Date) As DataTable
ReservationManager.vb:97:        If dataDo <= dataOd Then
ReservationManager.vb:98:            Throw New ArgumentException("Niepoprawny okres")
ReservationManager.vb:99:        End If
ReservationManager.vb:100:        Return New ReservationDAL().RezerwacjeWOkresie(dataOd, dataDo)
ReservationManager.vb:101:    End Function
ReservationManager.vb:102:
ReservationManager.vb:103:    Public Sub Potwierdz(ByVal rezerwacjaId As Integer)
ReservationManager.vb:104:        Dim r As Rezerwacja = PobierzRezerwacje(rezerwacjaId)
ReservationManager.vb:105:        If r Is Nothing Then
ReservationManager.vb:106:            Throw New BladBiznesowyException("Nie znaleziono rezerwacji " & rezerwacjaId)
ReservationManager.vb:107:        End If
ReservationManager.vb:108:        If r.Status <> "N" Then
ReservationManager.vb:109:            Throw New BladBiznesowyException("Potwierdzic mozna tylko nowa rezerwacje")
ReservationManager.vb:110:        End If
ReservationManager.vb:111:        Dim dal As New ReservationDAL()
ReservationManager.vb:112:        dal.ZmienStatus(rezerwacjaId, "P")
ReservationManager.vb:113:        Log.Info("Rezerwacja " & rezerwacjaId & " potwierdzona")
ReservationManager.vb:114:    End Sub
ReservationManager.vb:115:
ReservationManager.vb:116:    Public Sub Anuluj(ByVal rezerwacjaId As Integer)
ReservationManager.vb:117:        Dim r As Rezerwacja = PobierzRezerwacje(rezerwacjaId)
ReservationManager.vb:118:        If r Is Nothing Then
ReservationManager.vb:119:            Throw New BladBiznesowyException("Nie znaleziono rezerwacji " & rezerwacjaId)
ReservationManager.vb:120:        End If
ReservationManager.vb:121:        If r.Status = "Z" Then
ReservationManager.vb:122:            Throw New BladBiznesowyException("Rezerwacja zrealizowana - nie mozna anulowac")
ReservationManager.vb:123:        End If
ReservationManager.vb:124:        If r.Status = "A" Then
ReservationManager.vb:125:            Log.Debug("Rezerwacja " & rezerwacjaId & " juz anulowana")
ReservationManager.vb:126:            Return
ReservationManager.vb:127:        End If
ReservationManager.vb:128:        Dim dal As New ReservationDAL()
ReservationManager.vb:129:        dal.ZmienStatus(rezerwacjaId, "A")
ReservationManager.vb:130:        dal.DopiszDoDziennika("ANULOWANIE", "Rezerwacja " & rezerwacjaId & " (klient " & r.KlientId & ")")
ReservationManager.vb:131:        Log.Info("Rezerwacja " & rezerwacjaId & " anulowana")
ReservationManager.vb:132:    End Sub
ReservationManager.vb:133:
ReservationManager.vb:134:    Public Sub OznaczZrealizowana(ByVal rezerwacjaId As Integer)
ReservationManager.vb:135:        Dim dal As New ReservationDAL()
ReservationManager.vb:136:        dal.ZmienStatus(rezerwacjaId, "Z")
ReservationManager.vb:137:        Log.Info("Rezerwacja " & rezerwacjaId & " zrealizowana")
ReservationManager.vb:138:    End Sub
ReservationManager.vb:139:
ReservationManager.vb:140:    ' nowe rezerwacje, ktorych termin minal bez wydania - anulowane hurtowo (wolane recznie z panelu)
ReservationManager.vb:141:    Public Function AnulujPrzeterminowane(ByVal naDzien As Date) As Integer
ReservationManager.vb:142:        Dim dal As New ReservationDAL()
ReservationManager.vb:143:        Dim tabela As DataTable = dal.PrzeterminowaneNowe(naDzien)
ReservationManager.vb:144:        Dim ile As Integer = 0
ReservationManager.vb:145:        For Each wiersz As DataRow In tabela.Rows
ReservationManager.vb:146:            Try
ReservationManager.vb:147:                dal.ZmienStatus(CInt(wiersz("Id")), "A")
ReservationManager.vb:148:                ile = ile + 1
ReservationManager.vb:149:            Catch ex As SqlException
ReservationManager.vb:150:                Log.Error("Nie udalo sie anulowac rezerwacji " & wiersz("Id"), ex)
ReservationManager.vb:151:            End Try
ReservationManager.vb:152:        Next
ReservationManager.vb:153:        If ile > 0 Then
ReservationManager.vb:154:            dal.DopiszDoDziennika("ANULOWANIE", "Przeterminowane nowe rezerwacje: " & ile)
ReservationManager.vb:155:        End If
ReservationManager.vb:156:        Return ile
ReservationManager.vb:157:    End Function
ReservationManager.vb:158:
ReservationManager.vb:159:    ' zmiana terminu nowej albo potwierdzonej rezerwacji; cena liczona od nowa
ReservationManager.vb:160:    Public Sub ZmienTermin(ByVal rezerwacjaId As Integer, ByVal nowaDataOd As Date, ByVal nowaDataDo As Date)
ReservationManager.vb:161:        If nowaDataDo <= nowaDataOd Then
ReservationManager.vb:162:            Throw New ArgumentException("Data zakonczenia musi byc pozniejsza niz data rozpoczecia")
ReservationManager.vb:163:        End If
ReservationManager.vb:164:        Dim r As Rezerwacja = PobierzRezerwacje(rezerwacjaId)
ReservationManager.vb:165:        If r Is Nothing Then
ReservationManager.vb:166:            Throw New BladBiznesowyException("Nie znaleziono rezerwacji " & rezerwacjaId)
ReservationManager.vb:167:        End If
ReservationManager.vb:168:        If r.Status <> "N" AndAlso r.Status <> "P" Then
ReservationManager.vb:169:            Throw New BladBiznesowyException("Termin mozna zmienic tylko dla aktywnej rezerwacji")
ReservationManager.vb:170:        End If
ReservationManager.vb:171:        Dim dal As New ReservationDAL()
ReservationManager.vb:172:        Dim kolizje As DataTable = dal.PobierzKolizjeBezRezerwacji(r.SprzetId, nowaDataOd, nowaDataDo, rezerwacjaId)
ReservationManager.vb:173:        If kolizje.Rows.Count > 0 Then
ReservationManager.vb:174:            Throw New BladBiznesowyException("Sprzet niedostepny w wybranym terminie")
ReservationManager.vb:175:        End If
ReservationManager.vb:176:        Dim cena As Decimal = New PricingManager().ObliczOplate(r.SprzetId, nowaDataOd, nowaDataDo, r.KlientId)
ReservationManager.vb:177:        dal.ZmienTermin(rezerwacjaId, nowaDataOd, nowaDataDo, cena)
ReservationManager.vb:178:        dal.DopiszDoDziennika("ZMIANA TERMINU", "Rezerwacja " & rezerwacjaId & ": " & r.Termin & " -> " & _
ReservationManager.vb:179:                              nowaDataOd.ToString("yyyy-MM-dd HH:mm") & " - " & nowaDataDo.ToString("yyyy-MM-dd HH:mm"))
ReservationManager.vb:180:        Log.Info("Rezerwacja " & rezerwacjaId & " - nowy termin, cena " & cena)
ReservationManager.vb:181:    End Sub
ReservationManager.vb:182:
ReservationManager.vb:183:    ' ponowne przeliczenie ceny po zmianie cennika (wolane recznie)
ReservationManager.vb:184:    Public Function PrzeliczCene(ByVal rezerwacjaId As Integer) As Decimal
ReservationManager.vb:185:        Dim r As Rezerwacja = PobierzRezerwacje(rezerwacjaId)
ReservationManager.vb:186:        If r Is Nothing Then
ReservationManager.vb:187:            Throw New BladBiznesowyException("Nie znaleziono rezerwacji " & rezerwacjaId)
ReservationManager.vb:188:        End If
ReservationManager.vb:189:        Dim cena As Decimal = New PricingManager().ObliczOplate(r.SprzetId, r.DataOd, r.DataDo, r.KlientId)
ReservationManager.vb:190:        If cena <> r.CenaSzacunkowa Then
ReservationManager.vb:191:            Dim dal As New ReservationDAL()
ReservationManager.vb:192:            dal.ZmienTermin(rezerwacjaId, r.DataOd, r.DataDo, cena)
ReservationManager.vb:193:            Log.Info("Rezerwacja " & rezerwacjaId & ": cena " & r.CenaSzacunkowa & " -> " & cena)
ReservationManager.vb:194:        End If
ReservationManager.vb:195:        Return cena
ReservationManager.vb:196:    End Function
ReservationManager.vb:197:
ReservationManager.vb:198:    ' kolumny: Id, KlientId, Klient, SprzetId, Sprzet, DataOd, DataDo, Status
ReservationManager.vb:199:    Public Function RezerwacjeNaDzien(ByVal dzien As Date) As DataTable
ReservationManager.vb:200:        Return New ReservationDAL().RezerwacjeNaDzien(dzien)
ReservationManager.vb:201:    End Function
ReservationManager.vb:202:
ReservationManager.vb:203:    ' kolumny: Status, Liczba, Suma
ReservationManager.vb:204:    Public Function Statystyki(ByVal dataOd As Date, ByVal dataDo As Date) As DataTable
ReservationManager.vb:205:        Return New ReservationDAL().Statystyki(dataOd, dataDo)
ReservationManager.vb:206:    End Function
ReservationManager.vb:207:
ReservationManager.vb:208:    ' dni miesiaca zajete przez rezerwacje (do podswietlenia w kalendarzu)
ReservationManager.vb:209:    Public Function ZajeteDni(ByVal sprzetId As Integer, ByVal rok As Integer, ByVal miesiac As Integer) As List(Of Date)
ReservationManager.vb:210:        Dim wynik As New List(Of Date)()
ReservationManager.vb:211:        Dim tabela As DataTable = New ReservationDAL().KalendarzSprzetu(sprzetId, rok, miesiac)
ReservationManager.vb:212:        For Each wiersz As DataRow In tabela.Rows
ReservationManager.vb:213:            Dim od As Date = CDate(wiersz("DataOd")).Date
ReservationManager.vb:214:            Dim doDnia As Date = CDate(wiersz("DataDo")).Date
ReservationManager.vb:215:            Dim dzien As Date = od
ReservationManager.vb:216:            While dzien <= doDnia
ReservationManager.vb:217:                If dzien.Month = miesiac AndAlso Not wynik.Contains(dzien) Then
ReservationManager.vb:218:                    wynik.Add(dzien)
ReservationManager.vb:219:                End If
ReservationManager.vb:220:                dzien = dzien.AddDays(1)
ReservationManager.vb:221:            End While
ReservationManager.vb:222:        Next
ReservationManager.vb:223:        wynik.Sort()
ReservationManager.vb:224:        Return wynik
ReservationManager.vb:225:    End Function
ReservationManager.vb:226:
ReservationManager.vb:227:    Public Function DoPotwierdzenia() As DataTable
ReservationManager.vb:228:        Return New ReservationDAL().DoPotwierdzenia()
ReservationManager.vb:229:    End Function
ReservationManager.vb:230:
ReservationManager.vb:231:    Public Function PotwierdzWszystkie() As Integer
ReservationManager.vb:232:        Dim tabela As DataTable = DoPotwierdzenia()
ReservationManager.vb:233:        Dim ile As Integer = 0
ReservationManager.vb:234:        For Each wiersz As DataRow In tabela.Rows
ReservationManager.vb:235:            Try
ReservationManager.vb:236:                Potwierdz(CInt(wiersz("Id")))
ReservationManager.vb:237:                ile = ile + 1
ReservationManager.vb:238:            Catch ex As BladBiznesowyException
ReservationManager.vb:239:                Log.Warn("Pominieto rezerwacje " & wiersz("Id") & ": " & ex.Message)
ReservationManager.vb:240:            End Try
ReservationManager.vb:241:        Next
ReservationManager.vb:242:        Return ile
ReservationManager.vb:243:    End Function
ReservationManager.vb:244:
ReservationManager.vb:245:    Public Sub ZmienUwagi(ByVal rezerwacjaId As Integer, ByVal uwagi As String)
ReservationManager.vb:246:        Dim r As Rezerwacja = PobierzRezerwacje(rezerwacjaId)
ReservationManager.vb:247:        If r Is Nothing Then
ReservationManager.vb:248:            Throw New BladBiznesowyException("Nie znaleziono rezerwacji " & rezerwacjaId)
ReservationManager.vb:249:        End If
ReservationManager.vb:250:        Dim dal As New ReservationDAL()
ReservationManager.vb:251:        dal.ZapiszUwagi(rezerwacjaId, PrzytnijUwagi(uwagi))
ReservationManager.vb:252:    End Sub
ReservationManager.vb:253:
ReservationManager.vb:254:    Public Function CzyKlientMaAktywne(ByVal klientId As Integer) As Boolean
ReservationManager.vb:255:        Return New ReservationDAL().LiczbaAktywnychKlienta(klientId) > 0
ReservationManager.vb:256:    End Function
ReservationManager.vb:257:
ReservationManager.vb:258:    Public Function OpisRezerwacji(ByVal rezerwacjaId As Integer) As String
ReservationManager.vb:259:        Dim r As Rezerwacja = PobierzRezerwacje(rezerwacjaId)
ReservationManager.vb:260:        If r Is Nothing Then
ReservationManager.vb:261:            Return ""
ReservationManager.vb:262:        End If
ReservationManager.vb:263:        Dim sprzet As Sprzet = New EquipmentManager().PobierzSprzet(r.SprzetId)
ReservationManager.vb:264:        Dim nazwa As String = "?"
ReservationManager.vb:265:        If sprzet IsNot Nothing Then
ReservationManager.vb:266:            nazwa = sprzet.Nazwa
ReservationManager.vb:267:        End If
ReservationManager.vb:268:        Return "Rezerwacja " & r.Id & ": " & nazwa & ", " & r.Termin & ", " & r.OpisStatusu
ReservationManager.vb:269:    End Function
ReservationManager.vb:270:
ReservationManager.vb:271:    Private Function Mapuj(ByVal wiersz As DataRow) As Rezerwacja
ReservationManager.vb:272:        Dim r As New Rezerwacja()
ReservationManager.vb:273:        r.Id = CInt(wiersz("Id"))
ReservationManager.vb:274:        r.KlientId = CInt(wiersz("KlientId"))
ReservationManager.vb:275:        r.SprzetId = CInt(wiersz("SprzetId"))
ReservationManager.vb:276:        r.DataOd = CDate(wiersz("DataOd"))
ReservationManager.vb:277:        r.DataDo = CDate(wiersz("DataDo"))
ReservationManager.vb:278:        r.Status = CStr(wiersz("Status"))
ReservationManager.vb:279:        r.CenaSzacunkowa = CDec(wiersz("CenaSzacunkowa"))
ReservationManager.vb:280:        If IsDBNull(wiersz("Uwagi")) Then
ReservationManager.vb:281:            r.Uwagi = ""
ReservationManager.vb:282:        Else
ReservationManager.vb:283:            r.Uwagi = CStr(wiersz("Uwagi"))
ReservationManager.vb:284:        End If
ReservationManager.vb:285:        Return r
ReservationManager.vb:286:    End Function
ReservationManager.vb:287:
ReservationManager.vb:288:    Private Function PrzytnijUwagi(ByVal uwagi As String) As String
ReservationManager.vb:289:        If uwagi Is Nothing Then
ReservationManager.vb:290:            Return ""
ReservationManager.vb:291:        End If
ReservationManager.vb:292:        uwagi = uwagi.Trim()
ReservationManager.vb:293:        If uwagi.Length > MaksDlugoscUwag Then
ReservationManager.vb:294:            uwagi = uwagi.Substring(0, MaksDlugoscUwag)
ReservationManager.vb:295:        End If
ReservationManager.vb:296:        Return uwagi
ReservationManager.vb:297:    End Function
ReservationManager.vb:298:
ReservationManager.vb:299:End Class
```

## M14 — surowy wynik H4

`git --no-pager -C 'legacy-app-parent\legacy-app\WypozyczalniaCore\DAL' grep -n -I -e '^' -- '*.vb'`

```text
DbHelper.vb:1:' Modul: DAL\DbHelper.vb - wspolny dostep ADO.NET do bazy Wypozyczalnia | Autor: zespol IT | 2014-03-11
DbHelper.vb:2:' Historia: 2014-03-11 JK utworzenie
DbHelper.vb:3:'           2016-01-18 MW obsluga parametrow OUTPUT dla procedur
DbHelper.vb:4:'           2019-04-02 PT domyslny connection string dla zadania nocnego
DbHelper.vb:5:Imports System.Data
DbHelper.vb:6:Imports System.Data.SqlClient
DbHelper.vb:7:Imports System.Configuration
DbHelper.vb:8:
DbHelper.vb:9:Public Class BladBiznesowyException
DbHelper.vb:10:    Inherits ApplicationException
DbHelper.vb:11:
DbHelper.vb:12:    Public Property KodBledu As String
DbHelper.vb:13:
DbHelper.vb:14:    Public Sub New(ByVal komunikat As String)
DbHelper.vb:15:        MyBase.New(komunikat)
DbHelper.vb:16:        KodBledu = "BIZ"
DbHelper.vb:17:    End Sub
DbHelper.vb:18:
DbHelper.vb:19:    Public Sub New(ByVal komunikat As String, ByVal kodBledu As String)
DbHelper.vb:20:        MyBase.New(komunikat)
DbHelper.vb:21:        Me.KodBledu = kodBledu
DbHelper.vb:22:    End Sub
DbHelper.vb:23:End Class
DbHelper.vb:24:
DbHelper.vb:25:Namespace DAL
DbHelper.vb:26:
DbHelper.vb:27:    Public NotInheritable Class DbHelper
DbHelper.vb:28:
DbHelper.vb:29:        Private Const DomyslnyTimeout As Integer = 60
DbHelper.vb:30:
DbHelper.vb:31:        Private Sub New()
DbHelper.vb:32:        End Sub
DbHelper.vb:33:
DbHelper.vb:34:        Public Shared Function PobierzConnectionString() As String
DbHelper.vb:35:            Dim wpis As ConnectionStringSettings = ConfigurationManager.ConnectionStrings("WypozyczalniaDb")
DbHelper.vb:36:            If wpis Is Nothing OrElse String.IsNullOrEmpty(wpis.ConnectionString) Then
DbHelper.vb:37:                Return "Data Source=sql-wypozyczalnia;Initial Catalog=Wypozyczalnia;Integrated Security=True"
DbHelper.vb:38:            End If
DbHelper.vb:39:            Return wpis.ConnectionString
DbHelper.vb:40:        End Function
DbHelper.vb:41:
DbHelper.vb:42:        Public Shared Function WykonajZapytanie(ByVal sql As String, ByVal ParamArray parametry() As SqlParameter) As DataTable
DbHelper.vb:43:            Dim wynik As New DataTable()
DbHelper.vb:44:            Using cn As New SqlConnection(PobierzConnectionString())
DbHelper.vb:45:                Using cmd As New SqlCommand(sql, cn)
DbHelper.vb:46:                    cmd.CommandType = CommandType.Text
DbHelper.vb:47:                    cmd.CommandTimeout = DomyslnyTimeout
DbHelper.vb:48:                    DodajParametry(cmd, parametry)
DbHelper.vb:49:                    Using da As New SqlDataAdapter(cmd)
DbHelper.vb:50:                        da.Fill(wynik)
DbHelper.vb:51:                    End Using
DbHelper.vb:52:                End Using
DbHelper.vb:53:            End Using
DbHelper.vb:54:            Return wynik
DbHelper.vb:55:        End Function
DbHelper.vb:56:
DbHelper.vb:57:        Public Shared Function WykonajPolecenie(ByVal sql As String, ByVal ParamArray parametry() As SqlParameter) As Integer
DbHelper.vb:58:            Using cn As New SqlConnection(PobierzConnectionString())
DbHelper.vb:59:                Using cmd As New SqlCommand(sql, cn)
DbHelper.vb:60:                    cmd.CommandType = CommandType.Text
DbHelper.vb:61:                    cmd.CommandTimeout = DomyslnyTimeout
DbHelper.vb:62:                    DodajParametry(cmd, parametry)
DbHelper.vb:63:                    cn.Open()
DbHelper.vb:64:                    Return cmd.ExecuteNonQuery()
DbHelper.vb:65:                End Using
DbHelper.vb:66:            End Using
DbHelper.vb:67:        End Function
DbHelper.vb:68:
DbHelper.vb:69:        Public Shared Function WykonajProcedure(ByVal nazwa As String, ByVal ParamArray parametry() As SqlParameter) As DataTable
DbHelper.vb:70:            Dim wynik As New DataTable()
DbHelper.vb:71:            Using cn As New SqlConnection(PobierzConnectionString())
DbHelper.vb:72:                Using cmd As New SqlCommand(nazwa, cn)
DbHelper.vb:73:                    cmd.CommandType = CommandType.StoredProcedure
DbHelper.vb:74:                    cmd.CommandTimeout = DomyslnyTimeout
DbHelper.vb:75:                    DodajParametry(cmd, parametry)
DbHelper.vb:76:                    Using da As New SqlDataAdapter(cmd)
DbHelper.vb:77:                        da.Fill(wynik)
DbHelper.vb:78:                    End Using
DbHelper.vb:79:                End Using
DbHelper.vb:80:            End Using
DbHelper.vb:81:            Return wynik
DbHelper.vb:82:        End Function
DbHelper.vb:83:
DbHelper.vb:84:        Public Shared Function WykonajProcedureBezWyniku(ByVal nazwa As String, ByVal ParamArray parametry() As SqlParameter) As Integer
DbHelper.vb:85:            Using cn As New SqlConnection(PobierzConnectionString())
DbHelper.vb:86:                Using cmd As New SqlCommand(nazwa, cn)
DbHelper.vb:87:                    cmd.CommandType = CommandType.StoredProcedure
DbHelper.vb:88:                    cmd.CommandTimeout = DomyslnyTimeout
DbHelper.vb:89:                    DodajParametry(cmd, parametry)
DbHelper.vb:90:                    cn.Open()
DbHelper.vb:91:                    Return cmd.ExecuteNonQuery()
DbHelper.vb:92:                End Using
DbHelper.vb:93:            End Using
DbHelper.vb:94:        End Function
DbHelper.vb:95:
DbHelper.vb:96:        Public Shared Function Parametr(ByVal nazwa As String, ByVal wartosc As Object) As SqlParameter
DbHelper.vb:97:            If wartosc Is Nothing Then
DbHelper.vb:98:                Return New SqlParameter(nazwa, DBNull.Value)
DbHelper.vb:99:            End If
DbHelper.vb:100:            Return New SqlParameter(nazwa, wartosc)
DbHelper.vb:101:        End Function
DbHelper.vb:102:
DbHelper.vb:103:        Public Shared Function ParametrWyjsciowy(ByVal nazwa As String, ByVal typ As SqlDbType, Optional ByVal rozmiar As Integer = 0) As SqlParameter
DbHelper.vb:104:            Dim p As New SqlParameter(nazwa, typ)
DbHelper.vb:105:            If rozmiar > 0 Then p.Size = rozmiar
DbHelper.vb:106:            p.Direction = ParameterDirection.Output
DbHelper.vb:107:            Return p
DbHelper.vb:108:        End Function
DbHelper.vb:109:
DbHelper.vb:110:        Private Shared Sub DodajParametry(ByVal cmd As SqlCommand, ByVal parametry() As SqlParameter)
DbHelper.vb:111:            If parametry Is Nothing Then Return
DbHelper.vb:112:            For Each p As SqlParameter In parametry
DbHelper.vb:113:                If p IsNot Nothing Then cmd.Parameters.Add(p)
DbHelper.vb:114:            Next
DbHelper.vb:115:        End Sub
DbHelper.vb:116:
DbHelper.vb:117:    End Class
DbHelper.vb:118:
DbHelper.vb:119:End Namespace
EquipmentDAL.vb:1:' Modul: DAL - dostep do tabeli Sprzet | Autor: zespol IT | 2014-03-11
EquipmentDAL.vb:2:' Historia:
EquipmentDAL.vb:3:'   2015-06-02 MW  kolumna Kaucja
EquipmentDAL.vb:4:'   2018-01-15 PT  lista bez wycofanych (status X)
EquipmentDAL.vb:5:
EquipmentDAL.vb:6:Imports System.Data
EquipmentDAL.vb:7:Imports System.Data.SqlClient
EquipmentDAL.vb:8:Imports Wypozyczalnia.Core.Model
EquipmentDAL.vb:9:
EquipmentDAL.vb:10:Namespace DAL
EquipmentDAL.vb:11:
EquipmentDAL.vb:12:    Public Class EquipmentDAL
EquipmentDAL.vb:13:
EquipmentDAL.vb:14:        Private Const KolumnySprzetu As String = "Id, NumerInwentarzowy, Nazwa, Kategoria, StawkaDobowa, Kaucja, Status"
EquipmentDAL.vb:15:
EquipmentDAL.vb:16:        Public Function Pobierz(ByVal sprzetId As Integer) As Sprzet
EquipmentDAL.vb:17:            Dim sql As String = "SELECT " & KolumnySprzetu & " FROM Sprzet WHERE Id = @Id"
EquipmentDAL.vb:18:            Dim tabela As DataTable = DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@Id", sprzetId))
EquipmentDAL.vb:19:            If tabela.Rows.Count = 0 Then
EquipmentDAL.vb:20:                Return Nothing
EquipmentDAL.vb:21:            End If
EquipmentDAL.vb:22:            Return Mapuj(tabela.Rows(0))
EquipmentDAL.vb:23:        End Function
EquipmentDAL.vb:24:
EquipmentDAL.vb:25:        Public Function PobierzPoNumerze(ByVal numerInwentarzowy As String) As Sprzet
EquipmentDAL.vb:26:            Dim sql As String = "SELECT " & KolumnySprzetu & " FROM Sprzet WHERE NumerInwentarzowy = @Numer"
EquipmentDAL.vb:27:            Dim tabela As DataTable = DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@Numer", numerInwentarzowy))
EquipmentDAL.vb:28:            If tabela.Rows.Count = 0 Then
EquipmentDAL.vb:29:                Return Nothing
EquipmentDAL.vb:30:            End If
EquipmentDAL.vb:31:            Return Mapuj(tabela.Rows(0))
EquipmentDAL.vb:32:        End Function
EquipmentDAL.vb:33:
EquipmentDAL.vb:34:        ' kategoria pusta = wszystkie kategorie; wycofane (X) nie sa pokazywane
EquipmentDAL.vb:35:        Public Function Lista(ByVal kategoria As String) As List(Of Sprzet)
EquipmentDAL.vb:36:            Dim wynik As New List(Of Sprzet)()
EquipmentDAL.vb:37:            Dim tabela As DataTable
EquipmentDAL.vb:38:            If String.IsNullOrEmpty(kategoria) Then
EquipmentDAL.vb:39:                tabela = DbHelper.WykonajZapytanie("SELECT " & KolumnySprzetu & " FROM Sprzet WHERE Status <> 'X' ORDER BY Kategoria, NumerInwentarzowy")
EquipmentDAL.vb:40:            Else
EquipmentDAL.vb:41:                tabela = DbHelper.WykonajZapytanie("SELECT " & KolumnySprzetu & " FROM Sprzet WHERE Status <> 'X' AND Kategoria = @Kategoria ORDER BY NumerInwentarzowy", _
EquipmentDAL.vb:42:                                                   DbHelper.Parametr("@Kategoria", kategoria))
EquipmentDAL.vb:43:            End If
EquipmentDAL.vb:44:            For Each wiersz As DataRow In tabela.Rows
EquipmentDAL.vb:45:                wynik.Add(Mapuj(wiersz))
EquipmentDAL.vb:46:            Next
EquipmentDAL.vb:47:            Return wynik
EquipmentDAL.vb:48:        End Function
EquipmentDAL.vb:49:
EquipmentDAL.vb:50:        Public Function ListaWStatusie(ByVal status As String) As List(Of Sprzet)
EquipmentDAL.vb:51:            Dim wynik As New List(Of Sprzet)()
EquipmentDAL.vb:52:            Dim sql As String = "SELECT " & KolumnySprzetu & " FROM Sprzet WHERE Status = @Status ORDER BY NumerInwentarzowy"
EquipmentDAL.vb:53:            Dim tabela As DataTable = DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@Status", status))
EquipmentDAL.vb:54:            For Each wiersz As DataRow In tabela.Rows
EquipmentDAL.vb:55:                wynik.Add(Mapuj(wiersz))
EquipmentDAL.vb:56:            Next
EquipmentDAL.vb:57:            Return wynik
EquipmentDAL.vb:58:        End Function
EquipmentDAL.vb:59:
EquipmentDAL.vb:60:        Public Function LiczbaWKategoriach() As DataTable
EquipmentDAL.vb:61:            Dim sql As String = "SELECT Kategoria, COUNT(*) AS Liczba, SUM(CASE WHEN Status = 'D' THEN 1 ELSE 0 END) AS Dostepne " & _
EquipmentDAL.vb:62:                                "FROM Sprzet WHERE Status <> 'X' GROUP BY Kategoria ORDER BY Kategoria"
EquipmentDAL.vb:63:            Return DbHelper.WykonajZapytanie(sql)
EquipmentDAL.vb:64:        End Function
EquipmentDAL.vb:65:
EquipmentDAL.vb:66:        Public Function CzyNumerZajety(ByVal numerInwentarzowy As String, ByVal pominId As Integer) As Boolean
EquipmentDAL.vb:67:            Dim sql As String = "SELECT COUNT(*) AS Ile FROM Sprzet WHERE NumerInwentarzowy = @Numer AND Id <> @Id"
EquipmentDAL.vb:68:            Dim tabela As DataTable = DbHelper.WykonajZapytanie(sql, _
EquipmentDAL.vb:69:                                                                DbHelper.Parametr("@Numer", numerInwentarzowy), _
EquipmentDAL.vb:70:                                                                DbHelper.Parametr("@Id", pominId))
EquipmentDAL.vb:71:            Return CInt(tabela.Rows(0)("Ile")) > 0
EquipmentDAL.vb:72:        End Function
EquipmentDAL.vb:73:
EquipmentDAL.vb:74:        Public Function Dodaj(ByVal s As Sprzet) As Integer
EquipmentDAL.vb:75:            Dim sql As String = "INSERT INTO Sprzet (NumerInwentarzowy, Nazwa, Kategoria, StawkaDobowa, Kaucja, Status) " & _
EquipmentDAL.vb:76:                                "VALUES (@Numer, @Nazwa, @Kategoria, @Stawka, @Kaucja, @Status); " & _
EquipmentDAL.vb:77:                                "SELECT CAST(SCOPE_IDENTITY() AS INT) AS NoweId"
EquipmentDAL.vb:78:            Dim tabela As DataTable = DbHelper.WykonajZapytanie(sql, _
EquipmentDAL.vb:79:                                                                DbHelper.Parametr("@Numer", s.NumerInwentarzowy), _
EquipmentDAL.vb:80:                                                                DbHelper.Parametr("@Nazwa", s.Nazwa), _
EquipmentDAL.vb:81:                                                                DbHelper.Parametr("@Kategoria", s.Kategoria), _
EquipmentDAL.vb:82:                                                                DbHelper.Parametr("@Stawka", s.StawkaDobowa), _
EquipmentDAL.vb:83:                                                                DbHelper.Parametr("@Kaucja", s.Kaucja), _
EquipmentDAL.vb:84:                                                                DbHelper.Parametr("@Status", s.Status))
EquipmentDAL.vb:85:            Return CInt(tabela.Rows(0)("NoweId"))
EquipmentDAL.vb:86:        End Function
EquipmentDAL.vb:87:
EquipmentDAL.vb:88:        Public Function Aktualizuj(ByVal s As Sprzet) As Integer
EquipmentDAL.vb:89:            Dim sql As String = "UPDATE Sprzet SET NumerInwentarzowy = @Numer, Nazwa = @Nazwa, Kategoria = @Kategoria, " & _
EquipmentDAL.vb:90:                                "StawkaDobowa = @Stawka, Kaucja = @Kaucja, Status = @Status WHERE Id = @Id"
EquipmentDAL.vb:91:            Return DbHelper.WykonajPolecenie(sql, _
EquipmentDAL.vb:92:                                             DbHelper.Parametr("@Numer", s.NumerInwentarzowy), _
EquipmentDAL.vb:93:                                             DbHelper.Parametr("@Nazwa", s.Nazwa), _
EquipmentDAL.vb:94:                                             DbHelper.Parametr("@Kategoria", s.Kategoria), _
EquipmentDAL.vb:95:                                             DbHelper.Parametr("@Stawka", s.StawkaDobowa), _
EquipmentDAL.vb:96:                                             DbHelper.Parametr("@Kaucja", s.Kaucja), _
EquipmentDAL.vb:97:                                             DbHelper.Parametr("@Status", s.Status), _
EquipmentDAL.vb:98:                                             DbHelper.Parametr("@Id", s.Id))
EquipmentDAL.vb:99:        End Function
EquipmentDAL.vb:100:
EquipmentDAL.vb:101:        Public Function ZmienStatus(ByVal sprzetId As Integer, ByVal status As String) As Integer
EquipmentDAL.vb:102:            Dim sql As String = "UPDATE Sprzet SET Status = @Status WHERE Id = @Id"
EquipmentDAL.vb:103:            Dim ile As Integer = DbHelper.WykonajPolecenie(sql, _
EquipmentDAL.vb:104:                                                           DbHelper.Parametr("@Status", status), _
EquipmentDAL.vb:105:                                                           DbHelper.Parametr("@Id", sprzetId))
EquipmentDAL.vb:106:            DbHelper.WykonajPolecenie("INSERT INTO DziennikOperacji (Operacja, Szczegoly) VALUES ('STATUS SPRZETU', @Opis)", _
EquipmentDAL.vb:107:                                      DbHelper.Parametr("@Opis", "Sprzet " & sprzetId & " -> " & status))
EquipmentDAL.vb:108:            Return ile
EquipmentDAL.vb:109:        End Function
EquipmentDAL.vb:110:
EquipmentDAL.vb:111:        ' wyszukiwanie po nazwie albo numerze (formularz w katalogu)
EquipmentDAL.vb:112:        Public Function Szukaj(ByVal fraza As String) As List(Of Sprzet)
EquipmentDAL.vb:113:            Dim wynik As New List(Of Sprzet)()
EquipmentDAL.vb:114:            If String.IsNullOrEmpty(fraza) Then
EquipmentDAL.vb:115:                Return wynik
EquipmentDAL.vb:116:            End If
EquipmentDAL.vb:117:            Dim sql As String = "SELECT " & KolumnySprzetu & " FROM Sprzet " & _
EquipmentDAL.vb:118:                                "WHERE (Nazwa LIKE @Fraza OR NumerInwentarzowy LIKE @Fraza) AND Status <> 'X' " & _
EquipmentDAL.vb:119:                                "ORDER BY NumerInwentarzowy"
EquipmentDAL.vb:120:            Dim tabela As DataTable = DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@Fraza", "%" & fraza.Trim() & "%"))
EquipmentDAL.vb:121:            For Each wiersz As DataRow In tabela.Rows
EquipmentDAL.vb:122:                wynik.Add(Mapuj(wiersz))
EquipmentDAL.vb:123:            Next
EquipmentDAL.vb:124:            Return wynik
EquipmentDAL.vb:125:        End Function
EquipmentDAL.vb:126:
EquipmentDAL.vb:127:        ' kolumny: Id, Klient, DataWydania, PlanowanyZwrot, DataZwrotu, Status
EquipmentDAL.vb:128:        Public Function HistoriaWypozyczen(ByVal sprzetId As Integer) As DataTable
EquipmentDAL.vb:129:            Dim sql As String = "SELECT w.Id, k.Nazwisko AS Klient, w.DataWydania, w.PlanowanyZwrot, w.DataZwrotu, w.Status " & _
EquipmentDAL.vb:130:                                "FROM Wypozyczenia w INNER JOIN Klienci k ON k.Id = w.KlientId " & _
EquipmentDAL.vb:131:                                "WHERE w.SprzetId = @SprzetId " & _
EquipmentDAL.vb:132:                                "ORDER BY w.DataWydania DESC"
EquipmentDAL.vb:133:            Return DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@SprzetId", sprzetId))
EquipmentDAL.vb:134:        End Function
EquipmentDAL.vb:135:
EquipmentDAL.vb:136:        Public Function ZmienStawke(ByVal sprzetId As Integer, ByVal stawkaDobowa As Decimal, ByVal kaucja As Decimal) As Integer
EquipmentDAL.vb:137:            Dim sql As String = "UPDATE Sprzet SET StawkaDobowa = @Stawka, Kaucja = @Kaucja WHERE Id = @Id"
EquipmentDAL.vb:138:            Return DbHelper.WykonajPolecenie(sql, _
EquipmentDAL.vb:139:                                             DbHelper.Parametr("@Stawka", stawkaDobowa), _
EquipmentDAL.vb:140:                                             DbHelper.Parametr("@Kaucja", kaucja), _
EquipmentDAL.vb:141:                                             DbHelper.Parametr("@Id", sprzetId))
EquipmentDAL.vb:142:        End Function
EquipmentDAL.vb:143:
EquipmentDAL.vb:144:        Public Function MaOtwarteWypozyczenie(ByVal sprzetId As Integer) As Boolean
EquipmentDAL.vb:145:            Dim sql As String = "SELECT COUNT(*) AS Ile FROM Wypozyczenia WHERE SprzetId = @SprzetId AND Status = 'O'"
EquipmentDAL.vb:146:            Dim tabela As DataTable = DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@SprzetId", sprzetId))
EquipmentDAL.vb:147:            Return CInt(tabela.Rows(0)("Ile")) > 0
EquipmentDAL.vb:148:        End Function
EquipmentDAL.vb:149:
EquipmentDAL.vb:150:        Private Function Mapuj(ByVal wiersz As DataRow) As Sprzet
EquipmentDAL.vb:151:            Dim s As New Sprzet()
EquipmentDAL.vb:152:            s.Id = CInt(wiersz("Id"))
EquipmentDAL.vb:153:            s.NumerInwentarzowy = CStr(wiersz("NumerInwentarzowy"))
EquipmentDAL.vb:154:            s.Nazwa = CStr(wiersz("Nazwa"))
EquipmentDAL.vb:155:            If IsDBNull(wiersz("Kategoria")) Then
EquipmentDAL.vb:156:                s.Kategoria = ""
EquipmentDAL.vb:157:            Else
EquipmentDAL.vb:158:                s.Kategoria = CStr(wiersz("Kategoria")).Trim()
EquipmentDAL.vb:159:            End If
EquipmentDAL.vb:160:            s.StawkaDobowa = CDec(wiersz("StawkaDobowa"))
EquipmentDAL.vb:161:            s.Kaucja = CDec(wiersz("Kaucja"))
EquipmentDAL.vb:162:            s.Status = CStr(wiersz("Status"))
EquipmentDAL.vb:163:            Return s
EquipmentDAL.vb:164:        End Function
EquipmentDAL.vb:165:
EquipmentDAL.vb:166:    End Class
EquipmentDAL.vb:167:
EquipmentDAL.vb:168:End Namespace
InvoiceDAL.vb:1:' Modul: DAL - dostep do tabel Faktury i PozycjeFaktur | Autor: zespol IT | 2014-05-19
InvoiceDAL.vb:2:' Historia:
InvoiceDAL.vb:3:'   2014-11-27 JK  numer faktury nadawany w bazie (usp_WystawFakture)
InvoiceDAL.vb:4:'   2018-04-03 PT  FakturyZDnia dla eksportu nocnego
InvoiceDAL.vb:5:
InvoiceDAL.vb:6:Imports System.Data
InvoiceDAL.vb:7:Imports System.Data.SqlClient
InvoiceDAL.vb:8:Imports Wypozyczalnia.Core.Model
InvoiceDAL.vb:9:
InvoiceDAL.vb:10:Namespace DAL
InvoiceDAL.vb:11:
InvoiceDAL.vb:12:    Public Class InvoiceDAL
InvoiceDAL.vb:13:
InvoiceDAL.vb:14:        Private Const KolumnyFaktury As String = "f.Id, f.Numer, f.KlientId, f.WypozyczenieId, f.DataWystawienia, f.Netto, f.Vat, f.Brutto, f.Status, f.IdTransakcji"
InvoiceDAL.vb:15:
InvoiceDAL.vb:16:        ' naglowek przez procedure - zwraca nowe Id, numer ustawiany w obiekcie
InvoiceDAL.vb:17:        Public Function ZapiszNaglowek(ByVal f As Faktura) As Integer
InvoiceDAL.vb:18:            Dim noweId As SqlParameter = DbHelper.ParametrWyjsciowy("@NoweId", SqlDbType.Int)
InvoiceDAL.vb:19:            Dim numer As SqlParameter = DbHelper.ParametrWyjsciowy("@Numer", SqlDbType.VarChar, 30)
InvoiceDAL.vb:20:            DbHelper.WykonajProcedureBezWyniku("usp_WystawFakture", _
InvoiceDAL.vb:21:                                               DbHelper.Parametr("@KlientId", f.KlientId), _
InvoiceDAL.vb:22:                                               DbHelper.Parametr("@WypozyczenieId", f.WypozyczenieId), _
InvoiceDAL.vb:23:                                               DbHelper.Parametr("@Netto", f.Netto), _
InvoiceDAL.vb:24:                                               DbHelper.Parametr("@Vat", f.Vat), _
InvoiceDAL.vb:25:                                               DbHelper.Parametr("@Brutto", f.Brutto), _
InvoiceDAL.vb:26:                                               noweId, _
InvoiceDAL.vb:27:                                               numer)
InvoiceDAL.vb:28:            If noweId.Value Is Nothing OrElse IsDBNull(noweId.Value) Then
InvoiceDAL.vb:29:                Throw New ApplicationException("Procedura usp_WystawFakture nie zwrocila Id")
InvoiceDAL.vb:30:            End If
InvoiceDAL.vb:31:            f.Id = CInt(noweId.Value)
InvoiceDAL.vb:32:            f.Numer = numer.Value.ToString()
InvoiceDAL.vb:33:            Return f.Id
InvoiceDAL.vb:34:        End Function
InvoiceDAL.vb:35:
InvoiceDAL.vb:36:        Public Sub ZapiszPozycje(ByVal fakturaId As Integer, ByVal p As PozycjaFaktury)
InvoiceDAL.vb:37:            Dim sql As String = "INSERT INTO PozycjeFaktur (FakturaId, Lp, Opis, Ilosc, CenaNetto, StawkaVat, WartoscNetto) " & _
InvoiceDAL.vb:38:                                "VALUES (@FakturaId, @Lp, @Opis, @Ilosc, @CenaNetto, @StawkaVat, @WartoscNetto)"
InvoiceDAL.vb:39:            DbHelper.WykonajPolecenie(sql, _
InvoiceDAL.vb:40:                                      DbHelper.Parametr("@FakturaId", fakturaId), _
InvoiceDAL.vb:41:                                      DbHelper.Parametr("@Lp", p.Lp), _
InvoiceDAL.vb:42:                                      DbHelper.Parametr("@Opis", p.Opis), _
InvoiceDAL.vb:43:                                      DbHelper.Parametr("@Ilosc", p.Ilosc), _
InvoiceDAL.vb:44:                                      DbHelper.Parametr("@CenaNetto", p.CenaNetto), _
InvoiceDAL.vb:45:                                      DbHelper.Parametr("@StawkaVat", p.StawkaVat), _
InvoiceDAL.vb:46:                                      DbHelper.Parametr("@WartoscNetto", p.WartoscNetto))
InvoiceDAL.vb:47:        End Sub
InvoiceDAL.vb:48:
InvoiceDAL.vb:49:        Public Function Pobierz(ByVal fakturaId As Integer) As DataRow
InvoiceDAL.vb:50:            Dim sql As String = "SELECT " & KolumnyFaktury & " FROM Faktury f WHERE f.Id = @Id"
InvoiceDAL.vb:51:            Dim tabela As DataTable = DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@Id", fakturaId))
InvoiceDAL.vb:52:            If tabela.Rows.Count = 0 Then
InvoiceDAL.vb:53:                Return Nothing
InvoiceDAL.vb:54:            End If
InvoiceDAL.vb:55:            Return tabela.Rows(0)
InvoiceDAL.vb:56:        End Function
InvoiceDAL.vb:57:
InvoiceDAL.vb:58:        Public Function PobierzDlaWypozyczenia(ByVal wypozyczenieId As Integer) As DataRow
InvoiceDAL.vb:59:            Dim sql As String = "SELECT TOP 1 " & KolumnyFaktury & " FROM Faktury f " & _
InvoiceDAL.vb:60:                                "WHERE f.WypozyczenieId = @WypozyczenieId ORDER BY f.Id DESC"
InvoiceDAL.vb:61:            Dim tabela As DataTable = DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@WypozyczenieId", wypozyczenieId))
InvoiceDAL.vb:62:            If tabela.Rows.Count = 0 Then
InvoiceDAL.vb:63:                Return Nothing
InvoiceDAL.vb:64:            End If
InvoiceDAL.vb:65:            Return tabela.Rows(0)
InvoiceDAL.vb:66:        End Function
InvoiceDAL.vb:67:
InvoiceDAL.vb:68:        Public Function PobierzPozycje(ByVal fakturaId As Integer) As DataTable
InvoiceDAL.vb:69:            Dim sql As String = "SELECT Lp, Opis, Ilosc, CenaNetto, StawkaVat, WartoscNetto " & _
InvoiceDAL.vb:70:                                "FROM PozycjeFaktur WHERE FakturaId = @FakturaId ORDER BY Lp"
InvoiceDAL.vb:71:            Return DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@FakturaId", fakturaId))
InvoiceDAL.vb:72:        End Function
InvoiceDAL.vb:73:
InvoiceDAL.vb:74:        Public Function UstawOplacona(ByVal fakturaId As Integer, ByVal idTransakcji As String) As Integer
InvoiceDAL.vb:75:            Dim sql As String = "UPDATE Faktury SET Status = 'O', IdTransakcji = @IdTransakcji WHERE Id = @Id AND Status = 'W'"
InvoiceDAL.vb:76:            Dim ile As Integer = DbHelper.WykonajPolecenie(sql, _
InvoiceDAL.vb:77:                                                          DbHelper.Parametr("@IdTransakcji", idTransakcji), _
InvoiceDAL.vb:78:                                                          DbHelper.Parametr("@Id", fakturaId))
InvoiceDAL.vb:79:            If ile > 0 Then
InvoiceDAL.vb:80:                DbHelper.WykonajPolecenie("INSERT INTO DziennikOperacji (Operacja, Szczegoly) VALUES ('PLATNOSC', @Szczegoly)", _
InvoiceDAL.vb:81:                                          DbHelper.Parametr("@Szczegoly", "Faktura " & fakturaId & ", transakcja " & idTransakcji))
InvoiceDAL.vb:82:            End If
InvoiceDAL.vb:83:            Return ile
InvoiceDAL.vb:84:        End Function
InvoiceDAL.vb:85:
InvoiceDAL.vb:86:        ' kolumny: Id, Numer, KlientId, Klient, WypozyczenieId, DataWystawienia, Netto, Vat, Brutto, Status
InvoiceDAL.vb:87:        Public Function FakturyZDnia(ByVal dzien As Date) As DataTable
InvoiceDAL.vb:88:            Dim sql As String = "SELECT f.Id, f.Numer, f.KlientId, " & _
InvoiceDAL.vb:89:                                "CASE WHEN k.Typ = 'F' THEN k.NazwaFirmy ELSE k.Imie + ' ' + k.Nazwisko END AS Klient, " & _
InvoiceDAL.vb:90:                                "f.WypozyczenieId, f.DataWystawienia, f.Netto, f.Vat, f.Brutto, f.Status " & _
InvoiceDAL.vb:91:                                "FROM Faktury f INNER JOIN Klienci k ON k.Id = f.KlientId " & _
InvoiceDAL.vb:92:                                "WHERE f.DataWystawienia >= @Od AND f.DataWystawienia < @Do " & _
InvoiceDAL.vb:93:                                "ORDER BY f.Numer"
InvoiceDAL.vb:94:            Return DbHelper.WykonajZapytanie(sql, _
InvoiceDAL.vb:95:                                             DbHelper.Parametr("@Od", dzien.Date), _
InvoiceDAL.vb:96:                                             DbHelper.Parametr("@Do", dzien.Date.AddDays(1)))
InvoiceDAL.vb:97:        End Function
InvoiceDAL.vb:98:
InvoiceDAL.vb:99:        Public Function FakturyKlienta(ByVal klientId As Integer) As DataTable
InvoiceDAL.vb:100:            Dim sql As String = "SELECT " & KolumnyFaktury & " FROM Faktury f " & _
InvoiceDAL.vb:101:                                "WHERE f.KlientId = @KlientId ORDER BY f.DataWystawienia DESC"
InvoiceDAL.vb:102:            Return DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@KlientId", klientId))
InvoiceDAL.vb:103:        End Function
InvoiceDAL.vb:104:
InvoiceDAL.vb:105:        Public Function Nieoplacone() As DataTable
InvoiceDAL.vb:106:            Dim sql As String = "SELECT " & KolumnyFaktury & " FROM Faktury f " & _
InvoiceDAL.vb:107:                                "WHERE f.Status = 'W' ORDER BY f.DataWystawienia"
InvoiceDAL.vb:108:            Return DbHelper.WykonajZapytanie(sql)
InvoiceDAL.vb:109:        End Function
InvoiceDAL.vb:110:
InvoiceDAL.vb:111:        Public Function PobierzPoNumerze(ByVal numer As String) As DataRow
InvoiceDAL.vb:112:            Dim sql As String = "SELECT " & KolumnyFaktury & " FROM Faktury f WHERE f.Numer = @Numer"
InvoiceDAL.vb:113:            Dim tabela As DataTable = DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@Numer", numer))
InvoiceDAL.vb:114:            If tabela.Rows.Count = 0 Then
InvoiceDAL.vb:115:                Return Nothing
InvoiceDAL.vb:116:            End If
InvoiceDAL.vb:117:            Return tabela.Rows(0)
InvoiceDAL.vb:118:        End Function
InvoiceDAL.vb:119:
InvoiceDAL.vb:120:        ' zestawienie miesieczne dla ksiegowosci (ekran nieuzywany od 2017, zostawione dla raportu)
InvoiceDAL.vb:121:        Public Function ZestawienieMiesiaca(ByVal rok As Integer, ByVal miesiac As Integer) As DataTable
InvoiceDAL.vb:122:            Dim od As New Date(rok, miesiac, 1)
InvoiceDAL.vb:123:            Dim sql As String = "SELECT f.Status, COUNT(*) AS Liczba, SUM(f.Netto) AS Netto, SUM(f.Vat) AS Vat, SUM(f.Brutto) AS Brutto " & _
InvoiceDAL.vb:124:                                "FROM Faktury f WHERE f.DataWystawienia >= @Od AND f.DataWystawienia < @Do " & _
InvoiceDAL.vb:125:                                "GROUP BY f.Status"
InvoiceDAL.vb:126:            Return DbHelper.WykonajZapytanie(sql, _
InvoiceDAL.vb:127:                                             DbHelper.Parametr("@Od", od), _
InvoiceDAL.vb:128:                                             DbHelper.Parametr("@Do", od.AddMonths(1)))
InvoiceDAL.vb:129:        End Function
InvoiceDAL.vb:130:
InvoiceDAL.vb:131:        ' usuniecie nieoplaconej faktury razem z pozycjami - jedna transakcja
InvoiceDAL.vb:132:        Public Sub Usun(ByVal fakturaId As Integer)
InvoiceDAL.vb:133:            Using polaczenie As New SqlConnection(DbHelper.PobierzConnectionString())
InvoiceDAL.vb:134:                polaczenie.Open()
InvoiceDAL.vb:135:                Dim transakcja As SqlTransaction = polaczenie.BeginTransaction()
InvoiceDAL.vb:136:                Try
InvoiceDAL.vb:137:                    Dim cmdPozycje As New SqlCommand("DELETE FROM PozycjeFaktur WHERE FakturaId = @Id", polaczenie, transakcja)
InvoiceDAL.vb:138:                    cmdPozycje.Parameters.AddWithValue("@Id", fakturaId)
InvoiceDAL.vb:139:                    cmdPozycje.ExecuteNonQuery()
InvoiceDAL.vb:140:                    Dim cmdFaktura As New SqlCommand("DELETE FROM Faktury WHERE Id = @Id AND Status = 'W'", polaczenie, transakcja)
InvoiceDAL.vb:141:                    cmdFaktura.Parameters.AddWithValue("@Id", fakturaId)
InvoiceDAL.vb:142:                    If cmdFaktura.ExecuteNonQuery() = 0 Then
InvoiceDAL.vb:143:                        Throw New BladBiznesowyException("Mozna usunac tylko nieoplacona fakture", "FV_STATUS")
InvoiceDAL.vb:144:                    End If
InvoiceDAL.vb:145:                    transakcja.Commit()
InvoiceDAL.vb:146:                Catch
InvoiceDAL.vb:147:                    transakcja.Rollback()
InvoiceDAL.vb:148:                    Throw
InvoiceDAL.vb:149:                End Try
InvoiceDAL.vb:150:            End Using
InvoiceDAL.vb:151:        End Sub
InvoiceDAL.vb:152:
InvoiceDAL.vb:153:        Public Function CzyIstniejeDlaWypozyczenia(ByVal wypozyczenieId As Integer) As Boolean
InvoiceDAL.vb:154:            Dim tabela As DataTable = DbHelper.WykonajZapytanie("SELECT COUNT(*) AS Ile FROM Faktury WHERE WypozyczenieId = @WypozyczenieId", _
InvoiceDAL.vb:155:                                                                DbHelper.Parametr("@WypozyczenieId", wypozyczenieId))
InvoiceDAL.vb:156:            Return CInt(tabela.Rows(0)("Ile")) > 0
InvoiceDAL.vb:157:        End Function
InvoiceDAL.vb:158:
InvoiceDAL.vb:159:    End Class
InvoiceDAL.vb:160:
InvoiceDAL.vb:161:End Namespace
PricingDAL.vb:1:' Modul: DAL - taryfy i stawki sprzetu | Autor: zespol IT | 2014-03-11
PricingDAL.vb:2:' Historia:
PricingDAL.vb:3:'   2015-02-04 MW: taryfy z data obowiazywania (usp_PobierzTaryfe)
PricingDAL.vb:4:'   2017-06-12 JK: ZaktualizujStawkeSprzetu dla importu cennika XML
PricingDAL.vb:5:
PricingDAL.vb:6:Imports System.Data
PricingDAL.vb:7:Imports System.Data.SqlClient
PricingDAL.vb:8:Imports Wypozyczalnia.Core.Model
PricingDAL.vb:9:
PricingDAL.vb:10:Namespace DAL
PricingDAL.vb:11:
PricingDAL.vb:12:    Public Class PricingDAL
PricingDAL.vb:13:
PricingDAL.vb:14:        Public Function PobierzTaryfe(ByVal kategoria As String, ByVal naDzien As Date) As Taryfa
PricingDAL.vb:15:            Dim dt As DataTable = DbHelper.WykonajProcedure("usp_PobierzTaryfe",
PricingDAL.vb:16:                DbHelper.Parametr("@Kategoria", kategoria),
PricingDAL.vb:17:                DbHelper.Parametr("@NaDzien", naDzien.Date))
PricingDAL.vb:18:            If dt.Rows.Count = 0 Then
PricingDAL.vb:19:                Throw New BladBiznesowyException("Brak taryfy dla kategorii " & kategoria & " na dzien " & naDzien.ToString("yyyy-MM-dd"), "TARYFA_BRAK")
PricingDAL.vb:20:            End If
PricingDAL.vb:21:            Return MapujTaryfe(dt.Rows(0))
PricingDAL.vb:22:        End Function
PricingDAL.vb:23:
PricingDAL.vb:24:        Public Sub ZapiszTaryfe(ByVal t As Taryfa)
PricingDAL.vb:25:            If t Is Nothing Then Throw New ArgumentNullException("t")
PricingDAL.vb:26:            DbHelper.WykonajProcedureBezWyniku("usp_ZapiszTaryfe",
PricingDAL.vb:27:                DbHelper.Parametr("@Kategoria", t.Kategoria),
PricingDAL.vb:28:                DbHelper.Parametr("@MnoznikWeekend", t.MnoznikWeekend),
PricingDAL.vb:29:                DbHelper.Parametr("@RabatTygodniowyProc", t.RabatTygodniowyProc),
PricingDAL.vb:30:                DbHelper.Parametr("@ObowiazujeOd", t.ObowiazujeOd.Date))
PricingDAL.vb:31:        End Sub
PricingDAL.vb:32:
PricingDAL.vb:33:        Public Function ListaTaryf() As DataTable
PricingDAL.vb:34:            Dim sql As String = "SELECT Id, Kategoria, MnoznikWeekend, RabatTygodniowyProc, ObowiazujeOd " &
PricingDAL.vb:35:                                "FROM Taryfy ORDER BY Kategoria, ObowiazujeOd DESC"
PricingDAL.vb:36:            Return DbHelper.WykonajZapytanie(sql)
PricingDAL.vb:37:        End Function
PricingDAL.vb:38:
PricingDAL.vb:39:        Public Function TaryfyKategorii(ByVal kategoria As String) As List(Of Taryfa)
PricingDAL.vb:40:            Dim wynik As New List(Of Taryfa)()
PricingDAL.vb:41:            Dim sql As String = "SELECT Id, Kategoria, MnoznikWeekend, RabatTygodniowyProc, ObowiazujeOd " &
PricingDAL.vb:42:                                "FROM Taryfy WHERE Kategoria = @Kategoria ORDER BY ObowiazujeOd DESC"
PricingDAL.vb:43:            Dim dt As DataTable = DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@Kategoria", kategoria))
PricingDAL.vb:44:            For Each row As DataRow In dt.Rows
PricingDAL.vb:45:                wynik.Add(MapujTaryfe(row))
PricingDAL.vb:46:            Next
PricingDAL.vb:47:            Return wynik
PricingDAL.vb:48:        End Function
PricingDAL.vb:49:
PricingDAL.vb:50:        Public Function UsunTaryfe(ByVal taryfaId As Integer) As Integer
PricingDAL.vb:51:            Dim sql As String = "DELETE FROM Taryfy WHERE Id = @Id"
PricingDAL.vb:52:            Return DbHelper.WykonajPolecenie(sql, DbHelper.Parametr("@Id", taryfaId))
PricingDAL.vb:53:        End Function
PricingDAL.vb:54:
PricingDAL.vb:55:        Public Function ZaktualizujStawkeSprzetu(ByVal numerInwentarzowy As String, ByVal stawka As Decimal, ByVal kaucja As Decimal) As Integer
PricingDAL.vb:56:            Dim sql As String = "UPDATE Sprzet SET StawkaDobowa = @Stawka, Kaucja = @Kaucja " &
PricingDAL.vb:57:                                "WHERE NumerInwentarzowy = @Numer AND Status <> 'X'"
PricingDAL.vb:58:            Return DbHelper.WykonajPolecenie(sql,
PricingDAL.vb:59:                DbHelper.Parametr("@Stawka", stawka),
PricingDAL.vb:60:                DbHelper.Parametr("@Kaucja", kaucja),
PricingDAL.vb:61:                DbHelper.Parametr("@Numer", numerInwentarzowy))
PricingDAL.vb:62:        End Function
PricingDAL.vb:63:
PricingDAL.vb:64:        Public Function StawkaSprzetu(ByVal sprzetId As Integer) As Decimal
PricingDAL.vb:65:            Dim sql As String = "SELECT StawkaDobowa FROM Sprzet WHERE Id = @Id"
PricingDAL.vb:66:            Dim dt As DataTable = DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@Id", sprzetId))
PricingDAL.vb:67:            If dt.Rows.Count = 0 Then Return 0D
PricingDAL.vb:68:            Return CDec(dt.Rows(0)("StawkaDobowa"))
PricingDAL.vb:69:        End Function
PricingDAL.vb:70:
PricingDAL.vb:71:        Public Sub ZapiszWDzienniku(ByVal operacja As String, ByVal szczegoly As String)
PricingDAL.vb:72:            Dim sql As String = "INSERT INTO DziennikOperacji (Kiedy, Operacja, Szczegoly) VALUES (GETDATE(), @Operacja, @Szczegoly)"
PricingDAL.vb:73:            If szczegoly IsNot Nothing AndAlso szczegoly.Length > 400 Then szczegoly = szczegoly.Substring(0, 400)
PricingDAL.vb:74:            DbHelper.WykonajPolecenie(sql,
PricingDAL.vb:75:                DbHelper.Parametr("@Operacja", operacja),
PricingDAL.vb:76:                DbHelper.Parametr("@Szczegoly", szczegoly))
PricingDAL.vb:77:        End Sub
PricingDAL.vb:78:
PricingDAL.vb:79:        Public Function TaryfaIstnieje(ByVal kategoria As String, ByVal obowiazujeOd As Date) As Boolean
PricingDAL.vb:80:            Dim sql As String = "SELECT COUNT(*) AS Ile FROM Taryfy WHERE Kategoria = @Kategoria AND ObowiazujeOd = @Od"
PricingDAL.vb:81:            Dim dt As DataTable = DbHelper.WykonajZapytanie(sql,
PricingDAL.vb:82:                DbHelper.Parametr("@Kategoria", kategoria),
PricingDAL.vb:83:                DbHelper.Parametr("@Od", obowiazujeOd.Date))
PricingDAL.vb:84:            Return CInt(dt.Rows(0)("Ile")) > 0
PricingDAL.vb:85:        End Function
PricingDAL.vb:86:
PricingDAL.vb:87:        Public Function TaryfyNaDzien(ByVal naDzien As Date) As List(Of Taryfa)
PricingDAL.vb:88:            Dim wynik As New List(Of Taryfa)()
PricingDAL.vb:89:            For Each kategoria As String In Taryfa.KategorieZnane
PricingDAL.vb:90:                Try
PricingDAL.vb:91:                    wynik.Add(PobierzTaryfe(kategoria, naDzien))
PricingDAL.vb:92:                Catch ex As BladBiznesowyException
PricingDAL.vb:93:                    ' brak taryfy dla kategorii - pomijamy
PricingDAL.vb:94:                End Try
PricingDAL.vb:95:            Next
PricingDAL.vb:96:            Return wynik
PricingDAL.vb:97:        End Function
PricingDAL.vb:98:
PricingDAL.vb:99:        Public Function ListaSprzetuZeStawkami(ByVal kategoria As String) As DataTable
PricingDAL.vb:100:            Dim sql As String = "SELECT Id, NumerInwentarzowy, Nazwa, Kategoria, StawkaDobowa, Kaucja, Status " &
PricingDAL.vb:101:                                "FROM Sprzet WHERE Status <> 'X'"
PricingDAL.vb:102:            If Not String.IsNullOrEmpty(kategoria) Then
PricingDAL.vb:103:                sql = sql & " AND Kategoria = @Kategoria ORDER BY NumerInwentarzowy"
PricingDAL.vb:104:                Return DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@Kategoria", kategoria))
PricingDAL.vb:105:            End If
PricingDAL.vb:106:            sql = sql & " ORDER BY Kategoria, NumerInwentarzowy"
PricingDAL.vb:107:            Return DbHelper.WykonajZapytanie(sql)
PricingDAL.vb:108:        End Function
PricingDAL.vb:109:
PricingDAL.vb:110:        Public Function HistoriaZmianCennika(ByVal odDnia As Date) As DataTable
PricingDAL.vb:111:            Dim sql As String = "SELECT Id, Kiedy, Operacja, Szczegoly FROM DziennikOperacji " &
PricingDAL.vb:112:                                "WHERE Operacja IN ('TARYFA', 'STAWKA', 'IMPORT_CENNIKA') AND Kiedy >= @Od " &
PricingDAL.vb:113:                                "ORDER BY Kiedy DESC"
PricingDAL.vb:114:            Return DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@Od", odDnia.Date))
PricingDAL.vb:115:        End Function
PricingDAL.vb:116:
PricingDAL.vb:117:        Public Function ZaktualizujKaucje(ByVal sprzetId As Integer, ByVal kaucja As Decimal) As Integer
PricingDAL.vb:118:            Dim sql As String = "UPDATE Sprzet SET Kaucja = @Kaucja WHERE Id = @Id"
PricingDAL.vb:119:            Return DbHelper.WykonajPolecenie(sql,
PricingDAL.vb:120:                DbHelper.Parametr("@Kaucja", kaucja),
PricingDAL.vb:121:                DbHelper.Parametr("@Id", sprzetId))
PricingDAL.vb:122:        End Function
PricingDAL.vb:123:
PricingDAL.vb:124:        Public Function PobierzTaryfePoId(ByVal taryfaId As Integer) As Taryfa
PricingDAL.vb:125:            Dim sql As String = "SELECT Id, Kategoria, MnoznikWeekend, RabatTygodniowyProc, ObowiazujeOd FROM Taryfy WHERE Id = @Id"
PricingDAL.vb:126:            Dim dt As DataTable = DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@Id", taryfaId))
PricingDAL.vb:127:            If dt.Rows.Count = 0 Then Return Nothing
PricingDAL.vb:128:            Return MapujTaryfe(dt.Rows(0))
PricingDAL.vb:129:        End Function
PricingDAL.vb:130:
PricingDAL.vb:131:        Public Function KategorieZTaryfami() As List(Of String)
PricingDAL.vb:132:            Dim wynik As New List(Of String)()
PricingDAL.vb:133:            Dim dt As DataTable = DbHelper.WykonajZapytanie("SELECT DISTINCT Kategoria FROM Taryfy ORDER BY Kategoria")
PricingDAL.vb:134:            For Each row As DataRow In dt.Rows
PricingDAL.vb:135:                wynik.Add(CStr(row("Kategoria")).Trim())
PricingDAL.vb:136:            Next
PricingDAL.vb:137:            Return wynik
PricingDAL.vb:138:        End Function
PricingDAL.vb:139:
PricingDAL.vb:140:        Private Function MapujTaryfe(ByVal row As DataRow) As Taryfa
PricingDAL.vb:141:            Dim t As New Taryfa()
PricingDAL.vb:142:            t.Id = CInt(row("Id"))
PricingDAL.vb:143:            t.Kategoria = CStr(row("Kategoria")).Trim()
PricingDAL.vb:144:            t.MnoznikWeekend = CDec(row("MnoznikWeekend"))
PricingDAL.vb:145:            t.RabatTygodniowyProc = CDec(row("RabatTygodniowyProc"))
PricingDAL.vb:146:            t.ObowiazujeOd = CDate(row("ObowiazujeOd"))
PricingDAL.vb:147:            Return t
PricingDAL.vb:148:        End Function
PricingDAL.vb:149:
PricingDAL.vb:150:    End Class
PricingDAL.vb:151:
PricingDAL.vb:152:End Namespace
RentalDAL.vb:1:' Modul: DAL\RentalDAL.vb | Autor: zespol IT | 2014-03-11
RentalDAL.vb:2:' Dostep do tabeli Wypozyczenia (wydanie i zwrot przez procedury, odczyty inline).
RentalDAL.vb:3:' 2015-06-22 MW: UstawNumerPrzesylki
RentalDAL.vb:4:' 2018-02-09 PT: WypozyczeniaZDnia dla eksportu nocnego
RentalDAL.vb:5:
RentalDAL.vb:6:Imports System.Data
RentalDAL.vb:7:Imports System.Data.SqlClient
RentalDAL.vb:8:Imports Wypozyczalnia.Core.Model
RentalDAL.vb:9:
RentalDAL.vb:10:Namespace DAL
RentalDAL.vb:11:
RentalDAL.vb:12:    Public Class RentalDAL
RentalDAL.vb:13:
RentalDAL.vb:14:        Private Const SqlKolumny As String =
RentalDAL.vb:15:            "SELECT w.Id, w.RezerwacjaId, w.KlientId, w.SprzetId, w.DataWydania, w.PlanowanyZwrot, w.DataZwrotu, " &
RentalDAL.vb:16:            "w.KaucjaPobrana, w.Oplata, w.Kara, w.KaucjaZwrocona, w.Status, w.NumerPrzesylki FROM Wypozyczenia w "
RentalDAL.vb:17:
RentalDAL.vb:18:        Private Const SqlNazwaKlienta As String =
RentalDAL.vb:19:            "CASE WHEN k.Typ = 'F' AND k.NazwaFirmy IS NOT NULL THEN k.NazwaFirmy ELSE k.Imie + ' ' + k.Nazwisko END"
RentalDAL.vb:20:
RentalDAL.vb:21:        Public Function Wydaj(ByVal w As Wypozyczenie) As Integer
RentalDAL.vb:22:            Dim noweId As SqlParameter = DbHelper.ParametrWyjsciowy("@NoweId", SqlDbType.Int)
RentalDAL.vb:23:            Dim rezerwacja As Object = Nothing
RentalDAL.vb:24:            If w.RezerwacjaId > 0 Then rezerwacja = w.RezerwacjaId
RentalDAL.vb:25:            DbHelper.WykonajProcedureBezWyniku("usp_WydajSprzet",
RentalDAL.vb:26:                DbHelper.Parametr("@RezerwacjaId", rezerwacja),
RentalDAL.vb:27:                DbHelper.Parametr("@KlientId", w.KlientId),
RentalDAL.vb:28:                DbHelper.Parametr("@SprzetId", w.SprzetId),
RentalDAL.vb:29:                DbHelper.Parametr("@DataWydania", w.DataWydania),
RentalDAL.vb:30:                DbHelper.Parametr("@PlanowanyZwrot", w.PlanowanyZwrot),
RentalDAL.vb:31:                DbHelper.Parametr("@KaucjaPobrana", w.KaucjaPobrana),
RentalDAL.vb:32:                noweId)
RentalDAL.vb:33:            w.Id = CInt(noweId.Value)
RentalDAL.vb:34:            Return w.Id
RentalDAL.vb:35:        End Function
RentalDAL.vb:36:
RentalDAL.vb:37:        Public Sub ZapiszZwrot(ByVal w As Wypozyczenie)
RentalDAL.vb:38:            If Not w.DataZwrotu.HasValue Then
RentalDAL.vb:39:                Throw New ArgumentException("Brak daty zwrotu dla wypozyczenia " & w.Id)
RentalDAL.vb:40:            End If
RentalDAL.vb:41:            DbHelper.WykonajProcedureBezWyniku("usp_ZarejestrujZwrot",
RentalDAL.vb:42:                DbHelper.Parametr("@WypozyczenieId", w.Id),
RentalDAL.vb:43:                DbHelper.Parametr("@DataZwrotu", w.DataZwrotu.Value),
RentalDAL.vb:44:                DbHelper.Parametr("@Oplata", w.Oplata),
RentalDAL.vb:45:                DbHelper.Parametr("@Kara", w.Kara),
RentalDAL.vb:46:                DbHelper.Parametr("@KaucjaZwrocona", w.KaucjaZwrocona))
RentalDAL.vb:47:        End Sub
RentalDAL.vb:48:
RentalDAL.vb:49:        Public Function Pobierz(ByVal wypozyczenieId As Integer) As DataRow
RentalDAL.vb:50:            Dim tabela As DataTable = DbHelper.WykonajZapytanie(SqlKolumny & "WHERE w.Id = @Id",
RentalDAL.vb:51:                DbHelper.Parametr("@Id", wypozyczenieId))
RentalDAL.vb:52:            If tabela.Rows.Count = 0 Then Return Nothing
RentalDAL.vb:53:            Return tabela.Rows(0)
RentalDAL.vb:54:        End Function
RentalDAL.vb:55:
RentalDAL.vb:56:        Public Function PobierzWypozyczenie(ByVal wypozyczenieId As Integer) As Wypozyczenie
RentalDAL.vb:57:            Dim wiersz As DataRow = Pobierz(wypozyczenieId)
RentalDAL.vb:58:            If wiersz Is Nothing Then Return Nothing
RentalDAL.vb:59:            Return Wypozyczenie.ZWiersza(wiersz)
RentalDAL.vb:60:        End Function
RentalDAL.vb:61:
RentalDAL.vb:62:        Public Sub UstawNumerPrzesylki(ByVal wypozyczenieId As Integer, ByVal numerPrzesylki As String)
RentalDAL.vb:63:            DbHelper.WykonajPolecenie("UPDATE Wypozyczenia SET NumerPrzesylki = @Numer WHERE Id = @Id",
RentalDAL.vb:64:                DbHelper.Parametr("@Numer", numerPrzesylki),
RentalDAL.vb:65:                DbHelper.Parametr("@Id", wypozyczenieId))
RentalDAL.vb:66:        End Sub
RentalDAL.vb:67:
RentalDAL.vb:68:        Public Function Otwarte() As DataTable
RentalDAL.vb:69:            Dim sql As String =
RentalDAL.vb:70:                "SELECT w.Id, " & SqlNazwaKlienta & " AS Klient, s.NumerInwentarzowy + ' ' + s.Nazwa AS Sprzet, " &
RentalDAL.vb:71:                "w.DataWydania, w.PlanowanyZwrot " &
RentalDAL.vb:72:                "FROM Wypozyczenia w " &
RentalDAL.vb:73:                "INNER JOIN Klienci k ON k.Id = w.KlientId " &
RentalDAL.vb:74:                "INNER JOIN Sprzet s ON s.Id = w.SprzetId " &
RentalDAL.vb:75:                "WHERE w.Status = 'O' " &
RentalDAL.vb:76:                "ORDER BY w.PlanowanyZwrot"
RentalDAL.vb:77:            Return DbHelper.WykonajZapytanie(sql)
RentalDAL.vb:78:        End Function
RentalDAL.vb:79:
RentalDAL.vb:80:        Public Function WypozyczeniaZDnia(ByVal dzien As Date) As DataTable
RentalDAL.vb:81:            Dim od As Date = dzien.Date
RentalDAL.vb:82:            Dim doDnia As Date = od.AddDays(1)
RentalDAL.vb:83:            Dim sql As String =
RentalDAL.vb:84:                "SELECT w.Id, w.KlientId, " & SqlNazwaKlienta & " AS Klient, w.SprzetId, s.Nazwa AS Sprzet, " &
RentalDAL.vb:85:                "w.DataWydania, w.PlanowanyZwrot, w.DataZwrotu, w.Oplata, w.Kara " &
RentalDAL.vb:86:                "FROM Wypozyczenia w " &
RentalDAL.vb:87:                "INNER JOIN Klienci k ON k.Id = w.KlientId " &
RentalDAL.vb:88:                "INNER JOIN Sprzet s ON s.Id = w.SprzetId " &
RentalDAL.vb:89:                "WHERE (w.DataWydania >= @Od AND w.DataWydania < @Do) " &
RentalDAL.vb:90:                "   OR (w.DataZwrotu >= @Od AND w.DataZwrotu < @Do) " &
RentalDAL.vb:91:                "ORDER BY w.Id"
RentalDAL.vb:92:            Return DbHelper.WykonajZapytanie(sql,
RentalDAL.vb:93:                DbHelper.Parametr("@Od", od),
RentalDAL.vb:94:                DbHelper.Parametr("@Do", doDnia))
RentalDAL.vb:95:        End Function
RentalDAL.vb:96:
RentalDAL.vb:97:        Public Function WypozyczeniaKlienta(ByVal klientId As Integer) As DataTable
RentalDAL.vb:98:            Dim sql As String =
RentalDAL.vb:99:                "SELECT w.Id, s.Nazwa AS Sprzet, w.DataWydania, w.PlanowanyZwrot, w.DataZwrotu, w.Status " &
RentalDAL.vb:100:                "FROM Wypozyczenia w " &
RentalDAL.vb:101:                "INNER JOIN Sprzet s ON s.Id = w.SprzetId " &
RentalDAL.vb:102:                "WHERE w.KlientId = @KlientId " &
RentalDAL.vb:103:                "ORDER BY w.DataWydania DESC"
RentalDAL.vb:104:            Return DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@KlientId", klientId))
RentalDAL.vb:105:        End Function
RentalDAL.vb:106:
RentalDAL.vb:107:        Public Function PoTerminie(ByVal naDzien As Date) As DataTable
RentalDAL.vb:108:            Dim sql As String =
RentalDAL.vb:109:                "SELECT w.Id, w.KlientId, w.SprzetId, w.PlanowanyZwrot, " &
RentalDAL.vb:110:                "DATEDIFF(MINUTE, w.PlanowanyZwrot, @NaDzien) AS MinutySpoznienia " &
RentalDAL.vb:111:                "FROM Wypozyczenia w " &
RentalDAL.vb:112:                "WHERE w.Status = 'O' AND w.PlanowanyZwrot < @NaDzien " &
RentalDAL.vb:113:                "ORDER BY w.PlanowanyZwrot"
RentalDAL.vb:114:            Return DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@NaDzien", naDzien))
RentalDAL.vb:115:        End Function
RentalDAL.vb:116:
RentalDAL.vb:117:        Public Sub ZmienPlanowanyZwrot(ByVal wypozyczenieId As Integer, ByVal nowyTermin As Date)
RentalDAL.vb:118:            Dim zmienione As Integer = DbHelper.WykonajPolecenie(
RentalDAL.vb:119:                "UPDATE Wypozyczenia SET PlanowanyZwrot = @Termin WHERE Id = @Id AND Status = 'O'",
RentalDAL.vb:120:                DbHelper.Parametr("@Termin", nowyTermin),
RentalDAL.vb:121:                DbHelper.Parametr("@Id", wypozyczenieId))
RentalDAL.vb:122:            If zmienione = 0 Then
RentalDAL.vb:123:                Throw New BladBiznesowyException("Nie zmieniono terminu wypozyczenia " & wypozyczenieId)
RentalDAL.vb:124:            End If
RentalDAL.vb:125:        End Sub
RentalDAL.vb:126:
RentalDAL.vb:127:        Public Function DoZwrotuWOkresie(ByVal odDaty As Date, ByVal doDaty As Date) As List(Of Wypozyczenie)
RentalDAL.vb:128:            Dim tabela As DataTable = DbHelper.WykonajZapytanie(
RentalDAL.vb:129:                SqlKolumny & "WHERE w.Status = 'O' AND w.PlanowanyZwrot >= @Od AND w.PlanowanyZwrot < @Do ORDER BY w.PlanowanyZwrot",
RentalDAL.vb:130:                DbHelper.Parametr("@Od", odDaty),
RentalDAL.vb:131:                DbHelper.Parametr("@Do", doDaty))
RentalDAL.vb:132:            Return Mapuj(tabela)
RentalDAL.vb:133:        End Function
RentalDAL.vb:134:
RentalDAL.vb:135:        Public Function OtwarteKlienta(ByVal klientId As Integer) As List(Of Wypozyczenie)
RentalDAL.vb:136:            Dim tabela As DataTable = DbHelper.WykonajZapytanie(
RentalDAL.vb:137:                SqlKolumny & "WHERE w.KlientId = @KlientId AND w.Status = 'O' ORDER BY w.DataWydania",
RentalDAL.vb:138:                DbHelper.Parametr("@KlientId", klientId))
RentalDAL.vb:139:            Return Mapuj(tabela)
RentalDAL.vb:140:        End Function
RentalDAL.vb:141:
RentalDAL.vb:142:        Public Function OstatnieDlaSprzetu(ByVal sprzetId As Integer, ByVal ile As Integer) As List(Of Wypozyczenie)
RentalDAL.vb:143:            Dim tabela As DataTable = DbHelper.WykonajZapytanie(
RentalDAL.vb:144:                "SELECT TOP (@Ile) " & SqlKolumny.Substring(7) & "WHERE w.SprzetId = @SprzetId ORDER BY w.DataWydania DESC",
RentalDAL.vb:145:                DbHelper.Parametr("@Ile", ile),
RentalDAL.vb:146:                DbHelper.Parametr("@SprzetId", sprzetId))
RentalDAL.vb:147:            Return Mapuj(tabela)
RentalDAL.vb:148:        End Function
RentalDAL.vb:149:
RentalDAL.vb:150:        Private Function Mapuj(ByVal tabela As DataTable) As List(Of Wypozyczenie)
RentalDAL.vb:151:            Dim lista As New List(Of Wypozyczenie)
RentalDAL.vb:152:            For Each wiersz As DataRow In tabela.Rows
RentalDAL.vb:153:                lista.Add(Wypozyczenie.ZWiersza(wiersz))
RentalDAL.vb:154:            Next
RentalDAL.vb:155:            Return lista
RentalDAL.vb:156:        End Function
RentalDAL.vb:157:
RentalDAL.vb:158:        Public Function CzyOtwarteDlaSprzetu(ByVal sprzetId As Integer) As Boolean
RentalDAL.vb:159:            Dim tabela As DataTable = DbHelper.WykonajZapytanie(
RentalDAL.vb:160:                "SELECT COUNT(*) AS Ile FROM Wypozyczenia WHERE SprzetId = @SprzetId AND Status = 'O'",
RentalDAL.vb:161:                DbHelper.Parametr("@SprzetId", sprzetId))
RentalDAL.vb:162:            Return CInt(tabela.Rows(0)("Ile")) > 0
RentalDAL.vb:163:        End Function
RentalDAL.vb:164:
RentalDAL.vb:165:        Public Function LiczbaOtwartych() As Integer
RentalDAL.vb:166:            Dim tabela As DataTable = DbHelper.WykonajZapytanie(
RentalDAL.vb:167:                "SELECT COUNT(*) AS Ile FROM Wypozyczenia WHERE Status = 'O'")
RentalDAL.vb:168:            Return CInt(tabela.Rows(0)("Ile"))
RentalDAL.vb:169:        End Function
RentalDAL.vb:170:
RentalDAL.vb:171:        Public Function SumaKaucjiOtwartych() As Decimal
RentalDAL.vb:172:            Dim tabela As DataTable = DbHelper.WykonajZapytanie(
RentalDAL.vb:173:                "SELECT ISNULL(SUM(KaucjaPobrana), 0) AS Suma FROM Wypozyczenia WHERE Status = 'O'")
RentalDAL.vb:174:            Return CDec(tabela.Rows(0)("Suma"))
RentalDAL.vb:175:        End Function
RentalDAL.vb:176:
RentalDAL.vb:177:        Public Function SumaKarZOkresu(ByVal odDaty As Date, ByVal doDaty As Date) As Decimal
RentalDAL.vb:178:            Dim tabela As DataTable = DbHelper.WykonajZapytanie(
RentalDAL.vb:179:                "SELECT ISNULL(SUM(Kara), 0) AS Suma FROM Wypozyczenia " &
RentalDAL.vb:180:                "WHERE Status = 'Z' AND DataZwrotu >= @Od AND DataZwrotu < @Do",
RentalDAL.vb:181:                DbHelper.Parametr("@Od", odDaty),
RentalDAL.vb:182:                DbHelper.Parametr("@Do", doDaty))
RentalDAL.vb:183:            Return CDec(tabela.Rows(0)("Suma"))
RentalDAL.vb:184:        End Function
RentalDAL.vb:185:
RentalDAL.vb:186:        Public Sub ZapiszDziennik(ByVal operacja As String, ByVal szczegoly As String)
RentalDAL.vb:187:            Try
RentalDAL.vb:188:                DbHelper.WykonajPolecenie(
RentalDAL.vb:189:                    "INSERT INTO DziennikOperacji (Kiedy, Operacja, Szczegoly) VALUES (GETDATE(), @Operacja, @Szczegoly)",
RentalDAL.vb:190:                    DbHelper.Parametr("@Operacja", operacja),
RentalDAL.vb:191:                    DbHelper.Parametr("@Szczegoly", szczegoly))
RentalDAL.vb:192:            Catch ex As SqlException
RentalDAL.vb:193:                ' dziennik nie moze blokowac wydania ani zwrotu
RentalDAL.vb:194:                System.Diagnostics.Trace.WriteLine("DziennikOperacji: " & ex.Message)
RentalDAL.vb:195:            End Try
RentalDAL.vb:196:        End Sub
RentalDAL.vb:197:
RentalDAL.vb:198:    End Class
RentalDAL.vb:199:
RentalDAL.vb:200:End Namespace
ReservationDAL.vb:1:' Modul: DAL - dostep do tabeli Rezerwacje | Autor: zespol IT | 2014-03-11
ReservationDAL.vb:2:' Historia:
ReservationDAL.vb:3:'   2014-09-22 JK  zapis przez procedure usp_ZapiszRezerwacje (wpis do dziennika)
ReservationDAL.vb:4:'   2016-11-08 PT  kolizje liczone w bazie (usp_SprawdzDostepnosc) zamiast petli w VB
ReservationDAL.vb:5:
ReservationDAL.vb:6:Imports System.Data
ReservationDAL.vb:7:Imports System.Data.SqlClient
ReservationDAL.vb:8:Imports Wypozyczalnia.Core.Model
ReservationDAL.vb:9:
ReservationDAL.vb:10:Namespace DAL
ReservationDAL.vb:11:
ReservationDAL.vb:12:    Public Class ReservationDAL
ReservationDAL.vb:13:
ReservationDAL.vb:14:        Private Const KolumnyRezerwacji As String = "r.Id, r.KlientId, r.SprzetId, r.DataOd, r.DataDo, r.Status, r.CenaSzacunkowa, r.Uwagi, r.Utworzono"
ReservationDAL.vb:15:
ReservationDAL.vb:16:        ' wynik: Zrodlo, Id, DataOd, DataDo - kazdy wiersz oznacza kolizje
ReservationDAL.vb:17:        Public Function PobierzKolizje(ByVal sprzetId As Integer, ByVal dataOd As Date, ByVal dataDo As Date) As DataTable
ReservationDAL.vb:18:            Return DbHelper.WykonajProcedure("usp_SprawdzDostepnosc", _
ReservationDAL.vb:19:                                             DbHelper.Parametr("@SprzetId", sprzetId), _
ReservationDAL.vb:20:                                             DbHelper.Parametr("@DataOd", dataOd), _
ReservationDAL.vb:21:                                             DbHelper.Parametr("@DataDo", dataDo))
ReservationDAL.vb:22:        End Function
ReservationDAL.vb:23:
ReservationDAL.vb:24:        Public Function Zapisz(ByVal r As Rezerwacja) As Integer
ReservationDAL.vb:25:            Dim noweId As SqlParameter = DbHelper.ParametrWyjsciowy("@NoweId", SqlDbType.Int)
ReservationDAL.vb:26:            Dim uwagi As Object = Nothing
ReservationDAL.vb:27:            If Not String.IsNullOrEmpty(r.Uwagi) Then
ReservationDAL.vb:28:                uwagi = r.Uwagi
ReservationDAL.vb:29:            End If
ReservationDAL.vb:30:            DbHelper.WykonajProcedureBezWyniku("usp_ZapiszRezerwacje", _
ReservationDAL.vb:31:                                               DbHelper.Parametr("@KlientId", r.KlientId), _
ReservationDAL.vb:32:                                               DbHelper.Parametr("@SprzetId", r.SprzetId), _
ReservationDAL.vb:33:                                               DbHelper.Parametr("@DataOd", r.DataOd), _
ReservationDAL.vb:34:                                               DbHelper.Parametr("@DataDo", r.DataDo), _
ReservationDAL.vb:35:                                               DbHelper.Parametr("@CenaSzacunkowa", r.CenaSzacunkowa), _
ReservationDAL.vb:36:                                               DbHelper.Parametr("@Uwagi", uwagi), _
ReservationDAL.vb:37:                                               noweId)
ReservationDAL.vb:38:            If noweId.Value Is Nothing OrElse IsDBNull(noweId.Value) Then
ReservationDAL.vb:39:                Throw New ApplicationException("Procedura usp_ZapiszRezerwacje nie zwrocila Id")
ReservationDAL.vb:40:            End If
ReservationDAL.vb:41:            Return CInt(noweId.Value)
ReservationDAL.vb:42:        End Function
ReservationDAL.vb:43:
ReservationDAL.vb:44:        Public Function Pobierz(ByVal rezerwacjaId As Integer) As DataRow
ReservationDAL.vb:45:            Dim sql As String = "SELECT " & KolumnyRezerwacji & " FROM Rezerwacje r WHERE r.Id = @Id"
ReservationDAL.vb:46:            Dim tabela As DataTable = DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@Id", rezerwacjaId))
ReservationDAL.vb:47:            If tabela.Rows.Count = 0 Then
ReservationDAL.vb:48:                Return Nothing
ReservationDAL.vb:49:            End If
ReservationDAL.vb:50:            Return tabela.Rows(0)
ReservationDAL.vb:51:        End Function
ReservationDAL.vb:52:
ReservationDAL.vb:53:        Public Sub ZmienStatus(ByVal rezerwacjaId As Integer, ByVal status As String)
ReservationDAL.vb:54:            DbHelper.WykonajProcedureBezWyniku("usp_ZmienStatusRezerwacji", _
ReservationDAL.vb:55:                                               DbHelper.Parametr("@RezerwacjaId", rezerwacjaId), _
ReservationDAL.vb:56:                                               DbHelper.Parametr("@Status", status))
ReservationDAL.vb:57:        End Sub
ReservationDAL.vb:58:
ReservationDAL.vb:59:        ' kolumny: Id, SprzetId, Sprzet, DataOd, DataDo, Status, CenaSzacunkowa
ReservationDAL.vb:60:        Public Function RezerwacjeKlienta(ByVal klientId As Integer) As DataTable
ReservationDAL.vb:61:            Dim sql As String = "SELECT r.Id, r.SprzetId, s.Nazwa AS Sprzet, r.DataOd, r.DataDo, r.Status, r.CenaSzacunkowa " & _
ReservationDAL.vb:62:                                "FROM Rezerwacje r INNER JOIN Sprzet s ON s.Id = r.SprzetId " & _
ReservationDAL.vb:63:                                "WHERE r.KlientId = @KlientId " & _
ReservationDAL.vb:64:                                "ORDER BY r.DataOd DESC"
ReservationDAL.vb:65:            Return DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@KlientId", klientId))
ReservationDAL.vb:66:        End Function
ReservationDAL.vb:67:
ReservationDAL.vb:68:        Public Function RezerwacjeSprzetu(ByVal sprzetId As Integer, ByVal odDnia As Date) As DataTable
ReservationDAL.vb:69:            Dim sql As String = "SELECT r.Id, r.KlientId, k.Nazwisko AS Klient, r.DataOd, r.DataDo, r.Status " & _
ReservationDAL.vb:70:                                "FROM Rezerwacje r INNER JOIN Klienci k ON k.Id = r.KlientId " & _
ReservationDAL.vb:71:                                "WHERE r.SprzetId = @SprzetId AND r.DataDo >= @OdDnia AND r.Status IN ('N', 'P') " & _
ReservationDAL.vb:72:                                "ORDER BY r.DataOd"
ReservationDAL.vb:73:            Return DbHelper.WykonajZapytanie(sql, _
ReservationDAL.vb:74:                                             DbHelper.Parametr("@SprzetId", sprzetId), _
ReservationDAL.vb:75:                                             DbHelper.Parametr("@OdDnia", odDnia))
ReservationDAL.vb:76:        End Function
ReservationDAL.vb:77:
ReservationDAL.vb:78:        Public Function RezerwacjeWOkresie(ByVal dataOd As Date, ByVal dataDo As Date) As DataTable
ReservationDAL.vb:79:            Dim sql As String = "SELECT r.Id, r.KlientId, r.SprzetId, s.Nazwa AS Sprzet, r.DataOd, r.DataDo, r.Status, r.CenaSzacunkowa " & _
ReservationDAL.vb:80:                                "FROM Rezerwacje r INNER JOIN Sprzet s ON s.Id = r.SprzetId " & _
ReservationDAL.vb:81:                                "WHERE r.DataOd < @DataDo AND r.DataDo > @DataOd " & _
ReservationDAL.vb:82:                                "ORDER BY r.DataOd, s.NumerInwentarzowy"
ReservationDAL.vb:83:            Return DbHelper.WykonajZapytanie(sql, _
ReservationDAL.vb:84:                                             DbHelper.Parametr("@DataOd", dataOd), _
ReservationDAL.vb:85:                                             DbHelper.Parametr("@DataDo", dataDo))
ReservationDAL.vb:86:        End Function
ReservationDAL.vb:87:
ReservationDAL.vb:88:        Public Function LiczbaAktywnychKlienta(ByVal klientId As Integer) As Integer
ReservationDAL.vb:89:            Dim sql As String = "SELECT COUNT(*) AS Ile FROM Rezerwacje WHERE KlientId = @KlientId AND Status IN ('N', 'P')"
ReservationDAL.vb:90:            Dim tabela As DataTable = DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@KlientId", klientId))
ReservationDAL.vb:91:            Return CInt(tabela.Rows(0)("Ile"))
ReservationDAL.vb:92:        End Function
ReservationDAL.vb:93:
ReservationDAL.vb:94:        Public Function PrzeterminowaneNowe(ByVal naDzien As Date) As DataTable
ReservationDAL.vb:95:            Dim sql As String = "SELECT r.Id, r.KlientId, r.SprzetId, r.DataOd, r.DataDo, r.Status " & _
ReservationDAL.vb:96:                                "FROM Rezerwacje r " & _
ReservationDAL.vb:97:                                "WHERE r.Status = 'N' AND r.DataOd < @NaDzien " & _
ReservationDAL.vb:98:                                "ORDER BY r.DataOd"
ReservationDAL.vb:99:            Return DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@NaDzien", naDzien))
ReservationDAL.vb:100:        End Function
ReservationDAL.vb:101:
ReservationDAL.vb:102:        ' jak PobierzKolizje, ale bez wierszy samej rezerwacji (zmiana terminu istniejacej)
ReservationDAL.vb:103:        Public Function PobierzKolizjeBezRezerwacji(ByVal sprzetId As Integer, ByVal dataOd As Date, ByVal dataDo As Date, ByVal pominRezerwacjaId As Integer) As DataTable
ReservationDAL.vb:104:            Dim kolizje As DataTable = PobierzKolizje(sprzetId, dataOd, dataDo)
ReservationDAL.vb:105:            For i As Integer = kolizje.Rows.Count - 1 To 0 Step -1
ReservationDAL.vb:106:                Dim wiersz As DataRow = kolizje.Rows(i)
ReservationDAL.vb:107:                If CStr(wiersz("Zrodlo")) = "R" AndAlso CInt(wiersz("Id")) = pominRezerwacjaId Then
ReservationDAL.vb:108:                    kolizje.Rows.RemoveAt(i)
ReservationDAL.vb:109:                End If
ReservationDAL.vb:110:            Next
ReservationDAL.vb:111:            Return kolizje
ReservationDAL.vb:112:        End Function
ReservationDAL.vb:113:
ReservationDAL.vb:114:        Public Sub ZmienTermin(ByVal rezerwacjaId As Integer, ByVal dataOd As Date, ByVal dataDo As Date, ByVal cenaSzacunkowa As Decimal)
ReservationDAL.vb:115:            Dim sql As String = "UPDATE Rezerwacje SET DataOd = @DataOd, DataDo = @DataDo, CenaSzacunkowa = @Cena WHERE Id = @Id"
ReservationDAL.vb:116:            DbHelper.WykonajPolecenie(sql, _
ReservationDAL.vb:117:                                      DbHelper.Parametr("@DataOd", dataOd), _
ReservationDAL.vb:118:                                      DbHelper.Parametr("@DataDo", dataDo), _
ReservationDAL.vb:119:                                      DbHelper.Parametr("@Cena", cenaSzacunkowa), _
ReservationDAL.vb:120:                                      DbHelper.Parametr("@Id", rezerwacjaId))
ReservationDAL.vb:121:        End Sub
ReservationDAL.vb:122:
ReservationDAL.vb:123:        ' kolumny: Id, KlientId, Klient, SprzetId, Sprzet, DataOd, DataDo, Status
ReservationDAL.vb:124:        Public Function RezerwacjeNaDzien(ByVal dzien As Date) As DataTable
ReservationDAL.vb:125:            Dim sql As String = "SELECT r.Id, r.KlientId, k.Nazwisko AS Klient, r.SprzetId, s.Nazwa AS Sprzet, r.DataOd, r.DataDo, r.Status " & _
ReservationDAL.vb:126:                                "FROM Rezerwacje r " & _
ReservationDAL.vb:127:                                "INNER JOIN Klienci k ON k.Id = r.KlientId " & _
ReservationDAL.vb:128:                                "INNER JOIN Sprzet s ON s.Id = r.SprzetId " & _
ReservationDAL.vb:129:                                "WHERE r.DataOd >= @Od AND r.DataOd < @Do AND r.Status IN ('N', 'P') " & _
ReservationDAL.vb:130:                                "ORDER BY r.DataOd"
ReservationDAL.vb:131:            Return DbHelper.WykonajZapytanie(sql, _
ReservationDAL.vb:132:                                             DbHelper.Parametr("@Od", dzien.Date), _
ReservationDAL.vb:133:                                             DbHelper.Parametr("@Do", dzien.Date.AddDays(1)))
ReservationDAL.vb:134:        End Function
ReservationDAL.vb:135:
ReservationDAL.vb:136:        ' kolumny: Status, Liczba, Suma
ReservationDAL.vb:137:        Public Function Statystyki(ByVal dataOd As Date, ByVal dataDo As Date) As DataTable
ReservationDAL.vb:138:            Dim sql As String = "SELECT Status, COUNT(*) AS Liczba, SUM(CenaSzacunkowa) AS Suma " & _
ReservationDAL.vb:139:                                "FROM Rezerwacje " & _
ReservationDAL.vb:140:                                "WHERE Utworzono >= @Od AND Utworzono < @Do " & _
ReservationDAL.vb:141:                                "GROUP BY Status ORDER BY Status"
ReservationDAL.vb:142:            Return DbHelper.WykonajZapytanie(sql, _
ReservationDAL.vb:143:                                             DbHelper.Parametr("@Od", dataOd), _
ReservationDAL.vb:144:                                             DbHelper.Parametr("@Do", dataDo))
ReservationDAL.vb:145:        End Function
ReservationDAL.vb:146:
ReservationDAL.vb:147:        ' kolumny: Id, DataOd, DataDo, Status (rezerwacje sprzetu zachodzace na miesiac)
ReservationDAL.vb:148:        Public Function KalendarzSprzetu(ByVal sprzetId As Integer, ByVal rok As Integer, ByVal miesiac As Integer) As DataTable
ReservationDAL.vb:149:            Dim poczatek As New Date(rok, miesiac, 1)
ReservationDAL.vb:150:            Dim koniec As Date = poczatek.AddMonths(1)
ReservationDAL.vb:151:            Dim sql As String = "SELECT r.Id, r.DataOd, r.DataDo, r.Status " & _
ReservationDAL.vb:152:                                "FROM Rezerwacje r " & _
ReservationDAL.vb:153:                                "WHERE r.SprzetId = @SprzetId AND r.Status IN ('N', 'P', 'Z') " & _
ReservationDAL.vb:154:                                "AND r.DataOd < @Koniec AND r.DataDo > @Poczatek " & _
ReservationDAL.vb:155:                                "ORDER BY r.DataOd"
ReservationDAL.vb:156:            Return DbHelper.WykonajZapytanie(sql, _
ReservationDAL.vb:157:                                             DbHelper.Parametr("@SprzetId", sprzetId), _
ReservationDAL.vb:158:                                             DbHelper.Parametr("@Poczatek", poczatek), _
ReservationDAL.vb:159:                                             DbHelper.Parametr("@Koniec", koniec))
ReservationDAL.vb:160:        End Function
ReservationDAL.vb:161:
ReservationDAL.vb:162:        ' kolumny: Id, KlientId, Klient, Sprzet, DataOd, DataDo, CenaSzacunkowa
ReservationDAL.vb:163:        Public Function DoPotwierdzenia() As DataTable
ReservationDAL.vb:164:            Dim sql As String = "SELECT r.Id, r.KlientId, k.Nazwisko AS Klient, s.Nazwa AS Sprzet, r.DataOd, r.DataDo, r.CenaSzacunkowa " & _
ReservationDAL.vb:165:                                "FROM Rezerwacje r " & _
ReservationDAL.vb:166:                                "INNER JOIN Klienci k ON k.Id = r.KlientId " & _
ReservationDAL.vb:167:                                "INNER JOIN Sprzet s ON s.Id = r.SprzetId " & _
ReservationDAL.vb:168:                                "WHERE r.Status = 'N' AND r.DataOd >= GETDATE() " & _
ReservationDAL.vb:169:                                "ORDER BY r.DataOd"
ReservationDAL.vb:170:            Return DbHelper.WykonajZapytanie(sql)
ReservationDAL.vb:171:        End Function
ReservationDAL.vb:172:
ReservationDAL.vb:173:        Public Sub ZapiszUwagi(ByVal rezerwacjaId As Integer, ByVal uwagi As String)
ReservationDAL.vb:174:            Dim sql As String = "UPDATE Rezerwacje SET Uwagi = @Uwagi WHERE Id = @Id"
ReservationDAL.vb:175:            DbHelper.WykonajPolecenie(sql, _
ReservationDAL.vb:176:                                      DbHelper.Parametr("@Uwagi", uwagi), _
ReservationDAL.vb:177:                                      DbHelper.Parametr("@Id", rezerwacjaId))
ReservationDAL.vb:178:        End Sub
ReservationDAL.vb:179:
ReservationDAL.vb:180:        Public Sub DopiszDoDziennika(ByVal operacja As String, ByVal szczegoly As String)
ReservationDAL.vb:181:            Dim sql As String = "INSERT INTO DziennikOperacji (Operacja, Szczegoly) VALUES (@Operacja, @Szczegoly)"
ReservationDAL.vb:182:            If szczegoly IsNot Nothing AndAlso szczegoly.Length > 400 Then
ReservationDAL.vb:183:                szczegoly = szczegoly.Substring(0, 400)
ReservationDAL.vb:184:            End If
ReservationDAL.vb:185:            DbHelper.WykonajPolecenie(sql, _
ReservationDAL.vb:186:                                      DbHelper.Parametr("@Operacja", operacja), _
ReservationDAL.vb:187:                                      DbHelper.Parametr("@Szczegoly", szczegoly))
ReservationDAL.vb:188:        End Sub
ReservationDAL.vb:189:
ReservationDAL.vb:190:    End Class
ReservationDAL.vb:191:
ReservationDAL.vb:192:End Namespace
```

## M15 — surowy wynik H5

`git --no-pager -C 'legacy-app-parent\legacy-app\Database' grep -n -I -e '^' -- '02_procedury.sql'`

```text
02_procedury.sql:1:-- =============================================================
02_procedury.sql:2:-- Wypozyczalnia - procedury skladowane
02_procedury.sql:3:-- Autor: zespol IT | 2014-03-11
02_procedury.sql:4:-- Historia:
02_procedury.sql:5:--   2015-02-04 MW: usp_PobierzTaryfe, usp_ZapiszTaryfe (taryfy z data obowiazywania)
02_procedury.sql:6:--   2016-09-21 JK: usp_WydajSprzet zamyka rezerwacje
02_procedury.sql:7:--   2018-01-15 PT: usp_WystawFakture - numeracja miesieczna FV/rrrr/mm/nnnn
02_procedury.sql:8:-- Uruchamiac po 01_schema.sql, przed 03_seed.sql.
02_procedury.sql:9:-- =============================================================
02_procedury.sql:10:
02_procedury.sql:11:USE Wypozyczalnia
02_procedury.sql:12:GO
02_procedury.sql:13:
02_procedury.sql:14:SET ANSI_NULLS ON
02_procedury.sql:15:GO
02_procedury.sql:16:SET QUOTED_IDENTIFIER ON
02_procedury.sql:17:GO
02_procedury.sql:18:
02_procedury.sql:19:-- -------------------------------------------------------------
02_procedury.sql:20:-- usp_SprawdzDostepnosc
02_procedury.sql:21:-- Zwraca wszystko, co koliduje z terminem dla danego sprzetu.
02_procedury.sql:22:-- Brak wierszy = sprzet dostepny.
02_procedury.sql:23:-- -------------------------------------------------------------
02_procedury.sql:24:IF OBJECT_ID('dbo.usp_SprawdzDostepnosc', 'P') IS NOT NULL
02_procedury.sql:25:    DROP PROCEDURE dbo.usp_SprawdzDostepnosc
02_procedury.sql:26:GO
02_procedury.sql:27:CREATE PROCEDURE dbo.usp_SprawdzDostepnosc @SprzetId INT, @DataOd DATETIME, @DataDo DATETIME
02_procedury.sql:28:AS
02_procedury.sql:29:BEGIN
02_procedury.sql:30:    SET NOCOUNT ON;
02_procedury.sql:31:
02_procedury.sql:32:    IF @DataDo <= @DataOd
02_procedury.sql:33:    BEGIN
02_procedury.sql:34:        RAISERROR('Nieprawidlowy zakres dat', 16, 1);
02_procedury.sql:35:        RETURN;
02_procedury.sql:36:    END
02_procedury.sql:37:
02_procedury.sql:38:    SELECT CAST('R' AS CHAR(1)) AS Zrodlo,
02_procedury.sql:39:           r.Id AS Id,
02_procedury.sql:40:           r.DataOd AS DataOd,
02_procedury.sql:41:           r.DataDo AS DataDo
02_procedury.sql:42:    FROM dbo.Rezerwacje r
02_procedury.sql:43:    WHERE r.SprzetId = @SprzetId
02_procedury.sql:44:      AND r.Status IN ('N', 'P')
02_procedury.sql:45:      AND r.DataOd < @DataDo
02_procedury.sql:46:      AND r.DataDo > @DataOd
02_procedury.sql:47:
02_procedury.sql:48:    UNION ALL
02_procedury.sql:49:
02_procedury.sql:50:    SELECT CAST('W' AS CHAR(1)) AS Zrodlo,
02_procedury.sql:51:           w.Id AS Id,
02_procedury.sql:52:           w.DataWydania AS DataOd,
02_procedury.sql:53:           w.PlanowanyZwrot AS DataDo
02_procedury.sql:54:    FROM dbo.Wypozyczenia w
02_procedury.sql:55:    WHERE w.SprzetId = @SprzetId
02_procedury.sql:56:      AND w.Status = 'O'
02_procedury.sql:57:      AND w.DataWydania < @DataDo
02_procedury.sql:58:      AND w.PlanowanyZwrot > @DataOd
02_procedury.sql:59:
02_procedury.sql:60:    UNION ALL
02_procedury.sql:61:
02_procedury.sql:62:    SELECT CAST('S' AS CHAR(1)) AS Zrodlo,
02_procedury.sql:63:           s.Id AS Id,
02_procedury.sql:64:           CAST(NULL AS DATETIME) AS DataOd,
02_procedury.sql:65:           CAST(NULL AS DATETIME) AS DataDo
02_procedury.sql:66:    FROM dbo.Sprzet s
02_procedury.sql:67:    WHERE s.Id = @SprzetId
02_procedury.sql:68:      AND s.Status IN ('S', 'X')
02_procedury.sql:69:
02_procedury.sql:70:    ORDER BY Zrodlo, DataOd;
02_procedury.sql:71:END
02_procedury.sql:72:GO
02_procedury.sql:73:
02_procedury.sql:74:-- -------------------------------------------------------------
02_procedury.sql:75:-- usp_ZapiszRezerwacje
02_procedury.sql:76:-- -------------------------------------------------------------
02_procedury.sql:77:IF OBJECT_ID('dbo.usp_ZapiszRezerwacje', 'P') IS NOT NULL
02_procedury.sql:78:    DROP PROCEDURE dbo.usp_ZapiszRezerwacje
02_procedury.sql:79:GO
02_procedury.sql:80:CREATE PROCEDURE dbo.usp_ZapiszRezerwacje @KlientId INT, @SprzetId INT, @DataOd DATETIME, @DataDo DATETIME, @CenaSzacunkowa DECIMAL(10,2), @Uwagi NVARCHAR(500), @NoweId INT OUTPUT
02_procedury.sql:81:AS
02_procedury.sql:82:BEGIN
02_procedury.sql:83:    SET NOCOUNT ON;
02_procedury.sql:84:
02_procedury.sql:85:    IF NOT EXISTS (SELECT 1 FROM dbo.Klienci WHERE Id = @KlientId)
02_procedury.sql:86:    BEGIN
02_procedury.sql:87:        RAISERROR('Nieznany klient %d', 16, 1, @KlientId);
02_procedury.sql:88:        RETURN;
02_procedury.sql:89:    END
02_procedury.sql:90:
02_procedury.sql:91:    IF NOT EXISTS (SELECT 1 FROM dbo.Sprzet WHERE Id = @SprzetId)
02_procedury.sql:92:    BEGIN
02_procedury.sql:93:        RAISERROR('Nieznany sprzet %d', 16, 1, @SprzetId);
02_procedury.sql:94:        RETURN;
02_procedury.sql:95:    END
02_procedury.sql:96:
02_procedury.sql:97:    BEGIN TRY
02_procedury.sql:98:        BEGIN TRANSACTION;
02_procedury.sql:99:
02_procedury.sql:100:        INSERT INTO dbo.Rezerwacje (KlientId, SprzetId, DataOd, DataDo, Status, CenaSzacunkowa, Uwagi, Utworzono)
02_procedury.sql:101:        VALUES (@KlientId, @SprzetId, @DataOd, @DataDo, 'N', @CenaSzacunkowa, @Uwagi, GETDATE());
02_procedury.sql:102:
02_procedury.sql:103:        SET @NoweId = CAST(SCOPE_IDENTITY() AS INT);
02_procedury.sql:104:
02_procedury.sql:105:        INSERT INTO dbo.DziennikOperacji (Kiedy, Operacja, Szczegoly)
02_procedury.sql:106:        VALUES (GETDATE(), 'REZERWACJA',
02_procedury.sql:107:                N'Rezerwacja ' + CAST(@NoweId AS NVARCHAR(10)) + N', klient ' + CAST(@KlientId AS NVARCHAR(10))
02_procedury.sql:108:                + N', sprzet ' + CAST(@SprzetId AS NVARCHAR(10)));
02_procedury.sql:109:
02_procedury.sql:110:        COMMIT TRANSACTION;
02_procedury.sql:111:    END TRY
02_procedury.sql:112:    BEGIN CATCH
02_procedury.sql:113:        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
02_procedury.sql:114:        DECLARE @Blad NVARCHAR(2048) = ERROR_MESSAGE();
02_procedury.sql:115:        RAISERROR(@Blad, 16, 1);
02_procedury.sql:116:    END CATCH
02_procedury.sql:117:END
02_procedury.sql:118:GO
02_procedury.sql:119:
02_procedury.sql:120:-- -------------------------------------------------------------
02_procedury.sql:121:-- usp_ZmienStatusRezerwacji
02_procedury.sql:122:-- -------------------------------------------------------------
02_procedury.sql:123:IF OBJECT_ID('dbo.usp_ZmienStatusRezerwacji', 'P') IS NOT NULL
02_procedury.sql:124:    DROP PROCEDURE dbo.usp_ZmienStatusRezerwacji
02_procedury.sql:125:GO
02_procedury.sql:126:CREATE PROCEDURE dbo.usp_ZmienStatusRezerwacji @RezerwacjaId INT, @Status CHAR(1)
02_procedury.sql:127:AS
02_procedury.sql:128:BEGIN
02_procedury.sql:129:    SET NOCOUNT ON;
02_procedury.sql:130:
02_procedury.sql:131:    IF @Status NOT IN ('N', 'P', 'A', 'Z')
02_procedury.sql:132:    BEGIN
02_procedury.sql:133:        RAISERROR('Nieprawidlowy status rezerwacji: %s', 16, 1, @Status);
02_procedury.sql:134:        RETURN;
02_procedury.sql:135:    END
02_procedury.sql:136:
02_procedury.sql:137:    DECLARE @StaryStatus CHAR(1);
02_procedury.sql:138:    SELECT @StaryStatus = Status FROM dbo.Rezerwacje WHERE Id = @RezerwacjaId;
02_procedury.sql:139:
02_procedury.sql:140:    IF @StaryStatus IS NULL
02_procedury.sql:141:    BEGIN
02_procedury.sql:142:        RAISERROR('Nieznana rezerwacja %d', 16, 1, @RezerwacjaId);
02_procedury.sql:143:        RETURN;
02_procedury.sql:144:    END
02_procedury.sql:145:
02_procedury.sql:146:    UPDATE dbo.Rezerwacje
02_procedury.sql:147:    SET Status = @Status
02_procedury.sql:148:    WHERE Id = @RezerwacjaId;
02_procedury.sql:149:
02_procedury.sql:150:    INSERT INTO dbo.DziennikOperacji (Kiedy, Operacja, Szczegoly)
02_procedury.sql:151:    VALUES (GETDATE(), 'STATUS_REZERWACJI',
02_procedury.sql:152:            N'Rezerwacja ' + CAST(@RezerwacjaId AS NVARCHAR(10)) + N': ' + @StaryStatus + N' -> ' + @Status);
02_procedury.sql:153:END
02_procedury.sql:154:GO
02_procedury.sql:155:
02_procedury.sql:156:-- -------------------------------------------------------------
02_procedury.sql:157:-- usp_WydajSprzet
02_procedury.sql:158:-- -------------------------------------------------------------
02_procedury.sql:159:IF OBJECT_ID('dbo.usp_WydajSprzet', 'P') IS NOT NULL
02_procedury.sql:160:    DROP PROCEDURE dbo.usp_WydajSprzet
02_procedury.sql:161:GO
02_procedury.sql:162:CREATE PROCEDURE dbo.usp_WydajSprzet @RezerwacjaId INT = NULL, @KlientId INT, @SprzetId INT, @DataWydania DATETIME, @PlanowanyZwrot DATETIME, @KaucjaPobrana DECIMAL(10,2), @NoweId INT OUTPUT
02_procedury.sql:163:AS
02_procedury.sql:164:BEGIN
02_procedury.sql:165:    SET NOCOUNT ON;
02_procedury.sql:166:
02_procedury.sql:167:    IF @RezerwacjaId = 0 SET @RezerwacjaId = NULL;
02_procedury.sql:168:
02_procedury.sql:169:    IF @PlanowanyZwrot <= @DataWydania
02_procedury.sql:170:    BEGIN
02_procedury.sql:171:        RAISERROR('Planowany zwrot musi byc pozniejszy niz wydanie', 16, 1);
02_procedury.sql:172:        RETURN;
02_procedury.sql:173:    END
02_procedury.sql:174:
02_procedury.sql:175:    BEGIN TRY
02_procedury.sql:176:        BEGIN TRANSACTION;
02_procedury.sql:177:
02_procedury.sql:178:        INSERT INTO dbo.Wypozyczenia (RezerwacjaId, KlientId, SprzetId, DataWydania, PlanowanyZwrot, KaucjaPobrana, Status)
02_procedury.sql:179:        VALUES (@RezerwacjaId, @KlientId, @SprzetId, @DataWydania, @PlanowanyZwrot, @KaucjaPobrana, 'O');
02_procedury.sql:180:
02_procedury.sql:181:        SET @NoweId = CAST(SCOPE_IDENTITY() AS INT);
02_procedury.sql:182:
02_procedury.sql:183:        UPDATE dbo.Sprzet
02_procedury.sql:184:        SET Status = 'W'
02_procedury.sql:185:        WHERE Id = @SprzetId;
02_procedury.sql:186:
02_procedury.sql:187:        IF @RezerwacjaId IS NOT NULL
02_procedury.sql:188:        BEGIN
02_procedury.sql:189:            UPDATE dbo.Rezerwacje
02_procedury.sql:190:            SET Status = 'Z'
02_procedury.sql:191:            WHERE Id = @RezerwacjaId;
02_procedury.sql:192:        END
02_procedury.sql:193:
02_procedury.sql:194:        INSERT INTO dbo.DziennikOperacji (Kiedy, Operacja, Szczegoly)
02_procedury.sql:195:        VALUES (GETDATE(), 'WYDANIE',
02_procedury.sql:196:                N'Wypozyczenie ' + CAST(@NoweId AS NVARCHAR(10)) + N', sprzet ' + CAST(@SprzetId AS NVARCHAR(10))
02_procedury.sql:197:                + ISNULL(N', rezerwacja ' + CAST(@RezerwacjaId AS NVARCHAR(10)), N''));
02_procedury.sql:198:
02_procedury.sql:199:        COMMIT TRANSACTION;
02_procedury.sql:200:    END TRY
02_procedury.sql:201:    BEGIN CATCH
02_procedury.sql:202:        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
02_procedury.sql:203:        DECLARE @Blad NVARCHAR(2048) = ERROR_MESSAGE();
02_procedury.sql:204:        RAISERROR(@Blad, 16, 1);
02_procedury.sql:205:    END CATCH
02_procedury.sql:206:END
02_procedury.sql:207:GO
02_procedury.sql:208:
02_procedury.sql:209:-- -------------------------------------------------------------
02_procedury.sql:210:-- usp_ZarejestrujZwrot
02_procedury.sql:211:-- -------------------------------------------------------------
02_procedury.sql:212:IF OBJECT_ID('dbo.usp_ZarejestrujZwrot', 'P') IS NOT NULL
02_procedury.sql:213:    DROP PROCEDURE dbo.usp_ZarejestrujZwrot
02_procedury.sql:214:GO
02_procedury.sql:215:CREATE PROCEDURE dbo.usp_ZarejestrujZwrot @WypozyczenieId INT, @DataZwrotu DATETIME, @Oplata DECIMAL(10,2), @Kara DECIMAL(10,2), @KaucjaZwrocona DECIMAL(10,2)
02_procedury.sql:216:AS
02_procedury.sql:217:BEGIN
02_procedury.sql:218:    SET NOCOUNT ON;
02_procedury.sql:219:
02_procedury.sql:220:    DECLARE @SprzetId INT;
02_procedury.sql:221:    DECLARE @Status CHAR(1);
02_procedury.sql:222:
02_procedury.sql:223:    SELECT @SprzetId = SprzetId, @Status = Status
02_procedury.sql:224:    FROM dbo.Wypozyczenia
02_procedury.sql:225:    WHERE Id = @WypozyczenieId;
02_procedury.sql:226:
02_procedury.sql:227:    IF @SprzetId IS NULL
02_procedury.sql:228:    BEGIN
02_procedury.sql:229:        RAISERROR('Nieznane wypozyczenie %d', 16, 1, @WypozyczenieId);
02_procedury.sql:230:        RETURN;
02_procedury.sql:231:    END
02_procedury.sql:232:
02_procedury.sql:233:    IF @Status = 'Z'
02_procedury.sql:234:    BEGIN
02_procedury.sql:235:        RAISERROR('Wypozyczenie %d jest juz zamkniete', 16, 1, @WypozyczenieId);
02_procedury.sql:236:        RETURN;
02_procedury.sql:237:    END
02_procedury.sql:238:
02_procedury.sql:239:    BEGIN TRY
02_procedury.sql:240:        BEGIN TRANSACTION;
02_procedury.sql:241:
02_procedury.sql:242:        UPDATE dbo.Wypozyczenia
02_procedury.sql:243:        SET DataZwrotu = @DataZwrotu,
02_procedury.sql:244:            Oplata = @Oplata,
02_procedury.sql:245:            Kara = @Kara,
02_procedury.sql:246:            KaucjaZwrocona = @KaucjaZwrocona,
02_procedury.sql:247:            Status = 'Z'
02_procedury.sql:248:        WHERE Id = @WypozyczenieId;
02_procedury.sql:249:
02_procedury.sql:250:        UPDATE dbo.Sprzet
02_procedury.sql:251:        SET Status = 'D'
02_procedury.sql:252:        WHERE Id = @SprzetId;
02_procedury.sql:253:
02_procedury.sql:254:        INSERT INTO dbo.DziennikOperacji (Kiedy, Operacja, Szczegoly)
02_procedury.sql:255:        VALUES (GETDATE(), 'ZWROT',
02_procedury.sql:256:                N'Wypozyczenie ' + CAST(@WypozyczenieId AS NVARCHAR(10)) + N', oplata ' + CAST(@Oplata AS NVARCHAR(20))
02_procedury.sql:257:                + N', kara ' + CAST(@Kara AS NVARCHAR(20)));
02_procedury.sql:258:
02_procedury.sql:259:        COMMIT TRANSACTION;
02_procedury.sql:260:    END TRY
02_procedury.sql:261:    BEGIN CATCH
02_procedury.sql:262:        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
02_procedury.sql:263:        DECLARE @Blad NVARCHAR(2048) = ERROR_MESSAGE();
02_procedury.sql:264:        RAISERROR(@Blad, 16, 1);
02_procedury.sql:265:    END CATCH
02_procedury.sql:266:END
02_procedury.sql:267:GO
02_procedury.sql:268:
02_procedury.sql:269:-- -------------------------------------------------------------
02_procedury.sql:270:-- usp_PobierzTaryfe
02_procedury.sql:271:-- Taryfa obowiazujaca w danym dniu (najnowsza z ObowiazujeOd <= dzien).
02_procedury.sql:272:-- -------------------------------------------------------------
02_procedury.sql:273:IF OBJECT_ID('dbo.usp_PobierzTaryfe', 'P') IS NOT NULL
02_procedury.sql:274:    DROP PROCEDURE dbo.usp_PobierzTaryfe
02_procedury.sql:275:GO
02_procedury.sql:276:CREATE PROCEDURE dbo.usp_PobierzTaryfe @Kategoria CHAR(3), @NaDzien DATE
02_procedury.sql:277:AS
02_procedury.sql:278:BEGIN
02_procedury.sql:279:    SET NOCOUNT ON;
02_procedury.sql:280:
02_procedury.sql:281:    SELECT TOP 1
02_procedury.sql:282:           Id,
02_procedury.sql:283:           Kategoria,
02_procedury.sql:284:           MnoznikWeekend,
02_procedury.sql:285:           RabatTygodniowyProc,
02_procedury.sql:286:           ObowiazujeOd
02_procedury.sql:287:    FROM dbo.Taryfy
02_procedury.sql:288:    WHERE Kategoria = @Kategoria
02_procedury.sql:289:      AND ObowiazujeOd <= @NaDzien
02_procedury.sql:290:    ORDER BY ObowiazujeOd DESC;
02_procedury.sql:291:END
02_procedury.sql:292:GO
02_procedury.sql:293:
02_procedury.sql:294:-- -------------------------------------------------------------
02_procedury.sql:295:-- usp_ZapiszTaryfe
02_procedury.sql:296:-- Upsert po (Kategoria, ObowiazujeOd).
02_procedury.sql:297:-- -------------------------------------------------------------
02_procedury.sql:298:IF OBJECT_ID('dbo.usp_ZapiszTaryfe', 'P') IS NOT NULL
02_procedury.sql:299:    DROP PROCEDURE dbo.usp_ZapiszTaryfe
02_procedury.sql:300:GO
02_procedury.sql:301:CREATE PROCEDURE dbo.usp_ZapiszTaryfe @Kategoria CHAR(3), @MnoznikWeekend DECIMAL(4,2), @RabatTygodniowyProc DECIMAL(5,2), @ObowiazujeOd DATE
02_procedury.sql:302:AS
02_procedury.sql:303:BEGIN
02_procedury.sql:304:    SET NOCOUNT ON;
02_procedury.sql:305:
02_procedury.sql:306:    IF @MnoznikWeekend <= 0 OR @RabatTygodniowyProc < 0
02_procedury.sql:307:    BEGIN
02_procedury.sql:308:        RAISERROR('Nieprawidlowe parametry taryfy', 16, 1);
02_procedury.sql:309:        RETURN;
02_procedury.sql:310:    END
02_procedury.sql:311:
02_procedury.sql:312:    IF EXISTS (SELECT 1 FROM dbo.Taryfy WHERE Kategoria = @Kategoria AND ObowiazujeOd = @ObowiazujeOd)
02_procedury.sql:313:    BEGIN
02_procedury.sql:314:        UPDATE dbo.Taryfy
02_procedury.sql:315:        SET MnoznikWeekend = @MnoznikWeekend,
02_procedury.sql:316:            RabatTygodniowyProc = @RabatTygodniowyProc
02_procedury.sql:317:        WHERE Kategoria = @Kategoria
02_procedury.sql:318:          AND ObowiazujeOd = @ObowiazujeOd;
02_procedury.sql:319:    END
02_procedury.sql:320:    ELSE
02_procedury.sql:321:    BEGIN
02_procedury.sql:322:        INSERT INTO dbo.Taryfy (Kategoria, MnoznikWeekend, RabatTygodniowyProc, ObowiazujeOd)
02_procedury.sql:323:        VALUES (@Kategoria, @MnoznikWeekend, @RabatTygodniowyProc, @ObowiazujeOd);
02_procedury.sql:324:    END
02_procedury.sql:325:
02_procedury.sql:326:    INSERT INTO dbo.DziennikOperacji (Kiedy, Operacja, Szczegoly)
02_procedury.sql:327:    VALUES (GETDATE(), 'TARYFA',
02_procedury.sql:328:            @Kategoria + N' od ' + CONVERT(NVARCHAR(10), @ObowiazujeOd, 120));
02_procedury.sql:329:END
02_procedury.sql:330:GO
02_procedury.sql:331:
02_procedury.sql:332:-- -------------------------------------------------------------
02_procedury.sql:333:-- usp_WystawFakture
02_procedury.sql:334:-- Numer: FV/rrrr/mm/nnnn, numeracja od 1 w kazdym miesiacu.
02_procedury.sql:335:-- -------------------------------------------------------------
02_procedury.sql:336:IF OBJECT_ID('dbo.usp_WystawFakture', 'P') IS NOT NULL
02_procedury.sql:337:    DROP PROCEDURE dbo.usp_WystawFakture
02_procedury.sql:338:GO
02_procedury.sql:339:CREATE PROCEDURE dbo.usp_WystawFakture @KlientId INT, @WypozyczenieId INT, @Netto DECIMAL(10,2), @Vat DECIMAL(10,2), @Brutto DECIMAL(10,2), @NoweId INT OUTPUT, @Numer VARCHAR(30) OUTPUT
02_procedury.sql:340:AS
02_procedury.sql:341:BEGIN
02_procedury.sql:342:    SET NOCOUNT ON;
02_procedury.sql:343:
02_procedury.sql:344:    DECLARE @Teraz DATETIME = GETDATE();
02_procedury.sql:345:    DECLARE @Rok INT = YEAR(@Teraz);
02_procedury.sql:346:    DECLARE @Miesiac INT = MONTH(@Teraz);
02_procedury.sql:347:    DECLARE @Kolejny INT;
02_procedury.sql:348:
02_procedury.sql:349:    BEGIN TRY
02_procedury.sql:350:        BEGIN TRANSACTION;
02_procedury.sql:351:
02_procedury.sql:352:        SELECT @Kolejny = COUNT(*) + 1
02_procedury.sql:353:        FROM dbo.Faktury WITH (UPDLOCK, HOLDLOCK)
02_procedury.sql:354:        WHERE YEAR(DataWystawienia) = @Rok
02_procedury.sql:355:          AND MONTH(DataWystawienia) = @Miesiac;
02_procedury.sql:356:
02_procedury.sql:357:        SET @Numer = 'FV/' + CAST(@Rok AS VARCHAR(4)) + '/'
02_procedury.sql:358:                   + RIGHT('0' + CAST(@Miesiac AS VARCHAR(2)), 2) + '/'
02_procedury.sql:359:                   + RIGHT('0000' + CAST(@Kolejny AS VARCHAR(4)), 4);
02_procedury.sql:360:
02_procedury.sql:361:        INSERT INTO dbo.Faktury (Numer, KlientId, WypozyczenieId, DataWystawienia, Netto, Vat, Brutto, Status)
02_procedury.sql:362:        VALUES (@Numer, @KlientId, @WypozyczenieId, @Teraz, @Netto, @Vat, @Brutto, 'W');
02_procedury.sql:363:
02_procedury.sql:364:        SET @NoweId = CAST(SCOPE_IDENTITY() AS INT);
02_procedury.sql:365:
02_procedury.sql:366:        INSERT INTO dbo.DziennikOperacji (Kiedy, Operacja, Szczegoly)
02_procedury.sql:367:        VALUES (@Teraz, 'FAKTURA',
02_procedury.sql:368:                N'Faktura ' + @Numer + N', wypozyczenie ' + CAST(@WypozyczenieId AS NVARCHAR(10)));
02_procedury.sql:369:
02_procedury.sql:370:        COMMIT TRANSACTION;
02_procedury.sql:371:    END TRY
02_procedury.sql:372:    BEGIN CATCH
02_procedury.sql:373:        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
02_procedury.sql:374:        DECLARE @Blad NVARCHAR(2048) = ERROR_MESSAGE();
02_procedury.sql:375:        RAISERROR(@Blad, 16, 1);
02_procedury.sql:376:    END CATCH
02_procedury.sql:377:END
02_procedury.sql:378:GO
```

`git --no-pager -C 'legacy-app-parent\legacy-app' grep -n -I -i -E 'CommandType|StoredProcedure|CommandText|Execute|exec[[:space:]]|usp_|dbo\.' -- '*.vb'`

```text
WypozyczalniaCore/DAL/DbHelper.vb:46:                    cmd.CommandType = CommandType.Text
WypozyczalniaCore/DAL/DbHelper.vb:60:                    cmd.CommandType = CommandType.Text
WypozyczalniaCore/DAL/DbHelper.vb:64:                    Return cmd.ExecuteNonQuery()
WypozyczalniaCore/DAL/DbHelper.vb:73:                    cmd.CommandType = CommandType.StoredProcedure
WypozyczalniaCore/DAL/DbHelper.vb:87:                    cmd.CommandType = CommandType.StoredProcedure
WypozyczalniaCore/DAL/DbHelper.vb:91:                    Return cmd.ExecuteNonQuery()
WypozyczalniaCore/DAL/InvoiceDAL.vb:3:'   2014-11-27 JK  numer faktury nadawany w bazie (usp_WystawFakture)
WypozyczalniaCore/DAL/InvoiceDAL.vb:20:            DbHelper.WykonajProcedureBezWyniku("usp_WystawFakture", _
WypozyczalniaCore/DAL/InvoiceDAL.vb:29:                Throw New ApplicationException("Procedura usp_WystawFakture nie zwrocila Id")
WypozyczalniaCore/DAL/InvoiceDAL.vb:139:                    cmdPozycje.ExecuteNonQuery()
WypozyczalniaCore/DAL/InvoiceDAL.vb:142:                    If cmdFaktura.ExecuteNonQuery() = 0 Then
WypozyczalniaCore/DAL/PricingDAL.vb:3:'   2015-02-04 MW: taryfy z data obowiazywania (usp_PobierzTaryfe)
WypozyczalniaCore/DAL/PricingDAL.vb:15:            Dim dt As DataTable = DbHelper.WykonajProcedure("usp_PobierzTaryfe",
WypozyczalniaCore/DAL/PricingDAL.vb:26:            DbHelper.WykonajProcedureBezWyniku("usp_ZapiszTaryfe",
WypozyczalniaCore/DAL/RentalDAL.vb:25:            DbHelper.WykonajProcedureBezWyniku("usp_WydajSprzet",
WypozyczalniaCore/DAL/RentalDAL.vb:41:            DbHelper.WykonajProcedureBezWyniku("usp_ZarejestrujZwrot",
WypozyczalniaCore/DAL/ReservationDAL.vb:3:'   2014-09-22 JK  zapis przez procedure usp_ZapiszRezerwacje (wpis do dziennika)
WypozyczalniaCore/DAL/ReservationDAL.vb:4:'   2016-11-08 PT  kolizje liczone w bazie (usp_SprawdzDostepnosc) zamiast petli w VB
WypozyczalniaCore/DAL/ReservationDAL.vb:18:            Return DbHelper.WykonajProcedure("usp_SprawdzDostepnosc", _
WypozyczalniaCore/DAL/ReservationDAL.vb:30:            DbHelper.WykonajProcedureBezWyniku("usp_ZapiszRezerwacje", _
WypozyczalniaCore/DAL/ReservationDAL.vb:39:                Throw New ApplicationException("Procedura usp_ZapiszRezerwacje nie zwrocila Id")
WypozyczalniaCore/DAL/ReservationDAL.vb:54:            DbHelper.WykonajProcedureBezWyniku("usp_ZmienStatusRezerwacji", _
```

## M16 — surowy wynik H6

`git --no-pager -C 'legacy-app-parent\legacy-app\WypozyczalniaCore' grep -n -I -e '^' -- 'CustomerManager.vb'`

```text
CustomerManager.vb:1:' Modul: CustomerManager.vb - obsluga klientow wypozyczalni | Autor: zespol IT | 2014-03-11
CustomerManager.vb:2:' Historia: 2014-03-11 JK utworzenie
CustomerManager.vb:3:'           2015-11-23 MW blokowanie klientow (zalegle platnosci, zgubiony sprzet)
CustomerManager.vb:4:'           2018-09-04 PT klienci firmowi (NIP, NazwaFirmy)
CustomerManager.vb:5:Imports System.Data
CustomerManager.vb:6:Imports System.Data.SqlClient
CustomerManager.vb:7:Imports Wypozyczalnia.Core.Model
CustomerManager.vb:8:Imports Wypozyczalnia.Core.DAL
CustomerManager.vb:9:
CustomerManager.vb:10:Public Class CustomerManager
CustomerManager.vb:11:
CustomerManager.vb:12:    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(CustomerManager))
CustomerManager.vb:13:
CustomerManager.vb:14:    Private Const KolumnyKlienta As String = "Id, Imie, Nazwisko, NazwaFirmy, Email, Telefon, NumerDokumentu, NIP, Typ, ProgRabatu, Zablokowany, PowodBlokady, DataRejestracji"
CustomerManager.vb:15:
CustomerManager.vb:16:    Public Function PobierzKlienta(ByVal klientId As Integer) As Klient
CustomerManager.vb:17:        Dim dt As DataTable = DbHelper.WykonajZapytanie( _
CustomerManager.vb:18:            "SELECT " & KolumnyKlienta & " FROM Klienci WHERE Id = @Id", _
CustomerManager.vb:19:            DbHelper.Parametr("@Id", klientId))
CustomerManager.vb:20:        If dt.Rows.Count = 0 Then
CustomerManager.vb:21:            Return Nothing
CustomerManager.vb:22:        End If
CustomerManager.vb:23:        Return MapujKlienta(dt.Rows(0))
CustomerManager.vb:24:    End Function
CustomerManager.vb:25:
CustomerManager.vb:26:    Public Function CzyMozeWypozyczac(ByVal klientId As Integer) As Boolean
CustomerManager.vb:27:        Dim k As Klient = PobierzKlienta(klientId)
CustomerManager.vb:28:        If k Is Nothing Then
CustomerManager.vb:29:            Log.Warn("Sprawdzenie nieistniejacego klienta: " & klientId)
CustomerManager.vb:30:            Return False
CustomerManager.vb:31:        End If
CustomerManager.vb:32:        If k.Zablokowany Then
CustomerManager.vb:33:            Log.Info("Klient " & klientId & " zablokowany: " & k.PowodBlokady)
CustomerManager.vb:34:            Return False
CustomerManager.vb:35:        End If
CustomerManager.vb:36:        Return True
CustomerManager.vb:37:    End Function
CustomerManager.vb:38:
CustomerManager.vb:39:    Public Function ProgRabatu(ByVal klientId As Integer) As Integer
CustomerManager.vb:40:        Dim dt As DataTable = DbHelper.WykonajZapytanie( _
CustomerManager.vb:41:            "SELECT ProgRabatu FROM Klienci WHERE Id = @Id", _
CustomerManager.vb:42:            DbHelper.Parametr("@Id", klientId))
CustomerManager.vb:43:        If dt.Rows.Count = 0 Then
CustomerManager.vb:44:            Return 0
CustomerManager.vb:45:        End If
CustomerManager.vb:46:        Dim prog As Integer = dt.Rows(0)("ProgRabatu")
CustomerManager.vb:47:        If prog < 0 Then prog = 0
CustomerManager.vb:48:        If prog > 3 Then prog = 3
CustomerManager.vb:49:        Return prog
CustomerManager.vb:50:    End Function
CustomerManager.vb:51:
CustomerManager.vb:52:    Public Function Szukaj(ByVal fraza As String) As DataTable
CustomerManager.vb:53:        If fraza Is Nothing Then fraza = ""
CustomerManager.vb:54:        fraza = fraza.Trim()
CustomerManager.vb:55:        Dim sql As String = "SELECT TOP 200 Id, Imie, Nazwisko, NazwaFirmy, Email, Telefon FROM Klienci " & _
CustomerManager.vb:56:                            "WHERE (@Fraza = '' OR Nazwisko LIKE @Wzorzec OR NazwaFirmy LIKE @Wzorzec OR Email LIKE @Wzorzec) " & _
CustomerManager.vb:57:                            "ORDER BY Nazwisko, Imie"
CustomerManager.vb:58:        Return DbHelper.WykonajZapytanie(sql, _
CustomerManager.vb:59:            DbHelper.Parametr("@Fraza", fraza), _
CustomerManager.vb:60:            DbHelper.Parametr("@Wzorzec", "%" & fraza & "%"))
CustomerManager.vb:61:    End Function
CustomerManager.vb:62:
CustomerManager.vb:63:    Public Function Zapisz(ByVal klient As Klient) As Integer
CustomerManager.vb:64:        If klient Is Nothing Then Throw New ArgumentNullException("klient")
CustomerManager.vb:65:        If String.IsNullOrEmpty(klient.Nazwisko) AndAlso String.IsNullOrEmpty(klient.NazwaFirmy) Then
CustomerManager.vb:66:            Throw New BladBiznesowyException("Podaj nazwisko albo nazwe firmy", "KLI-01")
CustomerManager.vb:67:        End If
CustomerManager.vb:68:        If String.IsNullOrEmpty(klient.Typ) Then klient.Typ = "I"
CustomerManager.vb:69:        If klient.Typ = "F" AndAlso String.IsNullOrEmpty(klient.NIP) Then
CustomerManager.vb:70:            Throw New BladBiznesowyException("Klient firmowy wymaga numeru NIP", "KLI-02")
CustomerManager.vb:71:        End If
CustomerManager.vb:72:        If klient.Imie Is Nothing Then klient.Imie = ""
CustomerManager.vb:73:        If klient.Nazwisko Is Nothing Then klient.Nazwisko = ""
CustomerManager.vb:74:
CustomerManager.vb:75:        If klient.Id = 0 Then
CustomerManager.vb:76:            Dim dt As DataTable = DbHelper.WykonajZapytanie( _
CustomerManager.vb:77:                "INSERT INTO Klienci (Imie, Nazwisko, NazwaFirmy, Email, Telefon, NumerDokumentu, NIP, Typ, ProgRabatu) " & _
CustomerManager.vb:78:                "VALUES (@Imie, @Nazwisko, @NazwaFirmy, @Email, @Telefon, @NumerDokumentu, @NIP, @Typ, @ProgRabatu); " & _
CustomerManager.vb:79:                "SELECT CAST(SCOPE_IDENTITY() AS INT) AS NoweId", _
CustomerManager.vb:80:                ParametryKlienta(klient))
CustomerManager.vb:81:            klient.Id = dt.Rows(0)("NoweId")
CustomerManager.vb:82:            Log.Info("Dodano klienta " & klient.Id)
CustomerManager.vb:83:            ZapiszDziennik("KLIENT_NOWY", "Id=" & klient.Id)
CustomerManager.vb:84:        Else
CustomerManager.vb:85:            Dim parametry As New List(Of SqlParameter)(ParametryKlienta(klient))
CustomerManager.vb:86:            parametry.Add(DbHelper.Parametr("@Id", klient.Id))
CustomerManager.vb:87:            Dim zmienione As Integer = DbHelper.WykonajPolecenie( _
CustomerManager.vb:88:                "UPDATE Klienci SET Imie = @Imie, Nazwisko = @Nazwisko, NazwaFirmy = @NazwaFirmy, Email = @Email, " & _
CustomerManager.vb:89:                "Telefon = @Telefon, NumerDokumentu = @NumerDokumentu, NIP = @NIP, Typ = @Typ, ProgRabatu = @ProgRabatu " & _
CustomerManager.vb:90:                "WHERE Id = @Id", _
CustomerManager.vb:91:                parametry.ToArray())
CustomerManager.vb:92:            If zmienione = 0 Then
CustomerManager.vb:93:                Throw New BladBiznesowyException("Nie znaleziono klienta nr " & klient.Id, "KLI-03")
CustomerManager.vb:94:            End If
CustomerManager.vb:95:            Log.Info("Zaktualizowano klienta " & klient.Id)
CustomerManager.vb:96:        End If
CustomerManager.vb:97:        Return klient.Id
CustomerManager.vb:98:    End Function
CustomerManager.vb:99:
CustomerManager.vb:100:    Public Sub Zablokuj(ByVal klientId As Integer, ByVal powod As String)
CustomerManager.vb:101:        If powod IsNot Nothing AndAlso powod.Length > 200 Then
CustomerManager.vb:102:            powod = powod.Substring(0, 200)
CustomerManager.vb:103:        End If
CustomerManager.vb:104:        DbHelper.WykonajPolecenie( _
CustomerManager.vb:105:            "UPDATE Klienci SET Zablokowany = 1, PowodBlokady = @Powod WHERE Id = @Id", _
CustomerManager.vb:106:            DbHelper.Parametr("@Powod", powod), _
CustomerManager.vb:107:            DbHelper.Parametr("@Id", klientId))
CustomerManager.vb:108:        Log.Warn("Zablokowano klienta " & klientId & ": " & powod)
CustomerManager.vb:109:        ZapiszDziennik("KLIENT_BLOKADA", "Id=" & klientId & "; " & powod)
CustomerManager.vb:110:    End Sub
CustomerManager.vb:111:
CustomerManager.vb:112:    Public Sub Odblokuj(ByVal klientId As Integer)
CustomerManager.vb:113:        DbHelper.WykonajPolecenie( _
CustomerManager.vb:114:            "UPDATE Klienci SET Zablokowany = 0, PowodBlokady = NULL WHERE Id = @Id", _
CustomerManager.vb:115:            DbHelper.Parametr("@Id", klientId))
CustomerManager.vb:116:        ZapiszDziennik("KLIENT_ODBLOKOWANIE", "Id=" & klientId)
CustomerManager.vb:117:    End Sub
CustomerManager.vb:118:
CustomerManager.vb:119:    Public Function PobierzEmail(ByVal klientId As Integer) As String
CustomerManager.vb:120:        Dim dt As DataTable = DbHelper.WykonajZapytanie( _
CustomerManager.vb:121:            "SELECT Email FROM Klienci WHERE Id = @Id", DbHelper.Parametr("@Id", klientId))
CustomerManager.vb:122:        If dt.Rows.Count = 0 Then Return ""
CustomerManager.vb:123:        Return dt.Rows(0)("Email").ToString()
CustomerManager.vb:124:    End Function
CustomerManager.vb:125:
CustomerManager.vb:126:    Public Function LiczbaOtwartychWypozyczen(ByVal klientId As Integer) As Integer
CustomerManager.vb:127:        Dim dt As DataTable = DbHelper.WykonajZapytanie( _
CustomerManager.vb:128:            "SELECT COUNT(*) AS Liczba FROM Wypozyczenia WHERE KlientId = @Id AND Status = 'O'", _
CustomerManager.vb:129:            DbHelper.Parametr("@Id", klientId))
CustomerManager.vb:130:        If dt.Rows.Count = 0 Then Return 0
CustomerManager.vb:131:        Return dt.Rows(0)("Liczba")
CustomerManager.vb:132:    End Function
CustomerManager.vb:133:
CustomerManager.vb:134:    Public Function KlienciZablokowani() As DataTable
CustomerManager.vb:135:        Return DbHelper.WykonajZapytanie( _
CustomerManager.vb:136:            "SELECT Id, Imie, Nazwisko, NazwaFirmy, Email, Telefon, PowodBlokady FROM Klienci " & _
CustomerManager.vb:137:            "WHERE Zablokowany = 1 ORDER BY Nazwisko, Imie")
CustomerManager.vb:138:    End Function
CustomerManager.vb:139:
CustomerManager.vb:140:    Public Function CzyEmailZajety(ByVal email As String, ByVal pominKlientaId As Integer) As Boolean
CustomerManager.vb:141:        If String.IsNullOrEmpty(email) Then Return False
CustomerManager.vb:142:        Dim dt As DataTable = DbHelper.WykonajZapytanie( _
CustomerManager.vb:143:            "SELECT COUNT(*) AS Liczba FROM Klienci WHERE Email = @Email AND Id <> @Id", _
CustomerManager.vb:144:            DbHelper.Parametr("@Email", email.Trim()), _
CustomerManager.vb:145:            DbHelper.Parametr("@Id", pominKlientaId))
CustomerManager.vb:146:        Return CInt(dt.Rows(0)("Liczba")) > 0
CustomerManager.vb:147:    End Function
CustomerManager.vb:148:
CustomerManager.vb:149:    Private Function ParametryKlienta(ByVal k As Klient) As SqlParameter()
CustomerManager.vb:150:        Return New SqlParameter() { _
CustomerManager.vb:151:            DbHelper.Parametr("@Imie", k.Imie), _
CustomerManager.vb:152:            DbHelper.Parametr("@Nazwisko", k.Nazwisko), _
CustomerManager.vb:153:            DbHelper.Parametr("@NazwaFirmy", PustyNaNull(k.NazwaFirmy)), _
CustomerManager.vb:154:            DbHelper.Parametr("@Email", k.Email), _
CustomerManager.vb:155:            DbHelper.Parametr("@Telefon", k.Telefon), _
CustomerManager.vb:156:            DbHelper.Parametr("@NumerDokumentu", PustyNaNull(k.NumerDokumentu)), _
CustomerManager.vb:157:            DbHelper.Parametr("@NIP", PustyNaNull(k.NIP)), _
CustomerManager.vb:158:            DbHelper.Parametr("@Typ", k.Typ), _
CustomerManager.vb:159:            DbHelper.Parametr("@ProgRabatu", k.ProgRabatu)}
CustomerManager.vb:160:    End Function
CustomerManager.vb:161:
CustomerManager.vb:162:    Private Function MapujKlienta(ByVal r As DataRow) As Klient
CustomerManager.vb:163:        Dim k As New Klient()
CustomerManager.vb:164:        k.Id = r("Id")
CustomerManager.vb:165:        k.Imie = r("Imie").ToString()
CustomerManager.vb:166:        k.Nazwisko = r("Nazwisko").ToString()
CustomerManager.vb:167:        k.NazwaFirmy = If(IsDBNull(r("NazwaFirmy")), Nothing, r("NazwaFirmy").ToString())
CustomerManager.vb:168:        k.Email = r("Email").ToString()
CustomerManager.vb:169:        k.Telefon = r("Telefon").ToString()
CustomerManager.vb:170:        k.NumerDokumentu = If(IsDBNull(r("NumerDokumentu")), Nothing, r("NumerDokumentu").ToString())
CustomerManager.vb:171:        k.NIP = If(IsDBNull(r("NIP")), Nothing, r("NIP").ToString())
CustomerManager.vb:172:        k.Typ = r("Typ").ToString()
CustomerManager.vb:173:        k.ProgRabatu = r("ProgRabatu")
CustomerManager.vb:174:        k.Zablokowany = r("Zablokowany")
CustomerManager.vb:175:        k.PowodBlokady = If(IsDBNull(r("PowodBlokady")), Nothing, r("PowodBlokady").ToString())
CustomerManager.vb:176:        k.DataRejestracji = r("DataRejestracji")
CustomerManager.vb:177:        Return k
CustomerManager.vb:178:    End Function
CustomerManager.vb:179:
CustomerManager.vb:180:    Private Function PustyNaNull(ByVal wartosc As String) As String
CustomerManager.vb:181:        If wartosc Is Nothing OrElse wartosc.Trim() = "" Then Return Nothing
CustomerManager.vb:182:        Return wartosc.Trim()
CustomerManager.vb:183:    End Function
CustomerManager.vb:184:
CustomerManager.vb:185:    Private Sub ZapiszDziennik(ByVal operacja As String, ByVal szczegoly As String)
CustomerManager.vb:186:        Try
CustomerManager.vb:187:            DbHelper.WykonajPolecenie( _
CustomerManager.vb:188:                "INSERT INTO DziennikOperacji (Operacja, Szczegoly) VALUES (@Operacja, @Szczegoly)", _
CustomerManager.vb:189:                DbHelper.Parametr("@Operacja", operacja), _
CustomerManager.vb:190:                DbHelper.Parametr("@Szczegoly", szczegoly))
CustomerManager.vb:191:        Catch ex As SqlException
CustomerManager.vb:192:            Log.Error("Nie udalo sie zapisac dziennika operacji", ex)
CustomerManager.vb:193:        End Try
CustomerManager.vb:194:    End Sub
CustomerManager.vb:195:
CustomerManager.vb:196:End Class
```

## M17 — surowy wynik H7

`git --no-pager -C 'legacy-app-parent\legacy-app\WypozyczalniaWeb' grep -n -I -e '^' -- 'Wypozyczenie.aspx.vb' 'Zwrot.aspx.vb'`

```text
Wypozyczenie.aspx.vb:1:' Modul: Wypozyczenie.aspx.vb | Autor: zespol IT | 2014-03-11
Wypozyczenie.aspx.vb:2:' Wydanie sprzetu klientowi (z rezerwacji albo od reki), opcjonalnie dostawa kurierem.
Wypozyczenie.aspx.vb:3:' 2015-06-22 MW: dostawa kurierem (KurierClient)
Wypozyczenie.aspx.vb:4:' 2018-02-09 PT: lista wydan i zwrotow z dnia na dole strony
Wypozyczenie.aspx.vb:5:
Wypozyczenie.aspx.vb:6:Imports System.Data
Wypozyczenie.aspx.vb:7:Imports System.Globalization
Wypozyczenie.aspx.vb:8:Imports Wypozyczalnia.Core
Wypozyczenie.aspx.vb:9:Imports Wypozyczalnia.Core.Model
Wypozyczenie.aspx.vb:10:
Wypozyczenie.aspx.vb:11:Partial Class Wypozyczenie
Wypozyczenie.aspx.vb:12:    Inherits System.Web.UI.Page
Wypozyczenie.aspx.vb:13:
Wypozyczenie.aspx.vb:14:    Protected WithEvents lblKlient As Global.System.Web.UI.WebControls.Label
Wypozyczenie.aspx.vb:15:    Protected WithEvents lblKomunikat As Global.System.Web.UI.WebControls.Label
Wypozyczenie.aspx.vb:16:    Protected WithEvents ddlRezerwacje As Global.System.Web.UI.WebControls.DropDownList
Wypozyczenie.aspx.vb:17:    Protected WithEvents ddlSprzet As Global.System.Web.UI.WebControls.DropDownList
Wypozyczenie.aspx.vb:18:    Protected WithEvents txtPlanowanyZwrot As Global.System.Web.UI.WebControls.TextBox
Wypozyczenie.aspx.vb:19:    Protected WithEvents rfvPlanowanyZwrot As Global.System.Web.UI.WebControls.RequiredFieldValidator
Wypozyczenie.aspx.vb:20:    Protected WithEvents lblKaucja As Global.System.Web.UI.WebControls.Label
Wypozyczenie.aspx.vb:21:    Protected WithEvents chkDostawa As Global.System.Web.UI.WebControls.CheckBox
Wypozyczenie.aspx.vb:22:    Protected WithEvents txtAdres As Global.System.Web.UI.WebControls.TextBox
Wypozyczenie.aspx.vb:23:    Protected WithEvents btnWydaj As Global.System.Web.UI.WebControls.Button
Wypozyczenie.aspx.vb:24:    Protected WithEvents lblNumerPrzesylki As Global.System.Web.UI.WebControls.Label
Wypozyczenie.aspx.vb:25:    Protected WithEvents gvWydaneDzis As Global.System.Web.UI.WebControls.GridView
Wypozyczenie.aspx.vb:26:
Wypozyczenie.aspx.vb:27:    Private Const FormatDaty As String = "yyyy-MM-dd HH:mm"
Wypozyczenie.aspx.vb:28:
Wypozyczenie.aspx.vb:29:    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
Wypozyczenie.aspx.vb:30:        If Not IsPostBack Then
Wypozyczenie.aspx.vb:31:            Dim klientId As Integer = Session("WybranyKlientId")
Wypozyczenie.aspx.vb:32:            If klientId = 0 Then
Wypozyczenie.aspx.vb:33:                lblKomunikat.Text = "Najpierw wybierz klienta na stronie Klienci."
Wypozyczenie.aspx.vb:34:                btnWydaj.Enabled = False
Wypozyczenie.aspx.vb:35:            Else
Wypozyczenie.aspx.vb:36:                PokazKlienta(klientId)
Wypozyczenie.aspx.vb:37:                WczytajRezerwacje(klientId)
Wypozyczenie.aspx.vb:38:            End If
Wypozyczenie.aspx.vb:39:            WczytajSprzet()
Wypozyczenie.aspx.vb:40:            WczytajRuchZDnia()
Wypozyczenie.aspx.vb:41:            Dim zAdresu As String = Request.QueryString("rezerwacja")
Wypozyczenie.aspx.vb:42:            If Not String.IsNullOrEmpty(zAdresu) AndAlso ddlRezerwacje.Items.FindByValue(zAdresu) IsNot Nothing Then
Wypozyczenie.aspx.vb:43:                ddlRezerwacje.SelectedValue = zAdresu
Wypozyczenie.aspx.vb:44:                ddlRezerwacje_SelectedIndexChanged(ddlRezerwacje, EventArgs.Empty)
Wypozyczenie.aspx.vb:45:            End If
Wypozyczenie.aspx.vb:46:        End If
Wypozyczenie.aspx.vb:47:    End Sub
Wypozyczenie.aspx.vb:48:
Wypozyczenie.aspx.vb:49:    Protected Sub gvWydaneDzis_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvWydaneDzis.RowDataBound
Wypozyczenie.aspx.vb:50:        If e.Row.RowType <> DataControlRowType.DataRow Then Return
Wypozyczenie.aspx.vb:51:        Dim wiersz As DataRowView = CType(e.Row.DataItem, DataRowView)
Wypozyczenie.aspx.vb:52:        If Not IsDBNull(wiersz("DataZwrotu")) Then
Wypozyczenie.aspx.vb:53:            e.Row.ForeColor = Drawing.Color.Gray
Wypozyczenie.aspx.vb:54:            e.Row.ToolTip = "Zwrocono " & CDate(wiersz("DataZwrotu")).ToString(FormatDaty)
Wypozyczenie.aspx.vb:55:        ElseIf CDate(wiersz("PlanowanyZwrot")) < Date.Now Then
Wypozyczenie.aspx.vb:56:            e.Row.ForeColor = Drawing.Color.DarkRed
Wypozyczenie.aspx.vb:57:            e.Row.ToolTip = "Po terminie zwrotu"
Wypozyczenie.aspx.vb:58:        End If
Wypozyczenie.aspx.vb:59:    End Sub
Wypozyczenie.aspx.vb:60:
Wypozyczenie.aspx.vb:61:    Private Sub PokazKlienta(ByVal klientId As Integer)
Wypozyczenie.aspx.vb:62:        Dim klient As Klient = New CustomerManager().PobierzKlienta(klientId)
Wypozyczenie.aspx.vb:63:        If klient Is Nothing Then
Wypozyczenie.aspx.vb:64:            lblKlient.Text = "(nie znaleziono klienta " & klientId & ")"
Wypozyczenie.aspx.vb:65:            btnWydaj.Enabled = False
Wypozyczenie.aspx.vb:66:            Return
Wypozyczenie.aspx.vb:67:        End If
Wypozyczenie.aspx.vb:68:        If klient.Typ = "F" AndAlso Not String.IsNullOrEmpty(klient.NazwaFirmy) Then
Wypozyczenie.aspx.vb:69:            lblKlient.Text = Server.HtmlEncode(klient.NazwaFirmy & " (" & klient.Imie & " " & klient.Nazwisko & ")")
Wypozyczenie.aspx.vb:70:        Else
Wypozyczenie.aspx.vb:71:            lblKlient.Text = Server.HtmlEncode(klient.Imie & " " & klient.Nazwisko)
Wypozyczenie.aspx.vb:72:        End If
Wypozyczenie.aspx.vb:73:        If klient.Zablokowany Then
Wypozyczenie.aspx.vb:74:            lblKomunikat.Text = "Klient zablokowany: " & Server.HtmlEncode(klient.PowodBlokady)
Wypozyczenie.aspx.vb:75:            btnWydaj.Enabled = False
Wypozyczenie.aspx.vb:76:            Return
Wypozyczenie.aspx.vb:77:        End If
Wypozyczenie.aspx.vb:78:        Dim otwarte As List(Of Model.Wypozyczenie) = New RentalManager().OtwarteKlienta(klientId)
Wypozyczenie.aspx.vb:79:        If otwarte.Count > 0 Then
Wypozyczenie.aspx.vb:80:            Dim poTerminie As Integer = 0
Wypozyczenie.aspx.vb:81:            For Each w As Model.Wypozyczenie In otwarte
Wypozyczenie.aspx.vb:82:                If w.CzyPoTerminie(Date.Now) Then poTerminie = poTerminie + 1
Wypozyczenie.aspx.vb:83:            Next
Wypozyczenie.aspx.vb:84:            lblKlient.Text = lblKlient.Text & " - otwarte wypozyczenia: " & otwarte.Count
Wypozyczenie.aspx.vb:85:            If poTerminie > 0 Then
Wypozyczenie.aspx.vb:86:                lblKomunikat.Text = "Uwaga: klient ma " & poTerminie & " wypozyczen po terminie zwrotu."
Wypozyczenie.aspx.vb:87:            End If
Wypozyczenie.aspx.vb:88:        End If
Wypozyczenie.aspx.vb:89:    End Sub
Wypozyczenie.aspx.vb:90:
Wypozyczenie.aspx.vb:91:    Private Sub WczytajRezerwacje(ByVal klientId As Integer)
Wypozyczenie.aspx.vb:92:        ddlRezerwacje.Items.Clear()
Wypozyczenie.aspx.vb:93:        ddlRezerwacje.Items.Add(New ListItem("-- bez rezerwacji --", "0"))
Wypozyczenie.aspx.vb:94:        Dim tabela As DataTable = New ReservationManager().RezerwacjeKlienta(klientId)
Wypozyczenie.aspx.vb:95:        For Each wiersz As DataRow In tabela.Rows
Wypozyczenie.aspx.vb:96:            Dim status As String = wiersz("Status").ToString()
Wypozyczenie.aspx.vb:97:            If status = "N" OrElse status = "P" Then
Wypozyczenie.aspx.vb:98:                Dim tekst As String = String.Format("#{0} {1} | {2:yyyy-MM-dd HH:mm} - {3:yyyy-MM-dd HH:mm} | {4}",
Wypozyczenie.aspx.vb:99:                    wiersz("Id"), wiersz("Sprzet"), wiersz("DataOd"), wiersz("DataDo"), status)
Wypozyczenie.aspx.vb:100:                ddlRezerwacje.Items.Add(New ListItem(tekst, wiersz("Id").ToString()))
Wypozyczenie.aspx.vb:101:            End If
Wypozyczenie.aspx.vb:102:        Next
Wypozyczenie.aspx.vb:103:    End Sub
Wypozyczenie.aspx.vb:104:
Wypozyczenie.aspx.vb:105:    Private Sub WczytajSprzet()
Wypozyczenie.aspx.vb:106:        ddlSprzet.Items.Clear()
Wypozyczenie.aspx.vb:107:        ddlSprzet.Items.Add(New ListItem("-- wybierz --", "0"))
Wypozyczenie.aspx.vb:108:        Dim lista As List(Of Sprzet) = New EquipmentManager().ListaWKategorii("")
Wypozyczenie.aspx.vb:109:        For Each s As Sprzet In lista
Wypozyczenie.aspx.vb:110:            If s.Status = "D" Then
Wypozyczenie.aspx.vb:111:                ddlSprzet.Items.Add(New ListItem(s.NumerInwentarzowy & " " & s.Nazwa, s.Id.ToString()))
Wypozyczenie.aspx.vb:112:            End If
Wypozyczenie.aspx.vb:113:        Next
Wypozyczenie.aspx.vb:114:    End Sub
Wypozyczenie.aspx.vb:115:
Wypozyczenie.aspx.vb:116:    Private Sub WczytajRuchZDnia()
Wypozyczenie.aspx.vb:117:        gvWydaneDzis.DataSource = New RentalManager().WypozyczeniaZDnia(Date.Today)
Wypozyczenie.aspx.vb:118:        gvWydaneDzis.DataBind()
Wypozyczenie.aspx.vb:119:    End Sub
Wypozyczenie.aspx.vb:120:
Wypozyczenie.aspx.vb:121:    Protected Sub ddlRezerwacje_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlRezerwacje.SelectedIndexChanged
Wypozyczenie.aspx.vb:122:        lblNumerPrzesylki.Text = "-"
Wypozyczenie.aspx.vb:123:        Dim rezerwacjaId As Integer = CInt(ddlRezerwacje.SelectedValue)
Wypozyczenie.aspx.vb:124:        If rezerwacjaId = 0 Then
Wypozyczenie.aspx.vb:125:            ddlSprzet.Enabled = True
Wypozyczenie.aspx.vb:126:            txtPlanowanyZwrot.Text = ""
Wypozyczenie.aspx.vb:127:            lblKaucja.Text = "-"
Wypozyczenie.aspx.vb:128:            Return
Wypozyczenie.aspx.vb:129:        End If
Wypozyczenie.aspx.vb:130:        Dim r As Rezerwacja = New ReservationManager().PobierzRezerwacje(rezerwacjaId)
Wypozyczenie.aspx.vb:131:        If r Is Nothing Then
Wypozyczenie.aspx.vb:132:            lblKomunikat.Text = "Nie znaleziono rezerwacji " & rezerwacjaId
Wypozyczenie.aspx.vb:133:            Return
Wypozyczenie.aspx.vb:134:        End If
Wypozyczenie.aspx.vb:135:        ddlSprzet.SelectedIndex = 0
Wypozyczenie.aspx.vb:136:        ddlSprzet.Enabled = False
Wypozyczenie.aspx.vb:137:        txtPlanowanyZwrot.Text = r.DataDo.ToString(FormatDaty)
Wypozyczenie.aspx.vb:138:        PokazKaucje(r.SprzetId)
Wypozyczenie.aspx.vb:139:    End Sub
Wypozyczenie.aspx.vb:140:
Wypozyczenie.aspx.vb:141:    Protected Sub ddlSprzet_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlSprzet.SelectedIndexChanged
Wypozyczenie.aspx.vb:142:        Dim sprzetId As Integer = CInt(ddlSprzet.SelectedValue)
Wypozyczenie.aspx.vb:143:        If sprzetId = 0 Then
Wypozyczenie.aspx.vb:144:            lblKaucja.Text = "-"
Wypozyczenie.aspx.vb:145:        Else
Wypozyczenie.aspx.vb:146:            PokazKaucje(sprzetId)
Wypozyczenie.aspx.vb:147:            If txtPlanowanyZwrot.Text = "" Then
Wypozyczenie.aspx.vb:148:                txtPlanowanyZwrot.Text = Date.Now.AddDays(1).ToString(FormatDaty)
Wypozyczenie.aspx.vb:149:            End If
Wypozyczenie.aspx.vb:150:        End If
Wypozyczenie.aspx.vb:151:    End Sub
Wypozyczenie.aspx.vb:152:
Wypozyczenie.aspx.vb:153:    Private Sub PokazKaucje(ByVal sprzetId As Integer)
Wypozyczenie.aspx.vb:154:        Dim s As Sprzet = New EquipmentManager().PobierzSprzet(sprzetId)
Wypozyczenie.aspx.vb:155:        If s Is Nothing Then
Wypozyczenie.aspx.vb:156:            lblKaucja.Text = "-"
Wypozyczenie.aspx.vb:157:        Else
Wypozyczenie.aspx.vb:158:            lblKaucja.Text = s.Kaucja.ToString("N2")
Wypozyczenie.aspx.vb:159:        End If
Wypozyczenie.aspx.vb:160:    End Sub
Wypozyczenie.aspx.vb:161:
Wypozyczenie.aspx.vb:162:    Protected Sub chkDostawa_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkDostawa.CheckedChanged
Wypozyczenie.aspx.vb:163:        txtAdres.Enabled = chkDostawa.Checked
Wypozyczenie.aspx.vb:164:        If Not chkDostawa.Checked Then
Wypozyczenie.aspx.vb:165:            txtAdres.Text = ""
Wypozyczenie.aspx.vb:166:        End If
Wypozyczenie.aspx.vb:167:    End Sub
Wypozyczenie.aspx.vb:168:
Wypozyczenie.aspx.vb:169:    Protected Sub btnWydaj_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnWydaj.Click
Wypozyczenie.aspx.vb:170:        Dim klientId As Integer = Session("WybranyKlientId")
Wypozyczenie.aspx.vb:171:        If klientId = 0 Then
Wypozyczenie.aspx.vb:172:            lblKomunikat.Text = "Sesja wygasla - wybierz klienta ponownie."
Wypozyczenie.aspx.vb:173:            Return
Wypozyczenie.aspx.vb:174:        End If
Wypozyczenie.aspx.vb:175:
Wypozyczenie.aspx.vb:176:        Dim rezerwacjaId As Integer = CInt(ddlRezerwacje.SelectedValue)
Wypozyczenie.aspx.vb:177:        Dim sprzetId As Integer
Wypozyczenie.aspx.vb:178:        If rezerwacjaId > 0 Then
Wypozyczenie.aspx.vb:179:            Dim r As Rezerwacja = New ReservationManager().PobierzRezerwacje(rezerwacjaId)
Wypozyczenie.aspx.vb:180:            If r Is Nothing OrElse r.KlientId <> klientId Then
Wypozyczenie.aspx.vb:181:                lblKomunikat.Text = "Rezerwacja nie nalezy do wybranego klienta."
Wypozyczenie.aspx.vb:182:                Return
Wypozyczenie.aspx.vb:183:            End If
Wypozyczenie.aspx.vb:184:            sprzetId = r.SprzetId
Wypozyczenie.aspx.vb:185:        Else
Wypozyczenie.aspx.vb:186:            sprzetId = CInt(ddlSprzet.SelectedValue)
Wypozyczenie.aspx.vb:187:        End If
Wypozyczenie.aspx.vb:188:        If sprzetId = 0 Then
Wypozyczenie.aspx.vb:189:            lblKomunikat.Text = "Wybierz rezerwacje albo sprzet."
Wypozyczenie.aspx.vb:190:            Return
Wypozyczenie.aspx.vb:191:        End If
Wypozyczenie.aspx.vb:192:
Wypozyczenie.aspx.vb:193:        Dim planowanyZwrot As Date
Wypozyczenie.aspx.vb:194:        If Not Date.TryParseExact(txtPlanowanyZwrot.Text.Trim(), FormatDaty, CultureInfo.InvariantCulture, DateTimeStyles.None, planowanyZwrot) Then
Wypozyczenie.aspx.vb:195:            lblKomunikat.Text = "Niepoprawny format daty zwrotu (rrrr-mm-dd gg:mm)."
Wypozyczenie.aspx.vb:196:            Return
Wypozyczenie.aspx.vb:197:        End If
Wypozyczenie.aspx.vb:198:        If planowanyZwrot <= Date.Now Then
Wypozyczenie.aspx.vb:199:            lblKomunikat.Text = "Planowany zwrot musi byc pozniejszy niz chwila wydania."
Wypozyczenie.aspx.vb:200:            Return
Wypozyczenie.aspx.vb:201:        End If
Wypozyczenie.aspx.vb:202:
Wypozyczenie.aspx.vb:203:        Dim adres As String = ""
Wypozyczenie.aspx.vb:204:        If chkDostawa.Checked Then
Wypozyczenie.aspx.vb:205:            adres = txtAdres.Text.Trim()
Wypozyczenie.aspx.vb:206:            If adres = "" Then
Wypozyczenie.aspx.vb:207:                lblKomunikat.Text = "Podaj adres dostawy albo odznacz dostawe."
Wypozyczenie.aspx.vb:208:                Return
Wypozyczenie.aspx.vb:209:            End If
Wypozyczenie.aspx.vb:210:        End If
Wypozyczenie.aspx.vb:211:
Wypozyczenie.aspx.vb:212:        Try
Wypozyczenie.aspx.vb:213:            Dim manager As New RentalManager()
Wypozyczenie.aspx.vb:214:            Dim noweId As Integer = manager.WydajSprzet(rezerwacjaId, klientId, sprzetId, planowanyZwrot, adres)
Wypozyczenie.aspx.vb:215:            Dim w As Model.Wypozyczenie = manager.PobierzWypozyczenie(noweId)
Wypozyczenie.aspx.vb:216:            lblKaucja.Text = w.KaucjaPobrana.ToString("N2")
Wypozyczenie.aspx.vb:217:            If chkDostawa.Checked Then
Wypozyczenie.aspx.vb:218:                If String.IsNullOrEmpty(w.NumerPrzesylki) Then
Wypozyczenie.aspx.vb:219:                    lblNumerPrzesylki.Text = "brak - zamow kuriera telefonicznie"
Wypozyczenie.aspx.vb:220:                Else
Wypozyczenie.aspx.vb:221:                    lblNumerPrzesylki.Text = w.NumerPrzesylki
Wypozyczenie.aspx.vb:222:                End If
Wypozyczenie.aspx.vb:223:            Else
Wypozyczenie.aspx.vb:224:                lblNumerPrzesylki.Text = "odbior osobisty"
Wypozyczenie.aspx.vb:225:            End If
Wypozyczenie.aspx.vb:226:            lblKomunikat.ForeColor = Drawing.Color.Green
Wypozyczenie.aspx.vb:227:            lblKomunikat.Text = "Wydano sprzet, wypozyczenie nr " & noweId & "."
Wypozyczenie.aspx.vb:228:            WczytajRezerwacje(klientId)
Wypozyczenie.aspx.vb:229:            WczytajSprzet()
Wypozyczenie.aspx.vb:230:            WczytajRuchZDnia()
Wypozyczenie.aspx.vb:231:            ddlSprzet.Enabled = True
Wypozyczenie.aspx.vb:232:            txtPlanowanyZwrot.Text = ""
Wypozyczenie.aspx.vb:233:            chkDostawa.Checked = False
Wypozyczenie.aspx.vb:234:            txtAdres.Text = ""
Wypozyczenie.aspx.vb:235:            txtAdres.Enabled = False
Wypozyczenie.aspx.vb:236:        Catch ex As BladBiznesowyException
Wypozyczenie.aspx.vb:237:            lblKomunikat.Text = ex.Message
Wypozyczenie.aspx.vb:238:        Catch ex As Exception
Wypozyczenie.aspx.vb:239:            lblKomunikat.Text = "Blad podczas wydania: " & ex.Message
Wypozyczenie.aspx.vb:240:        End Try
Wypozyczenie.aspx.vb:241:    End Sub
Wypozyczenie.aspx.vb:242:
Wypozyczenie.aspx.vb:243:End Class
Zwrot.aspx.vb:1:Option Strict Off
Zwrot.aspx.vb:2:
Zwrot.aspx.vb:3:' Modul: Zwrot.aspx.vb | Autor: zespol IT | 2014-03-11
Zwrot.aspx.vb:4:' Przyjecie zwrotu sprzetu: kara za spoznienie, szkody, rozliczenie kaucji.
Zwrot.aspx.vb:5:' 2014-09-30 JK: szkody z listy zamiast pola tekstowego
Zwrot.aspx.vb:6:' 2017-05-16 MW: link do faktury po zwrocie
Zwrot.aspx.vb:7:
Zwrot.aspx.vb:8:Imports System.Data
Zwrot.aspx.vb:9:Imports System.Globalization
Zwrot.aspx.vb:10:Imports Wypozyczalnia.Core
Zwrot.aspx.vb:11:Imports Wypozyczalnia.Core.Model
Zwrot.aspx.vb:12:
Zwrot.aspx.vb:13:Partial Class Zwrot
Zwrot.aspx.vb:14:    Inherits System.Web.UI.Page
Zwrot.aspx.vb:15:
Zwrot.aspx.vb:16:    Protected WithEvents lblKomunikat As Global.System.Web.UI.WebControls.Label
Zwrot.aspx.vb:17:    Protected WithEvents ddlOtwarte As Global.System.Web.UI.WebControls.DropDownList
Zwrot.aspx.vb:18:    Protected WithEvents lblSzczegoly As Global.System.Web.UI.WebControls.Label
Zwrot.aspx.vb:19:    Protected WithEvents txtDataZwrotu As Global.System.Web.UI.WebControls.TextBox
Zwrot.aspx.vb:20:    Protected WithEvents cblSzkody As Global.System.Web.UI.WebControls.CheckBoxList
Zwrot.aspx.vb:21:    Protected WithEvents btnPodglad As Global.System.Web.UI.WebControls.Button
Zwrot.aspx.vb:22:    Protected WithEvents btnZwrot As Global.System.Web.UI.WebControls.Button
Zwrot.aspx.vb:23:    Protected WithEvents lblOplata As Global.System.Web.UI.WebControls.Label
Zwrot.aspx.vb:24:    Protected WithEvents lblKara As Global.System.Web.UI.WebControls.Label
Zwrot.aspx.vb:25:    Protected WithEvents lblKaucjaZwrot As Global.System.Web.UI.WebControls.Label
Zwrot.aspx.vb:26:    Protected WithEvents lnkFaktura As Global.System.Web.UI.WebControls.HyperLink
Zwrot.aspx.vb:27:    Protected WithEvents gvOtwarte As Global.System.Web.UI.WebControls.GridView
Zwrot.aspx.vb:28:
Zwrot.aspx.vb:29:    Private Const FormatDaty As String = "yyyy-MM-dd HH:mm"
Zwrot.aspx.vb:30:
Zwrot.aspx.vb:31:    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
Zwrot.aspx.vb:32:        If Not IsPostBack Then
Zwrot.aspx.vb:33:            WypelnijSzkody()
Zwrot.aspx.vb:34:            WczytajOtwarte()
Zwrot.aspx.vb:35:            WyczyscRozliczenie()
Zwrot.aspx.vb:36:            Dim zAdresu As String = Request.QueryString("id")
Zwrot.aspx.vb:37:            If Not String.IsNullOrEmpty(zAdresu) AndAlso ddlOtwarte.Items.FindByValue(zAdresu) IsNot Nothing Then
Zwrot.aspx.vb:38:                ddlOtwarte.SelectedValue = zAdresu
Zwrot.aspx.vb:39:                ddlOtwarte_SelectedIndexChanged(ddlOtwarte, EventArgs.Empty)
Zwrot.aspx.vb:40:            End If
Zwrot.aspx.vb:41:        End If
Zwrot.aspx.vb:42:    End Sub
Zwrot.aspx.vb:43:
Zwrot.aspx.vb:44:    Protected Sub gvOtwarte_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvOtwarte.RowDataBound
Zwrot.aspx.vb:45:        If e.Row.RowType <> DataControlRowType.DataRow Then Return
Zwrot.aspx.vb:46:        Dim wiersz As DataRowView = e.Row.DataItem
Zwrot.aspx.vb:47:        Dim plan As Date = wiersz("PlanowanyZwrot")
Zwrot.aspx.vb:48:        If plan < Date.Now.AddHours(-1) Then
Zwrot.aspx.vb:49:            e.Row.BackColor = Drawing.Color.MistyRose
Zwrot.aspx.vb:50:            e.Row.ToolTip = "Po terminie zwrotu"
Zwrot.aspx.vb:51:        ElseIf plan < Date.Now.AddHours(24) Then
Zwrot.aspx.vb:52:            e.Row.BackColor = Drawing.Color.LightYellow
Zwrot.aspx.vb:53:            e.Row.ToolTip = "Zwrot w ciagu doby"
Zwrot.aspx.vb:54:        End If
Zwrot.aspx.vb:55:    End Sub
Zwrot.aspx.vb:56:
Zwrot.aspx.vb:57:    Protected Sub gvOtwarte_SelectedIndexChanging(ByVal sender As Object, ByVal e As GridViewSelectEventArgs) Handles gvOtwarte.SelectedIndexChanging
Zwrot.aspx.vb:58:        Dim id As String = gvOtwarte.Rows(e.NewSelectedIndex).Cells(0).Text
Zwrot.aspx.vb:59:        If ddlOtwarte.Items.FindByValue(id) IsNot Nothing Then
Zwrot.aspx.vb:60:            ddlOtwarte.SelectedValue = id
Zwrot.aspx.vb:61:            ddlOtwarte_SelectedIndexChanged(ddlOtwarte, EventArgs.Empty)
Zwrot.aspx.vb:62:        End If
Zwrot.aspx.vb:63:    End Sub
Zwrot.aspx.vb:64:
Zwrot.aspx.vb:65:    Private Sub WypelnijSzkody()
Zwrot.aspx.vb:66:        cblSzkody.Items.Clear()
Zwrot.aspx.vb:67:        cblSzkody.Items.Add(New ListItem("Uszkodzona obudowa (50 zł)", "Uszkodzona obudowa"))
Zwrot.aspx.vb:68:        cblSzkody.Items.Add(New ListItem("Brak akcesoriów (30 zł)", "Brak akcesoriów"))
Zwrot.aspx.vb:69:        cblSzkody.Items.Add(New ListItem("Zabrudzenie (20 zł)", "Zabrudzenie"))
Zwrot.aspx.vb:70:    End Sub
Zwrot.aspx.vb:71:
Zwrot.aspx.vb:72:    Private Sub WczytajOtwarte()
Zwrot.aspx.vb:73:        Dim tabela As DataTable = New RentalManager().Otwarte()
Zwrot.aspx.vb:74:        ddlOtwarte.Items.Clear()
Zwrot.aspx.vb:75:        ddlOtwarte.Items.Add(New ListItem("-- wybierz --", "0"))
Zwrot.aspx.vb:76:        For Each wiersz As DataRow In tabela.Rows
Zwrot.aspx.vb:77:            Dim plan As Date = wiersz("PlanowanyZwrot")
Zwrot.aspx.vb:78:            Dim tekst As String = "#" & wiersz("Id") & " " & wiersz("Klient") & " | " & wiersz("Sprzet") & " | do " & plan.ToString(FormatDaty)
Zwrot.aspx.vb:79:            If plan < Date.Now Then
Zwrot.aspx.vb:80:                tekst = tekst & " (po terminie)"
Zwrot.aspx.vb:81:            End If
Zwrot.aspx.vb:82:            ddlOtwarte.Items.Add(New ListItem(tekst, wiersz("Id").ToString()))
Zwrot.aspx.vb:83:        Next
Zwrot.aspx.vb:84:        gvOtwarte.DataSource = tabela
Zwrot.aspx.vb:85:        gvOtwarte.DataBind()
Zwrot.aspx.vb:86:    End Sub
Zwrot.aspx.vb:87:
Zwrot.aspx.vb:88:    Private Sub WyczyscRozliczenie()
Zwrot.aspx.vb:89:        lblOplata.Text = "-"
Zwrot.aspx.vb:90:        lblKara.Text = "-"
Zwrot.aspx.vb:91:        lblKaucjaZwrot.Text = "-"
Zwrot.aspx.vb:92:        lnkFaktura.Visible = False
Zwrot.aspx.vb:93:    End Sub
Zwrot.aspx.vb:94:
Zwrot.aspx.vb:95:    Protected Sub ddlOtwarte_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlOtwarte.SelectedIndexChanged
Zwrot.aspx.vb:96:        WyczyscRozliczenie()
Zwrot.aspx.vb:97:        For Each item As ListItem In cblSzkody.Items
Zwrot.aspx.vb:98:            item.Selected = False
Zwrot.aspx.vb:99:        Next
Zwrot.aspx.vb:100:        Dim id As Integer = ddlOtwarte.SelectedValue
Zwrot.aspx.vb:101:        If id = 0 Then
Zwrot.aspx.vb:102:            lblSzczegoly.Text = "-"
Zwrot.aspx.vb:103:            Return
Zwrot.aspx.vb:104:        End If
Zwrot.aspx.vb:105:        Dim w As Wypozyczenie = New RentalManager().PobierzWypozyczenie(id)
Zwrot.aspx.vb:106:        If w Is Nothing Then
Zwrot.aspx.vb:107:            lblSzczegoly.Text = "-"
Zwrot.aspx.vb:108:            lblKomunikat.Text = "Nie znaleziono wypozyczenia " & id
Zwrot.aspx.vb:109:            Return
Zwrot.aspx.vb:110:        End If
Zwrot.aspx.vb:111:        Dim s As Sprzet = New EquipmentManager().PobierzSprzet(w.SprzetId)
Zwrot.aspx.vb:112:        Dim opis As String = "Wydano " & w.DataWydania.ToString(FormatDaty) & ", zwrot do " & w.PlanowanyZwrot.ToString(FormatDaty) &
Zwrot.aspx.vb:113:            ", kaucja " & w.KaucjaPobrana.ToString("N2") & " zł"
Zwrot.aspx.vb:114:        If s IsNot Nothing Then
Zwrot.aspx.vb:115:            opis = s.NumerInwentarzowy & " " & s.Nazwa & " (" & s.StawkaDobowa.ToString("N2") & " zł/doba). " & opis
Zwrot.aspx.vb:116:        End If
Zwrot.aspx.vb:117:        If Not String.IsNullOrEmpty(w.NumerPrzesylki) Then
Zwrot.aspx.vb:118:            opis = opis & ", przesylka " & w.NumerPrzesylki
Zwrot.aspx.vb:119:        End If
Zwrot.aspx.vb:120:        lblSzczegoly.Text = Server.HtmlEncode(opis)
Zwrot.aspx.vb:121:        If txtDataZwrotu.Text = "" Then
Zwrot.aspx.vb:122:            txtDataZwrotu.Text = Date.Now.ToString(FormatDaty)
Zwrot.aspx.vb:123:        End If
Zwrot.aspx.vb:124:    End Sub
Zwrot.aspx.vb:125:
Zwrot.aspx.vb:126:    Private Function OpisSpoznienia(ByVal plan As Date, ByVal zwrot As Date) As String
Zwrot.aspx.vb:127:        Dim minuty As Long = DateDiff(DateInterval.Minute, plan, zwrot)
Zwrot.aspx.vb:128:        If minuty <= 0 Then
Zwrot.aspx.vb:129:            Return "zwrot w terminie"
Zwrot.aspx.vb:130:        End If
Zwrot.aspx.vb:131:        If minuty <= 60 Then
Zwrot.aspx.vb:132:            Return "spoznienie " & minuty & " min (w tolerancji)"
Zwrot.aspx.vb:133:        End If
Zwrot.aspx.vb:134:        Dim godziny As Long = minuty \ 60
Zwrot.aspx.vb:135:        Dim reszta As Long = minuty Mod 60
Zwrot.aspx.vb:136:        If godziny < 24 Then
Zwrot.aspx.vb:137:            Return "spoznienie " & godziny & " h " & reszta & " min"
Zwrot.aspx.vb:138:        End If
Zwrot.aspx.vb:139:        Return "spoznienie " & (godziny \ 24) & " d " & (godziny Mod 24) & " h " & reszta & " min"
Zwrot.aspx.vb:140:    End Function
Zwrot.aspx.vb:141:
Zwrot.aspx.vb:142:    Private Function OdczytajDateZwrotu(ByRef dataZwrotu As Date) As Boolean
Zwrot.aspx.vb:143:        Dim tekst As String = txtDataZwrotu.Text.Trim()
Zwrot.aspx.vb:144:        If tekst = "" Then
Zwrot.aspx.vb:145:            dataZwrotu = Date.Now
Zwrot.aspx.vb:146:            Return True
Zwrot.aspx.vb:147:        End If
Zwrot.aspx.vb:148:        If Date.TryParseExact(tekst, FormatDaty, CultureInfo.InvariantCulture, DateTimeStyles.None, dataZwrotu) Then
Zwrot.aspx.vb:149:            Return True
Zwrot.aspx.vb:150:        End If
Zwrot.aspx.vb:151:        Return Date.TryParse(tekst, dataZwrotu)
Zwrot.aspx.vb:152:    End Function
Zwrot.aspx.vb:153:
Zwrot.aspx.vb:154:    Protected Sub btnPodglad_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPodglad.Click
Zwrot.aspx.vb:155:        Dim id As Integer = ddlOtwarte.SelectedValue
Zwrot.aspx.vb:156:        If id = 0 Then
Zwrot.aspx.vb:157:            lblKomunikat.Text = "Wybierz wypozyczenie."
Zwrot.aspx.vb:158:            Return
Zwrot.aspx.vb:159:        End If
Zwrot.aspx.vb:160:        Dim dataZwrotu As Date
Zwrot.aspx.vb:161:        If Not OdczytajDateZwrotu(dataZwrotu) Then
Zwrot.aspx.vb:162:            lblKomunikat.Text = "Niepoprawna data zwrotu."
Zwrot.aspx.vb:163:            Return
Zwrot.aspx.vb:164:        End If
Zwrot.aspx.vb:165:        Dim manager As New RentalManager()
Zwrot.aspx.vb:166:        Dim w As Wypozyczenie = manager.PobierzWypozyczenie(id)
Zwrot.aspx.vb:167:        If w Is Nothing Then
Zwrot.aspx.vb:168:            lblKomunikat.Text = "Nie znaleziono wypozyczenia " & id
Zwrot.aspx.vb:169:            Return
Zwrot.aspx.vb:170:        End If
Zwrot.aspx.vb:171:        Dim s As Sprzet = New EquipmentManager().PobierzSprzet(w.SprzetId)
Zwrot.aspx.vb:172:        Dim kara As Decimal = manager.ObliczKare(w.PlanowanyZwrot, dataZwrotu, s.StawkaDobowa)
Zwrot.aspx.vb:173:        Dim szkody As Decimal = 0
Zwrot.aspx.vb:174:        For Each item As ListItem In cblSzkody.Items
Zwrot.aspx.vb:175:            If item.Selected Then
Zwrot.aspx.vb:176:                Select Case item.Value
Zwrot.aspx.vb:177:                    Case "Uszkodzona obudowa"
Zwrot.aspx.vb:178:                        szkody = szkody + 50
Zwrot.aspx.vb:179:                    Case "Brak akcesoriów"
Zwrot.aspx.vb:180:                        szkody = szkody + 30
Zwrot.aspx.vb:181:                    Case "Zabrudzenie"
Zwrot.aspx.vb:182:                        szkody = szkody + 20
Zwrot.aspx.vb:183:                End Select
Zwrot.aspx.vb:184:            End If
Zwrot.aspx.vb:185:        Next
Zwrot.aspx.vb:186:        Dim kaucja As Decimal = w.KaucjaPobrana - kara - szkody
Zwrot.aspx.vb:187:        If kaucja < 0 Then kaucja = 0
Zwrot.aspx.vb:188:        lblOplata.Text = "(przy zwrocie)"
Zwrot.aspx.vb:189:        lblKara.Text = kara
Zwrot.aspx.vb:190:        lblKaucjaZwrot.Text = kaucja
Zwrot.aspx.vb:191:        lblKomunikat.ForeColor = Drawing.Color.Navy
Zwrot.aspx.vb:192:        lblKomunikat.Text = "Podglad (" & OpisSpoznienia(w.PlanowanyZwrot, dataZwrotu) & ") - zwrot nie zostal jeszcze zapisany."
Zwrot.aspx.vb:193:    End Sub
Zwrot.aspx.vb:194:
Zwrot.aspx.vb:195:    Protected Sub btnZwrot_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZwrot.Click
Zwrot.aspx.vb:196:        Dim id As Integer = ddlOtwarte.SelectedValue
Zwrot.aspx.vb:197:        If id = 0 Then
Zwrot.aspx.vb:198:            lblKomunikat.Text = "Wybierz wypozyczenie."
Zwrot.aspx.vb:199:            Return
Zwrot.aspx.vb:200:        End If
Zwrot.aspx.vb:201:
Zwrot.aspx.vb:202:        Dim dataZwrotu As Date
Zwrot.aspx.vb:203:        If Not OdczytajDateZwrotu(dataZwrotu) Then
Zwrot.aspx.vb:204:            lblKomunikat.Text = "Niepoprawna data zwrotu."
Zwrot.aspx.vb:205:            Return
Zwrot.aspx.vb:206:        End If
Zwrot.aspx.vb:207:        If dataZwrotu > Date.Now.AddMinutes(5) Then
Zwrot.aspx.vb:208:            lblKomunikat.Text = "Data zwrotu nie moze byc z przyszlosci."
Zwrot.aspx.vb:209:            Return
Zwrot.aspx.vb:210:        End If
Zwrot.aspx.vb:211:
Zwrot.aspx.vb:212:        Dim manager As New RentalManager()
Zwrot.aspx.vb:213:        Dim przed As Wypozyczenie = manager.PobierzWypozyczenie(id)
Zwrot.aspx.vb:214:        If przed Is Nothing Then
Zwrot.aspx.vb:215:            lblKomunikat.Text = "Nie znaleziono wypozyczenia " & id
Zwrot.aspx.vb:216:            Return
Zwrot.aspx.vb:217:        End If
Zwrot.aspx.vb:218:        If dataZwrotu < przed.DataWydania Then
Zwrot.aspx.vb:219:            lblKomunikat.Text = "Data zwrotu wczesniejsza niz data wydania (" & przed.DataWydania.ToString(FormatDaty) & ")."
Zwrot.aspx.vb:220:            Return
Zwrot.aspx.vb:221:        End If
Zwrot.aspx.vb:222:
Zwrot.aspx.vb:223:        ' slownik szkod: kwoty uzgodnione z kierownikiem punktu (cennik 2014)
Zwrot.aspx.vb:224:        Dim cennikSzkod As New Dictionary(Of String, Decimal)
Zwrot.aspx.vb:225:        cennikSzkod.Add("Uszkodzona obudowa", 50D)
Zwrot.aspx.vb:226:        cennikSzkod.Add("Brak akcesoriów", 30D)
Zwrot.aspx.vb:227:        cennikSzkod.Add("Zabrudzenie", 20D)
Zwrot.aspx.vb:228:
Zwrot.aspx.vb:229:        Dim szkody As Decimal = 0
Zwrot.aspx.vb:230:        Dim opisSzkod As New List(Of String)
Zwrot.aspx.vb:231:        For Each item As ListItem In cblSzkody.Items
Zwrot.aspx.vb:232:            If item.Selected AndAlso cennikSzkod.ContainsKey(item.Value) Then
Zwrot.aspx.vb:233:                szkody = szkody + cennikSzkod(item.Value)
Zwrot.aspx.vb:234:                opisSzkod.Add(item.Value)
Zwrot.aspx.vb:235:            End If
Zwrot.aspx.vb:236:        Next
Zwrot.aspx.vb:237:        If szkody > przed.KaucjaPobrana Then
Zwrot.aspx.vb:238:            lblKomunikat.Text = "Uwaga: szkody przekraczaja kaucje - roznica trafi na fakture."
Zwrot.aspx.vb:239:        End If
Zwrot.aspx.vb:240:
Zwrot.aspx.vb:241:        Dim po As Wypozyczenie
Zwrot.aspx.vb:242:        Try
Zwrot.aspx.vb:243:            po = manager.ZarejestrujZwrot(id, dataZwrotu, szkody)
Zwrot.aspx.vb:244:        Catch ex As BladBiznesowyException
Zwrot.aspx.vb:245:            lblKomunikat.Text = ex.Message
Zwrot.aspx.vb:246:            Return
Zwrot.aspx.vb:247:        Catch ex As Exception
Zwrot.aspx.vb:248:            lblKomunikat.Text = "Blad rejestracji zwrotu: " & ex.Message
Zwrot.aspx.vb:249:            Return
Zwrot.aspx.vb:250:        End Try
Zwrot.aspx.vb:251:
Zwrot.aspx.vb:252:        lblOplata.Text = po.Oplata
Zwrot.aspx.vb:253:        lblKara.Text = po.Kara
Zwrot.aspx.vb:254:        lblKaucjaZwrot.Text = po.KaucjaZwrocona
Zwrot.aspx.vb:255:
Zwrot.aspx.vb:256:        Session("OstatnieWypozyczenieId") = po.Id
Zwrot.aspx.vb:257:        lnkFaktura.NavigateUrl = "Faktura.aspx?id=" & po.Id
Zwrot.aspx.vb:258:        lnkFaktura.Visible = True
Zwrot.aspx.vb:259:
Zwrot.aspx.vb:260:        Dim komunikat As String = "Zwrot zarejestrowany (wypozyczenie nr " & po.Id & ")."
Zwrot.aspx.vb:261:        If po.Kara > 0 Then
Zwrot.aspx.vb:262:            komunikat = komunikat & " Naliczono kare za spoznienie (" & OpisSpoznienia(po.PlanowanyZwrot, dataZwrotu) & ")."
Zwrot.aspx.vb:263:        End If
Zwrot.aspx.vb:264:        If opisSzkod.Count > 0 Then
Zwrot.aspx.vb:265:            komunikat = komunikat & " Szkody: " & String.Join(", ", opisSzkod.ToArray()) & "."
Zwrot.aspx.vb:266:        End If
Zwrot.aspx.vb:267:        If po.KaucjaZwrocona = 0 AndAlso po.KaucjaPobrana > 0 Then
Zwrot.aspx.vb:268:            komunikat = komunikat & " Kaucja w calosci zatrzymana."
Zwrot.aspx.vb:269:        End If
Zwrot.aspx.vb:270:        lblKomunikat.ForeColor = Drawing.Color.Green
Zwrot.aspx.vb:271:        lblKomunikat.Text = Server.HtmlEncode(komunikat)
Zwrot.aspx.vb:272:
Zwrot.aspx.vb:273:        txtDataZwrotu.Text = ""
Zwrot.aspx.vb:274:        For Each item As ListItem In cblSzkody.Items
Zwrot.aspx.vb:275:            item.Selected = False
Zwrot.aspx.vb:276:        Next
Zwrot.aspx.vb:277:        lblSzczegoly.Text = "-"
Zwrot.aspx.vb:278:        WczytajOtwarte()
Zwrot.aspx.vb:279:    End Sub
Zwrot.aspx.vb:280:
Zwrot.aspx.vb:281:End Class
```

## M18 — surowy wynik H8

`git --no-pager -C 'legacy-app-parent\legacy-app\WypozyczalniaCore' grep -n -I -e '^' -- 'PricingManager.vb'`

```text
PricingManager.vb:1:Option Strict Off
PricingManager.vb:2:' Modul: PricingManager - oplata za wynajem, rabaty, taryfy | Autor: zespol IT | 2014-03-11
PricingManager.vb:3:' Historia:
PricingManager.vb:4:'   2015-02-04 MW: mnoznik weekendowy i rabat tygodniowy z tabeli Taryfy
PricingManager.vb:5:'   2016-11-08 JK: rabat stalego klienta wg progu
PricingManager.vb:6:'   2019-04-02 PT: SzczegolyOplaty dla kalkulatora w Cennik.aspx
PricingManager.vb:7:
PricingManager.vb:8:Imports System.Data
PricingManager.vb:9:Imports System.Text
PricingManager.vb:10:Imports System.Globalization
PricingManager.vb:11:Imports Wypozyczalnia.Core.Model
PricingManager.vb:12:Imports Wypozyczalnia.Core.DAL
PricingManager.vb:13:
PricingManager.vb:14:Public Class PricingManager
PricingManager.vb:15:
PricingManager.vb:16:    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(PricingManager))
PricingManager.vb:17:
PricingManager.vb:18:    Private Const MaksMnoznikWeekend As Decimal = 3D
PricingManager.vb:19:    Private Const MaksRabatTygodniowy As Decimal = 50D
PricingManager.vb:20:    Private Const MaksStawkaDobowa As Decimal = 10000D
PricingManager.vb:21:
PricingManager.vb:22:    Public Function LiczbaDob(ByVal dataOd As Date, ByVal dataDo As Date) As Integer
PricingManager.vb:23:        Dim minuty As Long = DateDiff(DateInterval.Minute, dataOd, dataDo)
PricingManager.vb:24:        If minuty <= 0 Then Throw New ArgumentException("Data zwrotu musi byc pozniejsza niz data wydania")
PricingManager.vb:25:        Dim doby As Integer = CInt(minuty \ 1440)
PricingManager.vb:26:        If minuty Mod 1440 > 60 Then doby = doby + 1   ' rozpoczeta doba liczona jako pelna (1 h tolerancji)
PricingManager.vb:27:        If doby = 0 Then doby = 1
PricingManager.vb:28:        Return doby
PricingManager.vb:29:    End Function
PricingManager.vb:30:
PricingManager.vb:31:    Public Function ObliczOplate(ByVal sprzetId As Integer, ByVal dataOd As Date, ByVal dataDo As Date, ByVal klientId As Integer) As Decimal
PricingManager.vb:32:        Dim sprzet As Sprzet = New EquipmentManager().PobierzSprzet(sprzetId)
PricingManager.vb:33:        If sprzet Is Nothing Then Throw New BladBiznesowyException("Nieznany sprzet: " & sprzetId)
PricingManager.vb:34:        Dim taryfa As Taryfa = New PricingDAL().PobierzTaryfe(sprzet.Kategoria, dataOd)
PricingManager.vb:35:        Dim doby As Integer = LiczbaDob(dataOd, dataDo)
PricingManager.vb:36:        Dim stawkaWeekend As Decimal = Math.Round(sprzet.StawkaDobowa * taryfa.MnoznikWeekend, 2)
PricingManager.vb:37:        Dim kwota As Decimal = 0
PricingManager.vb:38:        For k As Integer = 0 To doby - 1
PricingManager.vb:39:            Dim dzien As DayOfWeek = dataOd.AddDays(k).DayOfWeek
PricingManager.vb:40:            If dzien = DayOfWeek.Saturday Or dzien = DayOfWeek.Sunday Then
PricingManager.vb:41:                kwota = kwota + stawkaWeekend
PricingManager.vb:42:            Else
PricingManager.vb:43:                kwota = kwota + sprzet.StawkaDobowa
PricingManager.vb:44:            End If
PricingManager.vb:45:        Next
PricingManager.vb:46:        If doby \ 7 >= 1 Then
PricingManager.vb:47:            kwota = kwota - Math.Round(kwota * taryfa.RabatTygodniowyProc / 100D, 2)
PricingManager.vb:48:        End If
PricingManager.vb:49:        ' rabat 2,5% za kazdy prog stalego klienta (prog 1 = 2,5%)
PricingManager.vb:50:        Dim prog = New CustomerManager().ProgRabatu(klientId)
PricingManager.vb:51:        Dim rabatProc As Integer = RabatProgu(prog)
PricingManager.vb:52:        If rabatProc > 0 Then
PricingManager.vb:53:            kwota = Math.Round(kwota * (100 - rabatProc) / 100D, 2)
PricingManager.vb:54:        End If
PricingManager.vb:55:        Return kwota
PricingManager.vb:56:    End Function
PricingManager.vb:57:
PricingManager.vb:58:    Public Function RabatProgu(ByVal prog As Integer) As Integer
PricingManager.vb:59:        Return CInt(prog * 2.5)
PricingManager.vb:60:    End Function
PricingManager.vb:61:
PricingManager.vb:62:    Public Function ObliczOplateBezKlienta(ByVal sprzetId As Integer, ByVal dataOd As Date, ByVal dataDo As Date) As Decimal
PricingManager.vb:63:        Return ObliczOplate(sprzetId, dataOd, dataDo, 0)
PricingManager.vb:64:    End Function
PricingManager.vb:65:
PricingManager.vb:66:    Public Function LiczbaDobWeekendowych(ByVal dataOd As Date, ByVal dataDo As Date) As Integer
PricingManager.vb:67:        Dim doby As Integer = LiczbaDob(dataOd, dataDo)
PricingManager.vb:68:        Dim ile As Integer = 0
PricingManager.vb:69:        For k As Integer = 0 To doby - 1
PricingManager.vb:70:            If CzyWeekend(dataOd.AddDays(k)) Then ile = ile + 1
PricingManager.vb:71:        Next
PricingManager.vb:72:        Return ile
PricingManager.vb:73:    End Function
PricingManager.vb:74:
PricingManager.vb:75:    Public Function SzczegolyOplaty(ByVal sprzetId As Integer, ByVal dataOd As Date, ByVal dataDo As Date, ByVal klientId As Integer) As String
PricingManager.vb:76:        Dim sprzet As Sprzet = New EquipmentManager().PobierzSprzet(sprzetId)
PricingManager.vb:77:        If sprzet Is Nothing Then Return "Nieznany sprzet: " & sprzetId
PricingManager.vb:78:        Dim taryfa As Taryfa = PobierzTaryfeDlaSprzetu(sprzetId, dataOd)
PricingManager.vb:79:        Dim doby As Integer = LiczbaDob(dataOd, dataDo)
PricingManager.vb:80:        Dim weekendowe As Integer = LiczbaDobWeekendowych(dataOd, dataDo)
PricingManager.vb:81:        Dim prog As Integer = New CustomerManager().ProgRabatu(klientId)
PricingManager.vb:82:        Dim kultura As New CultureInfo("pl-PL")
PricingManager.vb:83:        Dim sb As New StringBuilder()
PricingManager.vb:84:        sb.Append(sprzet.Nazwa).Append(" (").Append(sprzet.NumerInwentarzowy).Append(")")
PricingManager.vb:85:        sb.Append(" | dob: ").Append(doby)
PricingManager.vb:86:        sb.Append(" (w tym weekendowych: ").Append(weekendowe).Append(")")
PricingManager.vb:87:        sb.Append(" | stawka: ").Append(sprzet.StawkaDobowa.ToString("N2", kultura))
PricingManager.vb:88:        sb.Append(" | weekend x").Append(taryfa.MnoznikWeekend.ToString(kultura))
PricingManager.vb:89:        If doby \ 7 >= 1 Then
PricingManager.vb:90:            sb.Append(" | rabat tygodniowy ").Append(taryfa.RabatTygodniowyProc.ToString(kultura)).Append("%")
PricingManager.vb:91:        End If
PricingManager.vb:92:        If prog > 0 Then
PricingManager.vb:93:            sb.Append(" | ").Append(OpisRabatu(prog))
PricingManager.vb:94:        End If
PricingManager.vb:95:        sb.Append(" | razem: ").Append(ObliczOplate(sprzetId, dataOd, dataDo, klientId).ToString("N2", kultura))
PricingManager.vb:96:        Return sb.ToString()
PricingManager.vb:97:    End Function
PricingManager.vb:98:
PricingManager.vb:99:    Public Function OpisRabatu(ByVal prog As Integer) As String
PricingManager.vb:100:        If prog <= 0 Then Return "brak rabatu"
PricingManager.vb:101:        Return "staly klient, prog " & prog & ": " & RabatProgu(prog) & "%"
PricingManager.vb:102:    End Function
PricingManager.vb:103:
PricingManager.vb:104:    Public Function PobierzTaryfeDlaSprzetu(ByVal sprzetId As Integer, ByVal naDzien As Date) As Taryfa
PricingManager.vb:105:        Dim sprzet As Sprzet = New EquipmentManager().PobierzSprzet(sprzetId)
PricingManager.vb:106:        If sprzet Is Nothing Then Throw New BladBiznesowyException("Nieznany sprzet: " & sprzetId)
PricingManager.vb:107:        Return New PricingDAL().PobierzTaryfe(sprzet.Kategoria, naDzien)
PricingManager.vb:108:    End Function
PricingManager.vb:109:
PricingManager.vb:110:    Public Function ListaTaryf() As DataTable
PricingManager.vb:111:        Return New PricingDAL().ListaTaryf()
PricingManager.vb:112:    End Function
PricingManager.vb:113:
PricingManager.vb:114:    Public Sub ZapiszTaryfe(ByVal t As Taryfa)
PricingManager.vb:115:        If t Is Nothing Then Throw New ArgumentNullException("t")
PricingManager.vb:116:        If Not Taryfa.CzyZnanaKategoria(t.Kategoria) Then
PricingManager.vb:117:            Throw New ArgumentException("Nieznana kategoria: " & t.Kategoria)
PricingManager.vb:118:        End If
PricingManager.vb:119:        If t.MnoznikWeekend < 1D Or t.MnoznikWeekend > MaksMnoznikWeekend Then
PricingManager.vb:120:            Throw New ArgumentException("Mnoznik weekendowy poza zakresem 1-" & MaksMnoznikWeekend)
PricingManager.vb:121:        End If
PricingManager.vb:122:        If t.RabatTygodniowyProc < 0D Or t.RabatTygodniowyProc > MaksRabatTygodniowy Then
PricingManager.vb:123:            Throw New ArgumentException("Rabat tygodniowy poza zakresem 0-" & MaksRabatTygodniowy)
PricingManager.vb:124:        End If
PricingManager.vb:125:        Dim dal As New PricingDAL()
PricingManager.vb:126:        dal.ZapiszTaryfe(t)
PricingManager.vb:127:        dal.ZapiszWDzienniku("TARYFA", t.ToString())
PricingManager.vb:128:        Log.Info("Zapisano taryfe " & t.ToString())
PricingManager.vb:129:    End Sub
PricingManager.vb:130:
PricingManager.vb:131:    Public Sub ZmienStawke(ByVal sprzetId As Integer, ByVal nowaStawka As Decimal)
PricingManager.vb:132:        If nowaStawka <= 0D Or nowaStawka > MaksStawkaDobowa Then
PricingManager.vb:133:            Throw New ArgumentException("Nieprawidlowa stawka dobowa: " & nowaStawka)
PricingManager.vb:134:        End If
PricingManager.vb:135:        Dim em As New EquipmentManager()
PricingManager.vb:136:        Dim sprzet As Sprzet = em.PobierzSprzet(sprzetId)
PricingManager.vb:137:        If sprzet Is Nothing Then Throw New BladBiznesowyException("Nieznany sprzet: " & sprzetId)
PricingManager.vb:138:        Dim stara As Decimal = sprzet.StawkaDobowa
PricingManager.vb:139:        sprzet.StawkaDobowa = nowaStawka
PricingManager.vb:140:        em.Zapisz(sprzet)
PricingManager.vb:141:        Call New PricingDAL().ZapiszWDzienniku("STAWKA", sprzet.NumerInwentarzowy & ": " & stara & " -> " & nowaStawka)
PricingManager.vb:142:        Log.Info("Zmiana stawki " & sprzet.NumerInwentarzowy & " z " & stara & " na " & nowaStawka)
PricingManager.vb:143:    End Sub
PricingManager.vb:144:
PricingManager.vb:145:    Public Function KaucjaDlaSprzetu(ByVal sprzetId As Integer) As Decimal
PricingManager.vb:146:        Dim sprzet As Sprzet = New EquipmentManager().PobierzSprzet(sprzetId)
PricingManager.vb:147:        If sprzet Is Nothing Then Return 0D
PricingManager.vb:148:        Return sprzet.Kaucja
PricingManager.vb:149:    End Function
PricingManager.vb:150:
PricingManager.vb:151:    Public Function PrzeliczRezerwacjeNowe() As Integer
PricingManager.vb:152:        Dim sql As String = "SELECT Id, KlientId, SprzetId, DataOd, DataDo, CenaSzacunkowa FROM Rezerwacje WHERE Status = 'N'"
PricingManager.vb:153:        Dim dt As DataTable = DbHelper.WykonajZapytanie(sql)
PricingManager.vb:154:        Dim zmienione As Integer = 0
PricingManager.vb:155:        For Each row As DataRow In dt.Rows
PricingManager.vb:156:            Try
PricingManager.vb:157:                Dim nowa As Decimal = ObliczOplate(row("SprzetId"), row("DataOd"), row("DataDo"), row("KlientId"))
PricingManager.vb:158:                If nowa <> row("CenaSzacunkowa") Then
PricingManager.vb:159:                    DbHelper.WykonajPolecenie("UPDATE Rezerwacje SET CenaSzacunkowa = @Cena WHERE Id = @Id",
PricingManager.vb:160:                        DbHelper.Parametr("@Cena", nowa),
PricingManager.vb:161:                        DbHelper.Parametr("@Id", row("Id")))
PricingManager.vb:162:                    zmienione = zmienione + 1
PricingManager.vb:163:                End If
PricingManager.vb:164:            Catch ex As BladBiznesowyException
PricingManager.vb:165:                Log.Warn("Nie przeliczono rezerwacji " & row("Id") & ": " & ex.Message)
PricingManager.vb:166:            End Try
PricingManager.vb:167:        Next
PricingManager.vb:168:        Log.Info("Przeliczono rezerwacje nowe, zmienionych: " & zmienione)
PricingManager.vb:169:        Return zmienione
PricingManager.vb:170:    End Function
PricingManager.vb:171:
PricingManager.vb:172:    Public Function CennikKategorii(ByVal kategoria As String, ByVal naDzien As Date) As DataTable
PricingManager.vb:173:        Dim wynik As New DataTable("Cennik")
PricingManager.vb:174:        wynik.Columns.Add("Id", GetType(Integer))
PricingManager.vb:175:        wynik.Columns.Add("Nazwa", GetType(String))
PricingManager.vb:176:        wynik.Columns.Add("StawkaDobowa", GetType(Decimal))
PricingManager.vb:177:        wynik.Columns.Add("StawkaWeekend", GetType(Decimal))
PricingManager.vb:178:        wynik.Columns.Add("Kaucja", GetType(Decimal))
PricingManager.vb:179:        Dim taryfa As Taryfa = New PricingDAL().PobierzTaryfe(kategoria, naDzien)
PricingManager.vb:180:        For Each s As Sprzet In New EquipmentManager().ListaWKategorii(kategoria)
PricingManager.vb:181:            Dim r As DataRow = wynik.NewRow()
PricingManager.vb:182:            r("Id") = s.Id
PricingManager.vb:183:            r("Nazwa") = s.Nazwa
PricingManager.vb:184:            r("StawkaDobowa") = s.StawkaDobowa
PricingManager.vb:185:            r("StawkaWeekend") = s.StawkaDobowa * taryfa.MnoznikWeekend
PricingManager.vb:186:            r("Kaucja") = s.Kaucja
PricingManager.vb:187:            wynik.Rows.Add(r)
PricingManager.vb:188:        Next
PricingManager.vb:189:        Return wynik
PricingManager.vb:190:    End Function
PricingManager.vb:191:
PricingManager.vb:192:    Public Function SprawdzTerminy(ByVal dataOd As Date, ByVal dataDo As Date) As String
PricingManager.vb:193:        If dataDo <= dataOd Then Return "Data zwrotu musi byc pozniejsza niz data wydania"
PricingManager.vb:194:        If dataOd < Date.Today.AddDays(-1) Then Return "Data wydania z przeszlosci"
PricingManager.vb:195:        If DateDiff(DateInterval.Day, dataOd, dataDo) > 90 Then Return "Maksymalny okres wynajmu to 90 dni"
PricingManager.vb:196:        Return ""
PricingManager.vb:197:    End Function
PricingManager.vb:198:
PricingManager.vb:199:    Public Function SymulujOplate(ByVal sprzetId As Integer, ByVal dataOd As Date, ByVal liczbaDob As Integer, ByVal klientId As Integer) As Decimal
PricingManager.vb:200:        If liczbaDob < 1 Then Throw New ArgumentException("Liczba dob musi byc dodatnia")
PricingManager.vb:201:        Return ObliczOplate(sprzetId, dataOd, dataOd.AddDays(liczbaDob), klientId)
PricingManager.vb:202:    End Function
PricingManager.vb:203:
PricingManager.vb:204:    Public Function TabelaOplat(ByVal sprzetId As Integer, ByVal dataOd As Date, ByVal klientId As Integer, ByVal maksDob As Integer) As DataTable
PricingManager.vb:205:        Dim wynik As New DataTable("TabelaOplat")
PricingManager.vb:206:        wynik.Columns.Add("Doby", GetType(Integer))
PricingManager.vb:207:        wynik.Columns.Add("DataDo", GetType(Date))
PricingManager.vb:208:        wynik.Columns.Add("Oplata", GetType(Decimal))
PricingManager.vb:209:        wynik.Columns.Add("ZaDobe", GetType(Decimal))
PricingManager.vb:210:        If maksDob < 1 Then maksDob = 1
PricingManager.vb:211:        If maksDob > 30 Then maksDob = 30
PricingManager.vb:212:        For d As Integer = 1 To maksDob
PricingManager.vb:213:            Dim r As DataRow = wynik.NewRow()
PricingManager.vb:214:            Dim kwota As Decimal = SymulujOplate(sprzetId, dataOd, d, klientId)
PricingManager.vb:215:            r("Doby") = d
PricingManager.vb:216:            r("DataDo") = dataOd.AddDays(d)
PricingManager.vb:217:            r("Oplata") = kwota
PricingManager.vb:218:            r("ZaDobe") = kwota / d
PricingManager.vb:219:            wynik.Rows.Add(r)
PricingManager.vb:220:        Next
PricingManager.vb:221:        Return wynik
PricingManager.vb:222:    End Function
PricingManager.vb:223:
PricingManager.vb:224:    Public Function TaryfyNaDzien(ByVal naDzien As Date) As List(Of Taryfa)
PricingManager.vb:225:        Return New PricingDAL().TaryfyNaDzien(naDzien)
PricingManager.vb:226:    End Function
PricingManager.vb:227:
PricingManager.vb:228:    Public Function HistoriaTaryf(ByVal kategoria As String) As String
PricingManager.vb:229:        Dim lista As List(Of Taryfa) = New PricingDAL().TaryfyKategorii(kategoria)
PricingManager.vb:230:        If lista.Count = 0 Then Return "Brak taryf dla kategorii " & kategoria
PricingManager.vb:231:        Dim sb As New StringBuilder()
PricingManager.vb:232:        sb.AppendLine(Taryfa.NazwaKategorii(kategoria) & ":")
PricingManager.vb:233:        Dim poprzednia As Taryfa = Nothing
PricingManager.vb:234:        For Each t As Taryfa In lista
PricingManager.vb:235:            sb.Append("  ").Append(t.ToString())
PricingManager.vb:236:            If poprzednia IsNot Nothing AndAlso poprzednia.MnoznikWeekend <> t.MnoznikWeekend Then
PricingManager.vb:237:                sb.Append(" [zmiana mnoznika: ").Append(t.MnoznikWeekend).Append(" -> ").Append(poprzednia.MnoznikWeekend).Append("]")
PricingManager.vb:238:            End If
PricingManager.vb:239:            sb.AppendLine()
PricingManager.vb:240:            poprzednia = t
PricingManager.vb:241:        Next
PricingManager.vb:242:        Return sb.ToString()
PricingManager.vb:243:    End Function
PricingManager.vb:244:
PricingManager.vb:245:    Public Function ZmienKaucje(ByVal sprzetId As Integer, ByVal nowaKaucja As Decimal) As Boolean
PricingManager.vb:246:        If nowaKaucja < 0D Then Throw New ArgumentException("Kaucja nie moze byc ujemna")
PricingManager.vb:247:        Dim dal As New PricingDAL()
PricingManager.vb:248:        Dim ile As Integer = dal.ZaktualizujKaucje(sprzetId, nowaKaucja)
PricingManager.vb:249:        If ile = 0 Then
PricingManager.vb:250:            Log.Warn("Nie zmieniono kaucji sprzetu " & sprzetId)
PricingManager.vb:251:            Return False
PricingManager.vb:252:        End If
PricingManager.vb:253:        dal.ZapiszWDzienniku("STAWKA", "kaucja sprzetu " & sprzetId & ": " & nowaKaucja)
PricingManager.vb:254:        Return True
PricingManager.vb:255:    End Function
PricingManager.vb:256:
PricingManager.vb:257:    Public Function BrakujaceTaryfy(ByVal naDzien As Date) As List(Of String)
PricingManager.vb:258:        Dim brak As New List(Of String)()
PricingManager.vb:259:        Dim dal As New PricingDAL()
PricingManager.vb:260:        For Each kategoria As String In Taryfa.KategorieZnane
PricingManager.vb:261:            Try
PricingManager.vb:262:                dal.PobierzTaryfe(kategoria, naDzien)
PricingManager.vb:263:            Catch ex As BladBiznesowyException
PricingManager.vb:264:                brak.Add(kategoria)
PricingManager.vb:265:            End Try
PricingManager.vb:266:        Next
PricingManager.vb:267:        If brak.Count > 0 Then Log.Warn("Brak taryf na " & naDzien.ToShortDateString() & ": " & String.Join(",", brak.ToArray()))
PricingManager.vb:268:        Return brak
PricingManager.vb:269:    End Function
PricingManager.vb:270:
PricingManager.vb:271:    Public Function ZestawienieKategorii(ByVal naDzien As Date) As DataTable
PricingManager.vb:272:        Dim wynik As New DataTable("Zestawienie")
PricingManager.vb:273:        wynik.Columns.Add("Kategoria", GetType(String))
PricingManager.vb:274:        wynik.Columns.Add("Nazwa", GetType(String))
PricingManager.vb:275:        wynik.Columns.Add("MnoznikWeekend", GetType(Decimal))
PricingManager.vb:276:        wynik.Columns.Add("RabatTygodniowyProc", GetType(Decimal))
PricingManager.vb:277:        wynik.Columns.Add("LiczbaSprzetu", GetType(Integer))
PricingManager.vb:278:        Dim em As New EquipmentManager()
PricingManager.vb:279:        For Each t As Taryfa In TaryfyNaDzien(naDzien)
PricingManager.vb:280:            Dim r As DataRow = wynik.NewRow()
PricingManager.vb:281:            r("Kategoria") = t.Kategoria
PricingManager.vb:282:            r("Nazwa") = Taryfa.NazwaKategorii(t.Kategoria)
PricingManager.vb:283:            r("MnoznikWeekend") = t.MnoznikWeekend
PricingManager.vb:284:            r("RabatTygodniowyProc") = t.RabatTygodniowyProc
PricingManager.vb:285:            r("LiczbaSprzetu") = em.ListaWKategorii(t.Kategoria).Count
PricingManager.vb:286:            wynik.Rows.Add(r)
PricingManager.vb:287:        Next
PricingManager.vb:288:        Return wynik
PricingManager.vb:289:    End Function
PricingManager.vb:290:
PricingManager.vb:291:    Public Function CzyCenaAktualna(ByVal rezerwacjaId As Integer) As Boolean
PricingManager.vb:292:        Dim dt As DataTable = DbHelper.WykonajZapytanie("SELECT KlientId, SprzetId, DataOd, DataDo, CenaSzacunkowa FROM Rezerwacje WHERE Id = @Id",
PricingManager.vb:293:            DbHelper.Parametr("@Id", rezerwacjaId))
PricingManager.vb:294:        If dt.Rows.Count = 0 Then Return False
PricingManager.vb:295:        Dim r As DataRow = dt.Rows(0)
PricingManager.vb:296:        Try
PricingManager.vb:297:            Return ObliczOplate(r("SprzetId"), r("DataOd"), r("DataDo"), r("KlientId")) = r("CenaSzacunkowa")
PricingManager.vb:298:        Catch ex As BladBiznesowyException
PricingManager.vb:299:            Log.Warn("Nie mozna sprawdzic ceny rezerwacji " & rezerwacjaId & ": " & ex.Message)
PricingManager.vb:300:            Return False
PricingManager.vb:301:        End Try
PricingManager.vb:302:    End Function
PricingManager.vb:303:
PricingManager.vb:304:    Private Function CzyWeekend(ByVal dzien As Date) As Boolean
PricingManager.vb:305:        Return dzien.DayOfWeek = DayOfWeek.Saturday Or dzien.DayOfWeek = DayOfWeek.Sunday
PricingManager.vb:306:    End Function
PricingManager.vb:307:
PricingManager.vb:308:End Class
```

`git --no-pager -C 'legacy-app-parent\legacy-app\WypozyczalniaWeb' grep -n -I -e '^' -- 'Cennik.aspx.vb'`

```text
Cennik.aspx.vb:1:' Modul: Cennik.aspx - taryfy, stawki, import cennika, kalkulator | Autor: zespol IT | 2014-03-11
Cennik.aspx.vb:2:' Historia:
Cennik.aspx.vb:3:'   2015-02-04 MW: lista taryf z data obowiazywania
Cennik.aspx.vb:4:'   2017-06-12 JK: przycisk importu cennika XML
Cennik.aspx.vb:5:'   2019-04-02 PT: kalkulator oplaty dla wybranego klienta
Cennik.aspx.vb:6:
Cennik.aspx.vb:7:Imports System.Data
Cennik.aspx.vb:8:Imports System.Configuration
Cennik.aspx.vb:9:Imports System.Globalization
Cennik.aspx.vb:10:Imports Wypozyczalnia.Core
Cennik.aspx.vb:11:Imports Wypozyczalnia.Core.Model
Cennik.aspx.vb:12:Imports Wypozyczalnia.Core.Integracje
Cennik.aspx.vb:13:
Cennik.aspx.vb:14:Partial Class Cennik
Cennik.aspx.vb:15:    Inherits System.Web.UI.Page
Cennik.aspx.vb:16:
Cennik.aspx.vb:17:    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Cennik))
Cennik.aspx.vb:18:
Cennik.aspx.vb:19:    Protected WithEvents lblWynik As Global.System.Web.UI.WebControls.Label
Cennik.aspx.vb:20:    Protected WithEvents gvTaryfy As Global.System.Web.UI.WebControls.GridView
Cennik.aspx.vb:21:    Protected WithEvents ddlSprzet As Global.System.Web.UI.WebControls.DropDownList
Cennik.aspx.vb:22:    Protected WithEvents txtStawka As Global.System.Web.UI.WebControls.TextBox
Cennik.aspx.vb:23:    Protected WithEvents btnZapiszStawke As Global.System.Web.UI.WebControls.Button
Cennik.aspx.vb:24:    Protected WithEvents btnImportuj As Global.System.Web.UI.WebControls.Button
Cennik.aspx.vb:25:    Protected WithEvents lblKlient As Global.System.Web.UI.WebControls.Label
Cennik.aspx.vb:26:    Protected WithEvents ddlSprzetKalk As Global.System.Web.UI.WebControls.DropDownList
Cennik.aspx.vb:27:    Protected WithEvents txtOd As Global.System.Web.UI.WebControls.TextBox
Cennik.aspx.vb:28:    Protected WithEvents txtDo As Global.System.Web.UI.WebControls.TextBox
Cennik.aspx.vb:29:    Protected WithEvents btnOblicz As Global.System.Web.UI.WebControls.Button
Cennik.aspx.vb:30:    Protected WithEvents lblKwota As Global.System.Web.UI.WebControls.Label
Cennik.aspx.vb:31:    Protected WithEvents lblSzczegoly As Global.System.Web.UI.WebControls.Label
Cennik.aspx.vb:32:
Cennik.aspx.vb:33:    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
Cennik.aspx.vb:34:        If Not IsPostBack Then
Cennik.aspx.vb:35:            ZaladujTaryfy()
Cennik.aspx.vb:36:            ZaladujSprzet()
Cennik.aspx.vb:37:            PokazAktualnaStawke()
Cennik.aspx.vb:38:            txtOd.Text = Date.Today.AddDays(1).ToString("yyyy-MM-dd") & " 08:00"
Cennik.aspx.vb:39:            txtDo.Text = Date.Today.AddDays(2).ToString("yyyy-MM-dd") & " 08:00"
Cennik.aspx.vb:40:        End If
Cennik.aspx.vb:41:        PokazKlienta()
Cennik.aspx.vb:42:    End Sub
Cennik.aspx.vb:43:
Cennik.aspx.vb:44:    Protected Sub ddlSprzet_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlSprzet.SelectedIndexChanged
Cennik.aspx.vb:45:        PokazAktualnaStawke()
Cennik.aspx.vb:46:    End Sub
Cennik.aspx.vb:47:
Cennik.aspx.vb:48:    Protected Sub btnZapiszStawke_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZapiszStawke.Click
Cennik.aspx.vb:49:        If txtStawka.Text.Trim() = "" Then
Cennik.aspx.vb:50:            lblWynik.Text = "Podaj nową stawkę."
Cennik.aspx.vb:51:            Return
Cennik.aspx.vb:52:        End If
Cennik.aspx.vb:53:        If ddlSprzet.SelectedValue = "" Then
Cennik.aspx.vb:54:            lblWynik.Text = "Wybierz sprzęt."
Cennik.aspx.vb:55:            Return
Cennik.aspx.vb:56:        End If
Cennik.aspx.vb:57:        Try
Cennik.aspx.vb:58:            Dim sql As String = "UPDATE Sprzet SET StawkaDobowa = " & txtStawka.Text.Replace(",", ".") & " WHERE Id = " & ddlSprzet.SelectedValue
Cennik.aspx.vb:59:            Dim ile As Integer = DbHelper.WykonajPolecenie(sql)
Cennik.aspx.vb:60:            If ile = 1 Then
Cennik.aspx.vb:61:                lblWynik.Text = "Zapisano stawkę " & txtStawka.Text & " zł dla " & ddlSprzet.SelectedItem.Text & "."
Cennik.aspx.vb:62:                Log.Info("Zmiana stawki z ekranu cennika: " & ddlSprzet.SelectedValue & " = " & txtStawka.Text)
Cennik.aspx.vb:63:            Else
Cennik.aspx.vb:64:                lblWynik.Text = "Nie znaleziono sprzętu."
Cennik.aspx.vb:65:            End If
Cennik.aspx.vb:66:            txtStawka.Text = ""
Cennik.aspx.vb:67:            ZaladujSprzet()
Cennik.aspx.vb:68:            PokazAktualnaStawke()
Cennik.aspx.vb:69:        Catch ex As Exception
Cennik.aspx.vb:70:            Log.Error("Blad zapisu stawki", ex)
Cennik.aspx.vb:71:            lblWynik.Text = "Błąd zapisu stawki: " & ex.Message
Cennik.aspx.vb:72:        End Try
Cennik.aspx.vb:73:    End Sub
Cennik.aspx.vb:74:
Cennik.aspx.vb:75:    Protected Sub btnImportuj_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnImportuj.Click
Cennik.aspx.vb:76:        Dim katalog As String = ConfigurationManager.AppSettings("ImportCennikaKatalog")
Cennik.aspx.vb:77:        Dim import As New ImportCennikaXml()
Cennik.aspx.vb:78:        Try
Cennik.aspx.vb:79:            Dim oczekujace As String() = import.PlikiOczekujace(katalog)
Cennik.aspx.vb:80:            If oczekujace.Length = 0 Then
Cennik.aspx.vb:81:                lblWynik.Text = "Brak plików do importu w katalogu " & katalog & "."
Cennik.aspx.vb:82:                Return
Cennik.aspx.vb:83:            End If
Cennik.aspx.vb:84:            Dim ile As Integer = import.ImportujKatalog(katalog)
Cennik.aspx.vb:85:            lblWynik.Text = "Zaimportowano plików: " & ile & " (" & String.Join(", ", oczekujace) & ")."
Cennik.aspx.vb:86:            ZaladujTaryfy()
Cennik.aspx.vb:87:            ZaladujSprzet()
Cennik.aspx.vb:88:            PokazAktualnaStawke()
Cennik.aspx.vb:89:        Catch ex As System.IO.IOException
Cennik.aspx.vb:90:            Log.Error("Import cennika - blad pliku", ex)
Cennik.aspx.vb:91:            lblWynik.Text = "Błąd dostępu do katalogu importu: " & ex.Message
Cennik.aspx.vb:92:        Catch ex As System.Xml.XmlException
Cennik.aspx.vb:93:            Log.Error("Import cennika - niepoprawny XML", ex)
Cennik.aspx.vb:94:            lblWynik.Text = "Niepoprawny plik XML: " & ex.Message
Cennik.aspx.vb:95:        Catch ex As FormatException
Cennik.aspx.vb:96:            Log.Error("Import cennika - niepoprawna liczba", ex)
Cennik.aspx.vb:97:            lblWynik.Text = "Niepoprawna wartość liczbowa w pliku: " & ex.Message
Cennik.aspx.vb:98:        End Try
Cennik.aspx.vb:99:    End Sub
Cennik.aspx.vb:100:
Cennik.aspx.vb:101:    Protected Sub btnOblicz_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOblicz.Click
Cennik.aspx.vb:102:        lblKwota.Text = ""
Cennik.aspx.vb:103:        lblSzczegoly.Text = ""
Cennik.aspx.vb:104:        Dim klientId As Integer = Session("WybranyKlientId")
Cennik.aspx.vb:105:        Dim dataOd As Date
Cennik.aspx.vb:106:        Dim dataDo As Date
Cennik.aspx.vb:107:        If Not Date.TryParse(txtOd.Text, dataOd) Then
Cennik.aspx.vb:108:            lblWynik.Text = "Niepoprawna data od."
Cennik.aspx.vb:109:            Return
Cennik.aspx.vb:110:        End If
Cennik.aspx.vb:111:        If Not Date.TryParse(txtDo.Text, dataDo) Then
Cennik.aspx.vb:112:            lblWynik.Text = "Niepoprawna data do."
Cennik.aspx.vb:113:            Return
Cennik.aspx.vb:114:        End If
Cennik.aspx.vb:115:        If ddlSprzetKalk.SelectedValue = "" Then
Cennik.aspx.vb:116:            lblWynik.Text = "Wybierz sprzęt do kalkulacji."
Cennik.aspx.vb:117:            Return
Cennik.aspx.vb:118:        End If
Cennik.aspx.vb:119:        Dim pm As New PricingManager()
Cennik.aspx.vb:120:        Try
Cennik.aspx.vb:121:            Dim kwota As Decimal = pm.ObliczOplate(CInt(ddlSprzetKalk.SelectedValue), dataOd, dataDo, klientId)
Cennik.aspx.vb:122:            lblKwota.Text = kwota.ToString("N2") & " zł (" & pm.LiczbaDob(dataOd, dataDo) & " dób)"
Cennik.aspx.vb:123:            lblSzczegoly.Text = Server.HtmlEncode(pm.SzczegolyOplaty(CInt(ddlSprzetKalk.SelectedValue), dataOd, dataDo, klientId))
Cennik.aspx.vb:124:        Catch ex As ArgumentException
Cennik.aspx.vb:125:            lblWynik.Text = ex.Message
Cennik.aspx.vb:126:        Catch ex As BladBiznesowyException
Cennik.aspx.vb:127:            lblWynik.Text = ex.Message
Cennik.aspx.vb:128:        End Try
Cennik.aspx.vb:129:    End Sub
Cennik.aspx.vb:130:
Cennik.aspx.vb:131:    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PreRender
Cennik.aspx.vb:132:        Dim katalog As String = ConfigurationManager.AppSettings("ImportCennikaKatalog")
Cennik.aspx.vb:133:        If String.IsNullOrEmpty(katalog) Then
Cennik.aspx.vb:134:            btnImportuj.Enabled = False
Cennik.aspx.vb:135:            btnImportuj.ToolTip = "Brak klucza ImportCennikaKatalog w konfiguracji"
Cennik.aspx.vb:136:        Else
Cennik.aspx.vb:137:            btnImportuj.ToolTip = "Katalog: " & katalog
Cennik.aspx.vb:138:        End If
Cennik.aspx.vb:139:    End Sub
Cennik.aspx.vb:140:
Cennik.aspx.vb:141:    Private _aktualneKategorie As New List(Of String)()
Cennik.aspx.vb:142:
Cennik.aspx.vb:143:    Protected Sub gvTaryfy_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvTaryfy.RowDataBound
Cennik.aspx.vb:144:        If e.Row.RowType <> DataControlRowType.DataRow Then Return
Cennik.aspx.vb:145:        Dim wiersz As DataRowView = CType(e.Row.DataItem, DataRowView)
Cennik.aspx.vb:146:        Dim kategoria As String = wiersz("Kategoria").ToString().Trim()
Cennik.aspx.vb:147:        Dim od As Date = wiersz("ObowiazujeOd")
Cennik.aspx.vb:148:        If od > Date.Today Then
Cennik.aspx.vb:149:            e.Row.ForeColor = Drawing.Color.Gray
Cennik.aspx.vb:150:            e.Row.ToolTip = "Taryfa przyszła"
Cennik.aspx.vb:151:            Return
Cennik.aspx.vb:152:        End If
Cennik.aspx.vb:153:        If Not _aktualneKategorie.Contains(kategoria) Then
Cennik.aspx.vb:154:            _aktualneKategorie.Add(kategoria)
Cennik.aspx.vb:155:            e.Row.Font.Bold = True
Cennik.aspx.vb:156:            e.Row.ToolTip = "Taryfa obowiązująca"
Cennik.aspx.vb:157:        Else
Cennik.aspx.vb:158:            e.Row.ForeColor = Drawing.Color.DarkGray
Cennik.aspx.vb:159:            e.Row.ToolTip = "Taryfa archiwalna"
Cennik.aspx.vb:160:        End If
Cennik.aspx.vb:161:    End Sub
Cennik.aspx.vb:162:
Cennik.aspx.vb:163:    Private Sub ZaladujTaryfy()
Cennik.aspx.vb:164:        Dim sql As String = "SELECT Id, Kategoria, MnoznikWeekend, RabatTygodniowyProc, ObowiazujeOd FROM Taryfy ORDER BY Kategoria, ObowiazujeOd DESC"
Cennik.aspx.vb:165:        Try
Cennik.aspx.vb:166:            gvTaryfy.DataSource = DbHelper.WykonajZapytanie(sql)
Cennik.aspx.vb:167:            gvTaryfy.DataBind()
Cennik.aspx.vb:168:        Catch ex As Exception
Cennik.aspx.vb:169:            Log.Error("Blad odczytu taryf", ex)
Cennik.aspx.vb:170:            lblWynik.Text = "Nie udało się wczytać taryf."
Cennik.aspx.vb:171:        End Try
Cennik.aspx.vb:172:    End Sub
Cennik.aspx.vb:173:
Cennik.aspx.vb:174:    Private Sub ZaladujSprzet()
Cennik.aspx.vb:175:        Dim wybrany As String = ddlSprzet.SelectedValue
Cennik.aspx.vb:176:        Dim wybranyKalk As String = ddlSprzetKalk.SelectedValue
Cennik.aspx.vb:177:        Dim lista As List(Of Sprzet) = New EquipmentManager().ListaWKategorii("")
Cennik.aspx.vb:178:
Cennik.aspx.vb:179:        ddlSprzet.Items.Clear()
Cennik.aspx.vb:180:        ddlSprzetKalk.Items.Clear()
Cennik.aspx.vb:181:        ddlSprzet.Items.Add(New ListItem("-- wybierz --", ""))
Cennik.aspx.vb:182:        ddlSprzetKalk.Items.Add(New ListItem("-- wybierz --", ""))
Cennik.aspx.vb:183:        For Each s As Sprzet In lista
Cennik.aspx.vb:184:            Dim tekst As String = s.NumerInwentarzowy & " " & s.Nazwa
Cennik.aspx.vb:185:            ddlSprzet.Items.Add(New ListItem(tekst, s.Id.ToString()))
Cennik.aspx.vb:186:            ddlSprzetKalk.Items.Add(New ListItem(tekst, s.Id.ToString()))
Cennik.aspx.vb:187:        Next
Cennik.aspx.vb:188:        If wybrany <> "" AndAlso ddlSprzet.Items.FindByValue(wybrany) IsNot Nothing Then
Cennik.aspx.vb:189:            ddlSprzet.SelectedValue = wybrany
Cennik.aspx.vb:190:        End If
Cennik.aspx.vb:191:        If wybranyKalk <> "" AndAlso ddlSprzetKalk.Items.FindByValue(wybranyKalk) IsNot Nothing Then
Cennik.aspx.vb:192:            ddlSprzetKalk.SelectedValue = wybranyKalk
Cennik.aspx.vb:193:        End If
Cennik.aspx.vb:194:    End Sub
Cennik.aspx.vb:195:
Cennik.aspx.vb:196:    Private Sub PokazAktualnaStawke()
Cennik.aspx.vb:197:        If ddlSprzet.SelectedValue = "" Then
Cennik.aspx.vb:198:            txtStawka.ToolTip = ""
Cennik.aspx.vb:199:            Return
Cennik.aspx.vb:200:        End If
Cennik.aspx.vb:201:        Dim s As Sprzet = New EquipmentManager().PobierzSprzet(CInt(ddlSprzet.SelectedValue))
Cennik.aspx.vb:202:        If s Is Nothing Then Return
Cennik.aspx.vb:203:        txtStawka.ToolTip = "Aktualnie: " & s.StawkaDobowa.ToString("N2") & " zł, kaucja " & s.Kaucja.ToString("N2") & " zł"
Cennik.aspx.vb:204:        txtStawka.Attributes("placeholder") = s.StawkaDobowa.ToString("N2")
Cennik.aspx.vb:205:    End Sub
Cennik.aspx.vb:206:
Cennik.aspx.vb:207:    Private Sub PokazKlienta()
Cennik.aspx.vb:208:        Dim klientId As Integer = Session("WybranyKlientId")
Cennik.aspx.vb:209:        If klientId = 0 Then
Cennik.aspx.vb:210:            lblKlient.Text = "Klient: (nie wybrano - kalkulacja bez rabatu)"
Cennik.aspx.vb:211:            Return
Cennik.aspx.vb:212:        End If
Cennik.aspx.vb:213:        Dim k As Klient = New CustomerManager().PobierzKlienta(klientId)
Cennik.aspx.vb:214:        If k Is Nothing Then
Cennik.aspx.vb:215:            lblKlient.Text = "Klient: (nie znaleziono)"
Cennik.aspx.vb:216:        ElseIf k.Typ = "F" Then
Cennik.aspx.vb:217:            lblKlient.Text = "Klient: " & Server.HtmlEncode(k.NazwaFirmy) & " (próg rabatu " & k.ProgRabatu & ")"
Cennik.aspx.vb:218:        Else
Cennik.aspx.vb:219:            lblKlient.Text = "Klient: " & Server.HtmlEncode(k.Imie & " " & k.Nazwisko) & " (próg rabatu " & k.ProgRabatu & ")"
Cennik.aspx.vb:220:        End If
Cennik.aspx.vb:221:    End Sub
Cennik.aspx.vb:222:
Cennik.aspx.vb:223:End Class
```

## M19 — surowy wynik H9

`git --no-pager -C 'legacy-app-parent\legacy-app\WypozyczalniaCore\Model' grep -n -I -e '^' -- '*.vb'`

```text
Faktura.vb:1:' Modul: Model\Faktura.vb | Autor: zespol IT | 2014-05-19
Faktura.vb:2:' Faktura za wypozyczenie oraz jej pozycje.
Faktura.vb:3:' 2015-01-12 MW: IdTransakcji (platnosc przez bramke)
Faktura.vb:4:' 2018-04-03 PT: ZWiersza dla naglowka i pozycji
Faktura.vb:5:
Faktura.vb:6:Imports System.Data
Faktura.vb:7:
Faktura.vb:8:Namespace Model
Faktura.vb:9:
Faktura.vb:10:    Public Class Faktura
Faktura.vb:11:
Faktura.vb:12:        Public Property Id As Integer
Faktura.vb:13:        Public Property Numer As String
Faktura.vb:14:        Public Property KlientId As Integer
Faktura.vb:15:        Public Property WypozyczenieId As Integer
Faktura.vb:16:        Public Property DataWystawienia As Date
Faktura.vb:17:        Public Property Netto As Decimal
Faktura.vb:18:        Public Property Vat As Decimal
Faktura.vb:19:        Public Property Brutto As Decimal
Faktura.vb:20:        Public Property Status As String
Faktura.vb:21:        Public Property IdTransakcji As String
Faktura.vb:22:        Public Property Pozycje As List(Of PozycjaFaktury)
Faktura.vb:23:
Faktura.vb:24:        Public Sub New()
Faktura.vb:25:            Pozycje = New List(Of PozycjaFaktury)()
Faktura.vb:26:            Status = "W"
Faktura.vb:27:            Numer = ""
Faktura.vb:28:            IdTransakcji = ""
Faktura.vb:29:            DataWystawienia = Date.Now
Faktura.vb:30:        End Sub
Faktura.vb:31:
Faktura.vb:32:        Public ReadOnly Property CzyOplacona As Boolean
Faktura.vb:33:            Get
Faktura.vb:34:                Return Status = "O"
Faktura.vb:35:            End Get
Faktura.vb:36:        End Property
Faktura.vb:37:
Faktura.vb:38:        Public ReadOnly Property LiczbaPozycji As Integer
Faktura.vb:39:            Get
Faktura.vb:40:                If Pozycje Is Nothing Then Return 0
Faktura.vb:41:                Return Pozycje.Count
Faktura.vb:42:            End Get
Faktura.vb:43:        End Property
Faktura.vb:44:
Faktura.vb:45:        Public Function SumaPozycjiNetto() As Decimal
Faktura.vb:46:            Dim suma As Decimal = 0D
Faktura.vb:47:            For Each p As PozycjaFaktury In Pozycje
Faktura.vb:48:                suma = suma + p.WartoscNetto
Faktura.vb:49:            Next
Faktura.vb:50:            Return suma
Faktura.vb:51:        End Function
Faktura.vb:52:
Faktura.vb:53:        Public Shared Function ZWiersza(ByVal wiersz As DataRow) As Faktura
Faktura.vb:54:            Dim f As New Faktura()
Faktura.vb:55:            f.Id = CInt(wiersz("Id"))
Faktura.vb:56:            f.Numer = wiersz("Numer").ToString()
Faktura.vb:57:            f.KlientId = CInt(wiersz("KlientId"))
Faktura.vb:58:            f.WypozyczenieId = CInt(wiersz("WypozyczenieId"))
Faktura.vb:59:            f.DataWystawienia = CDate(wiersz("DataWystawienia"))
Faktura.vb:60:            f.Netto = CDec(wiersz("Netto"))
Faktura.vb:61:            f.Vat = CDec(wiersz("Vat"))
Faktura.vb:62:            f.Brutto = CDec(wiersz("Brutto"))
Faktura.vb:63:            f.Status = wiersz("Status").ToString()
Faktura.vb:64:            If Not IsDBNull(wiersz("IdTransakcji")) Then f.IdTransakcji = wiersz("IdTransakcji").ToString()
Faktura.vb:65:            Return f
Faktura.vb:66:        End Function
Faktura.vb:67:
Faktura.vb:68:        Public Overrides Function ToString() As String
Faktura.vb:69:            Return String.Format("Faktura {0} (wypozyczenie {1}, brutto {2})", Numer, WypozyczenieId, Brutto)
Faktura.vb:70:        End Function
Faktura.vb:71:
Faktura.vb:72:    End Class
Faktura.vb:73:
Faktura.vb:74:    Public Class PozycjaFaktury
Faktura.vb:75:
Faktura.vb:76:        Public Property Lp As Integer
Faktura.vb:77:        Public Property Opis As String
Faktura.vb:78:        Public Property Ilosc As Decimal
Faktura.vb:79:        Public Property CenaNetto As Decimal
Faktura.vb:80:        Public Property StawkaVat As Decimal
Faktura.vb:81:        Public Property WartoscNetto As Decimal
Faktura.vb:82:
Faktura.vb:83:        Public Shared Function ZWiersza(ByVal wiersz As DataRow) As PozycjaFaktury
Faktura.vb:84:            Dim p As New PozycjaFaktury()
Faktura.vb:85:            p.Lp = CInt(wiersz("Lp"))
Faktura.vb:86:            p.Opis = wiersz("Opis").ToString()
Faktura.vb:87:            p.Ilosc = CDec(wiersz("Ilosc"))
Faktura.vb:88:            p.CenaNetto = CDec(wiersz("CenaNetto"))
Faktura.vb:89:            p.StawkaVat = CDec(wiersz("StawkaVat"))
Faktura.vb:90:            p.WartoscNetto = CDec(wiersz("WartoscNetto"))
Faktura.vb:91:            Return p
Faktura.vb:92:        End Function
Faktura.vb:93:
Faktura.vb:94:    End Class
Faktura.vb:95:
Faktura.vb:96:End Namespace
Klient.vb:1:' Modul: Model\Klient.vb - encja klienta wypozyczalni | Autor: zespol IT | 2014-03-11
Klient.vb:2:' Historia: 2014-03-11 JK utworzenie
Klient.vb:3:'           2018-09-04 PT NazwaFirmy, NIP, Typ (klienci firmowi)
Klient.vb:4:
Klient.vb:5:Namespace Model
Klient.vb:6:
Klient.vb:7:    <Serializable()>
Klient.vb:8:    Public Class Klient
Klient.vb:9:
Klient.vb:10:        Public Property Id As Integer
Klient.vb:11:        Public Property Imie As String
Klient.vb:12:        Public Property Nazwisko As String
Klient.vb:13:        Public Property NazwaFirmy As String
Klient.vb:14:        Public Property Email As String
Klient.vb:15:        Public Property Telefon As String
Klient.vb:16:        Public Property NumerDokumentu As String
Klient.vb:17:        Public Property NIP As String
Klient.vb:18:
Klient.vb:19:        ' I = osoba prywatna, F = firma
Klient.vb:20:        Public Property Typ As String
Klient.vb:21:
Klient.vb:22:        ' 0 - 3, ustawiany recznie przez obsluge
Klient.vb:23:        Public Property ProgRabatu As Integer
Klient.vb:24:
Klient.vb:25:        Public Property Zablokowany As Boolean
Klient.vb:26:        Public Property PowodBlokady As String
Klient.vb:27:        Public Property DataRejestracji As Date
Klient.vb:28:
Klient.vb:29:        Public Sub New()
Klient.vb:30:            Typ = "I"
Klient.vb:31:            ProgRabatu = 0
Klient.vb:32:            Zablokowany = False
Klient.vb:33:            DataRejestracji = Date.Now
Klient.vb:34:        End Sub
Klient.vb:35:
Klient.vb:36:        Public ReadOnly Property CzyFirma As Boolean
Klient.vb:37:            Get
Klient.vb:38:                Return Typ = "F"
Klient.vb:39:            End Get
Klient.vb:40:        End Property
Klient.vb:41:
Klient.vb:42:        Public ReadOnly Property NazwaWyswietlana As String
Klient.vb:43:            Get
Klient.vb:44:                If CzyFirma AndAlso Not String.IsNullOrEmpty(NazwaFirmy) Then
Klient.vb:45:                    Return NazwaFirmy & " (" & Imie & " " & Nazwisko & ")"
Klient.vb:46:                End If
Klient.vb:47:                Return Imie & " " & Nazwisko
Klient.vb:48:            End Get
Klient.vb:49:        End Property
Klient.vb:50:
Klient.vb:51:        Public Overrides Function ToString() As String
Klient.vb:52:            Return Id.ToString() & " - " & NazwaWyswietlana
Klient.vb:53:        End Function
Klient.vb:54:
Klient.vb:55:        Public Overrides Function Equals(ByVal obj As Object) As Boolean
Klient.vb:56:            Dim inny As Klient = TryCast(obj, Klient)
Klient.vb:57:            If inny Is Nothing Then Return False
Klient.vb:58:            Return inny.Id = Me.Id
Klient.vb:59:        End Function
Klient.vb:60:
Klient.vb:61:        Public Overrides Function GetHashCode() As Integer
Klient.vb:62:            Return Id.GetHashCode()
Klient.vb:63:        End Function
Klient.vb:64:
Klient.vb:65:    End Class
Klient.vb:66:
Klient.vb:67:End Namespace
Rezerwacja.vb:1:' Modul: Model - rezerwacja sprzetu na termin | Autor: zespol IT | 2014-03-11
Rezerwacja.vb:2:' Historia:
Rezerwacja.vb:3:'   2014-09-22 JK  CenaSzacunkowa zapisywana przy rezerwacji (wczesniej liczona w locie)
Rezerwacja.vb:4:'   2017-04-03 MW  status A (anulowana) zamiast DELETE
Rezerwacja.vb:5:
Rezerwacja.vb:6:Namespace Model
Rezerwacja.vb:7:
Rezerwacja.vb:8:    <Serializable()>
Rezerwacja.vb:9:    Public Class Rezerwacja
Rezerwacja.vb:10:
Rezerwacja.vb:11:        Public Const StatusNowa As String = "N"
Rezerwacja.vb:12:        Public Const StatusPotwierdzona As String = "P"
Rezerwacja.vb:13:        Public Const StatusAnulowana As String = "A"
Rezerwacja.vb:14:        Public Const StatusZrealizowana As String = "Z"
Rezerwacja.vb:15:
Rezerwacja.vb:16:        Public Property Id As Integer
Rezerwacja.vb:17:        Public Property KlientId As Integer
Rezerwacja.vb:18:        Public Property SprzetId As Integer
Rezerwacja.vb:19:        Public Property DataOd As Date
Rezerwacja.vb:20:        Public Property DataDo As Date
Rezerwacja.vb:21:        Public Property Status As String
Rezerwacja.vb:22:        Public Property CenaSzacunkowa As Decimal
Rezerwacja.vb:23:        Public Property Uwagi As String
Rezerwacja.vb:24:
Rezerwacja.vb:25:        Public Sub New()
Rezerwacja.vb:26:            Id = 0
Rezerwacja.vb:27:            KlientId = 0
Rezerwacja.vb:28:            SprzetId = 0
Rezerwacja.vb:29:            DataOd = Date.MinValue
Rezerwacja.vb:30:            DataDo = Date.MinValue
Rezerwacja.vb:31:            Status = StatusNowa
Rezerwacja.vb:32:            CenaSzacunkowa = 0D
Rezerwacja.vb:33:            Uwagi = ""
Rezerwacja.vb:34:        End Sub
Rezerwacja.vb:35:
Rezerwacja.vb:36:        ' N i P blokuja termin, A i Z juz nie
Rezerwacja.vb:37:        Public ReadOnly Property CzyAktywna As Boolean
Rezerwacja.vb:38:            Get
Rezerwacja.vb:39:                Return Status = StatusNowa OrElse Status = StatusPotwierdzona
Rezerwacja.vb:40:            End Get
Rezerwacja.vb:41:        End Property
Rezerwacja.vb:42:
Rezerwacja.vb:43:        Public ReadOnly Property OpisStatusu As String
Rezerwacja.vb:44:            Get
Rezerwacja.vb:45:                Select Case Status
Rezerwacja.vb:46:                    Case StatusNowa : Return "nowa"
Rezerwacja.vb:47:                    Case StatusPotwierdzona : Return "potwierdzona"
Rezerwacja.vb:48:                    Case StatusAnulowana : Return "anulowana"
Rezerwacja.vb:49:                    Case StatusZrealizowana : Return "zrealizowana"
Rezerwacja.vb:50:                    Case Else : Return "?"
Rezerwacja.vb:51:                End Select
Rezerwacja.vb:52:            End Get
Rezerwacja.vb:53:        End Property
Rezerwacja.vb:54:
Rezerwacja.vb:55:        Public ReadOnly Property Termin As String
Rezerwacja.vb:56:            Get
Rezerwacja.vb:57:                Return DataOd.ToString("yyyy-MM-dd HH:mm") & " - " & DataDo.ToString("yyyy-MM-dd HH:mm")
Rezerwacja.vb:58:            End Get
Rezerwacja.vb:59:        End Property
Rezerwacja.vb:60:
Rezerwacja.vb:61:        Public Overrides Function ToString() As String
Rezerwacja.vb:62:            Return "Rezerwacja " & Id & " (" & Termin & ", " & OpisStatusu & ")"
Rezerwacja.vb:63:        End Function
Rezerwacja.vb:64:
Rezerwacja.vb:65:    End Class
Rezerwacja.vb:66:
Rezerwacja.vb:67:End Namespace
Sprzet.vb:1:' Modul: Model - pozycja sprzetu w katalogu wypozyczalni | Autor: zespol IT | 2014-03-11
Sprzet.vb:2:' Historia:
Sprzet.vb:3:'   2015-06-02 MW  dodane pole Kaucja (wczesniej liczona z cennika)
Sprzet.vb:4:'   2018-01-15 PT  status X (wycofany) zamiast kasowania rekordow
Sprzet.vb:5:
Sprzet.vb:6:Namespace Model
Sprzet.vb:7:
Sprzet.vb:8:    <Serializable()>
Sprzet.vb:9:    Public Class Sprzet
Sprzet.vb:10:
Sprzet.vb:11:        Public Const StatusDostepny As String = "D"
Sprzet.vb:12:        Public Const StatusWypozyczony As String = "W"
Sprzet.vb:13:        Public Const StatusSerwis As String = "S"
Sprzet.vb:14:        Public Const StatusWycofany As String = "X"
Sprzet.vb:15:
Sprzet.vb:16:        Public Property Id As Integer
Sprzet.vb:17:        Public Property NumerInwentarzowy As String
Sprzet.vb:18:        Public Property Nazwa As String
Sprzet.vb:19:        Public Property Kategoria As String
Sprzet.vb:20:        Public Property StawkaDobowa As Decimal
Sprzet.vb:21:        Public Property Kaucja As Decimal
Sprzet.vb:22:        Public Property Status As String
Sprzet.vb:23:
Sprzet.vb:24:        Public Sub New()
Sprzet.vb:25:            Id = 0
Sprzet.vb:26:            NumerInwentarzowy = ""
Sprzet.vb:27:            Nazwa = ""
Sprzet.vb:28:            Kategoria = "BUD"
Sprzet.vb:29:            StawkaDobowa = 0D
Sprzet.vb:30:            Kaucja = 0D
Sprzet.vb:31:            Status = StatusDostepny
Sprzet.vb:32:        End Sub
Sprzet.vb:33:
Sprzet.vb:34:        Public ReadOnly Property OpisStatusu As String
Sprzet.vb:35:            Get
Sprzet.vb:36:                Select Case Status
Sprzet.vb:37:                    Case StatusDostepny : Return "dostepny"
Sprzet.vb:38:                    Case StatusWypozyczony : Return "wypozyczony"
Sprzet.vb:39:                    Case StatusSerwis : Return "serwis"
Sprzet.vb:40:                    Case StatusWycofany : Return "wycofany"
Sprzet.vb:41:                    Case Else : Return "?"
Sprzet.vb:42:                End Select
Sprzet.vb:43:            End Get
Sprzet.vb:44:        End Property
Sprzet.vb:45:
Sprzet.vb:46:        Public ReadOnly Property OpisKategorii As String
Sprzet.vb:47:            Get
Sprzet.vb:48:                Select Case Kategoria
Sprzet.vb:49:                    Case "BUD" : Return "Budowlany"
Sprzet.vb:50:                    Case "OGR" : Return "Ogrodowy"
Sprzet.vb:51:                    Case "IMP" : Return "Imprezowy"
Sprzet.vb:52:                    Case "TRN" : Return "Transportowy"
Sprzet.vb:53:                    Case Else : Return Kategoria
Sprzet.vb:54:                End Select
Sprzet.vb:55:            End Get
Sprzet.vb:56:        End Property
Sprzet.vb:57:
Sprzet.vb:58:        ' czy mozna w ogole wydac (nie sprawdza terminow - od tego jest EquipmentManager)
Sprzet.vb:59:        Public Function CzyMoznaWydac() As Boolean
Sprzet.vb:60:            Return Status = StatusDostepny
Sprzet.vb:61:        End Function
Sprzet.vb:62:
Sprzet.vb:63:        Public Overrides Function ToString() As String
Sprzet.vb:64:            Return NumerInwentarzowy & " " & Nazwa
Sprzet.vb:65:        End Function
Sprzet.vb:66:
Sprzet.vb:67:    End Class
Sprzet.vb:68:
Sprzet.vb:69:End Namespace
Taryfa.vb:1:' Modul: Model - taryfa cennikowa kategorii sprzetu | Autor: zespol IT | 2014-03-11
Taryfa.vb:2:' Historia:
Taryfa.vb:3:'   2015-02-04 MW: dodane ObowiazujeOd (taryfy z data obowiazywania)
Taryfa.vb:4:'   2018-09-17 PT: NazwaKategorii dla ekranu cennika
Taryfa.vb:5:
Taryfa.vb:6:Namespace Model
Taryfa.vb:7:
Taryfa.vb:8:    <Serializable()>
Taryfa.vb:9:    Public Class Taryfa
Taryfa.vb:10:
Taryfa.vb:11:        Public Property Id As Integer
Taryfa.vb:12:        Public Property Kategoria As String
Taryfa.vb:13:        Public Property MnoznikWeekend As Decimal
Taryfa.vb:14:        Public Property RabatTygodniowyProc As Decimal
Taryfa.vb:15:        Public Property ObowiazujeOd As Date
Taryfa.vb:16:
Taryfa.vb:17:        Public Shared ReadOnly KategorieZnane As String() = {"BUD", "OGR", "IMP", "TRN"}
Taryfa.vb:18:
Taryfa.vb:19:        Public Sub New()
Taryfa.vb:20:            Kategoria = ""
Taryfa.vb:21:            MnoznikWeekend = 1D
Taryfa.vb:22:            RabatTygodniowyProc = 0D
Taryfa.vb:23:            ObowiazujeOd = Date.Today
Taryfa.vb:24:        End Sub
Taryfa.vb:25:
Taryfa.vb:26:        Public Sub New(ByVal kategoria As String, ByVal mnoznikWeekend As Decimal, ByVal rabatTygodniowyProc As Decimal, ByVal obowiazujeOd As Date)
Taryfa.vb:27:            Me.Kategoria = kategoria
Taryfa.vb:28:            Me.MnoznikWeekend = mnoznikWeekend
Taryfa.vb:29:            Me.RabatTygodniowyProc = rabatTygodniowyProc
Taryfa.vb:30:            Me.ObowiazujeOd = obowiazujeOd
Taryfa.vb:31:        End Sub
Taryfa.vb:32:
Taryfa.vb:33:        Public Function CzyObowiazujeNa(ByVal dzien As Date) As Boolean
Taryfa.vb:34:            Return ObowiazujeOd <= dzien.Date
Taryfa.vb:35:        End Function
Taryfa.vb:36:
Taryfa.vb:37:        Public Shared Function CzyZnanaKategoria(ByVal kategoria As String) As Boolean
Taryfa.vb:38:            If String.IsNullOrEmpty(kategoria) Then Return False
Taryfa.vb:39:            Return Array.IndexOf(KategorieZnane, kategoria.Trim().ToUpper()) >= 0
Taryfa.vb:40:        End Function
Taryfa.vb:41:
Taryfa.vb:42:        Public Shared Function NazwaKategorii(ByVal kategoria As String) As String
Taryfa.vb:43:            Select Case kategoria
Taryfa.vb:44:                Case "BUD" : Return "Budowlany"
Taryfa.vb:45:                Case "OGR" : Return "Ogrodniczy"
Taryfa.vb:46:                Case "IMP" : Return "Imprezowy"
Taryfa.vb:47:                Case "TRN" : Return "Transportowy"
Taryfa.vb:48:                Case Else : Return "Inny (" & kategoria & ")"
Taryfa.vb:49:            End Select
Taryfa.vb:50:        End Function
Taryfa.vb:51:
Taryfa.vb:52:        Public Function Kopia() As Taryfa
Taryfa.vb:53:            Dim t As New Taryfa(Kategoria, MnoznikWeekend, RabatTygodniowyProc, ObowiazujeOd)
Taryfa.vb:54:            t.Id = Id
Taryfa.vb:55:            Return t
Taryfa.vb:56:        End Function
Taryfa.vb:57:
Taryfa.vb:58:        Public Shared Function Domyslna(ByVal kategoria As String) As Taryfa
Taryfa.vb:59:            Dim t As New Taryfa(kategoria, 1D, 0D, New Date(2014, 1, 1))
Taryfa.vb:60:            Return t
Taryfa.vb:61:        End Function
Taryfa.vb:62:
Taryfa.vb:63:        Public Function TakaSama(ByVal inna As Taryfa) As Boolean
Taryfa.vb:64:            If inna Is Nothing Then Return False
Taryfa.vb:65:            Return Kategoria = inna.Kategoria AndAlso MnoznikWeekend = inna.MnoznikWeekend _
Taryfa.vb:66:                AndAlso RabatTygodniowyProc = inna.RabatTygodniowyProc AndAlso ObowiazujeOd = inna.ObowiazujeOd
Taryfa.vb:67:        End Function
Taryfa.vb:68:
Taryfa.vb:69:        Public Overrides Function ToString() As String
Taryfa.vb:70:            Return Kategoria & " od " & ObowiazujeOd.ToString("yyyy-MM-dd") & " (weekend x" & MnoznikWeekend.ToString() & ", tydzien -" & RabatTygodniowyProc.ToString() & "%)"
Taryfa.vb:71:        End Function
Taryfa.vb:72:
Taryfa.vb:73:    End Class
Taryfa.vb:74:
Taryfa.vb:75:End Namespace
Wypozyczenie.vb:1:' Modul: Model\Wypozyczenie.vb | Autor: zespol IT | 2014-03-11
Wypozyczenie.vb:2:' Wypozyczenie (wydanie sprzetu klientowi) wraz z rozliczeniem zwrotu.
Wypozyczenie.vb:3:' 2015-06-22 MW: NumerPrzesylki (dostawa kurierem)
Wypozyczenie.vb:4:' 2016-11-03 JK: ZWiersza - mapowanie z DataRow
Wypozyczenie.vb:5:
Wypozyczenie.vb:6:Imports System.Data
Wypozyczenie.vb:7:
Wypozyczenie.vb:8:Namespace Model
Wypozyczenie.vb:9:
Wypozyczenie.vb:10:    Public Class Wypozyczenie
Wypozyczenie.vb:11:
Wypozyczenie.vb:12:        Public Property Id As Integer
Wypozyczenie.vb:13:        Public Property RezerwacjaId As Integer
Wypozyczenie.vb:14:        Public Property KlientId As Integer
Wypozyczenie.vb:15:        Public Property SprzetId As Integer
Wypozyczenie.vb:16:        Public Property DataWydania As Date
Wypozyczenie.vb:17:        Public Property PlanowanyZwrot As Date
Wypozyczenie.vb:18:        Public Property DataZwrotu As Nullable(Of Date)
Wypozyczenie.vb:19:        Public Property KaucjaPobrana As Decimal
Wypozyczenie.vb:20:        Public Property Oplata As Decimal
Wypozyczenie.vb:21:        Public Property Kara As Decimal
Wypozyczenie.vb:22:        Public Property KaucjaZwrocona As Decimal
Wypozyczenie.vb:23:        Public Property Status As String
Wypozyczenie.vb:24:        Public Property NumerPrzesylki As String
Wypozyczenie.vb:25:
Wypozyczenie.vb:26:        Public Sub New()
Wypozyczenie.vb:27:            RezerwacjaId = 0
Wypozyczenie.vb:28:            Status = "O"
Wypozyczenie.vb:29:            NumerPrzesylki = ""
Wypozyczenie.vb:30:        End Sub
Wypozyczenie.vb:31:
Wypozyczenie.vb:32:        Public ReadOnly Property CzyOtwarte As Boolean
Wypozyczenie.vb:33:            Get
Wypozyczenie.vb:34:                Return Status = "O"
Wypozyczenie.vb:35:            End Get
Wypozyczenie.vb:36:        End Property
Wypozyczenie.vb:37:
Wypozyczenie.vb:38:        Public ReadOnly Property CzyZRezerwacji As Boolean
Wypozyczenie.vb:39:            Get
Wypozyczenie.vb:40:                Return RezerwacjaId > 0
Wypozyczenie.vb:41:            End Get
Wypozyczenie.vb:42:        End Property
Wypozyczenie.vb:43:
Wypozyczenie.vb:44:        Public ReadOnly Property CzyZDostawa As Boolean
Wypozyczenie.vb:45:            Get
Wypozyczenie.vb:46:                Return Not String.IsNullOrEmpty(NumerPrzesylki)
Wypozyczenie.vb:47:            End Get
Wypozyczenie.vb:48:        End Property
Wypozyczenie.vb:49:
Wypozyczenie.vb:50:        Public Function CzyPoTerminie(ByVal naDzien As Date) As Boolean
Wypozyczenie.vb:51:            If Not CzyOtwarte Then Return False
Wypozyczenie.vb:52:            Return naDzien > PlanowanyZwrot
Wypozyczenie.vb:53:        End Function
Wypozyczenie.vb:54:
Wypozyczenie.vb:55:        Public Shared Function ZWiersza(ByVal wiersz As DataRow) As Wypozyczenie
Wypozyczenie.vb:56:            Dim w As New Wypozyczenie()
Wypozyczenie.vb:57:            w.Id = CInt(wiersz("Id"))
Wypozyczenie.vb:58:            If IsDBNull(wiersz("RezerwacjaId")) Then
Wypozyczenie.vb:59:                w.RezerwacjaId = 0
Wypozyczenie.vb:60:            Else
Wypozyczenie.vb:61:                w.RezerwacjaId = CInt(wiersz("RezerwacjaId"))
Wypozyczenie.vb:62:            End If
Wypozyczenie.vb:63:            w.KlientId = CInt(wiersz("KlientId"))
Wypozyczenie.vb:64:            w.SprzetId = CInt(wiersz("SprzetId"))
Wypozyczenie.vb:65:            w.DataWydania = CDate(wiersz("DataWydania"))
Wypozyczenie.vb:66:            w.PlanowanyZwrot = CDate(wiersz("PlanowanyZwrot"))
Wypozyczenie.vb:67:            If Not IsDBNull(wiersz("DataZwrotu")) Then w.DataZwrotu = CDate(wiersz("DataZwrotu"))
Wypozyczenie.vb:68:            w.KaucjaPobrana = CDec(wiersz("KaucjaPobrana"))
Wypozyczenie.vb:69:            If Not IsDBNull(wiersz("Oplata")) Then w.Oplata = CDec(wiersz("Oplata"))
Wypozyczenie.vb:70:            If Not IsDBNull(wiersz("Kara")) Then w.Kara = CDec(wiersz("Kara"))
Wypozyczenie.vb:71:            If Not IsDBNull(wiersz("KaucjaZwrocona")) Then w.KaucjaZwrocona = CDec(wiersz("KaucjaZwrocona"))
Wypozyczenie.vb:72:            w.Status = wiersz("Status").ToString()
Wypozyczenie.vb:73:            If Not IsDBNull(wiersz("NumerPrzesylki")) Then w.NumerPrzesylki = wiersz("NumerPrzesylki").ToString()
Wypozyczenie.vb:74:            Return w
Wypozyczenie.vb:75:        End Function
Wypozyczenie.vb:76:
Wypozyczenie.vb:77:        Public Overrides Function ToString() As String
Wypozyczenie.vb:78:            Return String.Format("Wypozyczenie {0} (klient {1}, sprzet {2}, status {3})", Id, KlientId, SprzetId, Status)
Wypozyczenie.vb:79:        End Function
Wypozyczenie.vb:80:
Wypozyczenie.vb:81:    End Class
Wypozyczenie.vb:82:
Wypozyczenie.vb:83:End Namespace
```

## M20 — surowy wynik H10

`git --no-pager -C 'legacy-app-parent\legacy-app\WypozyczalniaWeb' grep -n -I -e '^' -- '*.aspx.vb'`

```text
Cennik.aspx.vb:1:' Modul: Cennik.aspx - taryfy, stawki, import cennika, kalkulator | Autor: zespol IT | 2014-03-11
Cennik.aspx.vb:2:' Historia:
Cennik.aspx.vb:3:'   2015-02-04 MW: lista taryf z data obowiazywania
Cennik.aspx.vb:4:'   2017-06-12 JK: przycisk importu cennika XML
Cennik.aspx.vb:5:'   2019-04-02 PT: kalkulator oplaty dla wybranego klienta
Cennik.aspx.vb:6:
Cennik.aspx.vb:7:Imports System.Data
Cennik.aspx.vb:8:Imports System.Configuration
Cennik.aspx.vb:9:Imports System.Globalization
Cennik.aspx.vb:10:Imports Wypozyczalnia.Core
Cennik.aspx.vb:11:Imports Wypozyczalnia.Core.Model
Cennik.aspx.vb:12:Imports Wypozyczalnia.Core.Integracje
Cennik.aspx.vb:13:
Cennik.aspx.vb:14:Partial Class Cennik
Cennik.aspx.vb:15:    Inherits System.Web.UI.Page
Cennik.aspx.vb:16:
Cennik.aspx.vb:17:    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Cennik))
Cennik.aspx.vb:18:
Cennik.aspx.vb:19:    Protected WithEvents lblWynik As Global.System.Web.UI.WebControls.Label
Cennik.aspx.vb:20:    Protected WithEvents gvTaryfy As Global.System.Web.UI.WebControls.GridView
Cennik.aspx.vb:21:    Protected WithEvents ddlSprzet As Global.System.Web.UI.WebControls.DropDownList
Cennik.aspx.vb:22:    Protected WithEvents txtStawka As Global.System.Web.UI.WebControls.TextBox
Cennik.aspx.vb:23:    Protected WithEvents btnZapiszStawke As Global.System.Web.UI.WebControls.Button
Cennik.aspx.vb:24:    Protected WithEvents btnImportuj As Global.System.Web.UI.WebControls.Button
Cennik.aspx.vb:25:    Protected WithEvents lblKlient As Global.System.Web.UI.WebControls.Label
Cennik.aspx.vb:26:    Protected WithEvents ddlSprzetKalk As Global.System.Web.UI.WebControls.DropDownList
Cennik.aspx.vb:27:    Protected WithEvents txtOd As Global.System.Web.UI.WebControls.TextBox
Cennik.aspx.vb:28:    Protected WithEvents txtDo As Global.System.Web.UI.WebControls.TextBox
Cennik.aspx.vb:29:    Protected WithEvents btnOblicz As Global.System.Web.UI.WebControls.Button
Cennik.aspx.vb:30:    Protected WithEvents lblKwota As Global.System.Web.UI.WebControls.Label
Cennik.aspx.vb:31:    Protected WithEvents lblSzczegoly As Global.System.Web.UI.WebControls.Label
Cennik.aspx.vb:32:
Cennik.aspx.vb:33:    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
Cennik.aspx.vb:34:        If Not IsPostBack Then
Cennik.aspx.vb:35:            ZaladujTaryfy()
Cennik.aspx.vb:36:            ZaladujSprzet()
Cennik.aspx.vb:37:            PokazAktualnaStawke()
Cennik.aspx.vb:38:            txtOd.Text = Date.Today.AddDays(1).ToString("yyyy-MM-dd") & " 08:00"
Cennik.aspx.vb:39:            txtDo.Text = Date.Today.AddDays(2).ToString("yyyy-MM-dd") & " 08:00"
Cennik.aspx.vb:40:        End If
Cennik.aspx.vb:41:        PokazKlienta()
Cennik.aspx.vb:42:    End Sub
Cennik.aspx.vb:43:
Cennik.aspx.vb:44:    Protected Sub ddlSprzet_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlSprzet.SelectedIndexChanged
Cennik.aspx.vb:45:        PokazAktualnaStawke()
Cennik.aspx.vb:46:    End Sub
Cennik.aspx.vb:47:
Cennik.aspx.vb:48:    Protected Sub btnZapiszStawke_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZapiszStawke.Click
Cennik.aspx.vb:49:        If txtStawka.Text.Trim() = "" Then
Cennik.aspx.vb:50:            lblWynik.Text = "Podaj nową stawkę."
Cennik.aspx.vb:51:            Return
Cennik.aspx.vb:52:        End If
Cennik.aspx.vb:53:        If ddlSprzet.SelectedValue = "" Then
Cennik.aspx.vb:54:            lblWynik.Text = "Wybierz sprzęt."
Cennik.aspx.vb:55:            Return
Cennik.aspx.vb:56:        End If
Cennik.aspx.vb:57:        Try
Cennik.aspx.vb:58:            Dim sql As String = "UPDATE Sprzet SET StawkaDobowa = " & txtStawka.Text.Replace(",", ".") & " WHERE Id = " & ddlSprzet.SelectedValue
Cennik.aspx.vb:59:            Dim ile As Integer = DbHelper.WykonajPolecenie(sql)
Cennik.aspx.vb:60:            If ile = 1 Then
Cennik.aspx.vb:61:                lblWynik.Text = "Zapisano stawkę " & txtStawka.Text & " zł dla " & ddlSprzet.SelectedItem.Text & "."
Cennik.aspx.vb:62:                Log.Info("Zmiana stawki z ekranu cennika: " & ddlSprzet.SelectedValue & " = " & txtStawka.Text)
Cennik.aspx.vb:63:            Else
Cennik.aspx.vb:64:                lblWynik.Text = "Nie znaleziono sprzętu."
Cennik.aspx.vb:65:            End If
Cennik.aspx.vb:66:            txtStawka.Text = ""
Cennik.aspx.vb:67:            ZaladujSprzet()
Cennik.aspx.vb:68:            PokazAktualnaStawke()
Cennik.aspx.vb:69:        Catch ex As Exception
Cennik.aspx.vb:70:            Log.Error("Blad zapisu stawki", ex)
Cennik.aspx.vb:71:            lblWynik.Text = "Błąd zapisu stawki: " & ex.Message
Cennik.aspx.vb:72:        End Try
Cennik.aspx.vb:73:    End Sub
Cennik.aspx.vb:74:
Cennik.aspx.vb:75:    Protected Sub btnImportuj_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnImportuj.Click
Cennik.aspx.vb:76:        Dim katalog As String = ConfigurationManager.AppSettings("ImportCennikaKatalog")
Cennik.aspx.vb:77:        Dim import As New ImportCennikaXml()
Cennik.aspx.vb:78:        Try
Cennik.aspx.vb:79:            Dim oczekujace As String() = import.PlikiOczekujace(katalog)
Cennik.aspx.vb:80:            If oczekujace.Length = 0 Then
Cennik.aspx.vb:81:                lblWynik.Text = "Brak plików do importu w katalogu " & katalog & "."
Cennik.aspx.vb:82:                Return
Cennik.aspx.vb:83:            End If
Cennik.aspx.vb:84:            Dim ile As Integer = import.ImportujKatalog(katalog)
Cennik.aspx.vb:85:            lblWynik.Text = "Zaimportowano plików: " & ile & " (" & String.Join(", ", oczekujace) & ")."
Cennik.aspx.vb:86:            ZaladujTaryfy()
Cennik.aspx.vb:87:            ZaladujSprzet()
Cennik.aspx.vb:88:            PokazAktualnaStawke()
Cennik.aspx.vb:89:        Catch ex As System.IO.IOException
Cennik.aspx.vb:90:            Log.Error("Import cennika - blad pliku", ex)
Cennik.aspx.vb:91:            lblWynik.Text = "Błąd dostępu do katalogu importu: " & ex.Message
Cennik.aspx.vb:92:        Catch ex As System.Xml.XmlException
Cennik.aspx.vb:93:            Log.Error("Import cennika - niepoprawny XML", ex)
Cennik.aspx.vb:94:            lblWynik.Text = "Niepoprawny plik XML: " & ex.Message
Cennik.aspx.vb:95:        Catch ex As FormatException
Cennik.aspx.vb:96:            Log.Error("Import cennika - niepoprawna liczba", ex)
Cennik.aspx.vb:97:            lblWynik.Text = "Niepoprawna wartość liczbowa w pliku: " & ex.Message
Cennik.aspx.vb:98:        End Try
Cennik.aspx.vb:99:    End Sub
Cennik.aspx.vb:100:
Cennik.aspx.vb:101:    Protected Sub btnOblicz_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOblicz.Click
Cennik.aspx.vb:102:        lblKwota.Text = ""
Cennik.aspx.vb:103:        lblSzczegoly.Text = ""
Cennik.aspx.vb:104:        Dim klientId As Integer = Session("WybranyKlientId")
Cennik.aspx.vb:105:        Dim dataOd As Date
Cennik.aspx.vb:106:        Dim dataDo As Date
Cennik.aspx.vb:107:        If Not Date.TryParse(txtOd.Text, dataOd) Then
Cennik.aspx.vb:108:            lblWynik.Text = "Niepoprawna data od."
Cennik.aspx.vb:109:            Return
Cennik.aspx.vb:110:        End If
Cennik.aspx.vb:111:        If Not Date.TryParse(txtDo.Text, dataDo) Then
Cennik.aspx.vb:112:            lblWynik.Text = "Niepoprawna data do."
Cennik.aspx.vb:113:            Return
Cennik.aspx.vb:114:        End If
Cennik.aspx.vb:115:        If ddlSprzetKalk.SelectedValue = "" Then
Cennik.aspx.vb:116:            lblWynik.Text = "Wybierz sprzęt do kalkulacji."
Cennik.aspx.vb:117:            Return
Cennik.aspx.vb:118:        End If
Cennik.aspx.vb:119:        Dim pm As New PricingManager()
Cennik.aspx.vb:120:        Try
Cennik.aspx.vb:121:            Dim kwota As Decimal = pm.ObliczOplate(CInt(ddlSprzetKalk.SelectedValue), dataOd, dataDo, klientId)
Cennik.aspx.vb:122:            lblKwota.Text = kwota.ToString("N2") & " zł (" & pm.LiczbaDob(dataOd, dataDo) & " dób)"
Cennik.aspx.vb:123:            lblSzczegoly.Text = Server.HtmlEncode(pm.SzczegolyOplaty(CInt(ddlSprzetKalk.SelectedValue), dataOd, dataDo, klientId))
Cennik.aspx.vb:124:        Catch ex As ArgumentException
Cennik.aspx.vb:125:            lblWynik.Text = ex.Message
Cennik.aspx.vb:126:        Catch ex As BladBiznesowyException
Cennik.aspx.vb:127:            lblWynik.Text = ex.Message
Cennik.aspx.vb:128:        End Try
Cennik.aspx.vb:129:    End Sub
Cennik.aspx.vb:130:
Cennik.aspx.vb:131:    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PreRender
Cennik.aspx.vb:132:        Dim katalog As String = ConfigurationManager.AppSettings("ImportCennikaKatalog")
Cennik.aspx.vb:133:        If String.IsNullOrEmpty(katalog) Then
Cennik.aspx.vb:134:            btnImportuj.Enabled = False
Cennik.aspx.vb:135:            btnImportuj.ToolTip = "Brak klucza ImportCennikaKatalog w konfiguracji"
Cennik.aspx.vb:136:        Else
Cennik.aspx.vb:137:            btnImportuj.ToolTip = "Katalog: " & katalog
Cennik.aspx.vb:138:        End If
Cennik.aspx.vb:139:    End Sub
Cennik.aspx.vb:140:
Cennik.aspx.vb:141:    Private _aktualneKategorie As New List(Of String)()
Cennik.aspx.vb:142:
Cennik.aspx.vb:143:    Protected Sub gvTaryfy_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvTaryfy.RowDataBound
Cennik.aspx.vb:144:        If e.Row.RowType <> DataControlRowType.DataRow Then Return
Cennik.aspx.vb:145:        Dim wiersz As DataRowView = CType(e.Row.DataItem, DataRowView)
Cennik.aspx.vb:146:        Dim kategoria As String = wiersz("Kategoria").ToString().Trim()
Cennik.aspx.vb:147:        Dim od As Date = wiersz("ObowiazujeOd")
Cennik.aspx.vb:148:        If od > Date.Today Then
Cennik.aspx.vb:149:            e.Row.ForeColor = Drawing.Color.Gray
Cennik.aspx.vb:150:            e.Row.ToolTip = "Taryfa przyszła"
Cennik.aspx.vb:151:            Return
Cennik.aspx.vb:152:        End If
Cennik.aspx.vb:153:        If Not _aktualneKategorie.Contains(kategoria) Then
Cennik.aspx.vb:154:            _aktualneKategorie.Add(kategoria)
Cennik.aspx.vb:155:            e.Row.Font.Bold = True
Cennik.aspx.vb:156:            e.Row.ToolTip = "Taryfa obowiązująca"
Cennik.aspx.vb:157:        Else
Cennik.aspx.vb:158:            e.Row.ForeColor = Drawing.Color.DarkGray
Cennik.aspx.vb:159:            e.Row.ToolTip = "Taryfa archiwalna"
Cennik.aspx.vb:160:        End If
Cennik.aspx.vb:161:    End Sub
Cennik.aspx.vb:162:
Cennik.aspx.vb:163:    Private Sub ZaladujTaryfy()
Cennik.aspx.vb:164:        Dim sql As String = "SELECT Id, Kategoria, MnoznikWeekend, RabatTygodniowyProc, ObowiazujeOd FROM Taryfy ORDER BY Kategoria, ObowiazujeOd DESC"
Cennik.aspx.vb:165:        Try
Cennik.aspx.vb:166:            gvTaryfy.DataSource = DbHelper.WykonajZapytanie(sql)
Cennik.aspx.vb:167:            gvTaryfy.DataBind()
Cennik.aspx.vb:168:        Catch ex As Exception
Cennik.aspx.vb:169:            Log.Error("Blad odczytu taryf", ex)
Cennik.aspx.vb:170:            lblWynik.Text = "Nie udało się wczytać taryf."
Cennik.aspx.vb:171:        End Try
Cennik.aspx.vb:172:    End Sub
Cennik.aspx.vb:173:
Cennik.aspx.vb:174:    Private Sub ZaladujSprzet()
Cennik.aspx.vb:175:        Dim wybrany As String = ddlSprzet.SelectedValue
Cennik.aspx.vb:176:        Dim wybranyKalk As String = ddlSprzetKalk.SelectedValue
Cennik.aspx.vb:177:        Dim lista As List(Of Sprzet) = New EquipmentManager().ListaWKategorii("")
Cennik.aspx.vb:178:
Cennik.aspx.vb:179:        ddlSprzet.Items.Clear()
Cennik.aspx.vb:180:        ddlSprzetKalk.Items.Clear()
Cennik.aspx.vb:181:        ddlSprzet.Items.Add(New ListItem("-- wybierz --", ""))
Cennik.aspx.vb:182:        ddlSprzetKalk.Items.Add(New ListItem("-- wybierz --", ""))
Cennik.aspx.vb:183:        For Each s As Sprzet In lista
Cennik.aspx.vb:184:            Dim tekst As String = s.NumerInwentarzowy & " " & s.Nazwa
Cennik.aspx.vb:185:            ddlSprzet.Items.Add(New ListItem(tekst, s.Id.ToString()))
Cennik.aspx.vb:186:            ddlSprzetKalk.Items.Add(New ListItem(tekst, s.Id.ToString()))
Cennik.aspx.vb:187:        Next
Cennik.aspx.vb:188:        If wybrany <> "" AndAlso ddlSprzet.Items.FindByValue(wybrany) IsNot Nothing Then
Cennik.aspx.vb:189:            ddlSprzet.SelectedValue = wybrany
Cennik.aspx.vb:190:        End If
Cennik.aspx.vb:191:        If wybranyKalk <> "" AndAlso ddlSprzetKalk.Items.FindByValue(wybranyKalk) IsNot Nothing Then
Cennik.aspx.vb:192:            ddlSprzetKalk.SelectedValue = wybranyKalk
Cennik.aspx.vb:193:        End If
Cennik.aspx.vb:194:    End Sub
Cennik.aspx.vb:195:
Cennik.aspx.vb:196:    Private Sub PokazAktualnaStawke()
Cennik.aspx.vb:197:        If ddlSprzet.SelectedValue = "" Then
Cennik.aspx.vb:198:            txtStawka.ToolTip = ""
Cennik.aspx.vb:199:            Return
Cennik.aspx.vb:200:        End If
Cennik.aspx.vb:201:        Dim s As Sprzet = New EquipmentManager().PobierzSprzet(CInt(ddlSprzet.SelectedValue))
Cennik.aspx.vb:202:        If s Is Nothing Then Return
Cennik.aspx.vb:203:        txtStawka.ToolTip = "Aktualnie: " & s.StawkaDobowa.ToString("N2") & " zł, kaucja " & s.Kaucja.ToString("N2") & " zł"
Cennik.aspx.vb:204:        txtStawka.Attributes("placeholder") = s.StawkaDobowa.ToString("N2")
Cennik.aspx.vb:205:    End Sub
Cennik.aspx.vb:206:
Cennik.aspx.vb:207:    Private Sub PokazKlienta()
Cennik.aspx.vb:208:        Dim klientId As Integer = Session("WybranyKlientId")
Cennik.aspx.vb:209:        If klientId = 0 Then
Cennik.aspx.vb:210:            lblKlient.Text = "Klient: (nie wybrano - kalkulacja bez rabatu)"
Cennik.aspx.vb:211:            Return
Cennik.aspx.vb:212:        End If
Cennik.aspx.vb:213:        Dim k As Klient = New CustomerManager().PobierzKlienta(klientId)
Cennik.aspx.vb:214:        If k Is Nothing Then
Cennik.aspx.vb:215:            lblKlient.Text = "Klient: (nie znaleziono)"
Cennik.aspx.vb:216:        ElseIf k.Typ = "F" Then
Cennik.aspx.vb:217:            lblKlient.Text = "Klient: " & Server.HtmlEncode(k.NazwaFirmy) & " (próg rabatu " & k.ProgRabatu & ")"
Cennik.aspx.vb:218:        Else
Cennik.aspx.vb:219:            lblKlient.Text = "Klient: " & Server.HtmlEncode(k.Imie & " " & k.Nazwisko) & " (próg rabatu " & k.ProgRabatu & ")"
Cennik.aspx.vb:220:        End If
Cennik.aspx.vb:221:    End Sub
Cennik.aspx.vb:222:
Cennik.aspx.vb:223:End Class
Faktura.aspx.vb:1:' Modul: Faktura.aspx.vb | Autor: zespol IT | 2014-05-19
Faktura.aspx.vb:2:' Faktura za wypozyczenie: wystawienie, platnosc przez bramke, eksport CSV dnia.
Faktura.aspx.vb:3:' 2015-01-12 MW: platnosc przez bramke SOAP (zamiast strony Platnosc.aspx)
Faktura.aspx.vb:4:' 2018-04-03 PT: lista faktur z dnia i przycisk eksportu CSV
Faktura.aspx.vb:5:
Faktura.aspx.vb:6:Imports System.Data
Faktura.aspx.vb:7:Imports System.Net
Faktura.aspx.vb:8:Imports System.Web.Services.Protocols
Faktura.aspx.vb:9:Imports Wypozyczalnia.Core
Faktura.aspx.vb:10:Imports Wypozyczalnia.Core.Model
Faktura.aspx.vb:11:Imports Wypozyczalnia.Core.Integracje
Faktura.aspx.vb:12:
Faktura.aspx.vb:13:Partial Class Faktura
Faktura.aspx.vb:14:    Inherits System.Web.UI.Page
Faktura.aspx.vb:15:
Faktura.aspx.vb:16:    Protected WithEvents lblKomunikat As Global.System.Web.UI.WebControls.Label
Faktura.aspx.vb:17:    Protected WithEvents lblWypozyczenie As Global.System.Web.UI.WebControls.Label
Faktura.aspx.vb:18:    Protected WithEvents lblKlient As Global.System.Web.UI.WebControls.Label
Faktura.aspx.vb:19:    Protected WithEvents lblNumer As Global.System.Web.UI.WebControls.Label
Faktura.aspx.vb:20:    Protected WithEvents lblStatus As Global.System.Web.UI.WebControls.Label
Faktura.aspx.vb:21:    Protected WithEvents btnWystaw As Global.System.Web.UI.WebControls.Button
Faktura.aspx.vb:22:    Protected WithEvents gvPozycje As Global.System.Web.UI.WebControls.GridView
Faktura.aspx.vb:23:    Protected WithEvents lblNetto As Global.System.Web.UI.WebControls.Label
Faktura.aspx.vb:24:    Protected WithEvents lblVat As Global.System.Web.UI.WebControls.Label
Faktura.aspx.vb:25:    Protected WithEvents lblBrutto As Global.System.Web.UI.WebControls.Label
Faktura.aspx.vb:26:    Protected WithEvents btnZaplac As Global.System.Web.UI.WebControls.Button
Faktura.aspx.vb:27:    Protected WithEvents lblPlatnosc As Global.System.Web.UI.WebControls.Label
Faktura.aspx.vb:28:    Protected WithEvents gvFakturyDnia As Global.System.Web.UI.WebControls.GridView
Faktura.aspx.vb:29:    Protected WithEvents btnEksportCsv As Global.System.Web.UI.WebControls.Button
Faktura.aspx.vb:30:    Protected WithEvents lblEksport As Global.System.Web.UI.WebControls.Label
Faktura.aspx.vb:31:
Faktura.aspx.vb:32:    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Faktura))
Faktura.aspx.vb:33:
Faktura.aspx.vb:34:    Private Const Waluta As String = "PLN"
Faktura.aspx.vb:35:    Private Const FormatDaty As String = "yyyy-MM-dd HH:mm"
Faktura.aspx.vb:36:
Faktura.aspx.vb:37:    Private Property WypozyczenieId As Integer
Faktura.aspx.vb:38:        Get
Faktura.aspx.vb:39:            If ViewState("WypozyczenieId") Is Nothing Then Return 0
Faktura.aspx.vb:40:            Return ViewState("WypozyczenieId")
Faktura.aspx.vb:41:        End Get
Faktura.aspx.vb:42:        Set(ByVal value As Integer)
Faktura.aspx.vb:43:            ViewState("WypozyczenieId") = value
Faktura.aspx.vb:44:        End Set
Faktura.aspx.vb:45:    End Property
Faktura.aspx.vb:46:
Faktura.aspx.vb:47:    Private Property FakturaId As Integer
Faktura.aspx.vb:48:        Get
Faktura.aspx.vb:49:            If ViewState("FakturaId") Is Nothing Then Return 0
Faktura.aspx.vb:50:            Return ViewState("FakturaId")
Faktura.aspx.vb:51:        End Get
Faktura.aspx.vb:52:        Set(ByVal value As Integer)
Faktura.aspx.vb:53:            ViewState("FakturaId") = value
Faktura.aspx.vb:54:        End Set
Faktura.aspx.vb:55:    End Property
Faktura.aspx.vb:56:
Faktura.aspx.vb:57:    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
Faktura.aspx.vb:58:        If Not IsPostBack Then
Faktura.aspx.vb:59:            WypozyczenieId = UstalWypozyczenie()
Faktura.aspx.vb:60:            PokazWypozyczenie()
Faktura.aspx.vb:61:            WczytajFakturyDnia()
Faktura.aspx.vb:62:        End If
Faktura.aspx.vb:63:    End Sub
Faktura.aspx.vb:64:
Faktura.aspx.vb:65:    ' najpierw ?id= z linku po zwrocie, potem sesja
Faktura.aspx.vb:66:    Private Function UstalWypozyczenie() As Integer
Faktura.aspx.vb:67:        Dim id As Integer = 0
Faktura.aspx.vb:68:        Dim zParametru As String = Request.QueryString("id")
Faktura.aspx.vb:69:        If Not String.IsNullOrEmpty(zParametru) Then
Faktura.aspx.vb:70:            Integer.TryParse(zParametru, id)
Faktura.aspx.vb:71:        End If
Faktura.aspx.vb:72:        If id = 0 AndAlso Session("OstatnieWypozyczenieId") IsNot Nothing Then
Faktura.aspx.vb:73:            id = Session("OstatnieWypozyczenieId")
Faktura.aspx.vb:74:        End If
Faktura.aspx.vb:75:        Return id
Faktura.aspx.vb:76:    End Function
Faktura.aspx.vb:77:
Faktura.aspx.vb:78:    Private Sub PokazWypozyczenie()
Faktura.aspx.vb:79:        WyczyscFakture()
Faktura.aspx.vb:80:        If WypozyczenieId = 0 Then
Faktura.aspx.vb:81:            lblWypozyczenie.Text = "nie wybrano (wejdź z ekranu zwrotu)"
Faktura.aspx.vb:82:            PokazWybranegoKlienta()
Faktura.aspx.vb:83:            btnWystaw.Enabled = False
Faktura.aspx.vb:84:            btnZaplac.Enabled = False
Faktura.aspx.vb:85:            Return
Faktura.aspx.vb:86:        End If
Faktura.aspx.vb:87:
Faktura.aspx.vb:88:        Dim w As Wypozyczenie = New RentalManager().PobierzWypozyczenie(WypozyczenieId)
Faktura.aspx.vb:89:        If w Is Nothing Then
Faktura.aspx.vb:90:            lblKomunikat.Text = "Nie znaleziono wypożyczenia nr " & WypozyczenieId
Faktura.aspx.vb:91:            btnWystaw.Enabled = False
Faktura.aspx.vb:92:            btnZaplac.Enabled = False
Faktura.aspx.vb:93:            Return
Faktura.aspx.vb:94:        End If
Faktura.aspx.vb:95:
Faktura.aspx.vb:96:        Dim sprzet As Sprzet = New EquipmentManager().PobierzSprzet(w.SprzetId)
Faktura.aspx.vb:97:        Dim opis As String = "#" & w.Id & " | wydano " & w.DataWydania.ToString(FormatDaty)
Faktura.aspx.vb:98:        If sprzet IsNot Nothing Then opis = opis & " | " & sprzet.Nazwa
Faktura.aspx.vb:99:        If w.DataZwrotu.HasValue Then
Faktura.aspx.vb:100:            opis = opis & " | zwrot " & w.DataZwrotu.Value.ToString(FormatDaty)
Faktura.aspx.vb:101:        Else
Faktura.aspx.vb:102:            opis = opis & " | jeszcze nie zwrócono"
Faktura.aspx.vb:103:        End If
Faktura.aspx.vb:104:        lblWypozyczenie.Text = opis
Faktura.aspx.vb:105:
Faktura.aspx.vb:106:        Dim klient As Klient = New CustomerManager().PobierzKlienta(w.KlientId)
Faktura.aspx.vb:107:        If klient IsNot Nothing Then
Faktura.aspx.vb:108:            lblKlient.Text = NazwaKlienta(klient)
Faktura.aspx.vb:109:        End If
Faktura.aspx.vb:110:        If Session("WybranyKlientId") <> 0 AndAlso Session("WybranyKlientId") <> w.KlientId Then
Faktura.aspx.vb:111:            lblKomunikat.Text = "Uwaga: wypożyczenie należy do innego klienta niż wybrany na liście klientów."
Faktura.aspx.vb:112:        End If
Faktura.aspx.vb:113:
Faktura.aspx.vb:114:        Dim mgr As New InvoiceManager()
Faktura.aspx.vb:115:        Dim f As Wypozyczalnia.Core.Model.Faktura = mgr.PobierzFaktureDlaWypozyczenia(w.Id)
Faktura.aspx.vb:116:        If f IsNot Nothing Then
Faktura.aspx.vb:117:            PokazFakture(f)
Faktura.aspx.vb:118:            btnWystaw.Enabled = False
Faktura.aspx.vb:119:        ElseIf w.Status = "Z" Then
Faktura.aspx.vb:120:            Dim podglad As Wypozyczalnia.Core.Model.Faktura = mgr.PodgladFaktury(w.Id)
Faktura.aspx.vb:121:            If podglad IsNot Nothing Then
Faktura.aspx.vb:122:                PokazPozycje(podglad)
Faktura.aspx.vb:123:                lblNumer.Text = "(podgląd - faktura nie wystawiona)"
Faktura.aspx.vb:124:            End If
Faktura.aspx.vb:125:            btnWystaw.Enabled = True
Faktura.aspx.vb:126:            btnZaplac.Enabled = False
Faktura.aspx.vb:127:        Else
Faktura.aspx.vb:128:            lblKomunikat.Text = "Fakturę można wystawić dopiero po zwrocie sprzętu."
Faktura.aspx.vb:129:            btnWystaw.Enabled = False
Faktura.aspx.vb:130:            btnZaplac.Enabled = False
Faktura.aspx.vb:131:        End If
Faktura.aspx.vb:132:    End Sub
Faktura.aspx.vb:133:
Faktura.aspx.vb:134:    Private Sub PokazWybranegoKlienta()
Faktura.aspx.vb:135:        If Session("WybranyKlientId") Is Nothing OrElse Session("WybranyKlientId") = 0 Then
Faktura.aspx.vb:136:            lblKlient.Text = "-"
Faktura.aspx.vb:137:            Return
Faktura.aspx.vb:138:        End If
Faktura.aspx.vb:139:        Dim klient As Klient = New CustomerManager().PobierzKlienta(Session("WybranyKlientId"))
Faktura.aspx.vb:140:        If klient IsNot Nothing Then
Faktura.aspx.vb:141:            lblKlient.Text = NazwaKlienta(klient) & " (wybrany na liście klientów)"
Faktura.aspx.vb:142:        End If
Faktura.aspx.vb:143:    End Sub
Faktura.aspx.vb:144:
Faktura.aspx.vb:145:    Private Sub PokazFakture(ByVal f As Wypozyczalnia.Core.Model.Faktura)
Faktura.aspx.vb:146:        FakturaId = f.Id
Faktura.aspx.vb:147:        lblNumer.Text = f.Numer & " z dnia " & f.DataWystawienia.ToString("yyyy-MM-dd")
Faktura.aspx.vb:148:        If f.Status = "O" Then
Faktura.aspx.vb:149:            lblStatus.Text = "OPŁACONA (transakcja " & f.IdTransakcji & ")"
Faktura.aspx.vb:150:            lblStatus.ForeColor = Drawing.Color.Green
Faktura.aspx.vb:151:            btnZaplac.Enabled = False
Faktura.aspx.vb:152:        Else
Faktura.aspx.vb:153:            lblStatus.Text = "do zapłaty"
Faktura.aspx.vb:154:            lblStatus.ForeColor = Drawing.Color.DarkOrange
Faktura.aspx.vb:155:            btnZaplac.Enabled = True
Faktura.aspx.vb:156:        End If
Faktura.aspx.vb:157:        PokazPozycje(f)
Faktura.aspx.vb:158:    End Sub
Faktura.aspx.vb:159:
Faktura.aspx.vb:160:    Private Sub PokazPozycje(ByVal f As Wypozyczalnia.Core.Model.Faktura)
Faktura.aspx.vb:161:        gvPozycje.DataSource = f.Pozycje
Faktura.aspx.vb:162:        gvPozycje.DataBind()
Faktura.aspx.vb:163:        lblNetto.Text = f.Netto
Faktura.aspx.vb:164:        lblVat.Text = f.Vat
Faktura.aspx.vb:165:        lblBrutto.Text = f.Brutto
Faktura.aspx.vb:166:    End Sub
Faktura.aspx.vb:167:
Faktura.aspx.vb:168:    Private Sub WyczyscFakture()
Faktura.aspx.vb:169:        FakturaId = 0
Faktura.aspx.vb:170:        lblNumer.Text = "(nie wystawiono)"
Faktura.aspx.vb:171:        lblStatus.Text = ""
Faktura.aspx.vb:172:        lblNetto.Text = "0,00"
Faktura.aspx.vb:173:        lblVat.Text = "0,00"
Faktura.aspx.vb:174:        lblBrutto.Text = "0,00"
Faktura.aspx.vb:175:        lblPlatnosc.Text = ""
Faktura.aspx.vb:176:        gvPozycje.DataSource = Nothing
Faktura.aspx.vb:177:        gvPozycje.DataBind()
Faktura.aspx.vb:178:    End Sub
Faktura.aspx.vb:179:
Faktura.aspx.vb:180:    Private Sub WczytajFakturyDnia()
Faktura.aspx.vb:181:        Try
Faktura.aspx.vb:182:            gvFakturyDnia.DataSource = New InvoiceManager().FakturyZDnia(Date.Today)
Faktura.aspx.vb:183:            gvFakturyDnia.DataBind()
Faktura.aspx.vb:184:        Catch ex As Exception
Faktura.aspx.vb:185:            Log.Error("Blad pobierania faktur z dnia", ex)
Faktura.aspx.vb:186:        End Try
Faktura.aspx.vb:187:    End Sub
Faktura.aspx.vb:188:
Faktura.aspx.vb:189:    Protected Sub btnWystaw_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnWystaw.Click
Faktura.aspx.vb:190:        If WypozyczenieId = 0 Then
Faktura.aspx.vb:191:            lblKomunikat.Text = "Nie wybrano wypożyczenia."
Faktura.aspx.vb:192:            Return
Faktura.aspx.vb:193:        End If
Faktura.aspx.vb:194:        Try
Faktura.aspx.vb:195:            Dim f As Wypozyczalnia.Core.Model.Faktura = New InvoiceManager().WystawFaktureZaWypozyczenie(WypozyczenieId)
Faktura.aspx.vb:196:            PokazFakture(f)
Faktura.aspx.vb:197:            btnWystaw.Enabled = False
Faktura.aspx.vb:198:            lblKomunikat.ForeColor = Drawing.Color.Green
Faktura.aspx.vb:199:            lblKomunikat.Text = "Wystawiono fakturę " & f.Numer
Faktura.aspx.vb:200:            WczytajFakturyDnia()
Faktura.aspx.vb:201:        Catch ex As BladBiznesowyException
Faktura.aspx.vb:202:            lblKomunikat.Text = ex.Message
Faktura.aspx.vb:203:        Catch ex As Exception
Faktura.aspx.vb:204:            Log.Error("Blad wystawiania faktury dla wypozyczenia " & WypozyczenieId, ex)
Faktura.aspx.vb:205:            lblKomunikat.Text = "Błąd wystawiania faktury: " & ex.Message
Faktura.aspx.vb:206:        End Try
Faktura.aspx.vb:207:    End Sub
Faktura.aspx.vb:208:
Faktura.aspx.vb:209:    Protected Sub btnZaplac_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZaplac.Click
Faktura.aspx.vb:210:        If FakturaId = 0 Then
Faktura.aspx.vb:211:            lblKomunikat.Text = "Najpierw wystaw fakturę."
Faktura.aspx.vb:212:            Return
Faktura.aspx.vb:213:        End If
Faktura.aspx.vb:214:        Dim mgr As New InvoiceManager()
Faktura.aspx.vb:215:        Dim f As Wypozyczalnia.Core.Model.Faktura = mgr.PobierzFakture(FakturaId)
Faktura.aspx.vb:216:        If f Is Nothing Then
Faktura.aspx.vb:217:            lblKomunikat.Text = "Nie znaleziono faktury."
Faktura.aspx.vb:218:            Return
Faktura.aspx.vb:219:        End If
Faktura.aspx.vb:220:        If f.Status = "O" Then
Faktura.aspx.vb:221:            lblPlatnosc.Text = "Faktura jest już opłacona."
Faktura.aspx.vb:222:            Return
Faktura.aspx.vb:223:        End If
Faktura.aspx.vb:224:
Faktura.aspx.vb:225:        Dim bramka As New BramkaPlatnosci.BramkaService()
Faktura.aspx.vb:226:        bramka.Timeout = 30000
Faktura.aspx.vb:227:        Try
Faktura.aspx.vb:228:            Dim wynik As BramkaPlatnosci.WynikPlatnosci = bramka.ZarejestrujPlatnosc(f.Numer, f.Brutto, Waluta)
Faktura.aspx.vb:229:            Log.Info("Bramka: faktura " & f.Numer & ", status " & wynik.Status & ", transakcja " & wynik.IdTransakcji)
Faktura.aspx.vb:230:            Select Case wynik.Status
Faktura.aspx.vb:231:                Case "OK"
Faktura.aspx.vb:232:                    mgr.OznaczOplacona(f.Id, wynik.IdTransakcji)
Faktura.aspx.vb:233:                    lblPlatnosc.ForeColor = Drawing.Color.Green
Faktura.aspx.vb:234:                    lblPlatnosc.Text = "Płatność przyjęta, transakcja " & wynik.IdTransakcji
Faktura.aspx.vb:235:                    Session("OstatnieWypozyczenieId") = Nothing
Faktura.aspx.vb:236:                    PokazFakture(mgr.PobierzFakture(f.Id))
Faktura.aspx.vb:237:                    WczytajFakturyDnia()
Faktura.aspx.vb:238:                Case "ODRZUCONA"
Faktura.aspx.vb:239:                    lblPlatnosc.ForeColor = Drawing.Color.Red
Faktura.aspx.vb:240:                    lblPlatnosc.Text = "Bramka odrzuciła płatność: " & wynik.Komunikat
Faktura.aspx.vb:241:                Case Else
Faktura.aspx.vb:242:                    lblPlatnosc.ForeColor = Drawing.Color.Red
Faktura.aspx.vb:243:                    lblPlatnosc.Text = "Błąd bramki: " & wynik.Komunikat
Faktura.aspx.vb:244:            End Select
Faktura.aspx.vb:245:        Catch ex As SoapException
Faktura.aspx.vb:246:            Log.Error("SOAP: blad bramki dla faktury " & f.Numer, ex)
Faktura.aspx.vb:247:            lblPlatnosc.Text = "Bramka zwróciła błąd: " & ex.Message
Faktura.aspx.vb:248:        Catch ex As WebException
Faktura.aspx.vb:249:            Log.Error("Brak polaczenia z bramka platnosci", ex)
Faktura.aspx.vb:250:            lblPlatnosc.Text = "Brak połączenia z bramką płatności. Spróbuj ponownie później."
Faktura.aspx.vb:251:        Finally
Faktura.aspx.vb:252:            bramka.Dispose()
Faktura.aspx.vb:253:        End Try
Faktura.aspx.vb:254:    End Sub
Faktura.aspx.vb:255:
Faktura.aspx.vb:256:    Protected Sub btnEksportCsv_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEksportCsv.Click
Faktura.aspx.vb:257:        Try
Faktura.aspx.vb:258:            Dim plik As String = New EksportCsv().EksportujDzien(Date.Today)
Faktura.aspx.vb:259:            lblEksport.ForeColor = Drawing.Color.Green
Faktura.aspx.vb:260:            lblEksport.Text = "Zapisano: " & plik
Faktura.aspx.vb:261:        Catch ex As Exception
Faktura.aspx.vb:262:            Log.Error("Blad eksportu CSV z ekranu faktur", ex)
Faktura.aspx.vb:263:            lblEksport.ForeColor = Drawing.Color.Red
Faktura.aspx.vb:264:            lblEksport.Text = "Eksport nieudany: " & ex.Message
Faktura.aspx.vb:265:        End Try
Faktura.aspx.vb:266:    End Sub
Faktura.aspx.vb:267:
Faktura.aspx.vb:268:    Private Function NazwaKlienta(ByVal k As Klient) As String
Faktura.aspx.vb:269:        If k.Typ = "F" AndAlso Not String.IsNullOrEmpty(k.NazwaFirmy) Then
Faktura.aspx.vb:270:            Return k.NazwaFirmy & " (" & k.Imie & " " & k.Nazwisko & ")"
Faktura.aspx.vb:271:        End If
Faktura.aspx.vb:272:        Return k.Imie & " " & k.Nazwisko
Faktura.aspx.vb:273:    End Function
Faktura.aspx.vb:274:
Faktura.aspx.vb:275:End Class
Klienci.aspx.vb:1:' Modul: Klienci.aspx.vb - lista, wyszukiwanie i edycja klientow | Autor: zespol IT | 2014-03-11
Klienci.aspx.vb:2:' Historia: 2014-03-11 JK utworzenie strony
Klienci.aspx.vb:3:'           2015-11-23 MW blokowanie klientow, przejscie do rezerwacji
Klienci.aspx.vb:4:'           2018-09-04 PT pole NIP i typ klienta (firmy)
Klienci.aspx.vb:5:Imports System.Data
Klienci.aspx.vb:6:Imports System.Data.SqlClient
Klienci.aspx.vb:7:Imports Wypozyczalnia.Core
Klienci.aspx.vb:8:Imports Wypozyczalnia.Core.Model
Klienci.aspx.vb:9:Imports Wypozyczalnia.Core.DAL
Klienci.aspx.vb:10:
Klienci.aspx.vb:11:Partial Class Klienci
Klienci.aspx.vb:12:    Inherits System.Web.UI.Page
Klienci.aspx.vb:13:
Klienci.aspx.vb:14:    Protected WithEvents lblKomunikat As Global.System.Web.UI.WebControls.Label
Klienci.aspx.vb:15:    Protected WithEvents txtSzukaj As Global.System.Web.UI.WebControls.TextBox
Klienci.aspx.vb:16:    Protected WithEvents btnSzukaj As Global.System.Web.UI.WebControls.Button
Klienci.aspx.vb:17:    Protected WithEvents btnNowy As Global.System.Web.UI.WebControls.Button
Klienci.aspx.vb:18:    Protected WithEvents gvKlienci As Global.System.Web.UI.WebControls.GridView
Klienci.aspx.vb:19:    Protected WithEvents lblTrybFormularza As Global.System.Web.UI.WebControls.Label
Klienci.aspx.vb:20:    Protected WithEvents hfKlientId As Global.System.Web.UI.WebControls.HiddenField
Klienci.aspx.vb:21:    Protected WithEvents txtImie As Global.System.Web.UI.WebControls.TextBox
Klienci.aspx.vb:22:    Protected WithEvents txtNazwisko As Global.System.Web.UI.WebControls.TextBox
Klienci.aspx.vb:23:    Protected WithEvents txtNazwaFirmy As Global.System.Web.UI.WebControls.TextBox
Klienci.aspx.vb:24:    Protected WithEvents txtEmail As Global.System.Web.UI.WebControls.TextBox
Klienci.aspx.vb:25:    Protected WithEvents txtTelefon As Global.System.Web.UI.WebControls.TextBox
Klienci.aspx.vb:26:    Protected WithEvents txtNumerDokumentu As Global.System.Web.UI.WebControls.TextBox
Klienci.aspx.vb:27:    Protected WithEvents txtNIP As Global.System.Web.UI.WebControls.TextBox
Klienci.aspx.vb:28:    Protected WithEvents ddlTyp As Global.System.Web.UI.WebControls.DropDownList
Klienci.aspx.vb:29:    Protected WithEvents ddlProg As Global.System.Web.UI.WebControls.DropDownList
Klienci.aspx.vb:30:    Protected WithEvents txtPowodBlokady As Global.System.Web.UI.WebControls.TextBox
Klienci.aspx.vb:31:    Protected WithEvents btnZapisz As Global.System.Web.UI.WebControls.Button
Klienci.aspx.vb:32:    Protected WithEvents btnZablokuj As Global.System.Web.UI.WebControls.Button
Klienci.aspx.vb:33:
Klienci.aspx.vb:34:    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Klienci))
Klienci.aspx.vb:35:
Klienci.aspx.vb:36:    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
Klienci.aspx.vb:37:        If Not IsPostBack Then
Klienci.aspx.vb:38:            WyczyscFormularz()
Klienci.aspx.vb:39:            ZaladujListe("")
Klienci.aspx.vb:40:            Dim wybrany As Integer = Session("WybranyKlientId")
Klienci.aspx.vb:41:            If wybrany > 0 Then
Klienci.aspx.vb:42:                lblKomunikat.Text = "Aktualnie wybrany klient: nr " & wybrany
Klienci.aspx.vb:43:                lblKomunikat.ForeColor = Drawing.Color.DarkGreen
Klienci.aspx.vb:44:            End If
Klienci.aspx.vb:45:        End If
Klienci.aspx.vb:46:    End Sub
Klienci.aspx.vb:47:
Klienci.aspx.vb:48:    Protected Sub btnSzukaj_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSzukaj.Click
Klienci.aspx.vb:49:        Dim sql As String = "SELECT Id, Imie, Nazwisko, NazwaFirmy, Email, Telefon FROM Klienci WHERE Nazwisko LIKE '%" & txtSzukaj.Text & "%' ORDER BY Nazwisko"
Klienci.aspx.vb:50:        Try
Klienci.aspx.vb:51:            Dim dt As DataTable = DbHelper.WykonajZapytanie(sql)
Klienci.aspx.vb:52:            gvKlienci.PageIndex = 0
Klienci.aspx.vb:53:            gvKlienci.DataSource = dt
Klienci.aspx.vb:54:            gvKlienci.DataBind()
Klienci.aspx.vb:55:            ViewState("OstatniaFraza") = txtSzukaj.Text
Klienci.aspx.vb:56:            If dt.Rows.Count = 0 Then
Klienci.aspx.vb:57:                lblKomunikat.Text = "Nie znaleziono klientow."
Klienci.aspx.vb:58:            End If
Klienci.aspx.vb:59:        Catch ex As SqlException
Klienci.aspx.vb:60:            Log.Error("Blad wyszukiwania klientow", ex)
Klienci.aspx.vb:61:            lblKomunikat.Text = "Blad bazy danych: " & ex.Message
Klienci.aspx.vb:62:        End Try
Klienci.aspx.vb:63:    End Sub
Klienci.aspx.vb:64:
Klienci.aspx.vb:65:    Protected Sub btnNowy_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNowy.Click
Klienci.aspx.vb:66:        WyczyscFormularz()
Klienci.aspx.vb:67:        gvKlienci.SelectedIndex = -1
Klienci.aspx.vb:68:    End Sub
Klienci.aspx.vb:69:
Klienci.aspx.vb:70:    Protected Sub gvKlienci_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gvKlienci.SelectedIndexChanged
Klienci.aspx.vb:71:        Dim klientId As Integer = gvKlienci.SelectedDataKey.Value
Klienci.aspx.vb:72:        Dim cm As New CustomerManager()
Klienci.aspx.vb:73:        If Not cm.CzyMozeWypozyczac(klientId) Then
Klienci.aspx.vb:74:            Dim k As Klient = cm.PobierzKlienta(klientId)
Klienci.aspx.vb:75:            lblKomunikat.Text = "Klient zablokowany: " & If(k Is Nothing, "", k.PowodBlokady)
Klienci.aspx.vb:76:            Return
Klienci.aspx.vb:77:        End If
Klienci.aspx.vb:78:        Session("WybranyKlientId") = klientId
Klienci.aspx.vb:79:        Log.Debug("Wybrano klienta " & klientId & " w sesji " & Session.SessionID)
Klienci.aspx.vb:80:        Response.Redirect("Rezerwacja.aspx")
Klienci.aspx.vb:81:    End Sub
Klienci.aspx.vb:82:
Klienci.aspx.vb:83:    Protected Sub gvKlienci_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles gvKlienci.RowCommand
Klienci.aspx.vb:84:        If e.CommandName <> "Edytuj" Then Return
Klienci.aspx.vb:85:        Dim indeks As Integer = Convert.ToInt32(e.CommandArgument)
Klienci.aspx.vb:86:        Dim klientId As Integer = gvKlienci.DataKeys(indeks).Value
Klienci.aspx.vb:87:        WczytajDoFormularza(klientId)
Klienci.aspx.vb:88:    End Sub
Klienci.aspx.vb:89:
Klienci.aspx.vb:90:    Protected Sub gvKlienci_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvKlienci.PageIndexChanging
Klienci.aspx.vb:91:        gvKlienci.PageIndex = e.NewPageIndex
Klienci.aspx.vb:92:        ZaladujListe(ViewState("OstatniaFraza"))
Klienci.aspx.vb:93:    End Sub
Klienci.aspx.vb:94:
Klienci.aspx.vb:95:    Protected Sub btnZapisz_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZapisz.Click
Klienci.aspx.vb:96:        lblKomunikat.Text = ""
Klienci.aspx.vb:97:        If txtEmail.Text.Trim() = "" OrElse Not txtEmail.Text.Contains("@") Then
Klienci.aspx.vb:98:            lblKomunikat.Text = "Niepoprawny adres e-mail."
Klienci.aspx.vb:99:            Return
Klienci.aspx.vb:100:        End If
Klienci.aspx.vb:101:        Dim blad As String = WalidujFormularz()
Klienci.aspx.vb:102:        If blad <> "" Then
Klienci.aspx.vb:103:            lblKomunikat.Text = blad
Klienci.aspx.vb:104:            Return
Klienci.aspx.vb:105:        End If
Klienci.aspx.vb:106:
Klienci.aspx.vb:107:        Dim k As New Klient()
Klienci.aspx.vb:108:        k.Id = CInt(hfKlientId.Value)
Klienci.aspx.vb:109:        k.Imie = txtImie.Text.Trim()
Klienci.aspx.vb:110:        k.Nazwisko = txtNazwisko.Text.Trim()
Klienci.aspx.vb:111:        k.NazwaFirmy = txtNazwaFirmy.Text.Trim()
Klienci.aspx.vb:112:        k.Email = txtEmail.Text.Trim()
Klienci.aspx.vb:113:        k.Telefon = txtTelefon.Text.Trim()
Klienci.aspx.vb:114:        k.NumerDokumentu = txtNumerDokumentu.Text.Trim().ToUpper()
Klienci.aspx.vb:115:        k.NIP = txtNIP.Text.Replace("-", "").Trim()
Klienci.aspx.vb:116:        k.Typ = ddlTyp.SelectedValue
Klienci.aspx.vb:117:        k.ProgRabatu = ddlProg.SelectedValue
Klienci.aspx.vb:118:        If k.Typ = "I" Then
Klienci.aspx.vb:119:            k.NIP = Nothing
Klienci.aspx.vb:120:            If k.NazwaFirmy = "" Then k.NazwaFirmy = Nothing
Klienci.aspx.vb:121:        End If
Klienci.aspx.vb:122:
Klienci.aspx.vb:123:        Try
Klienci.aspx.vb:124:            Dim cm As New CustomerManager()
Klienci.aspx.vb:125:            Dim noweId As Integer = cm.Zapisz(k)
Klienci.aspx.vb:126:            hfKlientId.Value = noweId
Klienci.aspx.vb:127:            lblTrybFormularza.Text = "Edycja klienta nr " & noweId
Klienci.aspx.vb:128:            lblKomunikat.Text = "Zapisano klienta nr " & noweId
Klienci.aspx.vb:129:            lblKomunikat.ForeColor = Drawing.Color.DarkGreen
Klienci.aspx.vb:130:            ZaladujListe(ViewState("OstatniaFraza"))
Klienci.aspx.vb:131:        Catch ex As BladBiznesowyException
Klienci.aspx.vb:132:            lblKomunikat.Text = ex.Message
Klienci.aspx.vb:133:        Catch ex As SqlException
Klienci.aspx.vb:134:            Log.Error("Blad zapisu klienta", ex)
Klienci.aspx.vb:135:            lblKomunikat.Text = "Nie udalo sie zapisac klienta: " & ex.Message
Klienci.aspx.vb:136:        End Try
Klienci.aspx.vb:137:    End Sub
Klienci.aspx.vb:138:
Klienci.aspx.vb:139:    Protected Sub btnZablokuj_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZablokuj.Click
Klienci.aspx.vb:140:        Dim klientId As Integer = CInt(hfKlientId.Value)
Klienci.aspx.vb:141:        If klientId = 0 Then
Klienci.aspx.vb:142:            lblKomunikat.Text = "Najpierw wybierz klienta do edycji."
Klienci.aspx.vb:143:            Return
Klienci.aspx.vb:144:        End If
Klienci.aspx.vb:145:        Dim powod As String = txtPowodBlokady.Text.Trim()
Klienci.aspx.vb:146:        If powod = "" Then powod = "blokada recznie z ekranu Klienci"
Klienci.aspx.vb:147:        Dim cm As New CustomerManager()
Klienci.aspx.vb:148:        cm.Zablokuj(klientId, powod)
Klienci.aspx.vb:149:        If Session("WybranyKlientId") = klientId Then
Klienci.aspx.vb:150:            Session("WybranyKlientId") = 0
Klienci.aspx.vb:151:        End If
Klienci.aspx.vb:152:        lblKomunikat.Text = "Klient nr " & klientId & " zostal zablokowany."
Klienci.aspx.vb:153:        ZaladujListe(ViewState("OstatniaFraza"))
Klienci.aspx.vb:154:    End Sub
Klienci.aspx.vb:155:
Klienci.aspx.vb:156:    Private Sub ZaladujListe(ByVal fraza As String)
Klienci.aspx.vb:157:        Try
Klienci.aspx.vb:158:            Dim cm As New CustomerManager()
Klienci.aspx.vb:159:            gvKlienci.DataSource = cm.Szukaj(fraza)
Klienci.aspx.vb:160:            gvKlienci.DataBind()
Klienci.aspx.vb:161:        Catch ex As SqlException
Klienci.aspx.vb:162:            Log.Error("Blad ladowania listy klientow", ex)
Klienci.aspx.vb:163:            lblKomunikat.Text = "Blad bazy danych."
Klienci.aspx.vb:164:        End Try
Klienci.aspx.vb:165:    End Sub
Klienci.aspx.vb:166:
Klienci.aspx.vb:167:    Private Sub WczytajDoFormularza(ByVal klientId As Integer)
Klienci.aspx.vb:168:        Dim cm As New CustomerManager()
Klienci.aspx.vb:169:        Dim k As Klient = cm.PobierzKlienta(klientId)
Klienci.aspx.vb:170:        If k Is Nothing Then
Klienci.aspx.vb:171:            lblKomunikat.Text = "Nie znaleziono klienta nr " & klientId
Klienci.aspx.vb:172:            Return
Klienci.aspx.vb:173:        End If
Klienci.aspx.vb:174:        hfKlientId.Value = k.Id
Klienci.aspx.vb:175:        txtImie.Text = k.Imie
Klienci.aspx.vb:176:        txtNazwisko.Text = k.Nazwisko
Klienci.aspx.vb:177:        txtNazwaFirmy.Text = k.NazwaFirmy
Klienci.aspx.vb:178:        txtEmail.Text = k.Email
Klienci.aspx.vb:179:        txtTelefon.Text = k.Telefon
Klienci.aspx.vb:180:        txtNumerDokumentu.Text = k.NumerDokumentu
Klienci.aspx.vb:181:        txtNIP.Text = k.NIP
Klienci.aspx.vb:182:        UstawWybor(ddlTyp, k.Typ)
Klienci.aspx.vb:183:        UstawWybor(ddlProg, k.ProgRabatu.ToString())
Klienci.aspx.vb:184:        txtPowodBlokady.Text = k.PowodBlokady
Klienci.aspx.vb:185:        lblTrybFormularza.Text = "Edycja klienta nr " & k.Id
Klienci.aspx.vb:186:        If k.Zablokowany Then
Klienci.aspx.vb:187:            lblTrybFormularza.Text = lblTrybFormularza.Text & " (ZABLOKOWANY)"
Klienci.aspx.vb:188:            btnZablokuj.Enabled = False
Klienci.aspx.vb:189:        Else
Klienci.aspx.vb:190:            btnZablokuj.Enabled = True
Klienci.aspx.vb:191:        End If
Klienci.aspx.vb:192:    End Sub
Klienci.aspx.vb:193:
Klienci.aspx.vb:194:    Private Function WalidujFormularz() As String
Klienci.aspx.vb:195:        If txtTelefon.Text.Trim() = "" Then
Klienci.aspx.vb:196:            Return "Telefon jest wymagany."
Klienci.aspx.vb:197:        End If
Klienci.aspx.vb:198:        If txtTelefon.Text.Trim().Length > 20 Then
Klienci.aspx.vb:199:            Return "Telefon moze miec maksymalnie 20 znakow."
Klienci.aspx.vb:200:        End If
Klienci.aspx.vb:201:        If ddlTyp.SelectedValue = "F" Then
Klienci.aspx.vb:202:            If txtNazwaFirmy.Text.Trim() = "" Then
Klienci.aspx.vb:203:                Return "Dla firmy podaj nazwe firmy."
Klienci.aspx.vb:204:            End If
Klienci.aspx.vb:205:            Dim nip As String = txtNIP.Text.Replace("-", "").Replace(" ", "").Trim()
Klienci.aspx.vb:206:            If nip.Length <> 10 OrElse Not IsNumeric(nip) Then
Klienci.aspx.vb:207:                Return "NIP musi miec 10 cyfr."
Klienci.aspx.vb:208:            End If
Klienci.aspx.vb:209:        End If
Klienci.aspx.vb:210:        If ddlTyp.SelectedValue = "I" AndAlso txtNumerDokumentu.Text.Trim() = "" Then
Klienci.aspx.vb:211:            Return "Dla osoby prywatnej podaj numer dokumentu."
Klienci.aspx.vb:212:        End If
Klienci.aspx.vb:213:        Return ""
Klienci.aspx.vb:214:    End Function
Klienci.aspx.vb:215:
Klienci.aspx.vb:216:    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PreRender
Klienci.aspx.vb:217:        Dim wybrany As Integer = Session("WybranyKlientId")
Klienci.aspx.vb:218:        If wybrany > 0 AndAlso gvKlienci.Rows.Count > 0 Then
Klienci.aspx.vb:219:            For i As Integer = 0 To gvKlienci.Rows.Count - 1
Klienci.aspx.vb:220:                If gvKlienci.DataKeys(i).Value = wybrany Then
Klienci.aspx.vb:221:                    gvKlienci.Rows(i).Font.Bold = True
Klienci.aspx.vb:222:                End If
Klienci.aspx.vb:223:            Next
Klienci.aspx.vb:224:        End If
Klienci.aspx.vb:225:    End Sub
Klienci.aspx.vb:226:
Klienci.aspx.vb:227:    Private Sub UstawWybor(ByVal lista As DropDownList, ByVal wartosc As String)
Klienci.aspx.vb:228:        Dim pozycja As ListItem = lista.Items.FindByValue(wartosc)
Klienci.aspx.vb:229:        If pozycja IsNot Nothing Then
Klienci.aspx.vb:230:            lista.ClearSelection()
Klienci.aspx.vb:231:            pozycja.Selected = True
Klienci.aspx.vb:232:        End If
Klienci.aspx.vb:233:    End Sub
Klienci.aspx.vb:234:
Klienci.aspx.vb:235:    Private Sub WyczyscFormularz()
Klienci.aspx.vb:236:        hfKlientId.Value = "0"
Klienci.aspx.vb:237:        txtImie.Text = ""
Klienci.aspx.vb:238:        txtNazwisko.Text = ""
Klienci.aspx.vb:239:        txtNazwaFirmy.Text = ""
Klienci.aspx.vb:240:        txtEmail.Text = ""
Klienci.aspx.vb:241:        txtTelefon.Text = ""
Klienci.aspx.vb:242:        txtNumerDokumentu.Text = ""
Klienci.aspx.vb:243:        txtNIP.Text = ""
Klienci.aspx.vb:244:        txtPowodBlokady.Text = ""
Klienci.aspx.vb:245:        ddlTyp.SelectedIndex = 0
Klienci.aspx.vb:246:        ddlProg.SelectedIndex = 0
Klienci.aspx.vb:247:        btnZablokuj.Enabled = False
Klienci.aspx.vb:248:        lblTrybFormularza.Text = "Nowy klient"
Klienci.aspx.vb:249:    End Sub
Klienci.aspx.vb:250:
Klienci.aspx.vb:251:End Class
Rezerwacja.aspx.vb:1:Option Strict Off
Rezerwacja.aspx.vb:2:' Modul: Rezerwacja.aspx - formularz rezerwacji sprzetu | Autor: zespol IT | 2014-03-11
Rezerwacja.aspx.vb:3:' Historia:
Rezerwacja.aspx.vb:4:'   2014-09-22 JK  szacunek ceny przed zapisem (przycisk Sprawdz)
Rezerwacja.aspx.vb:5:'   2016-05-30 MW  kalendarze AjaxControlToolkit zamiast pol tekstowych
Rezerwacja.aspx.vb:6:'   2019-02-20 MW  anulowanie z listy rezerwacji klienta
Rezerwacja.aspx.vb:7:
Rezerwacja.aspx.vb:8:Imports System.Data
Rezerwacja.aspx.vb:9:Imports System.Globalization
Rezerwacja.aspx.vb:10:Imports Wypozyczalnia.Core
Rezerwacja.aspx.vb:11:Imports Wypozyczalnia.Core.Model
Rezerwacja.aspx.vb:12:
Rezerwacja.aspx.vb:13:Partial Class Rezerwacja
Rezerwacja.aspx.vb:14:    Inherits System.Web.UI.Page
Rezerwacja.aspx.vb:15:
Rezerwacja.aspx.vb:16:    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Rezerwacja))
Rezerwacja.aspx.vb:17:
Rezerwacja.aspx.vb:18:    Private Shared ReadOnly FormatyTerminu() As String = {"yyyy-MM-dd HH:mm", "yyyy-MM-dd H:mm", "dd.MM.yyyy HH:mm", "dd.MM.yyyy H:mm"}
Rezerwacja.aspx.vb:19:
Rezerwacja.aspx.vb:20:    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
Rezerwacja.aspx.vb:21:        If Not IsPostBack Then
Rezerwacja.aspx.vb:22:            PokazKlienta()
Rezerwacja.aspx.vb:23:            WypelnijSprzet()
Rezerwacja.aspx.vb:24:            WybierzSprzetZAdresu()
Rezerwacja.aspx.vb:25:            txtDataOd.Text = Date.Today.AddDays(1).ToString("yyyy-MM-dd")
Rezerwacja.aspx.vb:26:            txtDataDo.Text = Date.Today.AddDays(2).ToString("yyyy-MM-dd")
Rezerwacja.aspx.vb:27:            If Session("SzacunekRezerwacji") IsNot Nothing Then
Rezerwacja.aspx.vb:28:                lblSzacunek.Text = Session("SzacunekRezerwacji")
Rezerwacja.aspx.vb:29:            End If
Rezerwacja.aspx.vb:30:            PokazRezerwacje()
Rezerwacja.aspx.vb:31:        End If
Rezerwacja.aspx.vb:32:    End Sub
Rezerwacja.aspx.vb:33:
Rezerwacja.aspx.vb:34:    Protected Sub ddlKategoria_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlKategoria.SelectedIndexChanged
Rezerwacja.aspx.vb:35:        WypelnijSprzet()
Rezerwacja.aspx.vb:36:        lblSzacunek.Text = "-"
Rezerwacja.aspx.vb:37:        Session("SzacunekRezerwacji") = Nothing
Rezerwacja.aspx.vb:38:    End Sub
Rezerwacja.aspx.vb:39:
Rezerwacja.aspx.vb:40:    Protected Sub btnSprawdz_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSprawdz.Click
Rezerwacja.aspx.vb:41:        Dim dataOd As Date
Rezerwacja.aspx.vb:42:        Dim dataDo As Date
Rezerwacja.aspx.vb:43:        If ddlSprzet.SelectedValue = "" Then
Rezerwacja.aspx.vb:44:            PokazBlad("Wybierz sprzet.")
Rezerwacja.aspx.vb:45:            Return
Rezerwacja.aspx.vb:46:        End If
Rezerwacja.aspx.vb:47:        If Not ParsujTermin(txtDataOd.Text, txtGodzinaOd.Text, dataOd) OrElse Not ParsujTermin(txtDataDo.Text, txtGodzinaDo.Text, dataDo) Then
Rezerwacja.aspx.vb:48:            PokazBlad("Niepoprawny termin. Format: rrrr-mm-dd gg:mm")
Rezerwacja.aspx.vb:49:            Return
Rezerwacja.aspx.vb:50:        End If
Rezerwacja.aspx.vb:51:        If dataDo <= dataOd Then
Rezerwacja.aspx.vb:52:            PokazBlad("Data zakonczenia musi byc pozniejsza niz data rozpoczecia.")
Rezerwacja.aspx.vb:53:            Return
Rezerwacja.aspx.vb:54:        End If
Rezerwacja.aspx.vb:55:
Rezerwacja.aspx.vb:56:        Dim sprzetId As Integer = ddlSprzet.SelectedValue
Rezerwacja.aspx.vb:57:        Dim mgr As New EquipmentManager()
Rezerwacja.aspx.vb:58:        If Not mgr.SprawdzDostepnosc(sprzetId, dataOd, dataDo) Then
Rezerwacja.aspx.vb:59:            PokazBlad("Sprzet niedostepny w wybranym terminie: " & OpisKolizji(mgr.KolizjeWTerminie(sprzetId, dataOd, dataDo)))
Rezerwacja.aspx.vb:60:            lblSzacunek.Text = "-"
Rezerwacja.aspx.vb:61:            Session("SzacunekRezerwacji") = Nothing
Rezerwacja.aspx.vb:62:            Return
Rezerwacja.aspx.vb:63:        End If
Rezerwacja.aspx.vb:64:
Rezerwacja.aspx.vb:65:        Dim sprzet As Sprzet = mgr.PobierzSprzet(sprzetId)
Rezerwacja.aspx.vb:66:        Dim klientId As Integer = Session("WybranyKlientId")
Rezerwacja.aspx.vb:67:        Dim prog As Integer = 0
Rezerwacja.aspx.vb:68:        If klientId > 0 Then
Rezerwacja.aspx.vb:69:            prog = New CustomerManager().ProgRabatu(klientId)
Rezerwacja.aspx.vb:70:        End If
Rezerwacja.aspx.vb:71:        Dim szacunek As Decimal = ObliczSzacunek(sprzet, dataOd, dataDo, prog)
Rezerwacja.aspx.vb:72:        lblSzacunek.Text = szacunek.ToString("N2")
Rezerwacja.aspx.vb:73:        Session("SzacunekRezerwacji") = szacunek
Rezerwacja.aspx.vb:74:        PokazInfo("Sprzet dostepny w wybranym terminie.")
Rezerwacja.aspx.vb:75:    End Sub
Rezerwacja.aspx.vb:76:
Rezerwacja.aspx.vb:77:    Protected Sub btnZarezerwuj_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZarezerwuj.Click
Rezerwacja.aspx.vb:78:        Dim klientId As Integer = Session("WybranyKlientId")
Rezerwacja.aspx.vb:79:        If klientId = 0 Then
Rezerwacja.aspx.vb:80:            PokazBlad("Najpierw wybierz klienta na stronie Klienci.")
Rezerwacja.aspx.vb:81:            Return
Rezerwacja.aspx.vb:82:        End If
Rezerwacja.aspx.vb:83:        If ddlSprzet.SelectedValue = "" Then
Rezerwacja.aspx.vb:84:            PokazBlad("Wybierz sprzet.")
Rezerwacja.aspx.vb:85:            Return
Rezerwacja.aspx.vb:86:        End If
Rezerwacja.aspx.vb:87:
Rezerwacja.aspx.vb:88:        Dim dataOd As Date
Rezerwacja.aspx.vb:89:        Dim dataDo As Date
Rezerwacja.aspx.vb:90:        If Not ParsujTermin(txtDataOd.Text, txtGodzinaOd.Text, dataOd) Then
Rezerwacja.aspx.vb:91:            PokazBlad("Niepoprawna data rozpoczecia.")
Rezerwacja.aspx.vb:92:            Return
Rezerwacja.aspx.vb:93:        End If
Rezerwacja.aspx.vb:94:        If Not ParsujTermin(txtDataDo.Text, txtGodzinaDo.Text, dataDo) Then
Rezerwacja.aspx.vb:95:            PokazBlad("Niepoprawna data zakonczenia.")
Rezerwacja.aspx.vb:96:            Return
Rezerwacja.aspx.vb:97:        End If
Rezerwacja.aspx.vb:98:        If dataOd < Date.Now.AddMinutes(-30) Then
Rezerwacja.aspx.vb:99:            PokazBlad("Nie mozna rezerwowac wstecz.")
Rezerwacja.aspx.vb:100:            Return
Rezerwacja.aspx.vb:101:        End If
Rezerwacja.aspx.vb:102:        If dataDo <= dataOd Then
Rezerwacja.aspx.vb:103:            PokazBlad("Data zakonczenia musi byc pozniejsza niz data rozpoczecia.")
Rezerwacja.aspx.vb:104:            Return
Rezerwacja.aspx.vb:105:        End If
Rezerwacja.aspx.vb:106:        If dataOd.Hour < 7 OrElse dataOd.Hour > 18 Then
Rezerwacja.aspx.vb:107:            PokazBlad("Wydania sprzetu tylko w godzinach 7:00 - 18:00.")
Rezerwacja.aspx.vb:108:            Return
Rezerwacja.aspx.vb:109:        End If
Rezerwacja.aspx.vb:110:        If txtUwagi.Text.Length > 500 Then
Rezerwacja.aspx.vb:111:            txtUwagi.Text = txtUwagi.Text.Substring(0, 500)
Rezerwacja.aspx.vb:112:        End If
Rezerwacja.aspx.vb:113:
Rezerwacja.aspx.vb:114:        Dim rezerwacje As New ReservationManager()
Rezerwacja.aspx.vb:115:        Try
Rezerwacja.aspx.vb:116:            Dim noweId As Integer = rezerwacje.UtworzRezerwacje(klientId, ddlSprzet.SelectedValue, dataOd, dataDo, txtUwagi.Text)
Rezerwacja.aspx.vb:117:            Dim nowa = rezerwacje.PobierzRezerwacje(noweId)
Rezerwacja.aspx.vb:118:            PokazInfo("Zapisano rezerwacje nr " & noweId & ". Cena: " & nowa.CenaSzacunkowa.ToString("N2") & " zl.")
Rezerwacja.aspx.vb:119:            txtUwagi.Text = ""
Rezerwacja.aspx.vb:120:            PokazRezerwacje()
Rezerwacja.aspx.vb:121:        Catch ex As BladBiznesowyException
Rezerwacja.aspx.vb:122:            PokazBlad(ex.Message)
Rezerwacja.aspx.vb:123:        Catch ex As ArgumentException
Rezerwacja.aspx.vb:124:            PokazBlad(ex.Message)
Rezerwacja.aspx.vb:125:        Catch ex As Exception
Rezerwacja.aspx.vb:126:            Log.Error("Blad zapisu rezerwacji, klient " & klientId, ex)
Rezerwacja.aspx.vb:127:            PokazBlad("Nie udalo sie zapisac rezerwacji. Sprobuj ponownie.")
Rezerwacja.aspx.vb:128:        End Try
Rezerwacja.aspx.vb:129:    End Sub
Rezerwacja.aspx.vb:130:
Rezerwacja.aspx.vb:131:    Protected Sub gvRezerwacje_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gvRezerwacje.SelectedIndexChanged
Rezerwacja.aspx.vb:132:        If gvRezerwacje.SelectedIndex < 0 Then
Rezerwacja.aspx.vb:133:            Return
Rezerwacja.aspx.vb:134:        End If
Rezerwacja.aspx.vb:135:        ViewState("WybranaRezerwacja") = gvRezerwacje.SelectedDataKey.Value
Rezerwacja.aspx.vb:136:        Dim rezerwacje As New ReservationManager()
Rezerwacja.aspx.vb:137:        Dim wybrana = rezerwacje.PobierzRezerwacje(gvRezerwacje.SelectedDataKey.Value)
Rezerwacja.aspx.vb:138:        If wybrana Is Nothing Then
Rezerwacja.aspx.vb:139:            PokazBlad("Rezerwacja nie istnieje (odswiez liste).")
Rezerwacja.aspx.vb:140:            btnAnuluj.Enabled = False
Rezerwacja.aspx.vb:141:            Return
Rezerwacja.aspx.vb:142:        End If
Rezerwacja.aspx.vb:143:        btnAnuluj.Enabled = wybrana.CzyAktywna
Rezerwacja.aspx.vb:144:        Dim opis As String = rezerwacje.OpisRezerwacji(wybrana.Id)
Rezerwacja.aspx.vb:145:        If Not String.IsNullOrEmpty(wybrana.Uwagi) Then
Rezerwacja.aspx.vb:146:            opis = opis & ". Uwagi: " & wybrana.Uwagi
Rezerwacja.aspx.vb:147:        End If
Rezerwacja.aspx.vb:148:        PokazInfo(opis)
Rezerwacja.aspx.vb:149:    End Sub
Rezerwacja.aspx.vb:150:
Rezerwacja.aspx.vb:151:    Protected Sub gvRezerwacje_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvRezerwacje.RowDataBound
Rezerwacja.aspx.vb:152:        If e.Row.RowType <> DataControlRowType.DataRow Then
Rezerwacja.aspx.vb:153:            Return
Rezerwacja.aspx.vb:154:        End If
Rezerwacja.aspx.vb:155:        Dim wiersz As DataRowView = CType(e.Row.DataItem, DataRowView)
Rezerwacja.aspx.vb:156:        Dim komorkaStatusu As TableCell = e.Row.Cells(5)
Rezerwacja.aspx.vb:157:        Select Case CStr(wiersz("Status"))
Rezerwacja.aspx.vb:158:            Case "N"
Rezerwacja.aspx.vb:159:                komorkaStatusu.Text = "nowa"
Rezerwacja.aspx.vb:160:            Case "P"
Rezerwacja.aspx.vb:161:                komorkaStatusu.Text = "potwierdzona"
Rezerwacja.aspx.vb:162:                komorkaStatusu.Font.Bold = True
Rezerwacja.aspx.vb:163:            Case "A"
Rezerwacja.aspx.vb:164:                komorkaStatusu.Text = "anulowana"
Rezerwacja.aspx.vb:165:                e.Row.CssClass = "wyszarzony"
Rezerwacja.aspx.vb:166:            Case "Z"
Rezerwacja.aspx.vb:167:                komorkaStatusu.Text = "zrealizowana"
Rezerwacja.aspx.vb:168:                e.Row.CssClass = "wyszarzony"
Rezerwacja.aspx.vb:169:        End Select
Rezerwacja.aspx.vb:170:        If CDate(wiersz("DataDo")) < Date.Now AndAlso CStr(wiersz("Status")) = "N" Then
Rezerwacja.aspx.vb:171:            komorkaStatusu.Text = komorkaStatusu.Text & " (termin minal)"
Rezerwacja.aspx.vb:172:        End If
Rezerwacja.aspx.vb:173:    End Sub
Rezerwacja.aspx.vb:174:
Rezerwacja.aspx.vb:175:    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PreRender
Rezerwacja.aspx.vb:176:        ceDataOd.StartDate = Date.Today
Rezerwacja.aspx.vb:177:        ceDataDo.StartDate = Date.Today
Rezerwacja.aspx.vb:178:        btnSprawdz.Enabled = ddlSprzet.Items.Count > 1
Rezerwacja.aspx.vb:179:    End Sub
Rezerwacja.aspx.vb:180:
Rezerwacja.aspx.vb:181:    ' Rezerwacja.aspx?sprzet=<Id> - link "rezerwuj" z katalogu sprzetu
Rezerwacja.aspx.vb:182:    Private Sub WybierzSprzetZAdresu()
Rezerwacja.aspx.vb:183:        Dim parametr As String = Request.QueryString("sprzet")
Rezerwacja.aspx.vb:184:        Dim sprzetId As Integer
Rezerwacja.aspx.vb:185:        If String.IsNullOrEmpty(parametr) OrElse Not Integer.TryParse(parametr, sprzetId) Then
Rezerwacja.aspx.vb:186:            Return
Rezerwacja.aspx.vb:187:        End If
Rezerwacja.aspx.vb:188:        Dim sprzet As Sprzet = New EquipmentManager().PobierzSprzet(sprzetId)
Rezerwacja.aspx.vb:189:        If sprzet Is Nothing Then
Rezerwacja.aspx.vb:190:            Return
Rezerwacja.aspx.vb:191:        End If
Rezerwacja.aspx.vb:192:        Dim pozycjaKategorii As ListItem = ddlKategoria.Items.FindByValue(sprzet.Kategoria)
Rezerwacja.aspx.vb:193:        If pozycjaKategorii IsNot Nothing Then
Rezerwacja.aspx.vb:194:            ddlKategoria.ClearSelection()
Rezerwacja.aspx.vb:195:            pozycjaKategorii.Selected = True
Rezerwacja.aspx.vb:196:            WypelnijSprzet()
Rezerwacja.aspx.vb:197:        End If
Rezerwacja.aspx.vb:198:        Dim pozycja As ListItem = ddlSprzet.Items.FindByValue(sprzetId.ToString())
Rezerwacja.aspx.vb:199:        If pozycja IsNot Nothing Then
Rezerwacja.aspx.vb:200:            ddlSprzet.ClearSelection()
Rezerwacja.aspx.vb:201:            pozycja.Selected = True
Rezerwacja.aspx.vb:202:        End If
Rezerwacja.aspx.vb:203:    End Sub
Rezerwacja.aspx.vb:204:
Rezerwacja.aspx.vb:205:    Protected Sub btnAnuluj_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAnuluj.Click
Rezerwacja.aspx.vb:206:        If ViewState("WybranaRezerwacja") Is Nothing Then
Rezerwacja.aspx.vb:207:            PokazBlad("Wybierz rezerwacje z listy.")
Rezerwacja.aspx.vb:208:            Return
Rezerwacja.aspx.vb:209:        End If
Rezerwacja.aspx.vb:210:        Dim rezerwacjaId As Integer = ViewState("WybranaRezerwacja")
Rezerwacja.aspx.vb:211:        Try
Rezerwacja.aspx.vb:212:            Dim rezerwacje As New ReservationManager()
Rezerwacja.aspx.vb:213:            rezerwacje.Anuluj(rezerwacjaId)
Rezerwacja.aspx.vb:214:            PokazInfo("Rezerwacja nr " & rezerwacjaId & " anulowana.")
Rezerwacja.aspx.vb:215:            ViewState("WybranaRezerwacja") = Nothing
Rezerwacja.aspx.vb:216:            gvRezerwacje.SelectedIndex = -1
Rezerwacja.aspx.vb:217:            PokazRezerwacje()
Rezerwacja.aspx.vb:218:        Catch ex As BladBiznesowyException
Rezerwacja.aspx.vb:219:            PokazBlad(ex.Message)
Rezerwacja.aspx.vb:220:        End Try
Rezerwacja.aspx.vb:221:    End Sub
Rezerwacja.aspx.vb:222:
Rezerwacja.aspx.vb:223:    Private Sub PokazKlienta()
Rezerwacja.aspx.vb:224:        Dim klientId As Integer = Session("WybranyKlientId")
Rezerwacja.aspx.vb:225:        If klientId = 0 Then
Rezerwacja.aspx.vb:226:            lblKlient.Text = "(nie wybrano - wybierz klienta na stronie Klienci)"
Rezerwacja.aspx.vb:227:            btnZarezerwuj.Enabled = False
Rezerwacja.aspx.vb:228:            Return
Rezerwacja.aspx.vb:229:        End If
Rezerwacja.aspx.vb:230:        Dim klient As Klient = New CustomerManager().PobierzKlienta(klientId)
Rezerwacja.aspx.vb:231:        If klient Is Nothing Then
Rezerwacja.aspx.vb:232:            lblKlient.Text = "(nieznany klient " & klientId & ")"
Rezerwacja.aspx.vb:233:            btnZarezerwuj.Enabled = False
Rezerwacja.aspx.vb:234:            Return
Rezerwacja.aspx.vb:235:        End If
Rezerwacja.aspx.vb:236:        If klient.Typ = "F" AndAlso Not String.IsNullOrEmpty(klient.NazwaFirmy) Then
Rezerwacja.aspx.vb:237:            lblKlient.Text = Server.HtmlEncode(klient.NazwaFirmy & " (" & klient.Imie & " " & klient.Nazwisko & ")")
Rezerwacja.aspx.vb:238:        Else
Rezerwacja.aspx.vb:239:            lblKlient.Text = Server.HtmlEncode(klient.Imie & " " & klient.Nazwisko)
Rezerwacja.aspx.vb:240:        End If
Rezerwacja.aspx.vb:241:        If klient.Zablokowany Then
Rezerwacja.aspx.vb:242:            lblKlient.Text = lblKlient.Text & " - ZABLOKOWANY"
Rezerwacja.aspx.vb:243:            lblKlient.CssClass = "blad"
Rezerwacja.aspx.vb:244:        End If
Rezerwacja.aspx.vb:245:        btnZarezerwuj.Enabled = True
Rezerwacja.aspx.vb:246:    End Sub
Rezerwacja.aspx.vb:247:
Rezerwacja.aspx.vb:248:    Private Sub WypelnijSprzet()
Rezerwacja.aspx.vb:249:        ddlSprzet.Items.Clear()
Rezerwacja.aspx.vb:250:        ddlSprzet.Items.Add(New ListItem("-- wybierz --", ""))
Rezerwacja.aspx.vb:251:        Dim mgr As New EquipmentManager()
Rezerwacja.aspx.vb:252:        For Each s In mgr.ListaWKategorii(ddlKategoria.SelectedValue)
Rezerwacja.aspx.vb:253:            Dim opis As String = s.NumerInwentarzowy & " - " & s.Nazwa & " (" & s.StawkaDobowa.ToString("N2") & " zl/doba)"
Rezerwacja.aspx.vb:254:            If s.Status = "S" Then
Rezerwacja.aspx.vb:255:                opis = opis & " [serwis]"
Rezerwacja.aspx.vb:256:            End If
Rezerwacja.aspx.vb:257:            ddlSprzet.Items.Add(New ListItem(opis, s.Id.ToString()))
Rezerwacja.aspx.vb:258:        Next
Rezerwacja.aspx.vb:259:    End Sub
Rezerwacja.aspx.vb:260:
Rezerwacja.aspx.vb:261:    Private Sub PokazRezerwacje()
Rezerwacja.aspx.vb:262:        Dim klientId As Integer = Session("WybranyKlientId")
Rezerwacja.aspx.vb:263:        If klientId = 0 Then
Rezerwacja.aspx.vb:264:            gvRezerwacje.DataSource = Nothing
Rezerwacja.aspx.vb:265:            gvRezerwacje.DataBind()
Rezerwacja.aspx.vb:266:            btnAnuluj.Enabled = False
Rezerwacja.aspx.vb:267:            Return
Rezerwacja.aspx.vb:268:        End If
Rezerwacja.aspx.vb:269:        Dim tabela As DataTable = New ReservationManager().RezerwacjeKlienta(klientId)
Rezerwacja.aspx.vb:270:        gvRezerwacje.DataSource = tabela
Rezerwacja.aspx.vb:271:        gvRezerwacje.DataBind()
Rezerwacja.aspx.vb:272:        btnAnuluj.Enabled = tabela.Rows.Count > 0
Rezerwacja.aspx.vb:273:    End Sub
Rezerwacja.aspx.vb:274:
Rezerwacja.aspx.vb:275:    Private Function OpisKolizji(ByVal kolizje As DataTable) As String
Rezerwacja.aspx.vb:276:        Dim opisy As New List(Of String)()
Rezerwacja.aspx.vb:277:        For Each wiersz As DataRow In kolizje.Rows
Rezerwacja.aspx.vb:278:            Select Case CStr(wiersz("Zrodlo"))
Rezerwacja.aspx.vb:279:                Case "R"
Rezerwacja.aspx.vb:280:                    opisy.Add("rezerwacja nr " & wiersz("Id") & " (" & CDate(wiersz("DataOd")).ToString("yyyy-MM-dd HH:mm") & _
Rezerwacja.aspx.vb:281:                              " - " & CDate(wiersz("DataDo")).ToString("yyyy-MM-dd HH:mm") & ")")
Rezerwacja.aspx.vb:282:                Case "W"
Rezerwacja.aspx.vb:283:                    opisy.Add("wypozyczenie nr " & wiersz("Id") & " do " & CDate(wiersz("DataDo")).ToString("yyyy-MM-dd HH:mm"))
Rezerwacja.aspx.vb:284:                Case "S"
Rezerwacja.aspx.vb:285:                    opisy.Add("sprzet w serwisie albo wycofany")
Rezerwacja.aspx.vb:286:                Case Else
Rezerwacja.aspx.vb:287:                    opisy.Add("kolizja " & wiersz("Zrodlo"))
Rezerwacja.aspx.vb:288:            End Select
Rezerwacja.aspx.vb:289:        Next
Rezerwacja.aspx.vb:290:        Return String.Join("; ", opisy.ToArray())
Rezerwacja.aspx.vb:291:    End Function
Rezerwacja.aspx.vb:292:
Rezerwacja.aspx.vb:293:    Private Function ParsujTermin(ByVal data As String, ByVal godzina As String, ByRef wynik As Date) As Boolean
Rezerwacja.aspx.vb:294:        If String.IsNullOrEmpty(data) Then
Rezerwacja.aspx.vb:295:            Return False
Rezerwacja.aspx.vb:296:        End If
Rezerwacja.aspx.vb:297:        If String.IsNullOrEmpty(godzina) Then
Rezerwacja.aspx.vb:298:            godzina = "08:00"
Rezerwacja.aspx.vb:299:        End If
Rezerwacja.aspx.vb:300:        Return Date.TryParseExact(data.Trim() & " " & godzina.Trim(), FormatyTerminu, CultureInfo.InvariantCulture, DateTimeStyles.None, wynik)
Rezerwacja.aspx.vb:301:    End Function
Rezerwacja.aspx.vb:302:
Rezerwacja.aspx.vb:303:    Private Sub PokazBlad(ByVal tekst As String)
Rezerwacja.aspx.vb:304:        lblKomunikat.CssClass = "blad"
Rezerwacja.aspx.vb:305:        lblKomunikat.Text = Server.HtmlEncode(tekst)
Rezerwacja.aspx.vb:306:    End Sub
Rezerwacja.aspx.vb:307:
Rezerwacja.aspx.vb:308:    Private Sub PokazInfo(ByVal tekst As String)
Rezerwacja.aspx.vb:309:        lblKomunikat.CssClass = "info"
Rezerwacja.aspx.vb:310:        lblKomunikat.Text = Server.HtmlEncode(tekst)
Rezerwacja.aspx.vb:311:    End Sub
Rezerwacja.aspx.vb:312:
Rezerwacja.aspx.vb:313:    Private Function ObliczSzacunek(ByVal sprzet As Sprzet, ByVal dataOd As Date, ByVal dataDo As Date, ByVal prog As Integer) As Decimal
Rezerwacja.aspx.vb:314:        Dim dni As Long = DateDiff(DateInterval.Day, dataOd.Date, dataDo.Date)
Rezerwacja.aspx.vb:315:        If dni < 1 Then dni = 1
Rezerwacja.aspx.vb:316:        Dim kwota As Decimal = dni * sprzet.StawkaDobowa
Rezerwacja.aspx.vb:317:        kwota = kwota * (100D - prog * 2.5D) / 100D
Rezerwacja.aspx.vb:318:        Return Math.Round(kwota, 2, MidpointRounding.AwayFromZero)
Rezerwacja.aspx.vb:319:    End Function
Rezerwacja.aspx.vb:320:
Rezerwacja.aspx.vb:321:End Class
Sprzet.aspx.vb:1:' Modul: Sprzet.aspx - katalog sprzetu, edycja, serwis | Autor: zespol IT | 2014-03-11
Sprzet.aspx.vb:2:' Historia:
Sprzet.aspx.vb:3:'   2015-06-02 MW  pole kaucji w formularzu
Sprzet.aspx.vb:4:'   2018-01-15 PT  historia wypozyczen pod formularzem
Sprzet.aspx.vb:5:
Sprzet.aspx.vb:6:Imports System.Data
Sprzet.aspx.vb:7:Imports System.Globalization
Sprzet.aspx.vb:8:Imports Wypozyczalnia.Core
Sprzet.aspx.vb:9:Imports SprzetModel = Wypozyczalnia.Core.Model.Sprzet
Sprzet.aspx.vb:10:
Sprzet.aspx.vb:11:Partial Class Sprzet
Sprzet.aspx.vb:12:    Inherits System.Web.UI.Page
Sprzet.aspx.vb:13:
Sprzet.aspx.vb:14:    Protected WithEvents form1 As Global.System.Web.UI.HtmlControls.HtmlForm
Sprzet.aspx.vb:15:    Protected WithEvents ddlKategoria As Global.System.Web.UI.WebControls.DropDownList
Sprzet.aspx.vb:16:    Protected WithEvents gvSprzet As Global.System.Web.UI.WebControls.GridView
Sprzet.aspx.vb:17:    Protected WithEvents hfSprzetId As Global.System.Web.UI.WebControls.HiddenField
Sprzet.aspx.vb:18:    Protected WithEvents txtNumer As Global.System.Web.UI.WebControls.TextBox
Sprzet.aspx.vb:19:    Protected WithEvents txtNazwa As Global.System.Web.UI.WebControls.TextBox
Sprzet.aspx.vb:20:    Protected WithEvents ddlKategoriaEdycja As Global.System.Web.UI.WebControls.DropDownList
Sprzet.aspx.vb:21:    Protected WithEvents txtStawka As Global.System.Web.UI.WebControls.TextBox
Sprzet.aspx.vb:22:    Protected WithEvents txtKaucja As Global.System.Web.UI.WebControls.TextBox
Sprzet.aspx.vb:23:    Protected WithEvents lblStatus As Global.System.Web.UI.WebControls.Label
Sprzet.aspx.vb:24:    Protected WithEvents btnNowy As Global.System.Web.UI.WebControls.Button
Sprzet.aspx.vb:25:    Protected WithEvents btnZapisz As Global.System.Web.UI.WebControls.Button
Sprzet.aspx.vb:26:    Protected WithEvents btnSerwis As Global.System.Web.UI.WebControls.Button
Sprzet.aspx.vb:27:    Protected WithEvents lblKomunikat As Global.System.Web.UI.WebControls.Label
Sprzet.aspx.vb:28:    Protected WithEvents gvHistoria As Global.System.Web.UI.WebControls.GridView
Sprzet.aspx.vb:29:
Sprzet.aspx.vb:30:    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Sprzet))
Sprzet.aspx.vb:31:
Sprzet.aspx.vb:32:    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
Sprzet.aspx.vb:33:        If Not IsPostBack Then
Sprzet.aspx.vb:34:            PokazListe()
Sprzet.aspx.vb:35:            WyczyscFormularz()
Sprzet.aspx.vb:36:        End If
Sprzet.aspx.vb:37:    End Sub
Sprzet.aspx.vb:38:
Sprzet.aspx.vb:39:    Protected Sub ddlKategoria_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlKategoria.SelectedIndexChanged
Sprzet.aspx.vb:40:        gvSprzet.SelectedIndex = -1
Sprzet.aspx.vb:41:        PokazListe()
Sprzet.aspx.vb:42:        WyczyscFormularz()
Sprzet.aspx.vb:43:    End Sub
Sprzet.aspx.vb:44:
Sprzet.aspx.vb:45:    Protected Sub gvSprzet_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gvSprzet.SelectedIndexChanged
Sprzet.aspx.vb:46:        Dim sprzetId As Integer = CInt(gvSprzet.SelectedDataKey.Value)
Sprzet.aspx.vb:47:        Dim s As SprzetModel = New EquipmentManager().PobierzSprzet(sprzetId)
Sprzet.aspx.vb:48:        If s Is Nothing Then
Sprzet.aspx.vb:49:            PokazBlad("Sprzet nie istnieje (odswiez liste).")
Sprzet.aspx.vb:50:            WyczyscFormularz()
Sprzet.aspx.vb:51:            Return
Sprzet.aspx.vb:52:        End If
Sprzet.aspx.vb:53:        WypelnijFormularz(s)
Sprzet.aspx.vb:54:        PokazHistorie(s.Id)
Sprzet.aspx.vb:55:    End Sub
Sprzet.aspx.vb:56:
Sprzet.aspx.vb:57:    Protected Sub gvSprzet_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvSprzet.RowDataBound
Sprzet.aspx.vb:58:        If e.Row.RowType <> DataControlRowType.DataRow Then
Sprzet.aspx.vb:59:            Return
Sprzet.aspx.vb:60:        End If
Sprzet.aspx.vb:61:        Dim s As SprzetModel = CType(e.Row.DataItem, SprzetModel)
Sprzet.aspx.vb:62:        e.Row.Cells(6).Text = s.OpisStatusu
Sprzet.aspx.vb:63:        If s.Status = "S" Then
Sprzet.aspx.vb:64:            e.Row.CssClass = "wyszarzony"
Sprzet.aspx.vb:65:        End If
Sprzet.aspx.vb:66:        If s.Status <> "D" AndAlso s.Status <> "W" Then
Sprzet.aspx.vb:67:            e.Row.Cells(7).Text = ""
Sprzet.aspx.vb:68:        End If
Sprzet.aspx.vb:69:    End Sub
Sprzet.aspx.vb:70:
Sprzet.aspx.vb:71:    Protected Sub btnNowy_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNowy.Click
Sprzet.aspx.vb:72:        gvSprzet.SelectedIndex = -1
Sprzet.aspx.vb:73:        WyczyscFormularz()
Sprzet.aspx.vb:74:        txtNumer.Focus()
Sprzet.aspx.vb:75:    End Sub
Sprzet.aspx.vb:76:
Sprzet.aspx.vb:77:    Protected Sub btnZapisz_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZapisz.Click
Sprzet.aspx.vb:78:        Dim stawka As Decimal
Sprzet.aspx.vb:79:        Dim kaucja As Decimal
Sprzet.aspx.vb:80:        If Not Decimal.TryParse(txtStawka.Text.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, stawka) Then
Sprzet.aspx.vb:81:            PokazBlad("Niepoprawna stawka dobowa.")
Sprzet.aspx.vb:82:            Return
Sprzet.aspx.vb:83:        End If
Sprzet.aspx.vb:84:        If txtKaucja.Text.Trim() = "" Then
Sprzet.aspx.vb:85:            kaucja = 0D
Sprzet.aspx.vb:86:        ElseIf Not Decimal.TryParse(txtKaucja.Text.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, kaucja) Then
Sprzet.aspx.vb:87:            PokazBlad("Niepoprawna kaucja.")
Sprzet.aspx.vb:88:            Return
Sprzet.aspx.vb:89:        End If
Sprzet.aspx.vb:90:
Sprzet.aspx.vb:91:        Dim mgr As New EquipmentManager()
Sprzet.aspx.vb:92:        Dim s As SprzetModel
Sprzet.aspx.vb:93:        Dim sprzetId As Integer = CInt(hfSprzetId.Value)
Sprzet.aspx.vb:94:        If sprzetId > 0 Then
Sprzet.aspx.vb:95:            s = mgr.PobierzSprzet(sprzetId)
Sprzet.aspx.vb:96:            If s Is Nothing Then
Sprzet.aspx.vb:97:                PokazBlad("Sprzet nie istnieje.")
Sprzet.aspx.vb:98:                Return
Sprzet.aspx.vb:99:            End If
Sprzet.aspx.vb:100:            If s.Kategoria <> ddlKategoriaEdycja.SelectedValue AndAlso Not mgr.CzyMoznaZmienicKategorie(sprzetId) Then
Sprzet.aspx.vb:101:                PokazBlad("Nie mozna zmienic kategorii wypozyczonego sprzetu.")
Sprzet.aspx.vb:102:                Return
Sprzet.aspx.vb:103:            End If
Sprzet.aspx.vb:104:        Else
Sprzet.aspx.vb:105:            s = New SprzetModel()
Sprzet.aspx.vb:106:        End If
Sprzet.aspx.vb:107:        s.NumerInwentarzowy = txtNumer.Text
Sprzet.aspx.vb:108:        s.Nazwa = txtNazwa.Text.Trim()
Sprzet.aspx.vb:109:        s.Kategoria = ddlKategoriaEdycja.SelectedValue
Sprzet.aspx.vb:110:        s.StawkaDobowa = stawka
Sprzet.aspx.vb:111:        s.Kaucja = kaucja
Sprzet.aspx.vb:112:
Sprzet.aspx.vb:113:        Try
Sprzet.aspx.vb:114:            Dim zapisaneId As Integer = mgr.Zapisz(s)
Sprzet.aspx.vb:115:            hfSprzetId.Value = zapisaneId.ToString()
Sprzet.aspx.vb:116:            PokazInfo("Zapisano sprzet " & s.NumerInwentarzowy & ".")
Sprzet.aspx.vb:117:            PokazListe()
Sprzet.aspx.vb:118:            ZaznaczWiersz(zapisaneId)
Sprzet.aspx.vb:119:        Catch ex As BladBiznesowyException
Sprzet.aspx.vb:120:            PokazBlad(ex.Message)
Sprzet.aspx.vb:121:        Catch ex As Exception
Sprzet.aspx.vb:122:            Log.Error("Blad zapisu sprzetu " & s.NumerInwentarzowy, ex)
Sprzet.aspx.vb:123:            PokazBlad("Nie udalo sie zapisac sprzetu.")
Sprzet.aspx.vb:124:        End Try
Sprzet.aspx.vb:125:    End Sub
Sprzet.aspx.vb:126:
Sprzet.aspx.vb:127:    Protected Sub btnSerwis_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSerwis.Click
Sprzet.aspx.vb:128:        Dim sprzetId As Integer = CInt(hfSprzetId.Value)
Sprzet.aspx.vb:129:        If sprzetId = 0 Then
Sprzet.aspx.vb:130:            PokazBlad("Wybierz sprzet z listy.")
Sprzet.aspx.vb:131:            Return
Sprzet.aspx.vb:132:        End If
Sprzet.aspx.vb:133:        Try
Sprzet.aspx.vb:134:            Dim mgr As New EquipmentManager()
Sprzet.aspx.vb:135:            mgr.WyslijDoSerwisu(sprzetId)
Sprzet.aspx.vb:136:            lblStatus.Text = "serwis"
Sprzet.aspx.vb:137:            PokazInfo("Sprzet przekazany do serwisu.")
Sprzet.aspx.vb:138:            PokazListe()
Sprzet.aspx.vb:139:            ZaznaczWiersz(sprzetId)
Sprzet.aspx.vb:140:        Catch ex As BladBiznesowyException
Sprzet.aspx.vb:141:            PokazBlad(ex.Message)
Sprzet.aspx.vb:142:        End Try
Sprzet.aspx.vb:143:    End Sub
Sprzet.aspx.vb:144:
Sprzet.aspx.vb:145:    Private Sub PokazListe()
Sprzet.aspx.vb:146:        Dim lista As List(Of SprzetModel) = New EquipmentManager().ListaWKategorii(ddlKategoria.SelectedValue)
Sprzet.aspx.vb:147:        gvSprzet.DataSource = lista
Sprzet.aspx.vb:148:        gvSprzet.DataBind()
Sprzet.aspx.vb:149:    End Sub
Sprzet.aspx.vb:150:
Sprzet.aspx.vb:151:    Private Sub PokazHistorie(ByVal sprzetId As Integer)
Sprzet.aspx.vb:152:        Dim tabela As DataTable = New EquipmentManager().HistoriaWypozyczen(sprzetId)
Sprzet.aspx.vb:153:        gvHistoria.DataSource = tabela
Sprzet.aspx.vb:154:        gvHistoria.DataBind()
Sprzet.aspx.vb:155:        gvHistoria.Visible = True
Sprzet.aspx.vb:156:    End Sub
Sprzet.aspx.vb:157:
Sprzet.aspx.vb:158:    Private Sub WypelnijFormularz(ByVal s As SprzetModel)
Sprzet.aspx.vb:159:        hfSprzetId.Value = s.Id.ToString()
Sprzet.aspx.vb:160:        txtNumer.Text = s.NumerInwentarzowy
Sprzet.aspx.vb:161:        txtNazwa.Text = s.Nazwa
Sprzet.aspx.vb:162:        If ddlKategoriaEdycja.Items.FindByValue(s.Kategoria) IsNot Nothing Then
Sprzet.aspx.vb:163:            ddlKategoriaEdycja.SelectedValue = s.Kategoria
Sprzet.aspx.vb:164:        End If
Sprzet.aspx.vb:165:        txtStawka.Text = s.StawkaDobowa.ToString("0.00")
Sprzet.aspx.vb:166:        txtKaucja.Text = s.Kaucja.ToString("0.00")
Sprzet.aspx.vb:167:        lblStatus.Text = s.OpisStatusu
Sprzet.aspx.vb:168:        btnSerwis.Enabled = (s.Status = "D")
Sprzet.aspx.vb:169:        txtNumer.ReadOnly = True
Sprzet.aspx.vb:170:    End Sub
Sprzet.aspx.vb:171:
Sprzet.aspx.vb:172:    Private Sub WyczyscFormularz()
Sprzet.aspx.vb:173:        hfSprzetId.Value = "0"
Sprzet.aspx.vb:174:        txtNumer.Text = ""
Sprzet.aspx.vb:175:        txtNazwa.Text = ""
Sprzet.aspx.vb:176:        txtStawka.Text = ""
Sprzet.aspx.vb:177:        txtKaucja.Text = ""
Sprzet.aspx.vb:178:        lblStatus.Text = "-"
Sprzet.aspx.vb:179:        txtNumer.ReadOnly = False
Sprzet.aspx.vb:180:        btnSerwis.Enabled = False
Sprzet.aspx.vb:181:        If ddlKategoria.SelectedValue <> "" Then
Sprzet.aspx.vb:182:            ddlKategoriaEdycja.SelectedValue = ddlKategoria.SelectedValue
Sprzet.aspx.vb:183:        End If
Sprzet.aspx.vb:184:        gvHistoria.DataSource = Nothing
Sprzet.aspx.vb:185:        gvHistoria.DataBind()
Sprzet.aspx.vb:186:        gvHistoria.Visible = False
Sprzet.aspx.vb:187:    End Sub
Sprzet.aspx.vb:188:
Sprzet.aspx.vb:189:    Private Sub ZaznaczWiersz(ByVal sprzetId As Integer)
Sprzet.aspx.vb:190:        For i As Integer = 0 To gvSprzet.DataKeys.Count - 1
Sprzet.aspx.vb:191:            If CInt(gvSprzet.DataKeys(i).Value) = sprzetId Then
Sprzet.aspx.vb:192:                gvSprzet.SelectedIndex = i
Sprzet.aspx.vb:193:                Return
Sprzet.aspx.vb:194:            End If
Sprzet.aspx.vb:195:        Next
Sprzet.aspx.vb:196:        gvSprzet.SelectedIndex = -1
Sprzet.aspx.vb:197:    End Sub
Sprzet.aspx.vb:198:
Sprzet.aspx.vb:199:    Private Sub PokazBlad(ByVal tekst As String)
Sprzet.aspx.vb:200:        lblKomunikat.CssClass = "blad"
Sprzet.aspx.vb:201:        lblKomunikat.Text = Server.HtmlEncode(tekst)
Sprzet.aspx.vb:202:    End Sub
Sprzet.aspx.vb:203:
Sprzet.aspx.vb:204:    Private Sub PokazInfo(ByVal tekst As String)
Sprzet.aspx.vb:205:        lblKomunikat.CssClass = "info"
Sprzet.aspx.vb:206:        lblKomunikat.Text = Server.HtmlEncode(tekst)
Sprzet.aspx.vb:207:    End Sub
Sprzet.aspx.vb:208:
Sprzet.aspx.vb:209:End Class
Wypozyczenie.aspx.vb:1:' Modul: Wypozyczenie.aspx.vb | Autor: zespol IT | 2014-03-11
Wypozyczenie.aspx.vb:2:' Wydanie sprzetu klientowi (z rezerwacji albo od reki), opcjonalnie dostawa kurierem.
Wypozyczenie.aspx.vb:3:' 2015-06-22 MW: dostawa kurierem (KurierClient)
Wypozyczenie.aspx.vb:4:' 2018-02-09 PT: lista wydan i zwrotow z dnia na dole strony
Wypozyczenie.aspx.vb:5:
Wypozyczenie.aspx.vb:6:Imports System.Data
Wypozyczenie.aspx.vb:7:Imports System.Globalization
Wypozyczenie.aspx.vb:8:Imports Wypozyczalnia.Core
Wypozyczenie.aspx.vb:9:Imports Wypozyczalnia.Core.Model
Wypozyczenie.aspx.vb:10:
Wypozyczenie.aspx.vb:11:Partial Class Wypozyczenie
Wypozyczenie.aspx.vb:12:    Inherits System.Web.UI.Page
Wypozyczenie.aspx.vb:13:
Wypozyczenie.aspx.vb:14:    Protected WithEvents lblKlient As Global.System.Web.UI.WebControls.Label
Wypozyczenie.aspx.vb:15:    Protected WithEvents lblKomunikat As Global.System.Web.UI.WebControls.Label
Wypozyczenie.aspx.vb:16:    Protected WithEvents ddlRezerwacje As Global.System.Web.UI.WebControls.DropDownList
Wypozyczenie.aspx.vb:17:    Protected WithEvents ddlSprzet As Global.System.Web.UI.WebControls.DropDownList
Wypozyczenie.aspx.vb:18:    Protected WithEvents txtPlanowanyZwrot As Global.System.Web.UI.WebControls.TextBox
Wypozyczenie.aspx.vb:19:    Protected WithEvents rfvPlanowanyZwrot As Global.System.Web.UI.WebControls.RequiredFieldValidator
Wypozyczenie.aspx.vb:20:    Protected WithEvents lblKaucja As Global.System.Web.UI.WebControls.Label
Wypozyczenie.aspx.vb:21:    Protected WithEvents chkDostawa As Global.System.Web.UI.WebControls.CheckBox
Wypozyczenie.aspx.vb:22:    Protected WithEvents txtAdres As Global.System.Web.UI.WebControls.TextBox
Wypozyczenie.aspx.vb:23:    Protected WithEvents btnWydaj As Global.System.Web.UI.WebControls.Button
Wypozyczenie.aspx.vb:24:    Protected WithEvents lblNumerPrzesylki As Global.System.Web.UI.WebControls.Label
Wypozyczenie.aspx.vb:25:    Protected WithEvents gvWydaneDzis As Global.System.Web.UI.WebControls.GridView
Wypozyczenie.aspx.vb:26:
Wypozyczenie.aspx.vb:27:    Private Const FormatDaty As String = "yyyy-MM-dd HH:mm"
Wypozyczenie.aspx.vb:28:
Wypozyczenie.aspx.vb:29:    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
Wypozyczenie.aspx.vb:30:        If Not IsPostBack Then
Wypozyczenie.aspx.vb:31:            Dim klientId As Integer = Session("WybranyKlientId")
Wypozyczenie.aspx.vb:32:            If klientId = 0 Then
Wypozyczenie.aspx.vb:33:                lblKomunikat.Text = "Najpierw wybierz klienta na stronie Klienci."
Wypozyczenie.aspx.vb:34:                btnWydaj.Enabled = False
Wypozyczenie.aspx.vb:35:            Else
Wypozyczenie.aspx.vb:36:                PokazKlienta(klientId)
Wypozyczenie.aspx.vb:37:                WczytajRezerwacje(klientId)
Wypozyczenie.aspx.vb:38:            End If
Wypozyczenie.aspx.vb:39:            WczytajSprzet()
Wypozyczenie.aspx.vb:40:            WczytajRuchZDnia()
Wypozyczenie.aspx.vb:41:            Dim zAdresu As String = Request.QueryString("rezerwacja")
Wypozyczenie.aspx.vb:42:            If Not String.IsNullOrEmpty(zAdresu) AndAlso ddlRezerwacje.Items.FindByValue(zAdresu) IsNot Nothing Then
Wypozyczenie.aspx.vb:43:                ddlRezerwacje.SelectedValue = zAdresu
Wypozyczenie.aspx.vb:44:                ddlRezerwacje_SelectedIndexChanged(ddlRezerwacje, EventArgs.Empty)
Wypozyczenie.aspx.vb:45:            End If
Wypozyczenie.aspx.vb:46:        End If
Wypozyczenie.aspx.vb:47:    End Sub
Wypozyczenie.aspx.vb:48:
Wypozyczenie.aspx.vb:49:    Protected Sub gvWydaneDzis_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvWydaneDzis.RowDataBound
Wypozyczenie.aspx.vb:50:        If e.Row.RowType <> DataControlRowType.DataRow Then Return
Wypozyczenie.aspx.vb:51:        Dim wiersz As DataRowView = CType(e.Row.DataItem, DataRowView)
Wypozyczenie.aspx.vb:52:        If Not IsDBNull(wiersz("DataZwrotu")) Then
Wypozyczenie.aspx.vb:53:            e.Row.ForeColor = Drawing.Color.Gray
Wypozyczenie.aspx.vb:54:            e.Row.ToolTip = "Zwrocono " & CDate(wiersz("DataZwrotu")).ToString(FormatDaty)
Wypozyczenie.aspx.vb:55:        ElseIf CDate(wiersz("PlanowanyZwrot")) < Date.Now Then
Wypozyczenie.aspx.vb:56:            e.Row.ForeColor = Drawing.Color.DarkRed
Wypozyczenie.aspx.vb:57:            e.Row.ToolTip = "Po terminie zwrotu"
Wypozyczenie.aspx.vb:58:        End If
Wypozyczenie.aspx.vb:59:    End Sub
Wypozyczenie.aspx.vb:60:
Wypozyczenie.aspx.vb:61:    Private Sub PokazKlienta(ByVal klientId As Integer)
Wypozyczenie.aspx.vb:62:        Dim klient As Klient = New CustomerManager().PobierzKlienta(klientId)
Wypozyczenie.aspx.vb:63:        If klient Is Nothing Then
Wypozyczenie.aspx.vb:64:            lblKlient.Text = "(nie znaleziono klienta " & klientId & ")"
Wypozyczenie.aspx.vb:65:            btnWydaj.Enabled = False
Wypozyczenie.aspx.vb:66:            Return
Wypozyczenie.aspx.vb:67:        End If
Wypozyczenie.aspx.vb:68:        If klient.Typ = "F" AndAlso Not String.IsNullOrEmpty(klient.NazwaFirmy) Then
Wypozyczenie.aspx.vb:69:            lblKlient.Text = Server.HtmlEncode(klient.NazwaFirmy & " (" & klient.Imie & " " & klient.Nazwisko & ")")
Wypozyczenie.aspx.vb:70:        Else
Wypozyczenie.aspx.vb:71:            lblKlient.Text = Server.HtmlEncode(klient.Imie & " " & klient.Nazwisko)
Wypozyczenie.aspx.vb:72:        End If
Wypozyczenie.aspx.vb:73:        If klient.Zablokowany Then
Wypozyczenie.aspx.vb:74:            lblKomunikat.Text = "Klient zablokowany: " & Server.HtmlEncode(klient.PowodBlokady)
Wypozyczenie.aspx.vb:75:            btnWydaj.Enabled = False
Wypozyczenie.aspx.vb:76:            Return
Wypozyczenie.aspx.vb:77:        End If
Wypozyczenie.aspx.vb:78:        Dim otwarte As List(Of Model.Wypozyczenie) = New RentalManager().OtwarteKlienta(klientId)
Wypozyczenie.aspx.vb:79:        If otwarte.Count > 0 Then
Wypozyczenie.aspx.vb:80:            Dim poTerminie As Integer = 0
Wypozyczenie.aspx.vb:81:            For Each w As Model.Wypozyczenie In otwarte
Wypozyczenie.aspx.vb:82:                If w.CzyPoTerminie(Date.Now) Then poTerminie = poTerminie + 1
Wypozyczenie.aspx.vb:83:            Next
Wypozyczenie.aspx.vb:84:            lblKlient.Text = lblKlient.Text & " - otwarte wypozyczenia: " & otwarte.Count
Wypozyczenie.aspx.vb:85:            If poTerminie > 0 Then
Wypozyczenie.aspx.vb:86:                lblKomunikat.Text = "Uwaga: klient ma " & poTerminie & " wypozyczen po terminie zwrotu."
Wypozyczenie.aspx.vb:87:            End If
Wypozyczenie.aspx.vb:88:        End If
Wypozyczenie.aspx.vb:89:    End Sub
Wypozyczenie.aspx.vb:90:
Wypozyczenie.aspx.vb:91:    Private Sub WczytajRezerwacje(ByVal klientId As Integer)
Wypozyczenie.aspx.vb:92:        ddlRezerwacje.Items.Clear()
Wypozyczenie.aspx.vb:93:        ddlRezerwacje.Items.Add(New ListItem("-- bez rezerwacji --", "0"))
Wypozyczenie.aspx.vb:94:        Dim tabela As DataTable = New ReservationManager().RezerwacjeKlienta(klientId)
Wypozyczenie.aspx.vb:95:        For Each wiersz As DataRow In tabela.Rows
Wypozyczenie.aspx.vb:96:            Dim status As String = wiersz("Status").ToString()
Wypozyczenie.aspx.vb:97:            If status = "N" OrElse status = "P" Then
Wypozyczenie.aspx.vb:98:                Dim tekst As String = String.Format("#{0} {1} | {2:yyyy-MM-dd HH:mm} - {3:yyyy-MM-dd HH:mm} | {4}",
Wypozyczenie.aspx.vb:99:                    wiersz("Id"), wiersz("Sprzet"), wiersz("DataOd"), wiersz("DataDo"), status)
Wypozyczenie.aspx.vb:100:                ddlRezerwacje.Items.Add(New ListItem(tekst, wiersz("Id").ToString()))
Wypozyczenie.aspx.vb:101:            End If
Wypozyczenie.aspx.vb:102:        Next
Wypozyczenie.aspx.vb:103:    End Sub
Wypozyczenie.aspx.vb:104:
Wypozyczenie.aspx.vb:105:    Private Sub WczytajSprzet()
Wypozyczenie.aspx.vb:106:        ddlSprzet.Items.Clear()
Wypozyczenie.aspx.vb:107:        ddlSprzet.Items.Add(New ListItem("-- wybierz --", "0"))
Wypozyczenie.aspx.vb:108:        Dim lista As List(Of Sprzet) = New EquipmentManager().ListaWKategorii("")
Wypozyczenie.aspx.vb:109:        For Each s As Sprzet In lista
Wypozyczenie.aspx.vb:110:            If s.Status = "D" Then
Wypozyczenie.aspx.vb:111:                ddlSprzet.Items.Add(New ListItem(s.NumerInwentarzowy & " " & s.Nazwa, s.Id.ToString()))
Wypozyczenie.aspx.vb:112:            End If
Wypozyczenie.aspx.vb:113:        Next
Wypozyczenie.aspx.vb:114:    End Sub
Wypozyczenie.aspx.vb:115:
Wypozyczenie.aspx.vb:116:    Private Sub WczytajRuchZDnia()
Wypozyczenie.aspx.vb:117:        gvWydaneDzis.DataSource = New RentalManager().WypozyczeniaZDnia(Date.Today)
Wypozyczenie.aspx.vb:118:        gvWydaneDzis.DataBind()
Wypozyczenie.aspx.vb:119:    End Sub
Wypozyczenie.aspx.vb:120:
Wypozyczenie.aspx.vb:121:    Protected Sub ddlRezerwacje_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlRezerwacje.SelectedIndexChanged
Wypozyczenie.aspx.vb:122:        lblNumerPrzesylki.Text = "-"
Wypozyczenie.aspx.vb:123:        Dim rezerwacjaId As Integer = CInt(ddlRezerwacje.SelectedValue)
Wypozyczenie.aspx.vb:124:        If rezerwacjaId = 0 Then
Wypozyczenie.aspx.vb:125:            ddlSprzet.Enabled = True
Wypozyczenie.aspx.vb:126:            txtPlanowanyZwrot.Text = ""
Wypozyczenie.aspx.vb:127:            lblKaucja.Text = "-"
Wypozyczenie.aspx.vb:128:            Return
Wypozyczenie.aspx.vb:129:        End If
Wypozyczenie.aspx.vb:130:        Dim r As Rezerwacja = New ReservationManager().PobierzRezerwacje(rezerwacjaId)
Wypozyczenie.aspx.vb:131:        If r Is Nothing Then
Wypozyczenie.aspx.vb:132:            lblKomunikat.Text = "Nie znaleziono rezerwacji " & rezerwacjaId
Wypozyczenie.aspx.vb:133:            Return
Wypozyczenie.aspx.vb:134:        End If
Wypozyczenie.aspx.vb:135:        ddlSprzet.SelectedIndex = 0
Wypozyczenie.aspx.vb:136:        ddlSprzet.Enabled = False
Wypozyczenie.aspx.vb:137:        txtPlanowanyZwrot.Text = r.DataDo.ToString(FormatDaty)
Wypozyczenie.aspx.vb:138:        PokazKaucje(r.SprzetId)
Wypozyczenie.aspx.vb:139:    End Sub
Wypozyczenie.aspx.vb:140:
Wypozyczenie.aspx.vb:141:    Protected Sub ddlSprzet_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlSprzet.SelectedIndexChanged
Wypozyczenie.aspx.vb:142:        Dim sprzetId As Integer = CInt(ddlSprzet.SelectedValue)
Wypozyczenie.aspx.vb:143:        If sprzetId = 0 Then
Wypozyczenie.aspx.vb:144:            lblKaucja.Text = "-"
Wypozyczenie.aspx.vb:145:        Else
Wypozyczenie.aspx.vb:146:            PokazKaucje(sprzetId)
Wypozyczenie.aspx.vb:147:            If txtPlanowanyZwrot.Text = "" Then
Wypozyczenie.aspx.vb:148:                txtPlanowanyZwrot.Text = Date.Now.AddDays(1).ToString(FormatDaty)
Wypozyczenie.aspx.vb:149:            End If
Wypozyczenie.aspx.vb:150:        End If
Wypozyczenie.aspx.vb:151:    End Sub
Wypozyczenie.aspx.vb:152:
Wypozyczenie.aspx.vb:153:    Private Sub PokazKaucje(ByVal sprzetId As Integer)
Wypozyczenie.aspx.vb:154:        Dim s As Sprzet = New EquipmentManager().PobierzSprzet(sprzetId)
Wypozyczenie.aspx.vb:155:        If s Is Nothing Then
Wypozyczenie.aspx.vb:156:            lblKaucja.Text = "-"
Wypozyczenie.aspx.vb:157:        Else
Wypozyczenie.aspx.vb:158:            lblKaucja.Text = s.Kaucja.ToString("N2")
Wypozyczenie.aspx.vb:159:        End If
Wypozyczenie.aspx.vb:160:    End Sub
Wypozyczenie.aspx.vb:161:
Wypozyczenie.aspx.vb:162:    Protected Sub chkDostawa_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkDostawa.CheckedChanged
Wypozyczenie.aspx.vb:163:        txtAdres.Enabled = chkDostawa.Checked
Wypozyczenie.aspx.vb:164:        If Not chkDostawa.Checked Then
Wypozyczenie.aspx.vb:165:            txtAdres.Text = ""
Wypozyczenie.aspx.vb:166:        End If
Wypozyczenie.aspx.vb:167:    End Sub
Wypozyczenie.aspx.vb:168:
Wypozyczenie.aspx.vb:169:    Protected Sub btnWydaj_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnWydaj.Click
Wypozyczenie.aspx.vb:170:        Dim klientId As Integer = Session("WybranyKlientId")
Wypozyczenie.aspx.vb:171:        If klientId = 0 Then
Wypozyczenie.aspx.vb:172:            lblKomunikat.Text = "Sesja wygasla - wybierz klienta ponownie."
Wypozyczenie.aspx.vb:173:            Return
Wypozyczenie.aspx.vb:174:        End If
Wypozyczenie.aspx.vb:175:
Wypozyczenie.aspx.vb:176:        Dim rezerwacjaId As Integer = CInt(ddlRezerwacje.SelectedValue)
Wypozyczenie.aspx.vb:177:        Dim sprzetId As Integer
Wypozyczenie.aspx.vb:178:        If rezerwacjaId > 0 Then
Wypozyczenie.aspx.vb:179:            Dim r As Rezerwacja = New ReservationManager().PobierzRezerwacje(rezerwacjaId)
Wypozyczenie.aspx.vb:180:            If r Is Nothing OrElse r.KlientId <> klientId Then
Wypozyczenie.aspx.vb:181:                lblKomunikat.Text = "Rezerwacja nie nalezy do wybranego klienta."
Wypozyczenie.aspx.vb:182:                Return
Wypozyczenie.aspx.vb:183:            End If
Wypozyczenie.aspx.vb:184:            sprzetId = r.SprzetId
Wypozyczenie.aspx.vb:185:        Else
Wypozyczenie.aspx.vb:186:            sprzetId = CInt(ddlSprzet.SelectedValue)
Wypozyczenie.aspx.vb:187:        End If
Wypozyczenie.aspx.vb:188:        If sprzetId = 0 Then
Wypozyczenie.aspx.vb:189:            lblKomunikat.Text = "Wybierz rezerwacje albo sprzet."
Wypozyczenie.aspx.vb:190:            Return
Wypozyczenie.aspx.vb:191:        End If
Wypozyczenie.aspx.vb:192:
Wypozyczenie.aspx.vb:193:        Dim planowanyZwrot As Date
Wypozyczenie.aspx.vb:194:        If Not Date.TryParseExact(txtPlanowanyZwrot.Text.Trim(), FormatDaty, CultureInfo.InvariantCulture, DateTimeStyles.None, planowanyZwrot) Then
Wypozyczenie.aspx.vb:195:            lblKomunikat.Text = "Niepoprawny format daty zwrotu (rrrr-mm-dd gg:mm)."
Wypozyczenie.aspx.vb:196:            Return
Wypozyczenie.aspx.vb:197:        End If
Wypozyczenie.aspx.vb:198:        If planowanyZwrot <= Date.Now Then
Wypozyczenie.aspx.vb:199:            lblKomunikat.Text = "Planowany zwrot musi byc pozniejszy niz chwila wydania."
Wypozyczenie.aspx.vb:200:            Return
Wypozyczenie.aspx.vb:201:        End If
Wypozyczenie.aspx.vb:202:
Wypozyczenie.aspx.vb:203:        Dim adres As String = ""
Wypozyczenie.aspx.vb:204:        If chkDostawa.Checked Then
Wypozyczenie.aspx.vb:205:            adres = txtAdres.Text.Trim()
Wypozyczenie.aspx.vb:206:            If adres = "" Then
Wypozyczenie.aspx.vb:207:                lblKomunikat.Text = "Podaj adres dostawy albo odznacz dostawe."
Wypozyczenie.aspx.vb:208:                Return
Wypozyczenie.aspx.vb:209:            End If
Wypozyczenie.aspx.vb:210:        End If
Wypozyczenie.aspx.vb:211:
Wypozyczenie.aspx.vb:212:        Try
Wypozyczenie.aspx.vb:213:            Dim manager As New RentalManager()
Wypozyczenie.aspx.vb:214:            Dim noweId As Integer = manager.WydajSprzet(rezerwacjaId, klientId, sprzetId, planowanyZwrot, adres)
Wypozyczenie.aspx.vb:215:            Dim w As Model.Wypozyczenie = manager.PobierzWypozyczenie(noweId)
Wypozyczenie.aspx.vb:216:            lblKaucja.Text = w.KaucjaPobrana.ToString("N2")
Wypozyczenie.aspx.vb:217:            If chkDostawa.Checked Then
Wypozyczenie.aspx.vb:218:                If String.IsNullOrEmpty(w.NumerPrzesylki) Then
Wypozyczenie.aspx.vb:219:                    lblNumerPrzesylki.Text = "brak - zamow kuriera telefonicznie"
Wypozyczenie.aspx.vb:220:                Else
Wypozyczenie.aspx.vb:221:                    lblNumerPrzesylki.Text = w.NumerPrzesylki
Wypozyczenie.aspx.vb:222:                End If
Wypozyczenie.aspx.vb:223:            Else
Wypozyczenie.aspx.vb:224:                lblNumerPrzesylki.Text = "odbior osobisty"
Wypozyczenie.aspx.vb:225:            End If
Wypozyczenie.aspx.vb:226:            lblKomunikat.ForeColor = Drawing.Color.Green
Wypozyczenie.aspx.vb:227:            lblKomunikat.Text = "Wydano sprzet, wypozyczenie nr " & noweId & "."
Wypozyczenie.aspx.vb:228:            WczytajRezerwacje(klientId)
Wypozyczenie.aspx.vb:229:            WczytajSprzet()
Wypozyczenie.aspx.vb:230:            WczytajRuchZDnia()
Wypozyczenie.aspx.vb:231:            ddlSprzet.Enabled = True
Wypozyczenie.aspx.vb:232:            txtPlanowanyZwrot.Text = ""
Wypozyczenie.aspx.vb:233:            chkDostawa.Checked = False
Wypozyczenie.aspx.vb:234:            txtAdres.Text = ""
Wypozyczenie.aspx.vb:235:            txtAdres.Enabled = False
Wypozyczenie.aspx.vb:236:        Catch ex As BladBiznesowyException
Wypozyczenie.aspx.vb:237:            lblKomunikat.Text = ex.Message
Wypozyczenie.aspx.vb:238:        Catch ex As Exception
Wypozyczenie.aspx.vb:239:            lblKomunikat.Text = "Blad podczas wydania: " & ex.Message
Wypozyczenie.aspx.vb:240:        End Try
Wypozyczenie.aspx.vb:241:    End Sub
Wypozyczenie.aspx.vb:242:
Wypozyczenie.aspx.vb:243:End Class
Zwrot.aspx.vb:1:Option Strict Off
Zwrot.aspx.vb:2:
Zwrot.aspx.vb:3:' Modul: Zwrot.aspx.vb | Autor: zespol IT | 2014-03-11
Zwrot.aspx.vb:4:' Przyjecie zwrotu sprzetu: kara za spoznienie, szkody, rozliczenie kaucji.
Zwrot.aspx.vb:5:' 2014-09-30 JK: szkody z listy zamiast pola tekstowego
Zwrot.aspx.vb:6:' 2017-05-16 MW: link do faktury po zwrocie
Zwrot.aspx.vb:7:
Zwrot.aspx.vb:8:Imports System.Data
Zwrot.aspx.vb:9:Imports System.Globalization
Zwrot.aspx.vb:10:Imports Wypozyczalnia.Core
Zwrot.aspx.vb:11:Imports Wypozyczalnia.Core.Model
Zwrot.aspx.vb:12:
Zwrot.aspx.vb:13:Partial Class Zwrot
Zwrot.aspx.vb:14:    Inherits System.Web.UI.Page
Zwrot.aspx.vb:15:
Zwrot.aspx.vb:16:    Protected WithEvents lblKomunikat As Global.System.Web.UI.WebControls.Label
Zwrot.aspx.vb:17:    Protected WithEvents ddlOtwarte As Global.System.Web.UI.WebControls.DropDownList
Zwrot.aspx.vb:18:    Protected WithEvents lblSzczegoly As Global.System.Web.UI.WebControls.Label
Zwrot.aspx.vb:19:    Protected WithEvents txtDataZwrotu As Global.System.Web.UI.WebControls.TextBox
Zwrot.aspx.vb:20:    Protected WithEvents cblSzkody As Global.System.Web.UI.WebControls.CheckBoxList
Zwrot.aspx.vb:21:    Protected WithEvents btnPodglad As Global.System.Web.UI.WebControls.Button
Zwrot.aspx.vb:22:    Protected WithEvents btnZwrot As Global.System.Web.UI.WebControls.Button
Zwrot.aspx.vb:23:    Protected WithEvents lblOplata As Global.System.Web.UI.WebControls.Label
Zwrot.aspx.vb:24:    Protected WithEvents lblKara As Global.System.Web.UI.WebControls.Label
Zwrot.aspx.vb:25:    Protected WithEvents lblKaucjaZwrot As Global.System.Web.UI.WebControls.Label
Zwrot.aspx.vb:26:    Protected WithEvents lnkFaktura As Global.System.Web.UI.WebControls.HyperLink
Zwrot.aspx.vb:27:    Protected WithEvents gvOtwarte As Global.System.Web.UI.WebControls.GridView
Zwrot.aspx.vb:28:
Zwrot.aspx.vb:29:    Private Const FormatDaty As String = "yyyy-MM-dd HH:mm"
Zwrot.aspx.vb:30:
Zwrot.aspx.vb:31:    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
Zwrot.aspx.vb:32:        If Not IsPostBack Then
Zwrot.aspx.vb:33:            WypelnijSzkody()
Zwrot.aspx.vb:34:            WczytajOtwarte()
Zwrot.aspx.vb:35:            WyczyscRozliczenie()
Zwrot.aspx.vb:36:            Dim zAdresu As String = Request.QueryString("id")
Zwrot.aspx.vb:37:            If Not String.IsNullOrEmpty(zAdresu) AndAlso ddlOtwarte.Items.FindByValue(zAdresu) IsNot Nothing Then
Zwrot.aspx.vb:38:                ddlOtwarte.SelectedValue = zAdresu
Zwrot.aspx.vb:39:                ddlOtwarte_SelectedIndexChanged(ddlOtwarte, EventArgs.Empty)
Zwrot.aspx.vb:40:            End If
Zwrot.aspx.vb:41:        End If
Zwrot.aspx.vb:42:    End Sub
Zwrot.aspx.vb:43:
Zwrot.aspx.vb:44:    Protected Sub gvOtwarte_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvOtwarte.RowDataBound
Zwrot.aspx.vb:45:        If e.Row.RowType <> DataControlRowType.DataRow Then Return
Zwrot.aspx.vb:46:        Dim wiersz As DataRowView = e.Row.DataItem
Zwrot.aspx.vb:47:        Dim plan As Date = wiersz("PlanowanyZwrot")
Zwrot.aspx.vb:48:        If plan < Date.Now.AddHours(-1) Then
Zwrot.aspx.vb:49:            e.Row.BackColor = Drawing.Color.MistyRose
Zwrot.aspx.vb:50:            e.Row.ToolTip = "Po terminie zwrotu"
Zwrot.aspx.vb:51:        ElseIf plan < Date.Now.AddHours(24) Then
Zwrot.aspx.vb:52:            e.Row.BackColor = Drawing.Color.LightYellow
Zwrot.aspx.vb:53:            e.Row.ToolTip = "Zwrot w ciagu doby"
Zwrot.aspx.vb:54:        End If
Zwrot.aspx.vb:55:    End Sub
Zwrot.aspx.vb:56:
Zwrot.aspx.vb:57:    Protected Sub gvOtwarte_SelectedIndexChanging(ByVal sender As Object, ByVal e As GridViewSelectEventArgs) Handles gvOtwarte.SelectedIndexChanging
Zwrot.aspx.vb:58:        Dim id As String = gvOtwarte.Rows(e.NewSelectedIndex).Cells(0).Text
Zwrot.aspx.vb:59:        If ddlOtwarte.Items.FindByValue(id) IsNot Nothing Then
Zwrot.aspx.vb:60:            ddlOtwarte.SelectedValue = id
Zwrot.aspx.vb:61:            ddlOtwarte_SelectedIndexChanged(ddlOtwarte, EventArgs.Empty)
Zwrot.aspx.vb:62:        End If
Zwrot.aspx.vb:63:    End Sub
Zwrot.aspx.vb:64:
Zwrot.aspx.vb:65:    Private Sub WypelnijSzkody()
Zwrot.aspx.vb:66:        cblSzkody.Items.Clear()
Zwrot.aspx.vb:67:        cblSzkody.Items.Add(New ListItem("Uszkodzona obudowa (50 zł)", "Uszkodzona obudowa"))
Zwrot.aspx.vb:68:        cblSzkody.Items.Add(New ListItem("Brak akcesoriów (30 zł)", "Brak akcesoriów"))
Zwrot.aspx.vb:69:        cblSzkody.Items.Add(New ListItem("Zabrudzenie (20 zł)", "Zabrudzenie"))
Zwrot.aspx.vb:70:    End Sub
Zwrot.aspx.vb:71:
Zwrot.aspx.vb:72:    Private Sub WczytajOtwarte()
Zwrot.aspx.vb:73:        Dim tabela As DataTable = New RentalManager().Otwarte()
Zwrot.aspx.vb:74:        ddlOtwarte.Items.Clear()
Zwrot.aspx.vb:75:        ddlOtwarte.Items.Add(New ListItem("-- wybierz --", "0"))
Zwrot.aspx.vb:76:        For Each wiersz As DataRow In tabela.Rows
Zwrot.aspx.vb:77:            Dim plan As Date = wiersz("PlanowanyZwrot")
Zwrot.aspx.vb:78:            Dim tekst As String = "#" & wiersz("Id") & " " & wiersz("Klient") & " | " & wiersz("Sprzet") & " | do " & plan.ToString(FormatDaty)
Zwrot.aspx.vb:79:            If plan < Date.Now Then
Zwrot.aspx.vb:80:                tekst = tekst & " (po terminie)"
Zwrot.aspx.vb:81:            End If
Zwrot.aspx.vb:82:            ddlOtwarte.Items.Add(New ListItem(tekst, wiersz("Id").ToString()))
Zwrot.aspx.vb:83:        Next
Zwrot.aspx.vb:84:        gvOtwarte.DataSource = tabela
Zwrot.aspx.vb:85:        gvOtwarte.DataBind()
Zwrot.aspx.vb:86:    End Sub
Zwrot.aspx.vb:87:
Zwrot.aspx.vb:88:    Private Sub WyczyscRozliczenie()
Zwrot.aspx.vb:89:        lblOplata.Text = "-"
Zwrot.aspx.vb:90:        lblKara.Text = "-"
Zwrot.aspx.vb:91:        lblKaucjaZwrot.Text = "-"
Zwrot.aspx.vb:92:        lnkFaktura.Visible = False
Zwrot.aspx.vb:93:    End Sub
Zwrot.aspx.vb:94:
Zwrot.aspx.vb:95:    Protected Sub ddlOtwarte_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlOtwarte.SelectedIndexChanged
Zwrot.aspx.vb:96:        WyczyscRozliczenie()
Zwrot.aspx.vb:97:        For Each item As ListItem In cblSzkody.Items
Zwrot.aspx.vb:98:            item.Selected = False
Zwrot.aspx.vb:99:        Next
Zwrot.aspx.vb:100:        Dim id As Integer = ddlOtwarte.SelectedValue
Zwrot.aspx.vb:101:        If id = 0 Then
Zwrot.aspx.vb:102:            lblSzczegoly.Text = "-"
Zwrot.aspx.vb:103:            Return
Zwrot.aspx.vb:104:        End If
Zwrot.aspx.vb:105:        Dim w As Wypozyczenie = New RentalManager().PobierzWypozyczenie(id)
Zwrot.aspx.vb:106:        If w Is Nothing Then
Zwrot.aspx.vb:107:            lblSzczegoly.Text = "-"
Zwrot.aspx.vb:108:            lblKomunikat.Text = "Nie znaleziono wypozyczenia " & id
Zwrot.aspx.vb:109:            Return
Zwrot.aspx.vb:110:        End If
Zwrot.aspx.vb:111:        Dim s As Sprzet = New EquipmentManager().PobierzSprzet(w.SprzetId)
Zwrot.aspx.vb:112:        Dim opis As String = "Wydano " & w.DataWydania.ToString(FormatDaty) & ", zwrot do " & w.PlanowanyZwrot.ToString(FormatDaty) &
Zwrot.aspx.vb:113:            ", kaucja " & w.KaucjaPobrana.ToString("N2") & " zł"
Zwrot.aspx.vb:114:        If s IsNot Nothing Then
Zwrot.aspx.vb:115:            opis = s.NumerInwentarzowy & " " & s.Nazwa & " (" & s.StawkaDobowa.ToString("N2") & " zł/doba). " & opis
Zwrot.aspx.vb:116:        End If
Zwrot.aspx.vb:117:        If Not String.IsNullOrEmpty(w.NumerPrzesylki) Then
Zwrot.aspx.vb:118:            opis = opis & ", przesylka " & w.NumerPrzesylki
Zwrot.aspx.vb:119:        End If
Zwrot.aspx.vb:120:        lblSzczegoly.Text = Server.HtmlEncode(opis)
Zwrot.aspx.vb:121:        If txtDataZwrotu.Text = "" Then
Zwrot.aspx.vb:122:            txtDataZwrotu.Text = Date.Now.ToString(FormatDaty)
Zwrot.aspx.vb:123:        End If
Zwrot.aspx.vb:124:    End Sub
Zwrot.aspx.vb:125:
Zwrot.aspx.vb:126:    Private Function OpisSpoznienia(ByVal plan As Date, ByVal zwrot As Date) As String
Zwrot.aspx.vb:127:        Dim minuty As Long = DateDiff(DateInterval.Minute, plan, zwrot)
Zwrot.aspx.vb:128:        If minuty <= 0 Then
Zwrot.aspx.vb:129:            Return "zwrot w terminie"
Zwrot.aspx.vb:130:        End If
Zwrot.aspx.vb:131:        If minuty <= 60 Then
Zwrot.aspx.vb:132:            Return "spoznienie " & minuty & " min (w tolerancji)"
Zwrot.aspx.vb:133:        End If
Zwrot.aspx.vb:134:        Dim godziny As Long = minuty \ 60
Zwrot.aspx.vb:135:        Dim reszta As Long = minuty Mod 60
Zwrot.aspx.vb:136:        If godziny < 24 Then
Zwrot.aspx.vb:137:            Return "spoznienie " & godziny & " h " & reszta & " min"
Zwrot.aspx.vb:138:        End If
Zwrot.aspx.vb:139:        Return "spoznienie " & (godziny \ 24) & " d " & (godziny Mod 24) & " h " & reszta & " min"
Zwrot.aspx.vb:140:    End Function
Zwrot.aspx.vb:141:
Zwrot.aspx.vb:142:    Private Function OdczytajDateZwrotu(ByRef dataZwrotu As Date) As Boolean
Zwrot.aspx.vb:143:        Dim tekst As String = txtDataZwrotu.Text.Trim()
Zwrot.aspx.vb:144:        If tekst = "" Then
Zwrot.aspx.vb:145:            dataZwrotu = Date.Now
Zwrot.aspx.vb:146:            Return True
Zwrot.aspx.vb:147:        End If
Zwrot.aspx.vb:148:        If Date.TryParseExact(tekst, FormatDaty, CultureInfo.InvariantCulture, DateTimeStyles.None, dataZwrotu) Then
Zwrot.aspx.vb:149:            Return True
Zwrot.aspx.vb:150:        End If
Zwrot.aspx.vb:151:        Return Date.TryParse(tekst, dataZwrotu)
Zwrot.aspx.vb:152:    End Function
Zwrot.aspx.vb:153:
Zwrot.aspx.vb:154:    Protected Sub btnPodglad_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPodglad.Click
Zwrot.aspx.vb:155:        Dim id As Integer = ddlOtwarte.SelectedValue
Zwrot.aspx.vb:156:        If id = 0 Then
Zwrot.aspx.vb:157:            lblKomunikat.Text = "Wybierz wypozyczenie."
Zwrot.aspx.vb:158:            Return
Zwrot.aspx.vb:159:        End If
Zwrot.aspx.vb:160:        Dim dataZwrotu As Date
Zwrot.aspx.vb:161:        If Not OdczytajDateZwrotu(dataZwrotu) Then
Zwrot.aspx.vb:162:            lblKomunikat.Text = "Niepoprawna data zwrotu."
Zwrot.aspx.vb:163:            Return
Zwrot.aspx.vb:164:        End If
Zwrot.aspx.vb:165:        Dim manager As New RentalManager()
Zwrot.aspx.vb:166:        Dim w As Wypozyczenie = manager.PobierzWypozyczenie(id)
Zwrot.aspx.vb:167:        If w Is Nothing Then
Zwrot.aspx.vb:168:            lblKomunikat.Text = "Nie znaleziono wypozyczenia " & id
Zwrot.aspx.vb:169:            Return
Zwrot.aspx.vb:170:        End If
Zwrot.aspx.vb:171:        Dim s As Sprzet = New EquipmentManager().PobierzSprzet(w.SprzetId)
Zwrot.aspx.vb:172:        Dim kara As Decimal = manager.ObliczKare(w.PlanowanyZwrot, dataZwrotu, s.StawkaDobowa)
Zwrot.aspx.vb:173:        Dim szkody As Decimal = 0
Zwrot.aspx.vb:174:        For Each item As ListItem In cblSzkody.Items
Zwrot.aspx.vb:175:            If item.Selected Then
Zwrot.aspx.vb:176:                Select Case item.Value
Zwrot.aspx.vb:177:                    Case "Uszkodzona obudowa"
Zwrot.aspx.vb:178:                        szkody = szkody + 50
Zwrot.aspx.vb:179:                    Case "Brak akcesoriów"
Zwrot.aspx.vb:180:                        szkody = szkody + 30
Zwrot.aspx.vb:181:                    Case "Zabrudzenie"
Zwrot.aspx.vb:182:                        szkody = szkody + 20
Zwrot.aspx.vb:183:                End Select
Zwrot.aspx.vb:184:            End If
Zwrot.aspx.vb:185:        Next
Zwrot.aspx.vb:186:        Dim kaucja As Decimal = w.KaucjaPobrana - kara - szkody
Zwrot.aspx.vb:187:        If kaucja < 0 Then kaucja = 0
Zwrot.aspx.vb:188:        lblOplata.Text = "(przy zwrocie)"
Zwrot.aspx.vb:189:        lblKara.Text = kara
Zwrot.aspx.vb:190:        lblKaucjaZwrot.Text = kaucja
Zwrot.aspx.vb:191:        lblKomunikat.ForeColor = Drawing.Color.Navy
Zwrot.aspx.vb:192:        lblKomunikat.Text = "Podglad (" & OpisSpoznienia(w.PlanowanyZwrot, dataZwrotu) & ") - zwrot nie zostal jeszcze zapisany."
Zwrot.aspx.vb:193:    End Sub
Zwrot.aspx.vb:194:
Zwrot.aspx.vb:195:    Protected Sub btnZwrot_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZwrot.Click
Zwrot.aspx.vb:196:        Dim id As Integer = ddlOtwarte.SelectedValue
Zwrot.aspx.vb:197:        If id = 0 Then
Zwrot.aspx.vb:198:            lblKomunikat.Text = "Wybierz wypozyczenie."
Zwrot.aspx.vb:199:            Return
Zwrot.aspx.vb:200:        End If
Zwrot.aspx.vb:201:
Zwrot.aspx.vb:202:        Dim dataZwrotu As Date
Zwrot.aspx.vb:203:        If Not OdczytajDateZwrotu(dataZwrotu) Then
Zwrot.aspx.vb:204:            lblKomunikat.Text = "Niepoprawna data zwrotu."
Zwrot.aspx.vb:205:            Return
Zwrot.aspx.vb:206:        End If
Zwrot.aspx.vb:207:        If dataZwrotu > Date.Now.AddMinutes(5) Then
Zwrot.aspx.vb:208:            lblKomunikat.Text = "Data zwrotu nie moze byc z przyszlosci."
Zwrot.aspx.vb:209:            Return
Zwrot.aspx.vb:210:        End If
Zwrot.aspx.vb:211:
Zwrot.aspx.vb:212:        Dim manager As New RentalManager()
Zwrot.aspx.vb:213:        Dim przed As Wypozyczenie = manager.PobierzWypozyczenie(id)
Zwrot.aspx.vb:214:        If przed Is Nothing Then
Zwrot.aspx.vb:215:            lblKomunikat.Text = "Nie znaleziono wypozyczenia " & id
Zwrot.aspx.vb:216:            Return
Zwrot.aspx.vb:217:        End If
Zwrot.aspx.vb:218:        If dataZwrotu < przed.DataWydania Then
Zwrot.aspx.vb:219:            lblKomunikat.Text = "Data zwrotu wczesniejsza niz data wydania (" & przed.DataWydania.ToString(FormatDaty) & ")."
Zwrot.aspx.vb:220:            Return
Zwrot.aspx.vb:221:        End If
Zwrot.aspx.vb:222:
Zwrot.aspx.vb:223:        ' slownik szkod: kwoty uzgodnione z kierownikiem punktu (cennik 2014)
Zwrot.aspx.vb:224:        Dim cennikSzkod As New Dictionary(Of String, Decimal)
Zwrot.aspx.vb:225:        cennikSzkod.Add("Uszkodzona obudowa", 50D)
Zwrot.aspx.vb:226:        cennikSzkod.Add("Brak akcesoriów", 30D)
Zwrot.aspx.vb:227:        cennikSzkod.Add("Zabrudzenie", 20D)
Zwrot.aspx.vb:228:
Zwrot.aspx.vb:229:        Dim szkody As Decimal = 0
Zwrot.aspx.vb:230:        Dim opisSzkod As New List(Of String)
Zwrot.aspx.vb:231:        For Each item As ListItem In cblSzkody.Items
Zwrot.aspx.vb:232:            If item.Selected AndAlso cennikSzkod.ContainsKey(item.Value) Then
Zwrot.aspx.vb:233:                szkody = szkody + cennikSzkod(item.Value)
Zwrot.aspx.vb:234:                opisSzkod.Add(item.Value)
Zwrot.aspx.vb:235:            End If
Zwrot.aspx.vb:236:        Next
Zwrot.aspx.vb:237:        If szkody > przed.KaucjaPobrana Then
Zwrot.aspx.vb:238:            lblKomunikat.Text = "Uwaga: szkody przekraczaja kaucje - roznica trafi na fakture."
Zwrot.aspx.vb:239:        End If
Zwrot.aspx.vb:240:
Zwrot.aspx.vb:241:        Dim po As Wypozyczenie
Zwrot.aspx.vb:242:        Try
Zwrot.aspx.vb:243:            po = manager.ZarejestrujZwrot(id, dataZwrotu, szkody)
Zwrot.aspx.vb:244:        Catch ex As BladBiznesowyException
Zwrot.aspx.vb:245:            lblKomunikat.Text = ex.Message
Zwrot.aspx.vb:246:            Return
Zwrot.aspx.vb:247:        Catch ex As Exception
Zwrot.aspx.vb:248:            lblKomunikat.Text = "Blad rejestracji zwrotu: " & ex.Message
Zwrot.aspx.vb:249:            Return
Zwrot.aspx.vb:250:        End Try
Zwrot.aspx.vb:251:
Zwrot.aspx.vb:252:        lblOplata.Text = po.Oplata
Zwrot.aspx.vb:253:        lblKara.Text = po.Kara
Zwrot.aspx.vb:254:        lblKaucjaZwrot.Text = po.KaucjaZwrocona
Zwrot.aspx.vb:255:
Zwrot.aspx.vb:256:        Session("OstatnieWypozyczenieId") = po.Id
Zwrot.aspx.vb:257:        lnkFaktura.NavigateUrl = "Faktura.aspx?id=" & po.Id
Zwrot.aspx.vb:258:        lnkFaktura.Visible = True
Zwrot.aspx.vb:259:
Zwrot.aspx.vb:260:        Dim komunikat As String = "Zwrot zarejestrowany (wypozyczenie nr " & po.Id & ")."
Zwrot.aspx.vb:261:        If po.Kara > 0 Then
Zwrot.aspx.vb:262:            komunikat = komunikat & " Naliczono kare za spoznienie (" & OpisSpoznienia(po.PlanowanyZwrot, dataZwrotu) & ")."
Zwrot.aspx.vb:263:        End If
Zwrot.aspx.vb:264:        If opisSzkod.Count > 0 Then
Zwrot.aspx.vb:265:            komunikat = komunikat & " Szkody: " & String.Join(", ", opisSzkod.ToArray()) & "."
Zwrot.aspx.vb:266:        End If
Zwrot.aspx.vb:267:        If po.KaucjaZwrocona = 0 AndAlso po.KaucjaPobrana > 0 Then
Zwrot.aspx.vb:268:            komunikat = komunikat & " Kaucja w calosci zatrzymana."
Zwrot.aspx.vb:269:        End If
Zwrot.aspx.vb:270:        lblKomunikat.ForeColor = Drawing.Color.Green
Zwrot.aspx.vb:271:        lblKomunikat.Text = Server.HtmlEncode(komunikat)
Zwrot.aspx.vb:272:
Zwrot.aspx.vb:273:        txtDataZwrotu.Text = ""
Zwrot.aspx.vb:274:        For Each item As ListItem In cblSzkody.Items
Zwrot.aspx.vb:275:            item.Selected = False
Zwrot.aspx.vb:276:        Next
Zwrot.aspx.vb:277:        lblSzczegoly.Text = "-"
Zwrot.aspx.vb:278:        WczytajOtwarte()
Zwrot.aspx.vb:279:    End Sub
Zwrot.aspx.vb:280:
Zwrot.aspx.vb:281:End Class
```

`git --no-pager -C 'legacy-app-parent\legacy-app\WypozyczalniaWeb\Web References\BramkaPlatnosci' grep -n -I -e '^' -- 'Reference.vb'`

```text
Reference.vb:1:'------------------------------------------------------------------------------
Reference.vb:2:' <auto-generated>
Reference.vb:3:'     This code was generated by a tool.
Reference.vb:4:'     Runtime Version:4.0.30319.42000
Reference.vb:5:'
Reference.vb:6:'     Changes to this file may cause incorrect behavior and will be lost if
Reference.vb:7:'     the code is regenerated.
Reference.vb:8:' </auto-generated>
Reference.vb:9:'------------------------------------------------------------------------------
Reference.vb:10:
Reference.vb:11:Option Strict Off
Reference.vb:12:Option Explicit On
Reference.vb:13:
Reference.vb:14:Imports System
Reference.vb:15:Imports System.ComponentModel
Reference.vb:16:Imports System.Configuration
Reference.vb:17:Imports System.Diagnostics
Reference.vb:18:Imports System.Web.Services
Reference.vb:19:Imports System.Web.Services.Protocols
Reference.vb:20:Imports System.Xml.Serialization
Reference.vb:21:
Reference.vb:22:'
Reference.vb:23:'This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
Reference.vb:24:'
Reference.vb:25:Namespace BramkaPlatnosci
Reference.vb:26:
Reference.vb:27:    '''<remarks/>
Reference.vb:28:    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0"),  _
Reference.vb:29:     System.Diagnostics.DebuggerStepThroughAttribute(),  _
Reference.vb:30:     System.ComponentModel.DesignerCategoryAttribute("code"),  _
Reference.vb:31:     System.Web.Services.WebServiceBindingAttribute(Name:="BramkaServiceSoap", [Namespace]:="http://bramka.example.invalid/")>  _
Reference.vb:32:    Partial Public Class BramkaService
Reference.vb:33:        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
Reference.vb:34:
Reference.vb:35:        Private useDefaultCredentialsSetExplicitly As Boolean
Reference.vb:36:
Reference.vb:37:        '''<remarks/>
Reference.vb:38:        Public Sub New()
Reference.vb:39:            MyBase.New
Reference.vb:40:            'Me.Url = "https://bramka.example.invalid/BramkaService.asmx"
Reference.vb:41:            Me.Url = ConfigurationManager.AppSettings("BramkaPlatnosciUrl")  ' zmienione recznie po wygenerowaniu
Reference.vb:42:            If (Me.IsLocalFileSystemWebService(Me.Url) = true) Then
Reference.vb:43:                Me.UseDefaultCredentials = true
Reference.vb:44:                Me.useDefaultCredentialsSetExplicitly = false
Reference.vb:45:            Else
Reference.vb:46:                Me.useDefaultCredentialsSetExplicitly = true
Reference.vb:47:            End If
Reference.vb:48:        End Sub
Reference.vb:49:
Reference.vb:50:        Public Shadows Property Url() As String
Reference.vb:51:            Get
Reference.vb:52:                Return MyBase.Url
Reference.vb:53:            End Get
Reference.vb:54:            Set
Reference.vb:55:                If (((Me.IsLocalFileSystemWebService(MyBase.Url) = true)  _
Reference.vb:56:                            AndAlso (Me.useDefaultCredentialsSetExplicitly = false))  _
Reference.vb:57:                            AndAlso (Me.IsLocalFileSystemWebService(value) = false)) Then
Reference.vb:58:                    MyBase.UseDefaultCredentials = false
Reference.vb:59:                End If
Reference.vb:60:                MyBase.Url = value
Reference.vb:61:            End Set
Reference.vb:62:        End Property
Reference.vb:63:
Reference.vb:64:        Public Shadows Property UseDefaultCredentials() As Boolean
Reference.vb:65:            Get
Reference.vb:66:                Return MyBase.UseDefaultCredentials
Reference.vb:67:            End Get
Reference.vb:68:            Set
Reference.vb:69:                MyBase.UseDefaultCredentials = value
Reference.vb:70:                Me.useDefaultCredentialsSetExplicitly = true
Reference.vb:71:            End Set
Reference.vb:72:        End Property
Reference.vb:73:
Reference.vb:74:        '''<remarks/>
Reference.vb:75:        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://bramka.example.invalid/ZarejestrujPlatnosc", RequestNamespace:="http://bramka.example.invalid/", ResponseNamespace:="http://bramka.example.invalid/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
Reference.vb:76:        Public Function ZarejestrujPlatnosc(ByVal numerFaktury As String, ByVal kwota As Decimal, ByVal waluta As String) As WynikPlatnosci
Reference.vb:77:            Dim results() As Object = Me.Invoke("ZarejestrujPlatnosc", New Object() {numerFaktury, kwota, waluta})
Reference.vb:78:            Return CType(results(0),WynikPlatnosci)
Reference.vb:79:        End Function
Reference.vb:80:
Reference.vb:81:        '''<remarks/>
Reference.vb:82:        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://bramka.example.invalid/StatusPlatnosci", RequestNamespace:="http://bramka.example.invalid/", ResponseNamespace:="http://bramka.example.invalid/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
Reference.vb:83:        Public Function StatusPlatnosci(ByVal idTransakcji As String) As String
Reference.vb:84:            Dim results() As Object = Me.Invoke("StatusPlatnosci", New Object() {idTransakcji})
Reference.vb:85:            Return CType(results(0),String)
Reference.vb:86:        End Function
Reference.vb:87:
Reference.vb:88:        '''<remarks/>
Reference.vb:89:        Public Shadows Sub CancelAsync(ByVal userState As Object)
Reference.vb:90:            MyBase.CancelAsync(userState)
Reference.vb:91:        End Sub
Reference.vb:92:
Reference.vb:93:        Private Function IsLocalFileSystemWebService(ByVal url As String) As Boolean
Reference.vb:94:            If ((url Is Nothing)  _
Reference.vb:95:                        OrElse (url Is String.Empty)) Then
Reference.vb:96:                Return false
Reference.vb:97:            End If
Reference.vb:98:            Dim wsUri As System.Uri = New System.Uri(url)
Reference.vb:99:            If ((wsUri.Port >= 1024)  _
Reference.vb:100:                        AndAlso (String.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) = 0)) Then
Reference.vb:101:                Return true
Reference.vb:102:            End If
Reference.vb:103:            Return false
Reference.vb:104:        End Function
Reference.vb:105:    End Class
Reference.vb:106:
Reference.vb:107:    '''<remarks/>
Reference.vb:108:    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),  _
Reference.vb:109:     System.SerializableAttribute(),  _
Reference.vb:110:     System.Diagnostics.DebuggerStepThroughAttribute(),  _
Reference.vb:111:     System.ComponentModel.DesignerCategoryAttribute("code"),  _
Reference.vb:112:     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://bramka.example.invalid/")>  _
Reference.vb:113:    Partial Public Class WynikPlatnosci
Reference.vb:114:
Reference.vb:115:        '''<remarks/>
Reference.vb:116:        Public Property IdTransakcji As String
Reference.vb:117:
Reference.vb:118:        '''<remarks/>
Reference.vb:119:        Public Property Status As String
Reference.vb:120:
Reference.vb:121:        '''<remarks/>
Reference.vb:122:        Public Property Komunikat As String
Reference.vb:123:    End Class
Reference.vb:124:End Namespace
```

## M21 — surowy wynik H11

`git --no-pager -C 'legacy-app-parent\legacy-app\WypozyczalniaCore\Integracje' grep -n -I -e '^' -- 'KurierClient.vb'`

```text
KurierClient.vb:1:' Modul: Integracje\KurierClient.vb | Autor: zespol IT | 2015-06-22
KurierClient.vb:2:' Klient REST firmy kurierskiej: zamowienie dostawy i status przesylki.
KurierClient.vb:3:' 2015-06-22 MW: pierwsza wersja (API v1, JSON)
KurierClient.vb:4:' 2019-01-14 PT: TLS 1.2 wymagany przez dostawce
KurierClient.vb:5:
KurierClient.vb:6:Imports System.Configuration
KurierClient.vb:7:Imports System.IO
KurierClient.vb:8:Imports System.Net
KurierClient.vb:9:Imports System.Text
KurierClient.vb:10:Imports Newtonsoft.Json
KurierClient.vb:11:Imports Newtonsoft.Json.Linq
KurierClient.vb:12:
KurierClient.vb:13:Namespace Integracje
KurierClient.vb:14:
KurierClient.vb:15:    Public Class KurierClient
KurierClient.vb:16:
KurierClient.vb:17:        Private Const LimitCzasuMs As Integer = 15000
KurierClient.vb:18:
KurierClient.vb:19:        Private ReadOnly _adresApi As String
KurierClient.vb:20:        Private ReadOnly _kluczApi As String
KurierClient.vb:21:
KurierClient.vb:22:        Public Sub New()
KurierClient.vb:23:            _adresApi = ConfigurationManager.AppSettings("KurierApiUrl")
KurierClient.vb:24:            _kluczApi = ConfigurationManager.AppSettings("KurierApiKey")
KurierClient.vb:25:            ServicePointManager.SecurityProtocol = ServicePointManager.SecurityProtocol Or SecurityProtocolType.Tls12
KurierClient.vb:26:        End Sub
KurierClient.vb:27:
KurierClient.vb:28:        Public Function ZamowDostawe(ByVal wypozyczenieId As Integer, ByVal adres As String, ByVal telefon As String) As String
KurierClient.vb:29:            If String.IsNullOrEmpty(_adresApi) Then Return ""
KurierClient.vb:30:            Dim zlecenie As New JObject()
KurierClient.vb:31:            zlecenie("referencja") = "WYP-" & wypozyczenieId.ToString("000000")
KurierClient.vb:32:            zlecenie("adresDostawy") = adres
KurierClient.vb:33:            zlecenie("telefonOdbiorcy") = telefon
KurierClient.vb:34:            zlecenie("rodzaj") = "PALETA"
KurierClient.vb:35:            zlecenie("pobranie") = False
KurierClient.vb:36:            zlecenie("terminOd") = Date.Today.AddDays(1).ToString("yyyy-MM-dd")
KurierClient.vb:37:            Dim tresc As String = JsonConvert.SerializeObject(zlecenie)
KurierClient.vb:38:
KurierClient.vb:39:            Try
KurierClient.vb:40:                Dim zadanie As HttpWebRequest = UtworzZadanie(_adresApi, "POST")
KurierClient.vb:41:                Dim bajty As Byte() = Encoding.UTF8.GetBytes(tresc)
KurierClient.vb:42:                zadanie.ContentType = "application/json; charset=utf-8"
KurierClient.vb:43:                zadanie.ContentLength = bajty.Length
KurierClient.vb:44:                Using strumien As Stream = zadanie.GetRequestStream()
KurierClient.vb:45:                    strumien.Write(bajty, 0, bajty.Length)
KurierClient.vb:46:                End Using
KurierClient.vb:47:
KurierClient.vb:48:                Dim odpowiedz As String = CzytajOdpowiedz(zadanie)
KurierClient.vb:49:                Dim wynik As JObject = JObject.Parse(odpowiedz)
KurierClient.vb:50:                Dim numer As JToken = wynik("numerPrzesylki")
KurierClient.vb:51:                If numer Is Nothing OrElse numer.Type = JTokenType.Null Then
KurierClient.vb:52:                    Return ""
KurierClient.vb:53:                End If
KurierClient.vb:54:                Return numer.ToString()
KurierClient.vb:55:            Catch ex As WebException
KurierClient.vb:56:                System.Diagnostics.Trace.WriteLine("Kurier: blad zamowienia dla " & wypozyczenieId & ": " & OpisBledu(ex))
KurierClient.vb:57:                Return ""
KurierClient.vb:58:            End Try
KurierClient.vb:59:        End Function
KurierClient.vb:60:
KurierClient.vb:61:        Public Function StatusPrzesylki(ByVal numerPrzesylki As String) As String
KurierClient.vb:62:            If String.IsNullOrEmpty(numerPrzesylki) Then Return ""
KurierClient.vb:63:            Dim adres As String = _adresApi.TrimEnd("/"c) & "/" & Uri.EscapeDataString(numerPrzesylki) & "/status"
KurierClient.vb:64:            Dim zadanie As HttpWebRequest = UtworzZadanie(adres, "GET")
KurierClient.vb:65:            Dim odpowiedz As String = CzytajOdpowiedz(zadanie)
KurierClient.vb:66:            Dim wynik As JObject = JObject.Parse(odpowiedz)
KurierClient.vb:67:            Dim status As JToken = wynik("status")
KurierClient.vb:68:            If status Is Nothing Then Return "NIEZNANY"
KurierClient.vb:69:            Return TlumaczStatus(status.ToString())
KurierClient.vb:70:        End Function
KurierClient.vb:71:
KurierClient.vb:72:        Public Function AnulujPrzesylke(ByVal numerPrzesylki As String) As Boolean
KurierClient.vb:73:            If String.IsNullOrEmpty(numerPrzesylki) OrElse String.IsNullOrEmpty(_adresApi) Then Return False
KurierClient.vb:74:            Dim adres As String = _adresApi.TrimEnd("/"c) & "/" & Uri.EscapeDataString(numerPrzesylki)
KurierClient.vb:75:            Try
KurierClient.vb:76:                Dim zadanie As HttpWebRequest = UtworzZadanie(adres, "DELETE")
KurierClient.vb:77:                Using odpowiedz As HttpWebResponse = CType(zadanie.GetResponse(), HttpWebResponse)
KurierClient.vb:78:                    Return odpowiedz.StatusCode = HttpStatusCode.OK OrElse odpowiedz.StatusCode = HttpStatusCode.NoContent
KurierClient.vb:79:                End Using
KurierClient.vb:80:            Catch ex As WebException
KurierClient.vb:81:                System.Diagnostics.Trace.WriteLine("Kurier: blad anulowania " & numerPrzesylki & ": " & OpisBledu(ex))
KurierClient.vb:82:                Return False
KurierClient.vb:83:            End Try
KurierClient.vb:84:        End Function
KurierClient.vb:85:
KurierClient.vb:86:        Public Function CzyDostepny() As Boolean
KurierClient.vb:87:            If String.IsNullOrEmpty(_adresApi) Then Return False
KurierClient.vb:88:            Dim adres As String = _adresApi.TrimEnd("/"c) & "/ping"
KurierClient.vb:89:            Try
KurierClient.vb:90:                Dim zadanie As HttpWebRequest = UtworzZadanie(adres, "GET")
KurierClient.vb:91:                zadanie.Timeout = 3000
KurierClient.vb:92:                Using odpowiedz As HttpWebResponse = CType(zadanie.GetResponse(), HttpWebResponse)
KurierClient.vb:93:                    Return odpowiedz.StatusCode = HttpStatusCode.OK
KurierClient.vb:94:                End Using
KurierClient.vb:95:            Catch ex As WebException
KurierClient.vb:96:                Return False
KurierClient.vb:97:            End Try
KurierClient.vb:98:        End Function
KurierClient.vb:99:
KurierClient.vb:100:        Private Function UtworzZadanie(ByVal adres As String, ByVal metoda As String) As HttpWebRequest
KurierClient.vb:101:            Dim zadanie As HttpWebRequest = CType(WebRequest.Create(adres), HttpWebRequest)
KurierClient.vb:102:            zadanie.Method = metoda
KurierClient.vb:103:            zadanie.Accept = "application/json"
KurierClient.vb:104:            zadanie.Timeout = LimitCzasuMs
KurierClient.vb:105:            zadanie.ReadWriteTimeout = LimitCzasuMs
KurierClient.vb:106:            zadanie.Headers.Add("X-Api-Key", _kluczApi)
KurierClient.vb:107:            zadanie.UserAgent = "WypozyczalniaWeb/1.4"
KurierClient.vb:108:            Return zadanie
KurierClient.vb:109:        End Function
KurierClient.vb:110:
KurierClient.vb:111:        Private Function CzytajOdpowiedz(ByVal zadanie As HttpWebRequest) As String
KurierClient.vb:112:            Using odpowiedz As HttpWebResponse = CType(zadanie.GetResponse(), HttpWebResponse)
KurierClient.vb:113:                Using czytnik As New StreamReader(odpowiedz.GetResponseStream(), Encoding.UTF8)
KurierClient.vb:114:                    Return czytnik.ReadToEnd()
KurierClient.vb:115:                End Using
KurierClient.vb:116:            End Using
KurierClient.vb:117:        End Function
KurierClient.vb:118:
KurierClient.vb:119:        Private Function OpisBledu(ByVal ex As WebException) As String
KurierClient.vb:120:            If ex.Response Is Nothing Then Return ex.Status.ToString() & " " & ex.Message
KurierClient.vb:121:            Try
KurierClient.vb:122:                Dim http As HttpWebResponse = CType(ex.Response, HttpWebResponse)
KurierClient.vb:123:                Using czytnik As New StreamReader(http.GetResponseStream(), Encoding.UTF8)
KurierClient.vb:124:                    Return CInt(http.StatusCode).ToString() & " " & czytnik.ReadToEnd()
KurierClient.vb:125:                End Using
KurierClient.vb:126:            Catch
KurierClient.vb:127:                Return ex.Message
KurierClient.vb:128:            End Try
KurierClient.vb:129:        End Function
KurierClient.vb:130:
KurierClient.vb:131:        Private Function TlumaczStatus(ByVal kod As String) As String
KurierClient.vb:132:            Select Case kod.ToUpperInvariant()
KurierClient.vb:133:                Case "CREATED", "NEW"
KurierClient.vb:134:                    Return "PRZYJETA"
KurierClient.vb:135:                Case "IN_TRANSIT"
KurierClient.vb:136:                    Return "W DRODZE"
KurierClient.vb:137:                Case "DELIVERED"
KurierClient.vb:138:                    Return "DOSTARCZONA"
KurierClient.vb:139:                Case "RETURNED"
KurierClient.vb:140:                    Return "ZWROCONA"
KurierClient.vb:141:                Case Else
KurierClient.vb:142:                    Return kod
KurierClient.vb:143:            End Select
KurierClient.vb:144:        End Function
KurierClient.vb:145:
KurierClient.vb:146:    End Class
KurierClient.vb:147:
KurierClient.vb:148:End Namespace
```

## M22 — surowy wynik H12

`git --no-pager -C 'legacy-app-parent\legacy-app\WypozyczalniaCore\Integracje' grep -n -I -e '^' -- 'ImportCennikaXml.vb' 'EksportCsv.vb'`

```text
EksportCsv.vb:1:' Modul: Integracje\EksportCsv.vb - dzienny eksport CSV na udzial sieciowy | Autor: zespol IT | 2015-08-04
EksportCsv.vb:2:' Pliki odbiera system ksiegowy z udzialu (wypozyczenia_yyyyMMdd.csv, faktury_yyyyMMdd.csv).
EksportCsv.vb:3:' 2016-10-12 MW: drugi plik z fakturami dnia
EksportCsv.vb:4:' 2018-04-03 PT: przeciazenie z katalogiem docelowym (zadanie nocne)
EksportCsv.vb:5:
EksportCsv.vb:6:Imports System.Data
EksportCsv.vb:7:Imports System.IO
EksportCsv.vb:8:Imports System.Text
EksportCsv.vb:9:Imports System.Configuration
EksportCsv.vb:10:
EksportCsv.vb:11:Namespace Integracje
EksportCsv.vb:12:
EksportCsv.vb:13:    Public Class EksportCsv
EksportCsv.vb:14:
EksportCsv.vb:15:        Private Const Separator As String = ";"
EksportCsv.vb:16:        Private Const NaglowekWypozyczen As String = "Id;Klient;Sprzet;DataWydania;PlanowanyZwrot;DataZwrotu;Oplata;Kara"
EksportCsv.vb:17:        Private Const NaglowekFaktur As String = "Numer;Klient;WypozyczenieId;DataWystawienia;Netto;Vat;Brutto;Status"
EksportCsv.vb:18:        Private Const FormatDaty As String = "yyyy-MM-dd HH:mm"
EksportCsv.vb:19:
EksportCsv.vb:20:        Public Function EksportujDzien(ByVal dzien As Date) As String
EksportCsv.vb:21:            Dim katalog As String = ConfigurationManager.AppSettings("EksportUncSciezka")
EksportCsv.vb:22:            If String.IsNullOrEmpty(katalog) Then
EksportCsv.vb:23:                Throw New ConfigurationErrorsException("Brak klucza EksportUncSciezka w konfiguracji")
EksportCsv.vb:24:            End If
EksportCsv.vb:25:            Return EksportujDzien(dzien, katalog)
EksportCsv.vb:26:        End Function
EksportCsv.vb:27:
EksportCsv.vb:28:        Public Function EksportujDzien(ByVal dzien As Date, ByVal katalogDocelowy As String) As String
EksportCsv.vb:29:            Dim wypozyczenia As DataTable = New RentalManager().WypozyczeniaZDnia(dzien)
EksportCsv.vb:30:            Dim tresc As New StringBuilder()
EksportCsv.vb:31:            tresc.AppendLine(NaglowekWypozyczen)
EksportCsv.vb:32:            For Each wiersz As DataRow In wypozyczenia.Rows
EksportCsv.vb:33:                Dim linia As New StringBuilder()
EksportCsv.vb:34:                linia.Append(wiersz("Id").ToString()).Append(Separator)
EksportCsv.vb:35:                linia.Append(Oczysc(wiersz("Klient"))).Append(Separator)
EksportCsv.vb:36:                linia.Append(Oczysc(wiersz("Sprzet"))).Append(Separator)
EksportCsv.vb:37:                linia.Append(Data(wiersz("DataWydania"))).Append(Separator)
EksportCsv.vb:38:                linia.Append(Data(wiersz("PlanowanyZwrot"))).Append(Separator)
EksportCsv.vb:39:                linia.Append(Data(wiersz("DataZwrotu"))).Append(Separator)
EksportCsv.vb:40:                linia.Append(Kwota(wiersz("Oplata"))).Append(Separator)
EksportCsv.vb:41:                linia.Append(Kwota(wiersz("Kara")))
EksportCsv.vb:42:                tresc.AppendLine(linia.ToString())
EksportCsv.vb:43:            Next
EksportCsv.vb:44:
EksportCsv.vb:45:            Dim plikWypozyczen As String = Path.Combine(katalogDocelowy, "wypozyczenia_" & dzien.ToString("yyyyMMdd") & ".csv")
EksportCsv.vb:46:            ZapiszNaUdzial(plikWypozyczen, tresc.ToString())
EksportCsv.vb:47:
EksportCsv.vb:48:            Dim faktury As DataTable = New InvoiceManager().FakturyZDnia(dzien)
EksportCsv.vb:49:            Dim trescFaktur As New StringBuilder()
EksportCsv.vb:50:            trescFaktur.AppendLine(NaglowekFaktur)
EksportCsv.vb:51:            For Each wiersz As DataRow In faktury.Rows
EksportCsv.vb:52:                trescFaktur.Append(wiersz("Numer").ToString()).Append(Separator)
EksportCsv.vb:53:                trescFaktur.Append(Oczysc(wiersz("Klient"))).Append(Separator)
EksportCsv.vb:54:                trescFaktur.Append(wiersz("WypozyczenieId").ToString()).Append(Separator)
EksportCsv.vb:55:                trescFaktur.Append(Data(wiersz("DataWystawienia"))).Append(Separator)
EksportCsv.vb:56:                trescFaktur.Append(Kwota(wiersz("Netto"))).Append(Separator)
EksportCsv.vb:57:                trescFaktur.Append(Kwota(wiersz("Vat"))).Append(Separator)
EksportCsv.vb:58:                trescFaktur.Append(Kwota(wiersz("Brutto"))).Append(Separator)
EksportCsv.vb:59:                trescFaktur.AppendLine(wiersz("Status").ToString())
EksportCsv.vb:60:            Next
EksportCsv.vb:61:            Dim plikFaktur As String = Path.Combine(katalogDocelowy, "faktury_" & dzien.ToString("yyyyMMdd") & ".csv")
EksportCsv.vb:62:            ZapiszNaUdzial(plikFaktur, trescFaktur.ToString())
EksportCsv.vb:63:
EksportCsv.vb:64:            Return plikWypozyczen
EksportCsv.vb:65:        End Function
EksportCsv.vb:66:
EksportCsv.vb:67:        Private Sub ZapiszNaUdzial(ByVal sciezka As String, ByVal tresc As String)
EksportCsv.vb:68:            On Error Resume Next
EksportCsv.vb:69:            Dim katalog As String = Path.GetDirectoryName(sciezka)
EksportCsv.vb:70:            If Not Directory.Exists(katalog) Then
EksportCsv.vb:71:                Directory.CreateDirectory(katalog)
EksportCsv.vb:72:            End If
EksportCsv.vb:73:            If File.Exists(sciezka) Then
EksportCsv.vb:74:                File.Copy(sciezka, sciezka & ".bak", True)
EksportCsv.vb:75:            End If
EksportCsv.vb:76:            File.WriteAllText(sciezka, tresc, Encoding.GetEncoding(1250))
EksportCsv.vb:77:        End Sub
EksportCsv.vb:78:
EksportCsv.vb:79:        Private Function Kwota(ByVal wartosc As Object) As String
EksportCsv.vb:80:            If wartosc Is Nothing OrElse IsDBNull(wartosc) Then
EksportCsv.vb:81:                Return ""
EksportCsv.vb:82:            End If
EksportCsv.vb:83:            Return CDec(wartosc).ToString("0.00")
EksportCsv.vb:84:        End Function
EksportCsv.vb:85:
EksportCsv.vb:86:        Private Function Data(ByVal wartosc As Object) As String
EksportCsv.vb:87:            If wartosc Is Nothing OrElse IsDBNull(wartosc) Then
EksportCsv.vb:88:                Return ""
EksportCsv.vb:89:            End If
EksportCsv.vb:90:            Return CDate(wartosc).ToString(FormatDaty)
EksportCsv.vb:91:        End Function
EksportCsv.vb:92:
EksportCsv.vb:93:        Private Function Oczysc(ByVal wartosc As Object) As String
EksportCsv.vb:94:            If wartosc Is Nothing OrElse IsDBNull(wartosc) Then
EksportCsv.vb:95:                Return ""
EksportCsv.vb:96:            End If
EksportCsv.vb:97:            Dim tekst As String = wartosc.ToString()
EksportCsv.vb:98:            tekst = tekst.Replace(Separator, ",")
EksportCsv.vb:99:            tekst = tekst.Replace(vbCr, " ").Replace(vbLf, " ")
EksportCsv.vb:100:            Return tekst.Trim()
EksportCsv.vb:101:        End Function
EksportCsv.vb:102:
EksportCsv.vb:103:        ' powtorzenie eksportu za kilka dni (np. po awarii udzialu)
EksportCsv.vb:104:        Public Function EksportujOkres(ByVal dataOd As Date, ByVal dataDo As Date, ByVal katalogDocelowy As String) As Integer
EksportCsv.vb:105:            If dataDo < dataOd Then
EksportCsv.vb:106:                Throw New ArgumentException("Data koncowa wczesniejsza niz poczatkowa")
EksportCsv.vb:107:            End If
EksportCsv.vb:108:            Dim ile As Integer = 0
EksportCsv.vb:109:            Dim dzien As Date = dataOd.Date
EksportCsv.vb:110:            While dzien <= dataDo.Date
EksportCsv.vb:111:                EksportujDzien(dzien, katalogDocelowy)
EksportCsv.vb:112:                If CzyWyeksportowano(dzien, katalogDocelowy) Then
EksportCsv.vb:113:                    ile = ile + 1
EksportCsv.vb:114:                End If
EksportCsv.vb:115:                dzien = dzien.AddDays(1)
EksportCsv.vb:116:            End While
EksportCsv.vb:117:            Return ile
EksportCsv.vb:118:        End Function
EksportCsv.vb:119:
EksportCsv.vb:120:        Public Function NazwaPlikuDnia(ByVal dzien As Date) As String
EksportCsv.vb:121:            Return "wypozyczenia_" & dzien.ToString("yyyyMMdd") & ".csv"
EksportCsv.vb:122:        End Function
EksportCsv.vb:123:
EksportCsv.vb:124:        Public Function CzyWyeksportowano(ByVal dzien As Date, ByVal katalogDocelowy As String) As Boolean
EksportCsv.vb:125:            If String.IsNullOrEmpty(katalogDocelowy) Then Return False
EksportCsv.vb:126:            Return File.Exists(Path.Combine(katalogDocelowy, NazwaPlikuDnia(dzien)))
EksportCsv.vb:127:        End Function
EksportCsv.vb:128:
EksportCsv.vb:129:    End Class
EksportCsv.vb:130:
EksportCsv.vb:131:End Namespace
ImportCennikaXml.vb:1:' Modul: Integracje - import cennika z plikow XML od dzialu handlowego | Autor: zespol IT | 2014-03-11
ImportCennikaXml.vb:2:' Historia:
ImportCennikaXml.vb:3:'   2017-06-12 JK: przenoszenie przetworzonych plikow do podkatalogu
ImportCennikaXml.vb:4:'   2018-02-27 MW: kaucja w elemencie pozycja
ImportCennikaXml.vb:5:
ImportCennikaXml.vb:6:Imports System.IO
ImportCennikaXml.vb:7:Imports System.Data
ImportCennikaXml.vb:8:Imports System.Xml
ImportCennikaXml.vb:9:Imports Wypozyczalnia.Core.Model
ImportCennikaXml.vb:10:Imports Wypozyczalnia.Core.DAL
ImportCennikaXml.vb:11:
ImportCennikaXml.vb:12:Namespace Integracje
ImportCennikaXml.vb:13:
ImportCennikaXml.vb:14:    Public Class ImportCennikaXml
ImportCennikaXml.vb:15:
ImportCennikaXml.vb:16:        Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(ImportCennikaXml))
ImportCennikaXml.vb:17:
ImportCennikaXml.vb:18:        Private Const KatalogPrzetworzonych As String = "przetworzone"
ImportCennikaXml.vb:19:
ImportCennikaXml.vb:20:        Public Property OstatniBlad As String
ImportCennikaXml.vb:21:
ImportCennikaXml.vb:22:        Public Function ImportujKatalog(ByVal katalog As String) As Integer
ImportCennikaXml.vb:23:            If String.IsNullOrEmpty(katalog) Then Throw New ArgumentException("Nie podano katalogu importu")
ImportCennikaXml.vb:24:            If Not Directory.Exists(katalog) Then
ImportCennikaXml.vb:25:                Throw New IOException("Katalog importu nie istnieje: " & katalog)
ImportCennikaXml.vb:26:            End If
ImportCennikaXml.vb:27:            Dim docelowy As String = Path.Combine(katalog, KatalogPrzetworzonych)
ImportCennikaXml.vb:28:            If Not Directory.Exists(docelowy) Then Directory.CreateDirectory(docelowy)
ImportCennikaXml.vb:29:
ImportCennikaXml.vb:30:            Dim pliki As String() = Directory.GetFiles(katalog, "*.xml")
ImportCennikaXml.vb:31:            Array.Sort(pliki)
ImportCennikaXml.vb:32:            Dim licznik As Integer = 0
ImportCennikaXml.vb:33:            For Each plik As String In pliki
ImportCennikaXml.vb:34:                Log.Info("Import cennika: " & plik)
ImportCennikaXml.vb:35:                Dim wiersze As Integer = ImportujPlik(plik)
ImportCennikaXml.vb:36:                Log.Info("Zaktualizowano wierszy: " & wiersze)
ImportCennikaXml.vb:37:                PrzeniesPlik(plik, docelowy)
ImportCennikaXml.vb:38:                licznik = licznik + 1
ImportCennikaXml.vb:39:            Next
ImportCennikaXml.vb:40:            Return licznik
ImportCennikaXml.vb:41:        End Function
ImportCennikaXml.vb:42:
ImportCennikaXml.vb:43:        Public Function ImportujPlik(ByVal sciezka As String) As Integer
ImportCennikaXml.vb:44:            Dim doc As New XmlDocument()
ImportCennikaXml.vb:45:            doc.Load(sciezka)
ImportCennikaXml.vb:46:            Dim korzen As XmlElement = doc.DocumentElement
ImportCennikaXml.vb:47:            If korzen Is Nothing OrElse korzen.Name <> "cennik" Then
ImportCennikaXml.vb:48:                Throw New XmlException("Nieprawidlowy element glowny w pliku " & Path.GetFileName(sciezka))
ImportCennikaXml.vb:49:            End If
ImportCennikaXml.vb:50:
ImportCennikaXml.vb:51:            Dim obowiazujeOd As Date = Date.Today
ImportCennikaXml.vb:52:            If korzen.HasAttribute("obowiazujeOd") Then
ImportCennikaXml.vb:53:                obowiazujeOd = Date.Parse(korzen.GetAttribute("obowiazujeOd"))
ImportCennikaXml.vb:54:            End If
ImportCennikaXml.vb:55:
ImportCennikaXml.vb:56:            Dim dal As New PricingDAL()
ImportCennikaXml.vb:57:            Dim zaktualizowane As Integer = 0
ImportCennikaXml.vb:58:
ImportCennikaXml.vb:59:            For Each wezel As XmlNode In korzen.SelectNodes("taryfa")
ImportCennikaXml.vb:60:                Dim t As New Taryfa()
ImportCennikaXml.vb:61:                t.Kategoria = Atrybut(wezel, "kategoria").ToUpper()
ImportCennikaXml.vb:62:                t.MnoznikWeekend = Decimal.Parse(Atrybut(wezel, "mnoznikWeekend"))
ImportCennikaXml.vb:63:                t.RabatTygodniowyProc = CDec(Atrybut(wezel, "rabatTygodniowy"))
ImportCennikaXml.vb:64:                t.ObowiazujeOd = obowiazujeOd
ImportCennikaXml.vb:65:                If Not Taryfa.CzyZnanaKategoria(t.Kategoria) Then
ImportCennikaXml.vb:66:                    Log.Warn("Pominieto taryfe nieznanej kategorii: " & t.Kategoria)
ImportCennikaXml.vb:67:                    Continue For
ImportCennikaXml.vb:68:                End If
ImportCennikaXml.vb:69:                dal.ZapiszTaryfe(t)
ImportCennikaXml.vb:70:                zaktualizowane = zaktualizowane + 1
ImportCennikaXml.vb:71:            Next
ImportCennikaXml.vb:72:
ImportCennikaXml.vb:73:            For Each wezel As XmlNode In korzen.SelectNodes("pozycja")
ImportCennikaXml.vb:74:                Dim numer As String = Atrybut(wezel, "numer")
ImportCennikaXml.vb:75:                Dim stawka As Decimal = Decimal.Parse(Atrybut(wezel, "stawka"))
ImportCennikaXml.vb:76:                Dim kaucja As Decimal = 0D
ImportCennikaXml.vb:77:                If wezel.Attributes("kaucja") IsNot Nothing Then
ImportCennikaXml.vb:78:                    kaucja = CDec(Atrybut(wezel, "kaucja"))
ImportCennikaXml.vb:79:                Else
ImportCennikaXml.vb:80:                    kaucja = KaucjaBiezaca(numer)
ImportCennikaXml.vb:81:                End If
ImportCennikaXml.vb:82:                If stawka <= 0D Then
ImportCennikaXml.vb:83:                    Log.Warn("Pominieto pozycje " & numer & " ze stawka " & stawka)
ImportCennikaXml.vb:84:                    Continue For
ImportCennikaXml.vb:85:                End If
ImportCennikaXml.vb:86:                Dim ile As Integer = dal.ZaktualizujStawkeSprzetu(numer, stawka, kaucja)
ImportCennikaXml.vb:87:                If ile = 0 Then
ImportCennikaXml.vb:88:                    Log.Warn("Brak sprzetu o numerze " & numer)
ImportCennikaXml.vb:89:                End If
ImportCennikaXml.vb:90:                zaktualizowane = zaktualizowane + ile
ImportCennikaXml.vb:91:            Next
ImportCennikaXml.vb:92:
ImportCennikaXml.vb:93:            dal.ZapiszWDzienniku("IMPORT_CENNIKA", Path.GetFileName(sciezka) & ": " & zaktualizowane)
ImportCennikaXml.vb:94:            Return zaktualizowane
ImportCennikaXml.vb:95:        End Function
ImportCennikaXml.vb:96:
ImportCennikaXml.vb:97:        Public Function PlikiOczekujace(ByVal katalog As String) As String()
ImportCennikaXml.vb:98:            If String.IsNullOrEmpty(katalog) OrElse Not Directory.Exists(katalog) Then Return New String() {}
ImportCennikaXml.vb:99:            Dim pliki As String() = Directory.GetFiles(katalog, "*.xml")
ImportCennikaXml.vb:100:            For i As Integer = 0 To pliki.Length - 1
ImportCennikaXml.vb:101:                pliki(i) = Path.GetFileName(pliki(i))
ImportCennikaXml.vb:102:            Next
ImportCennikaXml.vb:103:            Array.Sort(pliki)
ImportCennikaXml.vb:104:            Return pliki
ImportCennikaXml.vb:105:        End Function
ImportCennikaXml.vb:106:
ImportCennikaXml.vb:107:        Public Function SprawdzPlik(ByVal sciezka As String) As List(Of String)
ImportCennikaXml.vb:108:            Dim uwagi As New List(Of String)()
ImportCennikaXml.vb:109:            Dim doc As New XmlDocument()
ImportCennikaXml.vb:110:            Try
ImportCennikaXml.vb:111:                doc.Load(sciezka)
ImportCennikaXml.vb:112:            Catch ex As XmlException
ImportCennikaXml.vb:113:                uwagi.Add("Plik nie jest poprawnym XML: " & ex.Message)
ImportCennikaXml.vb:114:                Return uwagi
ImportCennikaXml.vb:115:            End Try
ImportCennikaXml.vb:116:            Dim korzen As XmlElement = doc.DocumentElement
ImportCennikaXml.vb:117:            If korzen.Name <> "cennik" Then uwagi.Add("Element glowny: " & korzen.Name)
ImportCennikaXml.vb:118:            If Not korzen.HasAttribute("obowiazujeOd") Then uwagi.Add("Brak atrybutu obowiazujeOd (przyjeta data biezaca)")
ImportCennikaXml.vb:119:            For Each wezel As XmlNode In korzen.SelectNodes("taryfa")
ImportCennikaXml.vb:120:                If wezel.Attributes("kategoria") Is Nothing Then uwagi.Add("Taryfa bez kategorii")
ImportCennikaXml.vb:121:            Next
ImportCennikaXml.vb:122:            For Each wezel As XmlNode In korzen.SelectNodes("pozycja")
ImportCennikaXml.vb:123:                If wezel.Attributes("numer") Is Nothing Then uwagi.Add("Pozycja bez numeru")
ImportCennikaXml.vb:124:                If wezel.Attributes("stawka") Is Nothing Then uwagi.Add("Pozycja bez stawki")
ImportCennikaXml.vb:125:            Next
ImportCennikaXml.vb:126:            If korzen.SelectNodes("taryfa").Count = 0 AndAlso korzen.SelectNodes("pozycja").Count = 0 Then
ImportCennikaXml.vb:127:                uwagi.Add("Plik nie zawiera taryf ani pozycji")
ImportCennikaXml.vb:128:            End If
ImportCennikaXml.vb:129:            Return uwagi
ImportCennikaXml.vb:130:        End Function
ImportCennikaXml.vb:131:
ImportCennikaXml.vb:132:        Private Function Atrybut(ByVal wezel As XmlNode, ByVal nazwa As String) As String
ImportCennikaXml.vb:133:            Dim a As XmlAttribute = wezel.Attributes(nazwa)
ImportCennikaXml.vb:134:            If a Is Nothing Then
ImportCennikaXml.vb:135:                Throw New XmlException("Brak atrybutu " & nazwa & " w elemencie " & wezel.Name)
ImportCennikaXml.vb:136:            End If
ImportCennikaXml.vb:137:            Return a.Value.Trim()
ImportCennikaXml.vb:138:        End Function
ImportCennikaXml.vb:139:
ImportCennikaXml.vb:140:        Private Function KaucjaBiezaca(ByVal numer As String) As Decimal
ImportCennikaXml.vb:141:            Dim dt As DataTable = DbHelper.WykonajZapytanie("SELECT Kaucja FROM Sprzet WHERE NumerInwentarzowy = @Numer",
ImportCennikaXml.vb:142:                DbHelper.Parametr("@Numer", numer))
ImportCennikaXml.vb:143:            If dt.Rows.Count = 0 Then Return 0D
ImportCennikaXml.vb:144:            Return CDec(dt.Rows(0)("Kaucja"))
ImportCennikaXml.vb:145:        End Function
ImportCennikaXml.vb:146:
ImportCennikaXml.vb:147:        Private Sub PrzeniesPlik(ByVal plik As String, ByVal docelowy As String)
ImportCennikaXml.vb:148:            Dim nazwa As String = Path.GetFileName(plik)
ImportCennikaXml.vb:149:            Dim cel As String = Path.Combine(docelowy, nazwa)
ImportCennikaXml.vb:150:            If File.Exists(cel) Then
ImportCennikaXml.vb:151:                cel = Path.Combine(docelowy, Path.GetFileNameWithoutExtension(nazwa) & "_" & DateTime.Now.ToString("yyyyMMddHHmmss") & ".xml")
ImportCennikaXml.vb:152:            End If
ImportCennikaXml.vb:153:            Try
ImportCennikaXml.vb:154:                File.Move(plik, cel)
ImportCennikaXml.vb:155:            Catch ex As IOException
ImportCennikaXml.vb:156:                OstatniBlad = ex.Message
ImportCennikaXml.vb:157:                Log.Error("Nie przeniesiono pliku " & plik, ex)
ImportCennikaXml.vb:158:                Throw
ImportCennikaXml.vb:159:            End Try
ImportCennikaXml.vb:160:        End Sub
ImportCennikaXml.vb:161:
ImportCennikaXml.vb:162:    End Class
ImportCennikaXml.vb:163:
ImportCennikaXml.vb:164:End Namespace
```

## M23 — surowy wynik H13

`git --no-pager -C 'legacy-app-parent\legacy-app\Jobs\EksportNocny' grep -n -I -e '^' -- 'Program.vb'`

```text
Program.vb:1:' Modul: Program.vb - zadanie nocne EksportNocny | Autor: zespol IT | 2016-10-12
Program.vb:2:' Uruchamiane z Harmonogramu zadan o 02:30: import cennika z katalogu XML, eksport CSV za wczoraj.
Program.vb:3:' 2017-06-12 JK: tryb import (cennik XML)
Program.vb:4:' 2018-04-03 PT: eksport do katalogu podanego jawnie, nie z Web.config
Program.vb:5:
Program.vb:6:Imports System.Diagnostics
Program.vb:7:Imports System.Globalization
Program.vb:8:Imports System.IO
Program.vb:9:Imports System.Threading
Program.vb:10:Imports Wypozyczalnia.Core.Integracje
Program.vb:11:
Program.vb:12:Module Program
Program.vb:13:
Program.vb:14:    Private Const KatalogImportu As String = "D:\Wypozyczalnia\import\cennik"
Program.vb:15:    Private Const KatalogEksportu As String = "\\plikownia\eksport$\wypozyczalnia"
Program.vb:16:    Private Const KatalogAwaryjny As String = "D:\Wypozyczalnia\eksport-awaryjny"
Program.vb:17:
Program.vb:18:    Private Const KodOk As Integer = 0
Program.vb:19:    Private Const KodBlad As Integer = 1
Program.vb:20:
Program.vb:21:    Sub Main(ByVal args() As String)
Program.vb:22:        Thread.CurrentThread.CurrentCulture = New CultureInfo("pl-PL")
Program.vb:23:        Thread.CurrentThread.CurrentUICulture = New CultureInfo("pl-PL")
Program.vb:24:
Program.vb:25:        Dim tryb As String = OdczytajTryb(args)
Program.vb:26:        Dim dzien As Date = OdczytajDzien(args)
Program.vb:27:        Dim kod As Integer = KodOk
Program.vb:28:        Dim stoper As Stopwatch = Stopwatch.StartNew()
Program.vb:29:
Program.vb:30:        Loguj("Start EksportNocny, tryb=" & tryb & ", dzien=" & dzien.ToString("yyyy-MM-dd"))
Program.vb:31:
Program.vb:32:        Select Case tryb
Program.vb:33:            Case "pelny"
Program.vb:34:                If Not Importuj() Then kod = KodBlad
Program.vb:35:                If Not Eksportuj(dzien) Then kod = KodBlad
Program.vb:36:            Case "import"
Program.vb:37:                If Not Importuj() Then kod = KodBlad
Program.vb:38:            Case "eksport"
Program.vb:39:                If Not Eksportuj(dzien) Then kod = KodBlad
Program.vb:40:            Case Else
Program.vb:41:                Loguj("Nieznany tryb: " & tryb)
Program.vb:42:                WypiszPomoc()
Program.vb:43:                kod = KodBlad
Program.vb:44:        End Select
Program.vb:45:
Program.vb:46:        If kod = KodOk AndAlso tryb <> "import" Then
Program.vb:47:            SprzatnijKopie(KatalogEksportu, 30)
Program.vb:48:        End If
Program.vb:49:
Program.vb:50:        stoper.Stop()
Program.vb:51:        Loguj("Czas wykonania: " & stoper.Elapsed.TotalSeconds.ToString("0.0") & " s")
Program.vb:52:        Loguj("Koniec EksportNocny, kod wyjscia " & kod)
Program.vb:53:        Environment.Exit(kod)
Program.vb:54:    End Sub
Program.vb:55:
Program.vb:56:    Private Function Importuj() As Boolean
Program.vb:57:        Try
Program.vb:58:            If Not Directory.Exists(KatalogImportu) Then
Program.vb:59:                Loguj("Brak katalogu importu " & KatalogImportu & " - pomijam import")
Program.vb:60:                Return True
Program.vb:61:            End If
Program.vb:62:            Dim import As New ImportCennikaXml()
Program.vb:63:            Dim ilePlikow As Integer = import.ImportujKatalog(KatalogImportu)
Program.vb:64:            Loguj("Import cennika: przetworzono plikow " & ilePlikow)
Program.vb:65:            Return True
Program.vb:66:        Catch ex As IOException
Program.vb:67:            Loguj("Blad dostepu do katalogu importu: " & ex.Message)
Program.vb:68:            Return False
Program.vb:69:        Catch ex As Exception
Program.vb:70:            Loguj("Blad importu cennika: " & ex.ToString())
Program.vb:71:            Return False
Program.vb:72:        End Try
Program.vb:73:    End Function
Program.vb:74:
Program.vb:75:    Private Function Eksportuj(ByVal dzien As Date) As Boolean
Program.vb:76:        Dim eksport As New EksportCsv()
Program.vb:77:        Try
Program.vb:78:            Dim plik As String = eksport.EksportujDzien(dzien, KatalogEksportu)
Program.vb:79:            If Not eksport.CzyWyeksportowano(dzien, KatalogEksportu) Then
Program.vb:80:                Loguj("Plik eksportu nie pojawil sie na udziale, zapis awaryjny do " & KatalogAwaryjny)
Program.vb:81:                plik = eksport.EksportujDzien(dzien, KatalogAwaryjny)
Program.vb:82:            End If
Program.vb:83:            Loguj("Eksport CSV: " & plik)
Program.vb:84:            Return True
Program.vb:85:        Catch ex As Exception
Program.vb:86:            Loguj("Blad eksportu CSV: " & ex.ToString())
Program.vb:87:            Return False
Program.vb:88:        End Try
Program.vb:89:    End Function
Program.vb:90:
Program.vb:91:    ' kopie .bak zostawiane przez eksport przy nadpisaniu pliku
Program.vb:92:    Private Sub SprzatnijKopie(ByVal katalog As String, ByVal dni As Integer)
Program.vb:93:        Try
Program.vb:94:            If Not Directory.Exists(katalog) Then Return
Program.vb:95:            Dim granica As Date = Date.Now.AddDays(-dni)
Program.vb:96:            Dim usuniete As Integer = 0
Program.vb:97:            For Each plik As String In Directory.GetFiles(katalog, "*.csv.bak")
Program.vb:98:                If File.GetLastWriteTime(plik) < granica Then
Program.vb:99:                    File.Delete(plik)
Program.vb:100:                    usuniete = usuniete + 1
Program.vb:101:                End If
Program.vb:102:            Next
Program.vb:103:            If usuniete > 0 Then
Program.vb:104:                Loguj("Usunieto starych kopii: " & usuniete)
Program.vb:105:            End If
Program.vb:106:        Catch ex As Exception
Program.vb:107:            Loguj("Nie udalo sie posprzatac kopii w " & katalog & ": " & ex.Message)
Program.vb:108:        End Try
Program.vb:109:    End Sub
Program.vb:110:
Program.vb:111:    Private Function OdczytajTryb(ByVal args() As String) As String
Program.vb:112:        If args Is Nothing Then Return "pelny"
Program.vb:113:        For Each arg As String In args
Program.vb:114:            If arg.StartsWith("/tryb:", StringComparison.OrdinalIgnoreCase) Then
Program.vb:115:                Return arg.Substring(6).Trim().ToLower()
Program.vb:116:            End If
Program.vb:117:        Next
Program.vb:118:        Return "pelny"
Program.vb:119:    End Function
Program.vb:120:
Program.vb:121:    ' domyslnie dzien poprzedni; /dzien:2018-04-02 do recznego powtorzenia eksportu
Program.vb:122:    Private Function OdczytajDzien(ByVal args() As String) As Date
Program.vb:123:        Dim wynik As Date = Date.Today.AddDays(-1)
Program.vb:124:        If args Is Nothing Then Return wynik
Program.vb:125:        For Each arg As String In args
Program.vb:126:            If arg.StartsWith("/dzien:", StringComparison.OrdinalIgnoreCase) Then
Program.vb:127:                Dim tekst As String = arg.Substring(7).Trim()
Program.vb:128:                Dim d As Date
Program.vb:129:                If Date.TryParseExact(tekst, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, d) Then
Program.vb:130:                    wynik = d
Program.vb:131:                Else
Program.vb:132:                    Loguj("Niepoprawna data w parametrze: " & tekst & " - uzywam " & wynik.ToString("yyyy-MM-dd"))
Program.vb:133:                End If
Program.vb:134:            End If
Program.vb:135:        Next
Program.vb:136:        Return wynik
Program.vb:137:    End Function
Program.vb:138:
Program.vb:139:    Private Sub WypiszPomoc()
Program.vb:140:        Console.WriteLine("Uzycie: EksportNocny.exe /tryb:pelny|import|eksport [/dzien:rrrr-mm-dd]")
Program.vb:141:        Console.WriteLine("  pelny   - import cennika XML, potem eksport CSV (domyslnie)")
Program.vb:142:        Console.WriteLine("  import  - tylko import cennika z " & KatalogImportu)
Program.vb:143:        Console.WriteLine("  eksport - tylko eksport CSV do " & KatalogEksportu)
Program.vb:144:    End Sub
Program.vb:145:
Program.vb:146:    Private Sub Loguj(ByVal tekst As String)
Program.vb:147:        Console.WriteLine(Date.Now.ToString("yyyy-MM-dd HH:mm:ss") & " " & tekst)
Program.vb:148:    End Sub
Program.vb:149:
Program.vb:150:End Module
```

## M24 — surowy wynik H14

`git --no-pager -C 'legacy-app-parent\legacy-app\Database' grep -n -I -e '^' -- '02_procedury.sql'`

```text
02_procedury.sql:1:-- =============================================================
02_procedury.sql:2:-- Wypozyczalnia - procedury skladowane
02_procedury.sql:3:-- Autor: zespol IT | 2014-03-11
02_procedury.sql:4:-- Historia:
02_procedury.sql:5:--   2015-02-04 MW: usp_PobierzTaryfe, usp_ZapiszTaryfe (taryfy z data obowiazywania)
02_procedury.sql:6:--   2016-09-21 JK: usp_WydajSprzet zamyka rezerwacje
02_procedury.sql:7:--   2018-01-15 PT: usp_WystawFakture - numeracja miesieczna FV/rrrr/mm/nnnn
02_procedury.sql:8:-- Uruchamiac po 01_schema.sql, przed 03_seed.sql.
02_procedury.sql:9:-- =============================================================
02_procedury.sql:10:
02_procedury.sql:11:USE Wypozyczalnia
02_procedury.sql:12:GO
02_procedury.sql:13:
02_procedury.sql:14:SET ANSI_NULLS ON
02_procedury.sql:15:GO
02_procedury.sql:16:SET QUOTED_IDENTIFIER ON
02_procedury.sql:17:GO
02_procedury.sql:18:
02_procedury.sql:19:-- -------------------------------------------------------------
02_procedury.sql:20:-- usp_SprawdzDostepnosc
02_procedury.sql:21:-- Zwraca wszystko, co koliduje z terminem dla danego sprzetu.
02_procedury.sql:22:-- Brak wierszy = sprzet dostepny.
02_procedury.sql:23:-- -------------------------------------------------------------
02_procedury.sql:24:IF OBJECT_ID('dbo.usp_SprawdzDostepnosc', 'P') IS NOT NULL
02_procedury.sql:25:    DROP PROCEDURE dbo.usp_SprawdzDostepnosc
02_procedury.sql:26:GO
02_procedury.sql:27:CREATE PROCEDURE dbo.usp_SprawdzDostepnosc @SprzetId INT, @DataOd DATETIME, @DataDo DATETIME
02_procedury.sql:28:AS
02_procedury.sql:29:BEGIN
02_procedury.sql:30:    SET NOCOUNT ON;
02_procedury.sql:31:
02_procedury.sql:32:    IF @DataDo <= @DataOd
02_procedury.sql:33:    BEGIN
02_procedury.sql:34:        RAISERROR('Nieprawidlowy zakres dat', 16, 1);
02_procedury.sql:35:        RETURN;
02_procedury.sql:36:    END
02_procedury.sql:37:
02_procedury.sql:38:    SELECT CAST('R' AS CHAR(1)) AS Zrodlo,
02_procedury.sql:39:           r.Id AS Id,
02_procedury.sql:40:           r.DataOd AS DataOd,
02_procedury.sql:41:           r.DataDo AS DataDo
02_procedury.sql:42:    FROM dbo.Rezerwacje r
02_procedury.sql:43:    WHERE r.SprzetId = @SprzetId
02_procedury.sql:44:      AND r.Status IN ('N', 'P')
02_procedury.sql:45:      AND r.DataOd < @DataDo
02_procedury.sql:46:      AND r.DataDo > @DataOd
02_procedury.sql:47:
02_procedury.sql:48:    UNION ALL
02_procedury.sql:49:
02_procedury.sql:50:    SELECT CAST('W' AS CHAR(1)) AS Zrodlo,
02_procedury.sql:51:           w.Id AS Id,
02_procedury.sql:52:           w.DataWydania AS DataOd,
02_procedury.sql:53:           w.PlanowanyZwrot AS DataDo
02_procedury.sql:54:    FROM dbo.Wypozyczenia w
02_procedury.sql:55:    WHERE w.SprzetId = @SprzetId
02_procedury.sql:56:      AND w.Status = 'O'
02_procedury.sql:57:      AND w.DataWydania < @DataDo
02_procedury.sql:58:      AND w.PlanowanyZwrot > @DataOd
02_procedury.sql:59:
02_procedury.sql:60:    UNION ALL
02_procedury.sql:61:
02_procedury.sql:62:    SELECT CAST('S' AS CHAR(1)) AS Zrodlo,
02_procedury.sql:63:           s.Id AS Id,
02_procedury.sql:64:           CAST(NULL AS DATETIME) AS DataOd,
02_procedury.sql:65:           CAST(NULL AS DATETIME) AS DataDo
02_procedury.sql:66:    FROM dbo.Sprzet s
02_procedury.sql:67:    WHERE s.Id = @SprzetId
02_procedury.sql:68:      AND s.Status IN ('S', 'X')
02_procedury.sql:69:
02_procedury.sql:70:    ORDER BY Zrodlo, DataOd;
02_procedury.sql:71:END
02_procedury.sql:72:GO
02_procedury.sql:73:
02_procedury.sql:74:-- -------------------------------------------------------------
02_procedury.sql:75:-- usp_ZapiszRezerwacje
02_procedury.sql:76:-- -------------------------------------------------------------
02_procedury.sql:77:IF OBJECT_ID('dbo.usp_ZapiszRezerwacje', 'P') IS NOT NULL
02_procedury.sql:78:    DROP PROCEDURE dbo.usp_ZapiszRezerwacje
02_procedury.sql:79:GO
02_procedury.sql:80:CREATE PROCEDURE dbo.usp_ZapiszRezerwacje @KlientId INT, @SprzetId INT, @DataOd DATETIME, @DataDo DATETIME, @CenaSzacunkowa DECIMAL(10,2), @Uwagi NVARCHAR(500), @NoweId INT OUTPUT
02_procedury.sql:81:AS
02_procedury.sql:82:BEGIN
02_procedury.sql:83:    SET NOCOUNT ON;
02_procedury.sql:84:
02_procedury.sql:85:    IF NOT EXISTS (SELECT 1 FROM dbo.Klienci WHERE Id = @KlientId)
02_procedury.sql:86:    BEGIN
02_procedury.sql:87:        RAISERROR('Nieznany klient %d', 16, 1, @KlientId);
02_procedury.sql:88:        RETURN;
02_procedury.sql:89:    END
02_procedury.sql:90:
02_procedury.sql:91:    IF NOT EXISTS (SELECT 1 FROM dbo.Sprzet WHERE Id = @SprzetId)
02_procedury.sql:92:    BEGIN
02_procedury.sql:93:        RAISERROR('Nieznany sprzet %d', 16, 1, @SprzetId);
02_procedury.sql:94:        RETURN;
02_procedury.sql:95:    END
02_procedury.sql:96:
02_procedury.sql:97:    BEGIN TRY
02_procedury.sql:98:        BEGIN TRANSACTION;
02_procedury.sql:99:
02_procedury.sql:100:        INSERT INTO dbo.Rezerwacje (KlientId, SprzetId, DataOd, DataDo, Status, CenaSzacunkowa, Uwagi, Utworzono)
02_procedury.sql:101:        VALUES (@KlientId, @SprzetId, @DataOd, @DataDo, 'N', @CenaSzacunkowa, @Uwagi, GETDATE());
02_procedury.sql:102:
02_procedury.sql:103:        SET @NoweId = CAST(SCOPE_IDENTITY() AS INT);
02_procedury.sql:104:
02_procedury.sql:105:        INSERT INTO dbo.DziennikOperacji (Kiedy, Operacja, Szczegoly)
02_procedury.sql:106:        VALUES (GETDATE(), 'REZERWACJA',
02_procedury.sql:107:                N'Rezerwacja ' + CAST(@NoweId AS NVARCHAR(10)) + N', klient ' + CAST(@KlientId AS NVARCHAR(10))
02_procedury.sql:108:                + N', sprzet ' + CAST(@SprzetId AS NVARCHAR(10)));
02_procedury.sql:109:
02_procedury.sql:110:        COMMIT TRANSACTION;
02_procedury.sql:111:    END TRY
02_procedury.sql:112:    BEGIN CATCH
02_procedury.sql:113:        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
02_procedury.sql:114:        DECLARE @Blad NVARCHAR(2048) = ERROR_MESSAGE();
02_procedury.sql:115:        RAISERROR(@Blad, 16, 1);
02_procedury.sql:116:    END CATCH
02_procedury.sql:117:END
02_procedury.sql:118:GO
02_procedury.sql:119:
02_procedury.sql:120:-- -------------------------------------------------------------
02_procedury.sql:121:-- usp_ZmienStatusRezerwacji
02_procedury.sql:122:-- -------------------------------------------------------------
02_procedury.sql:123:IF OBJECT_ID('dbo.usp_ZmienStatusRezerwacji', 'P') IS NOT NULL
02_procedury.sql:124:    DROP PROCEDURE dbo.usp_ZmienStatusRezerwacji
02_procedury.sql:125:GO
02_procedury.sql:126:CREATE PROCEDURE dbo.usp_ZmienStatusRezerwacji @RezerwacjaId INT, @Status CHAR(1)
02_procedury.sql:127:AS
02_procedury.sql:128:BEGIN
02_procedury.sql:129:    SET NOCOUNT ON;
02_procedury.sql:130:
02_procedury.sql:131:    IF @Status NOT IN ('N', 'P', 'A', 'Z')
02_procedury.sql:132:    BEGIN
02_procedury.sql:133:        RAISERROR('Nieprawidlowy status rezerwacji: %s', 16, 1, @Status);
02_procedury.sql:134:        RETURN;
02_procedury.sql:135:    END
02_procedury.sql:136:
02_procedury.sql:137:    DECLARE @StaryStatus CHAR(1);
02_procedury.sql:138:    SELECT @StaryStatus = Status FROM dbo.Rezerwacje WHERE Id = @RezerwacjaId;
02_procedury.sql:139:
02_procedury.sql:140:    IF @StaryStatus IS NULL
02_procedury.sql:141:    BEGIN
02_procedury.sql:142:        RAISERROR('Nieznana rezerwacja %d', 16, 1, @RezerwacjaId);
02_procedury.sql:143:        RETURN;
02_procedury.sql:144:    END
02_procedury.sql:145:
02_procedury.sql:146:    UPDATE dbo.Rezerwacje
02_procedury.sql:147:    SET Status = @Status
02_procedury.sql:148:    WHERE Id = @RezerwacjaId;
02_procedury.sql:149:
02_procedury.sql:150:    INSERT INTO dbo.DziennikOperacji (Kiedy, Operacja, Szczegoly)
02_procedury.sql:151:    VALUES (GETDATE(), 'STATUS_REZERWACJI',
02_procedury.sql:152:            N'Rezerwacja ' + CAST(@RezerwacjaId AS NVARCHAR(10)) + N': ' + @StaryStatus + N' -> ' + @Status);
02_procedury.sql:153:END
02_procedury.sql:154:GO
02_procedury.sql:155:
02_procedury.sql:156:-- -------------------------------------------------------------
02_procedury.sql:157:-- usp_WydajSprzet
02_procedury.sql:158:-- -------------------------------------------------------------
02_procedury.sql:159:IF OBJECT_ID('dbo.usp_WydajSprzet', 'P') IS NOT NULL
02_procedury.sql:160:    DROP PROCEDURE dbo.usp_WydajSprzet
02_procedury.sql:161:GO
02_procedury.sql:162:CREATE PROCEDURE dbo.usp_WydajSprzet @RezerwacjaId INT = NULL, @KlientId INT, @SprzetId INT, @DataWydania DATETIME, @PlanowanyZwrot DATETIME, @KaucjaPobrana DECIMAL(10,2), @NoweId INT OUTPUT
02_procedury.sql:163:AS
02_procedury.sql:164:BEGIN
02_procedury.sql:165:    SET NOCOUNT ON;
02_procedury.sql:166:
02_procedury.sql:167:    IF @RezerwacjaId = 0 SET @RezerwacjaId = NULL;
02_procedury.sql:168:
02_procedury.sql:169:    IF @PlanowanyZwrot <= @DataWydania
02_procedury.sql:170:    BEGIN
02_procedury.sql:171:        RAISERROR('Planowany zwrot musi byc pozniejszy niz wydanie', 16, 1);
02_procedury.sql:172:        RETURN;
02_procedury.sql:173:    END
02_procedury.sql:174:
02_procedury.sql:175:    BEGIN TRY
02_procedury.sql:176:        BEGIN TRANSACTION;
02_procedury.sql:177:
02_procedury.sql:178:        INSERT INTO dbo.Wypozyczenia (RezerwacjaId, KlientId, SprzetId, DataWydania, PlanowanyZwrot, KaucjaPobrana, Status)
02_procedury.sql:179:        VALUES (@RezerwacjaId, @KlientId, @SprzetId, @DataWydania, @PlanowanyZwrot, @KaucjaPobrana, 'O');
02_procedury.sql:180:
02_procedury.sql:181:        SET @NoweId = CAST(SCOPE_IDENTITY() AS INT);
02_procedury.sql:182:
02_procedury.sql:183:        UPDATE dbo.Sprzet
02_procedury.sql:184:        SET Status = 'W'
02_procedury.sql:185:        WHERE Id = @SprzetId;
02_procedury.sql:186:
02_procedury.sql:187:        IF @RezerwacjaId IS NOT NULL
02_procedury.sql:188:        BEGIN
02_procedury.sql:189:            UPDATE dbo.Rezerwacje
02_procedury.sql:190:            SET Status = 'Z'
02_procedury.sql:191:            WHERE Id = @RezerwacjaId;
02_procedury.sql:192:        END
02_procedury.sql:193:
02_procedury.sql:194:        INSERT INTO dbo.DziennikOperacji (Kiedy, Operacja, Szczegoly)
02_procedury.sql:195:        VALUES (GETDATE(), 'WYDANIE',
02_procedury.sql:196:                N'Wypozyczenie ' + CAST(@NoweId AS NVARCHAR(10)) + N', sprzet ' + CAST(@SprzetId AS NVARCHAR(10))
02_procedury.sql:197:                + ISNULL(N', rezerwacja ' + CAST(@RezerwacjaId AS NVARCHAR(10)), N''));
02_procedury.sql:198:
02_procedury.sql:199:        COMMIT TRANSACTION;
02_procedury.sql:200:    END TRY
02_procedury.sql:201:    BEGIN CATCH
02_procedury.sql:202:        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
02_procedury.sql:203:        DECLARE @Blad NVARCHAR(2048) = ERROR_MESSAGE();
02_procedury.sql:204:        RAISERROR(@Blad, 16, 1);
02_procedury.sql:205:    END CATCH
02_procedury.sql:206:END
02_procedury.sql:207:GO
02_procedury.sql:208:
02_procedury.sql:209:-- -------------------------------------------------------------
02_procedury.sql:210:-- usp_ZarejestrujZwrot
02_procedury.sql:211:-- -------------------------------------------------------------
02_procedury.sql:212:IF OBJECT_ID('dbo.usp_ZarejestrujZwrot', 'P') IS NOT NULL
02_procedury.sql:213:    DROP PROCEDURE dbo.usp_ZarejestrujZwrot
02_procedury.sql:214:GO
02_procedury.sql:215:CREATE PROCEDURE dbo.usp_ZarejestrujZwrot @WypozyczenieId INT, @DataZwrotu DATETIME, @Oplata DECIMAL(10,2), @Kara DECIMAL(10,2), @KaucjaZwrocona DECIMAL(10,2)
02_procedury.sql:216:AS
02_procedury.sql:217:BEGIN
02_procedury.sql:218:    SET NOCOUNT ON;
02_procedury.sql:219:
02_procedury.sql:220:    DECLARE @SprzetId INT;
02_procedury.sql:221:    DECLARE @Status CHAR(1);
02_procedury.sql:222:
02_procedury.sql:223:    SELECT @SprzetId = SprzetId, @Status = Status
02_procedury.sql:224:    FROM dbo.Wypozyczenia
02_procedury.sql:225:    WHERE Id = @WypozyczenieId;
02_procedury.sql:226:
02_procedury.sql:227:    IF @SprzetId IS NULL
02_procedury.sql:228:    BEGIN
02_procedury.sql:229:        RAISERROR('Nieznane wypozyczenie %d', 16, 1, @WypozyczenieId);
02_procedury.sql:230:        RETURN;
02_procedury.sql:231:    END
02_procedury.sql:232:
02_procedury.sql:233:    IF @Status = 'Z'
02_procedury.sql:234:    BEGIN
02_procedury.sql:235:        RAISERROR('Wypozyczenie %d jest juz zamkniete', 16, 1, @WypozyczenieId);
02_procedury.sql:236:        RETURN;
02_procedury.sql:237:    END
02_procedury.sql:238:
02_procedury.sql:239:    BEGIN TRY
02_procedury.sql:240:        BEGIN TRANSACTION;
02_procedury.sql:241:
02_procedury.sql:242:        UPDATE dbo.Wypozyczenia
02_procedury.sql:243:        SET DataZwrotu = @DataZwrotu,
02_procedury.sql:244:            Oplata = @Oplata,
02_procedury.sql:245:            Kara = @Kara,
02_procedury.sql:246:            KaucjaZwrocona = @KaucjaZwrocona,
02_procedury.sql:247:            Status = 'Z'
02_procedury.sql:248:        WHERE Id = @WypozyczenieId;
02_procedury.sql:249:
02_procedury.sql:250:        UPDATE dbo.Sprzet
02_procedury.sql:251:        SET Status = 'D'
02_procedury.sql:252:        WHERE Id = @SprzetId;
02_procedury.sql:253:
02_procedury.sql:254:        INSERT INTO dbo.DziennikOperacji (Kiedy, Operacja, Szczegoly)
02_procedury.sql:255:        VALUES (GETDATE(), 'ZWROT',
02_procedury.sql:256:                N'Wypozyczenie ' + CAST(@WypozyczenieId AS NVARCHAR(10)) + N', oplata ' + CAST(@Oplata AS NVARCHAR(20))
02_procedury.sql:257:                + N', kara ' + CAST(@Kara AS NVARCHAR(20)));
02_procedury.sql:258:
02_procedury.sql:259:        COMMIT TRANSACTION;
02_procedury.sql:260:    END TRY
02_procedury.sql:261:    BEGIN CATCH
02_procedury.sql:262:        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
02_procedury.sql:263:        DECLARE @Blad NVARCHAR(2048) = ERROR_MESSAGE();
02_procedury.sql:264:        RAISERROR(@Blad, 16, 1);
02_procedury.sql:265:    END CATCH
02_procedury.sql:266:END
02_procedury.sql:267:GO
02_procedury.sql:268:
02_procedury.sql:269:-- -------------------------------------------------------------
02_procedury.sql:270:-- usp_PobierzTaryfe
02_procedury.sql:271:-- Taryfa obowiazujaca w danym dniu (najnowsza z ObowiazujeOd <= dzien).
02_procedury.sql:272:-- -------------------------------------------------------------
02_procedury.sql:273:IF OBJECT_ID('dbo.usp_PobierzTaryfe', 'P') IS NOT NULL
02_procedury.sql:274:    DROP PROCEDURE dbo.usp_PobierzTaryfe
02_procedury.sql:275:GO
02_procedury.sql:276:CREATE PROCEDURE dbo.usp_PobierzTaryfe @Kategoria CHAR(3), @NaDzien DATE
02_procedury.sql:277:AS
02_procedury.sql:278:BEGIN
02_procedury.sql:279:    SET NOCOUNT ON;
02_procedury.sql:280:
02_procedury.sql:281:    SELECT TOP 1
02_procedury.sql:282:           Id,
02_procedury.sql:283:           Kategoria,
02_procedury.sql:284:           MnoznikWeekend,
02_procedury.sql:285:           RabatTygodniowyProc,
02_procedury.sql:286:           ObowiazujeOd
02_procedury.sql:287:    FROM dbo.Taryfy
02_procedury.sql:288:    WHERE Kategoria = @Kategoria
02_procedury.sql:289:      AND ObowiazujeOd <= @NaDzien
02_procedury.sql:290:    ORDER BY ObowiazujeOd DESC;
02_procedury.sql:291:END
02_procedury.sql:292:GO
02_procedury.sql:293:
02_procedury.sql:294:-- -------------------------------------------------------------
02_procedury.sql:295:-- usp_ZapiszTaryfe
02_procedury.sql:296:-- Upsert po (Kategoria, ObowiazujeOd).
02_procedury.sql:297:-- -------------------------------------------------------------
02_procedury.sql:298:IF OBJECT_ID('dbo.usp_ZapiszTaryfe', 'P') IS NOT NULL
02_procedury.sql:299:    DROP PROCEDURE dbo.usp_ZapiszTaryfe
02_procedury.sql:300:GO
02_procedury.sql:301:CREATE PROCEDURE dbo.usp_ZapiszTaryfe @Kategoria CHAR(3), @MnoznikWeekend DECIMAL(4,2), @RabatTygodniowyProc DECIMAL(5,2), @ObowiazujeOd DATE
02_procedury.sql:302:AS
02_procedury.sql:303:BEGIN
02_procedury.sql:304:    SET NOCOUNT ON;
02_procedury.sql:305:
02_procedury.sql:306:    IF @MnoznikWeekend <= 0 OR @RabatTygodniowyProc < 0
02_procedury.sql:307:    BEGIN
02_procedury.sql:308:        RAISERROR('Nieprawidlowe parametry taryfy', 16, 1);
02_procedury.sql:309:        RETURN;
02_procedury.sql:310:    END
02_procedury.sql:311:
02_procedury.sql:312:    IF EXISTS (SELECT 1 FROM dbo.Taryfy WHERE Kategoria = @Kategoria AND ObowiazujeOd = @ObowiazujeOd)
02_procedury.sql:313:    BEGIN
02_procedury.sql:314:        UPDATE dbo.Taryfy
02_procedury.sql:315:        SET MnoznikWeekend = @MnoznikWeekend,
02_procedury.sql:316:            RabatTygodniowyProc = @RabatTygodniowyProc
02_procedury.sql:317:        WHERE Kategoria = @Kategoria
02_procedury.sql:318:          AND ObowiazujeOd = @ObowiazujeOd;
02_procedury.sql:319:    END
02_procedury.sql:320:    ELSE
02_procedury.sql:321:    BEGIN
02_procedury.sql:322:        INSERT INTO dbo.Taryfy (Kategoria, MnoznikWeekend, RabatTygodniowyProc, ObowiazujeOd)
02_procedury.sql:323:        VALUES (@Kategoria, @MnoznikWeekend, @RabatTygodniowyProc, @ObowiazujeOd);
02_procedury.sql:324:    END
02_procedury.sql:325:
02_procedury.sql:326:    INSERT INTO dbo.DziennikOperacji (Kiedy, Operacja, Szczegoly)
02_procedury.sql:327:    VALUES (GETDATE(), 'TARYFA',
02_procedury.sql:328:            @Kategoria + N' od ' + CONVERT(NVARCHAR(10), @ObowiazujeOd, 120));
02_procedury.sql:329:END
02_procedury.sql:330:GO
02_procedury.sql:331:
02_procedury.sql:332:-- -------------------------------------------------------------
02_procedury.sql:333:-- usp_WystawFakture
02_procedury.sql:334:-- Numer: FV/rrrr/mm/nnnn, numeracja od 1 w kazdym miesiacu.
02_procedury.sql:335:-- -------------------------------------------------------------
02_procedury.sql:336:IF OBJECT_ID('dbo.usp_WystawFakture', 'P') IS NOT NULL
02_procedury.sql:337:    DROP PROCEDURE dbo.usp_WystawFakture
02_procedury.sql:338:GO
02_procedury.sql:339:CREATE PROCEDURE dbo.usp_WystawFakture @KlientId INT, @WypozyczenieId INT, @Netto DECIMAL(10,2), @Vat DECIMAL(10,2), @Brutto DECIMAL(10,2), @NoweId INT OUTPUT, @Numer VARCHAR(30) OUTPUT
02_procedury.sql:340:AS
02_procedury.sql:341:BEGIN
02_procedury.sql:342:    SET NOCOUNT ON;
02_procedury.sql:343:
02_procedury.sql:344:    DECLARE @Teraz DATETIME = GETDATE();
02_procedury.sql:345:    DECLARE @Rok INT = YEAR(@Teraz);
02_procedury.sql:346:    DECLARE @Miesiac INT = MONTH(@Teraz);
02_procedury.sql:347:    DECLARE @Kolejny INT;
02_procedury.sql:348:
02_procedury.sql:349:    BEGIN TRY
02_procedury.sql:350:        BEGIN TRANSACTION;
02_procedury.sql:351:
02_procedury.sql:352:        SELECT @Kolejny = COUNT(*) + 1
02_procedury.sql:353:        FROM dbo.Faktury WITH (UPDLOCK, HOLDLOCK)
02_procedury.sql:354:        WHERE YEAR(DataWystawienia) = @Rok
02_procedury.sql:355:          AND MONTH(DataWystawienia) = @Miesiac;
02_procedury.sql:356:
02_procedury.sql:357:        SET @Numer = 'FV/' + CAST(@Rok AS VARCHAR(4)) + '/'
02_procedury.sql:358:                   + RIGHT('0' + CAST(@Miesiac AS VARCHAR(2)), 2) + '/'
02_procedury.sql:359:                   + RIGHT('0000' + CAST(@Kolejny AS VARCHAR(4)), 4);
02_procedury.sql:360:
02_procedury.sql:361:        INSERT INTO dbo.Faktury (Numer, KlientId, WypozyczenieId, DataWystawienia, Netto, Vat, Brutto, Status)
02_procedury.sql:362:        VALUES (@Numer, @KlientId, @WypozyczenieId, @Teraz, @Netto, @Vat, @Brutto, 'W');
02_procedury.sql:363:
02_procedury.sql:364:        SET @NoweId = CAST(SCOPE_IDENTITY() AS INT);
02_procedury.sql:365:
02_procedury.sql:366:        INSERT INTO dbo.DziennikOperacji (Kiedy, Operacja, Szczegoly)
02_procedury.sql:367:        VALUES (@Teraz, 'FAKTURA',
02_procedury.sql:368:                N'Faktura ' + @Numer + N', wypozyczenie ' + CAST(@WypozyczenieId AS NVARCHAR(10)));
02_procedury.sql:369:
02_procedury.sql:370:        COMMIT TRANSACTION;
02_procedury.sql:371:    END TRY
02_procedury.sql:372:    BEGIN CATCH
02_procedury.sql:373:        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
02_procedury.sql:374:        DECLARE @Blad NVARCHAR(2048) = ERROR_MESSAGE();
02_procedury.sql:375:        RAISERROR(@Blad, 16, 1);
02_procedury.sql:376:    END CATCH
02_procedury.sql:377:END
02_procedury.sql:378:GO
```

