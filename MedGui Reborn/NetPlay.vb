Imports System.IO
Imports System.Net

Module NetPlay
    Public MyLocalIp As String

    Public Sub MyIp()
        Try
            Dim wrq As WebRequest = WebRequest.Create("http://checkip.dyndns.org/") 'http://pchelplive.com/ip.php http://www.vocesuip.com/getip.php http://checkip.dyndns.org/
            Dim wrp As WebResponse = wrq.GetResponse()
            Using sr As New StreamReader(wrp.GetResponseStream())
                MyLocalIp = sr.ReadToEnd

                Dim u_ip() As String
                Dim uX As Integer
                uX = 1

                u_ip = Split(MyLocalIp, ":")

                For uX = 1 To UBound(u_ip)
                    MyLocalIp = ":" & u_ip(uX)
                Next
                MyLocalIp = Replace(MyLocalIp, ":", "")
                MyLocalIp = Replace(MyLocalIp, "</body></html>", "")

                MgrSetting.TextBox8.Text = MyLocalIp.Trim

                wrp.Close()
                sr.Dispose()
                sr.Close()
            End Using
        Catch ex As Exception
            MsgBox("Server not Available at the moment, try later", MsgBoxStyle.Exclamation, "IP not found")
        End Try

    End Sub

    Public Sub MednafenServer()

        If IO.File.Exists(MedExtra & "NetPlay" & "\standard.conf") = False Then
            MsgBox("No standard.conf detected.", vbCritical + MsgBoxStyle.OkOnly, "Standard.conf Not Detected")
            Standard_Conf.ShowDialog()
        End If

        wDir = (MedExtra & "NetPlay")
        tProcess = "mednafen-server"
        Arg = " standard.conf"
        StartProcess()
    End Sub

End Module