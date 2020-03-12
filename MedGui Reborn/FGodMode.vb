Public Class FGodMode
    Public DestFile As String = ""

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim RRenameFile As String = MsgBox("I will perform rom operations.." & vbCrLf & "THIS OPERATION IS IRREVERSIBLE!!", MsgBoxStyle.Exclamation + vbYesNo, "Manage the romset?")
        If RRenameFile = vbYes Then
            If RadioButton1.Checked = True Then
                RenameLikeDat = 1
            Else
                Dim folder As New FolderBrowserDialog
                folder.Description = "Choose the folder where the files will be sorted"
                folder.ShowNewFolderButton = True

                If folder.ShowDialog = Windows.Forms.DialogResult.OK Then
                    DestFile = folder.SelectedPath
                    RenameLikeDat = 2
                End If
            End If
            MedGuiR.RebuildToolStripButton.PerformClick()
            MsgBox("All Done!", MsgBoxStyle.Information + vbOKOnly, "Job Performed...")
            Me.Close()
        Else
            RenameLikeDat = 0
        End If
    End Sub

    Private Sub FGodMode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        F1 = Me
        CenterForm()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        RenameLikeDat = 0
        Me.Close()
    End Sub

    Private Sub FGodMode_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        RenameLikeDat = 0
    End Sub

End Class