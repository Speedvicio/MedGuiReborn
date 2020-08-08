Imports System.Net

'//Code publicated on https://stackoverflow.com/questions/2825377/how-can-i-get-the-webclient-to-use-cookies%EF%BC%89
Public Class CookieAwareWebClient
    Inherits WebClient

    Private cc As New CookieContainer()
    Private lastPage As String

    Protected Overrides Function GetWebRequest(ByVal address As System.Uri) As WebRequest
        ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)

        Dim R = MyBase.GetWebRequest(address)
        If TypeOf R Is HttpWebRequest Then
            With DirectCast(R, HttpWebRequest)
                .CookieContainer = cc
                If Not lastPage Is Nothing Then
                    .Referer = lastPage
                End If
            End With
        End If
        lastPage = address.ToString()
        Return R
    End Function

End Class