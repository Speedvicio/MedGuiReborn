﻿Imports System.IO
Imports System.Net

Module GlobalVar

    Public Startup_Path, UCInick, UCIserver, UCIport, UCIchannel, vmedClear, MedShader, UpdateServer, MGRH,
    JUP, JDOWN, JLEFT, JRIGHT, JSTART, JSELECT, JA, JX, JY, JB, JL, JR, p_c, x864, DMedConf, SScart As String, forMax, stopiso, noftp, SMedClient, D_UCI, emu4crt As Boolean,
    TypeOS As String = UCase(My.Computer.Info.OSFullName)

    Public NewAPI As Boolean = True
    Public uWine As Boolean = False
    Public gIcon As Icon
    Public TypeTls As SecurityProtocolType

    Public Sub Startup_setting()
        GeneralRMIni()
        If MedGuiR.GridToolStripMenuItem.Checked = True Then GridRStyle()
        DirectoryRMIni()
        UCIRMini()
        RJoypadMini()
        GridRMIni()
        NetPlayMini()

        If MedGuiR.ComboBox1.Text = "" Then MedGuiR.ComboBox1.Text = "NoIntro"
        'If File.Exists(MedExtra & "Scanned\" & Startup_Path & ".csv") = False Then
        'MsgBox("..\Scanned\" & Startup_Path & ".csv missing" & vbCrLf &
        '      "Please rebuild it if necessary", MsgBoxStyle.OkOnly + vbExclamation, Startup_Path & ".csv missing")
        'Startup_Path = ""
        'End If

        If SMedClient = True Then
            'non scansionare
        ElseIf Startup_Path = "fav" Then
            MedGuiR.FavouritesToolStripButton.PerformClick()
        Else
            'MedGuiR.SY.SelectedItem = Startup_Path
            MedGuiR.ModuleToolStripComboBox2.SelectedItem = Startup_Path
        End If

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

        If File.Exists(MedExtra & "\DATs\genre.txt") Then
            MedGuiR.GENREToolStripMenuItem.Enabled = True
            MedGuiR.GENREToolStripMenuItem1.Enabled = True
            MedGuiR.GENREToolStripComboBox2.Items.AddRange(IO.File.ReadAllLines(MedExtra & "\DATs\genre.txt"))
            MedGuiR.GENREComboBox1.Items.AddRange(IO.File.ReadAllLines(MedExtra & "\DATs\genre.txt"))
        End If

        'If IO.File.Exists(MedExtra & "\NetPlay\mednafen-server.exe") = False Then MedGuiR.ServerToolStripButton.Enabled = False
        F1 = MedGuiR
        ColorizeForm()
    End Sub

    Public Sub Test_Server()
        Dim webexist As Boolean = False
        If My.Computer.Network.IsAvailable = True Then

            Try
                Dim request As Net.HttpWebRequest = DirectCast(Net.HttpWebRequest.Create("https://github.com/Speedvicio/MedGui_Resource/tree/main/MedGuiR"), Net.HttpWebRequest)
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

        End If

        If webexist = True Then
            UpdateServer = "https://raw.githubusercontent.com/Speedvicio/MedGui_Resource/main"
        Else
            UpdateServer = "ftp://anonymous@speedvicio.ddns.net"
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
            Dim plugin() As String
            plugin = New String(11) {"\SevenZipSharp.dll", "\MedGuiR\Plugins\Proxy7z.dll", "\IrcClient.dll", "\FTPclient.dll", "\DiscTools.dll",
                "\LinqBridge.dll", "\Newtonsoft.Json.dll", "\PeakMeterCtrl.dll", "\CoreAudioApi.dll", "\fmod.dll", "\NAudio.dll", "\MedGuiR\Plugins\7zPlugins\7z.dll"}

            For i = 0 To 11
                If File.Exists(Application.StartupPath & plugin(i)) = False Then
                    MissingResource(plugin(i).Substring(1))
                    MedGuiR.Close()
                    Threading.Thread.Sleep(2000)
                    Exit Sub
                End If
            Next

            If File.Exists(MedExtra & "FAQ\MedClientTutorial\Start.html") Then
                MedGuiR.Button57.Enabled = True
            End If

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
                            Message.Text = "Mednafen not found"
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

        Dim CountAttemp As Integer = 0

        tProcess = "mednafen"
        wDir = MedGuiR.TextBox4.Text
        Arg = "COPYING" 'Nothing
        StartProcess()

ReCheckConfig:

        CountAttemp += 1

        If ISON_Mednafen(150) = False Then
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
                Threading.Thread.Sleep(2000)
            End If
        Else
            If CountAttemp > 20 Then
                Dim mop As MsgBoxResult = MsgBox("Please close Mednafen emulation to open MedGui Reborn" & vbCrLf &
                             "Ok = Continue, Cancel = Close MedGuiR", MsgBoxStyle.Information + vbOKCancel, "Can't run MedGui Reborn...")

                If mop = vbCancel Then MedGuiR.ResetAll = True : MedGuiR.Close() : Exit Sub
            End If

            GoTo ReCheckConfig

        End If

        DetectMedGuiR()
        Detectx86_4()

        Dim readText() As String = File.ReadAllLines(MedGuiR.TextBox4.Text & "\" & DMedConf & ".cfg", System.Text.Encoding.UTF8)

        Dim vmedFull As String
        vmedFull = readText.GetValue(0)
        vmedFull = Replace(vmedFull, ";VERSION ", "")
        vmedClear = Replace(vmedFull, ".", "")
        vmedClear = Replace(vmedClear, "-UNSTABLE", "")

        If Len(vmedClear.Trim) < 5 Then vmedClear = vmedClear & "0"
        If Val(vmedClear) < 9380 And vmedClear <> "" Then
            Message.Text = "Mednafen outdated"
            Message.Label1.Text = "MedGui Reborn support only Mednafen >= 0.9.38" & vbCrLf &
        "Please download Last Mednafen version at:"
            Message.LinkLabel1.Text = "http://forum.fobby.net/index.php?t=thread&frm_id=19&"
            Message.ShowDialog()
            MedGuiR.Close()
        Else
            If detect_module("video.resolution_switch") = True Then
                emu4crt = True
            Else
                emu4crt = False
            End If
            vmedFull = Replace(vmedFull, "-UNSTABLE", "-U")
            Dim T_exe As String
            If emu4crt = True Then
                T_exe = "EMU4CRT"
            Else
                T_exe = "Mednafen"
            End If
            MedGuiR.Label8.Text = T_exe & " v." & vmedFull
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

    Public Function detect_module(parameter) As Boolean
        Dim cont As Integer = 0
        If Dir(MedGuiR.TextBox4.Text & "\" & DMedConf & ".cfg") <> "" Then
            Dim readText() As String = File.ReadAllLines(MedGuiR.TextBox4.Text & "\" & DMedConf & ".cfg")
            Dim s As String
            For Each s In readText
                If s.Contains(parameter) Then
                    cont = cont + 1
                    Exit For
                ElseIf s.Contains(parameter) = False Then
                    cont = 0
                End If
            Next

            If cont = 1 Then
                detect_module = True
            Else
                detect_module = False
            End If
        End If
    End Function

    Public Sub OS_Version()
        Try
            'Specifies the TLS 1.X security protocol.
            'Ssl3    48
            'SystemDefault   0
            'Tls     192
            'Tls11   768
            'Tls12   3072
            'Tls13   12288

            Select Case True
                Case TypeOS.Contains("XP")
                    TypeTls = 0
                Case TypeOS.Contains("7"), TypeOS.Contains("8"), TypeOS.Contains("10"), TypeOS.Contains("11")
                    TypeTls = 3072
                Case Else
                    TypeTls = 0
            End Select

            'Forced Tls12 security protocol because August net framework patch create problems with Discord connection
            ServicePointManager.SecurityProtocol = TypeTls

            If TypeOS.Contains("XP") Then
                Exit Sub
            Else
                Application.EnableVisualStyles()
            End If

            Dim p() As Process = Process.GetProcessesByName(“winlogon”, My.Computer.Name)
            If p.Length <= 0 Then
                uWine = True
                MedGuiR.CheckBox17.Checked = True
            End If
        Catch
        End Try
    End Sub

    Public Sub SingleScan()
        MedGuiR.TextBox3.Text = ""
        MedGuiR.FNameToolStripTextBox.Text = ""
        skipother = False
        stopscan = False
        stopiso = False
        Pismo()
        TempFolder = "RomTemp"
        type_csv = ""
        MedGuiR.Text = "MedGui Reborn"
        MedGuiR.TextBox1.Text = R_RelPath(percorso)
        Counter = 0
        get_ext()

        Try
            If Counter <= 0 And skipother = False Then '// controlla se skipother non combina casini in futuro
                scan.decript()
                MedGuiR.MainGrid.Rows(0).Cells(3).ToolTipText = "CRC " & base_file
            End If
        Catch ex As Exception
            MGRWriteLog("GlobalVar - SingleScan: " & ex.Message)
        End Try
        SoxStatus.Close()
        fileTXT = ""

        'MedGuiR.remove_double()
        'MedGuiR.Datagrid_filter()
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

            If UpdateServer.StartsWith("https://") Then
                My.Computer.Network.DownloadFile(UpdateServer & "/MedGuiR/RomExt.ini", MedExtra & "\RomExt.ini", "anonymous", "anonymous", True, 1000, True)
                My.Computer.Network.DownloadFile(UpdateServer & "/MedGuiR/ext", MedExtra & "DATs\ext", "anonymous", "anonymous", True, 1000, True)
            ElseIf UpdateServer.StartsWith("ftp://") Then
                FTPDownloadFile(MedExtra & "\RomExt.ini", UpdateServer & "/MedGuiR/RomExt.ini", "anonymous", "anonymous")
                FTPDownloadFile(MedExtra & "DATs\ext", UpdateServer & "/MedGuiR/ext", "anonymous", "anonymous")
            End If
        Else
            MsgBox("RomExt.ini missing!, please download MedGui Reborn full package from Sourceforge", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "Essential file missing")
            Exit Sub
        End If
    End Sub

    Public Sub DetectMedGuiR()
        filepath = Application.StartupPath & "\MedGuiR.exe"
        SHA1CalcFile()

        PurgeConfigBackup()

        Try
            If r_sha <> MGRH Then
                My.Computer.FileSystem.CopyFile(MedGuiR.TextBox4.Text & "\" & DMedConf & ".cfg", MedExtra & "Backup\" & Date.Today.ToString("ddMMyyyy") & "_ByUpdate.cfg", True)
                MsgBox("A new MedGui Reborn version was detected since the last launch." & vbCrLf & vbCrLf & "Backup of " & DMedConf & ".cfg was created in:" & vbCrLf & vbCrLf &
                MedExtra & "Backup\" & vbCrLf & vbCrLf &
                "in case of possible corruption.", vbOKOnly + MsgBoxStyle.Information, "Backup " & DMedConf & ".cfg...")
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