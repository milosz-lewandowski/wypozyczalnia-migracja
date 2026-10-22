<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Rezerwacja.aspx.vb" Inherits="WypozyczalniaWeb.Rezerwacja" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Wypożyczalnia - Rezerwacje</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="smRezerwacja" runat="server" />
        <div class="menu">
            <a href="Klienci.aspx">Klienci</a> |
            <a href="Sprzet.aspx">Sprzęt</a> |
            <a href="Rezerwacja.aspx">Rezerwacje</a> |
            <a href="Wypozyczenie.aspx">Wydanie</a> |
            <a href="Zwrot.aspx">Zwrot</a> |
            <a href="Cennik.aspx">Cennik</a> |
            <a href="Faktura.aspx">Faktury</a>
        </div>
        <h2>Rezerwacje</h2>
        <p>
            Klient: <asp:Label ID="lblKlient" runat="server" Font-Bold="true" Text="(nie wybrano)" />
        </p>
        <table cellpadding="3" cellspacing="0" border="0">
            <tr>
                <td>Kategoria:</td>
                <td>
                    <asp:DropDownList ID="ddlKategoria" runat="server" AutoPostBack="true">
                        <asp:ListItem Value="" Text="-- wszystkie --" />
                        <asp:ListItem Value="BUD" Text="Budowlany" />
                        <asp:ListItem Value="OGR" Text="Ogrodowy" />
                        <asp:ListItem Value="IMP" Text="Imprezowy" />
                        <asp:ListItem Value="TRN" Text="Transportowy" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Sprzęt:</td>
                <td>
                    <asp:DropDownList ID="ddlSprzet" runat="server" Width="320px" />
                </td>
            </tr>
            <tr>
                <td>Od (data, godzina):</td>
                <td>
                    <asp:TextBox ID="txtDataOd" runat="server" Width="90px" MaxLength="10" />
                    <ajaxToolkit:CalendarExtender ID="ceDataOd" runat="server" TargetControlID="txtDataOd" Format="yyyy-MM-dd" FirstDayOfWeek="Monday" />
                    <asp:TextBox ID="txtGodzinaOd" runat="server" Width="50px" MaxLength="5" Text="08:00" />
                </td>
            </tr>
            <tr>
                <td>Do (data, godzina):</td>
                <td>
                    <asp:TextBox ID="txtDataDo" runat="server" Width="90px" MaxLength="10" />
                    <ajaxToolkit:CalendarExtender ID="ceDataDo" runat="server" TargetControlID="txtDataDo" Format="yyyy-MM-dd" FirstDayOfWeek="Monday" />
                    <asp:TextBox ID="txtGodzinaDo" runat="server" Width="50px" MaxLength="5" Text="08:00" />
                </td>
            </tr>
            <tr>
                <td>Uwagi:</td>
                <td>
                    <asp:TextBox ID="txtUwagi" runat="server" TextMode="MultiLine" Rows="2" Columns="40" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnSprawdz" runat="server" Text="Sprawdź dostępność" />
                    <asp:Button ID="btnZarezerwuj" runat="server" Text="Zarezerwuj" OnClientClick="return confirm('Zapisać rezerwację?');" />
                </td>
            </tr>
            <tr>
                <td>Szacunek:</td>
                <td>
                    <asp:Label ID="lblSzacunek" runat="server" Text="-" /> zł
                </td>
            </tr>
        </table>
        <p>
            <asp:Label ID="lblKomunikat" runat="server" ForeColor="Red" EnableViewState="false" />
        </p>
        <h3>Rezerwacje klienta</h3>
        <asp:GridView ID="gvRezerwacje" runat="server" AutoGenerateColumns="false" DataKeyNames="Id"
            CellPadding="3" GridLines="Horizontal" EmptyDataText="Brak rezerwacji.">
            <Columns>
                <asp:CommandField ShowSelectButton="true" SelectText="wybierz" />
                <asp:BoundField DataField="Id" HeaderText="Nr" />
                <asp:BoundField DataField="Sprzet" HeaderText="Sprzęt" />
                <asp:BoundField DataField="DataOd" HeaderText="Od" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                <asp:BoundField DataField="DataDo" HeaderText="Do" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
                <asp:BoundField DataField="CenaSzacunkowa" HeaderText="Cena" DataFormatString="{0:N2}" />
            </Columns>
            <SelectedRowStyle BackColor="#FFFFCC" />
        </asp:GridView>
        <p>
            <asp:Button ID="btnAnuluj" runat="server" Text="Anuluj wybraną rezerwację" OnClientClick="return confirm('Anulować rezerwację?');" />
        </p>
    </form>
</body>
</html>
