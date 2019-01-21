Imports System.IO

Public Class Mcheat
    Dim TypeCheat, CheatActive, LittleEndian, ByteLenght, CodeAdress, ByteValue, CheatName, CheatConsole As String
    Dim TWriteRAM As Boolean = True
    Dim ControlCheatPresence As Integer
    Dim DoesentPrepare As Boolean = False

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

        Select Case CheatConsole
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
                If SplitAdress.Length > 1 Then TextBox3.Text = SplitAdress(1)

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
        PrepareCodeforMednafen()
    End Sub

    Private Sub PrepareCodeforMednafen()
        If TWriteRAM = False Then AnalizeRAWCode(TextBox1.Text)

        SetCodeMode()
        Label11.Text = TypeCheat & CheatActive & ByteLenght & LittleEndian & CodeAdress & ByteValue & CheatName
        Label11.Left = (Me.Width / 2) - (Label11.Width / 2)
    End Sub

    Private Function FormatText(TextValue As String, MTLenght As Integer) As String
        TextValue = TextValue.PadLeft(MTLenght, "0")
        If TextValue.Length > MTLenght Then TextValue = TextValue.Remove(0, 1)
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

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If ListBox1.SelectedIndex < 0 Then Exit Sub
        WorkingWithCheat(ListBox1.SelectedItem.ToString & vbLf & vbLf, "", False)
        If ListBox1.Items.Count = 0 Then
            WorkingWithCheat(vbLf & "[" & TextBox2.Text & "] " & Label7.Text.Trim & vbLf, "", False)
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If ListBox1.SelectedIndex < 0 Then Exit Sub
        PrepareCodeforMednafen()
        WorkingWithCheat(ListBox1.SelectedItem.ToString, Label11.Text, False)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        PrepareCodeforMednafen()

        Dim cheatpath As String = Path.Combine(MedGuiR.TextBox4.Text, "cheats\" & CheatConsole & ".cht")

        If File.Exists(cheatpath) = False Then
            File.Create(cheatpath).Dispose()
        End If

        ListBox1.Items.Clear()
        ParseCht()

        If ControlCheatPresence = 1 Then Exit Sub

        If ListBox1.Items.Count < 1 Then
            Dim ExNovo As String = vbLf & "[" & TextBox2.Text & "] " & Label7.Text.Trim & vbLf &
                Label11.Text.Trim & vbLf & vbLf

            'Dim myFile As New FileInfo(cheatpath)

            'If myFile.Length < 40 Then
            WorkingWithCheat("", ExNovo, True)
            'Else
            'WorkingWithCheat("", ExNovo, True)
            'End If

        Else
            If ListBox1.SelectedIndex < 0 Then DoesentPrepare = True
            ListBox1.SelectedIndex = ListBox1.Items.Count - 1
            If ListBox1.SelectedItem.ToString.Trim = Label11.Text.Trim Then Exit Sub
            WorkingWithCheat(ListBox1.SelectedItem.ToString.Trim, ListBox1.SelectedItem.ToString.Trim & vbLf & vbLf & Label11.Text.Trim & vbLf, False)
        End If
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.Items.Count < 1 Or ListBox1.SelectedIndex < 0 Then Exit Sub

        RadioButton6.Checked = True
        Dim RetrieveCheatValue() As String = ListBox1.SelectedItem.ToString.Split(" ")

        Select Case RetrieveCheatValue(0)
            Case "R"
                RadioButton1.Checked = True
            Case "A"
                RadioButton2.Checked = True
            Case "T"
                RadioButton3.Checked = True
            Case "S"
                RadioButton4.Checked = True
            Case "C"
                RadioButton5.Checked = True
        End Select

        Select Case RetrieveCheatValue(1)
            Case "A"
                CheckBox1.Checked = True
            Case "I"
                CheckBox1.Checked = False
        End Select

        NumericUpDown1.Value = Val(RetrieveCheatValue(2))

        Select Case RetrieveCheatValue(3)
            Case "B"
                CheckBox2.Checked = False
            Case "L"
                CheckBox2.Checked = True
        End Select

        TextBox1.MaxLength = RetrieveCheatValue(5).Length + 1
        TextBox1.Text = RetrieveCheatValue(5)

        TextBox3.MaxLength = RetrieveCheatValue(6).Length + 1
        TextBox3.Text = RetrieveCheatValue(6)

        Dim cheatname As String
        For i = 7 To RetrieveCheatValue.Length - 1
            cheatname += RetrieveCheatValue(i) & " "
        Next

        If cheatname Is Nothing = False Then TextBox4.Text = cheatname.Trim

        If DoesentPrepare = False Then PrepareCodeforMednafen()
    End Sub

    Private Sub Mcheat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ResetAll()
        CheatConsole = LCase(MedGuiR.DataGridView1.CurrentRow.Cells(6).Value)
        Select Case LCase(Path.GetExtension(percorso))
            Case ".zip", ".rar", ".7z"
                simple_extract()
            Case ".cue", ".toc", ".ccd", ".m3u"
        End Select

        filepath = percorso
        MD5CalcFile()

        MedGuiR.SetSpecialModule()
        If CheatConsole = "snes" And MedGuiR.tpce = "_faust" Then
            CheatConsole = "snes_faust"
            TextBox2.Text = r_sha.Substring(0, 32)
        Else
            TextBox2.Text = r_md5
        End If

        Label7.Text = Path.GetFileNameWithoutExtension(filepath)

        If File.Exists(Path.Combine(MedGuiR.TextBox4.Text, "cheats\" & CheatConsole & ".cht")) Then
            ParseCht()
        End If
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

    Private Sub ParseCht()
        ControlCheatPresence = 0
        Dim readText As String = File.ReadAllText(Path.Combine(MedGuiR.TextBox4.Text, "cheats\" & CheatConsole & ".cht"))
        Dim DeatilCheat() As String = readText.Split("[")

        Dim SplitCheat() As String
        For i = 0 To DeatilCheat.Length - 1
            If DeatilCheat(i).Contains(TextBox2.Text) Or
                DeatilCheat(i).Contains(Label7.Text) Then
                SplitCheat = DeatilCheat(i).Split(vbLf)
                Exit For
            End If
        Next

        If SplitCheat Is Nothing Then Exit Sub

        For i = 1 To SplitCheat.Length - 1
            If SplitCheat(i).Trim = "" Then Continue For
            ListBox1.Items.Add(SplitCheat(i).ToString)
            If SplitCheat(i).ToString.Trim = Label11.Text.Trim Then ControlCheatPresence = 1
        Next

    End Sub

    Private Sub ResetAll()
        ListBox1.Items.Clear()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        Label7.Text = ""
        RadioButton6.Checked = True
        RadioButton1.Checked = True
        CheckBox1.Checked = True
        CheckBox2.Checked = True
        NumericUpDown1.Value = 1
        Label11.Text = "Result Code"
        Label11.Left = (Me.Width / 2) - (Label11.Width / 2)
    End Sub

    Private Function WorkingWithCheat(OriginalString As String, StringChange As String, AppendTxt As Boolean)
        RadioButton1.Checked = True

        Dim txtcheat = My.Computer.FileSystem.ReadAllText(Path.Combine(MedGuiR.TextBox4.Text, "cheats\" & CheatConsole & ".cht"))

        If AppendTxt = False Then
            txtcheat = txtcheat.Replace(OriginalString, StringChange)
        Else
            txtcheat = StringChange
        End If
        My.Computer.FileSystem.WriteAllText(Path.Combine(MedGuiR.TextBox4.Text,
            "cheats\" & CheatConsole & ".cht"), txtcheat, AppendTxt)

        DoesentPrepare = False
        ListBox1.Items.Clear()
        ParseCht()
    End Function
End Class