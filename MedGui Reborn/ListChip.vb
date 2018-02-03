Imports System.IO

Module ListChip

    Public Sub LoadChipInfo()
        SoundList.RichTextBox1.Clear()
        If File.Exists(MedExtra & "\RomTemp\info.txt") Then
            SoundList.RichTextBox1.LoadFile(MedExtra & "\RomTemp\info.txt", RichTextBoxStreamType.PlainText)
        Else
            SoundList.RichTextBox1.Text = "No Info"
        End If
    End Sub

    Public Sub PopulateChip()
        SoundList.ListBox1.Items.Clear()
        If Dir(MedExtra & "\RomTemp\*.spc") <> "" Then
            For Each txtFile As String In IO.Directory.GetFiles(MedExtra & "\RomTemp\", "*.spc")
                SoundList.ListBox1.Items.Add(IO.Path.GetFileName(txtFile))
            Next
            LoadChipInfo()
            SoundList.Show()
            ChaseL()
            SoundList.ListBox1.SelectedIndex = 0
        End If
    End Sub

End Module