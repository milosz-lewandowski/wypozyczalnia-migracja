' Modul: Model - rezerwacja sprzetu na termin | Autor: zespol IT | 2014-03-11
' Historia:
'   2014-09-22 JK  CenaSzacunkowa zapisywana przy rezerwacji (wczesniej liczona w locie)
'   2017-04-03 MW  status A (anulowana) zamiast DELETE

Namespace Model

    <Serializable()>
    Public Class Rezerwacja

        Public Const StatusNowa As String = "N"
        Public Const StatusPotwierdzona As String = "P"
        Public Const StatusAnulowana As String = "A"
        Public Const StatusZrealizowana As String = "Z"

        Public Property Id As Integer
        Public Property KlientId As Integer
        Public Property SprzetId As Integer
        Public Property DataOd As Date
        Public Property DataDo As Date
        Public Property Status As String
        Public Property CenaSzacunkowa As Decimal
        Public Property Uwagi As String

        Public Sub New()
            Id = 0
            KlientId = 0
            SprzetId = 0
            DataOd = Date.MinValue
            DataDo = Date.MinValue
            Status = StatusNowa
            CenaSzacunkowa = 0D
            Uwagi = ""
        End Sub

        ' N i P blokuja termin, A i Z juz nie
        Public ReadOnly Property CzyAktywna As Boolean
            Get
                Return Status = StatusNowa OrElse Status = StatusPotwierdzona
            End Get
        End Property

        Public ReadOnly Property OpisStatusu As String
            Get
                Select Case Status
                    Case StatusNowa : Return "nowa"
                    Case StatusPotwierdzona : Return "potwierdzona"
                    Case StatusAnulowana : Return "anulowana"
                    Case StatusZrealizowana : Return "zrealizowana"
                    Case Else : Return "?"
                End Select
            End Get
        End Property

        Public ReadOnly Property Termin As String
            Get
                Return DataOd.ToString("yyyy-MM-dd HH:mm") & " - " & DataDo.ToString("yyyy-MM-dd HH:mm")
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return "Rezerwacja " & Id & " (" & Termin & ", " & OpisStatusu & ")"
        End Function

    End Class

End Namespace
