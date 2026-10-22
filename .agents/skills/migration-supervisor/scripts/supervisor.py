#!/usr/bin/env python3
"""migration-supervisor: strażnik postępu i stanu migracji procesów P1-P7 (Python 3, tylko biblioteka standardowa).

Stan:    new-app-parent/new-docs/migracja/stan-migracji.json
Raport:  new-app-parent/new-docs/NEW-PRZEPLYWY-BIZNESOWE.md (generowany)
Rejestr: .agents/skills/migration-supervisor/procesy.json

Komendy (z korzenia repo):
  info [--json]                      postęp: % zmigrowanych z 7, częściowe osobno, co zostało
  status [P<n>] [--json]             szczegóły procesu albo wszystkich
  rozpocznij P<n> [--agent A] [--galaz G] [--uwaga U] [--dokonczenie]
                                     GUARD: zgoda na migrację (kod 3 = odmowa)
  zakoncz P<n> --status zmigrowany|czesciowy --dokument D [--braki "a; b"] [--uwaga U]
  odnotuj P<n> --opis O [--braki "a; b"]
                                     zmiana w procesie już (częściowo) zmigrowanym lub w toku;
                                     --braki zastępuje listę braków procesu częściowego
  porzuc P<n> [--uwaga U]            w-toku -> poprzedni status
  klasyfikuj [--staged] [--json]     zmienione pliki -> P<n> / wspolny / poza-procesem
  weryfikuj [--json]                 spójność stanu z kodem new-app
  raport                             regeneracja NEW-PRZEPLYWY-BIZNESOWE.md
  hook-pre-commit                    sprawdzenie indeksu przed commitem (wywołuje .githooks/pre-commit)
  zainstaluj-hooki                   lokalnie core.hooksPath=.githooks (hooki przekazują do globalnych)
Opcja globalna: --repo <ścieżka> (domyślnie: git rev-parse --show-toplevel).

Kody wyjścia: 0 OK, 1 błąd weryfikacji / blokada hooka, 2 złe argumenty lub brak plików, 3 odmowa guarda.
"""

import argparse
import datetime
import json
import os
import re
import subprocess
import sys
import tempfile

KATALOG_SKILLA = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
REJESTR = os.path.join(KATALOG_SKILLA, "procesy.json")
STAN = "new-app-parent/new-docs/migracja/stan-migracji.json"
RAPORT = "new-app-parent/new-docs/NEW-PRZEPLYWY-BIZNESOWE.md"
SKRYPT = ".agents/skills/migration-supervisor/scripts/supervisor.py"

OK, BLAD, ZLE_ARGUMENTY, ODMOWA = 0, 1, 2, 3
STATUSY = ("nierozpoczety", "w-toku", "czesciowy", "zmigrowany")
NAZWY_STATUSOW = {
    "nierozpoczety": "nierozpoczęty",
    "w-toku": "w toku",
    "czesciowy": "częściowo",
    "zmigrowany": "zmigrowany",
}
ZASLEPKA = re.compile(r"export\s+const\s+migrated\s*=\s*false")


class Przerwij(Exception):
    def __init__(self, kod, komunikat):
        super().__init__(komunikat)
        self.kod = kod
        self.komunikat = komunikat


def ustaw_utf8():
    for strumien in (sys.stdout, sys.stderr):
        try:
            strumien.reconfigure(encoding="utf-8", errors="replace")
        except (AttributeError, ValueError):
            pass


def git(repo, *argumenty, sprawdz=True):
    wynik = subprocess.run(["git", *argumenty], cwd=repo, stdout=subprocess.PIPE, stderr=subprocess.PIPE)
    if sprawdz and wynik.returncode != 0:
        raise Przerwij(ZLE_ARGUMENTY, "git %s: %s" % (" ".join(argumenty), wynik.stderr.decode("utf-8", "replace").strip()))
    return wynik


def korzen_repo(podany):
    if podany:
        return os.path.abspath(podany)
    wynik = subprocess.run(["git", "rev-parse", "--show-toplevel"], stdout=subprocess.PIPE, stderr=subprocess.PIPE)
    if wynik.returncode != 0:
        raise Przerwij(ZLE_ARGUMENTY, "nie jestem w repozytorium git (użyj --repo)")
    return wynik.stdout.decode("utf-8").strip()


def biezaca_galaz(repo):
    wynik = git(repo, "rev-parse", "--abbrev-ref", "HEAD", sprawdz=False)
    if wynik.returncode != 0:
        wynik = git(repo, "symbolic-ref", "--short", "HEAD", sprawdz=False)
    return wynik.stdout.decode("utf-8").strip() or "nieznana"


def teraz():
    return datetime.datetime.now().astimezone().isoformat(timespec="seconds")


def wczytaj_rejestr():
    with open(REJESTR, encoding="utf-8") as plik:
        return json.load(plik)


def wczytaj_stan(repo):
    sciezka = os.path.join(repo, STAN)
    if not os.path.isfile(sciezka):
        raise Przerwij(ZLE_ARGUMENTY, "brak pliku stanu %s" % STAN)
    with open(sciezka, encoding="utf-8") as plik:
        return json.load(plik)


def zapisz_tekst(sciezka, tekst):
    katalog = os.path.dirname(sciezka)
    os.makedirs(katalog, exist_ok=True)
    deskryptor, tymczasowy = tempfile.mkstemp(dir=katalog, prefix=".tmp-", suffix=".part")
    try:
        with os.fdopen(deskryptor, "w", encoding="utf-8", newline="\n") as plik:
            plik.write(tekst)
        os.replace(tymczasowy, sciezka)
    except BaseException:
        if os.path.exists(tymczasowy):
            os.remove(tymczasowy)
        raise


def zapisz_stan(repo, stan, rejestr):
    zapisz_tekst(os.path.join(repo, STAN), json.dumps(stan, ensure_ascii=False, indent=2) + "\n")
    zapisz_tekst(os.path.join(repo, RAPORT), zbuduj_raport(rejestr, stan))


def proces_z_rejestru(rejestr, identyfikator):
    for proces in rejestr["procesy"]:
        if proces["id"] == identyfikator.upper():
            return proces
    raise Przerwij(ZLE_ARGUMENTY, "nieznany proces %s (dozwolone: %s)"
                   % (identyfikator, ", ".join(p["id"] for p in rejestr["procesy"])))


def stan_procesu(stan, identyfikator):
    procesy = stan.setdefault("procesy", {})
    return procesy.setdefault(identyfikator, {
        "status": "nierozpoczety", "poprzedni_status": None, "galaz": None, "agent": None,
        "dokument": None, "braki": [], "historia": [],
    })


def dopisz_historie(wpis_procesu, akcja, opis, galaz, agent):
    wpis_procesu.setdefault("historia", []).append(
        {"kiedy": teraz(), "akcja": akcja, "opis": opis, "galaz": galaz, "agent": agent})


def procent_tekst(wartosc):
    return ("%.1f" % wartosc).replace(".", ",")


# --- postęp ---

def policz(rejestr, stan):
    identyfikatory = [p["id"] for p in rejestr["procesy"]]
    nazwy = {p["id"]: p["nazwa"] for p in rejestr["procesy"]}
    wg_statusu = {s: [] for s in STATUSY}
    for identyfikator in identyfikatory:
        wg_statusu[stan_procesu(stan, identyfikator)["status"]].append(identyfikator)
    zmigrowane = wg_statusu["zmigrowany"]
    procent = round(100.0 * len(zmigrowane) / len(identyfikatory), 1) if identyfikatory else 0.0
    return {
        "procesow": len(identyfikatory),
        "zmigrowane": zmigrowane,
        "procent": procent,
        "czesciowe": [{"id": i, "nazwa": nazwy[i], "braki": stan_procesu(stan, i).get("braki", [])}
                      for i in wg_statusu["czesciowy"]],
        "w_toku": [{"id": i, "nazwa": nazwy[i], "galaz": stan_procesu(stan, i).get("galaz"),
                    "agent": stan_procesu(stan, i).get("agent")} for i in wg_statusu["w-toku"]],
        "nierozpoczete": wg_statusu["nierozpoczety"],
        "pozostale": [i for i in identyfikatory if i not in zmigrowane],
        "nazwy": nazwy,
    }


def pasek(procent, szerokosc=20):
    pelne = int(round(procent / 100.0 * szerokosc))
    return "[" + "#" * pelne + "." * (szerokosc - pelne) + "]"


def tekst_info(licznik):
    nazwy = licznik["nazwy"]
    linie = [
        "Postęp migracji procesów biznesowych (legacy -> new-app)",
        "%s %s%% zmigrowanych: %d/%d" % (pasek(licznik["procent"]), procent_tekst(licznik["procent"]),
                                         len(licznik["zmigrowane"]), licznik["procesow"]),
        "Zmigrowane: " + (", ".join("%s %s" % (i, nazwy[i]) for i in licznik["zmigrowane"]) or "brak"),
    ]
    if licznik["czesciowe"]:
        linie.append("Częściowo (nie wliczane do %%): %d" % len(licznik["czesciowe"]))
        for proces in licznik["czesciowe"]:
            linie.append("  - %s %s; braki: %s" % (proces["id"], proces["nazwa"], "; ".join(proces["braki"]) or "nie opisano"))
    else:
        linie.append("Częściowo: brak")
    if licznik["w_toku"]:
        linie.append("W toku: " + ", ".join("%s %s (gałąź %s, agent %s)" % (p["id"], p["nazwa"], p["galaz"], p["agent"])
                                            for p in licznik["w_toku"]))
    else:
        linie.append("W toku: brak")
    pozostalo = len(licznik["pozostale"])
    brakuje = round(100.0 - licznik["procent"], 1)
    linie.append("Pozostało do pełnej migracji: %d/%d (%s%%): %s" % (
        pozostalo, licznik["procesow"], procent_tekst(brakuje),
        ", ".join("%s %s" % (i, nazwy[i]) for i in licznik["pozostale"]) or "nic"))
    linie.append("Nierozpoczęte w legacy: " + (", ".join("%s %s" % (i, nazwy[i]) for i in licznik["nierozpoczete"]) or "brak"))
    return "\n".join(linie)


# --- raport Markdown ---

def wzgledna(od_pliku, do_sciezki):
    katalog = os.path.dirname(od_pliku)
    kotwica = ""
    if "#" in do_sciezki:
        do_sciezki, kotwica = do_sciezki.split("#", 1)
        kotwica = "#" + kotwica
    sciezka = os.path.relpath(do_sciezki, katalog).replace("\\", "/")
    if not sciezka.startswith("."):
        sciezka = "./" + sciezka
    return sciezka + kotwica


def zbuduj_raport(rejestr, stan):
    licznik = policz(rejestr, stan)
    nazwy = licznik["nazwy"]
    linie = [
        "# Przepływy biznesowe w nowej aplikacji — stan migracji",
        "",
        "Plik generowany przez skill `migration-supervisor` (`python %s raport`); nie edytuj ręcznie. "
        "Źródło: [stan-migracji.json](%s), rejestr procesów: [procesy.json](%s), reguły legacy: [08](%s)."
        % (SKRYPT, wzgledna(RAPORT, STAN), wzgledna(RAPORT, ".agents/skills/migration-supervisor/procesy.json"),
           wzgledna(RAPORT, rejestr["analiza"])),
        "",
        "## Postęp",
        "",
        "- zmigrowane: %d/%d = %s%%" % (len(licznik["zmigrowane"]), licznik["procesow"], procent_tekst(licznik["procent"])),
        "- częściowo (poza %%): %s" % (", ".join("%s %s" % (p["id"], p["nazwa"]) for p in licznik["czesciowe"]) or "brak"),
        "- w toku: %s" % (", ".join("%s %s (gałąź `%s`)" % (p["id"], p["nazwa"], p["galaz"]) for p in licznik["w_toku"]) or "brak"),
        "- pozostało: %s" % (", ".join("%s %s" % (i, nazwy[i]) for i in licznik["pozostale"]) or "nic"),
        "",
        "## Procesy",
        "",
        "| Proces | Status | Dokument pochodzenia | Wejście legacy | Nowa aplikacja | Ostatnia zmiana |",
        "|---|---|---|---|---|---|",
    ]
    for proces in rejestr["procesy"]:
        wpis = stan_procesu(stan, proces["id"])
        dokument = wpis.get("dokument")
        dokument_tekst = "[%s](%s)" % (os.path.basename(dokument), wzgledna(RAPORT, dokument)) if dokument else "—"
        legacy = "<br>".join("%s `%s`" % (z["opis"], z["dowod"]) for z in proces["legacy"])
        nowa = []
        if proces.get("endpointy") and wpis["status"] != "nierozpoczety":
            nowa.extend("`%s`" % e for e in proces["endpointy"])
        if proces.get("strona") and wpis["status"] != "nierozpoczety":
            nowa.append("`%s`" % os.path.basename(proces["strona"]))
        historia = wpis.get("historia") or []
        ostatnia = "%s %s" % (historia[-1]["kiedy"][:10], historia[-1]["akcja"]) if historia else "—"
        linie.append("| [%s — %s](%s) | %s | %s | %s | %s | %s |" % (
            proces["id"], proces["nazwa"], wzgledna(RAPORT, proces["sekcja08"]), NAZWY_STATUSOW[wpis["status"]],
            dokument_tekst, legacy, "<br>".join(nowa) or "—", ostatnia))
    if licznik["czesciowe"]:
        linie += ["", "## Braki procesów częściowych", ""]
        for proces in licznik["czesciowe"]:
            for brak in proces["braki"] or ["nie opisano"]:
                linie.append("- %s %s: %s" % (proces["id"], proces["nazwa"], brak))
    return "\n".join(linie) + "\n"


# --- klasyfikacja ---

def klasyfikuj_plik(rejestr, sciezka):
    sciezka = sciezka.replace("\\", "/")
    if any(re.search(w, sciezka) for w in rejestr["pomijane"]):
        return "poza-procesem", []
    dopasowanie = re.match(r"^new-app-parent/new-docs/migracja/(P\d+)-[^/]+\.md$", sciezka)
    if dopasowanie:
        return dopasowanie.group(1), []
    if sciezka in rejestr["pliki_meta"]:
        return "poza-procesem", []
    if not any(re.search(w, sciezka) for w in rejestr["zakres_procesowy"]):
        return "poza-procesem", []
    if any(re.search(w, sciezka) for w in rejestr["wspolne"]):
        return "wspolny", []
    nazwa = sciezka.rsplit("/", 1)[-1]
    kandydaci = [p["id"] for p in rejestr["procesy"]
                 if any(re.search(w, nazwa, re.IGNORECASE) for w in p["wzorce_nazw"])]
    if len(kandydaci) == 1:
        return kandydaci[0], []
    return "wspolny", kandydaci


def rozdziel_z(dane):
    return [s for s in dane.decode("utf-8").split("\0") if s]


def zmienione_pliki(repo, tylko_indeks):
    if tylko_indeks:
        return sorted(set(rozdziel_z(git(repo, "diff", "--cached", "--name-only", "-z", "--no-renames").stdout)))
    ma_head = git(repo, "rev-parse", "--verify", "-q", "HEAD", sprawdz=False).returncode == 0
    if ma_head:
        sledzone = rozdziel_z(git(repo, "diff", "HEAD", "--name-only", "-z", "--no-renames").stdout)
    else:
        sledzone = rozdziel_z(git(repo, "ls-files", "--cached", "-z").stdout)
    nowe = rozdziel_z(git(repo, "ls-files", "--others", "--exclude-standard", "-z").stdout)
    return sorted(set(sledzone) | set(nowe))


def klasyfikuj(rejestr, pliki):
    wynik = {"procesy": {}, "wspolny": [], "poza-procesem": []}
    for sciezka in pliki:
        klasa, kandydaci = klasyfikuj_plik(rejestr, sciezka)
        if klasa == "wspolny":
            wynik["wspolny"].append({"plik": sciezka, "kandydaci": kandydaci})
        elif klasa == "poza-procesem":
            wynik["poza-procesem"].append(sciezka)
        else:
            wynik["procesy"].setdefault(klasa, []).append(sciezka)
    return wynik


# --- weryfikacja ---

def pliki_drzewa(repo):
    wynik = []
    korzen = os.path.join(repo, "new-app-parent", "new-app")
    for katalog, podkatalogi, pliki in os.walk(korzen):
        podkatalogi[:] = [k for k in podkatalogi if k not in ("bin", "obj", "node_modules", "dist", ".vite")]
        for nazwa in pliki:
            wynik.append(os.path.relpath(os.path.join(katalog, nazwa), repo).replace("\\", "/"))
    return wynik


def stan_strony(repo, strona):
    if not strona:
        return "brak"
    sciezka = os.path.join(repo, strona)
    if not os.path.isfile(sciezka):
        return "brak"
    with open(sciezka, encoding="utf-8", errors="replace") as plik:
        return "zaslepka" if ZASLEPKA.search(plik.read()) else "gotowa"


def weryfikuj(repo, rejestr, stan):
    bledy, ostrzezenia, informacje = [], [], []
    pliki = pliki_drzewa(repo)
    for proces in rejestr["procesy"]:
        identyfikator = proces["id"]
        wpis = stan_procesu(stan, identyfikator)
        status = wpis["status"]
        if status not in STATUSY:
            bledy.append("%s: nieznany status %r" % (identyfikator, status))
            continue
        sygnaly = [p for p in pliki if any(re.search(w, p) for w in proces["sygnaly"])]
        strona = stan_strony(repo, proces.get("strona"))
        kod_obecny = bool(sygnaly) or strona == "gotowa"
        dokument = wpis.get("dokument")
        dokument_istnieje = bool(dokument) and os.path.isfile(os.path.join(repo, dokument))
        if status == "nierozpoczety" and kod_obecny:
            bledy.append("%s: w new-app jest kod procesu (%s), a status to 'nierozpoczety' -> uruchom "
                         "'rozpocznij %s' (i po finalizacji 'zakoncz')"
                         % (identyfikator, ", ".join(sygnaly) or proces.get("strona"), identyfikator))
        if status == "zmigrowany":
            if not dokument_istnieje:
                bledy.append("%s: status 'zmigrowany' bez istniejącego dokumentu pochodzenia (%s)" % (identyfikator, dokument))
            if strona == "zaslepka":
                bledy.append("%s: status 'zmigrowany', a strona %s jest zaślepką (migrated = false)" % (identyfikator, proces["strona"]))
            if not kod_obecny:
                bledy.append("%s: status 'zmigrowany', ale nie znaleziono kodu procesu w new-app" % identyfikator)
        if status == "czesciowy":
            if dokument and not dokument_istnieje:
                bledy.append("%s: brak dokumentu pochodzenia %s" % (identyfikator, dokument))
            if not dokument:
                ostrzezenia.append("%s: status 'czesciowy' bez dokumentu pochodzenia (stan sprzed workflow)" % identyfikator)
        if status == "w-toku":
            informacje.append("%s: w toku na gałęzi %s (agent %s)" % (identyfikator, wpis.get("galaz"), wpis.get("agent")))
    sciezka_raportu = os.path.join(repo, RAPORT)
    oczekiwany = zbuduj_raport(rejestr, stan)
    if not os.path.isfile(sciezka_raportu):
        ostrzezenia.append("brak raportu %s -> uruchom 'raport'" % RAPORT)
    else:
        with open(sciezka_raportu, encoding="utf-8") as plik:
            if plik.read() != oczekiwany:
                ostrzezenia.append("raport %s jest nieaktualny -> uruchom 'raport'" % RAPORT)
    return {"bledy": bledy, "ostrzezenia": ostrzezenia, "informacje": informacje}


# --- hook pre-commit ---

def plik_z_indeksu(repo, sciezka):
    wynik = git(repo, "show", ":" + sciezka, sprawdz=False)
    return wynik.stdout.decode("utf-8") if wynik.returncode == 0 else None


def plik_z_head(repo, sciezka):
    wynik = git(repo, "show", "HEAD:" + sciezka, sprawdz=False)
    return wynik.stdout.decode("utf-8") if wynik.returncode == 0 else None


def hook_pre_commit(repo, rejestr):
    pliki = zmienione_pliki(repo, tylko_indeks=True)
    podzial = klasyfikuj(rejestr, pliki)
    procesy = podzial["procesy"]
    if not procesy:
        if podzial["wspolny"]:
            print("migration-supervisor: ostrzeżenie: commit zawiera tylko pliki wspólne (%s); "
                  "sprawdź, czy nie należą do procesu." % ", ".join(w["plik"] for w in podzial["wspolny"]))
        return OK
    bledy = []
    tekst_stanu = plik_z_indeksu(repo, STAN)
    if tekst_stanu is None:
        bledy.append("w indeksie brak %s, a commit zawiera pliki procesów %s" % (STAN, ", ".join(sorted(procesy))))
    else:
        try:
            stan_indeksu = json.loads(tekst_stanu)
        except ValueError as blad:
            stan_indeksu = None
            bledy.append("%s w indeksie nie jest poprawnym JSON: %s" % (STAN, blad))
        tekst_head = plik_z_head(repo, STAN)
        stan_head = json.loads(tekst_head) if tekst_head else {"procesy": {}}
        for identyfikator in sorted(procesy) if stan_indeksu is not None else []:
            wpis = stan_indeksu.get("procesy", {}).get(identyfikator)
            if wpis is None:
                bledy.append("%s: brak procesu w stanie migracji" % identyfikator)
                continue
            historia_head = stan_head.get("procesy", {}).get(identyfikator, {}).get("historia", [])
            nowe = [w for w in wpis.get("historia", []) if w not in historia_head]
            akcje = {w.get("akcja") for w in nowe}
            status = wpis.get("status")
            pliki_procesu = ", ".join(procesy[identyfikator][:3]) + (" ..." if len(procesy[identyfikator]) > 3 else "")
            if not nowe:
                bledy.append("%s: commit zmienia pliki procesu (%s), ale stan migracji tego procesu nie został "
                             "zaktualizowany -> 'zakoncz %s ...' albo 'odnotuj %s --opis ...', potem git add %s"
                             % (identyfikator, pliki_procesu, identyfikator, identyfikator, STAN))
                continue
            if status == "nierozpoczety":
                bledy.append("%s: status 'nierozpoczety' -> najpierw 'rozpocznij %s' (zgoda supervisora)" % (identyfikator, identyfikator))
            elif status == "w-toku" and "odnotuj" not in akcje:
                bledy.append("%s: proces nadal 'w-toku' -> 'zakoncz %s --status zmigrowany|czesciowy --dokument ...' "
                             "(albo 'odnotuj' dla commitu częściowej pracy)" % (identyfikator, identyfikator))
            dokument = wpis.get("dokument")
            wymaga_dokumentu = status == "zmigrowany" or "zakoncz" in akcje
            if wymaga_dokumentu and not dokument:
                bledy.append("%s: brak dokumentu pochodzenia w stanie migracji" % identyfikator)
            elif dokument and plik_z_indeksu(repo, dokument) is None:
                bledy.append("%s: dokument pochodzenia %s nie jest w indeksie (git add)" % (identyfikator, dokument))
    if bledy:
        print("migration-supervisor: COMMIT ZABLOKOWANY (hook pre-commit)", file=sys.stderr)
        for blad in bledy:
            print("  - " + blad, file=sys.stderr)
        print("Komendy: python %s <komenda>. Nie używaj 'git commit --no-verify'." % SKRYPT, file=sys.stderr)
        return BLAD
    print("migration-supervisor: stan migracji zgodny z commitem (%s)" % ", ".join(sorted(procesy)))
    return OK


# --- komendy ---

def k_info(repo, rejestr, argumenty):
    licznik = policz(rejestr, wczytaj_stan(repo))
    if argumenty.json:
        dane = dict(licznik)
        dane.pop("nazwy")
        print(json.dumps(dane, ensure_ascii=False, indent=2))
    else:
        print(tekst_info(licznik))
    return OK


def k_status(repo, rejestr, argumenty):
    stan = wczytaj_stan(repo)
    identyfikatory = [proces_z_rejestru(rejestr, argumenty.proces)["id"]] if argumenty.proces else [p["id"] for p in rejestr["procesy"]]
    dane = {i: stan_procesu(stan, i) for i in identyfikatory}
    if argumenty.json:
        print(json.dumps(dane, ensure_ascii=False, indent=2))
        return OK
    for identyfikator in identyfikatory:
        proces = proces_z_rejestru(rejestr, identyfikator)
        wpis = dane[identyfikator]
        print("%s %s: %s" % (identyfikator, proces["nazwa"], NAZWY_STATUSOW[wpis["status"]]))
        if wpis.get("galaz"):
            print("  gałąź: %s, agent: %s" % (wpis["galaz"], wpis.get("agent")))
        if wpis.get("dokument"):
            print("  dokument: %s" % wpis["dokument"])
        for brak in wpis.get("braki") or []:
            print("  brak: %s" % brak)
        for zdarzenie in (wpis.get("historia") or [])[-3:]:
            print("  %s %s: %s" % (zdarzenie["kiedy"], zdarzenie["akcja"], zdarzenie["opis"]))
    return OK


def k_rozpocznij(repo, rejestr, argumenty):
    proces = proces_z_rejestru(rejestr, argumenty.proces)
    stan = wczytaj_stan(repo)
    wpis = stan_procesu(stan, proces["id"])
    galaz = argumenty.galaz or biezaca_galaz(repo)
    status = wpis["status"]
    if status == "zmigrowany":
        raise Przerwij(ODMOWA, "ODMOWA: %s %s jest już zmigrowany; zmiany w procesie zgłaszaj przez 'odnotuj %s --opis ...'"
                       % (proces["id"], proces["nazwa"], proces["id"]))
    if status == "w-toku":
        if wpis.get("galaz") != galaz:
            raise Przerwij(ODMOWA, "ODMOWA: %s jest w toku na innej gałęzi (%s, agent %s); zakończ albo porzuć tamtą pracę"
                           % (proces["id"], wpis.get("galaz"), wpis.get("agent")))
        print("%s już w toku na gałęzi %s: zgoda podtrzymana" % (proces["id"], galaz))
        return OK
    if status == "czesciowy" and not argumenty.dokonczenie:
        raise Przerwij(ODMOWA, "ODMOWA: %s jest częściowo zmigrowany (braki: %s); dokończenie wymaga --dokonczenie"
                       % (proces["id"], "; ".join(wpis.get("braki") or []) or "nie opisano"))
    wpis["poprzedni_status"] = status
    wpis["status"] = "w-toku"
    wpis["galaz"] = galaz
    wpis["agent"] = argumenty.agent
    opis = "zgoda na migrację" + (" (dokończenie)" if status == "czesciowy" else "")
    if argumenty.uwaga:
        opis += "; " + argumenty.uwaga
    dopisz_historie(wpis, "rozpocznij", opis, galaz, argumenty.agent)
    zapisz_stan(repo, stan, rejestr)
    print("ZGODA: %s %s -> w toku (gałąź %s, agent %s)" % (proces["id"], proces["nazwa"], galaz, argumenty.agent))
    return OK


def sprawdz_galaz(repo, proces, wpis):
    """Proces w toku może zakończyć, porzucić albo odnotować tylko gałąź, która dostała zgodę."""
    biezaca = biezaca_galaz(repo)
    if wpis["status"] == "w-toku" and wpis.get("galaz") and wpis["galaz"] != biezaca:
        raise Przerwij(ODMOWA, "ODMOWA: %s jest w toku na gałęzi %s, a bieżąca gałąź to %s"
                       % (proces["id"], wpis["galaz"], biezaca))


def k_zakoncz(repo, rejestr, argumenty):
    proces = proces_z_rejestru(rejestr, argumenty.proces)
    stan = wczytaj_stan(repo)
    wpis = stan_procesu(stan, proces["id"])
    if wpis["status"] != "w-toku":
        raise Przerwij(ODMOWA, "ODMOWA: %s ma status '%s', nie 'w-toku'; najpierw 'rozpocznij %s' (zgoda supervisora)"
                       % (proces["id"], wpis["status"], proces["id"]))
    sprawdz_galaz(repo, proces, wpis)
    dokument = argumenty.dokument.replace("\\", "/")
    if not os.path.isfile(os.path.join(repo, dokument)):
        raise Przerwij(ODMOWA, "ODMOWA: dokument pochodzenia %s nie istnieje" % dokument)
    braki = [b.strip() for b in (argumenty.braki or "").split(";") if b.strip()]
    if argumenty.status == "czesciowy" and not braki:
        raise Przerwij(ZLE_ARGUMENTY, "status 'czesciowy' wymaga --braki \"brak 1; brak 2\"")
    galaz = wpis.get("galaz") or biezaca_galaz(repo)
    agent = wpis.get("agent")
    wpis["status"] = argumenty.status
    wpis["poprzedni_status"] = None
    wpis["dokument"] = dokument
    wpis["braki"] = braki
    wpis["galaz"] = None
    wpis["agent"] = None
    opis = "status %s, dokument %s" % (argumenty.status, dokument)
    if argumenty.uwaga:
        opis += "; " + argumenty.uwaga
    dopisz_historie(wpis, "zakoncz", opis, galaz, agent)
    zapisz_stan(repo, stan, rejestr)
    print("%s %s -> %s" % (proces["id"], proces["nazwa"], NAZWY_STATUSOW[argumenty.status]))
    print(tekst_info(policz(rejestr, stan)))
    return OK


def k_odnotuj(repo, rejestr, argumenty):
    proces = proces_z_rejestru(rejestr, argumenty.proces)
    stan = wczytaj_stan(repo)
    wpis = stan_procesu(stan, proces["id"])
    if wpis["status"] == "nierozpoczety":
        raise Przerwij(ODMOWA, "ODMOWA: %s jest nierozpoczęty; zmiany w nim wymagają 'rozpocznij %s'" % (proces["id"], proces["id"]))
    sprawdz_galaz(repo, proces, wpis)
    opis = argumenty.opis
    if argumenty.braki is not None:
        if wpis["status"] != "czesciowy":
            raise Przerwij(ZLE_ARGUMENTY, "--braki dotyczy tylko procesu 'czesciowy' (status %s)" % wpis["status"])
        braki = [b.strip() for b in argumenty.braki.split(";") if b.strip()]
        if not braki:
            raise Przerwij(ZLE_ARGUMENTY, "proces 'czesciowy' musi mieć co najmniej jeden brak; "
                                          "pełne dokończenie: 'rozpocznij --dokonczenie' i 'zakoncz'")
        usuniete = [b for b in wpis.get("braki", []) if b not in braki]
        wpis["braki"] = braki
        if usuniete:
            opis += "; usunięte braki: " + " | ".join(usuniete)
    galaz = wpis.get("galaz") or biezaca_galaz(repo)
    dopisz_historie(wpis, "odnotuj", opis, galaz, wpis.get("agent") or argumenty.agent)
    zapisz_stan(repo, stan, rejestr)
    print("odnotowano zmianę w %s %s" % (proces["id"], proces["nazwa"]))
    return OK


def k_porzuc(repo, rejestr, argumenty):
    proces = proces_z_rejestru(rejestr, argumenty.proces)
    stan = wczytaj_stan(repo)
    wpis = stan_procesu(stan, proces["id"])
    if wpis["status"] != "w-toku":
        raise Przerwij(ODMOWA, "ODMOWA: %s nie jest w toku" % proces["id"])
    sprawdz_galaz(repo, proces, wpis)
    galaz, agent = wpis.get("galaz"), wpis.get("agent")
    wpis["status"] = wpis.get("poprzedni_status") or "nierozpoczety"
    wpis["poprzedni_status"] = None
    wpis["galaz"] = None
    wpis["agent"] = None
    dopisz_historie(wpis, "porzuc", "praca porzucona" + ("; " + argumenty.uwaga if argumenty.uwaga else ""), galaz, agent)
    zapisz_stan(repo, stan, rejestr)
    print("%s -> %s" % (proces["id"], NAZWY_STATUSOW[wpis["status"]]))
    return OK


def k_klasyfikuj(repo, rejestr, argumenty):
    podzial = klasyfikuj(rejestr, zmienione_pliki(repo, argumenty.staged))
    if argumenty.json:
        print(json.dumps(podzial, ensure_ascii=False, indent=2))
        return OK
    for identyfikator in sorted(podzial["procesy"]):
        print("%s %s:" % (identyfikator, proces_z_rejestru(rejestr, identyfikator)["nazwa"]))
        for sciezka in podzial["procesy"][identyfikator]:
            print("  " + sciezka)
    if podzial["wspolny"]:
        print("wspolny (podział na poziomie zawartości):")
        for wpis in podzial["wspolny"]:
            print("  %s%s" % (wpis["plik"], " (kandydaci: %s)" % ", ".join(wpis["kandydaci"]) if wpis["kandydaci"] else ""))
    if podzial["poza-procesem"]:
        print("poza-procesem:")
        for sciezka in podzial["poza-procesem"]:
            print("  " + sciezka)
    if not any([podzial["procesy"], podzial["wspolny"], podzial["poza-procesem"]]):
        print("brak zmian")
    return OK


def k_weryfikuj(repo, rejestr, argumenty):
    wynik = weryfikuj(repo, rejestr, wczytaj_stan(repo))
    if argumenty.json:
        print(json.dumps(wynik, ensure_ascii=False, indent=2))
    else:
        for informacja in wynik["informacje"]:
            print("info: " + informacja)
        for ostrzezenie in wynik["ostrzezenia"]:
            print("ostrzeżenie: " + ostrzezenie)
        for blad in wynik["bledy"]:
            print("nie: " + blad)
        print("weryfikacja: %s" % ("zgodna" if not wynik["bledy"] else "NIEZGODNA (%d)" % len(wynik["bledy"])))
    return BLAD if wynik["bledy"] else OK


def k_raport(repo, rejestr, argumenty):
    stan = wczytaj_stan(repo)
    zapisz_tekst(os.path.join(repo, RAPORT), zbuduj_raport(rejestr, stan))
    print("zapisano " + RAPORT)
    return OK


def k_hook(repo, rejestr, argumenty):
    return hook_pre_commit(repo, rejestr)


def k_zainstaluj(repo, rejestr, argumenty):
    for nazwa in ("pre-commit", "pre-push"):
        sciezka = os.path.join(repo, ".githooks", nazwa)
        if not os.path.isfile(sciezka):
            raise Przerwij(ZLE_ARGUMENTY, "brak hooka .githooks/%s" % nazwa)
        if os.name == "posix":
            os.chmod(sciezka, os.stat(sciezka).st_mode | 0o111)
    git(repo, "config", "--local", "core.hooksPath", ".githooks")
    globalny = git(repo, "config", "--global", "--get", "core.hooksPath", sprawdz=False).stdout.decode("utf-8").strip()
    print("ustawiono lokalnie core.hooksPath=.githooks (pre-commit: hook-pre-commit supervisora)")
    if globalny:
        print("globalne hooki (%s) są wywoływane dalej przez .githooks (np. pre-push)" % globalny)
    return OK


def parser():
    glowny = argparse.ArgumentParser(prog="supervisor.py", description="migration-supervisor: postęp i guard migracji P1-P7")
    glowny.add_argument("--repo", help="korzeń repozytorium (domyślnie bieżące repo git)")
    komendy = glowny.add_subparsers(dest="komenda", required=True)

    p = komendy.add_parser("info", help="postęp migracji")
    p.add_argument("--json", action="store_true")
    p.set_defaults(funkcja=k_info)

    p = komendy.add_parser("status", help="stan procesu")
    p.add_argument("proces", nargs="?")
    p.add_argument("--json", action="store_true")
    p.set_defaults(funkcja=k_status)

    p = komendy.add_parser("rozpocznij", help="zgoda na migrację procesu (guard)")
    p.add_argument("proces")
    p.add_argument("--agent", default="nieznany")
    p.add_argument("--galaz")
    p.add_argument("--uwaga")
    p.add_argument("--dokonczenie", action="store_true")
    p.set_defaults(funkcja=k_rozpocznij)

    p = komendy.add_parser("zakoncz", help="zakończenie migracji procesu")
    p.add_argument("proces")
    p.add_argument("--status", required=True, choices=("zmigrowany", "czesciowy"))
    p.add_argument("--dokument", required=True)
    p.add_argument("--braki")
    p.add_argument("--uwaga")
    p.set_defaults(funkcja=k_zakoncz)

    p = komendy.add_parser("odnotuj", help="zmiana w procesie zmigrowanym, częściowym albo w toku")
    p.add_argument("proces")
    p.add_argument("--opis", required=True)
    p.add_argument("--agent")
    p.add_argument("--braki", help="nowa lista braków procesu 'czesciowy' (rozdzielana ';')")
    p.set_defaults(funkcja=k_odnotuj)

    p = komendy.add_parser("porzuc", help="porzucenie pracy w toku")
    p.add_argument("proces")
    p.add_argument("--uwaga")
    p.set_defaults(funkcja=k_porzuc)

    p = komendy.add_parser("klasyfikuj", help="zmienione pliki -> procesy")
    p.add_argument("--staged", action="store_true")
    p.add_argument("--json", action="store_true")
    p.set_defaults(funkcja=k_klasyfikuj)

    p = komendy.add_parser("weryfikuj", help="spójność stanu z kodem")
    p.add_argument("--json", action="store_true")
    p.set_defaults(funkcja=k_weryfikuj)

    komendy.add_parser("raport", help="regeneracja raportu").set_defaults(funkcja=k_raport)
    komendy.add_parser("hook-pre-commit", help="sprawdzenie indeksu przed commitem").set_defaults(funkcja=k_hook)
    komendy.add_parser("zainstaluj-hooki", help="lokalnie core.hooksPath=.githooks").set_defaults(funkcja=k_zainstaluj)
    return glowny


def main(argv=None):
    ustaw_utf8()
    try:
        argumenty = parser().parse_args(argv)
    except SystemExit as wyjscie:
        return ZLE_ARGUMENTY if wyjscie.code else OK
    try:
        repo = korzen_repo(argumenty.repo)
        return argumenty.funkcja(repo, wczytaj_rejestr(), argumenty)
    except Przerwij as przerwanie:
        print(przerwanie.komunikat, file=sys.stderr)
        return przerwanie.kod


if __name__ == "__main__":
    sys.exit(main())
