# tools/

Narzędzia pomocnicze repo (analiza legacy i nowej aplikacji). Uruchamiane z korzenia repo.

| narzędzie | wersja | do czego | uruchomienie |
|---|---|---|---|
| `klasyfikuj.py` | - (własny, Python 3, tylko biblioteka standardowa) | klasyfikacja plików legacy według wzorców z `tools/wzorce.json` (reguły dopisuje człowiek albo agent); bez `wzorce.json` kończy się komunikatem „brak reguł” | `python tools/klasyfikuj.py` (`--json`, `--reguly <plik>`) |
| `cloc-2.10.exe` | cloc 2.10 (Windows x64) | liczba plików i linii per język | `tools\cloc-2.10.exe legacy-app-parent/legacy-app` |
| `sprawdz_dowody.py` | - (własny, Python 3, tylko biblioteka standardowa) | sprawdza, że dowody `plik:linia` w dokumentach `*.md` istnieją (nie że twierdzenie jest prawdziwe) | `python tools/sprawdz_dowody.py` |
| `bramka.ps1` + `bramka/` | - (własna: PowerShell + Python 3) | bramka jakości nowej aplikacji: build i testy backendu (`dotnet test`), typy frontendu, reguły zespołu, przypadki akceptacyjne przez API; opis w `new-app-parent/new-docs/BRAMKA.md` | `.\tools\bramka.ps1` |
| `uruchom-backend.ps1` | - (własny, PowerShell) | backend z hot reloadem (`dotnet watch`, port 8080) | `.\tools\uruchom-backend.ps1` |
| `uruchom-frontend.ps1` | - (własny, PowerShell) | frontend (`npm run dev`, Vite, port 5173) | `.\tools\uruchom-frontend.ps1` |
| `uruchom-wszystko.ps1` | - (własny, PowerShell) | backend i frontend w osobnych kartach terminala | `.\tools\uruchom-wszystko.ps1` |
| `restart-backend.ps1` | - (własny, PowerShell) | restart backendu | `.\tools\restart-backend.ps1` |
| `zatrzymaj.ps1` | - (własny, PowerShell) | zatrzymanie, zwalnia porty 8080 i 5173 | `.\tools\zatrzymaj.ps1` |

Uruchamianie aplikacji krok po kroku: `new-app-parent/new-docs/URUCHOMIENIE.md`.

## cloc - pochodzenie i weryfikacja

Binarka w repo od `s1-start`, zatwierdzona po sprawdzeniu sumy (rozmiar i sha256 zgodne, `--version` = 2.10).

- źródło: oficjalny release autora, https://github.com/AlDanial/cloc/releases/download/v2.10/cloc-2.10.exe
- rozmiar: 10002926 bajtów
- sha256: `97786C4B5BB71D2BE67E42061A2F5D03DB73BBF88228F8710001D770E867BBEF` (zapis w `SHA256SUMS.txt`)
- licencja: GPL-2.0 (tekst: `cloc-LICENSE.txt`); źródło: https://github.com/AlDanial/cloc/tree/v2.10

Sprawdzenie przed pierwszym uruchomieniem (PowerShell, w katalogu `tools`; czysty odczyt bajtów):

```powershell
(Get-FileHash -Algorithm SHA256 .\cloc-2.10.exe).Hash -eq '97786C4B5BB71D2BE67E42061A2F5D03DB73BBF88228F8710001D770E867BBEF'
```

Linux/macOS: `sha256sum -c SHA256SUMS.txt` w katalogu `tools`.

Suma niezgodna = nie uruchamiaj pliku na swoim komputerze.
