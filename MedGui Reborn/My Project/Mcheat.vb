Imports System.IO

Public Class Mcheat
    Dim TypeCheat, CheatActive, LittleEndian, ByteLenght, CodeAdress, ByteValue, CheatName As String

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        TextBox3.MaxLength = (NumericUpDown1.Value * 2) + 1
        UpdateValue()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start("https://gamehacking.org/")
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Process.Start("http://bsfree.shadowflareindustries.com/index.php")
    End Sub

    Private Function AnalizeRAWCode(FirstSplit As String, SecondSplit As String)
        If consoles Then

        End If
    End Function

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        TextBox1.Text = FormatText(TextBox1.Text, 8)
        TextBox1.Select(TextBox1.Text.Length, 0)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        SetCodeMode()
        Label11.Text = TypeCheat & CheatActive & ByteLenght & LittleEndian & CodeAdress & ByteValue & CheatName
        Label11.Left = (Me.Width / 2) - (Label11.Width / 2)
    End Sub

    Private Function FormatText(TextValue As String, MTLenght As Integer) As String
        TextValue = TextValue.PadLeft(MTLenght, "0")
        If TextValue.Length > MTLenght - 1 Then TextValue = TextValue.Remove(0, 1)
        FormatText = TextValue
    End Function

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        UpdateValue()
    End Sub

    Private Sub UpdateValue()
        TextBox3.Text = FormatText(TextBox3.Text, TextBox3.MaxLength)
        TextBox3.Select(TextBox3.Text.Length, 0)
    End Sub

    Private Sub Mcheat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim mmodule As String = LCase(MedGuiR.DataGridView1.CurrentRow.Cells(6).Value)
        Select Case LCase(Path.GetExtension(percorso))
            Case ".zip", ".rar", ".7z"
                simple_extract()
            Case ".cue", ".toc", ".ccd", ".m3u"
        End Select

        filepath = percorso
        MD5CalcFile()

        MedGuiR.SetSpecialModule()
        If mmodule = "snes" And MedGuiR.tpce = "_faust" Then
            Label6.Text = "[" & r_sha.Substring(0, 32) & "]"
        Else
            Label6.Text = "[" & r_md5 & "]"
        End If

        Label7.Text = Path.GetFileNameWithoutExtension(filepath)
    End Sub

    Private Sub SetCodeMode()
        If RadioButton1.Checked = True Then
            TypeCheat = "R "
        ElseIf RadioButton2.Checked = True Then
            TypeCheat = "A "
        ElseIf RadioButton3.Checked = True Then
            TypeCheat = "T "
        ElseIf RadioButton4.Checked = True Then
            TypeCheat = "S "
        ElseIf RadioButton5.Checked = True Then
            TypeCheat = "C "
        End If

        If CheckBox1.Checked = True Then
            CheatActive = "A "
        Else
            CheatActive = "I "
        End If

        If CheckBox2.Checked = True Then
            LittleEndian = "L 0 "
        Else
            LittleEndian = "B 0 "
        End If

        ByteLenght = NumericUpDown1.Value.ToString.Trim & " "

        CodeAdress = TextBox1.Text.Trim & " "
        ByteValue = TextBox3.Text.Trim & " "
        CheatName = TextBox4.Text.Trim
    End Sub

    Private Sub Label11_DoubleClick(sender As Object, e As EventArgs) Handles Label11.DoubleClick
        If Label11.Text.Length > 0 And Label11.Text <> "Result Code" Then Clipboard.SetDataObject(Label11.Text)
    End Sub

End Class