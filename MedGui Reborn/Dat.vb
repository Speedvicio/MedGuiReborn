Imports System.IO

Module Dat

    Public Sub w_dat()
        If Directory.Exists(MedExtra & "\DATs\MDM\") Then
        Else
            Directory.CreateDirectory(MedExtra & "\DATs\MDM\")
        End If
        ' Create an instance of StreamWriter to write text to a file
        MDM.Enabled = False
        Using sw As StreamWriter = File.AppendText(MedExtra & "\DATs\MDM\" & MDM.ComboBox1.Text & ".dat")
            ' Add some text to the file.
            'sw.Write("This is a Custom Dat for MedGui Reborn")
            'sw.WriteLine("System Console: " & Form1.ComboBox1.Text)
            sw.WriteLine("")
            ' Arbitrary objects can also be written to the file.
            'sw.Write("The date is: ")
            'Dim r_clear As String = Replace(Trim(MDM.MDMr_name), MDM.MDMext, "")
            Dim r_clear As String = Path.GetFileNameWithoutExtension(Trim(MDM.MDMr_name))
            Dim Music As String
            If MDM.CheckBox1.Checked = True Then Music = "Soundtrack" Else Music = ""
            If MDM.MDMr_name.Contains("(") = False And MDM.MDMr_name.Contains(").") = False Then MDM.MDMr_name = Path.GetFileNameWithoutExtension(MDM.MDMr_name) & "(" & Music & ")" & Path.GetExtension(MDM.MDMr_name)
            sw.WriteLine("game (")
            sw.WriteLine(vbTab & "name " & Chr(34) & r_clear & Chr(34))
            sw.WriteLine(vbTab & "description " & Chr(34) & r_clear & Chr(34))
            sw.WriteLine(vbTab & "rom ( name " & Chr(34) & MDM.MDMr_name & Chr(34) & " crc " & base_file & " md5 " & r_md5 & " sha1 " & r_sha & " )")
            sw.WriteLine(")")
            sw.Dispose()
            sw.Close()
        End Using
    End Sub

End Module