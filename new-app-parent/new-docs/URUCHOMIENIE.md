# Uruchomienie nowej aplikacji - ściąga

Wymagania: .NET SDK 10, Node.js z npm, Python 3 (narzędzia w `tools/`). Komendy wpisujesz w PowerShellu w **korzeniu repo**.

| czynność | komenda do skopiowania |
|---|---|
| uruchom wszystko (2 karty terminala: `backend:8080`, `frontend:5173`) | `.\tools\uruchom-wszystko.ps1` |
| sam backend (w bieżącym oknie) | `.\tools\uruchom-backend.ps1` |
| sam frontend (w bieżącym oknie) | `.\tools\uruchom-frontend.ps1` |
| przeładuj backend (nowa karta `backend:8080`) | `.\tools\restart-backend.ps1` |
| przeładuj backend w karcie backendu | `Ctrl+R` w tej karcie |
| hot reload frontu | nic - zapis pliku odświeża przeglądarkę |
| testy backendu | `dotnet test new-app-parent\new-app\backend\Wypozyczalnia.sln` |
| bramka jakości (testy, typy, reguły, akceptacja) | `.\tools\bramka.ps1` (Linux: `pwsh tools/bramka.ps1`) |
| zatrzymaj wszystko (porty 8080 i 5173) | `.\tools\zatrzymaj.ps1` |
| port zajęty przez proces spoza repo | `.\tools\zatrzymaj.ps1 -Wymus` |
| pierwsze uruchomienie frontu (brak `node_modules`) | `cd new-app-parent\new-app\frontend; npm ci; cd ..\..\..` |
| build frontu | `cd new-app-parent\new-app\frontend; npm run build; cd ..\..\..` |
| skrypty zablokowane przez politykę wykonywania | `powershell -ExecutionPolicy Bypass -File .\tools\uruchom-wszystko.ps1` |

Adresy: aplikacja `http://localhost:5173` · backend `http://localhost:8080/api/ping` · przez proxy frontu `http://localhost:5173/api/ping` (Vite przekazuje `/api` na 8080). Baza: SQLite w pamięci, dane startowe z `Database/schema.sql` i `Database/data.sql` przy każdym starcie backendu.

## Co przeładowuje się samo

- **Frontend** (Vite HMR): każda zmiana w `frontend/src` - od razu w przeglądarce, bez restartu.
- **Backend** (`dotnet watch`): zmiana w ciele metody → hot reload w działającej aplikacji (stan bazy zostaje).
  Zmiana, której hot reload nie obsłuży („rude edit", np. zmiana sygnatury metody, konstruktora, atrybutu) → backend sam się restartuje bez pytania, bo skrypt ustawia `DOTNET_WATCH_RESTART_ON_RUDE_EDIT=1`. Restart = baza w pamięci wraca do danych startowych.

## Co wymaga `.\tools\restart-backend.ps1` (albo `Ctrl+R` w karcie backendu)

- zmiana `.csproj`, nowy pakiet NuGet, `Program.cs`, `appsettings*.json`, `Database/*.sql`;
- backend nie odpowiada albo zachowuje się dziwnie po serii hot reloadów;
- potrzebny czysty stan bazy (dane startowe).

Port zajęty przy starcie: skrypt wypisze, co go trzyma, i komendę `.\tools\zatrzymaj.ps1`. `zatrzymaj.ps1` zatrzymuje tylko procesy z tego repo (cały `dotnet watch` albo `npm` z Vite); obcy proces zostawia i podpowiada `-Wymus`.
