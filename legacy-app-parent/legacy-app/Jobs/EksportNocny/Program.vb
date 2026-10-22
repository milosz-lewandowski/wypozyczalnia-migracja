' Modul: Program.vb - zadanie nocne EksportNocny | Autor: zespol IT | 2016-10-12
' Uruchamiane z Harmonogramu zadan o 02:30: import cennika z katalogu XML, eksport CSV za wczoraj.
' 2017-06-12 JK: tryb import (cennik XML)
' 2018-04-03 PT: eksport do katalogu podanego jawnie, nie z Web.config

Imports System.Diagnostics
Imports System.Globalization
Imports System.IO
Imports System.Threading
Imports Wypozyczalnia.Core.Integracje

Module Program

    Private Const KatalogImportu As String = "D:\Wypozyczalnia\import\cennik"
    Private Const KatalogEksportu As String = "\\plikownia\eksport$\wypozyczalnia"
    Private Const KatalogAwaryjny As String = "D:\Wypozyczalnia\eksport-awaryjny"

    Private Const KodOk As Integer = 0
    Private Const KodBlad As Integer = 1

    Sub Main(ByVal args() As String)
        Thread.CurrentThread.CurrentCulture = New CultureInfo("pl-PL")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("pl-PL")

        Dim tryb As String = OdczytajTryb(args)
        Dim dzien As Date = OdczytajDzien(args)
        Dim kod As Integer = KodOk
        Dim stoper As Stopwatch = Stopwatch.StartNew()

        Loguj("Start EksportNocny, tryb=" & tryb & ", dzien=" & dzien.ToString("yyyy-MM-dd"))

        Select Case tryb
            Case "pelny"
                If Not Importuj() Then kod = KodBlad
                If Not Eksportuj(dzien) Then kod = KodBlad
            Case "import"
                If Not Importuj() Then kod = KodBlad
            Case "eksport"
                If Not Eksportuj(dzien) Then kod = KodBlad
            Case Else
                Loguj("Nieznany tryb: " & tryb)
                WypiszPomoc()
                kod = KodBlad
        End Select

        If kod = KodOk AndAlso tryb <> "import" Then
            SprzatnijKopie(KatalogEksportu, 30)
        End If

        stoper.Stop()
        Loguj("Czas wykonania: " & stoper.Elapsed.TotalSeconds.ToString("0.0") & " s")
        Loguj("Koniec EksportNocny, kod wyjscia " & kod)
        Environment.Exit(kod)
    End Sub

    Private Function Importuj() As Boolean
        Try
            If Not Directory.Exists(KatalogImportu) Then
                Loguj("Brak katalogu importu " & KatalogImportu & " - pomijam import")
                Return True
            End If
            Dim import As New ImportCennikaXml()
            Dim ilePlikow As Integer = import.ImportujKatalog(KatalogImportu)
            Loguj("Import cennika: przetworzono plikow " & ilePlikow)
            Return True
        Catch ex As IOException
            Loguj("Blad dostepu do katalogu importu: " & ex.Message)
            Return False
        Catch ex As Exception
            Loguj("Blad importu cennika: " & ex.ToString())
            Return False
        End Try
    End Function

    Private Function Eksportuj(ByVal dzien As Date) As Boolean
        Dim eksport As New EksportCsv()
        Try
            Dim plik As String = eksport.EksportujDzien(dzien, KatalogEksportu)
            If Not eksport.CzyWyeksportowano(dzien, KatalogEksportu) Then
                Loguj("Plik eksportu nie pojawil sie na udziale, zapis awaryjny do " & KatalogAwaryjny)
                plik = eksport.EksportujDzien(dzien, KatalogAwaryjny)
            End If
            Loguj("Eksport CSV: " & plik)
            Return True
        Catch ex As Exception
            Loguj("Blad eksportu CSV: " & ex.ToString())
            Return False
        End Try
    End Function

    ' kopie .bak zostawiane przez eksport przy nadpisaniu pliku
    Private Sub SprzatnijKopie(ByVal katalog As String, ByVal dni As Integer)
        Try
            If Not Directory.Exists(katalog) Then Return
            Dim granica As Date = Date.Now.AddDays(-dni)
            Dim usuniete As Integer = 0
            For Each plik As String In Directory.GetFiles(katalog, "*.csv.bak")
                If File.GetLastWriteTime(plik) < granica Then
                    File.Delete(plik)
                    usuniete = usuniete + 1
                End If
            Next
            If usuniete > 0 Then
                Loguj("Usunieto starych kopii: " & usuniete)
            End If
        Catch ex As Exception
            Loguj("Nie udalo sie posprzatac kopii w " & katalog & ": " & ex.Message)
        End Try
    End Sub

    Private Function OdczytajTryb(ByVal args() As String) As String
        If args Is Nothing Then Return "pelny"
        For Each arg As String In args
            If arg.StartsWith("/tryb:", StringComparison.OrdinalIgnoreCase) Then
                Return arg.Substring(6).Trim().ToLower()
            End If
        Next
        Return "pelny"
    End Function

    ' domyslnie dzien poprzedni; /dzien:2018-04-02 do recznego powtorzenia eksportu
    Private Function OdczytajDzien(ByVal args() As String) As Date
        Dim wynik As Date = Date.Today.AddDays(-1)
        If args Is Nothing Then Return wynik
        For Each arg As String In args
            If arg.StartsWith("/dzien:", StringComparison.OrdinalIgnoreCase) Then
                Dim tekst As String = arg.Substring(7).Trim()
                Dim d As Date
                If Date.TryParseExact(tekst, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, d) Then
                    wynik = d
                Else
                    Loguj("Niepoprawna data w parametrze: " & tekst & " - uzywam " & wynik.ToString("yyyy-MM-dd"))
                End If
            End If
        Next
        Return wynik
    End Function

    Private Sub WypiszPomoc()
        Console.WriteLine("Uzycie: EksportNocny.exe /tryb:pelny|import|eksport [/dzien:rrrr-mm-dd]")
        Console.WriteLine("  pelny   - import cennika XML, potem eksport CSV (domyslnie)")
        Console.WriteLine("  import  - tylko import cennika z " & KatalogImportu)
        Console.WriteLine("  eksport - tylko eksport CSV do " & KatalogEksportu)
    End Sub

    Private Sub Loguj(ByVal tekst As String)
        Console.WriteLine(Date.Now.ToString("yyyy-MM-dd HH:mm:ss") & " " & tekst)
    End Sub

End Module
