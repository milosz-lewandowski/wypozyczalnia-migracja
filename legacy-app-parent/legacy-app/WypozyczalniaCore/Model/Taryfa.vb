' Modul: Model - taryfa cennikowa kategorii sprzetu | Autor: zespol IT | 2014-03-11
' Historia:
'   2015-02-04 MW: dodane ObowiazujeOd (taryfy z data obowiazywania)
'   2018-09-17 PT: NazwaKategorii dla ekranu cennika

Namespace Model

    <Serializable()>
    Public Class Taryfa

        Public Property Id As Integer
        Public Property Kategoria As String
        Public Property MnoznikWeekend As Decimal
        Public Property RabatTygodniowyProc As Decimal
        Public Property ObowiazujeOd As Date

        Public Shared ReadOnly KategorieZnane As String() = {"BUD", "OGR", "IMP", "TRN"}

        Public Sub New()
            Kategoria = ""
            MnoznikWeekend = 1D
            RabatTygodniowyProc = 0D
            ObowiazujeOd = Date.Today
        End Sub

        Public Sub New(ByVal kategoria As String, ByVal mnoznikWeekend As Decimal, ByVal rabatTygodniowyProc As Decimal, ByVal obowiazujeOd As Date)
            Me.Kategoria = kategoria
            Me.MnoznikWeekend = mnoznikWeekend
            Me.RabatTygodniowyProc = rabatTygodniowyProc
            Me.ObowiazujeOd = obowiazujeOd
        End Sub

        Public Function CzyObowiazujeNa(ByVal dzien As Date) As Boolean
            Return ObowiazujeOd <= dzien.Date
        End Function

        Public Shared Function CzyZnanaKategoria(ByVal kategoria As String) As Boolean
            If String.IsNullOrEmpty(kategoria) Then Return False
            Return Array.IndexOf(KategorieZnane, kategoria.Trim().ToUpper()) >= 0
        End Function

        Public Shared Function NazwaKategorii(ByVal kategoria As String) As String
            Select Case kategoria
                Case "BUD" : Return "Budowlany"
                Case "OGR" : Return "Ogrodniczy"
                Case "IMP" : Return "Imprezowy"
                Case "TRN" : Return "Transportowy"
                Case Else : Return "Inny (" & kategoria & ")"
            End Select
        End Function

        Public Function Kopia() As Taryfa
            Dim t As New Taryfa(Kategoria, MnoznikWeekend, RabatTygodniowyProc, ObowiazujeOd)
            t.Id = Id
            Return t
        End Function

        Public Shared Function Domyslna(ByVal kategoria As String) As Taryfa
            Dim t As New Taryfa(kategoria, 1D, 0D, New Date(2014, 1, 1))
            Return t
        End Function

        Public Function TakaSama(ByVal inna As Taryfa) As Boolean
            If inna Is Nothing Then Return False
            Return Kategoria = inna.Kategoria AndAlso MnoznikWeekend = inna.MnoznikWeekend _
                AndAlso RabatTygodniowyProc = inna.RabatTygodniowyProc AndAlso ObowiazujeOd = inna.ObowiazujeOd
        End Function

        Public Overrides Function ToString() As String
            Return Kategoria & " od " & ObowiazujeOd.ToString("yyyy-MM-dd") & " (weekend x" & MnoznikWeekend.ToString() & ", tydzien -" & RabatTygodniowyProc.ToString() & "%)"
        End Function

    End Class

End Namespace
