Imports System.IO

Module Snaps
    Public SnapsFolder, TSnaps, circa As String

    Public Sub PopoulateSnaps()
        MedGuiR.ListBox1.Items.Clear()
        If circa = "" Then circa = "*"

        Try
            Dim SouCartella As New IO.DirectoryInfo(SnapsFolder)
            Dim Soufile() As IO.FileInfo
            Dim file_nella_cartella As IO.FileInfo
            Soufile = SouCartella.GetFiles(circa & ".png")

            For Each file_nella_cartella In Soufile
                MedGuiR.ListBox1.Items.Add(file_nella_cartella.Name)
            Next
        Catch
        End Try
    End Sub

    Public Sub LoadSnaps()
        MedGuiR.PictureBox6.Load(SnapsFolder & MedGuiR.ListBox1.Text)
    End Sub

    Public Sub RenameSnaps()
        If MedGuiR.ListBox1.Text = "" Then MsgBox("Nothing to rename", vbOKOnly + vbInformation) : Exit Sub

        Dim Rsnap As String

        Try
            If MedGuiR.RadioButton4.Checked = True Then
                Rsnap = SnapsFolder & MedGuiR.MainGrid.CurrentRow.Cells(5).Value() & "\Named_" & TSnaps & "\" & Trim(MedGuiR.MainGrid.CurrentRow.Cells(0).Value() & MedGuiR.MainGrid.CurrentRow.Cells(2).Value()) & ".png"
            Else
                Rsnap = MedExtra & "Snaps\" & MedGuiR.MainGrid.CurrentRow.Cells(5).Value() & "\CRC_" & TSnaps & "\" & Trim(MedGuiR.MainGrid.CurrentRow.Cells(8).Value()) & ".png"
            End If

            If (Not System.IO.Directory.Exists(Rsnap)) Then
                System.IO.Directory.CreateDirectory(Path.GetDirectoryName(Rsnap))
            End If

            IO.File.Copy(SnapsFolder & MedGuiR.ListBox1.Text, Rsnap, True)
            MsgBox("Png file renamed!", vbOKOnly + vbInformation)
        Catch
        End Try
    End Sub

End Module