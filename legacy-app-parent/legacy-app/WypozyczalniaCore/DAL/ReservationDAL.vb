' Modul: DAL - dostep do tabeli Rezerwacje | Autor: zespol IT | 2014-03-11
' Historia:
'   2014-09-22 JK  zapis przez procedure usp_ZapiszRezerwacje (wpis do dziennika)
'   2016-11-08 PT  kolizje liczone w bazie (usp_SprawdzDostepnosc) zamiast petli w VB

Imports System.Data
Imports System.Data.SqlClient
Imports Wypozyczalnia.Core.Model

Namespace DAL

    Public Class ReservationDAL

        Private Const KolumnyRezerwacji As String = "r.Id, r.KlientId, r.SprzetId, r.DataOd, r.DataDo, r.Status, r.CenaSzacunkowa, r.Uwagi, r.Utworzono"

        ' wynik: Zrodlo, Id, DataOd, DataDo - kazdy wiersz oznacza kolizje
        Public Function PobierzKolizje(ByVal sprzetId As Integer, ByVal dataOd As Date, ByVal dataDo As Date) As DataTable
            Return DbHelper.WykonajProcedure("usp_SprawdzDostepnosc", _
                                             DbHelper.Parametr("@SprzetId", sprzetId), _
                                             DbHelper.Parametr("@DataOd", dataOd), _
                                             DbHelper.Parametr("@DataDo", dataDo))
        End Function

        Public Function Zapisz(ByVal r As Rezerwacja) As Integer
            Dim noweId As SqlParameter = DbHelper.ParametrWyjsciowy("@NoweId", SqlDbType.Int)
            Dim uwagi As Object = Nothing
            If Not String.IsNullOrEmpty(r.Uwagi) Then
                uwagi = r.Uwagi
            End If
            DbHelper.WykonajProcedureBezWyniku("usp_ZapiszRezerwacje", _
                                               DbHelper.Parametr("@KlientId", r.KlientId), _
                                               DbHelper.Parametr("@SprzetId", r.SprzetId), _
                                               DbHelper.Parametr("@DataOd", r.DataOd), _
                                               DbHelper.Parametr("@DataDo", r.DataDo), _
                                               DbHelper.Parametr("@CenaSzacunkowa", r.CenaSzacunkowa), _
                                               DbHelper.Parametr("@Uwagi", uwagi), _
                                               noweId)
            If noweId.Value Is Nothing OrElse IsDBNull(noweId.Value) Then
                Throw New ApplicationException("Procedura usp_ZapiszRezerwacje nie zwrocila Id")
            End If
            Return CInt(noweId.Value)
        End Function

        Public Function Pobierz(ByVal rezerwacjaId As Integer) As DataRow
            Dim sql As String = "SELECT " & KolumnyRezerwacji & " FROM Rezerwacje r WHERE r.Id = @Id"
            Dim tabela As DataTable = DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@Id", rezerwacjaId))
            If tabela.Rows.Count = 0 Then
                Return Nothing
            End If
            Return tabela.Rows(0)
        End Function

        Public Sub ZmienStatus(ByVal rezerwacjaId As Integer, ByVal status As String)
            DbHelper.WykonajProcedureBezWyniku("usp_ZmienStatusRezerwacji", _
                                               DbHelper.Parametr("@RezerwacjaId", rezerwacjaId), _
                                               DbHelper.Parametr("@Status", status))
        End Sub

        ' kolumny: Id, SprzetId, Sprzet, DataOd, DataDo, Status, CenaSzacunkowa
        Public Function RezerwacjeKlienta(ByVal klientId As Integer) As DataTable
            Dim sql As String = "SELECT r.Id, r.SprzetId, s.Nazwa AS Sprzet, r.DataOd, r.DataDo, r.Status, r.CenaSzacunkowa " & _
                                "FROM Rezerwacje r INNER JOIN Sprzet s ON s.Id = r.SprzetId " & _
                                "WHERE r.KlientId = @KlientId " & _
                                "ORDER BY r.DataOd DESC"
            Return DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@KlientId", klientId))
        End Function

        Public Function RezerwacjeSprzetu(ByVal sprzetId As Integer, ByVal odDnia As Date) As DataTable
            Dim sql As String = "SELECT r.Id, r.KlientId, k.Nazwisko AS Klient, r.DataOd, r.DataDo, r.Status " & _
                                "FROM Rezerwacje r INNER JOIN Klienci k ON k.Id = r.KlientId " & _
                                "WHERE r.SprzetId = @SprzetId AND r.DataDo >= @OdDnia AND r.Status IN ('N', 'P') " & _
                                "ORDER BY r.DataOd"
            Return DbHelper.WykonajZapytanie(sql, _
                                             DbHelper.Parametr("@SprzetId", sprzetId), _
                                             DbHelper.Parametr("@OdDnia", odDnia))
        End Function

        Public Function RezerwacjeWOkresie(ByVal dataOd As Date, ByVal dataDo As Date) As DataTable
            Dim sql As String = "SELECT r.Id, r.KlientId, r.SprzetId, s.Nazwa AS Sprzet, r.DataOd, r.DataDo, r.Status, r.CenaSzacunkowa " & _
                                "FROM Rezerwacje r INNER JOIN Sprzet s ON s.Id = r.SprzetId " & _
                                "WHERE r.DataOd < @DataDo AND r.DataDo > @DataOd " & _
                                "ORDER BY r.DataOd, s.NumerInwentarzowy"
            Return DbHelper.WykonajZapytanie(sql, _
                                             DbHelper.Parametr("@DataOd", dataOd), _
                                             DbHelper.Parametr("@DataDo", dataDo))
        End Function

        Public Function LiczbaAktywnychKlienta(ByVal klientId As Integer) As Integer
            Dim sql As String = "SELECT COUNT(*) AS Ile FROM Rezerwacje WHERE KlientId = @KlientId AND Status IN ('N', 'P')"
            Dim tabela As DataTable = DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@KlientId", klientId))
            Return CInt(tabela.Rows(0)("Ile"))
        End Function

        Public Function PrzeterminowaneNowe(ByVal naDzien As Date) As DataTable
            Dim sql As String = "SELECT r.Id, r.KlientId, r.SprzetId, r.DataOd, r.DataDo, r.Status " & _
                                "FROM Rezerwacje r " & _
                                "WHERE r.Status = 'N' AND r.DataOd < @NaDzien " & _
                                "ORDER BY r.DataOd"
            Return DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@NaDzien", naDzien))
        End Function

        ' jak PobierzKolizje, ale bez wierszy samej rezerwacji (zmiana terminu istniejacej)
        Public Function PobierzKolizjeBezRezerwacji(ByVal sprzetId As Integer, ByVal dataOd As Date, ByVal dataDo As Date, ByVal pominRezerwacjaId As Integer) As DataTable
            Dim kolizje As DataTable = PobierzKolizje(sprzetId, dataOd, dataDo)
            For i As Integer = kolizje.Rows.Count - 1 To 0 Step -1
                Dim wiersz As DataRow = kolizje.Rows(i)
                If CStr(wiersz("Zrodlo")) = "R" AndAlso CInt(wiersz("Id")) = pominRezerwacjaId Then
                    kolizje.Rows.RemoveAt(i)
                End If
            Next
            Return kolizje
        End Function

        Public Sub ZmienTermin(ByVal rezerwacjaId As Integer, ByVal dataOd As Date, ByVal dataDo As Date, ByVal cenaSzacunkowa As Decimal)
            Dim sql As String = "UPDATE Rezerwacje SET DataOd = @DataOd, DataDo = @DataDo, CenaSzacunkowa = @Cena WHERE Id = @Id"
            DbHelper.WykonajPolecenie(sql, _
                                      DbHelper.Parametr("@DataOd", dataOd), _
                                      DbHelper.Parametr("@DataDo", dataDo), _
                                      DbHelper.Parametr("@Cena", cenaSzacunkowa), _
                                      DbHelper.Parametr("@Id", rezerwacjaId))
        End Sub

        ' kolumny: Id, KlientId, Klient, SprzetId, Sprzet, DataOd, DataDo, Status
        Public Function RezerwacjeNaDzien(ByVal dzien As Date) As DataTable
            Dim sql As String = "SELECT r.Id, r.KlientId, k.Nazwisko AS Klient, r.SprzetId, s.Nazwa AS Sprzet, r.DataOd, r.DataDo, r.Status " & _
                                "FROM Rezerwacje r " & _
                                "INNER JOIN Klienci k ON k.Id = r.KlientId " & _
                                "INNER JOIN Sprzet s ON s.Id = r.SprzetId " & _
                                "WHERE r.DataOd >= @Od AND r.DataOd < @Do AND r.Status IN ('N', 'P') " & _
                                "ORDER BY r.DataOd"
            Return DbHelper.WykonajZapytanie(sql, _
                                             DbHelper.Parametr("@Od", dzien.Date), _
                                             DbHelper.Parametr("@Do", dzien.Date.AddDays(1)))
        End Function

        ' kolumny: Status, Liczba, Suma
        Public Function Statystyki(ByVal dataOd As Date, ByVal dataDo As Date) As DataTable
            Dim sql As String = "SELECT Status, COUNT(*) AS Liczba, SUM(CenaSzacunkowa) AS Suma " & _
                                "FROM Rezerwacje " & _
                                "WHERE Utworzono >= @Od AND Utworzono < @Do " & _
                                "GROUP BY Status ORDER BY Status"
            Return DbHelper.WykonajZapytanie(sql, _
                                             DbHelper.Parametr("@Od", dataOd), _
                                             DbHelper.Parametr("@Do", dataDo))
        End Function

        ' kolumny: Id, DataOd, DataDo, Status (rezerwacje sprzetu zachodzace na miesiac)
        Public Function KalendarzSprzetu(ByVal sprzetId As Integer, ByVal rok As Integer, ByVal miesiac As Integer) As DataTable
            Dim poczatek As New Date(rok, miesiac, 1)
            Dim koniec As Date = poczatek.AddMonths(1)
            Dim sql As String = "SELECT r.Id, r.DataOd, r.DataDo, r.Status " & _
                                "FROM Rezerwacje r " & _
                                "WHERE r.SprzetId = @SprzetId AND r.Status IN ('N', 'P', 'Z') " & _
                                "AND r.DataOd < @Koniec AND r.DataDo > @Poczatek " & _
                                "ORDER BY r.DataOd"
            Return DbHelper.WykonajZapytanie(sql, _
                                             DbHelper.Parametr("@SprzetId", sprzetId), _
                                             DbHelper.Parametr("@Poczatek", poczatek), _
                                             DbHelper.Parametr("@Koniec", koniec))
        End Function

        ' kolumny: Id, KlientId, Klient, Sprzet, DataOd, DataDo, CenaSzacunkowa
        Public Function DoPotwierdzenia() As DataTable
            Dim sql As String = "SELECT r.Id, r.KlientId, k.Nazwisko AS Klient, s.Nazwa AS Sprzet, r.DataOd, r.DataDo, r.CenaSzacunkowa " & _
                                "FROM Rezerwacje r " & _
                                "INNER JOIN Klienci k ON k.Id = r.KlientId " & _
                                "INNER JOIN Sprzet s ON s.Id = r.SprzetId " & _
                                "WHERE r.Status = 'N' AND r.DataOd >= GETDATE() " & _
                                "ORDER BY r.DataOd"
            Return DbHelper.WykonajZapytanie(sql)
        End Function

        Public Sub ZapiszUwagi(ByVal rezerwacjaId As Integer, ByVal uwagi As String)
            Dim sql As String = "UPDATE Rezerwacje SET Uwagi = @Uwagi WHERE Id = @Id"
            DbHelper.WykonajPolecenie(sql, _
                                      DbHelper.Parametr("@Uwagi", uwagi), _
                                      DbHelper.Parametr("@Id", rezerwacjaId))
        End Sub

        Public Sub DopiszDoDziennika(ByVal operacja As String, ByVal szczegoly As String)
            Dim sql As String = "INSERT INTO DziennikOperacji (Operacja, Szczegoly) VALUES (@Operacja, @Szczegoly)"
            If szczegoly IsNot Nothing AndAlso szczegoly.Length > 400 Then
                szczegoly = szczegoly.Substring(0, 400)
            End If
            DbHelper.WykonajPolecenie(sql, _
                                      DbHelper.Parametr("@Operacja", operacja), _
                                      DbHelper.Parametr("@Szczegoly", szczegoly))
        End Sub

    End Class

End Namespace
