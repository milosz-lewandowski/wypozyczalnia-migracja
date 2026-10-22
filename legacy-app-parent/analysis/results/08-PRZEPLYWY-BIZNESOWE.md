# Przepływy biznesowe — zachowanie legacy

| Proces | Punkt wejścia: konkretny handler / harmonogram | Metoda logiki z dowodem | Stan nowej wg dokumentu |
|---|---|---|---|
| P1 — wycena | `Cennik.btnOblicz_Click`, `legacy-app-parent/legacy-app/WypozyczalniaWeb/Cennik.aspx.vb:101-129`; alternatywny szacunek: `Rezerwacja.btnSprawdz_Click`, `legacy-app-parent/legacy-app/WypozyczalniaWeb/Rezerwacja.aspx.vb:40-74` | `PricingManager.ObliczOplate` / `LiczbaDob` / `RabatProgu`, `legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:22-60` | Brak dokumentu NEW-PRZEPLYWY-BIZNESOWE.md; status nieustalony. |
| P2 — rezerwacja | `Rezerwacja.btnZarezerwuj_Click`, `legacy-app-parent/legacy-app/WypozyczalniaWeb/Rezerwacja.aspx.vb:77-119`; anulowanie: `legacy-app-parent/legacy-app/WypozyczalniaWeb/Rezerwacja.aspx.vb:205` | `ReservationManager.UtworzRezerwacje`, `legacy-app-parent/legacy-app/WypozyczalniaCore/ReservationManager.vb:18-59` | Brak dokumentu; status nieustalony. |
| P3 — wydanie | `Wypozyczenie.btnWydaj_Click`, `legacy-app-parent/legacy-app/WypozyczalniaWeb/Wypozyczenie.aspx.vb:169-217` | `RentalManager.WydajSprzet`, `legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:20-93` | Brak dokumentu; status nieustalony. |
| P4 — zwrot, opłata, kara, kaucja, szkody | `Zwrot.btnPodglad_Click` / `btnZwrot_Click`, `legacy-app-parent/legacy-app/WypozyczalniaWeb/Zwrot.aspx.vb:154-244` | `RentalManager.ZarejestrujZwrot` / `ObliczKare`, `legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:95-143` | Brak dokumentu; status nieustalony. |
| P5 — faktura i płatność | `Faktura.btnWystaw_Click` / `btnZaplac_Click`, `legacy-app-parent/legacy-app/WypozyczalniaWeb/Faktura.aspx.vb:189-253` | `InvoiceManager.WystawFaktureZaWypozyczenie` / `OznaczOplacona`, `legacy-app-parent/legacy-app/WypozyczalniaCore/InvoiceManager.vb:19-114` | Brak dokumentu; status nieustalony. |
| P6 — eksport nocny z importem cennika | `CalendarTrigger`: codziennie 02:30, `legacy-app-parent/legacy-app/Jobs/EksportNocny/zadanie-harmonogramu.xml:10-16`; uruchomienie EXE `/tryb:pelny`, `legacy-app-parent/legacy-app/Jobs/EksportNocny/zadanie-harmonogramu.xml:38-42` | `Program.Main`, `legacy-app-parent/legacy-app/Jobs/EksportNocny/Program.vb:21-54`; `ImportCennikaXml.ImportujPlik`, `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/ImportCennikaXml.vb:43-95`; `EksportCsv.EksportujDzien`, `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/EksportCsv.vb:28-65` | Brak dokumentu; status nieustalony. |
| P7 — klienci | `Klienci.btnSzukaj_Click` / `btnZapisz_Click` / `btnZablokuj_Click`, `legacy-app-parent/legacy-app/WypozyczalniaWeb/Klienci.aspx.vb:48-63`, `legacy-app-parent/legacy-app/WypozyczalniaWeb/Klienci.aspx.vb:95-156` | `CustomerManager.Zapisz` / `CzyMozeWypozyczac` / `ProgRabatu`, `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:26-98` | Brak dokumentu; status nieustalony. |

## Zakres i sposób odczytu

Reguły poniżej wynikają z wykonywalnych instrukcji VB i SQL, nie z komentarzy ani samych nazw metod. Jest to analiza statyczna, nie dowód wykonania aplikacji. Nie kompilowano legacy. Numery W pochodzą z `legacy-app-parent/analysis/results/05-KLASYFIKACJA-WYJATKI.md:138-220`. W repozytorium brak pliku `new-app-parent/new-docs/NEW-PRZEPLYWY-BIZNESOWE.md`; dlatego dla wszystkich procesów jawnie brak informacji o realizacji w nowej aplikacji. Nie oznacza to automatycznie „niezaimplementowane”.

## P1 — wycena

### Ścieżka

- `Cennik.aspx → btnOblicz_Click → PricingManager.ObliczOplate → EquipmentManager.PobierzSprzet → EquipmentDAL.Pobierz` (odczyt SQL, **bez procedury**) oraz `PricingDAL.PobierzTaryfe → usp_PobierzTaryfe`. Próg klienta: `CustomerManager.ProgRabatu → DbHelper → SQL`, **pominięty osobny DAL i procedura**. Dowody: `legacy-app-parent/legacy-app/WypozyczalniaWeb/Cennik.aspx.vb:101-129`, `legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:31-54`, `legacy-app-parent/legacy-app/WypozyczalniaCore/EquipmentManager.vb:19-25`, `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/EquipmentDAL.vb:16-23`, `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/PricingDAL.vb:14-22`, `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:39-49`.
- `Rezerwacja.aspx → btnSprawdz_Click → ObliczSzacunek`: **pominięta wycena PricingManager i taryfy**; dostępność ma osobną ścieżkę opisaną w P2. Dowody: `legacy-app-parent/legacy-app/WypozyczalniaWeb/Rezerwacja.aspx.vb:57-74`, `legacy-app-parent/legacy-app/WypozyczalniaWeb/Rezerwacja.aspx.vb:313-319`.

### Reguły

1. Liczba dób: `minuty = DateDiff(Minute, od, do)`; dla `minuty <= 0` wyjątek. Część całkowita `minuty \ 1440`, dodatkowa doba tylko gdy reszta **> 60 minut**, minimum **1**. Zatem 24 h 60 min to 1 doba, 24 h 61 min to 2 doby. Dowód: `legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:22-29`.
2. Taryfa to najnowszy wpis kategorii z `ObowiazujeOd <= dataOd` (SQL `TOP 1`, sortowanie malejąco). Jest pobierana raz dla początku okresu, nie osobno dla każdego dnia; brak taryfy rzuca `TARYFA_BRAK`, nie podstawia wartości domyślnej. Dowody: `legacy-app-parent/legacy-app/Database/02_procedury.sql:276-291`, `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/PricingDAL.vb:14-22`, `legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:31-44`.
3. Każda naliczana doba jest przypisana do `dataOd.AddDays(k)`. Sobota i niedziela: `Round(stawkaDobowa × mnoznikWeekend, 2)`; pozostałe dni: stawka dobowa. Po zsumowaniu, przy `doby \ 7 >= 1` odejmowany jest **jeden rabat od całej kwoty**, `Round(kwota × procent / 100, 2)`, a nie rabat wielokrotny za każdy tydzień. Dowód: `legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:35-46`.
4. Próg odczytany z bazy jest ograniczany do **0–3**, brak klienta daje **0**. Core przelicza `CInt(prog × 2.5)`, czyli progi **0/1/2/3 → 0/2/5/8%** (konwersja do najbliższej liczby całkowitej, połówki do parzystej). Rabat klienta stosowany jest po tygodniowym; wynik `Math.Round(..., 2)` bez wskazania trybu oznacza zaokrąglenie połówkowe do parzystej. Dowody: `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:39-49`, `legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:47-60`.
5. Szacunek UI rezerwacji jest innym algorytmem: różnica dat kalendarzowych, minimum **1 dzień**, stawka bez taryfy weekendowej i rabatu tygodniowego, rabat **0/2,5/5/7,5%**, końcowe `Round(..., 2, AwayFromZero)`. Zapis rezerwacji nie używa wartości z sesji, lecz ponownie wycenia przez Core. Dowody: `legacy-app-parent/legacy-app/WypozyczalniaWeb/Rezerwacja.aspx.vb:313-319`, `legacy-app-parent/legacy-app/WypozyczalniaCore/ReservationManager.vb:37-48`.
6. W danych startowych od **2026-01-01**: BUD mnożnik **1,25**, rabat tygodniowy **10%**; OGR **1,20 / 10%**; IMP **1,50 / 5%**; TRN **1,00 / 15%**. To rekordy seed, nie niezmienne stałe algorytmu. Dowód: `legacy-app-parent/legacy-app/Database/03_seed.sql:104-113`.
7. Zmiana stawki w `Cennik.btnZapiszStawke_Click` wykonuje bezpośrednie `UPDATE`, omijając manager i jego ograniczenie **> 0 do 10000**. Odrębna metoda `PricingManager.ZapiszTaryfe` waliduje mnożnik **1–3** i rabat **0–50%**; nie wolno przypisywać tych limitów wszystkim ścieżkom importu lub UI. Dowody: `legacy-app-parent/legacy-app/WypozyczalniaWeb/Cennik.aspx.vb:48-60`, `legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:18-20`, `legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:115-138`.

### Dane tabele

`Sprzet` — kategoria, bieżąca stawka i kaucja; `Taryfy` — mnożnik, rabat i data; `Klienci` — próg. Kwoty sprzętu `DECIMAL(10,2)`, mnożnik `DECIMAL(4,2)`, procent `DECIMAL(5,2)`; unikalność taryfy po kategorii i dacie. Dowód: `legacy-app-parent/legacy-app/Database/01_schema.sql:28-79`.

### Wyjątki

**W1** (próg i pomocnicze operacje SQL poza DAL), **W2** (aktualizacja stawki z UI), **W3** (szacunek UI różny od Core), **W4** (powielone 2,5), **W6** (pomocnicze metody nie dowodzą aktywnej ścieżki). Definicje: `legacy-app-parent/analysis/results/05-KLASYFIKACJA-WYJATKI.md:138-220`.

## P2 — rezerwacja

### Ścieżka

`Rezerwacja.aspx → btnZarezerwuj_Click → ReservationManager.UtworzRezerwacje → ReservationDAL.Zapisz → usp_ZapiszRezerwacje`. Przed zapisem: `EquipmentManager.SprawdzDostepnosc → ReservationDAL.PobierzKolizje → usp_SprawdzDostepnosc`; wycena według P1. Potwierdzenie/anulowanie: manager → `ReservationDAL.ZmienStatus → usp_ZmienStatusRezerwacji`. Dowody: `legacy-app-parent/legacy-app/WypozyczalniaWeb/Rezerwacja.aspx.vb:77-119`, `legacy-app-parent/legacy-app/WypozyczalniaCore/ReservationManager.vb:18-59`, `legacy-app-parent/legacy-app/WypozyczalniaCore/EquipmentManager.vb:45-54`, `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/ReservationDAL.vb:17-57`. Odczyt klienta omija osobny DAL i procedurę: `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:16-36`.

### Reguły

1. UI wymaga klienta z sesji i sprzętu, odrzuca początek wcześniejszy niż **teraz − 30 min**, koniec `<=` początek oraz godzinę początku `< 7` lub `> 18`. Sprawdza samo `Hour`, więc **18:59 przechodzi**, mimo tekstu o 18:00. Uwagi obcina do **500** znaków. Dowód: `legacy-app-parent/legacy-app/WypozyczalniaWeb/Rezerwacja.aspx.vb:77-115`.
2. Manager niezależnie wymaga końca późniejszego od początku i długości `TotalDays <= 60`, dopuszczonego klienta i braku kolizji; ustawia status **N** oraz cenę z Core. Nie powtarza ograniczenia godzin ani „teraz − 30 min”. Dowód: `legacy-app-parent/legacy-app/WypozyczalniaCore/ReservationManager.vb:15-50`.
3. SQL kolizji: rezerwacje **N/P** oraz wypożyczenia **O**, z nierównościami `początek < żądanyKoniec` i `koniec > żądanyPoczątek`; styk granic nie koliduje. Sprzęt **S/X** dodaje kolizję niezależną od dat. Sam status sprzętu **W** nie jest tu osobną kolizją — liczy się przedział otwartego wypożyczenia. Dowód: `legacy-app-parent/legacy-app/Database/02_procedury.sql:27-71`.
4. `usp_ZapiszRezerwacje` sprawdza istnienie klienta i sprzętu, zapisuje **N**, identyfikator i dziennik w transakcji. Nie powtarza zapytania o kolizje ani blokady klienta; sprawdzenie dostępności przed zapisem nie jest wspólną transakcją tej procedury. Dowód: `legacy-app-parent/legacy-app/Database/02_procedury.sql:80-118`.
5. Manager potwierdza tylko **N → P**, nie anuluje **Z**, anulowanie **A** jest bezczynne, pozostałe przechodzą w **A**; oznaczenie realizacji ustawia **Z**. SQL dopuszcza wyłącznie zbiór **N/P/A/Z** i wymaga istnienia rekordu, ale nie narzuca tych przejść managera. Dowody: `legacy-app-parent/legacy-app/WypozyczalniaCore/ReservationManager.vb:103-138`, `legacy-app-parent/legacy-app/Database/02_procedury.sql:126-152`.

### Dane tabele

`Rezerwacje` — FK do `Klienci` i `Sprzet`, daty, status, cena `DECIMAL(10,2)`, uwagi `NVARCHAR(500)`; odczyt `Wypozyczenia` i `Taryfy`, zapis `DziennikOperacji`. Dowody: `legacy-app-parent/legacy-app/Database/01_schema.sql:72-112`, `legacy-app-parent/legacy-app/Database/02_procedury.sql:38-69`, `legacy-app-parent/legacy-app/Database/02_procedury.sql:101-111`; wycena i taryfy: `legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:31-48`.

### Wyjątki

**W1** — klient odczytywany poza DAL; **W3/W4** — wcześniejszy szacunek UI nie jest ceną zapisu; **W6** — brak leksykalnego użycia metod pomocniczych nie potwierdza ich wykonania. Definicje: `legacy-app-parent/analysis/results/05-KLASYFIKACJA-WYJATKI.md:138-220`.

## P3 — wydanie

### Ścieżka

`Wypozyczenie.aspx → btnWydaj_Click → RentalManager.WydajSprzet → RentalDAL.Wydaj → usp_WydajSprzet`. Dalej manager ponawia zmianę statusu sprzętu przez `EquipmentManager → EquipmentDAL → UPDATE` (**bez procedury**) i rezerwacji przez `ReservationManager → ReservationDAL → usp_ZmienStatusRezerwacji`; opcjonalnie `KurierClient → HTTP`, potem `RentalDAL.UstawNumerPrzesylki → UPDATE` (**bez procedury**). Dowody: `legacy-app-parent/legacy-app/WypozyczalniaWeb/Wypozyczenie.aspx.vb:169-217`, `legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:60-91`, `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/RentalDAL.vb:21-35`, `legacy-app-parent/legacy-app/WypozyczalniaCore/EquipmentManager.vb:69-76`, `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/EquipmentDAL.vb:101-107`, `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/RentalDAL.vb:62-66`.

### Reguły

1. Wydanie z rezerwacji wybiera sprzęt z jej danych i wymaga zgodnego klienta sesji; bez rezerwacji używa wybranego sprzętu. Planowany zwrot musi być późniejszy niż teraz; zaznaczona dostawa wymaga niepustego adresu. Dowód: `legacy-app-parent/legacy-app/WypozyczalniaWeb/Wypozyczenie.aspx.vb:169-212`.
2. Manager odrzuca klienta niedopuszczonego, nieznany sprzęt i status inny niż **D**. Dla rezerwacji `id > 0` sprawdza istnienie, zgodność klienta i sprzętu oraz status **N/P**. W tej metodzie nie ma ponownego wywołania kontroli kolizji całego terminu ani wymogu zgodności planowanego zwrotu z końcem rezerwacji. Dowód: `legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:20-59`.
3. Data wydania to `Date.Now`, kaucja to bieżące `Sprzet.Kaucja`, status wypożyczenia **O**. SQL zamienia rezerwację **0 na NULL** i w jednej transakcji zapisuje wypożyczenie, sprzęt **W**, opcjonalną rezerwację **Z** oraz dziennik. SQL nie powtarza wszystkich walidacji managera. Dowody: `legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:51-65`, `legacy-app-parent/legacy-app/Database/02_procedury.sql:162-206`.
4. Zmiany statusów ponowione w managerze oraz kurier są już po zapisie procedury, nie należą do jej transakcji. Kurier dostaje referencję `WYP-` z ID do **6 cyfr**, rodzaj **PALETA**, pobranie **False**, termin od jutra. Brak API, brak numeru w odpowiedzi lub `WebException` daje pusty numer; nie cofa wydania. UI dla pustego numeru wskazuje kontakt telefoniczny. Dowody: `legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:62-91`, `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/KurierClient.vb:28-60`, `legacy-app-parent/legacy-app/WypozyczalniaWeb/Wypozyczenie.aspx.vb:215-228`.

### Dane tabele

`Wypozyczenia` — daty, kaucja pobrana, opcjonalna rezerwacja i numer przesyłki; `Sprzet`, `Rezerwacje`, `DziennikOperacji` — zapisy opisane w SQL; `Klienci` — odczyt dopuszczenia i telefonu. Dowody: `legacy-app-parent/legacy-app/Database/01_schema.sql:94-108`, `legacy-app-parent/legacy-app/Database/02_procedury.sql:179-200`, `legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:21-24`, `legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:74-80`.

### Wyjątki

**W1** — CustomerManager bez osobnego DAL; **W6** — nie utożsamiać pomocniczego `WydajZRezerwacji` ani metod kuriera bez potwierdzonego wywołania z wejściem UI. Definicje: `legacy-app-parent/analysis/results/05-KLASYFIKACJA-WYJATKI.md:138-150`, `legacy-app-parent/analysis/results/05-KLASYFIKACJA-WYJATKI.md:201-220`.

## P4 — zwrot, opłata, kara, kaucja, szkody

### Ścieżka

`Zwrot.aspx → btnZwrot_Click → RentalManager.ZarejestrujZwrot → PricingManager.ObliczOplate` (P1) oraz `RentalManager.ObliczKare → RentalDAL.ZapiszZwrot → usp_ZarejestrujZwrot`. Następnie manager ponawia status sprzętu przez `EquipmentManager → EquipmentDAL → UPDATE`, **bez procedury**. Podgląd `btnPodglad_Click` wylicza karę i kaucję bez zapisu i bez naliczenia opłaty. Dowody: `legacy-app-parent/legacy-app/WypozyczalniaWeb/Zwrot.aspx.vb:154-244`, `legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:95-143`, `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/RentalDAL.vb:37-47`, `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/EquipmentDAL.vb:101-107`.

### Reguły

1. UI odrzuca zwrot późniejszy niż **teraz + 5 min** oraz wcześniejszy od wydania. Manager sprawdza istnienie wypożyczenia i sprzętu, odrzuca zamknięty status **Z**, ujemne szkody zamienia na **0** i ponownie kontroluje datę względem wydania, ale nie względem teraz. Dowody: `legacy-app-parent/legacy-app/WypozyczalniaWeb/Zwrot.aspx.vb:195-223`, `legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:90-107`.
2. Opłata wynika z okresu **DataWydania → PlanowanyZwrot**, nie daty faktycznego zwrotu i nie zapisanej ceny rezerwacji. W chwili rozliczenia pobierane są dane wyceny P1 (stawka, taryfa według daty wydania i aktualny próg klienta); brak zamrożenia ceny rezerwacji. Dowody: `legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:107-115`, `legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:31-53`.
3. Kara: `minuty = DateDiff(Minute, planowanyZwrot, dataZwrotu)`; **minuty <= 60 → 0**. W przeciwnym razie `dobySpoznienia = (minuty - 60 + 1439) \ 1440`; kara `Math.Round(dobySpoznienia × stawkaDobowa × 1.5D, 2)`. To **150%**, bez mnożnika weekendowego i rabatów, połówki do parzystej. Pierwszy przedział płatny to **61–1500 min**, kolejny zaczyna się od **1501 min**. Dowód: `legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:137-143`.
4. **Przykład wymagany: stawka 72,35 zł, opóźnienie 3 h.** `minuty = 180`, `(180 − 60 + 1439) \ 1440 = 1559 \ 1440 = 1`; `1 × 72,35 × 1,5 = 108,525`; `Math.Round(..., 2)` daje **108,52 zł**, nie 108,53 zł i nie 72,35 zł. Kwota wejściowa występuje dla BUD-0002 w seed. Dowody: `legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:137-143`, `legacy-app-parent/legacy-app/Database/03_seed.sql:65-66`.
5. Szkody UI sumują zaznaczone pozycje: **uszkodzona obudowa 50 zł**, **brak akcesoriów 30 zł**, **zabrudzenie 20 zł**. Podgląd ma własną kopię tych kwot; zapis przekazuje ich sumę managerowi. Dowody: `legacy-app-parent/legacy-app/WypozyczalniaWeb/Zwrot.aspx.vb:173-187`, `legacy-app-parent/legacy-app/WypozyczalniaWeb/Zwrot.aspx.vb:225-244`.
6. Zwrot kaucji to `max(0, kaucjaPobrana − kara − szkody)`: **opłata za wynajem nie jest potrącana z kaucji**. Przekroczenie kaucji przez karę i szkody tylko generuje ostrzeżenie w tej metodzie; nie zapisuje osobnego salda różnicy. Kwota szkód nie trafia do osobnej kolumny wypożyczenia; występuje w dzienniku managera. Dowód: `legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:109-134`.
7. Procedura wymaga istniejącego i niezamkniętego wypożyczenia; w transakcji zapisuje datę, opłatę, karę, kaucję zwróconą, status **Z**, sprzęt **D** i dziennik. Nie wylicza kary ani szkód i nie kieruje sprzętu automatycznie do serwisu nawet przy szkodach. Dowód: `legacy-app-parent/legacy-app/Database/02_procedury.sql:215-267`.

### Dane tabele

`Wypozyczenia` — zapis rozliczenia bez kolumny „Szkody”; `Sprzet` — bieżąca stawka/kaucja oraz status; `Klienci`, `Taryfy` — wycena; `DziennikOperacji` — zapis zwrotu. Dowody: `legacy-app-parent/legacy-app/Database/01_schema.sql:94-108`, `legacy-app-parent/legacy-app/Database/02_procedury.sql:242-258`, `legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:31-48`.

### Wyjątki

**W5** — rozbieżny komentarz nie zmienia wykonywanego mnożnika **1,5**; **W1/W4** — odczyt klienta i procent rabatu w wycenie; **W6** — pomocniczych sposobów zamknięcia/przedłużenia nie uznano za handler zwrotu. Definicje: `legacy-app-parent/analysis/results/05-KLASYFIKACJA-WYJATKI.md:138-150`, `legacy-app-parent/analysis/results/05-KLASYFIKACJA-WYJATKI.md:180-220`.

## P5 — faktura i płatność

### Ścieżka

`Faktura.aspx → btnWystaw_Click → InvoiceManager.WystawFaktureZaWypozyczenie → InvoiceDAL.ZapiszNaglowek → usp_WystawFakture`; pozycje osobno `InvoiceDAL.ZapiszPozycje → INSERT`, **bez procedury**. Płatność: `btnZaplac_Click → BramkaService.ZarejestrujPlatnosc (SOAP) → InvoiceManager.OznaczOplacona → InvoiceDAL.UstawOplacona → UPDATE + INSERT dziennika`, **bez procedury płatności**. Dowody: `legacy-app-parent/legacy-app/WypozyczalniaWeb/Faktura.aspx.vb:189-253`, `legacy-app-parent/legacy-app/WypozyczalniaCore/InvoiceManager.vb:19-77`, `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/InvoiceDAL.vb:17-47`, `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/InvoiceDAL.vb:74-84`.

### Reguły

1. Wystawienie wymaga wypożyczenia **Z**, braku już istniejącej faktury oraz istniejących klienta i sprzętu. Błędy mają kody **WYP_BRAK, WYP_OTWARTE, FV_ISTNIEJE, KLIENT_BRAK, SPRZET_BRAK**. Dowód: `legacy-app-parent/legacy-app/WypozyczalniaCore/InvoiceManager.vb:19-46`.
2. Pozycja wynajmu ma ilość **1** i cenę netto `w.Oplata`; kara dochodzi tylko dla `w.Kara > 0`. „Szkody” są odtwarzane jako `kaucjaPobrana − kara − kaucjaZwrocona` i dodawane tylko gdy kaucja zwrócona **= 0** i odtworzona kwota **> 0**. Nie jest to zapis pierwotnych szkód ani gwarancja zafakturowania nadwyżki ponad kaucję. Przykład: kaucja 100, kara 0, szkody rzeczywiste 150 → zwrot 0 → fakturowane „Szkody” 100, nie 150 ani 50. Dowody: `legacy-app-parent/legacy-app/WypozyczalniaCore/InvoiceManager.vb:54-65`, `legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:109-112`.
3. VAT pochodzi z klucza `StawkaVat`; konfiguracja zawiera **0,23 (23%)**, brak lub pusty klucz daje fallback **„0,23”**, parsowany `CDec`. Netto pozycji `Round(ilość × cenaNetto, 2)`, VAT sumą `Round(nettoPozycji × stawkaVat, 2)` dla każdej pozycji, brutto netto + VAT; zaokrąglenia bez jawnego trybu są do parzystej. Dowody: `legacy-app-parent/legacy-app/WypozyczalniaWeb/Web.config:23`, `legacy-app-parent/legacy-app/WypozyczalniaCore/InvoiceManager.vb:17`, `legacy-app-parent/legacy-app/WypozyczalniaCore/InvoiceManager.vb:199-235`.
4. SQL numeruje **FV/rrrr/mm/nnnn**, biorąc **COUNT(*) + 1** faktur danego miesiąca przy `UPDLOCK, HOLDLOCK`, z datą serwera `GETDATE()` i statusem **W**. To nie sekwencja ani `MAX + 1`. Transakcja obejmuje nagłówek i dziennik, ale nie późniejszą pętlę zapisu pozycji VB. Dowody: `legacy-app-parent/legacy-app/Database/02_procedury.sql:339-378`, `legacy-app-parent/legacy-app/WypozyczalniaCore/InvoiceManager.vb:67-72`.
5. Bramka otrzymuje numer, **brutto**, walutę **PLN**; timeout **30000 ms**. Tylko wynik **OK** wywołuje oznaczenie opłacenia; **ODRZUCONA**, inne statusy, `SoapException` i `WebException` nie aktualizują płatności. Dowody: `legacy-app-parent/legacy-app/WypozyczalniaWeb/Faktura.aspx.vb:34`, `legacy-app-parent/legacy-app/WypozyczalniaWeb/Faktura.aspx.vb:209-253`.
6. Manager wymaga niepustego ID transakcji, dla już opłaconej **O** nic nie zmienia; DAL aktualizuje tylko **W → O** i zapisuje ID transakcji. Brak zmienionego rekordu powoduje **FV_STATUS**. Dowody: `legacy-app-parent/legacy-app/WypozyczalniaCore/InvoiceManager.vb:94-113`, `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/InvoiceDAL.vb:74-84`.
7. Pomocnicze `TerminPlatnosci` zwraca datę wystawienia + **14 dni** dla typu **F**, inaczej + **7 dni**; nie jest to dowód wywołania tej funkcji w ścieżce wystawienia/płatności. Pomocniczy podgląd faktury dodaje wynajem i karę, ale nie pozycję szkód. Dowód: `legacy-app-parent/legacy-app/WypozyczalniaCore/InvoiceManager.vb:161-197`.

### Dane tabele

`Faktury` — nagłówek, sumy, status, transakcja; unikalny **Numer**, ale brak unikalnego ograniczenia na `WypozyczenieId`. `PozycjeFaktur` — ilość, netto, VAT. Odczyty `Wypozyczenia`, `Klienci`, `Sprzet`; zapis `DziennikOperacji`. Dowody: `legacy-app-parent/legacy-app/Database/01_schema.sql:117-151`, `legacy-app-parent/legacy-app/WypozyczalniaCore/InvoiceManager.vb:19-72`, `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/InvoiceDAL.vb:74-84`.

### Wyjątki

**W4** — VAT w konfiguracji i fallbacku; **W1** — odczyt klienta przez manager SQL; **W6** — m.in. termin płatności i anulowanie są kandydatami bez potwierdzonego wywołania, nie automatycznymi etapami procesu. Definicje: `legacy-app-parent/analysis/results/05-KLASYFIKACJA-WYJATKI.md:138-150`, `legacy-app-parent/analysis/results/05-KLASYFIKACJA-WYJATKI.md:180-220`.

## P6 — eksport nocny z importem cennika

### Ścieżka

**Brak strony** dla wejścia nocnego: `CalendarTrigger → EksportNocny.exe /tryb:pelny → Program.Main → Importuj → ImportCennikaXml.ImportujKatalog/ImportujPlik → PricingDAL.ZapiszTaryfe → usp_ZapiszTaryfe`; sprzęt: `PricingDAL.ZaktualizujStawkeSprzetu → UPDATE` (**bez procedury**), brak kaucji: bezpośredni `DbHelper` w integracji (**pominięty DAL**). Import **omija PricingManager**. Eksport: `Program.Eksportuj → EksportCsv.EksportujDzien → RentalManager/InvoiceManager → RentalDAL/InvoiceDAL → SELECT` (**bez procedur eksportu**) → pliki. Dowody: `legacy-app-parent/legacy-app/Jobs/EksportNocny/zadanie-harmonogramu.xml:10-16`, `legacy-app-parent/legacy-app/Jobs/EksportNocny/zadanie-harmonogramu.xml:38-42`, `legacy-app-parent/legacy-app/Jobs/EksportNocny/Program.vb:32-88`, `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/ImportCennikaXml.vb:56-94`, `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/ImportCennikaXml.vb:140-145`, `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/PricingDAL.vb:24-31`, `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/PricingDAL.vb:55-63`, `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/EksportCsv.vb:28-65`.

Ręczne wejścia dodatkowe: `Cennik.btnImportuj_Click` i `Faktura.btnEksportCsv_Click`, nie harmonogram: `legacy-app-parent/legacy-app/WypozyczalniaWeb/Cennik.aspx.vb:75-86`, `legacy-app-parent/legacy-app/WypozyczalniaWeb/Faktura.aspx.vb:256`.

### Reguły

1. Harmonogram: codziennie **02:30**, `IgnoreNew`, start przy dostępności sieci, limit **1 h**. Tryb pełny uruchamia import, a następnie eksport **również po zwróconym błędzie importu**; kod zakończenia **0/1**. Dzień domyślny to wczoraj, opcjonalny `/dzien:yyyy-MM-dd`, niepoprawny parametr zachowuje dotychczasową datę. Kultura zadania **pl-PL**. Dowody: `legacy-app-parent/legacy-app/Jobs/EksportNocny/zadanie-harmonogramu.xml:10-35`, `legacy-app-parent/legacy-app/Jobs/EksportNocny/Program.vb:18-54`, `legacy-app-parent/legacy-app/Jobs/EksportNocny/Program.vb:113-139`.
2. Import nocny czyta katalog `D:\Wypozyczalnia\import\cennik`; jego brak jest traktowany jako sukces/pominięcie. Integracja sortuje pliki `*.xml`, po przetworzeniu przenosi do **przetworzone**; konflikt nazwy dodaje znacznik **yyyyMMddHHmmss**. Dowody: `legacy-app-parent/legacy-app/Jobs/EksportNocny/Program.vb:14-16`, `legacy-app-parent/legacy-app/Jobs/EksportNocny/Program.vb:56-73`, `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/ImportCennikaXml.vb:18-40`, `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/ImportCennikaXml.vb:147-160`.
3. XML musi mieć korzeń **cennik**; `obowiazujeOd` domyślnie dzisiaj. Nieznana kategoria taryfy jest pomijana. Import zapisuje taryfy bez managera: SQL wymaga tylko mnożnika **> 0** i rabatu **>= 0**, wykonuje upsert po kategorii i dacie — nie narzuca limitów managera **1–3 / 0–50%**. Dowody: `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/ImportCennikaXml.vb:43-70`, `legacy-app-parent/legacy-app/Database/02_procedury.sql:301-327`, `legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:115-126`.
4. Pozycja sprzętu identyfikowana numerem inwentarzowym: stawka **<= 0** jest pomijana; brak sprzętu daje ostrzeżenie, brak atrybutu kaucji zachowuje bieżącą kaucję (dla nieznanego numeru odczyt zwraca **0**). Brak wymaganego atrybutu rzuca `XmlException`. Zapisy kolejnych pozycji nie są objęte jedną transakcją całego pliku; błąd późniejszego wiersza może nastąpić po wcześniejszych zapisach. Dowody: `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/ImportCennikaXml.vb:72-95`, `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/ImportCennikaXml.vb:131-145`, `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/PricingDAL.vb:55-63`.
5. Eksport wybiera wypożyczenia **wydane LUB zwrócone** w przedziale `[dzień 00:00, następny dzień 00:00)` i faktury **wystawione** w tym przedziale; nie oznacza to wyłącznie zwrotów ani płatności dnia. Dowody: `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/RentalDAL.vb:81-99`, `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/InvoiceDAL.vb:88-99`.
6. Powstają **wypozyczenia_yyyyMMdd.csv** i **faktury_yyyyMMdd.csv**, separator **;**, kodowanie **Windows-1250**, daty **yyyy-MM-dd HH:mm**, kwoty **0.00** według bieżącej kultury (nocą pl-PL). Pola tekstowe zamieniają średnik na przecinek, CR/LF na spacje; NULL daje pusty tekst. Nadpisanie tworzy **.bak**. Dowód: `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/EksportCsv.vb:15-18`, `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/EksportCsv.vb:28-101`.
7. Zapis pliku ma `On Error Resume Next`. Sprawdzenie sukcesu kontroluje istnienie **tylko pliku wypożyczeń**, nie faktur ani świeżości pliku. Brak tego pliku na udziale powoduje eksport do `D:\Wypozyczalnia\eksport-awaryjny`; stary istniejący plik może przesłonić awarię zapisu, a po zapisie awaryjnym brak ponownego sprawdzenia. Po kodzie **0**, poza trybem import, kasowane są kopie `*.csv.bak` starsze niż **30 dni** tylko z udziału głównego. Dowody: `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/EksportCsv.vb:67-77`, `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/EksportCsv.vb:120-127`, `legacy-app-parent/legacy-app/Jobs/EksportNocny/Program.vb:14-16`, `legacy-app-parent/legacy-app/Jobs/EksportNocny/Program.vb:46-48`, `legacy-app-parent/legacy-app/Jobs/EksportNocny/Program.vb:75-110`.

### Dane tabele

Import: zapis `Taryfy`, `Sprzet`, `DziennikOperacji`. Eksport: odczyt `Wypozyczenia`, `Faktury`, `Klienci`, `Sprzet`, bez aktualizacji flagi eksportu w tych zapytaniach. Dowody: `legacy-app-parent/legacy-app/Database/02_procedury.sql:311-331`, `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/PricingDAL.vb:55-63`, `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/ImportCennikaXml.vb:94`, `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/RentalDAL.vb:81-99`, `legacy-app-parent/legacy-app/WypozyczalniaCore/DAL/InvoiceDAL.vb:88-99`.

### Wyjątki

**W1** — bieżąca kaucja odczytywana w integracji poza DAL; **W6** — `SprawdzPlik` i `EksportujOkres` nie są automatycznie etapami zadania nocnego. Definicje: `legacy-app-parent/analysis/results/05-KLASYFIKACJA-WYJATKI.md:138-150`, `legacy-app-parent/analysis/results/05-KLASYFIKACJA-WYJATKI.md:201-220`. Zachowania tłumienia błędów opisano z kodu, bez nadawania nowych numerów W.

## P7 — klienci

### Ścieżka

`Klienci.aspx → btnZapisz_Click / btnZablokuj_Click → CustomerManager.Zapisz / Zablokuj → DbHelper → INSERT/UPDATE`. **Brak osobnego CustomerDAL i brak procedury** w tej ścieżce. Wyszukanie przyciskiem: `btnSzukaj_Click → DbHelper → SELECT`, **pominięty manager, DAL i procedura**; ładowanie listy i stronicowanie korzystają natomiast z `CustomerManager.Szukaj`. Dowody: `legacy-app-parent/legacy-app/WypozyczalniaWeb/Klienci.aspx.vb:48-63`, `legacy-app-parent/legacy-app/WypozyczalniaWeb/Klienci.aspx.vb:90-163`, `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:52-98`, `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:100-119`.

### Reguły

1. Zapis UI wymaga niepustego e-maila zawierającego **@**, niepustego telefonu o długości maks. **20** znaków; dla **F** nazwy firmy oraz NIP po usunięciu myślników/spacji: długość **10** i `IsNumeric` (nie kontrola sumy NIP); dla **I** numeru dokumentu. Dowody: `legacy-app-parent/legacy-app/WypozyczalniaWeb/Klienci.aspx.vb:95-105`, `legacy-app-parent/legacy-app/WypozyczalniaWeb/Klienci.aspx.vb:194-214`.
2. Dane są przycinane; numer dokumentu zamieniany na wielkie litery. Przy zapisie NIP usuwa myślniki i trimuje, ale nie usuwa spacji wewnętrznych, choć walidacja je usuwa. Dla **I** NIP jest ustawiany na `Nothing`. Dowód: `legacy-app-parent/legacy-app/WypozyczalniaWeb/Klienci.aspx.vb:107-122`.
3. Manager wymaga nazwiska **lub** nazwy firmy, pusty typ ustawia na **I**, dla **F** wymaga niepustego NIP. Nie powtarza pełnej walidacji UI. `Id = 0` oznacza INSERT i dziennik **KLIENT_NOWY**, w przeciwnym przypadku UPDATE; brak aktualizowanego wiersza to **KLI-03**. Dowód: `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:63-98`.
4. Dopuszczenie do wypożyczania zależy w `CzyMozeWypozyczac` wyłącznie od istnienia klienta i `Zablokowany = False`, nie od automatycznej analizy zaległości. Próg rabatu odczytany z bazy jest ograniczany do **0–3**, brak klienta daje **0**. Dowód: `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:26-49`.
5. Wybór klienta niedopuszczonego nie ustawia sesji; prawidłowy wybór zapisuje `WybranyKlientId` i przekierowuje do rezerwacji. Blokada z UI wymaga niezerowego ID, pusty powód zastępuje tekstem domyślnym i zeruje wybór klienta w tej sesji. Manager obcina powód do **200** znaków i ustawia blokadę **1**; `Odblokuj` ustawia **0 / NULL**, ale nie wykazano handlera UI dla odblokowania. Dowody: `legacy-app-parent/legacy-app/WypozyczalniaWeb/Klienci.aspx.vb:70-81`, `legacy-app-parent/legacy-app/WypozyczalniaWeb/Klienci.aspx.vb:139-156`, `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:100-119`.
6. Wyszukiwanie przyciskiem filtruje **tylko nazwisko**, konkatenacją `LIKE '%tekst%'`, sortuje po nazwisku, bez `TOP`. `CustomerManager.Szukaj` przycina frazę, stosuje parametry, filtruje **nazwisko LUB firmę LUB e-mail**, dla pustej frazy dopuszcza wszystkich, zwraca **TOP 200**, sortuje po nazwisku i imieniu. To różne zachowania, nie jedna zunifikowana wyszukiwarka. Dowody: `legacy-app-parent/legacy-app/WypozyczalniaWeb/Klienci.aspx.vb:48-56`, `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:52-60`.

### Dane tabele

`Klienci` — dane identyfikacyjne, typ domyślny **I**, próg domyślny **0**, blokada domyślna **0**, powód do **200** znaków, rejestracja `GETDATE()`; w definicji brak unikalności e-maila. `DziennikOperacji` — utworzenie i blokada/odblokowanie. Dowody: `legacy-app-parent/legacy-app/Database/01_schema.sql:28-42`, `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:75-119`.

### Wyjątki

**W1** — SQL w CustomerManager; **W2** — zapytanie wyszukujące w code-behind; **W6** — m.in. `Odblokuj` i `CzyEmailZajety` nie są dowodem automatycznego odblokowania ani wymuszania unikalności e-maila. Definicje: `legacy-app-parent/analysis/results/05-KLASYFIKACJA-WYJATKI.md:138-163`, `legacy-app-parent/analysis/results/05-KLASYFIKACJA-WYJATKI.md:201-220`.
