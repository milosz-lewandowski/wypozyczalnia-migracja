# Przepływy biznesowe w nowej aplikacji — stan migracji

Plik generowany przez skill `migration-supervisor` (`python .agents/skills/migration-supervisor/scripts/supervisor.py raport`); nie edytuj ręcznie. Źródło: [stan-migracji.json](./migracja/stan-migracji.json), rejestr procesów: [procesy.json](../../.agents/skills/migration-supervisor/procesy.json), reguły legacy: [08](../../legacy-app-parent/analysis/results/08-PRZEPLYWY-BIZNESOWE.md).

## Postęp

- zmigrowane: 0/7 = 0,0%
- częściowo (poza %): P2 rezerwacja
- w toku: brak
- pozostało: P1 wycena, P2 rezerwacja, P3 wydanie, P4 zwrot (opłata, kara, kaucja, szkody), P5 faktura i płatność, P6 eksport nocny z importem cennika, P7 klienci

## Procesy

| Proces | Status | Dokument pochodzenia | Wejście legacy | Nowa aplikacja | Ostatnia zmiana |
|---|---|---|---|---|---|
| [P1 — wycena](../../legacy-app-parent/analysis/results/08-PRZEPLYWY-BIZNESOWE.md#p1--wycena) | nierozpoczęty | — | Cennik.btnOblicz_Click `legacy-app-parent/legacy-app/WypozyczalniaWeb/Cennik.aspx.vb:101-129`<br>szacunek Rezerwacja.btnSprawdz_Click `legacy-app-parent/legacy-app/WypozyczalniaWeb/Rezerwacja.aspx.vb:40-74`<br>PricingManager.ObliczOplate / LiczbaDob / RabatProgu `legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:22-60` | — | — |
| [P2 — rezerwacja](../../legacy-app-parent/analysis/results/08-PRZEPLYWY-BIZNESOWE.md#p2--rezerwacja) | częściowo | — | Rezerwacja.btnZarezerwuj_Click `legacy-app-parent/legacy-app/WypozyczalniaWeb/Rezerwacja.aspx.vb:77-119`<br>anulowanie rezerwacji `legacy-app-parent/legacy-app/WypozyczalniaWeb/Rezerwacja.aspx.vb:205`<br>ReservationManager.UtworzRezerwacje `legacy-app-parent/legacy-app/WypozyczalniaCore/ReservationManager.vb:18-59` | `POST /api/reservations`<br>`GET /api/reservations`<br>`GET /api/equipment/available`<br>`RezerwacjaPage.tsx` | 2026-10-22 stan-bazowy |
| [P3 — wydanie](../../legacy-app-parent/analysis/results/08-PRZEPLYWY-BIZNESOWE.md#p3--wydanie) | nierozpoczęty | — | Wypozyczenie.btnWydaj_Click `legacy-app-parent/legacy-app/WypozyczalniaWeb/Wypozyczenie.aspx.vb:169-217`<br>RentalManager.WydajSprzet `legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:20-93` | — | — |
| [P4 — zwrot (opłata, kara, kaucja, szkody)](../../legacy-app-parent/analysis/results/08-PRZEPLYWY-BIZNESOWE.md#p4--zwrot-opłata-kara-kaucja-szkody) | nierozpoczęty | — | Zwrot.btnPodglad_Click / btnZwrot_Click `legacy-app-parent/legacy-app/WypozyczalniaWeb/Zwrot.aspx.vb:154-244`<br>RentalManager.ZarejestrujZwrot / ObliczKare `legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:95-143` | — | — |
| [P5 — faktura i płatność](../../legacy-app-parent/analysis/results/08-PRZEPLYWY-BIZNESOWE.md#p5--faktura-i-płatność) | nierozpoczęty | — | Faktura.btnWystaw_Click / btnZaplac_Click `legacy-app-parent/legacy-app/WypozyczalniaWeb/Faktura.aspx.vb:189-253`<br>InvoiceManager.WystawFaktureZaWypozyczenie / OznaczOplacona `legacy-app-parent/legacy-app/WypozyczalniaCore/InvoiceManager.vb:19-114` | — | — |
| [P6 — eksport nocny z importem cennika](../../legacy-app-parent/analysis/results/08-PRZEPLYWY-BIZNESOWE.md#p6--eksport-nocny-z-importem-cennika) | nierozpoczęty | — | harmonogram CalendarTrigger 02:30 `legacy-app-parent/legacy-app/Jobs/EksportNocny/zadanie-harmonogramu.xml:10-16`<br>Program.Main `legacy-app-parent/legacy-app/Jobs/EksportNocny/Program.vb:21-54`<br>ImportCennikaXml.ImportujPlik `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/ImportCennikaXml.vb:43-95`<br>EksportCsv.EksportujDzien `legacy-app-parent/legacy-app/WypozyczalniaCore/Integracje/EksportCsv.vb:28-65` | — | — |
| [P7 — klienci](../../legacy-app-parent/analysis/results/08-PRZEPLYWY-BIZNESOWE.md#p7--klienci) | nierozpoczęty | — | Klienci.btnSzukaj_Click `legacy-app-parent/legacy-app/WypozyczalniaWeb/Klienci.aspx.vb:48-63`<br>Klienci.btnZapisz_Click / btnZablokuj_Click `legacy-app-parent/legacy-app/WypozyczalniaWeb/Klienci.aspx.vb:95-156`<br>CustomerManager.Zapisz / CzyMozeWypozyczac / ProgRabatu `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:26-98` | — | — |

## Braki procesów częściowych

- P2 rezerwacja: anulowanie rezerwacji z legacy (`legacy-app-parent/legacy-app/WypozyczalniaWeb/Rezerwacja.aspx.vb:205`) nie jest przeniesione
- P2 rezerwacja: cena szacunkowa rezerwacji = 0,00 do czasu przeniesienia wyceny P1 (`legacy-app-parent/legacy-app/WypozyczalniaCore/ReservationManager.vb:38-39`)
