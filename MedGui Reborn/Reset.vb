Public Class Reset

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If CheckBox1.Checked = True Or CheckBox2.Checked = True Or CheckBox3.Checked = True _
                        Or CheckBox4.Checked = True Or CheckBox5.Checked = True _
                        Or CheckBox6.Checked = True Or CheckBox7.Checked = True Then

            Dim ResRisp = MsgBox("Do you want to Delete this options?" & vbCrLf &
                "MedGuiR will be closed, after the cleaning please reopen it", vbOKCancel + vbCritical, "Delete Setting Configurations...")
            If ResRisp = MsgBoxResult.Ok Then

                If CheckBox2.Checked = True Then
                    CleanSetting(MedGuiR.TextBox4.Text, "*.cfg*")
                End If

                If CheckBox1.Checked = True Then
                    IO.File.Delete(Application.StartupPath & "\MedGuiR\Mini.ini")
                End If

                If CheckBox4.Checked = True Then
                    CleanSetting(MedExtra & "Scanned", "*.csv*")
                End If

                If CheckBox3.Checked = True Then
                    CleanSetting(MedExtra & "BoxArt", "*.png*")
                End If

                If CheckBox5.Checked = True Then
                    CleanSetting(MedExtra & "Snaps", "*.png*")
                End If

                If CheckBox6.Checked = True Then
                    CleanSetting(MedExtra & "Scraped", "*.*")
                End If

                If CheckBox7.Checked = True Then
                    CleanSetting(MedExtra & "Cheats", "*.*")
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

    Private Function CleanSetting(path As String, extens As String)
        For Each foundFile As String In My.Computer.FileSystem.GetFiles(path, FileIO.SearchOption.SearchAllSubDirectories, extens)
            IO.File.Delete(foundFile)
        Next
#Disable Warning BC42105 ' La funzione 'CleanSetting' non restituisce un valore in tutti i percorsi del codice. È possibile che in fase di esecuzione venga restituita un'eccezione dovuta a un riferimento Null quando viene usato il risultato.
    End Function

#Enable Warning BC42105 ' La funzione 'CleanSetting' non restituisce un valore in tutti i percorsi del codice. È possibile che in fase di esecuzione venga restituita un'eccezione dovuta a un riferimento Null quando viene usato il risultato.

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        MedGuiR.ResetAll = False
        Me.Close()
    End Sub

    Private Sub Reset_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = gIcon
        F1 = Me
        CenterForm()
        ColorizeForm()
    End Sub

End Class