' Modul: Klienci.aspx.vb - lista, wyszukiwanie i edycja klientow | Autor: zespol IT | 2014-03-11
' Historia: 2014-03-11 JK utworzenie strony
'           2015-11-23 MW blokowanie klientow, przejscie do rezerwacji
'           2018-09-04 PT pole NIP i typ klienta (firmy)
Imports System.Data
Imports System.Data.SqlClient
Imports Wypozyczalnia.Core
Imports Wypozyczalnia.Core.Model
Imports Wypozyczalnia.Core.DAL

Partial Class Klienci
    Inherits System.Web.UI.Page

    Protected WithEvents lblKomunikat As Global.System.Web.UI.WebControls.Label
    Protected WithEvents txtSzukaj As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSzukaj As Global.System.Web.UI.WebControls.Button
    Protected WithEvents btnNowy As Global.System.Web.UI.WebControls.Button
    Protected WithEvents gvKlienci As Global.System.Web.UI.WebControls.GridView
    Protected WithEvents lblTrybFormularza As Global.System.Web.UI.WebControls.Label
    Protected WithEvents hfKlientId As Global.System.Web.UI.WebControls.HiddenField
    Protected WithEvents txtImie As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNazwisko As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNazwaFirmy As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmail As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTelefon As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNumerDokumentu As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNIP As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlTyp As Global.System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlProg As Global.System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtPowodBlokady As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents btnZapisz As Global.System.Web.UI.WebControls.Button
    Protected WithEvents btnZablokuj As Global.System.Web.UI.WebControls.Button

    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Klienci))

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            WyczyscFormularz()
            ZaladujListe("")
            Dim wybrany As Integer = Session("WybranyKlientId")
            If wybrany > 0 Then
                lblKomunikat.Text = "Aktualnie wybrany klient: nr " & wybrany
                lblKomunikat.ForeColor = Drawing.Color.DarkGreen
            End If
        End If
    End Sub

    Protected Sub btnSzukaj_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSzukaj.Click
        Dim sql As String = "SELECT Id, Imie, Nazwisko, NazwaFirmy, Email, Telefon FROM Klienci WHERE Nazwisko LIKE '%" & txtSzukaj.Text & "%' ORDER BY Nazwisko"
        Try
            Dim dt As DataTable = DbHelper.WykonajZapytanie(sql)
            gvKlienci.PageIndex = 0
            gvKlienci.DataSource = dt
            gvKlienci.DataBind()
            ViewState("OstatniaFraza") = txtSzukaj.Text
            If dt.Rows.Count = 0 Then
                lblKomunikat.Text = "Nie znaleziono klientow."
            End If
        Catch ex As SqlException
            Log.Error("Blad wyszukiwania klientow", ex)
            lblKomunikat.Text = "Blad bazy danych: " & ex.Message
        End Try
    End Sub

    Protected Sub btnNowy_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNowy.Click
        WyczyscFormularz()
        gvKlienci.SelectedIndex = -1
    End Sub

    Protected Sub gvKlienci_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gvKlienci.SelectedIndexChanged
        Dim klientId As Integer = gvKlienci.SelectedDataKey.Value
        Dim cm As New CustomerManager()
        If Not cm.CzyMozeWypozyczac(klientId) Then
            Dim k As Klient = cm.PobierzKlienta(klientId)
            lblKomunikat.Text = "Klient zablokowany: " & If(k Is Nothing, "", k.PowodBlokady)
            Return
        End If
        Session("WybranyKlientId") = klientId
        Log.Debug("Wybrano klienta " & klientId & " w sesji " & Session.SessionID)
        Response.Redirect("Rezerwacja.aspx")
    End Sub

    Protected Sub gvKlienci_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles gvKlienci.RowCommand
        If e.CommandName <> "Edytuj" Then Return
        Dim indeks As Integer = Convert.ToInt32(e.CommandArgument)
        Dim klientId As Integer = gvKlienci.DataKeys(indeks).Value
        WczytajDoFormularza(klientId)
    End Sub

    Protected Sub gvKlienci_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvKlienci.PageIndexChanging
        gvKlienci.PageIndex = e.NewPageIndex
        ZaladujListe(ViewState("OstatniaFraza"))
    End Sub

    Protected Sub btnZapisz_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZapisz.Click
        lblKomunikat.Text = ""
        If txtEmail.Text.Trim() = "" OrElse Not txtEmail.Text.Contains("@") Then
            lblKomunikat.Text = "Niepoprawny adres e-mail."
            Return
        End If
        Dim blad As String = WalidujFormularz()
        If blad <> "" Then
            lblKomunikat.Text = blad
            Return
        End If

        Dim k As New Klient()
        k.Id = CInt(hfKlientId.Value)
        k.Imie = txtImie.Text.Trim()
        k.Nazwisko = txtNazwisko.Text.Trim()
        k.NazwaFirmy = txtNazwaFirmy.Text.Trim()
        k.Email = txtEmail.Text.Trim()
        k.Telefon = txtTelefon.Text.Trim()
        k.NumerDokumentu = txtNumerDokumentu.Text.Trim().ToUpper()
        k.NIP = txtNIP.Text.Replace("-", "").Trim()
        k.Typ = ddlTyp.SelectedValue
        k.ProgRabatu = ddlProg.SelectedValue
        If k.Typ = "I" Then
            k.NIP = Nothing
            If k.NazwaFirmy = "" Then k.NazwaFirmy = Nothing
        End If

        Try
            Dim cm As New CustomerManager()
            Dim noweId As Integer = cm.Zapisz(k)
            hfKlientId.Value = noweId
            lblTrybFormularza.Text = "Edycja klienta nr " & noweId
            lblKomunikat.Text = "Zapisano klienta nr " & noweId
            lblKomunikat.ForeColor = Drawing.Color.DarkGreen
            ZaladujListe(ViewState("OstatniaFraza"))
        Catch ex As BladBiznesowyException
            lblKomunikat.Text = ex.Message
        Catch ex As SqlException
            Log.Error("Blad zapisu klienta", ex)
            lblKomunikat.Text = "Nie udalo sie zapisac klienta: " & ex.Message
        End Try
    End Sub

    Protected Sub btnZablokuj_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZablokuj.Click
        Dim klientId As Integer = CInt(hfKlientId.Value)
        If klientId = 0 Then
            lblKomunikat.Text = "Najpierw wybierz klienta do edycji."
            Return
        End If
        Dim powod As String = txtPowodBlokady.Text.Trim()
        If powod = "" Then powod = "blokada recznie z ekranu Klienci"
        Dim cm As New CustomerManager()
        cm.Zablokuj(klientId, powod)
        If Session("WybranyKlientId") = klientId Then
            Session("WybranyKlientId") = 0
        End If
        lblKomunikat.Text = "Klient nr " & klientId & " zostal zablokowany."
        ZaladujListe(ViewState("OstatniaFraza"))
    End Sub

    Private Sub ZaladujListe(ByVal fraza As String)
        Try
            Dim cm As New CustomerManager()
            gvKlienci.DataSource = cm.Szukaj(fraza)
            gvKlienci.DataBind()
        Catch ex As SqlException
            Log.Error("Blad ladowania listy klientow", ex)
            lblKomunikat.Text = "Blad bazy danych."
        End Try
    End Sub

    Private Sub WczytajDoFormularza(ByVal klientId As Integer)
        Dim cm As New CustomerManager()
        Dim k As Klient = cm.PobierzKlienta(klientId)
        If k Is Nothing Then
            lblKomunikat.Text = "Nie znaleziono klienta nr " & klientId
            Return
        End If
        hfKlientId.Value = k.Id
        txtImie.Text = k.Imie
        txtNazwisko.Text = k.Nazwisko
        txtNazwaFirmy.Text = k.NazwaFirmy
        txtEmail.Text = k.Email
        txtTelefon.Text = k.Telefon
        txtNumerDokumentu.Text = k.NumerDokumentu
        txtNIP.Text = k.NIP
        UstawWybor(ddlTyp, k.Typ)
        UstawWybor(ddlProg, k.ProgRabatu.ToString())
        txtPowodBlokady.Text = k.PowodBlokady
        lblTrybFormularza.Text = "Edycja klienta nr " & k.Id
        If k.Zablokowany Then
            lblTrybFormularza.Text = lblTrybFormularza.Text & " (ZABLOKOWANY)"
            btnZablokuj.Enabled = False
        Else
            btnZablokuj.Enabled = True
        End If
    End Sub

    Private Function WalidujFormularz() As String
        If txtTelefon.Text.Trim() = "" Then
            Return "Telefon jest wymagany."
        End If
        If txtTelefon.Text.Trim().Length > 20 Then
            Return "Telefon moze miec maksymalnie 20 znakow."
        End If
        If ddlTyp.SelectedValue = "F" Then
            If txtNazwaFirmy.Text.Trim() = "" Then
                Return "Dla firmy podaj nazwe firmy."
            End If
            Dim nip As String = txtNIP.Text.Replace("-", "").Replace(" ", "").Trim()
            If nip.Length <> 10 OrElse Not IsNumeric(nip) Then
                Return "NIP musi miec 10 cyfr."
            End If
        End If
        If ddlTyp.SelectedValue = "I" AndAlso txtNumerDokumentu.Text.Trim() = "" Then
            Return "Dla osoby prywatnej podaj numer dokumentu."
        End If
        Return ""
    End Function

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PreRender
        Dim wybrany As Integer = Session("WybranyKlientId")
        If wybrany > 0 AndAlso gvKlienci.Rows.Count > 0 Then
            For i As Integer = 0 To gvKlienci.Rows.Count - 1
                If gvKlienci.DataKeys(i).Value = wybrany Then
                    gvKlienci.Rows(i).Font.Bold = True
                End If
            Next
        End If
    End Sub

    Private Sub UstawWybor(ByVal lista As DropDownList, ByVal wartosc As String)
        Dim pozycja As ListItem = lista.Items.FindByValue(wartosc)
        If pozycja IsNot Nothing Then
            lista.ClearSelection()
            pozycja.Selected = True
        End If
    End Sub

    Private Sub WyczyscFormularz()
        hfKlientId.Value = "0"
        txtImie.Text = ""
        txtNazwisko.Text = ""
        txtNazwaFirmy.Text = ""
        txtEmail.Text = ""
        txtTelefon.Text = ""
        txtNumerDokumentu.Text = ""
        txtNIP.Text = ""
        txtPowodBlokady.Text = ""
        ddlTyp.SelectedIndex = 0
        ddlProg.SelectedIndex = 0
        btnZablokuj.Enabled = False
        lblTrybFormularza.Text = "Nowy klient"
    End Sub

End Class
