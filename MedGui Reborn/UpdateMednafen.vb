Imports System.IO
Imports System.Net

Module UpdateMednafen
    Public LastMednafenClean, LastMednafenFull As String
    Private REGUpdate As Integer = 0
    Private UpMedServ As String = ""

    Public Sub DetectLastMednafen()
        If Directory.Exists(MedExtra & "Update") = False Then Directory.CreateDirectory(MedExtra & "Update\")
        If UpdateServer Is Nothing Or UpdateServer = "" Then Test_Server()

        If Val(Environment.OSVersion.Version.ToString) >= 6 Then
            'MakeRegKey()
            'If REGUpdate = -1 Then
            'My.Computer.Network.DownloadFile(UpdateServer & "/MedGuiR/Mednafen/ChangeLog.txt", MedExtra & "Update\MednafenUpdate.txt", "anonymous", "anonymous", True, 1000, True)
            'UpMedServ = UpdateServer & "/MedGuiR/Mednafen/mednafen-"
            'Else

            Try
                ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
                Dim W As New WebClient
                W.DownloadFile("https://mednafen.github.io/documentation/ChangeLog", MedExtra & "Update\MednafenUpdate.txt")
                UpMedServ = "https://raw.githubusercontent.com/mednafen/mednafen.github.io/master/releases/files/mednafen-"
            Catch
                Message.Label1.Text = Environment.OSVersion.Platform.ToString & " .NET framework version 3.5 and earlier versions did not provide support for applications to use TLS 1.2 System Default Versions as a cryptographic protocol." & vbCrLf &
                 "You must to install a Microsoft update to enables the use of TLS v1.2 in the .NET Framework 3.5 and earlier." & vbCrLf &
                "Follow this instruction to install specific version for your OS"
                Message.LinkLabel1.Text = "https://community.qualys.com/thread/16917-net-framework"
                Message.ShowDialog()
                'Process.Start(Message.LinkLabel1.Text)
                Exit Sub
            End Try

            'End If
        Else
            Try

                If UpdateServer.StartsWith("https://") Then
                    My.Computer.Network.DownloadFile(UpdateServer & "/MedGuiR/Mednafen/ChangeLog.txt", MedExtra & "Update\MednafenUpdate.txt", "anonymous", "anonymous", True, 1000, True)
                ElseIf UpdateServer.StartsWith("ftp://") Then
                    FTPDownloadFile(MedExtra & "Update\MednafenUpdate.txt", UpdateServer & "/MedGuiR/Mednafen/ChangeLog.txt", "anonymous", "anonymous")
                End If

                UpMedServ = UpdateServer & "/MedGuiR/Mednafen/mednafen-"
            Catch
                MsgBox("Unable to detect last Mednafen version", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation)
                Exit Sub
            End Try
        End If

        If File.Exists(MedExtra & "Update\MednafenUpdate.txt") = False Then
            MsgBox("Unable to detect last Mednafen version", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim fileReader As StreamReader
        fileReader = My.Computer.FileSystem.OpenTextFileReader(MedExtra & "Update\MednafenUpdate.txt")
        Dim stringReader As String
        stringReader = fileReader.ReadLine()
        stringReader = Replace(stringReader, "--", "")
        stringReader = Replace(stringReader, ":", "")
        LastMednafenFull = stringReader
        stringReader = Replace(stringReader, ".", "")
        LastMednafenClean = Replace(stringReader, "-UNSTABLE", "")
        If Len(LastMednafenClean.Trim) < 5 Then LastMednafenClean = LastMednafenClean & "0"
        LastMednafenClean = Val(LastMednafenClean)
        fileReader.Close()

        Dim m_mu As String
        If Val(vmedClear) < LastMednafenClean Then
            m_mu = MsgBox("Mednafen v" & LastMednafenFull & " is Available to download." & vbCrLf &
                           "Do you want download it?", MsgBoxStyle.YesNo + MsgBoxStyle.Information)
            If m_mu = vbYes Then UpdateLastMednafen()
        Else
            If MedGuiR.AutoUp = False Then MsgBox("Congratulations, your Mednafen version is up to date!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
        End If

    End Sub

    Public Sub UpdateLastMednafen()
        contr_os()
        Try

            If UpMedServ.StartsWith("https://") Then
                My.Computer.Network.DownloadFile(UpMedServ & LastMednafenFull.Trim & "-win" & c_os & ".zip", MedExtra & "Update\LastMednafen.zip", "", "", True, 1000, True)
            ElseIf UpMedServ.StartsWith("ftp://") Then
                FTPDownloadFile(MedExtra & "Update\LastMednafen.zip", UpMedServ & LastMednafenFull.Trim & "-win" & c_os & ".zip", "anonymous", "anonymous")
            End If

            'SevenZipExtractor.SetLibraryPath(MedExtra & "Plugins\" & sevenzdll)
            'Dim szip As SevenZipExtractor = New SevenZipExtractor(MedExtra & "Update\LastMednafen.zip")
            'szip.ExtractArchive(MedGuiR.TextBox4.Text)
            'SoxStatus.Text = "Waiting for extraction..."
            'SoxStatus.Label1.Text = "..."
            'SoxStatus.Show()

            DecompressArchive(MedExtra & "Update\LastMednafen.zip", MedGuiR.TextBox4.Text)
        Catch
            MsgBox("unexpected error while Download/extract", vbOKOnly + MsgBoxStyle.Critical)
            SoxStatus.Close()
        End Try

        Threading.Thread.Sleep(2000)
        'SoxStatus.Close()

        File.Delete(MedExtra & "Update\LastMednafen.zip")
        MednafenV()
    End Sub

    Private Sub MakeRegKey()
        Try
            If My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\.NETFramework\v2.0.50727",
"SystemDefaultTlsVersions", Nothing) IsNot Nothing Then
            Else
                REGUpdate = 1
            End If

            If My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\.NETFramework\v2.0.50727",
"SystemDefaultTlsVersions", Nothing) IsNot Nothing Then
            Else
                REGUpdate = 2
            End If

            If REGUpdate > 0 Then
                MsgBox("Now github support only tls 1.2, I must to set a register key to make it compatible with .net 2.0", MsgBoxStyle.Exclamation + vbOKOnly, "WRITE WINDOWS REGISTER KEY...")
                Dim REGRISP = MsgBox("I'm writing a registry key on HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\.NETFramework\v2.0.50727" &
                  vbCrLf & "I do not consider myself responsible for any problems with your system registry, do you want to continue?", MsgBoxStyle.Critical + vbOKCancel, "WRITING REGISTRY...")

                If REGRISP = vbOK Then
                    Select Case c_os
                        Case "64"
                            My.Computer.Registry.LocalMachine.CreateSubKey("SOFTWARE\Microsoft\.NETFramework\v2.0.50727")
                            My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\.NETFramework\v2.0.50727",
              "SystemDefaultTlsVersions", 1)
                            My.Computer.Registry.LocalMachine.CreateSubKey("SOFTWARE\Wow6432Node\Microsoft\.NETFramework\v2.0.50727")
                            My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\.NETFramework\v2.0.50727",
              "SystemDefaultTlsVersions", 1)
                        Case "32"
                            My.Computer.Registry.LocalMachine.CreateSubKey("SOFTWARE\Microsoft\.NETFramework\v2.0.50727")
                            My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\.NETFramework\v2.0.50727",
              "SystemDefaultTlsVersions", 1)
                    End Select
                Else
                    REGUpdate = -1
                End If

            End If
        Catch
            REGUpdate = -1
        End Try

    End Sub

End Module