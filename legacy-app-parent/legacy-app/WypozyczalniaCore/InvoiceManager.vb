' Modul: InvoiceManager - wystawianie faktur za wypozyczenia, VAT | Autor: zespol IT | 2014-05-19
' Historia:
'   2014-11-27 JK  numeracja w bazie, pozycje zapisywane osobno
'   2016-02-09 MW  stawka VAT z Web.config zamiast stalej w kodzie
'   2019-03-21 PT  pozycja "Szkody" gdy kaucja nie pokryla rozliczenia

Imports System.Data
Imports System.Configuration
Imports System.Globalization
Imports Wypozyczalnia.Core.Model
Imports Wypozyczalnia.Core.DAL

Public Class InvoiceManager

    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(InvoiceManager))

    Private Const DomyslnaStawkaVat As String = "0,23"

    Public Function WystawFaktureZaWypozyczenie(ByVal wypozyczenieId As Integer) As Faktura
        Dim rental As New RentalManager()
        Dim w As Wypozyczenie = rental.PobierzWypozyczenie(wypozyczenieId)
        If w Is Nothing Then
            Throw New BladBiznesowyException("Nie znaleziono wypozyczenia " & wypozyczenieId, "WYP_BRAK")
        End If
        If w.Status <> "Z" Then
            Throw New BladBiznesowyException("Wypozyczenie nie zostalo jeszcze rozliczone (brak zwrotu)", "WYP_OTWARTE")
        End If

        Dim dal As New InvoiceDAL()
        If dal.CzyIstniejeDlaWypozyczenia(wypozyczenieId) Then
            Throw New BladBiznesowyException("Faktura za to wypozyczenie juz istnieje", "FV_ISTNIEJE")
        End If

        Dim klient As Klient = New CustomerManager().PobierzKlienta(w.KlientId)
        If klient Is Nothing Then
            Throw New BladBiznesowyException("Nieznany klient: " & w.KlientId, "KLIENT_BRAK")
        End If
        Dim sprzet As Sprzet = New EquipmentManager().PobierzSprzet(w.SprzetId)
        If sprzet Is Nothing Then
            Throw New BladBiznesowyException("Nieznany sprzet: " & w.SprzetId, "SPRZET_BRAK")
        End If

        Dim stawkaVat As Decimal = PobierzStawkeVat()
        Dim f As New Faktura()
        f.KlientId = w.KlientId
        f.WypozyczenieId = w.Id
        f.DataWystawienia = Date.Now

        Dim doby As Integer = LiczbaDobDoOpisu(w.DataWydania, w.PlanowanyZwrot)
        DodajPozycje(f, "Wynajem: " & sprzet.Nazwa & " (" & doby & " " & OdmianaDoby(doby) & ")", 1D, w.Oplata, stawkaVat)

        If w.Kara > 0D Then
            DodajPozycje(f, "Kara za spóźniony zwrot", 1D, w.Kara, stawkaVat)
        End If

        ' szkody potracone z kaucji; na fakture tylko gdy kaucja zostala wyczerpana
        Dim szkody As Decimal = w.KaucjaPobrana - w.Kara - w.KaucjaZwrocona
        If w.KaucjaZwrocona = 0D AndAlso szkody > 0D Then
            DodajPozycje(f, "Szkody", 1D, szkody, stawkaVat)
        End If

        PrzeliczSumy(f)

        dal.ZapiszNaglowek(f)
        For Each p As PozycjaFaktury In f.Pozycje
            dal.ZapiszPozycje(f.Id, p)
        Next

        Log.Info("Wystawiono fakture " & f.Numer & " za wypozyczenie " & w.Id & ", brutto " & f.Brutto.ToString("0.00"))
        Return f
    End Function

    Public Function PobierzFakture(ByVal fakturaId As Integer) As Faktura
        Dim dal As New InvoiceDAL()
        Dim wiersz As DataRow = dal.Pobierz(fakturaId)
        If wiersz Is Nothing Then
            Return Nothing
        End If
        Dim f As Faktura = Faktura.ZWiersza(wiersz)
        WczytajPozycje(dal, f)
        Return f
    End Function

    Public Function PobierzFaktureDlaWypozyczenia(ByVal wypozyczenieId As Integer) As Faktura
        Dim dal As New InvoiceDAL()
        Dim wiersz As DataRow = dal.PobierzDlaWypozyczenia(wypozyczenieId)
        If wiersz Is Nothing Then
            Return Nothing
        End If
        Dim f As Faktura = Faktura.ZWiersza(wiersz)
        WczytajPozycje(dal, f)
        Return f
    End Function

    Public Sub OznaczOplacona(ByVal fakturaId As Integer, ByVal idTransakcji As String)
        If String.IsNullOrEmpty(idTransakcji) Then
            Throw New ArgumentException("Brak identyfikatora transakcji", "idTransakcji")
        End If
        Dim f As Faktura = PobierzFakture(fakturaId)
        If f Is Nothing Then
            Throw New BladBiznesowyException("Nie znaleziono faktury " & fakturaId, "FV_BRAK")
        End If
        If f.Status = "O" Then
            Log.Warn("Faktura " & f.Numer & " juz oplacona (transakcja " & f.IdTransakcji & "), pomijam " & idTransakcji)
            Return
        End If
        Dim ile As Integer = New InvoiceDAL().UstawOplacona(fakturaId, idTransakcji)
        If ile = 0 Then
            Throw New BladBiznesowyException("Nie udalo sie zmienic statusu faktury " & f.Numer, "FV_STATUS")
        End If
        Log.Info("Faktura " & f.Numer & " oplacona, transakcja " & idTransakcji)
    End Sub

    ' kolumny: Id, Numer, KlientId, Klient, WypozyczenieId, DataWystawienia, Netto, Vat, Brutto, Status
    Public Function FakturyZDnia(ByVal dzien As Date) As DataTable
        Return New InvoiceDAL().FakturyZDnia(dzien)
    End Function

    Public Function FakturyKlienta(ByVal klientId As Integer) As DataTable
        Return New InvoiceDAL().FakturyKlienta(klientId)
    End Function

    Public Function Nieoplacone() As DataTable
        Return New InvoiceDAL().Nieoplacone()
    End Function

    Public Function PobierzPoNumerze(ByVal numer As String) As Faktura
        If String.IsNullOrEmpty(numer) Then Return Nothing
        Dim dal As New InvoiceDAL()
        Dim wiersz As DataRow = dal.PobierzPoNumerze(numer.Trim().ToUpper())
        If wiersz Is Nothing Then
            Return Nothing
        End If
        Dim f As Faktura = Faktura.ZWiersza(wiersz)
        WczytajPozycje(dal, f)
        Return f
    End Function

    Public Function CzyMoznaWystawic(ByVal wypozyczenieId As Integer) As Boolean
        Dim w As Wypozyczenie = New RentalManager().PobierzWypozyczenie(wypozyczenieId)
        If w Is Nothing Then Return False
        If w.Status <> "Z" Then Return False
        Return Not New InvoiceDAL().CzyIstniejeDlaWypozyczenia(wypozyczenieId)
    End Function

    ' anulowanie = usuniecie nieoplaconej faktury (numer zostaje "dziura" w numeracji)
    Public Sub AnulujNieoplacona(ByVal fakturaId As Integer)
        Dim f As Faktura = PobierzFakture(fakturaId)
        If f Is Nothing Then
            Throw New BladBiznesowyException("Nie znaleziono faktury " & fakturaId, "FV_BRAK")
        End If
        If f.Status = "O" Then
            Throw New BladBiznesowyException("Faktura " & f.Numer & " jest oplacona - anulowanie niemozliwe", "FV_STATUS")
        End If
        Dim dal As New InvoiceDAL()
        dal.Usun(fakturaId)
        Log.Warn("Usunieto nieoplacona fakture " & f.Numer)
    End Sub

    Public Function TerminPlatnosci(ByVal f As Faktura) As Date
        Dim klient As Klient = New CustomerManager().PobierzKlienta(f.KlientId)
        If klient IsNot Nothing AndAlso klient.Typ = "F" Then
            Return f.DataWystawienia.Date.AddDays(14)
        End If
        Return f.DataWystawienia.Date.AddDays(7)
    End Function

    Public Function ZestawienieMiesiaca(ByVal rok As Integer, ByVal miesiac As Integer) As DataTable
        Return New InvoiceDAL().ZestawienieMiesiaca(rok, miesiac)
    End Function

    Public Function PodgladFaktury(ByVal wypozyczenieId As Integer) As Faktura
        Dim w As Wypozyczenie = New RentalManager().PobierzWypozyczenie(wypozyczenieId)
        If w Is Nothing OrElse w.Status <> "Z" Then
            Return Nothing
        End If
        Dim sprzet As Sprzet = New EquipmentManager().PobierzSprzet(w.SprzetId)
        Dim stawkaVat As Decimal = PobierzStawkeVat()
        Dim f As New Faktura()
        f.KlientId = w.KlientId
        f.WypozyczenieId = w.Id
        Dim doby As Integer = LiczbaDobDoOpisu(w.DataWydania, w.PlanowanyZwrot)
        Dim nazwa As String = "?"
        If sprzet IsNot Nothing Then nazwa = sprzet.Nazwa
        DodajPozycje(f, "Wynajem: " & nazwa & " (" & doby & " " & OdmianaDoby(doby) & ")", 1D, w.Oplata, stawkaVat)
        If w.Kara > 0D Then
            DodajPozycje(f, "Kara za spóźniony zwrot", 1D, w.Kara, stawkaVat)
        End If
        PrzeliczSumy(f)
        Return f
    End Function

    Private Sub DodajPozycje(ByVal f As Faktura, ByVal opis As String, ByVal ilosc As Decimal, ByVal cenaNetto As Decimal, ByVal stawkaVat As Decimal)
        Dim p As New PozycjaFaktury()
        p.Lp = f.Pozycje.Count + 1
        p.Opis = opis
        p.Ilosc = ilosc
        p.CenaNetto = cenaNetto
        p.StawkaVat = stawkaVat
        p.WartoscNetto = Math.Round(ilosc * cenaNetto, 2)
        f.Pozycje.Add(p)
    End Sub

    ' VAT liczony od kazdej pozycji osobno i sumowany
    Private Sub PrzeliczSumy(ByVal f As Faktura)
        Dim netto As Decimal = 0D
        Dim vat As Decimal = 0D
        For Each p As PozycjaFaktury In f.Pozycje
            netto = netto + p.WartoscNetto
            vat = vat + Math.Round(p.WartoscNetto * p.StawkaVat, 2)
        Next
        f.Netto = netto
        f.Vat = vat
        f.Brutto = f.Netto + f.Vat
    End Sub

    Private Sub WczytajPozycje(ByVal dal As InvoiceDAL, ByVal f As Faktura)
        f.Pozycje.Clear()
        For Each wiersz As DataRow In dal.PobierzPozycje(f.Id).Rows
            f.Pozycje.Add(PozycjaFaktury.ZWiersza(wiersz))
        Next
    End Sub

    Private Function PobierzStawkeVat() As Decimal
        Dim tekst As String = ConfigurationManager.AppSettings("StawkaVat")
        If String.IsNullOrEmpty(tekst) Then
            Log.Warn("Brak klucza StawkaVat w konfiguracji, uzywam " & DomyslnaStawkaVat)
            tekst = DomyslnaStawkaVat
        End If
        Return CDec(tekst)
    End Function

    Private Function LiczbaDobDoOpisu(ByVal dataOd As Date, ByVal dataDo As Date) As Integer
        Dim n As Integer = dataDo.Subtract(dataOd).Days
        If n < 1 Then n = 1
        Return n
    End Function

    Private Function OdmianaDoby(ByVal n As Integer) As String
        If n = 1 Then Return "doba"
        Dim reszta10 As Integer = n Mod 10
        Dim reszta100 As Integer = n Mod 100
        If reszta10 >= 2 AndAlso reszta10 <= 4 AndAlso (reszta100 < 12 OrElse reszta100 > 14) Then
            Return "doby"
        End If
        Return "dób"
    End Function

End Class
