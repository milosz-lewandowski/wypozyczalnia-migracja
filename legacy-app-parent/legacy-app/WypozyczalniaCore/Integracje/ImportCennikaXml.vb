' Modul: Integracje - import cennika z plikow XML od dzialu handlowego | Autor: zespol IT | 2014-03-11
' Historia:
'   2017-06-12 JK: przenoszenie przetworzonych plikow do podkatalogu
'   2018-02-27 MW: kaucja w elemencie pozycja

Imports System.IO
Imports System.Data
Imports System.Xml
Imports Wypozyczalnia.Core.Model
Imports Wypozyczalnia.Core.DAL

Namespace Integracje

    Public Class ImportCennikaXml

        Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(ImportCennikaXml))

        Private Const KatalogPrzetworzonych As String = "przetworzone"

        Public Property OstatniBlad As String

        Public Function ImportujKatalog(ByVal katalog As String) As Integer
            If String.IsNullOrEmpty(katalog) Then Throw New ArgumentException("Nie podano katalogu importu")
            If Not Directory.Exists(katalog) Then
                Throw New IOException("Katalog importu nie istnieje: " & katalog)
            End If
            Dim docelowy As String = Path.Combine(katalog, KatalogPrzetworzonych)
            If Not Directory.Exists(docelowy) Then Directory.CreateDirectory(docelowy)

            Dim pliki As String() = Directory.GetFiles(katalog, "*.xml")
            Array.Sort(pliki)
            Dim licznik As Integer = 0
            For Each plik As String In pliki
                Log.Info("Import cennika: " & plik)
                Dim wiersze As Integer = ImportujPlik(plik)
                Log.Info("Zaktualizowano wierszy: " & wiersze)
                PrzeniesPlik(plik, docelowy)
                licznik = licznik + 1
            Next
            Return licznik
        End Function

        Public Function ImportujPlik(ByVal sciezka As String) As Integer
            Dim doc As New XmlDocument()
            doc.Load(sciezka)
            Dim korzen As XmlElement = doc.DocumentElement
            If korzen Is Nothing OrElse korzen.Name <> "cennik" Then
                Throw New XmlException("Nieprawidlowy element glowny w pliku " & Path.GetFileName(sciezka))
            End If

            Dim obowiazujeOd As Date = Date.Today
            If korzen.HasAttribute("obowiazujeOd") Then
                obowiazujeOd = Date.Parse(korzen.GetAttribute("obowiazujeOd"))
            End If

            Dim dal As New PricingDAL()
            Dim zaktualizowane As Integer = 0

            For Each wezel As XmlNode In korzen.SelectNodes("taryfa")
                Dim t As New Taryfa()
                t.Kategoria = Atrybut(wezel, "kategoria").ToUpper()
                t.MnoznikWeekend = Decimal.Parse(Atrybut(wezel, "mnoznikWeekend"))
                t.RabatTygodniowyProc = CDec(Atrybut(wezel, "rabatTygodniowy"))
                t.ObowiazujeOd = obowiazujeOd
                If Not Taryfa.CzyZnanaKategoria(t.Kategoria) Then
                    Log.Warn("Pominieto taryfe nieznanej kategorii: " & t.Kategoria)
                    Continue For
                End If
                dal.ZapiszTaryfe(t)
                zaktualizowane = zaktualizowane + 1
            Next

            For Each wezel As XmlNode In korzen.SelectNodes("pozycja")
                Dim numer As String = Atrybut(wezel, "numer")
                Dim stawka As Decimal = Decimal.Parse(Atrybut(wezel, "stawka"))
                Dim kaucja As Decimal = 0D
                If wezel.Attributes("kaucja") IsNot Nothing Then
                    kaucja = CDec(Atrybut(wezel, "kaucja"))
                Else
                    kaucja = KaucjaBiezaca(numer)
                End If
                If stawka <= 0D Then
                    Log.Warn("Pominieto pozycje " & numer & " ze stawka " & stawka)
                    Continue For
                End If
                Dim ile As Integer = dal.ZaktualizujStawkeSprzetu(numer, stawka, kaucja)
                If ile = 0 Then
                    Log.Warn("Brak sprzetu o numerze " & numer)
                End If
                zaktualizowane = zaktualizowane + ile
            Next

            dal.ZapiszWDzienniku("IMPORT_CENNIKA", Path.GetFileName(sciezka) & ": " & zaktualizowane)
            Return zaktualizowane
        End Function

        Public Function PlikiOczekujace(ByVal katalog As String) As String()
            If String.IsNullOrEmpty(katalog) OrElse Not Directory.Exists(katalog) Then Return New String() {}
            Dim pliki As String() = Directory.GetFiles(katalog, "*.xml")
            For i As Integer = 0 To pliki.Length - 1
                pliki(i) = Path.GetFileName(pliki(i))
            Next
            Array.Sort(pliki)
            Return pliki
        End Function

        Public Function SprawdzPlik(ByVal sciezka As String) As List(Of String)
            Dim uwagi As New List(Of String)()
            Dim doc As New XmlDocument()
            Try
                doc.Load(sciezka)
            Catch ex As XmlException
                uwagi.Add("Plik nie jest poprawnym XML: " & ex.Message)
                Return uwagi
            End Try
            Dim korzen As XmlElement = doc.DocumentElement
            If korzen.Name <> "cennik" Then uwagi.Add("Element glowny: " & korzen.Name)
            If Not korzen.HasAttribute("obowiazujeOd") Then uwagi.Add("Brak atrybutu obowiazujeOd (przyjeta data biezaca)")
            For Each wezel As XmlNode In korzen.SelectNodes("taryfa")
                If wezel.Attributes("kategoria") Is Nothing Then uwagi.Add("Taryfa bez kategorii")
            Next
            For Each wezel As XmlNode In korzen.SelectNodes("pozycja")
                If wezel.Attributes("numer") Is Nothing Then uwagi.Add("Pozycja bez numeru")
                If wezel.Attributes("stawka") Is Nothing Then uwagi.Add("Pozycja bez stawki")
            Next
            If korzen.SelectNodes("taryfa").Count = 0 AndAlso korzen.SelectNodes("pozycja").Count = 0 Then
                uwagi.Add("Plik nie zawiera taryf ani pozycji")
            End If
            Return uwagi
        End Function

        Private Function Atrybut(ByVal wezel As XmlNode, ByVal nazwa As String) As String
            Dim a As XmlAttribute = wezel.Attributes(nazwa)
            If a Is Nothing Then
                Throw New XmlException("Brak atrybutu " & nazwa & " w elemencie " & wezel.Name)
            End If
            Return a.Value.Trim()
        End Function

        Private Function KaucjaBiezaca(ByVal numer As String) As Decimal
            Dim dt As DataTable = DbHelper.WykonajZapytanie("SELECT Kaucja FROM Sprzet WHERE NumerInwentarzowy = @Numer",
                DbHelper.Parametr("@Numer", numer))
            If dt.Rows.Count = 0 Then Return 0D
            Return CDec(dt.Rows(0)("Kaucja"))
        End Function

        Private Sub PrzeniesPlik(ByVal plik As String, ByVal docelowy As String)
            Dim nazwa As String = Path.GetFileName(plik)
            Dim cel As String = Path.Combine(docelowy, nazwa)
            If File.Exists(cel) Then
                cel = Path.Combine(docelowy, Path.GetFileNameWithoutExtension(nazwa) & "_" & DateTime.Now.ToString("yyyyMMddHHmmss") & ".xml")
            End If
            Try
                File.Move(plik, cel)
            Catch ex As IOException
                OstatniBlad = ex.Message
                Log.Error("Nie przeniesiono pliku " & plik, ex)
                Throw
            End Try
        End Sub

    End Class

End Namespace
