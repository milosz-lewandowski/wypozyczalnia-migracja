' Modul: Model\Klient.vb - encja klienta wypozyczalni | Autor: zespol IT | 2014-03-11
' Historia: 2014-03-11 JK utworzenie
'           2018-09-04 PT NazwaFirmy, NIP, Typ (klienci firmowi)

Namespace Model

    <Serializable()>
    Public Class Klient

        Public Property Id As Integer
        Public Property Imie As String
        Public Property Nazwisko As String
        Public Property NazwaFirmy As String
        Public Property Email As String
        Public Property Telefon As String
        Public Property NumerDokumentu As String
        Public Property NIP As String

        ' I = osoba prywatna, F = firma
        Public Property Typ As String

        ' 0 - 3, ustawiany recznie przez obsluge
        Public Property ProgRabatu As Integer

        Public Property Zablokowany As Boolean
        Public Property PowodBlokady As String
        Public Property DataRejestracji As Date

        Public Sub New()
            Typ = "I"
            ProgRabatu = 0
            Zablokowany = False
            DataRejestracji = Date.Now
        End Sub

        Public ReadOnly Property CzyFirma As Boolean
            Get
                Return Typ = "F"
            End Get
        End Property

        Public ReadOnly Property NazwaWyswietlana As String
            Get
                If CzyFirma AndAlso Not String.IsNullOrEmpty(NazwaFirmy) Then
                    Return NazwaFirmy & " (" & Imie & " " & Nazwisko & ")"
                End If
                Return Imie & " " & Nazwisko
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return Id.ToString() & " - " & NazwaWyswietlana
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Dim inny As Klient = TryCast(obj, Klient)
            If inny Is Nothing Then Return False
            Return inny.Id = Me.Id
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return Id.GetHashCode()
        End Function

    End Class

End Namespace
