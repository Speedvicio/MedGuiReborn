Imports System.IO

Public Class MDM
    Public MDMpath, MDMext, MDMoperations, MDMr_name As String

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim folder As New FolderBrowserDialog

        If folder.ShowDialog = Windows.Forms.DialogResult.OK Then
            MDMpath = folder.SelectedPath
            cleaning()
        Else
            Exit Sub
        End If
        MDMoperations = "populate"
        scans()
    End Sub

    Private Sub extension()
        Select Case ComboBox1.Text
            Case "Atari - Lynx"
                ext = ".lnx"
            Case "Bandai - WonderSwan Color"
                ext = ".wsc"
            Case "Bandai - WonderSwan"
                ext = ".ws"
            Case "NEC - PC Engine - TurboGrafx 16", "NEC - Super Grafx"
                ext = ".pce"
            Case "Nintendo - Famicom Disk System"
                ext = ".fds."
            Case "Nintendo - Game Boy Advance"
                ext = ".gba"
            Case "Nintendo - Game Boy Color"
                ext = ".gbc"
            Case "Nintendo - Game Boy"
                ext = ".gb"
            Case "Nintendo - Super Nintendo Entertainment System"
                ext = ".sfc"
            Case ("Nintendo - Virtual Boy")
                ext = ".vb"
            Case "Nintendo Entertainment System"
                ext = ".nes"
            Case "Sega - Game Gear"
                ext = ".gg"
            Case "Sega - Master System - Mark III"
                ext = ".sms"
            Case "Sega - Mega Drive - Genesis"
                ext = ".md"
            Case "SNK - Neo Geo Pocket Color"
                ext = ".ngc"
            Case "SNK - Neo Geo Pocket"
                ext = ".ngp"
            Case "", "All"
                ext = ".*"
        End Select
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If ext = ".*" Then
            Dim in_ext As String
            in_ext = InputBox("Select a extension for file without '.'")
            ext = "." & in_ext
        End If
        Dim newpath As String = MDMpath & "\" & TextBox1.Text & ext
        Dim index As Integer = ListBox1.SelectedIndex
        System.IO.File.Move(filepath, newpath)
        ListBox1.Items(index) = TextBox1.Text & ext
        TextBox1.Text = ListBox1.Items(index)
    End Sub

    Public Sub scans()
        extension()

        Try
            For Each File As String In IO.Directory.GetFileSystemEntries(MDMpath, "*" & ext)
                Select Case MDMoperations
                    Case "populate"
                        ListBox1.Items.Add(Replace(File, MDMpath & "\", ""))
                    Case "writeDat"
                        MDMr_name = Replace(File, MDMpath & "\", "")
                        filepath = File
                        MD5CalcFile()
                        GetCRC32()
                        w_dat()
                End Select
            Next

            If ListBox1.Items.Count > 0 Then
                Button2.Enabled = True
            Else
                Button2.Enabled = False
                Button3.Enabled = False
            End If
        Catch ex As Exception
            MsgBox("No " & ext & " file inside the folder", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        MDMoperations = "writeDat"
        Dim in_dat As String
        If ComboBox1.Text = "" Then
            in_dat = InputBox("Select a name for Dat")
            If in_dat = "" Then Exit Sub
            ComboBox1.Text = in_dat
        End If
        scans()
        MsgBox("Dat Created!", vbOKOnly + MsgBoxStyle.Information)
        Me.Enabled = True
        cleaning()
        Button2.Enabled = False
        Button3.Enabled = False
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedItem <> "" Then
            filepath = MDMpath & "\" & ListBox1.SelectedItem
            MD5CalcFile()
            GetCRC32()
            Dim indice = ListBox1.SelectedItem.LastIndexOf(".")
            TextBox1.Text = ListBox1.SelectedItem.remove(indice)
            TextBox2.Text = r_crc
            TextBox3.Text = r_md5
            TextBox4.Text = r_sha
            Button3.Enabled = True
        End If
    End Sub

    Private Sub MDM_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        MedGuiR.list_DATs()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = gIcon
        cleaning()
        ComboBox1.Items.Clear()
        Read_Resource()
        Dim readText() As String = File.ReadAllLines(MedExtra & "DATs\ext")
        Dim s As String
        For Each s In readText
            Dim indice = s.LastIndexOf("=")
            ComboBox1.Items.Add(s.Substring(0, indice - 1))
        Next
        F1 = Me
        CenterForm()
        ColorizeForm()
    End Sub

    Private Sub cleaning()
        ListBox1.Items.Clear()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If MDMpath <> "" Then
            cleaning()
            extension()
            MDMoperations = "populate"
            scans()
        End If
    End Sub

End Class