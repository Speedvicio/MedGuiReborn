﻿Imports System.IO
Imports System.Text.RegularExpressions

Public Class MedGuiR

    Public StartRom, romName, last_consoles, last_rom, LoadCD, tpce, multimedia, regioni, tempiso, Vjoypad, M3UDisk As String,
        ssetting, dwnboxm, SorF, label2index As Integer, SwSetting, AutoUp, ResetAll, missingame As Boolean

    Public FirstStart As Boolean = True
    Public HideUCIEz As Boolean = False
    Dim prevcrc As String
    Public deadPOV As String
    Dim countPOV As Integer
    Public FormIsON As Boolean

    Public tgdbCID As String

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TabControl1.DrawMode = TabDrawMode.OwnerDrawFixed
        checkpismo = False
        MainGrid.DoubleBuffered(True)

        gIcon = My.Resources.MedGuiR
        Me.Icon = gIcon

        'FirstStart = True
        InitJoy()
        MedExtra = Application.StartupPath & "\MedGuiR\"
        If File.Exists(Application.StartupPath & "\MGRerr.log") Then File.Delete(Application.StartupPath & "\MGRerr.log")

        DetectFolder()
        OS_Version()
        Startup_setting()
        exist_Mednafen()
        Read_Resource()

        If ResetAll = True Then Exit Sub

        MednafenV()

        If detect_module("snes_faust") = True Then
            CheckBox15.Visible = True
            CheckBox15.Enabled = True
        Else
            CheckBox15.Visible = False
            CheckBox15.Enabled = False
        End If

        contr_os()
        list_DATs()
        CustomScanFolder()
        'InitJoy()
        Me.StartPosition = FormStartPosition.CenterScreen
        If CheckBox20.Checked = True Then
            If My.Computer.Network.IsAvailable = False Then MsgBox("Connections is not Available", vbOKOnly + vbExclamation) : Exit Sub
            AutoUp = True
            get_update()
            DetectLastMednafen()
            AutoUp = False
        End If
        ParseCommandLineArgs()

        If ComboBox7.Text = "Speedvicio.dtdns.net" Then ComboBox7.Text = "speedvicio.ddns.net"

        SoxStatus.Close()
        FirstStart = False

        If SMedClient = True Then
            Me.WindowState = FormWindowState.Minimized
            MedClient.Show()
            MedClient.WindowState = FormWindowState.Maximized
        Else
            MainGrid.Focus()
        End If
    End Sub

    Private Sub ParseCommandLineArgs()
        Dim inputArgument As String '= "-file="
        Dim inputName As String = ""
        Dim pScanFolder As String = ""

        For Each s As String In Environment.GetCommandLineArgs
            Select Case True
                Case s.ToLower.Contains("-file=")
                    inputArgument = "-file="
                    inputName = s.Remove(0, inputArgument.Length)
                Case s.ToLower.Contains("-folder=")
                    inputArgument = "-folder="
                    pScanFolder = s.Remove(0, inputArgument.Length)
                Case Else
                    inputName = ""
                    pScanFolder = ""
            End Select
            'If s.ToLower.StartsWith(inputArgument) Then
            'inputName = s.Remove(0, inputArgument.Length)
            'End If
        Next

        If inputName.Trim <> "" Then
            SY.Text = ""
            percorso = R_RelPath(inputName)
            SingleScan()
            RealcdIsoName()
        End If

        If pScanFolder <> "" Then
            SY.Text = ""
            SY.Text = pScanFolder
        End If
        Environment.CommandLine.Remove(0, Environment.CommandLine.Length)

    End Sub

    Private Sub MedGuiR_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed

        If ResetAll = False Then RWIni()

        ClearFile()
        If noftp = False Then MedClient.CloseNetSession() : CleanLocalParsed() '
        If MedClient.NotifyIcon1.Visible = True Then MedClient.NotifyIcon1.Dispose()
    End Sub

    Private Sub DataGridView1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles MainGrid.CellEnter
        SelectRom()
    End Sub

    'Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
    'SelectRom()
    'End Sub

    Private Sub DataGridView1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MainGrid.MouseClick
        MainGrid.ContextMenuStrip = Nothing
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then

                If MainGrid.Rows.Count <= 0 Then Exit Sub

                If percorso IsNot Nothing = False Then
                        MsgBox("Missing game path", vbOKOnly + MsgBoxStyle.Critical, "missing path")
                        Exit Sub
                    End If

                    'verMednafen()
                    sender.ClearSelection()
                    Dim ht As DataGridView.HitTestInfo = sender.HitTest _
                (e.X, e.Y)
                    If ht.ColumnIndex <> -1 And ht.RowIndex <> -1 Then
                        sender.Item(ht.ColumnIndex, ht.RowIndex).Selected = True
                        MainGrid.CurrentCell = MainGrid.Item(ht.ColumnIndex, ht.RowIndex)
                    'SelectRom()

                    If ServerToolStripComboBox2.Text = "" Or ServerToolStripComboBox2.Items.Count <= 0 Then MgrSetting.ListServer_reload()

                    ParseMednafenConfig()

                    'inserisci #menu in questa linea
                    'If type_csv = "fav" Or type_csv = "last" Then
                    'RemoveFromFavoritesToolStripMenuItem.Enabled = True
                    'Else
                    'RemoveFromFavoritesToolStripMenuItem.Enabled = False
                    'End If

                    Select Case SY.Text
                            Case "ss", "psx", "pcfx", ""
                                ADVManageToolStripMenuItem.Enabled = False
                            Case Else
                                ADVManageToolStripMenuItem.Enabled = True
                        End Select

                        If type_csv <> "" Then RenameEntryStripMenuItem.Enabled = True Else RenameEntryStripMenuItem.Enabled = False

                        If R_RelPath(percorso).Trim <> "" Then
                            If File.Exists(R_RelPath(percorso) & ".ips") Then
                                RIPSToolStripMenuItem.Enabled = True
                            Else
                                RIPSToolStripMenuItem.Enabled = False
                            End If

                            If File.Exists(Path.GetDirectoryName(R_RelPath(percorso)) & "\" & Path.GetFileNameWithoutExtension(R_RelPath(percorso)) & ".sbi") Then
                                RSBIToolStripMenuItem.Enabled = True
                            Else
                                RSBIToolStripMenuItem.Enabled = False
                            End If
                        Else
                            RIPSToolStripMenuItem.Enabled = False
                            RSBIToolStripMenuItem.Enabled = False
                        End If

                        If File.Exists(MedExtra & "\Plugins\Controller\MedPad.exe") Then
                            MedPadToolStripMenuItem.Enabled = True
                        Else
                            MedPadToolStripMenuItem.Enabled = False
                        End If
                        MainGrid.ContextMenuStrip = AdvancedMenu
                        MainGrid.ContextMenuStrip.Show(Cursor.Position.X, Cursor.Position.Y)
                        'If last_consoles <> consoles Or Setting.Visible = False Then SwSetting = True : improm() : last_consoles = consoles
                    End If
                End If
        Catch
            MsgBox("A strange error occurred!" &
        vbCrLf & "Please select a game to open specific setting", MsgBoxStyle.Exclamation)
        End Try

    End Sub

    Private Sub AdvancedMenu_Click(sender As Object, e As EventArgs) Handles AdvancedMenu.Click
        'AdvancedMenu.Close()
    End Sub

    Private Sub AdvancedMenu_Closed(sender As Object, e As ToolStripDropDownClosedEventArgs) Handles AdvancedMenu.Closed
        MainGrid.ContextMenuStrip = Nothing
    End Sub

    Private Sub DataGridView1_MouseLeave(sender As Object, e As EventArgs) Handles MainGrid.MouseLeave
        'If DataGridView1.ContextMenuStrip IsNot Nothing Then DataGridView1.ContextMenuStrip.Close()
    End Sub

    Public Sub remove_tabs()

        For i = 8 To MgrSetting.TabControl1.TabPages.Count
            Select Case i
                Case 24, 28, 31
                Case Else
                    MgrSetting.TabControl1.TabPages.RemoveByKey("TabPage" & i)
            End Select
        Next i

    End Sub

    Public Sub SetSRom()

        Select Case SY.Text
            Case "def"
                StartRom = TextBox9.Text
            Case "apple2"
                StartRom = TextBox22.Text
            Case "gb"
                StartRom = TextBox7.Text
            Case "gba"
                StartRom = TextBox5.Text
            Case "gg"
                StartRom = TextBox14.Text
            Case "lynx"
                StartRom = TextBox8.Text
            Case "md"
                StartRom = TextBox13.Text
            Case "nes"
                StartRom = TextBox11.Text
            Case "ngp"
                StartRom = TextBox6.Text
            Case "pce"
                StartRom = TextBox10.Text
            Case "pcfx"
                StartRom = TextBox15.Text
            Case "psx"
                StartRom = TextBox18.Text
            Case "sms"
                StartRom = TextBox19.Text
            Case "snes"
                StartRom = TextBox17.Text
            Case "ss"
                StartRom = TextBox20.Text
            Case "vb"
                StartRom = TextBox12.Text
            Case "wswan"
                StartRom = TextBox16.Text
            Case ""
                StartRom = ""
        End Select
    End Sub

    Private Sub MedGuiR_Move(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Move
        ChaseR()
        ChaseL()
    End Sub

    Private Sub improm()
        MgrSetting.Close()
        real_name = MainGrid.CurrentRow.Cells(5).Value()
        ext = MainGrid.CurrentRow.Cells(7).Value()

        ReadPSValue()

        MgrSetting.Show()
        If SwSetting = True Then
            remove_tabs()
            MgrSetting.add_tabs()
            Dim ExtraText As String
            SetSpecialModule()

            If tpce <> "" Then
                ExtraText = Replace(tpce, "_", "").ToUpper & " "
            Else
                ExtraText = ""
            End If

            MgrSetting.Text = ExtraText & MainGrid.CurrentRow.Cells(5).Value()
        Else
            MgrSetting.Text = "Global Console Setting"
        End If
    End Sub

    Public Sub StartEmu()
        Try
            Pismo()
            If ErLog.Visible = True Then ErLog.Close()

            If File.Exists(TextBox4.Text & "\mednafen.exe") = False Then
                MsgBox("Can't find mednafen.exe, please reselect it in general tab", vbOKOnly + vbCritical, "Missing mednafen.exe")
                TabControl1.SelectedTab = TabPage2
                TextBox4.Focus()
                Exit Sub
            End If

            verMednafen()
            percorso = R_RelPath(TextBox1.Text)
            If ssetting = 0 Then percorso = ""

            Select Case LCase(Path.GetExtension(R_RelPath(percorso)))
                Case ".zip"
                    Dim existMAI As Boolean = False
                    Dim szip As SevenZip.SevenZipExtractor = New SevenZip.SevenZipExtractor(R_RelPath(percorso))
                    If szip.ArchiveFileData.Count > 1 Then
                        For Each ArchiveFileInfo In szip.ArchiveFileData
                            Select Case LCase(Path.GetExtension(ArchiveFileInfo.FileName))
                                Case ".bin"
                                    If Regex.IsMatch(ArchiveFileInfo.FileName, ".*([\d]+).*") = True And ArchiveFileInfo.FileName.Contains("ic") Then
                                        existMAI = True
                                        Exit For
                                    End If
                                Case ".mai", ".21", ".30", ".31", ".sd0", ".1", ".2", ".3", ".4", ".5", ".6", ".7", ".ic8", ".u1", ".ic13", ".nv"
                                    existMAI = True
                                    Exit For
                            End Select
                        Next
                        If existMAI = False Then simple_extract()
                    End If
                Case ".rar", ".7z"
                    simple_extract()
                Case ".rsn"
                    simple_extract()
                    PopulateChip()
                    Exit Sub
                Case ".vgm", ".vgz"
                    scan.VGtoBIN()
                Case ".gbs"
                    scan.GBS2GB()
                Case ".iso", ".bin"
                    'If percorso.Contains(".bin.ecm") Then TextBox1.Text = Replace(percorso, "bin.", "")
                    If Dir(Replace(R_RelPath(TextBox1.Text), dettaglio.Extension, ".cue")) <> "" And dettaglio.Length > 10485760 Then
                        percorso = Replace(R_RelPath(TextBox1.Text), dettaglio.Extension, ".cue")
                        If Label34.Text <> "" Then Label34.Text = ""
                    End If
                Case ".cfs"
                    Dim Grow As Integer = MainGrid.Rows.Count
                    If checkpismo = True Then
                        MountPismo()
                        RecuScan()
                    End If
                    If Grow < MainGrid.Rows.Count Then
                        MainGrid.Rows.RemoveAt(MainGrid.Rows.Count - 1)
                    End If
                Case Is <> ".spc", ".rsn"
                    SoundList.Close()
            End Select

            consoles = MainGrid.CurrentRow.Cells(6).Value()

            If consoles = "psx" Then Sbi_Scan()

            If ssetting = 0 Then percorso = " "
            tProcess = "mednafen"
            wDir = TextBox4.Text

            If TextBox2.Text.Trim <> "" And TextBox2.Text.Contains("-") Then
                custom = " " & TextBox2.Text.Trim
            Else
                custom = Nothing
            End If

            'Enable NoDesync option
            Dim net As String
            pargMT = ""

            SetSpecialModule()

            Dim extramsg As String = ""
            If real_name = "Sega Saturn" Then
                extramsg = "This time you can listen only good music from your game"
                consoles = "cdplay"
            End If

            If consoles = "ss" Then
                If c_os = "32" Then
                    MsgBox(real_name & " emulation is supported only on Windows 64 bit version" & vbCrLf &
                      extramsg, vbOKOnly + vbInformation, real_name & " emulation not supported...")
                    If extramsg = "" Then Exit Sub
                End If

                If c_os = "64" And Label57.Text = "x86" Then
                    MsgBox("You are running Mednafen 32 bit version on  Windows 64 bit version" & vbCrLf &
       real_name & " emulation is supported only by Mednafen 64 bit version" & vbCrLf &
       "Please upgrade your Mednafen to a 64 bit version", vbOKOnly + vbInformation, real_name & " emulation not supported...")
                    TabControl1.SelectedTab = TabPage2
                End If
            End If

            If consoles = "psx" And ssetting = 1 Then RestoreMCR()
            If consoles = "gba" Then GBAMemory()

            If File.Exists(TextBox4.Text & "\" & consoles & tpce & ".cfg") Then Read_Desync() Else contdes = 3
            If NetToolStripButton.BackColor = Color.Red And ssetting <> 0 Then

                If consoles = "psx" Then BackupMCR()

                QuestionMultitap()
                net = " -connect" & pargMT
                If contdes = 0 Then My.Computer.FileSystem.MoveFile(TextBox4.Text & "\" & consoles & tpce & ".cfg", MedExtra & "\NoDesync\Backup\" & consoles & tpce & ".cfg")
                If File.Exists(MedExtra & "\NoDesync\" & consoles & tpce & ".cfg") Then
                    Dim wjoy As String = ""
                    'If consoles = "psx" Then
                    'wjoy = MsgBox("Set NoDesync with all 8 joypad to dualshock?", vbYesNo + vbInformation, "Set with dualshock")
                    'If wjoy = vbYes Then wjoy = "J" Else wjoy = ""
                    'End If

                    'If consoles = "nes" Then
                    'wjoy = MsgBox("Have you enabled 4-player or partytap Adapter?", vbYesNo + vbInformation, "Set with 4-player/partytap Adapter")
                    'If wjoy = vbYes Then wjoy = "M" Else wjoy = ""
                    'End If

                    File.Copy(MedExtra & "\NoDesync\" & consoles & tpce & wjoy & ".cfg", TextBox4.Text & "\" & consoles & tpce & ".cfg", True)
                End If
            Else
                net = Nothing
                RebuilDesync()
            End If

            If LoadCD = "" And MainGrid.SelectedRows.Count <> 0 Then consoles = MainGrid.CurrentRow.Cells(6).Value()

            If Len(R_RelPath(TextBox1.Text)) >= 3 Then LoadCD = "" Else percorso = "\\.\" & R_RelPath(percorso)
            LoadCD = Nothing
            'If TextBox1.Text.Contains("\\.\*") Then  Else  : LoadCD = ""

            Dim skipm3u As Boolean = False
            If MgrSetting.NoCheck = False Then
                skipm3u = False
            Else
                skipm3u = True
            End If

            If skipm3u = False Then
                If MainGrid.CurrentRow.Cells(7).Value() = ".m3u" And M3UDisk = Nothing Then
                    M3UDisk = InputBox("Input the disk that you want to load from 1 to " & System.IO.File.ReadAllLines(R_RelPath(TextBox1.Text)).Length, "Select a CD", "1")
                    If M3UDisk = "" Then Exit Sub
                    M3UDisk = " -which_medium " & (M3UDisk - 1)
                End If
            End If

            Dim Fmodule As String = Nothing

            If consoles = "generic" And Val(vmedClear) > 12710 Then
                Fmodule = Nothing
            Else
                Fmodule = " -force_module " & consoles & tpce
            End If

            Dim OVconfig As String = Nothing

            If CheckBox26.Checked = True And File.Exists(TextBox26.Text) Then
                OVconfig = " -ovconfig " & Chr(34) & (TextBox26.Text) & Chr(34)
            End If

            Arg = pArg & net & custom & LoadCD & Fmodule & OVconfig & M3UDisk & " " & Chr(34) & R_RelPath(percorso) & Chr(34)

            'VerifyPerSystem()

            StartProcess()

            If MgrSetting.NoCheck = True Then MgrSetting.NoCheck = False : Exit Sub
            Threading.Thread.Sleep(2000)

            TimerControlMednafen.Enabled = True
            TimerControlMednafen.Start()

            M3UDisk = Nothing
        Catch ex As Exception
            MsgBox(ex.ToString, vbCritical)
        End Try
    End Sub

    Private Sub VerifyPerSystem()
        Dim sucamilla As String = ""
        If File.Exists(Path.Combine(ExtractPath("path_pgconfig"), Path.GetFileNameWithoutExtension(R_RelPath(TextBox1.Text)) & "." & p_c & ".cfg")) = True Then
            sucamilla = Path.Combine(ExtractPath("path_pgconfig"), Path.GetFileNameWithoutExtension(R_RelPath(TextBox1.Text)) & "." & p_c)
        ElseIf File.Exists(TextBox4.Text & "\" & p_c & ".cfg") = True Then
            sucamilla = p_c
        End If

        If sucamilla <> "" Then
            MgrSetting.TPerC = True
            File.Copy(TextBox4.Text & "\" & DMedConf & ".cfg", TextBox4.Text & "\back_" & DMedConf & ".cfg", True)
            File.Copy(TextBox4.Text & "\" & sucamilla & ".cfg", TextBox4.Text & "\" & DMedConf & ".cfg", True)
        End If
    End Sub

    Public Sub SetSpecialModule()

        Select Case consoles
            Case "pce"
                If CheckBox1.Checked = True Then tpce = "_fast" Else tpce = Nothing
            Case "snes"
                If CheckBox15.Checked = True Then tpce = "_faust" : SnesSpecialChip() Else tpce = Nothing
            Case Else
                tpce = Nothing
        End Select

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)
        StartStatic_emu()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Try
            ' System.Diagnostics.Process.Start(TextBox4.Text & "\stdout.txt")
            _link = (TextBox4.Text & "\stdout.txt")
            open_link()
        Catch ex As Exception
            MsgBox("No stdout.txt file found", MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub SelectRom()

        missingame = False

        If File.Exists(MainGrid.CurrentRow.Cells(4).Value()) Then
        Else
            Dim RMiss = MsgBox("Missing game" & vbCrLf &
         "Press Del or Canc key to remove from the Games list.",
         MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation)
            missingame = True
            Exit Sub
        End If

        'If last_rom = DataGridView1.CurrentRow.Cells(4).Value() Then Exit Sub

        Try
            TextBox1.Text = R_RelPath(MainGrid.CurrentRow.Cells(4).Value())
            romName = Trim(MainGrid.CurrentRow.Cells(0).Value())
            percorso = R_RelPath(TextBox1.Text)
            last_rom = R_RelPath(TextBox1.Text)
            prevcrc = MainGrid.CurrentRow.Cells(8).Value()
            Specific_Info()
            consoles = MainGrid.CurrentRow.Cells(6).Value()
        Catch ex As Exception
            MGRWriteLog("MedGuiR - SelectRom: " & Date.Today.ToString & " " & ex.Message)
        End Try

        If prevcrc = MainGrid.CurrentRow.Cells(8).Value() Then Exit Sub
        If CheckBox2.Checked = True Then
            Try
                Dim dimBA As New System.IO.FileInfo(MedExtra & "BoxArt\" & MainGrid.CurrentRow.Cells(5).Value() & "\" & rn & ".png")
                Console.WriteLine(dimBA.Exists)
                Dim dimension As Integer
                dimension = (Decimal.Round(dimBA.Length.ToString) / 1024)

                If dimension <= 12 Then
                    DownloadCover()
                End If
            Catch ex As Exception
                DownloadCover()
            End Try
        End If

    End Sub

    Private Sub RemoveRow()
        Try
            MainGrid.Rows.Remove(MainGrid.CurrentRow)
            SaveGridDataInFile()
            If My.Computer.FileSystem.GetFileInfo(MedExtra & "Scanned\" & type_csv & ".csv").Length = 0 Then
                System.IO.File.Delete(MedExtra & "Scanned\" & type_csv & ".csv")
            End If
        Catch es As Exception
            MsgBox(es.ToString, MsgBoxStyle.Critical)
        End Try
        missingame = False
    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles MainGrid.KeyUp
        If MainGrid.CurrentRow Is Nothing Then Exit Sub
        If FormIsON = False Then Exit Sub
        Select Case e.KeyCode
            Case Keys.Enter
                StartStatic_emu()
            Case Keys.Cancel, Keys.Delete
                If type_csv = "fav" Or type_csv = "last" Or missingame = True Then
                    RemoveRow()
                End If
            Case Keys.ShiftKey
                RebuilDesync()
                If last_consoles <> consoles Or MgrSetting.Visible = False Then SwSetting = True : improm() : last_consoles = consoles
            Case Keys.F
                type_csv = "fav"
                SaveGridDataInRow()
            Case Keys.S
                CreateShortcut()
        End Select
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles MainGrid.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter, Keys.Cancel, Keys.Delete, Keys.F, Keys.S
                e.SuppressKeyPress = True
        End Select
    End Sub

    Public Sub StartStatic_emu()
        If TextBox1.Text = "" Then MsgBox("Please Select a valid file", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, "No rom selected") : Exit Sub
        ssetting = 1
        rec()
        pArg = record
        StartEmu()
        If last_rom <> "" Then TextBox1.Text = R_RelPath(last_rom)
    End Sub

    Private Sub DataGridView1_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles MainGrid.CellContentDoubleClick
        ssetting = 1
        rec()
        pArg = record
        StartEmu()
        TextBox1.Text = R_RelPath(last_rom)
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        If Button11.BackColor = SystemColors.Control Then
            Button11.BackColor = Color.Red
            Button12.BackColor = SystemColors.Control
        Else
            Button11.BackColor = SystemColors.Control
            verMednafen()
        End If
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        If Button12.BackColor = SystemColors.Control Then
            Button12.BackColor = Color.Red
            Button11.BackColor = SystemColors.Control
        Else
            Button12.BackColor = SystemColors.Control
            verMednafen()
        End If
    End Sub

    Private Sub rec()
        'Record
        Dim mrec, srec As String

        If Button11.BackColor = Color.Red Then multimedia = Chr(34) & MedExtra & "Media\Audio\" & MainGrid.CurrentRow.Cells(5).Value() & "\" & romName & ".wav" & Chr(34) : ver_rec() : srec = " -soundrecord " & Chr(34) & MedExtra & "Media\Audio\" & MainGrid.CurrentRow.Cells(5).Value() & "\" & romName & ".wav" & Chr(34) Else srec = ""
        If Button12.BackColor = Color.Red Then multimedia = Chr(34) & MedExtra & "Media\Movie\" & MainGrid.CurrentRow.Cells(5).Value() & "\" & romName & ".mov" & Chr(34) : ver_rec() : mrec = " -qtrecord " & Chr(34) & MedExtra & "Media\Movie\" & MainGrid.CurrentRow.Cells(5).Value() & "\" & romName & ".mov" & Chr(34) Else mrec = ""

        '" -qtrecord.vcodec " & MgrSetting.ComboBox4.Text & " -qtrecord.h_double_threshold " & MgrSetting.TrackBar5.Value & " -qtrecord.w_double_threshold " & MgrSetting.TrackBar4.Value &

        record = mrec & srec
        If Button11.BackColor = Color.FromKnownColor(KnownColor.Transparent) And Button12.BackColor = Color.FromKnownColor(KnownColor.Transparent) Then record = Nothing
    End Sub

    Private Sub ver_rec()
        Dim ow_file
        If IO.File.Exists(multimedia) = True Then
            ow_file = MsgBox("File already exist, do you want to overwrite it?", MsgBoxStyle.YesNoCancel + MsgBoxStyle.Exclamation)
            If ow_file = vbYes Then
                System.IO.File.Delete(multimedia)
            ElseIf ow_file = vbNo Then
                Dim rn_file
                rn_file = InputBox("Select a new name for multimedia file", , romName & "_")
                If rn_file = "" Then Exit Sub
                romName = rn_file
            ElseIf ow_file = vbCancel Then
            End If

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MainGrid.SelectionChanged
        Try
            record = ""
            'SelectRom()
            'ReadPSValue()
            consoles = MainGrid.CurrentRow.Cells(6).Value()

            If CheckBox10.Checked = True Then TextBox35.Text = rn
            If CheckBox10.Checked = True And CheckBox11.Checked = True Then TextBox35.Text = rn & " " & MainGrid.CurrentRow.Cells(2).Value()

            Select Case MainGrid.CurrentRow.Cells(7).Value()
                Case ".wsr", ".psf", ".psf1", ".minipsf", ".gsf", ".minigsf", ".hes", ".nsf", ".spc", ".rsn", ".vgz", ".vgm", ".gbs", ".ssf", ".minissf"
                    MainGrid.CurrentRow.Cells(2).Value() = "(Soundtrack)"
                    Label3.Text = "Version: " & MainGrid.CurrentRow.Cells(2).Value()
                    DetectChipmodule()
                    If AllTags <> "" Then MainGrid.CurrentRow.Cells(0).ToolTipText = AllTags : ChipTAG.RichTextBox1.Text = AllTags
            End Select
        Catch
        End Try
    End Sub

    Public Sub remove_double()
        If MainGrid.RowCount > 1 Then

            'For i = 1 To DataGridView1.RowCount - 1
            'If DataGridView1.Rows(i - 1).Cells(4).Value = DataGridView1.Rows(i).Cells(4).Value Then
            'DataGridView1.Rows.RemoveAt(i)
            ' End If
            'Next
            Try
                For i = 0 To MainGrid.RowCount - 1
                    If i > MainGrid.RowCount Then
                        i = MainGrid.RowCount - 1
                        If MainGrid.Rows(i).Cells(6).Value = "" Then
                            MainGrid.Rows.RemoveAt(i)
                        End If
                        Exit Sub
                    Else
                        If MainGrid.Rows(i).Cells(6).Value = "" Then
                            MainGrid.Rows.RemoveAt(i)
                        End If
                    End If
                Next
            Catch
            End Try
        End If
    End Sub

    Public Sub ScanFolder()
        If Directory.Exists(T_MedExtra & TempFolder) = False Then
            If SY.Text.Trim <> "" Then MsgBox("Nothing to Scan...", MsgBoxStyle.Exclamation + vbOKOnly, "Directory not exist...")
            MainGrid.Rows.Clear()
            Exit Sub
        End If

        Dim fcount As Integer = Directory.GetFiles(T_MedExtra & TempFolder & "\", "*.*", SearchOption.AllDirectories).Length
        If fcount = 0 Then
            MsgBox("No files founded in " & TempFolder & " Folder...", vbInformation + vbOKOnly)
            MainGrid.Rows.Clear()
            TempFolder = ""
            Exit Sub
        End If
        TextBox3.Text = ""
        FNameToolStripTextBox.Text = ""

        SevenZCounter = 0
        stopscan = False
        stopiso = True
        'controllo cfs implementato dopo release 0.123 vedi se crea casini
        'MainGrid.Rows.Clear()
        scansiona()
        SoxStatus.Close()
    End Sub

    Private Sub WORLDToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WORLDToolStripMenuItem1.Click
        FlagToolStripSplitButton.Image = (New Bitmap(MedExtra & "Resource\Flags\world.png"))
        FilterToolStripMenuItem.Image = FlagToolStripSplitButton.Image
        regioni = ""
        SearchGridGenreInRow()
    End Sub

    Private Sub EUToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EUToolStripMenuItem1.Click
        FlagToolStripSplitButton.Image = (New Bitmap(MedExtra & "Resource\Flags\eu.png"))
        FilterToolStripMenuItem.Image = FlagToolStripSplitButton.Image
        regioni = "eu"
        SearchGridGenreInRow()
    End Sub

    Private Sub USToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles USToolStripMenuItem1.Click
        FlagToolStripSplitButton.Image = (New Bitmap(MedExtra & "Resource\Flags\us.png"))
        FilterToolStripMenuItem.Image = FlagToolStripSplitButton.Image
        regioni = "us"
        SearchGridGenreInRow()
    End Sub

    Private Sub JPToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles JPToolStripMenuItem1.Click
        FlagToolStripSplitButton.Image = (New Bitmap(MedExtra & "Resource\Flags\jp.png"))
        FilterToolStripMenuItem.Image = FlagToolStripSplitButton.Image
        regioni = "ja"
        SearchGridGenreInRow()
    End Sub

    Private Sub PDToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PDToolStripMenuItem1.Click
        FlagToolStripSplitButton.Image = (New Bitmap(MedExtra & "Resource\Flags\pd.png"))
        FilterToolStripMenuItem.Image = FlagToolStripSplitButton.Image
        regioni = "pd"
        SearchGridGenreInRow()
    End Sub

    Private Sub MUSICToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MUSICToolStripMenuItem1.Click
        FlagToolStripSplitButton.Image = (New Bitmap(MedExtra & "Resource\Gui\modland.png"))
        FilterToolStripMenuItem.Image = FlagToolStripSplitButton.Image
        regioni = "soundtrack"
        SearchGridGenreInRow()
    End Sub

    Private Sub ToolStripComboBox1_SelectedIndexChanged1(sender As Object, e As EventArgs) Handles SY.SelectedIndexChanged
        ModuleToolStripComboBox2.Text = SY.Text
        ChangeModule()
        Startup_Path = SY.Text
    End Sub

    Private Sub ChangeModule()
        If type_csv = SY.Text Then Exit Sub
        If SY.Text = "" Then 'Or SY.Text = "psx" Or SY.Text = "ss" Or SY.Text = "pcfx"
            Me.Text = "MedGui Reborn"
            MainGrid.Rows.Clear()
            Datagrid_filter()
            RebuildToolStripButton.Enabled = False
            RescanToolStripMenuItem.Enabled = False
            type_csv = SY.Text
            Exit Sub
        Else
            RebuildToolStripButton.Enabled = True
            RescanToolStripMenuItem.Enabled = True
        End If
        Select_system()
        type_csv = SY.Text
        Datagrid_filter()
        Purge_Grid()
        TempFolder = ""

    End Sub

    Private Sub RebuildToolStripButton_Click(sender As Object, e As EventArgs) Handles RebuildToolStripButton.Click
        SetSRom()

        If StartRom.Trim = "" Or Directory.Exists(StartRom.Trim) = False Then
            MsgBox("You have set a empty or wrong path on " & UCase(SY.Text) & " MedGuiR Rom Path section", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "Path error...")
            Select Case SY.Text
                Case "def", "apple2", "lynx", "gb", "gba", "ngp", "nes", "pce", "gg", "md"
                    TabControl1.SelectedTab = TabPage3
                Case "sms", "snes", "vb", "wswan", "ss", "psx"
                    TabControl1.SelectedTab = TabPage4
            End Select
            Exit Sub
        End If

        Dim risp As String
        risp = MsgBox("Do you want To Rebuild " & UCase(SY.Text) & ".CSV?", vbInformation + vbOKCancel)
        If risp = vbCancel Then Exit Sub
        Me.Text = "MedGui Reborn"
        TextBox3.Text = ""
        SetSRom()
        TempFolder = StartRom

        MainGrid.Rows.Clear()

        If CheckBox14.Checked = True Then
            RecuScan()
        Else
            ScanFolder()
        End If

        SaveGridDataInFile()
        Datagrid_filter()
        ReleaseMemory()

    End Sub

    Private Sub LoadCDToolStripButton_Click(sender As Object, e As EventArgs) Handles LoadCDToolStripButton.Click
        IsoSelector.CheckBox1.Enabled = True
        IsoSelector.UNI.Enabled = True
        IsoSelector.CheckBox1.Checked = True

        Dim DTL As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Disc Soft")
        Dim DTLxp As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\DT Soft")

        If DTL Is Nothing And DTLxp Is Nothing Then
        Else
            IsoSelector.Button11.Enabled = True
        End If

        IsoSelector.ShowDialog()
    End Sub

    Private Sub FavouritesToolStripButton_Click(sender As Object, e As EventArgs) Handles FavouritesToolStripButton.Click
        Dim old_type_csv = type_csv
        If Me.Text.Contains("MedGui Reborn - Favorites Roms") Then
            Me.Text = "MedGui Reborn"
            Select_system()
        Else
            type_csv = "fav"
            RebuildToolStripButton.Enabled = False
            If Dir(MedExtra & "Scanned\" & type_csv & ".csv") = "" Then
                MsgBox("There Is no list With your favorites roms.", vbInformation + vbOKOnly)
                type_csv = old_type_csv
                If Me.Text <> "MedGui Reborn - Recent Roms" And SY.Text <> "" Then RebuildToolStripButton.Enabled = True
            Else
                Me.Text = "MedGui Reborn - Favorites Roms"
                RebuildToolStripButton.Enabled = False
                MainGrid.Rows.Clear()
                LoadGridDataInFile()
                Datagrid_filter()
            End If

            T_MedExtra = MedExtra
            Purge_Grid()
        End If

    End Sub

    Private Sub LoadRomToolStripButton_Click(sender As Object, e As EventArgs) Handles LoadRomToolStripButton.Click
        Dim fdlg As OpenFileDialog = New OpenFileDialog()
        SetSRom()
        Dim ssr As String = StartRom
        If StartRom = "" Then StartRom = Application.StartupPath
        fdlg.Title = "Select rom"
        fdlg.InitialDirectory = ssr
        fdlg.Filter = "All supported format (*.zip,*.7z,*.rar,*.zst,*.cue,*.toc,*.m3u,*.ccd,*.cfs,*.ciso,*.iso,*.ecm,*.pbp,*.chd,*.mai)|*.zip;*.7z;*.rar;*.zst;*.cue;*.toc;*.m3u;*.ccd;*.cfs;*.ciso;*.iso;*.ecm;*.pbp;*.chd|Virtual image (*.cue,*.toc,*.m3u,*.ccd)|*.cue;*.toc;*.m3u;*.ccd|Compressed archive (*.zip,*.7z,*.rar,*.zst)|*.zip;*.7z;*.rar;*.zst|Compressed Virtual Image (*.ecm,*.pbp,*.cfs,*.ciso,*.chd)|*.ecm;*.pbp;*.cfs;*.ciso;*.chd|File MAI (*.mai)|*.mai|PS-X EXE (*.exe)|*.exe|All files (*.*)|*.*"
        fdlg.FilterIndex = 1
        fdlg.RestoreDirectory = True
        If fdlg.ShowDialog() = DialogResult.OK Then
            If MainGrid.Rows.Count > 0 Then MainGrid.Rows.Clear()
            percorso = R_RelPath(fdlg.FileName)
            SevenZCounter = 0
            SingleScan()
        Else
            Exit Sub
        End If
        SevenZCounter = 0

        If ext <> ".m3u" Then RealcdIsoName()

        stopscan = False
        Datagrid_filter()

        Try
            Me.MainGrid.CurrentCell = Me.MainGrid(1, 0)
        Catch
        End Try
    End Sub

    Private Sub SettingToolStripButton_Click(sender As Object, e As EventArgs) Handles ModLandToolStripButton.Click
        ModLand.Show()
        ModLand.TextBox1.Focus()
    End Sub

    Private Sub NetToolStripButton_Click(sender As Object, e As EventArgs) Handles ServerToolStripButton.Click
        ex_Server()
        MednafenServer()
    End Sub

    Private Sub FindToolStripButton_Click(sender As Object, e As EventArgs) Handles FindToolStripButton.Click
        SearchGridGenreInRow()
        Datagrid_filter()
        TextBox3.Focus()
        TextBox3.SelectionStart = TextBox3.Text.Length + 1
    End Sub

    Private Sub IRCToolStripButton_Click(sender As Object, e As EventArgs) Handles IRCToolStripButton.Click
        Try
            HideUCIEz = True
            UCI.Show()
        Catch
        End Try
    End Sub

    Private Sub TextBox3_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox3.KeyUp
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            SearchGridGenreInRow()
            Datagrid_filter()
            TextBox3.Focus()
            TextBox3.SelectionStart = TextBox3.Text.Length + 1
        End If
    End Sub

    Private Sub FoldeRomToolStripButton_Click(sender As Object, e As EventArgs) Handles FoldeRomToolStripButton.Click
        stopscan = False
        ScanFolderRom()
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        If TextBox3.Text.Trim = "" Then
            SearchGridGenreInRow()
            Datagrid_filter()
        End If
    End Sub

    Private Sub Button44_Click_1(sender As Object, e As EventArgs) Handles Button44.Click
        MDM.ShowDialog()
    End Sub

    Private Sub LinkLabel13_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel13.LinkClicked
        _link = "https://mednafen.github.io/"
        open_link()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        _link = "https://datomatic.no-intro.org/?page=news"
        open_link()
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        _link = "https://gamehacking.org/"
        open_link()
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        _link = "https://github.com/speedvicio"
        open_link()
    End Sub

    Private Sub LinkLabel4_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel4.LinkClicked
        _link = "https://sox.sourceforge.net/"
        open_link()
    End Sub

    Private Sub LinkLabel5_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel5.LinkClicked
        _link = "https://forum.fobby.net/index.php?t=tree&th=924&start=0&"
        open_link()
    End Sub

    Private Sub LinkLabel6_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        _link = "https://emumovies.com/forums/index.php/page/portal"
        open_link()
    End Sub

    Private Sub LinkLabel7_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        _link = "https://tech.reboot.pro/showthread.php?tid=1706"
        open_link()
    End Sub

    Private Sub LinkLabel9_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        _link = "https://www.mednafen-it.org/"
        open_link()
    End Sub

    Private Sub LinkLabel12_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel12.LinkClicked
        _link = "https://notepad-plus-plus.org"
        open_link()
    End Sub

    Private Sub LinkLabel11_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel11.LinkClicked
        _link = "https://www.imgburn.com"
        open_link()
    End Sub

    Private Sub LinkLabel8_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel8.LinkClicked
        Try
            _link = (Application.StartupPath & "\HOW-TO r1.txt")
            open_link()
        Catch ex As Exception
            MsgBox("No MedGui Reborn Guide Detected", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub LinkLabel14_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel14.LinkClicked
        _link = UpdateServer & "/MedGuiR/LazyAss.zip"
        open_link()
    End Sub

    Private Sub LinkLabel10_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel10.LinkClicked
        Try
            If CheckBox17.Checked = False Then
                Process.Start(Chr(34) & TextBox4.Text & "\Documentation\mednafen.html" & Chr(34))
            Else
                MedBrowser.Show()
                MedBrowser.WebBrowser1.Navigate(TextBox4.Text & "\Documentation\mednafen.html")
            End If
        Catch ex As Exception
            MsgBox("No Mednafen Help Detected", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub Button42_Click(sender As Object, e As EventArgs) Handles Button42.Click
        select_link()
        If WS.Text <> "Mednafen Bios" Then open_link()
    End Sub

    Private Sub Button29_Click(sender As Object, e As EventArgs)
        Convert_cue()
    End Sub

    Private Sub Button31_Click(sender As Object, e As EventArgs) Handles Button31.Click
        typeSTR()
        isof.Title = "Select a " & tiso & " file"
        isof.Filter = tiso1
        isof.RestoreDirectory = True
        If isof.ShowDialog() = DialogResult.OK Then
            percorso = R_RelPath(isof.FileName)
            Label34.Text = R_RelPath(percorso)
            biso = Path.GetFileName(Label34.Text)
            tempiso = Replace(Label34.Text, Path.GetExtension(Label34.Text), "." & miso)
        End If
    End Sub

    Private Sub Button30_Click(sender As Object, e As EventArgs) Handles Button30.Click
        typeSTR()
        strimg = ""
        isof.Filter = tiso1
        isof.RestoreDirectory = True

        For i = 1 To NumericUpDown1.Value
            isof.Title = "Select " & tiso & " file n°" & i
            If isof.ShowDialog() = DialogResult.OK Then
                Label34.Text = ""

                If i = 1 Then
                    strimg = isof.SafeFileName
                Else
                    If strimg.Contains(isof.SafeFileName) Then
                        MsgBox("You have selected again " & isof.SafeFileName & vbCrLf &
                               "Select the correct CD " & i, MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, "Duplicate selection...")
                        i = i - 1
                    Else
                        strimg = strimg & vbCrLf & isof.SafeFileName
                    End If
                End If
                If i = NumericUpDown1.Value Then
                    tempiso = InputBox("Select a Name for M3U File", "Put a M3U Name (Default: Selected M3U Name)", Replace(isof.SafeFileName, Path.GetExtension(isof.SafeFileName), ""))
                    If tempiso = "" Then strimg = "" : Exit Sub
                    tempiso = Path.GetDirectoryName(isof.FileName) & "\" & tempiso & ".m3u"
                End If
            Else
                strimg = ""
                tempiso = ""
                Exit Sub
            End If
        Next
        Button32.PerformClick()
    End Sub

    Private Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click
        If tempiso = "" Then MsgBox("Please choose first the CD image file!", vbExclamation + MsgBoxStyle.OkOnly) : Exit Sub
        typeSTR()

        If RadioButton1.Checked = True Then
            stopscan = False
            Make_Temp_CUE()
        ElseIf RadioButton2.Checked = True Or RadioButton3.Checked = True Then
            Make_CUE()
        End If

    End Sub

    Public Sub Make_CUE()
        Dim fiso As StreamWriter
        fiso = File.CreateText(tempiso)
        fiso.WriteLine(strimg)
        fiso.Flush()
        fiso.Dispose()
        fiso.Close()
        MsgBox(UCase(miso) & " Created!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
        If consoles = "psx" Then percorso = R_RelPath(tempiso) : Change_PSX()
        tempiso = ""
    End Sub

    Public Sub yPath()
        DeFolder.Description = rDes
        If DeFolder.ShowDialog() = DialogResult.OK Then rPath = DeFolder.SelectedPath Else rPath = ""
        Exit Sub
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        rDes = "Select Mednafen Path"
        yPath()
        If rPath <> "" Then TextBox4.Text = rPath : exist_Mednafen() :
        MednafenV()
    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        If Dir(TextBox4.Text & "\" & DMedConf & ".cfg") = "" Then
            MsgBox("No " & DMedConf & ".cfg Available.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Backup Failed")
            Exit Sub
        Else
            My.Computer.FileSystem.CopyFile(TextBox4.Text & "\" & DMedConf & ".cfg", MedExtra & "Backup\" & Date.Today.ToString("ddMMyyyy") & "_" & vmedClear & ".cfg", True)
            MsgBox("Backup completed successfully.", MsgBoxStyle.MsgBoxRight + MsgBoxStyle.OkOnly, "Backup Done")
        End If
    End Sub

    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        Dim fCfg As OpenFileDialog = New OpenFileDialog()
        fCfg.Title = "Select Mednafen Config Files"
        fCfg.InitialDirectory = MedExtra & "Backup"
        fCfg.Filter = "Mednafen Config Files (*.cfg)|*.cfg"
        fCfg.RestoreDirectory = True
        If fCfg.ShowDialog() = DialogResult.OK Then
            My.Computer.FileSystem.CopyFile(fCfg.FileName, TextBox4.Text & "\" & DMedConf & ".cfg", True)
            MsgBox("Restored " & DMedConf & ".cfg backup.", MsgBoxStyle.MsgBoxRight + MsgBoxStyle.OkOnly, "Restore Done")
        End If
    End Sub

    Private Sub Button22_Click(sender As Object, e As EventArgs)
        About.ShowDialog()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If My.Computer.Network.IsAvailable = False Then MsgBox("Connections is not Available", vbOKOnly + vbExclamation) : Exit Sub
        get_update()
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        rDes = "Select Default Generic Rom Path"
        yPath()
        If rPath <> "" Then TextBox9.Text = rPath : RWIni()
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        rDes = "Select Default Lynx Rom Path"
        yPath()
        If rPath <> "" Then TextBox8.Text = rPath : RWIni()
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        rDes = "Select Default GameBoy/Color Rom Path"
        yPath()
        If rPath <> "" Then TextBox7.Text = rPath : RWIni()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        rDes = "Select Default GameBoy Advance Rom Path"
        yPath()
        If rPath <> "" Then TextBox5.Text = rPath : RWIni()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        rDes = "Select Default Neo Geo Pocket Rom Path"
        yPath()
        If rPath <> "" Then TextBox6.Text = rPath : RWIni()
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        rDes = "Select Default NES Rom Path"
        yPath()
        If rPath <> "" Then TextBox11.Text = rPath : RWIni()
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        rDes = "Select Default PC Engine Rom Path"
        yPath()
        If rPath <> "" Then TextBox10.Text = rPath : RWIni()
    End Sub

    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        rDes = "Select Default PC-FX Iso Path"
        yPath()
        If rPath <> "" Then TextBox15.Text = rPath : RWIni()
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        rDes = "Select Default Game Gear Rom Path"
        yPath()
        If rPath <> "" Then TextBox14.Text = rPath : RWIni()
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        rDes = "Select Default Megadrive Rom Path"
        yPath()
        If rPath <> "" Then TextBox13.Text = rPath : RWIni()
    End Sub

    Private Sub Button58_Click(sender As Object, e As EventArgs) Handles Button58.Click
        rDes = "Select Default Apple ][ / + Rom Path"
        yPath()
        If rPath <> "" Then TextBox22.Text = rPath : RWIni()
    End Sub

    Private Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click
        rDes = "Select Default Master System Rom Path"
        yPath()
        If rPath <> "" Then TextBox19.Text = rPath : RWIni()
    End Sub

    Private Sub Button27_Click(sender As Object, e As EventArgs) Handles Button27.Click
        rDes = "Select Default Playstation Iso Path"
        yPath()
        If rPath <> "" Then TextBox18.Text = rPath : RWIni()
    End Sub

    Private Sub Button26_Click(sender As Object, e As EventArgs) Handles Button26.Click
        rDes = "Select Default SNES Rom Path"
        yPath()
        If rPath <> "" Then TextBox17.Text = rPath : RWIni()
    End Sub

    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        rDes = "Select Default Virtual Boy Rom Path"
        yPath()
        If rPath <> "" Then TextBox12.Text = rPath : RWIni()
    End Sub

    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        rDes = "Select Default WonderSwan Rom Path"
        yPath()
        If rPath <> "" Then TextBox16.Text = rPath : RWIni()
    End Sub

    Private Sub Button49_Click(sender As Object, e As EventArgs) Handles Button49.Click
        rDes = "Select Default Saturn Iso Path"
        yPath()
        If rPath <> "" Then TextBox20.Text = rPath : RWIni()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles TimerSoxConv.Tick
        If ListAddsFile.Items.Count = 0 Then TimerSoxConv.Stop() : TimerSoxConv.Enabled = False : Exit Sub

        Dim Sox_Exe() As Process
        Sox_Exe = Process.GetProcessesByName("sox", My.Computer.Name)

        If RepeatSingleToolStripMenuItem.Checked = True And Sox_Exe.Length = 0 Then
            play()
        ElseIf RepeatAllToolStripMenuItem.Checked = True And Sox_Exe.Length = 0 Then
            If ListAddsFile.SelectedIndex >= 0 Then
                play()
                Try
                    ListAddsFile.SelectedIndex = ListAddsFile.SelectedIndex + 1
                Catch ex As Exception
                    ListAddsFile.SelectedIndex = 0
                End Try
            End If

        ElseIf RepeatSingleToolStripMenuItem.Checked = False And RepeatAllToolStripMenuItem.Checked = False And Sox_Exe.Length = 0 Then
            TimerSoxConv.Stop()
            TimerSoxConv.Enabled = False
        End If
    End Sub

    Private Sub OggToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OggToolStripMenuItem.Click
        cfile = "Ogg"
        Single_Convert()
    End Sub

    Private Sub WavToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WavToolStripMenuItem.Click
        cfile = "Wav"
        Single_Convert()
    End Sub

    Private Sub OggToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles OggToolStripMenuItem1.Click
        cfile = "Ogg"
        Folder_Convert()
    End Sub

    Private Sub WavToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles WavToolStripMenuItem1.Click
        cfile = "Wav"
        Folder_Convert()
    End Sub

    Public Sub Disable_format_audio()

        Select Case ToolStripComboBox1.Text
            Case "Wav"
                OggToolStripMenuItem.Enabled = True
                OggToolStripMenuItem1.Enabled = True
                WavToolStripMenuItem.Enabled = False
                WavToolStripMenuItem1.Enabled = False
            Case "Ogg", "Ape", "Mpc"
                OggToolStripMenuItem.Enabled = False
                OggToolStripMenuItem1.Enabled = False
                WavToolStripMenuItem.Enabled = True
                WavToolStripMenuItem1.Enabled = True
            Case Else
                OggToolStripMenuItem.Enabled = True
                OggToolStripMenuItem1.Enabled = True
                WavToolStripMenuItem.Enabled = True
                WavToolStripMenuItem1.Enabled = True
        End Select

    End Sub

    Private Sub DeleteAfterConversionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteAfterConversionToolStripMenuItem.Click
        If DeleteAfterConversionToolStripMenuItem.Checked = False Then
            DeleteAfterConversionToolStripMenuItem.Checked = True
        Else
            DeleteAfterConversionToolStripMenuItem.Checked = False
        End If
    End Sub

    Private Sub PlayToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PlayToolStripMenuItem.Click
        play()
    End Sub

    Private Sub StopToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StopToolStripMenuItem.Click
        tProcess = "sox"
        KillProcess()
        TimerSoxConv.Stop()
        TimerSoxConv.Enabled = False
    End Sub

    Private Sub NextToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NextToolStripMenuItem.Click
        If ListAddsFile.SelectedIndex >= 0 And ListAddsFile.SelectedIndex < ListAddsFile.Items.Count - 1 Then
            ListAddsFile.SelectedIndex = ListAddsFile.SelectedIndex + 1
            play()
        End If
    End Sub

    Private Sub PreviousToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PreviousToolStripMenuItem.Click
        If ListAddsFile.SelectedIndex >= 1 Then
            ListAddsFile.SelectedIndex = ListAddsFile.SelectedIndex - 1
            play()
        End If
    End Sub

    Private Sub RepeatSingleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RepeatSingleToolStripMenuItem.Click
        If RepeatSingleToolStripMenuItem.Checked = False Then
            RepeatSingleToolStripMenuItem.Checked = True
            TimerSoxConv.Enabled = True
            RepeatAllToolStripMenuItem.Checked = False
        Else
            RepeatSingleToolStripMenuItem.Checked = False
        End If
    End Sub

    Private Sub RepeatAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RepeatAllToolStripMenuItem.Click
        If RepeatAllToolStripMenuItem.Checked = False Then
            RepeatAllToolStripMenuItem.Checked = True
            TimerSoxConv.Enabled = True
            RepeatSingleToolStripMenuItem.Checked = False
        Else
            RepeatAllToolStripMenuItem.Checked = False
        End If
    End Sub

    Public Sub list_DATs()
        Dim Directories() As String
        Dim u As DirectoryInfo
        ComboBox1.Items.Clear()
        Directories = Directory.GetDirectories(MedExtra & "DATs\")
        For Each Dir As String In Directories
            u = New DirectoryInfo(Dir)
            ComboBox1.Items.Add(u.Name)
        Next
    End Sub

    Function ContainsSpecialChars(s As String) As Boolean
        s = Replace(s, Path.GetDirectoryName(s) & "\", "")
        Return s.IndexOfAny("[~`!@#$%^&*()-+=|{}':;.,<>/?]".ToCharArray) <> -1
    End Function

    Private Sub ToolStripComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged

        rDes = "Select Supported Multimedia File"
        yPath()
        Dim ssa As String = rPath
        If Dir(rPath & "\*.*") = "" Or rPath = "" Then Exit Sub
        ListAddsFile.Items.Clear()

        If ContainsSpecialChars(ssa) = True Then
            MsgBox("Detected special char in folder name" & vbCrLf &
                   "It is recommended that you remove these characters from the path", vbExclamation + vbOKOnly, "Check your folder name...")
        End If

        Dim SouCartella As New IO.DirectoryInfo(ssa)
        Dim Soufile() As IO.FileInfo
        Dim file_nella_cartella As IO.FileInfo
        Soufile = SouCartella.GetFiles("*." & LCase(ToolStripComboBox1.Text))

        Try
            For Each file_nella_cartella In Soufile
                ListAddsFile.Items.Add(file_nella_cartella.Name)
            Next
            ListAddsFile.SelectedIndex = 0
            dConvert()
            Disable_format_audio()
        Catch ex As Exception
            MsgBox("No file " & ToolStripComboBox1.Text & " in folder", MsgBoxStyle.Exclamation, "Nothing to add")
        End Try
    End Sub

    Private Sub ListAddsFile_DoubleClick(sender As Object, e As EventArgs) Handles ListAddsFile.DoubleClick
        play()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            _link = "https://www.youtube.com/results?search_query=" & Replace(Trim(cleanpsx(MainGrid.CurrentRow.Cells(0).Value())), "&", "%26") _
                & "+" & MainGrid.CurrentRow.Cells(5).Value() & "&sm=3"
            open_link()
        Catch
        End Try
    End Sub

    Private Sub Button9_Click_1(sender As Object, e As EventArgs) Handles Button9.Click
        Try
            '_link = "https://www.gamesdatabase.org/list.aspx?in=1&searchtext=" & Trim(DataGridView1.CurrentRow.Cells(0).Value()) & "&searchtype=1"
            Dim webSystem As String
            Select Case MainGrid.CurrentRow.Cells(5).Value()
                Case "Atari - Lynx"
                    webSystem = "atari_lynx"
                Case "Bandai - WonderSwan"
                    webSystem = "bandai_wonderSwan"
                Case "Bandai - WonderSwan Color"
                    webSystem = "bandai_wonderSwan_color"
                Case "SNK - Neo Geo Pocket"
                    webSystem = "snk_neo-geo_pocket"
                Case "SNK - Neo Geo Pocket Color"
                    webSystem = "snk_neo-geo_pocket_color"
                Case "Nintendo - Game Boy Advance"
                    webSystem = "nintendo_game_boy_advance"
                Case "Nintendo Entertainment System"
                    webSystem = "nintendo_nes"
                Case "Nintendo - Game Boy"
                    webSystem = "nintendo_game_boy"
                Case "Nintendo - Game Boy Color"
                    webSystem = "nintendo_game_boy_color"
                Case "Virtual Boy"
                    webSystem = "nintendo_virtual_boy"
                Case "Sega - Game Gear"
                    webSystem = "sega_game_gear"
                Case "Sega - Master System - Mark III"
                    webSystem = "sega_master_system"
                Case "Sega - Mega Drive - Genesis"
                    webSystem = "sega_genesis"
                Case "Super Nintendo Entertainment System"
                    webSystem = "nintendo_snes"
                Case "Sega Saturn"
                    webSystem = "sega_saturn"
                Case "Nintendo - Famicom Disk System"
                    webSystem = "nintendo_famicom_disk_system"
            End Select

            _link = "https://www.gamesdatabase.org/list.aspx?DM=0&searchtext=" & Replace(Trim(cleanpsx(MainGrid.CurrentRow.Cells(0).Value())), "&", "and") & "&searchtype=1&system=" & webSystem & "&sort=Game"
            open_link()
        Catch
        End Try
    End Sub

    Private Sub Button8_Click_1(sender As Object, e As EventArgs) Handles Button8.Click
        Try
            '_link = "https://thegamesdb.net/search/?string=" & Trim(DataGridView1.CurrentRow.Cells(0).Value()) & " function=Search"

            TGDBPlatform()
            _link = "https://thegamesdb.net/search.php?name=" & Replace(Trim(cleanpsx(MainGrid.CurrentRow.Cells(0).Value())), "&", "%26") & "&platform_id%5B%5D=" & tgdbCID
            open_link()
        Catch
        End Try
    End Sub

    Public Sub TGDBPlatform()
        tgdbCID = ""
        Select Case MainGrid.CurrentRow.Cells(5).Value()
            Case "Apple II/II+"
                tgdbCID = "4942"
            Case "Atari - Lynx"
                tgdbCID = "4924"
            Case "Bandai - WonderSwan"
                tgdbCID = "4925"
            Case "Bandai - WonderSwan Color"
                tgdbCID = "4926"
            Case "Nintendo - Famicom Disk System"
                tgdbCID = "4936"
            Case "SNK - Neo Geo Pocket"
                tgdbCID = "4922"
            Case "SNK - Neo Geo Pocket Color"
                tgdbCID = "4923"
            Case "Nintendo - Game Boy Advance"
                tgdbCID = "5"
            Case "Nintendo Entertainment System"
                tgdbCID = "7"
            Case "Nintendo - Game Boy"
                tgdbCID = "4"
            Case "Nintendo - Game Boy Color"
                tgdbCID = "41"
            Case "Virtual Boy"
                tgdbCID = "4918"
            Case "Sega - Game Gear"
                tgdbCID = "20"
            Case "Sega - Mega Drive - Genesis"
                'If LCase(MainGrid.CurrentRow.Cells(2).Value().ToString).Contains("us") Then
                tgdbCID = "18"
                'Else
                'tgdbCID = "36"
                'End If
            Case "Sega - Master System - Mark III"
                tgdbCID = "35"
            Case "Super Nintendo Entertainment System"
                tgdbCID = "6"
            Case "Sony PlayStation"
                tgdbCID = "10"
            Case "Sega Saturn"
                tgdbCID = "17"
            Case "PC Engine - TurboGrafx 16"
                tgdbCID = "34"
            Case "TurboGrafx 16 (CD)"
                tgdbCID = "4955"
            Case "Virtual Boy"
                tgdbCID = "4918"
        End Select

        '<option value="33">Sega 32X</option>
        '<option value="21">Sega CD</option>
        '<option value="18">Sega Genesis</option>
        '<option value="36">Sega Mega Drive</option>
    End Sub

    Private Sub Button7_Click_1(sender As Object, e As EventArgs) Handles Button7.Click
        Try
            _link = "https://en.wikipedia.org/wiki/" & Replace(Replace(Trim(cleanpsx(MainGrid.CurrentRow.Cells(0).Value())), " ", "_"), "&", "%26")
            open_link()
        Catch
        End Try
    End Sub

    Private Sub Button50_Click(sender As Object, e As EventArgs) Handles Button50.Click
        MPCG.Show()
    End Sub

    Private Sub CheckBox7_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox7.CheckedChanged
        If FirstStart = False Then Datagrid_filter()
    End Sub

    Private Sub CheckBox6_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox6.CheckedChanged
        If FirstStart = False Then Datagrid_filter()
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        If FirstStart = False Then Datagrid_filter()
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged
        If FirstStart = False Then Datagrid_filter()
    End Sub

    Private Sub CheckBox8_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox8.CheckedChanged
        If FirstStart = False Then Datagrid_filter()
    End Sub

    Private Sub Button43_Click_1(sender As Object, e As EventArgs) Handles Button43.Click
        If My.Computer.Network.IsAvailable = False Then MsgBox("Connections is not Available", vbOKOnly + vbExclamation) : Exit Sub

        If CheckBox2.Checked = True Then
            For i = 0 To MainGrid.Rows.Count - 1
                MainGrid.Rows(i).Cells(0).Selected = True
                DownloadCover()
            Next
            MsgBox("Task completed!", vbOKOnly + MsgBoxStyle.Information)
            Exit Sub
        End If

        DownloadCover()
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        If ComboBox1.Text = "NoIntro" Then get_Datupdate() Else ScanFolder()
        Datagrid_filter()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If FirstStart = False Then Datagrid_filter()
    End Sub

    Private Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click
        CPM.Show()
    End Sub

    Private Sub Button34_Click(sender As Object, e As EventArgs) Handles Button34.Click

        If MainGrid.CurrentRow.Cells(5).Value() = "Nintendo - Game Boy Advance" Then

            Dim SGBA As StreamWriter
            SGBA = File.CreateText(Path.Combine(ExtractPath("path_sav"), Path.GetFileNameWithoutExtension(MainGrid.CurrentRow.Cells(4).Value()) & ".type"))
            SGBA.WriteLine(ComboBox3.Text & " " & ComboBox4.Text)
            If CheckBox12.Checked = True And ComboBox3.Text <> "rtc" Then SGBA.WriteLine("rtc")
            SGBA.Flush()
            SGBA.Close()
            MsgBox("GBA " & ComboBox3.Text & "backup Created!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
        Else
            MsgBox("You can make save backup only on GBA Game.", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub PictureBox1_DoubleClick(sender As Object, e As EventArgs) Handles PictureBox1.DoubleClick
        Try
            Process.Start(pathimage)
            TopMost = False
        Catch
        End Try
    End Sub

    Private Sub PictureBox4_DoubleClick(sender As Object, e As System.EventArgs) Handles PictureBox4.DoubleClick
        Try
            Process.Start(title)
            TopMost = False
        Catch
        End Try
    End Sub

    Private Sub PictureBox4_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox4.MouseEnter
        If CheckBox3.Checked = True And File.Exists(title) = True Then PopUp.PopupPic = title : PopUp.Show()
    End Sub

    Private Sub PictureBox5_DoubleClick(sender As Object, e As System.EventArgs) Handles PictureBox5.DoubleClick
        Try
            Process.Start(snap)
            TopMost = False
        Catch
        End Try
    End Sub

    Private Sub PictureBox5_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox5.MouseEnter
        If CheckBox3.Checked = True And File.Exists(snap) = True Then PopUp.PopupPic = snap : PopUp.Show()
    End Sub

    Private Sub PictureBox6_DoubleClick(sender As Object, e As EventArgs) Handles PictureBox6.DoubleClick
        Try
            Process.Start(SnapsFolder & ListBox1.Text)
            TopMost = False
        Catch
        End Try
    End Sub

    Private Sub TextBox35_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox35.KeyUp
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            select_link()
            open_link()
        End If
    End Sub

    Private Sub Button29_Click_1(sender As Object, e As EventArgs) Handles Button29.Click
        Convert_cue()
    End Sub

    Private Sub Label2_DoubleClick(sender As Object, e As EventArgs) Handles Label2.DoubleClick
        Dim tdebug As String = Replace(Label2.Text, "Game Name: " & vbCrLf, "")
        tdebug = Replace(tdebug, "&&", "&").Trim
        If tdebug.Length > 0 Then Clipboard.SetDataObject(tdebug)
    End Sub

    Private Sub DataGridView1_DragEnter(ByVal sender As Object, ByVal e As _
System.Windows.Forms.DragEventArgs) Handles MainGrid.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        End If
    End Sub

    Private Sub DataGridView1_DragDrop(ByVal sender As Object, ByVal e As _
    System.Windows.Forms.DragEventArgs) Handles MainGrid.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            SevenZCounter = 0
            stopscan = False

            If MainGrid.Rows.Count > 0 Then MainGrid.Rows.Clear()
            Dim MyFiles() As String
            Dim i As Integer

            MyFiles = e.Data.GetData(DataFormats.FileDrop)

            For Each Frecord In MyFiles
                old_fullpath = ""
                If Directory.Exists(Frecord) Then
                    T_MedExtra = ""
                    type_csv = ""

                    Dim attributes = File.GetAttributes(Frecord)
                    If attributes = FileAttributes.Directory Or FileAttributes.Directory.Compressed Then
                        TempFolder = MyFiles(i)
                    Else
                        TempFolder = Path.GetDirectoryName(MyFiles(i))
                    End If

                    If CheckBox14.Checked = True Then
                        RecuScan()
                    Else
                        ScanFolder()
                    End If
                    Me.Text = "MedGui Reborn"
                Else
                    percorso = R_RelPath(Frecord)
                    SingleScan()
                End If
            Next

            Datagrid_filter()

        End If
    End Sub

    Private Sub Button35_Click(sender As Object, e As EventArgs) Handles Button35.Click
        Try
            ScrapeForce = 0
            If My.Computer.Network.IsAvailable = False And
            File.Exists(MedExtra & "Scraped\" & MainGrid.CurrentRow.Cells(5).Value() & "\" & Trim(MainGrid.CurrentRow.Cells(0).Value()) & ".xml") = False _
            Then MsgBox("Connections is not Available", vbOKOnly + vbExclamation) : Exit Sub
            Scrape.GetParseXML()
        Catch
        End Try
    End Sub

    Public Sub Datagrid_filter()

        If CheckBox7.Checked = True Then
            MainGrid.Columns(1).Visible = False
        Else
            MainGrid.Columns(1).Visible = True
        End If

        If CheckBox6.Checked = True Then
            MainGrid.Columns(2).Visible = False
        Else
            MainGrid.Columns(2).Visible = True
        End If

        If CheckBox4.Checked = True Then
            MainGrid.Columns(3).Visible = False
        Else
            MainGrid.Columns(3).Visible = True
        End If

        If CheckBox5.Checked = True Then
            MainGrid.Columns(5).Visible = False
        Else
            MainGrid.Columns(5).Visible = True
        End If

        If CheckBox8.Checked = True Then
            MainGrid.AutoResizeColumns()
            ResizeGrid()
        Else
            Me.Width = 784
            MainGrid.Width = 439
        End If

        Dim Coluns_filter As Integer
        Select Case ComboBox2.Text
            Case "Rom Name"
                Coluns_filter = 0
            Case "Version"
                Coluns_filter = 2
            Case "Status"
                Coluns_filter = 3
            Case "System"
                Coluns_filter = 5
        End Select

        If type_csv <> "last" Then
            MainGrid.Sort(MainGrid.Columns(Coluns_filter), System.ComponentModel.ListSortDirection.Ascending)
        End If

        If Me.Text.Contains(" @ Files " & MainGrid.RowCount) Then
        Else
            Me.Text = Me.Text & " @ Files " & MainGrid.RowCount
        End If
        ReleaseMemory()
    End Sub

    Public Sub ResizeGrid()
        Dim tGwidth, wCol2, wCol3, wCol4, wCol6 As Integer
        Dim wCol1 As Integer = MainGrid.Columns.Item("Column1").Width
        If MainGrid.Columns.Item("Column2").Visible = True Then
            wCol2 = MainGrid.Columns.Item("Column2").Width : Else : wCol2 = 0
        End If
        If MainGrid.Columns.Item("Column3").Visible = True Then
            wCol3 = MainGrid.Columns.Item("Column3").Width : Else : wCol3 = 0
        End If
        If MainGrid.Columns.Item("Column4").Visible = True Then
            wCol4 = MainGrid.Columns.Item("Column4").Width : Else : wCol4 = 0
        End If
        If MainGrid.Columns.Item("Column6").Visible = True Then
            wCol6 = MainGrid.Columns.Item("Column6").Width : Else : wCol6 = 0
        End If
        Dim eDataGD As Integer
        If MainGrid.Controls(1).Visible = True Then
            eDataGD = 61
        Else
            eDataGD = 43
        End If

        tGwidth = wCol1 + wCol2 + wCol3 + wCol4 + wCol6 + eDataGD
        If tGwidth + 345 > Screen.PrimaryScreen.Bounds.Width Then
            tGwidth = Screen.PrimaryScreen.Bounds.Width - 345
        End If
        MainGrid.Width = tGwidth
        Me.Width = 345 + tGwidth
        If tGwidth < 439 Then Me.Width = 784
        MainGrid.PerformLayout()
    End Sub

    Private Sub Button40_Click(sender As Object, e As EventArgs) Handles Button40.Click
        TSnaps = "Titles"
        RenameSnaps()
    End Sub

    Private Sub Button46_Click(sender As Object, e As EventArgs) Handles Button46.Click
        TSnaps = "Snaps"
        RenameSnaps()
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        LoadSnaps()
    End Sub

    Private Sub Button38_Click(sender As Object, e As EventArgs) Handles Button38.Click
        If ListBox1.SelectedIndex >= 0 And ListBox1.SelectedIndex < ListBox1.Items.Count - 1 Then
            ListBox1.SelectedIndex = ListBox1.SelectedIndex + 1
        End If
    End Sub

    Private Sub Button45_Click(sender As Object, e As EventArgs) Handles Button45.Click
        If ListBox1.SelectedIndex >= 1 Then
            ListBox1.SelectedIndex = ListBox1.SelectedIndex - 1
        End If
    End Sub

    Private Sub Button39_Click(sender As Object, e As EventArgs) Handles Button39.Click
        If ListBox1.SelectedItem = "" Then Exit Sub
        Dim dels As MsgBoxResult = MsgBox("Do you want to delete """ & ListBox1.Text & """ Snap?", vbOKCancel + vbExclamation)
        If dels = vbOK Then File.Delete(SnapsFolder & ListBox1.Text) : PopoulateSnaps()
    End Sub

    Private Sub Button37_Click(sender As Object, e As EventArgs) Handles Button37.Click
        If File.Exists(SnapsFolder & ListBox1.Text) Then
            Process.Start("explorer.exe", " /select ," & Chr(34) & SnapsFolder & ListBox1.Text & Chr(34))
        End If
    End Sub

    Private Sub Label31_Click(sender As Object, e As EventArgs) Handles Label31.Click
        Try
            If circa = "*" And SnapsFolder <> "" Then
                circa = rn.Substring(0, 3) & "*"
            Else
                circa = "*"
            End If
            PopoulateSnaps()
        Catch
        End Try
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles TimerControlMednafen.Tick

        If ISON_Mednafen(1500) = True Then
            TimerControlMednafen.Stop()
            TimerControlMednafen.Enabled = False

            If TextBox1.Text.Contains("\RomTemp\") = False Then
                type_csv = "last"
                SaveGridDataInRow()
                type_csv = SY.Text
            End If

            If CheckBox18.Checked = True And NetToolStripButton.BackColor = Color.Red Then
                MedClient.Show()
                If ftperror = False Then
                    NetIn = False
                    ParseMednafenConfig()
                    FtpUploadOnConnect()
                    ParseUsedData()
                    MedClient.checkmed = True
                    MedClient.TimerNetPlay.Start()
                End If
            End If

            If MgrSetting.TPerC = True Then
                TimerPerConfig.Start()
            Else
                MgrSetting.TPerC = False
                TimerPerConfig.Stop()
            End If
        Else

            TimerControlMednafen.Stop()
            TimerControlMednafen.Enabled = False

            MsgBox("An Error Occur on Game Load, read the Mednafen Log to Recognize it", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Rom Load Error")
            StdOutBox()

        End If

    End Sub

    Private Sub StdOutBox()
        Try
            Dim sr As StreamReader = Nothing
            Dim Eriga As String = Nothing

            ErLog.RichTextBox1.Text = ""
            sr = New StreamReader(TextBox4.Text & "\stdout.txt")
            Eriga = sr.ReadLine()

            While Not Eriga Is Nothing

                Select Case True
                    Case Eriga.Contains("File format is unknown")
                        ErLog.RichTextBox1.SelectionColor = Color.Fuchsia
                    Case Eriga.Contains("Error") Or Eriga.Contains("is an incorrect size") Or Eriga.Contains("Unrecognized system")
                        Select Case True
                            Case LCase(Eriga.Contains(".sbi")), LCase(Eriga.Contains(".cfg")), LCase(Eriga.Contains(".cht")), LCase(Eriga.Contains(".ips"))
                                ErLog.RichTextBox1.SelectionColor = Color.DarkGreen
                            Case Else
                                ErLog.RichTextBox1.SelectionColor = Color.DarkRed
                        End Select
                    Case Eriga.Contains("image is too large")
                        ErLog.RichTextBox1.SelectionColor = Color.DarkGoldenrod
                    Case Else
                        ErLog.RichTextBox1.SelectionColor = Color.Black
                End Select

                ErLog.RichTextBox1.AppendText(vbNewLine & Eriga)
                Eriga = sr.ReadLine()
            End While
            ErLog.ShowDialog()

            sr.Dispose()
            sr.Close()
        Catch ex As Exception
            MGRWriteLog("MedGuiR - ErLog: " & ex.Message)
            TimerControlMednafen.Stop()
            TimerControlMednafen.Enabled = False
        End Try
    End Sub

    Public Sub DetectFolder()
        If IO.Directory.Exists(MedExtra & "Converter") = False Then
            ListAddsFile.Enabled = False
        End If
        If IO.Directory.Exists(MedExtra & "NetPlay") = False Then
            ServerToolStripButton.Enabled = False
            ServerToolStripButton.ToolTipText = "mednafen-server missing"
        End If
        If IO.Directory.Exists(MedExtra & "Media") = False Then
            System.IO.Directory.CreateDirectory(MedExtra & "\Media")
        End If
        If IO.File.Exists(MedExtra & "Mini.ini") = False Then
            System.IO.File.Create(MedExtra & "\Mini.ini")
            Test_Server()
        End If
        If IO.Directory.Exists(MedExtra & "Media\Movie") = False Then
            System.IO.Directory.CreateDirectory(MedExtra & "Media\Movie")
        End If
        If IO.Directory.Exists(MedExtra & "Media\Audio") = False Then
            System.IO.Directory.CreateDirectory(MedExtra & "Media\Audio")
        End If
        If IO.Directory.Exists(MedExtra & "Backup") = False Then
            System.IO.Directory.CreateDirectory(MedExtra & "Backup")
        End If
        If IO.Directory.Exists(MedExtra & "BoxArt") = False Then
            System.IO.Directory.CreateDirectory(MedExtra & "BoxArt")
        End If
        If IO.Directory.Exists(MedExtra & "DATs") = False Then
            System.IO.Directory.CreateDirectory(MedExtra & "DATs")
        End If
        If IO.Directory.Exists(MedExtra & "RomTemp") = False Then
            System.IO.Directory.CreateDirectory(MedExtra & "RomTemp")
        End If
        If IO.Directory.Exists(MedExtra & "Scanned") = False Then
            System.IO.Directory.CreateDirectory(MedExtra & "Scanned")
        End If
        If IO.Directory.Exists(MedExtra & "Palettes\GB") = False Then
            System.IO.Directory.CreateDirectory(MedExtra & "Palettes\GB")
        End If
        If IO.Directory.Exists(MedExtra & "Plugins") = False Then
            System.IO.Directory.CreateDirectory(MedExtra & "Plugins")
        End If
        If IO.Directory.Exists(MedExtra & "Update") = False Then
            System.IO.Directory.CreateDirectory(MedExtra & "Update")
        End If
        If IO.Directory.Exists(MedExtra & "Snaps") = False Then
            System.IO.Directory.CreateDirectory(MedExtra & "Snaps")
        End If
        If IO.Directory.Exists(MedExtra & "Icons") = False Then
            System.IO.Directory.CreateDirectory(MedExtra & "Icons")
        End If
        If IO.Directory.Exists(MedExtra & "MedPlay") = False Then
            System.IO.Directory.CreateDirectory(MedExtra & "MedPlay")
        End If
        'atm TRANSLATE doesnt work, need to investigate
        'If IO.Directory.Exists(MedExtra & "Language") = False Then
        'System.IO.Directory.CreateDirectory(MedExtra & "Language")
        'getallforms(Me)
        'End If
    End Sub

    Private Sub WS_SelectedIndexChanged(sender As Object, e As EventArgs) Handles WS.SelectedIndexChanged
        If WS.Text = "Mednafen Bios" Then CheckBox10.Checked = False : TextBox35.Text = ""
    End Sub

    Private Sub CheckBox11_Click(sender As Object, e As EventArgs) Handles CheckBox11.Click
        TextBox35.Text = rn & " " & MainGrid.CurrentRow.Cells(2).Value()
        If CheckBox11.Checked = False Then TextBox35.Text = rn
    End Sub

    Private Sub CheckBox10_Click(sender As Object, e As EventArgs) Handles CheckBox10.Click
        If CheckBox10.Checked = True Then TextBox35.Text = rn : CheckBox11.Enabled = True Else CheckBox11.Enabled = False : CheckBox11.Checked = False : TextBox35.Text = ""
    End Sub

    Private Sub Button4_Click_1(sender As Object, e As EventArgs) Handles Button4.Click
        If File.Exists(R_RelPath(percorso)) Then
            Process.Start("explorer.exe", " /select ," & Chr(34) & R_RelPath(percorso) & Chr(34))
        Else
            MsgBox("This file not exist", MsgBoxStyle.Exclamation + vbOKOnly, "Unrecognized file")
        End If
    End Sub

    Private Sub NetToolStripButton_Click_1(sender As Object, e As EventArgs) Handles NetToolStripButton.Click
        If NetToolStripButton.BackColor = Color.Transparent Then
            NetToolStripButton.BackColor = Color.Red
            AutoConnectToolStripMenuItem.Checked = True
            ConnectedToolStripMenuItem.BackColor = Color.Green
        Else
            NetToolStripButton.BackColor = Color.Transparent
            AutoConnectToolStripMenuItem.Checked = False
            ConnectedToolStripMenuItem.BackColor = Color.Red
        End If

    End Sub

    Private Sub Button48_Click(sender As Object, e As EventArgs) Handles Button48.Click
        _link = "https://redump.org/discs/quicksearch/" & Serial_PSX
        open_link()
    End Sub

    Private Sub Select_system()
        Me.Text = "MedGui Reborn"
        SetSRom()

        If StartRom <> "" Then
            TempFolder = StartRom
            Me.Text = "MedGui Reborn - " & UCase(SY.Text) & " Folder"
            T_MedExtra = ""

            type_csv = SY.Text

            If Dir(MedExtra & "Scanned\" & SY.Text & ".csv") = "" Then
                If CheckBox14.Checked = True Then
                    RecuScan()
                Else
                    ScanFolder()
                End If
                SaveGridDataInFile()
            Else

                If MainGrid.RowCount >= 0 Then MainGrid.Refresh() : MainGrid.Rows.Clear()
                LoadGridDataInFile()
                SearchGridGenreInRow()

            End If
        ElseIf StartRom = "" And SY.Text = "" Then
            TempFolder = "RomTemp"
            Me.Text = "MedGui Reborn - " & TempFolder
            T_MedExtra = MedExtra
            MainGrid.Rows.Clear()
            scansiona()
        Else
            MainGrid.Rows.Clear()
        End If
        If SY.Text <> "" Then RebuildToolStripButton.Enabled = True

        Try : MainGrid.Focus() : MainGrid.Rows(0).Cells(0).Selected = True : Catch ex As Exception : MGRWriteLog("MedGuiR - Select_system: " & ex.Message)
            SY.Focus() : End Try
    End Sub

    Private Sub Button36_Click(sender As System.Object, e As System.EventArgs) Handles Button36.Click
        SnapsFolder = ExtractPath("path_snap")
        circa = "*"
        PopoulateSnaps()
    End Sub

    Private Sub CheckBox15_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox15.CheckedChanged
        If CheckBox15.Checked = True Then
            CheckBox15.BackColor = Color.Goldenrod
        Else
            CheckBox15.BackColor = SystemColors.Control
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            CheckBox1.BackColor = Color.Black
        Else
            CheckBox1.BackColor = SystemColors.Control
        End If
    End Sub

    Private Sub Button41_Click(sender As System.Object, e As System.EventArgs) Handles Button41.Click
        DeFolder.Description = "Select a Snaps Folder"
        If DeFolder.ShowDialog() = DialogResult.OK Then SnapsFolder = DeFolder.SelectedPath & "\" : circa = "*"
        PopoulateSnaps()
        Exit Sub
    End Sub

    Private Sub RecentToolStripButton1_Click(sender As System.Object, e As System.EventArgs) Handles RecentToolStripButton1.Click
        Dim old_type_csv = type_csv
        If Me.Text.Contains("MedGui Reborn - Recent Roms") Then
            Me.Text = "MedGui Reborn"
            Select_system()
        Else
            type_csv = "last"
            RebuildToolStripButton.Enabled = False
            If Dir(MedExtra & "Scanned\" & type_csv & ".csv") = "" Then
                MsgBox("There is no list with your recent roms.", vbInformation + vbOKOnly)
                type_csv = old_type_csv
                If Me.Text <> "MedGui Reborn - Favorites Roms" And SY.Text <> "" Then RebuildToolStripButton.Enabled = True
            Else
                Me.Text = "MedGui Reborn - Recent Roms"
                RebuildToolStripButton.Enabled = False
                MainGrid.Rows.Clear()
                LoadGridDataInFile()
                Datagrid_filter()
            End If

            T_MedExtra = MedExtra
            Purge_Grid()
            MainGrid.Focus()
        End If
    End Sub

    Private Sub RecentToolStripButton1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles RecentToolStripButton1.MouseDown

        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                type_csv = "last"
                Del_PreScanned()
            End If
        Catch
            MsgBox("A strange error occurred!" &
                       vbCrLf & "Please select a game to open specific setting", MsgBoxStyle.Exclamation)
        End Try

    End Sub

    Private Sub FavouritesToolStripButton_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles FavouritesToolStripButton.MouseDown

        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                type_csv = "fav"
                Del_PreScanned()
            End If
        Catch
            MsgBox("A strange error occurred!" &
                       vbCrLf & "Please select a game to open specific setting", MsgBoxStyle.Exclamation)
        End Try

    End Sub

    Private Sub Del_PreScanned()
        If File.Exists(MedExtra & "Scanned\" & type_csv & ".csv") = False Then Exit Sub

        Dim DSca As String = ""

        Select Case type_csv
            Case "fav"
                DSca = "Favorites"
            Case "last"
                DSca = "Recents"
        End Select

        Dim delrec = MsgBox("Do you want to clear " & DSca & " Roms?", MsgBoxStyle.OkCancel + MsgBoxStyle.Information)
        If delrec = MsgBoxResult.Ok Then
            File.Delete(MedExtra & "Scanned\" & type_csv & ".csv")
            MainGrid.Rows.Clear()
            Select_system()
        End If
    End Sub

    Private Sub FolderRomToolStripButton_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles FoldeRomToolStripButton.MouseDown

        If e.Button = Windows.Forms.MouseButtons.Right Then
            TempFolder = ""
            stopscan = False
            ScanFolderRom()
            CustomPlaylist()
        End If

    End Sub

    Private Sub CustomPlaylist()
        Try
            Dim AppendInp As String = ""
            If CheckBox14.Checked = True Then AppendInp = "R - " Else AppendInp = "- "
            If TempFolder = "" Then
                MsgBox("Unable to save a playlist" & vbCrLf &
                       "Select a directory to scan", vbOKOnly + vbExclamation, "No directory to scan...")
                Exit Sub
            End If
inputagain:
            Dim Rmsg = InputBox("Select a Name for Prescanned File", "Put a Folder Name (Default: Selected Folder Name)", Path.GetFileName(TempFolder))
            If Rmsg.Trim = "" Then Exit Sub 'GoTo inputagain

            'If Len(Rmsg) >= 3 Then
            'If Rmsg.Substring(0, 2) <> "- " Or Rmsg.Substring(0, 4) <> "R - " Then Rmsg = AppendInp & Rmsg.Trim
            'End If

            Rmsg = AppendInp & Rmsg.Trim
            type_csv = Rmsg
            SaveGridDataInFile()
            Datagrid_filter()
            CustomScanFolder()
        Catch
        End Try
    End Sub

    Private Sub ScanFolderRom()
        Dim TDeFolder As FolderBrowserDialog = New FolderBrowserDialog()
        TDeFolder.Description = "Select a Folder with Roms Inside"

        If TDeFolder.ShowDialog() = DialogResult.OK Then
            old_fullpath = ""
            T_MedExtra = ""
            type_csv = ""
            TempFolder = TDeFolder.SelectedPath
            '#####
            If CheckBox14.Checked = True Then
                RecuScan()
            Else
                ScanFolder()
            End If

            Me.Text = "MedGui Reborn"
            Datagrid_filter()
        Else
            TempFolder = ""
            Exit Sub
        End If

    End Sub

    Private Sub ListBox2_KeyUp(sender As Object, e As KeyEventArgs) Handles ListBox2.KeyUp
        Select Case e.KeyCode
            Case Keys.Enter
                If ListBox2.SelectedItem = "" Then Exit Sub
                Populate_List2()
                TempFolder = ""
        End Select
    End Sub

    Private Sub Button51_Click(sender As Object, e As EventArgs) Handles Button51.Click
        MJoyConfig.Show()
    End Sub

    Private Sub Button52_Click(sender As Object, e As EventArgs) Handles Button52.Click
        DetectLastMednafen()
    End Sub

    Private Sub ListBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles ListBox2.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                e.SuppressKeyPress = True
        End Select
    End Sub

    Private Sub Populate_List2()
        If ListBox2.SelectedItem = "" Then Exit Sub
        SY.Text = ""
        Me.Text = "MedGui Reborn - " & ListBox2.SelectedItem & " Rom"
        MainGrid.Rows.Clear()
        type_csv = ListBox2.SelectedItem
        LoadGridDataInFile()
        T_MedExtra = MedExtra
        Datagrid_filter()
        type_csv = ListBox2.SelectedItem
        Purge_Grid()
        CustomScanFolder()
    End Sub

    Private Sub ListBox2_DoubleClick(sender As Object, e As System.EventArgs) Handles ListBox2.DoubleClick
        If ListBox2.SelectedItem = "" Then Exit Sub
        Populate_List2()
        TempFolder = ""
    End Sub

    Private Sub Button47_Click(sender As System.Object, e As System.EventArgs) Handles Button47.Click
        If ListBox2.SelectedItem = "" Then Exit Sub
        Dim dels As MsgBoxResult = MsgBox("Do you want to delete """ & ListBox2.Text & """ Path?", vbOKCancel + vbExclamation)
        If dels = vbOK Then
            File.Delete(MedExtra & "Scanned\" & ListBox2.Text & ".csv")
            CustomScanFolder()
            MainGrid.Rows.Clear()
            Select_system()
        End If
    End Sub

    Private Sub Button53_Click(sender As Object, e As EventArgs) Handles Button53.Click
        If CheckBox18.Checked = False Then CheckBox18.Checked = True

        If ComboBox7.Text.Trim = "" Then
            MsgBox("FTP Adress empty!", vbOKOnly + vbExclamation)
        ElseIf TextBox25.Text.Trim = "" Then
            MsgBox("Username empty!", vbOKOnly + vbExclamation)
        ElseIf TextBox24.Text.Trim = "" Then
            MsgBox("Password empty!", vbOKOnly + vbExclamation)
        ElseIf TextBox23.Text.Trim = "" Then
            MsgBox("Initial Path empty!", vbOKOnly + vbExclamation)
        Else
            MedClient.Show()
        End If

    End Sub

    Private Sub Button54_Click(sender As Object, e As EventArgs) Handles Button54.Click
        rDes = "Select Destination Path for Downloaded Rom"
        yPath()
        If rPath <> "" Then TextBox21.Text = rPath : RWIni()
    End Sub

    Private Sub CheckBox18_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox18.CheckedChanged

        If CheckBox18.Checked = True And File.Exists(TextBox4.Text & "\" & DMedConf & ".cfg") Then
            NetIn = True
            ParseMednafenConfig()
            Dim inputnick As String
            If Nick = "" Then
                Dim rnd1 As New Random()
                inputnick = InputBox("You don't have set a netplay nick on Mednafen, please input one")
                If inputnick.Trim = "" Then inputnick = "NoNick" & rnd1.Next(1, 300)

                Arg = " -netplay.nick " & inputnick
                tProcess = "mednafen"
                wDir = TextBox4.Text
                StartProcess()
            End If
        End If

        If CheckBox18.Checked = True Then
            CheckBox19.Enabled = True
        Else
            CheckBox19.Enabled = False
        End If
    End Sub

    Private Sub StartGameToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StartGameToolStripMenuItem.Click
        NetToolStripButton.BackColor = SystemColors.Control
        StartStatic_emu()
    End Sub

    Private Sub RemoveFromFavoritesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveFromFavoritesToolStripMenuItem.Click
        MainGrid.Rows.Remove(MainGrid.CurrentRow)
        SaveGridDataInFile()
        If My.Computer.FileSystem.GetFileInfo(MedExtra & "Scanned\" & type_csv & ".csv").Length = 0 Then
            System.IO.File.Delete(MedExtra & "Scanned\" & type_csv & ".csv")
        End If
    End Sub

    Public Sub RebuilDesync()
        set_special_module()
        If File.Exists(TextBox4.Text & "\" & p_c & ".cfg”) Then Read_Desync()

        If contdes = 1 And File.Exists(TextBox4.Text & "\" & p_c & ".cfg") Then File.Delete(TextBox4.Text & "\" & p_c & ".cfg")
        If File.Exists(MedExtra & "\NoDesync\Backup\" & p_c & ".cfg") Then
            My.Computer.FileSystem.MoveFile(MedExtra & "\NoDesync\Backup\" & p_c & ".cfg", TextBox4.Text & "\" & p_c & ".cfg")
        End If

    End Sub

    Private Sub AdvancedSettingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdvancedSettingToolStripMenuItem.Click
        If consoles = "generic" Then
            MsgBox("You can't set options on GENERIC detection", vbOKOnly + MsgBoxStyle.Critical, "Can't open setting...")
            Exit Sub
        End If
        RebuilDesync()
        If last_consoles <> consoles Or MgrSetting.Visible = False Then SwSetting = True : improm() : last_consoles = consoles
    End Sub

    Private Sub ImportFromFile_Click(sender As Object, e As EventArgs) Handles ImportFromFile.Click
        Dim fdlg As OpenFileDialog = New OpenFileDialog()
        fdlg.Title = "Select an image"
        fdlg.Filter = "All supported format (*.png,*.jpg)|*.png;*.jpg"
        fdlg.FilterIndex = 1
        fdlg.RestoreDirectory = True
        If fdlg.ShowDialog() = DialogResult.OK Then
            If Directory.Exists(Path.GetDirectoryName(pathimage)) = False Then
                Directory.CreateDirectory(Path.GetDirectoryName(pathimage))
            End If
            File.Copy(fdlg.FileName, pathimage, True)
            PictureBox1.Load(pathimage)
        End If
    End Sub

    Private Sub mMetroMed_Click(sender As Object, e As EventArgs) Handles mMetroMed.Click
        If File.Exists(Application.StartupPath & "\MetroMed.exe") Then
            Process.Start(Application.StartupPath & "\MetroMed.exe")
        Else
            MsgBox("MMetroMed not detected!", vbOKOnly + MsgBoxStyle.Exclamation, "MetroMed not detected...")
        End If
    End Sub

    Private Sub RIPSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RIPSToolStripMenuItem.Click
        If File.Exists(R_RelPath(percorso) & ".ips") Then My.Computer.FileSystem.DeleteFile(R_RelPath(percorso) & ".ips", FileIO.UIOption.AllDialogs, FileIO.RecycleOption.SendToRecycleBin, FileIO.UICancelOption.DoNothing)
    End Sub

    Private Sub RSBIToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RSBIToolStripMenuItem.Click
        If File.Exists(Path.GetDirectoryName(R_RelPath(percorso)) & "\" & Path.GetFileNameWithoutExtension(R_RelPath(percorso)) & ".sbi") Then My.Computer.FileSystem.DeleteFile(Path.GetDirectoryName(R_RelPath(percorso)) & "\" & Path.GetFileNameWithoutExtension(R_RelPath(percorso)) & ".sbi", FileIO.UIOption.AllDialogs, FileIO.RecycleOption.SendToRecycleBin, FileIO.UICancelOption.DoNothing)
    End Sub

    Private Sub IPSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IPSToolStripMenuItem.Click
        Dim fdlg As OpenFileDialog = New OpenFileDialog()
        fdlg.Title = "Select an ips/sbi patch"
        fdlg.Filter = "All supported format (*.ips,*.sbi,*.bps)|*.ips;*.sbi;*.bps"
        fdlg.FilterIndex = 1
        fdlg.RestoreDirectory = True
        If fdlg.ShowDialog() = DialogResult.OK Then
            Dim orisp As String

            Select Case LCase(Path.GetExtension(fdlg.SafeFileName))
                Case ".ips"
                    If File.Exists(R_RelPath(percorso) & ".ips") Then
                        orisp = MsgBox(Path.GetFileName(R_RelPath(percorso)) & ".ips exist" & vbCrLf & "Do you want to overwrite it?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, "IPS exist...")
                    End If
                    If orisp <> vbNo Then
                        My.Computer.FileSystem.CopyFile(fdlg.FileName, R_RelPath(percorso) & ".ips", overwrite:=True)
                        MsgBox("IPS Patch moved in the same game folder", MsgBoxStyle.Information + vbOKOnly, "Patch moved...")
                    End If
                Case ".sbi"
                    If File.Exists(Path.GetDirectoryName(R_RelPath(percorso)) & "\" & Path.GetFileNameWithoutExtension(R_RelPath(percorso)) & ".sbi") Then
                        orisp = MsgBox(Path.GetFileNameWithoutExtension(R_RelPath(percorso)) & ".sbi exist" & vbCrLf & "Do you want to overwrite it?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, "SBI exist...")
                    End If
                    If orisp <> vbNo Then
                        My.Computer.FileSystem.CopyFile(fdlg.FileName, Path.GetDirectoryName(R_RelPath(percorso)) & "\" & Path.GetFileNameWithoutExtension(R_RelPath(percorso)) & ".sbi", overwrite:=True)
                        MsgBox("SBI Patch moved in the same game folder", MsgBoxStyle.Information + vbOKOnly, "Patch moved...")
                    End If
                Case ".bps"
                    If File.Exists(MedExtra & "\Plugins\flips.exe") Then
                        Dim viewex = LCase(Path.GetExtension(R_RelPath(percorso)))
                        Select Case viewex
                            Case ".zip", ".rar", ".7z"
                                simple_extract()
                        End Select

                        tProcess = "flips"
                        wDir = (MedExtra & "Plugins")
                        Arg = "-a " & Chr(34) & fdlg.FileName & Chr(34) & " " & Chr(34) & R_RelPath(percorso) & Chr(34) & " " & Chr(34) & Path.GetDirectoryName(R_RelPath(percorso)) & "\" & Path.GetFileNameWithoutExtension(R_RelPath(percorso)) _
                        & "_patched" & Path.GetExtension(R_RelPath(percorso)) & Chr(34)
                        StartProcess()
                        execute.WaitForExit()
                        Dim respatch As String
                        If R_RelPath(percorso).Contains("\RomTemp\") Then
                            File.Delete(R_RelPath(percorso))
                            respatch = "File patched and putted into MedGuiR RomTemp folder"
                        Else
                            respatch = "File patched and putted into the same rom folder"
                        End If
                        percorso = Path.GetDirectoryName(R_RelPath(percorso)) & "\" & Path.GetFileNameWithoutExtension(R_RelPath(percorso)) _
                        & "_patched" & Path.GetExtension(R_RelPath(percorso))
                        MsgBox(respatch, MsgBoxStyle.Information + vbOKOnly, "File patched...")
                        Process.Start("explorer.exe", " /select ," & Chr(34) & R_RelPath(percorso) & Chr(34))
                        SingleScan()
                    End If

            End Select

        End If
    End Sub

    Private Sub LinkLabel15_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel15.LinkClicked
        Process.Start("https://discord.gg/hDpSjMb")
    End Sub

    Private Sub OnlineToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles OnlineToolStripMenuItem1.Click
        Dim gk, pw As String

        If GameKeyToolStripTextBox1.Text.Trim <> "" Then gk = GameKeyToolStripTextBox1.Text.Trim Else gk = """"""
        If PasswordToolStripTextBox1.Text.Trim <> "" Then pw = PasswordToolStripTextBox1.Text.Trim Else pw = """"""

        If ServerToolStripComboBox2.Text.Trim = "" Then Exit Sub
        Dim MenuNet As String = ""
        MenuNet = "-netplay.nick " & NickToolStripTextBox1.Text.Trim & " -netplay.host " & ServerToolStripComboBox2.Text.Trim &
           " -netplay.port " & PortToolStripTextBox1.Text & " -netplay.gamekey " & gk & " -netplay.password " & pw

        tProcess = "mednafen"
        wDir = TextBox4.Text
        Arg = MenuNet
        StartProcess()

        'Process.Start(TextBox4.Text & "\mednafen.exe", MenuNet)
        Threading.Thread.Sleep(2000)

        NetToolStripButton.BackColor = Color.Red
        StartStatic_emu()

        'NetToolStripButton.BackColor = SystemColors.Control
    End Sub

    Private Sub TimerPerConfig_Tick(sender As Object, e As EventArgs) Handles TimerPerConfig.Tick
        Dim process_med() As Process
        process_med = Process.GetProcessesByName("mednafen", My.Computer.Name)
        If process_med.Length > 0 Then
        Else
            TimerPerConfig.Stop()
            MgrSetting.TPerC = True
            RebuilDesync()
            MgrSetting.save_per_config()
        End If
    End Sub

    Private Sub AddToFavoritesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddToFavoritesToolStripMenuItem.Click
        type_csv = "fav"
        SaveGridDataInRow()
    End Sub

    Private Sub ResetToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ResetToolStripMenuItem.Click
        Reset.ShowDialog()
        If ResetAll = True Then Me.Close()
    End Sub

    Private Sub Button55_Click(sender As Object, e As EventArgs) Handles Button55.Click
        CustomScanFolder()
    End Sub

    Private Sub Button56_Click(sender As Object, e As EventArgs) Handles Button56.Click
        ComboBox7.Enabled = True
        TextBox25.Enabled = True
        TextBox24.Enabled = True
        TextBox23.Enabled = True
    End Sub

    Private Sub Button57_Click(sender As Object, e As EventArgs) Handles Button57.Click
        Try
            _link = (MedExtra & "FAQ\MedClientTutorial\Start.html")
            open_link()
        Catch ex As Exception
            MsgBox("No MedClient Guide Detected", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub AddShortuctToDesktopToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddShortuctToDesktopToolStripMenuItem.Click
        CreateShortcut()
    End Sub

    Private Sub MedPadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MedPadToolStripMenuItem.Click
        SendToMedPad()
    End Sub

    Private Sub LinkLabel9_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel9.LinkClicked
        _link = "https://www.youtube.com/playlist?list=PL6SV3kdlUgnECXxQzrIbCrbzo01sA1K60"
        open_link()
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
        If Val(Environment.OSVersion.Version.ToString) >= 6 Then
            TGDBSettings.ShowDialog()
        End If

    End Sub

    Private Sub BCKPToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BCKPToolStripMenuItem.Click
        Dim BackupHash As String = ""
        Dim BackupExt As String = ""
        Dim BackupPath As String = ""
        Dim BCKRisp As MsgBoxResult
        Dim mmodule As String = LCase(MainGrid.CurrentRow.Cells(6).Value)

        Dim fdlg As OpenFileDialog = New OpenFileDialog()
        fdlg.Title = "Select a Save/Backup to import"
        fdlg.Filter = "All supported format (*.srm,*.sav,*.rtc,*.mcr,*.flash,*.eep,*.bkr,*.bcr)|*.srm;*.sav;*.rtc;*.mcr;*.flash;*.eep;*.bkr;*.bcr"
        fdlg.FilterIndex = 1
        fdlg.RestoreDirectory = True
        If fdlg.ShowDialog() = DialogResult.OK Then
            BackupExt = LCase(Path.GetExtension(fdlg.FileName))
            BackupPath = fdlg.FileName

            BCKRisp = MsgBox("Do you want to add the file hash?" & vbCrLf &
                             "Yes = File name + hash" & vbCrLf &
                             "No = File name", vbYesNo + MsgBoxStyle.Information, "Import with hash...")

            Select Case LCase(Path.GetExtension(R_RelPath(percorso)))
                Case ".zip", ".rar", ".7z"
                    simple_extract()
                Case ".cue", ".toc", ".ccd", ".m3u", ".zst"
                    BackupHash = ""
                    GoTo SKIPHASH
            End Select

            If BCKRisp = vbYes Then
                filepath = R_RelPath(percorso)

                Select Case mmodule
                    Case "nes"
                        Mcheat.RemoveHeader(16)
                    Case "lynx"
                        Mcheat.RemoveHeader(64)
                End Select

                SHA1CalcFile()

                SetSpecialModule()
                If mmodule = "snes" And tpce = "_faust" Then
                    BackupHash = "." & r_sha.Substring(0, 32)
                Else
                    BackupHash = "." & r_md5
                End If
            Else
                BackupHash = ""
            End If
        Else
            Exit Sub
        End If

SKIPHASH:
        Dim MCSlot As String = ""
        Dim matchs = {Path.GetFileNameWithoutExtension(R_RelPath(percorso)),
         MCSlot, BackupExt}

        Select Case mmodule
            Case "md"
                BackupExt = ".sav"
            Case "gba"
                AppendAllBytes(BackupPath, 65536)
            Case "psx", "ss", "pcfx"
                If mmodule = "psx" Then MCSlot = ".0"
                If BCKRisp = vbYes Then
                    For Each foundFile As String In My.Computer.FileSystem.GetFiles(ExtractPath("path_sav"))
                        Dim splitmatch() As String = foundFile.Split(".")
                        If foundFile.Contains(matchs(0)) And foundFile.Contains(matchs(1)) And foundFile.Contains(matchs(2)) Then
                            BackupHash = "." & (splitmatch(1))
                            Exit For
                        End If
                    Next
                Else
                    BackupHash = ""
                End If
        End Select

        If Directory.Exists(ExtractPath("path_sav")) Then
            If File.Exists(Path.Combine(ExtractPath("path_sav"), Path.GetFileNameWithoutExtension(R_RelPath(percorso)) & BackupHash & MCSlot & BackupExt)) Then
                BCKRisp = MsgBox("Save already exist, do you want to overwrite it?", vbYesNo + MsgBoxStyle.Exclamation, "Save file exist...")
                If BCKRisp = vbYes Then
                    File.Copy(BackupPath, Path.Combine(ExtractPath("path_sav"), Path.GetFileNameWithoutExtension(R_RelPath(percorso)) & BackupHash & MCSlot & BackupExt), True)
                    Select Case mmodule
                        Case "gba"
                            File.Delete(BackupPath)
                            FileSystem.Rename(BackupPath & ".backup", BackupPath)
                    End Select
                Else
                    Exit Sub
                End If
            Else
                File.Copy(BackupPath, Path.Combine(ExtractPath("path_sav"), Path.GetFileNameWithoutExtension(R_RelPath(percorso)) & BackupHash & MCSlot & BackupExt), True)
            End If
            MsgBox("Backup/Save Imported!", vbOKOnly + MsgBoxStyle.Information, "Backup/Save Imported...")
        Else
            MsgBox("Unable to find sav directory!", vbOKOnly + vbCritical, "Missing folder...")
        End If
    End Sub

    Public Shared Sub AppendAllBytes(path As String, dimension As Integer)
        'argument-checking here.

        Dim size As Long = 0
        If String.IsNullOrEmpty(path) = False AndAlso IO.File.Exists(path) Then
            size = New IO.FileInfo(path).Length
        Else
            Exit Sub
        End If
        '& vbCrLf & "I will create a backup of original file"
        If size < dimension Then
            File.Copy(path, path & ".backup", True)
            MsgBox("File size mismatch, I try to resize it.", vbOKOnly + MsgBoxStyle.Exclamation, "Resize Sav/Backup...")
            Dim bytes As Byte() = New Byte(dimension - size) {}
            Using stream = New FileStream(path, FileMode.Append)
                stream.Write(bytes, 0, bytes.Length - 1)
            End Using
        End If
    End Sub

    Private Sub Button60_Click(sender As Object, e As EventArgs) Handles Button60.Click
        MAImaker.Show()
    End Sub

    Private Sub CheatToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheatToolStripMenuItem.Click
        Mcheat.Close()
        Mcheat.Show()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        About.ShowDialog()
    End Sub

    Private Sub Button59_Click(sender As Object, e As EventArgs) Handles Button59.Click
        'TranslateAllCtrls("English")
    End Sub

    Private Sub MedGuiR_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd
        If Me.Width < 794 Then Me.Width = 794
        If Me.Height < 415 Then Me.Height = 415
        If CheckBox8.Checked = True Then ResizeGrid()
        ChaseR()
        ChaseL()
    End Sub

    Private Sub FontToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles FontToolStripMenuItem1.Click
        If FontDialog1.ShowDialog <> DialogResult.Cancel Then
            MainGrid.RowsDefaultCellStyle.Font = FontDialog1.Font
            MainGrid.RowsDefaultCellStyle.ForeColor = FontDialog1.Color
            MainGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            MainGrid.Refresh()

            If CheckBox8.Checked = True Then
                MainGrid.AutoResizeColumns()
                ResizeGrid()
            End If

            If ThemeChanger.switchTheme = False Then
                MsgBox("If the font appearance has not changed, please reload the games list from the top left menu in the main form.", MsgBoxStyle.Information + vbOKOnly, "Information...")
                ThemeChanger.switchTheme = True
            End If
        End If
    End Sub

    Private Sub CellsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CellsToolStripMenuItem.Click
        If ColorDialog1.ShowDialog <> DialogResult.Cancel Then
            MainGrid.RowsDefaultCellStyle.BackColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub FontToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles FontToolStripMenuItem2.Click
        If ColorDialog1.ShowDialog <> DialogResult.Cancel Then
            MainGrid.RowsDefaultCellStyle.SelectionForeColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub CellsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CellsToolStripMenuItem1.Click
        If ColorDialog1.ShowDialog <> DialogResult.Cancel Then
            MainGrid.RowsDefaultCellStyle.SelectionBackColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub GridToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles GridColToolStripMenuItem.Click
        If ColorDialog1.ShowDialog <> DialogResult.Cancel Then
            MainGrid.GridColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub BackgroudToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackgroudToolStripMenuItem.Click
        If ColorDialog1.ShowDialog <> DialogResult.Cancel Then
            MainGrid.BackgroundColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub TestPCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TestPCToolStripMenuItem.Click
        TestCPU.Show()
    End Sub

    Private Sub BackgroundToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackgroundToolStripMenuItem.Click
        If ColorDialog1.ShowDialog <> DialogResult.Cancel Then
            DefBack = ColorDialog1.Color
            ChangeControlColors(Me, "Background")
        End If
    End Sub

    Private Sub ForeColorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ForeColorToolStripMenuItem.Click
        If ColorDialog1.ShowDialog <> DialogResult.Cancel Then
            DefFore = ColorDialog1.Color
            ChangeControlColors(Me, "Forecolor")
        End If
    End Sub

    Private Sub ContrastToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ContrastToolStripMenuItem.Click
        If ColorDialog1.ShowDialog <> DialogResult.Cancel Then
            DefBack = ColorDialog1.Color
            DefFore = Color.FromArgb(DefBack.ToArgb() Xor &HFFFFFF)
            ChangeControlColors(Me, "Contrast")
        End If
    End Sub

    Private Sub ResetToDefaultToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ResetToDefaultToolStripMenuItem1.Click
        DefBack = Color.FromKnownColor(KnownColor.Control)
        DefFore = Color.FromKnownColor(KnownColor.Black)
        ChangeControlColors(Me, "Reset")
    End Sub

    Private Sub ResetToDefaultToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetToDefaultToolStripMenuItem.Click
        FontDialog1.Reset()
        FontDialog1.FontMustExist = True
        FontDialog1.ShowColor = True
        FontDialog1.ShowEffects = True
        FontDialog1.MaxSize = 18

        MainGrid.RowsDefaultCellStyle.ForeColor = Color.Black
        MainGrid.RowsDefaultCellStyle.BackColor = Color.White
        MainGrid.RowsDefaultCellStyle.SelectionForeColor = Color.Black
        MainGrid.RowsDefaultCellStyle.SelectionBackColor = Color.PaleGoldenrod
        MainGrid.RowsDefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
        MainGrid.GridColor = Color.FromKnownColor(KnownColor.ControlDark)
        MainGrid.BackgroundColor = Color.FromKnownColor(KnownColor.AppWorkspace)
        MainGrid.Refresh()

        If CheckBox8.Checked = True Then
            MainGrid.AutoResizeColumns()
            ResizeGrid()
        End If

    End Sub

    Private Sub PictureBox2_DoubleClick(sender As Object, e As EventArgs) Handles PictureBox2.DoubleClick
        Try
            DetectChipmodule()
            If AllTags <> "" Then ChipTAG.Show() : ChipTAG.RichTextBox1.Text = AllTags
        Catch
        End Try
    End Sub

    Private Sub OpenFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenFileToolStripMenuItem.Click
        LoadRomToolStripButton.PerformClick()
    End Sub

    Private Sub OpenFolderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenFolderToolStripMenuItem.Click
        FoldeRomToolStripButton.PerformClick()
    End Sub

    Private Sub RescanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RescanToolStripMenuItem.Click
        RebuildToolStripButton.PerformClick()
    End Sub

    Private Sub OpenFavouritesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenFavouritesToolStripMenuItem.Click
        FavouritesToolStripButton.PerformClick()
    End Sub

    Private Sub RecentsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RecentsToolStripMenuItem.Click
        RecentToolStripButton1.PerformClick()
    End Sub

    Private Sub SnesSpecialChip()
        Dim filechip As String

        If Val(vmedClear) >= 12400 Then
            Exit Sub
        ElseIf Val(vmedClear) = 12220 Then
            filechip = "SpecialChip1"
        Else
            filechip = "SpecialChip"
        End If

        If File.Exists(MedExtra & "Plugins\db\" & filechip & ".txt") = False And My.Computer.Network.IsAvailable = True Then
            'My.Computer.Network.DownloadFile(UpdateServer & "/MedGuiR/SpecialChip.txt", MedExtra & "Plugins\db\SpecialChip.txt", "anonymous", "anonymous", True, 1000, True)
            FTPDownloadFile(MedExtra & "Plugins\db\" & filechip & ".txt", UpdateServer & "/MedGuiR/SpecialChip.txt", "anonymous", "anonymous")
        ElseIf File.Exists(MedExtra & "Plugins\db\" & filechip & ".txt") = False And My.Computer.Network.IsAvailable = False Then
            'MsgBox("Connections Is Not Available", vbOKOnly + vbExclamation)
            Exit Sub
        End If

        Dim oRead As StreamReader

        Try
            oRead = File.OpenText(MedExtra & "Plugins\db\" & filechip & ".txt")
            While oRead.Peek <> -1
                If rn.Trim = (oRead.ReadLine().Trim) Then
                    MsgBox("Snes Faust Module at the moment Not support Super NES enhancement chips" & vbCrLf &
                                            "I'm switching  to BSnes Module", vbOKOnly + vbInformation)
                    tpce = Nothing
                End If
            End While

            oRead.Dispose()
            oRead.Close()
        Catch ex As Exception
        Finally
            oRead.Dispose()
            oRead.Close()
        End Try
    End Sub

    Private Sub ClientOptionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClientOptionToolStripMenuItem.Click
        If MainGrid.Rows.Count < 1 Then
            MsgBox("You need to load a game on the grid to select a server", vbOKOnly + vbCritical, "No games on grid...")
            Exit Sub
        End If
        AdvancedSettingToolStripMenuItem.PerformClick()
        MgrSetting.TabControl1.SelectedIndex = 8
    End Sub

    Private Sub ConfigureServerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConfigureServerToolStripMenuItem.Click
        Standard_Conf.Show()
    End Sub

    Private Sub AutoConnectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AutoConnectToolStripMenuItem.Click
        NetToolStripButton.PerformClick()
    End Sub

    Private Sub StartAServerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StartAServerToolStripMenuItem.Click
        ServerToolStripButton.PerformClick()
    End Sub

    Private Sub OpenIRCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenIRCToolStripMenuItem.Click
        IRCToolStripButton.PerformClick()
    End Sub

    Private Sub DownloadMusicModuleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DownloadMusicModuleToolStripMenuItem.Click
        ModLandToolStripButton.PerformClick()
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButton1.CheckedChanged
        Button30.Enabled = False
        Button31.Enabled = True
    End Sub

    Private Sub CheckBox23_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox23.CheckedChanged
        SetGUI()
    End Sub

    Public Sub SetGUI()
        If CheckBox23.Checked = True Then
            RE_tar_DDIT.Visible = True
            IconStrip.Visible = False
        Else
            RE_tar_DDIT.Visible = False
            IconStrip.Visible = True
        End If
    End Sub

    Private Sub RapidGameSearchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RapidGameSearchToolStripMenuItem.Click
        'FindToolStripButton.PerformClick()
    End Sub

    Private Sub SavesCutomPlaylistToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveCustomPlaylistToolStripMenuItem.Click
        If MainGrid.Rows.Count < 2 Then
            MsgBox("You need to load more than 2 games on the grid to save playlist", vbOKOnly + vbCritical, "No games on grid...")
            Exit Sub
        End If
        CustomPlaylist()
    End Sub

    Private Sub XToolStripMenuItem_MouseUp(sender As Object, e As MouseEventArgs) Handles XToolStripMenuItem.MouseUp
        FNameToolStripTextBox.Text = ""
        FNameToolStripTextBox.Focus()
        SendKeys.Send("{BKSP}") '("{ENTER}")
    End Sub

    Private Sub LoadToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles LoadToolStripMenuItem1.Click
        Select Case SY.Text
            Case "ss", "psx", "pcfx", ""
                ManageFileToolStripMenuItem.Enabled = False
            Case Else
                ManageFileToolStripMenuItem.Enabled = True
        End Select
    End Sub

    Private Sub EmulatorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EmulatorToolStripMenuItem.Click
        If MainGrid.Rows.Count < 1 Then
            MsgBox("You need to load a game on the grid to configure options", vbOKOnly + vbCritical, "No games on grid...")
            Exit Sub
        End If
        AdvancedSettingToolStripMenuItem.PerformClick()
    End Sub

    Private Sub ControllerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ControllerToolStripMenuItem.Click
        SendToMedPad()
    End Sub

    Private Sub CheatToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CheatToolStripMenuItem1.Click
        If MainGrid.Rows.Count < 1 Then
            MsgBox("You need to load a game on the grid to open cheat manager", vbOKOnly + vbCritical, "No games on grid...")
            Exit Sub
        End If
        CheatToolStripMenuItem.PerformClick()
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButton2.CheckedChanged
        Button30.Enabled = False
        Button31.Enabled = True
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButton3.CheckedChanged
        Button30.Enabled = True
        Button31.Enabled = False
    End Sub

    Private Sub ManageFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ManageFileToolStripMenuItem.Click
        FGodMode.ShowDialog()
    End Sub

    Private Sub ManageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ADVManageToolStripMenuItem.Click
        FGodMode.ShowDialog()
    End Sub

    Private Declare Function GetActiveWindow Lib "user32" () As Long

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles TimerControlJoy.Tick

        'If GetActiveWindow = 0 Then Exit Sub
        If FormIsON = False Then Exit Sub

        Dim customCulture As Globalization.CultureInfo = CType(Threading.Thread.CurrentThread.CurrentCulture.Clone(), Globalization.CultureInfo)
        customCulture.NumberFormat.NumberDecimalSeparator = "."
        Threading.Thread.CurrentThread.CurrentCulture = customCulture

        Try
            joyGetPosEx(ComboBox6.Text, MYJOYEX)

            Dim Buttonjoypad As String = MYJOYEX.dwButtons.ToString
            Dim povjoypad As String = (MYJOYEX.dwPOV / 100).ToString
            Dim IsPressed As Boolean = MYJOYEX.dwButtonNumber

            Dim Yaxisjoypad As String = (MYJOYEX.dwYpos / 100).ToString
            Dim Xaxisjoypad As String = (MYJOYEX.dwXpos / 100).ToString

CHECKDEAD:
            If countPOV < 51 Then
                deadPOV = povjoypad
                countPOV += 1
                GoTo CHECKDEAD
            End If

            If povjoypad = deadPOV And IsPressed = True Then
                Vjoypad = Buttonjoypad
            ElseIf IsPressed = False And povjoypad <> deadPOV Then
                Vjoypad = povjoypad
            Else
                Vjoypad = ""
            End If

            Select Case Vjoypad
                Case JA 'verde - invia
                    SendKeys.Send("{ENTER}")
                Case JB 'rosso - rim preferiti
                    If MainGrid.Focused = True Then
                        SendKeys.Send("{DELETE}")
                    ElseIf Me.Focused = True Then
                    ElseIf VirtualKbrd.Visible Then
                        If VirtualKbrd.ActiveControl Is VirtualKbrd.TextBox1 Then
                            SendKeys.Send("{DEL}")
                        Else
                            VirtualKbrd.btnBack.PerformClick()
                        End If
                    Else
                        SendKeys.Send("%{F4}")
                    End If
                Case JX 'blu - seleziona
                    'SendKeys.Send("{SPACE}")
                    If MainGrid.Focused = True Then
                        'SendKeys.Send("+")
                        NetToolStripButton.PerformClick()
                    ElseIf VirtualKbrd.Visible = True Then
                        VirtualKbrd.TextBox1.Text = ""
                    Else
                        SendKeys.Send(" ")
                    End If
                Case JY 'giallo - agg preferiti
                    If MainGrid.Focused = True Then
                        SendKeys.Send("{F}")
                    End If
                    If VirtualKbrd.Visible = True Then
                        If VirtualKbrd.ActiveControl Is VirtualKbrd.TextBox1 Then
                            VirtualKbrd.oldbutton.Select()
                        Else
                            VirtualKbrd.oldbutton = VirtualKbrd.ActiveControl
                            VirtualKbrd.TextBox1.Select()
                        End If
                    End If
                Case JL 'L - menu indietro
                    If MainGrid.Focused = False Then
                        SendKeys.Send("+{TAB}")
                    Else
                        If MainGrid.CurrentRow.Index - 10 <= 0 Then
                            MainGrid.CurrentCell = MainGrid(0, 0)
                        Else
                            MainGrid.CurrentCell = MainGrid(0, MainGrid.CurrentRow.Index - 10)
                        End If
                    End If
                Case JR 'R - menu avanti
                    If MainGrid.Focused = False Then
                        SendKeys.Send("{TAB}")
                    Else
                        If MainGrid.CurrentRow.Index + 10 >= MainGrid.RowCount - 1 Then
                            MainGrid.CurrentCell = MainGrid(0, MainGrid.RowCount - 1)
                        Else
                            MainGrid.CurrentCell = MainGrid(0, MainGrid.CurrentRow.Index + 10)
                        End If
                    End If
                Case JSELECT 'menu select
                    If VirtualKbrd.Visible = True Then
                        VirtualKbrd.btnCaps.PerformClick()
                    Else
                        If MainGrid.Focused = False Then
                            MainGrid.Focus()
                        Else
                            If IconStrip.Visible = True Then
                                SY.Focus()
                            ElseIf RE_tar_DDIT.Visible = True Then
                                Me.ActiveControl = ModuleToolStripComboBox2.Control
                            End If
                        End If
                    End If
                Case JSTART 'menu start
                    If VirtualKbrd.Visible = True Then
                        VirtualKbrd.Close()
                    Else
                        If MainGrid.Rows.Count > 9 Then VirtualKbrd.ShowDialog()
                    End If
                    'If TabControl1.Focused = False Then
                    'TabControl1.Select()
                    'Else
                    'DataGridView1.Focus()
                    'End If
                Case JRIGHT 'destra
                    SendKeys.Send("{RIGHT}")
                Case JLEFT 'sinistra
                    SendKeys.Send("{LEFT}")
                Case JUP 'su
                    SendKeys.Send("{UP}")
                Case JDOWN 'giu'
                    SendKeys.Send("{DOWN}")
            End Select
        Catch
            MsgBox("Unrecognized Joypad on port " & ComboBox6.Text, vbOKOnly + vbCritical, "unrecognized Joypad")
            CheckBox16.Checked = False
        End Try
    End Sub

    Private Sub CheckBox16_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox16.CheckedChanged
        If CheckBox16.Checked = True And CheckBox16.Enabled = True Then
            Button51.Enabled = True
            ComboBox6.Enabled = False
            MYJOYEX.dwSize = 64
            MYJOYEX.dwFlags = &HFF
            TimerControlJoy.Interval = 130
            deadPOV = ""
            countPOV = 0
            TimerControlJoy.Start()
            MainGrid.Focus()
        Else
            CheckBox16.Checked = False
            ComboBox6.Enabled = True
            Button51.Enabled = False
            TimerControlJoy.Stop()
        End If
    End Sub

    Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                If File.Exists(pathimage) = False Or pathimage.Contains("NoPr.png") Then Exit Sub
                Dim DelBox As String = MsgBox("Do you want to delete Boxart?", vbYesNo + MsgBoxStyle.Exclamation, "Delete Boxart")
                If DelBox = vbYes Then
                    File.Delete(pathimage)
                    EmptyBoxart(PictureBox1)
                End If
            End If
        Catch
            PictureBox1.Image = Nothing
        End Try
    End Sub

    Private Sub PictureBox4_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox4.MouseDown
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                If File.Exists(title) = False Or pathimage.Contains("NoPr.png") Then Exit Sub
                Dim DelBox As String = MsgBox("Do you want to delete Tile Image?", vbYesNo + MsgBoxStyle.Exclamation, "Delete Tile Image")
                If DelBox = vbYes Then
                    File.Delete(title)
                    EmptyBoxart(PictureBox4)
                End If
            End If
        Catch
            PictureBox4.Image = Nothing
        End Try
    End Sub

    Private Sub PictureBox5_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox5.MouseDown
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                If File.Exists(snap) = False Or pathimage.Contains("NoPr.png") Then Exit Sub
                Dim DelBox As String = MsgBox("Do you want to delete Snap Image?", vbYesNo + MsgBoxStyle.Exclamation, "Delete Snap Image")
                If DelBox = vbYes Then
                    File.Delete(snap)
                    EmptyBoxart(PictureBox5)
                End If
            End If
        Catch
            PictureBox5.Image = Nothing
        End Try
    End Sub

    Private Sub Timer4_Tick(sender As System.Object, e As System.EventArgs) Handles TimerLabelScroll.Tick
        Label47.Text = rn.Substring(label2index, rn.Length - label2index)
        label2index += 1
        If label2index > rn.Length Then label2index = 0
    End Sub

    Private Sub ConnectedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConnectedToolStripMenuItem.Click
        NetToolStripButton.PerformClick()
    End Sub

    Private Sub RenameEntryStripMenuItem_Click(sender As Object, e As EventArgs) Handles RenameEntryStripMenuItem.Click

        Dim newEntry As String
        newEntry = InputBox("Input the new name for this game ", "Rename Entry...", MainGrid.CurrentRow.Cells(0).Value)
        If newEntry.Trim = "" Then Exit Sub
        ModEntry(newEntry.Trim)
    End Sub

    Public Sub ModEntry(entry As String)
        MainGrid.CurrentRow.Cells(0).Value = entry
        MainGrid.Refresh()
        SaveGridDataInFile()
    End Sub

    Private Sub WORLDToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles WORLDToolStripMenuItem.Click
        WORLDToolStripMenuItem1.PerformClick()
    End Sub

    Private Sub EUToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles EUToolStripMenuItem.Click
        EUToolStripMenuItem1.PerformClick()
    End Sub

    Private Sub USToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles USToolStripMenuItem.Click
        USToolStripMenuItem1.PerformClick()
    End Sub

    Private Sub JPToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles JPToolStripMenuItem.Click
        JPToolStripMenuItem1.PerformClick()
    End Sub

    Private Sub PDToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles PDToolStripMenuItem.Click
        PDToolStripMenuItem1.PerformClick()
    End Sub

    Private Sub MUSICToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles MUSICToolStripMenuItem.Click
        MUSICToolStripMenuItem1.PerformClick()
    End Sub

    Private Sub Label47_DoubleClick(sender As Object, e As System.EventArgs) Handles Label47.DoubleClick
        Dim tdebug As String = rn
        tdebug = Replace(tdebug, "&&", "&").Trim
        If tdebug.Length > 0 Then Clipboard.SetDataObject(tdebug)
    End Sub

    Private Sub ServerToolStripComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ServerToolStripComboBox2.SelectedIndexChanged
        MgrSetting.SpecificServer = ServerToolStripComboBox2.Text
        MgrSetting.PopulateNetplay()
    End Sub

    Private Sub Purge_Grid()
        If CheckBox22.Checked = True Then
            SoxStatus.Close()
            MainGrid.Focus()
            Exit Sub
        End If

        Dim countmissing As Integer = 0

        If MainGrid.RowCount <= 0 And File.Exists(MedExtra & "Scanned\" & type_csv & ".csv") = True Then
            If type_csv = "last" Or type_csv = "fav" Then File.Delete(MedExtra & "Scanned\" & type_csv & ".csv") : Exit Sub
            Dim InvPre = MsgBox(type_csv & ".csv has unrecognized files or empty values." & vbCrLf &
                   "Do you want to delete it?", MsgBoxStyle.Exclamation + vbYesNo, "Invalid prescanned file...")
            If InvPre = MsgBoxResult.Yes Then File.Delete(MedExtra & "Scanned\" & type_csv & ".csv") : Exit Sub
        End If

        For i = 0 To MainGrid.RowCount - 1
            If File.Exists(MainGrid.Rows(i).Cells(4).Value()) = False Then
                countmissing = countmissing + 1
            End If
            If countmissing > 0 Then Exit For
        Next

        If countmissing > 0 Then
            Dim RMiss = MsgBox("There are missing game on your list" & vbCrLf &
         "Do you want to remove it from the grid?",
         MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation)

            If RMiss = vbYes Then
                countmissing = MainGrid.RowCount
                Me.Text = Replace(Me.Text, " @ Files " & countmissing, "")
MisScan:
                For i = 0 To countmissing - 1
                    If i > countmissing Then Exit For
                    If File.Exists(MainGrid.Rows(i).Cells(4).Value()) = False Then
                        MainGrid.Rows.RemoveAt(i)
                        countmissing = countmissing - 1
                        GoTo MisScan
                    End If
                Next

                SaveGridDataInFile()
                Me.Text = Me.Text & " @ Files " & MainGrid.RowCount

            End If
        End If
        MainGrid.Focus()
    End Sub

    Private Sub CleanEntriesMenuItem1_Click(sender As Object, e As EventArgs) Handles CleanEntriesMenuItem1.Click
        Dim filter_e As String = InputBox("Enter criteria for removing lines, you can use + for multiple criteria" & vbCrLf &
 "Example: (Taiwan)+(Proto 2)", "Select filter for list clean", "(Unl)+(Alt)+(Alt 1)+(Alt 2)+(Proto)+(Proto 1)+(Proto 2)+(Beta)+(Beta 1)+(Program)")
        If filter_e.Trim = "" And filter_e.Trim.Contains("(") = False And filter_e.Trim.Contains(")") = False Then
            MsgBox("Criteria must be in parentheses" & vbCrLf &
 "Example: (Alt 2)", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Selected bad filter")
            Exit Sub
        End If
        CleanBadEntries(filter_e.Trim)
        SaveGridDataInFile()
    End Sub

    Private Sub Button22_Click_1(sender As Object, e As EventArgs) Handles Button22.Click
        If File.Exists(Path.Combine(Application.StartupPath, "MedGuiR CSV Creator.exe")) Then
            Process.Start(Path.Combine(Application.StartupPath, "MedGuiR CSV Creator.exe"))
        Else
            MsgBox("MedGuiR CSV Creator not detected", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Missing MedGuiR CSV Creator")
        End If
    End Sub

    Private Sub Button61_Click(sender As Object, e As EventArgs) Handles Button61.Click
        Dim Cdlg As OpenFileDialog = New OpenFileDialog()
        Cdlg.Title = "Select global override settings from specified file"

        Cdlg.Filter = "Custom Mednafen configuration file (*.cfg)|*.cfg"
        Cdlg.RestoreDirectory = True
        If Cdlg.ShowDialog() = DialogResult.OK Then
            TextBox26.Text = Cdlg.FileName
        Else
            Exit Sub
        End If
    End Sub

    Private Sub Button56_MouseClick(sender As Object, e As MouseEventArgs) Handles Button56.MouseDown
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then

                ComboBox7.Text = "speedvicio.ddns.net"
                TextBox25.Text = "medguir"
                TextBox24.Text = "Mednafen"
                TextBox23.Text = "NetPlay"
            End If
        Catch
        End Try
    End Sub

    Private Sub SendToMedPad()
        set_special_module()
        Dim portpad As String

        Select Case p_c
            Case "apple2", "md", "psx", "snes_faust", "ss"
                portpad = "Virtual Port 1"
            Case "nes", "pce", "pce_fast", "pcfx", "sms", "demo"
                portpad = "Port 1"
            Case "snes"
                portpad = "Port 1/1A"
            Case Else
                portpad = "Built-In"
        End Select

        Dim FileParameter As String = ""
        If MainGrid.Rows.Count > 0 Then
            FileParameter = "-folder=" & Chr(34) & TextBox4.Text & Chr(34) & " -console=" & p_c & " -port=" & Chr(34) & portpad & Chr(34) & " -file=" & Chr(34) & Path.GetFileNameWithoutExtension(R_RelPath(percorso)) & Chr(34)
        End If

        If File.Exists(MedExtra & "\Plugins\Controller\MedPad.exe") Then
            tProcess = "MedPad"
            KillProcess()
            Process.Start(MedExtra & "\Plugins\Controller\MedPad.exe", FileParameter)
        Else
            MsgBox("MedPad Not detected!", vbAbort + vbExclamation, "MedPad Not detected...")
        End If
        FileParameter = ""
    End Sub

    Private Sub GridToolStripMenuItem_CheckStateChanged(sender As Object, e As EventArgs) Handles GridToolStripMenuItem.CheckStateChanged

        If GridToolStripMenuItem.Checked = True Then
            FontToolStripMenuItem.Enabled = True
            HighlightToolStripMenuItem.Enabled = True
            BackgroudToolStripMenuItem.Enabled = True
            GridColToolStripMenuItem.Enabled = True
            ResetToDefaultToolStripMenuItem.Enabled = True
            GridRStyle()
        Else
            FontToolStripMenuItem.Enabled = False
            HighlightToolStripMenuItem.Enabled = False
            BackgroudToolStripMenuItem.Enabled = False
            GridColToolStripMenuItem.Enabled = False
            ResetToDefaultToolStripMenuItem.Enabled = False

            MainGrid.RowsDefaultCellStyle.ForeColor = Color.Black
            MainGrid.RowsDefaultCellStyle.BackColor = Color.White
            MainGrid.RowsDefaultCellStyle.SelectionForeColor = Color.Black
            MainGrid.RowsDefaultCellStyle.SelectionBackColor = Color.PaleGoldenrod
            MainGrid.RowsDefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            MainGrid.Refresh()
        End If
    End Sub

    Private Sub MedGuiR_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        'If Me.WindowState = FormWindowState.Normal Then
        Dim actualtab As Integer = TabControl1.SelectedIndex
        TabControl1.SelectedIndex = 0
        TabControl1.SelectedIndex = 1
        TabControl1.SelectedIndex = actualtab
        'End If
    End Sub

    Private Sub ModuleToolStripComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ModuleToolStripComboBox2.SelectedIndexChanged
        SY.Text = ModuleToolStripComboBox2.Text
        ChangeModule()
    End Sub

    Private Sub ToolStripTextBox2_KeyUp(sender As Object, e As KeyEventArgs) Handles FNameToolStripTextBox.KeyUp
        TextBox3.Text = FNameToolStripTextBox.Text
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True    ' evita il Beep!
            FindToolStripButton.PerformClick()
        End If
    End Sub

    Private Sub LoadToolStripMenuItem1_MouseEnter(sender As Object, e As EventArgs) Handles LoadToolStripMenuItem1.MouseEnter
        If TempFolder = "" Then
            SaveCustomPlaylistToolStripMenuItem.Enabled = False
        Else
            SaveCustomPlaylistToolStripMenuItem.Enabled = True
        End If
    End Sub

    Private Sub MedGuiR_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        FormIsON = True
    End Sub

    Private Sub MedGuiR_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        If VirtualKbrd.Visible = False Then FormIsON = False
    End Sub

    Private Sub PictureBox1_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox1.MouseEnter
        If CheckBox3.Checked = True And File.Exists(pathimage) = True Then PopUp.PopupPic = pathimage : PopUp.Show()
    End Sub

    Private Sub PictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox1.MouseLeave
        PopUp.Close()
    End Sub

    Private Sub PictureBox4_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox4.MouseLeave
        PopUp.Close()
    End Sub

    Private Sub PictureBox5_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox5.MouseLeave
        PopUp.Close()
    End Sub

    'code from https://dotnetrix.co.uk/tabcontrol.htm
    Private Sub TabControl1_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles TabControl1.DrawItem

        'Firstly we'll define some parameters.
        Dim CurrentTab As TabPage = TabControl1.TabPages(e.Index)
        Dim ItemRect As Rectangle = TabControl1.GetTabRect(e.Index)
        Dim FillBrush As New SolidBrush(DefBack)
        Dim TextBrush As New SolidBrush(DefFore)
        Dim sf As New StringFormat
        sf.Alignment = StringAlignment.Center
        sf.LineAlignment = StringAlignment.Center

        'If we are currently painting the Selected TabItem we'll
        'change the brush colors and inflate the rectangle.
        If CBool(e.State And DrawItemState.Selected) Then
            FillBrush.Color = DefFore
            TextBrush.Color = DefBack
            ItemRect.Inflate(2, 2)
        End If

        'Set up rotation for left and right aligned tabs
        If TabControl1.Alignment = TabAlignment.Left Or TabControl1.Alignment = TabAlignment.Right Then
            Dim RotateAngle As Single = 90
            If TabControl1.Alignment = TabAlignment.Left Then RotateAngle = 270
            Dim cp As New PointF(ItemRect.Left + (ItemRect.Width \ 2), ItemRect.Top + (ItemRect.Height \ 2))
            e.Graphics.TranslateTransform(cp.X, cp.Y)
            e.Graphics.RotateTransform(RotateAngle)
            ItemRect = New Rectangle(-(ItemRect.Height \ 2), -(ItemRect.Width \ 2), ItemRect.Height, ItemRect.Width)
        End If

        'Next we'll paint the TabItem with our Fill Brush
        e.Graphics.FillRectangle(FillBrush, ItemRect)

        'Now draw the text.
        e.Graphics.DrawString(CurrentTab.Text, e.Font, TextBrush, RectangleF.op_Implicit(ItemRect), sf)

        'Reset any Graphics rotation
        e.Graphics.ResetTransform()

        'Finally, we should Dispose of our brushes.
        FillBrush.Dispose()
        TextBrush.Dispose()

    End Sub

    Private Sub GENREComboBox1_TextChanged(sender As Object, e As EventArgs) Handles GENREComboBox1.TextChanged
        GENREToolStripComboBox2.Text = GENREComboBox1.Text
    End Sub

    Private Sub GENREToolStripComboBox2_TextChanged(sender As Object, e As EventArgs) Handles GENREToolStripComboBox2.TextChanged
        '
    End Sub

    Private Sub GENREToolStripComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GENREToolStripComboBox2.SelectedIndexChanged
        SearchGridGenreInRow()
    End Sub

    Private Sub Label3_DoubleClick(sender As Object, e As EventArgs) Handles Label3.DoubleClick
        Dim tdebug As String = Replace(Label3.Text, "Version: ", "")
        tdebug = Replace(tdebug, "&&", "&").Trim
        If tdebug.Length > 0 Then Clipboard.SetDataObject(tdebug)
    End Sub

End Class