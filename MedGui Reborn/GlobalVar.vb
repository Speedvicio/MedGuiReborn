Imports System.IO

Module GlobalVar

    Public Startup_Path, UCInick, UCIserver, UCIport, UCIchannel, vmedClear, MedShader, UpdateServer, MGRH,
    JUP, JDOWN, JLEFT, JRIGHT, JSTART, JSELECT, JA, JX, JY, JB, JL, JR, p_c, x864, DMedConf, SScart As String, forMax, stopiso, noftp As Boolean

    Public NewAPI As Boolean = True
    Public gIcon As Icon

    Public Sub Startup_setting()
        GeneralRMIni()
        If MedGuiR.GridToolStripMenuItem.Checked = True Then GridRStyle()
        DirectoryRMIni()
        UCIRMini()
        RJoypadMini()

        If MedGuiR.ComboBox1.Text = "" Then MedGuiR.ComboBox1.Text = "NoIntro"
        'If File.Exists(MedExtra & "Scanned\" & Startup_Path & ".csv") = False Then
        'MsgBox("..\Scanned\" & Startup_Path & ".csv missing" & vbCrLf &
        '      "Please rebuild it if necessary", MsgBoxStyle.OkOnly + vbExclamation, Startup_Path & ".csv missing")
        'Startup_Path = ""
        'End If

        If Startup_Path = "fav" Then
            MedGuiR.FavouritesToolStripButton.PerformClick()
        Else
            MedGuiR.SY.SelectedItem = Startup_Path
        End If

        GridRMIni()
        NetPlayMini()

        If forMax = False Then
            MedGuiR.WindowState = FormWindowState.Normal
        ElseIf forMax = True Then
            MedGuiR.WindowState = FormWindowState.Maximized
        End If

        If IO.File.Exists(MedExtra & "\Converter\sox.exe") = False Then
            MedGuiR.ListAddsFile.Enabled = False
            MedGuiR.Label25.ForeColor = Color.Red
            MedGuiR.Label25.Text = "Sox.exe missing, this utility will be disabled"
        End If

        'If IO.File.Exists(MedExtra & "\NetPlay\mednafen-server.exe") = False Then MedGuiR.ServerToolStripButton.Enabled = False

    End Sub

    Public Sub Test_Server()
        If My.Computer.Network.IsAvailable = True Then
            Dim webexist As Boolean = False

            Try
                Dim request As Net.HttpWebRequest = DirectCast(Net.HttpWebRequest.Create("http://medgui.orgfree.com/"), Net.HttpWebRequest)
                request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; ru; rv:1.9.2.3) Gecko/20100401 Firefox/4.0 (.NET CLR 3.5.30729"
                'request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:64.0) Gecko/20100101 Firefox/64.0"
                request.KeepAlive = True
                request.Method = "GET"
                Using response As Net.HttpWebResponse = DirectCast(request.GetResponse(), Net.HttpWebResponse)
                    webexist = Not (response Is Nothing OrElse response.StatusCode <> Net.HttpStatusCode.OK)
                End Using
            Catch ex As Exception
                webexist = False
            End Try

            If webexist = True Then
                UpdateServer = "http://medgui.orgfree.com/"
            Else
                UpdateServer = "ftp://anonymous@speedvicio.ddns.net"
            End If

        End If

    End Sub

    Public Sub set_special_module()
        p_c = consoles
        If consoles = "pce" Then
            If MedGuiR.CheckBox1.Checked = False Then p_c = "pce" Else p_c = "pce_fast"
        ElseIf consoles = "snes" Then
            If MedGuiR.CheckBox15.Checked = False Then p_c = "snes" Else p_c = "snes_faust"
        Else : p_c = consoles
        End If
    End Sub

    Public Sub exist_Mednafen()

        Try
            Test_Server()

            If File.Exists(Application.StartupPath & "\SevenZipSharp.dll") = False Then
                noftp = True
                If My.Computer.Network.IsAvailable = True Then
                    'My.Computer.Network.DownloadFile(UpdateServer & "/MedGuiR/SevenZipSharp.txt", Application.StartupPath & "\SevenZipSharp.dll", "anonymous", "anonymous", True, 1000, True)
                    'My.Computer.Network.DownloadFile(UpdateServer & "/MedGuiR/7z.txt", MedExtra & "Plugins\7z.dll", "anonymous", "anonymous", True, 1000, True)
                    FTPDownloadFile(Application.StartupPath & "\SevenZipSharp.dll", UpdateServer & "/MedGuiR/SevenZipSharp.txt", "anonymous", "anonymous")
                    FTPDownloadFile(MedExtra & "Plugins\7z.dll", UpdateServer & "/MedGuiR/7z.txt", "anonymous", "anonymous")
                    MsgBox("SevenZipSharp.dll downloaded, please reboot MedGuiR", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation)
                    MedGuiR.Close()
                    Exit Sub
                Else
                    MsgBox("SevenZipSharp.dll missing!, please download MedGui Reborn full package from Sourceforge", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "Library missing")
                    MedGuiR.Close()
                    Exit Sub
                End If
            End If

            If File.Exists(MedExtra & "\Plugins\7z.dll") = False Then
                If My.Computer.Network.IsAvailable = True Then
                    'My.Computer.Network.DownloadFile(UpdateServer & "/MedGuiR/7z.txt", MedExtra & "Plugins\7z.dll", "anonymous", "anonymous", True, 1000, True)
                    FTPDownloadFile(MedExtra & "Plugins\7z.dll", UpdateServer & "/MedGuiR/7z.txt", "anonymous", "anonymous")
                Else
                    MsgBox("7z.dll missing!, please download MedGui Reborn full package from Sourceforge", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "Library missing")
                    Exit Sub
                End If
            End If

            If File.Exists(Application.StartupPath & "\IrcClient.dll") = False Then
                If My.Computer.Network.IsAvailable = True Then
                    'My.Computer.Network.DownloadFile(UpdateServer & "/MedGuiR/IrcClient.txt", Application.StartupPath & "\IrcClient.dll", "anonymous", "anonymous", True, 1000, True)
                    FTPDownloadFile(Application.StartupPath & "\IrcClient.dll", UpdateServer & "/MedGuiR/IrcClient.txt", "anonymous", "anonymous")
                Else
                    MedGuiR.IRCToolStripButton.Enabled = False
                End If
            End If

            If File.Exists(Application.StartupPath & "\FTPclient.dll") = False Then
                If My.Computer.Network.IsAvailable = True Then
                    'My.Computer.Network.DownloadFile(UpdateServer & "/MedGuiR/FTPclient.txt", Application.StartupPath & "\FTPclient.dll", "anonymous", "anonymous", True, 1000, True)
                    FTPDownloadFile(Application.StartupPath & "\FTPclient.dll", UpdateServer & "/MedGuiR/FTPclient.txt", "anonymous", "anonymous")
                Else
                    MedGuiR.Button53.Enabled = False
                    MedGuiR.CheckBox18.Checked = False
                    MedGuiR.CheckBox18.Enabled = False
                End If
            End If

            If File.Exists(Application.StartupPath & "\DiscSN.dll") Then
                File.Delete(Application.StartupPath & "\DiscSN.dll")
            End If

            If File.Exists(Application.StartupPath & "\DiscTools.dll") = False Then
                If My.Computer.Network.IsAvailable = True Then
                    'My.Computer.Network.DownloadFile(UpdateServer & "/MedGuiR/DiscTools.txt", Application.StartupPath & "\DiscTools.dll", "anonymous", "anonymous", True, 1000, True)
                    FTPDownloadFile(Application.StartupPath & "\DiscTools.dll", UpdateServer & "/MedGuiR/DiscTools.txt", "anonymous", "anonymous")
                End If
            End If

            If File.Exists(Application.StartupPath & "\PeakMeterCtrl.dll") = False Then
                If My.Computer.Network.IsAvailable = True Then
                    'My.Computer.Network.DownloadFile(UpdateServer & "/MedGuiR/PeakMeterCtrl.txt", Application.StartupPath & "\PeakMeterCtrl.dll", "anonymous", "anonymous", True, 1000, True)
                    FTPDownloadFile(Application.StartupPath & "\PeakMeterCtrl.dll", UpdateServer & "/MedGuiR/PeakMeterCtrl.txt", "anonymous", "anonymous")
                End If
            End If

            If File.Exists(Application.StartupPath & "\CoreAudioApi.dll") = False Then
                If My.Computer.Network.IsAvailable = True Then
                    'My.Computer.Network.DownloadFile(UpdateServer & "/MedGuiR/CoreAudioApi.txt", Application.StartupPath & "\CoreAudioApi.dll", "anonymous", "anonymous", True, 1000, True)
                    FTPDownloadFile(Application.StartupPath & "\CoreAudioApi.dll", UpdateServer & "/MedGuiR/CoreAudioApi.txt", "anonymous", "anonymous")
                End If
            End If

            If File.Exists(Application.StartupPath & "\fmod.dll") = False Then
                If My.Computer.Network.IsAvailable = True Then
                    'My.Computer.Network.DownloadFile(UpdateServer & "/MedGuiR/fmod.txt", Application.StartupPath & "\fmod.dll", "anonymous", "anonymous", True, 1000, True)
                    FTPDownloadFile(Application.StartupPath & "\fmod.dll", UpdateServer & "/MedGuiR/fmod.txt", "anonymous", "anonymous")
                End If
            End If

            If File.Exists(Application.StartupPath & "\LinqBridge.dll") = False Then
                If My.Computer.Network.IsAvailable = True Then
                    'My.Computer.Network.DownloadFile(UpdateServer & "/MedGuiR/LinqBridge.txt", Application.StartupPath & "\LinqBridge.dll", "anonymous", "anonymous", True, 1000, True)
                    FTPDownloadFile(Application.StartupPath & "\LinqBridge.dll", UpdateServer & "/MedGuiR/LinqBridge.txt", "anonymous", "anonymous")
                End If
            End If

            If File.Exists(MedExtra & "\Plugins\unecm.exe") = False Then
                If My.Computer.Network.IsAvailable = True Then
                    'My.Computer.Network.DownloadFile(UpdateServer & "/MedGuiR/unecm.txt", MedExtra & "Plugins\unecm.exe", "anonymous", "anonymous", True, 1000, True)
                    FTPDownloadFile(MedExtra & "Plugins\unecm.exe", UpdateServer & "/MedGuiR/unecm.txt", "anonymous", "anonymous")
                Else
                    MsgBox("unecm.exe missing!, please download MedGui Reborn full package from Sourceforge", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "Executable missing")
                    Exit Sub
                End If
            End If

            If File.Exists(MedExtra & "\Plugins\mico.exe") = False Then
                If My.Computer.Network.IsAvailable = True Then
                    'My.Computer.Network.DownloadFile(UpdateServer & "/MedGuiR/mico.txt", MedExtra & "Plugins\mico.exe", "anonymous", "anonymous", True, 1000, True)
                    FTPDownloadFile(MedExtra & "Plugins\mico.exe", UpdateServer & "/MedGuiR/mico.txt", "anonymous", "anonymous")
                Else
                    MsgBox("mico.exe missing!, please download MedGui Reborn full package from Sourceforge", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "Executable missing")
                    Exit Sub
                End If
            End If

            If File.Exists(MedExtra & "FAQ\MedClientTutorial\Start.html") Then
                MedGuiR.Button57.Enabled = True
            End If

            'MissingResource()

            Dim MednafenDetected As Boolean = False
            If File.Exists(MedGuiR.TextBox4.Text & "\mednafen.exe") = False Then
                Dim Directories() As String
                Dim d As DirectoryInfo
                Directories = Directory.GetDirectories(Application.StartupPath)
                For Each Dir As String In Directories
                    d = New DirectoryInfo(Dir)
                    If d.Name <> "MedGuiR" Then
                        Dim Files() As String
                        Dim f As FileInfo
                        Files = Directory.GetFiles(d.FullName)
                        For Each sFile As String In Files
                            f = New FileInfo(sFile)
                            If LCase(f.Name) = "mednafen.exe" Then
                                MedGuiR.TextBox4.Text = d.FullName
                                MednafenDetected = True
                                Exit For
                            ElseIf LCase(f.Name) = "emu4crt.exe" Then
                                Dim msg4crt = MsgBox("Seem that you are using emu4crt fork" & vbCrLf &
                                          "Do you want to make a clone renamed mednafen?", vbInformation + vbYesNo, "emu4crt detected...")
                                If msg4crt = MsgBoxResult.Yes Then
                                    My.Computer.FileSystem.CopyFile(f.FullName, Path.Combine(d.FullName, "mednafen.exe"))
                                    MedGuiR.TextBox4.Text = d.FullName
                                    MednafenDetected = True
                                    Exit For
                                End If
                            End If
                            If MednafenDetected = True Then Exit For
                        Next
                    End If
                Next

                If File.Exists(MedGuiR.TextBox4.Text & "\emu4crt.exe") = True And MednafenDetected = False Then
                    Dim msg4crt = MsgBox("Seem that you are using emu4crt fork" & vbCrLf &
                                                              "Do you want to make a clone renamed mednafen?", vbInformation + vbYesNo, "emu4crt detected...")
                    If msg4crt = MsgBoxResult.Yes Then
                        My.Computer.FileSystem.CopyFile(MedGuiR.TextBox4.Text & "\emu4crt.exe", MedGuiR.TextBox4.Text & "\mednafen.exe")
                        MedGuiR.TextBox4.Text = MedGuiR.TextBox4.Text
                        MednafenDetected = True
                    End If
                End If

                If MednafenDetected = False Then
                    Dim fdmdf As FolderBrowserDialog = New FolderBrowserDialog()
                    fdmdf.Description = "Select Mednafen Path"
                    MsgBox("Select a Valid Mednafen Path.", vbCritical + MsgBoxStyle.OkOnly, "Mednafen Not Detected")
                    If fdmdf.ShowDialog() = DialogResult.OK Then
                        MedGuiR.TextBox4.Text = fdmdf.SelectedPath
                        exist_Mednafen()
                    Else
                        Dim risp = MsgBox("Seem you have not already downloaded Mednafen" & vbCrLf &
                                          "Do you want to download it?", vbCritical + MsgBoxStyle.YesNo, "Mednafen Not Detected")
                        If risp = vbYes Then
                            vmedClear = 0
                            MedGuiR.TextBox4.Text = Application.StartupPath & "\Mednafen"
                            DetectLastMednafen()
                            exist_Mednafen()
                            Dim fRisp = MsgBox("I can also download Mednafen bios pack for you." & vbCrLf &
                                          "Do you want to download and extract it?", vbCritical + MsgBoxStyle.YesNo, "Mednafen Bios pack...")
                            If fRisp = vbYes Then DownExtractBios()
                            MsgBox("You're almost there." & vbCrLf &
"Now set up the folders containing your games in Rom Path 1/2 tabs on the right", vbOKOnly + MsgBoxStyle.Information, "Instructions")
                            MedGuiR.TabControl1.SelectedTab = MedGuiR.TabPage3
                        Else
                            Message.Label1.Text = "There is no sense to open this GUI without Mednafen" & vbCrLf &
                "Please download Last Mednafen version at:"
                            Message.LinkLabel1.Text = "http://forum.fobby.net/index.php?t=thread&frm_id=19&"
                            Message.ShowDialog()
                            MedGuiR.Close()
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MGRWriteLog("MedGuiR - exist_Mednafen: " & Date.Today.ToString & " " & ex.Message)
        End Try
    End Sub

    Public Sub MednafenV()
        If MedGuiR.TextBox4.Text = "" Or File.Exists(MedGuiR.TextBox4.Text & "\mednafen.exe") = False Then Exit Sub

CheckConfig:

        tProcess = "mednafen"
        wDir = MedGuiR.TextBox4.Text
        Arg = Nothing
        StartProcess()
        'Process.Start(MedGuiR.TextBox4.Text & "\mednafen.exe")
        Threading.Thread.Sleep(2000)

        If File.Exists(MedGuiR.TextBox4.Text & "\mednafen.cfg") And File.Exists(MedGuiR.TextBox4.Text & "\mednafen-09x.cfg") Then
            If File.Exists(MedExtra & "\Backup\OLD_mednafen-09x.cfg") Then File.Delete(MedExtra & "\Backup\OLD_mednafen-09x.cfg")
            File.Move(MedGuiR.TextBox4.Text & "\mednafen-09x.cfg", MedExtra & "\Backup\OLD_mednafen-09x.cfg")
            MsgBox("Old Mednafen Configuration File moved into Backup folder.", vbInformation + MsgBoxStyle.OkOnly)
            DMedConf = "mednafen"
        ElseIf File.Exists(MedGuiR.TextBox4.Text & "\mednafen.cfg") Then
            DMedConf = "mednafen"
        ElseIf File.Exists(MedGuiR.TextBox4.Text & "\mednafen-09x.cfg") Then
            DMedConf = "mednafen-09x"
        Else
            MsgBox("No Mednafen Configuration File Found , I proceeded to create one myself.", vbInformation + MsgBoxStyle.OkOnly)
            GoTo CheckConfig
        End If

        Dim CountAttemp As Integer = 0

CheckMednafen:
        Threading.Thread.Sleep(500)

        'If File.Exists(MedGuiR.TextBox4.Text & "\mednafen-09x.cfg") = True Then
        'Threading.Thread.Sleep(1000)
        Dim cmed() As Process = Process.GetProcessesByName("mednafen", My.Computer.Name)
        If cmed.Length > 0 Then
            CountAttemp = CountAttemp + 1
            If CountAttemp > 10 Then
                Dim mop = MsgBox("Please close Mednafen emulation to open MedGui Reborn" & vbCrLf &
                                 "Ok = Continue, Cancel = Close MedGuiR", MsgBoxStyle.Information + vbOKCancel, "Can't run MedGui Reborn...")

                If mop = MsgBoxResult.Cancel Then MedGuiR.ResetAll = True : MedGuiR.Close()
            End If
            GoTo CheckMednafen
        End If

        'End If

        DetectMedGuiR()
        Detectx86_4()

        Dim readText() As String = File.ReadAllLines(MedGuiR.TextBox4.Text & "\" & DMedConf & ".cfg")
        Dim vmedFull As String
        vmedFull = readText.GetValue(0)
        vmedFull = Replace(vmedFull, ";VERSION ", "")
        vmedClear = Replace(vmedFull, ".", "")
        vmedClear = Replace(vmedClear, "-UNSTABLE", "")
        If Len(vmedClear.Trim) < 5 Then vmedClear = vmedClear & "0"
        If Val(vmedClear) < 9380 And vmedClear <> "" Then
            Message.Label1.Text = "MedGui Reborn support only Mednafen >= 0.9.38" & vbCrLf &
                "Please download Last Mednafen version at:"
            Message.LinkLabel1.Text = "http://forum.fobby.net/index.php?t=thread&frm_id=19&"
            Message.ShowDialog()
            MedGuiR.Close()
        Else
            vmedFull = Replace(vmedFull, "-UNSTABLE", "-U")
            MedGuiR.Label8.Text = "Mednafen v." & vmedFull
            MedGuiR.Label57.Text = x864
        End If

        For i = 9380 To Val(vmedClear)
            Select Case i
                Case 9380
                    MedShader = ".pixshader"
                Case 9410
                    MedShader = ".shader"
            End Select
        Next i

        If Val(vmedClear) < 12200 Then
            MedGuiR.SY.Items.Remove("apple2")
            MedGuiR.TextBox22.Visible = False
            MedGuiR.Button58.Visible = False
            MedGuiR.Label29.Visible = False
            SScart = Nothing
        Else
            SScart = ".auto_default"
        End If

    End Sub

    Public Sub Detectx86_4()
        Try
            Dim fs As Stream = File.Open(MedGuiR.TextBox4.Text & "\mednafen.exe", FileMode.Open, FileAccess.Read)
            Dim br As BinaryReader = New BinaryReader(fs)

            Dim mz As UInt16 = br.ReadUInt16
            If (mz = 23117) Then
                fs.Position = 60
                ' this location contains the offset for the PE header
                Dim peoffset As UInt32 = br.ReadUInt32
                fs.Position = (peoffset + 4)
                ' contains the architecture
                Dim machine As UInt16 = br.ReadUInt16
                If (machine = 34404) Then
                    x864 = "x64"
                ElseIf (machine = 332) Then
                    x864 = "x86"
                ElseIf (machine = 512) Then
                    x864 = "x64"
                Else
                    x864 = "Unknown"
                End If
            Else
                MsgBox("Invalid image")
            End If

            br.Close()
        Catch
        End Try
    End Sub

    Public Sub Detect_Faust()
        Dim cont As Integer = 0
        If Dir(MedGuiR.TextBox4.Text & "\" & DMedConf & ".cfg") <> "" Then
            Dim readText() As String = File.ReadAllLines(MedGuiR.TextBox4.Text & "\" & DMedConf & ".cfg")
            Dim s As String
            For Each s In readText
                If s.Contains("snes_faust") Then
                    MedGuiR.CheckBox15.Visible = True
                    cont = cont + 1
                    Exit For
                ElseIf s.Contains("snes_faust") = False Then
                    cont = 0
                End If
            Next
            If cont = 0 Then MedGuiR.CheckBox15.Enabled = False
        End If
    End Sub

    Public Sub detect_saturn()
        Dim cont As Integer = 0
        If Dir(MedGuiR.TextBox4.Text & "\" & DMedConf & ".cfg") <> "" Then
            Dim readText() As String = File.ReadAllLines(MedGuiR.TextBox4.Text & "\" & DMedConf & ".cfg")
            Dim s As String
            For Each s In readText
                If s.Contains("ss.region") Then
                    IsoSelector.Button1.Visible = True
                    cont = cont + 1
                    Exit For
                ElseIf s.Contains("ss.region") = False Then
                    cont = 0
                End If
            Next
            If cont = 0 Then IsoSelector.Button1.Enabled = False
        End If
    End Sub

    Public Sub OS_Version()
        If UCase(My.Computer.Info.OSFullName.Contains("XP")) Then
            Exit Sub
        Else
            Application.EnableVisualStyles()
        End If
    End Sub

    Public Sub SingleScan()
        skipother = False
        stopscan = False
        stopiso = False
        TempFolder = "RomTemp"
        type_csv = ""
        MedGuiR.Text = "MedGui Reborn"
        MedGuiR.TextBox1.Text = percorso
        Counter = 0
        get_ext()

        Try
            If Counter <= 1 Then scan.decript() : MedGuiR.DataGridView1.Rows(0).Cells(3).ToolTipText = "CRC " & base_file
        Catch ex As Exception
            MGRWriteLog("GlobalVar - SingleScan: " & ex.Message)
        End Try
        SoxStatus.Close()
        fileTXT = ""

        'MedGuiR.remove_double()
        MedGuiR.Datagrid_filter()
    End Sub

    Public Sub CustomScanFolder()
        MedGuiR.ListBox2.Items.Clear()
        For Each PerScanF As String In IO.Directory.GetFiles(MedExtra & "Scanned\", "*.csv")
            Select Case Path.GetFileNameWithoutExtension(PerScanF)', ".csv", "")
                Case "def", "gb", "gba", "gg", "lynx", "md", "nes", "ngp", "pce", "pcfx", "psx", "sms", "snes", "ss", "vb", "wswan", "fav", "last"

                Case Else
                    MedGuiR.ListBox2.Items.Add(Path.GetFileNameWithoutExtension(PerScanF)) ', ".csv", ""))
            End Select
        Next
    End Sub

    Public Sub DownloadRomext()
        If My.Computer.Network.IsAvailable = True Then
            'My.Computer.Network.DownloadFile(UpdateServer & "/MedGuiR/RomExt.ini", MedExtra & "\RomExt.ini", "anonymous", "anonymous", True, 1000, True)
            'My.Computer.Network.DownloadFile(UpdateServer & "/MedGuiR/ext", MedExtra & "DATs\ext", "anonymous", "anonymous", True, 1000, True)
            FTPDownloadFile(MedExtra & "\RomExt.ini", UpdateServer & "/MedGuiR/RomExt.ini", "anonymous", "anonymous")
            FTPDownloadFile(MedExtra & "DATs\ext", UpdateServer & "/MedGuiR/ext", "anonymous", "anonymous")
        Else
            MsgBox("RomExt.ini missing!, please download MedGui Reborn full package from Sourceforge", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "Essential file missing")
            Exit Sub
        End If
    End Sub

    Public Sub DetectMedGuiR()
        filepath = Application.StartupPath & "\MedGuiR.exe"
        MD5CalcFile()

        PurgeConfigBackup()

        Try
            If r_sha <> MGRH Then
                My.Computer.FileSystem.CopyFile(MedGuiR.TextBox4.Text & "\" & DMedConf & ".cfg", MedExtra & "Backup\" & Date.Today.ToString("ddMMyyyy") & "_ByUpdate.cfg", True)
                MsgBox("New MedGui Reborn version detected, i have created a backup to prevent corruption of " & DMedConf & ".cfg", vbOKOnly + MsgBoxStyle.Information, "Backup " & DMedConf & ".cfg...")
            End If
        Catch
        End Try
        MGRH = r_sha
    End Sub

    Public Sub PurgeConfigBackup()
        Dim today, past As Date
        Try
            today = Date.Today
            Threading.Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("it-IT")
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(MedExtra & "\Backup\", FileIO.SearchOption.SearchTopLevelOnly, "*.cfg*")
                If foundFile.Contains("_ByUpdate.cfg") Then
                    past = DateTime.ParseExact(Replace(Path.GetFileName(foundFile), "_ByUpdate.cfg", ""), "ddMMyyyy", Globalization.CultureInfo.CreateSpecificCulture("it-IT"))
                    If DateDiff(DateInterval.Day, past, today) > 2 Then
                        File.Delete(foundFile)
                    End If
                End If
            Next
        Catch ex As Exception
            MGRWriteLog("MedGuiR - PurgeConfigBackup: " & Date.Today.ToString & " " & ex.Message)
        End Try
    End Sub

End Module