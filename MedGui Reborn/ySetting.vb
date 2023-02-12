Module ySetting

    Public pArg, sound, video, filters, various, netplay_, record, custom, apple2, lynx, gameboy, neogeop, famicom, pcengine, pcfx,
    genesis, mastersystem, snes, vboy, wswan, psx, minput, tminput, ss, specBios, s_record As String

    Public Sub parMednafen()
        Dim lfm As String

        'If MedGuiR.CheckBox1.Checked = True And consoles = "pce" Then MedGuiR.tpce = "_fast" Else MedGuiR.tpce = ""

        If consoles = "pce" Then
            If MedGuiR.CheckBox1.Checked = False Then MedGuiR.tpce = Nothing Else MedGuiR.tpce = "_fast"
        ElseIf consoles = "snes" Then
            If MedGuiR.CheckBox15.Checked = False Then MedGuiR.tpce = Nothing Else MedGuiR.tpce = "_faust"
        Else : MedGuiR.tpce = Nothing
        End If

        If consoles = "nes" Or consoles = "apple2" Then lfm = "" Else lfm = " -" & consoles & MedGuiR.tpce & ".forcemono " & MgrSetting.CheckBox13.CheckState
        'Sound
        sound = " -sound " & MgrSetting.CheckBox1.CheckState & lfm &
        " -sound.buffer_time " & MgrSetting.TrackBar1.Value & " -sound.period_time " & MgrSetting.TrackBar3.Value & " -sound.volume " & MgrSetting.TrackBar2.Value &
        " -sound.driver " & MgrSetting.ComboBox2.Text & " -sound.rate " & MgrSetting.ComboBox1.Text & " -sound.device " & Chr(34) & MgrSetting.ComboBox68.Text.ToString & Chr(34)

        'Video
        Dim xfs, yfs As String
        Dim i As Integer = 0
        Dim s() As String
        s = Split(MgrSetting.ComboBox6.Text, "x")
        For i = 0 To UBound(s)
            xfs = s(0)
            yfs = s(1)
        Next

        'Video
        video = " -video.driver " & MgrSetting.ComboBox3.Text & " -video.deinterlacer " & MgrSetting.ComboBox41.Text & " -video.blit_timesync " & MgrSetting.CheckBox2.CheckState & " -video.disable_composition " & MgrSetting.CheckBox7.CheckState & " -video.frameskip " & MgrSetting.CheckBox3.CheckState &
        " -video.fs " & MgrSetting.CheckBox4.CheckState & " -video.glvsync " & MgrSetting.CheckBox5.CheckState & " -" & consoles & MedGuiR.tpce & ".stretch " & MgrSetting.ComboBox5.Text &
         " -" & consoles & MedGuiR.tpce & ".xres " & xfs & " -" & consoles & MedGuiR.tpce & ".yres " & yfs & " -" & consoles & MedGuiR.tpce & ".xscale " & Replace(MgrSetting.NumericUpDown3.Value, ",", ".") & " -" & consoles & MedGuiR.tpce & ".yscale " & Replace(MgrSetting.NumericUpDown4.Value, ",", ".") &
         " -" & consoles & MedGuiR.tpce & ".xscalefs " & Replace(MgrSetting.NumericUpDown6.Value, ",", ".") & " -" & consoles & MedGuiR.tpce & ".yscalefs " & Replace(MgrSetting.NumericUpDown5.Value, ",", ".")

        'Filters
        filters = " -" & consoles & MedGuiR.tpce & MedShader & " " & MgrSetting.ComboBox7.Text & " -" & consoles & MedGuiR.tpce & ".special " & MgrSetting.ComboBox8.Text & " -" & consoles & MedGuiR.tpce & ".videoip " & MgrSetting.ComboBox9.Text &
        " -" & consoles & MedGuiR.tpce & ".scanlines " & MgrSetting.TrackBar8.Value & " -" & consoles & MedGuiR.tpce & ".tblur " & MgrSetting.CheckBox14.CheckState & " -" & consoles & MedGuiR.tpce & ".tblur.accum " & MgrSetting.CheckBox15.CheckState &
        " -" & consoles & MedGuiR.tpce & ".tblur.accum.amount " & MgrSetting.TrackBar9.Value

        'Specific video setting for music module
        Select Case LCase(ext)
            Case ".minigsf", ".gsflib", ".nsf", ".hes", ".psf", ".psf1", ".minipsf", ".ssf", ".minissf", ".rsn", ".spc", ".wsr"
                video = " -player.stretch " & MgrSetting.ComboBox5.Text & " -player.xres " & xfs & " -player.yres " & yfs &
                               " -player.xscale " & Replace(MgrSetting.NumericUpDown3.Value, ",", ".") & " -player.yscale " & Replace(MgrSetting.NumericUpDown4.Value, ",", ".") &
                " -player.xscalefs " & Replace(MgrSetting.NumericUpDown6.Value, ",", ".") & " -player.yscalefs " & Replace(MgrSetting.NumericUpDown5.Value, ",", ".")

                filters = " -player" & MedShader & " " & MgrSetting.ComboBox7.Text & " -player.special " & MgrSetting.ComboBox8.Text & " -player.videoip " & MgrSetting.ComboBox9.Text &
                " -player.scanlines " & MgrSetting.TrackBar8.Value & " -player.tblur " & MgrSetting.CheckBox14.CheckState & " -player.tblur.accum " & MgrSetting.CheckBox15.CheckState &
                " -player.tblur.accum.amount " & MgrSetting.TrackBar9.Value
        End Select

        'Variuos
        various = " -autosave " & MgrSetting.CheckBox9.CheckState & " -cd.image_memcache " & MgrSetting.CheckBox10.CheckState & " -cheats " & MgrSetting.CheckBox11.CheckState & " -srwautoenable " & MgrSetting.CheckBox60.CheckState &
        " -ffspeed " & Replace(MgrSetting.NumericUpDown1.Value, ",", ".") & " -sfspeed " & Replace(MgrSetting.NumericUpDown2.Value, ",", ".") &
        " -input.autofirefreq " & MgrSetting.TrackBar6.Value & " -input.joystick.axis_threshold " & MgrSetting.TrackBar7.Value & " -filesys.untrusted_fip_check " & MgrSetting.CheckBox72.CheckState &
        " -fftoggle " & MgrSetting.CheckBox105.CheckState & " -sftoggle " & MgrSetting.CheckBox106.CheckState '& " -filesys.disablesavegz " & Setting.CheckBox12.CheckState

        'Netplay
        Dim nnick, npass, nport, nkey As String
        nnick = " -netplay.nick " & Chr(34) & MgrSetting.TextBox5.Text & Chr(34)
        npass = " -netplay.password " & Chr(34) & MgrSetting.TextBox6.Text & Chr(34)
        nport = " -netplay.port " & Chr(34) & MgrSetting.TextBox7.Text & Chr(34)
        nkey = " -netplay.gamekey " & Chr(34) & MgrSetting.TextBox3.Text & Chr(34)
        'If Setting.TextBox5.Text = "" Then nnick = ""
        'If Setting.TextBox6.Text = "" Then npass = ""
        'If Setting.TextBox7.Text = "" Then nport = ""
        'If Setting.TextBox3.Text = "" Then nkey = ""

        netplay_ = " -netplay.host " & Chr(34) & MgrSetting.cmbServer.Text & Chr(34) & " -netplay.localplayers " & Replace(MgrSetting.NumericUpDown7.Value, ",", ".") & nnick &
        npass & nport & nkey & " -netplay.smallfont " & MgrSetting.CheckBox16.CheckState & " -netplay.console.lines " & Replace(MgrSetting.NumericUpDown20.Value, ",", ".") &
        " -netplay.console.scale " & Replace(MgrSetting.NumericUpDown21.Value, ",", ".") & " -netplay.console.font " & MgrSetting.ComboBox50.Text

        'Record

        'Dim mrec, srec As String
        'If Setting.CheckBox7.Checked = True Then srec = " -soundrecord " & Chr(34) & MedExtra & "Media\Audio\" & Setting.TextBox3.Text & ".wav" & Chr(34) Else srec = ""
        'If Setting.CheckBox6.Checked = True Then mrec = " -qtrecord " & (Chr(34) & MedExtra & "Media\Movie\" & Setting.TextBox3.Text & ".mov" & Chr(34)) Else mrec = ""

        s_record = " -qtrecord.vcodec " & MgrSetting.ComboBox4.Text & " -qtrecord.h_double_threshold " & MgrSetting.TrackBar5.Value & " -qtrecord.w_double_threshold " & MgrSetting.TrackBar4.Value '&
        'mrec & srec

        'Apple II/+
        apple2 = " -apple2.input.port1 " & MgrSetting.ComboBox58.Text & " -apple2.input.port1.joystick.axis_scale " & Replace(MgrSetting.NumericUpDown41.Value, ",", ".") &
            " -apple2.input.port2 " & MgrSetting.ComboBox60.Text & " -apple2.video.matrix " & MgrSetting.ComboBox61.Text & " -apple2.video.brightness " & Replace(MgrSetting.NumericUpDown29.Value, ",", ".") &
            " -apple2.video.contrast " & Replace(MgrSetting.NumericUpDown31.Value, ",", ".") & " -apple2.video.hue " & Replace(MgrSetting.NumericUpDown32.Value, ",", ".") &
            " -apple2.video.saturation " & Replace(MgrSetting.NumericUpDown33.Value, ",", ".") & " -apple2.video.color_lumafilter " & Replace(MgrSetting.NumericUpDown30.Value, ",", ".") &
            " -apple2.video.force_mono " & MgrSetting.Label163.Text & " -apple2.video.mono_lumafilter " & Replace(MgrSetting.NumericUpDown34.Value, ",", ".") &
            " -apple2.video.matrix.blue.i " & Replace(MgrSetting.NumericUpDown35.Value, ",", ".") & " -apple2.video.matrix.blue.q " & Replace(MgrSetting.NumericUpDown36.Value, ",", ".") &
            " -apple2.video.matrix.green.i " & Replace(MgrSetting.NumericUpDown37.Value, ",", ".") & " -apple2.video.matrix.green.q " & Replace(MgrSetting.NumericUpDown38.Value, ",", ".") &
            " -apple2.video.matrix.red.i " & Replace(MgrSetting.NumericUpDown39.Value, ",", ".") & " -apple2.video.matrix.red.q " & Replace(MgrSetting.NumericUpDown40.Value, ",", ".") &
            " -apple2.video.mixed_text_mono " & MgrSetting.CheckBox108.CheckState & " -apple2.video.color_smooth " & MgrSetting.CheckBox107.CheckState

        'Lynx
        lynx = " -lynx.lowpass " & MgrSetting.CheckBox18.CheckState & " -lynx.rotateinput " & MgrSetting.CheckBox17.CheckState

        'Gameboy
        gameboy = " -gb.system_type " & MgrSetting.ComboBox10.Text

        'NeoGeo Pocket
        neogeop = " -ngp.language " & MgrSetting.ComboBox11.Text

        'NES
        minput = ""
        'If Setting.CheckBox24.Checked = False And Setting.ComboBox13.Text = "gamepad" Then
        For i = 1 To 2
            tminput = " -nes.input.port" & i & " " & MgrSetting.ComboBox13.Text
            minput = minput & tminput
        Next
        'Else
        'minput = " -nes.input.port1 " & Setting.ComboBox13.Text
        'End If

        famicom = " -nes.clipsides " & MgrSetting.CheckBox19.CheckState & " -nes.correct_aspect " & MgrSetting.CheckBox20.CheckState & " -nes.gg " & MgrSetting.CheckBox21.CheckState &
        minput & " -nes.n106bs " & MgrSetting.CheckBox22.CheckState & " -nes.no8lim " & MgrSetting.CheckBox23.CheckState & " -nes.input.fcexp " & MgrSetting.ComboBox12.Text &
        " -nes.nofs " & MgrSetting.CheckBox24.CheckState & " -nes.ntsc.mergefields " & MgrSetting.CheckBox27.CheckState & " -nes.ntsc.preset " & MgrSetting.ComboBox14.Text &
        " -nes.ntscblitter " & MgrSetting.CheckBox25.CheckState & " -nes.pal " & MgrSetting.CheckBox26.CheckState & " -nes.soundq " & Replace(MgrSetting.NumericUpDown8.Value, ",", ".")

        'PC-Engine
        '" -pce" & MedGuiR.tpce & ".adpcmlp " & Setting.CheckBox28.CheckState &
        Dim pcesett As String
        minput = ""
        If MgrSetting.CheckBox74.Checked = False Then
            For i = 1 To 5
                MgrSetting.ComboBox40.Text = MgrSetting.ComboBox16.Text
                tminput = " -pce" & MedGuiR.tpce & ".input.port" & i & " " & MgrSetting.ComboBox16.Text
                minput = minput & tminput
            Next
        Else
            minput = " -pce" & MedGuiR.tpce & ".input.port1 " & MgrSetting.ComboBox16.Text & " -pce" & MedGuiR.tpce & ".input.port2 " & MgrSetting.ComboBox40.Text
        End If

        If MedGuiR.CheckBox1.Checked = False Then
            pcesett = " -pce" & MedGuiR.tpce & ".adpcmextraprec " & MgrSetting.CheckBox28.CheckState & " -pce" & MedGuiR.tpce & ".resamp_quality " & Replace(MgrSetting.NumericUpDown14.Value, ",", ".") '& _
        Else
            pcesett = " -pce" & MedGuiR.tpce & ".adpcmlp " & MgrSetting.CheckBox70.CheckState & " -pce" & MedGuiR.tpce & ".correct_aspect " & MgrSetting.CheckBox71.CheckState &
                " -pce" & MedGuiR.tpce & ".ocmultiplier " & Replace(MgrSetting.NumericUpDown25.Value, ",", ".")
        End If
        pcengine = pcesett & " -pce" & MedGuiR.tpce & ".adpcmvolume " & MgrSetting.TrackBar10.Value & " -pce" & MedGuiR.tpce & ".cddavolume " & MgrSetting.TrackBar11.Value &
        " -pce" & MedGuiR.tpce & ".cdpsgvolume " & MgrSetting.TrackBar12.Value & " -pce" & MedGuiR.tpce & ".arcadecard " & MgrSetting.CheckBox29.CheckState & " -pce" & MedGuiR.tpce & ".forcesgx " & MgrSetting.CheckBox30.CheckState &
        " -pce" & ".h_overscan " & MgrSetting.CheckBox31.CheckState & " -pce" & MedGuiR.tpce & ".nospritelimit " & MgrSetting.CheckBox32.CheckState & " -pce" & ".psgrevision " & MgrSetting.ComboBox15.Text &
        " -pce" & ".input.multitap " & MgrSetting.CheckBox33.CheckState & minput & " -pce" & MedGuiR.tpce & ".mouse_sensitivity " & Replace(MgrSetting.NumericUpDown9.Value, ",", ".")

        'Genesis
        minput = ""
        If MgrSetting.CheckBox73.Checked = False Then
            For i = 1 To 8
                MgrSetting.ComboBox20.Text = MgrSetting.ComboBox19.Text
                tminput = " -md.input.port" & i & " " & MgrSetting.ComboBox19.Text
                minput = minput & tminput
            Next
        Else
            minput = " -md.input.port1 " & MgrSetting.ComboBox19.Text & " -md.input.port2 " & MgrSetting.ComboBox20.Text
        End If

        genesis = " -md.correct_aspect " & MgrSetting.CheckBox34.CheckState & " -md.input.auto " & MgrSetting.CheckBox8.CheckState & " -md.region " & MgrSetting.ComboBox17.Text & " -md.reported_region " & MgrSetting.ComboBox18.Text &
        " -md.input.multitap " & MgrSetting.ComboBox42.Text & minput & " -md.input.mouse_sensitivity " & Replace(MgrSetting.NumericUpDown16.Value, ",", ".")

        'Master System
        mastersystem = " -sms.fm " & MgrSetting.CheckBox35.CheckState & " -sms.territory " & MgrSetting.ComboBox21.Text

        'SNES
        If MedGuiR.CheckBox15.Checked = False Then
            snes = " -snes.correct_aspect " & MgrSetting.CheckBox36.CheckState & " -snes.input.port1 " & MgrSetting.ComboBox22.Text & " -snes.input.port2 " & MgrSetting.ComboBox23.Text &
        " -snes.input.port1.multitap " & MgrSetting.CheckBox37.CheckState & " -snes.input.port2.multitap " & MgrSetting.CheckBox38.CheckState & " -snes.mouse_sensitivity " & Replace(MgrSetting.NumericUpDown10.Value, ",", ".") &
         " -snes.apu.resamp_quality " & Replace(MgrSetting.NumericUpDown17.Value, ",", ".")
        Else
            'FAUST
            snes = " -snes_faust.spex " & MgrSetting.CheckBox63.CheckState & " -snes_faust.spex.sound " & MgrSetting.CheckBox64.CheckState & " -snes_faust.input.port1 " & MgrSetting.ComboBox46.Text &
            " -snes_faust.input.port2 " & MgrSetting.ComboBox47.Text & " -snes_faust.resamp_quality " & Replace(MgrSetting.NumericUpDown18.Value, ",", ".")
        End If

        'Virtual Boy
        vboy = " -vb.allow_draw_skip " & MgrSetting.CheckBox40.CheckState & " -vb.3dreverse " & MgrSetting.CheckBox39.CheckState & " -vb.3dmode " & MgrSetting.ComboBox24.Text &
        " -vb.anaglyph.preset " & MgrSetting.ComboBox25.Text & " -vb.disable_parallax " & MgrSetting.CheckBox41.CheckState & " -vb.liprescale " & Replace(MgrSetting.NumericUpDown11.Value, ",", ".") &
        " -vb.sidebyside.separation " & MgrSetting.TrackBar13.Value & " -vb.default_color " & MgrSetting.Label91.Text & " -vb.anaglyph.lcolor " & MgrSetting.Label92.Text &
        " -vb.anaglyph.rcolor " & MgrSetting.Label93.Text & " -vb.cpu_emulation " & MgrSetting.ComboBox26.Text & " -vb.input.instant_read_hack " & MgrSetting.CheckBox42.CheckState &
        " -vb.instant_display_hack " & MgrSetting.CheckBox43.CheckState

        'Wonder Swan
        wswan = " -wswan.bday " & MgrSetting.ComboBox27.Text & " -wswan.bmonth " & MgrSetting.ComboBox28.Text & " -wswan.byear " & MgrSetting.ComboBox29.Text &
        " -wswan.blood " & MgrSetting.ComboBox30.Text & " -wswan.sex " & MgrSetting.ComboBox32.Text & " -wswan.language " & MgrSetting.ComboBox31.Text &
        " -wswan.name " & Chr(34) & MgrSetting.TextBox9.Text & Chr(34) & " -wswan.rotateinput " & MgrSetting.CheckBox44.CheckState

        'PC-FX
        minput = ""
        If MgrSetting.CheckBox50.Checked = True Or MgrSetting.CheckBox49.Checked = True Then
            For i = 1 To 8
                MgrSetting.ComboBox38.Text = MgrSetting.ComboBox35.Text
                tminput = " -pcfx.input.port" & i & " " & MgrSetting.ComboBox35.Text
                minput = minput & tminput
            Next
        Else
            minput = " -pcfx.input.port1 " & MgrSetting.ComboBox35.Text & " -pcfx.input.port2 " & MgrSetting.ComboBox36.Text
        End If

        pcfx = " -pcfx.adpcm.emulate_buggy_codec " & MgrSetting.CheckBox45.CheckState & " -pcfx.resamp_quality " & Replace(MgrSetting.NumericUpDown15.Value, ",", ".") & " -pcfx.adpcm.suppress_channel_reset_clicks " & MgrSetting.CheckBox46.CheckState &
        " -pcfx.cdspeed " & MgrSetting.TrackBar14.Value & " -pcfx.mouse_sensitivity " & Replace(MgrSetting.NumericUpDown12.Value, ",", ".") & " -pcfx.cpu_emulation " & MgrSetting.ComboBox33.Text &
        " -pcfx.high_dotclock_width " & MgrSetting.ComboBox34.Text & " -pcfx.nospritelimit " & MgrSetting.CheckBox47.CheckState & " -pcfx.rainbow.chromaip " & MgrSetting.CheckBox48.CheckState &
        " -pcfx.input.port1.multitap " & MgrSetting.CheckBox50.CheckState & " -pcfx.input.port2.multitap " & MgrSetting.CheckBox49.CheckState & minput

        'PSX
        minput = ""
        If MgrSetting.CheckBox61.Checked = True Then
            minput = " -psx.input.port1 " & MgrSetting.ComboBox37.Text & " -psx.input.port2 " & MgrSetting.ComboBox38.Text
        Else
            For i = 1 To 8
                MgrSetting.ComboBox38.Text = MgrSetting.ComboBox37.Text
                tminput = " -psx.input.port" & i & " " & MgrSetting.ComboBox37.Text
                minput = minput & tminput
            Next
        End If

        psx = " -psx.input.analog_mode_ct " & MgrSetting.CheckBox51.CheckState & " -psx.input.port1.memcard " & MgrSetting.CheckBox52.CheckState & " -psx.input.port2.memcard " & MgrSetting.CheckBox53.CheckState &
        " -psx.region_autodetect " & MgrSetting.CheckBox55.CheckState & " -psx.h_overscan " & MgrSetting.CheckBox57.CheckState & " -psx.bios_sanity " & MgrSetting.CheckBox58.CheckState & " -psx.region_default " & MgrSetting.ComboBox39.Text & " -psx.input.mouse_sensitivity " & Replace(MgrSetting.NumericUpDown13.Value, ",", ".") &
        minput & " -psx.input.pport1.multitap " & MgrSetting.CheckBox54.CheckState & " -psx.input.pport2.multitap " & MgrSetting.CheckBox56.CheckState &
        " -psx.input.port1.gun_chairs " & MgrSetting.Label86.Text & " -psx.input.port2.gun_chairs " & MgrSetting.Label87.Text & " -psx.spu.resamp_quality " & MgrSetting.TrackBar15.Value

        'SEGA SATURN
        minput = ""
        If MgrSetting.CheckBox69.Checked = True Then
            minput = " -ss.input.port1 " & MgrSetting.ComboBox45.Text & " -ss.input.port2 " & MgrSetting.ComboBox44.Text
        Else
            For i = 1 To 12
                MgrSetting.ComboBox44.Text = MgrSetting.ComboBox45.Text
                tminput = " -ss.input.port" & i & " " & MgrSetting.ComboBox45.Text
                minput = minput & tminput
            Next
        End If

        ss = " -ss.region_autodetect " & MgrSetting.CheckBox62.CheckState & " -ss.region_default " & MgrSetting.ComboBox43.Text & " -ss.smpc.autortc.lang " & MgrSetting.ComboBox48.Text & " -ss.cart" & GlobalVar.SScart & " " & MgrSetting.ComboBox49.Text &
        " -ss.scsp.resamp_quality " & MgrSetting.TrackBar16.Value & " -ssfplay.resamp_quality " & MgrSetting.TrackBar16.Value & minput & " -ss.input.mouse_sensitivity " & Replace(MgrSetting.NumericUpDown19.Value, ",", ".") &
        " -ss.bios_sanity " & MgrSetting.CheckBox67.CheckState & " -ss.cd_sanity " & MgrSetting.CheckBox68.CheckState & " -ss.smpc.autortc " & MgrSetting.CheckBox66.CheckState & " -ss.midsync " & MgrSetting.CheckBox65.CheckState &
        " -ss.cart.kof95_path " & MgrSetting.TextBox2.Text & " -ss.cart.ultraman_path " & MgrSetting.TextBox1.Text '" -ss.input.port1 " & Setting.ComboBox45.Text & " -ss.input.port2 " & Setting.ComboBox44.Text

        SpecificXversion()

        Select Case consoles
            Case "apple2"
                pArg = sound & video & filters & various & s_record & netplay_ & apple2
            Case "lynx"
                pArg = sound & video & filters & various & s_record & netplay_ & lynx
            Case "gb", "gbc"
                pArg = sound & video & filters & various & s_record & netplay_ & gameboy
            Case "gg"
                pArg = sound & video & filters & various & s_record & netplay_
            Case "gba"
                specBios = " -gba.bios " & Chr(34) & MgrSetting.TextBox10.Text & Chr(34)
                pArg = sound & video & filters & various & s_record & specBios & netplay_
            Case "md"
                specBios = " -md.cdbios " & Chr(34) & MgrSetting.TextBox14.Text & Chr(34)
                pArg = sound & video & filters & various & s_record & specBios & netplay_ & genesis
            Case "ngp"
                pArg = sound & video & filters & various & s_record & netplay_ & neogeop
            Case "nes"
                specBios = " -nes.ggrom " & Chr(34) & MgrSetting.TextBox11.Text & Chr(34)
                pArg = sound & video & filters & various & s_record & specBios & netplay_ & famicom
            Case "pce", "pce_fast"
                specBios = " -pce" & MedGuiR.tpce & ".cdbios " & Chr(34) & MgrSetting.TextBox12.Text & Chr(34) & " -pce.gecdbios " & Chr(34) & MgrSetting.TextBox18.Text & Chr(34)
                pArg = sound & video & filters & various & s_record & specBios & netplay_ & pcengine
            Case "psx"
                specBios = " -psx.bios_eu " & Chr(34) & MgrSetting.TextBox15.Text & Chr(34) & " -psx.bios_jp " & Chr(34) & MgrSetting.TextBox16.Text & Chr(34) & " -psx.bios_na " & Chr(34) & MgrSetting.TextBox17.Text & Chr(34)
                pArg = sound & video & filters & various & s_record & specBios & netplay_ & psx
            Case "sms"
                pArg = sound & video & filters & various & s_record & netplay_ & mastersystem
            Case "snes", "snes_faust"
                pArg = sound & video & filters & various & s_record & netplay_ & snes
            Case "ss"
                specBios = " -ss.bios_jp " & Chr(34) & MgrSetting.TextBox22.Text & Chr(34) & " -ss.bios_na_eu " & Chr(34) & MgrSetting.TextBox23.Text & Chr(34)
                pArg = sound & video & filters & various & s_record & specBios & netplay_ & ss
            Case "ssfplay"
                pArg = sound & video & filters & various & s_record & netplay_
            Case "vb"
                pArg = sound & video & filters & various & s_record & netplay_ & vboy
            Case "wswan"
                pArg = sound & video & filters & various & s_record & netplay_ & wswan
            Case "pcfx"
                specBios = " -pcfx.bios " & Chr(34) & MgrSetting.TextBox13.Text & Chr(34)
                pArg = sound & video & filters & various & s_record & specBios & netplay_ & pcfx
            Case "cdplay"
                pArg = sound & video & filters & various & s_record
        End Select
    End Sub

    Private Sub SpecificXversion()
        For i = 9380 To Val(vmedClear)
            Select Case i
                Case Is = 9410
                    mastersystem = mastersystem & " -sms.slstart " & MgrSetting.TrackBar19.Value & " -sms.slend " & MgrSetting.TrackBar17.Value & " -sms.slstartp " & MgrSetting.TrackBar20.Value & " -sms.slendp " & MgrSetting.TrackBar18.Value
                    ss = ss & " -ss.correct_aspect " & MgrSetting.CheckBox75.CheckState & " -ss.h_overscan " & MgrSetting.CheckBox77.CheckState & " -ss.h_blend " & MgrSetting.CheckBox76.CheckState
                    If MedGuiR.CheckBox15.Checked = False Then snes = snes & " -snes.h_blend " & MgrSetting.CheckBox96.CheckState
                    SumPSXAnalog()
                    filters = filters & " -" & consoles & MedGuiR.tpce & ".shader.goat.vdiv " & Replace(MgrSetting.NumericUpDown24.Value, ",", ".") & " -" & consoles & MedGuiR.tpce & ".shader.goat.hdiv " & Replace(MgrSetting.NumericUpDown22.Value, ",", ".") &
                    " -" & consoles & MedGuiR.tpce & ".shader.goat.tp " & Replace(MgrSetting.NumericUpDown23.Value, ",", ".") & " -" & consoles & MedGuiR.tpce & ".shader.goat.pat " & MgrSetting.ComboBox53.Text &
                    " -" & consoles & MedGuiR.tpce & ".shader.goat.fprog " & MgrSetting.CheckBox95.CheckState & " -" & consoles & MedGuiR.tpce & ".shader.goat.slen " & MgrSetting.CheckBox94.CheckState
                Case Is = 9420
                    If MedGuiR.CheckBox15.Checked = True Then
                        snes = snes & " -snes_faust.input.sport1.multitap " & MgrSetting.CheckBox99.CheckState & " -snes_faust.input.sport2.multitap " & MgrSetting.CheckBox98.CheckState
                    End If
                Case Is = 9430
                    ss = ss & " -ss.input.sport1.multitap " & MgrSetting.CheckBox101.CheckState & " -ss.input.sport2.multitap " & MgrSetting.CheckBox100.CheckState
                Case Is = 9440
                    If MedGuiR.CheckBox15.Checked = True Then
                        snes = snes & " -snes_faust.correct_aspect " & MgrSetting.CheckBox102.CheckState
                    End If
                    wswan = Replace(wswan, " -wswan.rotateinput " & MgrSetting.CheckBox44.CheckState, "")
                    wswan = wswan & " -wswan.input.builtin " & MgrSetting.ComboBox51.Text
                    vboy = vboy & " -vb.ledonscale " & Replace(MgrSetting.NumericUpDown26.Value, ",", ".")

                Case Is = 9460
                    If MgrSetting.CheckBox69.Checked = True Then
                        minput = " -ss.input.port1.3dpad.mode.defpos " & MgrSetting.ComboBox52.Text
                    Else
                        For z = 1 To 12
                            tminput = " -ss.input.port" & z & ".3dpad.mode.defpos " & MgrSetting.ComboBox52.Text
                            minput = minput & tminput
                        Next
                    End If
                    ss = ss & minput
                Case Is = 12100
                    video = video & " -video.fs.display " & CInt(MgrSetting.NumericUpDown27.Value) & " -fps.autoenable " & MgrSetting.CheckBox103.CheckState &
                    " -fps.textcolor " & MgrSetting.Label135.Text & " -fps.bgcolor " & MgrSetting.Label136.Text &
                    " -fps.font " & MgrSetting.ComboBox55.Text & " -fps.position " & MgrSetting.ComboBox56.Text & " -fps.scale " & CInt(MgrSetting.NumericUpDown28.Value)

                    If detect_module("video.resolution_switch") = True Then
                        video = video & " -video.resolution_switch " & MgrSetting.ComboBox57.Text
                        filters = Nothing
                    End If

                Case Is = 12200
                    ss = ss & " -ss.cart auto"
                Case Is = 12300
                    If MedGuiR.CheckBox15.Checked = True Then
                        snes = snes & " -snes_faust.cx4.clock_rate " & MgrSetting.NumericUpDown42.Value & " -snes_faust.h_filter " & MgrSetting.ComboBox59.Text
                    End If
                    apple2 = apple2 & " -apple2.video.mode " & MgrSetting.ComboBox62.Text
                Case Is = 12400
                    ss = Replace(ss, " -ss.midsync " & MgrSetting.CheckBox65.CheckState, Nothing)
                    psx = psx & " -psx.correct_aspect " & MgrSetting.CheckBox116.CheckState
                    If MedGuiR.CheckBox15.Checked = True Then
                        snes = Replace(snes & " -snes_faust.correct_aspect " & MgrSetting.CheckBox102.CheckState,
                                       " -snes_faust.correct_aspect " & MgrSetting.CheckBox102.CheckState, Nothing)
                        snes = snes & " -snes_faust.renderer " & MgrSetting.ComboBox63.Text & " -snes_faust.correct_aspect " & MgrSetting.ComboBox65.Text &
                        " -snes_faust.msu1.resamp_quality " & MgrSetting.NumericUpDown43.Value & " -snes_faust.region " & MgrSetting.ComboBox64.Text &
                        " -snes_faust.superfx.clock_rate " & MgrSetting.NumericUpDown44.Value & " -snes_faust.superfx.icache " & MgrSetting.CheckBox111.CheckState &
                        " -snes_faust.slend " & MgrSetting.NumericUpDown46.Value & " -snes_faust.slendp " & MgrSetting.NumericUpDown48.Value & " -snes_faust.slstart " &
                        MgrSetting.NumericUpDown45.Value & " -snes_faust.slstartp " & MgrSetting.NumericUpDown47.Value
                    End If
                Case Is = 12700
                    video = video & " -video.glformat " & MgrSetting.ComboBox66.Text
                Case Is = 12800
                    video = video & " -video.force_bbclear " & MgrSetting.CheckBox110.CheckState & " -video.cursorvis " & MgrSetting.ComboBox67.Text
                Case Is = 13100
                    ss = ss & " -ss.bios_stv_eu " & Chr(34) & MgrSetting.TextBox36.Text & Chr(34) & " -ss.bios_stv_jp " & Chr(34) & MgrSetting.TextBox37.Text & Chr(34) &
                    " -ss.bios_stv_na " & Chr(34) & MgrSetting.TextBox35.Text & Chr(34)
            End Select
        Next
    End Sub

    Public Sub SumPSXAnalog()
        Dim CAnM(15) As CheckBox, AnModValue(15) As Integer
        CAnM(0) = MgrSetting.CheckBox78
        CAnM(1) = MgrSetting.CheckBox80
        CAnM(2) = MgrSetting.CheckBox81
        CAnM(3) = MgrSetting.CheckBox79
        CAnM(4) = MgrSetting.CheckBox90
        CAnM(5) = MgrSetting.CheckBox93
        CAnM(6) = MgrSetting.CheckBox91
        CAnM(7) = MgrSetting.CheckBox92
        CAnM(8) = MgrSetting.CheckBox82
        CAnM(9) = MgrSetting.CheckBox83
        CAnM(10) = MgrSetting.CheckBox85
        CAnM(11) = MgrSetting.CheckBox84
        CAnM(12) = MgrSetting.CheckBox86
        CAnM(13) = MgrSetting.CheckBox89
        CAnM(14) = MgrSetting.CheckBox88
        CAnM(15) = MgrSetting.CheckBox87

        If CAnM(0).Checked = True Then AnModValue(0) = 1 Else AnModValue(0) = 0

        Dim result As Integer
        Dim CoInt As Integer = 1
        For i = 1 To 15
            CoInt = CoInt * 2
            If CAnM(i).Checked = True Then AnModValue(i) = CoInt Else AnModValue(i) = 0
            result = result + AnModValue(i)
        Next
        psx = psx & " -psx.input.analog_mode_ct.compare " & ("0x" & (result + AnModValue(0)).ToString("X4"))
    End Sub

End Module