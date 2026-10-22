' Modul: Model\Wypozyczenie.vb | Autor: zespol IT | 2014-03-11
' Wypozyczenie (wydanie sprzetu klientowi) wraz z rozliczeniem zwrotu.
' 2015-06-22 MW: NumerPrzesylki (dostawa kurierem)
' 2016-11-03 JK: ZWiersza - mapowanie z DataRow

Imports System.Data

Namespace Model

    Public Class Wypozyczenie

        Public Property Id As Integer
        Public Property RezerwacjaId As Integer
        Public Property KlientId As Integer
        Public Property SprzetId As Integer
        Public Property DataWydania As Date
        Public Property PlanowanyZwrot As Date
        Public Property DataZwrotu As Nullable(Of Date)
        Public Property KaucjaPobrana As Decimal
        Public Property Oplata As Decimal
        Public Property Kara As Decimal
        Public Property KaucjaZwrocona As Decimal
        Public Property Status As String
        Public Property NumerPrzesylki As String

        Public Sub New()
            RezerwacjaId = 0
            Status = "O"
            NumerPrzesylki = ""
        End Sub

        Public ReadOnly Property CzyOtwarte As Boolean
            Get
                Return Status = "O"
            End Get
        End Property

        Public ReadOnly Property CzyZRezerwacji As Boolean
            Get
                Return RezerwacjaId > 0
            End Get
        End Property

        Public ReadOnly Property CzyZDostawa As Boolean
            Get
                Return Not String.IsNullOrEmpty(NumerPrzesylki)
            End Get
        End Property

        Public Function CzyPoTerminie(ByVal naDzien As Date) As Boolean
            If Not CzyOtwarte Then Return False
            Return naDzien > PlanowanyZwrot
        End Function

        Public Shared Function ZWiersza(ByVal wiersz As DataRow) As Wypozyczenie
            Dim w As New Wypozyczenie()
            w.Id = CInt(wiersz("Id"))
            If IsDBNull(wiersz("RezerwacjaId")) Then
                w.RezerwacjaId = 0
            Else
                w.RezerwacjaId = CInt(wiersz("RezerwacjaId"))
            End If
            w.KlientId = CInt(wiersz("KlientId"))
            w.SprzetId = CInt(wiersz("SprzetId"))
            w.DataWydania = CDate(wiersz("DataWydania"))
            w.PlanowanyZwrot = CDate(wiersz("PlanowanyZwrot"))
            If Not IsDBNull(wiersz("DataZwrotu")) Then w.DataZwrotu = CDate(wiersz("DataZwrotu"))
            w.KaucjaPobrana = CDec(wiersz("KaucjaPobrana"))
            If Not IsDBNull(wiersz("Oplata")) Then w.Oplata = CDec(wiersz("Oplata"))
            If Not IsDBNull(wiersz("Kara")) Then w.Kara = CDec(wiersz("Kara"))
            If Not IsDBNull(wiersz("KaucjaZwrocona")) Then w.KaucjaZwrocona = CDec(wiersz("KaucjaZwrocona"))
            w.Status = wiersz("Status").ToString()
            If Not IsDBNull(wiersz("NumerPrzesylki")) Then w.NumerPrzesylki = wiersz("NumerPrzesylki").ToString()
            Return w
        End Function

        Public Overrides Function ToString() As String
            Return String.Format("Wypozyczenie {0} (klient {1}, sprzet {2}, status {3})", Id, KlientId, SprzetId, Status)
        End Function

    End Class

End Namespace
