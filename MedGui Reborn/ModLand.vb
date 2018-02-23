Imports System.IO
Imports System.Net

Public Class ModLand
    Dim filter1, download, cex As String
    Public ModServer As String

    Private Sub GResize()
        DataGridView1.AutoResizeColumns()
    End Sub

    Public Sub ChipLoadGridDataInFile()
        DataGridView1.Rows.Clear()
        SoxStatus.Text = "Waiting For ModLand Scrape"
        SoxStatus.Label1.Text = "Waiting..."
        SoxStatus.Show()

        Try
            Dim fName As String = ""
            Dim filter As String = ""
            Dim TextLine As String = ""
            filter = LCase(TextBox1.Text)
            ChipConsole()

            fName = String.Concat(MedExtra, "\Plugins\db\allmods.txt")
            If System.IO.File.Exists(fName) = True Then

                Using objReader As New System.IO.StreamReader(fName)
                    Dim cr As Integer = 0
                    While objReader.Peek() <> -1
                        cr = cr + 1
                        Dim firstsplit As String() = Split(objReader.ReadLine(), vbTab)
                        TextLine = firstsplit(1)
                        Dim SplitLine As String() = Split(TextLine, "/")

                        If TextBox1.Text <> "" And filter1 = "" Then
                            If LCase(TextLine).Contains(filter) Then
                                DataGridView1.Rows.Add(SplitLine)
                            End If
                        ElseIf TextBox1.Text = "" And filter1 = "" Then
                            DataGridView1.Rows.Add(SplitLine)
                        ElseIf TextBox1.Text = "" And filter1 <> "" Then
                            If TextLine.Contains(filter1) Then
                                DataGridView1.Rows.Add(SplitLine)
                            End If
                        ElseIf TextBox1.Text <> "" And filter1 <> "" Then
                            If TextLine.Contains(filter1) Then
                                If LCase(TextLine).Contains(filter) Then DataGridView1.Rows.Add(SplitLine)
                            End If
                        End If
                    End While
                    objReader.Dispose()
                    objReader.Close()
                End Using
                SoxStatus.Close()

                If DataGridView1.Rows.Count = 0 Then MsgBox("No Match Found!", vbOKOnly + MsgBoxStyle.Exclamation)
            Else
                SoxStatus.Close()
                MsgBox("ModLand database does Not Exist, please press refresh button", MsgBoxStyle.OkOnly, Nothing)
                Exit Sub
            End If

            GResize()
        Catch exception As System.Exception
            SoxStatus.Close()
        End Try
    End Sub

    Private Sub ChipConsole()
        filter1 = ""
        Select Case ComboBox1.Text
            Case "All System"
                filter1 = ""
            Case "Nintendo Entraiment System"
                filter1 = "Nintendo Sound Format"
            Case "NEC Pc Engine"
                filter1 = "HES"
            Case "Super Nintendo"
                filter1 = "Nintendo SPC"
            Case "Sega Megadrive - Genesis"
                filter1 = "Sega Megadrive"
            Case "Sega 32X", "Sega Game Gear", "Sega Master System"
                filter1 = ComboBox1.Text
            Case "Game Boy"
                filter1 = "Gameboy Sound System"
            Case "Game Boy Advence"
                filter1 = "Gameboy Sound Format"
            Case "Sony Playstation"
                filter1 = "Playstation Sound Format"
            Case "Bandai Wonderswan"
                filter1 = "WonderSwan"
            Case "Sega Saturn"
                filter1 = "Saturn Sound Format"
        End Select

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ChipLoadGridDataInFile()
    End Sub

    Private Sub changechipstate()
        Dim f As New FileInfo(MedExtra & "/Media/Module" & download)
        If f.Exists = False Then
            PictureBox1.BackColor = Color.DarkRed
            Button2.Enabled = False
            Button3.Enabled = False
        ElseIf f.Length < 10 Then
            MsgBox("There were problems downloading the file, try again or change the server.", vbOKOnly + vbExclamation, "Error on download...")
            PictureBox1.BackColor = Color.ForestGreen
            Button2.Enabled = True
            Button3.Enabled = True
        Else
            PictureBox1.BackColor = Color.ForestGreen
            Button2.Enabled = True
            Button3.Enabled = True
        End If
    End Sub

    Private Sub DataGridView1_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentDoubleClick
        download_Module()
    End Sub

    Public Sub download_Module()
        ServerLink()

        Try
            If Directory.Exists(MedExtra & "/Media/Module") Then
            Else
                Directory.CreateDirectory(MedExtra & "/Media/Module/")
            End If

            If File.Exists(MedExtra & "/Media/Module" & download) = False Then
                My.Computer.Network.DownloadFile(ModServer & "/pub/modules" & download, MedExtra & "/Media/Module" & download, "anonymous", "", True, 500, True)
                DownloadDriver()
                changechipstate()
                MsgBox("File and Driver Downloaded", vbOKOnly + MsgBoxStyle.Information)
            Else
                Select Case LCase(Path.GetExtension(MedExtra & "/Media/Module" & download))
                    Case ".wsr", ".psf", ".psf1", ".minipsf", ".gsf", ".minigsf", ".hes", ".nsf", ".spc", ".rsn", ".vgz", ".vgm", ".gbs", ".ssf", ".minissf"
                        DownloadDriver()
                        MedGuiR.SY.Text = ""
                        percorso = MedExtra & "Media/Module" & download
                        MedGuiR.TextBox1.Text = percorso
                        SingleScan()
                        MedGuiR.DataGridView1.CurrentCell = MedGuiR.DataGridView1(0, MedGuiR.DataGridView1.RowCount - 1)
                        MedGuiR.DataGridView1.Focus()
                        'Case ".mod", ".s3m", ".xm", ".it", ".midi"
                    Case Else
                        MsgBox("Mednafen still can not play this format, try an alternative player for your OS", vbOKOnly + MsgBoxStyle.Information)
                End Select

            End If
        Catch exio As IOException
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If File.Exists(MedExtra & "/Media/Module" & download) Then
            Dim realchipath As String = MedExtra & "Media\Module" & Replace(download, "/", "\")
            Process.Start("explorer.exe", " /select ," & Chr(34) & realchipath & Chr(34))
        Else
            MsgBox("This file not exist", MsgBoxStyle.Exclamation + vbOKOnly, "Unrecognized file")
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim dels As MsgBoxResult = MsgBox("Do you want to delete """ & Path.GetFileName(MedExtra & "/Media/Module" & download) & """ ?", vbOKCancel + vbExclamation)
        If dels = vbOK Then
            System.IO.File.Delete((MedExtra & "/Media/Module" & download))
        End If
    End Sub

    Private Sub DownloadDriver()

        Try
            'Select Case Path.GetExtension(download)
            'Case ".minigsf", ".minipsf", ".psf", ".ssf", ".minissf"
            Using objReader As New System.IO.StreamReader(MedExtra & "/Media/Module" & download)
                While objReader.Peek() <> -1
                    Dim LINE As String = objReader.ReadLine()
                    If LINE.Contains("_lib") Then
                        Dim firstsplit As String() = Split(LINE, "=")
                        cex = firstsplit(1)
                        TryToDownloadDriver()
                    End If
                End While
                objReader.Dispose()
                objReader.Close()
            End Using
            'Case Else
            'Exit Sub
            'End Select
        Catch
        End Try

    End Sub

    Private Sub TryToDownloadDriver()

        If Dir(MedExtra & "/Media/Module" & Path.GetDirectoryName(download) & "\" & cex) <> "" Then Exit Sub

        ServerLink()

        Dim req As FtpWebRequest = FtpWebRequest.Create(ModServer & "/pub/modules" & Path.GetDirectoryName(download) & "/")
        req.Credentials = New NetworkCredential("anonymous", "")
        req.Method = WebRequestMethods.Ftp.ListDirectory

        Dim sr As New StreamReader(req.GetResponse().GetResponseStream())
        Dim str As String = sr.ReadLine()

        While Not str Is Nothing

            Dim client As New Net.WebClient
            client.Credentials = New Net.NetworkCredential("anonymous", "")

            ServerLink()

            If LCase(str).Contains(LCase(cex)) Then
                client.DownloadFile(ModServer & "/pub/modules/" & Path.GetDirectoryName(download) & "/" & str, MedExtra & "/Media/Module" & Path.GetDirectoryName(download) & "/" & str)
            End If

            str = sr.ReadLine()
        End While

        sr.Close()
        sr = Nothing
        req = Nothing
    End Sub

    Private Sub ModLand_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Read_Resource()
    End Sub

    Private Sub TextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyUp
        Select Case e.KeyCode
            Case Keys.Enter
                ChipLoadGridDataInFile()
        End Select
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        ServerLink()
        get_Modsupdate()
    End Sub

    Private Sub Button2_MouseDown(sender As Object, e As MouseEventArgs) Handles Button2.MouseDown
        Dim fil, files() As String
        Dim f As FileInfo

        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim risCache = MsgBox("Do you want to Empty SoundChip Cache?", vbOKCancel + MsgBoxStyle.Exclamation, "SoundChip Clean Cache")

            If risCache = vbOK Then
                Try

                    files = Directory.GetFiles(MedExtra & "/Media/Module")
                    For Each fil In files
                        f = New FileInfo(fil)
                        f.Delete()
                    Next

                    If (System.IO.Directory.GetDirectories(MedExtra & "/Media/Module").Length) > 0 Then
                        System.IO.Directory.Delete(MedExtra & "/Media/Module/", True)
                    End If
                Catch ex As Exception
                    If Dir(MedExtra & "/Media/Module/*.*") <> "" Then
                        MsgBox("File " & fil & " is die hard, please remove it manually or delete RomTemp folder.", MsgBoxStyle.OkCancel + MsgBoxStyle.Critical)
                        Process.Start(MedExtra & "/Media/Module")
                        End
                    End If
                End Try
            End If
        End If

    End Sub

    Private Sub DataGridView1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseClick

        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                sender.ClearSelection()
                Dim ht As DataGridView.HitTestInfo = sender.HitTest _
                    (e.X, e.Y)
                If ht.ColumnIndex <> -1 And ht.RowIndex <> -1 Then
                    sender.Item(ht.ColumnIndex, ht.RowIndex).Selected = True
                    DataGridView1.CurrentCell = DataGridView1.Item(ht.ColumnIndex, ht.RowIndex)
                    If Me.DataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0 Then Clipboard.SetDataObject(Me.DataGridView1.GetClipboardContent())
                End If
            End If
        Catch
            MsgBox("A strange error occurred!" &
                       vbCrLf & "", MsgBoxStyle.Exclamation)
        End Try

    End Sub

    Private Sub ServerLink()

        Select Case ToolStripComboBox2.Text
            Case "Modland"
                ModServer = "ftp://ftp.modland.com"
            Case "Exotica"
                ModServer = "ftp://aero.exotica.org.uk/pub/mirrors/modland"
            Case "Ziphoid"
                ModServer = "ftp://modland.ziphoid.com"
            Case "AmigaScne"
                ModServer = "ftp://ftp.amigascne.org/mirrors/ftp.modland.com"
            Case Else
                ModServer = "ftp://ftp.modland.com"
        End Select

    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If Me.DataGridView1.GetCellCount(
    DataGridViewElementStates.Selected) > 0 Then
            download = ""
            Try
                For i = 0 To 4
                    If DataGridView1.CurrentRow.Cells(i).Value() <> "" Then
                        download = download & "/" & DataGridView1.CurrentRow.Cells(i).Value()
                    End If
                Next i
                changechipstate()
            Catch ex As System.Runtime.InteropServices.ExternalException
            End Try
        End If
    End Sub

End Class