# Pomiar CLOC

## `tools\cloc-2.10.exe legacy-app-parent/legacy-app`

```text
github.com/AlDanial/cloc v 2.10  T=0.99 s (52.7 files/s, 7810.3 lines/s)
--------------------------------------------------------------------------------------
Language                            files          blank        comment           code
--------------------------------------------------------------------------------------
Visual Basic .NET                      35            660            234           5125
ASP.NET                                 8              4              0            616
SQL                                     3             97             89            563
XML                                     3              7              4            144
Web Services Description                1              0              0             92
Text                                    1              5              0             36
Visual Studio Solution                  1              1              1             35
--------------------------------------------------------------------------------------
SUM:                                   52            774            328           6611
--------------------------------------------------------------------------------------
```

## `tools\cloc-2.10.exe --by-file legacy-app-parent/legacy-app`

```text
github.com/AlDanial/cloc v 2.10  T=1.00 s (52.2 files/s, 7746.4 lines/s)
----------------------------------------------------------------------------------------------------------------------------------------------------
File                                                                                                             blank        comment           code
----------------------------------------------------------------------------------------------------------------------------------------------------
legacy-app-parent\legacy-app\WypozyczalniaCore\RentalManager.vb                                                     38              5            289
legacy-app-parent\legacy-app\WypozyczalniaWeb\Rezerwacja.aspx.vb                                                    26              6            289
legacy-app-parent\legacy-app\Database\02_procedury.sql                                                              59             38            281
legacy-app-parent\legacy-app\WypozyczalniaCore\PricingManager.vb                                                    29              6            273
legacy-app-parent\legacy-app\WypozyczalniaCore\ReservationManager.vb                                                32             12            255
legacy-app-parent\legacy-app\WypozyczalniaWeb\Zwrot.aspx.vb                                                         26              5            250
legacy-app-parent\legacy-app\WypozyczalniaWeb\Faktura.aspx.vb                                                       25              5            245
legacy-app-parent\legacy-app\WypozyczalniaWeb\Klienci.aspx.vb                                                       20              4            227
legacy-app-parent\legacy-app\WypozyczalniaWeb\Wypozyczenie.aspx.vb                                                  20              4            219
legacy-app-parent\legacy-app\WypozyczalniaCore\InvoiceManager.vb                                                    33              9            207
legacy-app-parent\legacy-app\WypozyczalniaWeb\Cennik.aspx.vb                                                        18              5            200
legacy-app-parent\legacy-app\WypozyczalniaWeb\Sprzet.aspx.vb                                                        21              4            184
legacy-app-parent\legacy-app\WypozyczalniaCore\EquipmentManager.vb                                                  23             10            183
legacy-app-parent\legacy-app\WypozyczalniaCore\CustomerManager.vb                                                   20              4            172
legacy-app-parent\legacy-app\WypozyczalniaCore\DAL\RentalDAL.vb                                                     26              5            169
legacy-app-parent\legacy-app\Database\03_seed.sql                                                                   24             25            167
legacy-app-parent\legacy-app\WypozyczalniaCore\DAL\ReservationDAL.vb                                                23             11            158
legacy-app-parent\legacy-app\WypozyczalniaWeb\WypozyczalniaWeb.vbproj                                                0              0            148
legacy-app-parent\legacy-app\WypozyczalniaCore\DAL\EquipmentDAL.vb                                                  20              7            141
legacy-app-parent\legacy-app\WypozyczalniaCore\Integracje\ImportCennikaXml.vb                                       21              4            139
legacy-app-parent\legacy-app\WypozyczalniaCore\DAL\InvoiceDAL.vb                                                    19              8            134
legacy-app-parent\legacy-app\Jobs\EksportNocny\Program.vb                                                           18              6            126
legacy-app-parent\legacy-app\WypozyczalniaCore\DAL\PricingDAL.vb                                                    21              5            126
legacy-app-parent\legacy-app\WypozyczalniaCore\Integracje\KurierClient.vb                                           18              4            126
legacy-app-parent\legacy-app\Database\01_schema.sql                                                                 14             26            115
legacy-app-parent\legacy-app\WypozyczalniaCore\Integracje\EksportCsv.vb                                             18              5            108
legacy-app-parent\legacy-app\WypozyczalniaWeb\Rezerwacja.aspx                                                        0              0            101
legacy-app-parent\legacy-app\WypozyczalniaCore\DAL\DbHelper.vb                                                      18              4             97
legacy-app-parent\legacy-app\WypozyczalniaWeb\Faktura.aspx                                                           0              0             96
legacy-app-parent\legacy-app\WypozyczalniaCore\WypozyczalniaCore.vbproj                                              0              0             95
legacy-app-parent\legacy-app\WypozyczalniaWeb\Web References\BramkaPlatnosci\BramkaPlatnosci.wsdl                    0              0             92
legacy-app-parent\legacy-app\WypozyczalniaWeb\Wypozyczenie.aspx                                                      0              0             92
legacy-app-parent\legacy-app\WypozyczalniaWeb\Web.config                                                             7              4             91
legacy-app-parent\legacy-app\WypozyczalniaWeb\Zwrot.aspx                                                             0              0             87
legacy-app-parent\legacy-app\WypozyczalniaWeb\Web References\BramkaPlatnosci\Reference.vb                           16             22             86
legacy-app-parent\legacy-app\WypozyczalniaWeb\Sprzet.aspx                                                            0              0             84
legacy-app-parent\legacy-app\WypozyczalniaWeb\Klienci.aspx                                                           0              0             81
legacy-app-parent\legacy-app\WypozyczalniaCore\Model\Faktura.vb                                                     16              4             76
legacy-app-parent\legacy-app\WypozyczalniaWeb\Cennik.aspx                                                            4              0             74
legacy-app-parent\legacy-app\WypozyczalniaCore\Model\Wypozyczenie.vb                                                13              4             66
legacy-app-parent\legacy-app\Jobs\EksportNocny\EksportNocny.vbproj                                                   0              0             57
legacy-app-parent\legacy-app\WypozyczalniaCore\Model\Taryfa.vb                                                      15              4             56
legacy-app-parent\legacy-app\WypozyczalniaCore\Model\Sprzet.vb                                                      11              5             53
legacy-app-parent\legacy-app\WypozyczalniaCore\Model\Rezerwacja.vb                                                  11              5             51
legacy-app-parent\legacy-app\WypozyczalniaWeb\Global.asax.vb                                                        10              3             50
legacy-app-parent\legacy-app\WypozyczalniaCore\Model\Klient.vb                                                      14              5             48
legacy-app-parent\legacy-app\Jobs\EksportNocny\zadanie-harmonogramu.xml                                              0              0             44
legacy-app-parent\legacy-app\CZYTAJ.txt                                                                              5              0             36
legacy-app-parent\legacy-app\Wypozyczalnia.sln                                                                       1              1             35
legacy-app-parent\legacy-app\WypozyczalniaWeb\Rezerwacja.aspx.designer.vb                                           21             44             22
legacy-app-parent\legacy-app\WypozyczalniaWeb\packages.config                                                        0              0              9
legacy-app-parent\legacy-app\WypozyczalniaWeb\Global.asax                                                            0              0              1
----------------------------------------------------------------------------------------------------------------------------------------------------
SUM:                                                                                                               774            328           6611
----------------------------------------------------------------------------------------------------------------------------------------------------
```

## Zestawienie per rozszerzenie

Agregacja wyniku `--by-file`. Kategorie są rozłączne: `.designer.vb` i `.aspx.vb` wydzielono przed zwykłymi `.vb`; `.vbproj` ujęto osobno, choć CLOC zalicza je do Visual Basic .NET. Zwykłe `.vb` obejmują także `Global.asax.vb`. Kolumna `code` oznacza klasyfikację CLOC, również dla plików projektu i danych.

| Rozszerzenie | Pliki | Puste (`blank`) | Komentarze (`comment`) | Kod (`code`) |
|---|---:|---:|---:|---:|
| `.asax` | 1 | 0 | 0 | 1 |
| `.aspx` | 7 | 4 | 0 | 615 |
| `.aspx.vb` | 7 | 156 | 33 | 1614 |
| `.config` | 2 | 7 | 4 | 100 |
| `.designer.vb` | 1 | 21 | 44 | 22 |
| `.sln` | 1 | 1 | 1 | 35 |
| `.sql` | 3 | 97 | 89 | 563 |
| `.txt` | 1 | 5 | 0 | 36 |
| `.vb` | 24 | 483 | 157 | 3189 |
| `.vbproj` | 3 | 0 | 0 | 300 |
| `.wsdl` | 1 | 0 | 0 | 92 |
| `.xml` | 1 | 0 | 0 | 44 |
| **Suma** | **52** | **774** | **328** | **6611** |

## Obserwacje liczbowe

1. Pomiar obejmuje 52 pliki i 7713 linii: 6611 kodu, 328 komentarzy oraz 774 puste.
2. Visual Basic .NET według CLOC to 35 plików i 5125 linii kodu: 3189 w zwykłych `.vb`, 1614 w `.aspx.vb`, 22 w `.designer.vb` i 300 w `.vbproj`.
3. Trzy pliki `.vbproj` wnoszą 300 linii kodu; po ich odjęciu pozostałe 32 pliki kategorii Visual Basic .NET mają 4825 linii kodu.
4. Siedem plików `.aspx` ma 615 linii kodu, a jeden `.asax` ma 1 linię; razem odpowiadają 8 plikom i 616 liniom kodu kategorii ASP.NET.
5. Trzy pliki `.sql` mają 563 linie kodu, 89 komentarzy i 97 linii pustych.
6. Największa liczba linii kodu w pojedynczym pliku wynosi 289; taki wynik mają `RentalManager.vb` i `Rezerwacja.aspx.vb`.
