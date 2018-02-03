Module verProcess
    Public tProcess, wDir, Arg, pSox As String, execute As New Process

    Public Sub verMednafen()
        Dim Mednafen_Exe() As Process
        Mednafen_Exe = Process.GetProcessesByName("mednafen", My.Computer.Name)

        Threading.Thread.Sleep(200)
        If Mednafen_Exe.Length > 0 Then
            My.Computer.FileSystem.CopyFile(MedGuiR.TextBox4.Text & "\" & DMedConf & ".cfg", MedExtra & "Backup\" & "Bkp.tmp", True)
            'System.IO.File.SetAttributes(MedExtra & "Backup\" & "Bkp.tmp", System.IO.FileAttributes.Hidden)
            KillProcess()
            My.Computer.FileSystem.MoveFile(MedExtra & "Backup\" & "Bkp.tmp", MedGuiR.TextBox4.Text & "\" & DMedConf & ".cfg", True)
            'System.IO.File.SetAttributes(MedGuiR.TextBox4.Text & "\mednafen-09x.cfg", System.IO.FileAttributes.Normal)
        End If

    End Sub

    Public Sub EmbProcess()
        'Dim myProcess As New Process()
        'Dim myProcessStartInfo As New ProcessStartInfo("start.exe")
        'myProcessStartInfo.UseShellExecute = False
        'myProcessStartInfo.RedirectStandardOutput = True
        'myProcess.StartInfo = myProcessStartInfo
        'myProcess.Start()

        'Dim myStreamReader As IO.StreamReader = myProcess.StandardOutput
        ' Read the standard output of the spawned process.
        'Dim myString As String = myStreamReader.ReadLine()
        'Console.WriteLine(myString)

        'myProcess.Close()

    End Sub

    Public Sub KillProcess()

        Dim myProcesses() As Process
        Dim myProcess As Process
        myProcesses = Process.GetProcessesByName(tProcess)
        For Each myProcess In myProcesses
            Try
                If tProcess = "xmplay" Then myProcess.Kill() : Exit Sub
                myProcess.CloseMainWindow()
                myProcess.WaitForExit()
                myProcess.Close()
                myProcess.Kill()
            Catch
            End Try
        Next

    End Sub

    Public Sub StartProcess()
        Dim countstart As Integer = 0

AGAIN:

        Try
            With execute.StartInfo
                .FileName = tProcess
                .Arguments = Arg
                .WorkingDirectory = wDir

                Select Case tProcess
                    Case "vgmplay", "GBS2GB"
                    Case "mico"
                        .WindowStyle = ProcessWindowStyle.Hidden
                    Case Else
                        If SoxStatus.Visible = True Then .WindowStyle = ProcessWindowStyle.Hidden Else .WindowStyle = ProcessWindowStyle.Normal
                End Select
                'If tProcess <> "vgmPlay" Then
                'If SoxStatus.Visible = True Then .WindowStyle = ProcessWindowStyle.Hidden Else .WindowStyle = ProcessWindowStyle.Normal
                'End If
            End With
            execute.Start()
        Catch ex As Exception
            If countstart > 5 Then
                MsgBox("Unable to start " & tProcess, MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "Process Error")
            Else
                GoTo AGAIN
            End If
        End Try

    End Sub

End Module