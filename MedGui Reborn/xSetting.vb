Imports System.IO

Module xSetting
    Dim row, MedConfSpecific As String
    Public xValue As String

    Public Sub ReadXValue()
        Try
            Using reader As New StreamReader(MedConfSpecific & ".cfg")
                While Not reader.EndOfStream
                    row = reader.ReadLine
                    SetValue()
                    SetGeneral()
                    'Exit While
                End While
                reader.Dispose()
                reader.Close()
            End Using
            update_setting()
        Catch e As Exception
            Console.WriteLine("The file could not be read:")
            Console.WriteLine(e.Message)
        End Try
    End Sub

    Public Sub ReadPSValue()
        set_special_module()

        If IO.File.Exists(Path.Combine(ExtractPath("path_pgconfig"), Path.GetFileNameWithoutExtension(MedGuiR.TextBox1.Text) & "." & p_c & ".cfg")) = True Then
            MedConfSpecific = Path.Combine(MedGuiR.TextBox4.Text, DMedConf)
            ReadXValue()
            MedConfSpecific = Path.Combine(ExtractPath("path_pgconfig"), Path.GetFileNameWithoutExtension(MedGuiR.TextBox1.Text) & "." & p_c)
            MgrSetting.CheckBox6.Checked = False
            MgrSetting.CheckBox59.Checked = True
            MsgBox("Detected a specific game config, global settings will be ignored", vbOKOnly + MsgBoxStyle.Information)
        ElseIf File.Exists(MedGuiR.TextBox4.Text & "\" & p_c & ".cfg") = True Then
            Read_Desync()
            If contdes = 1 Then
                MedGuiR.RebuilDesync()
                'File.Delete(MedGuiR.TextBox4.Text & "\" & consoles & ".cfg”)
            Else
                MedConfSpecific = Path.Combine(MedGuiR.TextBox4.Text, DMedConf)
                ReadXValue()
                MedConfSpecific = p_c
                MgrSetting.CheckBox59.Checked = False
                MgrSetting.CheckBox6.Checked = True
                MsgBox("Detected a specific console config, global settings will be ignored", vbOKOnly + MsgBoxStyle.Information)
            End If
        Else
            MedConfSpecific = Path.Combine(MedGuiR.TextBox4.Text, DMedConf)
            MgrSetting.CheckBox59.Checked = False
            MgrSetting.CheckBox6.Checked = False
        End If

        ReadXValue()
    End Sub

    Public Sub SetValue()
        set_special_module()

        Select Case LCase(ext)
            Case ".po", ".dsk", ".do", ".woz", ".d13", ".mai"
                If row.Contains(consoles & ".video.mixed_text_mono ") Then xValue = Trim(Replace(row, consoles & ".video.mixed_text_mono", "")) : MgrSetting.CheckBox108.Checked = CBool(xValue)
                If row.Contains(consoles & ".video.color_smooth ") Then xValue = Trim(Replace(row, consoles & ".video.color_smooth", "")) : MgrSetting.CheckBox107.Checked = CBool(xValue)
                If row.Contains(consoles & ".input.port1.joystick.axis_scale ") Then xValue = Trim(Replace(row, consoles & ".input.port1.joystick.axis_scale", "")) : MgrSetting.NumericUpDown41.Value = Val(xValue)
                If row.Contains(consoles & ".video.brightness ") Then xValue = Trim(Replace(row, consoles & ".video.brightness", "")) : MgrSetting.NumericUpDown29.Value = Val(xValue)
                If row.Contains(consoles & ".video.contrast ") Then xValue = Trim(Replace(row, consoles & ".video.contrast", "")) : MgrSetting.NumericUpDown31.Value = Val(xValue)
                If row.Contains(consoles & ".video.hue ") Then xValue = Trim(Replace(row, consoles & ".video.hue", "")) : MgrSetting.NumericUpDown32.Value = Val(xValue)
                If row.Contains(consoles & ".video.saturation ") Then xValue = Trim(Replace(row, consoles & ".video.saturation", "")) : MgrSetting.NumericUpDown33.Value = Val(xValue)
                If row.Contains(consoles & ".video.color_lumafilter ") Then xValue = Trim(Replace(row, consoles & ".video.color_lumafilter", "")) : MgrSetting.NumericUpDown30.Value = Val(xValue)
                If row.Contains(consoles & ".video.mono_lumafilter ") Then xValue = Trim(Replace(row, consoles & ".video.mono_lumafilter", "")) : MgrSetting.NumericUpDown34.Value = Val(xValue)
                If row.Contains(consoles & ".video.matrix.blue.i ") Then xValue = Trim(Replace(row, consoles & ".video.matrix.blue.i", "")) : MgrSetting.NumericUpDown35.Value = Val(xValue)
                If row.Contains(consoles & ".video.matrix.blue.q ") Then xValue = Trim(Replace(row, consoles & ".video.matrix.blue.q", "")) : MgrSetting.NumericUpDown36.Value = Val(xValue)
                If row.Contains(consoles & ".video.matrix.green.i ") Then xValue = Trim(Replace(row, consoles & ".video.matrix.green.i", "")) : MgrSetting.NumericUpDown37.Value = Val(xValue)
                If row.Contains(consoles & ".video.matrix.green.q ") Then xValue = Trim(Replace(row, consoles & ".video.matrix.green.q", "")) : MgrSetting.NumericUpDown38.Value = Val(xValue)
                If row.Contains(consoles & ".video.matrix.red.i ") Then xValue = Trim(Replace(row, consoles & ".video.matrix.red.i", "")) : MgrSetting.NumericUpDown39.Value = Val(xValue)
                If row.Contains(consoles & ".video.matrix.red.q ") Then xValue = Trim(Replace(row, consoles & ".video.matrix.red.q", "")) : MgrSetting.NumericUpDown40.Value = Val(xValue)
                If row.Contains(consoles & ".input.port1 ") Then xValue = Trim(Replace(row, consoles & ".input.port1", "")) : MgrSetting.ComboBox58.Text = xValue
                If row.Contains(consoles & ".input.port2 ") Then xValue = Trim(Replace(row, consoles & ".input.port2", "")) : MgrSetting.ComboBox60.Text = xValue
                If row.Contains(consoles & ".video.matrix ") Then xValue = Trim(Replace(row, consoles & ".video.matrix", "")) : MgrSetting.ComboBox61.Text = xValue
                If row.Contains(consoles & ".video.force_mono ") Then xValue = Trim(Replace(row, consoles & ".video.force_mono", "")) : MgrSetting.Label163.Text = (xValue) : MgrSetting.Label163.BackColor = ColorTranslator.FromHtml(xValue) : MgrSetting.Label163.ForeColor = ColorTranslator.FromHtml(xValue)
                MgrSetting.tab_index = 21
            Case ".lnx"
                If row.Contains(consoles & ".lowpass ") Then xValue = Trim(Replace(row, consoles & ".lowpass", "")) : MgrSetting.CheckBox18.Checked = CBool(xValue)
                If row.Contains(consoles & ".rotateinput ") Then xValue = Trim(Replace(row, consoles & ".rotateinput", "")) : MgrSetting.CheckBox17.Checked = CBool(xValue)
                MgrSetting.tab_index = 7
            Case ".gb", ".gbc"
                If row.Contains("snes.") Then
                    Exit Sub
                Else
                    If row.Contains(consoles & ".system_type ") Then xValue = Trim(Replace(row, consoles & ".system_type", "")) : MgrSetting.ComboBox10.Text = xValue
                    MgrSetting.tab_index = 8
                End If
            Case ".ngp", ".ngc", ".npc"
                If row.Contains(consoles & ".language ") Then xValue = Trim(Replace(row, consoles & ".language", "")) : MgrSetting.ComboBox11.Text = xValue
                MgrSetting.tab_index = 9
            Case ".nes", ".nsf", ".unf", ".nez", ".fds"
                If row.Contains("snes.") Then
                    Exit Sub
                Else

                    If row.Contains(consoles & ".clipsides ") Then xValue = Trim(Replace(row, consoles & ".clipsides", "")) : MgrSetting.CheckBox19.Checked = CBool(xValue)
                    If row.Contains(consoles & ".correct_aspect ") Then xValue = Trim(Replace(row, consoles & ".correct_aspect", "")) : MgrSetting.CheckBox20.Checked = CBool(xValue)
                    If row.Contains(consoles & ".gg ") Then xValue = Trim(Replace(row, consoles & ".gg", "")) : MgrSetting.CheckBox21.Checked = CBool(xValue)
                    If row.Contains(consoles & ".input.fcexp ") Then xValue = Trim(Replace(row, consoles & ".input.fcexp", "")) : MgrSetting.ComboBox12.Text = xValue
                    'For i = 1 To 4
                    If row.Contains(consoles & ".input.port" & 1 & " ") Then xValue = Trim(Replace(row, consoles & ".input.port" & 1, "")) : MgrSetting.ComboBox13.Text = xValue
                    'Next
                    If row.Contains(consoles & ".n106bs ") Then xValue = Trim(Replace(row, consoles & ".n106bs", "")) : MgrSetting.CheckBox22.Checked = CBool(xValue)
                    If row.Contains(consoles & ".no8lim ") Then xValue = Trim(Replace(row, consoles & ".no8lim", "")) : MgrSetting.CheckBox23.Checked = CBool(xValue)
                    If row.Contains(consoles & ".ntsc.mergefields ") Then xValue = Trim(Replace(row, consoles & ".ntsc.mergefields", "")) : MgrSetting.CheckBox27.Checked = CBool(xValue)
                    If row.Contains(consoles & ".nofs ") Then xValue = Trim(Replace(row, consoles & ".nofs", "")) : MgrSetting.CheckBox24.Checked = CBool(xValue)
                    If row.Contains(consoles & ".ntsc.preset ") Then xValue = Trim(Replace(row, consoles & ".ntsc.preset", "")) : MgrSetting.ComboBox14.Text = xValue
                    If row.Contains(consoles & ".ntscblitter ") Then xValue = Trim(Replace(row, consoles & ".ntscblitter", "")) : MgrSetting.CheckBox25.Checked = CBool(xValue)
                    If row.Contains(consoles & ".pal ") Then xValue = Trim(Replace(row, consoles & ".pal", "")) : MgrSetting.CheckBox26.Checked = CBool(xValue)
                    If row.Contains(consoles & ".soundq ") Then xValue = Trim(Replace(row, consoles & ".soundq", "")) : MgrSetting.NumericUpDown8.Value = Val(xValue)

                    MgrSetting.tab_index = 10
                End If
            Case ".pce", ".hes"
                pce()
            Case ".smd", ".gen", ".32x", ".md", ".bin"
                If row.Contains(consoles & ".correct_aspect ") Then xValue = Trim(Replace(row, consoles & ".correct_aspect", "")) : MgrSetting.CheckBox34.Checked = CBool(xValue)
                If row.Contains(consoles & ".input.auto ") Then xValue = Trim(Replace(row, consoles & ".input.auto", "")) : MgrSetting.CheckBox8.Checked = CBool(xValue)

                For i = 1 To 2
                    Select Case i
                        Case 1
                            If row.Contains(consoles & ".input.port" & i & " ") Then xValue = Trim(Replace(row, consoles & ".input.port" & i, "")) : MgrSetting.ComboBox19.Text = xValue
                        Case 2
                            If row.Contains(consoles & ".input.port" & i & " ") Then xValue = Trim(Replace(row, consoles & ".input.port" & i, "")) : MgrSetting.ComboBox20.Text = xValue
                    End Select
                Next

                If row.Contains(consoles & ".region ") Then xValue = Trim(Replace(row, consoles & ".region", "")) : MgrSetting.ComboBox17.Text = xValue
                If row.Contains(consoles & ".reported_region ") Then xValue = Trim(Replace(row, consoles & ".reported_region", "")) : MgrSetting.ComboBox18.Text = xValue
                If row.Contains(consoles & ".input.multitap ") Then xValue = Trim(Replace(row, consoles & ".input.multitap", "")) : MgrSetting.ComboBox42.Text = xValue
                If row.Contains(consoles & ".input.mouse_sensitivity ") Then xValue = Trim(Replace(row, consoles & ".input.mouse_sensitivity", "")) : MgrSetting.NumericUpDown16.Value = Val(xValue)
                MgrSetting.tab_index = 12
            Case ".sms"
                If row.Contains(consoles & ".fm ") Then xValue = Trim(Replace(row, consoles & ".fm", "")) : MgrSetting.CheckBox35.Checked = CBool(xValue)
                If row.Contains(consoles & ".territory ") Then xValue = Trim(Replace(row, consoles & ".territory", "")) : MgrSetting.ComboBox21.Text = xValue
                If row.Contains(consoles & ".slend ") Then xValue = Trim(Replace(row, consoles & ".slend", "")) : MgrSetting.TrackBar17.Value = Val(xValue)
                If row.Contains(consoles & ".slendp ") Then xValue = Trim(Replace(row, consoles & ".slendp", "")) : MgrSetting.TrackBar18.Value = Val(xValue)
                If row.Contains(consoles & ".slstart ") Then xValue = Trim(Replace(row, consoles & ".slstart", "")) : MgrSetting.TrackBar19.Value = Val(xValue)
                If row.Contains(consoles & ".slstartp ") Then xValue = Trim(Replace(row, consoles & ".slstartp", "")) : MgrSetting.TrackBar20.Value = Val(xValue)
                MgrSetting.tab_index = 13
            Case ".smc", ".fig", ".sfc", ".swc", ".ufo", ".gd3", ".gd7", ".dx2", ".mgd", ".mgh", ".bs", ".st", ".spc"
                If p_c = "snes" Then
                    If row.Contains(consoles & ".correct_aspect ") Then xValue = Trim(Replace(row, consoles & ".correct_aspect", "")) : MgrSetting.CheckBox36.Checked = CBool(xValue)
                    If row.Contains(consoles & ".apu.resamp_quality ") Then xValue = Trim(Replace(row, consoles & ".apu.resamp_quality", "")) : MgrSetting.NumericUpDown17.Value = Val(xValue)
                    If row.Contains(consoles & ".h_blend ") Then xValue = Trim(Replace(row, consoles & ".h_blend", "")) : MgrSetting.CheckBox96.Checked = CBool(xValue)

                    For i = 1 To 2
                        Select Case i
                            Case 1
                                If row.Contains(consoles & ".input.port" & i & " ") Then xValue = Trim(Replace(row, consoles & ".input.port" & i, "")) : MgrSetting.ComboBox22.Text = xValue
                                If row.Contains(consoles & ".input.port" & i & ".multitap") Then xValue = Trim(Replace(row, consoles & ".input.port" & i & ".multitap", "")) : MgrSetting.CheckBox37.Checked = CBool(xValue)
                            Case 2
                                If row.Contains(consoles & ".input.port" & i & " ") Then xValue = Trim(Replace(row, consoles & ".input.port" & i, "")) : MgrSetting.ComboBox23.Text = xValue
                                If row.Contains(consoles & ".input.port" & i & ".multitap") Then xValue = Trim(Replace(row, consoles & ".input.port" & i & ".multitap", "")) : MgrSetting.CheckBox38.Checked = CBool(xValue)
                        End Select

                    Next
                    If row.Contains(consoles & ".mouse_sensitivity ") Then xValue = Trim(Replace(row, consoles & ".mouse_sensitivity", "")) : MgrSetting.NumericUpDown10.Value = Val(xValue)
                    MgrSetting.tab_index = 14
                ElseIf p_c = "snes_faust" Then
                    If row.Contains(p_c & ".spex ") Then xValue = Trim(Replace(row, p_c & ".spex", "")) : MgrSetting.CheckBox63.Checked = CBool(xValue)
                    If row.Contains(p_c & ".spex.sound ") Then xValue = Trim(Replace(row, p_c & ".spex.sound", "")) : MgrSetting.CheckBox64.Checked = CBool(xValue)
                    If row.Contains(p_c & ".resamp_quality ") Then xValue = Trim(Replace(row, p_c & ".resamp_quality", "")) : MgrSetting.NumericUpDown18.Value = Val(xValue)
                    If row.Contains(p_c & ".input.sport1.multitap ") Then xValue = Trim(Replace(row, p_c & ".input.sport1.multitap", "")) : MgrSetting.CheckBox99.Checked = CBool(xValue)
                    If row.Contains(p_c & ".input.sport2.multitap ") Then xValue = Trim(Replace(row, p_c & ".input.sport2.multitap", "")) : MgrSetting.CheckBox98.Checked = CBool(xValue)
                    If row.Contains(p_c & ".correct_aspect ") Then xValue = Trim(Replace(row, p_c & ".correct_aspect", "")) : MgrSetting.CheckBox102.Checked = CBool(xValue)

                    For i = 1 To 2
                        Select Case i
                            Case 1
                                If row.Contains(p_c & ".input.port" & i & " ") Then xValue = Trim(Replace(row, p_c & ".input.port" & i, "")) : MgrSetting.ComboBox46.Text = xValue
                            Case 2
                                If row.Contains(p_c & ".input.port" & i & " ") Then xValue = Trim(Replace(row, p_c & ".input.port" & i, "")) : MgrSetting.ComboBox47.Text = xValue
                        End Select

                    Next
                    MgrSetting.tab_index = 20
                End If
            Case ".vb", ".vboy"
                If row.Contains(consoles & ".3dmode ") Then xValue = Trim(Replace(row, consoles & ".3dmode", "")) : MgrSetting.ComboBox24.Text = xValue
                If row.Contains(consoles & ".3dreverse ") Then xValue = Trim(Replace(row, consoles & ".3dreverse", "")) : MgrSetting.CheckBox39.Checked = CBool(xValue)
                If row.Contains(consoles & ".allow_draw_skip ") Then xValue = Trim(Replace(row, consoles & ".allow_draw_skip", "")) : MgrSetting.CheckBox40.Checked = CBool(xValue)
                If row.Contains(consoles & ".anaglyph.lcolor ") Then xValue = Trim(Replace(row, consoles & ".anaglyph.lcolor", "")) : MgrSetting.Label92.Text = (xValue) : MgrSetting.Label92.BackColor = ColorTranslator.FromHtml(xValue) : MgrSetting.Label92.ForeColor = ColorTranslator.FromHtml(xValue)
                If row.Contains(consoles & ".anaglyph.preset ") Then xValue = Trim(Replace(row, consoles & ".anaglyph.preset", "")) : MgrSetting.ComboBox25.Text = xValue
                If row.Contains(consoles & ".anaglyph.rcolor ") Then xValue = Trim(Replace(row, consoles & ".anaglyph.rcolor", "")) : MgrSetting.Label93.Text = (xValue) : MgrSetting.Label93.BackColor = ColorTranslator.FromHtml(xValue) : MgrSetting.Label93.ForeColor = ColorTranslator.FromHtml(xValue)
                If row.Contains(consoles & ".cpu_emulation ") Then xValue = Trim(Replace(row, consoles & ".cpu_emulation", "")) : MgrSetting.ComboBox26.Text = xValue
                If row.Contains(consoles & ".default_color ") Then xValue = Trim(Replace(row, consoles & ".default_color", "")) : MgrSetting.Label91.Text = (xValue) : MgrSetting.Label91.BackColor = ColorTranslator.FromHtml(xValue) : MgrSetting.Label91.ForeColor = ColorTranslator.FromHtml(xValue)
                If row.Contains(consoles & ".disable_parallax ") Then xValue = Trim(Replace(row, consoles & ".disable_parallax", "")) : MgrSetting.CheckBox41.Checked = CBool(xValue)
                If row.Contains(consoles & ".input.instant_read_hack ") Then xValue = Trim(Replace(row, consoles & ".input.instant_read_hack", "")) : MgrSetting.CheckBox42.Checked = CBool(xValue)
                If row.Contains(consoles & ".instant_display_hack ") Then xValue = Trim(Replace(row, consoles & ".instant_display_hack", "")) : MgrSetting.CheckBox43.Checked = CBool(xValue)
                If row.Contains(consoles & ".liprescale ") Then xValue = Trim(Replace(row, consoles & ".liprescale", "")) : MgrSetting.NumericUpDown11.Value = Val(xValue)
                If row.Contains(consoles & ".sidebyside.separation ") Then xValue = Trim(Replace(row, consoles & ".sidebyside.separation", "")) : MgrSetting.TrackBar13.Value = Val(xValue)
                If row.Contains(consoles & ".ledonscale ") Then xValue = Trim(Replace(row, consoles & ".ledonscale", "")) : MgrSetting.NumericUpDown26.Value = Val(xValue)
                MgrSetting.tab_index = 15
            Case ".ws", ".wsr", ".wsc"
                If row.Contains(consoles & ".bday ") Then xValue = Trim(Replace(row, consoles & ".bday", "")) : MgrSetting.ComboBox27.Text = xValue
                If row.Contains(consoles & ".blood ") Then xValue = Trim(Replace(row, consoles & ".blood", "")) : MgrSetting.ComboBox30.Text = xValue
                If row.Contains(consoles & ".bmonth ") Then xValue = Trim(Replace(row, consoles & ".bmonth", "")) : MgrSetting.ComboBox28.Text = xValue
                If row.Contains(consoles & ".byear ") Then xValue = Trim(Replace(row, consoles & ".byear", "")) : MgrSetting.ComboBox29.Text = xValue
                If row.Contains(consoles & ".language ") Then xValue = Trim(Replace(row, consoles & ".language", "")) : MgrSetting.ComboBox31.Text = xValue
                If row.Contains(consoles & ".name ") Then xValue = Trim(Replace(row, consoles & ".name", "")) : MgrSetting.TextBox9.Text = xValue
                If row.Contains(consoles & ".rotateinput ") Then xValue = Trim(Replace(row, consoles & ".rotateinput", "")) : MgrSetting.CheckBox44.Checked = CBool(xValue)
                If row.Contains(consoles & ".input.builtin ") Then xValue = Trim(Replace(row, consoles & ".input.builtin", "")) : MgrSetting.ComboBox51.Text = xValue
                If row.Contains(consoles & ".sex ") Then xValue = Trim(Replace(row, consoles & ".sex", "")) : MgrSetting.ComboBox32.Text = xValue
                MgrSetting.tab_index = 16
            Case ".cue", ".m3u", ".toc", ".ccd", ".exe" '".ssf", ".minissf", ".psf", ".psf1", ".minipsf",
                SetIso()
                'If row.Contains(consoles & ".correct_aspect ") Then xValue = Trim(Replace(row, consoles & ".correct_aspect", "")) : MsgBox(xValue)
        End Select

        If row.Contains(p_c & ".forcemono ") Then xValue = Trim(Replace(row, p_c & ".forcemono", "")) : MgrSetting.CheckBox13.Checked = CBool(xValue)
        If row.Contains(p_c & MedShader & " ") Then xValue = Trim(Replace(row, p_c & MedShader, "")) : MgrSetting.ComboBox7.Text = xValue

        If row.Contains(p_c & ".shader.goat.pat ") Then xValue = Trim(Replace(row, p_c & ".shader.goat.pat", "")) : MgrSetting.ComboBox53.Text = xValue
        If row.Contains(p_c & ".shader.goat.vdiv ") Then xValue = Trim(Replace(row, p_c & ".shader.goat.vdiv", "")) : MgrSetting.NumericUpDown24.Value = Val(xValue)
        If row.Contains(p_c & ".shader.goat.hdiv ") Then xValue = Trim(Replace(row, p_c & ".shader.goat.hdiv", "")) : MgrSetting.NumericUpDown22.Value = Val(xValue)
        If row.Contains(p_c & ".shader.goat.tp ") Then xValue = Trim(Replace(row, p_c & ".shader.goat.tp", "")) : MgrSetting.NumericUpDown23.Value = Val(xValue)
        If row.Contains(p_c & ".shader.goat.fprog ") Then xValue = Trim(Replace(row, p_c & ".shader.goat.fprog", "")) : MgrSetting.CheckBox95.Checked = CBool(xValue)
        If row.Contains(p_c & ".shader.goat.slen ") Then xValue = Trim(Replace(row, p_c & ".shader.goat.slen", "")) : MgrSetting.CheckBox94.Checked = CBool(xValue)

        If row.Contains(p_c & ".scanlines ") Then xValue = Trim(Replace(row, p_c & ".scanlines", "")) : MgrSetting.TrackBar8.Value = Val(xValue)
        If row.Contains(p_c & ".special ") Then xValue = Trim(Replace(row, p_c & ".special", "")) : MgrSetting.ComboBox8.Text = xValue
        If row.Contains(p_c & ".stretch ") Then xValue = Trim(Replace(row, p_c & ".stretch", "")) : MgrSetting.ComboBox5.Text = xValue
        If row.Contains(p_c & ".tblur ") Then xValue = Trim(Replace(row, p_c & ".tblur", "")) : MgrSetting.CheckBox14.Checked = CBool(xValue)
        If row.Contains(p_c & ".tblur.accum ") Then xValue = Trim(Replace(row, p_c & ".tblur.accum", "")) : MgrSetting.CheckBox15.Checked = CBool(xValue)
        If row.Contains(p_c & ".tblur.accum.amount ") Then xValue = Trim(Replace(row, p_c & ".tblur.accum.amount", "")) : MgrSetting.TrackBar9.Value = Val(xValue)
        If row.Contains(p_c & ".videoip ") Then xValue = Trim(Replace(row, p_c & ".videoip", "")) : MgrSetting.ComboBox9.Text = xValue
        If row.Contains(p_c & ".xres ") Then xValue = Trim(Replace(row, p_c & ".xres", "")) : MgrSetting.ComboBox6.Text = xValue
        If row.Contains(p_c & ".xscale ") Then xValue = Trim(Replace(row, p_c & ".xscale", "")) : MgrSetting.NumericUpDown3.Value = Val(xValue)
        If row.Contains(p_c & ".xscalefs ") Then xValue = Trim(Replace(row, p_c & ".xscalefs", "")) : MgrSetting.NumericUpDown6.Value = Val(xValue)

        If row.Contains(p_c & ".yres ") Then
            xValue = Trim(Replace(row, p_c & ".yres", ""))
            If MgrSetting.ComboBox6.Items.Contains(MgrSetting.ComboBox6.Text & "x" & xValue) = False Then
                MgrSetting.ComboBox6.Items.Add(MgrSetting.ComboBox6.Text & "x" & xValue)
            End If
            MgrSetting.ComboBox6.Text = (MgrSetting.ComboBox6.Text & "x" & xValue)
        End If

        If row.Contains(p_c & ".yscale ") Then xValue = Trim(Replace(row, p_c & ".yscale", "")) : MgrSetting.NumericUpDown4.Value = Val(xValue)
        If row.Contains(p_c & ".yscalefs ") Then xValue = Trim(Replace(row, p_c & ".yscalefs", "")) : MgrSetting.NumericUpDown5.Value = Val(xValue)

        'BIOS
        If row.Contains("gba.bios ") Then xValue = Trim(Replace(row, "gba.bios", "")) : MgrSetting.TextBox10.Text = xValue
        If row.Contains("nes.ggrom ") Then xValue = Trim(Replace(row, "nes.ggrom", "")) : MgrSetting.TextBox11.Text = xValue
        If row.Contains(p_c & ".cdbios ") Then xValue = Trim(Replace(row, p_c & ".cdbios", "")) : MgrSetting.TextBox12.Text = xValue
        If row.Contains("pcfx.bios ") Then xValue = Trim(Replace(row, "pcfx.bios", "")) : MgrSetting.TextBox13.Text = xValue
        If row.Contains("md.cdbios ") Then xValue = Trim(Replace(row, "md.cdbios", "")) : MgrSetting.TextBox14.Text = xValue
        If row.Contains("pce.gecdbios ") Then xValue = Trim(Replace(row, "pce.gecdbios", "")) : MgrSetting.TextBox18.Text = xValue
        If row.Contains("psx.bios_eu ") Then xValue = Trim(Replace(row, "psx.bios_eu", "")) : MgrSetting.TextBox15.Text = xValue
        If row.Contains("psx.bios_jp ") Then xValue = Trim(Replace(row, "psx.bios_jp", "")) : MgrSetting.TextBox16.Text = xValue
        If row.Contains("psx.bios_na ") Then xValue = Trim(Replace(row, "psx.bios_na", "")) : MgrSetting.TextBox17.Text = xValue
        If row.Contains("ss.bios_jp ") Then xValue = Trim(Replace(row, "psx.bios_jp", "")) : MgrSetting.TextBox22.Text = xValue
        If row.Contains("ss.bios_na_eu ") Then xValue = Trim(Replace(row, "psx.bios_na", "")) : MgrSetting.TextBox23.Text = xValue

        'MEDNAFEN PATH
        If row.Contains("filesys.path_cheat ") Then xValue = Trim(Replace(row, "filesys.path_cheat", "")) : MgrSetting.TextBox34.Text = xValue
        If row.Contains("filesys.path_firmware ") Then xValue = Trim(Replace(row, "filesys.path_firmware", "")) : MgrSetting.TextBox33.Text = xValue
        If row.Contains("filesys.path_movie ") Then xValue = Trim(Replace(row, "filesys.path_movie", "")) : MgrSetting.TextBox32.Text = xValue
        If row.Contains("filesys.path_palette ") Then xValue = Trim(Replace(row, "filesys.path_palette", "")) : MgrSetting.TextBox31.Text = xValue
        If row.Contains("filesys.path_pgconfig ") Then xValue = Trim(Replace(row, "filesys.path_pgconfig", "")) : MgrSetting.TextBox30.Text = xValue
        If row.Contains("filesys.path_sav ") Then xValue = Trim(Replace(row, "filesys.path_sav", "")) : MgrSetting.TextBox29.Text = xValue
        If row.Contains("filesys.path_savbackup ") Then xValue = Trim(Replace(row, "filesys.path_savbackup", "")) : MgrSetting.TextBox28.Text = xValue
        If row.Contains("filesys.path_snap ") Then xValue = Trim(Replace(row, "filesys.path_snap", "")) : MgrSetting.TextBox27.Text = xValue
        If row.Contains("filesys.path_state ") Then xValue = Trim(Replace(row, "filesys.path_state", "")) : MgrSetting.TextBox26.Text = xValue

    End Sub

    Public Sub SetGeneral()
        If row.Contains(";") Or row.Contains("ffnosound") Then Exit Sub

        If row.Contains("autosave ") Then xValue = Trim(Replace(row, "autosave", "")) : MgrSetting.CheckBox9.Checked = CBool(xValue)
        If row.Contains("cd.image_memcache ") Then xValue = Trim(Replace(row, "cd.image_memcache", "")) : MgrSetting.CheckBox10.Checked = CBool(xValue)
        If row.Contains("cheats ") Then xValue = Trim(Replace(row, "cheats", "")) : MgrSetting.CheckBox11.Checked = CBool(xValue)
        If row.Contains("ffspeed ") Then xValue = Trim(Replace(row, "ffspeed", "")) : MgrSetting.NumericUpDown1.Value = Val(xValue)
        If row.Contains("srwautoenable ") Then xValue = Trim(Replace(row, "srwautoenable", "")) : MgrSetting.CheckBox60.Checked = CBool(xValue)
        If row.Contains("filesys.untrusted_fip_check ") Then xValue = Trim(Replace(row, "filesys.untrusted_fip_check", "")) : MgrSetting.CheckBox72.Checked = CBool(xValue)
        'If row.Contains("filesys.disablesavegz ") Then xValue = Trim(Replace(row, "filesys.disablesavegz", "")) : Setting.CheckBox12.Checked = CBool(xValue)
        If row.Contains("input.autofirefreq ") Then xValue = Trim(Replace(row, "input.autofirefreq", "")) : MgrSetting.TrackBar6.Value = Val(xValue)
        If row.Contains("input.joystick.axis_threshold ") Then xValue = Trim(Replace(row, "input.joystick.axis_threshold", "")) : MgrSetting.TrackBar7.Value = Val(xValue)
        If row.Contains("sfspeed ") Then xValue = Trim(Replace(row, "sfspeed", "")) : MgrSetting.NumericUpDown2.Value = Val(xValue)
        If row.Contains("filesys.path_firmware ") Then xValue = Trim(Replace(row, "filesys.path_firmware", "")) ': firmwarepath = xValue
        If row.Contains("fftoggle ") Then xValue = Trim(Replace(row, "fftoggle", "")) : MgrSetting.CheckBox105.Checked = CBool(xValue)
        If row.Contains("sftoggle ") Then xValue = Trim(Replace(row, "sftoggle", "")) : MgrSetting.CheckBox106.Checked = CBool(xValue)

        If row.Contains("sound ") And row.Contains("snes_faust.spex.sound ") = False Then xValue = Trim(Replace(row, "sound", "")) : MgrSetting.CheckBox1.Checked = CBool(xValue)
        If row.Contains("sound.buffer_time ") Then xValue = Trim(Replace(row, "sound.buffer_time", "")) : MgrSetting.TrackBar1.Value = Val(xValue)
        If row.Contains("sound.driver ") Then xValue = Trim(Replace(row, "sound.driver", "")) : MgrSetting.ComboBox2.Text = xValue
        If row.Contains("sound.period_time ") Then xValue = Trim(Replace(row, "sound.period_time", "")) : MgrSetting.TrackBar3.Value = Val(xValue)
        If row.Contains("sound.rate ") Then xValue = Trim(Replace(row, "sound.rate", "")) : MgrSetting.ComboBox1.Text = xValue
        If row.Contains("sound.volume ") Then xValue = Trim(Replace(row, "sound.volume", "")) : MgrSetting.TrackBar2.Value = Val(xValue)

        If row.Contains("video.blit_timesync ") Then xValue = Trim(Replace(row, "video.blit_timesync", "")) : MgrSetting.CheckBox2.Checked = CBool(xValue)
        If row.Contains("video.driver ") Then xValue = Trim(Replace(row, "video.driver", "")) : MgrSetting.ComboBox3.Text = xValue
        If row.Contains("video.deinterlacer ") Then xValue = Trim(Replace(row, "video.deinterlacer", "")) : MgrSetting.ComboBox41.Text = xValue
        If row.Contains("video.frameskip ") Then xValue = Trim(Replace(row, "video.frameskip", "")) : MgrSetting.CheckBox3.Checked = CBool(xValue)
        If row.Contains("video.fs ") Then xValue = Trim(Replace(row, "video.fs", "")) : MgrSetting.CheckBox4.Checked = CBool(xValue)
        If row.Contains("video.fs.display ") Then xValue = Trim(Replace(row, "video.fs.display", "")) : MgrSetting.NumericUpDown27.Value = xValue
        If row.Contains("video.glvsync ") Then xValue = Trim(Replace(row, "video.glvsync", "")) : MgrSetting.CheckBox5.Checked = CBool(xValue)
        If row.Contains("video.disable_composition ") Then xValue = Trim(Replace(row, "video.disable_composition", "")) : MgrSetting.CheckBox7.Checked = CBool(xValue)

        If row.Contains("video.resolution_switch ") Then xValue = Trim(Replace(row, "video.resolution_switch", "")) : MgrSetting.ComboBox57.Text = xValue

        If row.Contains("fps.autoenable ") Then xValue = Trim(Replace(row, "fps.autoenable", "")) : MgrSetting.CheckBox103.Checked = CBool(xValue)
        If row.Contains("fps.font ") Then xValue = Trim(Replace(row, "fps.font", "")) : MgrSetting.ComboBox55.Text = xValue
        If row.Contains("fps.position ") Then xValue = Trim(Replace(row, "fps.position", "")) : MgrSetting.ComboBox56.Text = xValue
        If row.Contains("fps.scale ") Then xValue = Trim(Replace(row, "fps.scale", "")) : MgrSetting.NumericUpDown28.Value = Val(xValue)
        If row.Contains("fps.textcolor ") Then xValue = Trim(Replace(row, "fps.textcolor ", "")) : MgrSetting.Label135.Text = (xValue) : MgrSetting.Label135.BackColor = ColorTranslator.FromHtml(xValue) : MgrSetting.Label135.ForeColor = ColorTranslator.FromHtml(xValue)
        If row.Contains("fps.bgcolor ") Then xValue = Trim(Replace(row, "fps.bgcolor ", "")) : MgrSetting.Label136.Text = (xValue) : MgrSetting.Label136.BackColor = ColorTranslator.FromHtml(xValue) : MgrSetting.Label136.ForeColor = ColorTranslator.FromHtml(xValue)

        If row.Contains("netplay.host ") Then xValue = Trim(Replace(row, "netplay.host", "")) : MgrSetting.cmbServer.Text = xValue
        If row.Contains("netplay.localplayers ") Then xValue = Trim(Replace(row, "netplay.localplayers", "")) : MgrSetting.NumericUpDown7.Value = Val(xValue)
        If row.Contains("netplay.nick ") Then xValue = Trim(Replace(row, "netplay.nick", "")) : MgrSetting.TextBox5.Text = xValue
        If row.Contains("netplay.password ") Then xValue = Trim(Replace(row, "netplay.password", "")) : MgrSetting.TextBox6.Text = xValue
        If row.Contains("netplay.gamekey ") Then xValue = Trim(Replace(row, "netplay.gamekey", "")) : MgrSetting.TextBox3.Text = xValue
        If row.Contains("netplay.port ") Then xValue = Trim(Replace(row, "netplay.port", "")) : MgrSetting.TextBox7.Text = xValue
        If row.Contains("netplay.smallfont ") Then xValue = Trim(Replace(row, "netplay.smallfont", "")) : MgrSetting.CheckBox16.Checked = CBool(xValue)
        If row.Contains("netplay.console.font ") Then xValue = Trim(Replace(row, "netplay.console.font", "")) : MgrSetting.ComboBox50.Text = xValue
        If row.Contains("netplay.console.scale ") Then xValue = Trim(Replace(row, "netplay.console.scale", "")) : MgrSetting.NumericUpDown21.Value = Val(xValue)
        If row.Contains("netplay.console.lines ") Then xValue = Trim(Replace(row, "netplay.console.lines", "")) : MgrSetting.NumericUpDown20.Value = Val(xValue)

        If row.Contains("qtrecord.vcodec ") Then xValue = Trim(Replace(row, "qtrecord.vcodec", "")) : MgrSetting.ComboBox4.Text = xValue
        If row.Contains("qtrecord.h_double_threshold ") Then xValue = Trim(Replace(row, "qtrecord.h_double_threshold", "")) : MgrSetting.TrackBar5.Value = Val(xValue)
        If row.Contains("qtrecord.w_double_threshold ") Then xValue = Trim(Replace(row, "qtrecord.w_double_threshold", "")) : MgrSetting.TrackBar4.Value = Val(xValue)

    End Sub

    Public Function hexToRbgNew(ByVal Hex As String) As Color
        Hex = Replace(Hex, "0x", "")
        Dim alpha As String = "&H" & Hex.Substring(0, 2)
        Hex = Replace(Hex, alpha, "", , 1)
        Dim red As String = "&H" & Hex.Substring(0, 2)
        Hex = Replace(Hex, red, "", , 1)
        Dim green As String = "&H" & Hex.Substring(0, 2)
        Hex = Replace(Hex, green, "", , 1)
        Dim blue As String = "&H" & Hex.Substring(0, 2)
        Hex = Replace(Hex, blue, "", , 1)
        Return Color.FromArgb(alpha, red, green, blue)
    End Function

    Public Sub pce()

        'If row.Contains(p_c & ".adpcmlp ") Then xValue = Trim(Replace(row, p_c & ".adpcmlp", "")) : Setting.CheckBox28.Checked = CBool(xValue)
        If row.Contains(p_c & ".adpcmextraprec ") Then xValue = Trim(Replace(row, p_c & ".adpcmextraprec", "")) : MgrSetting.CheckBox28.Checked = CBool(xValue)
        If row.Contains(p_c & ".adpcmvolume ") Then xValue = Trim(Replace(row, p_c & ".adpcmvolume", "")) : MgrSetting.TrackBar10.Value = Val(xValue)
        If row.Contains(p_c & ".arcadecard ") Then xValue = Trim(Replace(row, p_c & ".arcadecard", "")) : MgrSetting.CheckBox29.Checked = CBool(xValue)
        If row.Contains(p_c & ".cddavolume ") Then xValue = Trim(Replace(row, p_c & ".cddavolume", "")) : MgrSetting.TrackBar11.Value = Val(xValue)
        If row.Contains(p_c & ".cdpsgvolume ") Then xValue = Trim(Replace(row, p_c & ".cdpsgvolume", "")) : MgrSetting.TrackBar12.Value = Val(xValue)
        If row.Contains(p_c & ".resamp_quality ") Then xValue = Trim(Replace(row, p_c & ".resamp_quality", "")) : MgrSetting.NumericUpDown14.Value = Val(xValue)
        If row.Contains(p_c & ".forcesgx ") Then xValue = Trim(Replace(row, p_c & ".forcesgx", "")) : MgrSetting.CheckBox30.Checked = CBool(xValue)
        If row.Contains(p_c & ".h_overscan ") Then xValue = Trim(Replace(row, p_c & ".h_overscan", "")) : MgrSetting.CheckBox31.Checked = CBool(xValue)
        If row.Contains(p_c & ".input.multitap ") Then xValue = Trim(Replace(row, p_c & ".input.multitap", "")) : MgrSetting.CheckBox33.Checked = CBool(xValue)
        If row.Contains(p_c & ".adpcmlp ") Then xValue = Trim(Replace(row, p_c & ".adpcmlp", "")) : MgrSetting.CheckBox70.Checked = CBool(xValue)
        If row.Contains(p_c & ".correct_aspect ") Then xValue = Trim(Replace(row, p_c & ".correct_aspect", "")) : MgrSetting.CheckBox71.Checked = CBool(xValue)
        If row.Contains(p_c & ".ocmultiplier ") Then xValue = Trim(Replace(row, p_c & ".ocmultiplier", "")) : MgrSetting.NumericUpDown25.Value = Val(xValue)

        For i = 1 To 5
            Select Case i
                Case 1
                    If row.Contains(p_c & ".input.port" & i & " ") Then xValue = Trim(Replace(row, p_c & ".input.port" & i, "")) : MgrSetting.ComboBox16.Text = xValue
                Case 2
                    If row.Contains(p_c & ".input.port" & i & " ") Then xValue = Trim(Replace(row, p_c & ".input.port" & i, "")) : MgrSetting.ComboBox40.Text = xValue
            End Select
        Next

        If row.Contains(p_c & ".mouse_sensitivity ") Then xValue = Trim(Replace(row, p_c & ".mouse_sensitivity", "")) : MgrSetting.NumericUpDown9.Value = Val(xValue)
        If row.Contains(p_c & ".nospritelimit ") Then xValue = Trim(Replace(row, p_c & ".nospritelimit", "")) : MgrSetting.CheckBox32.Checked = CBool(xValue)
        If row.Contains(p_c & ".psgrevision ") Then xValue = Trim(Replace(row, p_c & ".psgrevision", "")) : MgrSetting.ComboBox15.Text = xValue
        MgrSetting.tab_index = 11
    End Sub

    Public Sub SetIso()
        Select Case consoles
            Case "pcfx"
                If row.Contains(consoles & ".adpcm.emulate_buggy_codec ") Then xValue = Trim(Replace(row, consoles & ".adpcm.emulate_buggy_codec", "")) : MgrSetting.CheckBox45.Checked = CBool(xValue)
                If row.Contains(consoles & ".adpcm.suppress_channel_reset_clicks ") Then xValue = Trim(Replace(row, consoles & ".adpcm.suppress_channel_reset_clicks", "")) : MgrSetting.CheckBox46.Checked = CBool(xValue)
                If row.Contains(consoles & ".cdspeed ") Then xValue = Trim(Replace(row, consoles & ".cdspeed", "")) : MgrSetting.TrackBar14.Value = Val(xValue)
                If row.Contains(consoles & ".cpu_emulation ") Then xValue = Trim(Replace(row, consoles & ".cpu_emulation", "")) : MgrSetting.ComboBox33.Text = xValue
                If row.Contains(consoles & ".high_dotclock_width ") Then xValue = Trim(Replace(row, consoles & ".high_dotclock_width", "")) : MgrSetting.ComboBox34.Text = xValue
                If row.Contains(consoles & ".resamp_quality ") Then xValue = Trim(Replace(row, consoles & ".resamp_quality", "")) : MgrSetting.NumericUpDown15.Value = Val(xValue)

                For i = 1 To 8
                    Select Case i
                        Case 1
                            If row.Contains(consoles & ".input.port" & i & " ") Then xValue = Trim(Replace(row, consoles & ".input.port" & i, "")) : MgrSetting.ComboBox35.Text = xValue
                            If row.Contains(consoles & ".input.port" & i & ".multitap ") Then xValue = Trim(Replace(row, consoles & ".input.port" & i & ".multitap", "")) : MgrSetting.CheckBox50.Checked = CBool(xValue)
                        Case 2
                            If row.Contains(consoles & ".input.port" & i & " ") Then xValue = Trim(Replace(row, consoles & ".input.port" & i, "")) : MgrSetting.ComboBox36.Text = xValue
                            If row.Contains(consoles & ".input.port" & i & ".multitap ") Then xValue = Trim(Replace(row, consoles & ".input.port" & i & ".multitap", "")) : MgrSetting.CheckBox49.Checked = CBool(xValue)
                    End Select
                Next

                If row.Contains(consoles & ".mouse_sensitivity ") Then xValue = Trim(Replace(row, consoles & ".mouse_sensitivity", "")) : MgrSetting.NumericUpDown12.Value = Val(xValue)
                If row.Contains(consoles & ".nospritelimit ") Then xValue = Trim(Replace(row, consoles & ".nospritelimit", "")) : MgrSetting.CheckBox47.Checked = CBool(xValue)
                If row.Contains(consoles & ".rainbow.chromaip ") Then xValue = Trim(Replace(row, consoles & ".rainbow.chromaip", "")) : MgrSetting.CheckBox48.Checked = CBool(xValue)
                MgrSetting.tab_index = 17
            Case "psx"

                If row.Contains(consoles & ".input.analog_mode_ct ") Then xValue = Trim(Replace(row, consoles & ".input.analog_mode_ct", "")) : MgrSetting.CheckBox51.Checked = CBool(xValue)
                If row.Contains(consoles & ".input.mouse_sensitivity ") Then xValue = Trim(Replace(row, consoles & ".input.mouse_sensitivity", "")) : MgrSetting.NumericUpDown13.Value = Val(xValue)

                For i = 1 To 8
                    Select Case i
                        Case 1
                            If row.Contains(consoles & ".input.pport" & i & ".multitap ") Then xValue = Trim(Replace(row, consoles & ".input.pport" & i & ".multitap", "")) : MgrSetting.CheckBox54.Checked = CBool(xValue)
                            If row.Contains(consoles & ".input.port" & i & " ") Then xValue = Trim(Replace(row, consoles & ".input.port" & i, "")) : MgrSetting.ComboBox37.Text = xValue
                            If row.Contains(consoles & ".input.port" & i & ".memcard ") Then xValue = Trim(Replace(row, consoles & ".input.port" & i & ".memcard", "")) : MgrSetting.CheckBox52.Checked = CBool(xValue)
                            If row.Contains(consoles & ".input.port" & i & ".gun_chairs ") Then xValue = Trim(Replace(row, consoles & ".input.port" & i & ".gun_chairs", "")) : MgrSetting.Label86.Text = (xValue) : MgrSetting.Label86.BackColor = ColorTranslator.FromHtml(xValue) : MgrSetting.Label86.ForeColor = ColorTranslator.FromHtml(xValue)
                        Case 2
                            If row.Contains(consoles & ".input.pport" & i & ".multitap ") Then xValue = Trim(Replace(row, consoles & ".input.pport" & i & ".multitap", "")) : MgrSetting.CheckBox56.Checked = CBool(xValue)
                            If row.Contains(consoles & ".input.port" & i & " ") Then xValue = Trim(Replace(row, consoles & ".input.port" & i, "")) : MgrSetting.ComboBox38.Text = xValue
                            If row.Contains(consoles & ".input.port" & i & ".memcard ") Then xValue = Trim(Replace(row, consoles & ".input.port" & i & ".memcard", "")) : MgrSetting.CheckBox53.Checked = CBool(xValue)
                            If row.Contains(consoles & ".input.port" & i & ".gun_chairs ") Then xValue = Trim(Replace(row, consoles & ".input.port" & i & ".gun_chairs", "")) : MgrSetting.Label87.Text = (xValue) : MgrSetting.Label87.BackColor = ColorTranslator.FromHtml(xValue) : MgrSetting.Label87.ForeColor = ColorTranslator.FromHtml(xValue)
                    End Select

                Next

                If row.Contains(consoles & ".input.analog_mode_ct.compare") Then
                    xValue = Trim(Replace(row, consoles & ".input.analog_mode_ct.compare", ""))

                    Dim n = Convert.ToString(Convert.ToInt32(xValue, 16), 2)
                    Dim str As [String] = StrReverse(n)
                    Dim binres As String
                    For Each c As Char In str
                        binres = c.ToString & binres

                        Select Case binres
                            Case 1
                                MgrSetting.CheckBox78.Checked = True
                            Case 10
                                MgrSetting.CheckBox80.Checked = True
                            Case 100
                                MgrSetting.CheckBox81.Checked = True
                            Case 1000
                                MgrSetting.CheckBox79.Checked = True
                            Case 10000
                                MgrSetting.CheckBox90.Checked = True
                            Case 100000
                                MgrSetting.CheckBox93.Checked = True
                            Case 1000000
                                MgrSetting.CheckBox91.Checked = True
                            Case 10000000
                                MgrSetting.CheckBox92.Checked = True
                            Case 100000000
                                MgrSetting.CheckBox82.Checked = True
                            Case 1000000000
                                MgrSetting.CheckBox83.Checked = True
                            Case 10000000000
                                MgrSetting.CheckBox85.Checked = True
                            Case 100000000000
                                MgrSetting.CheckBox84.Checked = True
                            Case 1000000000000
                                MgrSetting.CheckBox86.Checked = True
                            Case 10000000000000
                                MgrSetting.CheckBox89.Checked = True
                            Case 100000000000000
                                MgrSetting.CheckBox88.Checked = True
                            Case 1000000000000000
                                MgrSetting.CheckBox87.Checked = True
                        End Select
                        If binres.Contains("1") Then binres = Replace(binres, "1", "0")
                    Next
                End If

                If row.Contains(consoles & ".region_autodetect ") Then xValue = Trim(Replace(row, consoles & ".region_autodetect", "")) : MgrSetting.CheckBox55.Checked = CBool(xValue)
                If row.Contains(consoles & ".h_overscan ") Then xValue = Trim(Replace(row, consoles & ".h_overscan", "")) : MgrSetting.CheckBox57.Checked = CBool(xValue)
                If row.Contains(consoles & ".bios_sanity ") Then xValue = Trim(Replace(row, consoles & ".bios_sanity", "")) : MgrSetting.CheckBox58.Checked = CBool(xValue)
                If row.Contains(consoles & ".region_default ") Then xValue = Trim(Replace(row, consoles & ".region_default", "")) : MgrSetting.ComboBox39.Text = xValue
                If row.Contains(consoles & ".spu.resamp_quality ") Then xValue = Trim(Replace(row, consoles & ".spu.resamp_quality", "")) : MgrSetting.TrackBar15.Value = Val(xValue)
                MgrSetting.tab_index = 18
            Case "ss"
                For i = 1 To 12
                    Select Case i
                        Case 1
                            If row.Contains(consoles & ".input.port" & i & " ") Then xValue = Trim(Replace(row, consoles & ".input.port" & i, "")) : MgrSetting.ComboBox45.Text = xValue
                            If row.Contains(consoles & ".input.port" & i & ".3dpad.mode.defpos ") Then xValue = Trim(Replace(row, consoles & ".input.port" & i & ".3dpad.mode.defpos ", "")) : MgrSetting.ComboBox52.Text = xValue
                        Case 2
                            If row.Contains(consoles & ".input.port" & i & " ") Then xValue = Trim(Replace(row, consoles & ".input.port" & i, "")) : MgrSetting.ComboBox44.Text = xValue
                    End Select

                Next

                If row.Contains(consoles & ".region_autodetect ") Then xValue = Trim(Replace(row, consoles & ".region_autodetect", "")) : MgrSetting.CheckBox62.Checked = CBool(xValue)
                If row.Contains(consoles & ".region_default ") Then xValue = Trim(Replace(row, consoles & ".region_default", "")) : MgrSetting.ComboBox43.Text = xValue
                If row.Contains(consoles & ".scsp.resamp_quality ") Then xValue = Trim(Replace(row, consoles & ".scsp.resamp_quality", "")) : MgrSetting.TrackBar16.Value = Val(xValue)
                If row.Contains(consoles & ".smpc.autortc.lang ") Then xValue = Trim(Replace(row, consoles & ".smpc.autortc.lang", "")) : MgrSetting.ComboBox48.Text = xValue
                If row.Contains(consoles & ".cart" & GlobalVar.SScart & " ") Then xValue = Trim(Replace(row, consoles & ".cart" & GlobalVar.SScart, "")) : MgrSetting.ComboBox49.Text = xValue
                If row.Contains(consoles & ".bios_sanity ") Then xValue = Trim(Replace(row, consoles & ".bios_sanity", "")) : MgrSetting.CheckBox67.Checked = CBool(xValue)
                If row.Contains(consoles & ".cd_sanity ") Then xValue = Trim(Replace(row, consoles & ".cd_sanity", "")) : MgrSetting.CheckBox68.Checked = CBool(xValue)
                If row.Contains(consoles & ".smpc.autortc ") Then xValue = Trim(Replace(row, consoles & ".smpc.autortc", "")) : MgrSetting.CheckBox66.Checked = CBool(xValue)
                If row.Contains(consoles & ".cart.kof95_path ") Then xValue = Trim(Replace(row, consoles & ".cart.kof95_path", "")) : MgrSetting.TextBox2.Text = xValue
                If row.Contains(consoles & ".cart.ultraman_path ") Then xValue = Trim(Replace(row, consoles & ".cart.ultraman_path", "")) : MgrSetting.TextBox1.Text = xValue
                If row.Contains(consoles & ".midsync ") Then xValue = Trim(Replace(row, consoles & ".midsync", "")) : MgrSetting.CheckBox65.Checked = CBool(xValue)
                If row.Contains(consoles & ".correct_aspect ") Then xValue = Trim(Replace(row, consoles & ".correct_aspect", "")) : MgrSetting.CheckBox75.Checked = CBool(xValue)
                If row.Contains(consoles & ".h_blend ") Then xValue = Trim(Replace(row, consoles & ".h_blend", "")) : MgrSetting.CheckBox76.Checked = CBool(xValue)
                If row.Contains(consoles & ".h_overscan ") Then xValue = Trim(Replace(row, consoles & ".h_overscan", "")) : MgrSetting.CheckBox77.Checked = CBool(xValue)
                If row.Contains(consoles & ".input.mouse_sensitivity ") Then xValue = Trim(Replace(row, consoles & ".input.mouse_sensitivity", "")) : MgrSetting.NumericUpDown19.Value = Val(xValue)
                If row.Contains(consoles & ".input.sport1.multitap ") Then xValue = Trim(Replace(row, consoles & ".input.sport1.multitap", "")) : MgrSetting.CheckBox101.Checked = CBool(xValue)
                If row.Contains(consoles & ".input.sport2.multitap ") Then xValue = Trim(Replace(row, consoles & ".input.sport2.multitap", "")) : MgrSetting.CheckBox100.Checked = CBool(xValue)
                MgrSetting.tab_index = 19
            Case "pce"
                pce()
        End Select

    End Sub

    Public Sub update_setting()
        'Setting.Text = scan.real_name & " Setting"
        MgrSetting.Label1.Text = "Sound Buffer: " & MgrSetting.TrackBar1.Value
        MgrSetting.Label3.Text = "Sound Volume: " & MgrSetting.TrackBar2.Value
        MgrSetting.Label5.Text = "Sound Period Time: " & MgrSetting.TrackBar3.Value
        MgrSetting.Label8.Text = "Width Double Threshold: " & MgrSetting.TrackBar4.Value
        MgrSetting.Label9.Text = "Heigth Double Threshold: " & MgrSetting.TrackBar5.Value
        MgrSetting.Label12.Text = "Auto-fire frequency: " & MgrSetting.TrackBar6.Value
        MgrSetting.Label13.Text = "Joystick Axis Threshold: " & MgrSetting.TrackBar7.Value
        MgrSetting.Label24.Text = "Scanlines: " & MgrSetting.TrackBar8.Value
        MgrSetting.Label26.Text = "Blur Accumulation: " & MgrSetting.TrackBar9.Value
        MgrSetting.Label39.Text = "ADPCM Volume: " & MgrSetting.TrackBar10.Value
        MgrSetting.Label40.Text = "CD-DA Volume: " & MgrSetting.TrackBar11.Value
        MgrSetting.Label41.Text = "PSG Volume: " & MgrSetting.TrackBar12.Value
        MgrSetting.Label57.Text = "N° Pixels L/R to Views: " & MgrSetting.TrackBar13.Value
        MgrSetting.Label76.Text = "Emulated CD-ROM speed: " & MgrSetting.TrackBar14.Value
        MgrSetting.Label90.Text = "SPU Output Resampler Quality: " & MgrSetting.TrackBar15.Value
        MgrSetting.Label104.Text = "SCSP Output Resampler Quality: " & MgrSetting.TrackBar16.Value
        MgrSetting.Label120.Text = "Last scanline in NTSC mode: " & MgrSetting.TrackBar17.Value
        MgrSetting.Label121.Text = "Last scanline in PAL mode: " & MgrSetting.TrackBar18.Value
        MgrSetting.Label122.Text = "First scanline in NTSC mode: " & MgrSetting.TrackBar19.Value
        MgrSetting.Label123.Text = "First scanline in PAL mode: " & MgrSetting.TrackBar20.Value
    End Sub

    Public Function ExtractPath(parameter As String)
        Try
            Dim reader As StreamReader = My.Computer.FileSystem.OpenTextFileReader(Path.Combine(MedGuiR.TextBox4.Text, DMedConf & ".cfg"))
            Dim a As String
            Dim SplitPath() As String
            Dim ReturnedPath As String

            Do
                a = reader.ReadLine
                If a.Contains("filesys." & parameter) Then
                    SplitPath = Split(a, " ")

                    If SplitPath(1).Contains(":\") Then
                        If SplitPath.Length > 1 Then
                            For i = 1 To SplitPath.Length - 1
                                ReturnedPath += SplitPath(i).Trim & " "
                            Next
                        Else
                            ReturnedPath = SplitPath(1).Trim
                        End If
                    Else
                        If SplitPath(1).Trim = "" Then
                            Select Case parameter
                                Case "path_cheat"
                                    SplitPath(1) = "cheats"
                                Case "path_pgconfig"
                                    SplitPath(1) = "pgconfig"
                            End Select
                        End If
                        ReturnedPath = Path.Combine(MedGuiR.TextBox4.Text, SplitPath(1).Trim)
                    End If

                        Exit Do
                    End If
            Loop Until a Is Nothing
            reader.Close()
            Return (ReturnedPath.Trim & "\")
        Catch
        End Try
    End Function

End Module