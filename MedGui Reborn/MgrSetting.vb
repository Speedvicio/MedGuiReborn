﻿Imports System.IO
Imports System.Net
Imports System.Net.NetworkInformation
Imports NAudio.Wave

Public Class MgrSetting
    Public tab_index, mxSet As Integer, colour, SpecificServer, per_conf_path_name As String, NetVerified, NoCheck, TPerC, isARGB As Boolean
    Dim MuFolder As FolderBrowserDialog = New FolderBrowserDialog()
    Private tPath, fPath, pPath, xcv As String, sPath As OpenFileDialog = New OpenFileDialog(), versave As Boolean
    Private ostr, ofsr, oxfsr, oyfsr As String

    Public Sub add_tabs()
        Select Case tab_index
            Case 7
                TabControl1.TabPages.Insert(10, TabPage19)
                TabControl1.SelectedTab = TabPage19
            Case 8
                TabControl1.TabPages.Insert(10, TabPage8)
                TabControl1.SelectedTab = TabPage8
            Case 9
                TabControl1.TabPages.Insert(10, TabPage9)
                TabControl1.SelectedTab = TabPage9
            Case 10
                TabControl1.TabPages.Insert(10, TabPage10)
                TabControl1.SelectedTab = TabPage10

                TextBox11.Enabled = True
                TextBox11.BackColor = Color.LightGoldenrodYellow
                Button7.Enabled = True

            Case 11
                TabControl1.TabPages.Insert(10, TabPage11)
                TabControl1.TabPages.Insert(11, TabPage12)
                TabControl1.SelectedTab = TabPage11
                TabControl1.SelectedTab = TabPage12

                If MedGuiR.CheckBox1.Checked = True Then
                    CheckBox70.Enabled = True
                    CheckBox71.Enabled = True
                    CheckBox28.Enabled = False
                    CheckBox31.Enabled = False
                    CheckBox33.Enabled = False
                    NumericUpDown14.Enabled = False
                    NumericUpDown25.Enabled = True
                    ComboBox15.Enabled = False
                Else
                    CheckBox70.Enabled = False
                    CheckBox71.Enabled = False
                    CheckBox28.Enabled = True
                    CheckBox31.Enabled = True
                    CheckBox33.Enabled = True
                    NumericUpDown14.Enabled = True
                    NumericUpDown25.Enabled = False
                    ComboBox15.Enabled = True
                End If

                TextBox12.Enabled = True
                TextBox18.Enabled = True
                TextBox12.BackColor = Color.LightGoldenrodYellow
                TextBox18.BackColor = Color.LightGoldenrodYellow
                Button8.Enabled = True
                Button4.Enabled = True

            Case 12
                TabControl1.TabPages.Insert(10, TabPage13)
                TabControl1.SelectedTab = TabPage13

                TextBox14.Enabled = True
                TextBox14.BackColor = Color.LightGoldenrodYellow
                Button10.Enabled = True

            Case 13
                TabControl1.TabPages.Insert(10, TabPage14)
                TabControl1.SelectedTab = TabPage14
            Case 14
                TabControl1.TabPages.Insert(10, TabPage15)
                TabControl1.SelectedTab = TabPage15
            Case 15
                TabControl1.TabPages.Insert(10, TabPage16)
                TabControl1.TabPages.Insert(11, TabPage17)
                TabControl1.SelectedTab = TabPage16
                TabControl1.SelectedTab = TabPage17
            Case 16
                TabControl1.TabPages.Insert(10, TabPage18)
                TabControl1.SelectedTab = TabPage18
                wswan_set()
            Case 17
                TabControl1.TabPages.Insert(10, TabPage20)
                TabControl1.TabPages.Insert(11, TabPage21)
                TabControl1.SelectedTab = TabPage20
                TabControl1.SelectedTab = TabPage21

                TextBox13.Enabled = True
                TextBox13.BackColor = Color.LightGoldenrodYellow
                Button9.Enabled = True

            Case 18
                TabControl1.TabPages.Insert(10, TabPage22)
                TabControl1.TabPages.Insert(11, TabPage23)
                TabControl1.SelectedTab = TabPage22
                TabControl1.SelectedTab = TabPage23

                TextBox15.Enabled = True
                TextBox16.Enabled = True
                TextBox17.Enabled = True
                TextBox15.BackColor = Color.LightGoldenrodYellow
                TextBox16.BackColor = Color.LightGoldenrodYellow
                TextBox17.BackColor = Color.LightGoldenrodYellow
                Button11.Enabled = True
                Button12.Enabled = True
                Button13.Enabled = True

            Case 19
                TabControl1.TabPages.Insert(10, TabPage25)
                TabControl1.TabPages.Insert(11, TabPage27)
                TabControl1.SelectedTab = TabPage25
                TabControl1.SelectedTab = TabPage27

                TextBox23.Enabled = True
                TextBox22.Enabled = True
                TextBox35.Enabled = True
                TextBox36.Enabled = True
                TextBox37.Enabled = True
                TextBox23.BackColor = Color.LightGoldenrodYellow
                TextBox22.BackColor = Color.LightGoldenrodYellow
                TextBox35.BackColor = Color.LightGoldenrodYellow
                TextBox36.BackColor = Color.LightGoldenrodYellow
                TextBox37.BackColor = Color.LightGoldenrodYellow
                Button29.Enabled = True
                Button28.Enabled = True
                Button42.Enabled = True
                Button43.Enabled = True
                Button44.Enabled = True

            Case 20
                TabControl1.TabPages.Insert(10, TabPage26)
                TabControl1.TabPages.Insert(11, TabPage32)
                TabControl1.SelectedTab = TabPage26
                TabControl1.SelectedTab = TabPage32
            Case 21
                TabControl1.TabPages.Insert(10, TabPage29)
                TabControl1.TabPages.Insert(11, TabPage30)
                TabControl1.SelectedTab = TabPage29
                TabControl1.SelectedTab = TabPage30

                TextBox4.Enabled = True
                TextBox19.Enabled = True
                TextBox20.Enabled = True
                TextBox21.Enabled = True
                TextBox24.Enabled = True
                TextBox25.Enabled = True
                TextBox19.BackColor = Color.LightGoldenrodYellow
                TextBox20.BackColor = Color.LightGoldenrodYellow
                TextBox21.BackColor = Color.LightGoldenrodYellow
                TextBox24.BackColor = Color.LightGoldenrodYellow
                TextBox25.BackColor = Color.LightGoldenrodYellow
                TextBox4.BackColor = Color.LightGoldenrodYellow
                Button16.Enabled = True
                Button17.Enabled = True
                Button23.Enabled = True
                Button24.Enabled = True
                Button26.Enabled = True
                Button25.Enabled = True

            Case 22
                TextBox10.Enabled = True
                TextBox10.BackColor = Color.LightGoldenrodYellow
                Button6.Enabled = True
        End Select

        If uWine = False Then
            TabControl1.SelectedTab = TabPage2
            TabControl1.SelectedTab = TabPage3
            TabControl1.SelectedTab = TabPage4
            TabControl1.SelectedTab = TabPage31
            TabControl1.SelectedTab = TabPage28
            TabControl1.SelectedTab = TabPage5
            TabControl1.SelectedTab = TabPage6
            TabControl1.SelectedTab = TabPage7
            TabControl1.SelectedTab = TabPage24
            TabControl1.SelectedTab = TabPage1
        End If

        EnableSetOptions()

        'StartControlBios()
    End Sub

    Public Sub EnableSetOptions()

        If consoles = "nes" Or consoles = "apple2" Then
            CheckBox13.Enabled = False
        Else
            CheckBox13.Enabled = True
        End If

        Dim OnOff As Boolean
        For i = 9380 To Val(vmedClear)
            Select Case i
                Case 9380
                    OnOff = False
                Case 9410
                    OnOff = True
                    CheckBox16.Visible = False
                Case 9420
                    CheckBox99.Visible = True
                    CheckBox98.Visible = True
                Case < 9430
                    'ComboBox44.Items.Remove("wheel")
                    'ComboBox44.Items.Remove("mission")
                    'ComboBox44.Items.Remove("dmission")
                    'ComboBox44.Items.Remove("keyboard")
                    'ComboBox45.Items.Remove("wheel")
                    'ComboBox45.Items.Remove("mission")
                    'ComboBox45.Items.Remove("dmission")
                    'ComboBox45.Items.Remove("keyboard")
                Case 9430
                    CheckBox100.Visible = True
                    CheckBox101.Visible = True
                    'ComboBox44.Items.Add("wheel")
                    'ComboBox44.Items.Add("mission")
                    'ComboBox44.Items.Add("dmission")
                    'ComboBox44.Items.Add("keyboard")
                    'ComboBox45.Items.Add("wheel")
                    'ComboBox45.Items.Add("mission")
                    'ComboBox45.Items.Add("dmission")
                    'ComboBox45.Items.Add("keyboard")
                    CheckBox101.Enabled = True
                Case 9440
                    CheckBox102.Enabled = True
                    ComboBox51.Enabled = True
                    CheckBox44.Visible = False
                    NumericUpDown26.Enabled = True
                Case 9460
                    Label132.Visible = True
                    Label135.Visible = True
                    Label136.Visible = True
                    Label137.Visible = True
                    Label138.Visible = True
                    Label139.Visible = True
                    Label140.Visible = True
                    Label141.Visible = True
                    ComboBox52.Visible = True
                    ComboBox55.Visible = True
                    ComboBox56.Visible = True
                    CheckBox103.Visible = True
                    NumericUpDown28.Visible = True
            End Select
        Next i

        Panel1.Visible = OnOff
        CheckBox75.Visible = OnOff
        CheckBox76.Visible = OnOff
        CheckBox77.Visible = OnOff
        TrackBar17.Visible = OnOff
        TrackBar18.Visible = OnOff
        TrackBar19.Visible = OnOff
        TrackBar20.Visible = OnOff
        Label120.Visible = OnOff
        Label121.Visible = OnOff
        Label122.Visible = OnOff
        Label123.Visible = OnOff
        CheckBox96.Visible = OnOff

        If OnOff = False Then
            ComboBox7.Items.Remove("goat")
            TabControl1.TabPages.Remove(TabPage28)
        End If

        If Val(vmedClear) < 9440 Then
            ComboBox49.Items.Remove("cs1ram16")
        End If

        If Val(vmedClear) < 9450 Then
            ComboBox3.Items.Remove("default")
            ComboBox3.Items.Remove("softfb")
            ComboBox45.Items.Remove("gun")
            ComboBox44.Items.Remove("gun")
        End If

        If Val(vmedClear) <= 9480 Then
            ComboBox3.Items.Remove("default")
            ComboBox3.Items.Remove("softfb")
            NumericUpDown27.Visible = False
            NumericUpDown28.Visible = False
            Label134.Visible = False
            Label135.Visible = False
            Label136.Visible = False
            Label137.Visible = False
            Label138.Visible = False
            Label139.Visible = False
            Label140.Visible = False
            Label141.Visible = False
            CheckBox103.Visible = False
            ComboBox55.Visible = False
            ComboBox56.Visible = False
            ComboBox45.Items.Remove("jpkeyboard")
            ComboBox44.Items.Remove("jpkeyboard")
        End If

        If Val(vmedClear) > 9480 Then
            ComboBox3.Items.Remove("overlay")
            ComboBox3.Items.Remove("sdl")
            ToolTip1.SetToolTip(ComboBox3, "Video output driver.
default - Default
Selects the default video driver. Currently, this is OpenGL for all platforms, but may change in the future if better platform-specific drivers are added.

opengl - OpenGL
All video-related Mednafen features are available with this video driver.

softfb - Software Blitting to Framebuffer
Slower with lower-quality scaling than OpenGL, but if you don't have hardware-accelerated OpenGL rendering, it will probably be faster than software OpenGL rendering. Bilinear interpolation not available. OpenGL shaders do not work with this output method, of course.")

            If detect_module("video.resolution_switch") = True Then
                Label142.Visible = True
                ComboBox57.Visible = True
            Else
                Label142.Visible = False
                ComboBox57.Visible = False
            End If

        End If

        If Val(vmedClear) > 12130 Then
            ComboBox49.Items.Remove("auto")
        Else
            ComboBox41.Items.Remove("blend")
            ComboBox41.Items.Remove("blend_rg")
        End If

        If Val(vmedClear) > 12220 Then
            NumericUpDown1.DecimalPlaces = 2
            NumericUpDown1.Increment = 0.01
            NumericUpDown2.Maximum = 15
            NumericUpDown1.Minimum = 0.25
            NumericUpDown2.Minimum = 0.25
            Label176.Enabled = True
            Label177.Enabled = True
            NumericUpDown42.Enabled = True
            ComboBox59.Enabled = True
            Label178.Enabled = True
            ComboBox62.Enabled = True
        Else
            NumericUpDown1.DecimalPlaces = 1
            NumericUpDown1.Increment = 1
            NumericUpDown2.Maximum = 1
            NumericUpDown1.Minimum = 0
            NumericUpDown2.Minimum = 0
            Label176.Enabled = False
            Label177.Enabled = False
            NumericUpDown42.Enabled = False
            ComboBox59.Enabled = False
            Label178.Enabled = False
            ComboBox62.Enabled = False
        End If

        If Val(vmedClear) > 12300 Then
            CheckBox65.Visible = False
            CheckBox116.Visible = True
            ComboBox62.Items.Clear()
            ComboBox62.Items.AddRange(New String() {"composite", "RGB", "rgb_tfr", "rgb_alt", "rgb_alt_tfr"})
            CheckBox102.Enabled = False
        Else
            TabControl1.TabPages.Remove(TabPage32)
            Dim resultIndex As Integer = ComboBox59.FindStringExact("phr256blend_auto512")
            ComboBox59.Items.RemoveAt(resultIndex)
            CheckBox116.Visible = False
            CheckBox65.Visible = True
            ComboBox62.Items.Clear()
            ComboBox62.Items.AddRange(New String() {"composite", "rgb", "rgb_alt1", "rgb_alt2"})
            CheckBox102.Enabled = True
        End If

        If Val(vmedClear) > 12610 Then
            ComboBox66.Enabled = True
            ComboBox66.Items.Clear()
            ComboBox66.Items.AddRange(New String() {"auto", "truecolor", "hicolor", "rgb565", "rgb555"})
        Else
            ComboBox66.Enabled = False
        End If

        If Val(vmedClear) > 12710 Then
            ComboBox67.Enabled = True
            ComboBox69.Enabled = True
            CheckBox110.Enabled = True
            ComboBox24.Items.Clear()
            ComboBox24.Items.AddRange(New String() {"anaglyph", "cscope", "hli", "sidebyside", "vli", "left", "right"})
        Else
            ComboBox67.Enabled = False
            ComboBox69.Enabled = False
            CheckBox110.Enabled = False
            ComboBox24.Items.Remove("left")
            ComboBox24.Items.Remove("right")
        End If

        If Val(vmedClear) > 13100 Then
            ComboBox37.Items.Add("popnmusic")
            ComboBox38.Items.Add("popnmusic")
            CheckBox109.Enabled = True
            CheckBox113.Enabled = True
            NumericUpDown49.Enabled = True
            ComboBox70.Enabled = True
            ComboBox62.Items.Clear()
            ComboBox62.Items.AddRange(New String() {"composite", "rgb", "rgb_tfr", "rgb_alt", "rgb_alt_trf", "rgb_qd", "rgb_qd_tfr", "rgb_video7"})
        Else
            ComboBox37.Items.Remove("popnmusic")
            ComboBox38.Items.Remove("popnmusic")
            ComboBox70.Enabled = False
            CheckBox113.Enabled = False
            CheckBox109.Enabled = False
            NumericUpDown49.Enabled = False
            ComboBox62.Items.Clear()
            ComboBox62.Items.AddRange(New String() {"composite", "RGB", "rgb_tfr", "rgb_alt", "rgb_alt_tfr"})
        End If

    End Sub

    Public Sub wswan_set()
        ComboBox27.Items.Clear()
        For x = 1 To 31
            ComboBox27.Items.Add(x)
        Next x
        For y = 1 To 12
            ComboBox28.Items.Add(y)
        Next y
        For z = 1900 To 2100
            ComboBox28.Items.Add(z)
        Next z
    End Sub

    Private Sub Setting_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Dim smsg
        If versave = False Then
            smsg = MsgBox("Mednafen " & Me.Text & " configuration are not saved, do you want to save it?", vbOKCancel + vbInformation, "Save Mednafen " & Me.Text)
            If smsg = vbOK Then parMednafen() : Mednafen_Save_setting() : MsgBox(Me.Text & " configuration saved", vbOKOnly + vbInformation) Else
        End If
    End Sub

    Private Sub Setting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TabControl1.DrawMode = TabDrawMode.OwnerDrawFixed

        Dim customCulture As Globalization.CultureInfo = CType(Threading.Thread.CurrentThread.CurrentCulture.Clone(), Globalization.CultureInfo)
        customCulture.NumberFormat.NumberDecimalSeparator = "."
        Threading.Thread.CurrentThread.CurrentCulture = customCulture

        Me.Icon = gIcon

        ComboBox6.Items.Clear()
        ComboBox6.Items.Add("0x0")
        ComboBox6.Items.AddRange(SupportedScrnSizes.GetSizesAsStrings)
        ListServer_reload()
        versave = False
        Read_Resource()
        'If My.Computer.Network.IsAvailable = True And NetVerified = False Then check_NetPlayServer()
        F1 = Me
        CenterForm()
        ColorizeForm()

        If rn IsNot Nothing Then
            Select Case True
                Case UCase(rn.Contains("SCES")), UCase(rn.Contains("SLES"))
                    CheckBox104.Enabled = True
            End Select
        End If

        ostr = ComboBox5.Text
        ofsr = ComboBox6.Text
        oxfsr = NumericUpDown6.Value
        oyfsr = NumericUpDown6.Value

        DisableBioses()
        PopulateAudio()
    End Sub

    Private Sub PopulateAudio()
        ComboBox68.Items.Clear()
        ComboBox68.Items.Add("default")

        'For n As Integer = -1 To WaveOut.DeviceCount - 1
        'Dim caps = WaveOut.GetCapabilities(n)
        'ComboBox68.Items.Add(caps.ProductName)
        'Next

        For Each dev In DirectSoundOut.Devices
            ComboBox68.Items.Add(dev.Description)
        Next
    End Sub

    Private Sub DisableBioses()
        For Each cont As Control In GroupBox19.Controls
            If TypeOf cont Is TextBox Then
                CType(cont, TextBox).Enabled = False
                CType(cont, TextBox).BackColor = Color.Empty
            ElseIf TypeOf cont Is Button Then
                CType(cont, Button).Enabled = False
            End If
        Next

        For Each cont As Control In GroupBox22.Controls
            If TypeOf cont Is TextBox Then
                CType(cont, TextBox).Enabled = False
                CType(cont, TextBox).BackColor = Color.Empty
            ElseIf TypeOf cont Is Button Then
                CType(cont, Button).Enabled = False
            End If
        Next

        Button40.Enabled = True
        Button27.Enabled = True
    End Sub

    Private Sub Button41_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button41.Click
        Standard_Conf.ShowDialog()
    End Sub

    Public Sub conv_col()
        If (ColorDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            'Converte colore RGB in valore HEX
            Dim HexR, HexB, HexG, HexA As Object

            On Error GoTo ErrorHandler

            If isARGB = True Then
                Dim coltas As String = "FF" & ColorDialog1.Color.R.ToHexString(2) & ColorDialog1.Color.G.ToHexString(2) & ColorDialog1.Color.B.ToHexString(2)
                Alpha.TextBox1.Text = coltas
                Alpha.PictureBox1.BackColor = ColorDialog1.Color
                Alpha.ShowDialog()
                ColorDialog1.Color = Alpha.ncolor
                HexA = Hex(ColorDialog1.Color.A)
                If Len(HexA) < 2 Then HexA = "0" & HexA
            Else
                HexA = Nothing
            End If

            'R
            HexR = Hex(ColorDialog1.Color.R)
            If Len(HexR) < 2 Then HexR = "0" & HexR

            'Get Green Hex
            HexG = Hex(ColorDialog1.Color.G)
            If Len(HexG) < 2 Then HexG = "0" & HexG

            HexB = Hex(ColorDialog1.Color.B)
            If Len(HexB) < 2 Then HexB = "0" & HexB

            colour = "0x" & HexA & HexR & HexG & HexB
        Else

ErrorHandler:

        End If
    End Sub

    Private Sub Label92_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Label92.Click
        isARGB = False
        conv_col()
        Label92.BackColor = ColorDialog1.Color
        Label92.ForeColor = Label92.BackColor
        Label92.Text = colour
    End Sub

    Private Sub Label91_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Label91.Click
        isARGB = False
        conv_col()
        If colour = Nothing Then Exit Sub
        Label91.BackColor = ColorDialog1.Color
        Label91.ForeColor = Label91.BackColor
        Label91.Text = colour
    End Sub

    Private Sub Label93_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Label93.Click
        isARGB = False
        conv_col()
        If colour = Nothing Then Exit Sub
        Label93.BackColor = ColorDialog1.Color
        Label93.ForeColor = Label93.BackColor
        Label93.Text = colour
    End Sub

    Private Sub Label86_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label86.Click
        isARGB = False
        conv_col()
        If colour = Nothing Then Exit Sub
        Label86.BackColor = ColorDialog1.Color
        Label86.ForeColor = Label86.BackColor
        Label86.Text = colour
    End Sub

    Private Sub Label87_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label87.Click
        isARGB = False
        conv_col()
        If colour = Nothing Then Exit Sub
        Label87.BackColor = ColorDialog1.Color
        Label87.ForeColor = Label87.BackColor
        Label87.Text = colour
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        tPath = "Select GBA Bios"
        fPath = "GBA Bios (*.bin)|*.bin"
        xPath()
        If pPath <> "" Then TextBox10.Text = pPath
    End Sub

    Private Sub xPath()
        sPath.Title = tPath
        sPath.InitialDirectory = ExtractPath("path_firmware")
        sPath.Filter = fPath
        If sPath.ShowDialog() = DialogResult.OK Then
            pPath = sPath.FileName
        Else
            pPath = ""
        End If
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        tPath = "Select Nes Game Genie Rom"
        fPath = "Game Genie Rom (*.rom;*.zip;*.nes)|*.rom;*.zip;*.nes"
        xPath()
        If pPath <> "" Then TextBox11.Text = pPath
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        tPath = "Select PCE-CD Bios"
        fPath = "Select PCE-CD Bios (*.zip;*.pce)|*.zip;*.pce"
        xPath()
        If pPath <> "" Then TextBox12.Text = pPath
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        tPath = "Select PCFX Bios"
        fPath = "Select PCFX Bios (*.zip;*.bin;*.rom)|*.zip;*.bin;*.rom"
        xPath()
        If pPath <> "" Then TextBox13.Text = pPath : controlbios()
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        tPath = "Select Sega CD Bios"
        fPath = "Select Sega CD Bios (*.zip;*.bin)|*.zip;*.bin"
        xPath()
        If pPath <> "" Then TextBox14.Text = pPath
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        tPath = "Select PSX Eur Bios"
        fPath = "Select PSX Eur Bios (*.zip;*.bin)|*.zip;*.bin"
        xPath()
        If pPath <> "" Then TextBox15.Text = pPath : controlbios()
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        tPath = "Select PSX Jap Bios"
        fPath = "Select PSX Jap Bios (*.zip;*.bin)|*.zip;*.bin"
        xPath()
        If pPath <> "" Then TextBox16.Text = pPath : controlbios()
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        tPath = "Select PSX Usa Bios"
        fPath = "Select PSX Usa Bios (*.zip;*.bin)|*.zip;*.bin"
        xPath()
        If pPath <> "" Then TextBox17.Text = pPath : controlbios()
    End Sub

    Private Sub Button28_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button28.Click
        tPath = "Select Saturn Jap Bios"
        fPath = "Select Saturn Jap Bios (*.zip;*.bin)|*.zip;*.bin"
        xPath()
        If pPath <> "" Then TextBox22.Text = pPath : controlbios()
    End Sub

    Private Sub Button43_Click(sender As Object, e As EventArgs) Handles Button43.Click
        tPath = "Select ST-V Eur Bios"
        fPath = "Select ST-V Eur Bios (*.zip;*.ic8)|*.zip;*.ic8"
        xPath()
        If pPath <> "" Then TextBox36.Text = pPath : controlbios()
    End Sub

    Private Sub Button42_Click(sender As Object, e As EventArgs) Handles Button42.Click
        tPath = "Select ST-V Usa Bios"
        fPath = "Select ST-V Usa Bios (*.zip;*.ic8)|*.zip;*.ic8"
        xPath()
        If pPath <> "" Then TextBox35.Text = pPath : controlbios()
    End Sub

    Private Sub Button44_Click(sender As Object, e As EventArgs) Handles Button44.Click
        tPath = "Select ST-V Jap Bios"
        fPath = "Select ST-V Jap Bios (*.zip;*.ic8)|*.zip;*.ic8"
        xPath()
        If pPath <> "" Then TextBox37.Text = pPath : controlbios()
    End Sub

    Private Sub Button29_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button29.Click
        tPath = "Select Saturn Usa/Eur Bios"
        fPath = "Select Saturn Usa/Eur Bios (*.zip;*.bin)|*.zip;*.bin"
        xPath()
        If pPath <> "" Then TextBox23.Text = pPath : controlbios()
    End Sub

    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        tPath = "Select KoF 95 ROM image"
        fPath = "Select KoF 95 ROM image (*.ic1)|*.ic1"
        xPath()
        If pPath <> "" Then TextBox2.Text = pPath
    End Sub

    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        tPath = "Select Ultraman ROM image"
        fPath = "Select Ultraman ROM image (*.ic1)|*.ic1"
        xPath()
        If pPath <> "" Then TextBox1.Text = pPath
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs)
        tPath = "Select Apple ][ Concatenated Bios"
        fPath = "Select Apple ][ Concatenated Bios (*.zip;*.rom)|*.zip;*.rom"
        xPath()
        If pPath <> "" Then TextBox4.Text = pPath : controlbios()
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs)
        tPath = "Select Apple ][ + Concatenated Bios"
        fPath = "Select Apple ][ + Concatenated Bios (*.zip;*.rom)|*.zip;*.rom"
        xPath()
        If pPath <> "" Then TextBox19.Text = pPath : controlbios()
    End Sub

    Private Sub Button23_Click_1(sender As Object, e As EventArgs)
        tPath = "Disk II Interface 13-Sector P5 Boot ROM, 341-0009"
        fPath = "Select Apple ][ + Concatenated Bios (*.zip;*.rom)|*.zip;*.rom"
        xPath()
        If pPath <> "" Then TextBox20.Text = pPath : controlbios()
    End Sub

    Private Sub Button24_Click(sender As Object, e As EventArgs)
        tPath = "Disk II Interface 13-Sector P6 Sequencer ROM, 341-0010"
        fPath = "Select Apple ][ + Concatenated Bios (*.zip;*.rom)|*.zip;*.rom"
        xPath()
        If pPath <> "" Then TextBox21.Text = pPath : controlbios()
    End Sub

    Private Sub Button25_Click(sender As Object, e As EventArgs)
        tPath = "Disk II Interface 16-Sector P5 Boot ROM, 341-0027"
        fPath = "Select Apple ][ + Concatenated Bios (*.zip;*.rom)|*.zip;*.rom"
        xPath()
        If pPath <> "" Then TextBox24.Text = pPath : controlbios()
    End Sub

    Private Sub Button26_Click(sender As Object, e As EventArgs)
        tPath = "Disk II Interface 16-Sector P6 Sequencer ROM, 341-0028"
        fPath = "Select Apple ][ + Concatenated Bios (*.zip;*.rom)|*.zip;*.rom"
        xPath()
        If pPath <> "" Then TextBox25.Text = pPath : controlbios()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        TextBox8.Text = MyIp()
    End Sub

    Private Sub Button5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button5.Click
        parMednafen()

        TPerC = False
        Dim psc_mex As String
        If CheckBox6.Checked = True Then
            save_per_config()
            psc_mex = "Specific " & UCase(p_c)
        ElseIf CheckBox59.Checked = True Then
            save_per_config()
            psc_mex = IO.Path.GetFileNameWithoutExtension(R_RelPath(MedGuiR.TextBox1.Text))
        Else
            If MedGuiR.CheckBox26.Checked = True Then
                MsgBox("You cannot modify a custom config. Mednafen file.", MsgBoxStyle.Exclamation + vbOKOnly, "Custom config. file error")
                versave = True
                Exit Sub
            End If
            Dim splitP_C(1) As String
            Mednafen_Save_setting()
            If p_c.Contains("_") Then splitP_C = Split(p_c, "_")
            psc_mex = UCase(splitP_C(1)) & " " & MedGuiR.MainGrid.CurrentRow.Cells(5).Value()
        End If

        If xcv = vbCancel Then Exit Sub
        If mxSet = 0 Then MsgBox(psc_mex.Trim & ", configuration saved!", vbOKOnly + vbInformation)
        versave = True
    End Sub

    Public Sub save_per_config()
        set_special_module()

        'If TPerC = False Then

        'System.IO.File.Copy(MedGuiR.TextBox4.Text & "\mednafen-09x.cfg", MedGuiR.TextBox4.Text & "\back_mednafen-09x.cfg", True)
        'Mednafen_Save_setting()

        'End If

        per_conf_path_name = ""

        If CheckBox59.Checked = True Then 'Or IO.File.Exists(MedGuiR.TextBox4.Text & "\pgconfig\" & System.IO.Path.GetFileNameWithoutExtension(MedGuiR.TextBox1.Text) & "." & p_c & ".cfg") = True Then
            per_conf_path_name = IO.Path.Combine(ExtractPath("path_pgconfig"), IO.Path.GetFileNameWithoutExtension(R_RelPath(MedGuiR.TextBox1.Text)) & "." & p_c)
        ElseIf CheckBox6.Checked = True Then 'Or IO.File.Exists(MedGuiR.TextBox4.Text & "\" & p_c & ".cfg") = True Then
            per_conf_path_name = IO.Path.Combine(MedGuiR.TextBox4.Text, p_c)
        End If

        Dim sperset As String = ""
        Select Case consoles

            Case "lynx"
                sperset = lynx
            Case "gb", "gbc"
                sperset = gameboy
            Case "gg"
                sperset = ""
            Case "gba"
                sperset = ""
            Case "md"
                sperset = genesis
            Case "ngp"
                sperset = neogeop
            Case "nes"
                sperset = famicom
            Case "pce", "pce_fast"
                sperset = pcengine
            Case "psx"
                sperset = psx
            Case "sms"
                sperset = mastersystem
            Case "snes", "snes_faust"
                sperset = snes
            Case "ss"
                sperset = ss
            Case "ssfplay"
                sperset = ""
            Case "vb"
                sperset = vboy
            Case "wswan"
                sperset = wswan
            Case "pcfx"
                sperset = pcfx
            Case "cdplay"
                sperset = ""
        End Select

        Dim Cvideo, Cfilters As String
        Cvideo = FixNegative(video, "video.fs.display")
        Cfilters = FixNegative(filters, ".scanlines")
        Cfilters = FixNegative(Cfilters, ".shader.goat.vdiv")
        Cfilters = FixNegative(Cfilters, ".shader.goat.hdiv")

        Dim objStreamWriter As IO.StreamWriter
        Dim per_sett As String = "; << Sound Config >>" & vbCrLf & sound & vbCrLf & vbCrLf & "; << Video Config >>" & vbCrLf & Cvideo &
           vbCrLf & vbCrLf & "; << Filters Config >>" & vbCrLf & Cfilters & vbCrLf & vbCrLf &
         "; << Bios Config >>" & vbCrLf & Replace(specBios, """", "") & vbCrLf & vbCrLf & "; << Specific Console Config >>" & vbCrLf & sperset

        per_sett = Replace(per_sett, " -", vbCrLf)
        per_sett = Replace(per_sett, "*", "-")
        per_sett = per_sett & CheckInputConfig(per_conf_path_name)
        per_sett = per_sett.Replace("""", "")

        If IO.File.Exists(per_conf_path_name & ".cfg") Then IO.File.Delete(per_conf_path_name & ".cfg")

        objStreamWriter = New IO.StreamWriter(per_conf_path_name & ".cfg")
        objStreamWriter.WriteLine(per_sett)
        objStreamWriter.Close()
        'StudioRemoveLinesFromFile()

        TPerC = False
    End Sub

    Private Function FixNegative(testo As String, parametro As String)
        Dim c_ As Integer
        c_ = testo.IndexOf(parametro)
        If testo.Substring(c_ + parametro.Length + 1, 1) = "-" Then
            testo = testo.Remove(c_ + parametro.Length + 1, 1).Insert(c_ + parametro.Length + 1, "*")
        End If

        Return testo
    End Function

    Private Function CheckInputConfig(Ipath As String)
        Dim startSplit As Integer
        Dim result As String = ""

        If File.Exists(Ipath & ".cfg") Then
            Dim Ivalue As String = File.ReadAllText(Ipath & ".cfg")
            If Ivalue.Contains("; << Specific Input Config >>") Then
                startSplit = Ivalue.IndexOf("; << Specific Input Config >>")
                result = Ivalue.Substring(startSplit, Ivalue.Length - startSplit)
            End If
        End If

        If result <> "" Then
            Return vbCrLf & vbCrLf & result
        End If
    End Function

    Private Sub StudioRemoveLinesFromFile()
        Dim Sr As New IO.StreamReader(per_conf_path_name & ".cfg")
        Dim Sw As New IO.StreamWriter(per_conf_path_name & ".cfgxxx")

        Dim Line As String = Sr.ReadLine

        Do While Not Line Is Nothing

            Dim alreadywrite As Boolean = False

            'If Line.Contains(p_c & ".") Then
            If p_c = "nes" And Line.Contains("snes.") Then
                alreadywrite = True
            End If

            If Line.Contains(".port") And Line.Contains(".multitap") = False Then
                alreadywrite = True
            End If

            If alreadywrite = False Then Sw.WriteLine(Line)
            'End If
            Line = Sr.ReadLine
        Loop

        Sr.Close()
        Sw.Close()
        IO.File.Delete(per_conf_path_name & ".cfg")
        FileSystem.Rename(per_conf_path_name & ".cfgxxx", per_conf_path_name & ".cfg")
    End Sub

    Public Sub Mednafen_Save_setting()
        NoCheck = True
        MedGuiR.ssetting = 0
        MedGuiR.StartEmu()
    End Sub

    Private Sub Button14_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button14.Click
        Dim doc As String
        Dim MDoc = MsgBox("Do you want to open specific module documentation?" & vbCrLf &
                       "Yes = " & Me.Text & " Documentation" & vbCrLf & "No = General Documentation", MsgBoxStyle.YesNoCancel + MsgBoxStyle.Information, "Open Mednafen Doc...")

        If MDoc = MsgBoxResult.Yes Then
            doc = p_c
        ElseIf MDoc = MsgBoxResult.No Then
            doc = "mednafen"
        ElseIf MDoc = MsgBoxResult.Cancel Then
            Exit Sub
        End If

        Try
            If MedGuiR.CheckBox17.Checked = False Then
                Process.Start(Chr(34) & MedGuiR.TextBox4.Text & "\Documentation\" & doc & ".html" & Chr(34))
            Else
                MedBrowser.Show()
                MedBrowser.WebBrowser1.Navigate(MedGuiR.TextBox4.Text & "\Documentation\" & doc & ".html")
            End If
        Catch ex As Exception
            MsgBox("No Mednafen Help Detected", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub Button4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button4.Click
        tPath = "Select GE CD Bios"
        fPath = "Select GE CD Bios (*.zip;*.pce)|*.zip;*.pce"
        xPath()
        If pPath <> "" Then TextBox18.Text = pPath
    End Sub

    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll
        update_setting()
    End Sub

    Private Sub TrackBar3_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar3.Scroll
        update_setting()
    End Sub

    Private Sub TrackBar2_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar2.Scroll
        update_setting()
    End Sub

    Private Sub TrackBar8_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar8.Scroll
        update_setting()
    End Sub

    Private Sub TrackBar9_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar9.Scroll
        update_setting()
    End Sub

    Private Sub TrackBar6_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar6.Scroll
        update_setting()
    End Sub

    Private Sub TrackBar7_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar7.Scroll
        update_setting()
    End Sub

    Private Sub TrackBar5_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar5.Scroll
        update_setting()
    End Sub

    Private Sub TrackBar4_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar4.Scroll
        update_setting()
    End Sub

    Private Sub TrackBar10_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs)
        update_setting()
    End Sub

    Private Sub TrackBar11_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs)
        update_setting()
    End Sub

    Private Sub TrackBar12_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs)
        update_setting()
    End Sub

    Private Sub TrackBar13_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar13.Scroll
        update_setting()
    End Sub

    Private Sub TrackBar14_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar14.Scroll
        update_setting()
    End Sub

    Private Sub TrackBar15_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar15.Scroll
        update_setting()
    End Sub

    Private Sub TrackBar16_Scroll(sender As Object, e As EventArgs)
        update_setting()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Call MedGuiR.DetectFolder()
        Process.Start(MedExtra & "Media\Movie")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Call MedGuiR.DetectFolder()
        Process.Start(MedExtra & "Media\Audio")
    End Sub

    Private Sub Button15_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button15.Click
        Try
            If MedGuiR.CheckBox17.Checked = False Then
                Process.Start(Chr(34) & MedGuiR.TextBox4.Text & "\Documentation\netplay.html" & Chr(34))
            Else
                MedBrowser.Show()
                MedBrowser.WebBrowser1.Navigate(MedGuiR.TextBox4.Text & "\Documentation\netplay.html")
            End If
        Catch ex As Exception
            MsgBox("No Mednafen Help Detected", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub CheckBox59_Click(sender As Object, e As System.EventArgs) Handles CheckBox59.Click
        If CheckBox6.Checked = True Then CheckBox6.Checked = False
    End Sub

    Private Sub TrackBar16_Scroll_1(sender As Object, e As EventArgs) Handles TrackBar16.Scroll
        update_setting()
    End Sub

    Private Sub ComboBox54_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox54.SelectedIndexChanged
        SpecificServer = "=" & ComboBox54.Text
        PopulateNetplay()
    End Sub

    Private Sub Label163_Click(sender As Object, e As EventArgs) Handles Label163.Click
        isARGB = False
        conv_col()
        If colour = Nothing Then Exit Sub
        Label163.BackColor = ColorDialog1.Color
        Label163.ForeColor = Label163.BackColor
        Label163.Text = colour
    End Sub

    Private Sub Label135_Click(sender As Object, e As EventArgs) Handles Label135.Click
        isARGB = True
        conv_col()
        If colour = Nothing Then Exit Sub
        Label135.BackColor = ColorDialog1.Color
        Label135.ForeColor = Label135.BackColor
        Label135.Text = colour
    End Sub

    Private Sub Button27_Click(sender As Object, e As EventArgs) Handles Button27.Click
        StartControlBios()
    End Sub

    Private Sub Label136_Click(sender As Object, e As EventArgs) Handles Label136.Click
        isARGB = True
        conv_col()
        If colour = Nothing Then Exit Sub
        Label136.BackColor = ColorDialog1.Color
        Label136.ForeColor = Label136.BackColor
        Label136.Text = colour
    End Sub

    Private Sub Button38_Click(sender As Object, e As EventArgs) Handles Button38.Click
        TextBox34.Text = SetPath("cheats", "-filesys.path_cheat ", TextBox34.Text)
    End Sub

    Private Sub Button37_Click(sender As Object, e As EventArgs) Handles Button37.Click
        TextBox33.Text = SetPath("firmware", "-filesys.path_firmware ", TextBox33.Text)
    End Sub

    Private Sub Button36_Click(sender As Object, e As EventArgs) Handles Button36.Click
        TextBox32.Text = SetPath("movies", "-filesys.path_movie ", TextBox32.Text)
    End Sub

    Private Sub Button35_Click(sender As Object, e As EventArgs) Handles Button35.Click
        TextBox31.Text = SetPath("custom palettes", "-filesys.path_palette ", TextBox31.Text)
    End Sub

    Private Sub Button34_Click(sender As Object, e As EventArgs) Handles Button34.Click
        TextBox30.Text = SetPath("per-game configuration override files", "-filesys.path_pgconfig ", TextBox30.Text)
    End Sub

    Private Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click
        TextBox29.Text = SetPath("save games and nonvolatile memory", "-filesys.path_sav ", TextBox29.Text)
    End Sub

    Private Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click
        TextBox28.Text = SetPath("backups of save games and nonvolatile memory", "-filesys.path_savbackup ", TextBox28.Text)
    End Sub

    Private Sub Button31_Click(sender As Object, e As EventArgs) Handles Button31.Click
        TextBox27.Text = SetPath("screen snapshots", "-filesys.path_snap ", TextBox27.Text)
    End Sub

    Private Sub Button30_Click(sender As Object, e As EventArgs) Handles Button30.Click
        TextBox26.Text = SetPath("save states", "-filesys.path_state ", TextBox26.Text)
    End Sub

    Private Sub CheckBox104_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox104.CheckedChanged

        If CheckBox104.Checked = True Then
            ComboBox5.Text = "0"
            ComboBox6.Text = "0x0"
            NumericUpDown6.Value = "4,3"
            NumericUpDown5.Value = "4,3"
            CheckBox59.Checked = True
        Else
            ComboBox5.Text = ostr
            ComboBox6.Text = ofsr
            NumericUpDown6.Value = oxfsr
            NumericUpDown5.Value = oyfsr
            CheckBox59.Checked = False
        End If

    End Sub

    Private Sub Button39_Click(sender As Object, e As EventArgs) Handles Button39.Click
        ResetMedPath()
        MsgBox("Mednafen path resetted, please reopen advanced menu", MsgBoxStyle.Information + vbOKOnly, "Resetted path...")
        versave = True
        Me.Close()
    End Sub

    Private Sub Button23_Click(sender As Object, e As EventArgs)
        SumPSXAnalog()
    End Sub

    Private Sub Button40_Click(sender As Object, e As EventArgs) Handles Button40.Click
        ResetBioses()
        MsgBox("Mednafen bioses path resetted, please reopen advanced menu", MsgBoxStyle.Information + vbOKOnly, "Resetted bioses path...")
        versave = True
        Me.Close()
    End Sub

    Private Sub TrackBar17_Scroll(sender As Object, e As EventArgs) Handles TrackBar17.Scroll
        update_setting()
    End Sub

    Private Sub TrackBar18_Scroll(sender As Object, e As EventArgs) Handles TrackBar18.Scroll
        update_setting()
    End Sub

    Private Sub TrackBar19_Scroll(sender As Object, e As EventArgs) Handles TrackBar19.Scroll
        update_setting()
    End Sub

    Private Sub TrackBar20_Scroll(sender As Object, e As EventArgs) Handles TrackBar20.Scroll
        update_setting()
    End Sub

    Private Sub CheckBox6_Click(sender As Object, e As System.EventArgs) Handles CheckBox6.Click
        If CheckBox59.Checked = True Then CheckBox59.Checked = False
    End Sub

    Private Sub Button18_Click(sender As System.Object, e As System.EventArgs) Handles Button18.Click
        PerConf.ShowDialog()
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        'CheckBox6.Checked = True
        mxSet = 1
        'Button5.PerformClick()
        KeyAssign.ShowDialog()
        mxSet = 0
    End Sub

    Public Sub ListServer_reload()
        cmbServer.Items.Clear()
        ComboBox54.Items.Clear()
        MedGuiR.ServerToolStripComboBox2.Items.Clear()
        MedClient.ComboBox1.Items.Clear()

        Try
            Dim fullPath As String = MedExtra & "ListServer.txt"
            If IO.File.Exists(fullPath) Then
                Dim oFile As IO.File
                Dim oRead As IO.StreamReader
                Dim Sserver As String
                Dim line() As String
                Dim salias() As String

                Try
#Disable Warning BC42025 ' L'accesso del membro condiviso, del membro costante, del membro di enumerazione o del tipo nidificato verrà effettuato tramite un'istanza. L'espressione di qualificazione non verrà valutata.
                    oRead = oFile.OpenText(fullPath)
#Enable Warning BC42025 ' L'accesso del membro condiviso, del membro costante, del membro di enumerazione o del tipo nidificato verrà effettuato tramite un'istanza. L'espressione di qualificazione non verrà valutata.

                    While oRead.Peek <> -1
                        Dim xtest As String = oRead.ReadLine()
                        line = Split(xtest, ":")
                        Sserver = line(0)
                        If xtest.Contains("=") Then
                            salias = Split(line(1), "=")
                            ComboBox54.Items.Add(salias(1))
                        End If
                        'port = line(1)
                        cmbServer.Items.Add(Sserver)
                        MedGuiR.ServerToolStripComboBox2.Items.Add(Sserver)
                        MedClient.ComboBox1.Items.Add(Sserver)
                    End While
                Catch ex As Exception
                Finally
                    oRead.Close()
                End Try

                'cmbServer.Items.AddRange(IO.File.ReadAllLines(fullPath))
                'MedGuiR.ServerToolStripComboBox2.Items.AddRange(IO.File.ReadAllLines(fullPath))
            Else
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            MGRWriteLog("Setting - ListServer_reload" & ex.Message)
        End Try
    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        Dim old_server As String = cmbServer.Text
        If IO.File.Exists(MedExtra & "ListServer.txt") = False Then
            Using IO.File.Create(MedExtra & "ListServer.txt")
            End Using
        End If
        Try
            Dim lineS As String() = IO.File.ReadAllLines(MedExtra & "ListServer.txt")
            For i As Integer = 0 To lineS.Length - 1
                If lineS(i) = cmbServer.Text & ":" & TextBox7.Text Then Exit Sub
            Next
            Dim sw As IO.StreamWriter = IO.File.AppendText(MedExtra & "ListServer.txt")
            If TextBox7.Text.Trim = "" Then TextBox7.Text = "4046"
            Dim aliasss As String
            If ComboBox54.Text.Trim <> "" Then aliasss = "=" & ComboBox54.Text : Else aliasss = ""
            sw.WriteLine(cmbServer.Text & ":" & TextBox7.Text & aliasss)
            sw.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            MGRWriteLog("Setting - " & sender & ": " & ex.Message)
        End Try
        ListServer_reload()
        cmbServer.Text = old_server
    End Sub

    Public Sub PopulateNetplay()
        TextBox7.Text = "4046"
        MedGuiR.PortToolStripTextBox1.Text = "4046"

        Dim Sport As String
        Dim line() As String
        Dim salias() As String

        Using sr As New System.IO.StreamReader(MedExtra & "ListServer.txt")
            Do Until sr.EndOfStream
                Try
                    Dim sBuf As String = sr.ReadLine
                    If sBuf.Contains(SpecificServer) Then

                        If sBuf.Contains(":") Then
                            line = Split(sBuf, ":")

                            If line(1).Contains("=") Then
                                salias = Split(line(1), "=")
                                Sport = salias(0)
                                ComboBox54.Text = salias(1)
                            Else
                                ComboBox54.Text = ""
                                Sport = line(1)
                            End If
                            cmbServer.Text = line(0)
                        Else
                            Sport = "4046"
                            ComboBox54.Text = ""
                            cmbServer.Text = SpecificServer
                        End If

                        If Sport = "" Then Sport = "4046"
                        TextBox7.Text = Sport
                        MedGuiR.PortToolStripTextBox1.Text = Sport
                        ' fai qualcosa (1)
                    End If
                Catch ex As Exception
                    ' gestisci l’eccezione
                End Try
            Loop
        End Using

    End Sub

    Private Sub check_NetPlayServer()

        Dim hostname As String = cmbServer.Text
        Dim portno As Integer = TextBox7.Text

        If LCase(hostname) = "localhost" Then hostname = "127.0.0.1"
        Dim ipa As IPAddress = DirectCast(Dns.GetHostAddresses(hostname)(0), IPAddress)

        Try
            Dim sock As New System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp)
            sock.Connect(ipa, portno)
            If sock.Connected = True Then
                ' Port is in use and connection is successful
                Label27.ForeColor = Drawing.Color.Green
                Label27.Text = "Net-Play Host Open"
                Dim pingreq As Ping = New Ping()
                Dim rep As PingReply = pingreq.Send(hostname)
                MsgBox("Round-Trip Time: " & rep.RoundtripTime + 10, MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            End If

            sock.Close()
        Catch ex As System.Net.Sockets.SocketException
            If ex.ErrorCode = 10061 Then
                ' Port is unused and could not establish connection
                'MessageBox.Show("Port is Open!")
            Else
                Label27.ForeColor = Drawing.Color.Red
                Label27.Text = "Net-Play Host Closed"
                'MessageBox.Show(ex.Message)
            End If
        End Try
        'NetVerified = True
    End Sub

    Private Sub cmbServer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbServer.SelectedIndexChanged
        Label27.Text = "Net-Play Host"
        Label27.ForeColor = Label28.ForeColor
        PictureBox1.Image = Nothing
        SpecificServer = cmbServer.Text
        PopulateNetplay()
        Try
            ServerCountry(SpecificServer)
            PictureBox1.Image = Image.FromFile(SCountry)
            ToolTip1.SetToolTip(PictureBox1, SLocation)
        Catch
            SCountry = MedExtra & "Resource\System\unknow.gif"
            SLocation = "Unknown"
        Finally
            PictureBox1.Image = Image.FromFile(SCountry)
            ToolTip1.SetToolTip(PictureBox1, SLocation)
        End Try

    End Sub

    Private Sub Label27_DoubleClick(sender As Object, e As EventArgs) Handles Label27.DoubleClick
        Try
            Process.Start(MedExtra & "ListServer.txt")
        Catch ex As Exception
            MGRWriteLog("Setting - " & sender & ": " & ex.Message)
        End Try
    End Sub

    Private Sub controlbios()
        Try
            filepath = pPath
            SHA1CalcFile()

            Select Case tPath
                Case "Select PCFX Bios"
                    If r_sha = "1a77fd83e337f906aecab27a1604db064cf10074" Then Exit Sub
                Case "Select PSX Eur Bios"
                    If r_sha = "f6bc2d1f5eb6593de7d089c425ac681d6fffd3f0" Or r_md5 = "c53ca5908936d412331790f4426c6c33" Or r_sha = LCase("9E9F405FB9EDB4ABE1C5926D56DA5782732A8598") Or r_sha = LCase("71AD544EDBD2FE10F56FE0F690DF9BC20E7D7414") Or r_sha = LCase("904E953FC4C129B16471B7439463EF7A515A8ED8") Or r_sha = LCase("C40146361EB8CF670B19FDC9759190257803CAB7") Then Exit Sub
                Case "Select PSX Usa Bios"
                    If r_sha = "0555c6fae8906f3f09baf5988f00e55f88e9f30b" Or r_md5 = "c53ca5908936d412331790f4426c6c33" Or r_sha = LCase("9E9F405FB9EDB4ABE1C5926D56DA5782732A8598") Or r_sha = LCase("71AD544EDBD2FE10F56FE0F690DF9BC20E7D7414") Or r_sha = LCase("904E953FC4C129B16471B7439463EF7A515A8ED8") Or r_sha = LCase("C40146361EB8CF670B19FDC9759190257803CAB7") Then Exit Sub
                Case "Select PSX Jap Bios"
                    If r_sha = "b05def971d8ec59f346f2d9ac21fb742e3eb6917" Or r_md5 = "c53ca5908936d412331790f4426c6c33" Or r_sha = LCase("9E9F405FB9EDB4ABE1C5926D56DA5782732A8598") Or r_sha = LCase("71AD544EDBD2FE10F56FE0F690DF9BC20E7D7414") Or r_sha = LCase("904E953FC4C129B16471B7439463EF7A515A8ED8") Or r_sha = LCase("C40146361EB8CF670B19FDC9759190257803CAB7") Then Exit Sub
                Case "Select Saturn Usa/Eur Bios"
                    If r_sha = "faa8ea183a6d7bbe5d4e03bb1332519800d3fbc3" Or r_sha = "8bbe4c3368a453ebfe5c771d262b07aeccdf0a38" Then Exit Sub
                Case "Select Saturn Jap Bios"
                    If r_sha = "df94c5b4d47eb3cc404d88b33a8fda237eaf4720" Or r_sha = "8bbe4c3368a453ebfe5c771d262b07aeccdf0a38" Then Exit Sub
                Case "Select ST-V Jap Bios"
                    If r_sha = LCase("EFF0F54C70BCE05FF3A289BF30B1027E1C8CD117") Then Exit Sub
                Case "Select ST-V Usa Bios"
                    If r_sha = LCase("EAF1C3E5D602E1139D2090A78D7E19F04F916794") Then Exit Sub
                Case "Select ST-V Eur Bios"
                    If r_sha = LCase("AF79CFF317E5B57D49E463AF16A9F616ED1EEE08") Then Exit Sub
                Case "Select Apple ][ Concatenated Bios"
                    If r_sha = "2dfaf376fc6a0b106320911c1ebfc1512601dc6c" Then Exit Sub
                Case "Select Apple ][ + Concatenated Bios"
                    If r_sha = "33a24f5489ba9195b44be77d9afb2252594cb5c7" Then Exit Sub
                Case "Disk II Interface 13-Sector P5 Boot ROM, 341-0009"
                    If r_sha = "afd060e6f35faf3bb0146fa889fc787adf56330a" Then Exit Sub
                Case "Disk II Interface 13-Sector P6 Sequencer ROM, 341-0010"
                    If r_sha = "e3d6d1c30653572b49ecc2dc54ce073978411a04" Then Exit Sub
                Case "Disk II Interface 16-Sector P5 Boot ROM, 341-0027"
                    If r_sha = "d4181c9f046aafc3fb326b381baac809d9e38d16" Then Exit Sub
                Case "Disk II Interface 16-Sector P6 Sequencer ROM, 341-0028"
                    If r_sha = "bc39fbd5b9a8d2287ac5d0a42e639fc4d3c2f9d4" Then Exit Sub
                Case Else
                    Exit Sub
            End Select

            MsgBox(Replace(tPath, "Select ", "") & " not compatible with Mednafen", vbOKOnly + MsgBoxStyle.Exclamation, "incompatible Bios")
        Catch
        End Try

    End Sub

    Public Sub StartControlBios()
        Dim pops_bios As Boolean = False

        For Each foundbios As String In My.Computer.FileSystem.GetFiles(ExtractPath("path_firmware"))
            r_sha = ""
            filepath = foundbios
            SHA1CalcFile()

            Select Case True
                Case r_sha = "1a77fd83e337f906aecab27a1604db064cf10074"
                    Label71.ForeColor = Color.ForestGreen
                    TextBox13.Text = foundbios
                Case r_sha = "f6bc2d1f5eb6593de7d089c425ac681d6fffd3f0" And pops_bios = False
                    Label73.ForeColor = Color.ForestGreen
                    TextBox15.Text = foundbios
                Case r_sha = "0555c6fae8906f3f09baf5988f00e55f88e9f30b" And pops_bios = False
                    Label75.ForeColor = Color.ForestGreen
                    TextBox17.Text = foundbios
                Case r_sha = "b05def971d8ec59f346f2d9ac21fb742e3eb6917" And pops_bios = False
                    Label74.ForeColor = Color.ForestGreen
                    TextBox16.Text = foundbios
                Case r_sha = "faa8ea183a6d7bbe5d4e03bb1332519800d3fbc3" Or r_sha = "8bbe4c3368a453ebfe5c771d262b07aeccdf0a38"
                    Label115.ForeColor = Color.ForestGreen
                    TextBox23.Text = foundbios
                Case r_sha = "df94c5b4d47eb3cc404d88b33a8fda237eaf4720" Or r_sha = "8bbe4c3368a453ebfe5c771d262b07aeccdf0a38"
                    Label114.ForeColor = Color.ForestGreen
                    TextBox22.Text = foundbios
                Case r_sha = "1b4c260326d905bc718812dad0f68089977f427b" Or r_sha = "bf2f90bdc3f82bc4bf28b4e9707530165dedcdd2" _
                    Or r_sha = "79f5ff55dd10187c7fd7b8daab0b3ffbd1f56a2c"
                    Label70.ForeColor = Color.ForestGreen
                    TextBox12.Text = foundbios
                    'Case "300c20df6731a33952ded8c436f7f186d25d3492" 'GBA BIOS
                    'TextBox10.Text = foundbios
                    'Case "f430a0d752a9fa0c7032db8131f9090d18f71779" 'gg bios
                    'TextBox11.Text = foundbios
                    'Case "014881a959e045e00f4db8f52955200865d40280" 'gecard bios
                    'TextBox18.Text = foundbios
                    'Case "f4f315adcef9b8feb0364c21ab7f0eaf5457f3ed" 'megacd bios
                    'TextBox14.Text = foundbios
                Case r_sha = "2dfaf376fc6a0b106320911c1ebfc1512601dc6c"
                    Label11.ForeColor = Color.ForestGreen
                    TextBox4.Text = foundbios
                Case r_sha = "33a24f5489ba9195b44be77d9afb2252594cb5c7"
                    Label145.ForeColor = Color.ForestGreen
                    TextBox19.Text = foundbios
                Case r_sha = "afd060e6f35faf3bb0146fa889fc787adf56330a"
                    Label146.ForeColor = Color.ForestGreen
                    TextBox20.Text = foundbios
                Case r_sha = "e3d6d1c30653572b49ecc2dc54ce073978411a04"
                    Label147.ForeColor = Color.ForestGreen
                    TextBox21.Text = foundbios
                Case r_sha = "d4181c9f046aafc3fb326b381baac809d9e38d16"
                    Label148.ForeColor = Color.ForestGreen
                    TextBox24.Text = foundbios
                Case r_sha = "bc39fbd5b9a8d2287ac5d0a42e639fc4d3c2f9d4"
                    Label149.ForeColor = Color.ForestGreen
                    TextBox25.Text = foundbios
                Case r_md5 = "c53ca5908936d412331790f4426c6c33"
                    'Label74.ForeColor = Color.ForestGreen
                    TextBox16.Text = foundbios
                    'Label73.ForeColor = Color.ForestGreen
                    TextBox15.Text = foundbios
                    'Label75.ForeColor = Color.ForestGreen
                    TextBox17.Text = foundbios
                    pops_bios = True
                Case r_sha = LCase("EFF0F54C70BCE05FF3A289BF30B1027E1C8CD117")
                    Label192.ForeColor = Color.ForestGreen
                    TextBox37.Text = foundbios
                Case r_sha = LCase("EAF1C3E5D602E1139D2090A78D7E19F04F916794")
                    Label190.ForeColor = Color.ForestGreen
                    TextBox35.Text = foundbios
                Case r_sha = LCase("AF79CFF317E5B57D49E463AF16A9F616ED1EEE08")
                    Label191.ForeColor = Color.ForestGreen
                    TextBox36.Text = foundbios
            End Select
        Next
    End Sub

    Private Sub Button20_MouseClick(sender As Object, e As MouseEventArgs) Handles Button20.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If My.Computer.Network.IsAvailable = False Then Exit Sub

            Try
                If My.Computer.Network.Ping(cmbServer.Text) = True Then
                    check_NetPlayServer()
                Else
                    MsgBox("Server Unpingable!", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
                    'Label27.ForeColor = Drawing.Color.Red
                End If
            Catch ex As System.Net.NetworkInformation.PingException
                MsgBox("No Connection Available!", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
                Label27.ForeColor = Drawing.Color.Red
            End Try
        End If
    End Sub

    Private Sub TabControl1_MouseWheel(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles TabControl1.MouseWheel
        If e.Delta > 0 Then
            If TabControl1.TabPages.IndexOf(TabControl1.SelectedTab) < 29 And TabControl1.SelectedTab IsNot Nothing Then
                TabControl1.SelectedIndex = TabControl1.TabPages.IndexOf(TabControl1.SelectedTab) + 1
            End If
        Else
            If TabControl1.TabPages.IndexOf(TabControl1.SelectedTab) > 0 And TabControl1.SelectedTab IsNot Nothing Then
                TabControl1.SelectedIndex = TabControl1.TabPages.IndexOf(TabControl1.SelectedTab) - 1
            End If
        End If
    End Sub

    Private Function SetPath(description As String, ParPath As String, TempPath As String) As String
        Dim folder As New FolderBrowserDialog
        folder.Description = "Path to directory for " & description
        folder.SelectedPath = MedGuiR.TextBox4.Text
        If folder.ShowDialog = Windows.Forms.DialogResult.OK Then
            tProcess = "mednafen"
            wDir = MedGuiR.TextBox4.Text
            Arg = ParPath & Chr(34) & folder.SelectedPath & Chr(34)
            StartProcess()
            Return (folder.SelectedPath)
        Else
            Return (TempPath)
        End If
    End Function

    Private Sub ResetMedPath()
        tProcess = "mednafen"
        wDir = MedGuiR.TextBox4.Text

        Dim MPath(8) As String
        MPath(0) = "path_cheat cheats"
        MPath(1) = "path_firmware firmware"
        MPath(2) = "path_movie mcm"
        MPath(3) = "path_palette palettes"
        MPath(4) = "path_pgconfig pgconfig"
        MPath(5) = "path_sav sav"
        MPath(6) = "path_savbackup b"
        MPath(7) = "path_snap snaps"
        MPath(8) = "path_state mcs"

        For i = 0 To 8
            Arg = "-filesys." & MPath(i)
            StartProcess()
            execute.WaitForExit()
        Next
    End Sub

    Private Sub ResetBioses()
        tProcess = "mednafen"
        wDir = MedGuiR.TextBox4.Text

        Dim MPath(15) As String
        MPath(0) = "md.cdbios us_scd1_9210.bin"
        MPath(1) = "pce.cdbios syscard3.pce"
        MPath(2) = "pce.gecdbios gecard.pce"
        MPath(3) = "pce_fast.cdbios syscard3.pce"
        MPath(4) = "pcfx.bios pcfx.rom"
        MPath(5) = "psx.bios_eu scph5502.bin"
        MPath(6) = "psx.bios_jp scph5500.bin"
        MPath(7) = "psx.bios_na scph5501.bin"
        MPath(8) = "ss.bios_jp sega_101.bin"
        MPath(9) = "ss.bios_na_eu mpr-17933.bin"
        MPath(10) = "ss.cart.kof95_path mpr-18811-mx.ic1"
        MPath(11) = "ss.cart.ultraman_path mpr-19367-mx.ic1"
        MPath(12) = "gba.bios " & Chr(34) & Nothing & Chr(34)
        MPath(13) = "ss.bios_stv_eu epr-17954a.ic8"
        MPath(14) = "ss.bios_stv_jp epr-20091.ic8"
        MPath(15) = "ss.bios_stv_na epr-17952a.ic8"

        For i = 0 To 15
            Arg = "-" & MPath(i)
            StartProcess()
            execute.WaitForExit()
        Next
    End Sub

    'code from https://dotnetrix.co.uk/tabcontrol.htm

    Private Sub TabControl1_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles TabControl1.DrawItem

        'Firstly we'll define some parameters.
        Dim CurrentTab As TabPage = TabControl1.TabPages(e.Index)
        Dim ItemRect As Rectangle = TabControl1.GetTabRect(e.Index)
        Dim FillBrush As New SolidBrush(DefBack)
        Dim TextBrush As New SolidBrush(DefFore)
        Dim sf As New StringFormat
        sf.Alignment = StringAlignment.Center
        sf.LineAlignment = StringAlignment.Center

        'If we are currently painting the Selected TabItem we'll
        'change the brush colors and inflate the rectangle.
        If CBool(e.State And DrawItemState.Selected) Then
            FillBrush.Color = DefFore
            TextBrush.Color = DefBack
            ItemRect.Inflate(2, 2)
        End If

        'Set up rotation for left and right aligned tabs
        If TabControl1.Alignment = TabAlignment.Left Or TabControl1.Alignment = TabAlignment.Right Then
            Dim RotateAngle As Single = 90
            If TabControl1.Alignment = TabAlignment.Left Then RotateAngle = 270
            Dim cp As New PointF(ItemRect.Left + (ItemRect.Width \ 2), ItemRect.Top + (ItemRect.Height \ 2))
            e.Graphics.TranslateTransform(cp.X, cp.Y)
            e.Graphics.RotateTransform(RotateAngle)
            ItemRect = New Rectangle(-(ItemRect.Height \ 2), -(ItemRect.Width \ 2), ItemRect.Height, ItemRect.Width)
        End If

        'Next we'll paint the TabItem with our Fill Brush
        e.Graphics.FillRectangle(FillBrush, ItemRect)

        'Now draw the text.
        e.Graphics.DrawString(CurrentTab.Text, e.Font, TextBrush, RectangleF.op_Implicit(ItemRect), sf)

        'Reset any Graphics rotation
        e.Graphics.ResetTransform()

        'Finally, we should Dispose of our brushes.
        FillBrush.Dispose()
        TextBrush.Dispose()

    End Sub

End Class