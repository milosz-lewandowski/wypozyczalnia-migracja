<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Faktura.aspx.vb" Inherits="WypozyczalniaWeb.Faktura" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Wypożyczalnia - Faktura</title>
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
        <h2>Faktura</h2>
        <asp:Label ID="lblKomunikat" runat="server" ForeColor="Red" EnableViewState="false" />
        <table class="formularz" cellpadding="4" cellspacing="0" border="0">
            <tr>
                <td>Wypożyczenie:</td>
                <td>
                    <asp:Label ID="lblWypozyczenie" runat="server" Text="-" />
                </td>
            </tr>
            <tr>
                <td>Klient:</td>
                <td>
                    <asp:Label ID="lblKlient" runat="server" Text="-" />
                </td>
            </tr>
            <tr>
                <td>Numer faktury:</td>
                <td>
                    <asp:Label ID="lblNumer" runat="server" Text="(nie wystawiono)" />
                    &nbsp;
                    <asp:Label ID="lblStatus" runat="server" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnWystaw" runat="server" Text="Wystaw fakturę" />
                </td>
            </tr>
        </table>
        <h3>Pozycje</h3>
        <asp:GridView ID="gvPozycje" runat="server" AutoGenerateColumns="false" CellPadding="4" GridLines="Horizontal" EmptyDataText="Brak pozycji">
            <Columns>
                <asp:BoundField DataField="Lp" HeaderText="Lp." />
                <asp:BoundField DataField="Opis" HeaderText="Opis" />
                <asp:BoundField DataField="Ilosc" HeaderText="Ilość" DataFormatString="{0:0.##}" />
                <asp:BoundField DataField="CenaNetto" HeaderText="Cena netto" DataFormatString="{0:N2}" />
                <asp:BoundField DataField="StawkaVat" HeaderText="VAT" DataFormatString="{0:P0}" />
                <asp:BoundField DataField="WartoscNetto" HeaderText="Wartość netto" DataFormatString="{0:N2}" />
            </Columns>
        </asp:GridView>
        <table class="sumy" cellpadding="4" cellspacing="0" border="0">
            <tr>
                <td>Netto:</td>
                <td><asp:Label ID="lblNetto" runat="server" Text="0,00" /> zł</td>
            </tr>
            <tr>
                <td>VAT:</td>
                <td><asp:Label ID="lblVat" runat="server" Text="0,00" /> zł</td>
            </tr>
            <tr>
                <td><b>Brutto:</b></td>
                <td><b><asp:Label ID="lblBrutto" runat="server" Text="0,00" /> zł</b></td>
            </tr>
        </table>
        <p>
            <asp:Button ID="btnZaplac" runat="server" Text="Zapłać przez bramkę" OnClientClick="return confirm('Zarejestrować płatność w bramce?');" />
            &nbsp;
            <asp:Label ID="lblPlatnosc" runat="server" />
        </p>
        <div class="separator"></div>
        <h3>Faktury z dnia</h3>
        <asp:GridView ID="gvFakturyDnia" runat="server" AutoGenerateColumns="false" CellPadding="4" GridLines="Horizontal" EmptyDataText="Brak faktur z dzisiaj">
            <Columns>
                <asp:BoundField DataField="Numer" HeaderText="Numer" />
                <asp:BoundField DataField="Klient" HeaderText="Klient" />
                <asp:BoundField DataField="WypozyczenieId" HeaderText="Wypożyczenie" />
                <asp:BoundField DataField="Brutto" HeaderText="Brutto" DataFormatString="{0:N2}" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
            </Columns>
        </asp:GridView>
        <p>
            <asp:Button ID="btnEksportCsv" runat="server" Text="Eksport CSV (dzisiaj)" />
            &nbsp;
            <asp:Label ID="lblEksport" runat="server" />
        </p>
    </form>
</body>
</html>
