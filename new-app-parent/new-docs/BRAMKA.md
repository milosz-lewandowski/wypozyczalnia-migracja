# Bramka jakości

Uruchomienie z katalogu głównego repo: `.\tools\bramka.ps1`

Bramka sprawdza:
- kompilację i testy backendu (`dotnet test` na `backend/Wypozyczalnia.sln`, w tym testy architektury xUnit)
- typy we frontendzie
- reguły zespołu w kodzie backendu (pliki `.cs` w `backend/src`)
- przypadki akceptacyjne procesów, które są już przeniesione do nowej aplikacji (bramka startuje własny backend na porcie 8089, zmiana: zmienna `BRAMKA_PORT`)

Wymagania: .NET SDK 10 (`dotnet`), Python 3; typy frontendu po `npm ci`.

Wynik: `BRAMKA: OK` albo lista niespełnionych warunków, każdy z powodem.

Kontrakt API (ścieżki, parametry, pola), względem którego bramka sprawdza przeniesione procesy: [KONTRAKT-API.md](KONTRAKT-API.md).
