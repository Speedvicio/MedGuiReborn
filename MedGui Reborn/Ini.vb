Public Class Ini
    Private Declare Auto Function GetPrivateProfileString Lib "kernel32.dll" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Private Declare Auto Function WritePrivateProfileString Lib "kernel32.dll" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer

    Friend Function IniRead(ByVal Filename As String, ByVal Section As String, ByVal Key As String, Optional ByVal lpDefault As String = "", Optional ByVal bRaiseError As Boolean = False) As String

        Dim RetVal As String = New String(" ", 255)

        Dim LenResult As Integer

        Dim ErrString As String

        LenResult = GetPrivateProfileString(Section, Key, lpDefault, RetVal, RetVal.Length, Filename)

        If LenResult = 0 AndAlso bRaiseError Then

            If Not (System.IO.File.Exists(Filename)) Then

                ErrString = "Unable to open INI file" & Filename
            Else

                ErrString = "The section or the key are wrong and/or File access denied"

            End If

            Throw New Exception(ErrString)

        End If

        Return RetVal.Substring(0, LenResult)

    End Function

    Friend Function IniWrite(ByVal Filename As String, ByVal Section As String, ByVal Key As String, ByVal Value As String, Optional ByVal bRaiseError As Boolean = False) As Boolean

        Dim LenResult As Integer

        Dim ErrString As String

        LenResult = WritePrivateProfileString(Section, Key, Value, Filename)

        If LenResult = 0 And bRaiseError Then

            If Not (System.IO.File.Exists(Filename)) Then

                ErrString = "Unable to open INI file" & Filename
            Else

                ErrString = "File access denied"

            End If

            Throw New Exception(ErrString)

        End If

        Return IIf(LenResult = 0, False, True)

        End

    End Function

End Class