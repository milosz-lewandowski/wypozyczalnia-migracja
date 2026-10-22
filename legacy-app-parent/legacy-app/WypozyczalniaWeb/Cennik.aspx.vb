' Modul: Cennik.aspx - taryfy, stawki, import cennika, kalkulator | Autor: zespol IT | 2014-03-11
' Historia:
'   2015-02-04 MW: lista taryf z data obowiazywania
'   2017-06-12 JK: przycisk importu cennika XML
'   2019-04-02 PT: kalkulator oplaty dla wybranego klienta

Imports System.Data
Imports System.Configuration
Imports System.Globalization
Imports Wypozyczalnia.Core
Imports Wypozyczalnia.Core.Model
Imports Wypozyczalnia.Core.Integracje

Partial Class Cennik
    Inherits System.Web.UI.Page

    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Cennik))

    Protected WithEvents lblWynik As Global.System.Web.UI.WebControls.Label
    Protected WithEvents gvTaryfy As Global.System.Web.UI.WebControls.GridView
    Protected WithEvents ddlSprzet As Global.System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtStawka As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents btnZapiszStawke As Global.System.Web.UI.WebControls.Button
    Protected WithEvents btnImportuj As Global.System.Web.UI.WebControls.Button
    Protected WithEvents lblKlient As Global.System.Web.UI.WebControls.Label
    Protected WithEvents ddlSprzetKalk As Global.System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtOd As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDo As Global.System.Web.UI.WebControls.TextBox
    Protected WithEvents btnOblicz As Global.System.Web.UI.WebControls.Button
    Protected WithEvents lblKwota As Global.System.Web.UI.WebControls.Label
    Protected WithEvents lblSzczegoly As Global.System.Web.UI.WebControls.Label

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ZaladujTaryfy()
            ZaladujSprzet()
            PokazAktualnaStawke()
            txtOd.Text = Date.Today.AddDays(1).ToString("yyyy-MM-dd") & " 08:00"
            txtDo.Text = Date.Today.AddDays(2).ToString("yyyy-MM-dd") & " 08:00"
        End If
        PokazKlienta()
    End Sub

    Protected Sub ddlSprzet_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlSprzet.SelectedIndexChanged
        PokazAktualnaStawke()
    End Sub

    Protected Sub btnZapiszStawke_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZapiszStawke.Click
        If txtStawka.Text.Trim() = "" Then
            lblWynik.Text = "Podaj nową stawkę."
            Return
        End If
        If ddlSprzet.SelectedValue = "" Then
            lblWynik.Text = "Wybierz sprzęt."
            Return
        End If
        Try
            Dim sql As String = "UPDATE Sprzet SET StawkaDobowa = " & txtStawka.Text.Replace(",", ".") & " WHERE Id = " & ddlSprzet.SelectedValue
            Dim ile As Integer = DbHelper.WykonajPolecenie(sql)
            If ile = 1 Then
                lblWynik.Text = "Zapisano stawkę " & txtStawka.Text & " zł dla " & ddlSprzet.SelectedItem.Text & "."
                Log.Info("Zmiana stawki z ekranu cennika: " & ddlSprzet.SelectedValue & " = " & txtStawka.Text)
            Else
                lblWynik.Text = "Nie znaleziono sprzętu."
            End If
            txtStawka.Text = ""
            ZaladujSprzet()
            PokazAktualnaStawke()
        Catch ex As Exception
            Log.Error("Blad zapisu stawki", ex)
            lblWynik.Text = "Błąd zapisu stawki: " & ex.Message
        End Try
    End Sub

    Protected Sub btnImportuj_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnImportuj.Click
        Dim katalog As String = ConfigurationManager.AppSettings("ImportCennikaKatalog")
        Dim import As New ImportCennikaXml()
        Try
            Dim oczekujace As String() = import.PlikiOczekujace(katalog)
            If oczekujace.Length = 0 Then
                lblWynik.Text = "Brak plików do importu w katalogu " & katalog & "."
                Return
            End If
            Dim ile As Integer = import.ImportujKatalog(katalog)
            lblWynik.Text = "Zaimportowano plików: " & ile & " (" & String.Join(", ", oczekujace) & ")."
            ZaladujTaryfy()
            ZaladujSprzet()
            PokazAktualnaStawke()
        Catch ex As System.IO.IOException
            Log.Error("Import cennika - blad pliku", ex)
            lblWynik.Text = "Błąd dostępu do katalogu importu: " & ex.Message
        Catch ex As System.Xml.XmlException
            Log.Error("Import cennika - niepoprawny XML", ex)
            lblWynik.Text = "Niepoprawny plik XML: " & ex.Message
        Catch ex As FormatException
            Log.Error("Import cennika - niepoprawna liczba", ex)
            lblWynik.Text = "Niepoprawna wartość liczbowa w pliku: " & ex.Message
        End Try
    End Sub

    Protected Sub btnOblicz_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOblicz.Click
        lblKwota.Text = ""
        lblSzczegoly.Text = ""
        Dim klientId As Integer = Session("WybranyKlientId")
        Dim dataOd As Date
        Dim dataDo As Date
        If Not Date.TryParse(txtOd.Text, dataOd) Then
            lblWynik.Text = "Niepoprawna data od."
            Return
        End If
        If Not Date.TryParse(txtDo.Text, dataDo) Then
            lblWynik.Text = "Niepoprawna data do."
            Return
        End If
        If ddlSprzetKalk.SelectedValue = "" Then
            lblWynik.Text = "Wybierz sprzęt do kalkulacji."
            Return
        End If
        Dim pm As New PricingManager()
        Try
            Dim kwota As Decimal = pm.ObliczOplate(CInt(ddlSprzetKalk.SelectedValue), dataOd, dataDo, klientId)
            lblKwota.Text = kwota.ToString("N2") & " zł (" & pm.LiczbaDob(dataOd, dataDo) & " dób)"
            lblSzczegoly.Text = Server.HtmlEncode(pm.SzczegolyOplaty(CInt(ddlSprzetKalk.SelectedValue), dataOd, dataDo, klientId))
        Catch ex As ArgumentException
            lblWynik.Text = ex.Message
        Catch ex As BladBiznesowyException
            lblWynik.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PreRender
        Dim katalog As String = ConfigurationManager.AppSettings("ImportCennikaKatalog")
        If String.IsNullOrEmpty(katalog) Then
            btnImportuj.Enabled = False
            btnImportuj.ToolTip = "Brak klucza ImportCennikaKatalog w konfiguracji"
        Else
            btnImportuj.ToolTip = "Katalog: " & katalog
        End If
    End Sub

    Private _aktualneKategorie As New List(Of String)()

    Protected Sub gvTaryfy_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvTaryfy.RowDataBound
        If e.Row.RowType <> DataControlRowType.DataRow Then Return
        Dim wiersz As DataRowView = CType(e.Row.DataItem, DataRowView)
        Dim kategoria As String = wiersz("Kategoria").ToString().Trim()
        Dim od As Date = wiersz("ObowiazujeOd")
        If od > Date.Today Then
            e.Row.ForeColor = Drawing.Color.Gray
            e.Row.ToolTip = "Taryfa przyszła"
            Return
        End If
        If Not _aktualneKategorie.Contains(kategoria) Then
            _aktualneKategorie.Add(kategoria)
            e.Row.Font.Bold = True
            e.Row.ToolTip = "Taryfa obowiązująca"
        Else
            e.Row.ForeColor = Drawing.Color.DarkGray
            e.Row.ToolTip = "Taryfa archiwalna"
        End If
    End Sub

    Private Sub ZaladujTaryfy()
        Dim sql As String = "SELECT Id, Kategoria, MnoznikWeekend, RabatTygodniowyProc, ObowiazujeOd FROM Taryfy ORDER BY Kategoria, ObowiazujeOd DESC"
        Try
            gvTaryfy.DataSource = DbHelper.WykonajZapytanie(sql)
            gvTaryfy.DataBind()
        Catch ex As Exception
            Log.Error("Blad odczytu taryf", ex)
            lblWynik.Text = "Nie udało się wczytać taryf."
        End Try
    End Sub

    Private Sub ZaladujSprzet()
        Dim wybrany As String = ddlSprzet.SelectedValue
        Dim wybranyKalk As String = ddlSprzetKalk.SelectedValue
        Dim lista As List(Of Sprzet) = New EquipmentManager().ListaWKategorii("")

        ddlSprzet.Items.Clear()
        ddlSprzetKalk.Items.Clear()
        ddlSprzet.Items.Add(New ListItem("-- wybierz --", ""))
        ddlSprzetKalk.Items.Add(New ListItem("-- wybierz --", ""))
        For Each s As Sprzet In lista
            Dim tekst As String = s.NumerInwentarzowy & " " & s.Nazwa
            ddlSprzet.Items.Add(New ListItem(tekst, s.Id.ToString()))
            ddlSprzetKalk.Items.Add(New ListItem(tekst, s.Id.ToString()))
        Next
        If wybrany <> "" AndAlso ddlSprzet.Items.FindByValue(wybrany) IsNot Nothing Then
            ddlSprzet.SelectedValue = wybrany
        End If
        If wybranyKalk <> "" AndAlso ddlSprzetKalk.Items.FindByValue(wybranyKalk) IsNot Nothing Then
            ddlSprzetKalk.SelectedValue = wybranyKalk
        End If
    End Sub

    Private Sub PokazAktualnaStawke()
        If ddlSprzet.SelectedValue = "" Then
            txtStawka.ToolTip = ""
            Return
        End If
        Dim s As Sprzet = New EquipmentManager().PobierzSprzet(CInt(ddlSprzet.SelectedValue))
        If s Is Nothing Then Return
        txtStawka.ToolTip = "Aktualnie: " & s.StawkaDobowa.ToString("N2") & " zł, kaucja " & s.Kaucja.ToString("N2") & " zł"
        txtStawka.Attributes("placeholder") = s.StawkaDobowa.ToString("N2")
    End Sub

    Private Sub PokazKlienta()
        Dim klientId As Integer = Session("WybranyKlientId")
        If klientId = 0 Then
            lblKlient.Text = "Klient: (nie wybrano - kalkulacja bez rabatu)"
            Return
        End If
        Dim k As Klient = New CustomerManager().PobierzKlienta(klientId)
        If k Is Nothing Then
            lblKlient.Text = "Klient: (nie znaleziono)"
        ElseIf k.Typ = "F" Then
            lblKlient.Text = "Klient: " & Server.HtmlEncode(k.NazwaFirmy) & " (próg rabatu " & k.ProgRabatu & ")"
        Else
            lblKlient.Text = "Klient: " & Server.HtmlEncode(k.Imie & " " & k.Nazwisko) & " (próg rabatu " & k.ProgRabatu & ")"
        End If
    End Sub

End Class
