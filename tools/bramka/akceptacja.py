"""Przypadki akceptacyjne bramki: sprawdza nową aplikację jak tester, z zewnątrz, przez API.

Kroki:
  1. buduje backend (dotnet publish do katalogu tymczasowego) i uruchamia go (dotnet <dll>) na osobnym porcie
     (domyślnie 8089, ASPNETCORE_URLS; SQLite w pamięci), z dodatkowymi danymi przypadków
     (pole "dane_sql" w plikach przypadków, przekazane przez Database__ExtraDataScripts);
  2. czeka na GET /api/ping;
  3. dla każdego pliku tools/bramka/przypadki-*.json: sonda endpointu procesu; 404 albo 405 i brak plików
     procesu w src (nazwy plików bez bin/ i obj/, pole "wykrycie") = proces nie przeniesiony, pominięty (to nie błąd); 404 albo 405,
     a pliki procesu są = "nie" z odesłaniem do kontraktu API (pole "kontrakt");
     inaczej wysyła każdy przypadek i porównuje wynik;
  4. wypisuje tylko rozbieżności ("wycena-07 [amount]: oczekiwano 478,15, jest 478,16");
  5. zatrzymuje backend (całe drzewo procesów).

Użycie: python tools/bramka/akceptacja.py <korzeń_repo> [--backend <ścieżka względna>] [--port 8089]
Kod wyjścia: 0 = brak rozbieżności (procesy nieprzeniesione pominięte), 1 = rozbieżności albo backend
nie wstał, 2 = złe argumenty.
"""
import glob
import json
import os
import signal
import socket
import subprocess
import sys
import tempfile
import time
import urllib.error
import urllib.parse
import urllib.request
from decimal import Decimal, InvalidOperation

BACKEND_DOMYSLNY = os.path.join("new-app-parent", "new-app", "backend")
PORT_DOMYSLNY = 8089
CZAS_STARTU_S = 120
KATALOG_BRAMKI = os.path.dirname(os.path.abspath(__file__))
NA_WINDOWS = os.name == "nt"


def wypisz(tekst=""):
    print(tekst, flush=True)


def ogon_pliku(sciezka, linii=25):
    try:
        with open(sciezka, encoding="utf-8", errors="replace") as plik:
            return "".join(plik.readlines()[-linii:])
    except OSError:
        return ""


PROJEKT_API = os.path.join("src", "Wypozyczalnia.Api", "Wypozyczalnia.Api.csproj")
DLL_API = "Wypozyczalnia.Api.dll"
POMIJANE_KATALOGI = {"bin", "obj"}


def zbuduj_backend(katalog_backendu, katalog_wyjscia, log):
    """dotnet publish projektu API do katalogu tymczasowego; zwraca ścieżkę dll albo None."""
    polecenie = ["dotnet", "publish", os.path.join(katalog_backendu, PROJEKT_API),
                 "-c", "Release", "-o", katalog_wyjscia, "--nologo"]
    with open(log, "w", encoding="utf-8", errors="replace") as wyjscie:
        try:
            wynik = subprocess.run(polecenie, cwd=katalog_backendu, stdout=wyjscie, stderr=subprocess.STDOUT)
        except OSError as blad:
            wyjscie.write("nie da się uruchomić dotnet: %s\n" % blad)
            return None
    dll = os.path.join(katalog_wyjscia, DLL_API)
    if wynik.returncode != 0 or not os.path.isfile(dll):
        return None
    return dll


def port_zajety(port):
    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as gniazdo:
        gniazdo.settimeout(1)
        return gniazdo.connect_ex(("127.0.0.1", port)) == 0


def uruchom_backend(dll, port, pliki_danych, log):
    srodowisko = dict(os.environ)
    srodowisko["ASPNETCORE_URLS"] = "http://localhost:%d" % port
    srodowisko.pop("Database__ExtraDataScripts", None)
    if pliki_danych:
        srodowisko["Database__ExtraDataScripts"] = ";".join(os.path.abspath(p) for p in pliki_danych)
    polecenie = ["dotnet", dll]
    katalog_aplikacji = os.path.dirname(dll)
    wyjscie = open(log, "w", encoding="utf-8", errors="replace")
    if NA_WINDOWS:
        proces = subprocess.Popen(polecenie, cwd=katalog_aplikacji, env=srodowisko, stdout=wyjscie,
                                  stderr=subprocess.STDOUT, creationflags=subprocess.CREATE_NEW_PROCESS_GROUP)
    else:
        proces = subprocess.Popen(polecenie, cwd=katalog_aplikacji, env=srodowisko, stdout=wyjscie,
                                  stderr=subprocess.STDOUT, start_new_session=True)
    proces.plik_logu = wyjscie
    return proces


def zatrzymaj(proces):
    """Zatrzymuje backend razem z drzewem procesów potomnych."""
    if proces is None:
        return
    if proces.poll() is None:
        if NA_WINDOWS:
            subprocess.run(["taskkill", "/PID", str(proces.pid), "/T", "/F"],
                           stdout=subprocess.DEVNULL, stderr=subprocess.DEVNULL)
        else:
            try:
                os.killpg(os.getpgid(proces.pid), signal.SIGTERM)
            except (ProcessLookupError, PermissionError):
                pass
        try:
            proces.wait(timeout=20)
        except subprocess.TimeoutExpired:
            if not NA_WINDOWS:
                try:
                    os.killpg(os.getpgid(proces.pid), signal.SIGKILL)
                except (ProcessLookupError, PermissionError):
                    pass
            proces.kill()
    proces.plik_logu.close()


def czekaj_na_ping(proces, port):
    koniec = time.time() + CZAS_STARTU_S
    while time.time() < koniec:
        if proces.poll() is not None:
            return False
        try:
            with urllib.request.urlopen("http://127.0.0.1:%d/api/ping" % port, timeout=2) as odpowiedz:
                if odpowiedz.status == 200:
                    return True
        except (urllib.error.URLError, OSError):
            pass
        time.sleep(1)
    return False


def wyslij(port, zapytanie):
    """Zwraca (status, treść tekstowa)."""
    url = "http://127.0.0.1:%d%s" % (port, zapytanie["sciezka"])
    parametry = zapytanie.get("parametry")
    if parametry:
        url += "?" + urllib.parse.urlencode(parametry)
    dane = None
    naglowki = {"Accept": "application/json"}
    if "tresc" in zapytanie:
        dane = json.dumps(zapytanie["tresc"]).encode("utf-8")
    elif "tresc_surowa" in zapytanie:
        dane = zapytanie["tresc_surowa"].encode("utf-8")
    if dane is not None:
        naglowki["Content-Type"] = "application/json"
    zadanie = urllib.request.Request(url, data=dane, method=zapytanie.get("metoda", "GET"), headers=naglowki)
    try:
        with urllib.request.urlopen(zadanie, timeout=30) as odpowiedz:
            return odpowiedz.status, odpowiedz.read().decode("utf-8", errors="replace")
    except urllib.error.HTTPError as blad:
        return blad.code, blad.read().decode("utf-8", errors="replace")


def pliki_procesu(katalog_backendu, wzorce):
    """Nazwy plików w src backendu (bez bin/ i obj/) zawierające któryś z fragmentów (bez rozróżniania wielkości liter)."""
    fragmenty = [wzorzec.lower() for wzorzec in wzorce]
    znalezione = []
    for korzen, katalogi, pliki in os.walk(os.path.join(katalog_backendu, "src")):
        katalogi[:] = sorted(k for k in katalogi if k.lower() not in POMIJANE_KATALOGI)
        for nazwa in sorted(pliki):
            if any(fragment in nazwa.lower() for fragment in fragmenty):
                znalezione.append(nazwa)
    return znalezione


def jako_liczba(wartosc):
    if isinstance(wartosc, bool) or wartosc is None:
        return None
    try:
        return Decimal(str(wartosc))
    except (InvalidOperation, ValueError):
        return None


def pokaz(wartosc):
    """Wartość do komunikatu: liczby z przecinkiem dziesiętnym jak w legacy."""
    if isinstance(wartosc, (Decimal, float)) or (isinstance(wartosc, str) and jako_liczba(wartosc) is not None
                                                   and "." in wartosc):
        return str(wartosc).replace(".", ",")
    if wartosc is None:
        return "brak"
    return str(wartosc)


def zgodne(oczekiwana, faktyczna):
    if isinstance(oczekiwana, (int, Decimal)) or (isinstance(oczekiwana, str) and jako_liczba(oczekiwana) is not None):
        liczba_oczekiwana, liczba_faktyczna = jako_liczba(oczekiwana), jako_liczba(faktyczna)
        if liczba_faktyczna is None:
            return False
        return liczba_oczekiwana == liczba_faktyczna
    return oczekiwana == faktyczna


def sprawdz_przypadek(port, przypadek):
    """Zwraca listę rozbieżności (teksty) dla jednego przypadku."""
    oczekiwane = przypadek["oczekiwane"]
    status, tresc = wyslij(port, przypadek["zapytanie"])
    rozbieznosci = []
    if status != oczekiwane["status"]:
        rozbieznosci.append("status HTTP: oczekiwano %d, jest %d" % (oczekiwane["status"], status))
        return rozbieznosci
    try:
        odpowiedz = json.loads(tresc, parse_float=Decimal) if tresc.strip() else None
    except ValueError:
        odpowiedz = None
    pola = oczekiwane.get("pola", {})
    obecne = oczekiwane.get("pola_obecne", [])
    if (pola or obecne) and not isinstance(odpowiedz, dict):
        return ["odpowiedź nie jest obiektem JSON"]
    for nazwa in obecne:
        if nazwa not in odpowiedz:
            rozbieznosci.append("[%s]: brak pola w odpowiedzi" % nazwa)
    for nazwa, oczekiwana in pola.items():
        if nazwa not in odpowiedz:
            rozbieznosci.append("[%s]: brak pola w odpowiedzi" % nazwa)
        elif not zgodne(oczekiwana, odpowiedz[nazwa]):
            rozbieznosci.append("[%s]: oczekiwano %s, jest %s" % (nazwa, pokaz(oczekiwana), pokaz(odpowiedz[nazwa])))
    return rozbieznosci


def main(argv):
    try:
        sys.stdout.reconfigure(errors="replace")
    except AttributeError:
        pass
    backend = BACKEND_DOMYSLNY
    port = int(os.environ.get("BRAMKA_PORT", PORT_DOMYSLNY))
    if "--backend" in argv:
        indeks = argv.index("--backend")
        backend = argv[indeks + 1] if indeks + 1 < len(argv) else ""
        argv = argv[:indeks] + argv[indeks + 2:]
    if "--port" in argv:
        indeks = argv.index("--port")
        port = int(argv[indeks + 1])
        argv = argv[:indeks] + argv[indeks + 2:]
    if len(argv) != 1 or not backend:
        print("użycie: python tools/bramka/akceptacja.py <korzeń_repo> [--backend <ścieżka>] [--port 8089]",
              file=sys.stderr)
        return 2
    katalog_backendu = os.path.abspath(os.path.join(argv[0], backend))
    if not os.path.isdir(katalog_backendu):
        print("brak katalogu %s" % katalog_backendu, file=sys.stderr)
        return 2

    pliki_przypadkow = sorted(glob.glob(os.path.join(KATALOG_BRAMKI, "przypadki-*.json")))
    procesy = []
    for sciezka in pliki_przypadkow:
        with open(sciezka, encoding="utf-8") as plik:
            procesy.append(json.load(plik))
    pliki_danych = [os.path.join(KATALOG_BRAMKI, p["dane_sql"]) for p in procesy if p.get("dane_sql")]

    katalog_logow = tempfile.mkdtemp(prefix="bramka-")
    log_budowy = os.path.join(katalog_logow, "publish.log")
    katalog_aplikacji = os.path.join(katalog_logow, "app")
    log_backendu = os.path.join(katalog_logow, "backend.log")

    dll = zbuduj_backend(katalog_backendu, katalog_aplikacji, log_budowy)
    if dll is None:
        wypisz("nie: backend się nie zbudował (dotnet publish), więc przypadków nie da się sprawdzić; koniec logu:")
        wypisz(ogon_pliku(log_budowy))
        return 1
    if port_zajety(port):
        wypisz("nie: port %d jest zajęty, backend bramki nie może wystartować (zmienna BRAMKA_PORT zmienia port)"
               % port)
        return 1

    dane_zaladowane = True
    proces = None
    wynik = 0
    try:
        proces = uruchom_backend(dll, port, pliki_danych, log_backendu)
        if not czekaj_na_ping(proces, port):
            zatrzymaj(proces)
            wypisz("uwaga: backend nie wstał z danymi przypadków; ponowny start bez nich")
            dane_zaladowane = False
            proces = uruchom_backend(dll, port, [], log_backendu)
            if not czekaj_na_ping(proces, port):
                wypisz("nie: backend nie odpowiedział na /api/ping w %d s; koniec logu:" % CZAS_STARTU_S)
                wypisz(ogon_pliku(log_backendu))
                return 1

        for opis_procesu in procesy:
            nazwa = opis_procesu["proces"]
            sonda = opis_procesu["sonda"]
            status_sondy, _ = wyslij(port, sonda["zapytanie"])
            if status_sondy in (404, 405):
                znalezione = pliki_procesu(katalog_backendu, opis_procesu.get("wykrycie", []))
                if not znalezione:
                    wypisz("%s: proces nie przeniesiony (%s zwraca %d): pominięto"
                           % (nazwa, sonda["opis"], status_sondy))
                    continue
                wypisz("nie: %s: w kodzie jest implementacja (%s), ale aplikacja nie odpowiada na %s; kontrakt: %s"
                       % (nazwa, ", ".join(znalezione[:3]), sonda["opis"], opis_procesu.get("kontrakt", "-")))
                wynik = 1
                continue
            if opis_procesu.get("dane_sql") and not dane_zaladowane:
                wypisz("nie: %s: dane przypadków %s nie załadowały się (zmieniony schemat tabel?), "
                       "więc wyników nie da się porównać z legacy" % (nazwa, opis_procesu["dane_sql"]))
                wynik = 1
                continue
            przypadki = opis_procesu["przypadki"]
            rozbieznych = 0
            for przypadek in przypadki:
                rozbieznosci = sprawdz_przypadek(port, przypadek)
                if rozbieznosci:
                    rozbieznych += 1
                    for tekst in rozbieznosci:
                        separator = " " if tekst.startswith("[") else ": "
                        wypisz("nie: %s%s%s   (wejście: %s)" % (przypadek["id"], separator, tekst, przypadek["wejscie"]))
            wypisz("%s: przypadków %d, niezgodnych z legacy %d -> %s" % (
                nazwa, len(przypadki), rozbieznych, "CZERWONO" if rozbieznych else "zielono"))
            if rozbieznych:
                wynik = 1
    finally:
        zatrzymaj(proces)
    return wynik


if __name__ == "__main__":
    sys.exit(main(sys.argv[1:]))
