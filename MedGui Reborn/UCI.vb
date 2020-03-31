Imports TechLifeForum

Public Class UCI
    Public WithEvents irc As IrcClient, oldch, nuser, RawCommand As String
    Public p As New System.Diagnostics.Process
    Dim rnd1 As New Random()
    Dim NickPsw As String
    Dim ConnAttemp As Integer = 0

    Private Sub btnConnect_Click(sender As System.Object, e As System.EventArgs) Handles btnConnect.Click
        ConnAttemp = 0
        btnIRCConnect()
    End Sub

    Public Sub btnIRCConnect()
        Try
            If (String.IsNullOrEmpty(cmbServer.Text.Trim())) Then MessageBox.Show("Please specify a server") : Return
            If (String.IsNullOrEmpty(cmbChannel.Text.Trim())) Then MessageBox.Show("Please specify a channel") : Return
            If (String.IsNullOrEmpty(txtNick.Text.Trim())) Then
                Dim rinp As Object = InputBox("Please specify a nick", "Input a nick...", "")
                If rinp.trim = "" Then Exit Sub Else txtNick.Text = rinp : NickButton.Text = "&" & txtNick.Text
            End If

            If cmbChannel.Text.Contains("#") = False Then cmbChannel.Text = "#" & cmbChannel.Text

            If btnConnect.Text = "&Connect" Then
                irc = New IrcClient(cmbServer.Text, CInt(txtPort.Text))
                irc.Nick = txtNick.Text

                'NickButton.Text = txtNick.Text

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
                NickButton.Enabled = False
            End If
        Catch
        End Try
    End Sub

    Private Sub btnSend_Click(sender As System.Object, e As System.EventArgs) Handles btnSend.Click
        If txtSend.Text.Trim = "" Then Exit Sub
        RawCommand = ""

        Try
            If (txtSend.Text.StartsWith("/")) Then
                cmdCRI()
                irc.SendRAW(Replace(Trim(RawCommand), "/", ""))
                If RawCommand.Contains("PRIVMSG ") Then
                    rtbOutput.SelectionColor = Color.Chocolate
                    rtbOutput.AppendText("PVT TO " & Replace(RawCommand, "PRIVMSG ", "") &
                                     vbTab & txtSend.Text & vbCrLf)
                ElseIf RawCommand.Contains("NOTICE ") Then
                    rtbOutput.SelectionColor = Color.OrangeRed
                    rtbOutput.AppendText(Replace(RawCommand, "NOTICE ", "") &
                                     vbTab & txtSend.Text & vbCrLf)
                End If
                rtbOutput.ScrollToCaret()
            Else
                irc.SendMessage(cmbChannel.Text, Trim(txtSend.Text))
                rtbOutput.AppendText("<" & txtNick.Text & "> " & txtSend.Text & vbCrLf)
                rtbOutput.ScrollToCaret()
                txtSend.Clear()
            End If
            txtSend.Focus()
        Catch
        End Try

    End Sub

    Private Sub txtSend_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtSend.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnSend.PerformClick()
        End If
    End Sub

    Private Sub irc_PvtMessage(User As String, Message As String) Handles irc.PrivateMessage
        Try
            If MedClient.MuteNotification = False Then
                My.Computer.Audio.Play(MedExtra & "Resource\Music\notification.wav")
                MedClient.NotifyEz("Private Message From:", User & vbTab + Message, 0)
            End If
            rtbOutput.SelectionColor = Color.Fuchsia
            rtbOutput.AppendText(User & vbTab + Message & vbNewLine)
            rtbOutput.ScrollToCaret()
        Catch
        End Try
    End Sub

    Private Sub irc_OvtMessage(Channel As String, User As String, Message As String) Handles irc.ChannelMessage
        Try
            If Message.Contains(txtNick.Text) Then
                If MedClient.MuteNotification = False Then
                    My.Computer.Audio.Play(MedExtra & "Resource\Music\notification.wav")
                    MedClient.NotifyEz("Message From Irc Channel:", User & vbTab + Message, 0)
                End If
            End If
            rtbOutput.AppendText(User & vbTab + Message & vbNewLine)
            rtbOutput.ScrollToCaret()
        Catch
        End Try
    End Sub

    Private Sub irc_ExceptionThrown(ex As System.Exception) Handles irc.ExceptionThrown
        MessageBox.Show(ex.Message)
        ResetOnError()
    End Sub

    Private Sub ResetOnError()
        txtPort.Enabled = True
        cmbServer.Enabled = True
        txtNick.Enabled = True
        btnConnect.Text = "&Connect"
        lstUsers.Items.Clear()
    End Sub

    Private Sub irc_OnConnect() Handles irc.OnConnect
        Try
            rtbOutput.AppendText("Connected!" & vbNewLine)
            irc.JoinChannel(cmbChannel.Text.Trim())
            oldch = cmbChannel.Text.Trim()
            btnSend.Enabled = True
            btnConnect.Text = "&Disconnect"

            GlobalVar.UCInick = txtNick.Text
            GlobalVar.UCIserver = cmbServer.Text
            GlobalVar.UCIport = txtPort.Text
            GlobalVar.UCIchannel = cmbChannel.Text
        Catch
        End Try
    End Sub

    Private Sub irc_userleft(channel As String, User As String) Handles irc.UserLeft
        Try
            rtbOutput.SelectionColor = Color.Red
            MedClient.NotifyEz(cmbChannel.Text & " Notice:", "*** " & User & " has left the chat-room", 2)
            rtbOutput.AppendText("*** " & User & " has left the chat-room" & vbNewLine)
            lstUsers.Items.Remove("@" & User)
            lstUsers.Items.Remove(User)
            lstUsers.Update()
            If channel = cmbChannel.Text Then lstUsers.Items.Clear()
            rtbOutput.ScrollToCaret()
        Catch
        End Try
    End Sub

    Private Sub UserNickChangeEventDelegate(oldUser As String, newUser As String) Handles irc.UserNickChange
        Try
            rtbOutput.SelectionColor = Color.Blue
            'newUser = nuser
            MedClient.NotifyEz(cmbChannel.Text & " Notice:", "*** " & oldUser & " is now knows as " & newUser, 0)
            rtbOutput.AppendText("*** " & oldUser & " is now knows as " & newUser & vbNewLine)
            lstUsers.Items.Remove(oldUser)
            If oldUser = txtNick.Text Then
                txtNick.Text = newUser
                NickButton.Text = newUser
            End If
            If newUser <> "" Then lstUsers.Items.Add(newUser) : lstUsers.Update()
            rtbOutput.ScrollToCaret()
        Catch
        End Try
    End Sub

    Private Sub irc_userjoin(channel As String, User As String) Handles irc.UserJoined
        Try
            rtbOutput.SelectionColor = Color.Green
            MedClient.NotifyEz(cmbChannel.Text & " Notice:", "*** " & User & " has joined the chat-room", 0)
            rtbOutput.AppendText("*** " & User & " has joined the chat-room" & vbNewLine)
            If User.Trim <> txtNick.Text Then lstUsers.Items.Add(User.ToString.Trim) : lstUsers.Update()
            rtbOutput.ScrollToCaret()
            If User = txtNick.Text Then
                NickButton.Text = User
                NickButton.Enabled = True
            End If
        Catch
        End Try
    End Sub

    Private Sub irc_sendnotice(Channel As String, Message As String) Handles irc.NoticeMessage
        If MedPlay.VerifyForm("MedClient") And cmbChannel.Text = "#MedPlay" Then
            If MedClient.MuteNotification = False Then My.Computer.Audio.Play(MedExtra & "Resource\Music\notification.wav")
        End If

        Try
            Select Case True
                Case Message.Contains("This nickname is registered.")
                    Dim InputPsw As Object
                    InputPsw = InputBox("This nickname is registered, input the password", "Input your password...")
                    If InputPsw.trim = "" Then
                        Exit Sub
                    Else
                        NickPsw = InputPsw.trim
                        txtSend.Text = "/msg NickServ identify " & txtNick.Text & " " & NickPsw
                    End If
                    'irc.ServerPass = InputPsw
                    If txtSend.Text.Trim <> "" Then btnSend.PerformClick()
            End Select
            MedClient.NotifyEz(cmbChannel.Text & " Notice:", Message, 0)
            rtbOutput.SelectionColor = Color.Indigo
            rtbOutput.AppendText(Message & vbNewLine)
            rtbOutput.ScrollToCaret()
        Catch
        End Try
    End Sub

    Private Sub irc_ServerMessage(message As String) Handles irc.ServerMessage
        Try
            Select Case True
                Case message.Contains("Nick/channel is temporarily unavailable")
                    If Nick = "" Then Nick = txtNick.Text.Trim
                    UsedNick("Nick/channel is temporarily unavailable, select another", "")
                Case message.Substring(0, 2) = "+o"
                    Dim Op As String = Replace(message, "+o", "")
                    lstUsers.Items.Remove(Op.Trim)
                    lstUsers.Items.Add("@" & Op.Trim)
                Case message.Contains("End of /WHOIS list")
                    rtbOutput.SelectionColor = Color.DarkRed
                Case message = ("Connected!")
                    ConnAttemp = 0
                Case message.Contains("(Connection timed out)")
                    ConnAttemp += 1
                    If ConnAttemp < 4 Then
                        rtbOutput.SelectionColor = Color.Red
                        rtbOutput.AppendText(message & vbNewLine & "Connection attempt n°" & ConnAttemp & vbNewLine)
                        rtbOutput.ScrollToCaret()
                        irc = New IrcClient(cmbServer.Text, CInt(txtPort.Text))
                        irc.Nick = txtNick.Text
                        irc.Connect()
                    Else
                        rtbOutput.SelectionColor = Color.Red
                        rtbOutput.AppendText(message & vbNewLine & (ConnAttemp - 1) & " CONNECTION ATTEMPT! CONNECT MANUALLT." & vbNewLine)
                        rtbOutput.ScrollToCaret()
                        ResetOnError()
                    End If
                    Exit Sub
            End Select
            rtbOutput.AppendText(message & vbNewLine)
            rtbOutput.ScrollToCaret()
        Catch
        End Try
    End Sub

    Public Sub nickttaken(Nick As String) Handles irc.NickTaken
        Try
            rtbOutput.SelectionColor = Color.Purple
            rtbOutput.AppendText("*** " & Nick & " already used" & vbNewLine)
            UsedNick(Nick & " already used" & vbCrLf & "Select a new Nick", "")
        Catch
        End Try
    End Sub

    Private Sub UsedNick(mxg As String, res As String)

        Dim ChangeNick As Object
        ChangeNick = InputBox(mxg, "Change Nick...", res)
        If ChangeNick.Trim <> "" And IsNumeric(ChangeNick.Trim) = False Then
            txtNick.Text = ChangeNick
            'NickButton.Text = "&" & txtNick.Text

            If btnConnect.Text = "&Connect" Then
                txtPort.Enabled = True
                cmbServer.Enabled = True
                txtNick.Enabled = True
                btnConnect.PerformClick()
            ElseIf btnConnect.Text = "&Disconnect" Then
                txtSend.Text = "/nick " & ChangeNick
                btnSend.PerformClick()
            End If

        End If
    End Sub

    Private Sub irc_UpdateUsers(Channel As String, userlist() As String) Handles irc.UpdateUsers
        'lstUsers.Items.Clear()

        For Each s As String In userlist
            If Array.IndexOf(userlist, s) = 0 Then
                If s.Contains(txtSend.Text.Trim) Then
                    Dim at As String = ""
                    If s.Substring(0, 1) = "@" Then at = "@"
                    lstUsers.Items.Add(at & txtNick.Text.Trim)
                End If
            Else
                If Not lstUsers.Items.Contains(s) Then 'And Not lstUsers.Items.Contains("@" & s) Then
                    lstUsers.Items.Add(s.ToString)
                End If
            End If
        Next

        'lstUsers.Items.AddRange(userlist)
        lstUsers.Update()
    End Sub

    Private Sub SIrClient_FormClosed(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try
            If btnConnect.Text = "&Disconnect" Then irc.Disconnect()
            Dim risp = MsgBox("Do you want to Close UCI?", MsgBoxStyle.Information + MsgBoxStyle.YesNo)
            If risp = vbNo Then
                e.Cancel = True
            Else
                NickButton.Enabled = False
                irc.Disconnect()
            End If
        Catch
        End Try
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
                NickButton.Enabled = False
            Case LCase(txtSend.Text.StartsWith("/nick"))
                RawCommand = Replace(txtSend.Text, "/nick", "NICK")
                nuser = txtSend.Text.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)(1)

                'txtNick.Text = nuser
                'NickButton.Text = "&" & txtNick.Text

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

    Private Function cleanuser(user As String) As String
        If user.Substring(0, 1) = "@" Then
            cleanuser = user.Remove(0, 1)
        Else
            cleanuser = user
        End If
    End Function

    Private Sub SIrClient_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = gIcon

        If GlobalVar.UCInick = "" Then
            If MgrSetting.TextBox5.Text <> "" Then txtNick.Text = MgrSetting.TextBox5.Text Else : txtNick.Text = "Crappy" & rnd1.Next(500)
        Else
            txtNick.Text = GlobalVar.UCInick
            NickButton.Text = "&" & txtNick.Text
        End If

        If GlobalVar.UCIserver <> "" Then cmbServer.Text = GlobalVar.UCIserver
        If GlobalVar.UCIport <> "" Then txtPort.Text = GlobalVar.UCIport
        If GlobalVar.UCIchannel <> "" Then cmbChannel.Text = GlobalVar.UCIchannel

        irc_reload()
        If MyIp() <> "" Then Label3.Text = "MyIP: " & MyIp() Else Label3.Visible = False

        F1 = Me
        CenterForm()
        ColorizeForm()
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
            lstUsers.Items.Clear()
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

    Private Sub NickButton_Click(sender As Object, e As EventArgs) Handles NickButton.Click
        Dim ninp As Object = InputBox("Please specify a new nick", "Input a new nick...", "")
        If ninp.trim = "" Then Exit Sub 'Else txtNick.Text = ninp : NickButton.Text = "&" & txtNick.Text
        txtSend.Text = "/nick " & ninp.trim
        If txtSend.Text.Trim <> "" Then btnSend.PerformClick()
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

    Private Sub UserInfoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UserInfoToolStripMenuItem.Click
        txtSend.Text = "/whois " & cleanuser(lstUsers.SelectedItem.ToString.Trim)
        If txtSend.Text.Trim <> "" Then
            rtbOutput.SelectionColor = Color.DarkRed
            rtbOutput.AppendText(vbNewLine & "*** WHOIS " & lstUsers.SelectedItem.ToString.Trim & vbNewLine)
            rtbOutput.ScrollToCaret()
            btnSend.PerformClick()
        End If

    End Sub

    Private Sub PrivateMessageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrivateMessageToolStripMenuItem.Click
        If lstUsers.Items.Count <= 1 Or lstUsers.SelectedItem = txtNick.Text Then Return

        Dim pvtMessage As Object = InputBox("Private message to: " & cleanuser(lstUsers.SelectedItem.ToString), "Send a private message...", "")
        If pvtMessage.trim = "" Then Exit Sub
        txtSend.Text = "/msg " & cleanuser(lstUsers.SelectedItem.ToString) & " " & pvtMessage.trim
        If txtSend.Text.Trim <> "" Then btnSend.PerformClick()
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

    Private Sub lstUsers_MouseDown(sender As Object, e As MouseEventArgs) Handles lstUsers.MouseDown
        Try
            lstUsers.SelectedIndex = lstUsers.IndexFromPoint(e.X, e.Y)
            If cleanuser(lstUsers.SelectedItem.ToString) = txtNick.Text Then
                PrivateMessageToolStripMenuItem.Enabled = False
            Else
                PrivateMessageToolStripMenuItem.Enabled = True
            End If
        Catch
        End Try
    End Sub

End Class