Public Class MJoyConfig
    Dim buttonjoypad, povjoypad As String, ArrTxt(11) As TextBox, yi As Integer

    Private Sub MJoyConfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = gIcon
        If MedGuiR.CheckBox16.Checked = True Then MedGuiR.TimerControlJoy.Stop()
        SetPad()
        InitJoy()
        MYJOYEX.dwSize = 64
        MYJOYEX.dwFlags = &HFF
        Timer1.Interval = 200
        Label13.Text = "Configure Joypad on port: " & MedGuiR.ComboBox6.Text

        ArrTxt(0) = Me.TextBox1
        ArrTxt(1) = Me.TextBox2
        ArrTxt(2) = Me.TextBox3
        ArrTxt(3) = Me.TextBox4
        ArrTxt(4) = Me.TextBox5
        ArrTxt(5) = Me.TextBox6
        ArrTxt(6) = Me.TextBox7
        ArrTxt(7) = Me.TextBox8
        ArrTxt(8) = Me.TextBox9
        ArrTxt(9) = Me.TextBox10
        ArrTxt(10) = Me.TextBox11
        ArrTxt(11) = Me.TextBox12

        F1 = Me
        CenterForm()
        ColorizeForm()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Enabled = True
        TextBox1.Text = ""
        yi = 1
        Timer1.Start()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox2.Enabled = True
        TextBox2.Text = ""
        yi = 2
        Timer1.Start()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox3.Enabled = True
        TextBox3.Text = ""
        yi = 3
        Timer1.Start()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TextBox4.Enabled = True
        TextBox4.Text = ""
        yi = 4
        Timer1.Start()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        TextBox5.Enabled = True
        TextBox5.Text = ""
        yi = 5
        Timer1.Start()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        TextBox6.Enabled = True
        TextBox6.Text = ""
        yi = 6
        Timer1.Start()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        TextBox7.Enabled = True
        TextBox7.Text = ""
        yi = 7
        Timer1.Start()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        TextBox8.Enabled = True
        TextBox8.Text = ""
        yi = 8
        Timer1.Start()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        TextBox9.Enabled = True
        TextBox9.Text = ""
        yi = 9
        Timer1.Start()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        TextBox10.Enabled = True
        TextBox10.Text = ""
        yi = 10
        Timer1.Start()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        TextBox11.Enabled = True
        TextBox11.Text = ""
        yi = 11
        Timer1.Start()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        TextBox12.Enabled = True
        TextBox12.Text = ""
        yi = 12
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            joyGetPosEx(MedGuiR.ComboBox6.Text, MYJOYEX)

            Dim bjoy As String = MYJOYEX.dwButtons.ToString
            Dim pjoy As String = (MYJOYEX.dwPOV / 100).ToString
            If bjoy <> "0" Then buttonjoypad = bjoy
            If pjoy <> "655.35" Then povjoypad = pjoy

            If ArrTxt(yi - 1).Enabled = True And ArrTxt(yi - 1).Text = "" Then
                If yi <= 4 Then
                    ArrTxt(yi - 1).Text = povjoypad
                Else
                    ArrTxt(yi - 1).Text = buttonjoypad
                End If

                If ArrTxt(yi - 1).Text <> "" Then
                    ArrTxt(yi - 1).Enabled = False
                    buttonjoypad = ""
                    povjoypad = ""
                    Timer1.Stop()
                    ControlInput()
                End If
            End If
        Catch
            Timer1.Stop()
            MsgBox("Unrecognized Joypad on port " & MedGuiR.ComboBox6.Text, vbOKOnly + vbCritical, "unrecognized Joypad")
        End Try
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        For i = 0 To 11
            ArrTxt(i).Text = ""
            ArrTxt(i).Enabled = False
        Next
    End Sub

    Private Sub MJoyConfig_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If MedGuiR.CheckBox16.Checked = True Then MedGuiR.TimerControlJoy.Start()
        JUP = TextBox1.Text
        JDOWN = TextBox2.Text
        JLEFT = TextBox3.Text
        JRIGHT = TextBox4.Text
        JSELECT = TextBox5.Text
        JSTART = TextBox6.Text
        JA = TextBox7.Text
        JX = TextBox8.Text
        JY = TextBox9.Text
        JB = TextBox10.Text
        JL = TextBox11.Text
        JR = TextBox12.Text
        RWIni()
        Timer1.Stop()
        RJoypadMini()
    End Sub

    Private Sub SetPad()
        TextBox1.Text = JUP
        TextBox2.Text = JDOWN
        TextBox3.Text = JLEFT
        TextBox4.Text = JRIGHT
        TextBox5.Text = JSELECT
        TextBox6.Text = JSTART
        TextBox7.Text = JA
        TextBox8.Text = JX
        TextBox9.Text = JY
        TextBox10.Text = JB
        TextBox11.Text = JL
        TextBox12.Text = JR
    End Sub

    Private Sub ControlInput()
        Dim cinput As String
        For i = 0 To ArrTxt.Length - 1
            cinput = ArrTxt(i).Text
            For x = 0 To ArrTxt.Length - 1
                If x <> i Then
                    If cinput = ArrTxt(x).Text Then
                        MsgBox("This joypad input value is already assigned to another button", vbOKOnly + vbExclamation, "Change input button")
                        ArrTxt(i).Text = ""
                        ArrTxt(i).Enabled = True
                        Timer1.Start()
                        Exit Sub
                    End If
                End If
            Next
        Next
    End Sub

End Class