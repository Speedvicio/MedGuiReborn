﻿Imports System.IO

Public Class About
    Private AudioAbout As New Audio
    Private oldSOUNDIR As String

    Private Sub About_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = gIcon

        Try
            Dim privateFonts As New System.Drawing.Text.PrivateFontCollection()
            privateFonts.AddFontFile(MedExtra & "Resource\Gui\metallord.ttf")
            Dim font As New System.Drawing.Font(privateFonts.Families(0), 20)
            Label1.Font = font
        Catch
        End Try

        PictureBox1.Load(MedExtra & "Resource\Gui\MedGuiR.png")
        Label2.Text = "Version: " & Replace(MedGuiR.Label6.Text, "MedGuiR v.", "")

        'tProcess = "xmplay"
        'KillProcess()
        'Arg = " module.zip" & " -play -tray"
        'wDir = (MedExtra & "Resource\Music")
        'StartProcess()

        If File.Exists(Path.Combine(Application.StartupPath, "External.rtf")) Then
            RichTextBox1.LoadFile(Path.Combine(Application.StartupPath, "External.rtf"), RichTextBoxStreamType.RichText)
        Else
            Me.Width = 320
        End If

        F1 = Me
        CenterForm()

        If IO.File.Exists(Application.StartupPath & "\fmod.dll") Then
RETRYMOD:
            AudioAbout.SOUNDDIR = GetRandomFilePath(MedExtra & "Resource\Music\module")
            If oldSOUNDIR <> AudioAbout.SOUNDDIR And AudioAbout.SOUNDDIR <> "" Then
                AudioAbout.PlaySound()
                oldSOUNDIR = AudioAbout.SOUNDDIR
            Else
                GoTo RETRYMOD
            End If

            If Environment.OSVersion.Version.Major >= 6 And IO.File.Exists(Application.StartupPath & "\CoreAudioApi.dll") And IO.File.Exists(Application.StartupPath & "\PeakMeterCtrl.dll") Then
                StartPeak()
                Timer1.Start()
            Else
                PeakMeterCtrl1.Dispose()
                PeakMeterCtrl2.Dispose()
            End If

        End If
    End Sub

    Public Function GetRandomFilePath(ByVal folderPath As String) As String
        Dim files() As String = IO.Directory.GetFiles(folderPath, "*.*")
        Dim random As Random = New Random()
        Return files(random.Next(0, files.Length - 1))
    End Function

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        _link = "https://github.com/Asnivor"
        open_link()
    End Sub

    Private Sub LinkLabel9_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel9.LinkClicked
        _link = "http://www.mednafen-it.org/"
        open_link()
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        _link = "https://github.com/zeromus"
        open_link()
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        _link = "https://discord.gg/qVvsxjg"
        open_link()
    End Sub

    Private Sub LinkLabel4_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel4.LinkClicked
        _link = "https://www.instagram.com/da_beatkitchen/"
        open_link()
    End Sub

    Private Sub About_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If IO.File.Exists(Application.StartupPath & "\fmod.dll") Then
            AudioAbout.StopMusic()
        End If
        'KillProcess()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        MovePeak()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        If IO.File.Exists(Application.StartupPath & "\fmod.dll") Then
            AudioAbout.StopMusic()
            AudioAbout.SOUNDDIR = GetRandomFilePath(MedExtra & "Resource\Music\module")
            AudioAbout.PlaySound()
        End If
    End Sub

    Private Sub LinkLabel5_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel5.LinkClicked
        _link = "https://gamehacking.org/"
        open_link()
    End Sub

    Private Sub RichTextBox1_LinkClicked(sender As Object, e As LinkClickedEventArgs) Handles RichTextBox1.LinkClicked
        _link = e.LinkText
        open_link()
    End Sub

    Private Sub LinkLabel6_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel6.LinkClicked
        _link = "https://github.com/Speedvicio"
        open_link()
    End Sub

    Private Sub LinkLabel7_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel7.LinkClicked
        _link = "https://github.com/Speedvicio/MedGuiReborn/blob/master/LICENSE"
        open_link()
    End Sub

    Private Sub LinkLabel8_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel8.LinkClicked
        _link = "https://modarchive.org/index.php?request=view_profile&query=93077"
        open_link()
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        If Label13.ForeColor = SystemColors.ControlText Then
            Label13.ForeColor = Color.DarkRed
        Else
            Label13.ForeColor = SystemColors.ControlText
        End If
    End Sub

End Class