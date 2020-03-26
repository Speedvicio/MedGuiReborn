Imports System.IO
Imports System.Net
Imports System.Net.NetworkInformation
Imports Utilities.FTP

Module MedPlay
    Public SCountry, SLocation, Server, Password, Nick, NGameName, NRomName, NCRC, NModule, Gamekey, port, ping, NMednafenV, NNetParameters As String, NetIn, ftperror As Boolean
    Public GamePar As String
    Public ftp As New FTPclient

    Public Sub SetFTPData()
        ftp.CurrentDirectory = "/" & MedGuiR.TextBox23.Text
        'NetRom = "/" & MedClient.TextBox7.Text
        ftp.Username = MedGuiR.TextBox25.Text
        ftp.Password = MedGuiR.TextBox24.Text
        ftp.Hostname = "ftp://" & MedGuiR.ComboBox7.Text
    End Sub

    Public Sub FtpDownloadOnConnect()
        ftperror = False
        Try
            If ftp.FtpFileExists("/" & MedGuiR.TextBox23.Text & "/DONTDELETE.txt") Then
                'Get the detailed directory listing of /pub/
                Dim dirList As FTPdirectory = ftp.ListDirectoryDetail(ftp.CurrentDirectory)

                'filter out only the files in the list
                Dim filesOnly As FTPdirectory = dirList.GetFiles()

                'download these files
                For Each file As FTPfileInfo In filesOnly
                    ftp.Download(file, MedExtra & "\MedPlay\" & file.Filename, True)
                Next
            Else
                MsgBox("Invalid " & MedGuiR.TextBox23.Text & " Path on " & MedGuiR.ComboBox7.Text & " Server", vbOKOnly + MsgBoxStyle.Critical, "Path not Detected on this server")
                ftperror = True
                MedClient.Close()
            End If
        Catch
            MsgBox("Unable to detect server ftp, verify data access or try to connect later", vbOKOnly + MsgBoxStyle.Exclamation, "FTP Connection error...")
            ftperror = True
            MedClient.Close()
        End Try
    End Sub

    Public Sub FtpUploadOnConnect()
        Try
            'If ftp.FtpFileExists("/pub/upload.exe") Then

            'If ftp.GetFileSize("/pub/upload.exe") <> 0 Then
            'rename a file
            'ftp.FtpRename("/pub/upload.exe", "/pub/newname.exe")
            'delete a file
            'ftp.FtpDelete("/pub/newname.exe")
            'End If
            'End If
            If ftp.FtpFileExists(MedGuiR.TextBox23.Text & "/DONTDELETE.txt") Then
            Else
                ftp.FtpCreateDirectory(ftp.CurrentDirectory)
                ftp.Upload(MedExtra & "\DONTDELETE.txt", "DONTDELETE.txt")
            End If

            ftp.Upload(MedExtra & "\MedPlay\" & Nick, Nick)

            If MedGuiR.CheckBox19.Checked = True Then
                Select Case LCase(Path.GetExtension(percorso))
                    Case ".cue", ".ccd", ".m3u"
                    Case Else
                        If percorso.Length > 10485760 Then
                            MsgBox("You can't upload file > of 10 mb", vbOKOnly + MsgBoxStyle.Exclamation, "Upload error...")
                            ftperror = True
                            MedClient.Close()
                        End If
                        ftp.FtpCreateDirectory(ftp.CurrentDirectory & "Rom_" & Nick)
                        ftp.Upload(percorso, ftp.CurrentDirectory & "Rom_" & Nick & "/" & Path.GetFileName(percorso))
                End Select
            End If
        Catch
            MsgBox("Unable to detect server ftp, verify data access or try to connect later", vbOKOnly + MsgBoxStyle.Exclamation, "FTP Connection error...")
            ftperror = True
            MedClient.Close()
        End Try
    End Sub

    Public Sub FtpDeleteOnDisconnect()
        Try
            If ftp.FtpFileExists(Nick) Then
                ftp.FtpDelete(Nick)
            End If
            If ftp.FtpFileExists(ftp.CurrentDirectory & "Rom_" & Nick & "/" & NRomName) Then
                ftp.FtpDelete(ftp.CurrentDirectory & "Rom_" & Nick & "/" & NRomName)
                ftp.FtpDeleteDirectory(ftp.CurrentDirectory & "Rom_" & Nick)
            End If
        Catch
            MsgBox("Unable to detect server ftp, verify data access or try to connect later", vbOKOnly + MsgBoxStyle.Exclamation, "FTP Connection error...")
            ftperror = True
            MedClient.Close()
        End Try
    End Sub

    Public Sub ParseMednafenConfig()
        GamePar = ""
        Dim rnd3 As New Random()
        Dim row, splitrow() As String
        Try
            Using reader As New StreamReader(MedGuiR.TextBox4.Text & "\" & DMedConf & ".cfg")
                While Not reader.EndOfStream
                    row = reader.ReadLine
                    splitrow = row.Split(" ")
                    Dim splitrow1() = row.Split(".")
                    Select Case True
                        Case row.Contains("netplay.host")
                            Server = splitrow(1)
                            MedGuiR.ServerToolStripComboBox2.Text = Server.Trim
                        Case row.Contains("netplay.port")
                            port = splitrow(1)
                            MedGuiR.PortToolStripTextBox1.Text = port.Trim
                        Case row.Contains("netplay.nick")
                            Nick = splitrow(1)
                            If Nick.Trim = "" Then Nick = "NoNick" & rnd3.Next(1, 500)
                            MedGuiR.NickToolStripTextBox1.Text = Nick.Trim
                        Case row.Contains("netplay.password")
                            If splitrow(1) = "" Then Password = "No" Else Password = "Yes"
                        Case row.Contains("netplay.gamekey")
                            Gamekey = splitrow(1)
                            MedGuiR.GameKeyToolStripTextBox1.Text = Gamekey.Trim
                            'Case row.Contains(".input.port") And row.Contains(consoles & MedGuiR.tpce)
                            'GamePar += row.ToString & " "
                        Case row.Contains(".multitap ") And splitrow1(0) = consoles & MedGuiR.tpce
                            GamePar += "-" & row.ToString & " "
                        Case row.Contains(".memcard ") And splitrow1(0) = consoles & MedGuiR.tpce
                            GamePar += "-" & row.ToString & " "
                        Case row.Contains(".input.port") And splitrow1(0) = consoles & MedGuiR.tpce
                            For i = 1 To 8
                                If row.Contains(".input.port" & i & " ") Then
                                    GamePar += "-" & row.ToString & " "
                                End If
                            Next
                    End Select
                End While

                If NetIn = False Then
                    WriteNetPlaySession()
                End If

                reader.Dispose()
                reader.Close()
            End Using
        Catch
        End Try
    End Sub

    Public Function VerifyForm(ByVal nameForm As String) _
           As Boolean
        Dim f As Form
        For Each f In Application.OpenForms
            If f.Name = nameForm Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Sub WriteNetPlaySession()

        If NetIn = False Then
            NGameName = MedGuiR.DataGridView1.CurrentRow.Cells(0).Value().Trim & " " & Replace(MedGuiR.DataGridView1.CurrentRow.Cells(2).Value(), ".", "").Trim
            NModule = consoles & MedGuiR.tpce
            CheckCRCNet()
        End If

        Dim submedv(1) As String
        submedv = MedGuiR.Label8.Text.Split("v.")
        NMednafenV = submedv(1).Substring(1, submedv(1).Length - 1).Trim
        NRomName = Path.GetFileName(percorso)
        Dim fiso As StreamWriter
        Dim ContentUp = VSTripleDES.EncryptData("Nick=" & Nick) & vbCrLf & VSTripleDES.EncryptData("Server=" & Server) & vbCrLf & VSTripleDES.EncryptData("Port=" & port) & vbCrLf &
        VSTripleDES.EncryptData("GameName=" & NGameName) & vbCrLf & VSTripleDES.EncryptData("Module=" & NModule) & vbCrLf & VSTripleDES.EncryptData("Password=" & Password) & vbCrLf &
        VSTripleDES.EncryptData("Gamekey=" & Gamekey) & vbCrLf & VSTripleDES.EncryptData("RomFile=" & NRomName) & vbCrLf & VSTripleDES.EncryptData("CRC=" & NCRC) & vbCrLf & VSTripleDES.EncryptData("MednafenV=" & NMednafenV) &
         vbCrLf & VSTripleDES.EncryptData("NetParameter=" & GamePar.Trim)
        fiso = File.CreateText(MedExtra & "\MedPlay\" & Nick)
        fiso.WriteLine(ContentUp)
        fiso.Flush()
        fiso.Dispose()
        fiso.Close()

        'Enable it the next attemp
        'SendState()
    End Sub

    Public Sub SendState()
        If VerifyForm("UCI") And UCI.cmbChannel.Text = "#MedPlay" And UCI.btnConnect.Text = "&Disconnect" Then
            UCI.txtSend.Text = "/notice " & Nick & " has started a " & NGameName & " session with Mednafen " & NModule & " module"
            UCI.btnSend.PerformClick()
        End If
    End Sub

    Public Sub CheckCRCNet()
        Select Case LCase(Path.GetExtension(percorso))
            Case ".zip"
                stopzip = True
                scan_ext_compressed()
                NCRC = base_file
            Case ".cue", ".ccd", ".m3u"
                NCRC = "image"
            Case Else
                filepath = percorso
                GetCRC32()
                NCRC = base_file
        End Select
        stopzip = False
    End Sub

    Public Sub CleanLocalParsed()
        Try
            Dim dirInfo As New IO.DirectoryInfo(MedExtra & "\MedPlay\")
            Dim files = dirInfo.GetFiles()
            For Each theFile In files
                My.Computer.FileSystem.DeleteFile(theFile.FullName)
            Next
        Catch
        End Try
    End Sub

    Public Sub ParseUsedData()
        Dim row, splitrow() As String
        MedClient.DataGridView1.Rows.Clear()
        Try

            Dim dirInfo As New IO.DirectoryInfo(MedExtra & "\MedPlay\")
            Dim files = dirInfo.GetFiles()
            For Each theFile In files
                If theFile.Extension <> ".txt" Then

                    Using reader As New StreamReader(theFile.FullName)
                        While Not reader.EndOfStream
                            row = VSTripleDES.DecryptData(reader.ReadLine)
                            splitrow = row.Split("=")

                            Select Case True
                                Case splitrow(0) = ("Server")
                                    Server = splitrow(1)
                                Case splitrow(0) = ("Port")
                                    port = splitrow(1)
                                Case splitrow(0) = ("Nick")
                                    Nick = splitrow(1)
                                Case splitrow(0) = ("Password")
                                    Password = splitrow(1)
                                Case splitrow(0) = ("Gamekey")
                                    Gamekey = splitrow(1)
                                Case splitrow(0) = ("GameName")
                                    NGameName = splitrow(1)
                                Case splitrow(0) = ("Module")
                                    NModule = splitrow(1)
                                Case splitrow(0) = ("RomFile")
                                    NRomName = splitrow(1)
                                Case splitrow(0) = ("CRC")
                                    NCRC = splitrow(1)
                                Case splitrow(0) = ("MednafenV")
                                    NMednafenV = splitrow(1)
                                Case splitrow(0) = ("NetParameter")
                                    NNetParameters = splitrow(1)
                            End Select
                        End While
                        ReadNetPlaySession()
                        reader.Dispose()
                        reader.Close()
                    End Using
                End If
            Next
            If MedClient.DataGridView1.Rows.Count <= 0 Then MedClient.Button2.Enabled = False
        Catch
        End Try
    End Sub

    Private Sub CheckPing()

        Dim ipa As IPAddress = DirectCast(Dns.GetHostAddresses(Server)(0), IPAddress)
        Try
            Dim sock As New System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp)
            sock.Connect(ipa, port)
            If sock.Connected = True Then
                ' Port is in use and connection is successful
                Dim pingreq As Ping = New Ping()
                Dim rep As PingReply = pingreq.Send(Server)
                ping = rep.RoundtripTime + 10
            End If

            sock.Close()
        Catch ex As System.Net.Sockets.SocketException
            If ex.ErrorCode = 10061 Then
                ' Port is unused and could not establish connection
                'MessageBox.Show("Port is Open!")
            Else
                ping = "Closed"
                'MessageBox.Show(ex.Message)
            End If
        End Try
        'NetVerified = True
    End Sub

    Public Sub ReadNetPlaySession()
        Try
            CheckPing()
            If ping = "" Or ping = Nothing Then ping = "Unknown"
            ServerCountry(Server)
        Catch
        Finally
            MedClient.DataGridView1.Rows.Add(Nick, NGameName, NModule, ping, Server, port, Password, Gamekey, NCRC, NRomName, NMednafenV, Image.FromFile(SCountry))
        End Try
    End Sub

End Module