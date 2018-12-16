Imports System.IO

Public Class MPCG
    Private objStreamWriter As StreamWriter

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        rDes = "Select a Folder with Supported Multimedia File"
        MedGuiR.yPath()
        If Dir(rPath & "\*.*") = "" Or rPath = "" Then Exit Sub

        Dim SoundCartella As New IO.DirectoryInfo(rPath)
        Dim AudioFile() As IO.FileInfo
        Dim AudioInFolder As IO.FileInfo
        AudioFile = SoundCartella.GetFiles("*.*")

        Try
            For Each AudioInFolder In AudioFile
                Select Case AudioInFolder.Extension
                    Case ".mpc", ".ogg", ".flac", ".wav"
                        ListBox1.Items.Add(AudioInFolder.FullName)
                End Select
            Next
            ListBox1.SelectedIndex = ListBox1.Items.Count - 1
            ListBox1.Focus()
        Catch
        End Try
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        ListBox1.Items.Clear()
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Dim fdlgA As OpenFileDialog = New OpenFileDialog()
        fdlgA.Title = "Select Supported Multimedia File"
        fdlgA.Filter = "All supported format (*.mpc, *.ogg,*.flac,*.wav)|*.mpc;*.ogg;*.flac;*.wav"
        fdlgA.FilterIndex = 1
        fdlgA.RestoreDirectory = True
        If fdlgA.ShowDialog() = DialogResult.OK Then
            ListBox1.Items.Add(fdlgA.FileName)
            ListBox1.SelectedIndex = ListBox1.Items.Count - 1
        End If
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        If ListBox1.SelectedItem = Nothing Then Exit Sub
        Dim indice As Integer = ListBox1.SelectedIndex
        ListBox1.Items.RemoveAt(indice)

        If ListBox1.Items.Count >= indice And ListBox1.Items.Count > 0 Then
            If indice = 0 Then
                ListBox1.SelectedIndex = indice
            Else
                ListBox1.SelectedIndex = indice - 1
            End If

        End If
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        If ListBox1.SelectedItem = Nothing Or ListBox1.Items.Count < 0 Then Exit Sub

        Dim TRACK As String
        ListBox1.SelectedIndex = 0

        Dim Cfullpath = MsgBox("Do you want to include full path into TRACK?", MsgBoxStyle.Information + vbYesNo, "Include path...")
        Dim otherscue As String = ""

        Dim saveFileDialog1 As New SaveFileDialog()
        saveFileDialog1.Filter = "Cue Audio Playlist|*.cue"
        saveFileDialog1.Title = "Save an Cue Audio Playlist File"

        If saveFileDialog1.ShowDialog() = DialogResult.OK Then
            If saveFileDialog1.FileName <> "" Then
                If File.Exists(saveFileDialog1.FileName) Then File.Delete(saveFileDialog1.FileName)

                For i = 0 To ListBox1.Items.Count - 1
                    Dim AudioEx As String = LCase(Path.GetExtension(ListBox1.Text))
                    Select Case AudioEx
                        Case ".mpc", ".ogg"
                            TRACK = " " & UCase(Replace(AudioEx, ".", "")) & vbCrLf & "  TRACK " & (i + 1).ToString("D2") & " AUDIO" & vbCrLf & "    INDEX 01 00:00:00" & vbCrLf
                        Case ".flac", ".wav"
                            TRACK = " WAVE" & vbCrLf & "  TRACK " & (i + 1).ToString("D2") & " AUDIO" & vbCrLf & "    INDEX 01 00:00:00" & vbCrLf
                    End Select

                    objStreamWriter = File.AppendText(saveFileDialog1.FileName)
                    If Cfullpath = vbYes Then
                        objStreamWriter.Write("FILE " & Chr(34) & ListBox1.Text & Chr(34) & TRACK)
                        otherscue = vbCrLf & "Disable filesys.untrusted_fip_check to load it on Mednafen"
                    Else
                        objStreamWriter.Write("FILE " & Chr(34) & Path.GetFileName(ListBox1.Text) & Chr(34) & TRACK)
                        otherscue = ""
                    End If
                    objStreamWriter.Close()

                    If ListBox1.SelectedIndex < ListBox1.Items.Count - 1 Then
                        ListBox1.SelectedIndex = ListBox1.SelectedIndex + 1
                    End If
                Next
                objStreamWriter.Dispose()
                MsgBox("Cue Playlist Generated!" & otherscue, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "Playlist Cue Generated")
            End If
        End If
    End Sub

    Private Sub MPCG_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = gIcon
        Read_Resource()
    End Sub

End Class