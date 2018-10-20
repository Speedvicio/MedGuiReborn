Imports System.IO

Public Class TGBSettings
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            Panel1.Enabled = True
            NewAPI = True
        Else
            Panel1.Enabled = False
            NewAPI = False
        End If
    End Sub

    Private Sub TGBSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = gIcon
    End Sub
    Public Function MakeTGDBList(ByVal APIvalue As String)
        Dim myWebClient As New Net.WebClient()
        Dim SplittedScrapedValue() As String
        Dim ResultScaped As String
        Dim myStream As Stream = myWebClient.OpenRead("https://api.thegamesdb.net/" & APIvalue & "?apikey=" & VSTripleDES.DecryptData("WGjcjnPpu1N7Cj92IF4kIiHtXEd5iNXtaZWpDkM5FVIwZuE2Kpo8R5KzvfENFA8kWmEGOig1hY30hlgoajQ+JjL+Zyv5rmRS+FPemgiaKTTMsGDF4jNR0W1rCBblcD9p6CrUK9MH7YKlexs1HkfrqTlyGKzDLlla1vBfFiI1gmp9haPCMNH84Q=="))
        Dim sr As New StreamReader(myStream)

        Dim salva As New IO.StreamWriter(Application.StartupPath & "\" & APIvalue) 'salva il file'
        salva.WriteLine(sr.ReadToEnd.Split(vbCrLf))
        salva.Close()

        Dim oFile As System.IO.File
        Dim oRead As System.IO.StreamReader

        Try
            oRead = oFile.OpenText(Application.StartupPath & "\" & APIvalue)

            While oRead.Peek <> -1
                SplittedScrapedValue = Split(oRead.ReadLine(), ":")

                Select Case SplittedScrapedValue(0).Trim
                    Case """id"""
                        ResultScaped += Replace(SplittedScrapedValue(1).Trim, ",", " | ")
                    Case """name"""
                        ResultScaped += SplittedScrapedValue(1).Trim & vbCrLf
                End Select
            End While

            Dim wfile As System.IO.StreamWriter
            wfile = My.Computer.FileSystem.OpenTextFileWriter(Application.StartupPath & "\" & APIvalue & ".txt", False)
            wfile.Write(ResultScaped)
            wfile.Close()
        Catch ex As Exception
        Finally
            sr.Close()
        End Try
    End Function

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        MakeTGDBList("Platforms")
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        MakeTGDBList("Developers")
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        MakeTGDBList("Genres")
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        MakeTGDBList("Publishers")
    End Sub
End Class