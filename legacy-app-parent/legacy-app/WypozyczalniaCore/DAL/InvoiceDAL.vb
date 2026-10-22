' Modul: DAL - dostep do tabel Faktury i PozycjeFaktur | Autor: zespol IT | 2014-05-19
' Historia:
'   2014-11-27 JK  numer faktury nadawany w bazie (usp_WystawFakture)
'   2018-04-03 PT  FakturyZDnia dla eksportu nocnego

Imports System.Data
Imports System.Data.SqlClient
Imports Wypozyczalnia.Core.Model

Namespace DAL

    Public Class InvoiceDAL

        Private Const KolumnyFaktury As String = "f.Id, f.Numer, f.KlientId, f.WypozyczenieId, f.DataWystawienia, f.Netto, f.Vat, f.Brutto, f.Status, f.IdTransakcji"

        ' naglowek przez procedure - zwraca nowe Id, numer ustawiany w obiekcie
        Public Function ZapiszNaglowek(ByVal f As Faktura) As Integer
            Dim noweId As SqlParameter = DbHelper.ParametrWyjsciowy("@NoweId", SqlDbType.Int)
            Dim numer As SqlParameter = DbHelper.ParametrWyjsciowy("@Numer", SqlDbType.VarChar, 30)
            DbHelper.WykonajProcedureBezWyniku("usp_WystawFakture", _
                                               DbHelper.Parametr("@KlientId", f.KlientId), _
                                               DbHelper.Parametr("@WypozyczenieId", f.WypozyczenieId), _
                                               DbHelper.Parametr("@Netto", f.Netto), _
                                               DbHelper.Parametr("@Vat", f.Vat), _
                                               DbHelper.Parametr("@Brutto", f.Brutto), _
                                               noweId, _
                                               numer)
            If noweId.Value Is Nothing OrElse IsDBNull(noweId.Value) Then
                Throw New ApplicationException("Procedura usp_WystawFakture nie zwrocila Id")
            End If
            f.Id = CInt(noweId.Value)
            f.Numer = numer.Value.ToString()
            Return f.Id
        End Function

        Public Sub ZapiszPozycje(ByVal fakturaId As Integer, ByVal p As PozycjaFaktury)
            Dim sql As String = "INSERT INTO PozycjeFaktur (FakturaId, Lp, Opis, Ilosc, CenaNetto, StawkaVat, WartoscNetto) " & _
                                "VALUES (@FakturaId, @Lp, @Opis, @Ilosc, @CenaNetto, @StawkaVat, @WartoscNetto)"
            DbHelper.WykonajPolecenie(sql, _
                                      DbHelper.Parametr("@FakturaId", fakturaId), _
                                      DbHelper.Parametr("@Lp", p.Lp), _
                                      DbHelper.Parametr("@Opis", p.Opis), _
                                      DbHelper.Parametr("@Ilosc", p.Ilosc), _
                                      DbHelper.Parametr("@CenaNetto", p.CenaNetto), _
                                      DbHelper.Parametr("@StawkaVat", p.StawkaVat), _
                                      DbHelper.Parametr("@WartoscNetto", p.WartoscNetto))
        End Sub

        Public Function Pobierz(ByVal fakturaId As Integer) As DataRow
            Dim sql As String = "SELECT " & KolumnyFaktury & " FROM Faktury f WHERE f.Id = @Id"
            Dim tabela As DataTable = DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@Id", fakturaId))
            If tabela.Rows.Count = 0 Then
                Return Nothing
            End If
            Return tabela.Rows(0)
        End Function

        Public Function PobierzDlaWypozyczenia(ByVal wypozyczenieId As Integer) As DataRow
            Dim sql As String = "SELECT TOP 1 " & KolumnyFaktury & " FROM Faktury f " & _
                                "WHERE f.WypozyczenieId = @WypozyczenieId ORDER BY f.Id DESC"
            Dim tabela As DataTable = DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@WypozyczenieId", wypozyczenieId))
            If tabela.Rows.Count = 0 Then
                Return Nothing
            End If
            Return tabela.Rows(0)
        End Function

        Public Function PobierzPozycje(ByVal fakturaId As Integer) As DataTable
            Dim sql As String = "SELECT Lp, Opis, Ilosc, CenaNetto, StawkaVat, WartoscNetto " & _
                                "FROM PozycjeFaktur WHERE FakturaId = @FakturaId ORDER BY Lp"
            Return DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@FakturaId", fakturaId))
        End Function

        Public Function UstawOplacona(ByVal fakturaId As Integer, ByVal idTransakcji As String) As Integer
            Dim sql As String = "UPDATE Faktury SET Status = 'O', IdTransakcji = @IdTransakcji WHERE Id = @Id AND Status = 'W'"
            Dim ile As Integer = DbHelper.WykonajPolecenie(sql, _
                                                          DbHelper.Parametr("@IdTransakcji", idTransakcji), _
                                                          DbHelper.Parametr("@Id", fakturaId))
            If ile > 0 Then
                DbHelper.WykonajPolecenie("INSERT INTO DziennikOperacji (Operacja, Szczegoly) VALUES ('PLATNOSC', @Szczegoly)", _
                                          DbHelper.Parametr("@Szczegoly", "Faktura " & fakturaId & ", transakcja " & idTransakcji))
            End If
            Return ile
        End Function

        ' kolumny: Id, Numer, KlientId, Klient, WypozyczenieId, DataWystawienia, Netto, Vat, Brutto, Status
        Public Function FakturyZDnia(ByVal dzien As Date) As DataTable
            Dim sql As String = "SELECT f.Id, f.Numer, f.KlientId, " & _
                                "CASE WHEN k.Typ = 'F' THEN k.NazwaFirmy ELSE k.Imie + ' ' + k.Nazwisko END AS Klient, " & _
                                "f.WypozyczenieId, f.DataWystawienia, f.Netto, f.Vat, f.Brutto, f.Status " & _
                                "FROM Faktury f INNER JOIN Klienci k ON k.Id = f.KlientId " & _
                                "WHERE f.DataWystawienia >= @Od AND f.DataWystawienia < @Do " & _
                                "ORDER BY f.Numer"
            Return DbHelper.WykonajZapytanie(sql, _
                                             DbHelper.Parametr("@Od", dzien.Date), _
                                             DbHelper.Parametr("@Do", dzien.Date.AddDays(1)))
        End Function

        Public Function FakturyKlienta(ByVal klientId As Integer) As DataTable
            Dim sql As String = "SELECT " & KolumnyFaktury & " FROM Faktury f " & _
                                "WHERE f.KlientId = @KlientId ORDER BY f.DataWystawienia DESC"
            Return DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@KlientId", klientId))
        End Function

        Public Function Nieoplacone() As DataTable
            Dim sql As String = "SELECT " & KolumnyFaktury & " FROM Faktury f " & _
                                "WHERE f.Status = 'W' ORDER BY f.DataWystawienia"
            Return DbHelper.WykonajZapytanie(sql)
        End Function

        Public Function PobierzPoNumerze(ByVal numer As String) As DataRow
            Dim sql As String = "SELECT " & KolumnyFaktury & " FROM Faktury f WHERE f.Numer = @Numer"
            Dim tabela As DataTable = DbHelper.WykonajZapytanie(sql, DbHelper.Parametr("@Numer", numer))
            If tabela.Rows.Count = 0 Then
                Return Nothing
            End If
            Return tabela.Rows(0)
        End Function

        ' zestawienie miesieczne dla ksiegowosci (ekran nieuzywany od 2017, zostawione dla raportu)
        Public Function ZestawienieMiesiaca(ByVal rok As Integer, ByVal miesiac As Integer) As DataTable
            Dim od As New Date(rok, miesiac, 1)
            Dim sql As String = "SELECT f.Status, COUNT(*) AS Liczba, SUM(f.Netto) AS Netto, SUM(f.Vat) AS Vat, SUM(f.Brutto) AS Brutto " & _
                                "FROM Faktury f WHERE f.DataWystawienia >= @Od AND f.DataWystawienia < @Do " & _
                                "GROUP BY f.Status"
            Return DbHelper.WykonajZapytanie(sql, _
                                             DbHelper.Parametr("@Od", od), _
                                             DbHelper.Parametr("@Do", od.AddMonths(1)))
        End Function

        ' usuniecie nieoplaconej faktury razem z pozycjami - jedna transakcja
        Public Sub Usun(ByVal fakturaId As Integer)
            Using polaczenie As New SqlConnection(DbHelper.PobierzConnectionString())
                polaczenie.Open()
                Dim transakcja As SqlTransaction = polaczenie.BeginTransaction()
                Try
                    Dim cmdPozycje As New SqlCommand("DELETE FROM PozycjeFaktur WHERE FakturaId = @Id", polaczenie, transakcja)
                    cmdPozycje.Parameters.AddWithValue("@Id", fakturaId)
                    cmdPozycje.ExecuteNonQuery()
                    Dim cmdFaktura As New SqlCommand("DELETE FROM Faktury WHERE Id = @Id AND Status = 'W'", polaczenie, transakcja)
                    cmdFaktura.Parameters.AddWithValue("@Id", fakturaId)
                    If cmdFaktura.ExecuteNonQuery() = 0 Then
                        Throw New BladBiznesowyException("Mozna usunac tylko nieoplacona fakture", "FV_STATUS")
                    End If
                    transakcja.Commit()
                Catch
                    transakcja.Rollback()
                    Throw
                End Try
            End Using
        End Sub

        Public Function CzyIstniejeDlaWypozyczenia(ByVal wypozyczenieId As Integer) As Boolean
            Dim tabela As DataTable = DbHelper.WykonajZapytanie("SELECT COUNT(*) AS Ile FROM Faktury WHERE WypozyczenieId = @WypozyczenieId", _
                                                                DbHelper.Parametr("@WypozyczenieId", wypozyczenieId))
            Return CInt(tabela.Rows(0)("Ile")) > 0
        End Function

    End Class

End Namespace
