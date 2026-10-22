<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Zwrot.aspx.vb" Inherits="WypozyczalniaWeb.Zwrot" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Wypożyczalnia - Zwrot sprzętu</title>
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
        <h2>Zwrot sprzętu</h2>
        <asp:Label ID="lblKomunikat" runat="server" ForeColor="Red" EnableViewState="false" />
        <table class="formularz" cellpadding="4" cellspacing="0" border="0">
            <tr>
                <td>Otwarte wypożyczenie:</td>
                <td>
                    <asp:DropDownList ID="ddlOtwarte" runat="server" AutoPostBack="true" Width="460px" />
                </td>
            </tr>
            <tr>
                <td>Szczegóły:</td>
                <td>
                    <asp:Label ID="lblSzczegoly" runat="server" Text="-" />
                </td>
            </tr>
            <tr>
                <td>Data i godzina zwrotu:</td>
                <td>
                    <asp:TextBox ID="txtDataZwrotu" runat="server" Width="160px" MaxLength="16" />
                    <span class="podpowiedz">(rrrr-mm-dd gg:mm, puste = teraz)</span>
                </td>
            </tr>
            <tr>
                <td valign="top">Stwierdzone szkody:</td>
                <td>
                    <asp:CheckBoxList ID="cblSzkody" runat="server" RepeatDirection="Vertical" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnPodglad" runat="server" Text="Podgląd kary" />
                    &nbsp;
                    <asp:Button ID="btnZwrot" runat="server" Text="Zarejestruj zwrot"
                        OnClientClick="return confirm('Zarejestrować zwrot? Operacji nie można cofnąć.');" />
                </td>
            </tr>
        </table>
        <h3>Rozliczenie</h3>
        <table class="rozliczenie" cellpadding="4" cellspacing="0" border="1">
            <tr>
                <td>Opłata za wynajem:</td>
                <td align="right"><asp:Label ID="lblOplata" runat="server" Text="-" /> zł</td>
            </tr>
            <tr>
                <td>Kara za spóźnienie:</td>
                <td align="right"><asp:Label ID="lblKara" runat="server" Text="-" /> zł</td>
            </tr>
            <tr>
                <td>Kaucja do zwrotu:</td>
                <td align="right"><asp:Label ID="lblKaucjaZwrot" runat="server" Text="-" /> zł</td>
            </tr>
        </table>
        <p>
            <asp:HyperLink ID="lnkFaktura" runat="server" Visible="false" Text="Wystaw fakturę za to wypożyczenie" />
        </p>
        <h3>Wszystkie otwarte wypożyczenia</h3>
        <asp:GridView ID="gvOtwarte" runat="server" AutoGenerateColumns="false" EmptyDataText="Brak otwartych wypożyczeń.">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Nr" />
                <asp:BoundField DataField="Klient" HeaderText="Klient" />
                <asp:BoundField DataField="Sprzet" HeaderText="Sprzęt" />
                <asp:BoundField DataField="DataWydania" HeaderText="Wydano" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                <asp:BoundField DataField="PlanowanyZwrot" HeaderText="Zwrot do" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                <asp:CommandField ShowSelectButton="true" SelectText="przyjmij" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
