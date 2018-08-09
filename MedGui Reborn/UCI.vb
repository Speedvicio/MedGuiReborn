Imports TechLifeForum

Public Class UCI
    Public WithEvents irc As IrcClient, oldch, nuser, RawCommand As String
    Public p As New System.Diagnostics.Process

    Private Sub btnConnect_Click(sender As System.Object, e As System.EventArgs) Handles btnConnect.Click
        btnIRCConnect()
    End Sub

    Public Sub btnIRCConnect()
        If (String.IsNullOrEmpty(cmbServer.Text.Trim())) Then MessageBox.Show("Please specify a server") : Return
        If (String.IsNullOrEmpty(cmbChannel.Text.Trim())) Then MessageBox.Show("Please specify a channel") : Return
        If (String.IsNullOrEmpty(txtNick.Text.Trim())) Then MessageBox.Show("Please specify a nick") : Return

        If cmbChannel.Text.Contains("#") = False Then cmbChannel.Text = "#" & cmbChannel.Text

        If btnConnect.Text = "&Connect" Then
            irc = New IrcClient(cmbServer.Text, CInt(txtPort.Text))
            irc.Nick = txtNick.Text
            'btnConnect.Enabled = False
            'cmbChannel.Enabled = False
            txtPort.Enabled = False
            cmbServer.Enabled = False
            txtNick.Enabled = False
            rtbOutput.Clear()
            irc.Connect()

        ElseIf btnConnect.Text = "&Disconnect" Then
            'btnConnect.Enabled = True
            'cmbChannel.Enabled = True
            txtPort.Enabled = True
            cmbServer.Enabled = True
            txtNick.Enabled = True
            lstUsers.Items.Clear()
            'txtSend.Enabled = False
            'btnSend.Enabled = False
            irc.Disconnect()
            btnConnect.Text = "&Connect"
        End If
    End Sub

    Private Sub btnSend_Click(sender As System.Object, e As System.EventArgs) Handles btnSend.Click
        If txtSend.Text.Trim = "" Then Exit Sub
        RawCommand = ""

        If (txtSend.Text.StartsWith("/")) Then
            cmdCRI()
            irc.SendRAW(Replace(Trim(RawCommand), "/", ""))
            If RawCommand.Contains("PRIVMSG ") Then
                rtbOutput.SelectionColor = Color.Chocolate
                rtbOutput.AppendText("TO " & Replace(RawCommand, "PRIVMSG ", "") &
                                     vbTab & txtSend.Text & vbCrLf)
            ElseIf RawCommand.Contains("NOTICE ") Then
                rtbOutput.SelectionColor = Color.Goldenrod
                rtbOutput.AppendText(Replace(RawCommand, "NOTICE ", "") &
                                     vbTab & txtSend.Text & vbCrLf)
            End If
            rtbOutput.ScrollToCaret()
        Else
            irc.SendMessage(cmbChannel.Text, Trim(txtSend.Text))
            rtbOutput.AppendText("<" & txtNick.Text & "> " & vbTab & txtSend.Text & vbCrLf)
            rtbOutput.ScrollToCaret()
            txtSend.Clear()
        End If
        txtSend.Focus()

    End Sub

    Private Sub txtSend_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtSend.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnSend.PerformClick()
        End If
    End Sub

    Private Sub irc_PvtMessage(User As String, Message As String) Handles irc.PrivateMessage
        My.Computer.Audio.Play(MedExtra & "Resource\Music\notification.wav")
        rtbOutput.SelectionColor = Color.Fuchsia
        rtbOutput.AppendText(User & vbTab + Message & vbNewLine)
        rtbOutput.ScrollToCaret()
    End Sub

    Private Sub irc_OvtMessage(Channel As String, User As String, Message As String) Handles irc.ChannelMessage
        If Message.Contains(txtNick.Text) Then My.Computer.Audio.Play(MedExtra & "Resource\Music\notification.wav")
        rtbOutput.AppendText(User & vbTab + Message & vbNewLine)
        rtbOutput.ScrollToCaret()
    End Sub

    Private Sub irc_ExceptionThrown(ex As System.Exception) Handles irc.ExceptionThrown
        MessageBox.Show(ex.Message)
        txtPort.Enabled = True
        cmbServer.Enabled = True
        txtNick.Enabled = True
        lstUsers.Items.Clear()
    End Sub

    Private Sub irc_OnConnect() Handles irc.OnConnect
        rtbOutput.AppendText("Connected!" & vbNewLine)
        irc.JoinChannel(cmbChannel.Text.Trim())
        oldch = cmbChannel.Text.Trim()
        btnSend.Enabled = True
        btnConnect.Text = "&Disconnect"

        GlobalVar.UCInick = txtNick.Text
        GlobalVar.UCIserver = cmbServer.Text
        GlobalVar.UCIport = txtPort.Text
        GlobalVar.UCIchannel = cmbChannel.Text
    End Sub

    Private Sub irc_userleft(channel As String, User As String) Handles irc.UserLeft
        rtbOutput.SelectionColor = Color.Red
        rtbOutput.AppendText("*** " & User & " has left the chat-room" & vbNewLine)
        lstUsers.Items.Remove(User)
        lstUsers.Update()
        If channel = cmbChannel.Text Then lstUsers.Items.Clear()
        rtbOutput.ScrollToCaret()
    End Sub

    Private Sub UserNickChangeEventDelegate(oldUser As String, newUser As String) Handles irc.UserNickChange
        rtbOutput.SelectionColor = Color.Blue
        'newUser = nuser
        rtbOutput.AppendText("*** " & oldUser & " is now knows as " & newUser & vbNewLine)
        lstUsers.Items.Remove(oldUser)
        If newUser <> "" Then lstUsers.Items.Add(newUser)
        rtbOutput.ScrollToCaret()
    End Sub

    Private Sub irc_userjoin(channel As String, User As String) Handles irc.UserJoined
        rtbOutput.SelectionColor = Color.Green
        rtbOutput.AppendText("*** " & User & " has joined the chat-room" & vbNewLine)
        lstUsers.Items.Add(User)
        lstUsers.Update()
        rtbOutput.ScrollToCaret()
    End Sub

    Private Sub irc_sendnotice(Channel As String, Message As String) Handles irc.NoticeMessage
        If MedPlay.VerifyForm("MedClient") And cmbChannel.Text = "#MedPlay" Then
            My.Computer.Audio.Play(MedExtra & "Resource\Music\notification.wav")
        End If

        rtbOutput.SelectionColor = Color.Goldenrod
        rtbOutput.AppendText(Message & vbNewLine)
        rtbOutput.ScrollToCaret()
    End Sub

    Private Sub irc_ServerMessage(message As String) Handles irc.ServerMessage
        rtbOutput.AppendText(message & vbNewLine)
        rtbOutput.ScrollToCaret()
    End Sub

    Public Sub nickttaken(Nick As String) Handles irc.NickTaken
        rtbOutput.SelectionColor = Color.Purple
        rtbOutput.AppendText("*** " & Nick & " already used" & vbNewLine)
        Dim ChangeNick As String
        ChangeNick = InputBox(Nick & " already used" & vbCrLf & "Select a new Nick",
                              "Select a new Nick", Nick & "_")
        txtNick.Text = ChangeNick

        If btnConnect.Text = "&Connect" Then
            txtPort.Enabled = True
            cmbServer.Enabled = True
            txtNick.Enabled = True
            btnConnect.PerformClick()
        ElseIf btnConnect.Text = "&Disconnect" Then
            txtSend.Text = "/nick " & ChangeNick
            btnSend.PerformClick()
        End If
    End Sub

    Private Sub irc_UpdateUsers(Channel As String, userlist() As String) Handles irc.UpdateUsers
        lstUsers.Items.Clear()

        For Each s As String In userlist
            If Not lstUsers.Items.Contains(s) Then
                lstUsers.Items.Add(s)
            End If
        Next

        'lstUsers.Items.AddRange(userlist)
        'lstUsers.Update()
    End Sub

    Private Sub SIrClient_FormClosed(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If btnConnect.Text = "&Disconnect" Then irc.Disconnect()
        Dim risp = MsgBox("Do you want to Close UCI?", MsgBoxStyle.Information + MsgBoxStyle.YesNo)
        If risp = vbNo Then e.Cancel = True
    End Sub

    Private Sub SIrClient_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If Me.Width < 780 Then Me.Width = 780
        If Me.Height < 400 Then Me.Height = 400
    End Sub

    Private Sub cmdCRI()
        Select Case True
            Case LCase(txtSend.Text.StartsWith("/quit"))
                oldch = ""
                irc.Disconnect()
                btnConnect.Text = "&Connect"
                lstUsers.Items.Clear()
                txtPort.Enabled = True
                cmbServer.Enabled = True
                txtNick.Enabled = True
            Case LCase(txtSend.Text.StartsWith("/nick"))
                RawCommand = Replace(txtSend.Text, "/nick", "NICK")
                nuser = txtSend.Text.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)(1)
                'If lstUsers.Items.Count > 0 Then
                '   For i = 0 To lstUsers.Items.Count
                '      If lstUsers.Items(i) = txtNick.Text Then
                '         lstUsers.Items(i) = nuser
                '        Exit For
                '   End If
                'Next
                txtNick.Text = nuser
                'End If
            Case LCase(txtSend.Text.StartsWith("/away"))
                RawCommand = Replace(txtSend.Text, "/away", "AWAY")
            Case txtSend.Text.StartsWith("/me")
            Case LCase(txtSend.Text.StartsWith("/msg")), LCase(txtSend.Text.StartsWith("/notice"))
                Dim rettext As String() = txtSend.Text.Split(" ")
                RawCommand = Replace(txtSend.Text, rettext(0) & " " & rettext(1) & " ", rettext(0) & " " & rettext(1) & " :")
                If RawCommand.Contains("/msg") Then
                    RawCommand = Replace(RawCommand, "/msg", "PRIVMSG")
                Else
                    RawCommand = Replace(RawCommand, "/notice", "NOTICE")
                End If
            Case LCase(txtSend.Text.StartsWith("/part"))
                RawCommand = Replace(txtSend.Text, "/part", "PART")
                lstUsers.Items.Clear()
            Case LCase(txtSend.Text.StartsWith("/join"))
                If txtSend.Text.Contains("#") = False Then MsgBox("Channel must start with #") : Return
                If txtSend.Text.Contains(cmbChannel.Text) Then Return
                irc.SendRAW("PART " & oldch)
                cmbChannel.Text = Replace(txtSend.Text, "/join ", "").Trim
                oldch = cmbChannel.Text
            Case LCase(txtSend.Text.StartsWith("/list")), LCase(txtSend.Text.StartsWith("/LIST"))
            Case Else
                RawCommand = txtSend.Text
        End Select
        txtSend.Clear()
    End Sub

    Private Sub lstUsers_DoubleClick(sender As Object, e As EventArgs) Handles lstUsers.DoubleClick
        If lstUsers.Items.Count <= 1 Or lstUsers.SelectedItem = txtNick.Text Then Return
        'CreatePVTForm()
        txtSend.Text = "/msg " & lstUsers.SelectedItem & " "
        txtSend.Focus()
        txtSend.SelectionStart = txtSend.Text.Length
    End Sub

    Private Sub SIrClient_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim rnd1 As New Random()

        If GlobalVar.UCInick = "" Then
            If MgrSetting.TextBox5.Text <> "" Then txtNick.Text = MgrSetting.TextBox5.Text Else : txtNick.Text = "Crappy" & rnd1.Next(1000)
        Else
            txtNick.Text = GlobalVar.UCInick
        End If

        If GlobalVar.UCIserver <> "" Then cmbServer.Text = GlobalVar.UCIserver
        If GlobalVar.UCIport <> "" Then txtPort.Text = GlobalVar.UCIport
        If GlobalVar.UCIchannel <> "" Then cmbChannel.Text = GlobalVar.UCIchannel

        irc_reload()
        If NetPlay.MyLocalIp <> "" Then Label3.Text = "MyIP: " & NetPlay.MyLocalIp Else Label3.Visible = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim old_irc As String = cmbChannel.Text
        If IO.File.Exists(MedExtra & "UCI.txt") = False Then
            Using IO.File.Create(MedExtra & "UCI.txt")
            End Using
        End If
        Try
            Dim lineS As String() = IO.File.ReadAllLines(MedExtra & "UCI.txt")
            For i As Integer = 0 To lineS.Length - 1
                If lineS(i) = cmbChannel.Text Then Exit Sub
            Next
            Dim sw As IO.StreamWriter = IO.File.AppendText(MedExtra & "UCI.txt")
            sw.WriteLine(cmbChannel.Text)
            sw.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            MGRWriteLog("UCI - " & sender & ": " & ex.Message)
        End Try
        irc_reload()
        cmbChannel.Text = old_irc
    End Sub

    Private Sub cmbChannel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbChannel.SelectedIndexChanged
        If btnConnect.Text = "&Disconnect" And oldch <> cmbChannel.Text.Trim() Then
            irc.PartChannel(oldch)
            irc.JoinChannel(cmbChannel.Text)
            oldch = cmbChannel.Text.Trim()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            txtSend.Text = txtSend.Text & " " & rn & " " & MedGuiR.DataGridView1.CurrentRow.Cells(2).Value() & " FOR " &
                MedGuiR.DataGridView1.CurrentRow.Cells(5).Value()
            btnSend.Focus()
        Catch
            MsgBox("Select a Game from MedGui Reborn grid", vbInformation)
        End Try
    End Sub

    Private Sub rtbOutput_LinkClicked(sender As Object, e As LinkClickedEventArgs) Handles rtbOutput.LinkClicked
        p = Process.Start(e.LinkText)
    End Sub

    Private Sub ToolTip1_Popup(sender As Object, e As PopupEventArgs) Handles ToolTip1.Popup

    End Sub

    Private Sub rtbOutput_TextChanged(sender As Object, e As EventArgs) Handles rtbOutput.TextChanged

    End Sub

    Private Sub irc_reload()
        cmbChannel.Items.Clear()
        Try
            Dim fullPath As String = MedExtra & "UCI.txt"
            If IO.File.Exists(fullPath) Then
                cmbChannel.Items.AddRange(IO.File.ReadAllLines(fullPath))
            Else
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            MGRWriteLog("UCI - irc_reload: " & ex.Message)
        End Try
    End Sub

    Private Sub Label3_DoubleClick(sender As Object, e As EventArgs) Handles Label3.DoubleClick
        Try
            txtSend.Text = txtSend.Text & " " & Replace(Label3.Text, "MyIP: ", "")
            btnSend.Focus()
        Catch
            MsgBox("No Ip detected", vbInformation)
        End Try
    End Sub

    Private Sub label1_DoubleClick(sender As Object, e As EventArgs) Handles label1.DoubleClick
        Try
            Process.Start(MedExtra & "UCI.txt")
        Catch ex As Exception
            MGRWriteLog("UCI - " & sender & ":  " & ex.Message)
        End Try
    End Sub

End Class