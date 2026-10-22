' Modul: CustomerManager.vb - obsluga klientow wypozyczalni | Autor: zespol IT | 2014-03-11
' Historia: 2014-03-11 JK utworzenie
'           2015-11-23 MW blokowanie klientow (zalegle platnosci, zgubiony sprzet)
'           2018-09-04 PT klienci firmowi (NIP, NazwaFirmy)
Imports System.Data
Imports System.Data.SqlClient
Imports Wypozyczalnia.Core.Model
Imports Wypozyczalnia.Core.DAL

Public Class CustomerManager

    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(CustomerManager))

    Private Const KolumnyKlienta As String = "Id, Imie, Nazwisko, NazwaFirmy, Email, Telefon, NumerDokumentu, NIP, Typ, ProgRabatu, Zablokowany, PowodBlokady, DataRejestracji"

    Public Function PobierzKlienta(ByVal klientId As Integer) As Klient
        Dim dt As DataTable = DbHelper.WykonajZapytanie( _
            "SELECT " & KolumnyKlienta & " FROM Klienci WHERE Id = @Id", _
            DbHelper.Parametr("@Id", klientId))
        If dt.Rows.Count = 0 Then
            Return Nothing
        End If
        Return MapujKlienta(dt.Rows(0))
    End Function

    Public Function CzyMozeWypozyczac(ByVal klientId As Integer) As Boolean
        Dim k As Klient = PobierzKlienta(klientId)
        If k Is Nothing Then
            Log.Warn("Sprawdzenie nieistniejacego klienta: " & klientId)
            Return False
        End If
        If k.Zablokowany Then
            Log.Info("Klient " & klientId & " zablokowany: " & k.PowodBlokady)
            Return False
        End If
        Return True
    End Function

    Public Function ProgRabatu(ByVal klientId As Integer) As Integer
        Dim dt As DataTable = DbHelper.WykonajZapytanie( _
            "SELECT ProgRabatu FROM Klienci WHERE Id = @Id", _
            DbHelper.Parametr("@Id", klientId))
        If dt.Rows.Count = 0 Then
            Return 0
        End If
        Dim prog As Integer = dt.Rows(0)("ProgRabatu")
        If prog < 0 Then prog = 0
        If prog > 3 Then prog = 3
        Return prog
    End Function

    Public Function Szukaj(ByVal fraza As String) As DataTable
        If fraza Is Nothing Then fraza = ""
        fraza = fraza.Trim()
        Dim sql As String = "SELECT TOP 200 Id, Imie, Nazwisko, NazwaFirmy, Email, Telefon FROM Klienci " & _
                            "WHERE (@Fraza = '' OR Nazwisko LIKE @Wzorzec OR NazwaFirmy LIKE @Wzorzec OR Email LIKE @Wzorzec) " & _
                            "ORDER BY Nazwisko, Imie"
        Return DbHelper.WykonajZapytanie(sql, _
            DbHelper.Parametr("@Fraza", fraza), _
            DbHelper.Parametr("@Wzorzec", "%" & fraza & "%"))
    End Function

    Public Function Zapisz(ByVal klient As Klient) As Integer
        If klient Is Nothing Then Throw New ArgumentNullException("klient")
        If String.IsNullOrEmpty(klient.Nazwisko) AndAlso String.IsNullOrEmpty(klient.NazwaFirmy) Then
            Throw New BladBiznesowyException("Podaj nazwisko albo nazwe firmy", "KLI-01")
        End If
        If String.IsNullOrEmpty(klient.Typ) Then klient.Typ = "I"
        If klient.Typ = "F" AndAlso String.IsNullOrEmpty(klient.NIP) Then
            Throw New BladBiznesowyException("Klient firmowy wymaga numeru NIP", "KLI-02")
        End If
        If klient.Imie Is Nothing Then klient.Imie = ""
        If klient.Nazwisko Is Nothing Then klient.Nazwisko = ""

        If klient.Id = 0 Then
            Dim dt As DataTable = DbHelper.WykonajZapytanie( _
                "INSERT INTO Klienci (Imie, Nazwisko, NazwaFirmy, Email, Telefon, NumerDokumentu, NIP, Typ, ProgRabatu) " & _
                "VALUES (@Imie, @Nazwisko, @NazwaFirmy, @Email, @Telefon, @NumerDokumentu, @NIP, @Typ, @ProgRabatu); " & _
                "SELECT CAST(SCOPE_IDENTITY() AS INT) AS NoweId", _
                ParametryKlienta(klient))
            klient.Id = dt.Rows(0)("NoweId")
            Log.Info("Dodano klienta " & klient.Id)
            ZapiszDziennik("KLIENT_NOWY", "Id=" & klient.Id)
        Else
            Dim parametry As New List(Of SqlParameter)(ParametryKlienta(klient))
            parametry.Add(DbHelper.Parametr("@Id", klient.Id))
            Dim zmienione As Integer = DbHelper.WykonajPolecenie( _
                "UPDATE Klienci SET Imie = @Imie, Nazwisko = @Nazwisko, NazwaFirmy = @NazwaFirmy, Email = @Email, " & _
                "Telefon = @Telefon, NumerDokumentu = @NumerDokumentu, NIP = @NIP, Typ = @Typ, ProgRabatu = @ProgRabatu " & _
                "WHERE Id = @Id", _
                parametry.ToArray())
            If zmienione = 0 Then
                Throw New BladBiznesowyException("Nie znaleziono klienta nr " & klient.Id, "KLI-03")
            End If
            Log.Info("Zaktualizowano klienta " & klient.Id)
        End If
        Return klient.Id
    End Function

    Public Sub Zablokuj(ByVal klientId As Integer, ByVal powod As String)
        If powod IsNot Nothing AndAlso powod.Length > 200 Then
            powod = powod.Substring(0, 200)
        End If
        DbHelper.WykonajPolecenie( _
            "UPDATE Klienci SET Zablokowany = 1, PowodBlokady = @Powod WHERE Id = @Id", _
            DbHelper.Parametr("@Powod", powod), _
            DbHelper.Parametr("@Id", klientId))
        Log.Warn("Zablokowano klienta " & klientId & ": " & powod)
        ZapiszDziennik("KLIENT_BLOKADA", "Id=" & klientId & "; " & powod)
    End Sub

    Public Sub Odblokuj(ByVal klientId As Integer)
        DbHelper.WykonajPolecenie( _
            "UPDATE Klienci SET Zablokowany = 0, PowodBlokady = NULL WHERE Id = @Id", _
            DbHelper.Parametr("@Id", klientId))
        ZapiszDziennik("KLIENT_ODBLOKOWANIE", "Id=" & klientId)
    End Sub

    Public Function PobierzEmail(ByVal klientId As Integer) As String
        Dim dt As DataTable = DbHelper.WykonajZapytanie( _
            "SELECT Email FROM Klienci WHERE Id = @Id", DbHelper.Parametr("@Id", klientId))
        If dt.Rows.Count = 0 Then Return ""
        Return dt.Rows(0)("Email").ToString()
    End Function

    Public Function LiczbaOtwartychWypozyczen(ByVal klientId As Integer) As Integer
        Dim dt As DataTable = DbHelper.WykonajZapytanie( _
            "SELECT COUNT(*) AS Liczba FROM Wypozyczenia WHERE KlientId = @Id AND Status = 'O'", _
            DbHelper.Parametr("@Id", klientId))
        If dt.Rows.Count = 0 Then Return 0
        Return dt.Rows(0)("Liczba")
    End Function

    Public Function KlienciZablokowani() As DataTable
        Return DbHelper.WykonajZapytanie( _
            "SELECT Id, Imie, Nazwisko, NazwaFirmy, Email, Telefon, PowodBlokady FROM Klienci " & _
            "WHERE Zablokowany = 1 ORDER BY Nazwisko, Imie")
    End Function

    Public Function CzyEmailZajety(ByVal email As String, ByVal pominKlientaId As Integer) As Boolean
        If String.IsNullOrEmpty(email) Then Return False
        Dim dt As DataTable = DbHelper.WykonajZapytanie( _
            "SELECT COUNT(*) AS Liczba FROM Klienci WHERE Email = @Email AND Id <> @Id", _
            DbHelper.Parametr("@Email", email.Trim()), _
            DbHelper.Parametr("@Id", pominKlientaId))
        Return CInt(dt.Rows(0)("Liczba")) > 0
    End Function

    Private Function ParametryKlienta(ByVal k As Klient) As SqlParameter()
        Return New SqlParameter() { _
            DbHelper.Parametr("@Imie", k.Imie), _
            DbHelper.Parametr("@Nazwisko", k.Nazwisko), _
            DbHelper.Parametr("@NazwaFirmy", PustyNaNull(k.NazwaFirmy)), _
            DbHelper.Parametr("@Email", k.Email), _
            DbHelper.Parametr("@Telefon", k.Telefon), _
            DbHelper.Parametr("@NumerDokumentu", PustyNaNull(k.NumerDokumentu)), _
            DbHelper.Parametr("@NIP", PustyNaNull(k.NIP)), _
            DbHelper.Parametr("@Typ", k.Typ), _
            DbHelper.Parametr("@ProgRabatu", k.ProgRabatu)}
    End Function

    Private Function MapujKlienta(ByVal r As DataRow) As Klient
        Dim k As New Klient()
        k.Id = r("Id")
        k.Imie = r("Imie").ToString()
        k.Nazwisko = r("Nazwisko").ToString()
        k.NazwaFirmy = If(IsDBNull(r("NazwaFirmy")), Nothing, r("NazwaFirmy").ToString())
        k.Email = r("Email").ToString()
        k.Telefon = r("Telefon").ToString()
        k.NumerDokumentu = If(IsDBNull(r("NumerDokumentu")), Nothing, r("NumerDokumentu").ToString())
        k.NIP = If(IsDBNull(r("NIP")), Nothing, r("NIP").ToString())
        k.Typ = r("Typ").ToString()
        k.ProgRabatu = r("ProgRabatu")
        k.Zablokowany = r("Zablokowany")
        k.PowodBlokady = If(IsDBNull(r("PowodBlokady")), Nothing, r("PowodBlokady").ToString())
        k.DataRejestracji = r("DataRejestracji")
        Return k
    End Function

    Private Function PustyNaNull(ByVal wartosc As String) As String
        If wartosc Is Nothing OrElse wartosc.Trim() = "" Then Return Nothing
        Return wartosc.Trim()
    End Function

    Private Sub ZapiszDziennik(ByVal operacja As String, ByVal szczegoly As String)
        Try
            DbHelper.WykonajPolecenie( _
                "INSERT INTO DziennikOperacji (Operacja, Szczegoly) VALUES (@Operacja, @Szczegoly)", _
                DbHelper.Parametr("@Operacja", operacja), _
                DbHelper.Parametr("@Szczegoly", szczegoly))
        Catch ex As SqlException
            Log.Error("Nie udalo sie zapisac dziennika operacji", ex)
        End Try
    End Sub

End Class
