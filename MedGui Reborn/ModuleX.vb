Imports Microsoft.DirectX
Imports Microsoft.DirectX.DirectInput
Module ModuleX
    'tutto direct input
    Public tastiera As Device 'controllo tastiera


    Sub creaTastiera(ByVal Form1 As Control, Optional ByVal esclusiva As Boolean = False)
        tastiera = New Device(SystemGuid.Keyboard)
        If esclusiva Then
            tastiera.SetCooperativeLevel(Form1, CooperativeLevelFlags.Foreground Or CooperativeLevelFlags.Exclusive)
        Else
            tastiera.SetCooperativeLevel(Form1, CooperativeLevelFlags.Background Or CooperativeLevelFlags.NonExclusive)
        End If
        tastiera.SetDataFormat(DeviceDataFormat.Keyboard)
        tastiera.Acquire()
    End Sub

    Function TastieraData() As KeyboardState
        TastieraData = tastiera.GetCurrentKeyboardState
    End Function
End Module
