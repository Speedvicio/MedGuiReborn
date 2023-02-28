Imports System.Net

Public Class MedBrowser

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        WebBrowser1.GoBack()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        WebBrowser1.GoForward()
    End Sub

    Private Sub Button42_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox1.Text.Trim <> "" Then GoWeb()
    End Sub

    Private Sub GoWeb()
        Dim searchweb As String
        If TextBox1.Text.Trim.StartsWith("www.") Or TextBox1.Text.Trim.StartsWith("http://") Or TextBox1.Text.Trim.StartsWith("https://") Then
            searchweb = TextBox1.Text.Trim
        Else
            searchweb = "https://duckduckgo.com/?q=" & TextBox1.Text.Trim & "&t=h_&ia=web"
        End If
        WebBrowser1.Navigate(searchweb)
    End Sub

    Private Sub TextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            GoWeb()
        End If
    End Sub

    Private Sub MedBrowser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = gIcon
        Read_Resource()
        F1 = Me
        CenterForm()
        ColorizeForm()
        WebBrowserFix.SetBrowserEmulationVersion(My.Application.Info.AssemblyName)
        ServicePointManager.ServerCertificateValidationCallback = Function() True
        WebBrowser1.ScriptErrorsSuppressed = True
    End Sub

End Class