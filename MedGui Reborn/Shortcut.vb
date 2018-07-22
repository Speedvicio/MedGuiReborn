Imports System.IO
Imports Microsoft.Win32

Module Shortcut
    Dim filePathIcon As String
    Dim DJ As Boolean

    Public Sub CreateShortcut()
        Dim wsh As Object = CreateObject("WScript.Shell")

        wsh = CreateObject("WScript.Shell")

        Dim MyShortcut, DesktopPath

        ' Read desktop path using WshSpecialFolders object

        DesktopPath = wsh.SpecialFolders("Desktop")

        ' Create a shortcut object on the desktop

        MyShortcut = wsh.CreateShortcut(DesktopPath & "\" & cleanpsx(rn) & ".lnk")

        ' Set shortcut object properties and save it

        MyShortcut.TargetPath = wsh.ExpandEnvironmentStrings(MedGuiR.TextBox4.Text & "\mednafen.exe")

        MyShortcut.WorkingDirectory = wsh.ExpandEnvironmentStrings(MedGuiR.TextBox4.Text)

        MedGuiR.SetSpecialModule()

        Dim ShortcutMsg As String = MsgBox("You use shortcut for online play?", vbYesNo + vbInformation, "Online Shortcut?")
        If ShortcutMsg = vbYes Then
            MyShortcut.arguments = "-connect " & "-force_module " & consoles & MedGuiR.tpce & " " & Chr(34) & percorso & Chr(34)
        Else
            MyShortcut.arguments = "-force_module " & consoles & MedGuiR.tpce & " " & Chr(34) & percorso & Chr(34)
        End If

        MyShortcut.WindowStyle = 4

        'Use this next line to assign a icon other then the default icon for the exe

        filePathIcon = ""

        If IO.File.Exists(pathimage) Then
            DetectJava()
            If DJ = True Then
                If File.Exists(MedExtra & "Plugins\mico.exe") Then
                    ConvertImageToIcon()
                End If
            Else
                ConvertImageToIcon1()
            End If
        Else
            MyShortcut.IconLocation = wsh.ExpandEnvironmentStrings(MedExtra & "\Resource\Gui\buggy.ico")
        End If

        If IO.File.Exists(filePathIcon) Then
            MyShortcut.IconLocation = wsh.ExpandEnvironmentStrings(filePathIcon)
        Else
            MyShortcut.IconLocation = wsh.ExpandEnvironmentStrings(MedExtra & "\Resource\Gui\buggy.ico")
        End If

        MyShortcut.Description = "Open this game with Mednafen"

        'Save the shortcut

        MyShortcut.Save()
    End Sub

    Private Sub DetectJava()
        Dim TargetKey As RegistryKey
        TargetKey = Registry.CurrentUser.OpenSubKey("Software\JavaSoft")
        If TargetKey Is Nothing Then
            DJ = False
        Else
            DJ = True
            TargetKey.Close()
            Exit Sub
        End If

        TargetKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\WOW6432Node\JavaSoft")
        If TargetKey Is Nothing Then
            DJ = False
        Else
            DJ = True
            TargetKey.Close()
            Exit Sub
        End If
    End Sub

    Public Sub ConvertImageToIcon1()
        CreateThumbnail()
        Dim CleanShort As String
        CleanShort = Replace(cleanpsx(rn), ",", " ")
        CleanShort = Replace(CleanShort, ".", " ")
        Dim filePathImage As String = MedExtra & "\Icons\test.png"
        filePathIcon = MedExtra & "\Icons\" & consoles & "_" & CleanShort.Trim & ".ico"
        Dim imageFile As Image = Image.FromFile(filePathImage, True)

        Using bitmapFile As New Bitmap(imageFile)
            bitmapFile.SetResolution(64, 64)
            Dim intptr As IntPtr = bitmapFile.GetHicon()

            Using iconFile As Icon = Icon.FromHandle(intptr)
                Using stream As Stream = File.Create(filePathIcon)
                    iconFile.Save(stream)
                End Using
            End Using
        End Using
    End Sub

    Public Sub ConvertImageToIcon()
        CreateThumbnail()

        If File.Exists(MedExtra & "\Icons\test.png") Then
            wDir = (MedExtra & "Plugins")
            tProcess = "mico"
            Arg = Chr(34) & MedExtra & "Icons\test.png" & Chr(34)
            StartProcess()
            execute.WaitForExit()

            If File.Exists(MedExtra & "\Plugins\mico.ico") Then
                Dim CleanShort As String
                CleanShort = Replace(cleanpsx(rn), ",", " ")
                CleanShort = Replace(CleanShort, ".", " ")
                filePathIcon = MedExtra & "Icons\" & consoles & "_" & CleanShort.Trim & ".ico"
                My.Computer.FileSystem.MoveFile(MedExtra & "Plugins\mico.ico", filePathIcon, True)
            End If
        End If
    End Sub

    Private Function ThumbnailCallback() As Boolean

    End Function

    Private Sub CreateThumbnail()
        Dim myCallback As Image.GetThumbnailImageAbort = AddressOf ThumbnailCallback
        Dim myBitmap As Bitmap = New Bitmap(pathimage)
        Dim myThumbnail As Image = myBitmap.GetThumbnailImage(128, 128, myCallback, IntPtr.Zero)
        myThumbnail.Save(MedExtra & "Icons\test.png")
    End Sub

End Module