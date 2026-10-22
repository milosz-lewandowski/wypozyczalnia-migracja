' Modul: RentalManager.vb | Autor: zespol IT | 2014-03-11
' Wydania sprzetu, zwroty, kara za spoznienie, rozliczenie kaucji.
' 2015-06-22 MW: zamowienie kuriera przy wydaniu
' 2016-04-18 JK: kara liczona od planowanego zwrotu z godzina tolerancji

Imports System.Data
Imports Wypozyczalnia.Core.Model
Imports Wypozyczalnia.Core.DAL
Imports Wypozyczalnia.Core.Integracje

Public Class RentalManager

    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(RentalManager))

    Public Function PobierzWypozyczenie(ByVal wypozyczenieId As Integer) As Wypozyczenie
        If wypozyczenieId <= 0 Then Return Nothing
        Return New RentalDAL().PobierzWypozyczenie(wypozyczenieId)
    End Function

    Public Function WydajSprzet(ByVal rezerwacjaId As Integer, ByVal klientId As Integer, ByVal sprzetId As Integer, ByVal planowanyZwrot As Date, ByVal adresDostawy As String) As Integer
        Dim klienci As New CustomerManager()
        If Not klienci.CzyMozeWypozyczac(klientId) Then
            Log.Warn("Proba wydania dla zablokowanego klienta " & klientId)
            Throw New BladBiznesowyException("Klient zablokowany")
        End If

        Dim sprzety As New EquipmentManager()
        Dim s As Sprzet = sprzety.PobierzSprzet(sprzetId)
        If s Is Nothing Then
            Throw New BladBiznesowyException("Nieznany sprzet: " & sprzetId)
        End If
        If s.Status <> "D" Then
            Throw New BladBiznesowyException("Sprzet " & s.NumerInwentarzowy & " nie jest dostepny (status " & s.Status & ")")
        End If

        Dim teraz As Date = Date.Now
        If planowanyZwrot <= teraz Then
            Throw New ArgumentException("Planowany zwrot musi byc pozniejszy niz data wydania")
        End If

        Dim rezerwacje As New ReservationManager()
        If rezerwacjaId > 0 Then
            Dim r As Rezerwacja = rezerwacje.PobierzRezerwacje(rezerwacjaId)
            If r Is Nothing Then
                Throw New BladBiznesowyException("Nie znaleziono rezerwacji " & rezerwacjaId)
            End If
            If r.KlientId <> klientId OrElse r.SprzetId <> sprzetId Then
                Throw New BladBiznesowyException("Rezerwacja " & rezerwacjaId & " dotyczy innego klienta albo sprzetu")
            End If
            If r.Status <> "N" AndAlso r.Status <> "P" Then
                Throw New BladBiznesowyException("Rezerwacja " & rezerwacjaId & " ma status " & r.Status)
            End If
        End If

        Dim w As New Wypozyczenie()
        w.RezerwacjaId = rezerwacjaId
        w.KlientId = klientId
        w.SprzetId = sprzetId
        w.DataWydania = teraz
        w.PlanowanyZwrot = planowanyZwrot
        w.KaucjaPobrana = s.Kaucja
        w.Status = "O"

        Dim dal As New RentalDAL()
        Dim noweId As Integer = dal.Wydaj(w)
        Log.Info("Wydano sprzet " & sprzetId & " klientowi " & klientId & ", wypozyczenie " & noweId)

        sprzety.ZmienStatus(sprzetId, "W")
        If rezerwacjaId > 0 Then
            rezerwacje.OznaczZrealizowana(rezerwacjaId)
        End If

        If Not String.IsNullOrEmpty(adresDostawy) AndAlso adresDostawy.Trim() <> "" Then
            Dim klient As Klient = klienci.PobierzKlienta(klientId)
            Dim telefon As String = ""
            If klient IsNot Nothing Then telefon = klient.Telefon
            Dim numer As String = New KurierClient().ZamowDostawe(noweId, adresDostawy.Trim(), telefon)
            If numer <> "" Then
                dal.UstawNumerPrzesylki(noweId, numer)
                Log.Info("Zamowiono kuriera dla wypozyczenia " & noweId & ": " & numer)
            Else
                Log.Warn("Kurier nie przyjal zlecenia dla wypozyczenia " & noweId)
            End If
        End If

        dal.ZapiszDziennik("WYDANIE", "Wypozyczenie " & noweId & ", sprzet " & s.NumerInwentarzowy & ", klient " & klientId)
        Return noweId
    End Function

    Public Function ZarejestrujZwrot(ByVal wypozyczenieId As Integer, ByVal dataZwrotu As Date, ByVal szkody As Decimal) As Wypozyczenie
        Dim w As Wypozyczenie = PobierzWypozyczenie(wypozyczenieId)
        If w Is Nothing Then
            Throw New BladBiznesowyException("Nie znaleziono wypozyczenia " & wypozyczenieId)
        End If
        If w.Status = "Z" Then
            Throw New BladBiznesowyException("Wypozyczenie " & wypozyczenieId & " jest juz zamkniete")
        End If
        If szkody < 0 Then szkody = 0
        If dataZwrotu < w.DataWydania Then
            Throw New ArgumentException("Data zwrotu wczesniejsza niz data wydania")
        End If

        Dim sprzety As New EquipmentManager()
        Dim s As Sprzet = sprzety.PobierzSprzet(w.SprzetId)
        If s Is Nothing Then
            Throw New BladBiznesowyException("Nieznany sprzet: " & w.SprzetId)
        End If

        Dim oplata As Decimal = New PricingManager().ObliczOplate(w.SprzetId, w.DataWydania, w.PlanowanyZwrot, w.KlientId)
        Dim kara As Decimal = ObliczKare(w.PlanowanyZwrot, dataZwrotu, s.StawkaDobowa)
        Dim kaucjaZwrot As Decimal = w.KaucjaPobrana - kara - szkody
        If kaucjaZwrot < 0 Then kaucjaZwrot = 0
        If kara > 0 Then
            Log.Info("Spozniony zwrot wypozyczenia " & wypozyczenieId & ", plan " & w.PlanowanyZwrot.ToString("yyyy-MM-dd HH:mm") &
                ", zwrot " & dataZwrotu.ToString("yyyy-MM-dd HH:mm"))
        End If
        If kara + szkody > w.KaucjaPobrana Then
            Log.Warn("Kara i szkody przekraczaja kaucje dla wypozyczenia " & wypozyczenieId)
        End If

        w.DataZwrotu = dataZwrotu
        w.Oplata = oplata
        w.Kara = kara
        w.KaucjaZwrocona = kaucjaZwrot
        w.Status = "Z"

        Dim dal As New RentalDAL()
        dal.ZapiszZwrot(w)
        sprzety.ZmienStatus(s.Id, "D")

        Log.Info(String.Format("Zwrot wypozyczenia {0}: oplata {1}, kara {2}, szkody {3}, kaucja zwrocona {4}",
            wypozyczenieId, oplata, kara, szkody, kaucjaZwrot))
        dal.ZapiszDziennik("ZWROT", "Wypozyczenie " & wypozyczenieId & ", kara " & kara & ", szkody " & szkody)
        Return w
    End Function

    Public Function ObliczKare(ByVal planowanyZwrot As Date, ByVal dataZwrotu As Date, ByVal stawkaDobowa As Decimal) As Decimal
        Dim minuty As Long = DateDiff(DateInterval.Minute, planowanyZwrot, dataZwrotu)
        If minuty <= 60 Then Return 0D
        Dim dobySpoznienia As Long = (minuty - 60 + 1439) \ 1440
        ' kara = 100% stawki dobowej za kazda dobe spoznienia
        Return Math.Round(dobySpoznienia * stawkaDobowa * 1.5D, 2)
    End Function

    Public Function Otwarte() As DataTable
        Return New RentalDAL().Otwarte()
    End Function

    Public Function WypozyczeniaZDnia(ByVal dzien As Date) As DataTable
        Return New RentalDAL().WypozyczeniaZDnia(dzien)
    End Function

    Public Function WypozyczeniaKlienta(ByVal klientId As Integer) As DataTable
        Return New RentalDAL().WypozyczeniaKlienta(klientId)
    End Function

    Public Function OtwarteKlienta(ByVal klientId As Integer) As List(Of Wypozyczenie)
        Return New RentalDAL().OtwarteKlienta(klientId)
    End Function

    Public Function CzyKlientMaZaleglosci(ByVal klientId As Integer) As Boolean
        Dim teraz As Date = Date.Now
        For Each w As Wypozyczenie In OtwarteKlienta(klientId)
            If w.CzyPoTerminie(teraz) Then
                Log.Debug("Klient " & klientId & " ma zalegle wypozyczenie " & w.Id)
                Return True
            End If
        Next
        Return False
    End Function

    Public Function HistoriaSprzetu(ByVal sprzetId As Integer, ByVal ile As Integer) As List(Of Wypozyczenie)
        If ile <= 0 Then ile = 10
        If ile > 100 Then ile = 100
        Return New RentalDAL().OstatnieDlaSprzetu(sprzetId, ile)
    End Function

    Public Function SzacowanaKara(ByVal wypozyczenieId As Integer, ByVal naDzien As Date) As Decimal
        Dim w As Wypozyczenie = PobierzWypozyczenie(wypozyczenieId)
        If w Is Nothing OrElse Not w.CzyOtwarte Then Return 0D
        Dim s As Sprzet = New EquipmentManager().PobierzSprzet(w.SprzetId)
        If s Is Nothing Then Return 0D
        Return ObliczKare(w.PlanowanyZwrot, naDzien, s.StawkaDobowa)
    End Function

    Public Function PoTerminie(ByVal naDzien As Date) As DataTable
        Dim tabela As DataTable = New RentalDAL().PoTerminie(naDzien)
        tabela.Columns.Add("SzacowanaKara", GetType(Decimal))
        Dim sprzety As New EquipmentManager()
        For Each wiersz As DataRow In tabela.Rows
            Dim s As Sprzet = sprzety.PobierzSprzet(CInt(wiersz("SprzetId")))
            If s Is Nothing Then
                wiersz("SzacowanaKara") = 0D
            Else
                wiersz("SzacowanaKara") = ObliczKare(CDate(wiersz("PlanowanyZwrot")), naDzien, s.StawkaDobowa)
            End If
        Next
        Return tabela
    End Function

    Public Function StatusDostawy(ByVal wypozyczenieId As Integer) As String
        Dim w As Wypozyczenie = PobierzWypozyczenie(wypozyczenieId)
        If w Is Nothing Then Return ""
        If String.IsNullOrEmpty(w.NumerPrzesylki) Then Return "BRAK"
        Try
            Return New KurierClient().StatusPrzesylki(w.NumerPrzesylki)
        Catch ex As Exception
            Log.Error("Status przesylki " & w.NumerPrzesylki & " niedostepny", ex)
            Return "NIEZNANY"
        End Try
    End Function

    Public Function PonowZamowienieKuriera(ByVal wypozyczenieId As Integer, ByVal adresDostawy As String) As String
        Dim w As Wypozyczenie = PobierzWypozyczenie(wypozyczenieId)
        If w Is Nothing Then
            Throw New BladBiznesowyException("Nie znaleziono wypozyczenia " & wypozyczenieId)
        End If
        If Not w.CzyOtwarte Then
            Throw New BladBiznesowyException("Wypozyczenie " & wypozyczenieId & " jest zamkniete")
        End If
        If Not String.IsNullOrEmpty(w.NumerPrzesylki) Then
            Return w.NumerPrzesylki
        End If
        Dim klient As Klient = New CustomerManager().PobierzKlienta(w.KlientId)
        Dim telefon As String = ""
        If klient IsNot Nothing Then telefon = klient.Telefon
        Dim numer As String = New KurierClient().ZamowDostawe(wypozyczenieId, adresDostawy, telefon)
        If numer <> "" Then
            Dim dal As New RentalDAL()
            dal.UstawNumerPrzesylki(wypozyczenieId, numer)
            dal.ZapiszDziennik("KURIER", "Wypozyczenie " & wypozyczenieId & ", przesylka " & numer)
        End If
        Return numer
    End Function

    Public Function CzySprzetWydany(ByVal sprzetId As Integer) As Boolean
        Return New RentalDAL().CzyOtwarteDlaSprzetu(sprzetId)
    End Function

    Public Function WydajZRezerwacji(ByVal rezerwacjaId As Integer, ByVal adresDostawy As String) As Integer
        Dim r As Rezerwacja = New ReservationManager().PobierzRezerwacje(rezerwacjaId)
        If r Is Nothing Then
            Throw New BladBiznesowyException("Nie znaleziono rezerwacji " & rezerwacjaId)
        End If
        Return WydajSprzet(rezerwacjaId, r.KlientId, r.SprzetId, r.DataDo, adresDostawy)
    End Function

    Public Sub PrzedluzWypozyczenie(ByVal wypozyczenieId As Integer, ByVal nowyTermin As Date)
        Dim w As Wypozyczenie = PobierzWypozyczenie(wypozyczenieId)
        If w Is Nothing Then
            Throw New BladBiznesowyException("Nie znaleziono wypozyczenia " & wypozyczenieId)
        End If
        If Not w.CzyOtwarte Then
            Throw New BladBiznesowyException("Wypozyczenie " & wypozyczenieId & " jest zamkniete")
        End If
        If nowyTermin <= w.PlanowanyZwrot Then
            Throw New ArgumentException("Nowy termin musi byc pozniejszy niz obecny planowany zwrot")
        End If
        If Not New CustomerManager().CzyMozeWypozyczac(w.KlientId) Then
            Throw New BladBiznesowyException("Klient zablokowany")
        End If
        If Not New EquipmentManager().SprawdzDostepnosc(w.SprzetId, w.PlanowanyZwrot, nowyTermin) Then
            Throw New BladBiznesowyException("Sprzet zarezerwowany w wybranym terminie - przedluzenie niemozliwe")
        End If
        Dim dal As New RentalDAL()
        dal.ZmienPlanowanyZwrot(wypozyczenieId, nowyTermin)
        dal.ZapiszDziennik("PRZEDLUZENIE", "Wypozyczenie " & wypozyczenieId & " do " & nowyTermin.ToString("yyyy-MM-dd HH:mm"))
        Log.Info("Przedluzono wypozyczenie " & wypozyczenieId & " do " & nowyTermin.ToString("yyyy-MM-dd HH:mm"))
    End Sub

    Public Function DoZwrotuWCiagu(ByVal godzin As Integer) As List(Of Wypozyczenie)
        Dim teraz As Date = Date.Now
        Return New RentalDAL().DoZwrotuWOkresie(teraz, teraz.AddHours(godzin))
    End Function

    Public Function PodsumowanieDnia(ByVal dzien As Date) As String
        Dim tabela As DataTable = WypozyczeniaZDnia(dzien)
        Dim wydania As Integer = 0
        Dim zwroty As Integer = 0
        Dim sumaOplat As Decimal = 0
        Dim sumaKar As Decimal = 0
        For Each wiersz As DataRow In tabela.Rows
            Dim wydano As Date = CDate(wiersz("DataWydania"))
            If wydano.Date = dzien.Date Then wydania = wydania + 1
            If Not IsDBNull(wiersz("DataZwrotu")) AndAlso CDate(wiersz("DataZwrotu")).Date = dzien.Date Then
                zwroty = zwroty + 1
                If Not IsDBNull(wiersz("Oplata")) Then sumaOplat = sumaOplat + CDec(wiersz("Oplata"))
                If Not IsDBNull(wiersz("Kara")) Then sumaKar = sumaKar + CDec(wiersz("Kara"))
            End If
        Next
        Return String.Format("{0:yyyy-MM-dd}: wydania {1}, zwroty {2}, oplaty {3:N2}, kary {4:N2}",
            dzien, wydania, zwroty, sumaOplat, sumaKar)
    End Function

    Public Function Przypomnienia(ByVal godzin As Integer) As List(Of String)
        Dim wynik As New List(Of String)
        Dim klienci As New CustomerManager()
        For Each w As Wypozyczenie In DoZwrotuWCiagu(godzin)
            Dim k As Klient = klienci.PobierzKlienta(w.KlientId)
            If k Is Nothing Then Continue For
            wynik.Add(String.Format("{0};{1};{2};{3:yyyy-MM-dd HH:mm}", w.Id, k.Email, k.Telefon, w.PlanowanyZwrot))
        Next
        Return wynik
    End Function

    Public Function ZamknijBezZwrotu(ByVal wypozyczenieId As Integer, ByVal powod As String) As Wypozyczenie
        Dim w As Wypozyczenie = PobierzWypozyczenie(wypozyczenieId)
        If w Is Nothing Then
            Throw New BladBiznesowyException("Nie znaleziono wypozyczenia " & wypozyczenieId)
        End If
        If w.Status = "Z" Then
            Throw New BladBiznesowyException("Wypozyczenie " & wypozyczenieId & " jest juz zamkniete")
        End If
        If String.IsNullOrEmpty(powod) Then
            Throw New BladBiznesowyException("Podaj powod zamkniecia bez zwrotu")
        End If
        Dim teraz As Date = Date.Now
        Dim s As Sprzet = New EquipmentManager().PobierzSprzet(w.SprzetId)
        w.DataZwrotu = teraz
        w.Oplata = New PricingManager().ObliczOplate(w.SprzetId, w.DataWydania, w.PlanowanyZwrot, w.KlientId)
        w.Kara = ObliczKare(w.PlanowanyZwrot, teraz, s.StawkaDobowa)
        w.KaucjaZwrocona = 0
        w.Status = "Z"
        Dim dal As New RentalDAL()
        dal.ZapiszZwrot(w)
        New EquipmentManager().ZmienStatus(w.SprzetId, "X")
        dal.ZapiszDziennik("UTRATA", "Wypozyczenie " & wypozyczenieId & ": " & powod)
        Log.Warn("Zamknieto bez zwrotu wypozyczenie " & wypozyczenieId & ": " & powod)
        Return w
    End Function

End Class
