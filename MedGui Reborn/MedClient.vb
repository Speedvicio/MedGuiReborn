Imports System.IO
Imports EZNotifications

Public Class MedClient
    Public checkmed As Boolean
    Public MuteNotification As Boolean = False
    Private InitialNetPath, NMedVersion() As String
    Dim rnd2 As New Random()

    Private Sub Form1_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        CloseNetSession()
        CleanLocalParsed()
        MedGuiR.IRCToolStripButton.Enabled = True
        MedClIniW()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = gIcon
        NotifyIcon1.Icon = gIcon
        Read_Resource()
        If Directory.Exists(MedExtra & "\MedPlay") = False Then Directory.CreateDirectory(MedExtra & "\MedPlay")
        Label4.Text = MedGuiR.Label8.Text & " " & MedGuiR.Label57.Text
        NMedVersion = MedGuiR.Label8.Text.Split("v.")
        NMedVersion(1) = NMedVersion(1).Substring(1, NMedVersion(1).Length - 1).Trim
        CleanLocalParsed()
        SetFTPData()
        FtpDownloadOnConnect()
        NetIn = True
        ParseMednafenConfig()
        TextBox1.Text = Nick
        ParseUsedData()

        F1 = Me
        CenterForm()
        ColorizeForm()

        MedGuiR.HideUCIEz = False
        AddUCI()
        RMedClIni()
        Me.WindowState = 2
        CheckBox1.Checked = True
        ConsoleComboBox.Text = MedGuiR.SY.Text
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles TimerNetPlay.Tick
        Dim process_med() As Process
        process_med = Process.GetProcessesByName("mednafen", My.Computer.Name)
        If process_med.Length > 0 Then
            CheckBox1.Checked = False
            TextBox1.Enabled = False
            If checkmed = False Then
                NetIn = True
                WriteNetPlaySession()
                checkmed = True
            End If
        Else
            CloseNetSession()
            TextBox1.Enabled = True
            CheckBox1.Checked = True
            'Read_Desync()
            'If contdes = 1 Then
            'File.Delete(MedGuiR.TextBox4.Text & "\" & consoles & ".cfg”)
            'End If

            If Gamekey.Trim <> "" Then
                Dim rispGamekey As String
                rispGamekey = MsgBox("Gamekey value is not null, do you want to reset it?", vbYesNo + MsgBoxStyle.Information, "Reset Gamekey?")
                If rispGamekey = vbYes Then
                    Gamekey = ""
                    Arg = " -netplay.gamekey " & """"""
                    tProcess = "mednafen"
                    wDir = MedGuiR.TextBox4.Text
                    StartProcess()
                End If
            End If

        End If
    End Sub

    Public Sub CloseNetSession()
        TimerNetPlay.Stop()
        checkmed = False
        Nick = TextBox1.Text
        If IO.File.Exists(MedExtra & "\MedPlay\" & Nick) Then
            My.Computer.FileSystem.DeleteFile(MedExtra & "\MedPlay\" & Nick)
            FtpDeleteOnDisconnect()
        End If
        ParseUsedData()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CleanLocalParsed()
        SetFTPData()
        FtpDownloadOnConnect()
        ParseUsedData()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        VerifyRomOnServer()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            If MedGuiR.TextBox21.Text = "" Then MedGuiR.TextBox21.Text = MedExtra & "RomTemp"

            If Directory.Exists(MedGuiR.TextBox21.Text) = False Then
                MsgBox("Invalid Rom Path on MedGui Reborn NetPlay Tab", vbOKOnly + MsgBoxStyle.Exclamation, "Invalid Rom Path...")
                Exit Sub
            End If

            Dim rom As String = DataGridView1.CurrentRow.Cells(9).Value()

            If File.Exists(MedGuiR.TextBox21.Text & "\" & rom) Then
                Dim Drom = MsgBox("You have already a game with this name" & vbCrLf &
                    "Do you want to download anyway?" & vbCrLf &
                    "I will append " & DataGridView1.CurrentRow.Cells(2).Value() & " to the game name", vbYesNo + MsgBoxStyle.Information, "Same rom name...")
                If Drom = MsgBoxResult.No Then
                    Exit Sub
                Else
                    rom = DataGridView1.CurrentRow.Cells(1).Value() & "_" & DataGridView1.CurrentRow.Cells(2).Value() & Path.GetExtension(DataGridView1.CurrentRow.Cells(9).Value())
                End If
            End If

            If ftp.FtpFileExists(ftp.CurrentDirectory & "Rom_" & DataGridView1.CurrentRow.Cells(0).Value() & "/" & Path.GetFileName(DataGridView1.CurrentRow.Cells(9).Value())) Then
                ftp.Download(ftp.CurrentDirectory & "Rom_" & DataGridView1.CurrentRow.Cells(0).Value() & "/" & DataGridView1.CurrentRow.Cells(9).Value(), MedGuiR.TextBox21.Text & "\" & rom, True)
                MsgBox("Download Done!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "File download done...")
            End If
        Catch
        End Try
    End Sub

    Private Sub DataGridView1_Click(sender As Object, e As EventArgs) Handles DataGridView1.Click
        VerifyRomOnServer()
    End Sub

    Private Sub VerifyRomOnServer()
        Try
            If ftp.FtpFileExists(ftp.CurrentDirectory & "Rom_" & DataGridView1.CurrentRow.Cells(0).Value() & "/" & DataGridView1.CurrentRow.Cells(9).Value()) = False Then
                Button2.Enabled = False
                Button2.Text = "Not Downloadable"
            Else
                Button2.Enabled = True
                Button2.Text = "&Downloadable"
            End If
        Catch
        End Try
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        If DataGridView1.Rows.Count <= 0 Then Exit Sub
        If TextBox1.Text.Trim = DataGridView1.CurrentRow.Cells(0).Value().Trim Then
            MsgBox("Same Nickname for players, change your Nickname", vbOKOnly + MsgBoxStyle.Exclamation, "Same Nickname...")
            Exit Sub
        End If
        NGameName = DataGridView1.CurrentRow.Cells(1).Value().Trim
        NModule = DataGridView1.CurrentRow.Cells(2).Value().Trim
        Server = DataGridView1.CurrentRow.Cells(4).Value()
        port = DataGridView1.CurrentRow.Cells(5).Value()
        Gamekey = DataGridView1.CurrentRow.Cells(7).Value()
        NMednafenV = DataGridView1.CurrentRow.Cells(10).Value()
        If TextBox1.Text = "" Then
            TextBox1.Text = "NoNick" & rnd2.Next(1, 300)
        End If
        Nick = TextBox1.Text
        Dim epass, egkey As String
        If TextBox2.Text.Trim = "" Then
            Password = "No"
            epass = ""
        Else
            Password = "Yes"
            epass = " -netplay.password " & TextBox2.Text
        End If

        If Gamekey.Trim = "" Then
            egkey = ""
        Else
            egkey = " -netplay.gamekey " & Gamekey
        End If

        If NMedVersion(1).Trim <> DataGridView1.CurrentRow.Cells(10).Value() Then
            MsgBox("Mednafen version mismatch!", vbOKOnly + MsgBoxStyle.Exclamation, "Mednafen version mismatch...")
            Exit Sub
        End If

        Dim Trom As String = DataGridView1.CurrentRow.Cells(1).Value() & "_" & DataGridView1.CurrentRow.Cells(2).Value() & Path.GetExtension(DataGridView1.CurrentRow.Cells(9).Value())

        If File.Exists(MedGuiR.TextBox21.Text & "\" & Trom) Then
            percorso = MedGuiR.TextBox21.Text & "\" & Trom
        ElseIf File.Exists(MedGuiR.TextBox21.Text & "\" & DataGridView1.CurrentRow.Cells(9).Value()) Then
            percorso = MedGuiR.TextBox21.Text & "\" & DataGridView1.CurrentRow.Cells(9).Value()
        Else

tryagain:
            If NGameName.EndsWith("?") Then NGameName.Substring(0, NGameName.Length - 1)
            OpenFileDialog1.Title = "Select " & NGameName.Trim & " game"
            OpenFileDialog1.Filter = "All files|*.*|Game Name|" & CleanRom(NGameName.Trim) & "*.*"
            'OpenFileDialog1.FileName = CleanRom(NGameName.Trim) & "*"

            StartNetPath()
            OpenFileDialog1.InitialDirectory = InitialNetPath
            If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                percorso = (OpenFileDialog1.FileName)
                CheckCRCNet()

                If NCRC.Trim = DataGridView1.CurrentRow.Cells(8).Value().ToString.Trim Or NCRC.Trim = "image" Then
                Else
                    MsgBox("CRC Rom mismatch, select a different rom", vbOKOnly + MsgBoxStyle.Exclamation, "CRC mismatch...")
                    GoTo tryagain
                End If
            Else
                Exit Sub
            End If

        End If

        If NModule = "psx" Then
            MedGuiR.TextBox1.Text = percorso
            BackupMCR()
        End If

        consoles = NModule
        QuestionMultitap()

        Arg = egkey & " -netplay.host " & Server & " -netplay.nick " & Nick & epass & " -netplay.port " &
              port & " -connect" & pargMT & " -force_module " & NModule & " " & NNetParameters & " " & Chr(34) & percorso & Chr(34)

        tProcess = "mednafen"
        wDir = MedGuiR.TextBox4.Text

        StartProcess()

        If MgrSetting.NoCheck = True Then MgrSetting.NoCheck = False : Exit Sub
        Threading.Thread.Sleep(200)
        checkmed = False
        TimerNetPlay.Enabled = True
        TimerNetPlay.Start()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            Button1.Enabled = False
            TimerRefresh.Interval = NumericUpDown1.Value * 1000
            TimerRefresh.Start()
        ElseIf CheckBox1.Checked = False Then
            Button1.Enabled = True
            TimerRefresh.Stop()
        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles TimerRefresh.Tick
        CleanLocalParsed()
        SetFTPData()
        FtpDownloadOnConnect()
        ParseUsedData()

        NotifyIcon1.Text = "MedClient" & vbCrLf & "NetPlay Session: " & DataGridView1.Rows.Count & vbCrLf & "Connected On IRC: " & UCI.lstUsers.Items.Count

        If DataGridView1.Rows.Count > 0 Then
            Dim CN, CG, CC, sessions As String
            Dim olen As Integer
            For i = 0 To DataGridView1.Rows.Count - 1
                If DataGridView1.Rows(i).Cells(0).Value = TextBox1.Text Then Continue For
                CN = DataGridView1.Rows(i).Cells(0).Value
                CG = DataGridView1.Rows(i).Cells(1).Value
                CC = DataGridView1.Rows(i).Cells(2).Value
                sessions += CN & " PLAY: " & CG & " USING: " & UCase(CC)

                Dim cl As String = ""
                For x = 0 To (sessions.Length - olen)
                    cl += "- "
                Next

                sessions += vbCrLf & cl & vbCrLf
                olen = sessions.Length
            Next
            If sessions <> "" Then NotifyEz("NetPlay Session Info", "MedClient Opened Sessions:" & vbCrLf & vbCrLf & sessions, 2)
        End If

    End Sub

    Friend Function NotifyEz(title As String, message As String, alert As Integer)
        If MuteNotification = True Or Me.Visible = True Or MedGuiR.HideUCIEz = True Then Exit Function

        Dim notify As New EZNotification
        Dim style As EZNotification.Style
        Dim design As EZNotification.FormDesign

        style = alert

        Dim codecolor As Integer = 0
        Select Case EzColoursToolStripComboBox.Text
            Case "Bright"
                codecolor = 0
            Case "Colorful"
                codecolor = 1
            Case "Dark"
                codecolor = 2
        End Select

        design = codecolor

        notify.Show(title, message, style, design)

    End Function

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        TimerRefresh.Interval = NumericUpDown1.Value * 1000
    End Sub

    Private Sub AddUCI()
        If UCI.Visible = True Then UCI.Close()

        UCI.TopLevel = False
        UCI.TopMost = True
        Panel1.Controls.Add(UCI)
        UCI.Dock = DockStyle.Fill
        UCI.FormBorderStyle = FormBorderStyle.None
        UCI.Show()

        UCI.txtNick.Text = TextBox1.Text
        UCI.cmbServer.Text = "irc.freenode.net"
        UCI.cmbChannel.Text = "#MedPlay"
        UCI.btnIRCConnect()
    End Sub

    Private Sub StartNetPath()

        Select Case NModule
            Case "gb"
                InitialNetPath = MedGuiR.TextBox7.Text
            Case "gba"
                InitialNetPath = MedGuiR.TextBox5.Text
            Case "gg"
                InitialNetPath = MedGuiR.TextBox14.Text
            Case "lynx"
                InitialNetPath = MedGuiR.TextBox8.Text
            Case "md"
                InitialNetPath = MedGuiR.TextBox13.Text
            Case "nes"
                InitialNetPath = MedGuiR.TextBox11.Text
            Case "ngp"
                InitialNetPath = MedGuiR.TextBox6.Text
            Case "pce", "pce_fast"
                InitialNetPath = MedGuiR.TextBox10.Text
            Case "pcfx"
                InitialNetPath = MedGuiR.TextBox15.Text
            Case "psx"
                InitialNetPath = MedGuiR.TextBox18.Text
            Case "sms"
                InitialNetPath = MedGuiR.TextBox19.Text
            Case "snes", "snes_faust"
                InitialNetPath = MedGuiR.TextBox17.Text
            Case "ss"
                InitialNetPath = MedGuiR.TextBox20.Text
            Case "vb"
                InitialNetPath = MedGuiR.TextBox12.Text
            Case "wswan"
                InitialNetPath = MedGuiR.TextBox16.Text
            Case ""
                InitialNetPath = ""
        End Select
    End Sub

    Private Sub MedClient_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Try
            UCI.irc.Disconnect()
        Catch
        End Try
        UCI.FormBorderStyle = FormBorderStyle.Sizable
        UCI.Dock = DockStyle.None
        NotifyIcon1.Dispose()
        MuteNotification = False
        MedGuiR.Button53.Enabled = True
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        ShowMClient()
    End Sub

    Private Sub MedClient_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If Me.Visible = True Then
            If Me.WindowState = FormWindowState.Minimized Then
                Me.Hide()
                NotifyIcon1.Visible = True
            End If
        End If
    End Sub

    Private Sub ShowMedClientToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowMedClientToolStripMenuItem.Click
        ShowMClient()
    End Sub

    Private Sub CloseMedClientToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseMedClientToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            MuteNotification = True
        Else
            MuteNotification = False
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            If MedGuiR.CheckBox17.Checked = False Then
                Process.Start(Chr(34) & MedGuiR.TextBox4.Text & "\Documentation\netplay.html" & Chr(34))
            Else
                MedBrowser.Show()
                MedBrowser.WebBrowser1.Navigate(MedGuiR.TextBox4.Text & "\Documentation\netplay.html")
            End If
        Catch ex As Exception
            MsgBox("No Mednafen Help Detected", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private allItems As New List(Of String)()
    Private csvList As String

    Private Sub ConsoleComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ConsoleComboBox.SelectedIndexChanged
        GameListBox.Items.Clear()
        csvList = MedExtra & "Scanned\" & ConsoleComboBox.Text.Trim & ".csv"
        ReadCsvList("GameN")
    End Sub

    Private Function ReadCsvList(GSplit As String) As String

        If File.Exists(csvList) Then
            Dim objReader As New StreamReader(csvList)
            Dim sLine As String = ""
            Dim arrText As New ArrayList()

            Dim FullGame() As String

            Do
                sLine = objReader.ReadLine()
                If Not sLine Is Nothing Then
                    arrText.Add(sLine)
                End If
            Loop Until sLine Is Nothing
            objReader.Close()

            For Each sLine In arrText
                FullGame = sLine.Split("|")
                If GSplit = "GameN" Then
                    GameListBox.Items.Add(Path.GetFileName(FullGame(4).ToString))
                    allItems.Add(Path.GetFileName(FullGame(4).ToString))
                Else
                    If FullGame(4).ToString.Contains(GSplit) Then
                        ReadCsvList = FullGame(4).ToString
                        Exit For
                    End If
                End If
            Next
        End If
    End Function

    Private Sub ShowMClient()
        Me.Show()
        Me.WindowState = FormWindowState.Maximized
        Me.Activate()
        NotifyIcon1.Visible = False
    End Sub

    Private Sub GameListBox_DoubleClick(sender As Object, e As EventArgs) Handles GameListBox.DoubleClick
        If GameListBox.Items.Count > 0 Then
            StartMedFromClient(ReadCsvList(GameListBox.SelectedItem.ToString))
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim fd As OpenFileDialog = New OpenFileDialog()

        fd.Title = "Select a Game"
        fd.Filter = "All files (*.*)|*.*"
        fd.FilterIndex = 2
        fd.RestoreDirectory = True

        If fd.ShowDialog() = DialogResult.OK Then
            StartMedFromClient(fd.FileName)
        End If
    End Sub

    Private Sub StartMedFromClient(Gpath As String)
        MedGuiR.SY.Text = ""
        MedGuiR.DataGridView1.Rows.Clear()
        percorso = Gpath
        SingleScan()

        'Arg = "-netplay.nick " & TextBox1.Text.Trim & " -netplay.host " & "" &
        '" -netplay.port " & "" & " -netplay.gamekey " & "" & " -netplay.password " & TextBox1.Text.Trim

        MedGuiR.CheckBox18.Checked = True
        MedGuiR.NetToolStripButton.BackColor = Color.Red
        MedGuiR.StartStatic_emu()
    End Sub

    Private Sub ConsoleComboBox_TextChanged(sender As Object, e As EventArgs) Handles ConsoleComboBox.TextChanged
        If GameListBox.Items.Count > 1 Then GameListBox.Items.Clear()

        If ConsoleComboBox.Text.Trim = "" Then
            GameListBox.Items.AddRange(allItems.ToArray())
        Else
            For Each item As String In allItems
                If item.StartsWith(ConsoleComboBox.Text, StringComparison.CurrentCultureIgnoreCase) Then
                    GameListBox.Items.Add(item)
                End If
            Next
        End If

    End Sub

End Class