' Modul: Global.asax.vb - zdarzenia aplikacji i sesji | Autor: zespol IT | 2014-03-11
' Historia: 2014-03-11 JK utworzenie
'           2017-06-02 MW log4net zamiast EventLog
Imports System.Web
Imports System.Web.SessionState
Imports System.Globalization
Imports System.Threading

Public Class Global_asax
    Inherits System.Web.HttpApplication

    Private Shared ReadOnly Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Global_asax))

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        log4net.Config.XmlConfigurator.Configure()
        Log.Info("Start aplikacji Wypozyczalnia, wersja " & _
                 System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString())
        Application("LiczbaSesji") = 0
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        Session("WybranyKlientId") = 0
        Session("OstatnieWypozyczenieId") = 0
        Application.Lock()
        Application("LiczbaSesji") = Application("LiczbaSesji") + 1
        Application.UnLock()
        Log.Debug("Nowa sesja: " & Session.SessionID)
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        Thread.CurrentThread.CurrentCulture = New CultureInfo("pl-PL")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("pl-PL")
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        Dim ex As Exception = Server.GetLastError()
        If ex Is Nothing Then Return
        If TypeOf ex Is HttpUnhandledException AndAlso ex.InnerException IsNot Nothing Then
            ex = ex.InnerException
        End If
        Dim adres As String = ""
        Try
            adres = Request.Url.ToString()
        Catch
        End Try
        Log.Error("Nieobsluzony blad na " & adres, ex)
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        Application.Lock()
        Application("LiczbaSesji") = Application("LiczbaSesji") - 1
        Application.UnLock()
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        Log.Info("Zatrzymanie aplikacji Wypozyczalnia")
        log4net.LogManager.Shutdown()
    End Sub

End Class
