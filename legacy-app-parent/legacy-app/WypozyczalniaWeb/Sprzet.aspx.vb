' Modul: Sprzet.aspx - katalog sprzetu, edycja, serwis | Autor: zespol IT | 2014-03-11
' Historia:
'   2015-06-02 MW  pole kaucji w formularzu
'   2018-01-15 PT  historia wypozyczen pod formularzem

Imports System.Data
Imports System.Globalization
Imports Wypozyczalnia.Core
Imports SprzetModel = Wypozyczalnia.Core.Model.Sprzet

Partial Class Sprzet
    Inherits System.Web.UI.Page

    Protected WithEvents form1 As Global.System.Web.UI.HtmlControls.HtmlForm
    Protected WithEvents ddlKategoria As Global.System.Web.UI.WebControls.DropDownList
    Protected WithEvents gvSprzet As Global.System.Web.UI.WebControls.GridView
    Protected WithEvents hfSprzetId As Global.System.Web.UI.WebControls.HiddenField
    Protected WithEvents txtNumer As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNazwa As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlKategoriaEdycja As Global.System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtStawka As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKaucja As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents lblStatus As Global.System.Web.UI.WebControls.Label
    Protected WithEvents btnNowy As Global.System.Web.UI.WebControls.Button
    Protected WithEvents btnZapisz As Global.System.Web.UI.WebControls.Button
    Protected WithEvents btnSerwis As Global.System.Web.UI.WebControls.Button
    Protected WithEvents lblKomunikat As Global.System.Web.UI.WebControls.Label
    Protected WithEvents gvHistoria As Global.System.Web.UI.WebControls.GridView

    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Sprzet))

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            PokazListe()
            WyczyscFormularz()
        End If
    End Sub

    Protected Sub ddlKategoria_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlKategoria.SelectedIndexChanged
        gvSprzet.SelectedIndex = -1
        PokazListe()
        WyczyscFormularz()
    End Sub

    Protected Sub gvSprzet_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gvSprzet.SelectedIndexChanged
        Dim sprzetId As Integer = CInt(gvSprzet.SelectedDataKey.Value)
        Dim s As SprzetModel = New EquipmentManager().PobierzSprzet(sprzetId)
        If s Is Nothing Then
            PokazBlad("Sprzet nie istnieje (odswiez liste).")
            WyczyscFormularz()
            Return
        End If
        WypelnijFormularz(s)
        PokazHistorie(s.Id)
    End Sub

    Protected Sub gvSprzet_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvSprzet.RowDataBound
        If e.Row.RowType <> DataControlRowType.DataRow Then
            Return
        End If
        Dim s As SprzetModel = CType(e.Row.DataItem, SprzetModel)
        e.Row.Cells(6).Text = s.OpisStatusu
        If s.Status = "S" Then
            e.Row.CssClass = "wyszarzony"
        End If
        If s.Status <> "D" AndAlso s.Status <> "W" Then
            e.Row.Cells(7).Text = ""
        End If
    End Sub

    Protected Sub btnNowy_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNowy.Click
        gvSprzet.SelectedIndex = -1
        WyczyscFormularz()
        txtNumer.Focus()
    End Sub

    Protected Sub btnZapisz_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZapisz.Click
        Dim stawka As Decimal
        Dim kaucja As Decimal
        If Not Decimal.TryParse(txtStawka.Text.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, stawka) Then
            PokazBlad("Niepoprawna stawka dobowa.")
            Return
        End If
        If txtKaucja.Text.Trim() = "" Then
            kaucja = 0D
        ElseIf Not Decimal.TryParse(txtKaucja.Text.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, kaucja) Then
            PokazBlad("Niepoprawna kaucja.")
            Return
        End If

        Dim mgr As New EquipmentManager()
        Dim s As SprzetModel
        Dim sprzetId As Integer = CInt(hfSprzetId.Value)
        If sprzetId > 0 Then
            s = mgr.PobierzSprzet(sprzetId)
            If s Is Nothing Then
                PokazBlad("Sprzet nie istnieje.")
                Return
            End If
            If s.Kategoria <> ddlKategoriaEdycja.SelectedValue AndAlso Not mgr.CzyMoznaZmienicKategorie(sprzetId) Then
                PokazBlad("Nie mozna zmienic kategorii wypozyczonego sprzetu.")
                Return
            End If
        Else
            s = New SprzetModel()
        End If
        s.NumerInwentarzowy = txtNumer.Text
        s.Nazwa = txtNazwa.Text.Trim()
        s.Kategoria = ddlKategoriaEdycja.SelectedValue
        s.StawkaDobowa = stawka
        s.Kaucja = kaucja

        Try
            Dim zapisaneId As Integer = mgr.Zapisz(s)
            hfSprzetId.Value = zapisaneId.ToString()
            PokazInfo("Zapisano sprzet " & s.NumerInwentarzowy & ".")
            PokazListe()
            ZaznaczWiersz(zapisaneId)
        Catch ex As BladBiznesowyException
            PokazBlad(ex.Message)
        Catch ex As Exception
            Log.Error("Blad zapisu sprzetu " & s.NumerInwentarzowy, ex)
            PokazBlad("Nie udalo sie zapisac sprzetu.")
        End Try
    End Sub

    Protected Sub btnSerwis_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSerwis.Click
        Dim sprzetId As Integer = CInt(hfSprzetId.Value)
        If sprzetId = 0 Then
            PokazBlad("Wybierz sprzet z listy.")
            Return
        End If
        Try
            Dim mgr As New EquipmentManager()
            mgr.WyslijDoSerwisu(sprzetId)
            lblStatus.Text = "serwis"
            PokazInfo("Sprzet przekazany do serwisu.")
            PokazListe()
            ZaznaczWiersz(sprzetId)
        Catch ex As BladBiznesowyException
            PokazBlad(ex.Message)
        End Try
    End Sub

    Private Sub PokazListe()
        Dim lista As List(Of SprzetModel) = New EquipmentManager().ListaWKategorii(ddlKategoria.SelectedValue)
        gvSprzet.DataSource = lista
        gvSprzet.DataBind()
    End Sub

    Private Sub PokazHistorie(ByVal sprzetId As Integer)
        Dim tabela As DataTable = New EquipmentManager().HistoriaWypozyczen(sprzetId)
        gvHistoria.DataSource = tabela
        gvHistoria.DataBind()
        gvHistoria.Visible = True
    End Sub

    Private Sub WypelnijFormularz(ByVal s As SprzetModel)
        hfSprzetId.Value = s.Id.ToString()
        txtNumer.Text = s.NumerInwentarzowy
        txtNazwa.Text = s.Nazwa
        If ddlKategoriaEdycja.Items.FindByValue(s.Kategoria) IsNot Nothing Then
            ddlKategoriaEdycja.SelectedValue = s.Kategoria
        End If
        txtStawka.Text = s.StawkaDobowa.ToString("0.00")
        txtKaucja.Text = s.Kaucja.ToString("0.00")
        lblStatus.Text = s.OpisStatusu
        btnSerwis.Enabled = (s.Status = "D")
        txtNumer.ReadOnly = True
    End Sub

    Private Sub WyczyscFormularz()
        hfSprzetId.Value = "0"
        txtNumer.Text = ""
        txtNazwa.Text = ""
        txtStawka.Text = ""
        txtKaucja.Text = ""
        lblStatus.Text = "-"
        txtNumer.ReadOnly = False
        btnSerwis.Enabled = False
        If ddlKategoria.SelectedValue <> "" Then
            ddlKategoriaEdycja.SelectedValue = ddlKategoria.SelectedValue
        End If
        gvHistoria.DataSource = Nothing
        gvHistoria.DataBind()
        gvHistoria.Visible = False
    End Sub

    Private Sub ZaznaczWiersz(ByVal sprzetId As Integer)
        For i As Integer = 0 To gvSprzet.DataKeys.Count - 1
            If CInt(gvSprzet.DataKeys(i).Value) = sprzetId Then
                gvSprzet.SelectedIndex = i
                Return
            End If
        Next
        gvSprzet.SelectedIndex = -1
    End Sub

    Private Sub PokazBlad(ByVal tekst As String)
        lblKomunikat.CssClass = "blad"
        lblKomunikat.Text = Server.HtmlEncode(tekst)
    End Sub

    Private Sub PokazInfo(ByVal tekst As String)
        lblKomunikat.CssClass = "info"
        lblKomunikat.Text = Server.HtmlEncode(tekst)
    End Sub

End Class
