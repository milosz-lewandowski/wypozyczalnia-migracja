' Modul: EquipmentManager - katalog sprzetu, statusy, dostepnosc | Autor: zespol IT | 2014-03-11
' TODO 2016: przeniesc sprawdzanie dostepnosci do ReservationManager
' Historia:
'   2016-11-08 PT  SprawdzDostepnosc przez ReservationDAL.PobierzKolizje (procedura w bazie)
'   2018-01-15 PT  status X, lista bez wycofanych

Imports System.Data
Imports System.Data.SqlClient
Imports Wypozyczalnia.Core.Model
Imports Wypozyczalnia.Core.DAL

Public Class EquipmentManager

    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(EquipmentManager))

    Private Shared ReadOnly DozwoloneStatusy() As String = {"D", "W", "S", "X"}
    Private Shared ReadOnly DozwoloneKategorie() As String = {"BUD", "OGR", "IMP", "TRN"}

    Public Function PobierzSprzet(ByVal sprzetId As Integer) As Sprzet
        If sprzetId <= 0 Then
            Return Nothing
        End If
        Dim dal As New EquipmentDAL()
        Return dal.Pobierz(sprzetId)
    End Function

    Public Function PobierzPoNumerze(ByVal numerInwentarzowy As String) As Sprzet
        If String.IsNullOrEmpty(numerInwentarzowy) Then
            Return Nothing
        End If
        Return New EquipmentDAL().PobierzPoNumerze(numerInwentarzowy.Trim().ToUpper())
    End Function

    ' kategoria pusta = wszystkie; wycofane (X) pomijane w DAL
    Public Function ListaWKategorii(ByVal kategoria As String) As List(Of Sprzet)
        Dim dal As New EquipmentDAL()
        If kategoria Is Nothing Then
            kategoria = ""
        End If
        Return dal.Lista(kategoria.Trim())
    End Function

    ' dostepnosc w terminie: kolizje z rezerwacjami, otwartymi wypozyczeniami i statusem sprzetu
    Public Function SprawdzDostepnosc(ByVal sprzetId As Integer, ByVal dataOd As Date, ByVal dataDo As Date) As Boolean
        Dim rezerwacje As New ReservationDAL()
        Dim kolizje As DataTable = rezerwacje.PobierzKolizje(sprzetId, dataOd, dataDo)
        If kolizje.Rows.Count > 0 Then
            Log.Debug("Sprzet " & sprzetId & " niedostepny " & dataOd.ToString("yyyy-MM-dd HH:mm") & " - " & _
                      dataDo.ToString("yyyy-MM-dd HH:mm") & ", kolizji: " & kolizje.Rows.Count)
        End If
        Return kolizje.Rows.Count = 0
    End Function

    ' sprzet z kategorii wolny w terminie (uzywane przy rezerwacji i wydaniu bez rezerwacji)
    Public Function DostepneWKategorii(ByVal kategoria As String, ByVal dataOd As Date, ByVal dataDo As Date) As List(Of Sprzet)
        Dim wynik As New List(Of Sprzet)()
        For Each s As Sprzet In ListaWKategorii(kategoria)
            If s.Status = "S" Then
                Continue For
            End If
            If SprawdzDostepnosc(s.Id, dataOd, dataDo) Then
                wynik.Add(s)
            End If
        Next
        Return wynik
    End Function

    Public Sub ZmienStatus(ByVal sprzetId As Integer, ByVal nowyStatus As String)
        If nowyStatus Is Nothing OrElse Array.IndexOf(DozwoloneStatusy, nowyStatus) < 0 Then
            Throw New ArgumentException("Nieznany status sprzetu: " & nowyStatus, "nowyStatus")
        End If
        Dim dal As New EquipmentDAL()
        Dim ile As Integer = dal.ZmienStatus(sprzetId, nowyStatus)
        If ile = 0 Then
            Log.Warn("ZmienStatus: brak sprzetu o Id " & sprzetId)
        Else
            Log.Info("Sprzet " & sprzetId & " -> status " & nowyStatus)
        End If
    End Sub

    Public Function Zapisz(ByVal sprzet As Sprzet) As Integer
        If sprzet Is Nothing Then
            Throw New ArgumentNullException("sprzet")
        End If
        Waliduj(sprzet)
        Dim dal As New EquipmentDAL()
        If dal.CzyNumerZajety(sprzet.NumerInwentarzowy, sprzet.Id) Then
            Throw New BladBiznesowyException("Numer inwentarzowy " & sprzet.NumerInwentarzowy & " jest juz uzyty", "SPRZET-NUMER")
        End If
        If sprzet.Id = 0 Then
            sprzet.Id = dal.Dodaj(sprzet)
            Log.Info("Dodano sprzet " & sprzet.Id & " (" & sprzet.NumerInwentarzowy & ")")
        Else
            Dim ile As Integer = dal.Aktualizuj(sprzet)
            If ile = 0 Then
                Throw New BladBiznesowyException("Sprzet " & sprzet.Id & " nie istnieje")
            End If
            Log.Info("Zaktualizowano sprzet " & sprzet.Id)
        End If
        Return sprzet.Id
    End Function

    Public Sub WyslijDoSerwisu(ByVal sprzetId As Integer)
        Dim s As Sprzet = PobierzSprzet(sprzetId)
        If s Is Nothing Then
            Throw New BladBiznesowyException("Nieznany sprzet: " & sprzetId)
        End If
        If s.Status = "W" Then
            Throw New BladBiznesowyException("Sprzet jest wypozyczony - najpierw zwrot")
        End If
        ZmienStatus(sprzetId, "S")
    End Sub

    Public Sub Wycofaj(ByVal sprzetId As Integer)
        Dim s As Sprzet = PobierzSprzet(sprzetId)
        If s Is Nothing Then
            Throw New BladBiznesowyException("Nieznany sprzet: " & sprzetId)
        End If
        If s.Status = "W" Then
            Throw New BladBiznesowyException("Nie mozna wycofac wypozyczonego sprzetu")
        End If
        ZmienStatus(sprzetId, "X")
    End Sub

    Public Sub PrzywrocZSerwisu(ByVal sprzetId As Integer)
        Dim s As Sprzet = PobierzSprzet(sprzetId)
        If s Is Nothing Then
            Throw New BladBiznesowyException("Nieznany sprzet: " & sprzetId)
        End If
        If s.Status <> "S" Then
            Throw New BladBiznesowyException("Sprzet nie jest w serwisie")
        End If
        ZmienStatus(sprzetId, "D")
    End Sub

    Public Sub ZmienStawke(ByVal sprzetId As Integer, ByVal stawkaDobowa As Decimal, ByVal kaucja As Decimal)
        If stawkaDobowa <= 0 Then
            Throw New BladBiznesowyException("Stawka dobowa musi byc dodatnia")
        End If
        If kaucja < 0 Then
            Throw New BladBiznesowyException("Kaucja nie moze byc ujemna")
        End If
        Dim dal As New EquipmentDAL()
        If dal.ZmienStawke(sprzetId, stawkaDobowa, kaucja) = 0 Then
            Throw New BladBiznesowyException("Nieznany sprzet: " & sprzetId)
        End If
        Log.Info("Sprzet " & sprzetId & ": stawka " & stawkaDobowa & ", kaucja " & kaucja)
    End Sub

    Public Function Szukaj(ByVal fraza As String) As List(Of Sprzet)
        If fraza Is Nothing OrElse fraza.Trim().Length < 2 Then
            Return New List(Of Sprzet)()
        End If
        Return New EquipmentDAL().Szukaj(fraza)
    End Function

    ' kolumny: Id, Klient, DataWydania, PlanowanyZwrot, DataZwrotu, Status
    Public Function HistoriaWypozyczen(ByVal sprzetId As Integer) As DataTable
        Return New EquipmentDAL().HistoriaWypozyczen(sprzetId)
    End Function

    ' kolejne kolizje w zadanym okresie (do podgladu na stronie rezerwacji)
    Public Function KolizjeWTerminie(ByVal sprzetId As Integer, ByVal dataOd As Date, ByVal dataDo As Date) As DataTable
        Return New ReservationDAL().PobierzKolizje(sprzetId, dataOd, dataDo)
    End Function

    Public Function CzyMoznaZmienicKategorie(ByVal sprzetId As Integer) As Boolean
        Dim dal As New EquipmentDAL()
        Return Not dal.MaOtwarteWypozyczenie(sprzetId)
    End Function

    Public Function Kategorie() As Dictionary(Of String, String)
        Dim wynik As New Dictionary(Of String, String)()
        wynik.Add("BUD", "Budowlany")
        wynik.Add("OGR", "Ogrodowy")
        wynik.Add("IMP", "Imprezowy")
        wynik.Add("TRN", "Transportowy")
        Return wynik
    End Function

    Public Function Podsumowanie() As DataTable
        Return New EquipmentDAL().LiczbaWKategoriach()
    End Function

    Private Sub Waliduj(ByVal sprzet As Sprzet)
        If String.IsNullOrEmpty(sprzet.NumerInwentarzowy) Then
            Throw New BladBiznesowyException("Numer inwentarzowy jest wymagany")
        End If
        If String.IsNullOrEmpty(sprzet.Nazwa) Then
            Throw New BladBiznesowyException("Nazwa sprzetu jest wymagana")
        End If
        sprzet.NumerInwentarzowy = sprzet.NumerInwentarzowy.Trim().ToUpper()
        If sprzet.NumerInwentarzowy.Length > 20 Then
            Throw New BladBiznesowyException("Numer inwentarzowy za dlugi (max 20)")
        End If
        If Array.IndexOf(DozwoloneKategorie, sprzet.Kategoria) < 0 Then
            Throw New BladBiznesowyException("Nieznana kategoria: " & sprzet.Kategoria)
        End If
        If Not sprzet.NumerInwentarzowy.StartsWith(sprzet.Kategoria & "-") Then
            Throw New BladBiznesowyException("Numer inwentarzowy musi zaczynac sie od kodu kategorii")
        End If
        If sprzet.StawkaDobowa <= 0 Then
            Throw New BladBiznesowyException("Stawka dobowa musi byc dodatnia")
        End If
        If sprzet.Kaucja < 0 Then
            Throw New BladBiznesowyException("Kaucja nie moze byc ujemna")
        End If
        If String.IsNullOrEmpty(sprzet.Status) Then
            sprzet.Status = "D"
        End If
        If Array.IndexOf(DozwoloneStatusy, sprzet.Status) < 0 Then
            Throw New BladBiznesowyException("Nieznany status: " & sprzet.Status)
        End If
    End Sub

End Class
