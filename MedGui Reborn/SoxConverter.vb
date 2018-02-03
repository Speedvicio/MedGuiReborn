Module SoxConverter
    Dim SM_A As Boolean

    Public Sub dConvert()
        Select Case LCase(MedGuiR.ToolStripComboBox1.Text)
            Case "ape"
                MedGuiR.MultimediaToolStripMenuItem.Enabled = False
                tProcess = "MAC"
                pSox = " -d"
                MedGuiR.OggToolStripMenuItem1.Enabled = False
                MedGuiR.OggToolStripMenuItem.Enabled = False
            Case "mpc"
                MedGuiR.MultimediaToolStripMenuItem.Enabled = False
                tProcess = "mpcdec"
                pSox = ""
                MedGuiR.OggToolStripMenuItem1.Enabled = False
                MedGuiR.OggToolStripMenuItem.Enabled = False
            Case Else
                MedGuiR.MultimediaToolStripMenuItem.Enabled = True
                tProcess = "sox"
                pSox = " rate 44100 norm"
                MedGuiR.OggToolStripMenuItem1.Enabled = True
                MedGuiR.OggToolStripMenuItem.Enabled = True
        End Select
    End Sub

    Public Sub Folder_Convert()
        dConvert()
        wDir = (MedExtra & "Converter")
        Dim audioFile As String

        For i As Integer = 0 To MedGuiR.ListAddsFile.Items.Count - 1
            audioFile = rPath & "\" & MedGuiR.ListAddsFile.Items(i)
            Arg = " " & Chr(34) & audioFile & Chr(34) & " " & Chr(34) & Replace(audioFile, LCase(MedGuiR.ToolStripComboBox1.Text), LCase(cfile)) & Chr(34) & pSox
            SoxStatus.Text = "Waiting for conversion..."
            SoxStatus.Label1.Text = "Convert " & MedGuiR.ListAddsFile.Items(i) & " in " & LCase(cfile) & " format..."
            SoxStatus.Show()
            StartProcess()
            execute.WaitForExit()
            SoxStatus.Close()
        Next
        MsgBox("Audio folder converted in " & LCase(cfile) & " format!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
        SM_A = True
        delete_audio()
    End Sub

    Public Sub Single_Convert()
        dConvert()
        wDir = (MedExtra & "Converter")
        Dim audioFile As String

        audioFile = rPath & "\" & MedGuiR.ListAddsFile.SelectedItem
        Arg = " " & Chr(34) & audioFile & Chr(34) & " " & Chr(34) & Replace(audioFile, LCase(MedGuiR.ToolStripComboBox1.Text), LCase(cfile)) & Chr(34) & pSox
        StartProcess()
        execute.WaitForExit()
        MsgBox("File converted in " & LCase(cfile) & " format!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
        SM_A = False
        delete_audio()
    End Sub

    Public Sub play()
        If MedGuiR.ListAddsFile.Items.Count = 0 Then MedGuiR.TimerSoxConv.Stop() : MedGuiR.TimerSoxConv.Enabled = False : Exit Sub

        tProcess = "sox"
        KillProcess()
        Arg = " " & Chr(34) & rPath & "\" & MedGuiR.ListAddsFile.SelectedItem & Chr(34) & " -d"
        wDir = (MedExtra & "Converter")
        StartProcess()
        MedGuiR.TimerSoxConv.Enabled = True
    End Sub

    Public Sub delete_audio()
        Dim audioFile As String
        Dim response As String

        If MedGuiR.DeleteAfterConversionToolStripMenuItem.Checked = True Then
            response = MsgBox("Do you want to move source audio file into Recycle Bin?", vbCritical + vbOKCancel)
            If response = vbOK Then
                If SM_A = True Then
                    For i As Integer = 0 To MedGuiR.ListAddsFile.Items.Count - 1
                        audioFile = rPath & "\" & MedGuiR.ListAddsFile.Items(i)
                        My.Computer.FileSystem.DeleteFile(audioFile, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                    Next
                Else
                    audioFile = rPath & "\" & MedGuiR.ListAddsFile.SelectedItem
                    My.Computer.FileSystem.DeleteFile(audioFile, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                End If
                MsgBox("All Done!!", vbInformation)
                MedGuiR.ListAddsFile.Items.Clear()
            End If
        End If
    End Sub

End Module