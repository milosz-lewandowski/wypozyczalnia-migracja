' Modul: ReservationManager - rezerwacje sprzetu na termin | Autor: zespol IT | 2014-03-11
' Historia:
'   2014-09-22 JK  cena szacunkowa liczona przez PricingManager przy zapisie
'   2016-11-08 PT  dostepnosc przez EquipmentManager.SprawdzDostepnosc
'   2019-02-20 MW  anulowanie zrealizowanej rezerwacji zablokowane

Imports System.Data
Imports System.Data.SqlClient
Imports Wypozyczalnia.Core.Model
Imports Wypozyczalnia.Core.DAL

Public Class ReservationManager

    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(ReservationManager))

    Private Const MaksDlugoscUwag As Integer = 500
    Private Const MaksDniRezerwacji As Integer = 60

    Public Function UtworzRezerwacje(ByVal klientId As Integer, ByVal sprzetId As Integer, ByVal dataOd As Date, ByVal dataDo As Date, ByVal uwagi As String) As Integer
        If dataDo <= dataOd Then
            Throw New ArgumentException("Data zakonczenia musi byc pozniejsza niz data rozpoczecia")
        End If
        If dataDo.Subtract(dataOd).TotalDays > MaksDniRezerwacji Then
            Throw New ArgumentException("Rezerwacja moze obejmowac maksymalnie " & MaksDniRezerwacji & " dni")
        End If

        Dim klienci As New CustomerManager()
        If Not klienci.CzyMozeWypozyczac(klientId) Then
            Log.Warn("Proba rezerwacji przez zablokowanego klienta " & klientId)
            Throw New BladBiznesowyException("Klient zablokowany")
        End If

        Dim sprzetMgr As New EquipmentManager()
        If Not sprzetMgr.SprawdzDostepnosc(sprzetId, dataOd, dataDo) Then
            Throw New BladBiznesowyException("Sprzet niedostepny w wybranym terminie")
        End If

        Dim cennik As New PricingManager()
        Dim cena As Decimal = cennik.ObliczOplate(sprzetId, dataOd, dataDo, klientId)

        Dim r As New Rezerwacja()
        r.KlientId = klientId
        r.SprzetId = sprzetId
        r.DataOd = dataOd
        r.DataDo = dataDo
        r.Status = "N"
        r.CenaSzacunkowa = cena
        r.Uwagi = PrzytnijUwagi(uwagi)

        Dim dal As New ReservationDAL()
        Dim noweId As Integer = dal.Zapisz(r)
        Log.Info("Rezerwacja " & noweId & ": klient " & klientId & ", sprzet " & sprzetId & ", " & _
                 dataOd.ToString("yyyy-MM-dd HH:mm") & " - " & dataDo.ToString("yyyy-MM-dd HH:mm") & ", cena " & cena)
        Return noweId
    End Function

    Public Function PobierzRezerwacje(ByVal rezerwacjaId As Integer) As Rezerwacja
        Dim dal As New ReservationDAL()
        Dim wiersz As DataRow = dal.Pobierz(rezerwacjaId)
        If wiersz Is Nothing Then
            Return Nothing
        End If
        Return Mapuj(wiersz)
    End Function

    ' kolumny: Id, SprzetId, Sprzet, DataOd, DataDo, Status, CenaSzacunkowa
    Public Function RezerwacjeKlienta(ByVal klientId As Integer) As DataTable
        Dim dal As New ReservationDAL()
        Return dal.RezerwacjeKlienta(klientId)
    End Function

    Public Function AktywneRezerwacjeKlienta(ByVal klientId As Integer) As List(Of Rezerwacja)
        Dim wynik As New List(Of Rezerwacja)()
        Dim tabela As DataTable = RezerwacjeKlienta(klientId)
        For Each wiersz As DataRow In tabela.Rows
            Dim status As String = CStr(wiersz("Status"))
            If status = "N" OrElse status = "P" Then
                Dim r As New Rezerwacja()
                r.Id = CInt(wiersz("Id"))
                r.KlientId = klientId
                r.SprzetId = CInt(wiersz("SprzetId"))
                r.DataOd = CDate(wiersz("DataOd"))
                r.DataDo = CDate(wiersz("DataDo"))
                r.Status = status
                r.CenaSzacunkowa = CDec(wiersz("CenaSzacunkowa"))
                wynik.Add(r)
            End If
        Next
        Return wynik
    End Function

    Public Function RezerwacjeSprzetu(ByVal sprzetId As Integer) As DataTable
        Return New ReservationDAL().RezerwacjeSprzetu(sprzetId, Date.Today)
    End Function

    Public Function RezerwacjeWOkresie(ByVal dataOd As Date, ByVal dataDo As Date) As DataTable
        If dataDo <= dataOd Then
            Throw New ArgumentException("Niepoprawny okres")
        End If
        Return New ReservationDAL().RezerwacjeWOkresie(dataOd, dataDo)
    End Function

    Public Sub Potwierdz(ByVal rezerwacjaId As Integer)
        Dim r As Rezerwacja = PobierzRezerwacje(rezerwacjaId)
        If r Is Nothing Then
            Throw New BladBiznesowyException("Nie znaleziono rezerwacji " & rezerwacjaId)
        End If
        If r.Status <> "N" Then
            Throw New BladBiznesowyException("Potwierdzic mozna tylko nowa rezerwacje")
        End If
        Dim dal As New ReservationDAL()
        dal.ZmienStatus(rezerwacjaId, "P")
        Log.Info("Rezerwacja " & rezerwacjaId & " potwierdzona")
    End Sub

    Public Sub Anuluj(ByVal rezerwacjaId As Integer)
        Dim r As Rezerwacja = PobierzRezerwacje(rezerwacjaId)
        If r Is Nothing Then
            Throw New BladBiznesowyException("Nie znaleziono rezerwacji " & rezerwacjaId)
        End If
        If r.Status = "Z" Then
            Throw New BladBiznesowyException("Rezerwacja zrealizowana - nie mozna anulowac")
        End If
        If r.Status = "A" Then
            Log.Debug("Rezerwacja " & rezerwacjaId & " juz anulowana")
            Return
        End If
        Dim dal As New ReservationDAL()
        dal.ZmienStatus(rezerwacjaId, "A")
        dal.DopiszDoDziennika("ANULOWANIE", "Rezerwacja " & rezerwacjaId & " (klient " & r.KlientId & ")")
        Log.Info("Rezerwacja " & rezerwacjaId & " anulowana")
    End Sub

    Public Sub OznaczZrealizowana(ByVal rezerwacjaId As Integer)
        Dim dal As New ReservationDAL()
        dal.ZmienStatus(rezerwacjaId, "Z")
        Log.Info("Rezerwacja " & rezerwacjaId & " zrealizowana")
    End Sub

    ' nowe rezerwacje, ktorych termin minal bez wydania - anulowane hurtowo (wolane recznie z panelu)
    Public Function AnulujPrzeterminowane(ByVal naDzien As Date) As Integer
        Dim dal As New ReservationDAL()
        Dim tabela As DataTable = dal.PrzeterminowaneNowe(naDzien)
        Dim ile As Integer = 0
        For Each wiersz As DataRow In tabela.Rows
            Try
                dal.ZmienStatus(CInt(wiersz("Id")), "A")
                ile = ile + 1
            Catch ex As SqlException
                Log.Error("Nie udalo sie anulowac rezerwacji " & wiersz("Id"), ex)
            End Try
        Next
        If ile > 0 Then
            dal.DopiszDoDziennika("ANULOWANIE", "Przeterminowane nowe rezerwacje: " & ile)
        End If
        Return ile
    End Function

    ' zmiana terminu nowej albo potwierdzonej rezerwacji; cena liczona od nowa
    Public Sub ZmienTermin(ByVal rezerwacjaId As Integer, ByVal nowaDataOd As Date, ByVal nowaDataDo As Date)
        If nowaDataDo <= nowaDataOd Then
            Throw New ArgumentException("Data zakonczenia musi byc pozniejsza niz data rozpoczecia")
        End If
        Dim r As Rezerwacja = PobierzRezerwacje(rezerwacjaId)
        If r Is Nothing Then
            Throw New BladBiznesowyException("Nie znaleziono rezerwacji " & rezerwacjaId)
        End If
        If r.Status <> "N" AndAlso r.Status <> "P" Then
            Throw New BladBiznesowyException("Termin mozna zmienic tylko dla aktywnej rezerwacji")
        End If
        Dim dal As New ReservationDAL()
        Dim kolizje As DataTable = dal.PobierzKolizjeBezRezerwacji(r.SprzetId, nowaDataOd, nowaDataDo, rezerwacjaId)
        If kolizje.Rows.Count > 0 Then
            Throw New BladBiznesowyException("Sprzet niedostepny w wybranym terminie")
        End If
        Dim cena As Decimal = New PricingManager().ObliczOplate(r.SprzetId, nowaDataOd, nowaDataDo, r.KlientId)
        dal.ZmienTermin(rezerwacjaId, nowaDataOd, nowaDataDo, cena)
        dal.DopiszDoDziennika("ZMIANA TERMINU", "Rezerwacja " & rezerwacjaId & ": " & r.Termin & " -> " & _
                              nowaDataOd.ToString("yyyy-MM-dd HH:mm") & " - " & nowaDataDo.ToString("yyyy-MM-dd HH:mm"))
        Log.Info("Rezerwacja " & rezerwacjaId & " - nowy termin, cena " & cena)
    End Sub

    ' ponowne przeliczenie ceny po zmianie cennika (wolane recznie)
    Public Function PrzeliczCene(ByVal rezerwacjaId As Integer) As Decimal
        Dim r As Rezerwacja = PobierzRezerwacje(rezerwacjaId)
        If r Is Nothing Then
            Throw New BladBiznesowyException("Nie znaleziono rezerwacji " & rezerwacjaId)
        End If
        Dim cena As Decimal = New PricingManager().ObliczOplate(r.SprzetId, r.DataOd, r.DataDo, r.KlientId)
        If cena <> r.CenaSzacunkowa Then
            Dim dal As New ReservationDAL()
            dal.ZmienTermin(rezerwacjaId, r.DataOd, r.DataDo, cena)
            Log.Info("Rezerwacja " & rezerwacjaId & ": cena " & r.CenaSzacunkowa & " -> " & cena)
        End If
        Return cena
    End Function

    ' kolumny: Id, KlientId, Klient, SprzetId, Sprzet, DataOd, DataDo, Status
    Public Function RezerwacjeNaDzien(ByVal dzien As Date) As DataTable
        Return New ReservationDAL().RezerwacjeNaDzien(dzien)
    End Function

    ' kolumny: Status, Liczba, Suma
    Public Function Statystyki(ByVal dataOd As Date, ByVal dataDo As Date) As DataTable
        Return New ReservationDAL().Statystyki(dataOd, dataDo)
    End Function

    ' dni miesiaca zajete przez rezerwacje (do podswietlenia w kalendarzu)
    Public Function ZajeteDni(ByVal sprzetId As Integer, ByVal rok As Integer, ByVal miesiac As Integer) As List(Of Date)
        Dim wynik As New List(Of Date)()
        Dim tabela As DataTable = New ReservationDAL().KalendarzSprzetu(sprzetId, rok, miesiac)
        For Each wiersz As DataRow In tabela.Rows
            Dim od As Date = CDate(wiersz("DataOd")).Date
            Dim doDnia As Date = CDate(wiersz("DataDo")).Date
            Dim dzien As Date = od
            While dzien <= doDnia
                If dzien.Month = miesiac AndAlso Not wynik.Contains(dzien) Then
                    wynik.Add(dzien)
                End If
                dzien = dzien.AddDays(1)
            End While
        Next
        wynik.Sort()
        Return wynik
    End Function

    Public Function DoPotwierdzenia() As DataTable
        Return New ReservationDAL().DoPotwierdzenia()
    End Function

    Public Function PotwierdzWszystkie() As Integer
        Dim tabela As DataTable = DoPotwierdzenia()
        Dim ile As Integer = 0
        For Each wiersz As DataRow In tabela.Rows
            Try
                Potwierdz(CInt(wiersz("Id")))
                ile = ile + 1
            Catch ex As BladBiznesowyException
                Log.Warn("Pominieto rezerwacje " & wiersz("Id") & ": " & ex.Message)
            End Try
        Next
        Return ile
    End Function

    Public Sub ZmienUwagi(ByVal rezerwacjaId As Integer, ByVal uwagi As String)
        Dim r As Rezerwacja = PobierzRezerwacje(rezerwacjaId)
        If r Is Nothing Then
            Throw New BladBiznesowyException("Nie znaleziono rezerwacji " & rezerwacjaId)
        End If
        Dim dal As New ReservationDAL()
        dal.ZapiszUwagi(rezerwacjaId, PrzytnijUwagi(uwagi))
    End Sub

    Public Function CzyKlientMaAktywne(ByVal klientId As Integer) As Boolean
        Return New ReservationDAL().LiczbaAktywnychKlienta(klientId) > 0
    End Function

    Public Function OpisRezerwacji(ByVal rezerwacjaId As Integer) As String
        Dim r As Rezerwacja = PobierzRezerwacje(rezerwacjaId)
        If r Is Nothing Then
            Return ""
        End If
        Dim sprzet As Sprzet = New EquipmentManager().PobierzSprzet(r.SprzetId)
        Dim nazwa As String = "?"
        If sprzet IsNot Nothing Then
            nazwa = sprzet.Nazwa
        End If
        Return "Rezerwacja " & r.Id & ": " & nazwa & ", " & r.Termin & ", " & r.OpisStatusu
    End Function

    Private Function Mapuj(ByVal wiersz As DataRow) As Rezerwacja
        Dim r As New Rezerwacja()
        r.Id = CInt(wiersz("Id"))
        r.KlientId = CInt(wiersz("KlientId"))
        r.SprzetId = CInt(wiersz("SprzetId"))
        r.DataOd = CDate(wiersz("DataOd"))
        r.DataDo = CDate(wiersz("DataDo"))
        r.Status = CStr(wiersz("Status"))
        r.CenaSzacunkowa = CDec(wiersz("CenaSzacunkowa"))
        If IsDBNull(wiersz("Uwagi")) Then
            r.Uwagi = ""
        Else
            r.Uwagi = CStr(wiersz("Uwagi"))
        End If
        Return r
    End Function

    Private Function PrzytnijUwagi(ByVal uwagi As String) As String
        If uwagi Is Nothing Then
            Return ""
        End If
        uwagi = uwagi.Trim()
        If uwagi.Length > MaksDlugoscUwag Then
            uwagi = uwagi.Substring(0, MaksDlugoscUwag)
        End If
        Return uwagi
    End Function

End Class
