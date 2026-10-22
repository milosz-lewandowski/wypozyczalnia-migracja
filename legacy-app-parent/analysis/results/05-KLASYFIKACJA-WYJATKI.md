# Klasyfikacja wzorców i wyjątki

Klasyfikacja obejmuje drzewo `legacy-app-parent/legacy-app`. Silnik raportuje pokrycie wzorcami plików, a reguły konstrukcji wyszukują wybrane linie dozwolone w określonych katalogach. Wynik jest pomiarem statycznym, nie oceną kompletności działania, podatnością ani dowodem wykonania ścieżki. Pliki bez wzorca: zero; wszystkie 52 pliki otrzymały zamierzoną kategorię.

## 1) Iteracje reguł

Wartości pokrycia oraz liczba plików bez wzorca pochodzą z silnika `klasyfikuj.py`. Każda iteracja korzystała z tych samych 17 wzorców plików; zmieniały się wyłącznie reguły konstrukcji.

| Iteracja | Reguły konstrukcji | Pliki objęte | Linie objęte | Pliki bez wzorca |
|---|---|---:|---:|---:|
| 1 | 0: same wzorce plików (WebForms, cykl życia, SOAP, manager, DAL, model, integracje, zadanie i konfiguracja) | 100,0% | 100,0% | 0 |
| 2 | 5: SQL inline, DbHelper poza DAL, Math.Round poza managerami, stała procentowa poza managerami, Session poza Web | 100,0% | 100,0% | 0 |
| 3 (końcowa) | 7: zestaw z iteracji 2 + współczynnik rabatu w formule + fallback stawki VAT zaszyty w kodzie | 100,0% | 100,0% | 0 |

Iteracje 2–3 pozostawiają te same wzorce plików, bez przypadkowego dopasowania plików implementacji do kategorii konfiguracji. Końcowa lista „Pliki bez wzorca” zawiera wyłącznie `brak`. Reguły konstrukcji celowo zwracają wystąpienia jako wyjątki do przeglądu, a nie jako powód do wyłączenia pliku z klasyfikacji.

## 2) Ostatni wynik silnika — bez zmian

Poniżej wynik stdout `python tools/klasyfikuj.py` z końcową wersją `tools/wzorce.json`:

```text
Katalog: `legacy-app-parent/legacy-app` · reguły: `tools/wzorce.json` · plików: 52 · linii niepustych: 6939

### Wzorce plików

| wzorzec | pliki | linie | % plików | % linii |
|---|---:|---:|---:|---:|
| WebForms — markup strony | 7 | 615 | 13,5 | 8,9 |
| WebForms — code-behind | 7 | 1647 | 13,5 | 23,7 |
| WebForms — designer | 1 | 66 | 1,9 | 1,0 |
| Cykl życia aplikacji Web | 2 | 54 | 3,8 | 0,8 |
| Proxy SOAP | 1 | 108 | 1,9 | 1,6 |
| Kontrakty integracji SOAP | 1 | 92 | 1,9 | 1,3 |
| Manager aplikacyjny | 6 | 1425 | 11,5 | 20,5 |
| Table Data Gateway / DAL | 6 | 865 | 11,5 | 12,5 |
| Model domenowy | 6 | 377 | 11,5 | 5,4 |
| Adapter integracyjny | 3 | 386 | 5,8 | 5,6 |
| Zadanie wsadowe | 1 | 132 | 1,9 | 1,9 |
| Projekt .NET | 3 | 300 | 5,8 | 4,3 |
| Solucja Visual Studio | 1 | 36 | 1,9 | 0,5 |
| Konfiguracja aplikacji | 2 | 104 | 3,8 | 1,5 |
| Skrypt bazy danych | 3 | 652 | 5,8 | 9,4 |
| Harmonogram zadania | 1 | 44 | 1,9 | 0,6 |
| Notatka wdrożeniowa | 1 | 36 | 1,9 | 0,5 |
| **pokryte wzorcami** | 52 | 6939 | 100,0 | 100,0 |

### Pliki bez wzorca (0)

- brak

### Konstrukcje

| konstrukcja | dozwolona w | wystąpienia | naruszenia |
|---|---|---:|---:|
| Literały SQL w kodzie VB | `WypozyczalniaCore/DAL/**` | 88 | 20 |
| Bezpośredni dostęp do DbHelper poza DAL | `WypozyczalniaCore/DAL/**` | 243 | 46 |
| Zaokrąglenie kwoty poza managerem | `WypozyczalniaCore/*Manager.vb` | 7 | 1 |
| Stała procentowa poza managerem | `WypozyczalniaCore/*Manager.vb` | 1 | 0 |
| Współczynnik rabatu zaszyty w formule | `WypozyczalniaCore/*Manager.vb` | 2 | 1 |
| Domyślna stawka VAT zaszyta w kodzie | nigdzie | 1 | 1 |
| Bezpośredni dostęp do Session poza Web | `WypozyczalniaWeb/**/*.vb` | 27 | 0 |

### Naruszenia (69)

- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:18` [Literały SQL w kodzie VB] "SELECT " & KolumnyKlienta & " FROM Klienci WHERE Id = @Id", _
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:41` [Literały SQL w kodzie VB] "SELECT ProgRabatu FROM Klienci WHERE Id = @Id", _
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:55` [Literały SQL w kodzie VB] Dim sql As String = "SELECT TOP 200 Id, Imie, Nazwisko, NazwaFirmy, Email, Telefon FROM Kl
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:77` [Literały SQL w kodzie VB] "INSERT INTO Klienci (Imie, Nazwisko, NazwaFirmy, Email, Telefon, NumerDokumentu, NIP, Typ
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:79` [Literały SQL w kodzie VB] "SELECT CAST(SCOPE_IDENTITY() AS INT) AS NoweId", _
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:88` [Literały SQL w kodzie VB] "UPDATE Klienci SET Imie = @Imie, Nazwisko = @Nazwisko, NazwaFirmy = @NazwaFirmy, Email = 
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:105` [Literały SQL w kodzie VB] "UPDATE Klienci SET Zablokowany = 1, PowodBlokady = @Powod WHERE Id = @Id", _
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:114` [Literały SQL w kodzie VB] "UPDATE Klienci SET Zablokowany = 0, PowodBlokady = NULL WHERE Id = @Id", _
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:121` [Literały SQL w kodzie VB] "SELECT Email FROM Klienci WHERE Id = @Id", DbHelper.Parametr("@Id", klientId))
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:128` [Literały SQL w kodzie VB] "SELECT COUNT(*) AS Liczba FROM Wypozyczenia WHERE KlientId = @Id AND Status = 'O'", _
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:136` [Literały SQL w kodzie VB] "SELECT Id, Imie, Nazwisko, NazwaFirmy, Email, Telefon, PowodBlokady FROM Klienci " & _
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:143` [Literały SQL w kodzie VB] "SELECT COUNT(*) AS Liczba FROM Klienci WHERE Email = @Email AND Id <> @Id", _
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:188` [Literały SQL w kodzie VB] "INSERT INTO DziennikOperacji (Operacja, Szczegoly) VALUES (@Operacja, @Szczegoly)", _
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:17` [Bezpośredni dostęp do DbHelper poza DAL] Dim dt As DataTable = DbHelper.WykonajZapytanie( _
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:19` [Bezpośredni dostęp do DbHelper poza DAL] DbHelper.Parametr("@Id", klientId))
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:40` [Bezpośredni dostęp do DbHelper poza DAL] Dim dt As DataTable = DbHelper.WykonajZapytanie( _
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:42` [Bezpośredni dostęp do DbHelper poza DAL] DbHelper.Parametr("@Id", klientId))
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:58` [Bezpośredni dostęp do DbHelper poza DAL] Return DbHelper.WykonajZapytanie(sql, _
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:59` [Bezpośredni dostęp do DbHelper poza DAL] DbHelper.Parametr("@Fraza", fraza), _
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:60` [Bezpośredni dostęp do DbHelper poza DAL] DbHelper.Parametr("@Wzorzec", "%" & fraza & "%"))
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:76` [Bezpośredni dostęp do DbHelper poza DAL] Dim dt As DataTable = DbHelper.WykonajZapytanie( _
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:86` [Bezpośredni dostęp do DbHelper poza DAL] parametry.Add(DbHelper.Parametr("@Id", klient.Id))
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:87` [Bezpośredni dostęp do DbHelper poza DAL] Dim zmienione As Integer = DbHelper.WykonajPolecenie( _
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:104` [Bezpośredni dostęp do DbHelper poza DAL] DbHelper.WykonajPolecenie( _
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:106` [Bezpośredni dostęp do DbHelper poza DAL] DbHelper.Parametr("@Powod", powod), _
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:107` [Bezpośredni dostęp do DbHelper poza DAL] DbHelper.Parametr("@Id", klientId))
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:113` [Bezpośredni dostęp do DbHelper poza DAL] DbHelper.WykonajPolecenie( _
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:115` [Bezpośredni dostęp do DbHelper poza DAL] DbHelper.Parametr("@Id", klientId))
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:120` [Bezpośredni dostęp do DbHelper poza DAL] Dim dt As DataTable = DbHelper.WykonajZapytanie( _
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:121` [Bezpośredni dostęp do DbHelper poza DAL] "SELECT Email FROM Klienci WHERE Id = @Id", DbHelper.Parametr("@Id", klientId))
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:127` [Bezpośredni dostęp do DbHelper poza DAL] Dim dt As DataTable = DbHelper.WykonajZapytanie( _
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:129` [Bezpośredni dostęp do DbHelper poza DAL] DbHelper.Parametr("@Id", klientId))
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:135` [Bezpośredni dostęp do DbHelper poza DAL] Return DbHelper.WykonajZapytanie( _
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:142` [Bezpośredni dostęp do DbHelper poza DAL] Dim dt As DataTable = DbHelper.WykonajZapytanie( _
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:144` [Bezpośredni dostęp do DbHelper poza DAL] DbHelper.Parametr("@Email", email.Trim()), _
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:145` [Bezpośredni dostęp do DbHelper poza DAL] DbHelper.Parametr("@Id", pominKlientaId))
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:151` [Bezpośredni dostęp do DbHelper poza DAL] DbHelper.Parametr("@Imie", k.Imie), _
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:152` [Bezpośredni dostęp do DbHelper poza DAL] DbHelper.Parametr("@Nazwisko", k.Nazwisko), _
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:153` [Bezpośredni dostęp do DbHelper poza DAL] DbHelper.Parametr("@NazwaFirmy", PustyNaNull(k.NazwaFirmy)), _
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:154` [Bezpośredni dostęp do DbHelper poza DAL] DbHelper.Parametr("@Email", k.Email), _
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:155` [Bezpośredni dostęp do DbHelper poza DAL] DbHelper.Parametr("@Telefon", k.Telefon), _
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:156` [Bezpośredni dostęp do DbHelper poza DAL] DbHelper.Parametr("@NumerDokumentu", PustyNaNull(k.NumerDokumentu)), _
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:157` [Bezpośredni dostęp do DbHelper poza DAL] DbHelper.Parametr("@NIP", PustyNaNull(k.NIP)), _
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:158` [Bezpośredni dostęp do DbHelper poza DAL] DbHelper.Parametr("@Typ", k.Typ), _
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:159` [Bezpośredni dostęp do DbHelper poza DAL] DbHelper.Parametr("@ProgRabatu", k.ProgRabatu)}
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:187` [Bezpośredni dostęp do DbHelper poza DAL] DbHelper.WykonajPolecenie( _
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:189` [Bezpośredni dostęp do DbHelper poza DAL] DbHelper.Parametr("@Operacja", operacja), _
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:190` [Bezpośredni dostęp do DbHelper poza DAL] DbHelper.Parametr("@Szczegoly", szczegoly))
- `legacy-app-parent/legacy-app/WypozyczalniaCore/InvoiceManager.vb:17` [Domyślna stawka VAT zaszyta w kodzie] Private Const DomyslnaStawkaVat As String = "0,23"
- `legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:152` [Literały SQL w kodzie VB] Dim sql As String = "SELECT Id, KlientId, SprzetId, DataOd, DataDo, CenaSzacunkowa FROM Re
- `legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:159` [Literały SQL w kodzie VB] DbHelper.WykonajPolecenie("UPDATE Rezerwacje SET CenaSzacunkowa = @Cena WHERE Id = @Id",
- `legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:292` [Literały SQL w kodzie VB] Dim dt As DataTable = DbHelper.WykonajZapytanie("SELECT KlientId, SprzetId, DataOd, DataDo
- `legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:153` [Bezpośredni dostęp do DbHelper poza DAL] Dim dt As DataTable = DbHelper.WykonajZapytanie(sql)
- `legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:159` [Bezpośredni dostęp do DbHelper poza DAL] DbHelper.WykonajPolecenie("UPDATE Rezerwacje SET CenaSzacunkowa = @Cena WHERE Id = @Id",
- `legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:160` [Bezpośredni dostęp do DbHelper poza DAL] DbHelper.Parametr("@Cena", nowa),
- `legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:161` [Bezpośredni dostęp do DbHelper poza DAL] DbHelper.Parametr("@Id", row("Id")))
- `legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:292` [Bezpośredni dostęp do DbHelper poza DAL] Dim dt As DataTable = DbHelper.WykonajZapytanie("SELECT KlientId, SprzetId, DataOd, DataDo
- `legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:293` [Bezpośredni dostęp do DbHelper poza DAL] DbHelper.Parametr("@Id", rezerwacjaId))
- `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/ImportCennikaXml.vb:141` [Literały SQL w kodzie VB] Dim dt As DataTable = DbHelper.WykonajZapytanie("SELECT Kaucja FROM Sprzet WHERE NumerInwe
- `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/ImportCennikaXml.vb:141` [Bezpośredni dostęp do DbHelper poza DAL] Dim dt As DataTable = DbHelper.WykonajZapytanie("SELECT Kaucja FROM Sprzet WHERE NumerInwe
- `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/ImportCennikaXml.vb:142` [Bezpośredni dostęp do DbHelper poza DAL] DbHelper.Parametr("@Numer", numer))
- `legacy-app-parent/legacy-app/WypozyczalniaWeb/Cennik.aspx.vb:58` [Literały SQL w kodzie VB] Dim sql As String = "UPDATE Sprzet SET StawkaDobowa = " & txtStawka.Text.Replace(",", ".")
- `legacy-app-parent/legacy-app/WypozyczalniaWeb/Cennik.aspx.vb:164` [Literały SQL w kodzie VB] Dim sql As String = "SELECT Id, Kategoria, MnoznikWeekend, RabatTygodniowyProc, Obowiazuje
- `legacy-app-parent/legacy-app/WypozyczalniaWeb/Cennik.aspx.vb:59` [Bezpośredni dostęp do DbHelper poza DAL] Dim ile As Integer = DbHelper.WykonajPolecenie(sql)
- `legacy-app-parent/legacy-app/WypozyczalniaWeb/Cennik.aspx.vb:166` [Bezpośredni dostęp do DbHelper poza DAL] gvTaryfy.DataSource = DbHelper.WykonajZapytanie(sql)
- `legacy-app-parent/legacy-app/WypozyczalniaWeb/Klienci.aspx.vb:49` [Literały SQL w kodzie VB] Dim sql As String = "SELECT Id, Imie, Nazwisko, NazwaFirmy, Email, Telefon FROM Klienci WH
- `legacy-app-parent/legacy-app/WypozyczalniaWeb/Klienci.aspx.vb:51` [Bezpośredni dostęp do DbHelper poza DAL] Dim dt As DataTable = DbHelper.WykonajZapytanie(sql)
- `legacy-app-parent/legacy-app/WypozyczalniaWeb/Rezerwacja.aspx.vb:318` [Zaokrąglenie kwoty poza managerem] Return Math.Round(kwota, 2, MidpointRounding.AwayFromZero)
- `legacy-app-parent/legacy-app/WypozyczalniaWeb/Rezerwacja.aspx.vb:317` [Współczynnik rabatu zaszyty w formule] kwota = kwota * (100D - prog * 2.5D) / 100D
```

## 3) Wyjątki widoczne w kodzie

### W1 — SQL i helper bazy są poza DAL

**Co łamie wzorzec:** przyjęta reguła migracyjna umieszcza polecenia i dostęp do danych za granicą DAL; w kodzie instrukcje SQL i/lub wywołania `DbHelper` znajdują się również w managerach, integracji i stronach.

- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:18`
- `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:77`
- `legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:152`
- `legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:159`
- `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/ImportCennikaXml.vb:141`
- `legacy-app-parent/legacy-app/WypozyczalniaWeb/Cennik.aspx.vb:58`
- `legacy-app-parent/legacy-app/WypozyczalniaWeb/Klienci.aspx.vb:49`

Silnik wykazał 20 dopasowanych linii SQL poza DAL oraz 46 dopasowanych linii z `DbHelper` poza DAL; liczby dotyczą dopasowań regexu, nie liczby unikatowych operacji. Część wystąpień nakłada się na siebie. Samo `CustomerManager` zawiera operacje SQL i wywołuje helper, a nie odpowiadający mu `CustomerDAL`. **Znaczenie dla migracji:** granica repozytorium/DAL wymaga decyzji o zachowaniu historycznych zachowań; przeniesienie zapytań może też ujawnić logikę biznesową ukrytą w SQL.

### W2 — strony WebForms wykonują dostęp do bazy bezpośrednio

**Co łamie wzorzec:** część code-behind pomija manager i używa `DbHelper` bezpośrednio. W `Klienci.aspx.vb` tekst wyszukiwania jest wstawiany do budowanego zapytania; w `Cennik.aspx.vb` wartości formularza są konkatenowane w poleceniu aktualizacji.

- `legacy-app-parent/legacy-app/WypozyczalniaWeb/Klienci.aspx.vb:49`
- `legacy-app-parent/legacy-app/WypozyczalniaWeb/Klienci.aspx.vb:51`
- `legacy-app-parent/legacy-app/WypozyczalniaWeb/Cennik.aspx.vb:58`
- `legacy-app-parent/legacy-app/WypozyczalniaWeb/Cennik.aspx.vb:59`
- `legacy-app-parent/legacy-app/WypozyczalniaWeb/Cennik.aspx.vb:164`
- `legacy-app-parent/legacy-app/WypozyczalniaWeb/Cennik.aspx.vb:166`

To stwierdzenie opisuje konkatenację wejścia w SQL, nie demonstruje jej wykorzystania. **Znaczenie dla migracji:** przenosząc te ścieżki do Spring, obsługę zapytań należy odseparować od UI, a wartości wejściowe przekazywać jako parametry zapytań; dla parity testów trzeba zachować filtrowanie, kolejność i wynik listy.

### W3 — obliczenie progowego rabatu i zaokrąglenie powielono w ścieżce UI

**Co łamie wzorzec:** szacunek na stronie liczy rabat `prog * 2.5D` bezpośrednio, podczas gdy `PricingManager` mapuje próg na całkowity procent przez `CInt(prog * 2.5)` i zaokrągla kwotę osobno. Strona używa `MidpointRounding.AwayFromZero`; wywołania `Math.Round` w `PricingManager` nie podają tego trybu. To widoczna różnica algorytmów, nie teza o błędności szacunku.

- `legacy-app-parent/legacy-app/WypozyczalniaWeb/Rezerwacja.aspx.vb:316`
- `legacy-app-parent/legacy-app/WypozyczalniaWeb/Rezerwacja.aspx.vb:317`
- `legacy-app-parent/legacy-app/WypozyczalniaWeb/Rezerwacja.aspx.vb:318`
- `legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:47`
- `legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:50`
- `legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:51`
- `legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:58`
- `legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:59`

Metoda strony nazywa się `ObliczSzacunek`, więc może celowo być przybliżeniem. **Znaczenie dla migracji:** nie kopiować obu implementacji niezależnie do React i Spring. Ustalić autorytatywne obliczenie oraz jawnie zweryfikować rozbieżności na progach i kwotach połówkowych, zanim UI zacznie prezentować wynik identyczny z wyceną Core.

### W4 — współczynnik rabatu i stawka VAT mają kopie w różnych miejscach

**Co łamie wzorzec:** współczynnik `2.5` występuje jako logika obliczenia w PricingManager i ponownie w formularzowym szacunku rezerwacji. Stawka VAT `0,23` jest skonfigurowana w Web.config, a dodatkowy fallback o tej samej wartości jest zakodowany jako stała w InvoiceManager.

- `legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:59`
- `legacy-app-parent/legacy-app/WypozyczalniaWeb/Rezerwacja.aspx.vb:317`
- `legacy-app-parent/legacy-app/WypozyczalniaWeb/Web.config:23`
- `legacy-app-parent/legacy-app/WypozyczalniaCore/InvoiceManager.vb:17`
- `legacy-app-parent/legacy-app/WypozyczalniaCore/InvoiceManager.vb:227`

**Znaczenie dla migracji:** kilka kopii wartości oznacza ryzyko niespójnej zmiany. VAT ma konfigurację i zachowanie awaryjne — migracja powinna świadomie ustalić, czy zachowuje fallback oraz jego wartość. Współczynnik rabatu powinien mieć jedno źródło reguły; powielenie UI/Core jest powiązane z różnicą z W3.

### W5 — komentarz o karze nie zgadza się z mnożnikiem

**Co łamie wzorzec:** komentarz opisuje karę jako 100% stawki dobowej za dobę spóźnienia, a wyrażenie mnoży liczbę dób przez stawkę i `1.5D`.

- `legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:141`
- `legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:142`

**Znaczenie dla migracji:** komentarz nie rozstrzyga, czy prawidłowa reguła to 100%, czy 150%. Nie przenosić arbitralnie żadnej z tych wartości — ustalić oczekiwane zachowanie z właścicielem procesu i testami/obserwacjami historycznymi.

### W6 — metody publiczne bez leksykalnych wywołań w skanie M10

**Co łamie wzorzec:** licznik z M10 w [04-POMIAR-STRUKTURY.md](./04-POMIAR-STRUKTURY.md) znajduje **61 deklaracji publicznych metod VB** o jednym wystąpieniu nazwy, w pliku deklaracji. To kandydaci do wyjaśnienia, nie potwierdzone martwe ścieżki: M10 nie wykrywa użyć refleksyjnych, wywołań spoza skanowanego repozytorium ani wszystkich możliwych konwencji wiązania.

Grupy kandydatów według pliku (liczby pochodzą z M10; pełne deklaracje są w wymienionych plikach):

| Plik | Kandydaci bez dodatkowego wystąpienia nazwy |
|---|---|
| `CustomerManager.vb` | `Odblokuj`, `PobierzEmail`, `LiczbaOtwartychWypozyczen`, `KlienciZablokowani`, `CzyEmailZajety` |
| `EquipmentDAL.vb` | `ListaWStatusie` |
| `PricingDAL.vb` | `UsunTaryfe`, `StawkaSprzetu`, `TaryfaIstnieje`, `ListaSprzetuZeStawkami`, `HistoriaZmianCennika`, `PobierzTaryfePoId`, `KategorieZTaryfami` |
| `RentalDAL.vb` | `LiczbaOtwartych`, `SumaKaucjiOtwartych`, `SumaKarZOkresu` |
| `EquipmentManager.vb` | `DostepneWKategorii`, `Wycofaj`, `PrzywrocZSerwisu`, `Podsumowanie` |
| `EksportCsv.vb` | `EksportujOkres` |
| `ImportCennikaXml.vb` | `SprawdzPlik` |
| `KurierClient.vb` | `AnulujPrzesylke`, `CzyDostepny` |
| `InvoiceManager.vb` | `CzyMoznaWystawic`, `AnulujNieoplacona`, `TerminPlatnosci` |
| `Faktura.vb`, `Klient.vb`, `Sprzet.vb`, `Taryfa.vb` | `SumaPozycjiNetto`, `Equals`, `CzyMoznaWydac`, `CzyObowiazujeNa`, `Kopia`, `Domyslna`, `TakaSama` |
| `PricingManager.vb` | `ObliczOplateBezKlienta`, `KaucjaDlaSprzetu`, `PrzeliczRezerwacjeNowe`, `CennikKategorii`, `SprawdzTerminy`, `HistoriaTaryf`, `ZmienKaucje`, `BrakujaceTaryfy`, `ZestawienieKategorii`, `CzyCenaAktualna` |
| `RentalManager.vb` | `CzyKlientMaZaleglosci`, `HistoriaSprzetu`, `StatusDostawy`, `PonowZamowienieKuriera`, `CzySprzetWydany`, `WydajZRezerwacji`, `PrzedluzWypozyczenie`, `PodsumowanieDnia`, `Przypomnienia`, `ZamknijBezZwrotu` |
| `ReservationManager.vb` | `AktywneRezerwacjeKlienta`, `AnulujPrzeterminowane`, `PrzeliczCene`, `ZajeteDni`, `PotwierdzWszystkie`, `ZmienUwagi`, `CzyKlientMaAktywne` |

Przykładowe deklaracje i odpowiadające im wiersze statystyki M10: `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:119` oraz `legacy-app-parent/analysis/results/04-POMIAR-STRUKTURY.md:245`; `legacy-app-parent/legacy-app/WypozyczalniaCore/ReservationManager.vb:184` oraz `legacy-app-parent/analysis/results/04-POMIAR-STRUKTURY.md:460`; `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/EksportCsv.vb:104` oraz `legacy-app-parent/analysis/results/04-POMIAR-STRUKTURY.md:353`.

**Znaczenie dla migracji:** przed pominięciem metody trzeba sprawdzić konsumentów zewnętrznych, refleksję i ewentualne wywołania konfigurowane tekstem. Po weryfikacji pozostałe metody mogą wskazywać nieużywane API lub funkcjonalność bez ścieżki UI; nie przenosić ani usuwać ich wyłącznie na podstawie M10.

## 4) Niepotwierdzone

- Nie potwierdzono, że kandydaci z W6 są martwym kodem; M10 to wyszukiwanie nazw w tekstach `.vb`, nie analiza przepływu ani skan konsumentów zewnętrznych.
- Nie rozstrzygnięto, czy rozbieżność 100% / 150% w W5 jest błędem komentarza, czy implementacji.
- Nie porównywano wyników cen dla pełnego zbioru przypadków ani nie ustalono, czy `ObliczSzacunek` ma celowo przybliżoną semantykę.
- Poza wskazanymi literałami SQL nie wnioskowano o bezpieczeństwie pozostałych zapytań, uprawnieniach ani konfiguracji wdrożenia.
- Nie znaleziono w wynikach tej klasyfikacji innych potwierdzonych komentarzy sprzecznych z działającym kodem; nie jest to dowód, że takich miejsc nie ma.
- Nie klasyfikowano żadnego pliku jako wyjątku bez wzorca: silnik zaliczył wszystkie pliki do zamierzonych kategorii.
