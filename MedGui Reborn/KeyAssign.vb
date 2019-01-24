Imports System.IO

Public Class KeyAssign
    Dim tasti, sdl, kParameter, _KPar As String, vbtasti As Integer

    Public Sub TransKeyToSdl()

        If DMedConf = "mednafen" Then
            TransKeyToSdl_2()
            Exit Sub
        End If

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

    Public Sub TransKeyToSdl_2()
        Select Case vbtasti
        'Escape
            Case 27
                sdl = 41

'F1
            Case 112
                sdl = 58

'F2
            Case 113
                sdl = 59

'F3
            Case 114
                sdl = 60

'F4
            Case 115
                sdl = 61

'F5
            Case 116
                sdl = 62

'F6
            Case 117
                sdl = 63

'F7
            Case 118
                sdl = 64

'F8
            Case 119
                sdl = 65

'F9
            Case 120
                sdl = 66

'F10
            Case 121
                sdl = 67

'F11
            Case 122
                sdl = 68

'F12
            Case 123
                sdl = 69

'D1
            Case 49
                sdl = 30

'D2
            Case 50
                sdl = 31

'D3
            Case 51
                sdl = 32

'D4
            Case 52
                sdl = 33

'D5
            Case 53
                sdl = 34

'D6
            Case 54
                sdl = 35

'D7
            Case 55
                sdl = 36

'D8
            Case 56
                sdl = 37

'D9
            Case 57
                sdl = 38

'D0
            Case 48
                sdl = 39

'Q
            Case 81
                sdl = 20

'W
            Case 87
                sdl = 26

'E
            Case 69
                sdl = 8

'R
            Case 82
                sdl = 21

'T
            Case 84
                sdl = 23

'Y
            Case 89
                sdl = 28

'U
            Case 85
                sdl = 24

'I
            Case 73
                sdl = 12

'O
            Case 79
                sdl = 18

'P
            Case 80
                sdl = 19

'A
            Case 65
                sdl = 4

'S
            Case 83
                sdl = 22

'D
            Case 68
                sdl = 7

'F
            Case 70
                sdl = 9

'G
            Case 71
                sdl = 10

'H
            Case 72
                sdl = 11

'J
            Case 74
                sdl = 13

'K
            Case 75
                sdl = 14

'L
            Case 76
                sdl = 15

'Z
            Case 90
                sdl = 29

'X
            Case 88
                sdl = 27

'C
            Case 67
                sdl = 6

'V
            Case 86
                sdl = 25

'B
            Case 66
                sdl = 5

'N
            Case 78
                sdl = 17

'M
            Case 77
                sdl = 16

'Backslash
            Case 220
                sdl = 49

'Unused/Generic
            Case 223
                sdl = 96

'Back
            Case 8
                sdl = 42

'Tab
            Case 9
                sdl = 43

'Return
            Case 13
                sdl = 40

'Capital
            Case 20
                sdl = 57

'[
            Case 219
                sdl = 47

']
            Case 221
                sdl = 48

';
            Case 186
                sdl = 51

'tilde
            Case 192
                sdl = 100

''
            Case 222
                sdl = 52

'ShiftKey
            Case 16
                sdl = 225

'Bracket
            Case 226
                sdl = 47

'comma
            Case 188
                sdl = 54

'Period
            Case 190
                sdl = 55

'Question
            Case 191
                'sdl =

'ControlKey
            Case 17
                sdl = 224

'LWin
            Case 91
                sdl = 227

'Alt
            Case 18
                sdl = 226

'Space
            Case 32
                sdl = 44

'RWin
            Case 92
                sdl = 231

'Apps
            Case 93
                sdl = 101

'PrintScreen
            Case 44
                sdl = 70

'Scroll
            Case 145
                sdl = 71

'Pause
            Case 19
                sdl = 72

'Insert
            Case 45
                sdl = 73

'Home
            Case 36
                sdl = 74

'PageUp
            Case 33
                sdl = 75

'PageDown
            Case 34
                sdl = 78

'End
            Case 35
                sdl = 77

'Delete
            Case 46
                sdl = 76

'Up
            Case 38
                sdl = 82

'Down
            Case 40
                sdl = 81

'Left
            Case 37
                sdl = 80

'Right
            Case 39
                sdl = 79

'SelectMedia
            Case 181
                sdl = 263

'VolumeMute
            Case 173
                sdl = 262

'VolumeDown
            Case 174
                sdl = 129

'VolumeUp
            Case 175
                sdl = 128

'MediaStop
            Case 178
                sdl = 260

'MediaPreviousTrack
            Case 177
                sdl = 259

'MediaPlayPause
            Case 179
                sdl = 261

'MediaNextTrack
            Case 176
                sdl = 258

'LaunchMail
            Case 180
                sdl = 265

'BrowserHome
            Case 172
                sdl = 264

'LaunchApplication2
            Case 183
                'sdl =

'NumLock
            Case 144
                sdl = 83

'KP_0
            Case 96
                sdl = 98

'KP_1
            Case 97
                sdl = 89

'KP_2
            Case 98
                sdl = 90

'KP_3
            Case 99
                sdl = 91

'KP_4
            Case 100
                sdl = 92

'KP_5
            Case 101
                sdl = 93

'KP_6
            Case 102
                sdl = 94

'KP_7
            Case 103
                sdl = 95

'KP_8
            Case 104
                sdl = 96

'KP_9
            Case 105
                sdl = 97

'Divide
            Case 111
                sdl = 84

'Multiply
            Case 106
                sdl = 85

'Subtract
            Case 109
                sdl = 86

'Add
            Case 107
                sdl = 87

'Decimal
            Case 110
                sdl = 220

'Clear
            Case 12
                sdl = 156
            Case Else
                MsgBox(tasti & " with code " & vbtasti & " is not assigned" &
                vbCrLf & "Please report KEYCODE " & vbtasti & " to MedGuiR topic to add it in the next release", MsgBoxStyle.Information) : tasti = ""
        End Select
        sdl = "0x0 " & sdl
    End Sub

    Private Sub WriteKeyConf()

        kParameter = _KPar & " keyboard " & sdl
        'backupKeyform()

        If File.Exists(MedGuiR.TextBox4.Text & "\" & DMedConf & ".cfg") Then

            Dim keylinee As String() = IO.File.ReadAllLines(MedGuiR.TextBox4.Text & "\" & DMedConf & ".cfg")
            'For i As Integer = 0 To keylinee.Length - 1
            'If keylinee(i) = kParameter Then
            'MsgBox(tasti & " already assigned to this Mednafen parameter") : Exit Sub
            'End If
            'Next

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
                    Case "command.pause"
                        TextBox20.Text = dopo
                    Case "command.togglecheatactive"
                        TextBox19.Text = dopo
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

    Private Sub TextBox19_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox19.KeyUp
        tasti = e.KeyData.ToString
        vbtasti = e.KeyData
        TransKeyToSdl()
        TextBox19.Text = tasti
        If tasti <> "" Then _KPar = "command.togglecheatactive" : WriteKeyConf()
    End Sub

    Private Sub TextBox20_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox20.KeyUp
        tasti = e.KeyData.ToString
        vbtasti = e.KeyData
        TransKeyToSdl()
        TextBox20.Text = tasti
        If tasti <> "" Then _KPar = "command.pause" : WriteKeyConf()
    End Sub

    Private Sub KeyAssign_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = gIcon
        LoadKeyForm()
        F1 = Me
        CenterForm()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        PerConf.ShowDialog()
    End Sub

End Class