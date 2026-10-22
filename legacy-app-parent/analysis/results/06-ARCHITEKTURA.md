# Architektura legacy — weryfikacja hipotez

## 1) Bilans

**Potwierdzone: 14; częściowo potwierdzone: 0; obalone: 0; nieznane: 0.** Liczymy wyłącznie H1–H14 według ich operacyjnych kryteriów z rozdziału 5 raportu 03, nie wszystkie przypuszczenia z jego diagramu. Zestawienie dowodów i zastrzeżeń znajduje się w sekcji 2 poniżej. Oryginalna pewność jest oceną sprzed lektury kodu, nie aktualnym prawdopodobieństwem ani wynikiem pomiaru.

W szczególności H2 wymaga istnienia delegacji, nie wyłączności tej drogi, a H8 obecności obliczeń w PricingManager, nie pełnej centralizacji cen. Dlatego wyjątki nie obalają tych wąskich hipotez; obalają za to mocniejszy obraz szczelnych warstw i jednego miejsca wszystkich reguł (03, sekcja 5, H2 i H8; 05, sekcja 3, W1–W4).

Zakres to analiza statyczna repozytorium. M3 z 04 liczy wybrane konstrukcje tekstowe; nie jest pełnym grafem wywołań. Przykładowo program rzeczywiście tworzy adaptery importu i eksportu, choć w M3 ma „—” w kolumnie konstruktorów: `legacy-app-parent/legacy-app/Jobs/EksportNocny/Program.vb:66-83` (04, sekcja M3). Nie uruchamiano ani nie kompilowano legacy.

## 2) Weryfikacja H1–H14

Treść i pewności pochodzą z niezmienionego 03, sekcja 5. M11–M24 w 04 zawierają odpowiednio materiał dla H1–H14 (04, wstęp).

| Hipoteza i oryginalna pewność | Wynik | Dowód oraz granica wniosku |
|---|---|---|
| H1 — model zdarzeniowy WebForms — **95%** | Potwierdzona | 04, M11: dyrektywy stron, dziedziczenie po Page i wiązania handlerów. Konkretne zdarzenie przycisku płatności: `legacy-app-parent/legacy-app/WypozyczalniaWeb/Faktura.aspx.vb:209`. Nie oznacza to MVC. |
| H2 — strony delegują do managerów Core — **85%** | Potwierdzona | 04, M12: Klienci → CustomerManager, Sprzet → EquipmentManager, Rezerwacja → ReservationManager, Wypozyczenie → RentalManager, Cennik → PricingManager, Faktura → InvoiceManager; materiał obejmuje wykonywane metody, nie tylko importy. Wyjątki SQL w UI: 05, W2. |
| H3 — pięć managerów używa odpowiadających DAL — **85%** | Potwierdzona | 04, M13: wywołania EquipmentDAL, ReservationDAL, RentalDAL, PricingDAL i InvoiceDAL z odpowiednich managerów. Przykład RentalManager → RentalDAL.Wydaj: `legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:64-65`. Nie jest to teza o wyłączności zależności. |
| H4 — współdzielony DbHelper wykonuje SQL — **80%** | Potwierdzona | Połączenia i polecenia tekstowe/procedury: `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/DbHelper.vb:43-94`; klienci w różnych DAL: `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/EquipmentDAL.vb:17-18`, `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/PricingDAL.vb:15-17` (04, M14). |
| H5 — VB wywołuje procedury ze skryptu — **75%** | Potwierdzona | InvoiceDAL przekazuje `usp_WystawFakture`: `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/InvoiceDAL.vb:20-26`; definicja: `legacy-app-parent/legacy-app/Database/02_procedury.sql:339`; wykonanie jako StoredProcedure: `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/DbHelper.vb:85-94` (04, M15). |
| H6 — CustomerManager sam inicjuje dostęp do bazy — **65%** | Potwierdzona | SELECT przez DbHelper: `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:17-19`; zapis klienta: `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:76-90` (04, M16; 05, W1). Brak dedykowanego DAL nie jest jedynym argumentem. |
| H7 — wydanie i zwrot współdzielą RentalManager — **80%** | Potwierdzona | Wydanie: `legacy-app-parent/legacy-app/WypozyczalniaWeb/Wypozyczenie.aspx.vb:213-214`; zwrot: `legacy-app-parent/legacy-app/WypozyczalniaWeb/Zwrot.aspx.vb:212-243` (04, M17). |
| H8 — PricingManager zawiera istotne obliczenia cen — **60%** | Potwierdzona | Próg rabatu, procent i zaokrąglenie: `legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:47-59` (04, M18). Kopia szacunku w UI i różne zaokrąglenia nie pozwalają mówić o kompletnej centralizacji: 05, W3–W4. |
| H9 — sześć plików modeli przenosi stan bez I/O — **65%** | Potwierdzona | 04, M19: pełne implementacje Klient, Sprzet, Rezerwacja, Wypozyczenie, Taryfa i Faktura nie wykonują SQL, sieci ani plików. Mają jednak zachowanie lokalne, np. walidację kategorii i kopię taryfy: `legacy-app-parent/legacy-app/WypozyczalniaCore/Model/Taryfa.vb:33-68`. Nie nazywamy ich wyłącznie pustymi DTO. |
| H10 — code-behind bezpośrednio inicjuje SOAP — **55%** | Potwierdzona | Handler tworzy BramkaService i wywołuje ZarejestrujPlatnosc: `legacy-app-parent/legacy-app/WypozyczalniaWeb/Faktura.aspx.vb:209-232` (04, M20). |
| H11 — KurierClient realizuje HTTP — **85%** | Potwierdzona | POST JSON i zapis do strumienia: `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/KurierClient.vb:28-49`; budowanie żądania i odbiór: `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/KurierClient.vb:100-117` (04, M21). |
| H12 — import XML i eksport CSV wykonują I/O plikowe — **85%** | Potwierdzona | Load z podanej ścieżki: `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/ImportCennikaXml.vb:43-45`; kopia i zapis pliku: `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/EksportCsv.vb:73-76` (04, M22). |
| H13 — pełny tryb zadania uruchamia oba adaptery Core — **90%** | Potwierdzona | Rozpoznanie `/tryb:`: `legacy-app-parent/legacy-app/Jobs/EksportNocny/Program.vb:113-121`; pelny uruchamia import, potem eksport: `legacy-app-parent/legacy-app/Jobs/EksportNocny/Program.vb:32-39`; implementacje delegacji: `legacy-app-parent/legacy-app/Jobs/EksportNocny/Program.vb:59-95` (04, M23). Błąd importu nie blokuje próby eksportu. |
| H14 — procedury zawierają reguły biznesowe — **65%** | Potwierdzona | Dostępność zależy od statusów i nakładania terminów: `legacy-app-parent/legacy-app/Database/02_procedury.sql:38-70`; numeracja faktur jest miesięczna: `legacy-app-parent/legacy-app/Database/02_procedury.sql:344-362` (04, M24). To więcej niż techniczny CRUD. |

## 3) Potwierdzona architektura

### Wzorce i udział linii

Poniższe wartości są **wyłącznie udziałami niepustych linii końcowego silnika z 05, sekcja 2**: 52 pliki, 6939 linii, pokrycie 100,0%. Nie są udziałami reguł biznesowych, czasu wykonania ani liniami `code` z CLOC. Nazwa kategorii pliku nie dowodzi czystości wzorca; odchylenia opisuje 05, sekcja 3.

| Wzorzec / kategoria końcowego silnika | Linie | % linii |
|---|---:|---:|
| WebForms — markup strony | 615 | 8,9% |
| WebForms — code-behind | 1647 | 23,7% |
| WebForms — designer | 66 | 1,0% |
| Cykl życia aplikacji Web | 54 | 0,8% |
| Proxy SOAP | 108 | 1,6% |
| Kontrakty integracji SOAP | 92 | 1,3% |
| Manager aplikacyjny | 1425 | 20,5% |
| Table Data Gateway / DAL | 865 | 12,5% |
| Model domenowy | 377 | 5,4% |
| Adapter integracyjny | 386 | 5,6% |
| Zadanie wsadowe | 132 | 1,9% |
| Projekt .NET | 300 | 4,3% |
| Solucja Visual Studio | 36 | 0,5% |
| Konfiguracja aplikacji | 104 | 1,5% |
| Skrypt bazy danych | 652 | 9,4% |
| Harmonogram zadania | 44 | 0,6% |
| Notatka wdrożeniowa | 36 | 0,5% |

Organizacja jest warstwowa **z obejściami**, nie szczelna Web → Manager → DAL → SQL. Delegacje potwierdzają H2–H5, ale CustomerManager łączy reguły i dostęp do danych, UI wykonuje SQL, a import korzysta bezpośrednio z DAL/helpera (05, W1–W2; `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/ImportCennikaXml.vb:56-70`, `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/ImportCennikaXml.vb:140-143`). Nie ma podstaw do utożsamiania tego z ORM/Repository czy całościową architekturą Ports and Adapters.

Managery mają rolę usług aplikacyjnych i sekwencyjnych skryptów transakcyjnych: wydanie składa zapis DAL, zmiany statusów, zamówienie kuriera i dziennik (`legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:64-87`). To interpretacja odpowiedzialności, nie dodatkowy procent pomiaru. Model ma stan i lokalne metody, a część decyzji pozostaje w SQL (H9 i H14). Obliczenia występują także w UI; komentarz o karze nie zgadza się z mnożnikiem w kodzie (05, W3–W5).

### Wejścia, wyjścia i granice procesów

| Granica | Potwierdzony kierunek i przepływ | Dowód |
|---|---|---|
| UI | Strony WebForms przyjmują zdarzenia formularzy; strona domyślna to Klienci.aspx. Sesja Web jest InProc z timeoutem 60 minut. | 04, M11; `legacy-app-parent/legacy-app/WypozyczalniaWeb/Web.config:31-33`, `legacy-app-parent/legacy-app/WypozyczalniaWeb/Web.config:44-48` |
| Web / Core | UI wywołuje klasy Core; nie jest to dowód odrębnego serwera usług. Zadanie ma osobny punkt wejścia Main i również używa Core. | 04, M12; `legacy-app-parent/legacy-app/Jobs/EksportNocny/Program.vb:21-39`, `legacy-app-parent/legacy-app/Jobs/EksportNocny/Program.vb:66-83` |
| Baza | Wyjście do SQL Server, wejście wyników i parametrów wyjściowych; ADO.NET wykonuje tekst SQL i procedury. W konfiguracji Web baza Wypozyczalnia, Integrated Security. | `legacy-app-parent/legacy-app/WypozyczalniaWeb/Web.config:11-15`; `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/DbHelper.vb:43-94`; `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/InvoiceDAL.vb:17-29` |
| SOAP płatności | Web → zewnętrzna bramka: numer, brutto, waluta; odpowiedź zawiera status i identyfikator transakcji. Przy OK UI zleca oznaczenie faktury opłaconej. Adres proxy jest czytany z konfiguracji. | `legacy-app-parent/legacy-app/WypozyczalniaWeb/Faktura.aspx.vb:225-236`; `legacy-app-parent/legacy-app/WypozyczalniaWeb/Web References/BramkaPlatnosci/Reference.vb:32-48` |
| REST/HTTP kuriera | Core → kurier: POST JSON; odpowiedź daje numer przesyłki. Klient posiada też GET statusu i DELETE anulowania. Wydanie wywołuje zamówienie warunkowo, jeśli podano adres dostawy. Nie jest to serwer REST tej aplikacji. | `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/KurierClient.vb:28-86`; `legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:73-84`; adres HTTPS: `legacy-app-parent/legacy-app/WypozyczalniaWeb/Web.config:18` |
| Pliki — wejście | Import czyta posortowane XML, parsuje cennik, zapisuje taryfy i stawki sprzętu, przenosi przetworzone pliki. | `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/ImportCennikaXml.vb:22-90`, `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/ImportCennikaXml.vb:147-154` |
| Pliki — wyjście | Eksport pobiera dane przez RentalManager i InvoiceManager, zapisuje CSV w Windows-1250, przed nadpisaniem kopiuje poprzedni plik do .bak. Zadanie używa stałych ścieżek importu, udziału UNC i katalogu awaryjnego. | `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/EksportCsv.vb:29-76`; `legacy-app-parent/legacy-app/Jobs/EksportNocny/Program.vb:14-16` |
| Harmonogram | Definicja Windows Task Scheduler: codziennie od 02:30, EksportNocny.exe `/tryb:pelny`, konto svc-eksport, IgnoreNew. Zadanie domyślnie liczy poprzedni dzień. Definicja nie dowodzi faktycznej rejestracji ani wykonania. | `legacy-app-parent/legacy-app/Jobs/EksportNocny/zadanie-harmonogramu.xml:9-25`, `legacy-app-parent/legacy-app/Jobs/EksportNocny/zadanie-harmonogramu.xml:37-41`; `legacy-app-parent/legacy-app/Jobs/EksportNocny/Program.vb:124-126` |

### Authentication a authorization

- **Authentication (uwierzytelnianie):** Web deklaruje Windows authentication; połączenie SQL Integrated Security. To wybór mechanizmu tożsamości, nie lista uprawnień użytkowników (`legacy-app-parent/legacy-app/WypozyczalniaWeb/Web.config:11-15`, `legacy-app-parent/legacy-app/WypozyczalniaWeb/Web.config:33`). Rzeczywiste ustawienia IIS i tożsamość połączenia pozostają niewiadome.
- **Authorization (autoryzacja):** w pokazanym system.web nie ma reguł allow/deny ani mapowania ról. Sama deklaracja authentication nie dowodzi kontroli operacji per rola (`legacy-app-parent/legacy-app/WypozyczalniaWeb/Web.config:27-39`). Wybór klienta w Session i warunek zgodności klienta z wypożyczeniem to stan/kontrola kontekstu biznesowego, nie dowód identyfikacji zalogowanego pracownika (`legacy-app-parent/legacy-app/WypozyczalniaWeb/Faktura.aspx.vb:110-113`; 04, M9). Nie potwierdzono pełnej macierzy uprawnień ani zabezpieczeń poza repo.
- **Integracje:** KurierClient dodaje X-Api-Key; to poświadczenie integracji, nie autoryzacja operatora Web (`legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/KurierClient.vb:100-108`). Proxy SOAP warunkowo ustawia UseDefaultCredentials dla adresu uznanego za lokalny; nie dowodzi to uwierzytelniania bramki na wdrożeniu (`legacy-app-parent/legacy-app/WypozyczalniaWeb/Web References/BramkaPlatnosci/Reference.vb:39-48`).

## 4) Kto dotyka której tabeli

R = odczyt, W = zapis (INSERT/UPDATE/DELETE). Pierwsza tabela dotyczy **jawnego SQL komponentu**, druga skutków procedur. Nie przypisujemy managerowi każdej tabeli tylko z jego nazwy ani nie uznajemy JOIN za zapis. DbHelper jest wykonawcą poleceń przekazanych przez klientów (`legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/DbHelper.vb:43-94`).

| Komponent z jawnym SQL | Tabele i operacje | Dowody |
|---|---|---|
| CustomerManager | Klienci R/W; Wypozyczenia R; DziennikOperacji W | `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:17-19`, `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:76-114`, `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:128`, `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:188` |
| EquipmentDAL | Sprzet R/W; Wypozyczenia R; Klienci R; DziennikOperacji W | `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/EquipmentDAL.vb:17-18`, `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/EquipmentDAL.vb:75-107`, `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/EquipmentDAL.vb:129-145` |
| ReservationDAL | Rezerwacje R/W; Sprzet R; Klienci R; DziennikOperacji W | `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/ReservationDAL.vb:45-47`, `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/ReservationDAL.vb:62-71`, `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/ReservationDAL.vb:115-119`, `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/ReservationDAL.vb:174-182` |
| RentalDAL | Wypozyczenia R/W; Klienci R; Sprzet R; DziennikOperacji W | `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/RentalDAL.vb:63-75`, `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/RentalDAL.vb:119-121`, `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/RentalDAL.vb:189-191` |
| PricingDAL | Taryfy R/W; Sprzet R/W; DziennikOperacji R/W | `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/PricingDAL.vb:35-80`, `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/PricingDAL.vb:111-120` |
| InvoiceDAL | Faktury R/W; PozycjeFaktur R/W; Klienci R; DziennikOperacji W | `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/InvoiceDAL.vb:37-80`, `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/InvoiceDAL.vb:91-93`, `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/InvoiceDAL.vb:137-141` |
| Klienci.aspx.vb | Klienci R bezpośrednio z UI | `legacy-app-parent/legacy-app/WypozyczalniaWeb/Klienci.aspx.vb:49-51` |
| Cennik.aspx.vb | Sprzet W; Taryfy R bezpośrednio z UI | `legacy-app-parent/legacy-app/WypozyczalniaWeb/Cennik.aspx.vb:58-59`, `legacy-app-parent/legacy-app/WypozyczalniaWeb/Cennik.aspx.vb:164-166` |
| ImportCennikaXml | Sprzet R przez DbHelper; poza tym zapis pośredni przez PricingDAL opisany powyżej | `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/ImportCennikaXml.vb:140-143`, `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/ImportCennikaXml.vb:56-88` |

| Procedura i klient VB | R / W tabel w jej ciele | Dowody definicji i wywołania |
|---|---|---|
| usp_SprawdzDostepnosc ← ReservationDAL | R Rezerwacje, Wypozyczenia, Sprzet | `legacy-app-parent/legacy-app/Database/02_procedury.sql:27-73`; `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/ReservationDAL.vb:18-21` |
| usp_ZapiszRezerwacje ← ReservationDAL | R Klienci, Sprzet; W Rezerwacje, DziennikOperacji | `legacy-app-parent/legacy-app/Database/02_procedury.sql:80-118`; `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/ReservationDAL.vb:30-36` |
| usp_ZmienStatusRezerwacji ← ReservationDAL | R/W Rezerwacje; W DziennikOperacji | `legacy-app-parent/legacy-app/Database/02_procedury.sql:126-154`; `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/ReservationDAL.vb:54-56` |
| usp_WydajSprzet ← RentalDAL | W Wypozyczenia, Sprzet, Rezerwacje (warunkowo), DziennikOperacji | `legacy-app-parent/legacy-app/Database/02_procedury.sql:162-207`; `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/RentalDAL.vb:25-33` |
| usp_ZarejestrujZwrot ← RentalDAL | R/W Wypozyczenia; W Sprzet, DziennikOperacji | `legacy-app-parent/legacy-app/Database/02_procedury.sql:215-267`; `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/RentalDAL.vb:41-46` |
| usp_PobierzTaryfe ← PricingDAL | R Taryfy | `legacy-app-parent/legacy-app/Database/02_procedury.sql:276-292`; `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/PricingDAL.vb:15-17` |
| usp_ZapiszTaryfe ← PricingDAL | R/W Taryfy; W DziennikOperacji | `legacy-app-parent/legacy-app/Database/02_procedury.sql:301-329`; `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/PricingDAL.vb:26-30` |
| usp_WystawFakture ← InvoiceDAL | R/W Faktury; W DziennikOperacji | `legacy-app-parent/legacy-app/Database/02_procedury.sql:339-378`; `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/InvoiceDAL.vb:20-26` |

To mapowanie obejmuje osiem tabel występujących w powyższych poleceniach, ale nie stanowi macierzy uprawnień SQL. Osobno należy traktować skrypty administracyjne: 01 tworzy schemat (`legacy-app-parent/legacy-app/Database/01_schema.sql:28-146`), a 03_seed kasuje zawartość wszystkich ośmiu tabel i wstawia dane syntetyczne (`legacy-app-parent/legacy-app/Database/03_seed.sql:12-36`). Nie jest to dowód, że aplikacja uruchamia seed przy starcie.

## 5) Niewiadome i ograniczenia

- **Wdrożenie i dostępy:** konfiguracja Windows/Integrated Security oraz konto zadania nie ustalają aktywnych ustawień IIS, ACL udziału, ról ani GRANT w bazie. Potrzebne są dane wdrożeniowe, nie wniosek z nazw kont (`legacy-app-parent/legacy-app/WypozyczalniaWeb/Web.config:11-15`, `legacy-app-parent/legacy-app/WypozyczalniaWeb/Web.config:33`; `legacy-app-parent/legacy-app/Jobs/EksportNocny/zadanie-harmonogramu.xml:18-23`).
- **Kompletność ścieżek:** M3 i M10 są leksykalne, nie dowodzą osiągalności ani braku konsumentów zewnętrznych. 61 kandydatów z W6 nie wolno automatycznie usuwać jako martwego kodu (04, M3 i M10; 05, W6).
- **Parity reguł:** nierozstrzygnięte są intencja różnic szacunku/wyceny, fallback VAT oraz kara opisana jako 100%, lecz liczona z mnożnikiem 1,5. Potwierdzone lokalizacje nie zastępują decyzji biznesowej i testów (05, W3–W5 i sekcja 4).
- **Transakcje i skutki zewnętrzne:** wydanie zapisuje dane przed wywołaniem kuriera, a płatność poprzedza lokalne oznaczenie opłacenia. Nie potwierdzono odporności całej ścieżki na awarię między tymi krokami ani end-to-end idempotencji (`legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:64-87`; `legacy-app-parent/legacy-app/WypozyczalniaWeb/Faktura.aspx.vb:225-232`). Transakcje i blokady w pojedynczej procedurze nie dowodzą atomowości między SQL, HTTP, SOAP i plikami (`legacy-app-parent/legacy-app/Database/02_procedury.sql:349-376`).
- **Zadanie i pliki:** katalog awaryjny jest użyty po negatywnej kontroli istnienia eksportu; wyjątek z pierwszego eksportu trafia do Catch, bez tej samej próby awaryjnej. Nie potwierdzono zachowania na rzeczywistym niedostępnym udziale ani bezpiecznego ponawiania całego importu (`legacy-app-parent/legacy-app/Jobs/EksportNocny/Program.vb:80-95`; `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/ImportCennikaXml.vb:31-38`).
- **Granica dowodów:** klasyfikacja 100,0% oznacza przypisanie wszystkich plików do kategorii, nie pełne rozpoznanie semantyki lub brak wyjątków (05, sekcje 1–2). Weryfikacja odnośników sprawdza istnienie ścieżek i zakresów, nie prawdziwość tez (`tools/sprawdz_dowody.py:2-19`).
