' Modul: Model - pozycja sprzetu w katalogu wypozyczalni | Autor: zespol IT | 2014-03-11
' Historia:
'   2015-06-02 MW  dodane pole Kaucja (wczesniej liczona z cennika)
'   2018-01-15 PT  status X (wycofany) zamiast kasowania rekordow

Namespace Model

    <Serializable()>
    Public Class Sprzet

        Public Const StatusDostepny As String = "D"
        Public Const StatusWypozyczony As String = "W"
        Public Const StatusSerwis As String = "S"
        Public Const StatusWycofany As String = "X"

        Public Property Id As Integer
        Public Property NumerInwentarzowy As String
        Public Property Nazwa As String
        Public Property Kategoria As String
        Public Property StawkaDobowa As Decimal
        Public Property Kaucja As Decimal
        Public Property Status As String

        Public Sub New()
            Id = 0
            NumerInwentarzowy = ""
            Nazwa = ""
            Kategoria = "BUD"
            StawkaDobowa = 0D
            Kaucja = 0D
            Status = StatusDostepny
        End Sub

        Public ReadOnly Property OpisStatusu As String
            Get
                Select Case Status
                    Case StatusDostepny : Return "dostepny"
                    Case StatusWypozyczony : Return "wypozyczony"
                    Case StatusSerwis : Return "serwis"
                    Case StatusWycofany : Return "wycofany"
                    Case Else : Return "?"
                End Select
            End Get
        End Property

        Public ReadOnly Property OpisKategorii As String
            Get
                Select Case Kategoria
                    Case "BUD" : Return "Budowlany"
                    Case "OGR" : Return "Ogrodowy"
                    Case "IMP" : Return "Imprezowy"
                    Case "TRN" : Return "Transportowy"
                    Case Else : Return Kategoria
                End Select
            End Get
        End Property

        ' czy mozna w ogole wydac (nie sprawdza terminow - od tego jest EquipmentManager)
        Public Function CzyMoznaWydac() As Boolean
            Return Status = StatusDostepny
        End Function

        Public Overrides Function ToString() As String
            Return NumerInwentarzowy & " " & Nazwa
        End Function

    End Class

End Namespace
