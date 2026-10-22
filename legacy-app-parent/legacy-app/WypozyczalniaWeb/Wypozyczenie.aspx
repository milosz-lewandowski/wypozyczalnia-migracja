<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Wypozyczenie.aspx.vb" Inherits="WypozyczalniaWeb.Wypozyczenie" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Wypożyczalnia - Wydanie sprzętu</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="menu">
            <a href="Klienci.aspx">Klienci</a> |
            <a href="Sprzet.aspx">Sprzęt</a> |
            <a href="Rezerwacja.aspx">Rezerwacje</a> |
            <a href="Wypozyczenie.aspx">Wydanie</a> |
            <a href="Zwrot.aspx">Zwrot</a> |
            <a href="Cennik.aspx">Cennik</a> |
            <a href="Faktura.aspx">Faktury</a>
        </div>
        <h2>Wydanie sprzętu</h2>
        <div class="klient">
            Klient: <asp:Label ID="lblKlient" runat="server" Font-Bold="true" Text="(nie wybrano)" />
        </div>
        <asp:Label ID="lblKomunikat" runat="server" ForeColor="Red" EnableViewState="false" />
        <table class="formularz" cellpadding="4" cellspacing="0" border="0">
            <tr>
                <td colspan="2"><b>Z rezerwacji</b></td>
            </tr>
            <tr>
                <td>Rezerwacja klienta:</td>
                <td>
                    <asp:DropDownList ID="ddlRezerwacje" runat="server" AutoPostBack="true" Width="420px" />
                </td>
            </tr>
            <tr>
                <td colspan="2"><b>albo bez rezerwacji</b></td>
            </tr>
            <tr>
                <td>Sprzęt dostępny od ręki:</td>
                <td>
                    <asp:DropDownList ID="ddlSprzet" runat="server" AutoPostBack="true" Width="420px" />
                </td>
            </tr>
            <tr>
                <td>Planowany zwrot:</td>
                <td>
                    <asp:TextBox ID="txtPlanowanyZwrot" runat="server" Width="160px" MaxLength="16" />
                    <span class="podpowiedz">(rrrr-mm-dd gg:mm)</span>
                    <asp:RequiredFieldValidator ID="rfvPlanowanyZwrot" runat="server"
                        ControlToValidate="txtPlanowanyZwrot" ErrorMessage="Podaj planowany termin zwrotu"
                        Display="Dynamic" ForeColor="Red" ValidationGroup="wydanie" />
                </td>
            </tr>
            <tr>
                <td>Kaucja do pobrania:</td>
                <td><asp:Label ID="lblKaucja" runat="server" Font-Bold="true" Text="-" /> zł</td>
            </tr>
            <tr>
                <td colspan="2"><b>Dostawa</b></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:CheckBox ID="chkDostawa" runat="server" AutoPostBack="true" Text="Dostawa kurierem pod adres klienta" />
                </td>
            </tr>
            <tr>
                <td>Adres dostawy:</td>
                <td>
                    <asp:TextBox ID="txtAdres" runat="server" TextMode="MultiLine" Rows="3" Columns="50" Enabled="false" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td><asp:Button ID="btnWydaj" runat="server" Text="Wydaj sprzęt" ValidationGroup="wydanie" /></td>
            </tr>
            <tr>
                <td>Numer przesyłki:</td>
                <td><asp:Label ID="lblNumerPrzesylki" runat="server" Text="-" /></td>
            </tr>
        </table>
        <h3>Wydania i zwroty z dnia dzisiejszego</h3>
        <asp:GridView ID="gvWydaneDzis" runat="server" AutoGenerateColumns="false" EmptyDataText="Brak ruchu w dniu dzisiejszym.">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Nr" />
                <asp:BoundField DataField="Klient" HeaderText="Klient" />
                <asp:BoundField DataField="Sprzet" HeaderText="Sprzęt" />
                <asp:BoundField DataField="DataWydania" HeaderText="Wydano" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                <asp:BoundField DataField="PlanowanyZwrot" HeaderText="Zwrot do" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
