Imports System.IO

Public Class CPM
    Dim palette, hexString As String, HexBR, HexBB, HexBG As Object

    Public Sub conv_col_pal()
        If (ColorDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            'Converte colore RGB in valore HEX
            Dim HexR, HexB, HexG As Object
            On Error GoTo ErrorHandler

            'R
            HexR = Hex(ColorDialog1.Color.R)
            If Len(HexR) < 2 Then HexR = "0" & HexR

            'Get Green Hex
            HexG = Hex(ColorDialog1.Color.G)
            If Len(HexG) < 2 Then HexG = "0" & HexG

            HexB = Hex(ColorDialog1.Color.B)
            If Len(HexB) < 2 Then HexB = "0" & HexB

            palette = HexB & HexG & HexR
        Else
            palette = Nothing
ErrorHandler:
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If MedGuiR.MainGrid.CurrentRow.Cells(5).Value() <> "Nintendo - Game Boy" Then
            MsgBox("You can make custom palette only for GB system (NO GBC)", vbCritical + vbOKOnly)
            Exit Sub
        End If

        Dim saveFileDialog1 As New SaveFileDialog()

        saveFileDialog1.Filter = "palette files (*.pal)|*.pal|All files (*.*)|*.*"
        saveFileDialog1.FilterIndex = 1
        saveFileDialog1.InitialDirectory = ExtractPath("path_palette")
        saveFileDialog1.RestoreDirectory = True

        If RadioButton1.Checked = True Then
            saveFileDialog1.FileName = MedGuiR.MainGrid.CurrentRow.Cells(6).Value()
        ElseIf RadioButton2.Checked = True Then
            saveFileDialog1.FileName = Path.GetFileNameWithoutExtension(MedGuiR.MainGrid.CurrentRow.Cells(4).Value())
        ElseIf RadioButton3.Checked = True Then
            saveFileDialog1.FileName = ""
        End If

        Dim RGB1 = Button1.Text & Button2.Text & Button3.Text & Button4.Text
        Dim RGB2 = Button9.Text & Button11.Text & Button10.Text & Button8.Text
        Dim RGB3 = Button13.Text & Button15.Text & Button14.Text & Button12.Text
        If GroupBox3.Enabled = False Then RGB2 = ""
        If GroupBox4.Enabled = False Then RGB3 = ""
        Dim RGB = RGB1 + RGB2 + RGB3

        Try
            If saveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                'If saveFileDialog1.FileName.Contains(".pal") = False Then saveFileDialog1.FileName = saveFileDialog1.FileName & ".pal"
                If Path.GetExtension(saveFileDialog1.FileName) <> ".pal" Then saveFileDialog1.FileName = saveFileDialog1.FileName & ".pal"
                Dim fs As New IO.FileStream(saveFileDialog1.FileName, IO.FileMode.Create)
                For x As Integer = 0 To RGB.Length - 1 Step 6
                    Dim ui As UInt32
                    ui = Convert.ToUInt32(RGB.Substring(x, 6), 16)
                    Dim b() As Byte = BitConverter.GetBytes(ui)
                    fs.Write(b, 0, b.Length - 1)
                Next
                fs.Dispose()
                fs.Close()
            End If
        Catch

        End Try

        Refresh_listbox()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs)
        Dim CPPath As OpenFileDialog = New OpenFileDialog()
        CPPath.Title = "Select a Custom Palette File"
        CPPath.InitialDirectory = ExtractPath("path_palette")
        CPPath.Filter = "palette files (*.pal)|*.pal|All files (*.*)|*.*"
        If CPPath.ShowDialog() = Windows.Forms.DialogResult.OK Then

            Dim dipal As Integer
            Dim infoReader As System.IO.FileInfo
            infoReader = My.Computer.FileSystem.GetFileInfo(CPPath.FileName)
            dipal = Val(infoReader.Length)

            If dipal < 12 Or dipal > 36 Then MsgBox("This is not a Game Boy compatible palette file!", vbCritical + MsgBoxStyle.OkOnly) : Exit Sub

            Using file As New IO.FileStream(CPPath.FileName, IO.FileMode.Open)
                Dim value As Integer = file.ReadByte()

                Do Until value = -1
                    hexString = hexString & (value.ToString("X2"))
                    value = file.ReadByte()
                Loop
            End Using

            convHexPal()
        End If

    End Sub

    Private Sub convHexPal()
        Load_Def_Pal()
        For i = 0 To hexString.Length - 1 Step 6
            Select Case i
                Case 0
                    Button1.BackColor = ColorTranslator.FromHtml("0x" & hexString.Substring(i, 6))
                    Button1.ForeColor = Button1.BackColor
                    HexBR = Hex(Button1.BackColor.R)
                    HexBG = Hex(Button1.BackColor.G)
                    HexBB = Hex(Button1.BackColor.B)
                    conv_col_butpal()
                    Button1.Text = palette
                Case 6
                    Button2.BackColor = ColorTranslator.FromHtml("0x" & hexString.Substring(i, 6))
                    Button2.ForeColor = Button2.BackColor
                    HexBR = Hex(Button2.BackColor.R)
                    HexBG = Hex(Button2.BackColor.G)
                    HexBB = Hex(Button2.BackColor.B)
                    conv_col_butpal()
                    Button2.Text = palette
                Case 12
                    Button3.BackColor = ColorTranslator.FromHtml("0x" & hexString.Substring(i, 6))
                    Button3.ForeColor = Button3.BackColor
                    HexBR = Hex(Button3.BackColor.R)
                    HexBG = Hex(Button3.BackColor.G)
                    HexBB = Hex(Button3.BackColor.B)
                    conv_col_butpal()
                    Button3.Text = palette
                Case 18
                    Button4.BackColor = ColorTranslator.FromHtml("0x" & hexString.Substring(i, 6))
                    Button4.ForeColor = Button4.BackColor
                    HexBR = Hex(Button4.BackColor.R)
                    HexBG = Hex(Button4.BackColor.G)
                    HexBB = Hex(Button4.BackColor.B)
                    conv_col_butpal()
                    Button4.Text = palette
                Case 24
                    Button9.BackColor = ColorTranslator.FromHtml("0x" & hexString.Substring(i, 6))
                    Button9.ForeColor = Button9.BackColor
                    HexBR = Hex(Button9.BackColor.R)
                    HexBG = Hex(Button9.BackColor.G)
                    HexBB = Hex(Button9.BackColor.B)
                    conv_col_butpal()
                    Button9.Text = palette
                    CheckBox1.Checked = True
                Case 30
                    Button11.BackColor = ColorTranslator.FromHtml("0x" & hexString.Substring(i, 6))
                    Button11.ForeColor = Button11.BackColor
                    HexBR = Hex(Button11.BackColor.R)
                    HexBG = Hex(Button11.BackColor.G)
                    HexBB = Hex(Button11.BackColor.B)
                    conv_col_butpal()
                    Button11.Text = palette
                Case 36
                    Button10.BackColor = ColorTranslator.FromHtml("0x" & hexString.Substring(i, 6))
                    Button10.ForeColor = Button10.BackColor
                    HexBR = Hex(Button10.BackColor.R)
                    HexBG = Hex(Button10.BackColor.G)
                    HexBB = Hex(Button10.BackColor.B)
                    conv_col_butpal()
                    Button10.Text = palette
                Case 42
                    Button8.BackColor = ColorTranslator.FromHtml("0x" & hexString.Substring(i, 6))
                    Button8.ForeColor = Button8.BackColor
                    HexBR = Hex(Button8.BackColor.R)
                    HexBG = Hex(Button8.BackColor.G)
                    HexBB = Hex(Button8.BackColor.B)
                    conv_col_butpal()
                    Button8.Text = palette
                Case 48
                    Button13.BackColor = ColorTranslator.FromHtml("0x" & hexString.Substring(i, 6))
                    Button13.ForeColor = Button13.BackColor
                    HexBR = Hex(Button13.BackColor.R)
                    HexBG = Hex(Button13.BackColor.G)
                    HexBB = Hex(Button13.BackColor.B)
                    conv_col_butpal()
                    Button13.Text = palette
                    CheckBox2.Checked = True
                Case 54
                    Button15.BackColor = ColorTranslator.FromHtml("0x" & hexString.Substring(i, 6))
                    Button15.ForeColor = Button15.BackColor
                    HexBR = Hex(Button15.BackColor.R)
                    HexBG = Hex(Button15.BackColor.G)
                    HexBB = Hex(Button15.BackColor.B)
                    conv_col_butpal()
                    Button15.Text = palette
                Case 60
                    Button14.BackColor = ColorTranslator.FromHtml("0x" & hexString.Substring(i, 6))
                    Button14.ForeColor = Button14.BackColor
                    HexBR = Hex(Button14.BackColor.R)
                    HexBG = Hex(Button14.BackColor.G)
                    HexBB = Hex(Button14.BackColor.B)
                    conv_col_butpal()
                    Button14.Text = palette
                Case 66
                    Button12.BackColor = ColorTranslator.FromHtml("0x" & hexString.Substring(i, 6))
                    Button12.ForeColor = Button12.BackColor
                    HexBR = Hex(Button12.BackColor.R)
                    HexBG = Hex(Button12.BackColor.G)
                    HexBB = Hex(Button12.BackColor.B)
                    conv_col_butpal()
                    Button12.Text = palette
            End Select

        Next
        hexString = ""
    End Sub

    Public Sub conv_col_butpal()
        On Error GoTo ErrorHandler

        If Len(HexBR) < 2 Then HexBR = "0" & HexBR
        If Len(HexBG) < 2 Then HexBG = "0" & HexBG
        If Len(HexBB) < 2 Then HexBB = "0" & HexBB

        palette = HexBB & HexBG & HexBR
ErrorHandler:

    End Sub

    Private Sub CPM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = gIcon
        Read_Resource()
        Load_Def_Pal()
        Refresh_listbox()
        F1 = Me
        CenterForm()
        ColorizeForm()
    End Sub

    Private Sub Load_Def_Pal()
        Button1.Text = "9BBC0F"
        Button1.BackColor = ColorTranslator.FromHtml("0x" & Button1.Text)
        Button1.ForeColor = Button1.BackColor
        Button2.Text = "8BAC0F"
        Button2.BackColor = ColorTranslator.FromHtml("0x" & Button2.Text)
        Button2.ForeColor = Button2.BackColor
        Button3.Text = "306230"
        Button3.BackColor = ColorTranslator.FromHtml("0x" & Button3.Text)
        Button3.ForeColor = Button3.BackColor
        Button4.Text = "0F380F"
        Button4.BackColor = ColorTranslator.FromHtml("0x" & Button4.Text)
        Button4.ForeColor = Button4.BackColor

        Button9.Text = ""
        Button9.BackColor = Color.Empty
        Button9.ForeColor = Color.Empty
        Button11.Text = ""
        Button11.BackColor = Color.Empty
        Button11.ForeColor = Color.Empty
        Button10.Text = ""
        Button10.BackColor = Color.Empty
        Button10.ForeColor = Color.Empty
        Button8.Text = ""
        Button8.BackColor = Color.Empty
        Button8.ForeColor = Color.Empty
        Button13.Text = ""
        Button13.BackColor = Color.Empty
        Button13.ForeColor = Color.Empty
        Button15.Text = ""
        Button15.BackColor = Color.Empty
        Button15.ForeColor = Color.Empty
        Button14.Text = ""
        Button14.BackColor = Color.Empty
        Button14.ForeColor = Color.Empty
        Button12.Text = ""
        Button12.BackColor = Color.Empty
        Button12.ForeColor = Color.Empty

        CheckBox1.Checked = False
        CheckBox2.Checked = False
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        Dim dipal As Integer
        Dim infoReader As System.IO.FileInfo
        infoReader = My.Computer.FileSystem.GetFileInfo(ExtractPath("path_palette") & ListBox1.SelectedItem)
        dipal = Val(infoReader.Length)

        If dipal < 12 Or dipal > 36 Then MsgBox("This is not a Game Boy compatible palette file!", vbCritical + MsgBoxStyle.OkOnly) : Exit Sub

        Using file As New IO.FileStream(ExtractPath("path_palette") & ListBox1.SelectedItem, IO.FileMode.Open)
            Dim value As Integer = file.ReadByte()

            Do Until value = -1
                hexString = hexString & (value.ToString("X2"))

                value = file.ReadByte()
            Loop
        End Using

        convHexPal()
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        Dim readText() As String = File.ReadAllLines(MedExtra & "\palettes\GB\" & ListBox2.SelectedItem)

        For Each s In readText
            hexString = Trim(Replace(s, " ", ""))
        Next

        If Len(hexString) < 24 Or Len(hexString) > 72 Then MsgBox("This is not a Game Boy compatible color-set" & vbCrLf _
            & "Use the RGB RGB RGB RGB hex format" & vbCrLf _
            & "Example: FFFFFF 52FF00 FF4200 000000", vbCritical + MsgBoxStyle.OkOnly) : Exit Sub

        convHexPal()
    End Sub

    Private Sub Refresh_listbox()
        ListBox1.Items.Clear()

        For Each binFile As String In Directory.GetFiles(ExtractPath("path_palette"), "*.pal")
            ListBox1.Items.Add(Path.GetFileName(binFile))
        Next

        ListBox2.Items.Clear()
        If Directory.Exists(MedExtra & "\palettes\GB\") Then
            For Each txtFile As String In Directory.GetFiles(MedExtra & "\palettes\GB\", "*.txt")
                ListBox2.Items.Add(Path.GetFileName(txtFile))
            Next
        Else
            Directory.CreateDirectory(MedExtra & "\palettes\GB\")
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Refresh_listbox()
    End Sub

    Private Sub Button6_Click_1(sender As Object, e As EventArgs) Handles Button6.Click
        If ListBox1.SelectedItem = "" Then Exit Sub
        System.IO.File.Delete(ExtractPath("path_palette") & ListBox1.SelectedItem)
        Refresh_listbox()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        conv_col_pal()
        If palette = Nothing Then Exit Sub
        Button1.BackColor = ColorDialog1.Color
        Button1.ForeColor = Button1.BackColor
        Button1.Text = palette
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        conv_col_pal()
        If palette = Nothing Then Exit Sub
        Button2.BackColor = ColorDialog1.Color
        Button2.ForeColor = Button2.BackColor
        Button2.Text = palette
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        conv_col_pal()
        If palette = Nothing Then Exit Sub
        Button3.BackColor = ColorDialog1.Color
        Button3.ForeColor = Button3.BackColor
        Button3.Text = palette
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        conv_col_pal()
        If palette = Nothing Then Exit Sub
        Button4.BackColor = ColorDialog1.Color
        Button4.ForeColor = Button4.BackColor
        Button4.Text = palette
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        conv_col_pal()
        If palette = Nothing Then Exit Sub
        Button9.BackColor = ColorDialog1.Color
        Button9.ForeColor = Button9.BackColor
        Button9.Text = palette
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        conv_col_pal()
        If palette = Nothing Then Exit Sub
        Button11.BackColor = ColorDialog1.Color
        Button11.ForeColor = Button11.BackColor
        Button11.Text = palette
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        conv_col_pal()
        If palette = Nothing Then Exit Sub
        Button10.BackColor = ColorDialog1.Color
        Button10.ForeColor = Button10.BackColor
        Button10.Text = palette
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        conv_col_pal()
        If palette = Nothing Then Exit Sub
        Button8.BackColor = ColorDialog1.Color
        Button8.ForeColor = Button8.BackColor
        Button8.Text = palette
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        conv_col_pal()
        If palette = Nothing Then Exit Sub
        Button13.BackColor = ColorDialog1.Color
        Button13.ForeColor = Button13.BackColor
        Button13.Text = palette
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        conv_col_pal()
        If palette = Nothing Then Exit Sub
        Button15.BackColor = ColorDialog1.Color
        Button15.ForeColor = Button15.BackColor
        Button15.Text = palette
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        conv_col_pal()
        If palette = Nothing Then Exit Sub
        Button14.BackColor = ColorDialog1.Color
        Button14.ForeColor = Button14.BackColor
        Button14.Text = palette
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        conv_col_pal()
        If palette = Nothing Then Exit Sub
        Button12.BackColor = ColorDialog1.Color
        Button12.ForeColor = Button12.BackColor
        Button12.Text = palette
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            GroupBox3.Enabled = True
        Else
            GroupBox3.Enabled = False
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            GroupBox4.Enabled = True
        Else
            GroupBox4.Enabled = False
        End If
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        _link = "https://tcrf.net/CGB_Bootstrap_ROM#Manual_Select_Palette_Configurations"
        open_link()
    End Sub

End Class