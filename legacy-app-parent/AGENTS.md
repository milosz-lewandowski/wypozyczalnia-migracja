# Praca z aplikacją legacy

- Legacy to źródło procesów biznesowych wypożyczalni: VB.NET WebForms, Core, ADO.NET/T-SQL i zadanie nocne; dane są syntetyczne.
- Zacznij od [09 — mapy nawigacji](./analysis/results/09-NAVIGATION-MAP.md): pytania, odpowiedzialności, procesy P1–P7 i ograniczenia.
- Nie edytuj ani nie kompiluj [źródeł legacy](./legacy-app/). Migracja trafia do nowej aplikacji, analizy do katalogu analysis.
- Twarde wyniki narzędzi: [01 — CLOC](./analysis/results/01-POMIAR-CLOC.md), [04 — pomiar M1–M24](./analysis/results/04-POMIAR-STRUKTURY.md), sekcja 2 [05 — wynik klasyfikatora](./analysis/results/05-KLASYFIKACJA-WYJATKI.md).
- Narzędzia i reguły są w [tools](../tools/): pomiar_struktury_legacy.py, klasyfikuj.py, wzorce.json, cloc-2.10.exe.
- Wzorce i wyjątki W1–W6: [05](./analysis/results/05-KLASYFIKACJA-WYJATKI.md); potwierdzona architektura: [06](./analysis/results/06-ARCHITEKTURA.md), zależności: [07](./analysis/results/07-STRUKTURA-ZALEZNOSCI.md), reguły procesów: [08](./analysis/results/08-PRZEPLYWY-BIZNESOWE.md).
- Nie zakładaj szczelnych warstw: SQL występuje poza DAL (W1), także w UI (W2); szacunek UI różni się od Core (W3), stałe mają kopie (W4).
- Reguły wyprowadzaj z wykonywalnego VB i SQL, nie komentarzy: kara ma mnożnik 1,5 mimo komentarza 100% (W5), `legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:137-143`.
- Każdą tezę o zachowaniu poprzyj dowodem w backtickach: ścieżka od korzenia repo i numer linii lub zakres; przykład: `legacy-app-parent/legacy-app/WypozyczalniaCore/CustomerManager.vb:17-19`.
- M3 nie jest pełnym grafem wywołań; M10/W6 nie dowodzi martwoty metod. Equals może być użyte niejawnie; sprawdź też konsumentów zewnętrznych i refleksję.
- Odróżniaj nieistniejące pliki od istniejących kandydatów bez wykazanego użycia; brak dokumentacji nowej aplikacji nie oznacza braku implementacji.
- Pisz po polsku, używaj względnych linków Markdown, oddzielaj fakt, interpretację i niewiadomą.
- Po zmianach dokumentacji uruchom z korzenia repo `python tools\sprawdz_dowody.py`; popraw własne błędy. Wynik potwierdza istnienie dowodów, nie prawdziwość tez.
