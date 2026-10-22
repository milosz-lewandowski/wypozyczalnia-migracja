# Mapa nawigacji po legacy

Punkt startowy do rozpoznania i migracji procesów wypożyczalni. Analizy 06–08 weryfikują kod; 03 zachowuje hipotezy sprzed jego lektury. Analiza statyczna nie dowodzi wykonania ani konfiguracji produkcyjnej. Źródła legacy pozostają tylko do odczytu, bez kompilowania.

## 1. Pytanie → analiza albo narzędzie

| Pytanie | Gdzie zacząć |
|---|---|
| Ile kodu i jakich języków obejmuje migracja? | [01 — wynik CLOC](./01-POMIAR-CLOC.md); [CLOC](../../../tools/cloc-2.10.exe). Nie mieszać linii `code` z liniami fizycznymi ani niepustymi. |
| Jaka platforma, konfiguracja i integracje? | [02 — stos technologiczny](./02-STOS-TECHNOLOGICZNY.md), następnie potwierdzenia granic w [06](./06-ARCHITEKTURA.md). |
| Skąd pochodzą hipotezy H1–H14? | [03 — hipoteza architektury](../temp/03-ARCHITEKTURA-HIPOTEZA.md); aktualna weryfikacja w sekcji 2 [06](./06-ARCHITEKTURA.md). |
| Gdzie są twarde wyniki skanu: pliki, zależności, SQL, sesja, kandydaci metod? | [04 — pomiar struktury](./04-POMIAR-STRUKTURY.md), M1–M24; [narzędzie pomiarowe](../../../tools/pomiar_struktury_legacy.py). M3 i M10 są leksykalne. |
| Jakie wzorce i wyjątki rzeczywiście znaleziono? | [05 — klasyfikacja i W1–W6](./05-KLASYFIKACJA-WYJATKI.md); [klasyfikator](../../../tools/klasyfikuj.py) i [reguły](../../../tools/wzorce.json). 100% klasyfikacji nie oznacza braku wyjątków. |
| Jak działają warstwy, granice i dostęp do tabel? | [06 — architektura](./06-ARCHITEKTURA.md), sekcje 3–5; nie zakładać szczelnego Web → Manager → DAL → SQL. |
| Gdzie jest drzewo i graf zależności, huby oraz ograniczenia grafu? | [07 — struktura i zależności](./07-STRUKTURA-ZALEZNOSCI.md). Ścieżka między plikami nie jest stosem runtime. |
| Gdzie są reguły P1–P7, walidacje, statusy i transakcje? | [08 — przepływy biznesowe](./08-PRZEPLYWY-BIZNESOWE.md); skrót wejść w tabeli poniżej. |
| Czy wskazane dowody istnieją? | [Weryfikator dowodów](../../../tools/sprawdz_dowody.py): z korzenia repo `python tools\sprawdz_dowody.py`. Sprawdza ścieżki i linie, nie prawdziwość interpretacji. |

## 2. Odpowiedzialność biznesowa → główny plik i wyjątek W

W1–W6 oznaczają wyłącznie wyjątki z sekcji 3 [05](./05-KLASYFIKACJA-WYJATKI.md), nie nowe kategorie. Główny plik jest wejściem do analizy, nie jedynym miejscem reguł.

| Odpowiedzialność | Główny plik kodu i dowód | Wyjątek / dalszy trop |
|---|---|---|
| Klienci, dopuszczenie i próg rabatu | [CustomerManager](../../legacy-app/WypozyczalniaCore/CustomerManager.vb); `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:26-98` | W1: SQL bez CustomerDAL; W2: wyszukiwanie także w Klienci.aspx.vb; W6: pomocnicze API. |
| Sprzęt, dostępność i status | [EquipmentManager](../../legacy-app/WypozyczalniaCore/EquipmentManager.vb); `legacy-app-parent/legacy-app/WypozyczalniaCore/EquipmentManager.vb:45-76` | W6: nie utożsamiać wszystkich metod z aktywną ścieżką UI; dostępność realizuje też ReservationDAL i SQL. |
| Taryfy, doby i wycena | [PricingManager](../../legacy-app/WypozyczalniaCore/PricingManager.vb); `legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:22-60` | W1: pomocniczy SQL; W2: zapis stawki w Cennik; W3/W4: odmienny szacunek i kopia procentu w Rezerwacja; W6. |
| Rezerwacje i przejścia statusów | [ReservationManager](../../legacy-app/WypozyczalniaCore/ReservationManager.vb); `legacy-app-parent/legacy-app/WypozyczalniaCore/ReservationManager.vb:18-59` | W1: klient przez CustomerManager; W3/W4: szacunek UI nie jest ceną zapisu; W6. |
| Wydanie, zwrot, kara i kaucja | [RentalManager](../../legacy-app/WypozyczalniaCore/RentalManager.vb); `legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:95-143` | W5: wykonywany mnożnik kary 1,5, nie komentarz 100%; W1/W4: wycena klienta; W6. |
| Faktura i lokalny stan płatności | [InvoiceManager](../../legacy-app/WypozyczalniaCore/InvoiceManager.vb); `legacy-app-parent/legacy-app/WypozyczalniaCore/InvoiceManager.vb:19-114` | W4: fallback VAT; W1: klient; W6. SOAP inicjuje strona Faktura, nie manager. |
| Import taryf i stawek | [ImportCennikaXml](../../legacy-app/WypozyczalniaCore/Integracje/ImportCennikaXml.vb); `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/ImportCennikaXml.vb:43-95` | W1: kaucja przez DbHelper; import omija PricingManager; W6: SprawdzPlik nie jest dowiedzionym etapem importu. |
| Nocna orkiestracja i eksport | [Program](../../legacy-app/Jobs/EksportNocny/Program.vb) i [EksportCsv](../../legacy-app/WypozyczalniaCore/Integracje/EksportCsv.vb); `legacy-app-parent/legacy-app/Jobs/EksportNocny/Program.vb:21-54` | W6: EksportujOkres nie jest automatycznie etapem zadania; szczegóły błędów i plików w P6. |

## 3. Proces P1–P7 → strona / logika / dane / sekcja 08

Nazwy DAL oznaczają pliki w [katalogu DAL](../../legacy-app/WypozyczalniaCore/DAL/), a procedury mają definicje w [02_procedury.sql](../../legacy-app/Database/02_procedury.sql). Schemat tabel: [01_schema.sql](../../legacy-app/Database/01_schema.sql). Szczegółowe dowody kolejnych kroków zawierają wskazane sekcje 08.

| Proces | Strona / wejście | Logika | Dane / droga zapisu lub odczytu | Sekcja 08 |
|---|---|---|---|---|
| P1 — wycena | Cennik.aspx, alternatywny szacunek Rezerwacja.aspx | PricingManager.ObliczOplate; szacunek UI ObliczSzacunek | EquipmentDAL, PricingDAL → usp_PobierzTaryfe; CustomerManager → DbHelper; Sprzet, Taryfy, Klienci | [P1](./08-PRZEPLYWY-BIZNESOWE.md#p1--wycena) |
| P2 — rezerwacja | Rezerwacja.aspx | ReservationManager.UtworzRezerwacje; EquipmentManager.SprawdzDostepnosc | ReservationDAL → usp_SprawdzDostepnosc / usp_ZapiszRezerwacje / usp_ZmienStatusRezerwacji; Rezerwacje, Sprzet, Klienci, Wypozyczenia, Taryfy, DziennikOperacji | [P2](./08-PRZEPLYWY-BIZNESOWE.md#p2--rezerwacja) |
| P3 — wydanie | Wypozyczenie.aspx | RentalManager.WydajSprzet; opcjonalnie KurierClient | RentalDAL → usp_WydajSprzet; dalsze aktualizacje statusów i przesyłki; Wypozyczenia, Sprzet, Rezerwacje, Klienci, DziennikOperacji | [P3](./08-PRZEPLYWY-BIZNESOWE.md#p3--wydanie) |
| P4 — zwrot | Zwrot.aspx | RentalManager.ZarejestrujZwrot / ObliczKare; PricingManager | RentalDAL → usp_ZarejestrujZwrot; EquipmentDAL → UPDATE; Wypozyczenia, Sprzet, Klienci, Taryfy, DziennikOperacji | [P4](./08-PRZEPLYWY-BIZNESOWE.md#p4--zwrot-opłata-kara-kaucja-szkody) |
| P5 — faktura i płatność | Faktura.aspx; SOAP z code-behind | InvoiceManager.WystawFaktureZaWypozyczenie / OznaczOplacona | InvoiceDAL → usp_WystawFakture; pozycje i płatność przez SQL inline; Faktury, PozycjeFaktur, Wypozyczenia, Klienci, Sprzet, DziennikOperacji | [P5](./08-PRZEPLYWY-BIZNESOWE.md#p5--faktura-i-płatność) |
| P6 — import i eksport nocny | Brak strony nocnej: harmonogram → Program.Main; ręcznie także Cennik/Faktura | ImportCennikaXml, EksportCsv, RentalManager, InvoiceManager | PricingDAL → usp_ZapiszTaryfe / UPDATE; DbHelper w imporcie; RentalDAL/InvoiceDAL → SELECT; XML → Taryfy/Sprzet/dziennik, dane wypożyczeń i faktur → CSV | [P6](./08-PRZEPLYWY-BIZNESOWE.md#p6--eksport-nocny-z-importem-cennika) |
| P7 — klienci | Klienci.aspx | CustomerManager.Zapisz / Zablokuj / CzyMozeWypozyczac / ProgRabatu | DbHelper bez osobnego DAL i procedur; wyszukiwanie przyciskiem także bez managera; Klienci, DziennikOperacji | [P7](./08-PRZEPLYWY-BIZNESOWE.md#p7--klienci) |

## 4. Czego nie szukać — brak pliku a niepotwierdzona osiągalność

- **Pliki nieistniejące:** w inwentarzu M1 [04](./04-POMIAR-STRUKTURY.md) nie ma CustomerDAL.vb ani ZwrotManager.vb. Nie są „martwym kodem”: nie ma takich plików. Klienci mają SQL w CustomerManager, a wydanie i zwrot współdzielą RentalManager ([06](./06-ARCHITEKTURA.md), H6–H7). Nie dopisywać hipotetycznych kontrolerów MVC, repozytoriów ORM ani osobnej procedury eksportu do mapy legacy.
- **Brak dokumentu to brak informacji:** 08 wskazuje brak NEW-PRZEPLYWY-BIZNESOWE.md w new-app-parent/new-docs; nie wynika z tego, że procesy w nowej aplikacji nie istnieją.
- **Istniejący kod wymagający rozpoznania:** W6 w [05](./05-KLASYFIKACJA-WYJATKI.md) opisuje 61 kandydatów z M10, m.in. Odblokuj, TerminPlatnosci, EksportujOkres i SprawdzPlik. M10 liczy wystąpienia nazw w VB; **nie dowodzi martwoty**, nie bada pełnego przepływu, refleksji, wiązań ani konsumentów zewnętrznych.
- **Equals może być użyte niejawnie**, np. przez mechanizmy porównywania obiektów; brak jawnego dodatkowego wystąpienia nazwy nie uzasadnia usunięcia nadpisanej metody. Przed pominięciem kandydatów sprawdzić kontrakty i konsumentów, nie przenosić ani nie usuwać automatycznie.
- Brak krawędzi M3 nie oznacza braku integracji: zadanie rzeczywiście tworzy adaptery (`legacy-app-parent/legacy-app/Jobs/EksportNocny/Program.vb:62-83`), choć graf pomiarowy pozostawia je poza zakresem celów konstruktorów.
