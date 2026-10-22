#!/usr/bin/env python3
"""Klasyfikacja plikow legacy wedlug wzorcow (Python 3, tylko biblioteka standardowa).

Silnik jest narzedziem zespolu; reguly dostarcza czlowiek albo agent w pliku
`wzorce.json` obok tego skryptu. Bez `wzorce.json` skrypt konczy sie komunikatem
"brak regul" (kod 0).

Format `wzorce.json`:

    {
      "katalog": "../legacy-app-parent/legacy-app",  # skanowany katalog, wzgledem wzorce.json
      "pomijaj": ["**/bin/**"],            # opcjonalnie: globy pomijanych plikow
      "wzorce": [                          # reguly plikowe; pierwsza pasujaca wygrywa
        {"nazwa": "DAL (Table Data Gateway)",
         "opis": "klasy dostepu do bazy",
         "pliki": ["WypozyczalniaCore/DAL/*.vb"]}
      ],
      "konstrukcje": [                     # reguly konstrukcji: regex szukany w liniach
        {"nazwa": "SQL inline",
         "regex": "\\"\\\\s*(SELECT|INSERT|UPDATE|DELETE)\\\\b",
         "pliki": ["**/*.vb"],             # gdzie szukac (puste = wszedzie)
         "dozwolone": ["WypozyczalniaCore/DAL/**"],   # gdzie wolno; [] = nigdzie
         "opis": "SQL tylko w DAL"}
      ]
    }

Komentarze `#` sa tylko w tym opisie (JSON ich nie dopuszcza). Globy sa wzgledne do `katalog`, separator `/`: `*` = dowolne znaki bez `/`,
`**` = dowolna liczba katalogow, `?` = jeden znak.

Wynik (Markdown na stdout): tabela wzorcow (pliki, linie niepuste, % plikow i linii),
pliki bez wzorca, tabela konstrukcji, naruszenia jako `plik:linia` (sciezki od korzenia repo).

Uzycie:
    python tools/klasyfikuj.py            # tabela Markdown
    python tools/klasyfikuj.py --json     # to samo jako JSON
    python tools/klasyfikuj.py --reguly inne-wzorce.json
Kod wyjscia: 0 (takze przy naruszeniach i braku regul); 2 = blad pliku regul.
"""

import argparse
import json
import os
import re
import sys

KATALOG_SKRYPTU = os.path.dirname(os.path.abspath(__file__))
DOMYSLNE_REGULY = os.path.join(KATALOG_SKRYPTU, "wzorce.json")
DLUGOSC_FRAGMENTU = 90


def znajdz_korzen_repo(start):
    katalog = start
    while True:
        if os.path.exists(os.path.join(katalog, ".git")):
            return katalog
        rodzic = os.path.dirname(katalog)
        if rodzic == katalog:
            return os.path.dirname(KATALOG_SKRYPTU)
        katalog = rodzic


def glob_na_regex(wzorzec_glob):
    """Zamienia glob (`*`, `**`, `?`) na skompilowane wyrazenie regularne."""
    wynik = []
    indeks = 0
    while indeks < len(wzorzec_glob):
        znak = wzorzec_glob[indeks]
        if wzorzec_glob.startswith("**/", indeks):
            wynik.append("(?:.*/)?")
            indeks += 3
        elif wzorzec_glob.startswith("**", indeks):
            wynik.append(".*")
            indeks += 2
        elif znak == "*":
            wynik.append("[^/]*")
            indeks += 1
        elif znak == "?":
            wynik.append("[^/]")
            indeks += 1
        else:
            wynik.append(re.escape(znak))
            indeks += 1
    return re.compile("^" + "".join(wynik) + "$")


def pasuje(sciezka_wzgledna, globy):
    return any(glob_na_regex(glob).match(sciezka_wzgledna) for glob in globy)


def wczytaj_linie(sciezka_pliku):
    with open(sciezka_pliku, "rb") as plik:
        dane = plik.read()
    if dane.startswith(b"\xef\xbb\xbf"):
        dane = dane[3:]
    tekst = dane.decode("utf-8", errors="replace")
    return tekst.replace("\r\n", "\n").replace("\r", "\n").split("\n")


def zbierz_pliki(katalog_skanu, pomijane):
    pliki = []
    for katalog, podkatalogi, nazwy in os.walk(katalog_skanu):
        podkatalogi[:] = sorted(nazwa for nazwa in podkatalogi if nazwa != ".git")
        for nazwa in sorted(nazwy):
            pelna = os.path.join(katalog, nazwa)
            wzgledna = os.path.relpath(pelna, katalog_skanu).replace(os.sep, "/")
            if pomijane and pasuje(wzgledna, pomijane):
                continue
            pliki.append((wzgledna, pelna))
    return pliki


def procent(czesc, calosc):
    return round(100.0 * czesc / calosc, 1) if calosc else 0.0


def klasyfikuj(reguly, sciezka_regul, korzen_repo):
    katalog_skanu = os.path.normpath(
        os.path.join(os.path.dirname(os.path.abspath(sciezka_regul)), reguly.get("katalog", "../legacy-app-parent/legacy-app"))
    )
    if not os.path.isdir(katalog_skanu):
        raise ValueError("katalog do skanowania nie istnieje: %s" % katalog_skanu)

    def od_korzenia(sciezka_pelna):
        return os.path.relpath(sciezka_pelna, korzen_repo).replace(os.sep, "/")

    wzorce = reguly.get("wzorce", [])
    konstrukcje = reguly.get("konstrukcje", [])
    for konstrukcja in konstrukcje:
        konstrukcja["_regex"] = re.compile(konstrukcja["regex"], re.IGNORECASE if konstrukcja.get("ignoruj_wielkosc") else 0)

    statystyki = {wzorzec["nazwa"]: {"pliki": 0, "linie": 0} for wzorzec in wzorce}
    bez_wzorca = []
    liczniki_konstrukcji = {k["nazwa"]: {"wystapienia": 0, "naruszenia": 0} for k in konstrukcje}
    naruszenia = []
    razem_pliki = 0
    razem_linie = 0

    for wzgledna, pelna in zbierz_pliki(katalog_skanu, reguly.get("pomijaj", [])):
        linie = wczytaj_linie(pelna)
        niepuste = sum(1 for linia in linie if linia.strip())
        razem_pliki += 1
        razem_linie += niepuste

        wzorzec_pliku = next((w["nazwa"] for w in wzorce if pasuje(wzgledna, w.get("pliki", []))), None)
        if wzorzec_pliku is None:
            bez_wzorca.append({"plik": od_korzenia(pelna), "linie": niepuste})
        else:
            statystyki[wzorzec_pliku]["pliki"] += 1
            statystyki[wzorzec_pliku]["linie"] += niepuste

        for konstrukcja in konstrukcje:
            zakres = konstrukcja.get("pliki", [])
            if zakres and not pasuje(wzgledna, zakres):
                continue
            dozwolone = pasuje(wzgledna, konstrukcja.get("dozwolone", []))
            for numer, linia in enumerate(linie, start=1):
                if konstrukcja["_regex"].search(linia):
                    liczniki_konstrukcji[konstrukcja["nazwa"]]["wystapienia"] += 1
                    if not dozwolone:
                        liczniki_konstrukcji[konstrukcja["nazwa"]]["naruszenia"] += 1
                        naruszenia.append({
                            "konstrukcja": konstrukcja["nazwa"],
                            "plik": od_korzenia(pelna),
                            "linia": numer,
                            "fragment": linia.strip()[:DLUGOSC_FRAGMENTU],
                        })

    tabela = []
    for wzorzec in wzorce:
        dane = statystyki[wzorzec["nazwa"]]
        tabela.append({
            "wzorzec": wzorzec["nazwa"],
            "opis": wzorzec.get("opis", ""),
            "pliki": dane["pliki"],
            "linie": dane["linie"],
            "proc_plikow": procent(dane["pliki"], razem_pliki),
            "proc_linii": procent(dane["linie"], razem_linie),
        })
    pokryte_pliki = sum(w["pliki"] for w in tabela)
    pokryte_linie = sum(w["linie"] for w in tabela)
    return {
        "katalog": od_korzenia(katalog_skanu),
        "reguly": od_korzenia(os.path.abspath(sciezka_regul)),
        "razem": {"pliki": razem_pliki, "linie": razem_linie},
        "pokrycie": {
            "pliki": pokryte_pliki,
            "linie": pokryte_linie,
            "proc_plikow": procent(pokryte_pliki, razem_pliki),
            "proc_linii": procent(pokryte_linie, razem_linie),
        },
        "wzorce": tabela,
        "bez_wzorca": bez_wzorca,
        "konstrukcje": [
            {
                "konstrukcja": k["nazwa"],
                "opis": k.get("opis", ""),
                "dozwolone": k.get("dozwolone", []),
                "wystapienia": liczniki_konstrukcji[k["nazwa"]]["wystapienia"],
                "naruszenia": liczniki_konstrukcji[k["nazwa"]]["naruszenia"],
            }
            for k in konstrukcje
        ],
        "naruszenia": naruszenia,
    }


def liczba_pl(wartosc):
    return ("%.1f" % wartosc).replace(".", ",")


def komorka(tekst):
    return tekst.replace("|", "\\|")


def jako_markdown(wynik):
    wiersze = []
    razem = wynik["razem"]
    wiersze.append("Katalog: `%s` · reguły: `%s` · plików: %d · linii niepustych: %d"
                   % (wynik["katalog"], wynik["reguly"], razem["pliki"], razem["linie"]))
    wiersze.append("")
    wiersze.append("### Wzorce plików")
    wiersze.append("")
    wiersze.append("| wzorzec | pliki | linie | % plików | % linii |")
    wiersze.append("|---|---:|---:|---:|---:|")
    for wzorzec in wynik["wzorce"]:
        wiersze.append("| %s | %d | %d | %s | %s |" % (
            komorka(wzorzec["wzorzec"]), wzorzec["pliki"], wzorzec["linie"],
            liczba_pl(wzorzec["proc_plikow"]), liczba_pl(wzorzec["proc_linii"])))
    pokrycie = wynik["pokrycie"]
    wiersze.append("| **pokryte wzorcami** | %d | %d | %s | %s |" % (
        pokrycie["pliki"], pokrycie["linie"], liczba_pl(pokrycie["proc_plikow"]), liczba_pl(pokrycie["proc_linii"])))
    wiersze.append("")
    wiersze.append("### Pliki bez wzorca (%d)" % len(wynik["bez_wzorca"]))
    wiersze.append("")
    if wynik["bez_wzorca"]:
        for pozycja in wynik["bez_wzorca"]:
            wiersze.append("- `%s` (%d linii)" % (pozycja["plik"], pozycja["linie"]))
    else:
        wiersze.append("- brak")
    wiersze.append("")
    wiersze.append("### Konstrukcje")
    wiersze.append("")
    if wynik["konstrukcje"]:
        wiersze.append("| konstrukcja | dozwolona w | wystąpienia | naruszenia |")
        wiersze.append("|---|---|---:|---:|")
        for konstrukcja in wynik["konstrukcje"]:
            dozwolone = ", ".join("`%s`" % glob for glob in konstrukcja["dozwolone"]) or "nigdzie"
            wiersze.append("| %s | %s | %d | %d |" % (
                komorka(konstrukcja["konstrukcja"]), dozwolone, konstrukcja["wystapienia"], konstrukcja["naruszenia"]))
    else:
        wiersze.append("- brak reguł konstrukcji")
    wiersze.append("")
    wiersze.append("### Naruszenia (%d)" % len(wynik["naruszenia"]))
    wiersze.append("")
    if wynik["naruszenia"]:
        for pozycja in wynik["naruszenia"]:
            wiersze.append("- `%s:%d` [%s] %s" % (
                pozycja["plik"], pozycja["linia"], pozycja["konstrukcja"], pozycja["fragment"].replace("`", "'")))
    else:
        wiersze.append("- brak")
    return "\n".join(wiersze)


def main(argumenty):
    parser = argparse.ArgumentParser(description="Klasyfikacja plikow legacy wedlug wzorcow z wzorce.json.")
    parser.add_argument("--json", action="store_true", help="wynik jako JSON")
    parser.add_argument("--reguly", default=DOMYSLNE_REGULY, help="plik regul (domyslnie wzorce.json obok skryptu)")
    opcje = parser.parse_args(argumenty)

    if hasattr(sys.stdout, "reconfigure"):
        sys.stdout.reconfigure(encoding="utf-8", newline="\n")

    if not os.path.isfile(opcje.reguly):
        print("Brak reguł: nie znaleziono pliku %s." % os.path.relpath(opcje.reguly).replace(os.sep, "/"))
        print("Utwórz wzorce.json obok klasyfikuj.py (format: opis na początku skryptu) i uruchom ponownie.")
        return 0

    try:
        with open(opcje.reguly, "r", encoding="utf-8-sig") as plik:
            reguly = json.load(plik)
    except (OSError, ValueError) as blad:
        print("Błąd pliku reguł %s: %s" % (opcje.reguly, blad))
        return 2

    if not reguly.get("wzorce") and not reguly.get("konstrukcje"):
        print("Brak reguł: plik %s nie zawiera wzorców ani konstrukcji." % os.path.relpath(opcje.reguly).replace(os.sep, "/"))
        return 0

    try:
        wynik = klasyfikuj(reguly, opcje.reguly, znajdz_korzen_repo(KATALOG_SKRYPTU))
    except (ValueError, re.error, KeyError) as blad:
        print("Błąd reguł: %s" % blad)
        return 2

    if opcje.json:
        print(json.dumps(wynik, ensure_ascii=False, indent=2))
    else:
        print(jako_markdown(wynik))
    return 0


if __name__ == "__main__":
    sys.exit(main(sys.argv[1:]))
