---
name: migration-commit-pr
description: "Finalizacja migracji: dzieli niezakomitowane zmiany po procesie biznesowym P1–P7, dla każdego procesu tworzy dokument pochodzenia z dowodami z legacy, uruchamia review migration-reviewer (CRITICAL/HIGH wracają do migration-implementer, maks. 3 rundy), bramkę jakości, aktualizuje stan w migration-supervisor i changelog, robi commit per proces i przygotowuje PR. Słowa-klucze: commit migracji, PR migracji, podział zmian po procesie, dokumentacja pochodzenia procesu, finalizacja migracji."
---

# Migration Commit & PR

Skill zamienia niezakomitowane zmiany migracji w **commity per proces biznesowy** z dokumentem pochodzenia, przeglądem i aktualnym stanem migracji, a potem w **PR** z opisem procesu i jego źródła w legacy. Wykonuje go orkiestrator, czyli agent prowadzący rozmowę. Implementacja poprawek należy do `migration-implementer`, przegląd do `migration-reviewer`.

Skrót komend: `$sup = "python .agents/skills/migration-supervisor/scripts/supervisor.py"`. W PowerShell 5.1 uruchamiaj komendy pełną ścieżką, bez `&&`.

## Zasady

- Kodu legacy (`legacy-app-parent/legacy-app/`) nie zmieniamy. Legacy jest jedynym autorytetem reguł.
- Jeden commit = jeden proces (plus jego część plików wspólnych). Kolejność commitów wynika z zależności.
- **Nigdy `git commit --no-verify` ani `git push --no-verify`.** Blokada hooka oznacza: napraw stan.
- Zakres zmian tylko dla zadania (DoD 9). Niejednoznaczna reguła to decyzja właściciela procesu (DoD 10).
- Dokumenty po polsku, dowody `plik:linia` w backtickach od korzenia repo, linki względne.

## Workflow

### 1. Inwentaryzacja i plan commitów

```powershell
git status --short
python .agents/skills/migration-supervisor/scripts/supervisor.py klasyfikuj --json
```

- `procesy`: pliki danego procesu. Sprawdź zależności między procesami (np. serwis jednego procesu używany przez inny: `grep` nazw klas). Proces bazowy idzie pierwszy.
- `wspolny`: podział na poziomie zawartości (krok 7). Przypisz każdy fragment do procesu.
- `poza-procesem`: customizacje AI i dokumenty idą do osobnego commitu `chore`/`docs`; eksporty sesji i artefakty pomijasz (`.gitignore`).
- Zapisz plan: commit → proces → pliki → fragmenty plików wspólnych → inne dotknięte procesy (`odnotuj`).

### 2. Stan w supervisorze

Każdy proces z planu musi być `w-toku`:

```powershell
python .agents/skills/migration-supervisor/scripts/supervisor.py status
python .agents/skills/migration-supervisor/scripts/supervisor.py rozpocznij P<n> --agent <agent> --uwaga "prace z sesji <id>"
```

Prace powstałe bez zgody rejestruj `rozpocznij` retroaktywnie, z `--uwaga` o pochodzeniu. Kod `3` oznacza STOP i raport do użytkownika.

### 3. Dokument pochodzenia procesu

Dla każdego procesu utwórz `new-app-parent/new-docs/migracja/P<n>-<slug>.md` z szablonu [pochodzenie-procesu.md](./szablony/pochodzenie-procesu.md). Slugi: `wycena`, `rezerwacja`, `wydanie`, `zwrot`, `faktura`, `eksport-nocny`, `klienci`. Dokumenty różnych procesów można pisać równolegle (subagenci). Każde twierdzenie o legacy ma dowód z wykonywalnego VB/SQL.

```powershell
python tools\sprawdz_dowody.py
```

Wynik musi mieć 0 błędów. Narzędzie potwierdza istnienie dowodów, nie prawdziwość tez.

### 4. Review i pętla poprawek

1. Wywołaj agenta `migration-reviewer` (`.github/agents/migration-reviewer.agent.md`) **jednym przebiegiem** dla wszystkich procesów z planu: przekaż procesy, pliki z `klasyfikuj --json` i dokumenty pochodzenia. Równoległe bramki kolidują na porcie 8089 i w `bin/obj`. Jeśli custom agent nie jest dostępny jako subagent, użyj ogólnego subagenta z tym samym modelem i effortem (Claude Fable 5.1, `medium`), a treść pliku agenta przekaż jako instrukcję.
2. Ustalenia **CRITICAL/HIGH**: orkiestrator **sam** pisze zgłoszenie z szablonu [zgloszenie-poprawki.md](./szablony/zgloszenie-poprawki.md). Opisuje w nim, na czym polega błąd, na czym polega potencjalna podatność lub usterka, dowód, scenariusz, oczekiwane zachowanie i wpływ na parity. Zgłoszenie kieruje do `migration-implementer` (`.github/agents/migration-implementer.agent.md`, tryb B; fallback: ogólny subagent z Claude Opus 5.5, `medium`). Poprawki wykonuj **sekwencyjnie**.
3. Po poprawkach ponowne review: zakres = poprawione ustalenia + regresja.
4. **Maks. 3 rundy.** Gdy po 3. rundzie zostaje CRITICAL/HIGH: STOP i eskalacja do użytkownika z pełnym opisem ustaleń i prób.
5. `parity-legacy` trafia do PR jako decyzja właściciela procesu (DoD 10), bez poprawki. MEDIUM/LOW trafiają do sekcji „Znane uwagi” w PR.

### 5. Bramka jakości

```powershell
.\tools\bramka.ps1
```

Wymagane `BRAMKA: OK`; inaczej STOP. Wynik (liczby testów i przypadków) zapisz do opisu PR.

### 6. Stan migracji i changelog (przed stagingiem danego commitu)

```powershell
python .agents/skills/migration-supervisor/scripts/supervisor.py zakoncz P<n> --status zmigrowany --dokument new-app-parent/new-docs/migracja/P<n>-<slug>.md
python .agents/skills/migration-supervisor/scripts/supervisor.py odnotuj P<m> --opis "<co zmieniono w procesie P<m> i dlaczego>"
```

`czesciowy` wymaga `--braki "brak 1; brak 2"`. Gdy zmiana usuwa brak procesu częściowego, podaj w `odnotuj` aktualną listę `--braki`. Dopisz wpis w `new-app-parent/new-docs/CHANGELOG.md` (sekcja `## [Niewydane]`, podsekcja `### Dodano`, `### Zmieniono` albo `### Naprawiono`). Format wpisu: `- P<n> <nazwa>: <co> (dokument pochodzenia: [P<n>-<slug>](./migracja/P<n>-<slug>.md))`.

### 7. Staging per proces

- Pliki w całości: `git add -- <ścieżki>`.
- Pliki wspólne z fragmentami kilku procesów: wersja pośrednia = `HEAD` + tylko zmiany danego procesu. Zapisz ją do pliku tymczasowego (Python, UTF-8 bez BOM, zachowane zakończenia linii oryginału; nie `Out-File` z PS 5.1) i wpisz do indeksu bez dotykania drzewa roboczego:
  ```powershell
  $blob = git hash-object -w --path <ścieżka> <plik-tymczasowy>
  git update-index --add --cacheinfo "100644,$blob,<ścieżka>"
  ```
  Nie używaj interaktywnego `git add -p`.
- Stan supervisora i raport: w indeksie wersja dla tego commitu (`stan-migracji.json`, `NEW-PRZEPLYWY-BIZNESOWE.md`, `CHANGELOG.md`). Gdy commity kilku procesów powstają z jednego drzewa, aktualizuj stan **między** commitami: `zakoncz` dla kolejnego procesu dopiero po commicie poprzedniego.
- Nowe skrypty wykonywalne: `git update-index --chmod=+x <ścieżka>`.
- Kontrola:
  ```powershell
  git diff --cached --stat
  python .agents/skills/migration-supervisor/scripts/supervisor.py klasyfikuj --staged
  ```

### 8. Commit

Wiadomość z szablonu [commit.md](./szablony/commit.md), zapisana do pliku tymczasowego:

```powershell
git commit -F <plik-wiadomości>
```

Hook pre-commit (`.githooks/pre-commit` → `supervisor.py hook-pre-commit`) sprawdza stan. Przy blokadzie popraw stan, dodaj go do indeksu i powtórz. Bez `--no-verify`.

### 9. Weryfikacja commitu w izolacji

Każdy commit musi się budować i przechodzić testy **bez** zmian kolejnych procesów:

```powershell
$wt = Join-Path $env:TEMP "wt-<sha>"
git worktree add --detach $wt <sha>
dotnet test (Join-Path $wt "new-app-parent/new-app/backend/Wypozyczalnia.sln") --nologo
git worktree remove --force $wt
```

Czerwony wynik: `git reset --soft HEAD~1`, popraw podział i commituj ponownie.

### 10. Po wszystkich commitach

```powershell
.\tools\bramka.ps1
python .agents/skills/migration-supervisor/scripts/supervisor.py weryfikuj
python .agents/skills/migration-supervisor/scripts/supervisor.py info
git status --short
```

Oczekiwane: `BRAMKA: OK`, weryfikacja zgodna, czyste drzewo (poza ignorowanymi plikami).

### 11. Push i PR

```powershell
git push -u origin <gałąź>
```

- Push zablokowany przez hook `pre-push` (np. globalny guard użytkownika): **nie obchodź go**. Przekaż użytkownikowi komendę do wykonania.
- Opis PR z szablonu [pr.md](./szablony/pr.md) zapisz w katalogu sesji (artefakt, nie w repo). Dołącz link `https://github.com/<owner>/<repo>/compare/<baza>...<gałąź>?expand=1`.
- W Copilot cloud agencie commity i PR tworzy platforma: nie pushuj ręcznie, a szablon PR wykorzystaj jako opis PR.

## Definition of Done skilla

- Każdy proces z planu ma osobny commit, a commit buduje się i przechodzi testy w izolacji.
- Każdy proces ma dokument pochodzenia z dowodami (`sprawdz_dowody.py`: 0 błędów).
- Review: werdykt bez otwartych CRITICAL/HIGH (maks. 3 rundy); uwagi trafiają do PR.
- `BRAMKA: OK` na końcowym HEAD.
- Stan w supervisorze zaktualizowany (`zakoncz`/`odnotuj`), raport i changelog aktualne; hook przepuścił commity bez `--no-verify`.
- Opis PR: co, dlaczego, skąd w legacy, jak sprawdzone, wynik review, postęp migracji (`info`).

## Przykład: P1 wycena i P4 zwrot na `m3-run-001`

- `klasyfikuj` daje P1 (Pricing*, Tariff*, `WycenaPage.tsx`, `api/pricing.ts`), P2 (`ReservationService.cs` i testy rezerwacji: cena szacunkowa = wycena), P4 (Rental*, `ZwrotPage.tsx`, `api/rentals.ts`) oraz pliki wspólne: `ServiceRegistration.cs`, `QueryParameters.cs`, `KONTRAKT-API.md`.
- Zależność: `RentalService` używa `PricingService`, więc najpierw commit P1, potem P4.
- `ServiceRegistration.cs` ma rejestracje P1 i P4 w jednym hunku: w commicie P1 wersja pośrednia z `TariffRepository` i `PricingService`, a `RentalRepository` i `RentalService` dochodzą w commicie P4. `QueryParameters.RequireLong` należy do P1. `KONTRAKT-API.md`: sekcja wyceny w commicie P1, sekcje zwrotu w P4.
- Commit P1: `zakoncz P1` + `odnotuj P2 --opis "cena szacunkowa rezerwacji = wycena P1" --braki "<pozostałe braki P2>"` (usuwa rozwiązany brak ceny 0,00). Commit P4: `zakoncz P4`. Po obu `info` daje 2/7 = 28,6%.

## Typowe błędy

- CRLF/BOM po zapisie z PowerShell 5.1: zapisuj przez Pythona z `newline` zgodnym z oryginałem.
- Hunk mieszany kilku procesów: tylko wersja pośrednia przez `hash-object` + `update-index`.
- Commit P1 nie kompiluje się bez P4 (np. rejestracja `RentalService` w DI): wychwyci to weryfikacja w worktree.
- `zakoncz` drugiego procesu przed commitem pierwszego: hook nie odróżni procesów w stanie, a historia commitów stanie się nieczytelna.
