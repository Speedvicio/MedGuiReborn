Imports System.IO

Public Class Mcheat
    Dim TypeCheat, CheatActive, LittleEndian, ByteLenght, CodeAdress, ByteValue, CheatName As String
    Dim TWriteRAM As Boolean = True

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

    Private Function AnalizeRAWCode(AdressCode As String) As String
        RadioButton1.Checked = True
        Dim delimiterChars As Char() = {"?", ":", "-", " "}
        Dim SplitAdress() As String = AdressCode.Trim.Split(delimiterChars)

        Select Case LCase(MedGuiR.DataGridView1.CurrentRow.Cells(6).Value)
            Case "gg", "sms"
                If SplitAdress(1).Length = 4 Then
                    TextBox1.Text = SplitAdress(0).Substring(2, 2) & SplitAdress(1).Substring(0, 2)
                    TextBox3.Text = SplitAdress(1).Substring(2, 2)
                Else
                    TextBox1.Text = SplitAdress(0)
                    TextBox3.Text = SplitAdress(1)
                End If

            Case "nes"
                If SplitAdress.Length > 2 Then
                    RadioButton5.Checked = True
                    TextBox1.Text = SplitAdress(0)
                    TextBox3.Text = SplitAdress(2) & " " & SplitAdress(1)
                Else
                    TextBox1.Text = SplitAdress(0)
                    TextBox3.Text = SplitAdress(1)
                End If
            Case Else
                TextBox1.Text = SplitAdress(0)
                TextBox3.Text = SplitAdress(1)
        End Select
        TextBox1.Text = TextBox1.Text.PadLeft(8, "0")
    End Function

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim Tlenght As Integer = 8
        If TWriteRAM = False Then Tlenght = 13 : Exit Sub
        TextBox1.Text = FormatText(TextBox1.Text, Tlenght)
        TextBox1.Select(TextBox1.Text.Length, 0)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If TWriteRAM = False Then AnalizeRAWCode(TextBox1.Text)

        SetCodeMode()
        Label11.Text = TypeCheat & CheatActive & ByteLenght & LittleEndian & CodeAdress & ByteValue & CheatName
        Label11.Left = (Me.Width / 2) - (Label11.Width / 2)
    End Sub

    Private Function FormatText(TextValue As String, MTLenght As Integer) As String
        TextValue = TextValue.PadLeft(MTLenght, "0")
        If TextValue.Length > MTLenght - 1 Then TextValue = TextValue.Remove(0, 1)
        FormatText = TextValue
    End Function

    Private Sub RadioButton6_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton6.CheckedChanged
        TypeWrite()
    End Sub

    Private Sub RadioButton7_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton7.CheckedChanged
        TypeWrite()
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        If TWriteRAM = False Then TextBox3.MaxLength = 5 : Exit Sub
        UpdateValue()
    End Sub

    Private Sub UpdateValue()
        TextBox3.Text = FormatText(TextBox3.Text, TextBox3.MaxLength)
        TextBox3.Select(TextBox3.Text.Length, 0)
    End Sub

    Private Sub TypeWrite()
        If RadioButton6.Checked = True Then
            TWriteRAM = True
            TextBox1.MaxLength = 9
            TextBox3.Enabled = True
            NumericUpDown1.Enabled = True
        Else
            TWriteRAM = False
            TextBox1.MaxLength = 14
            TextBox3.Enabled = False
            NumericUpDown1.Enabled = False
        End If

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
            TextBox2.Text = "[" & r_sha.Substring(0, 32) & "]"
        Else
            TextBox2.Text = "[" & r_md5 & "]"
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