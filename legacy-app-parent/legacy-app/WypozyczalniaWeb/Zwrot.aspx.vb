Option Strict Off

' Modul: Zwrot.aspx.vb | Autor: zespol IT | 2014-03-11
' Przyjecie zwrotu sprzetu: kara za spoznienie, szkody, rozliczenie kaucji.
' 2014-09-30 JK: szkody z listy zamiast pola tekstowego
' 2017-05-16 MW: link do faktury po zwrocie

Imports System.Data
Imports System.Globalization
Imports Wypozyczalnia.Core
Imports Wypozyczalnia.Core.Model

Partial Class Zwrot
    Inherits System.Web.UI.Page

    Protected WithEvents lblKomunikat As Global.System.Web.UI.WebControls.Label
    Protected WithEvents ddlOtwarte As Global.System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblSzczegoly As Global.System.Web.UI.WebControls.Label
    Protected WithEvents txtDataZwrotu As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents cblSzkody As Global.System.Web.UI.WebControls.CheckBoxList
    Protected WithEvents btnPodglad As Global.System.Web.UI.WebControls.Button
    Protected WithEvents btnZwrot As Global.System.Web.UI.WebControls.Button
    Protected WithEvents lblOplata As Global.System.Web.UI.WebControls.Label
    Protected WithEvents lblKara As Global.System.Web.UI.WebControls.Label
    Protected WithEvents lblKaucjaZwrot As Global.System.Web.UI.WebControls.Label
    Protected WithEvents lnkFaktura As Global.System.Web.UI.WebControls.HyperLink
    Protected WithEvents gvOtwarte As Global.System.Web.UI.WebControls.GridView

    Private Const FormatDaty As String = "yyyy-MM-dd HH:mm"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            WypelnijSzkody()
            WczytajOtwarte()
            WyczyscRozliczenie()
            Dim zAdresu As String = Request.QueryString("id")
            If Not String.IsNullOrEmpty(zAdresu) AndAlso ddlOtwarte.Items.FindByValue(zAdresu) IsNot Nothing Then
                ddlOtwarte.SelectedValue = zAdresu
                ddlOtwarte_SelectedIndexChanged(ddlOtwarte, EventArgs.Empty)
            End If
        End If
    End Sub

    Protected Sub gvOtwarte_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvOtwarte.RowDataBound
        If e.Row.RowType <> DataControlRowType.DataRow Then Return
        Dim wiersz As DataRowView = e.Row.DataItem
        Dim plan As Date = wiersz("PlanowanyZwrot")
        If plan < Date.Now.AddHours(-1) Then
            e.Row.BackColor = Drawing.Color.MistyRose
            e.Row.ToolTip = "Po terminie zwrotu"
        ElseIf plan < Date.Now.AddHours(24) Then
            e.Row.BackColor = Drawing.Color.LightYellow
            e.Row.ToolTip = "Zwrot w ciagu doby"
        End If
    End Sub

    Protected Sub gvOtwarte_SelectedIndexChanging(ByVal sender As Object, ByVal e As GridViewSelectEventArgs) Handles gvOtwarte.SelectedIndexChanging
        Dim id As String = gvOtwarte.Rows(e.NewSelectedIndex).Cells(0).Text
        If ddlOtwarte.Items.FindByValue(id) IsNot Nothing Then
            ddlOtwarte.SelectedValue = id
            ddlOtwarte_SelectedIndexChanged(ddlOtwarte, EventArgs.Empty)
        End If
    End Sub

    Private Sub WypelnijSzkody()
        cblSzkody.Items.Clear()
        cblSzkody.Items.Add(New ListItem("Uszkodzona obudowa (50 zł)", "Uszkodzona obudowa"))
        cblSzkody.Items.Add(New ListItem("Brak akcesoriów (30 zł)", "Brak akcesoriów"))
        cblSzkody.Items.Add(New ListItem("Zabrudzenie (20 zł)", "Zabrudzenie"))
    End Sub

    Private Sub WczytajOtwarte()
        Dim tabela As DataTable = New RentalManager().Otwarte()
        ddlOtwarte.Items.Clear()
        ddlOtwarte.Items.Add(New ListItem("-- wybierz --", "0"))
        For Each wiersz As DataRow In tabela.Rows
            Dim plan As Date = wiersz("PlanowanyZwrot")
            Dim tekst As String = "#" & wiersz("Id") & " " & wiersz("Klient") & " | " & wiersz("Sprzet") & " | do " & plan.ToString(FormatDaty)
            If plan < Date.Now Then
                tekst = tekst & " (po terminie)"
            End If
            ddlOtwarte.Items.Add(New ListItem(tekst, wiersz("Id").ToString()))
        Next
        gvOtwarte.DataSource = tabela
        gvOtwarte.DataBind()
    End Sub

    Private Sub WyczyscRozliczenie()
        lblOplata.Text = "-"
        lblKara.Text = "-"
        lblKaucjaZwrot.Text = "-"
        lnkFaktura.Visible = False
    End Sub

    Protected Sub ddlOtwarte_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlOtwarte.SelectedIndexChanged
        WyczyscRozliczenie()
        For Each item As ListItem In cblSzkody.Items
            item.Selected = False
        Next
        Dim id As Integer = ddlOtwarte.SelectedValue
        If id = 0 Then
            lblSzczegoly.Text = "-"
            Return
        End If
        Dim w As Wypozyczenie = New RentalManager().PobierzWypozyczenie(id)
        If w Is Nothing Then
            lblSzczegoly.Text = "-"
            lblKomunikat.Text = "Nie znaleziono wypozyczenia " & id
            Return
        End If
        Dim s As Sprzet = New EquipmentManager().PobierzSprzet(w.SprzetId)
        Dim opis As String = "Wydano " & w.DataWydania.ToString(FormatDaty) & ", zwrot do " & w.PlanowanyZwrot.ToString(FormatDaty) &
            ", kaucja " & w.KaucjaPobrana.ToString("N2") & " zł"
        If s IsNot Nothing Then
            opis = s.NumerInwentarzowy & " " & s.Nazwa & " (" & s.StawkaDobowa.ToString("N2") & " zł/doba). " & opis
        End If
        If Not String.IsNullOrEmpty(w.NumerPrzesylki) Then
            opis = opis & ", przesylka " & w.NumerPrzesylki
        End If
        lblSzczegoly.Text = Server.HtmlEncode(opis)
        If txtDataZwrotu.Text = "" Then
            txtDataZwrotu.Text = Date.Now.ToString(FormatDaty)
        End If
    End Sub

    Private Function OpisSpoznienia(ByVal plan As Date, ByVal zwrot As Date) As String
        Dim minuty As Long = DateDiff(DateInterval.Minute, plan, zwrot)
        If minuty <= 0 Then
            Return "zwrot w terminie"
        End If
        If minuty <= 60 Then
            Return "spoznienie " & minuty & " min (w tolerancji)"
        End If
        Dim godziny As Long = minuty \ 60
        Dim reszta As Long = minuty Mod 60
        If godziny < 24 Then
            Return "spoznienie " & godziny & " h " & reszta & " min"
        End If
        Return "spoznienie " & (godziny \ 24) & " d " & (godziny Mod 24) & " h " & reszta & " min"
    End Function

    Private Function OdczytajDateZwrotu(ByRef dataZwrotu As Date) As Boolean
        Dim tekst As String = txtDataZwrotu.Text.Trim()
        If tekst = "" Then
            dataZwrotu = Date.Now
            Return True
        End If
        If Date.TryParseExact(tekst, FormatDaty, CultureInfo.InvariantCulture, DateTimeStyles.None, dataZwrotu) Then
            Return True
        End If
        Return Date.TryParse(tekst, dataZwrotu)
    End Function

    Protected Sub btnPodglad_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPodglad.Click
        Dim id As Integer = ddlOtwarte.SelectedValue
        If id = 0 Then
            lblKomunikat.Text = "Wybierz wypozyczenie."
            Return
        End If
        Dim dataZwrotu As Date
        If Not OdczytajDateZwrotu(dataZwrotu) Then
            lblKomunikat.Text = "Niepoprawna data zwrotu."
            Return
        End If
        Dim manager As New RentalManager()
        Dim w As Wypozyczenie = manager.PobierzWypozyczenie(id)
        If w Is Nothing Then
            lblKomunikat.Text = "Nie znaleziono wypozyczenia " & id
            Return
        End If
        Dim s As Sprzet = New EquipmentManager().PobierzSprzet(w.SprzetId)
        Dim kara As Decimal = manager.ObliczKare(w.PlanowanyZwrot, dataZwrotu, s.StawkaDobowa)
        Dim szkody As Decimal = 0
        For Each item As ListItem In cblSzkody.Items
            If item.Selected Then
                Select Case item.Value
                    Case "Uszkodzona obudowa"
                        szkody = szkody + 50
                    Case "Brak akcesoriów"
                        szkody = szkody + 30
                    Case "Zabrudzenie"
                        szkody = szkody + 20
                End Select
            End If
        Next
        Dim kaucja As Decimal = w.KaucjaPobrana - kara - szkody
        If kaucja < 0 Then kaucja = 0
        lblOplata.Text = "(przy zwrocie)"
        lblKara.Text = kara
        lblKaucjaZwrot.Text = kaucja
        lblKomunikat.ForeColor = Drawing.Color.Navy
        lblKomunikat.Text = "Podglad (" & OpisSpoznienia(w.PlanowanyZwrot, dataZwrotu) & ") - zwrot nie zostal jeszcze zapisany."
    End Sub

    Protected Sub btnZwrot_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZwrot.Click
        Dim id As Integer = ddlOtwarte.SelectedValue
        If id = 0 Then
            lblKomunikat.Text = "Wybierz wypozyczenie."
            Return
        End If

        Dim dataZwrotu As Date
        If Not OdczytajDateZwrotu(dataZwrotu) Then
            lblKomunikat.Text = "Niepoprawna data zwrotu."
            Return
        End If
        If dataZwrotu > Date.Now.AddMinutes(5) Then
            lblKomunikat.Text = "Data zwrotu nie moze byc z przyszlosci."
            Return
        End If

        Dim manager As New RentalManager()
        Dim przed As Wypozyczenie = manager.PobierzWypozyczenie(id)
        If przed Is Nothing Then
            lblKomunikat.Text = "Nie znaleziono wypozyczenia " & id
            Return
        End If
        If dataZwrotu < przed.DataWydania Then
            lblKomunikat.Text = "Data zwrotu wczesniejsza niz data wydania (" & przed.DataWydania.ToString(FormatDaty) & ")."
            Return
        End If

        ' slownik szkod: kwoty uzgodnione z kierownikiem punktu (cennik 2014)
        Dim cennikSzkod As New Dictionary(Of String, Decimal)
        cennikSzkod.Add("Uszkodzona obudowa", 50D)
        cennikSzkod.Add("Brak akcesoriów", 30D)
        cennikSzkod.Add("Zabrudzenie", 20D)

        Dim szkody As Decimal = 0
        Dim opisSzkod As New List(Of String)
        For Each item As ListItem In cblSzkody.Items
            If item.Selected AndAlso cennikSzkod.ContainsKey(item.Value) Then
                szkody = szkody + cennikSzkod(item.Value)
                opisSzkod.Add(item.Value)
            End If
        Next
        If szkody > przed.KaucjaPobrana Then
            lblKomunikat.Text = "Uwaga: szkody przekraczaja kaucje - roznica trafi na fakture."
        End If

        Dim po As Wypozyczenie
        Try
            po = manager.ZarejestrujZwrot(id, dataZwrotu, szkody)
        Catch ex As BladBiznesowyException
            lblKomunikat.Text = ex.Message
            Return
        Catch ex As Exception
            lblKomunikat.Text = "Blad rejestracji zwrotu: " & ex.Message
            Return
        End Try

        lblOplata.Text = po.Oplata
        lblKara.Text = po.Kara
        lblKaucjaZwrot.Text = po.KaucjaZwrocona

        Session("OstatnieWypozyczenieId") = po.Id
        lnkFaktura.NavigateUrl = "Faktura.aspx?id=" & po.Id
        lnkFaktura.Visible = True

        Dim komunikat As String = "Zwrot zarejestrowany (wypozyczenie nr " & po.Id & ")."
        If po.Kara > 0 Then
            komunikat = komunikat & " Naliczono kare za spoznienie (" & OpisSpoznienia(po.PlanowanyZwrot, dataZwrotu) & ")."
        End If
        If opisSzkod.Count > 0 Then
            komunikat = komunikat & " Szkody: " & String.Join(", ", opisSzkod.ToArray()) & "."
        End If
        If po.KaucjaZwrocona = 0 AndAlso po.KaucjaPobrana > 0 Then
            komunikat = komunikat & " Kaucja w calosci zatrzymana."
        End If
        lblKomunikat.ForeColor = Drawing.Color.Green
        lblKomunikat.Text = Server.HtmlEncode(komunikat)

        txtDataZwrotu.Text = ""
        For Each item As ListItem In cblSzkody.Items
            item.Selected = False
        Next
        lblSzczegoly.Text = "-"
        WczytajOtwarte()
    End Sub

End Class
