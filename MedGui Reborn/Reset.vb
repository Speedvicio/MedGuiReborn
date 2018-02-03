Public Class Reset

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If CheckBox1.Checked = True Or CheckBox2.Checked = True Or CheckBox3.Checked = True Or CheckBox4.Checked = True Or CheckBox5.Checked = True Or CheckBox6.Checked = True Then

            Dim ResRisp = MsgBox("Do you want to Delete this configurations?" & vbCrLf &
                "MedGuiR will be closed, after the cleaning please reopen it", vbOKCancel + vbCritical, "Delete Setting Configurations...")
            If ResRisp = MsgBoxResult.Ok Then

                If CheckBox2.Checked = True Then
                    For Each foundFile As String In My.Computer.FileSystem.GetFiles(MedGuiR.TextBox4.Text, FileIO.SearchOption.SearchTopLevelOnly, "*.cfg*")
                        IO.File.Delete(foundFile)
                    Next
                End If

                If CheckBox1.Checked = True Then
                    IO.File.Delete(Application.StartupPath & "\MedGuiR\Mini.ini")
                End If

                If CheckBox4.Checked = True Then
                    For Each foundFile As String In My.Computer.FileSystem.GetFiles(MedExtra & "Scanned", FileIO.SearchOption.SearchAllSubDirectories, "*.csv*")
                        IO.File.Delete(foundFile)
                    Next
                End If

                If CheckBox3.Checked = True Then
                    For Each foundFile As String In My.Computer.FileSystem.GetFiles(MedExtra & "BoxArt", FileIO.SearchOption.SearchAllSubDirectories, "*.png*")
                        IO.File.Delete(foundFile)
                    Next
                End If

                If CheckBox5.Checked = True Then
                    For Each foundFile As String In My.Computer.FileSystem.GetFiles(MedExtra & "Snaps", FileIO.SearchOption.SearchAllSubDirectories, "*.png*")
                        IO.File.Delete(foundFile)
                    Next
                End If

                If CheckBox6.Checked = True Then
                    For Each foundFile As String In My.Computer.FileSystem.GetFiles(MedExtra & "Scraped", FileIO.SearchOption.SearchAllSubDirectories, "*.*")
                        IO.File.Delete(foundFile)
                    Next
                End If
            Else
                Exit Sub
            End If
            MedGuiR.ResetAll = True
        Else
            MsgBox("Nothing to do...", vbOKOnly + vbInformation, "...")
            Exit Sub
        End If

        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        MedGuiR.ResetAll = False
        Me.Close()
    End Sub

    Private Sub Reset_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        F1 = Me
        CenterForm()
    End Sub

End Class