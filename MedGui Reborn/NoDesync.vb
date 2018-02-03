Imports System.IO

Module NoDesync
    Public contdes As Integer, pargMT As String
    Dim existMCR As Boolean = False

    Public Sub Read_Desync()
        Dim oFile As File
        Dim oRead As StreamReader
        set_special_module()
        contdes = 0

        If File.Exists(MedGuiR.TextBox4.Text & "\" & p_c & ".cfg”) = False Then Exit Sub
        Try
            oRead = oFile.OpenText(MedGuiR.TextBox4.Text & "\" & p_c & ".cfg”) 'MedExtra & "\NoDesync\" & consoles & " .cfg”)

            While oRead.Peek <> -1
                If oRead.ReadLine().Contains("#NoDesync") = True Then
                    contdes = contdes + 1
                End If
            End While
        Catch ex As Exception
        Finally
            oRead.Close()
        End Try
    End Sub

    Public Sub QuestionMultitap()
        Dim Mrow, MultiMex As String
        Dim rMulti As Boolean = False
        Dim SMrow() As String

        set_special_module()

        Try
            Using reader As New StreamReader(MedGuiR.TextBox4.Text & "\" & DMedConf & ".cfg")
                While Not reader.EndOfStream
                    Dim EmptyValuse As String = " 0 "
                    Mrow = reader.ReadLine
                    rMulti = True
                    Select Case True
                        Case Mrow.Contains(p_c & ".input.multitap tp"), Mrow.Contains(p_c & ".input.multitap 4way")
                            EmptyValuse = " none "
                        Case Mrow.Contains(p_c & ".input.pport1.multitap 1")
                        Case Mrow.Contains(p_c & ".input.pport2.multitap 1")
                        Case Mrow.Contains(p_c & ".input.port1.multitap 1")
                            If p_c = "nes" Then rMulti = False
                        Case Mrow.Contains(p_c & ".input.port2.multitap 1")
                            If p_c = "nes" Then rMulti = False
                        Case Mrow.Contains(p_c & ".input.sport1.multitap 1")
                            If p_c = "nes" Then rMulti = False
                        Case Mrow.Contains(p_c & ".input.sport2.multitap 1")
                            If p_c = "nes" Then rMulti = False
                        Case Mrow.Contains(p_c & ".input.fcexp 4player"), Mrow.Contains(p_c & ".input.fcexp partytap")
                            EmptyValuse = " none "
                        Case Else
                            rMulti = False
                    End Select

                    If rMulti = True Then
                        MultiMex = MsgBox("It seems to have enabled the multitap parameter:" & vbCrLf &
                                          Mrow & vbCrLf &
"If you want I can disable it for you." & vbCrLf &
"Do you want to disable the multitap?", vbYesNo + vbInformation, "Message multitap")
                        If MultiMex = vbYes Then
                            SMrow = Split(Mrow, " ")
                            pargMT = pargMT & " -" & SMrow(0) & EmptyValuse
                        End If
                    End If

                End While
                reader.Dispose()
                reader.Close()
            End Using
        Catch
        End Try
    End Sub

    Public Sub BackupMCR()
        Dim cm As Integer = 0
        Try
            Dim fileEntries As String() = Directory.GetFiles(MedGuiR.TextBox4.Text & "\sav\")

            For Each fileName As String In fileEntries
                If fileName.Contains(Path.GetFileNameWithoutExtension(MedGuiR.TextBox1.Text)) And Path.GetExtension(fileName) = ".mcr" And existMCR = False Then
                    Dim rm As String
                    rm = MsgBox(cm & ".mcr save state detected, to prevent overwriting I backup it into a safe location.", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, "MCR savestate detected!")
                    My.Computer.FileSystem.CopyFile(fileName, MedExtra & "Backup\Save\" & Path.GetFileName(fileName), True)
                    cm = cm + 1
                End If
            Next
        Catch
        End Try

    End Sub

    Public Sub RestoreMCR()
        Dim cm As Integer = 0

        Try
            Dim fileEntries As String() = Directory.GetFiles(MedExtra & "Backup\Save\")
            For Each fileName As String In fileEntries
                If fileName.Contains(Path.GetFileNameWithoutExtension(MedGuiR.TextBox1.Text)) And Path.GetExtension(fileName) = ".mcr" Then
                    existMCR = True
                    If MedGuiR.NetToolStripButton.BackColor = SystemColors.Control Then
                        Dim rm As String
                        rm = MsgBox(cm & ".mcr save state backup detected." & vbCrLf &
                                    "Do you want to restore it?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, "MCR savestate detected!")
                        If rm = vbYes Then
                            My.Computer.FileSystem.MoveFile(fileName, MedGuiR.TextBox4.Text & "\sav\" & Path.GetFileName(fileName), True)
                        Else
                            My.Computer.FileSystem.DeleteFile(fileName, FileIO.UIOption.AllDialogs, FileIO.RecycleOption.SendToRecycleBin)
                        End If
                    End If
                    cm = cm + 1
                End If
            Next
            If cm = 0 Then existMCR = False
        Catch
        End Try

    End Sub

End Module