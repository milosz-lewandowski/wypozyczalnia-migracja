<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Cennik.aspx.vb" Inherits="WypozyczalniaWeb.Cennik" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Wypożyczalnia - Cennik i taryfy</title>
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
        <h2>Cennik i taryfy</h2>
        <asp:Label ID="lblWynik" runat="server" ForeColor="DarkRed" EnableViewState="false" />

        <h3>Taryfy kategorii</h3>
        <asp:GridView ID="gvTaryfy" runat="server" AutoGenerateColumns="false" CellPadding="3" GridLines="Horizontal">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" />
                <asp:BoundField DataField="Kategoria" HeaderText="Kategoria" />
                <asp:BoundField DataField="MnoznikWeekend" HeaderText="Mnożnik weekend" DataFormatString="{0:N2}" />
                <asp:BoundField DataField="RabatTygodniowyProc" HeaderText="Rabat tygodniowy %" DataFormatString="{0:N2}" />
                <asp:BoundField DataField="ObowiazujeOd" HeaderText="Obowiązuje od" DataFormatString="{0:yyyy-MM-dd}" />
            </Columns>
            <EmptyDataTemplate>Brak zdefiniowanych taryf.</EmptyDataTemplate>
        </asp:GridView>

        <h3>Stawka dobowa sprzętu</h3>
        <table>
            <tr>
                <td>Sprzęt:</td>
                <td><asp:DropDownList ID="ddlSprzet" runat="server" AutoPostBack="true" Width="320px" /></td>
            </tr>
            <tr>
                <td>Nowa stawka (zł):</td>
                <td>
                    <asp:TextBox ID="txtStawka" runat="server" Width="100px" />
                    <asp:Button ID="btnZapiszStawke" runat="server" Text="Zapisz stawkę" />
                </td>
            </tr>
        </table>

        <h3>Import cennika z katalogu</h3>
        <p>Pliki XML z katalogu importu zostaną wczytane i przeniesione do podkatalogu „przetworzone”.</p>
        <asp:Button ID="btnImportuj" runat="server" Text="Importuj cennik" OnClientClick="return confirm('Zaimportować pliki cennika?');" />

        <h3>Kalkulator opłaty</h3>
        <asp:Label ID="lblKlient" runat="server" Text="Klient: (nie wybrano)" /><br />
        <table>
            <tr>
                <td>Sprzęt:</td>
                <td><asp:DropDownList ID="ddlSprzetKalk" runat="server" Width="320px" /></td>
            </tr>
            <tr>
                <td>Od (rrrr-mm-dd gg:mm):</td>
                <td><asp:TextBox ID="txtOd" runat="server" Width="140px" /></td>
            </tr>
            <tr>
                <td>Do (rrrr-mm-dd gg:mm):</td>
                <td><asp:TextBox ID="txtDo" runat="server" Width="140px" /></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnOblicz" runat="server" Text="Oblicz" />
                    <asp:Label ID="lblKwota" runat="server" Font-Bold="true" />
                </td>
            </tr>
        </table>
        <asp:Label ID="lblSzczegoly" runat="server" Font-Size="Small" />
    </form>
</body>
</html>
