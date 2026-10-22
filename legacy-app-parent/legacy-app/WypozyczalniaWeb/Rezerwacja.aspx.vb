Option Strict Off
' Modul: Rezerwacja.aspx - formularz rezerwacji sprzetu | Autor: zespol IT | 2014-03-11
' Historia:
'   2014-09-22 JK  szacunek ceny przed zapisem (przycisk Sprawdz)
'   2016-05-30 MW  kalendarze AjaxControlToolkit zamiast pol tekstowych
'   2019-02-20 MW  anulowanie z listy rezerwacji klienta

Imports System.Data
Imports System.Globalization
Imports Wypozyczalnia.Core
Imports Wypozyczalnia.Core.Model

Partial Class Rezerwacja
    Inherits System.Web.UI.Page

    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Rezerwacja))

    Private Shared ReadOnly FormatyTerminu() As String = {"yyyy-MM-dd HH:mm", "yyyy-MM-dd H:mm", "dd.MM.yyyy HH:mm", "dd.MM.yyyy H:mm"}

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            PokazKlienta()
            WypelnijSprzet()
            WybierzSprzetZAdresu()
            txtDataOd.Text = Date.Today.AddDays(1).ToString("yyyy-MM-dd")
            txtDataDo.Text = Date.Today.AddDays(2).ToString("yyyy-MM-dd")
            If Session("SzacunekRezerwacji") IsNot Nothing Then
                lblSzacunek.Text = Session("SzacunekRezerwacji")
            End If
            PokazRezerwacje()
        End If
    End Sub

    Protected Sub ddlKategoria_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlKategoria.SelectedIndexChanged
        WypelnijSprzet()
        lblSzacunek.Text = "-"
        Session("SzacunekRezerwacji") = Nothing
    End Sub

    Protected Sub btnSprawdz_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSprawdz.Click
        Dim dataOd As Date
        Dim dataDo As Date
        If ddlSprzet.SelectedValue = "" Then
            PokazBlad("Wybierz sprzet.")
            Return
        End If
        If Not ParsujTermin(txtDataOd.Text, txtGodzinaOd.Text, dataOd) OrElse Not ParsujTermin(txtDataDo.Text, txtGodzinaDo.Text, dataDo) Then
            PokazBlad("Niepoprawny termin. Format: rrrr-mm-dd gg:mm")
            Return
        End If
        If dataDo <= dataOd Then
            PokazBlad("Data zakonczenia musi byc pozniejsza niz data rozpoczecia.")
            Return
        End If

        Dim sprzetId As Integer = ddlSprzet.SelectedValue
        Dim mgr As New EquipmentManager()
        If Not mgr.SprawdzDostepnosc(sprzetId, dataOd, dataDo) Then
            PokazBlad("Sprzet niedostepny w wybranym terminie: " & OpisKolizji(mgr.KolizjeWTerminie(sprzetId, dataOd, dataDo)))
            lblSzacunek.Text = "-"
            Session("SzacunekRezerwacji") = Nothing
            Return
        End If

        Dim sprzet As Sprzet = mgr.PobierzSprzet(sprzetId)
        Dim klientId As Integer = Session("WybranyKlientId")
        Dim prog As Integer = 0
        If klientId > 0 Then
            prog = New CustomerManager().ProgRabatu(klientId)
        End If
        Dim szacunek As Decimal = ObliczSzacunek(sprzet, dataOd, dataDo, prog)
        lblSzacunek.Text = szacunek.ToString("N2")
        Session("SzacunekRezerwacji") = szacunek
        PokazInfo("Sprzet dostepny w wybranym terminie.")
    End Sub

    Protected Sub btnZarezerwuj_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZarezerwuj.Click
        Dim klientId As Integer = Session("WybranyKlientId")
        If klientId = 0 Then
            PokazBlad("Najpierw wybierz klienta na stronie Klienci.")
            Return
        End If
        If ddlSprzet.SelectedValue = "" Then
            PokazBlad("Wybierz sprzet.")
            Return
        End If

        Dim dataOd As Date
        Dim dataDo As Date
        If Not ParsujTermin(txtDataOd.Text, txtGodzinaOd.Text, dataOd) Then
            PokazBlad("Niepoprawna data rozpoczecia.")
            Return
        End If
        If Not ParsujTermin(txtDataDo.Text, txtGodzinaDo.Text, dataDo) Then
            PokazBlad("Niepoprawna data zakonczenia.")
            Return
        End If
        If dataOd < Date.Now.AddMinutes(-30) Then
            PokazBlad("Nie mozna rezerwowac wstecz.")
            Return
        End If
        If dataDo <= dataOd Then
            PokazBlad("Data zakonczenia musi byc pozniejsza niz data rozpoczecia.")
            Return
        End If
        If dataOd.Hour < 7 OrElse dataOd.Hour > 18 Then
            PokazBlad("Wydania sprzetu tylko w godzinach 7:00 - 18:00.")
            Return
        End If
        If txtUwagi.Text.Length > 500 Then
            txtUwagi.Text = txtUwagi.Text.Substring(0, 500)
        End If

        Dim rezerwacje As New ReservationManager()
        Try
            Dim noweId As Integer = rezerwacje.UtworzRezerwacje(klientId, ddlSprzet.SelectedValue, dataOd, dataDo, txtUwagi.Text)
            Dim nowa = rezerwacje.PobierzRezerwacje(noweId)
            PokazInfo("Zapisano rezerwacje nr " & noweId & ". Cena: " & nowa.CenaSzacunkowa.ToString("N2") & " zl.")
            txtUwagi.Text = ""
            PokazRezerwacje()
        Catch ex As BladBiznesowyException
            PokazBlad(ex.Message)
        Catch ex As ArgumentException
            PokazBlad(ex.Message)
        Catch ex As Exception
            Log.Error("Blad zapisu rezerwacji, klient " & klientId, ex)
            PokazBlad("Nie udalo sie zapisac rezerwacji. Sprobuj ponownie.")
        End Try
    End Sub

    Protected Sub gvRezerwacje_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gvRezerwacje.SelectedIndexChanged
        If gvRezerwacje.SelectedIndex < 0 Then
            Return
        End If
        ViewState("WybranaRezerwacja") = gvRezerwacje.SelectedDataKey.Value
        Dim rezerwacje As New ReservationManager()
        Dim wybrana = rezerwacje.PobierzRezerwacje(gvRezerwacje.SelectedDataKey.Value)
        If wybrana Is Nothing Then
            PokazBlad("Rezerwacja nie istnieje (odswiez liste).")
            btnAnuluj.Enabled = False
            Return
        End If
        btnAnuluj.Enabled = wybrana.CzyAktywna
        Dim opis As String = rezerwacje.OpisRezerwacji(wybrana.Id)
        If Not String.IsNullOrEmpty(wybrana.Uwagi) Then
            opis = opis & ". Uwagi: " & wybrana.Uwagi
        End If
        PokazInfo(opis)
    End Sub

    Protected Sub gvRezerwacje_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvRezerwacje.RowDataBound
        If e.Row.RowType <> DataControlRowType.DataRow Then
            Return
        End If
        Dim wiersz As DataRowView = CType(e.Row.DataItem, DataRowView)
        Dim komorkaStatusu As TableCell = e.Row.Cells(5)
        Select Case CStr(wiersz("Status"))
            Case "N"
                komorkaStatusu.Text = "nowa"
            Case "P"
                komorkaStatusu.Text = "potwierdzona"
                komorkaStatusu.Font.Bold = True
            Case "A"
                komorkaStatusu.Text = "anulowana"
                e.Row.CssClass = "wyszarzony"
            Case "Z"
                komorkaStatusu.Text = "zrealizowana"
                e.Row.CssClass = "wyszarzony"
        End Select
        If CDate(wiersz("DataDo")) < Date.Now AndAlso CStr(wiersz("Status")) = "N" Then
            komorkaStatusu.Text = komorkaStatusu.Text & " (termin minal)"
        End If
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PreRender
        ceDataOd.StartDate = Date.Today
        ceDataDo.StartDate = Date.Today
        btnSprawdz.Enabled = ddlSprzet.Items.Count > 1
    End Sub

    ' Rezerwacja.aspx?sprzet=<Id> - link "rezerwuj" z katalogu sprzetu
    Private Sub WybierzSprzetZAdresu()
        Dim parametr As String = Request.QueryString("sprzet")
        Dim sprzetId As Integer
        If String.IsNullOrEmpty(parametr) OrElse Not Integer.TryParse(parametr, sprzetId) Then
            Return
        End If
        Dim sprzet As Sprzet = New EquipmentManager().PobierzSprzet(sprzetId)
        If sprzet Is Nothing Then
            Return
        End If
        Dim pozycjaKategorii As ListItem = ddlKategoria.Items.FindByValue(sprzet.Kategoria)
        If pozycjaKategorii IsNot Nothing Then
            ddlKategoria.ClearSelection()
            pozycjaKategorii.Selected = True
            WypelnijSprzet()
        End If
        Dim pozycja As ListItem = ddlSprzet.Items.FindByValue(sprzetId.ToString())
        If pozycja IsNot Nothing Then
            ddlSprzet.ClearSelection()
            pozycja.Selected = True
        End If
    End Sub

    Protected Sub btnAnuluj_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAnuluj.Click
        If ViewState("WybranaRezerwacja") Is Nothing Then
            PokazBlad("Wybierz rezerwacje z listy.")
            Return
        End If
        Dim rezerwacjaId As Integer = ViewState("WybranaRezerwacja")
        Try
            Dim rezerwacje As New ReservationManager()
            rezerwacje.Anuluj(rezerwacjaId)
            PokazInfo("Rezerwacja nr " & rezerwacjaId & " anulowana.")
            ViewState("WybranaRezerwacja") = Nothing
            gvRezerwacje.SelectedIndex = -1
            PokazRezerwacje()
        Catch ex As BladBiznesowyException
            PokazBlad(ex.Message)
        End Try
    End Sub

    Private Sub PokazKlienta()
        Dim klientId As Integer = Session("WybranyKlientId")
        If klientId = 0 Then
            lblKlient.Text = "(nie wybrano - wybierz klienta na stronie Klienci)"
            btnZarezerwuj.Enabled = False
            Return
        End If
        Dim klient As Klient = New CustomerManager().PobierzKlienta(klientId)
        If klient Is Nothing Then
            lblKlient.Text = "(nieznany klient " & klientId & ")"
            btnZarezerwuj.Enabled = False
            Return
        End If
        If klient.Typ = "F" AndAlso Not String.IsNullOrEmpty(klient.NazwaFirmy) Then
            lblKlient.Text = Server.HtmlEncode(klient.NazwaFirmy & " (" & klient.Imie & " " & klient.Nazwisko & ")")
        Else
            lblKlient.Text = Server.HtmlEncode(klient.Imie & " " & klient.Nazwisko)
        End If
        If klient.Zablokowany Then
            lblKlient.Text = lblKlient.Text & " - ZABLOKOWANY"
            lblKlient.CssClass = "blad"
        End If
        btnZarezerwuj.Enabled = True
    End Sub

    Private Sub WypelnijSprzet()
        ddlSprzet.Items.Clear()
        ddlSprzet.Items.Add(New ListItem("-- wybierz --", ""))
        Dim mgr As New EquipmentManager()
        For Each s In mgr.ListaWKategorii(ddlKategoria.SelectedValue)
            Dim opis As String = s.NumerInwentarzowy & " - " & s.Nazwa & " (" & s.StawkaDobowa.ToString("N2") & " zl/doba)"
            If s.Status = "S" Then
                opis = opis & " [serwis]"
            End If
            ddlSprzet.Items.Add(New ListItem(opis, s.Id.ToString()))
        Next
    End Sub

    Private Sub PokazRezerwacje()
        Dim klientId As Integer = Session("WybranyKlientId")
        If klientId = 0 Then
            gvRezerwacje.DataSource = Nothing
            gvRezerwacje.DataBind()
            btnAnuluj.Enabled = False
            Return
        End If
        Dim tabela As DataTable = New ReservationManager().RezerwacjeKlienta(klientId)
        gvRezerwacje.DataSource = tabela
        gvRezerwacje.DataBind()
        btnAnuluj.Enabled = tabela.Rows.Count > 0
    End Sub

    Private Function OpisKolizji(ByVal kolizje As DataTable) As String
        Dim opisy As New List(Of String)()
        For Each wiersz As DataRow In kolizje.Rows
            Select Case CStr(wiersz("Zrodlo"))
                Case "R"
                    opisy.Add("rezerwacja nr " & wiersz("Id") & " (" & CDate(wiersz("DataOd")).ToString("yyyy-MM-dd HH:mm") & _
                              " - " & CDate(wiersz("DataDo")).ToString("yyyy-MM-dd HH:mm") & ")")
                Case "W"
                    opisy.Add("wypozyczenie nr " & wiersz("Id") & " do " & CDate(wiersz("DataDo")).ToString("yyyy-MM-dd HH:mm"))
                Case "S"
                    opisy.Add("sprzet w serwisie albo wycofany")
                Case Else
                    opisy.Add("kolizja " & wiersz("Zrodlo"))
            End Select
        Next
        Return String.Join("; ", opisy.ToArray())
    End Function

    Private Function ParsujTermin(ByVal data As String, ByVal godzina As String, ByRef wynik As Date) As Boolean
        If String.IsNullOrEmpty(data) Then
            Return False
        End If
        If String.IsNullOrEmpty(godzina) Then
            godzina = "08:00"
        End If
        Return Date.TryParseExact(data.Trim() & " " & godzina.Trim(), FormatyTerminu, CultureInfo.InvariantCulture, DateTimeStyles.None, wynik)
    End Function

    Private Sub PokazBlad(ByVal tekst As String)
        lblKomunikat.CssClass = "blad"
        lblKomunikat.Text = Server.HtmlEncode(tekst)
    End Sub

    Private Sub PokazInfo(ByVal tekst As String)
        lblKomunikat.CssClass = "info"
        lblKomunikat.Text = Server.HtmlEncode(tekst)
    End Sub

    Private Function ObliczSzacunek(ByVal sprzet As Sprzet, ByVal dataOd As Date, ByVal dataDo As Date, ByVal prog As Integer) As Decimal
        Dim dni As Long = DateDiff(DateInterval.Day, dataOd.Date, dataDo.Date)
        If dni < 1 Then dni = 1
        Dim kwota As Decimal = dni * sprzet.StawkaDobowa
        kwota = kwota * (100D - prog * 2.5D) / 100D
        Return Math.Round(kwota, 2, MidpointRounding.AwayFromZero)
    End Function

End Class
