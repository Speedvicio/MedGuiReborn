Public Class Audio

    Private booAbortMusic As Boolean

    Dim XMPointer As Integer = 0
    Public SOUNDDIR As String
    Public Declare Function FSOUND_Close Lib "fmod.dll" Alias "_FSOUND_Close@0" () As Integer
    Public Declare Function FSOUND_Init Lib "fmod.dll" Alias "_FSOUND_Init@12" (ByVal mixrate As Integer, ByVal maxchannels As Integer, ByVal flags As Integer) As Byte
    Public Declare Function FMUSIC_LoadSong Lib "fmod.dll" Alias "_FMUSIC_LoadSong@4" (ByVal name As String) As Integer
    Public Declare Function FMUSIC_PlaySong Lib "fmod.dll" Alias "_FMUSIC_PlaySong@4" (ByVal xmodule As Integer) As Byte
    Public Declare Function FMUSIC_StopSong Lib "fmod.dll" Alias "_FMUSIC_StopSong@4" (ByVal xmodule As Integer) As Byte
    Public Declare Function FMUSIC_OptimizeChannels Lib "fmod.dll" Alias "_FMUSIC_OptimizeChannels@12" (ByVal xmodule As Integer, ByVal maxchannels As Integer, ByVal minvolume As Integer) As Byte
    Public Declare Function FMUSIC_SetLooping Lib "fmod.dll" Alias "_FMUSIC_SetLooping@8" (ByVal xmodule As Integer, ByVal looping As Byte) As Byte
    Public Declare Function FMUSIC_IsPlaying Lib "fmod.dll" Alias "_FMUSIC_IsPlaying@4" (ByVal xmodule As Integer) As Byte
    Public Declare Function FMUSIC_FreeSong Lib "fmod.dll" Alias "_FMUSIC_FreeSong@4" (ByVal xmodule As Integer) As Byte
    Private Declare Function FSOUND_DSP_GetSpectrum Lib “fmod.dll” Alias “_FSOUND_DSP_GetSpectrum@0” () As Integer
    Private Declare Function FSOUND_DSP_GetFFTUnit Lib “fmod.dll” Alias “_FSOUND_DSP_GetFFTUnit@0” () As Integer
    Private Declare Function FSOUND_DSP_SetActive Lib “fmod.dll” Alias “_FSOUND_DSP_SetActive@8” (ByVal unit As Integer, ByVal active As Integer) As Integer

    Public Sub New()
        FSOUND_Init(44100, 128, 0)
        FSOUND_DSP_GetFFTUnit
        FSOUND_DSP_GetSpectrum
    End Sub

    Public Sub PlaySound()
        If booAbortMusic = False Then
            XMPointer = FMUSIC_LoadSong(SOUNDDIR) ' path to the xm file in the temp folder
            FMUSIC_SetLooping(XMPointer, 1)
            FMUSIC_OptimizeChannels(XMPointer, 256, 10)
            FMUSIC_PlaySong(XMPointer) ' play the music
            booAbortMusic = True
        End If
    End Sub

    Public Sub StopMusic()
        'Stop the music.
        If booAbortMusic = True Then
            FMUSIC_StopSong(XMPointer)
            FMUSIC_FreeSong(XMPointer)
            'FSOUND_Close
            booAbortMusic = False
        End If
    End Sub

End Class