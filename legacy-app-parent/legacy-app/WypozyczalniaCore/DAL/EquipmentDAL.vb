' Modul: DAL - dostep do tabeli Sprzet | Autor: zespol IT | 2014-03-11
' Historia:
'   2015-06-02 MW  kolumna Kaucja
'   2018-01-15 PT  lista bez wycofanych (status X)

Imports System.Data
Imports System.Data.SqlClient
Imports Wypozyczalnia.Core.Model

Namespace DAL

    Public Class EquipmentDAL

        Private Const KolumnySprzetu As String = "Id, NumerInwentarzowy, Nazwa, Kategoria, StawkaDobowa, Kaucja, Status"

        Public Function Pobierz(ByVal sprzetId As Integer) As Sprzet
            Dim sql As String = "SELECT " & KolumnySprzetu & " FROM Sprzet WHERE Id = @Id"
            Dim tabela As DataTable = DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@Id", sprzetId))
            If tabela.Rows.Count = 0 Then
                Return Nothing
            End If
            Return Mapuj(tabela.Rows(0))
        End Function

        Public Function PobierzPoNumerze(ByVal numerInwentarzowy As String) As Sprzet
            Dim sql As String = "SELECT " & KolumnySprzetu & " FROM Sprzet WHERE NumerInwentarzowy = @Numer"
            Dim tabela As DataTable = DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@Numer", numerInwentarzowy))
            If tabela.Rows.Count = 0 Then
                Return Nothing
            End If
            Return Mapuj(tabela.Rows(0))
        End Function

        ' kategoria pusta = wszystkie kategorie; wycofane (X) nie sa pokazywane
        Public Function Lista(ByVal kategoria As String) As List(Of Sprzet)
            Dim wynik As New List(Of Sprzet)()
            Dim tabela As DataTable
            If String.IsNullOrEmpty(kategoria) Then
                tabela = DbHelper.WykonajZapytanie("SELECT " & KolumnySprzetu & " FROM Sprzet WHERE Status <> 'X' ORDER BY Kategoria, NumerInwentarzowy")
            Else
                tabela = DbHelper.WykonajZapytanie("SELECT " & KolumnySprzetu & " FROM Sprzet WHERE Status <> 'X' AND Kategoria = @Kategoria ORDER BY NumerInwentarzowy", _
                                                   DbHelper.Parametr("@Kategoria", kategoria))
            End If
            For Each wiersz As DataRow In tabela.Rows
                wynik.Add(Mapuj(wiersz))
            Next
            Return wynik
        End Function

        Public Function ListaWStatusie(ByVal status As String) As List(Of Sprzet)
            Dim wynik As New List(Of Sprzet)()
            Dim sql As String = "SELECT " & KolumnySprzetu & " FROM Sprzet WHERE Status = @Status ORDER BY NumerInwentarzowy"
            Dim tabela As DataTable = DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@Status", status))
            For Each wiersz As DataRow In tabela.Rows
                wynik.Add(Mapuj(wiersz))
            Next
            Return wynik
        End Function

        Public Function LiczbaWKategoriach() As DataTable
            Dim sql As String = "SELECT Kategoria, COUNT(*) AS Liczba, SUM(CASE WHEN Status = 'D' THEN 1 ELSE 0 END) AS Dostepne " & _
                                "FROM Sprzet WHERE Status <> 'X' GROUP BY Kategoria ORDER BY Kategoria"
            Return DbHelper.WykonajZapytanie(sql)
        End Function

        Public Function CzyNumerZajety(ByVal numerInwentarzowy As String, ByVal pominId As Integer) As Boolean
            Dim sql As String = "SELECT COUNT(*) AS Ile FROM Sprzet WHERE NumerInwentarzowy = @Numer AND Id <> @Id"
            Dim tabela As DataTable = DbHelper.WykonajZapytanie(sql, _
                                                                DbHelper.Parametr("@Numer", numerInwentarzowy), _
                                                                DbHelper.Parametr("@Id", pominId))
            Return CInt(tabela.Rows(0)("Ile")) > 0
        End Function

        Public Function Dodaj(ByVal s As Sprzet) As Integer
            Dim sql As String = "INSERT INTO Sprzet (NumerInwentarzowy, Nazwa, Kategoria, StawkaDobowa, Kaucja, Status) " & _
                                "VALUES (@Numer, @Nazwa, @Kategoria, @Stawka, @Kaucja, @Status); " & _
                                "SELECT CAST(SCOPE_IDENTITY() AS INT) AS NoweId"
            Dim tabela As DataTable = DbHelper.WykonajZapytanie(sql, _
                                                                DbHelper.Parametr("@Numer", s.NumerInwentarzowy), _
                                                                DbHelper.Parametr("@Nazwa", s.Nazwa), _
                                                                DbHelper.Parametr("@Kategoria", s.Kategoria), _
                                                                DbHelper.Parametr("@Stawka", s.StawkaDobowa), _
                                                                DbHelper.Parametr("@Kaucja", s.Kaucja), _
                                                                DbHelper.Parametr("@Status", s.Status))
            Return CInt(tabela.Rows(0)("NoweId"))
        End Function

        Public Function Aktualizuj(ByVal s As Sprzet) As Integer
            Dim sql As String = "UPDATE Sprzet SET NumerInwentarzowy = @Numer, Nazwa = @Nazwa, Kategoria = @Kategoria, " & _
                                "StawkaDobowa = @Stawka, Kaucja = @Kaucja, Status = @Status WHERE Id = @Id"
            Return DbHelper.WykonajPolecenie(sql, _
                                             DbHelper.Parametr("@Numer", s.NumerInwentarzowy), _
                                             DbHelper.Parametr("@Nazwa", s.Nazwa), _
                                             DbHelper.Parametr("@Kategoria", s.Kategoria), _
                                             DbHelper.Parametr("@Stawka", s.StawkaDobowa), _
                                             DbHelper.Parametr("@Kaucja", s.Kaucja), _
                                             DbHelper.Parametr("@Status", s.Status), _
                                             DbHelper.Parametr("@Id", s.Id))
        End Function

        Public Function ZmienStatus(ByVal sprzetId As Integer, ByVal status As String) As Integer
            Dim sql As String = "UPDATE Sprzet SET Status = @Status WHERE Id = @Id"
            Dim ile As Integer = DbHelper.WykonajPolecenie(sql, _
                                                           DbHelper.Parametr("@Status", status), _
                                                           DbHelper.Parametr("@Id", sprzetId))
            DbHelper.WykonajPolecenie("INSERT INTO DziennikOperacji (Operacja, Szczegoly) VALUES ('STATUS SPRZETU', @Opis)", _
                                      DbHelper.Parametr("@Opis", "Sprzet " & sprzetId & " -> " & status))
            Return ile
        End Function

        ' wyszukiwanie po nazwie albo numerze (formularz w katalogu)
        Public Function Szukaj(ByVal fraza As String) As List(Of Sprzet)
            Dim wynik As New List(Of Sprzet)()
            If String.IsNullOrEmpty(fraza) Then
                Return wynik
            End If
            Dim sql As String = "SELECT " & KolumnySprzetu & " FROM Sprzet " & _
                                "WHERE (Nazwa LIKE @Fraza OR NumerInwentarzowy LIKE @Fraza) AND Status <> 'X' " & _
                                "ORDER BY NumerInwentarzowy"
            Dim tabela As DataTable = DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@Fraza", "%" & fraza.Trim() & "%"))
            For Each wiersz As DataRow In tabela.Rows
                wynik.Add(Mapuj(wiersz))
            Next
            Return wynik
        End Function

        ' kolumny: Id, Klient, DataWydania, PlanowanyZwrot, DataZwrotu, Status
        Public Function HistoriaWypozyczen(ByVal sprzetId As Integer) As DataTable
            Dim sql As String = "SELECT w.Id, k.Nazwisko AS Klient, w.DataWydania, w.PlanowanyZwrot, w.DataZwrotu, w.Status " & _
                                "FROM Wypozyczenia w INNER JOIN Klienci k ON k.Id = w.KlientId " & _
                                "WHERE w.SprzetId = @SprzetId " & _
                                "ORDER BY w.DataWydania DESC"
            Return DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@SprzetId", sprzetId))
        End Function

        Public Function ZmienStawke(ByVal sprzetId As Integer, ByVal stawkaDobowa As Decimal, ByVal kaucja As Decimal) As Integer
            Dim sql As String = "UPDATE Sprzet SET StawkaDobowa = @Stawka, Kaucja = @Kaucja WHERE Id = @Id"
            Return DbHelper.WykonajPolecenie(sql, _
                                             DbHelper.Parametr("@Stawka", stawkaDobowa), _
                                             DbHelper.Parametr("@Kaucja", kaucja), _
                                             DbHelper.Parametr("@Id", sprzetId))
        End Function

        Public Function MaOtwarteWypozyczenie(ByVal sprzetId As Integer) As Boolean
            Dim sql As String = "SELECT COUNT(*) AS Ile FROM Wypozyczenia WHERE SprzetId = @SprzetId AND Status = 'O'"
            Dim tabela As DataTable = DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@SprzetId", sprzetId))
            Return CInt(tabela.Rows(0)("Ile")) > 0
        End Function

        Private Function Mapuj(ByVal wiersz As DataRow) As Sprzet
            Dim s As New Sprzet()
            s.Id = CInt(wiersz("Id"))
            s.NumerInwentarzowy = CStr(wiersz("NumerInwentarzowy"))
            s.Nazwa = CStr(wiersz("Nazwa"))
            If IsDBNull(wiersz("Kategoria")) Then
                s.Kategoria = ""
            Else
                s.Kategoria = CStr(wiersz("Kategoria")).Trim()
            End If
            s.StawkaDobowa = CDec(wiersz("StawkaDobowa"))
            s.Kaucja = CDec(wiersz("Kaucja"))
            s.Status = CStr(wiersz("Status"))
            Return s
        End Function

    End Class

End Namespace
