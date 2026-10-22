' Modul: Faktura.aspx.vb | Autor: zespol IT | 2014-05-19
' Faktura za wypozyczenie: wystawienie, platnosc przez bramke, eksport CSV dnia.
' 2015-01-12 MW: platnosc przez bramke SOAP (zamiast strony Platnosc.aspx)
' 2018-04-03 PT: lista faktur z dnia i przycisk eksportu CSV

Imports System.Data
Imports System.Net
Imports System.Web.Services.Protocols
Imports Wypozyczalnia.Core
Imports Wypozyczalnia.Core.Model
Imports Wypozyczalnia.Core.Integracje

Partial Class Faktura
    Inherits System.Web.UI.Page

    Protected WithEvents lblKomunikat As Global.System.Web.UI.WebControls.Label
    Protected WithEvents lblWypozyczenie As Global.System.Web.UI.WebControls.Label
    Protected WithEvents lblKlient As Global.System.Web.UI.WebControls.Label
    Protected WithEvents lblNumer As Global.System.Web.UI.WebControls.Label
    Protected WithEvents lblStatus As Global.System.Web.UI.WebControls.Label
    Protected WithEvents btnWystaw As Global.System.Web.UI.WebControls.Button
    Protected WithEvents gvPozycje As Global.System.Web.UI.WebControls.GridView
    Protected WithEvents lblNetto As Global.System.Web.UI.WebControls.Label
    Protected WithEvents lblVat As Global.System.Web.UI.WebControls.Label
    Protected WithEvents lblBrutto As Global.System.Web.UI.WebControls.Label
    Protected WithEvents btnZaplac As Global.System.Web.UI.WebControls.Button
    Protected WithEvents lblPlatnosc As Global.System.Web.UI.WebControls.Label
    Protected WithEvents gvFakturyDnia As Global.System.Web.UI.WebControls.GridView
    Protected WithEvents btnEksportCsv As Global.System.Web.UI.WebControls.Button
    Protected WithEvents lblEksport As Global.System.Web.UI.WebControls.Label

    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Faktura))

    Private Const Waluta As String = "PLN"
    Private Const FormatDaty As String = "yyyy-MM-dd HH:mm"

    Private Property WypozyczenieId As Integer
        Get
            If ViewState("WypozyczenieId") Is Nothing Then Return 0
            Return ViewState("WypozyczenieId")
        End Get
        Set(ByVal value As Integer)
            ViewState("WypozyczenieId") = value
        End Set
    End Property

    Private Property FakturaId As Integer
        Get
            If ViewState("FakturaId") Is Nothing Then Return 0
            Return ViewState("FakturaId")
        End Get
        Set(ByVal value As Integer)
            ViewState("FakturaId") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            WypozyczenieId = UstalWypozyczenie()
            PokazWypozyczenie()
            WczytajFakturyDnia()
        End If
    End Sub

    ' najpierw ?id= z linku po zwrocie, potem sesja
    Private Function UstalWypozyczenie() As Integer
        Dim id As Integer = 0
        Dim zParametru As String = Request.QueryString("id")
        If Not String.IsNullOrEmpty(zParametru) Then
            Integer.TryParse(zParametru, id)
        End If
        If id = 0 AndAlso Session("OstatnieWypozyczenieId") IsNot Nothing Then
            id = Session("OstatnieWypozyczenieId")
        End If
        Return id
    End Function

    Private Sub PokazWypozyczenie()
        WyczyscFakture()
        If WypozyczenieId = 0 Then
            lblWypozyczenie.Text = "nie wybrano (wejdź z ekranu zwrotu)"
            PokazWybranegoKlienta()
            btnWystaw.Enabled = False
            btnZaplac.Enabled = False
            Return
        End If

        Dim w As Wypozyczenie = New RentalManager().PobierzWypozyczenie(WypozyczenieId)
        If w Is Nothing Then
            lblKomunikat.Text = "Nie znaleziono wypożyczenia nr " & WypozyczenieId
            btnWystaw.Enabled = False
            btnZaplac.Enabled = False
            Return
        End If

        Dim sprzet As Sprzet = New EquipmentManager().PobierzSprzet(w.SprzetId)
        Dim opis As String = "#" & w.Id & " | wydano " & w.DataWydania.ToString(FormatDaty)
        If sprzet IsNot Nothing Then opis = opis & " | " & sprzet.Nazwa
        If w.DataZwrotu.HasValue Then
            opis = opis & " | zwrot " & w.DataZwrotu.Value.ToString(FormatDaty)
        Else
            opis = opis & " | jeszcze nie zwrócono"
        End If
        lblWypozyczenie.Text = opis

        Dim klient As Klient = New CustomerManager().PobierzKlienta(w.KlientId)
        If klient IsNot Nothing Then
            lblKlient.Text = NazwaKlienta(klient)
        End If
        If Session("WybranyKlientId") <> 0 AndAlso Session("WybranyKlientId") <> w.KlientId Then
            lblKomunikat.Text = "Uwaga: wypożyczenie należy do innego klienta niż wybrany na liście klientów."
        End If

        Dim mgr As New InvoiceManager()
        Dim f As Wypozyczalnia.Core.Model.Faktura = mgr.PobierzFaktureDlaWypozyczenia(w.Id)
        If f IsNot Nothing Then
            PokazFakture(f)
            btnWystaw.Enabled = False
        ElseIf w.Status = "Z" Then
            Dim podglad As Wypozyczalnia.Core.Model.Faktura = mgr.PodgladFaktury(w.Id)
            If podglad IsNot Nothing Then
                PokazPozycje(podglad)
                lblNumer.Text = "(podgląd - faktura nie wystawiona)"
            End If
            btnWystaw.Enabled = True
            btnZaplac.Enabled = False
        Else
            lblKomunikat.Text = "Fakturę można wystawić dopiero po zwrocie sprzętu."
            btnWystaw.Enabled = False
            btnZaplac.Enabled = False
        End If
    End Sub

    Private Sub PokazWybranegoKlienta()
        If Session("WybranyKlientId") Is Nothing OrElse Session("WybranyKlientId") = 0 Then
            lblKlient.Text = "-"
            Return
        End If
        Dim klient As Klient = New CustomerManager().PobierzKlienta(Session("WybranyKlientId"))
        If klient IsNot Nothing Then
            lblKlient.Text = NazwaKlienta(klient) & " (wybrany na liście klientów)"
        End If
    End Sub

    Private Sub PokazFakture(ByVal f As Wypozyczalnia.Core.Model.Faktura)
        FakturaId = f.Id
        lblNumer.Text = f.Numer & " z dnia " & f.DataWystawienia.ToString("yyyy-MM-dd")
        If f.Status = "O" Then
            lblStatus.Text = "OPŁACONA (transakcja " & f.IdTransakcji & ")"
            lblStatus.ForeColor = Drawing.Color.Green
            btnZaplac.Enabled = False
        Else
            lblStatus.Text = "do zapłaty"
            lblStatus.ForeColor = Drawing.Color.DarkOrange
            btnZaplac.Enabled = True
        End If
        PokazPozycje(f)
    End Sub

    Private Sub PokazPozycje(ByVal f As Wypozyczalnia.Core.Model.Faktura)
        gvPozycje.DataSource = f.Pozycje
        gvPozycje.DataBind()
        lblNetto.Text = f.Netto
        lblVat.Text = f.Vat
        lblBrutto.Text = f.Brutto
    End Sub

    Private Sub WyczyscFakture()
        FakturaId = 0
        lblNumer.Text = "(nie wystawiono)"
        lblStatus.Text = ""
        lblNetto.Text = "0,00"
        lblVat.Text = "0,00"
        lblBrutto.Text = "0,00"
        lblPlatnosc.Text = ""
        gvPozycje.DataSource = Nothing
        gvPozycje.DataBind()
    End Sub

    Private Sub WczytajFakturyDnia()
        Try
            gvFakturyDnia.DataSource = New InvoiceManager().FakturyZDnia(Date.Today)
            gvFakturyDnia.DataBind()
        Catch ex As Exception
            Log.Error("Blad pobierania faktur z dnia", ex)
        End Try
    End Sub

    Protected Sub btnWystaw_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnWystaw.Click
        If WypozyczenieId = 0 Then
            lblKomunikat.Text = "Nie wybrano wypożyczenia."
            Return
        End If
        Try
            Dim f As Wypozyczalnia.Core.Model.Faktura = New InvoiceManager().WystawFaktureZaWypozyczenie(WypozyczenieId)
            PokazFakture(f)
            btnWystaw.Enabled = False
            lblKomunikat.ForeColor = Drawing.Color.Green
            lblKomunikat.Text = "Wystawiono fakturę " & f.Numer
            WczytajFakturyDnia()
        Catch ex As BladBiznesowyException
            lblKomunikat.Text = ex.Message
        Catch ex As Exception
            Log.Error("Blad wystawiania faktury dla wypozyczenia " & WypozyczenieId, ex)
            lblKomunikat.Text = "Błąd wystawiania faktury: " & ex.Message
        End Try
    End Sub

    Protected Sub btnZaplac_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZaplac.Click
        If FakturaId = 0 Then
            lblKomunikat.Text = "Najpierw wystaw fakturę."
            Return
        End If
        Dim mgr As New InvoiceManager()
        Dim f As Wypozyczalnia.Core.Model.Faktura = mgr.PobierzFakture(FakturaId)
        If f Is Nothing Then
            lblKomunikat.Text = "Nie znaleziono faktury."
            Return
        End If
        If f.Status = "O" Then
            lblPlatnosc.Text = "Faktura jest już opłacona."
            Return
        End If

        Dim bramka As New BramkaPlatnosci.BramkaService()
        bramka.Timeout = 30000
        Try
            Dim wynik As BramkaPlatnosci.WynikPlatnosci = bramka.ZarejestrujPlatnosc(f.Numer, f.Brutto, Waluta)
            Log.Info("Bramka: faktura " & f.Numer & ", status " & wynik.Status & ", transakcja " & wynik.IdTransakcji)
            Select Case wynik.Status
                Case "OK"
                    mgr.OznaczOplacona(f.Id, wynik.IdTransakcji)
                    lblPlatnosc.ForeColor = Drawing.Color.Green
                    lblPlatnosc.Text = "Płatność przyjęta, transakcja " & wynik.IdTransakcji
                    Session("OstatnieWypozyczenieId") = Nothing
                    PokazFakture(mgr.PobierzFakture(f.Id))
                    WczytajFakturyDnia()
                Case "ODRZUCONA"
                    lblPlatnosc.ForeColor = Drawing.Color.Red
                    lblPlatnosc.Text = "Bramka odrzuciła płatność: " & wynik.Komunikat
                Case Else
                    lblPlatnosc.ForeColor = Drawing.Color.Red
                    lblPlatnosc.Text = "Błąd bramki: " & wynik.Komunikat
            End Select
        Catch ex As SoapException
            Log.Error("SOAP: blad bramki dla faktury " & f.Numer, ex)
            lblPlatnosc.Text = "Bramka zwróciła błąd: " & ex.Message
        Catch ex As WebException
            Log.Error("Brak polaczenia z bramka platnosci", ex)
            lblPlatnosc.Text = "Brak połączenia z bramką płatności. Spróbuj ponownie później."
        Finally
            bramka.Dispose()
        End Try
    End Sub

    Protected Sub btnEksportCsv_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEksportCsv.Click
        Try
            Dim plik As String = New EksportCsv().EksportujDzien(Date.Today)
            lblEksport.ForeColor = Drawing.Color.Green
            lblEksport.Text = "Zapisano: " & plik
        Catch ex As Exception
            Log.Error("Blad eksportu CSV z ekranu faktur", ex)
            lblEksport.ForeColor = Drawing.Color.Red
            lblEksport.Text = "Eksport nieudany: " & ex.Message
        End Try
    End Sub

    Private Function NazwaKlienta(ByVal k As Klient) As String
        If k.Typ = "F" AndAlso Not String.IsNullOrEmpty(k.NazwaFirmy) Then
            Return k.NazwaFirmy & " (" & k.Imie & " " & k.Nazwisko & ")"
        End If
        Return k.Imie & " " & k.Nazwisko
    End Function

End Class
