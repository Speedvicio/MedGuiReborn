Imports System.Threading
Imports System.Net
Imports System
Imports System.IO

Public Class MedInstaller
    Dim cartella As String

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim processesByName As Process() = Process.GetProcessesByName("MedGuiR")
        Dim num As Integer = 0
        While num < CInt(processesByName.Length)
            processesByName(num).Kill()
            num = num + 1
        End While

        If File.Exists(cartella & "\MedGuiR\ListServer.txt") And CheckBox2.Checked = False Then File.Delete(cartella & "\MedGuiR\Update\MedGuiR\ListServer.txt")
        If File.Exists(cartella & "\MedGuiR\UCI.txt") And CheckBox1.Checked = False Then File.Delete(cartella & "\MedGuiR\Update\MedGuiR\UCI.txt")

        Timer1.Enabled = True
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cartella = Application.StartupPath

        If File.Exists(cartella & "\MedGuiR\ListServer.txt") Then CheckBox2.Enabled = True
        If File.Exists(cartella & "\MedGuiR\UCI.txt") Then CheckBox1.Enabled = True

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Public Sub elimino_temp()
        'Elimino i file al termine dell'update
        Try
            Directory.Delete(cartella & "\MedGuiR\Update\", True)
        Catch
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        Dim attemp As Integer = 1
        Try
            Dim infoReader As System.IO.FileInfo
            infoReader = My.Computer.FileSystem.GetFileInfo(cartella & "\MedGuiR\Update\MedGuiR.exe")

            If Dir(cartella & "\MedGuiR\Update\MedGuiR.exe") = "" Or infoReader.Length < 100 Then
                MsgBox("No MedGui Reborn update found, please reupdate MedGui Reborn and try again.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Update error")
            Else
                Button1.Enabled = False
                Label1.Text = "Wait...."

                File.Delete(cartella & "\MedGuiR\Update\update.txt")
URETRY:
                My.Computer.FileSystem.MoveDirectory(cartella & "\MedGuiR\Update\", cartella & "\", True)
                MsgBox("Update Done, i will run MedGui Reborn.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Update Done")
            End If

            elimino_temp()
            Process.Start(cartella & "\MedGuiR.exe")
            If File.Exists(cartella & "\Changelog MedGuiR.txt") And CheckBox3.Checked = True Then Process.Start(cartella & "\Changelog MedGuiR.txt")
            Me.Close()
        Catch

            attemp += attemp

            If attemp < 7 And attemp > 1 Then
                GoTo URETRY
            Else
                MsgBox("Any file seem locked by OS, try to move it manually on main MedGui Reborn folder after closing MedInstaller", vbCritical + vbOKOnly, "Unable to move file...")
                Process.Start(cartella & "\MedGuiR\Update\")
                If File.Exists(cartella & "\MedGuiR.exe") Then
                    Process.Start(cartella & "\MedGuiR.exe")
                Else
                    MsgBox("Unable to recognize MedGui Reborn", vbCritical + vbOKOnly)
                End If
                Me.Close()
            End If
        End Try
    End Sub

End Class