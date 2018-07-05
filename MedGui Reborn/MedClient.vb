Imports System.IO

Public Class MedClient
    Public checkmed As Boolean
    Private InitialNetPath, NMedVersion() As String

    Private Sub Form1_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        CloseNetSession()
        CleanLocalParsed()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Directory.Exists(MedExtra & "\MedPlay") = False Then Directory.CreateDirectory(MedExtra & "\MedPlay")
        Label4.Text = MedGuiR.Label8.Text & " " & MedGuiR.Label57.Text
        NMedVersion = MedGuiR.Label8.Text.Split("v.")
        NMedVersion(1) = NMedVersion(1).Substring(1, NMedVersion(1).Length - 1).Trim
        SetFTPData()
        FtpDownloadOnConnect()
        NetIn = True
        ParseMednafenConfig()
        TextBox1.Text = Nick
        ParseUsedData()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles TimerNetPlay.Tick
        Dim process_med() As Process
        process_med = Process.GetProcessesByName("mednafen", My.Computer.Name)
        If process_med.Length > 0 Then
            TextBox1.Enabled = False
            If checkmed = False Then
                NetIn = True
                WriteNetPlaySession()
                checkmed = True
            End If
        Else
            CloseNetSession()
            TextBox1.Enabled = True

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

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs)
        VerifyRomOnServer()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            If MedGuiR.TextBox21.Text = "" Then MedGuiR.TextBox21.Text = MedExtra & "RomTemp"

            If Directory.Exists(MedGuiR.TextBox21.Text) = False Then
                MsgBox("Invalid Rom Path on MedGui Reborn NetPlay Tab", vbOKOnly + MsgBoxStyle.Exclamation, "Invalid Rom Path...")
                Exit Sub
            End If

            If File.Exists(MedGuiR.TextBox21.Text & "\" & DataGridView1.CurrentRow.Cells(9).Value()) Then
                MsgBox("You have already donwloaded this game", vbOKOnly + MsgBoxStyle.Information, "Rom already Downloaded...")
                Exit Sub
            End If

            If ftp.FtpFileExists(ftp.CurrentDirectory & "Rom_" & DataGridView1.CurrentRow.Cells(0).Value() & "/" & Path.GetFileName(DataGridView1.CurrentRow.Cells(9).Value())) Then
                ftp.Download(ftp.CurrentDirectory & "Rom_" & DataGridView1.CurrentRow.Cells(0).Value() & "/" & DataGridView1.CurrentRow.Cells(9).Value(), MedGuiR.TextBox21.Text & "\" & DataGridView1.CurrentRow.Cells(9).Value(), True)
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
        If TextBox1.Text = "" Then TextBox1.Text = "Idontwantanick" Else : Nick = TextBox1.Text
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

        If File.Exists(MedGuiR.TextBox21.Text & "\" & DataGridView1.CurrentRow.Cells(9).Value()) Then
            percorso = MedGuiR.TextBox21.Text & "\" & DataGridView1.CurrentRow.Cells(9).Value()
        Else

tryagain:
            If NGameName.EndsWith("?") Then NGameName.Substring(0, NGameName.Length - 1)
            OpenFileDialog1.Title = "Select a " & NGameName.Trim & " game"
            OpenFileDialog1.Filter = "All files (*.*)|*.*|Game (" & NGameName.Trim & ".*)|" & NGameName.Trim & ".*"
            'OpenFileDialog1.FileName = NGameName.Trim & ".*"
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
              port & " -connect" & pargMT & " -force_module " & NModule & " " & Chr(34) & percorso & Chr(34)

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
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        TimerRefresh.Interval = NumericUpDown1.Value * 1000
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If UCI.Visible = True Then
            UCI.Close()
            UCI.Show()
        Else
            UCI.Show()
        End If

        UCI.txtNick.Text = TextBox1.Text
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

End Class