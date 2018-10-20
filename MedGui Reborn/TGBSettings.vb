Imports System.IO
Imports Newtonsoft.Json

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
        F1 = Me
        CenterForm()
    End Sub

    Public Function MakeTGDBList(ByVal APIvalue As String)
        Dim SplittedScrapedValue() As String
        Dim ResultScaped As String
        Dim JsonT As String = New Net.WebClient().DownloadString("https://api.thegamesdb.net/" & APIvalue & "?apikey=" & VSTripleDES.DecryptData("WGjcjnPpu1N7Cj92IF4kIiHtXEd5iNXtaZWpDkM5FVIwZuE2Kpo8R5KzvfENFA8kWmEGOig1hY30hlgoajQ+JjL+Zyv5rmRS+FPemgiaKTTMsGDF4jNR0W1rCBblcD9p6CrUK9MH7YKlexs1HkfrqTlyGKzDLlla1vBfFiI1gmp9haPCMNH84Q=="))

        If File.Exists(MedExtra & "\Plugins\db\TGDB") = False Then
            Directory.CreateDirectory(MedExtra & "\Plugins\db\TGDB")
        End If

        File.WriteAllText(MedExtra & "\Plugins\db\TGDB\" & APIvalue, JsonConvert.SerializeObject(JsonConvert.DeserializeXmlNode(JsonT, "Root"), Formatting.Indented))

        Dim oFile As System.IO.File
        Dim oRead As System.IO.StreamReader

        Try
            oRead = oFile.OpenText(MedExtra & "\Plugins\db\TGDB\" & APIvalue)

            While oRead.Peek <> -1
                SplittedScrapedValue = Split(oRead.ReadLine(), ":")

                Select Case SplittedScrapedValue(0).Trim
                    Case """id"""
                        ResultScaped += Replace(SplittedScrapedValue(1).Trim, ",", " | ")
                    Case """name"""
                        ResultScaped += SplittedScrapedValue(1).Trim
                        If ResultScaped.Substring(ResultScaped.Length - 1, 1) = "," Then
                            ResultScaped = ResultScaped.Substring(0, ResultScaped.Length - 1)
                        End If
                        ResultScaped += vbCrLf
                End Select

                ResultScaped = Replace(ResultScaped, """", "")
            End While

            File.WriteAllText(MedExtra & "\Plugins\db\TGDB\" & APIvalue & ".txt", ResultScaped)
        Catch ex As Exception
        Finally
            oRead.Close()
            File.Delete(MedExtra & "\Plugins\db\TGDB\" & APIvalue)
            MsgBox(APIvalue & " Updated!", MsgBoxStyle.Information + vbOKOnly, "Update Done...")
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