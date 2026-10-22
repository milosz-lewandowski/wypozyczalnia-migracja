"""Reguły statyczne bramki jakości dla kodu backendu nowej aplikacji (C#).

Skanuje <backend>/src/**/*.cs (kod produkcyjny, bez katalogów bin/ i obj/); testy szuka w
<backend>/tests/**/*.cs. Zwraca tylko „nie” i dlaczego: plik:linia, nazwa reguły, jedno zdanie powodu.
Nie podpowiada gotowego kodu.

Reguły w kodzie (poza komentarzami i literałami tekstowymi):
  kwota-double          double/float (także Double/Single) w kodzie produkcyjnym
  half-up               tryb zaokrąglenia MidpointRounding.AwayFromZero
Reguły w literałach tekstowych ("...", @"...", $"...", $@"...", raw (trzy cudzysłowy), także wielolinijkowe):
  sql-poza-repository   tekst SQL poza folderem Repositories/
Reguły plików:
  test-dla-serwisu      każda klasa *Service w src ma w tests plik testu, którego nazwa zaczyna się od nazwy serwisu

Nowa reguła = jeden wpis w REGULY_KODU albo REGULY_LITERALOW (nazwa, wzorzec, zakres, powód).
Świadomy wyjątek: komentarz na tej samej linii `// bramki:ok <reguła>` (widoczny w review);
przy literale wielolinijkowym wystarczy na dowolnej jego linii.

Użycie: python tools/bramka/sprawdz.py <korzeń_repo> [--backend <ścieżka względna>] [--format github]
  --backend   katalog backendu względem korzenia (domyślnie new-app-parent/new-app/backend)
  --format github   dodatkowo adnotacje ::error dla GitHub Actions
Kod wyjścia: 0 = czysto, 1 = naruszenia, 2 = zły katalog albo złe argumenty.
"""
import os
import re
import sys
from collections import namedtuple

BACKEND_DOMYSLNY = os.path.join("new-app-parent", "new-app", "backend")
POMIJANE_KATALOGI = {"bin", "obj"}

Regula = namedtuple("Regula", "nazwa wzorzec zakres opis")


def otworz(sciezka):
    """open() odporny na długie ścieżki Windows (powyżej 260 znaków); BOM UTF-8 pomijany."""
    if os.name == "nt":
        pelna = os.path.abspath(sciezka)
        if len(pelna) > 240 and not pelna.startswith("\\\\?\\"):
            sciezka = "\\\\?\\" + pelna
    return open(sciezka, encoding="utf-8-sig")


def WSZEDZIE(sciezka):
    return True


def POZA_REPOSITORIES(sciezka):
    return "/repositories/" not in ("/" + sciezka).lower()


SQL = re.compile(r"\bselect\b.+\bfrom\b|\binsert\s+into\b|\bupdate\s+\w+\s+set\b"
                 r"|\bdelete\s+from\b|\bmerge\s+into\b|\bcreate\s+(?:table|index|view)\b|\bpragma\s+\w+",
                 re.IGNORECASE)

REGULY_KODU = [
    # Single bez nawiasu i <...> za nazwą: LINQ .Single() to nie typ liczbowy
    Regula("kwota-double",
           re.compile(r"\b(?:double|float|Double)\b|\bSingle\b(?!\s*[(<])"), WSZEDZIE,
           "liczba zmiennoprzecinkowa nie przechowuje dokładnie groszy, więc kwoty rozjadą się z legacy"),
    Regula("half-up", re.compile(r"\bAwayFromZero\b"), WSZEDZIE,
           "MidpointRounding.AwayFromZero zaokrągla połówki inaczej niż legacy, więc kwota różni się o grosz"),
]

REGULY_LITERALOW = [
    Regula("sql-poza-repository", SQL, POZA_REPOSITORIES,
           "zapytanie SQL poza warstwą repozytorium łamie podział warstw nowej aplikacji"),
]

REGULA_TESTU = Regula("test-dla-serwisu", None, None,
                      "serwis nie ma w tests testu, którego nazwa zaczyna się od nazwy serwisu, "
                      "więc jego reguł nic nie sprawdza")


class Rozbior:
    """Rozbiór pliku C#: kod bez komentarzy i literałów (lista linii) oraz lista literałów tekstowych."""

    def __init__(self, tekst):
        self.tekst = tekst
        self.kod = []          # znaki kodu; komentarze i literały jako spacje, nowe linie zachowane
        self.literaly = []     # (linia_start, linia_koniec, treść)
        self.i = 0
        self.linia = 1
        self._rozbierz()
        self.linie_kodu = "".join(self.kod).split("\n")

    def _znak(self, przesuniecie=0):
        pozycja = self.i + przesuniecie
        return self.tekst[pozycja] if pozycja < len(self.tekst) else ""

    def _pochlon(self, ile, do_kodu):
        """Przesuwa o `ile` znaków; do kodu trafiają te znaki (do_kodu) albo spacje; nowe linie zawsze."""
        fragment = self.tekst[self.i:self.i + ile]
        for znak in fragment:
            if znak == "\n":
                self.kod.append("\n")
                self.linia += 1
            else:
                self.kod.append(znak if do_kodu else " ")
        self.i += len(fragment)
        return fragment

    def _literal_surowy(self, start_linii):
        """Raw string: n >= 3 cudzysłowów, koniec na tej samej liczbie cudzysłowów."""
        ile = 0
        while self._znak(ile) == '"':
            ile += 1
        self._pochlon(ile, False)
        tresc = []
        while self.i < len(self.tekst):
            if self.tekst.startswith('"' * ile, self.i):
                self._pochlon(ile, False)
                break
            tresc.append(self._pochlon(1, False))
        self.literaly.append((start_linii, self.linia, "".join(tresc)))

    def _literal_zwykly(self, start_linii, dobitny, interpolowany):
        """"..." (escape \\), @"..." (escape ""), $"..." z dziurami {...} (literały zagnieżdżone w dziurach)."""
        self._pochlon(1, False)
        tresc = []
        glebokosc = 0
        while self.i < len(self.tekst):
            znak = self._znak()
            if glebokosc == 0:
                if znak == '"':
                    if dobitny and self._znak(1) == '"':
                        tresc.append(self._pochlon(2, False))
                        continue
                    self._pochlon(1, False)
                    break
                if znak == "\\" and not dobitny:
                    tresc.append(self._pochlon(2, False))
                    continue
                if znak == "\n" and not dobitny:
                    break  # niezamknięty literał kończy się z linią
                if interpolowany and znak == "{":
                    if self._znak(1) == "{":
                        tresc.append(self._pochlon(2, False))
                        continue
                    glebokosc = 1
                tresc.append(self._pochlon(1, False))
            else:
                if znak in '"$@' and self._probuj_literal(sprawdz_tylko=True):
                    self._probuj_literal()
                    tresc.append(" ")
                    continue
                if znak == "{":
                    glebokosc += 1
                elif znak == "}":
                    glebokosc -= 1
                tresc.append(self._pochlon(1, False))
        self.literaly.append((start_linii, self.linia, "".join(tresc)))

    def _probuj_literal(self, sprawdz_tylko=False):
        """Rozpoznaje prefiks literału ($, @, $@, @$, $$...) przed cudzysłowem; True, gdy to literał."""
        pozycja = self.i
        prefiks = ""
        while pozycja < len(self.tekst) and self.tekst[pozycja] in "$@":
            prefiks += self.tekst[pozycja]
            pozycja += 1
        if pozycja >= len(self.tekst) or self.tekst[pozycja] != '"' or prefiks.count("@") > 1:
            return False
        if sprawdz_tylko:
            return True
        start_linii = self.linia
        self._pochlon(len(prefiks), False)
        if self.tekst.startswith('"""', self.i):
            self._literal_surowy(start_linii)
        else:
            self._literal_zwykly(start_linii, "@" in prefiks, "$" in prefiks)
        return True

    def _znakowy(self):
        """Literał znakowy 'x', '\\'', '"'."""
        koniec = self.i + 1
        while koniec < len(self.tekst) and self.tekst[koniec] not in "'\n":
            koniec += 2 if self.tekst[koniec] == "\\" else 1
        self._pochlon(koniec + 1 - self.i, False)

    def _rozbierz(self):
        while self.i < len(self.tekst):
            znak = self._znak()
            if self.tekst.startswith("//", self.i):
                koniec = self.tekst.find("\n", self.i)
                self._pochlon((len(self.tekst) if koniec < 0 else koniec) - self.i, False)
            elif self.tekst.startswith("/*", self.i):
                koniec = self.tekst.find("*/", self.i + 2)
                self._pochlon((len(self.tekst) if koniec < 0 else koniec + 2) - self.i, False)
            elif znak in '"$@':
                if not self._probuj_literal():
                    self._pochlon(1, True)
            elif znak == "'":
                self._znakowy()
            else:
                self._pochlon(1, True)


def sprawdz_plik(sciezka, wzgledna):
    """Zwraca listę naruszeń (względna, numer linii, nazwa reguły, opis)."""
    naruszenia = []
    sciezka_ukosniki = wzgledna.replace(os.sep, "/")
    reguly_kodu = [r for r in REGULY_KODU if r.zakres(sciezka_ukosniki)]
    reguly_literalow = [r for r in REGULY_LITERALOW if r.zakres(sciezka_ukosniki)]
    with otworz(sciezka) as plik:
        tekst = plik.read().replace("\r\n", "\n").replace("\r", "\n")
    zgody = [set(re.findall(r"bramki:ok\s+([\w-]+)", linia)) for linia in tekst.split("\n")]
    rozbior = Rozbior(tekst)
    for indeks, kod in enumerate(rozbior.linie_kodu):
        for regula in reguly_kodu:
            if regula.wzorzec.search(kod) and regula.nazwa not in zgody[indeks]:
                naruszenia.append((sciezka_ukosniki, indeks + 1, regula.nazwa, regula.opis))
    for start, koniec, tresc in rozbior.literaly:
        jednolinijkowa = re.sub(r"\s+", " ", tresc)
        zgody_literalu = set().union(*zgody[start - 1:koniec])
        for regula in reguly_literalow:
            if regula.nazwa not in zgody_literalu and regula.wzorzec.search(jednolinijkowa):
                naruszenia.append((sciezka_ukosniki, start, regula.nazwa, regula.opis))
    naruszenia.sort(key=lambda naruszenie: (naruszenie[1], naruszenie[2]))
    return naruszenia


def pliki_cs(katalog):
    for korzen, katalogi, pliki in os.walk(katalog):
        katalogi[:] = sorted(k for k in katalogi if k.lower() not in POMIJANE_KATALOGI)
        for nazwa in sorted(pliki):
            if nazwa.endswith(".cs"):
                yield os.path.join(korzen, nazwa)


def deklaracja_typu(sciezka, nazwa_typu):
    """(numer linii, rodzaj) deklaracji typu; (1, None), gdy nie znaleziono."""
    wzorzec = re.compile(r"\b(class|record|interface|struct)\s+" + re.escape(nazwa_typu) + r"\b")
    with otworz(sciezka) as plik:
        for numer, linia in enumerate(plik, start=1):
            trafienie = wzorzec.search(linia.split("//", 1)[0])
            if trafienie:
                return numer, trafienie.group(1)
    return 1, None


def sprawdz_testy_serwisow(repo, katalog_src, katalog_testow):
    """test-dla-serwisu: klasa *Service w src ma w tests plik testu o nazwie zaczynającej się od nazwy serwisu."""
    testy = set()
    if os.path.isdir(katalog_testow):
        testy = {os.path.basename(p)[:-len(".cs")] for p in pliki_cs(katalog_testow)}
    naruszenia = []
    for pelna in pliki_cs(katalog_src):
        klasa = os.path.basename(pelna)[:-len(".cs")]
        if not klasa.endswith("Service"):
            continue
        numer, rodzaj = deklaracja_typu(pelna, klasa)
        if rodzaj == "interface":
            continue
        if any(test.startswith(klasa) and test != klasa for test in testy):
            continue
        with otworz(pelna) as plik:
            linie = plik.readlines()
        if numer <= len(linie) and re.search(r"bramki:ok\s+" + REGULA_TESTU.nazwa + r"\b", linie[numer - 1]):
            continue
        wzgledna = os.path.relpath(pelna, repo).replace(os.sep, "/")
        naruszenia.append((wzgledna, numer, REGULA_TESTU.nazwa, REGULA_TESTU.opis))
    return naruszenia


def main(argv):
    try:
        sys.stdout.reconfigure(errors="replace")
    except AttributeError:
        pass
    format_github = False
    backend = BACKEND_DOMYSLNY
    if "--format" in argv:
        indeks = argv.index("--format")
        format_github = argv[indeks + 1:indeks + 2] == ["github"]
        argv = argv[:indeks] + argv[indeks + 2:]
    if "--backend" in argv:
        indeks = argv.index("--backend")
        backend = argv[indeks + 1] if indeks + 1 < len(argv) else ""
        argv = argv[:indeks] + argv[indeks + 2:]
    if len(argv) != 1 or not backend:
        print("użycie: python tools/bramka/sprawdz.py <korzeń_repo> [--backend <ścieżka>] [--format github]",
              file=sys.stderr)
        return 2
    repo = argv[0]
    katalog_src = os.path.join(repo, backend, "src")
    katalog_testow = os.path.join(repo, backend, "tests")
    if not os.path.isdir(katalog_src):
        print("brak katalogu %s" % katalog_src, file=sys.stderr)
        return 2
    naruszenia = []
    plikow = 0
    for pelna in pliki_cs(katalog_src):
        plikow += 1
        naruszenia += sprawdz_plik(pelna, os.path.relpath(pelna, repo))
    naruszenia += sprawdz_testy_serwisow(repo, katalog_src, katalog_testow)
    for sciezka, numer, regula, opis in naruszenia:
        print("nie: %s:%d: [%s] %s" % (sciezka, numer, regula, opis))
        if format_github:
            print("::error file=%s,line=%d,title=bramka %s::%s" % (sciezka, numer, regula, opis))
    print("reguły statyczne: plików %d, naruszeń %d -> %s" % (
        plikow, len(naruszenia), "CZERWONO" if naruszenia else "zielono"))
    return 1 if naruszenia else 0


if __name__ == "__main__":
    sys.exit(main(sys.argv[1:]))
