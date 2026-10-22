' Modul: DAL\DbHelper.vb - wspolny dostep ADO.NET do bazy Wypozyczalnia | Autor: zespol IT | 2014-03-11
' Historia: 2014-03-11 JK utworzenie
'           2016-01-18 MW obsluga parametrow OUTPUT dla procedur
'           2019-04-02 PT domyslny connection string dla zadania nocnego
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Public Class BladBiznesowyException
    Inherits ApplicationException

    Public Property KodBledu As String

    Public Sub New(ByVal komunikat As String)
        MyBase.New(komunikat)
        KodBledu = "BIZ"
    End Sub

    Public Sub New(ByVal komunikat As String, ByVal kodBledu As String)
        MyBase.New(komunikat)
        Me.KodBledu = kodBledu
    End Sub
End Class

Namespace DAL

    Public NotInheritable Class DbHelper

        Private Const DomyslnyTimeout As Integer = 60

        Private Sub New()
        End Sub

        Public Shared Function PobierzConnectionString() As String
            Dim wpis As ConnectionStringSettings = ConfigurationManager.ConnectionStrings("WypozyczalniaDb")
            If wpis Is Nothing OrElse String.IsNullOrEmpty(wpis.ConnectionString) Then
                Return "Data Source=sql-wypozyczalnia;Initial Catalog=Wypozyczalnia;Integrated Security=True"
            End If
            Return wpis.ConnectionString
        End Function

        Public Shared Function WykonajZapytanie(ByVal sql As String, ByVal ParamArray parametry() As SqlParameter) As DataTable
            Dim wynik As New DataTable()
            Using cn As New SqlConnection(PobierzConnectionString())
                Using cmd As New SqlCommand(sql, cn)
                    cmd.CommandType = CommandType.Text
                    cmd.CommandTimeout = DomyslnyTimeout
                    DodajParametry(cmd, parametry)
                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(wynik)
                    End Using
                End Using
            End Using
            Return wynik
        End Function

        Public Shared Function WykonajPolecenie(ByVal sql As String, ByVal ParamArray parametry() As SqlParameter) As Integer
            Using cn As New SqlConnection(PobierzConnectionString())
                Using cmd As New SqlCommand(sql, cn)
                    cmd.CommandType = CommandType.Text
                    cmd.CommandTimeout = DomyslnyTimeout
                    DodajParametry(cmd, parametry)
                    cn.Open()
                    Return cmd.ExecuteNonQuery()
                End Using
            End Using
        End Function

        Public Shared Function WykonajProcedure(ByVal nazwa As String, ByVal ParamArray parametry() As SqlParameter) As DataTable
            Dim wynik As New DataTable()
            Using cn As New SqlConnection(PobierzConnectionString())
                Using cmd As New SqlCommand(nazwa, cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = DomyslnyTimeout
                    DodajParametry(cmd, parametry)
                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(wynik)
                    End Using
                End Using
            End Using
            Return wynik
        End Function

        Public Shared Function WykonajProcedureBezWyniku(ByVal nazwa As String, ByVal ParamArray parametry() As SqlParameter) As Integer
            Using cn As New SqlConnection(PobierzConnectionString())
                Using cmd As New SqlCommand(nazwa, cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = DomyslnyTimeout
                    DodajParametry(cmd, parametry)
                    cn.Open()
                    Return cmd.ExecuteNonQuery()
                End Using
            End Using
        End Function

        Public Shared Function Parametr(ByVal nazwa As String, ByVal wartosc As Object) As SqlParameter
            If wartosc Is Nothing Then
                Return New SqlParameter(nazwa, DBNull.Value)
            End If
            Return New SqlParameter(nazwa, wartosc)
        End Function

        Public Shared Function ParametrWyjsciowy(ByVal nazwa As String, ByVal typ As SqlDbType, Optional ByVal rozmiar As Integer = 0) As SqlParameter
            Dim p As New SqlParameter(nazwa, typ)
            If rozmiar > 0 Then p.Size = rozmiar
            p.Direction = ParameterDirection.Output
            Return p
        End Function

        Private Shared Sub DodajParametry(ByVal cmd As SqlCommand, ByVal parametry() As SqlParameter)
            If parametry Is Nothing Then Return
            For Each p As SqlParameter In parametry
                If p IsNot Nothing Then cmd.Parameters.Add(p)
            Next
        End Sub

    End Class

End Namespace
