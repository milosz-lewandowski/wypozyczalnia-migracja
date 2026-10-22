# Szablon opisu PR

```markdown
# Migracja: <P<n> nazwa>[, <P<m> nazwa>] z legacy do new-app

## Podsumowanie
<1–3 zdania: jakie procesy przeniesiono, z jakiej gałęzi do jakiej bazy>

## P<n> — <nazwa>
- **Co:** <endpointy, serwisy, UI, testy>
- **Dlaczego:** <cel biznesowy, co odblokowuje, np. cena szacunkowa rezerwacji>
- **Skąd (legacy):** <wejścia UI, logika, dane z dowodami>; dokument pochodzenia: [P<n>-<slug>](new-app-parent/new-docs/migracja/P<n>-<slug>.md)
- **Jak sprawdzone:** dotnet test <N>; przypadki bramki <proces> <N>/<N>; `BRAMKA: OK`
- **Review (`migration-reviewer`):** <werdykt>; rundy poprawek: <N>; naprawione: <ID ustaleń>
- **Znane uwagi:** <MEDIUM/LOW, parity-legacy do decyzji właściciela procesu> | brak

## Inne dotknięte procesy
- P<m>: <co i dlaczego> (odnotowane w supervisorze) | brak

## Postęp migracji
<wynik `supervisor.py info`>

## Commity
- <sha> <nagłówek>

## Checklista DoD
- [ ] 1. zachowanie zgodne z legacy (kwoty, progi, zaokrąglenia, komunikaty)
- [ ] 2. warstwy kontroler → serwis → repozytorium; SQL tylko w repozytorium; DTO na granicy
- [ ] 3. nazwy klas wg roli, DTO w `Wypozyczalnia.Dto`
- [ ] 4. kwoty bez liczb zmiennoprzecinkowych, zaokrąglenie bankierskie
- [ ] 5. backend i frontend kompilują się
- [ ] 6. testy dla każdego nowego serwisu
- [ ] 7. format błędów API jak w istniejących modułach
- [ ] 8. bezpieczeństwo: walidacja, brak sklejania SQL, brak HTML z danych, dane osobowe dla ról
- [ ] 9. zakres tylko zadania
- [ ] 10. niejednoznaczności przekazane właścicielowi procesu
- [ ] 11. opis PR + wpis w changelogu
- [ ] 12. bramka zielona

## Changelog
<wpisy dodane do new-app-parent/new-docs/CHANGELOG.md>
```
