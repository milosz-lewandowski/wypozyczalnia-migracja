"""Testy migration-supervisor (unittest, tymczasowe repozytoria git).

Uruchomienie z korzenia repo:
    python -m unittest discover -s .agents/skills/migration-supervisor/scripts -p "test_*.py" -v
"""

import json
import os
import shutil
import subprocess
import sys
import tempfile
import unittest

SKRYPTY = os.path.dirname(os.path.abspath(__file__))
SKILL = os.path.dirname(SKRYPTY)
REPO_ZRODLOWE = os.path.dirname(os.path.dirname(os.path.dirname(SKILL)))
SUPERVISOR = os.path.join(SKRYPTY, "supervisor.py")
STAN = "new-app-parent/new-docs/migracja/stan-migracji.json"
P1_PLIK = "new-app-parent/new-app/backend/src/Wypozyczalnia.Api/Services/PricingService.cs"
P1_DOKUMENT = "new-app-parent/new-docs/migracja/P1-wycena.md"
WSPOLNY_PLIK = "new-app-parent/new-app/backend/src/Wypozyczalnia.Api/Config/ServiceRegistration.cs"


def znajdz_sh():
    for kandydat in (shutil.which("sh"), r"C:\Program Files\Git\bin\sh.exe", r"C:\Program Files\Git\usr\bin\sh.exe"):
        if kandydat and os.path.isfile(kandydat):
            return kandydat
    return None


def stan_bazowy():
    procesy = {}
    for numer in range(1, 8):
        procesy["P%d" % numer] = {"status": "nierozpoczety", "poprzedni_status": None, "galaz": None, "agent": None,
                                  "dokument": None, "braki": [], "historia": []}
    procesy["P2"]["status"] = "czesciowy"
    procesy["P2"]["braki"] = ["anulowanie"]
    procesy["P2"]["historia"] = [{"kiedy": "2026-10-22T09:00:00+02:00", "akcja": "stan-bazowy", "opis": "baza",
                                  "galaz": "m3-start", "agent": None}]
    return {"wersja": 1, "procesy": procesy}


class TymczasoweRepo(unittest.TestCase):
    def setUp(self):
        self.katalog = tempfile.mkdtemp(prefix="supervisor-test-")
        self.repo = os.path.join(self.katalog, "repo")
        os.makedirs(self.repo)
        self.gitconfig = os.path.join(self.katalog, "gitconfig")
        with open(self.gitconfig, "w", encoding="utf-8") as plik:
            plik.write("[user]\n\tname = Test\n\temail = test@example.invalid\n[init]\n\tdefaultBranch = main\n")
        self.env = dict(os.environ, GIT_CONFIG_GLOBAL=self.gitconfig, GIT_CONFIG_NOSYSTEM="1")
        self.git("init", "-q")
        self.zapisz(STAN, json.dumps(stan_bazowy(), ensure_ascii=False, indent=2) + "\n")
        self.zapisz("README.md", "repo testowe\n")
        self.git("add", "-A")
        self.git("commit", "-q", "-m", "baza")

    def tearDown(self):
        shutil.rmtree(self.katalog, ignore_errors=True)

    def git(self, *argumenty, sprawdz=True):
        wynik = subprocess.run(["git", *argumenty], cwd=self.repo, env=self.env, capture_output=True, text=True,
                               encoding="utf-8", errors="replace")
        if sprawdz:
            self.assertEqual(wynik.returncode, 0, wynik.stderr)
        return wynik

    def zapisz(self, sciezka, tresc):
        pelna = os.path.join(self.repo, sciezka)
        os.makedirs(os.path.dirname(pelna), exist_ok=True)
        with open(pelna, "w", encoding="utf-8", newline="\n") as plik:
            plik.write(tresc)

    def sup(self, *argumenty):
        return subprocess.run([sys.executable, SUPERVISOR, "--repo", self.repo, *argumenty], cwd=self.repo, env=self.env,
                              capture_output=True, text=True, encoding="utf-8", errors="replace")

    def stan(self):
        with open(os.path.join(self.repo, STAN), encoding="utf-8") as plik:
            return json.load(plik)

    def ustaw_status(self, proces, status, **pola):
        stan = self.stan()
        stan["procesy"][proces]["status"] = status
        stan["procesy"][proces].update(pola)
        self.zapisz(STAN, json.dumps(stan, ensure_ascii=False, indent=2) + "\n")


class TestPostep(TymczasoweRepo):
    def test_info_liczy_tylko_zmigrowane(self):
        self.ustaw_status("P1", "zmigrowany")
        self.ustaw_status("P4", "zmigrowany")
        wynik = self.sup("info", "--json")
        self.assertEqual(wynik.returncode, 0, wynik.stderr)
        dane = json.loads(wynik.stdout)
        self.assertEqual(dane["procent"], 28.6)
        self.assertEqual(dane["zmigrowane"], ["P1", "P4"])
        self.assertEqual([p["id"] for p in dane["czesciowe"]], ["P2"])
        self.assertEqual(dane["pozostale"], ["P2", "P3", "P5", "P6", "P7"])

    def test_info_tekst_z_przecinkiem(self):
        self.ustaw_status("P1", "zmigrowany")
        wynik = self.sup("info")
        self.assertIn("14,3%", wynik.stdout)
        self.assertIn("Częściowo", wynik.stdout)


class TestGuard(TymczasoweRepo):
    def test_rozpocznij_zgoda_i_raport(self):
        wynik = self.sup("rozpocznij", "P1", "--agent", "test")
        self.assertEqual(wynik.returncode, 0, wynik.stderr)
        self.assertEqual(self.stan()["procesy"]["P1"]["status"], "w-toku")
        self.assertEqual(self.stan()["procesy"]["P1"]["galaz"], "main")
        self.assertTrue(os.path.isfile(os.path.join(self.repo, "new-app-parent/new-docs/NEW-PRZEPLYWY-BIZNESOWE.md")))

    def test_rozpocznij_odmowa_dla_zmigrowanego(self):
        self.ustaw_status("P1", "zmigrowany")
        self.assertEqual(self.sup("rozpocznij", "P1").returncode, 3)

    def test_rozpocznij_odmowa_na_innej_galezi(self):
        self.ustaw_status("P3", "w-toku", galaz="inna", agent="ktos")
        self.assertEqual(self.sup("rozpocznij", "P3").returncode, 3)
        self.assertEqual(self.sup("rozpocznij", "P3", "--galaz", "inna").returncode, 0)

    def test_rozpocznij_czesciowy_wymaga_dokonczenia(self):
        self.assertEqual(self.sup("rozpocznij", "P2").returncode, 3)
        self.assertEqual(self.sup("rozpocznij", "P2", "--dokonczenie").returncode, 0)
        self.assertEqual(self.stan()["procesy"]["P2"]["poprzedni_status"], "czesciowy")

    def test_zakoncz_bez_rozpocznij_odmowa(self):
        self.zapisz(P1_DOKUMENT, "# P1\n")
        self.assertEqual(self.sup("zakoncz", "P1", "--status", "zmigrowany", "--dokument", P1_DOKUMENT).returncode, 3)

    def test_zakoncz_bez_dokumentu_odmowa(self):
        self.sup("rozpocznij", "P1")
        self.assertEqual(self.sup("zakoncz", "P1", "--status", "zmigrowany", "--dokument", P1_DOKUMENT).returncode, 3)

    def test_zakoncz_czesciowy_wymaga_brakow(self):
        self.zapisz(P1_DOKUMENT, "# P1\n")
        self.sup("rozpocznij", "P1")
        self.assertEqual(self.sup("zakoncz", "P1", "--status", "czesciowy", "--dokument", P1_DOKUMENT).returncode, 2)
        self.assertEqual(self.sup("zakoncz", "P1", "--status", "czesciowy", "--dokument", P1_DOKUMENT,
                                  "--braki", "a; b").returncode, 0)
        self.assertEqual(self.stan()["procesy"]["P1"]["braki"], ["a", "b"])

    def test_porzuc_przywraca_status(self):
        self.sup("rozpocznij", "P2", "--dokonczenie")
        self.assertEqual(self.sup("porzuc", "P2").returncode, 0)
        self.assertEqual(self.stan()["procesy"]["P2"]["status"], "czesciowy")

    def test_odnotuj_nierozpoczety_odmowa(self):
        self.assertEqual(self.sup("odnotuj", "P5", "--opis", "x").returncode, 3)
        self.assertEqual(self.sup("odnotuj", "P2", "--opis", "cena = wycena").returncode, 0)

    def test_odnotuj_aktualizuje_braki_czesciowego(self):
        wynik = self.sup("odnotuj", "P2", "--opis", "cena = wycena", "--braki", "anulowanie; nowy brak")
        self.assertEqual(wynik.returncode, 0, wynik.stderr)
        p2 = self.stan()["procesy"]["P2"]
        self.assertEqual(p2["braki"], ["anulowanie", "nowy brak"])
        self.assertEqual(self.sup("odnotuj", "P2", "--opis", "x", "--braki", " ; ").returncode, 2)
        self.ustaw_status("P1", "zmigrowany")
        self.assertEqual(self.sup("odnotuj", "P1", "--opis", "x", "--braki", "a").returncode, 2)

    def test_zakoncz_porzuc_tylko_z_galezi_zgody(self):
        self.zapisz(P1_DOKUMENT, "# P1\n")
        self.sup("rozpocznij", "P1", "--galaz", "inna")
        self.assertEqual(self.sup("zakoncz", "P1", "--status", "zmigrowany", "--dokument", P1_DOKUMENT).returncode, 3)
        self.assertEqual(self.sup("porzuc", "P1").returncode, 3)
        self.assertEqual(self.sup("odnotuj", "P1", "--opis", "x").returncode, 3)
        self.assertEqual(self.stan()["procesy"]["P1"]["status"], "w-toku")


class TestKlasyfikacja(TymczasoweRepo):
    def test_klasyfikuj(self):
        self.zapisz(P1_PLIK, "class A {}\n")
        self.zapisz(WSPOLNY_PLIK, "class B {}\n")
        self.zapisz("new-app-parent/new-app/frontend/src/pages/ZwrotPage.tsx", "x\n")
        self.zapisz("new-app-parent/new-app/backend/src/Wypozyczalnia.Api/bin/Debug/PricingService.dll", "x\n")
        self.zapisz("docs/notatka.md", "x\n")
        dane = json.loads(self.sup("klasyfikuj", "--json").stdout)
        self.assertEqual(dane["procesy"]["P1"], [P1_PLIK])
        self.assertEqual(dane["procesy"]["P4"], ["new-app-parent/new-app/frontend/src/pages/ZwrotPage.tsx"])
        self.assertEqual([w["plik"] for w in dane["wspolny"]], [WSPOLNY_PLIK])
        self.assertIn("docs/notatka.md", dane["poza-procesem"])
        self.assertIn("new-app-parent/new-app/backend/src/Wypozyczalnia.Api/bin/Debug/PricingService.dll", dane["poza-procesem"])

    def test_klasyfikuj_staged(self):
        self.zapisz(P1_PLIK, "class A {}\n")
        self.zapisz("new-app-parent/new-app/frontend/src/pages/ZwrotPage.tsx", "x\n")
        self.git("add", P1_PLIK)
        dane = json.loads(self.sup("klasyfikuj", "--staged", "--json").stdout)
        self.assertEqual(list(dane["procesy"]), ["P1"])

    def test_weryfikuj_wykrywa_kod_bez_stanu(self):
        self.zapisz("new-app-parent/new-app/backend/src/Wypozyczalnia.Api/Controllers/PricingController.cs", "x\n")
        wynik = self.sup("weryfikuj", "--json")
        self.assertEqual(wynik.returncode, 1)
        self.assertTrue(any(b.startswith("P1:") for b in json.loads(wynik.stdout)["bledy"]))


class TestHookPreCommit(TymczasoweRepo):
    def test_blokada_bez_stanu(self):
        self.zapisz(P1_PLIK, "class A {}\n")
        self.git("add", P1_PLIK)
        wynik = self.sup("hook-pre-commit")
        self.assertEqual(wynik.returncode, 1)
        self.assertIn("P1", wynik.stderr)

    def test_blokada_gdy_proces_w_toku(self):
        self.sup("rozpocznij", "P1")
        self.zapisz(P1_PLIK, "class A {}\n")
        self.git("add", P1_PLIK, STAN)
        self.assertEqual(self.sup("hook-pre-commit").returncode, 1)

    def test_przepuszcza_po_zakoncz_z_dokumentem(self):
        self.sup("rozpocznij", "P1")
        self.zapisz(P1_DOKUMENT, "# P1\n")
        self.assertEqual(self.sup("zakoncz", "P1", "--status", "zmigrowany", "--dokument", P1_DOKUMENT).returncode, 0)
        self.zapisz(P1_PLIK, "class A {}\n")
        self.git("add", P1_PLIK, STAN)
        self.assertEqual(self.sup("hook-pre-commit").returncode, 1, "dokument poza indeksem musi blokować")
        self.git("add", P1_DOKUMENT)
        wynik = self.sup("hook-pre-commit")
        self.assertEqual(wynik.returncode, 0, wynik.stderr)

    def test_odnotuj_dla_procesu_czesciowego(self):
        plik_p2 = "new-app-parent/new-app/backend/src/Wypozyczalnia.Api/Services/ReservationService.cs"
        self.zapisz(plik_p2, "class R {}\n")
        self.git("add", plik_p2)
        self.assertEqual(self.sup("hook-pre-commit").returncode, 1)
        self.sup("odnotuj", "P2", "--opis", "cena = wycena")
        self.git("add", STAN)
        wynik = self.sup("hook-pre-commit")
        self.assertEqual(wynik.returncode, 0, wynik.stderr)

    def test_tylko_wspolny_przepuszcza(self):
        self.zapisz(WSPOLNY_PLIK, "class B {}\n")
        self.git("add", WSPOLNY_PLIK)
        wynik = self.sup("hook-pre-commit")
        self.assertEqual(wynik.returncode, 0)
        self.assertIn("ostrzeżenie", wynik.stdout)

    def test_stary_wpis_nie_wystarcza(self):
        self.sup("rozpocznij", "P1")
        self.zapisz(P1_DOKUMENT, "# P1\n")
        self.sup("zakoncz", "P1", "--status", "zmigrowany", "--dokument", P1_DOKUMENT)
        self.zapisz(P1_PLIK, "class A {}\n")
        self.git("add", "-A")
        self.git("commit", "-q", "-m", "P1")
        self.zapisz(P1_PLIK, "class A { int x; }\n")
        self.git("add", P1_PLIK)
        self.assertEqual(self.sup("hook-pre-commit").returncode, 1)


@unittest.skipIf(znajdz_sh() is None, "brak sh (Git for Windows)")
class TestHookiPowloki(TymczasoweRepo):
    def setUp(self):
        super().setUp()
        shutil.copytree(os.path.join(REPO_ZRODLOWE, ".githooks"), os.path.join(self.repo, ".githooks"))
        shutil.copytree(SKILL, os.path.join(self.repo, ".agents", "skills", "migration-supervisor"),
                        ignore=shutil.ignore_patterns("__pycache__"))
        self.globalne = os.path.join(self.katalog, "globalne-hooki")
        os.makedirs(self.globalne)
        self.znacznik = os.path.join(self.katalog, "znacznik").replace("\\", "/")
        with open(os.path.join(self.globalne, "pre-push"), "w", encoding="utf-8", newline="\n") as plik:
            plik.write("#!/bin/sh\necho \"$1 $2\" > \"%s\"\nexit 1\n" % self.znacznik)
        with open(self.gitconfig, "a", encoding="utf-8") as plik:
            plik.write("[core]\n\thooksPath = %s\n" % self.globalne.replace("\\", "/"))
        self.git("config", "--local", "core.hooksPath", ".githooks")

    def test_pre_push_przekazuje_do_globalnego(self):
        wynik = subprocess.run([znajdz_sh(), ".githooks/pre-push", "origin", "https://example.invalid/repo.git"],
                               cwd=self.repo, env=self.env, capture_output=True, text=True)
        self.assertEqual(wynik.returncode, 1, "kod wyjścia globalnego hooka musi przejść dalej")
        with open(self.znacznik, encoding="utf-8") as plik:
            self.assertIn("origin https://example.invalid/repo.git", plik.read())

    def test_pre_push_blokuje_gdy_globalny_katalog_nie_istnieje(self):
        with open(self.gitconfig, "a", encoding="utf-8") as plik:
            plik.write("[core]\n\thooksPath = %s\n" % os.path.join(self.katalog, "nie-ma").replace("\\", "/"))
        wynik = subprocess.run([znajdz_sh(), ".githooks/pre-push", "origin", "x"],
                               cwd=self.repo, env=self.env, capture_output=True, text=True, encoding="utf-8", errors="replace")
        self.assertEqual(wynik.returncode, 1)
        self.assertIn("nie istnieje", wynik.stderr)

    def test_zainstaluj_hooki_ustawia_lokalny_hooks_path(self):
        self.git("config", "--local", "--unset", "core.hooksPath")
        wynik = self.sup("zainstaluj-hooki")
        self.assertEqual(wynik.returncode, 0, wynik.stderr)
        self.assertEqual(self.git("config", "--local", "--get", "core.hooksPath").stdout.strip(), ".githooks")

    def test_commit_zablokowany_i_przepuszczony(self):
        self.zapisz(P1_PLIK, "class A {}\n")
        self.git("add", P1_PLIK)
        wynik = self.git("commit", "-q", "-m", "P1 bez stanu", sprawdz=False)
        self.assertNotEqual(wynik.returncode, 0)
        self.assertIn("COMMIT ZABLOKOWANY", wynik.stderr)
        self.sup("rozpocznij", "P1")
        self.zapisz(P1_DOKUMENT, "# P1\n")
        self.sup("zakoncz", "P1", "--status", "zmigrowany", "--dokument", P1_DOKUMENT)
        self.git("add", "-A")
        wynik = self.git("commit", "-q", "-m", "P1 ze stanem", sprawdz=False)
        self.assertEqual(wynik.returncode, 0, wynik.stderr)


if __name__ == "__main__":
    unittest.main()
