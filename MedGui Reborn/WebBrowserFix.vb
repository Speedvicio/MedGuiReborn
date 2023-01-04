'Code imported by this: https://www.codeproject.com/Messages/5583483/Heres-a-better-version-and-it-DOESNT-require-admin

Imports Microsoft.Win32

Public Class WebBrowserFix

    '   private veriable
    Private Shared wbfAssemblyName As String

    '   methods

    ''' <summary>
    ''' Get current version of Internet Explorer on user's machine
    ''' </summary>
    ''' <returns>IE version # (throws exception if
    ''' IE is not present or pre-version 7)</returns>
    Public Shared Function GetCurrentBrowserVersion() As Integer
        Dim browserVersion As Integer = 0
        '   get version ID
        Using ieKey As RegistryKey =
                Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Internet Explorer",
                    RegistryKeyPermissionCheck.ReadSubTree,
                    System.Security.AccessControl.RegistryRights.QueryValues)
            Dim version As Object = ieKey.GetValue("svcVersion")
            If version Is Nothing Then
                version = ieKey.GetValue("Version")
                '   no Internet Explorer
                If version Is Nothing Then _
                    Throw New ApplicationException("Microsoft Internet Explorer is required!")
            End If
            Integer.TryParse(version.ToString().Split("."c)(0), browserVersion)
        End Using
        '   make sure version is 7 or higher
        If browserVersion < 7 Then
            Throw New ApplicationException("Browser version too low for WebBrowser control!")
        End If
        Return browserVersion
    End Function

    ''' <summary>
    ''' Set Internet-Explorer emulation mode for most recent application's assembly
    ''' </summary>
    ''' <param name="browserVersion">Desired IE version to emulate
    ''' (defaults current version on user's system if omitted)</param>
    ''' <remarks>An exception is thrown if the assembly name wasn't previously
    ''' specified, using the other overload for this method</remarks>
    Public Shared Sub SetBrowserEmulationVersion(Optional ByVal browserVersion As Integer = 0)
        '   get name of last assembly specified and set browser version
        WebBrowserFix.SetBrowserEmulationVersion(wbfAssemblyName, browserVersion)
    End Sub

    ''' <summary>
    ''' Set Internet-Explorer emulation mode for specified application's assembly
    ''' </summary>
    ''' <param name="AssemblyName">Name of assembly of (parent) application
    ''' (an exception is thrownn if null)</param>
    ''' <param name="browserVersion">Desired IE version to emulate
    ''' (defaults current version on user's system if omitted)</param>
    Public Shared Sub SetBrowserEmulationVersion(ByVal AssemblyName As String,
        Optional ByVal browserVersion As Integer = 0)
        '   get name of current assembly
        If String.IsNullOrEmpty(AssemblyName) Then
            '   not given
            Throw New ApplicationException("Application's assembly name MUST be specified!")
        Else
            '   save for future calls
            wbfAssemblyName = AssemblyName
        End If
        '   get browser version if not given
        If browserVersion < 1 Then
            browserVersion = WebBrowserFix.GetCurrentBrowserVersion()
        End If
        '   set emulation keys for 32-bit/64-bit release/debug versions
        Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Internet Explorer" _
            & "\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION",
                wbfAssemblyName & ".exe", browserVersion)
        Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Internet Explorer" _
            & "\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION",
                wbfAssemblyName & ".vshost.exe", browserVersion)
        Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Wow6432Node\" _
            & "Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION",
                wbfAssemblyName & ".exe", browserVersion)
        Registry.SetValue("HKEY_CURRENT_USER\SOFTWARE\Wow6432Node\" _
            & "Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION",
                wbfAssemblyName & ".vshost.exe", browserVersion)
    End Sub

End Class