Public Class TestCPU
    Dim Mhz As Integer
    Dim CPUname As String
    Private Sub TestCPU_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Icon = gIcon
        F1 = Me
        CenterForm()

        Dim MyOBJ As Object
        Dim cpu As Object

        MyOBJ = GetObject("WinMgmts:").instancesof("Win32_Processor")
        For Each cpu In MyOBJ
            Mhz = Val(cpu.CurrentClockSpeed.ToString)
            CPUname = cpu.Name.ToString
        Next

        Label2.Text = CPUname.Trim
        Label4.Text = Mhz.ToString

        If Mhz <= 1500 Then
            Label6.Text = "Faust"
            Label7.Text = "Fast"
            Label9.Text = "Your CPU is CRAP"
            Label10.Text = "Your CPU is CRAP"
        ElseIf Mhz > 1500 And Mhz <= 2500 Then
            Label6.Text = "Faust"
            Label7.Text = "Standard"
            Label9.Text = "Your CPU is CRAP"
            Label10.Text = "Your CPU is CRAP"
        ElseIf Mhz > 2500 And Mhz <= 3300 Then
            Label6.Text = "bsnes"
            Label7.Text = "Standard"
            Label9.Text = "Standard"
            Label10.Text = "Your CPU is CRAP"
        Else
            Label6.Text = "bsnes"
            Label7.Text = "Standard"
            Label9.Text = "Standard"
            Label10.Text = "Standard"
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If Label7.Text = "Fast" Then
            MedGuiR.CheckBox1.Checked = True
        Else
            MedGuiR.CheckBox1.Checked = False
        End If

        If Label6.Text = "Faust" Then
            MedGuiR.CheckBox15.Checked = True
        Else
            MedGuiR.CheckBox15.Checked = False
        End If

    End Sub
End Class