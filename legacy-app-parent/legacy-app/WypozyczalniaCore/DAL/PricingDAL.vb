' Modul: DAL - taryfy i stawki sprzetu | Autor: zespol IT | 2014-03-11
' Historia:
'   2015-02-04 MW: taryfy z data obowiazywania (usp_PobierzTaryfe)
'   2017-06-12 JK: ZaktualizujStawkeSprzetu dla importu cennika XML

Imports System.Data
Imports System.Data.SqlClient
Imports Wypozyczalnia.Core.Model

Namespace DAL

    Public Class PricingDAL

        Public Function PobierzTaryfe(ByVal kategoria As String, ByVal naDzien As Date) As Taryfa
            Dim dt As DataTable = DbHelper.WykonajProcedure("usp_PobierzTaryfe",
                DbHelper.Parametr("@Kategoria", kategoria),
                DbHelper.Parametr("@NaDzien", naDzien.Date))
            If dt.Rows.Count = 0 Then
                Throw New BladBiznesowyException("Brak taryfy dla kategorii " & kategoria & " na dzien " & naDzien.ToString("yyyy-MM-dd"), "TARYFA_BRAK")
            End If
            Return MapujTaryfe(dt.Rows(0))
        End Function

        Public Sub ZapiszTaryfe(ByVal t As Taryfa)
            If t Is Nothing Then Throw New ArgumentNullException("t")
            DbHelper.WykonajProcedureBezWyniku("usp_ZapiszTaryfe",
                DbHelper.Parametr("@Kategoria", t.Kategoria),
                DbHelper.Parametr("@MnoznikWeekend", t.MnoznikWeekend),
                DbHelper.Parametr("@RabatTygodniowyProc", t.RabatTygodniowyProc),
                DbHelper.Parametr("@ObowiazujeOd", t.ObowiazujeOd.Date))
        End Sub

        Public Function ListaTaryf() As DataTable
            Dim sql As String = "SELECT Id, Kategoria, MnoznikWeekend, RabatTygodniowyProc, ObowiazujeOd " &
                                "FROM Taryfy ORDER BY Kategoria, ObowiazujeOd DESC"
            Return DbHelper.WykonajZapytanie(sql)
        End Function

        Public Function TaryfyKategorii(ByVal kategoria As String) As List(Of Taryfa)
            Dim wynik As New List(Of Taryfa)()
            Dim sql As String = "SELECT Id, Kategoria, MnoznikWeekend, RabatTygodniowyProc, ObowiazujeOd " &
                                "FROM Taryfy WHERE Kategoria = @Kategoria ORDER BY ObowiazujeOd DESC"
            Dim dt As DataTable = DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@Kategoria", kategoria))
            For Each row As DataRow In dt.Rows
                wynik.Add(MapujTaryfe(row))
            Next
            Return wynik
        End Function

        Public Function UsunTaryfe(ByVal taryfaId As Integer) As Integer
            Dim sql As String = "DELETE FROM Taryfy WHERE Id = @Id"
            Return DbHelper.WykonajPolecenie(sql, DbHelper.Parametr("@Id", taryfaId))
        End Function

        Public Function ZaktualizujStawkeSprzetu(ByVal numerInwentarzowy As String, ByVal stawka As Decimal, ByVal kaucja As Decimal) As Integer
            Dim sql As String = "UPDATE Sprzet SET StawkaDobowa = @Stawka, Kaucja = @Kaucja " &
                                "WHERE NumerInwentarzowy = @Numer AND Status <> 'X'"
            Return DbHelper.WykonajPolecenie(sql,
                DbHelper.Parametr("@Stawka", stawka),
                DbHelper.Parametr("@Kaucja", kaucja),
                DbHelper.Parametr("@Numer", numerInwentarzowy))
        End Function

        Public Function StawkaSprzetu(ByVal sprzetId As Integer) As Decimal
            Dim sql As String = "SELECT StawkaDobowa FROM Sprzet WHERE Id = @Id"
            Dim dt As DataTable = DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@Id", sprzetId))
            If dt.Rows.Count = 0 Then Return 0D
            Return CDec(dt.Rows(0)("StawkaDobowa"))
        End Function

        Public Sub ZapiszWDzienniku(ByVal operacja As String, ByVal szczegoly As String)
            Dim sql As String = "INSERT INTO DziennikOperacji (Kiedy, Operacja, Szczegoly) VALUES (GETDATE(), @Operacja, @Szczegoly)"
            If szczegoly IsNot Nothing AndAlso szczegoly.Length > 400 Then szczegoly = szczegoly.Substring(0, 400)
            DbHelper.WykonajPolecenie(sql,
                DbHelper.Parametr("@Operacja", operacja),
                DbHelper.Parametr("@Szczegoly", szczegoly))
        End Sub

        Public Function TaryfaIstnieje(ByVal kategoria As String, ByVal obowiazujeOd As Date) As Boolean
            Dim sql As String = "SELECT COUNT(*) AS Ile FROM Taryfy WHERE Kategoria = @Kategoria AND ObowiazujeOd = @Od"
            Dim dt As DataTable = DbHelper.WykonajZapytanie(sql,
                DbHelper.Parametr("@Kategoria", kategoria),
                DbHelper.Parametr("@Od", obowiazujeOd.Date))
            Return CInt(dt.Rows(0)("Ile")) > 0
        End Function

        Public Function TaryfyNaDzien(ByVal naDzien As Date) As List(Of Taryfa)
            Dim wynik As New List(Of Taryfa)()
            For Each kategoria As String In Taryfa.KategorieZnane
                Try
                    wynik.Add(PobierzTaryfe(kategoria, naDzien))
                Catch ex As BladBiznesowyException
                    ' brak taryfy dla kategorii - pomijamy
                End Try
            Next
            Return wynik
        End Function

        Public Function ListaSprzetuZeStawkami(ByVal kategoria As String) As DataTable
            Dim sql As String = "SELECT Id, NumerInwentarzowy, Nazwa, Kategoria, StawkaDobowa, Kaucja, Status " &
                                "FROM Sprzet WHERE Status <> 'X'"
            If Not String.IsNullOrEmpty(kategoria) Then
                sql = sql & " AND Kategoria = @Kategoria ORDER BY NumerInwentarzowy"
                Return DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@Kategoria", kategoria))
            End If
            sql = sql & " ORDER BY Kategoria, NumerInwentarzowy"
            Return DbHelper.WykonajZapytanie(sql)
        End Function

        Public Function HistoriaZmianCennika(ByVal odDnia As Date) As DataTable
            Dim sql As String = "SELECT Id, Kiedy, Operacja, Szczegoly FROM DziennikOperacji " &
                                "WHERE Operacja IN ('TARYFA', 'STAWKA', 'IMPORT_CENNIKA') AND Kiedy >= @Od " &
                                "ORDER BY Kiedy DESC"
            Return DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@Od", odDnia.Date))
        End Function

        Public Function ZaktualizujKaucje(ByVal sprzetId As Integer, ByVal kaucja As Decimal) As Integer
            Dim sql As String = "UPDATE Sprzet SET Kaucja = @Kaucja WHERE Id = @Id"
            Return DbHelper.WykonajPolecenie(sql,
                DbHelper.Parametr("@Kaucja", kaucja),
                DbHelper.Parametr("@Id", sprzetId))
        End Function

        Public Function PobierzTaryfePoId(ByVal taryfaId As Integer) As Taryfa
            Dim sql As String = "SELECT Id, Kategoria, MnoznikWeekend, RabatTygodniowyProc, ObowiazujeOd FROM Taryfy WHERE Id = @Id"
            Dim dt As DataTable = DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@Id", taryfaId))
            If dt.Rows.Count = 0 Then Return Nothing
            Return MapujTaryfe(dt.Rows(0))
        End Function

        Public Function KategorieZTaryfami() As List(Of String)
            Dim wynik As New List(Of String)()
            Dim dt As DataTable = DbHelper.WykonajZapytanie("SELECT DISTINCT Kategoria FROM Taryfy ORDER BY Kategoria")
            For Each row As DataRow In dt.Rows
                wynik.Add(CStr(row("Kategoria")).Trim())
            Next
            Return wynik
        End Function

        Private Function MapujTaryfe(ByVal row As DataRow) As Taryfa
            Dim t As New Taryfa()
            t.Id = CInt(row("Id"))
            t.Kategoria = CStr(row("Kategoria")).Trim()
            t.MnoznikWeekend = CDec(row("MnoznikWeekend"))
            t.RabatTygodniowyProc = CDec(row("RabatTygodniowyProc"))
            t.ObowiazujeOd = CDate(row("ObowiazujeOd"))
            Return t
        End Function

    End Class

End Namespace
