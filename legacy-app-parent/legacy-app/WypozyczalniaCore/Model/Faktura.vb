' Modul: Model\Faktura.vb | Autor: zespol IT | 2014-05-19
' Faktura za wypozyczenie oraz jej pozycje.
' 2015-01-12 MW: IdTransakcji (platnosc przez bramke)
' 2018-04-03 PT: ZWiersza dla naglowka i pozycji

Imports System.Data

Namespace Model

    Public Class Faktura

        Public Property Id As Integer
        Public Property Numer As String
        Public Property KlientId As Integer
        Public Property WypozyczenieId As Integer
        Public Property DataWystawienia As Date
        Public Property Netto As Decimal
        Public Property Vat As Decimal
        Public Property Brutto As Decimal
        Public Property Status As String
        Public Property IdTransakcji As String
        Public Property Pozycje As List(Of PozycjaFaktury)

        Public Sub New()
            Pozycje = New List(Of PozycjaFaktury)()
            Status = "W"
            Numer = ""
            IdTransakcji = ""
            DataWystawienia = Date.Now
        End Sub

        Public ReadOnly Property CzyOplacona As Boolean
            Get
                Return Status = "O"
            End Get
        End Property

        Public ReadOnly Property LiczbaPozycji As Integer
            Get
                If Pozycje Is Nothing Then Return 0
                Return Pozycje.Count
            End Get
        End Property

        Public Function SumaPozycjiNetto() As Decimal
            Dim suma As Decimal = 0D
            For Each p As PozycjaFaktury In Pozycje
                suma = suma + p.WartoscNetto
            Next
            Return suma
        End Function

        Public Shared Function ZWiersza(ByVal wiersz As DataRow) As Faktura
            Dim f As New Faktura()
            f.Id = CInt(wiersz("Id"))
            f.Numer = wiersz("Numer").ToString()
            f.KlientId = CInt(wiersz("KlientId"))
            f.WypozyczenieId = CInt(wiersz("WypozyczenieId"))
            f.DataWystawienia = CDate(wiersz("DataWystawienia"))
            f.Netto = CDec(wiersz("Netto"))
            f.Vat = CDec(wiersz("Vat"))
            f.Brutto = CDec(wiersz("Brutto"))
            f.Status = wiersz("Status").ToString()
            If Not IsDBNull(wiersz("IdTransakcji")) Then f.IdTransakcji = wiersz("IdTransakcji").ToString()
            Return f
        End Function

        Public Overrides Function ToString() As String
            Return String.Format("Faktura {0} (wypozyczenie {1}, brutto {2})", Numer, WypozyczenieId, Brutto)
        End Function

    End Class

    Public Class PozycjaFaktury

        Public Property Lp As Integer
        Public Property Opis As String
        Public Property Ilosc As Decimal
        Public Property CenaNetto As Decimal
        Public Property StawkaVat As Decimal
        Public Property WartoscNetto As Decimal

        Public Shared Function ZWiersza(ByVal wiersz As DataRow) As PozycjaFaktury
            Dim p As New PozycjaFaktury()
            p.Lp = CInt(wiersz("Lp"))
            p.Opis = wiersz("Opis").ToString()
            p.Ilosc = CDec(wiersz("Ilosc"))
            p.CenaNetto = CDec(wiersz("CenaNetto"))
            p.StawkaVat = CDec(wiersz("StawkaVat"))
            p.WartoscNetto = CDec(wiersz("WartoscNetto"))
            Return p
        End Function

    End Class

End Namespace
