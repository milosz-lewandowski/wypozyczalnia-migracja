# Definicja ukończenia (DoD) zmiany w nowej aplikacji

Zmiana (mały feature albo proces przeniesiony z legacy) jest gotowa, gdy:

1. Zachowanie zgadza się z dotychczasowym systemem: kwoty, progi, zaokrąglenia i komunikaty jak w legacy.
2. Warstwy: kontroler → serwis → repozytorium; SQL wyłącznie w repozytorium; na granicy API obiekty DTO.
3. Nazwy klas odpowiadają roli (`...Controller`, `...Service`, `...Repository`), DTO są w folderze `Dto` (przestrzeń nazw `Wypozyczalnia.Dto`).
4. Kwoty wyłącznie typem kwoty (`Money`), nigdy liczbą zmiennoprzecinkową; zaokrąglenie bankierskie.
5. Backend i frontend kompilują się bez błędów typów.
6. Każdy nowy serwis ma testy.
7. Błędy API mają ten sam format co w istniejących modułach.
8. Bezpieczeństwo: wejście walidowane, bez sklejania SQL, bez renderowania HTML z danych, dane osobowe tylko dla uprawnionych ról.
9. Zakres: zmiana dotyczy tylko zadania; kodu obok nie poprawiamy przy okazji.
10. Niejednoznaczna reguła biznesowa (kod mówi co innego niż komentarz albo dwa miejsca liczą inaczej) wymaga decyzji właściciela procesu, nie autora zmiany.
11. Opis PR: co, dlaczego, jak sprawdzone; wpis w changelogu.
12. Bramka jakości (`new-docs/BRAMKA.md`) jest zielona.
