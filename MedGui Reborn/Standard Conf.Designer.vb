<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Standard_Conf
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
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

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla nell'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.S_PASS = New System.Windows.Forms.TextBox()
        Me.Label96 = New System.Windows.Forms.Label()
        Me.S_POR = New System.Windows.Forms.TextBox()
        Me.Label95 = New System.Windows.Forms.Label()
        Me.C_TIM = New System.Windows.Forms.TextBox()
        Me.Label94 = New System.Windows.Forms.Label()
        Me.M_CLI = New System.Windows.Forms.TextBox()
        Me.Label93 = New System.Windows.Forms.Label()
        Me.S_IDLE = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.S_MCMD = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.S_MISENDQ = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.S_MASENDQ = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(157, 220)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(70, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "&Make"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'S_PASS
        '
        Me.S_PASS.Location = New System.Drawing.Point(122, 90)
        Me.S_PASS.Name = "S_PASS"
        Me.S_PASS.Size = New System.Drawing.Size(105, 20)
        Me.S_PASS.TabIndex = 45
        '
        'Label96
        '
        Me.Label96.AutoSize = True
        Me.Label96.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label96.Location = New System.Drawing.Point(12, 93)
        Me.Label96.Name = "Label96"
        Me.Label96.Size = New System.Drawing.Size(53, 13)
        Me.Label96.TabIndex = 44
        Me.Label96.Text = "Password"
        '
        'S_POR
        '
        Me.S_POR.Location = New System.Drawing.Point(122, 62)
        Me.S_POR.Name = "S_POR"
        Me.S_POR.Size = New System.Drawing.Size(105, 20)
        Me.S_POR.TabIndex = 43
        Me.S_POR.Text = "4046"
        '
        'Label95
        '
        Me.Label95.AutoSize = True
        Me.Label95.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label95.Location = New System.Drawing.Point(12, 65)
        Me.Label95.Name = "Label95"
        Me.Label95.Size = New System.Drawing.Size(26, 13)
        Me.Label95.TabIndex = 42
        Me.Label95.Text = "Port"
        '
        'C_TIM
        '
        Me.C_TIM.Location = New System.Drawing.Point(122, 34)
        Me.C_TIM.Name = "C_TIM"
        Me.C_TIM.Size = New System.Drawing.Size(105, 20)
        Me.C_TIM.TabIndex = 41
        Me.C_TIM.Text = "5"
        '
        'Label94
        '
        Me.Label94.AutoSize = True
        Me.Label94.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label94.Location = New System.Drawing.Point(12, 37)
        Me.Label94.Name = "Label94"
        Me.Label94.Size = New System.Drawing.Size(76, 13)
        Me.Label94.TabIndex = 40
        Me.Label94.Text = "Conn. Timeout"
        '
        'M_CLI
        '
        Me.M_CLI.Location = New System.Drawing.Point(122, 6)
        Me.M_CLI.Name = "M_CLI"
        Me.M_CLI.Size = New System.Drawing.Size(105, 20)
        Me.M_CLI.TabIndex = 39
        Me.M_CLI.Text = "50"
        '
        'Label93
        '
        Me.Label93.AutoSize = True
        Me.Label93.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label93.Location = New System.Drawing.Point(12, 9)
        Me.Label93.Name = "Label93"
        Me.Label93.Size = New System.Drawing.Size(61, 13)
        Me.Label93.TabIndex = 38
        Me.Label93.Text = "Max Clients"
        '
        'S_IDLE
        '
        Me.S_IDLE.Location = New System.Drawing.Point(122, 116)
        Me.S_IDLE.Name = "S_IDLE"
        Me.S_IDLE.Size = New System.Drawing.Size(105, 20)
        Me.S_IDLE.TabIndex = 47
        Me.S_IDLE.Text = "30"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(12, 119)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 46
        Me.Label1.Text = "Idle Timeout"
        '
        'S_MCMD
        '
        Me.S_MCMD.Location = New System.Drawing.Point(122, 142)
        Me.S_MCMD.Name = "S_MCMD"
        Me.S_MCMD.Size = New System.Drawing.Size(105, 20)
        Me.S_MCMD.TabIndex = 49
        Me.S_MCMD.Text = "5242880"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(12, 145)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 13)
        Me.Label2.TabIndex = 48
        Me.Label2.Text = "Max Cmd Payload"
        '
        'S_MISENDQ
        '
        Me.S_MISENDQ.Location = New System.Drawing.Point(122, 168)
        Me.S_MISENDQ.Name = "S_MISENDQ"
        Me.S_MISENDQ.Size = New System.Drawing.Size(105, 20)
        Me.S_MISENDQ.TabIndex = 51
        Me.S_MISENDQ.Text = "262144"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label3.Location = New System.Drawing.Point(12, 171)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(83, 13)
        Me.Label3.TabIndex = 50
        Me.Label3.Text = "Min Send QSize"
        '
        'S_MASENDQ
        '
        Me.S_MASENDQ.Location = New System.Drawing.Point(122, 194)
        Me.S_MASENDQ.Name = "S_MASENDQ"
        Me.S_MASENDQ.Size = New System.Drawing.Size(105, 20)
        Me.S_MASENDQ.TabIndex = 53
        Me.S_MASENDQ.Text = "8388608"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label4.Location = New System.Drawing.Point(12, 197)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 13)
        Me.Label4.TabIndex = 52
        Me.Label4.Text = "Max Send QSize"
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
        'Standard_Conf
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(239, 252)
        Me.Controls.Add(Me.S_MASENDQ)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.S_MISENDQ)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.S_MCMD)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.S_IDLE)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.S_PASS)
        Me.Controls.Add(Me.Label96)
        Me.Controls.Add(Me.S_POR)
        Me.Controls.Add(Me.Label95)
        Me.Controls.Add(Me.C_TIM)
        Me.Controls.Add(Me.Label94)
        Me.Controls.Add(Me.M_CLI)
        Me.Controls.Add(Me.Label93)
        Me.Controls.Add(Me.Button1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.Name = "Standard_Conf"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Standard Conf"
        Me.ToolTip1.SetToolTip(Me, "Make a standard.conf for Mednafen-server")
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents S_PASS As System.Windows.Forms.TextBox
    Friend WithEvents Label96 As System.Windows.Forms.Label
    Friend WithEvents S_POR As System.Windows.Forms.TextBox
    Friend WithEvents Label95 As System.Windows.Forms.Label
    Friend WithEvents C_TIM As System.Windows.Forms.TextBox
    Friend WithEvents Label94 As System.Windows.Forms.Label
    Friend WithEvents M_CLI As System.Windows.Forms.TextBox
    Friend WithEvents Label93 As System.Windows.Forms.Label
    Friend WithEvents S_IDLE As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents S_MCMD As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents S_MISENDQ As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents S_MASENDQ As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class
