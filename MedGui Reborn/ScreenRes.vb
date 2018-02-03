Imports System.Runtime.InteropServices

Module ScreenRes
    Public SupportedScrnSizes As New SupportedScreenSizes

    Public Class SupportedScreenSizes
        Private Const DM_PELSWIDTH As Integer = &H80000
        Private Const DM_PELSHEIGHT As Integer = &H100000

        <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
        Private Structure DEVMODEW
            <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)> Public dmDeviceName As String
            Public dmSpecVersion As UShort
            Public dmDriverVersion As UShort
            Public dmSize As UShort
            Public dmDriverExtra As UShort
            Public dmFields As UInteger
            Public Union1 As Anonymous_7a7460d9_d99f_4e9a_9ebb_cdd10c08463d
            Public dmColor As Short
            Public dmDuplex As Short
            Public dmYResolution As Short
            Public dmTTOption As Short
            Public dmCollate As Short
            <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)> Public dmFormName As String
            Public dmLogPixels As UShort 'The number of pixels per logical inch. Printer drivers do not use this member.
            Public dmBitsPerPel As UInteger 'Specifies the color resolution, in bits per pixel, of the display device.
            Public dmPelsWidth As UInteger 'Specifies the width, in pixels, of the visible device surface.
            Public dmPelsHeight As UInteger 'Specifies the height, in pixels, of the visible device surface.
            Public Union2 As Anonymous_084dbe97_5806_4c28_a299_ed6037f61d90
            Public dmDisplayFrequency As UInteger 'Specifies the frequency, in hertz (cycles per second), of the display device in a particular mode.
            Public dmICMMethod As UInteger
            Public dmICMIntent As UInteger
            Public dmMediaType As UInteger
            Public dmDitherType As UInteger
            Public dmReserved1 As UInteger
            Public dmReserved2 As UInteger
            Public dmPanningWidth As UInteger
            Public dmPanningHeight As UInteger
        End Structure

        <StructLayout(LayoutKind.Explicit)>
        Private Structure Anonymous_7a7460d9_d99f_4e9a_9ebb_cdd10c08463d
            <FieldOffset(0)> Public Struct1 As Anonymous_865d3c92_fe8c_4ee6_9601_a9eb2536957e
            <FieldOffset(0)> Public Struct2 As Anonymous_1b5f787e_41ca_472c_8595_3484490ffe0c
        End Structure

        <StructLayout(LayoutKind.Explicit)>
        Private Structure Anonymous_084dbe97_5806_4c28_a299_ed6037f61d90
            <FieldOffset(0)> Public dmDisplayFlags As UInteger
            <FieldOffset(0)> Public dmNup As UInteger
        End Structure

        <StructLayout(LayoutKind.Sequential)>
        Private Structure Anonymous_865d3c92_fe8c_4ee6_9601_a9eb2536957e
            Public dmOrientation As Short
            Public dmPaperSize As Short
            Public dmPaperLength As Short
            Public dmPaperWidth As Short
            Public dmScale As Short
            Public dmCopies As Short
            Public dmDefaultSource As Short
            Public dmPrintQuality As Short
        End Structure

        <StructLayout(LayoutKind.Sequential)>
        Private Structure Anonymous_1b5f787e_41ca_472c_8595_3484490ffe0c
            Public dmPosition As POINTL
            Public dmDisplayOrientation As UInteger
            Public dmDisplayFixedOutput As UInteger
        End Structure

        <StructLayout(LayoutKind.Sequential)>
        Private Structure POINTL
            Public x As Integer
            Public y As Integer
        End Structure

        <DllImport("user32.dll", EntryPoint:="EnumDisplaySettingsExW")>
        Private Shared Function EnumDisplaySettingsExW(<MarshalAs(UnmanagedType.LPWStr)> ByVal lpszDeviceName As String, ByVal iModeNum As Integer, ByRef lpDevMode As DEVMODEW, ByVal dwFlags As UInteger) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        ''' <summary>Gets a string array of supported screen sizes.</summary>
        ''' <param name="gDeviceName">Optional - The Device Name of the screen to get the supported sizes for. If no device name is supplied, the supported sizes of the primary screen are returned.</param>
        Public Function GetSizesAsStrings(Optional ByVal gDeviceName As String = "") As String()
            Dim sizelist As New List(Of String)
            For Each s As Size In GetSizes(gDeviceName)
                sizelist.Add(s.Width.ToString & "x" & s.Height.ToString)
            Next
            Return sizelist.ToArray
        End Function

        ''' <summary>Gets an array of supported screen sizes.</summary>
        ''' <param name="gDeviceName">Optional - The Device Name of the screen to get the supported sizes for. If no device name is supplied, the supported sizes of the primary screen are returned.</param>
        Public Function GetSizes(Optional ByVal gDeviceName As String = "") As Size()
            If gDeviceName = "" Then gDeviceName = Screen.PrimaryScreen.DeviceName
            Dim sizelist As New List(Of Size)
            Dim indx As Integer = 0
            Dim dm As New DEVMODEW
            dm.dmFields = DM_PELSWIDTH Or DM_PELSHEIGHT
            dm.dmSize = CUShort(Marshal.SizeOf(GetType(DEVMODEW)))
            While EnumDisplaySettingsExW(gDeviceName, indx, dm, 0)
                Dim sz As New Size(CInt(dm.dmPelsWidth), CInt(dm.dmPelsHeight))
                If Not sizelist.Contains(sz) Then sizelist.Add(sz)
                indx += 1
            End While
            Return sizelist.ToArray
        End Function

    End Class

End Module