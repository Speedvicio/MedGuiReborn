Imports System.IO

Public Class FGodMode
    Public DestFile As String = ""
    Public noADV As Boolean

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim RRenameFile As String
        If noADV = False Then
            RRenameFile = MsgBox("I will perform rom operations.." & vbCrLf & "THIS OPERATION IS IRREVERSIBLE!!", MsgBoxStyle.Exclamation + vbYesNo, "Manage the romset?")
        Else
            RRenameFile = vbYes
        End If

        If RRenameFile = vbYes Then
            If RadioButton1.Checked = True Then
                RenameLikeDat = 1
                noADV = False
            ElseIf RadioButton2.Checked = True Then
                Dim folder As New FolderBrowserDialog
                folder.Description = "Choose the folder where the files will be sorted"
                folder.ShowNewFolderButton = True

                If folder.ShowDialog = Windows.Forms.DialogResult.OK Then
                    DestFile = folder.SelectedPath
                    RenameLikeDat = 2
                    folder.RootFolder = folder.SelectedPath
                    noADV = False
                End If
            ElseIf RadioButton3.Checked = True Then
                If MedGuiR.DataGridView1.Rows.Count = 0 Then
                    MsgBox("Nothing to do here...", MsgBoxStyle.Exclamation + vbOKOnly, "Grid empty...")
                    RenameLikeDat = 0
                    Exit Sub
                End If
                CloneFile()
            End If
            If RadioButton3.Checked = False Then MedGuiR.RebuildToolStripButton.PerformClick()
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

    Private Sub CloneFile()
        Dim folder As New FolderBrowserDialog
        folder.Description = "Choose the folder where the files will be sorted"
        folder.ShowNewFolderButton = True

        If folder.ShowDialog = Windows.Forms.DialogResult.OK Then
            For Each dr As DataGridViewRow In MedGuiR.DataGridView1.Rows
                If dr.Visible = True Then
                    Dim newfolder As String = Path.Combine(folder.SelectedPath, dr.Cells(5).Value.ToString)
                    If Directory.Exists(newfolder) = False Then My.Computer.FileSystem.CreateDirectory(newfolder)
                    Dim cleanedRomName As String
                    cleanedRomName = dr.Cells(0).Value.ToString & " " & dr.Cells(2).Value.ToString & Path.GetExtension(dr.Cells(4).Value.ToString)
                    My.Computer.FileSystem.CopyFile(dr.Cells(4).Value.ToString, Path.Combine(newfolder, cleanedRomName), True)
                End If
            Next
            folder.RootFolder = folder.SelectedPath
        End If
        noADV = True
    End Sub

End Class