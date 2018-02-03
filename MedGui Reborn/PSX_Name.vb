Imports System.IO
Imports DiscTools

Module PSX_Name
    Public n_psx, r_psx, Serial_PSX As String

    Public Sub Change_PSX()
        r_psx = ""
        Read_PSX()

        Try
            If Serial_PSX = "" Then r_psx = Path.GetFileNameWithoutExtension(n_psx) : Exit Sub

            Using reader As New StreamReader(MedExtra & "\Plugins\db\ps1titles_us_eu_jp.txt")
                While Not reader.EndOfStream
                    Dim riga As String = reader.ReadLine
                    If riga.Contains(Serial_PSX) Then
                        r_psx = Trim((Replace(riga, Serial_PSX, "")))
                        r_psx = r_psx & " [" & Serial_PSX & "]"
                        Exit While
                    End If
                End While
                reader.Dispose()
                reader.Close()
            End Using
        Catch ex As Exception
            MGRWriteLog("PSX_Name - Read_PSX:" & ex.Message)
        End Try
    End Sub

    Public Sub renamePSX()
        If Path.GetFileNameWithoutExtension(n_psx) <> r_psx Then
            Dim i = MsgBox("I can rename the PSX file in a better way." & vbCrLf _
                           & "Do you want to rename the file in: " & r_psx, vbInformation + MsgBoxStyle.OkCancel)
            If i = vbOK Then
                My.Computer.FileSystem.RenameFile(percorso, r_psx & Path.GetExtension(percorso))
            End If
        End If
    End Sub

    Public Sub Read_PSX()
        Serial_PSX = ""
        Dim path_psx As String
        n_psx = Path.GetFileName(percorso)
        Select Case LCase(Path.GetExtension(n_psx))
            'If LCase(Path.GetExtension(n_psx)) = ".cue" Then
            Case ".cue" ', ".toc"
                Dim righe As String() = File.ReadAllLines(percorso)
                Dim result As String

                For i = 0 To 10
                    If righe(i).Contains("FILE ") Then
                        result = righe(i)
                        Exit For
                    End If
                Next

                Dim startPosition As Integer = result.IndexOf(Chr(34)) + 1
                Dim word2 As String = result.Substring(startPosition,
                                                         result.IndexOf(Chr(34), startPosition) - startPosition)
                path_psx = Replace(percorso, n_psx, "") & word2
            Case ".ccd"
                path_psx = Replace(percorso, ".ccd", "") & ".img"
            Case Else
                path_psx = percorso
        End Select
        'End If

        Try
            'Serial_PSX = DiscSN.SerialNumber.GetPSXSerial(percorso)

            Dim CDinspector = DiscInspector.ScanPSX(percorso)
            Serial_PSX = UCase(CDinspector.Data.SerialNumber)
        Catch ex As Exception
        Finally
            'MedGuiR.Show()
            'Application.DoEvents()
            'MedGuiR.TextBox1.Focus()
        End Try

        'If Serial_PSX = "" Then
        'If File.Exists(MedExtra & "\Plugins\psxt001z.exe") = False Then Exit Sub
        'Dim psi As New ProcessStartInfo(MedExtra & "\Plugins\psxt001z.exe", Chr(34) & path_psx & Chr(34))
        'psi.WindowStyle = ProcessWindowStyle.Hidden
        'psi.UseShellExecute = False
        'psi.RedirectStandardOutput = True
        'Dim proc As Process = Process.Start(psi)
        'proc.WaitForExit()
        'Dim output As String = proc.StandardOutput.ReadToEnd()

        'Dim q As Integer = output.IndexOf("ID:")
        'Dim s1 As String
        'If q >= 0 Then
        's1 = output.Substring(q, 14)
        's1 = Trim(Replace(s1, "ID:", ""))
        'Serial_PSX = s1
        'End If
        'End If
    End Sub

End Module