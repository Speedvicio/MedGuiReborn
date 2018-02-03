Imports System.IO

Module CUE
    Public DeFolder As FolderBrowserDialog = New FolderBrowserDialog(), isof As OpenFileDialog = New OpenFileDialog(), tiso, tiso1, biso, biso1, miso, rDes, rPath, cfile, strimg As String

    Public Sub typeSTR()
        If MedGuiR.RadioButton1.Checked = True Then
            tiso = "bin/img/iso"
            tiso1 = tiso & " files (*.bin,*.img,*.iso)|*.bin;*.img;*.iso"
            strimg = "FILE " & Chr(34) & biso & Chr(34) & " Binary" & vbCrLf & "  TRACK 01 MODE2/2352" & vbCrLf & "    INDEX 01 00:00:00"
            miso = "cue"
            MedGuiR.Button48.Enabled = True
        ElseIf MedGuiR.RadioButton2.Checked = True Then
            tiso = "bin/img"
            tiso1 = tiso & " files (*.bin,*.img,*.iso)|*.bin;*.img;*.iso"
            strimg = "CD_ROM_XA" & vbCrLf & vbCrLf & vbCrLf & "/ / Track 1" & vbCrLf & "TRACK MODE2_RAW" & vbCrLf & "NO Copy" & vbCrLf & "DATAFILE " & Chr(34) & biso & Chr(34)
            miso = "toc"
            MedGuiR.Button48.Enabled = False
        ElseIf MedGuiR.RadioButton3.Checked = True Then
            tiso = "cue/toc/ccd"
            tiso1 = tiso & " files (*.cue,*.toc,*.ccd)|*.cue;*.toc;*.ccd"
            'strimg = biso & vbCrLf & biso1
            miso = "m3u"
            MedGuiR.Button48.Enabled = False
        End If
    End Sub

    Public Sub Convert_cue()

        ' Apro il file
        Dim oldcue As String
        Dim cuefile As String

        isof.Title = "Select a CUE file"
        isof.Filter = "CUE files (*.cue)|*.cue"
        isof.RestoreDirectory = True
        If isof.ShowDialog() = DialogResult.OK Then cuefile = isof.FileName Else Exit Sub

        Dim content As String = ""
        ' Istanzio un nuovo oggetto StreamReader
        ' Utilizzo il metodo OpenText per aprire il file
        Using objStreamReader As New StreamReader(cuefile)
            'objStreamReader = File.OpenText(cuefile)
            ' Leggo il contenuto del file sino alla fine (ReadToEnd)
            content = objStreamReader.ReadToEnd()

            objStreamReader.Dispose()
            objStreamReader.Close()
            'sostituisco la stringa

            If MedGuiR.CheckBox13.Checked = True Then
                oldcue = "mp3"
                If MedGuiR.ComboBox5.Text = "ogg" Then
                    content = content.Replace(UCase(oldcue), UCase(MedGuiR.ComboBox5.Text))
                ElseIf MedGuiR.ComboBox5.Text = "wav" Then
                    content = content.Replace(UCase(oldcue), UCase(MedGuiR.ComboBox5.Text) & "E")
                End If
            Else
                If MedGuiR.ComboBox5.Text = "ogg" Then
                    oldcue = "wav"
                    content = content.Replace(UCase(oldcue) & "E", UCase(MedGuiR.ComboBox5.Text))
                Else
                    oldcue = "ogg"
                    content = content.Replace(UCase(oldcue), UCase(MedGuiR.ComboBox5.Text) & "E")
                End If
            End If

            content = content.Replace(oldcue, MedGuiR.ComboBox5.Text)
            ' Creo un oggetto StreamWriter col metodo AppendText
            Dim objStreamWriter As StreamWriter
            objStreamWriter = File.CreateText(Replace(cuefile, ".cue", "") & "_" & MedGuiR.ComboBox5.Text & ".cue")
            objStreamWriter.Write(content)

            objStreamWriter.Close()

            MsgBox("Cue " & oldcue & " Converted in Cue " & MedGuiR.ComboBox5.Text, MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
        End Using
    End Sub

End Module