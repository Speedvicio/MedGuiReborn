Imports System.IO
Imports System.Net

Public Class Mcheat
    Dim TypeCheat, CheatActive, LittleEndian, ByteLenght, CodeAdress, ByteValue, CheatName, CheatConsole, searchcheatcode As String
    Dim TWriteRAM As Boolean = True
    Dim linkcheat As Boolean
    Dim ControlCheatPresence As Integer
    Dim DoesentPrepare As Boolean = False

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        TextBox3.MaxLength = (NumericUpDown1.Value * 3) + 1
        UpdateValue()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        linkcheat = False
        DetectGameHacking()
        _link = "https://gamehacking.org/" & searchcheatcode
        open_link()
    End Sub

    Private Sub DetectGameHacking()
        searchcheatcode = ""

        Dim charcheats As String
        If linkcheat = False Then
            charcheats = "/"
        Else
            charcheats = "="
        End If

        Select Case CheatConsole
            Case "psx", "ss"
                searchcheatcode = "serial" & charcheats & GetSerial(rn)
            Case "pcfx"
                searchcheatcode = "search"
            Case "pce"
                If MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() = "TurboGrafx 16 (CD)" Then
                    searchcheatcode = "search"
                Else
                    searchcheatcode = "crc32" & charcheats & base_file
                End If
            Case Else
                searchcheatcode = "crc32" & charcheats & base_file
        End Select
    End Sub

    Private Function GetSerial(gamename As String) As String
        Dim splitrname() As String
        splitrname = gamename.Split("[")
        Return (Replace(splitrname(1), "]", ""))
    End Function

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        _link = "http://bsfree.shadowflareindustries.com/index.php"
        open_link()
    End Sub

    Private Function AnalizeRAWCode(AdressCode As String) As String
        'RadioButton1.Checked = True
        Dim delimiterChars As Char() = {"?", ":", "-", " ", "+"}
        Dim SplitAdress() As String = AdressCode.Trim.Split(delimiterChars)

        If SplitAdress.Length < 2 Then Exit Function

        Select Case CheatConsole
            Case "gg", "sms"
                'RadioButton1.Checked = True
                If SplitAdress.Length > 2 Then
                    RadioButton5.Checked = True
                    TextBox1.Text = SplitAdress(0)
                    TextBox3.Text = SplitAdress(2) & " " & SplitAdress(1)
                Else
                    RadioButton1.Checked = True
                    If SplitAdress(1).Length = 4 Then
                        TextBox1.Text = SplitAdress(0).Substring(2, 2) & SplitAdress(1).Substring(0, 2)
                        TextBox3.Text = SplitAdress(1).Substring(2, 2)
                    Else
                        TextBox1.Text = SplitAdress(0)
                        TextBox3.Text = SplitAdress(1)
                    End If
                End If

            Case "nes"
                If SplitAdress.Length > 2 Then
                    RadioButton5.Checked = True
                    TextBox1.Text = SplitAdress(0)
                    TextBox3.Text = SplitAdress(2) & " " & SplitAdress(1)
                Else
                    RadioButton1.Checked = True
                    TextBox1.Text = SplitAdress(0)
                    TextBox3.Text = SplitAdress(1)
                End If

            Case "lynx"
                If SplitAdress.Length > 2 Then
                    'RadioButton5.Checked = True
                    TextBox1.Text = SplitAdress(0)
                    TextBox3.Text = SplitAdress(1) & " " & SplitAdress(2)
                Else
                    'RadioButton1.Checked = True
                    TextBox1.Text = SplitAdress(0)
                    TextBox3.Text = SplitAdress(1)
                End If

            Case "snes", "snes_faust"
                'RadioButton4.Checked = True
                TextBox1.Text = SplitAdress(0)
                TextBox3.Text = SplitAdress(1)

            Case Else
                'RadioButton1.Checked = True
                TextBox1.Text = SplitAdress(0)
                If SplitAdress.Length > 1 Then
                    NumericUpDown1.Value = Math.Ceiling(SplitAdress(1).Length / 2)
                    TextBox3.Text = SplitAdress(1)
                    UpdateValue()
                End If

        End Select
        TextBox1.Text = TextBox1.Text.PadLeft(8, "0")
#Disable Warning BC42105 ' La funzione 'AnalizeRAWCode' non restituisce un valore in tutti i percorsi del codice. È possibile che in fase di esecuzione venga restituita un'eccezione dovuta a un riferimento Null quando viene usato il risultato.
    End Function

#Enable Warning BC42105 ' La funzione 'AnalizeRAWCode' non restituisce un valore in tutti i percorsi del codice. È possibile che in fase di esecuzione venga restituita un'eccezione dovuta a un riferimento Null quando viene usato il risultato.

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
        TypeWrite()
        If TWriteRAM = False Then AnalizeRAWCode(TextBox1.Text)

        SetCodeMode()
        Label11.Text = TypeCheat & CheatActive & ByteLenght & LittleEndian & FormatText(LCase(CodeAdress), 8) & FormatText(LCase(ByteValue), TextBox3.MaxLength) & CheatName
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
        TextBox3.Text = FormatText(TextBox3.Text, TextBox3.MaxLength - 1)
        TextBox3.Select(TextBox3.Text.Length, 0)
    End Sub

    Private Sub TypeWrite()
        If RadioButton6.Checked = True Then
            TWriteRAM = True
            'TextBox1.MaxLength = 9
            TextBox3.Enabled = True
            NumericUpDown1.Enabled = True
        Else
            TWriteRAM = False
            'TextBox1.MaxLength = 14
            TextBox3.Enabled = False
            NumericUpDown1.Enabled = False
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If ListBox1.SelectedIndex < 0 Then Exit Sub
        WorkingWithCheat(ListBox1.SelectedItem.ToString, "", False)
        If ListBox1.Items.Count = 0 Then
            WorkingWithCheat("[" & ComboBox1.Text & "] " & Label7.Text.Trim, "", False)
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If ListBox1.SelectedIndex < 0 Then Exit Sub
        PrepareCodeforMednafen()
        WorkingWithCheat(ListBox1.SelectedItem.ToString, Label11.Text, False)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        AddCheat()
    End Sub

    Private Sub AddCheat()
        TWriteRAM = True
        PrepareCodeforMednafen()

        Dim cheatpath As String = Path.Combine(ExtractPath("path_cheat"), CheatConsole & ".cht")

        If File.Exists(cheatpath) = False Then
            File.Create(cheatpath).Dispose()
        End If

        ListBox1.Items.Clear()
        ParseCht()

        If ControlCheatPresence = 1 Then Exit Sub

        If ListBox1.Items.Count < 1 Then
            Dim ExNovo As String = "[" & ComboBox1.Text & "] " & Label7.Text.Trim & vbLf & Label11.Text.Trim
            WorkingWithCheat("", ExNovo, True)
        Else
            If ListBox1.SelectedIndex < 0 Then DoesentPrepare = True
            ListBox1.SelectedIndex = ListBox1.Items.Count - 1
            If ListBox1.SelectedItem.ToString.Trim = Label11.Text.Trim Then Exit Sub
            WorkingWithCheat(ListBox1.SelectedItem.ToString.Trim, ListBox1.SelectedItem.ToString.Trim & vbLf & vbLf & Label11.Text.Trim, False)
        End If
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.Items.Count < 1 Or ListBox1.SelectedIndex < 0 Then Exit Sub

        'RadioButton6.Checked = True
        Try
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
                    'If CheatConsole = "ss" Then
                    'CheckBox2.Checked = False
                    'Else
                    CheckBox2.Checked = True
                    'End If
            End Select

            'TextBox1.MaxLength = RetrieveCheatValue(5).Length + 1
            TextBox1.Text = RetrieveCheatValue(5)

            TextBox3.MaxLength = RetrieveCheatValue(6).Length + 1
            TextBox3.Text = RetrieveCheatValue(6)

            Dim cheatname As String
            For i = 7 To RetrieveCheatValue.Length - 1
                cheatname += RetrieveCheatValue(i) & " "
            Next

            If cheatname Is Nothing = False Then TextBox4.Text = cheatname.Trim

            If DoesentPrepare = False Then PrepareCodeforMednafen()
        Catch
            MsgBox("There seems to be a formatting problem with selected code, try to remove it.")
        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If File.Exists(Path.Combine(ExtractPath("path_cheat"), CheatConsole & ".cht")) Then
            ListBox1.Items.Clear()
            ParseCht()
        End If
    End Sub

    Private Sub Mcheat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = gIcon
        F1 = Me
        CenterForm()
        ColorizeForm()

        ResetAll()

        If Val(Environment.OSVersion.Version.ToString) >= 6 Then
            Button5.Enabled = True
        Else
            Button5.Enabled = False
        End If

        CheatConsole = LCase(MedGuiR.DataGridView1.CurrentRow.Cells(6).Value)
        Select Case LCase(Path.GetExtension(percorso))
            Case ".zip", ".rar", ".7z"
                simple_extract()
            Case ".cue", ".toc", ".ccd", ".m3u", ".zst"
                Dim savetype As String
                Select Case CheatConsole
                    Case "psx"
                        savetype = ".mcr"
                    Case "ss"
                        savetype = ".bkr"
                    Case Else
                        savetype = ".sav"
                End Select
                For Each foundFile As String In My.Computer.FileSystem.GetFiles(ExtractPath("path_sav"))
                    If foundFile.Contains(Path.GetFileNameWithoutExtension(percorso)) And
                        foundFile.Contains(savetype) Then
                        Dim Splitmcr() As String
                        Splitmcr = foundFile.Split(".")
                        'TextBox2.Text = Splitmcr(1)
                        Dim correctmd5 As Integer
                        For z = 0 To Splitmcr.Length - 1
                            If Splitmcr(z).Length = 32 Then
                                correctmd5 = z
                                Exit For
                            End If
                        Next
                        Dim exist As Boolean = False
                        If ComboBox1.Items.Count > 0 Then
                            For i = 0 To ComboBox1.Items.Count - 1
                                If ComboBox1.Items(i) = Splitmcr(correctmd5) Then
                                    exist = True
                                    Exit For
                                End If
                            Next
                        End If
                        If exist = False Then ComboBox1.Items.Add(Splitmcr(correctmd5))
                    End If
                Next
                filepath = percorso
                GoTo skiphash
        End Select

        filepath = percorso

        Select Case CheatConsole
            Case "nes"
                RemoveHeader(16)
            Case "lynx"
                RemoveHeader(64)
        End Select

        SHA1CalcFile()
        GetCRC32()

        MedGuiR.SetSpecialModule()
        If CheatConsole = "snes" And MedGuiR.tpce = "_faust" Then
            CheatConsole = "snes_faust"
            ComboBox1.Items.Add(r_sha.Substring(0, 32))
        Else
            ComboBox1.Items.Add(r_md5)
        End If

skiphash:

        Label7.Text = Path.GetFileNameWithoutExtension(filepath)

        If ComboBox1.Items.Count > 0 Then
            ComboBox1.SelectedIndex = 0
        Else
            MsgBox("Unable to detect hash for this game, I can't search any cheats", vbOKOnly + MsgBoxStyle.Information, "Undetected hash...")
            Me.Close()
        End If

        If File.Exists(Path.Combine(ExtractPath("path_cheat"), CheatConsole & ".cht")) Then
            ListBox1.Items.Clear()
            ParseCht()
        End If

        Dim cheatpath As String = Path.Combine(MedExtra & "Cheats\" & CheatConsole, Trim(Label7.Text) & "." & ComboBox1.Text.Trim & ".cht")
        If File.Exists(cheatpath) Then ReadImported(cheatpath)

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim attempt As Boolean = False

        If Directory.Exists(MedExtra & "Cheats\" & CheatConsole) Then
        Else
            Directory.CreateDirectory(MedExtra & "Cheats\" & CheatConsole)
        End If

        linkcheat = True
        DetectGameHacking()
        Dim cheatpath As String = Path.Combine(MedExtra & "Cheats\" & CheatConsole, Trim(Label7.Text) & "." & ComboBox1.Text.Trim & ".cht")

        Try

RETRY:      ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
            Dim prova As New CookieAwareWebClient
            prova.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko")
            Dim test = "https://gamehacking.org/getcodes.php?" & searchcheatcode & "&format=mednafen"
            prova.DownloadFile("https://gamehacking.org/getcodes.php?" & searchcheatcode & "&format=mednafen", cheatpath)

            If File.Exists(cheatpath) Then ReadImported(cheatpath)
            'Dim W As New WebClient
            'W.UseDefaultCredentials = False
            'W.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.3; Trident/7.0; rv:11.0) like Gecko")
            'W.DownloadFile("https://gamehacking.org/getcodes.php?" & searchcheatcode & "&format=mednafen",
            'Path.Combine(MedExtra & "Cheats\" & CheatConsole, Trim(Label7.Text) & "." & ComboBox1.Text.Trim & ".cht"))

            '//Attemp to bypass ddos protection of bitmitigate by restsharp 2.0 dll (fail)
            'get_data("https://gamehacking.org", "getcodes.php?" & searchcheatcode & "&format=mednafen")
        Catch ex As Exception
            If ex.ToString.Contains("(401)") Or ex.ToString.Contains("(403)") Or ex.ToString.Contains("(404)") Or ex.ToString.Contains("(500)") Then
                linkcheat = False
                DetectGameHacking()
                MessageBox.Show(
    "Unable to contact gamehacking.org" & vbCrLf &
    "Press ? to open the link via the default browser." & vbCrLf &
    "On opened page select Format: Mednafen: " & vbCrLf &
    "Download and import cheat OR:" & vbCrLf &
    "1) Press the view button" & vbCrLf &
    "2) Copy the codes" & vbCrLf &
    "3) Press the right mouse button on the frontend Downloaded code list and paste them",
    "Unable to contact gamehacking.org (due to Bitmitigate?)",
    MessageBoxButtons.OK,
    MessageBoxIcon.Information,
    MessageBoxDefaultButton.Button2,
    0, '0 is default otherwise use MessageBoxOptions Enum
    "https://gamehacking.org/" & searchcheatcode,
    "keyword")
            Else
                If ex.ToString.Contains("(403)") And attempt = False Then
                    attempt = True
                    GoTo RETRY
                Else
                    MsgBox(ex.ToString)
                End If
            End If
        End Try
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If ListBox2.SelectedIndices.Count <= 0 Then Exit Sub

        For Each cheats In ListBox2.SelectedItems
            If cheats.trim <> "" Then
                ListBox1.Items.Add(cheats)
                ListBox1.SelectedItem = cheats
                AddCheat()
            End If
        Next
    End Sub

    Private Sub PasteCheatsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteCheatsToolStripMenuItem.Click

        If Directory.Exists(MedExtra & "Cheats\" & CheatConsole) Then
        Else
            Directory.CreateDirectory(MedExtra & "Cheats\" & CheatConsole)
        End If

        Try
            Dim cheatpath As String = Path.Combine(MedExtra & "Cheats\" & CheatConsole, Trim(Label7.Text) & "." & ComboBox1.Text.Trim & ".cht")
            File.WriteAllLines(cheatpath, Clipboard.GetText.Split(New String() {vbCrLf}, StringSplitOptions.RemoveEmptyEntries))
            ReadImported(cheatpath)
        Catch
        Finally
            Clipboard.Clear()
        End Try
    End Sub

    Private Sub PasteContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles PasteContextMenuStrip1.Opening
        If Clipboard.ContainsData(DataFormats.Text) = False Then
            PasteCheatsToolStripMenuItem.Enabled = False
        Else
            PasteCheatsToolStripMenuItem.Enabled = True
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim result As DialogResult = OpenFileDialog1.ShowDialog()
        If result = Windows.Forms.DialogResult.OK Then

            Dim path As String = OpenFileDialog1.FileName
            Try
                Dim text As String = File.ReadAllText(path)
                Dim tsplit() As String = text.Split(vbLf)

                For i = 0 To tsplit.Length - 1
                    If tsplit(i).Contains("[") Then
                        ReadImported(path)
                        Exit For
                    End If
                Next
            Catch ex As Exception
            End Try
        End If

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged

        If File.Exists(Path.Combine(ExtractPath("path_cheat"), CheatConsole & ".cht")) Then
            ListBox1.Items.Clear()
            ParseCht()
        End If

        If ListBox1.Items.Count < 1 Then Exit Sub

        Dim FilterCheat As String

        If ComboBox2.Text = "Enabled" Then
            FilterCheat = "A"
        ElseIf ComboBox2.Text = "Disabled" Then
            FilterCheat = "I"
        Else
            Exit Sub
        End If

        Dim splitindex() As String
        For i = ListBox1.Items.Count To 0 Step -1
            If i = 0 Then Exit For
            splitindex = ListBox1.Items(i - 1).ToString.Split(" ")
            If splitindex(1) <> FilterCheat Then
                ListBox1.Items.RemoveAt(i - 1)
            End If
        Next

    End Sub

    Private Sub ReadImported(cheatpath As String)
        ListBox2.Items.Clear()
        Dim oFile As File
        Dim oRead As StreamReader

        Try
            Dim linecheat As Integer = File.ReadAllLines(cheatpath).Length

#Disable Warning BC42025 ' L'accesso del membro condiviso, del membro costante, del membro di enumerazione o del tipo nidificato verrà effettuato tramite un'istanza. L'espressione di qualificazione non verrà valutata.
            oRead = oFile.OpenText(cheatpath)
#Enable Warning BC42025 ' L'accesso del membro condiviso, del membro costante, del membro di enumerazione o del tipo nidificato verrà effettuato tramite un'istanza. L'espressione di qualificazione non verrà valutata.

            While oRead.Peek <> -1
                Dim cheatcontainer As String = oRead.ReadLine().Trim
                If cheatcontainer.Contains("Can't find a game with the CRC32") Or linecheat < 2 Then
                    MsgBox(cheatcontainer, vbOKOnly + MsgBoxStyle.Information, "Cheat not available...")
                    oRead.Close()
                    File.Delete(cheatpath)
                    Exit Sub
                ElseIf cheatcontainer.Contains("[") Or cheatcontainer = Nothing Then
                    Continue While
                Else
                    ListBox2.Items.Add(cheatcontainer)
                End If
            End While
        Catch ex As Exception
        Finally
            oRead.Close()
        End Try
    End Sub

    Public Function RemoveHeader(rembyte As Integer)

        Dim backPath As String = Path.Combine(MedExtra, "RomTemp\" & Path.GetFileName(filepath) & "_back")
        Dim WithHeader() As Byte = My.Computer.FileSystem.ReadAllBytes(filepath)

        Dim fs As FileStream
        fs = New FileStream(backPath, FileMode.Create)
        fs.Write(WithHeader, rembyte, WithHeader.Length - rembyte)
        fs.Close()
        filepath = backPath
#Disable Warning BC42105 ' La funzione 'RemoveHeader' non restituisce un valore in tutti i percorsi del codice. È possibile che in fase di esecuzione venga restituita un'eccezione dovuta a un riferimento Null quando viene usato il risultato.
    End Function

#Enable Warning BC42105 ' La funzione 'RemoveHeader' non restituisce un valore in tutti i percorsi del codice. È possibile che in fase di esecuzione venga restituita un'eccezione dovuta a un riferimento Null quando viene usato il risultato.

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
        Dim readText As String = File.ReadAllText(Path.Combine(ExtractPath("path_cheat"), CheatConsole & ".cht"))
        Dim DeatilCheat() As String = readText.Split("[")

        Dim SplitCheat() As String

        For i = 0 To DeatilCheat.Length - 1
            If DeatilCheat(i).Contains(ComboBox1.Text) Or DeatilCheat(i).Contains(Label7.Text) Then
                If ComboBox1.Items.Count = 0 And DeatilCheat(i).Contains(cleanpsx(Label7.Text).Trim) Then
                    Dim SplitMd5() As String = DeatilCheat(i).Split("]")
                    ComboBox1.Text = SplitMd5(0)
                End If
                If DeatilCheat(i).Contains(vbLf) = False Then i += 1
                If DeatilCheat(i).Trim = ComboBox1.Text & "] " & Label7.Text Then Continue For
                If i > DeatilCheat.Length - 1 Then Exit For
                SplitCheat = DeatilCheat(i).Split(vbLf)

                If SplitCheat Is Nothing Then Continue For

                For z = 1 To SplitCheat.Length - 1
                    If SplitCheat(z).Trim = "" Then Continue For
                    ListBox1.Items.Add(SplitCheat(z).ToString)
                    If SplitCheat(z).ToString.Trim = Label11.Text.Trim Then ControlCheatPresence = 1
                Next
            End If
        Next

    End Sub

    Private Sub ResetAll()
        ListBox1.Items.Clear()
        TextBox1.Text = ""
        'TextBox2.Text = ""
        ComboBox1.Items.Clear()
        TextBox3.Text = ""
        TextBox4.Text = ""
        Label7.Text = ""
        RadioButton7.Checked = True
        RadioButton1.Checked = True
        CheckBox1.Checked = True
        CheckBox2.Checked = True
        NumericUpDown1.Value = 1
        Label11.Text = "Result Code"
        Label11.Left = (Me.Width / 2) - (Label11.Width / 2)
    End Sub

    Private Function WorkingWithCheat(OriginalString As String, StringChange As String, AppendTxt As Boolean)
        'RadioButton1.Checked = True

        Dim txtcheat = My.Computer.FileSystem.ReadAllText(Path.Combine(ExtractPath("path_cheat"), CheatConsole & ".cht"))

        If AppendTxt = False Then
            txtcheat = txtcheat.Replace(OriginalString, StringChange)
        Else
            txtcheat = StringChange
        End If
        My.Computer.FileSystem.WriteAllText(Path.Combine(ExtractPath("path_cheat"), CheatConsole & ".cht"), vbLf & txtcheat.Trim & vbLf, AppendTxt)

        DoesentPrepare = False
        ListBox1.Items.Clear()
        ParseCht()
#Disable Warning BC42105 ' La funzione 'WorkingWithCheat' non restituisce un valore in tutti i percorsi del codice. È possibile che in fase di esecuzione venga restituita un'eccezione dovuta a un riferimento Null quando viene usato il risultato.
    End Function

#Enable Warning BC42105 ' La funzione 'WorkingWithCheat' non restituisce un valore in tutti i percorsi del codice. È possibile che in fase di esecuzione venga restituita un'eccezione dovuta a un riferimento Null quando viene usato il risultato.

    Private Sub TextBox4_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox4.KeyDown
        If e.KeyCode = 219 Or e.KeyCode = 221 Then
            MsgBox("Chars [ and ] are not allowed!", vbOKOnly + MsgBoxStyle.Information, "Uncorrect chars...")
            e.SuppressKeyPress = True
        End If
    End Sub

End Class