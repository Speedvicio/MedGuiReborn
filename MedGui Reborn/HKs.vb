Imports Microsoft.DirectX
Imports Microsoft.DirectX.DirectInput
Public Class HKs
    Dim i, sdl, ff, ss, ls, enpci, sss, sidoip1, sidoip2, rt, hrt, exp As String, y As Boolean

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim tasti As KeyboardState
        tasti = TastieraData()

        'controllo di alcune lettere,IL PROGRAMMA ne segna solo una ma in
        'realtà il sistema permette di leggere anche più tasti contemporaneamente
        If tasti(Key.LeftAlt) Then : i = "LAlt" : y = True : End If
        If tasti(Key.RightAlt) Then : i = "RAlt" : y = True : End If
        If tasti(Key.Back) Then : i = "Back" : y = True : End If
        If tasti(Key.Slash) Then : i = "Slash" : y = True : End If
        If tasti(Key.Pause) Then : i = "Pause" : y = True : End If
        If tasti(Key.BackSlash) Then : i = "BackSlash" : y = True : End If
        If tasti(Key.BackSpace) Then : i = "BackSpace" : y = True : End If
        If tasti(Key.Delete) Then : i = "Delete" : y = True : End If
        If tasti(Key.Insert) Then : i = "Insert" : y = True : End If
        If tasti(Key.LeftShift) Then : i = "LShift" : y = True : End If
        If tasti(Key.RightShift) Then : i = "RShift" : y = True : End If
        If tasti(Key.End) Then : i = "End" : y = True : End If
        If tasti(Key.Escape) Then : i = "Escape" : y = True : End If
        If tasti(Key.RightBracket) Then : i = "RightBracket" : y = True : End If
        'If tasti(Key.RightMenu) Then : i = "RightMenu" : y = True : End If
        If tasti(Key.RightWindows) Then : i = "RightWindows" : y = True : End If
        If tasti(Key.Scroll) Then : i = "ScrolLock" : y = True : End If
        If tasti(Key.Subtract) Then : i = "Subtract" : y = True : End If
        If tasti(Key.Divide) Then : i = "Divide" : y = True : End If


        'tasti funzione
        If tasti(Key.F1) Then : i = "F1" : y = True : End If
        If tasti(Key.F2) Then : i = "F2" : y = True : End If
        If tasti(Key.F3) Then : i = "F3" : y = True : End If
        If tasti(Key.F4) Then : i = "F4" : y = True : End If
        If tasti(Key.F5) Then : i = "F5" : y = True : End If
        If tasti(Key.F6) Then : i = "F6" : y = True : End If
        If tasti(Key.F7) Then : i = "F7" : y = True : End If
        If tasti(Key.F8) Then : i = "F8" : y = True : End If
        If tasti(Key.F9) Then : i = "F9" : y = True : End If
        If tasti(Key.F10) Then : i = "F10" : y = True : End If
        If tasti(Key.F11) Then : i = "F11" : y = True : End If
        If tasti(Key.F12) Then : i = "F12" : y = True : End If

        If tasti(Key.Home) Then : i = "Home" : y = True : End If
        If tasti(Key.PageDown) Then : i = "PageDown" : y = True : End If
        If tasti(Key.PageUp) Then : i = "PageUp" : y = True : End If
        If tasti(Key.Add) Then : i = "Add" : y = True : End If
        If tasti(Key.Return) Then : i = "Return" : y = True : End If
        If tasti(Key.Space) Then : i = "Space" : y = True : End If
        If tasti(Key.Tab) Then : i = "Tab" : y = True : End If
        If tasti(Key.Up) Then : i = "Up" : y = True : End If
        If tasti(Key.Down) Then : i = "Down" : y = True : End If
        If tasti(Key.Left) Then : i = "Left" : y = True : End If
        If tasti(Key.Right) Then : i = "Right" : y = True : End If
        If tasti(Key.LeftControl) Then : i = "LCtrl" : y = True : End If
        If tasti(Key.RightControl) Then : i = "RCtrl" : y = True : End If

        'numpad
        If tasti(Key.NumPad0) Then : i = "KP0" : y = True : End If
        If tasti(Key.NumPad1) Then : i = "KP1" : y = True : End If
        If tasti(Key.NumPad2) Then : i = "KP2" : y = True : End If
        If tasti(Key.NumPad3) Then : i = "KP3" : y = True : End If
        If tasti(Key.NumPad4) Then : i = "KP4" : y = True : End If
        If tasti(Key.NumPad5) Then : i = "KP5" : y = True : End If
        If tasti(Key.NumPad6) Then : i = "KP6" : y = True : End If
        If tasti(Key.NumPad7) Then : i = "KP7" : y = True : End If
        If tasti(Key.NumPad8) Then : i = "KP8" : y = True : End If
        If tasti(Key.NumPad9) Then : i = "KP9" : y = True : End If
        If tasti(Key.NumPadEnter) Then : i = "KP_ENTER" : y = True : End If
        If tasti(Key.NumPadComma) Then : i = "KP_Comma" : y = True : End If
        If tasti(Key.NumPadEquals) Then : i = "KP_Equals" : y = True : End If
        If tasti(Key.NumPadMinus) Then : i = "KP_Minus" : y = True : End If
        If tasti(Key.NumPadPeriod) Then : i = "KP_Period" : y = True : End If
        If tasti(Key.NumPadPlus) Then : i = "KP_Plus" : y = True : End If
        If tasti(Key.NumPadSlash) Then : i = "KP_Divide" : y = True : End If
        If tasti(Key.NumPadStar) Then : i = "KP_Multiply" : y = True : End If

        'Lettere
        If tasti(Key.A) Then : i = "a" : y = True : End If
        If tasti(Key.B) Then : i = "b" : y = True : End If
        If tasti(Key.C) Then : i = "c" : y = True : End If
        If tasti(Key.D) Then : i = "d" : y = True : End If
        If tasti(Key.E) Then : i = "e" : y = True : End If
        If tasti(Key.F) Then : i = "f" : y = True : End If
        If tasti(Key.G) Then : i = "g" : y = True : End If
        If tasti(Key.H) Then : i = "h" : y = True : End If
        If tasti(Key.I) Then : i = "i" : y = True : End If
        If tasti(Key.J) Then : i = "j" : y = True : End If
        If tasti(Key.K) Then : i = "k" : y = True : End If
        If tasti(Key.L) Then : i = "l" : y = True : End If
        If tasti(Key.M) Then : i = "m" : y = True : End If
        If tasti(Key.N) Then : i = "n" : y = True : End If
        If tasti(Key.O) Then : i = "o" : y = True : End If
        If tasti(Key.P) Then : i = "p" : y = True : End If
        If tasti(Key.Q) Then : i = "q" : y = True : End If
        If tasti(Key.R) Then : i = "r" : y = True : End If
        If tasti(Key.S) Then : i = "s" : y = True : End If
        If tasti(Key.T) Then : i = "t" : y = True : End If
        If tasti(Key.U) Then : i = "u" : y = True : End If
        If tasti(Key.V) Then : i = "v" : y = True : End If
        If tasti(Key.W) Then : i = "w" : y = True : End If
        If tasti(Key.X) Then : i = "x" : y = True : End If
        If tasti(Key.Y) Then : i = "y" : y = True : End If
        If tasti(Key.Z) Then : i = "z" : y = True : End If

        'numeri
        If tasti(Key.D0) Then : i = "0" : y = True : End If
        If tasti(Key.D1) Then : i = "1" : y = True : End If
        If tasti(Key.D2) Then : i = "2" : y = True : End If
        If tasti(Key.D3) Then : i = "3" : y = True : End If
        If tasti(Key.D4) Then : i = "4" : y = True : End If
        If tasti(Key.D5) Then : i = "5" : y = True : End If
        If tasti(Key.D6) Then : i = "6" : y = True : End If
        If tasti(Key.D7) Then : i = "7" : y = True : End If
        If tasti(Key.D8) Then : i = "8" : y = True : End If
        If tasti(Key.D9) Then : i = "9" : y = True : End If
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'chiama la routine di creazione
        creaTastiera(Me, False)
    End Sub

    Public Sub SDLkeycode()
        '* The keyboard syms have been cleverly chosen to map to ASCII */
        If UCase(i) = "UNKNOWN" Then sdl = 0
        If UCase(i) = "FIRST" Then sdl = 0
        If UCase(i) = "BACKSPACE" Then sdl = 8
        If UCase(i) = "TAB" Then sdl = 9
        If UCase(i) = "CLEAR" Then sdl = 12
        If UCase(i) = "RETURN" Then sdl = 13
        If UCase(i) = "PAUSE" Then sdl = 19
        If UCase(i) = "ESCAPE" Then sdl = 27
        If i = "Space" Then sdl = 32
        If UCase(i) = "EXCLAIM" Then sdl = 33
        If UCase(i) = "QUOTEDBL" Then sdl = 34
        If UCase(i) = "HASH" Then sdl = 35
        If UCase(i) = "DOLLAR" Then sdl = 36
        If UCase(i) = "AMPERSAND" Then sdl = 38
        If UCase(i) = "QUOTE" Then sdl = 39
        If UCase(i) = "LEFTPAREN" Then sdl = 40
        If UCase(i) = "RIGHTPAREN" Then sdl = 41
        If UCase(i) = "ASTERISK" Then sdl = 42
        If UCase(i) = "PLUS" Then sdl = 43
        If UCase(i) = "COMMA" Then sdl = 44
        If UCase(i) = "MINUS" Then sdl = 45
        If UCase(i) = "PERIOD" Then sdl = 46
        If UCase(i) = "SLASH" Then sdl = 47
        If UCase(i) = "0" Then sdl = 48
        If UCase(i) = "1" Then sdl = 49
        If UCase(i) = "2" Then sdl = 50
        If UCase(i) = "3" Then sdl = 51
        If UCase(i) = "4" Then sdl = 52
        If UCase(i) = "5" Then sdl = 53
        If UCase(i) = "6" Then sdl = 54
        If UCase(i) = "7" Then sdl = 55
        If UCase(i) = "8" Then sdl = 56
        If UCase(i) = "9" Then sdl = 57
        If UCase(i) = "COLON" Then sdl = 58
        If UCase(i) = "SEMICOLON" Then sdl = 59
        If UCase(i) = "LESS" Then sdl = 60
        If i = "Equals" Then sdl = 61
        If UCase(i) = "GREATER" Then sdl = 62
        If UCase(i) = "QUESTION" Then sdl = 63
        If UCase(i) = "AT" Then sdl = 64

        'Skip uppercase letters

        If UCase(i) = "LEFTBRACKET" Then sdl = 91
        If UCase(i) = "BACKSLASH" Then sdl = 92
        If UCase(i) = "RIGHTBRACKET" Then sdl = 93
        If UCase(i) = "CARET" Then sdl = 94
        If UCase(i) = "UNDERSCORE" Then sdl = 95
        If UCase(i) = "BACKQUOTE" Then sdl = 96
        If LCase(i) = "a" Then sdl = 97
        If LCase(i) = "b" Then sdl = 98
        If LCase(i) = "c" Then sdl = 99
        If LCase(i) = "d" Then sdl = 100
        If LCase(i) = "e" Then sdl = 101
        If LCase(i) = "f" Then sdl = 102
        If LCase(i) = "g" Then sdl = 103
        If LCase(i) = "h" Then sdl = 104
        If LCase(i) = "i" Then sdl = 105
        If LCase(i) = "j" Then sdl = 106
        If LCase(i) = "k" Then sdl = 107
        If LCase(i) = "l" Then sdl = 108
        If LCase(i) = "m" Then sdl = 109
        If LCase(i) = "n" Then sdl = 110
        If LCase(i) = "o" Then sdl = 111
        If LCase(i) = "p" Then sdl = 112
        If LCase(i) = "q" Then sdl = 113
        If LCase(i) = "r" Then sdl = 114
        If LCase(i) = "s" Then sdl = 115
        If LCase(i) = "t" Then sdl = 116
        If LCase(i) = "u" Then sdl = 117
        If LCase(i) = "v" Then sdl = 118
        If LCase(i) = "w" Then sdl = 119
        If LCase(i) = "x" Then sdl = 120
        If LCase(i) = "y" Then sdl = 121
        If LCase(i) = "z" Then sdl = 122
        If UCase(i) = "DELETE" Then sdl = 127

        '/* Numeric keypad */
        If UCase(i) = "KP0" Then sdl = 256
        If UCase(i) = "KP1" Then sdl = 257
        If UCase(i) = "KP2" Then sdl = 258
        If UCase(i) = "KP3" Then sdl = 259
        If UCase(i) = "KP4" Then sdl = 260
        If UCase(i) = "KP5" Then sdl = 261
        If UCase(i) = "KP6" Then sdl = 262
        If UCase(i) = "KP7" Then sdl = 263
        If UCase(i) = "KP8" Then sdl = 264
        If UCase(i) = "KP9" Then sdl = 265
        If UCase(i) = "KP_PERIOD" Then sdl = 266
        If UCase(i) = "KP_DIVIDE" Then sdl = 267
        If UCase(i) = "KP_MULTIPLY" Then sdl = 268
        If UCase(i) = "KP_MINUS" Then sdl = 269
        If UCase(i) = "KP_PLUS" Then sdl = 270
        If UCase(i) = "KP_ENTER" Then sdl = 271
        If UCase(i) = "KP_EQUALS" Then sdl = 272

        '/* Arrows + Home/End pad */
        If UCase(i) = "UP" Then sdl = 273
        If UCase(i) = "DOWN" Then sdl = 274
        If i = "Right" Then sdl = 275
        If i = "Left" Then sdl = 276
        If UCase(i) = "INSERT" Then sdl = 277
        If UCase(i) = "HOME" Then sdl = 278
        If UCase(i) = "END" Then sdl = 279
        If UCase(i) = "PAGEUP" Then sdl = 280
        If UCase(i) = "PAGEDOWN" Then sdl = 281

        '/* Function keys */
        If UCase(i) = "F1" Then sdl = 282
        If UCase(i) = "F2" Then sdl = 283
        If UCase(i) = "F3" Then sdl = 284
        If UCase(i) = "F4" Then sdl = 285
        If UCase(i) = "F5" Then sdl = 286
        If UCase(i) = "F6" Then sdl = 287
        If UCase(i) = "F7" Then sdl = 288
        If UCase(i) = "F8" Then sdl = 289
        If UCase(i) = "F9" Then sdl = 290
        If UCase(i) = "F10" Then sdl = 291
        If UCase(i) = "F11" Then sdl = 292
        If UCase(i) = "F12" Then sdl = 293
        If UCase(i) = "F13" Then sdl = 294
        If UCase(i) = "F14" Then sdl = 295
        If UCase(i) = "F15" Then sdl = 296

        '/* Key state modifier keys */
        If UCase(i) = "NUMLOCK" Then sdl = 300
        If UCase(i) = "CAPSLOCK" Then sdl = 301
        If UCase(i) = "SCROLLOCK" Then sdl = 302
        If UCase(i) = "RSHIFT" Then sdl = 303
        If UCase(i) = "LSHIFT" Then sdl = 304
        If UCase(i) = "RCTRL" Then sdl = 305
        If UCase(i) = "LCTRL" Then sdl = 306
        If UCase(i) = "RALT" Then sdl = 307
        If UCase(i) = "LALT" Then sdl = 308
        If UCase(i) = "RMETA" Then sdl = 309
        If UCase(i) = "LMETA" Then sdl = 310
        If UCase(i) = "LSUPER" Then sdl = 311 '/* Left "Windows" key */
        If UCase(i) = "RSUPER" Then sdl = 312 '/* Right "Windows" key */
        If UCase(i) = "MODE" Then sdl = 313 '/* "Alt Gr" key */
        If UCase(i) = "COMPOSE" Then sdl = 314 '/* Multi-key compose key */

        '/* Miscellaneous function keys */
        If i = "Help" Then sdl = 315
        If i = "Print" Then sdl = 316
        If UCase(i) = "SYSREQ" Then sdl = 317
        If UCase(i) = "BREAK" Then sdl = 318
        If i = "Menu" Then sdl = 319
        If UCase(i) = "POWER" Then sdl = 320 '/* Power Macintosh power key */
        If UCase(i) = "EURO" Then sdl = 321 '/* Some european keyboards */
        If UCase(i) = "UNDO" Then sdl = 322
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Timer1.Start()
        GroupBox1.Enabled = True
        Button2.Enabled = True
        TextBox1.Focus()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Timer1.Stop()
        GroupBox1.Enabled = False
        Button2.Enabled = False
    End Sub

    Private Sub TextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If y = True Then TextBox1.Text = i
        y = False
        Call SDLkeycode()
        ff = " -command.fast_forward keyboard " & sdl
        TextBox2.Focus()
    End Sub
    Private Sub TextBox2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp
        If y = True Then TextBox2.Text = i
        y = False
        Call SDLkeycode()
        ss = " -command.save_state keyboard " & sdl
        TextBox3.Focus()
    End Sub

    Private Sub TextBox3_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyUp
        If y = True Then TextBox3.Text = i
        y = False
        Call SDLkeycode()
        ls = " -command.load_state keyboard " & sdl
        TextBox4.Focus()
    End Sub

    Private Sub TextBox4_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox4.KeyUp
        If y = True Then TextBox4.Text = i
        y = False
        Call SDLkeycode()
        enpci = " -command.togglenetview keyboard " & sdl
        TextBox5.Focus()
    End Sub

    Private Sub TextBox5_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox5.KeyUp
        If y = True Then TextBox5.Text = i
        y = False
        Call SDLkeycode()
        sss = " -Command.take_snapshot keyboard " & sdl
        TextBox6.Focus()
    End Sub

    Private Sub TextBox6_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox6.KeyUp
        If y = True Then TextBox6.Text = i
        y = False
        Call SDLkeycode()
        sidoip1 = " -Command.device_select1 keyboard " & sdl
        TextBox7.Focus()
    End Sub

    Private Sub TextBox7_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox7.KeyUp
        If y = True Then TextBox7.Text = i
        y = False
        Call SDLkeycode()
        sidoip2 = " -Command.device_select2 keyboard " & sdl
        TextBox8.Focus()
    End Sub

    Private Sub TextBox8_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox8.KeyUp
        If y = True Then TextBox8.Text = i
        y = False
        Call SDLkeycode()
        rt = " -Command.reset keyboard " & sdl
        TextBox9.Focus()
    End Sub

    Private Sub TextBox9_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox9.KeyUp
        If y = True Then TextBox9.Text = i
        y = False
        Call SDLkeycode()
        hrt = " -Command.power keyboard " & sdl
        TextBox10.Focus()
    End Sub

    Private Sub TextBox10_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox10.KeyUp
        If y = True Then TextBox10.Text = i
        y = False
        Call SDLkeycode()
        exp = " -Command.exit keyboard " & sdl
        Button2.Focus()
    End Sub
End Class
