Imports CoreAudioApi

Module VUMeter
    Dim NumLEDS As Integer = 20
    Private device As MMDevice

    Public Sub StartPeak()
        About.PeakMeterCtrl1.Start(1000 / 20)
        About.PeakMeterCtrl2.Start(1000 / 20)
        Dim DevEnum As New MMDeviceEnumerator()
        device = DevEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia)
        AddHandler device.AudioEndpointVolume.OnVolumeNotification, AddressOf OnVolumeNotification
        'Progressbar1.Value = CInt(device.AudioEndpointVolume.MasterVolumeLevelScalar * 100)
    End Sub

    Private Sub OnVolumeNotification(data As CoreAudioApi.AudioVolumeNotificationData)
        If About.InvokeRequired Then
            About.Invoke(New AudioEndpointVolumeNotificationDelegate(AddressOf OnVolumeNotification), data)
        Else
            'Progressbar1.Value = CInt(data.MasterVolume * 100)
        End If
    End Sub

    'Private Sub tbMaster_Scroll(sender As Object, e As EventArgs) Handles tbMaster.Scroll
    'device.AudioEndpointVolume.MasterVolumeLevelScalar = Progressbar1.Value / 100.0F
    'device.AudioEndpointVolume.Mute = True
    'End Sub

    Public Sub MovePeak()
        'Dim meters1() As Integer = New Integer((NumLEDS) - 1) {}
        Dim meters1() As Integer = New Integer((NumLEDS) - 1) {}
        Dim meters2() As Integer = New Integer((NumLEDS) - 1) {}
        Dim rand As Random = New Random
        Dim i As Integer = 0
        Do While (i < meters1.Length)
            'meters1(i) = rand.Next(0, Int(device.AudioMeterInformation.MasterPeakValue * 150)) 'rand.Next(0, 100)

            If Environment.OSVersion.Version.Major >= 6 And IO.File.Exists(Application.StartupPath & "\CoreAudioApi.dll") Then
                meters1(i) = Int(device.AudioMeterInformation.PeakValues(0) * 150)
                meters2(i) = Int(device.AudioMeterInformation.PeakValues(1) * 150)
            Else
                meters1(i) = rand.Next(0, 100)
                meters2(i) = rand.Next(0, 100)
            End If

            i = (i + 1)
        Loop

        About.PeakMeterCtrl2.SetData(meters1, 0, meters1.Length)
        About.PeakMeterCtrl1.SetData(meters2, 0, meters2.Length)
        'Me.PeakMeterCtrl3.SetData(meters3, 0, meters3.Length)
    End Sub

End Module