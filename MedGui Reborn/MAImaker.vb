Imports System.IO

Public Class MAImaker

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        If KeyAscii < 48 And KeyAscii <> 24 And KeyAscii <> 8 Then
            KeyAscii = 0
        ElseIf KeyAscii > 57 And KeyAscii < 97 And KeyAscii <> 95 Then
            KeyAscii = 0
        ElseIf KeyAscii > 122 Then
            KeyAscii = 0
        End If

        e.KeyChar = Chr(KeyAscii)
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            CheckBox2.Enabled = True
            CheckBox3.Enabled = True
        Else
            CheckBox2.Enabled = True
            CheckBox3.Enabled = True
            CheckBox2.Checked = False
            CheckBox3.Checked = False
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim fdlg As OpenFileDialog = New OpenFileDialog()
        fdlg.Title = "Select Apple ][ file"
        fdlg.Filter = "All supported format (*.po,*.dsk,*.do,*.woz,*.d13)|*.po;*.dsk;*.do;*.woz;*.d13"
        fdlg.RestoreDirectory = True
        If fdlg.ShowDialog() = DialogResult.OK Then
            TextBox6.Text = Path.GetFileName(fdlg.FileName)
            TextBox2.Text = Path.GetFileNameWithoutExtension(TextBox6.Text)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ListBox1.Items.Count > 0 Then
            Dim SplitItem() As String
            For Each myItem In ListBox1.Items
                SplitItem = myItem.split("""")
                If SplitItem(0).Trim = TextBox1.Text.Trim Then
                    MsgBox("Disk ID already used, set another ID", MsgBoxStyle.Exclamation + vbOKOnly, "Disk ID used...")
                    TextBox1.Select()
                    Exit Sub
                End If
            Next
        End If

        Dim AdDisk As String = TextBox1.Text.Trim & " """ & TextBox2.Text.Trim & """ " & """" & TextBox6.Text.Trim & """ " & Convert.ToInt32(CheckBox4.Checked)
        If TextBox1.Text.Trim <> "" And TextBox6.Text.Trim <> "" And TextBox2.Text.Trim <> "" Then
            ListBox1.Items.Add(AdDisk)
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If IDassigned() IsNot Nothing Then ListBox2.Items.Add(IDassigned)
    End Sub

    Private Function IDassigned()
        Try
            Dim SplitItem() As String = ListBox1.SelectedItem.split("""")
            If ListBox2.Items.Count > 0 Then
                For Each myItem In ListBox2.Items
                    If SplitItem(0).Trim = myItem Then
                        MsgBox("Disk ID already assigned, set another ID", MsgBoxStyle.Exclamation + vbOKOnly, "Disk ID assigned...")
                        Exit Function
                    End If
                Next
            End If

            If ListBox3.Items.Count > 0 Then
                For Each myItem In ListBox3.Items
                    If SplitItem(0).Trim = myItem Then
                        MsgBox("Disk ID already assigned, set another ID", MsgBoxStyle.Exclamation + vbOKOnly, "Disk ID assigned...")
                        Exit Function
                    End If
                Next
            End If

            Return SplitItem(0).Trim
        Catch
        End Try
    End Function

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If IDassigned() IsNot Nothing Then ListBox3.Items.Add(IDassigned)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            ListBox2.Items.RemoveAt(ListBox2.SelectedIndex)
        Catch
        End Try
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Try
            ListBox3.Items.RemoveAt(ListBox2.SelectedIndex)
        Catch
        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If ListBox2.Items.Count > 0 Then
            For i = 0 To ListBox2.Items.Count - 1
                ListBox2.Items(i) = Replace(ListBox2.Items(i), "*", "")
            Next
        End If

        Try
            ListBox2.Items(ListBox2.SelectedIndex) = "*" & ListBox2.Items(ListBox2.SelectedIndex)
        Catch
        End Try
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If ListBox3.Items.Count > 0 Then
            For i = 0 To ListBox3.Items.Count - 1
                ListBox3.Items(i) = Replace(ListBox3.Items(i), "*", "")
            Next
        End If

        Try
            ListBox3.Items(ListBox3.SelectedIndex) = "*" & ListBox3.Items(ListBox3.SelectedIndex)
        Catch
        End Try
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim concatenate, resistance As String

        If ComboBox4.Text = "joystick" Or ComboBox4.Text = "gamepad" Then
            resistance = " " & NumericUpDown2.Value
        Else
            resistance = Nothing
        End If

        Dim a(ListBox1.Items.Count - 1) As String
        ListBox1.Items.CopyTo(a, 0)
        Dim text As String = String.Join(vbCrLf & "disk2.disks.", a)

        Dim b(ListBox2.Items.Count - 1) As String
        ListBox2.Items.CopyTo(b, 0)
        Dim text2 As String = "disk2.drive1.disks " & String.Join(" ", b)

        Dim c(ListBox3.Items.Count - 1) As String
        ListBox3.Items.CopyTo(c, 0)
        Dim text3 As String = "disk2.drive2.disks " & String.Join(" ", c)

        concatenate = "MEDNAFEN_SYSTEM_APPLE2" & vbCrLf & vbCrLf & "ram " & NumericUpDown1.Value & vbCrLf &
            "firmware " & ComboBox1.Text & vbCrLf & "romcard " & ComboBox2.Text & vbCrLf &
            "gameio " & ComboBox4.Text & resistance & vbCrLf & "gameio.resistance 93551 125615 149425 164980" & vbCrLf &
            "disk2.enable " & Convert.ToInt32(CheckBox1.Checked) & vbCrLf & "disk2.drive1.enable " & Convert.ToInt32(CheckBox2.Checked) & vbCrLf &
            "disk2.drive2.enable " & Convert.ToInt32(CheckBox3.Checked) & vbCrLf & "disk2.firmware " & ComboBox3.Text & vbCrLf & "disk2.disks." &
            text & vbCrLf & text2 & vbCrLf & text3

        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            SaveFileDialog1.Title = "Save MAI file in the same folder of reference files"
            My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, concatenate, False)
        End If
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Try
            ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
        Catch
        End Try
    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged
        If ComboBox4.Text = "joystick" Or ComboBox4.Text = "gamepad" Then
            NumericUpDown2.Enabled = True
        Else
            NumericUpDown2.Enabled = False
        End If
    End Sub
End Class