---
name: migration-supervisor
description: "Strażnik postępu i stanu migracji procesów P1–P7 z legacy do new-app. Użyj, gdy pytanie dotyczy postępu migracji (flaga info: ile procent procesów zmigrowano, ile zostało), stanu procesu, zgody na rozpoczęcie migracji (guard przed legacy-migration-guide), zakończenia albo odnotowania zmian w procesie, klasyfikacji zmian po procesie albo hooka pre-commit. Słowa-klucze: migration-supervisor info, postęp migracji, stan migracji, zgoda na migrację, guard, pre-commit."
---

# Migration Supervisor

Skill pilnuje **stanu i postępu** migracji procesów biznesowych z `legacy-app-parent/legacy-app/` do `new-app-parent/new-app/`. Jest strażnikiem (guard): **żaden proces nie może być migrowany bez zgody supervisora** (`rozpocznij`). Żaden commit plików procesu nie przejdzie bez aktualizacji stanu (`zakoncz` / `odnotuj`): pilnuje tego hook pre-commit.

- CLI: `python .agents/skills/migration-supervisor/scripts/supervisor.py <komenda>` (z korzenia repo, Python 3, tylko biblioteka standardowa)
- stan: [stan-migracji.json](../../../new-app-parent/new-docs/migracja/stan-migracji.json), nie edytuj ręcznie
- raport (generowany): [NEW-PRZEPLYWY-BIZNESOWE.md](../../../new-app-parent/new-docs/NEW-PRZEPLYWY-BIZNESOWE.md)
- rejestr procesów: [procesy.json](./procesy.json) (P1–P7 z [08](../../../legacy-app-parent/analysis/results/08-PRZEPLYWY-BIZNESOWE.md): wejścia legacy z dowodami, wzorce plików, sygnały kodu, endpointy)
- dokumenty pochodzenia: `new-app-parent/new-docs/migracja/P<n>-<slug>.md`

## Flaga `info`

Gdy użytkownik woła „migration-supervisor info” (albo pyta o postęp), uruchom:

```powershell
python .agents/skills/migration-supervisor/scripts/supervisor.py info
```

i oddaj wynik: procent zmigrowanych procesów z 7, procesy częściowe z brakami (nie wliczają się do %), procesy w toku oraz to, ile i jakich procesów jeszcze brakuje. `--json` daje to samo w formie maszynowej.

## Obowiązek konsultacji przed migracją (guard)

Przed każdą migracją procesu (skill `legacy-migration-guide`, agent `migration-implementer`):

```powershell
python .agents/skills/migration-supervisor/scripts/supervisor.py rozpocznij P<n> --agent <nazwa-agenta> [--uwaga "..."]
```

- kod `0`: ZGODA, proces przechodzi w `w-toku` na bieżącej gałęzi;
- kod `3`: **ODMOWA**. Nie wolno migrować, zgłoś powód użytkownikowi. Odmowa pada, gdy proces jest już `zmigrowany` (zmiany wtedy przez `odnotuj`), jest `w-toku` na innej gałęzi albo jest `czesciowy` bez `--dokonczenie`.
- Proces `w-toku` może zakończyć (`zakoncz`), porzucić (`porzuc`) albo odnotować (`odnotuj`) tylko gałąź, która dostała zgodę.

## Statusy

| Status | Znaczenie | W procencie postępu |
|---|---|---|
| `nierozpoczety` | proces tylko w legacy | nie |
| `w-toku` | zgoda wydana, migracja trwa (gałąź, agent) | nie |
| `czesciowy` | część procesu w new-app, lista braków | nie (pokazywany osobno) |
| `zmigrowany` | proces przeniesiony 1:1, dokument pochodzenia istnieje | tak |

## Komendy

| Komenda | Opis |
|---|---|
| `info [--json]` | postęp: % zmigrowanych (`zmigrowany`/7), częściowe z brakami, w toku, pozostałe |
| `status [P<n>] [--json]` | szczegóły procesu (ostatnie wpisy historii) |
| `rozpocznij P<n> [--agent A] [--galaz G] [--uwaga U] [--dokonczenie]` | guard: zgoda na migrację |
| `zakoncz P<n> --status zmigrowany\|czesciowy --dokument D [--braki "a; b"] [--uwaga U]` | koniec migracji; wymaga `w-toku` i istniejącego dokumentu pochodzenia; `czesciowy` wymaga `--braki` |
| `odnotuj P<n> --opis O [--braki "a; b"]` | zmiana w procesie `zmigrowany`/`czesciowy`/`w-toku` (np. P2 dotknięty przy migracji P1); `--braki` zastępuje listę braków procesu `czesciowy` (usunięte braki trafiają do historii) |
| `porzuc P<n> [--uwaga U]` | `w-toku` → poprzedni status |
| `klasyfikuj [--staged] [--json]` | zmienione pliki → `P<n>` / `wspolny` (podział na poziomie zawartości) / `poza-procesem` |
| `weryfikuj [--json]` | spójność stanu z kodem new-app: kod procesu przy statusie `nierozpoczety`, `zmigrowany` bez dokumentu albo z zaślepką strony, nieaktualny raport |
| `raport` | regeneracja `NEW-PRZEPLYWY-BIZNESOWE.md` (robią to też `rozpocznij`/`zakoncz`/`odnotuj`/`porzuc`) |
| `hook-pre-commit` | sprawdzenie indeksu przed commitem (wywołuje `.githooks/pre-commit`) |
| `zainstaluj-hooki` | lokalnie `core.hooksPath=.githooks` |

Opcja globalna `--repo <ścieżka>` wskazuje inny korzeń repozytorium (testy).

Kody wyjścia: `0` OK, `1` błąd weryfikacji albo blokada hooka, `2` złe argumenty albo brak plików, `3` odmowa guarda.

## Hook pre-commit

Instalacja (raz na klon; w Copilot cloud agencie robi to `copilot-setup-steps.yml`):

```powershell
python .agents/skills/migration-supervisor/scripts/supervisor.py zainstaluj-hooki
```

Hooki w `.githooks/` przekazują wywołanie do globalnego `core.hooksPath` użytkownika (np. globalny guard `pre-push`), więc lokalne ustawienie go nie wyłącza. Gdy skonfigurowany globalny katalog nie istnieje, hook kończy się błędem (fail-closed).

Hook blokuje commit, gdy dla procesu z plikami w indeksie:
- `stan-migracji.json` nie jest w indeksie albo nie ma nowego wpisu historii tego procesu względem `HEAD`;
- status to `nierozpoczety` albo `w-toku` bez wpisu `odnotuj`, czyli praca nie jest zakończona przez `zakoncz`;
- dokument pochodzenia nie jest w indeksie, gdy status to `zmigrowany` albo wykonano `zakoncz`.

Pliki `wspolny` bez plików procesu dają tylko ostrzeżenie. **`git commit --no-verify` jest zabronione.** Przy blokadzie napraw stan komendą supervisora i dodaj plik stanu do indeksu.

## Przepływ

1. `info`: sprawdź postęp i wybierz proces.
2. `rozpocznij P<n> --agent migration-implementer`: zgoda (kod 3 = stop).
3. Implementacja: agent `migration-implementer` + skill `legacy-migration-guide`.
4. Finalizacja: skill `migration-commit-pr` (dokument pochodzenia, review `migration-reviewer`, bramka, `zakoncz`/`odnotuj`, commit per proces, PR).
5. Hook pre-commit sprawdza, czy stan zgadza się z commitem.

## Testy skilla

```powershell
python -m unittest discover -s .agents/skills/migration-supervisor/scripts -p "test_*.py" -v
```
