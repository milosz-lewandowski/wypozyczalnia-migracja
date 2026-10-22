#!/usr/bin/env python3
"""Sprawdza dowody `plik:linia` w dokumentach repo (Python 3, tylko biblioteka standardowa).

Skanuje wszystkie pliki *.md w repo (w tym kazdy AGENTS.md). Dowod = tekst w backtickach:
    `sciezka/plik.ext:N`          linia N
    `sciezka/plik.ext:N-M`        zakres linii N-M
    `sciezka/plik.ext:N#Symbol`   linia N, a Symbol wystepuje w liniach N-2 .. N+2 (dla zakresu: N-2 .. M+2)
Sciezka jest wzgledna do korzenia repo; gdy tam pliku nie ma, liczona od katalogu dokumentu.
Dla kazdego dowodu: plik istnieje, zakres miesci sie w dlugosci pliku, Symbol wystepuje obok.
Pomija bloki kodu ``` i ~~~ oraz odwolania bez numeru linii.

Skrypt sprawdza, ze dowod ISTNIEJE, nie ze twierdzenie obok niego jest prawdziwe.

Uzycie (z korzenia repo):
    python tools/sprawdz_dowody.py        # korzen repo = katalog z .git nad skryptem
    python tools/sprawdz_dowody.py .      # jawny korzen
Kod wyjscia: 0 = brak bledow, 1 = sa bledy, 2 = brak katalogu.
Port narzedzia recon_check.py (skan calego repo zamiast docs/ w korzeniu).
"""

import os
import re
import sys

POMIJANE_KATALOGI = {
    ".git", "node_modules", "build", "dist", "out", ".gradle",
    "test-results", "playwright-report", "__pycache__", ".idea",
}
MARGINES_SYMBOLU = 2

WZORZEC_KODU_INLINE = re.compile(r"(?<!`)(`+)(?!`)(.+?)(?<!`)\1(?!`)")
WZORZEC_DOWODU = re.compile(
    r"^(?P<sciezka>[^`:#\\]+?\.[A-Za-z0-9_]+)"
    r":(?P<poczatek>\d+)(?:-(?P<koniec>\d+))?"
    r"(?:#(?P<symbol>.+))?$"
)
WZORZEC_PLOTU = re.compile(r"^\s{0,3}(```|~~~)")


def znajdz_korzen_repo(start):
    katalog = start
    while True:
        if os.path.exists(os.path.join(katalog, ".git")):
            return katalog
        rodzic = os.path.dirname(katalog)
        if rodzic == katalog:
            return os.path.dirname(os.path.dirname(start))
        katalog = rodzic


def znajdz_dokumenty(korzen_repo):
    dokumenty = []
    for katalog, podkatalogi, pliki in os.walk(korzen_repo):
        podkatalogi[:] = sorted(nazwa for nazwa in podkatalogi if nazwa not in POMIJANE_KATALOGI)
        for nazwa_pliku in sorted(pliki):
            if nazwa_pliku.lower().endswith(".md"):
                dokumenty.append(os.path.join(katalog, nazwa_pliku))
    return dokumenty


def wyciagnij_dowody(tekst_dokumentu):
    """Zwraca liste (numer_linii_w_dokumencie, tekst_dowodu, dopasowanie)."""
    dowody = []
    w_bloku_kodu = False
    znacznik_plotu = None
    for numer_linii, linia in enumerate(tekst_dokumentu.split("\n"), start=1):
        dopasowanie_plotu = WZORZEC_PLOTU.match(linia)
        if dopasowanie_plotu:
            plot = dopasowanie_plotu.group(1)
            if not w_bloku_kodu:
                w_bloku_kodu = True
                znacznik_plotu = plot
            elif plot == znacznik_plotu:
                w_bloku_kodu = False
                znacznik_plotu = None
            continue
        if w_bloku_kodu:
            continue
        for dopasowanie_kodu in WZORZEC_KODU_INLINE.finditer(linia):
            tresc = dopasowanie_kodu.group(2).strip()
            dopasowanie_dowodu = WZORZEC_DOWODU.match(tresc)
            if dopasowanie_dowodu:
                dowody.append((numer_linii, tresc, dopasowanie_dowodu))
    return dowody


def wczytaj_linie(sciezka_pliku, pamiec_plikow):
    if sciezka_pliku not in pamiec_plikow:
        with open(sciezka_pliku, "r", encoding="utf-8", errors="replace") as plik:
            tekst = plik.read()
        tekst = tekst.replace("\r\n", "\n").replace("\r", "\n")
        linie = tekst.split("\n")
        if linie and linie[-1] == "":
            linie.pop()
        pamiec_plikow[sciezka_pliku] = linie
    return pamiec_plikow[sciezka_pliku]


def rozwiaz_sciezke(korzen_repo, katalog_dokumentu, sciezka_wzgledna):
    czesci = sciezka_wzgledna.split("/")
    for baza in (korzen_repo, katalog_dokumentu):
        kandydat = os.path.normpath(os.path.join(baza, *czesci))
        if os.path.isfile(kandydat):
            if os.path.relpath(kandydat, korzen_repo).startswith(".."):
                return None
            return kandydat
    return None


def sprawdz_dowod(korzen_repo, katalog_dokumentu, dopasowanie, pamiec_plikow):
    """Zwraca None gdy dowod poprawny, inaczej opis przyczyny."""
    sciezka_wzgledna = dopasowanie.group("sciezka").strip()
    poczatek = int(dopasowanie.group("poczatek"))
    koniec = int(dopasowanie.group("koniec") or poczatek)
    symbol = dopasowanie.group("symbol")

    if sciezka_wzgledna.startswith("/"):
        return "sciezka bezwzgledna"
    sciezka_pliku = rozwiaz_sciezke(korzen_repo, katalog_dokumentu, sciezka_wzgledna)
    if sciezka_pliku is None:
        return "plik nie istnieje"
    linie = wczytaj_linie(sciezka_pliku, pamiec_plikow)
    if poczatek < 1 or koniec < poczatek:
        return "niepoprawny zakres linii %d-%d" % (poczatek, koniec)
    if koniec > len(linie):
        return "linia %d poza plikiem (plik ma %d linii)" % (koniec, len(linie))
    if symbol is not None:
        symbol = symbol.strip()
        od_linii = max(1, poczatek - MARGINES_SYMBOLU)
        do_linii = min(len(linie), koniec + MARGINES_SYMBOLU)
        fragment = linie[od_linii - 1:do_linii]
        if not any(symbol in linia for linia in fragment):
            return "symbol '%s' nie wystepuje w liniach %d-%d" % (symbol, od_linii, do_linii)
    return None


def main(argumenty):
    if len(argumenty) > 1:
        korzen_repo = os.path.abspath(argumenty[1])
    else:
        korzen_repo = znajdz_korzen_repo(os.path.dirname(os.path.abspath(__file__)))
    if not os.path.isdir(korzen_repo):
        print("Brak katalogu: %s" % korzen_repo)
        return 2

    pamiec_plikow = {}
    liczba_dowodow = 0
    bledy = []
    statystyki_dokumentow = []

    for sciezka_dokumentu in znajdz_dokumenty(korzen_repo):
        dokument_wzgledny = os.path.relpath(sciezka_dokumentu, korzen_repo).replace(os.sep, "/")
        with open(sciezka_dokumentu, "r", encoding="utf-8", errors="replace") as plik:
            tekst_dokumentu = plik.read().replace("\r\n", "\n")
        dowody = wyciagnij_dowody(tekst_dokumentu)
        if not dowody and os.path.basename(sciezka_dokumentu) != "AGENTS.md":
            continue
        bledy_dokumentu = 0
        for numer_linii, tekst_dowodu, dopasowanie in dowody:
            przyczyna = sprawdz_dowod(korzen_repo, os.path.dirname(sciezka_dokumentu), dopasowanie, pamiec_plikow)
            if przyczyna is not None:
                bledy_dokumentu += 1
                bledy.append((dokument_wzgledny, numer_linii, tekst_dowodu, przyczyna))
        liczba_dowodow += len(dowody)
        statystyki_dokumentow.append((dokument_wzgledny, len(dowody), bledy_dokumentu))

    for dokument_wzgledny, numer_linii, tekst_dowodu, przyczyna in bledy:
        print("BLAD %s:%d  `%s`  -> %s" % (dokument_wzgledny, numer_linii, tekst_dowodu, przyczyna))

    print("")
    print("| dokument | dowody | bledy |")
    print("|---|---:|---:|")
    for dokument_wzgledny, liczba, liczba_bledow in statystyki_dokumentow:
        print("| `%s` | %d | %d |" % (dokument_wzgledny, liczba, liczba_bledow))
    print("")
    print("Dokumenty: %d (z dowodami albo AGENTS.md), dowody: %d, bledy: %d" % (len(statystyki_dokumentow), liczba_dowodow, len(bledy)))
    if bledy:
        print("Wynik: liczba bledow %d - popraw dowody powyzej." % len(bledy))
        return 1
    print("Wynik: 0 bledow - wszystkie dowody istnieja (to nie dowodzi, ze twierdzenia sa prawdziwe).")
    return 0


if __name__ == "__main__":
    sys.exit(main(sys.argv))
