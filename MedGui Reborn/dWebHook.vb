Imports System.Collections.Specialized
Imports System.Net

'This code by Kowalski7 & Literallycj
'https://github.com/Kowalski7/Discord-Webhook-Announcer/blob/11d604db1a356bd6aa52e8e60d38d67a296503b7/Discord%20Webhook%20Announcer/main.vb#L96

Public Class dWebHook
    Implements IDisposable

    Private ReadOnly client As WebClient
    Private Shared discordValues As NameValueCollection = New NameValueCollection()
    Public Property WebHook As String
    Public Property UserName As String
    Public Property ProfilePicture As String

    Public Sub New()
        'Specifies the TLS 1.X security protocol.
        'Ssl3    48
        'SystemDefault   0
        'Tls     192
        'Tls11   768
        'Tls12   3072
        'Tls13   12288

        'Forced Tls12 security protocol because August net framework patch create problems with Discord connection
        ServicePointManager.SecurityProtocol = 3072
        client = New WebClient()
    End Sub

    Public Sub SendMessage(ByVal msgSend As String)
        If msgSend = "" Or WebHook = "" Then
            MsgBox("The webhook link and message are required!", vbCritical + vbOKOnly)
            Return
        End If
        discordValues.Add("username", UserName)
        discordValues.Add("avatar_url", ProfilePicture)
        discordValues.Add("content", msgSend)
        Try
            client.UploadValues(WebHook, discordValues)
        Catch
            MsgBox("Unable to send message!" & vbNewLine & vbNewLine & "This issue can be caused by one or more of the following:" & vbNewLine & "- The webhook link is incorrect." & vbNewLine & "- There is no connection to the Internet." & vbNewLine & "- Another program or firewall is blocking this application's access to the Internet." & vbNewLine & "- Discord's servers are down." & vbNewLine & vbNewLine & "If you believe everything is in working order and this problem persists, please submit an issue on this program's Github page.", vbCritical + vbOKOnly, "Discord Webhook Announcer")
        End Try
        discordValues.Remove("username")
        discordValues.Remove("avatar_url")
        discordValues.Remove("content")
    End Sub

    Public Sub Dispose()
        client.Dispose()
    End Sub

    Private Sub IDisposable_Dispose() Implements IDisposable.Dispose
        DirectCast(client, IDisposable).Dispose()
    End Sub

End Class