' Modul: Wypozyczenie.aspx.vb | Autor: zespol IT | 2014-03-11
' Wydanie sprzetu klientowi (z rezerwacji albo od reki), opcjonalnie dostawa kurierem.
' 2015-06-22 MW: dostawa kurierem (KurierClient)
' 2018-02-09 PT: lista wydan i zwrotow z dnia na dole strony

Imports System.Data
Imports System.Globalization
Imports Wypozyczalnia.Core
Imports Wypozyczalnia.Core.Model

Partial Class Wypozyczenie
    Inherits System.Web.UI.Page

    Protected WithEvents lblKlient As Global.System.Web.UI.WebControls.Label
    Protected WithEvents lblKomunikat As Global.System.Web.UI.WebControls.Label
    Protected WithEvents ddlRezerwacje As Global.System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlSprzet As Global.System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtPlanowanyZwrot As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvPlanowanyZwrot As Global.System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblKaucja As Global.System.Web.UI.WebControls.Label
    Protected WithEvents chkDostawa As Global.System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtAdres As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents btnWydaj As Global.System.Web.UI.WebControls.Button
    Protected WithEvents lblNumerPrzesylki As Global.System.Web.UI.WebControls.Label
    Protected WithEvents gvWydaneDzis As Global.System.Web.UI.WebControls.GridView

    Private Const FormatDaty As String = "yyyy-MM-dd HH:mm"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim klientId As Integer = Session("WybranyKlientId")
            If klientId = 0 Then
                lblKomunikat.Text = "Najpierw wybierz klienta na stronie Klienci."
                btnWydaj.Enabled = False
            Else
                PokazKlienta(klientId)
                WczytajRezerwacje(klientId)
            End If
            WczytajSprzet()
            WczytajRuchZDnia()
            Dim zAdresu As String = Request.QueryString("rezerwacja")
            If Not String.IsNullOrEmpty(zAdresu) AndAlso ddlRezerwacje.Items.FindByValue(zAdresu) IsNot Nothing Then
                ddlRezerwacje.SelectedValue = zAdresu
                ddlRezerwacje_SelectedIndexChanged(ddlRezerwacje, EventArgs.Empty)
            End If
        End If
    End Sub

    Protected Sub gvWydaneDzis_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvWydaneDzis.RowDataBound
        If e.Row.RowType <> DataControlRowType.DataRow Then Return
        Dim wiersz As DataRowView = CType(e.Row.DataItem, DataRowView)
        If Not IsDBNull(wiersz("DataZwrotu")) Then
            e.Row.ForeColor = Drawing.Color.Gray
            e.Row.ToolTip = "Zwrocono " & CDate(wiersz("DataZwrotu")).ToString(FormatDaty)
        ElseIf CDate(wiersz("PlanowanyZwrot")) < Date.Now Then
            e.Row.ForeColor = Drawing.Color.DarkRed
            e.Row.ToolTip = "Po terminie zwrotu"
        End If
    End Sub

    Private Sub PokazKlienta(ByVal klientId As Integer)
        Dim klient As Klient = New CustomerManager().PobierzKlienta(klientId)
        If klient Is Nothing Then
            lblKlient.Text = "(nie znaleziono klienta " & klientId & ")"
            btnWydaj.Enabled = False
            Return
        End If
        If klient.Typ = "F" AndAlso Not String.IsNullOrEmpty(klient.NazwaFirmy) Then
            lblKlient.Text = Server.HtmlEncode(klient.NazwaFirmy & " (" & klient.Imie & " " & klient.Nazwisko & ")")
        Else
            lblKlient.Text = Server.HtmlEncode(klient.Imie & " " & klient.Nazwisko)
        End If
        If klient.Zablokowany Then
            lblKomunikat.Text = "Klient zablokowany: " & Server.HtmlEncode(klient.PowodBlokady)
            btnWydaj.Enabled = False
            Return
        End If
        Dim otwarte As List(Of Model.Wypozyczenie) = New RentalManager().OtwarteKlienta(klientId)
        If otwarte.Count > 0 Then
            Dim poTerminie As Integer = 0
            For Each w As Model.Wypozyczenie In otwarte
                If w.CzyPoTerminie(Date.Now) Then poTerminie = poTerminie + 1
            Next
            lblKlient.Text = lblKlient.Text & " - otwarte wypozyczenia: " & otwarte.Count
            If poTerminie > 0 Then
                lblKomunikat.Text = "Uwaga: klient ma " & poTerminie & " wypozyczen po terminie zwrotu."
            End If
        End If
    End Sub

    Private Sub WczytajRezerwacje(ByVal klientId As Integer)
        ddlRezerwacje.Items.Clear()
        ddlRezerwacje.Items.Add(New ListItem("-- bez rezerwacji --", "0"))
        Dim tabela As DataTable = New ReservationManager().RezerwacjeKlienta(klientId)
        For Each wiersz As DataRow In tabela.Rows
            Dim status As String = wiersz("Status").ToString()
            If status = "N" OrElse status = "P" Then
                Dim tekst As String = String.Format("#{0} {1} | {2:yyyy-MM-dd HH:mm} - {3:yyyy-MM-dd HH:mm} | {4}",
                    wiersz("Id"), wiersz("Sprzet"), wiersz("DataOd"), wiersz("DataDo"), status)
                ddlRezerwacje.Items.Add(New ListItem(tekst, wiersz("Id").ToString()))
            End If
        Next
    End Sub

    Private Sub WczytajSprzet()
        ddlSprzet.Items.Clear()
        ddlSprzet.Items.Add(New ListItem("-- wybierz --", "0"))
        Dim lista As List(Of Sprzet) = New EquipmentManager().ListaWKategorii("")
        For Each s As Sprzet In lista
            If s.Status = "D" Then
                ddlSprzet.Items.Add(New ListItem(s.NumerInwentarzowy & " " & s.Nazwa, s.Id.ToString()))
            End If
        Next
    End Sub

    Private Sub WczytajRuchZDnia()
        gvWydaneDzis.DataSource = New RentalManager().WypozyczeniaZDnia(Date.Today)
        gvWydaneDzis.DataBind()
    End Sub

    Protected Sub ddlRezerwacje_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlRezerwacje.SelectedIndexChanged
        lblNumerPrzesylki.Text = "-"
        Dim rezerwacjaId As Integer = CInt(ddlRezerwacje.SelectedValue)
        If rezerwacjaId = 0 Then
            ddlSprzet.Enabled = True
            txtPlanowanyZwrot.Text = ""
            lblKaucja.Text = "-"
            Return
        End If
        Dim r As Rezerwacja = New ReservationManager().PobierzRezerwacje(rezerwacjaId)
        If r Is Nothing Then
            lblKomunikat.Text = "Nie znaleziono rezerwacji " & rezerwacjaId
            Return
        End If
        ddlSprzet.SelectedIndex = 0
        ddlSprzet.Enabled = False
        txtPlanowanyZwrot.Text = r.DataDo.ToString(FormatDaty)
        PokazKaucje(r.SprzetId)
    End Sub

    Protected Sub ddlSprzet_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlSprzet.SelectedIndexChanged
        Dim sprzetId As Integer = CInt(ddlSprzet.SelectedValue)
        If sprzetId = 0 Then
            lblKaucja.Text = "-"
        Else
            PokazKaucje(sprzetId)
            If txtPlanowanyZwrot.Text = "" Then
                txtPlanowanyZwrot.Text = Date.Now.AddDays(1).ToString(FormatDaty)
            End If
        End If
    End Sub

    Private Sub PokazKaucje(ByVal sprzetId As Integer)
        Dim s As Sprzet = New EquipmentManager().PobierzSprzet(sprzetId)
        If s Is Nothing Then
            lblKaucja.Text = "-"
        Else
            lblKaucja.Text = s.Kaucja.ToString("N2")
        End If
    End Sub

    Protected Sub chkDostawa_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkDostawa.CheckedChanged
        txtAdres.Enabled = chkDostawa.Checked
        If Not chkDostawa.Checked Then
            txtAdres.Text = ""
        End If
    End Sub

    Protected Sub btnWydaj_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnWydaj.Click
        Dim klientId As Integer = Session("WybranyKlientId")
        If klientId = 0 Then
            lblKomunikat.Text = "Sesja wygasla - wybierz klienta ponownie."
            Return
        End If

        Dim rezerwacjaId As Integer = CInt(ddlRezerwacje.SelectedValue)
        Dim sprzetId As Integer
        If rezerwacjaId > 0 Then
            Dim r As Rezerwacja = New ReservationManager().PobierzRezerwacje(rezerwacjaId)
            If r Is Nothing OrElse r.KlientId <> klientId Then
                lblKomunikat.Text = "Rezerwacja nie nalezy do wybranego klienta."
                Return
            End If
            sprzetId = r.SprzetId
        Else
            sprzetId = CInt(ddlSprzet.SelectedValue)
        End If
        If sprzetId = 0 Then
            lblKomunikat.Text = "Wybierz rezerwacje albo sprzet."
            Return
        End If

        Dim planowanyZwrot As Date
        If Not Date.TryParseExact(txtPlanowanyZwrot.Text.Trim(), FormatDaty, CultureInfo.InvariantCulture, DateTimeStyles.None, planowanyZwrot) Then
            lblKomunikat.Text = "Niepoprawny format daty zwrotu (rrrr-mm-dd gg:mm)."
            Return
        End If
        If planowanyZwrot <= Date.Now Then
            lblKomunikat.Text = "Planowany zwrot musi byc pozniejszy niz chwila wydania."
            Return
        End If

        Dim adres As String = ""
        If chkDostawa.Checked Then
            adres = txtAdres.Text.Trim()
            If adres = "" Then
                lblKomunikat.Text = "Podaj adres dostawy albo odznacz dostawe."
                Return
            End If
        End If

        Try
            Dim manager As New RentalManager()
            Dim noweId As Integer = manager.WydajSprzet(rezerwacjaId, klientId, sprzetId, planowanyZwrot, adres)
            Dim w As Model.Wypozyczenie = manager.PobierzWypozyczenie(noweId)
            lblKaucja.Text = w.KaucjaPobrana.ToString("N2")
            If chkDostawa.Checked Then
                If String.IsNullOrEmpty(w.NumerPrzesylki) Then
                    lblNumerPrzesylki.Text = "brak - zamow kuriera telefonicznie"
                Else
                    lblNumerPrzesylki.Text = w.NumerPrzesylki
                End If
            Else
                lblNumerPrzesylki.Text = "odbior osobisty"
            End If
            lblKomunikat.ForeColor = Drawing.Color.Green
            lblKomunikat.Text = "Wydano sprzet, wypozyczenie nr " & noweId & "."
            WczytajRezerwacje(klientId)
            WczytajSprzet()
            WczytajRuchZDnia()
            ddlSprzet.Enabled = True
            txtPlanowanyZwrot.Text = ""
            chkDostawa.Checked = False
            txtAdres.Text = ""
            txtAdres.Enabled = False
        Catch ex As BladBiznesowyException
            lblKomunikat.Text = ex.Message
        Catch ex As Exception
            lblKomunikat.Text = "Blad podczas wydania: " & ex.Message
        End Try
    End Sub

End Class
