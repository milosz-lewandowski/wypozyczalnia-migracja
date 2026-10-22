' Modul: Integracje\EksportCsv.vb - dzienny eksport CSV na udzial sieciowy | Autor: zespol IT | 2015-08-04
' Pliki odbiera system ksiegowy z udzialu (wypozyczenia_yyyyMMdd.csv, faktury_yyyyMMdd.csv).
' 2016-10-12 MW: drugi plik z fakturami dnia
' 2018-04-03 PT: przeciazenie z katalogiem docelowym (zadanie nocne)

Imports System.Data
Imports System.IO
Imports System.Text
Imports System.Configuration

Namespace Integracje

    Public Class EksportCsv

        Private Const Separator As String = ";"
        Private Const NaglowekWypozyczen As String = "Id;Klient;Sprzet;DataWydania;PlanowanyZwrot;DataZwrotu;Oplata;Kara"
        Private Const NaglowekFaktur As String = "Numer;Klient;WypozyczenieId;DataWystawienia;Netto;Vat;Brutto;Status"
        Private Const FormatDaty As String = "yyyy-MM-dd HH:mm"

        Public Function EksportujDzien(ByVal dzien As Date) As String
            Dim katalog As String = ConfigurationManager.AppSettings("EksportUncSciezka")
            If String.IsNullOrEmpty(katalog) Then
                Throw New ConfigurationErrorsException("Brak klucza EksportUncSciezka w konfiguracji")
            End If
            Return EksportujDzien(dzien, katalog)
        End Function

        Public Function EksportujDzien(ByVal dzien As Date, ByVal katalogDocelowy As String) As String
            Dim wypozyczenia As DataTable = New RentalManager().WypozyczeniaZDnia(dzien)
            Dim tresc As New StringBuilder()
            tresc.AppendLine(NaglowekWypozyczen)
            For Each wiersz As DataRow In wypozyczenia.Rows
                Dim linia As New StringBuilder()
                linia.Append(wiersz("Id").ToString()).Append(Separator)
                linia.Append(Oczysc(wiersz("Klient"))).Append(Separator)
                linia.Append(Oczysc(wiersz("Sprzet"))).Append(Separator)
                linia.Append(Data(wiersz("DataWydania"))).Append(Separator)
                linia.Append(Data(wiersz("PlanowanyZwrot"))).Append(Separator)
                linia.Append(Data(wiersz("DataZwrotu"))).Append(Separator)
                linia.Append(Kwota(wiersz("Oplata"))).Append(Separator)
                linia.Append(Kwota(wiersz("Kara")))
                tresc.AppendLine(linia.ToString())
            Next

            Dim plikWypozyczen As String = Path.Combine(katalogDocelowy, "wypozyczenia_" & dzien.ToString("yyyyMMdd") & ".csv")
            ZapiszNaUdzial(plikWypozyczen, tresc.ToString())

            Dim faktury As DataTable = New InvoiceManager().FakturyZDnia(dzien)
            Dim trescFaktur As New StringBuilder()
            trescFaktur.AppendLine(NaglowekFaktur)
            For Each wiersz As DataRow In faktury.Rows
                trescFaktur.Append(wiersz("Numer").ToString()).Append(Separator)
                trescFaktur.Append(Oczysc(wiersz("Klient"))).Append(Separator)
                trescFaktur.Append(wiersz("WypozyczenieId").ToString()).Append(Separator)
                trescFaktur.Append(Data(wiersz("DataWystawienia"))).Append(Separator)
                trescFaktur.Append(Kwota(wiersz("Netto"))).Append(Separator)
                trescFaktur.Append(Kwota(wiersz("Vat"))).Append(Separator)
                trescFaktur.Append(Kwota(wiersz("Brutto"))).Append(Separator)
                trescFaktur.AppendLine(wiersz("Status").ToString())
            Next
            Dim plikFaktur As String = Path.Combine(katalogDocelowy, "faktury_" & dzien.ToString("yyyyMMdd") & ".csv")
            ZapiszNaUdzial(plikFaktur, trescFaktur.ToString())

            Return plikWypozyczen
        End Function

        Private Sub ZapiszNaUdzial(ByVal sciezka As String, ByVal tresc As String)
            On Error Resume Next
            Dim katalog As String = Path.GetDirectoryName(sciezka)
            If Not Directory.Exists(katalog) Then
                Directory.CreateDirectory(katalog)
            End If
            If File.Exists(sciezka) Then
                File.Copy(sciezka, sciezka & ".bak", True)
            End If
            File.WriteAllText(sciezka, tresc, Encoding.GetEncoding(1250))
        End Sub

        Private Function Kwota(ByVal wartosc As Object) As String
            If wartosc Is Nothing OrElse IsDBNull(wartosc) Then
                Return ""
            End If
            Return CDec(wartosc).ToString("0.00")
        End Function

        Private Function Data(ByVal wartosc As Object) As String
            If wartosc Is Nothing OrElse IsDBNull(wartosc) Then
                Return ""
            End If
            Return CDate(wartosc).ToString(FormatDaty)
        End Function

        Private Function Oczysc(ByVal wartosc As Object) As String
            If wartosc Is Nothing OrElse IsDBNull(wartosc) Then
                Return ""
            End If
            Dim tekst As String = wartosc.ToString()
            tekst = tekst.Replace(Separator, ",")
            tekst = tekst.Replace(vbCr, " ").Replace(vbLf, " ")
            Return tekst.Trim()
        End Function

        ' powtorzenie eksportu za kilka dni (np. po awarii udzialu)
        Public Function EksportujOkres(ByVal dataOd As Date, ByVal dataDo As Date, ByVal katalogDocelowy As String) As Integer
            If dataDo < dataOd Then
                Throw New ArgumentException("Data koncowa wczesniejsza niz poczatkowa")
            End If
            Dim ile As Integer = 0
            Dim dzien As Date = dataOd.Date
            While dzien <= dataDo.Date
                EksportujDzien(dzien, katalogDocelowy)
                If CzyWyeksportowano(dzien, katalogDocelowy) Then
                    ile = ile + 1
                End If
                dzien = dzien.AddDays(1)
            End While
            Return ile
        End Function

        Public Function NazwaPlikuDnia(ByVal dzien As Date) As String
            Return "wypozyczenia_" & dzien.ToString("yyyyMMdd") & ".csv"
        End Function

        Public Function CzyWyeksportowano(ByVal dzien As Date, ByVal katalogDocelowy As String) As Boolean
            If String.IsNullOrEmpty(katalogDocelowy) Then Return False
            Return File.Exists(Path.Combine(katalogDocelowy, NazwaPlikuDnia(dzien)))
        End Function

    End Class

End Namespace
