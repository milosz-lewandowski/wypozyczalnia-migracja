' Modul: DAL\RentalDAL.vb | Autor: zespol IT | 2014-03-11
' Dostep do tabeli Wypozyczenia (wydanie i zwrot przez procedury, odczyty inline).
' 2015-06-22 MW: UstawNumerPrzesylki
' 2018-02-09 PT: WypozyczeniaZDnia dla eksportu nocnego

Imports System.Data
Imports System.Data.SqlClient
Imports Wypozyczalnia.Core.Model

Namespace DAL

    Public Class RentalDAL

        Private Const SqlKolumny As String =
            "SELECT w.Id, w.RezerwacjaId, w.KlientId, w.SprzetId, w.DataWydania, w.PlanowanyZwrot, w.DataZwrotu, " &
            "w.KaucjaPobrana, w.Oplata, w.Kara, w.KaucjaZwrocona, w.Status, w.NumerPrzesylki FROM Wypozyczenia w "

        Private Const SqlNazwaKlienta As String =
            "CASE WHEN k.Typ = 'F' AND k.NazwaFirmy IS NOT NULL THEN k.NazwaFirmy ELSE k.Imie + ' ' + k.Nazwisko END"

        Public Function Wydaj(ByVal w As Wypozyczenie) As Integer
            Dim noweId As SqlParameter = DbHelper.ParametrWyjsciowy("@NoweId", SqlDbType.Int)
            Dim rezerwacja As Object = Nothing
            If w.RezerwacjaId > 0 Then rezerwacja = w.RezerwacjaId
            DbHelper.WykonajProcedureBezWyniku("usp_WydajSprzet",
                DbHelper.Parametr("@RezerwacjaId", rezerwacja),
                DbHelper.Parametr("@KlientId", w.KlientId),
                DbHelper.Parametr("@SprzetId", w.SprzetId),
                DbHelper.Parametr("@DataWydania", w.DataWydania),
                DbHelper.Parametr("@PlanowanyZwrot", w.PlanowanyZwrot),
                DbHelper.Parametr("@KaucjaPobrana", w.KaucjaPobrana),
                noweId)
            w.Id = CInt(noweId.Value)
            Return w.Id
        End Function

        Public Sub ZapiszZwrot(ByVal w As Wypozyczenie)
            If Not w.DataZwrotu.HasValue Then
                Throw New ArgumentException("Brak daty zwrotu dla wypozyczenia " & w.Id)
            End If
            DbHelper.WykonajProcedureBezWyniku("usp_ZarejestrujZwrot",
                DbHelper.Parametr("@WypozyczenieId", w.Id),
                DbHelper.Parametr("@DataZwrotu", w.DataZwrotu.Value),
                DbHelper.Parametr("@Oplata", w.Oplata),
                DbHelper.Parametr("@Kara", w.Kara),
                DbHelper.Parametr("@KaucjaZwrocona", w.KaucjaZwrocona))
        End Sub

        Public Function Pobierz(ByVal wypozyczenieId As Integer) As DataRow
            Dim tabela As DataTable = DbHelper.WykonajZapytanie(SqlKolumny & "WHERE w.Id = @Id",
                DbHelper.Parametr("@Id", wypozyczenieId))
            If tabela.Rows.Count = 0 Then Return Nothing
            Return tabela.Rows(0)
        End Function

        Public Function PobierzWypozyczenie(ByVal wypozyczenieId As Integer) As Wypozyczenie
            Dim wiersz As DataRow = Pobierz(wypozyczenieId)
            If wiersz Is Nothing Then Return Nothing
            Return Wypozyczenie.ZWiersza(wiersz)
        End Function

        Public Sub UstawNumerPrzesylki(ByVal wypozyczenieId As Integer, ByVal numerPrzesylki As String)
            DbHelper.WykonajPolecenie("UPDATE Wypozyczenia SET NumerPrzesylki = @Numer WHERE Id = @Id",
                DbHelper.Parametr("@Numer", numerPrzesylki),
                DbHelper.Parametr("@Id", wypozyczenieId))
        End Sub

        Public Function Otwarte() As DataTable
            Dim sql As String =
                "SELECT w.Id, " & SqlNazwaKlienta & " AS Klient, s.NumerInwentarzowy + ' ' + s.Nazwa AS Sprzet, " &
                "w.DataWydania, w.PlanowanyZwrot " &
                "FROM Wypozyczenia w " &
                "INNER JOIN Klienci k ON k.Id = w.KlientId " &
                "INNER JOIN Sprzet s ON s.Id = w.SprzetId " &
                "WHERE w.Status = 'O' " &
                "ORDER BY w.PlanowanyZwrot"
            Return DbHelper.WykonajZapytanie(sql)
        End Function

        Public Function WypozyczeniaZDnia(ByVal dzien As Date) As DataTable
            Dim od As Date = dzien.Date
            Dim doDnia As Date = od.AddDays(1)
            Dim sql As String =
                "SELECT w.Id, w.KlientId, " & SqlNazwaKlienta & " AS Klient, w.SprzetId, s.Nazwa AS Sprzet, " &
                "w.DataWydania, w.PlanowanyZwrot, w.DataZwrotu, w.Oplata, w.Kara " &
                "FROM Wypozyczenia w " &
                "INNER JOIN Klienci k ON k.Id = w.KlientId " &
                "INNER JOIN Sprzet s ON s.Id = w.SprzetId " &
                "WHERE (w.DataWydania >= @Od AND w.DataWydania < @Do) " &
                "   OR (w.DataZwrotu >= @Od AND w.DataZwrotu < @Do) " &
                "ORDER BY w.Id"
            Return DbHelper.WykonajZapytanie(sql,
                DbHelper.Parametr("@Od", od),
                DbHelper.Parametr("@Do", doDnia))
        End Function

        Public Function WypozyczeniaKlienta(ByVal klientId As Integer) As DataTable
            Dim sql As String =
                "SELECT w.Id, s.Nazwa AS Sprzet, w.DataWydania, w.PlanowanyZwrot, w.DataZwrotu, w.Status " &
                "FROM Wypozyczenia w " &
                "INNER JOIN Sprzet s ON s.Id = w.SprzetId " &
                "WHERE w.KlientId = @KlientId " &
                "ORDER BY w.DataWydania DESC"
            Return DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@KlientId", klientId))
        End Function

        Public Function PoTerminie(ByVal naDzien As Date) As DataTable
            Dim sql As String =
                "SELECT w.Id, w.KlientId, w.SprzetId, w.PlanowanyZwrot, " &
                "DATEDIFF(MINUTE, w.PlanowanyZwrot, @NaDzien) AS MinutySpoznienia " &
                "FROM Wypozyczenia w " &
                "WHERE w.Status = 'O' AND w.PlanowanyZwrot < @NaDzien " &
                "ORDER BY w.PlanowanyZwrot"
            Return DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@NaDzien", naDzien))
        End Function

        Public Sub ZmienPlanowanyZwrot(ByVal wypozyczenieId As Integer, ByVal nowyTermin As Date)
            Dim zmienione As Integer = DbHelper.WykonajPolecenie(
                "UPDATE Wypozyczenia SET PlanowanyZwrot = @Termin WHERE Id = @Id AND Status = 'O'",
                DbHelper.Parametr("@Termin", nowyTermin),
                DbHelper.Parametr("@Id", wypozyczenieId))
            If zmienione = 0 Then
                Throw New BladBiznesowyException("Nie zmieniono terminu wypozyczenia " & wypozyczenieId)
            End If
        End Sub

        Public Function DoZwrotuWOkresie(ByVal odDaty As Date, ByVal doDaty As Date) As List(Of Wypozyczenie)
            Dim tabela As DataTable = DbHelper.WykonajZapytanie(
                SqlKolumny & "WHERE w.Status = 'O' AND w.PlanowanyZwrot >= @Od AND w.PlanowanyZwrot < @Do ORDER BY w.PlanowanyZwrot",
                DbHelper.Parametr("@Od", odDaty),
                DbHelper.Parametr("@Do", doDaty))
            Return Mapuj(tabela)
        End Function

        Public Function OtwarteKlienta(ByVal klientId As Integer) As List(Of Wypozyczenie)
            Dim tabela As DataTable = DbHelper.WykonajZapytanie(
                SqlKolumny & "WHERE w.KlientId = @KlientId AND w.Status = 'O' ORDER BY w.DataWydania",
                DbHelper.Parametr("@KlientId", klientId))
            Return Mapuj(tabela)
        End Function

        Public Function OstatnieDlaSprzetu(ByVal sprzetId As Integer, ByVal ile As Integer) As List(Of Wypozyczenie)
            Dim tabela As DataTable = DbHelper.WykonajZapytanie(
                "SELECT TOP (@Ile) " & SqlKolumny.Substring(7) & "WHERE w.SprzetId = @SprzetId ORDER BY w.DataWydania DESC",
                DbHelper.Parametr("@Ile", ile),
                DbHelper.Parametr("@SprzetId", sprzetId))
            Return Mapuj(tabela)
        End Function

        Private Function Mapuj(ByVal tabela As DataTable) As List(Of Wypozyczenie)
            Dim lista As New List(Of Wypozyczenie)
            For Each wiersz As DataRow In tabela.Rows
                lista.Add(Wypozyczenie.ZWiersza(wiersz))
            Next
            Return lista
        End Function

        Public Function CzyOtwarteDlaSprzetu(ByVal sprzetId As Integer) As Boolean
            Dim tabela As DataTable = DbHelper.WykonajZapytanie(
                "SELECT COUNT(*) AS Ile FROM Wypozyczenia WHERE SprzetId = @SprzetId AND Status = 'O'",
                DbHelper.Parametr("@SprzetId", sprzetId))
            Return CInt(tabela.Rows(0)("Ile")) > 0
        End Function

        Public Function LiczbaOtwartych() As Integer
            Dim tabela As DataTable = DbHelper.WykonajZapytanie(
                "SELECT COUNT(*) AS Ile FROM Wypozyczenia WHERE Status = 'O'")
            Return CInt(tabela.Rows(0)("Ile"))
        End Function

        Public Function SumaKaucjiOtwartych() As Decimal
            Dim tabela As DataTable = DbHelper.WykonajZapytanie(
                "SELECT ISNULL(SUM(KaucjaPobrana), 0) AS Suma FROM Wypozyczenia WHERE Status = 'O'")
            Return CDec(tabela.Rows(0)("Suma"))
        End Function

        Public Function SumaKarZOkresu(ByVal odDaty As Date, ByVal doDaty As Date) As Decimal
            Dim tabela As DataTable = DbHelper.WykonajZapytanie(
                "SELECT ISNULL(SUM(Kara), 0) AS Suma FROM Wypozyczenia " &
                "WHERE Status = 'Z' AND DataZwrotu >= @Od AND DataZwrotu < @Do",
                DbHelper.Parametr("@Od", odDaty),
                DbHelper.Parametr("@Do", doDaty))
            Return CDec(tabela.Rows(0)("Suma"))
        End Function

        Public Sub ZapiszDziennik(ByVal operacja As String, ByVal szczegoly As String)
            Try
                DbHelper.WykonajPolecenie(
                    "INSERT INTO DziennikOperacji (Kiedy, Operacja, Szczegoly) VALUES (GETDATE(), @Operacja, @Szczegoly)",
                    DbHelper.Parametr("@Operacja", operacja),
                    DbHelper.Parametr("@Szczegoly", szczegoly))
            Catch ex As SqlException
                ' dziennik nie moze blokowac wydania ani zwrotu
                System.Diagnostics.Trace.WriteLine("DziennikOperacji: " & ex.Message)
            End Try
        End Sub

    End Class

End Namespace
