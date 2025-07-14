Module verProcess
    Public tProcess, wDir, Arg, pSox As String, execute As New Process

    Public Sub verMednafen()

        If ISON_Mednafen(200) = True Then
            My.Computer.FileSystem.CopyFile(MedGuiR.TextBox4.Text & "\" & DMedConf & ".cfg", MedExtra & "Backup\" & "Bkp.tmp", True)
            KillProcess()
            My.Computer.FileSystem.MoveFile(MedExtra & "Backup\" & "Bkp.tmp", MedGuiR.TextBox4.Text & "\" & DMedConf & ".cfg", True)
        End If
        'End If

    End Sub

    Friend Function ISON_Mednafen(sleep As Integer) As Boolean
        Threading.Thread.Sleep(sleep)
        Dim Mednafen_Exe() As Process
        Mednafen_Exe = Process.GetProcessesByName("mednafen", My.Computer.Name)
        If Mednafen_Exe.Length > 0 Then
            ISON_Mednafen = True
        Else
            ISON_Mednafen = False
        End If
    End Function

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
                Dim ismednafen As Boolean = False
                'If Val(vmedClear) > 9480 Then
                If tProcess = "mednafen" Then
                    System.Environment.SetEnvironmentVariable("MEDNAFEN_NOPOPUPS", 1, EnvironmentVariableTarget.Process)
                    .FileName = IO.Path.Combine(wDir, tProcess)
                    .WindowStyle = ProcessWindowStyle.Hidden
                    .CreateNoWindow = True
                    .UseShellExecute = False
                    ismednafen = True
                Else
                    System.Environment.SetEnvironmentVariable("MEDNAFEN_NOPOPUPS", Nothing, EnvironmentVariableTarget.Process)
                    .FileName = tProcess
                    .WorkingDirectory = wDir
                    .WindowStyle = ProcessWindowStyle.Normal
                    .CreateNoWindow = False
                    .UseShellExecute = True
                    ismednafen = False
                End If

                .Arguments = Arg

                If ismednafen = False Then
                    Select Case tProcess
                        Case "vgmplay", "GBS2GB"
                        Case "mico"
                            .WindowStyle = ProcessWindowStyle.Hidden
                            .CreateNoWindow = True
                            .UseShellExecute = True
                        Case Else
                            If SoxStatus.Visible = True Then
                                .WindowStyle = ProcessWindowStyle.Hidden
                                .CreateNoWindow = True
                                .UseShellExecute = True
                            Else
                                .WindowStyle = ProcessWindowStyle.Normal
                                .CreateNoWindow = False
                                .UseShellExecute = True
                            End If
                    End Select
                End If

                'If tProcess <> "vgmPlay" Then
                'If SoxStatus.Visible = True Then .WindowStyle = ProcessWindowStyle.Hidden Else .WindowStyle = ProcessWindowStyle.Normal
                'End If
            End With
            execute.Start()
        Catch ex As Exception
            If countstart > 10 Then
                MsgBox("Unable to start " & tProcess, MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "Process Error")
                Exit Sub
            Else
                countstart += 1
                'Threading.Thread.Sleep(500)
                GoTo AGAIN
            End If
        End Try

    End Sub

End Module