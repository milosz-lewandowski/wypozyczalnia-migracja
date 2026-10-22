<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Klienci.aspx.vb" Inherits="WypozyczalniaWeb.Klienci" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Wypożyczalnia - Klienci</title>
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
        <h2>Klienci</h2>
        <asp:Label ID="lblKomunikat" runat="server" ForeColor="Red" EnableViewState="false" />
        <table class="szukaj">
            <tr>
                <td>Nazwisko:</td>
                <td><asp:TextBox ID="txtSzukaj" runat="server" Width="250px" /></td>
                <td><asp:Button ID="btnSzukaj" runat="server" Text="Szukaj" /></td>
                <td><asp:Button ID="btnNowy" runat="server" Text="Nowy klient" CausesValidation="false" /></td>
            </tr>
        </table>
        <asp:GridView ID="gvKlienci" runat="server" AutoGenerateColumns="false" DataKeyNames="Id"
            CellPadding="4" GridLines="Horizontal" AllowPaging="true" PageSize="20"
            EmptyDataText="Brak klientów spełniających kryteria.">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Nr" />
                <asp:BoundField DataField="Imie" HeaderText="Imię" />
                <asp:BoundField DataField="Nazwisko" HeaderText="Nazwisko" />
                <asp:BoundField DataField="NazwaFirmy" HeaderText="Firma" />
                <asp:BoundField DataField="Email" HeaderText="E-mail" />
                <asp:BoundField DataField="Telefon" HeaderText="Telefon" />
                <asp:CommandField ShowSelectButton="true" SelectText="Wybierz" />
                <asp:ButtonField CommandName="Edytuj" Text="Edytuj" />
            </Columns>
            <SelectedRowStyle BackColor="#FFFFCC" />
        </asp:GridView>
        <div class="separator"></div>
        <h3><asp:Label ID="lblTrybFormularza" runat="server" Text="Nowy klient" /></h3>
        <asp:HiddenField ID="hfKlientId" runat="server" Value="0" />
        <table class="formularz">
            <tr><td>Imię:</td><td><asp:TextBox ID="txtImie" runat="server" MaxLength="50" /></td></tr>
            <tr><td>Nazwisko:</td><td><asp:TextBox ID="txtNazwisko" runat="server" MaxLength="80" /></td></tr>
            <tr><td>Nazwa firmy:</td><td><asp:TextBox ID="txtNazwaFirmy" runat="server" MaxLength="120" Width="300px" /></td></tr>
            <tr><td>E-mail:</td><td><asp:TextBox ID="txtEmail" runat="server" MaxLength="120" /></td></tr>
            <tr><td>Telefon:</td><td><asp:TextBox ID="txtTelefon" runat="server" MaxLength="20" /></td></tr>
            <tr><td>Nr dokumentu:</td><td><asp:TextBox ID="txtNumerDokumentu" runat="server" MaxLength="20" /></td></tr>
            <tr><td>NIP:</td><td><asp:TextBox ID="txtNIP" runat="server" MaxLength="13" /></td></tr>
            <tr>
                <td>Typ:</td>
                <td>
                    <asp:DropDownList ID="ddlTyp" runat="server">
                        <asp:ListItem Value="I" Text="Osoba prywatna" />
                        <asp:ListItem Value="F" Text="Firma" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Próg rabatu:</td>
                <td>
                    <asp:DropDownList ID="ddlProg" runat="server">
                        <asp:ListItem Value="0" Text="0 - brak" />
                        <asp:ListItem Value="1" Text="1" />
                        <asp:ListItem Value="2" Text="2" />
                        <asp:ListItem Value="3" Text="3" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr><td>Powód blokady:</td><td><asp:TextBox ID="txtPowodBlokady" runat="server" MaxLength="200" Width="300px" /></td></tr>
        </table>
        <asp:Button ID="btnZapisz" runat="server" Text="Zapisz" />
        <asp:Button ID="btnZablokuj" runat="server" Text="Zablokuj klienta"
            OnClientClick="return confirm('Zablokować klienta?');" />
    </form>
</body>
</html>
