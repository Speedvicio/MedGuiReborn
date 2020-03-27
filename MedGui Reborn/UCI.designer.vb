<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCI
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.btnConnect = New System.Windows.Forms.Button()
        Me.rtbOutput = New System.Windows.Forms.RichTextBox()
        Me.btnSend = New System.Windows.Forms.Button()
        Me.txtSend = New System.Windows.Forms.TextBox()
        Me.lstUsers = New System.Windows.Forms.ListBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.LabelPort = New System.Windows.Forms.Label()
        Me.LabelServer = New System.Windows.Forms.Label()
        Me.cmbChannel = New System.Windows.Forms.ComboBox()
        Me.txtNick = New System.Windows.Forms.TextBox()
        Me.txtPort = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmbServer = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnConnect
        '
        Me.btnConnect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnConnect.Location = New System.Drawing.Point(677, 10)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(75, 23)
        Me.btnConnect.TabIndex = 10
        Me.btnConnect.Text = "&Connect"
        Me.ToolTip1.SetToolTip(Me.btnConnect, "Connect on Channel")
        Me.btnConnect.UseVisualStyleBackColor = True
        '
        'rtbOutput
        '
        Me.rtbOutput.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtbOutput.BackColor = System.Drawing.Color.White
        Me.rtbOutput.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbOutput.Location = New System.Drawing.Point(12, 38)
        Me.rtbOutput.Name = "rtbOutput"
        Me.rtbOutput.ReadOnly = True
        Me.rtbOutput.Size = New System.Drawing.Size(599, 283)
        Me.rtbOutput.TabIndex = 9
        Me.rtbOutput.Text = ""
        '
        'btnSend
        '
        Me.btnSend.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSend.Location = New System.Drawing.Point(567, 329)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(44, 23)
        Me.btnSend.TabIndex = 8
        Me.btnSend.Text = "&Send"
        Me.ToolTip1.SetToolTip(Me.btnSend, "Send a Message")
        Me.btnSend.UseVisualStyleBackColor = True
        '
        'txtSend
        '
        Me.txtSend.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSend.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSend.Location = New System.Drawing.Point(12, 329)
        Me.txtSend.Name = "txtSend"
        Me.txtSend.Size = New System.Drawing.Size(518, 22)
        Me.txtSend.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.txtSend, "Text Send Message")
        '
        'lstUsers
        '
        Me.lstUsers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstUsers.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstUsers.FormattingEnabled = True
        Me.lstUsers.ItemHeight = 16
        Me.lstUsers.Location = New System.Drawing.Point(617, 41)
        Me.lstUsers.Name = "lstUsers"
        Me.lstUsers.Size = New System.Drawing.Size(135, 276)
        Me.lstUsers.Sorted = True
        Me.lstUsers.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.lstUsers, "List of User On Channel" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "- Double click on a nick to send a /msg to the specified" &
        " user")
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(12, 16)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(32, 13)
        Me.label2.TabIndex = 19
        Me.label2.Text = "Nick:"
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(394, 16)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(49, 13)
        Me.label1.TabIndex = 17
        Me.label1.Text = "Channel:"
        Me.ToolTip1.SetToolTip(Me.label1, "Double click to open Channel List on notepad")
        '
        'LabelPort
        '
        Me.LabelPort.AutoSize = True
        Me.LabelPort.Location = New System.Drawing.Point(309, 16)
        Me.LabelPort.Name = "LabelPort"
        Me.LabelPort.Size = New System.Drawing.Size(29, 13)
        Me.LabelPort.TabIndex = 16
        Me.LabelPort.Text = "Port:"
        '
        'LabelServer
        '
        Me.LabelServer.AutoSize = True
        Me.LabelServer.Location = New System.Drawing.Point(156, 15)
        Me.LabelServer.Name = "LabelServer"
        Me.LabelServer.Size = New System.Drawing.Size(41, 13)
        Me.LabelServer.TabIndex = 15
        Me.LabelServer.Text = "Server:"
        '
        'cmbChannel
        '
        Me.cmbChannel.FormattingEnabled = True
        Me.cmbChannel.Location = New System.Drawing.Point(449, 12)
        Me.cmbChannel.Name = "cmbChannel"
        Me.cmbChannel.Size = New System.Drawing.Size(131, 21)
        Me.cmbChannel.Sorted = True
        Me.cmbChannel.TabIndex = 21
        Me.cmbChannel.Text = "#mednafen"
        Me.ToolTip1.SetToolTip(Me.cmbChannel, "Select a Channel")
        '
        'txtNick
        '
        Me.txtNick.Location = New System.Drawing.Point(50, 13)
        Me.txtNick.Name = "txtNick"
        Me.txtNick.Size = New System.Drawing.Size(100, 20)
        Me.txtNick.TabIndex = 27
        Me.txtNick.Text = "Guest"
        Me.ToolTip1.SetToolTip(Me.txtNick, "Your Nick Name")
        '
        'txtPort
        '
        Me.txtPort.Location = New System.Drawing.Point(344, 13)
        Me.txtPort.Name = "txtPort"
        Me.txtPort.Size = New System.Drawing.Size(44, 20)
        Me.txtPort.TabIndex = 26
        Me.txtPort.Text = "6667"
        Me.ToolTip1.SetToolTip(Me.txtPort, "Server Port")
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(586, 10)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(25, 23)
        Me.Button1.TabIndex = 28
        Me.Button1.Text = "&+"
        Me.ToolTip1.SetToolTip(Me.Button1, "Add your Favourite Channels")
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.Location = New System.Drawing.Point(536, 329)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(25, 23)
        Me.Button2.TabIndex = 29
        Me.Button2.Text = "&G"
        Me.ToolTip1.SetToolTip(Me.Button2, "Send the Name of Selected Game From MedGuiR Grid")
        Me.Button2.UseVisualStyleBackColor = True
        '
        'ToolTip1
        '
        Me.ToolTip1.AutomaticDelay = 2000
        Me.ToolTip1.AutoPopDelay = 20000
        Me.ToolTip1.BackColor = System.Drawing.Color.DarkCyan
        Me.ToolTip1.ForeColor = System.Drawing.Color.LemonChiffon
        Me.ToolTip1.InitialDelay = 0
        Me.ToolTip1.IsBalloon = True
        Me.ToolTip1.ReshowDelay = 0
        Me.ToolTip1.ShowAlways = True
        '
        'cmbServer
        '
        Me.cmbServer.FormattingEnabled = True
        Me.cmbServer.Items.AddRange(New Object() {"irc.dal.net", "irc.efnet.org", "irc.freenode.net", "irc.gamesurge.net", "irc.irchighway.net", "irc.ircnet.org", "irc.quakenet.org", "irc.rizon.net", "irc.swiftirc.net", "irc.undernet.org", "irc.ustream.tv"})
        Me.cmbServer.Location = New System.Drawing.Point(203, 13)
        Me.cmbServer.Name = "cmbServer"
        Me.cmbServer.Size = New System.Drawing.Size(100, 21)
        Me.cmbServer.Sorted = True
        Me.cmbServer.TabIndex = 31
        Me.cmbServer.Text = "irc.freenode.net"
        Me.ToolTip1.SetToolTip(Me.cmbServer, "Server List")
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(617, 329)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 30
        Me.Label3.Text = "Label3"
        '
        'UCI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(764, 361)
        Me.Controls.Add(Me.cmbServer)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtNick)
        Me.Controls.Add(Me.txtPort)
        Me.Controls.Add(Me.cmbChannel)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.LabelPort)
        Me.Controls.Add(Me.LabelServer)
        Me.Controls.Add(Me.btnConnect)
        Me.Controls.Add(Me.rtbOutput)
        Me.Controls.Add(Me.btnSend)
        Me.Controls.Add(Me.txtSend)
        Me.Controls.Add(Me.lstUsers)
        Me.Name = "UCI"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ultra Crappy IRC"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents rtbOutput As System.Windows.Forms.RichTextBox
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents LabelPort As System.Windows.Forms.Label
    Private WithEvents LabelServer As System.Windows.Forms.Label
    Friend WithEvents cmbChannel As System.Windows.Forms.ComboBox
    Private WithEvents txtPort As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbServer As ComboBox
    Friend WithEvents txtNick As TextBox
    Public WithEvents btnConnect As Button
    Public WithEvents txtSend As TextBox
    Public WithEvents btnSend As Button
    Public WithEvents lstUsers As ListBox
End Class
