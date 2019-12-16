Imports System.Runtime.InteropServices

Module DetectJoypad
    Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer,
                                                               ByVal lParam As Integer) As Integer
    Declare Function joyGetPosEx Lib "winmm.dll" (ByVal uJoyID As Integer, ByRef pji As JOYINFOEX) As Integer
    Declare Function joyGetDevCaps Lib "winmm.dll" Alias "joyGetDevCapsA" (ByVal uJoyID As Integer, ByRef pjc As JOYCAPS, ByVal cjc As Integer) As Integer
    Declare Function joyGetNumDevs Lib "winmm.dll" () As Integer
    Public HASPOV As Integer, sdj As String

    <StructLayout(LayoutKind.Sequential)>
    Public Structure JOYCAPS
        Dim wMid As Short
        Dim wPid As Short

        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)>
        Dim szPname As String

        Dim wXmin As Integer
        Dim wXmax As Integer
        Dim wYmin As Integer
        Dim wYmax As Integer
        Dim wZmin As Integer
        Dim wZmax As Integer
        Dim wNumButtons As Integer
        Dim wPeriodMin As Integer
        Dim wPeriodMax As Integer
        Dim wRmin As Integer
        Dim wRmax As Integer
        Dim wUmin As Integer
        Dim wUmax As Integer
        Dim wVmin As Integer
        Dim wVmax As Integer
        Dim wCaps As JoyCapOpts
        Dim wMaxAxes As Integer
        Dim wNumAxes As Integer
        Dim wMaxButtons As Integer

        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)>
        Dim szRegKey As String

        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)>
        Dim szOEMVxD As String

    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Public Structure JOYINFOEX
        Public dwSize As Integer 'Size, in bytes, of this structure.
        Public dwFlags As Integer 'Flags indicating the valid information returned in this structure. Members that do not contain valid information are set to zero. The following flags are defined:VER PAGINA WEB
        Public dwXpos As Integer 'Current X-coordinate.
        Public dwYpos As Integer 'Current Y-coordinate.
        Public dwZpos As Integer 'Current Z-coordinate.
        Public dwRpos As Integer 'Current position of the rudder or fourth joystick axis.
        Public dwUpos As Integer 'Current fifth axis position.
        Public dwVpos As Integer 'Current sixth axis position.
        Public dwButtons As Integer 'Current state of the 32 joystick buttons. The value of this member can be set to any combination of JOY_BUTTONn flags, where n is a value in the range of 1 through 32 corresponding to the button that is pressed.
        Public dwButtonNumber As Integer 'Current button number that is pressed.
        Public dwPOV As Integer 'Current position of the point-of-view control. Values for this member are in the range 0 through 35,900. These values represent the angle, in degrees, of each view multiplied by 100.
        Public dwReserved1 As Integer 'Reserved; do not use.
        Public dwReserved2 As Integer 'Reserved; do not use.
    End Structure

    <Flags>
    Public Enum JoyCapOpts As UInteger
        ''' <summary>Joystick has z-coordinate information.</summary>
        Has_Z_Axis = &H1
        ''' <summary>Joystick has rudder (fourth axis) information.</summary>
        Has_R_Axis = &H2
        ''' <summary>Joystick has u-coordinate (fifth axis) information.</summary>
        Has_U_Axis = &H4
        ''' <summary>Joystick has v-coordinate (sixth axis) information.</summary>
        Has_V_Axis = &H8
        ''' <summary>Joystick has point-of-view information.</summary>
        Has_PointOfView = &H10
        ''' <summary>Joystick point-of-view supports discrete values (centered, forward, backward, left, and right).</summary>
        Has_PointOfView_4Direction = &H20
        ''' <summary>Joystick point-of-view supports continuous degree bearings.</summary>
        Has_PointOfView_Continuous = &H40
    End Enum

    Public Const JOYSTICKID1 = 0
    Public Const JOYSTICKID2 = 1
    Public Const MMSYSERR_BASE = 0

    ' The joystick driver is not present.
    Public Const MMSYSERR_NODRIVER = (MMSYSERR_BASE + 6)

    ' An invalid parameter was passed.
    Public Const MMSYSERR_INVALPARAM = (MMSYSERR_BASE + 11)

    ' Windows 95/98/Me: The specified joystick identifier is invalid.
    Public Const MMSYSERR_BADDEVICEID = (MMSYSERR_BASE + 2)

    Public Const JOYERR_NOERROR = 0

    'if successful or one of the following error values.
    Public Const JOYERR_BASE = 160

    ' Windows NT/2000/XP: The specified joystick identifier is invalid.
    Public Const JOYERR_PARMS = (JOYERR_BASE + 5)

    ' The specified joystick is not connected to the system.
    Public Const JOYERR_UNPLUGGED = (JOYERR_BASE + 7)

    Public JoyNum As Long
    Private JoyInfoExtended As JOYINFOEX
    Public MYJOYEX As JOYINFOEX

    Public Sub InitJoy()
        Dim customCulture As Globalization.CultureInfo = CType(Threading.Thread.CurrentThread.CurrentCulture.Clone(), Globalization.CultureInfo)
        customCulture.NumberFormat.NumberDecimalSeparator = "."
        Threading.Thread.CurrentThread.CurrentCulture = customCulture

        MedGuiR.ComboBox6.Items.Clear()

        'Get the joystick number in the system and about information
        Dim xJa, xRj As Long
        Dim xJn As Integer
        'Dim sDJ As String
        Dim CapX As JOYCAPS

        xJa = joyGetNumDevs
        ' The joyGetNumDevs function returns the number of joysticks supported by the
        ' current driver or zero if no driver is installed.
        If (xJa = 0) Then
            MsgBox("There is no joystick driver installed.", MsgBoxStyle.Critical)
        End If
        'MsgBox("There are " & xJa & " joysticks")
        For xJn = 0 To (xJa - 1)
            'MsgBox(xJn)
            xRj = joyGetDevCaps(xJn, CapX, 404)

            Select Case xRj
                Case MMSYSERR_NODRIVER
                    'The joystick driver is not present or joystick identifier is invalid
                    MsgBox("The joystick driver is not present or joystick identifier is invalid", MsgBoxStyle.Critical)
                Case MMSYSERR_INVALPARAM
                    ' An invalid parameter was passed or joystick identifier is invalid
                    MsgBox("An invalid parameter was passed or joystick identifier is invalid", MsgBoxStyle.Critical)
                Case JOYERR_NOERROR
                    MedGuiR.ComboBox6.Items.Add(xJn)
                Case Else
                    ' default
            End Select
        Next

        If MedGuiR.ComboBox6.Items.Count > 0 Then
            MedGuiR.CheckBox16.Enabled = True

            If MedGuiR.CheckBox16.Checked = False Then MedGuiR.ComboBox6.Enabled = True
        Else
            MedGuiR.CheckBox16.Checked = False
            MedGuiR.CheckBox16.Enabled = False
            MedGuiR.ComboBox6.Enabled = False
        End If
    End Sub

End Module