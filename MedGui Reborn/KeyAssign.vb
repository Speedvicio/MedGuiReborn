Imports System.IO

Public Class KeyAssign
    Dim tasti, sdl, kParameter, _KPar As String, vbtasti As Integer

    Public Sub TransKeyToSdl()

        'controllo di alcune lettere,IL PROGRAMMA ne segna solo una ma in
        'realtà il sistema permette di leggere anche più tasti contemporaneamente
        Select Case vbtasti
            Case 131089, 262161, 262162, 65552, 16, 17, 18, 91, 92, 93, 27, 112, 113, 123, 20, 144, 13
                MsgBox(tasti & " key is not assignable" &
                       vbCrLf & "Please select another key", MsgBoxStyle.Information) : tasti = "" : sdl = "" : Exit Sub
            Case 8
                sdl = 8
            Case 9
                sdl = 9
            Case 46
                sdl = 127
            Case 13
                sdl = 13
            Case 19
                sdl = 19
            Case 32
                sdl = 32
            Case 219
                sdl = 45
            Case 0
                sdl = 48
            Case 1
                sdl = 49
            Case 2
                sdl = 50
            Case 3
                sdl = 51
            Case 4
                sdl = 52
            Case 5
                sdl = 53
            Case 6
                sdl = 54
            Case 7
                sdl = 55
            Case 8
                sdl = 56
            Case 9
                sdl = 57
            Case 226
                sdl = 60
            Case 221
                sdl = 61

            'Uppercase letters
            Case 65
                sdl = 97
            Case 66
                sdl = 98
            Case 67
                sdl = 99
            Case 68
                sdl = 100
            Case 69
                sdl = 101
            Case 70
                sdl = 102
            Case 71
                sdl = 103
            Case 72
                sdl = 104
            Case 73
                sdl = 105
            Case 74
                sdl = 106
            Case 75
                sdl = 107
            Case 76
                sdl = 108
            Case 77
                sdl = 109
            Case 78
                sdl = 110
            Case 79
                sdl = 111
            Case 80
                sdl = 112
            Case 81
                sdl = 113
            Case 82
                sdl = 114
            Case 83
                sdl = 115
            Case 84
                sdl = 116
            Case 85
                sdl = 117
            Case 86
                sdl = 118
            Case 87
                sdl = 119
            Case 88
                sdl = 120
            Case 89
                sdl = 121
            Case 189
                sdl = 47

            'Special
            Case 90
                sdl = 122
            Case 220
                sdl = 96
            Case 186
                sdl = 91
            Case 187
                sdl = 93
            Case 188
                sdl = 44
            Case 190
                sdl = 46
            Case 192
                sdl = 59
            Case 222
                sdl = 39
            Case 191
                sdl = 92

            '/* Numeric keypad */
            Case 96
                sdl = 256
            Case 97
                sdl = 257
            Case 98
                sdl = 258
            Case 99
                sdl = 259
            Case 100
                sdl = 260
            Case 101
                sdl = 261
            Case 102
                sdl = 262
            Case 103
                sdl = 263
            Case 104
                sdl = 264
            Case 105
                sdl = 265
            Case 110
                sdl = 266
            Case 111
                sdl = 267
            Case 106
                sdl = 268
            Case 109
                sdl = 269
            Case 107
                sdl = 270
            'Case KP_ENTER" Then sdl = 271
            'Case KP_EQUALS" Then sdl = 272

            '/* Arrows + Home/End pad */
            Case 38
                sdl = 273
            Case 40
                sdl = 274
            Case 39
                sdl = 275
            Case 37
                sdl = 276
            Case 45
                sdl = 277
            Case 36
                sdl = 278
            Case 35
                sdl = 279
            Case 33
                sdl = 280
            Case 34
                sdl = 281

            '/* Function keys */
            Case 112
                sdl = 282
            Case 113
                sdl = 283
            Case 114
                sdl = 284
            Case 115
                sdl = 285
            Case 116
                sdl = 286
            Case 117
                sdl = 287
            Case 118
                sdl = 288
            Case 119
                sdl = 289
            Case 120
                sdl = 290
            Case 121
                sdl = 291
            Case 122
                sdl = 292
            Case 124
                sdl = 294
            Case 125
                sdl = 295
            Case 126
                sdl = 296
            Case Else
                MsgBox(tasti & " with code " & vbtasti & " is not assigned" &
                vbCrLf & "Please report KEYCODE " & vbtasti & " to MedGuiR topic to add it in the next release", MsgBoxStyle.Information) : tasti = ""
        End Select
    End Sub

    Private Sub SDLkeycode()
        '* The keyboard syms have been cleverly chosen to map to ASCII */
        'If UCase(i) = "UNKNOWN" Then sdl = 0
        'If UCase(i) = "FIRST" Then sdl = 0
        'If UCase(i) = "CLEAR" Then sdl = 12
        'If UCase(i) = "EXCLAIM" Then sdl = 33
        'If UCase(i) = "QUOTEDBL" Then sdl = 34
        'If UCase(i) = "HASH" Then sdl = 35
        'If UCase(i) = "DOLLAR" Then sdl = 36
        'If UCase(i) = "AMPERSAND" Then sdl = 38
        'If UCase(i) = "LEFTPAREN" Then sdl = 40
        'If UCase(i) = "RIGHTPAREN" Then sdl = 41
        'If UCase(i) = "ASTERISK" Then sdl = 42
        'If UCase(i) = "PLUS" Then sdl = 43

        'If UCase(i) = "COLON" Then sdl = 58

        'If UCase(i) = "GREATER" Then sdl = 62
        'If UCase(i) = "QUESTION" Then sdl = 63
        'If UCase(i) = "AT" Then sdl = 64

        'If UCase(i) = "CARET" Then sdl = 94
        'If UCase(i) = "UNDERSCORE" Then sdl = 95

        '/* Key state modifier keys */
        'If UCase(i) = "NUMLOCK" Then sdl = 300
        'If UCase(i) = "CAPSLOCK" Then sdl = 301
        'If UCase(i) = "SCROLLOCK" Then sdl = 302
        'If UCase(i) = "RSHIFT" Then sdl = 303
        'If UCase(i) = "LSHIFT" Then sdl = 304
        'If UCase(i) = "RCTRL" Then sdl = 305
        'If UCase(i) = "LCTRL" Then sdl = 306
        'If UCase(i) = "RALT" Then sdl = 307
        'If UCase(i) = "LALT" Then sdl = 308
        'If UCase(i) = "RMETA" Then sdl = 309
        'If UCase(i) = "LMETA" Then sdl = 310
        'If UCase(i) = "LSUPER" Then sdl = 311 '/* Left "Windows" key */
        'If UCase(i) = "RSUPER" Then sdl = 312 '/* Right "Windows" key */
        'If UCase(i) = "MODE" Then sdl = 313 '/* "Alt Gr" key */
        'If UCase(i) = "COMPOSE" Then sdl = 314 '/* Multi-key compose key */

        '/* Miscellaneous function keys */
        'If i = "Help" Then sdl = 315
        'If i = "Print" Then sdl = 316
        'If UCase(i) = "SYSREQ" Then sdl = 317
        'If UCase(i) = "BREAK" Then sdl = 318
        'If i = "Menu" Then sdl = 319
        'If UCase(i) = "POWER" Then sdl = 320 '/* Power Macintosh power key */
        'If UCase(i) = "EURO" Then sdl = 321 '/* Some european keyboards */
        'If UCase(i) = "UNDO" Then sdl = 322
    End Sub

    Private Sub WriteKeyConf()

        kParameter = _KPar & " keyboard " & sdl
        'backupKeyform()

        If File.Exists(MedGuiR.TextBox4.Text & "\" & DMedConf & ".cfg") Then

            Dim keylinee As String() = IO.File.ReadAllLines(MedGuiR.TextBox4.Text & "\" & DMedConf & ".cfg")
            For i As Integer = 0 To keylinee.Length - 1
                If keylinee(i) = kParameter Then
                    MsgBox(tasti & " already assigned to this Mednafen parameter") : Exit Sub
                End If
            Next

            For i As Integer = 0 To keylinee.Length - 1
                If keylinee(i).Contains("command.") And keylinee(i).Contains(" keyboard " & sdl) And keylinee(i).Contains("+") = False Then
                    MsgBox(tasti & " already assigned to " & keylinee(i).ToString & " , try another key") : Exit Sub
                End If
            Next

            'For i As Integer = 0 To keylinee.Length - 1
            'If keylinee(i).Contains(_KPar) Then
            'keylinee(i) = kParameter
            'File.WriteAllLines(MedGuiR.TextBox4.Text & "\" & p_c & ".cfg", keylinee) : Exit Sub
            'End If
            'Next

            'My.Computer.FileSystem.WriteAllText(
            'MedGuiR.TextBox4.Text & "\" & p_c & ".cfg", kParameter & vbCrLf, True)
            'pArg = " -" & kParameter
            Dim par As String = "-" & _KPar & " " & Chr(34) & "keyboard " & sdl & Chr(34)
            tProcess = "mednafen"
            wDir = MedGuiR.TextBox4.Text
            Arg = par
            StartProcess()
            'Process.Start(MedGuiR.TextBox4.Text & "\mednafen.exe", par)
            My.Computer.FileSystem.WriteAllText(
MedExtra & "Backup\FunctionKeys.txt", _KPar & "=" & tasti & vbCrLf, True)
            'MgrSetting.Mednafen_Save_setting()
        Else

            'My.Computer.FileSystem.WriteAllText(
            'MedGuiR.TextBox4.Text & "\" & p_c & ".cfg", "; << Command Keys Config >>" & vbCrLf & vbCrLf & kParameter & vbCrLf, True)

        End If

        'LoadKeyForm()
    End Sub

    Private Sub backupKeyform()

        set_special_module()

        If File.Exists(MedExtra & "Backup\" & p_c & ".cfg") Then
            Dim Sr As New StreamReader(MedExtra & "Backup\" & p_c & ".cfg")
            Dim Sw As New StreamWriter(MedExtra & "Backup\" & p_c & ".cfg_")

            Dim Line As String = Sr.ReadLine

            Do While Not Line Is Nothing

                If Line.Contains(_KPar) Then
                Else
                    Sw.WriteLine(Line)
                End If
                Line = Sr.ReadLine
            Loop

            Sr.Close()
            Sw.Close()

            File.Delete(MedExtra & "Backup\" & p_c & ".cfg")
            My.Computer.FileSystem.RenameFile(MedExtra & "Backup\" & p_c & ".cfg_", p_c & ".cfg")
            File.Delete(MedExtra & "Backup\" & p_c & ".cfg_")
        End If

        My.Computer.FileSystem.WriteAllText(
MedExtra & "Backup\" & p_c & ".cfg", _KPar & "= " & tasti & vbCrLf, True)

    End Sub

    Public Sub LoadKeyForm()

        set_special_module()

        If File.Exists(MedExtra & "Backup\FunctionKeys.txt") Then
            Dim readText() As String = File.ReadAllLines(MedExtra & "Backup\FunctionKeys.txt")
            Dim s As String
            Dim prima, dopo As String
            For Each s In readText
                Dim indice() = s.Split("=")
                prima = indice(0)
                dopo = indice(1)

                Select Case prima
                    Case "command.toggle_help"
                        TextBox1.Text = dopo
                    Case "command.save_state"
                        TextBox2.Text = dopo
                    Case "command.load_state"
                        TextBox3.Text = dopo
                    Case "command.save_movie"
                        TextBox4.Text = dopo
                    Case "command.load_movie"
                        TextBox5.Text = dopo
                    Case "command.togglenetview"
                        TextBox6.Text = dopo
                    Case "command.state_rewind"
                        TextBox7.Text = dopo
                    Case "command.take_snapshot"
                        TextBox8.Text = dopo
                    Case "command.take_scaled_snapshot"
                        TextBox9.Text = dopo
                    Case "command.rotate_screen"
                        TextBox10.Text = dopo
                    Case "command.toggle_fs"
                        TextBox11.Text = dopo
                    Case "command.fast_forward"
                        TextBox12.Text = dopo
                    Case "command.slow_forward"
                        TextBox13.Text = dopo
                    Case "command.input_config_abd"
                        TextBox14.Text = dopo
                    Case "command.toggle_grab_input"
                        TextBox15.Text = dopo
                    Case "command.reset"
                        TextBox16.Text = dopo
                    Case "command.power"
                        TextBox17.Text = dopo
                    Case "command.exit"
                        TextBox18.Text = dopo
                End Select
            Next
        Else
            ClearTextBoxes(Me)
            Exit Sub
        End If

    End Sub

    Public Sub ClearTextBoxes(ByVal ctl As Control)

        ' da notare l'utilizzo di TryCast che corrisponde grossomodo al comando
        ' as di C#
        Dim tb As TextBox = TryCast(ctl, TextBox)
        If tb IsNot Nothing Then
            tb.Text = [String].Empty
        ElseIf ctl.Controls.Count > 0 Then
            For k As Integer = 0 To ctl.Controls.Count - 1
                ClearTextBoxes(ctl.Controls(k)) 'ricorsione
            Next
        End If
    End Sub

    Private Sub TextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        tasti = e.KeyData.ToString
        vbtasti = e.KeyData
        TransKeyToSdl()
        TextBox1.Text = tasti
        If tasti <> "" Then _KPar = "command.toggle_help" : WriteKeyConf()

    End Sub

    Private Sub TextBox2_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp
        tasti = e.KeyData.ToString
        vbtasti = e.KeyData
        TransKeyToSdl()
        TextBox2.Text = tasti
        If tasti <> "" Then _KPar = "command.save_state" : WriteKeyConf()

    End Sub

    Private Sub TextBox3_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyUp
        tasti = e.KeyData.ToString
        vbtasti = e.KeyData
        TransKeyToSdl()
        TextBox3.Text = tasti
        If tasti <> "" Then _KPar = "command.load_state" : WriteKeyConf()

    End Sub

    Private Sub TextBox4_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox4.KeyUp
        tasti = e.KeyData.ToString
        vbtasti = e.KeyData
        TransKeyToSdl()
        TextBox4.Text = tasti
        If tasti <> "" Then _KPar = "command.save_movie" : WriteKeyConf()

    End Sub

    Private Sub TextBox5_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox5.KeyUp
        tasti = e.KeyData.ToString
        vbtasti = e.KeyData
        TransKeyToSdl()
        TextBox5.Text = tasti
        If tasti <> "" Then _KPar = "command.load_movie" : WriteKeyConf()

    End Sub

    Private Sub TextBox6_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox6.KeyUp
        tasti = e.KeyData.ToString
        vbtasti = e.KeyData
        TransKeyToSdl()
        TextBox6.Text = tasti
        If tasti <> "" Then _KPar = "command.togglenetview" : WriteKeyConf()

    End Sub

    Private Sub TextBox7_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox7.KeyUp
        tasti = e.KeyData.ToString
        vbtasti = e.KeyData
        TransKeyToSdl()
        TextBox7.Text = tasti
        If tasti <> "" Then _KPar = "command.state_rewind" : WriteKeyConf()

    End Sub

    Private Sub TextBox8_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox8.KeyUp
        tasti = e.KeyData.ToString
        vbtasti = e.KeyData
        TransKeyToSdl()
        TextBox8.Text = tasti
        If tasti <> "" Then _KPar = "command.take_snapshot" : WriteKeyConf()

    End Sub

    Private Sub TextBox9_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox9.KeyUp
        tasti = e.KeyData.ToString
        vbtasti = e.KeyData
        TransKeyToSdl()
        TextBox9.Text = tasti
        If tasti <> "" Then _KPar = "command.take_scaled_snapshot" : WriteKeyConf()

    End Sub

    Private Sub TextBox10_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox10.KeyUp
        tasti = e.KeyData.ToString
        vbtasti = e.KeyData
        TransKeyToSdl()
        TextBox10.Text = tasti
        If tasti <> "" Then _KPar = "command.rotate_screen" : WriteKeyConf()

    End Sub

    Private Sub TextBox11_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox11.KeyUp
        tasti = e.KeyData.ToString
        vbtasti = e.KeyData
        TransKeyToSdl()
        TextBox11.Text = tasti
        If tasti <> "" Then _KPar = "command.toggle_fs" : WriteKeyConf()

    End Sub

    Private Sub TextBox12_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox12.KeyUp
        tasti = e.KeyData.ToString
        vbtasti = e.KeyData
        TransKeyToSdl()
        TextBox12.Text = tasti
        If tasti <> "" Then _KPar = "command.fast_forward" : WriteKeyConf()

    End Sub

    Private Sub TextBox13_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox13.KeyUp
        tasti = e.KeyData.ToString
        vbtasti = e.KeyData
        TransKeyToSdl()
        TextBox13.Text = tasti
        If tasti <> "" Then _KPar = "command.slow_forward" : WriteKeyConf()

    End Sub

    Private Sub TextBox14_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox14.KeyUp
        tasti = e.KeyData.ToString
        vbtasti = e.KeyData
        TransKeyToSdl()
        TextBox14.Text = tasti
        If tasti <> "" Then _KPar = "command.input_config_abd" : WriteKeyConf()

    End Sub

    Private Sub TextBox15_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox15.KeyUp
        tasti = e.KeyData.ToString
        vbtasti = e.KeyData
        TransKeyToSdl()
        TextBox15.Text = tasti
        If tasti <> "" Then _KPar = "command.toggle_grab_input" : WriteKeyConf()

    End Sub

    Private Sub TextBox16_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox16.KeyUp
        tasti = e.KeyData.ToString
        vbtasti = e.KeyData
        TransKeyToSdl()
        TextBox16.Text = tasti
        If tasti <> "" Then _KPar = "command.reset" : WriteKeyConf()

    End Sub

    Private Sub TextBox17_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox17.KeyUp
        tasti = e.KeyData.ToString
        vbtasti = e.KeyData
        TransKeyToSdl()
        TextBox17.Text = tasti
        If tasti <> "" Then _KPar = "command.power" : WriteKeyConf()

    End Sub

    Private Sub TextBox18_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox18.KeyUp
        tasti = e.KeyData.ToString
        vbtasti = e.KeyData
        TransKeyToSdl()
        TextBox18.Text = tasti
        If tasti <> "" Then _KPar = "command.exit" : WriteKeyConf()
    End Sub

    Private Sub KeyAssign_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadKeyForm()
        F1 = Me
        CenterForm()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        PerConf.ShowDialog()
    End Sub

End Class