Public Class VirtualKbrd
    Public oldbutton As Control, oldsearch As String

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        If TextBox1.Text < " " Then
            TextBox1.Text = Mid(TextBox1.Text, 1, Len(TextBox1.Text) - 1 + 1)
        Else
            TextBox1.Text = Mid(TextBox1.Text, 1, Len(TextBox1.Text) - 1)
        End If

    End Sub

    Private Sub btnEnter_Click(sender As Object, e As EventArgs) Handles btnEnter.Click
        TextBox1.Text = TextBox1.Text & Environment.NewLine
    End Sub

    Private Sub btnShiftR_Click(sender As Object, e As EventArgs) Handles btnShiftL.Click, btnShiftR.Click
        If btnShiftR.FlatStyle = FlatStyle.Flat Then
            btnShiftR.FlatStyle = FlatStyle.Standard
            btnShiftL.FlatStyle = FlatStyle.Standard
            For Each ctl As Control In Me.Controls
                If (ctl.Name.StartsWith("btn")) Then
                    Dim bttn As Button = DirectCast(ctl, Button)
                    bttn.Text = bttn.Text.ToLower
                    btn1.Text = "1"
                    btn2.Text = "2"
                    btn3.Text = "3"
                    btn4.Text = "4"
                    btn5.Text = "5"
                    btn6.Text = "6"
                    btn7.Text = "7"
                    btn8.Text = "8"
                    btn9.Text = "9"
                    btn10.Text = "0"
                    btn11.Text = "-"
                    btn12.Text = "="
                    btn13.Text = "`"
                    btn14.Text = "\"
                    btn15.Text = "]"
                    btn16.Text = "["
                    btn29.Text = "'"
                    btn30.Text = ";"
                    btn28.Text = "/"
                    btn40.Text = "."
                    btn41.Text = ","
                End If
            Next
        ElseIf btnShiftR.FlatStyle = FlatStyle.Standard Then
            btnShiftL.FlatStyle = FlatStyle.Flat
            btnShiftR.FlatStyle = FlatStyle.Flat
            For Each ctl As Control In Me.Controls
                If (ctl.Name.StartsWith("btn")) Then
                    Dim bttn As Button = DirectCast(ctl, Button)
                    bttn.Text = bttn.Text.ToUpper
                    btn1.Text = "!"
                    btn2.Text = "@"
                    btn3.Text = "#"
                    btn4.Text = "$"
                    btn5.Text = "%"
                    btn6.Text = "^"
                    btn7.Text = "&"
                    btn8.Text = "*"
                    btn9.Text = "("
                    btn10.Text = ")"
                    btn11.Text = " _"
                    btn12.Text = "+"
                    btn13.Text = "~"
                    btn14.Text = "|"
                    btn15.Text = "}"
                    btn16.Text = "{"
                    btn29.Text = """"
                    btn30.Text = ":"
                    btn28.Text = "?"
                    btn40.Text = ">"
                    btn41.Text = "<"
                End If
            Next
        End If
    End Sub

    Private Sub btnCaps_Click(sender As Object, e As EventArgs) Handles btnCaps.Click
        If btnCaps.FlatStyle = FlatStyle.Flat Then
            btnCaps.FlatStyle = FlatStyle.Standard
            btnCaps.BackColor = Color.FromKnownColor(KnownColor.Control)
            For Each ctl As Control In Me.Controls
                If (ctl.Name.StartsWith("btn")) Then
                    Dim bttn As Button = DirectCast(ctl, Button)
                    bttn.Text = bttn.Text.ToLower
                    btn1.Text = "1"
                    btn2.Text = "2"
                    btn3.Text = "3"
                    btn4.Text = "4"
                    btn5.Text = "5"
                    btn6.Text = "6"
                    btn7.Text = "7"
                    btn8.Text = "8"
                    btn9.Text = "9"
                    btn10.Text = "0"
                    btn11.Text = "-"
                    btn12.Text = "="
                    btn13.Text = "`"
                    btn14.Text = "\"
                    btn15.Text = "]"
                    btn16.Text = "["
                    btn29.Text = "'"
                    btn30.Text = ";"
                    btn28.Text = "/"
                    btn40.Text = "."
                    btn41.Text = ","
                End If
            Next
        ElseIf btnCaps.FlatStyle = FlatStyle.Standard Then
            btnCaps.FlatStyle = FlatStyle.Flat
            btnCaps.BackColor = Color.LightGreen
            For Each ctl As Control In Me.Controls
                If (ctl.Name.StartsWith("btn")) Then
                    Dim bttn As Button = DirectCast(ctl, Button)
                    bttn.Text = bttn.Text.ToUpper
                    btn1.Text = "!"
                    btn2.Text = "@"
                    btn3.Text = "#"
                    btn4.Text = "$"
                    btn5.Text = "%"
                    btn6.Text = "^"
                    btn7.Text = "&"
                    btn8.Text = "*"
                    btn9.Text = "("
                    btn10.Text = ")"
                    btn11.Text = " _"
                    btn12.Text = "+"
                    btn13.Text = "~"
                    btn14.Text = "|"
                    btn15.Text = "}"
                    btn16.Text = "{"
                    btn29.Text = """"
                    btn30.Text = ":"
                    btn28.Text = "?"
                    btn40.Text = ">"
                    btn41.Text = "<"
                End If
            Next
        End If
    End Sub

    Private Sub btnTab_Click(sender As Object, e As EventArgs) Handles btnTab.Click
        TextBox1.Text = TextBox1.Text & "    "
    End Sub

    Private Sub btnSpace_Click(sender As Object, e As EventArgs) Handles btnSpace.Click
        TextBox1.Text = TextBox1.Text & " "
    End Sub

    Private Sub btn28_Click(sender As Object, e As EventArgs) Handles btn1.Click,
        btn10.Click, btn11.Click, btn12.Click, btn13.Click, btn14.Click,
        btn15.Click, btn16.Click, btn2.Click, btn29.Click, btn3.Click,
        btn30.Click, btn4.Click, btn40.Click, btn41.Click, btn5.Click,
        btn6.Click, btn7.Click, btn8.Click, btn9.Click, btnA.Click, btnB.Click,
        btnC.Click, btnD.Click, btnE.Click, btnF.Click, btnG.Click, btnH.Click,
        btnI.Click, btnJ.Click, btnK.Click, btnL.Click, btnM.Click, btnN.Click,
        btnO.Click, btnP.Click, btnQ.Click, btnR.Click, btnS.Click, btnT.Click,
        btnV.Click, btnU.Click, btnW.Click, btnX.Click, btnY.Click, btnZ.Click
        If btnShiftL.FlatStyle = FlatStyle.Flat Then
            TextBox1.Text = TextBox1.Text + sender.text
            btnShiftL.PerformClick()
        Else
            TextBox1.Text = TextBox1.Text + sender.text
        End If
    End Sub

    Private Sub VirtualKbrd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        F1 = Me
        CenterForm()
        ColorizeForm()
        MedGuiR.FormIsON = True
        oldbutton = btnQ
    End Sub

    Private Sub VirtualKbrd_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        MedGuiR.TextBox3.Text = TextBox1.Text
        MedGuiR.FNameToolStripTextBox.Text = TextBox1.Text
        If oldsearch <> TextBox1.Text Then MedGuiR.FindToolStripButton.PerformClick()
        MedGuiR.FormIsON = True
        oldsearch = TextBox1.Text
    End Sub

End Class