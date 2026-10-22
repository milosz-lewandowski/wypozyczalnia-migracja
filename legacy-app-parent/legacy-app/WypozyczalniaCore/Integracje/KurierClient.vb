' Modul: Integracje\KurierClient.vb | Autor: zespol IT | 2015-06-22
' Klient REST firmy kurierskiej: zamowienie dostawy i status przesylki.
' 2015-06-22 MW: pierwsza wersja (API v1, JSON)
' 2019-01-14 PT: TLS 1.2 wymagany przez dostawce

Imports System.Configuration
Imports System.IO
Imports System.Net
Imports System.Text
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Namespace Integracje

    Public Class KurierClient

        Private Const LimitCzasuMs As Integer = 15000

        Private ReadOnly _adresApi As String
        Private ReadOnly _kluczApi As String

        Public Sub New()
            _adresApi = ConfigurationManager.AppSettings("KurierApiUrl")
            _kluczApi = ConfigurationManager.AppSettings("KurierApiKey")
            ServicePointManager.SecurityProtocol = ServicePointManager.SecurityProtocol Or SecurityProtocolType.Tls12
        End Sub

        Public Function ZamowDostawe(ByVal wypozyczenieId As Integer, ByVal adres As String, ByVal telefon As String) As String
            If String.IsNullOrEmpty(_adresApi) Then Return ""
            Dim zlecenie As New JObject()
            zlecenie("referencja") = "WYP-" & wypozyczenieId.ToString("000000")
            zlecenie("adresDostawy") = adres
            zlecenie("telefonOdbiorcy") = telefon
            zlecenie("rodzaj") = "PALETA"
            zlecenie("pobranie") = False
            zlecenie("terminOd") = Date.Today.AddDays(1).ToString("yyyy-MM-dd")
            Dim tresc As String = JsonConvert.SerializeObject(zlecenie)

            Try
                Dim zadanie As HttpWebRequest = UtworzZadanie(_adresApi, "POST")
                Dim bajty As Byte() = Encoding.UTF8.GetBytes(tresc)
                zadanie.ContentType = "application/json; charset=utf-8"
                zadanie.ContentLength = bajty.Length
                Using strumien As Stream = zadanie.GetRequestStream()
                    strumien.Write(bajty, 0, bajty.Length)
                End Using

                Dim odpowiedz As String = CzytajOdpowiedz(zadanie)
                Dim wynik As JObject = JObject.Parse(odpowiedz)
                Dim numer As JToken = wynik("numerPrzesylki")
                If numer Is Nothing OrElse numer.Type = JTokenType.Null Then
                    Return ""
                End If
                Return numer.ToString()
            Catch ex As WebException
                System.Diagnostics.Trace.WriteLine("Kurier: blad zamowienia dla " & wypozyczenieId & ": " & OpisBledu(ex))
                Return ""
            End Try
        End Function

        Public Function StatusPrzesylki(ByVal numerPrzesylki As String) As String
            If String.IsNullOrEmpty(numerPrzesylki) Then Return ""
            Dim adres As String = _adresApi.TrimEnd("/"c) & "/" & Uri.EscapeDataString(numerPrzesylki) & "/status"
            Dim zadanie As HttpWebRequest = UtworzZadanie(adres, "GET")
            Dim odpowiedz As String = CzytajOdpowiedz(zadanie)
            Dim wynik As JObject = JObject.Parse(odpowiedz)
            Dim status As JToken = wynik("status")
            If status Is Nothing Then Return "NIEZNANY"
            Return TlumaczStatus(status.ToString())
        End Function

        Public Function AnulujPrzesylke(ByVal numerPrzesylki As String) As Boolean
            If String.IsNullOrEmpty(numerPrzesylki) OrElse String.IsNullOrEmpty(_adresApi) Then Return False
            Dim adres As String = _adresApi.TrimEnd("/"c) & "/" & Uri.EscapeDataString(numerPrzesylki)
            Try
                Dim zadanie As HttpWebRequest = UtworzZadanie(adres, "DELETE")
                Using odpowiedz As HttpWebResponse = CType(zadanie.GetResponse(), HttpWebResponse)
                    Return odpowiedz.StatusCode = HttpStatusCode.OK OrElse odpowiedz.StatusCode = HttpStatusCode.NoContent
                End Using
            Catch ex As WebException
                System.Diagnostics.Trace.WriteLine("Kurier: blad anulowania " & numerPrzesylki & ": " & OpisBledu(ex))
                Return False
            End Try
        End Function

        Public Function CzyDostepny() As Boolean
            If String.IsNullOrEmpty(_adresApi) Then Return False
            Dim adres As String = _adresApi.TrimEnd("/"c) & "/ping"
            Try
                Dim zadanie As HttpWebRequest = UtworzZadanie(adres, "GET")
                zadanie.Timeout = 3000
                Using odpowiedz As HttpWebResponse = CType(zadanie.GetResponse(), HttpWebResponse)
                    Return odpowiedz.StatusCode = HttpStatusCode.OK
                End Using
            Catch ex As WebException
                Return False
            End Try
        End Function

        Private Function UtworzZadanie(ByVal adres As String, ByVal metoda As String) As HttpWebRequest
            Dim zadanie As HttpWebRequest = CType(WebRequest.Create(adres), HttpWebRequest)
            zadanie.Method = metoda
            zadanie.Accept = "application/json"
            zadanie.Timeout = LimitCzasuMs
            zadanie.ReadWriteTimeout = LimitCzasuMs
            zadanie.Headers.Add("X-Api-Key", _kluczApi)
            zadanie.UserAgent = "WypozyczalniaWeb/1.4"
            Return zadanie
        End Function

        Private Function CzytajOdpowiedz(ByVal zadanie As HttpWebRequest) As String
            Using odpowiedz As HttpWebResponse = CType(zadanie.GetResponse(), HttpWebResponse)
                Using czytnik As New StreamReader(odpowiedz.GetResponseStream(), Encoding.UTF8)
                    Return czytnik.ReadToEnd()
                End Using
            End Using
        End Function

        Private Function OpisBledu(ByVal ex As WebException) As String
            If ex.Response Is Nothing Then Return ex.Status.ToString() & " " & ex.Message
            Try
                Dim http As HttpWebResponse = CType(ex.Response, HttpWebResponse)
                Using czytnik As New StreamReader(http.GetResponseStream(), Encoding.UTF8)
                    Return CInt(http.StatusCode).ToString() & " " & czytnik.ReadToEnd()
                End Using
            Catch
                Return ex.Message
            End Try
        End Function

        Private Function TlumaczStatus(ByVal kod As String) As String
            Select Case kod.ToUpperInvariant()
                Case "CREATED", "NEW"
                    Return "PRZYJETA"
                Case "IN_TRANSIT"
                    Return "W DRODZE"
                Case "DELIVERED"
                    Return "DOSTARCZONA"
                Case "RETURNED"
                    Return "ZWROCONA"
                Case Else
                    Return kod
            End Select
        End Function

    End Class

End Namespace
