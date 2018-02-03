Imports System.IO

Public Class Standard_Conf
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Crea il file standard.conf che sarà letto da mednafen-server.exe
        Dim fconf As StreamWriter
        Dim net_e As String
        fconf = File.CreateText(MedExtra & "\NetPlay" & "\standard.conf")
        fconf.WriteLine("maxclients " & M_CLI.Text)
        fconf.WriteLine("connecttimeout " & C_TIM.Text)
        fconf.WriteLine("port " & S_POR.Text)
        If S_PASS.Text = "" Then net_e = "; " Else net_e = ""
        fconf.WriteLine(net_e & "password " & S_PASS.Text)
        fconf.WriteLine("idletimeout " & S_IDLE.Text)
        fconf.WriteLine("maxcmdpayload " & S_MCMD.Text)
        fconf.WriteLine("minsendqsize " & S_MISENDQ.Text)
        fconf.WriteLine("maxsendqsize " & S_MASENDQ.Text)
        fconf.Flush()
        fconf.Dispose()
        fconf.Close()
        MsgBox("New standard.conf was created!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
        Me.Close()
    End Sub
End Class