<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Sprzet.aspx.vb" Inherits="WypozyczalniaWeb.Sprzet" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Wypożyczalnia - Katalog sprzętu</title>
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
        <h2>Katalog sprzętu</h2>
        <p>
            Kategoria:
            <asp:DropDownList ID="ddlKategoria" runat="server" AutoPostBack="true">
                <asp:ListItem Value="" Text="-- wszystkie --" />
                <asp:ListItem Value="BUD" Text="Budowlany" />
                <asp:ListItem Value="OGR" Text="Ogrodowy" />
                <asp:ListItem Value="IMP" Text="Imprezowy" />
                <asp:ListItem Value="TRN" Text="Transportowy" />
            </asp:DropDownList>
        </p>
        <asp:GridView ID="gvSprzet" runat="server" AutoGenerateColumns="false" DataKeyNames="Id"
            CellPadding="3" GridLines="Horizontal" EmptyDataText="Brak sprzętu w kategorii.">
            <Columns>
                <asp:CommandField ShowSelectButton="true" SelectText="edytuj" />
                <asp:BoundField DataField="NumerInwentarzowy" HeaderText="Nr inw." />
                <asp:BoundField DataField="Nazwa" HeaderText="Nazwa" />
                <asp:BoundField DataField="Kategoria" HeaderText="Kat." />
                <asp:BoundField DataField="StawkaDobowa" HeaderText="Stawka / doba" DataFormatString="{0:N2}" />
                <asp:BoundField DataField="Kaucja" HeaderText="Kaucja" DataFormatString="{0:N2}" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
                <asp:HyperLinkField Text="rezerwuj" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="Rezerwacja.aspx?sprzet={0}" />
            </Columns>
            <SelectedRowStyle BackColor="#FFFFCC" />
        </asp:GridView>
        <h3>Edycja sprzętu</h3>
        <asp:HiddenField ID="hfSprzetId" runat="server" Value="0" />
        <table cellpadding="3" cellspacing="0" border="0">
            <tr><td>Numer inwentarzowy:</td><td><asp:TextBox ID="txtNumer" runat="server" MaxLength="20" Width="120px" /></td></tr>
            <tr><td>Nazwa:</td><td><asp:TextBox ID="txtNazwa" runat="server" MaxLength="100" Width="320px" /></td></tr>
            <tr>
                <td>Kategoria:</td>
                <td>
                    <asp:DropDownList ID="ddlKategoriaEdycja" runat="server">
                        <asp:ListItem Value="BUD" Text="Budowlany" />
                        <asp:ListItem Value="OGR" Text="Ogrodowy" />
                        <asp:ListItem Value="IMP" Text="Imprezowy" />
                        <asp:ListItem Value="TRN" Text="Transportowy" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr><td>Stawka dobowa (zł):</td><td><asp:TextBox ID="txtStawka" runat="server" Width="80px" /></td></tr>
            <tr><td>Kaucja (zł):</td><td><asp:TextBox ID="txtKaucja" runat="server" Width="80px" /></td></tr>
            <tr><td>Status:</td><td><asp:Label ID="lblStatus" runat="server" Text="-" /></td></tr>
        </table>
        <p>
            <asp:Button ID="btnNowy" runat="server" Text="Nowy" CausesValidation="false" />
            <asp:Button ID="btnZapisz" runat="server" Text="Zapisz" />
            <asp:Button ID="btnSerwis" runat="server" Text="Do serwisu" OnClientClick="return confirm('Przekazać sprzęt do serwisu?');" />
        </p>
        <p>
            <asp:Label ID="lblKomunikat" runat="server" ForeColor="Red" EnableViewState="false" />
        </p>
        <h3>Historia wypożyczeń</h3>
        <asp:GridView ID="gvHistoria" runat="server" AutoGenerateColumns="false" CellPadding="3" GridLines="Horizontal"
            EmptyDataText="Brak wypożyczeń.">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Nr" />
                <asp:BoundField DataField="Klient" HeaderText="Klient" />
                <asp:BoundField DataField="DataWydania" HeaderText="Wydano" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                <asp:BoundField DataField="PlanowanyZwrot" HeaderText="Plan. zwrot" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                <asp:BoundField DataField="DataZwrotu" HeaderText="Zwrot" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
