Option Strict Off
' Modul: PricingManager - oplata za wynajem, rabaty, taryfy | Autor: zespol IT | 2014-03-11
' Historia:
'   2015-02-04 MW: mnoznik weekendowy i rabat tygodniowy z tabeli Taryfy
'   2016-11-08 JK: rabat stalego klienta wg progu
'   2019-04-02 PT: SzczegolyOplaty dla kalkulatora w Cennik.aspx

Imports System.Data
Imports System.Text
Imports System.Globalization
Imports Wypozyczalnia.Core.Model
Imports Wypozyczalnia.Core.DAL

Public Class PricingManager

    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(PricingManager))

    Private Const MaksMnoznikWeekend As Decimal = 3D
    Private Const MaksRabatTygodniowy As Decimal = 50D
    Private Const MaksStawkaDobowa As Decimal = 10000D

    Public Function LiczbaDob(ByVal dataOd As Date, ByVal dataDo As Date) As Integer
        Dim minuty As Long = DateDiff(DateInterval.Minute, dataOd, dataDo)
        If minuty <= 0 Then Throw New ArgumentException("Data zwrotu musi byc pozniejsza niz data wydania")
        Dim doby As Integer = CInt(minuty \ 1440)
        If minuty Mod 1440 > 60 Then doby = doby + 1   ' rozpoczeta doba liczona jako pelna (1 h tolerancji)
        If doby = 0 Then doby = 1
        Return doby
    End Function

    Public Function ObliczOplate(ByVal sprzetId As Integer, ByVal dataOd As Date, ByVal dataDo As Date, ByVal klientId As Integer) As Decimal
        Dim sprzet As Sprzet = New EquipmentManager().PobierzSprzet(sprzetId)
        If sprzet Is Nothing Then Throw New BladBiznesowyException("Nieznany sprzet: " & sprzetId)
        Dim taryfa As Taryfa = New PricingDAL().PobierzTaryfe(sprzet.Kategoria, dataOd)
        Dim doby As Integer = LiczbaDob(dataOd, dataDo)
        Dim stawkaWeekend As Decimal = Math.Round(sprzet.StawkaDobowa * taryfa.MnoznikWeekend, 2)
        Dim kwota As Decimal = 0
        For k As Integer = 0 To doby - 1
            Dim dzien As DayOfWeek = dataOd.AddDays(k).DayOfWeek
            If dzien = DayOfWeek.Saturday Or dzien = DayOfWeek.Sunday Then
                kwota = kwota + stawkaWeekend
            Else
                kwota = kwota + sprzet.StawkaDobowa
            End If
        Next
        If doby \ 7 >= 1 Then
            kwota = kwota - Math.Round(kwota * taryfa.RabatTygodniowyProc / 100D, 2)
        End If
        ' rabat 2,5% za kazdy prog stalego klienta (prog 1 = 2,5%)
        Dim prog = New CustomerManager().ProgRabatu(klientId)
        Dim rabatProc As Integer = RabatProgu(prog)
        If rabatProc > 0 Then
            kwota = Math.Round(kwota * (100 - rabatProc) / 100D, 2)
        End If
        Return kwota
    End Function

    Public Function RabatProgu(ByVal prog As Integer) As Integer
        Return CInt(prog * 2.5)
    End Function

    Public Function ObliczOplateBezKlienta(ByVal sprzetId As Integer, ByVal dataOd As Date, ByVal dataDo As Date) As Decimal
        Return ObliczOplate(sprzetId, dataOd, dataDo, 0)
    End Function

    Public Function LiczbaDobWeekendowych(ByVal dataOd As Date, ByVal dataDo As Date) As Integer
        Dim doby As Integer = LiczbaDob(dataOd, dataDo)
        Dim ile As Integer = 0
        For k As Integer = 0 To doby - 1
            If CzyWeekend(dataOd.AddDays(k)) Then ile = ile + 1
        Next
        Return ile
    End Function

    Public Function SzczegolyOplaty(ByVal sprzetId As Integer, ByVal dataOd As Date, ByVal dataDo As Date, ByVal klientId As Integer) As String
        Dim sprzet As Sprzet = New EquipmentManager().PobierzSprzet(sprzetId)
        If sprzet Is Nothing Then Return "Nieznany sprzet: " & sprzetId
        Dim taryfa As Taryfa = PobierzTaryfeDlaSprzetu(sprzetId, dataOd)
        Dim doby As Integer = LiczbaDob(dataOd, dataDo)
        Dim weekendowe As Integer = LiczbaDobWeekendowych(dataOd, dataDo)
        Dim prog As Integer = New CustomerManager().ProgRabatu(klientId)
        Dim kultura As New CultureInfo("pl-PL")
        Dim sb As New StringBuilder()
        sb.Append(sprzet.Nazwa).Append(" (").Append(sprzet.NumerInwentarzowy).Append(")")
        sb.Append(" | dob: ").Append(doby)
        sb.Append(" (w tym weekendowych: ").Append(weekendowe).Append(")")
        sb.Append(" | stawka: ").Append(sprzet.StawkaDobowa.ToString("N2", kultura))
        sb.Append(" | weekend x").Append(taryfa.MnoznikWeekend.ToString(kultura))
        If doby \ 7 >= 1 Then
            sb.Append(" | rabat tygodniowy ").Append(taryfa.RabatTygodniowyProc.ToString(kultura)).Append("%")
        End If
        If prog > 0 Then
            sb.Append(" | ").Append(OpisRabatu(prog))
        End If
        sb.Append(" | razem: ").Append(ObliczOplate(sprzetId, dataOd, dataDo, klientId).ToString("N2", kultura))
        Return sb.ToString()
    End Function

    Public Function OpisRabatu(ByVal prog As Integer) As String
        If prog <= 0 Then Return "brak rabatu"
        Return "staly klient, prog " & prog & ": " & RabatProgu(prog) & "%"
    End Function

    Public Function PobierzTaryfeDlaSprzetu(ByVal sprzetId As Integer, ByVal naDzien As Date) As Taryfa
        Dim sprzet As Sprzet = New EquipmentManager().PobierzSprzet(sprzetId)
        If sprzet Is Nothing Then Throw New BladBiznesowyException("Nieznany sprzet: " & sprzetId)
        Return New PricingDAL().PobierzTaryfe(sprzet.Kategoria, naDzien)
    End Function

    Public Function ListaTaryf() As DataTable
        Return New PricingDAL().ListaTaryf()
    End Function

    Public Sub ZapiszTaryfe(ByVal t As Taryfa)
        If t Is Nothing Then Throw New ArgumentNullException("t")
        If Not Taryfa.CzyZnanaKategoria(t.Kategoria) Then
            Throw New ArgumentException("Nieznana kategoria: " & t.Kategoria)
        End If
        If t.MnoznikWeekend < 1D Or t.MnoznikWeekend > MaksMnoznikWeekend Then
            Throw New ArgumentException("Mnoznik weekendowy poza zakresem 1-" & MaksMnoznikWeekend)
        End If
        If t.RabatTygodniowyProc < 0D Or t.RabatTygodniowyProc > MaksRabatTygodniowy Then
            Throw New ArgumentException("Rabat tygodniowy poza zakresem 0-" & MaksRabatTygodniowy)
        End If
        Dim dal As New PricingDAL()
        dal.ZapiszTaryfe(t)
        dal.ZapiszWDzienniku("TARYFA", t.ToString())
        Log.Info("Zapisano taryfe " & t.ToString())
    End Sub

    Public Sub ZmienStawke(ByVal sprzetId As Integer, ByVal nowaStawka As Decimal)
        If nowaStawka <= 0D Or nowaStawka > MaksStawkaDobowa Then
            Throw New ArgumentException("Nieprawidlowa stawka dobowa: " & nowaStawka)
        End If
        Dim em As New EquipmentManager()
        Dim sprzet As Sprzet = em.PobierzSprzet(sprzetId)
        If sprzet Is Nothing Then Throw New BladBiznesowyException("Nieznany sprzet: " & sprzetId)
        Dim stara As Decimal = sprzet.StawkaDobowa
        sprzet.StawkaDobowa = nowaStawka
        em.Zapisz(sprzet)
        Call New PricingDAL().ZapiszWDzienniku("STAWKA", sprzet.NumerInwentarzowy & ": " & stara & " -> " & nowaStawka)
        Log.Info("Zmiana stawki " & sprzet.NumerInwentarzowy & " z " & stara & " na " & nowaStawka)
    End Sub

    Public Function KaucjaDlaSprzetu(ByVal sprzetId As Integer) As Decimal
        Dim sprzet As Sprzet = New EquipmentManager().PobierzSprzet(sprzetId)
        If sprzet Is Nothing Then Return 0D
        Return sprzet.Kaucja
    End Function

    Public Function PrzeliczRezerwacjeNowe() As Integer
        Dim sql As String = "SELECT Id, KlientId, SprzetId, DataOd, DataDo, CenaSzacunkowa FROM Rezerwacje WHERE Status = 'N'"
        Dim dt As DataTable = DbHelper.WykonajZapytanie(sql)
        Dim zmienione As Integer = 0
        For Each row As DataRow In dt.Rows
            Try
                Dim nowa As Decimal = ObliczOplate(row("SprzetId"), row("DataOd"), row("DataDo"), row("KlientId"))
                If nowa <> row("CenaSzacunkowa") Then
                    DbHelper.WykonajPolecenie("UPDATE Rezerwacje SET CenaSzacunkowa = @Cena WHERE Id = @Id",
                        DbHelper.Parametr("@Cena", nowa),
                        DbHelper.Parametr("@Id", row("Id")))
                    zmienione = zmienione + 1
                End If
            Catch ex As BladBiznesowyException
                Log.Warn("Nie przeliczono rezerwacji " & row("Id") & ": " & ex.Message)
            End Try
        Next
        Log.Info("Przeliczono rezerwacje nowe, zmienionych: " & zmienione)
        Return zmienione
    End Function

    Public Function CennikKategorii(ByVal kategoria As String, ByVal naDzien As Date) As DataTable
        Dim wynik As New DataTable("Cennik")
        wynik.Columns.Add("Id", GetType(Integer))
        wynik.Columns.Add("Nazwa", GetType(String))
        wynik.Columns.Add("StawkaDobowa", GetType(Decimal))
        wynik.Columns.Add("StawkaWeekend", GetType(Decimal))
        wynik.Columns.Add("Kaucja", GetType(Decimal))
        Dim taryfa As Taryfa = New PricingDAL().PobierzTaryfe(kategoria, naDzien)
        For Each s As Sprzet In New EquipmentManager().ListaWKategorii(kategoria)
            Dim r As DataRow = wynik.NewRow()
            r("Id") = s.Id
            r("Nazwa") = s.Nazwa
            r("StawkaDobowa") = s.StawkaDobowa
            r("StawkaWeekend") = s.StawkaDobowa * taryfa.MnoznikWeekend
            r("Kaucja") = s.Kaucja
            wynik.Rows.Add(r)
        Next
        Return wynik
    End Function

    Public Function SprawdzTerminy(ByVal dataOd As Date, ByVal dataDo As Date) As String
        If dataDo <= dataOd Then Return "Data zwrotu musi byc pozniejsza niz data wydania"
        If dataOd < Date.Today.AddDays(-1) Then Return "Data wydania z przeszlosci"
        If DateDiff(DateInterval.Day, dataOd, dataDo) > 90 Then Return "Maksymalny okres wynajmu to 90 dni"
        Return ""
    End Function

    Public Function SymulujOplate(ByVal sprzetId As Integer, ByVal dataOd As Date, ByVal liczbaDob As Integer, ByVal klientId As Integer) As Decimal
        If liczbaDob < 1 Then Throw New ArgumentException("Liczba dob musi byc dodatnia")
        Return ObliczOplate(sprzetId, dataOd, dataOd.AddDays(liczbaDob), klientId)
    End Function

    Public Function TabelaOplat(ByVal sprzetId As Integer, ByVal dataOd As Date, ByVal klientId As Integer, ByVal maksDob As Integer) As DataTable
        Dim wynik As New DataTable("TabelaOplat")
        wynik.Columns.Add("Doby", GetType(Integer))
        wynik.Columns.Add("DataDo", GetType(Date))
        wynik.Columns.Add("Oplata", GetType(Decimal))
        wynik.Columns.Add("ZaDobe", GetType(Decimal))
        If maksDob < 1 Then maksDob = 1
        If maksDob > 30 Then maksDob = 30
        For d As Integer = 1 To maksDob
            Dim r As DataRow = wynik.NewRow()
            Dim kwota As Decimal = SymulujOplate(sprzetId, dataOd, d, klientId)
            r("Doby") = d
            r("DataDo") = dataOd.AddDays(d)
            r("Oplata") = kwota
            r("ZaDobe") = kwota / d
            wynik.Rows.Add(r)
        Next
        Return wynik
    End Function

    Public Function TaryfyNaDzien(ByVal naDzien As Date) As List(Of Taryfa)
        Return New PricingDAL().TaryfyNaDzien(naDzien)
    End Function

    Public Function HistoriaTaryf(ByVal kategoria As String) As String
        Dim lista As List(Of Taryfa) = New PricingDAL().TaryfyKategorii(kategoria)
        If lista.Count = 0 Then Return "Brak taryf dla kategorii " & kategoria
        Dim sb As New StringBuilder()
        sb.AppendLine(Taryfa.NazwaKategorii(kategoria) & ":")
        Dim poprzednia As Taryfa = Nothing
        For Each t As Taryfa In lista
            sb.Append("  ").Append(t.ToString())
            If poprzednia IsNot Nothing AndAlso poprzednia.MnoznikWeekend <> t.MnoznikWeekend Then
                sb.Append(" [zmiana mnoznika: ").Append(t.MnoznikWeekend).Append(" -> ").Append(poprzednia.MnoznikWeekend).Append("]")
            End If
            sb.AppendLine()
            poprzednia = t
        Next
        Return sb.ToString()
    End Function

    Public Function ZmienKaucje(ByVal sprzetId As Integer, ByVal nowaKaucja As Decimal) As Boolean
        If nowaKaucja < 0D Then Throw New ArgumentException("Kaucja nie moze byc ujemna")
        Dim dal As New PricingDAL()
        Dim ile As Integer = dal.ZaktualizujKaucje(sprzetId, nowaKaucja)
        If ile = 0 Then
            Log.Warn("Nie zmieniono kaucji sprzetu " & sprzetId)
            Return False
        End If
        dal.ZapiszWDzienniku("STAWKA", "kaucja sprzetu " & sprzetId & ": " & nowaKaucja)
        Return True
    End Function

    Public Function BrakujaceTaryfy(ByVal naDzien As Date) As List(Of String)
        Dim brak As New List(Of String)()
        Dim dal As New PricingDAL()
        For Each kategoria As String In Taryfa.KategorieZnane
            Try
                dal.PobierzTaryfe(kategoria, naDzien)
            Catch ex As BladBiznesowyException
                brak.Add(kategoria)
            End Try
        Next
        If brak.Count > 0 Then Log.Warn("Brak taryf na " & naDzien.ToShortDateString() & ": " & String.Join(",", brak.ToArray()))
        Return brak
    End Function

    Public Function ZestawienieKategorii(ByVal naDzien As Date) As DataTable
        Dim wynik As New DataTable("Zestawienie")
        wynik.Columns.Add("Kategoria", GetType(String))
        wynik.Columns.Add("Nazwa", GetType(String))
        wynik.Columns.Add("MnoznikWeekend", GetType(Decimal))
        wynik.Columns.Add("RabatTygodniowyProc", GetType(Decimal))
        wynik.Columns.Add("LiczbaSprzetu", GetType(Integer))
        Dim em As New EquipmentManager()
        For Each t As Taryfa In TaryfyNaDzien(naDzien)
            Dim r As DataRow = wynik.NewRow()
            r("Kategoria") = t.Kategoria
            r("Nazwa") = Taryfa.NazwaKategorii(t.Kategoria)
            r("MnoznikWeekend") = t.MnoznikWeekend
            r("RabatTygodniowyProc") = t.RabatTygodniowyProc
            r("LiczbaSprzetu") = em.ListaWKategorii(t.Kategoria).Count
            wynik.Rows.Add(r)
        Next
        Return wynik
    End Function

    Public Function CzyCenaAktualna(ByVal rezerwacjaId As Integer) As Boolean
        Dim dt As DataTable = DbHelper.WykonajZapytanie("SELECT KlientId, SprzetId, DataOd, DataDo, CenaSzacunkowa FROM Rezerwacje WHERE Id = @Id",
            DbHelper.Parametr("@Id", rezerwacjaId))
        If dt.Rows.Count = 0 Then Return False
        Dim r As DataRow = dt.Rows(0)
        Try
            Return ObliczOplate(r("SprzetId"), r("DataOd"), r("DataDo"), r("KlientId")) = r("CenaSzacunkowa")
        Catch ex As BladBiznesowyException
            Log.Warn("Nie mozna sprawdzic ceny rezerwacji " & rezerwacjaId & ": " & ex.Message)
            Return False
        End Try
    End Function

    Private Function CzyWeekend(ByVal dzien As Date) As Boolean
        Return dzien.DayOfWeek = DayOfWeek.Saturday Or dzien.DayOfWeek = DayOfWeek.Sunday
    End Function

End Class
