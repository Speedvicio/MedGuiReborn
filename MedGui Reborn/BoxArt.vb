Imports System.IO
Imports System.Net

Module BoxArt
    Dim webimagelenght As Integer
    Public rn, pathimage, snap, title As String

    Public Sub Specific_Info()
        MedGuiR.PictureBox1.BackColor = DefBack
        MedGuiR.PictureBox5.BackColor = DefBack
        MedGuiR.PictureBox4.BackColor = DefBack

        Try
            Dim drom As Integer
            Select Case LCase(Right(MedGuiR.MainGrid.CurrentRow.Cells(4).Value(), 3))
                Case "zip", "rar", ".7z"
                    Dim szip As SevenZip.SevenZipExtractor = New SevenZip.SevenZipExtractor(percorso)
                    For Each ArchiveFileInfo In szip.ArchiveFileData
                        base_file = Hex(ArchiveFileInfo.Crc)
                        Select Case Trim(Len(base_file))
                            Case 6
                                base_file = "00" & base_file.Trim
                            Case 7
                                base_file = "0" & base_file.Trim
                        End Select
                        If base_file = MedGuiR.MainGrid.CurrentRow.Cells(8).Value Then
                            drom = ArchiveFileInfo.Size
                        End If
                    Next
                Case "cue"
                    Dim FSI1 As New FileInfo(Replace(MedGuiR.MainGrid.CurrentRow.Cells(4).Value(), Path.GetExtension(percorso), ".bin"))
                    If (FSI1.Exists) = True Then drom = FSI1.Length.ToString
                Case "ccd"
                    Dim FSI1 As New FileInfo(Replace(MedGuiR.MainGrid.CurrentRow.Cells(4).Value(), Path.GetExtension(percorso), ".img"))
                    If (FSI1.Exists) = True Then drom = FSI1.Length.ToString
                Case Else
                    Dim FSI1 As New FileInfo(MedGuiR.MainGrid.CurrentRow.Cells(4).Value())
                    If (FSI1.Exists) = True Then drom = FSI1.Length.ToString
            End Select

            Dim dimension As Integer, size As String
            dimension = ((Decimal.Round(drom) / 1024) * 8).ToString
            If dimension < 1024 Then size = " Kilobit"
            If dimension >= 1024 Then dimension = (dimension / 1024).ToString : size = " Megabit"

            Select Case LCase(Path.GetExtension(percorso))
                Case ".mai"
                    MedGuiR.PictureBox2.Load(MedExtra & "\Resource\Gui\mai.png")
                    MedGuiR.ToolTip1.SetToolTip(MedGuiR.PictureBox2, "Apple II+ MAI File")
                Case ".rar"
                    MedGuiR.PictureBox2.Load(MedExtra & "\Resource\Gui\rar.png")
                    MedGuiR.ToolTip1.SetToolTip(MedGuiR.PictureBox2, "Rar Compressed Rom")
                Case ".7z"
                    MedGuiR.PictureBox2.Load(MedExtra & "\Resource\Gui\7zip.png")
                    MedGuiR.ToolTip1.SetToolTip(MedGuiR.PictureBox2, "7Zip Compressed Rom")
                Case ".zip"
                    MedGuiR.PictureBox2.Load(MedExtra & "\Resource\Gui\zip.png")
                    MedGuiR.ToolTip1.SetToolTip(MedGuiR.PictureBox2, "Zip Compressed Rom")
                Case ".cue", ".ccd", ".m3u"
                    MedGuiR.PictureBox2.Load(MedExtra & "\Resource\Gui\dtl.png")
                    MedGuiR.ToolTip1.SetToolTip(MedGuiR.PictureBox2, "Disc Image File")
                    dimension = Decimal.Round((drom) / 1048576).ToString : size = " Megabyte"
                Case ".wsr", ".psf", ".psf1", ".minipsf", ".gsf", ".minigsf", ".hes", ".nsf", ".spc", ".rsn", ".vgz", ".vgm", ".gbs", ".ssf", ".minissf"
                    MedGuiR.PictureBox2.Load(MedExtra & "\Resource\Gui\chipsound.png")
                    MedGuiR.ToolTip1.SetToolTip(MedGuiR.PictureBox2, "Chip Sound Rom")
                Case Else
                    MedGuiR.PictureBox2.Load(MedExtra & "\Resource\Gui\rom.png")
                    MedGuiR.ToolTip1.SetToolTip(MedGuiR.PictureBox2, "Uncompressed Rom")
            End Select

            rn = Trim(MedGuiR.MainGrid.CurrentRow.Cells(0).Value())
            MedGuiR.Label1.Text = "Console: " & vbCrLf & MedGuiR.MainGrid.CurrentRow.Cells(5).Value()
            'MedGuiR.PictureBox2.Image = New Bitmap(icon_console)
            MedGuiR.Label2.Text = "Game Name: " '& vbCrLf & Replace(cleanpsx(rn), "&", "&&")
            MedGuiR.Label47.Text = Replace(cleanpsx(rn), "&", "&&")
            ResizeTextLabel2()
            If MedGuiR.MainGrid.CurrentRow.Cells(5).Value() = "Sony PlayStation" Then psx_version()
            'If MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() = "Sega Saturn" Then MedGuiR.DataGridView1.CurrentRow.Cells(2).Value() = "(" & v_ss & ")"
            MedGuiR.Label3.Text = "Version: " & Replace(MedGuiR.MainGrid.CurrentRow.Cells(2).Value(), ".", "")
            MedGuiR.Label4.Text = "No-Intro Status: " & MedGuiR.MainGrid.CurrentRow.Cells(3).Value()
            MedGuiR.Label5.Text = "Size: " & dimension & size

            If Directory.Exists(Path.Combine(MedExtra & "Media\Movie\", MedGuiR.MainGrid.CurrentRow.Cells(5).Value())) = False Then
                Directory.CreateDirectory(Path.Combine(MedExtra & "Media\Movie\", MedGuiR.MainGrid.CurrentRow.Cells(5).Value()))
            End If

            pathimage = (MedExtra & "BoxArt\" & MedGuiR.MainGrid.CurrentRow.Cells(5).Value() & "\" & rn & ".png")
            snap = (MedExtra & "Snaps\" & MedGuiR.MainGrid.CurrentRow.Cells(5).Value() & "\CRC_Snaps\" & Trim(MedGuiR.MainGrid.CurrentRow.Cells(8).Value()) & ".png")
            title = (MedExtra & "Snaps\" & MedGuiR.MainGrid.CurrentRow.Cells(5).Value() & "\CRC_Titles\" & Trim(MedGuiR.MainGrid.CurrentRow.Cells(8).Value()) & ".png")
            'GC.Collect()
            MedGuiR.PictureBox1.Height = 149
            If File.Exists(pathimage) = True Then
                MedGuiR.PictureBox1.Load(pathimage)
            ElseIf Directory.Exists(MedExtra & "Scraped\" & MedGuiR.MainGrid.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.MainGrid.CurrentRow.Cells(0).Value())) Then
                SearchScrape()
            Else
                EmptyBoxart(MedGuiR.PictureBox1)
            End If

            If File.Exists(snap) = True Then
                MedGuiR.PictureBox1.Height = 97
                MedGuiR.PictureBox5.Load(snap)
            Else
                EmptyBoxart(MedGuiR.PictureBox5)
            End If

            If System.IO.File.Exists(title) = True Then
                MedGuiR.PictureBox1.Height = 97
                MedGuiR.PictureBox4.Load(title)
            Else
                EmptyBoxart(MedGuiR.PictureBox4)
            End If
        Catch ex As Exception
            EmptyBoxart(MedGuiR.PictureBox1)
        End Try
    End Sub

    Private Sub ResizeTextLabel2()
        Try
            MedGuiR.label2index = 0
            Dim StringSize As Size
            StringSize = TextRenderer.MeasureText(MedGuiR.Label47.Text, MedGuiR.Label47.Font)
            If StringSize.Width > 227 Then
                MedGuiR.TimerLabelScroll.Start()
            Else
                MedGuiR.TimerLabelScroll.Stop()
            End If
        Catch
        End Try
    End Sub

    Public Sub DownloadCover()
        ServicePointManager.SecurityProtocol = DirectCast(TypeTls, SecurityProtocolType)

        Dim dimg As Integer
        If File.Exists(pathimage) = False Then
            dimg = 0
        Else
            Dim infoReader As FileInfo
            infoReader = My.Computer.FileSystem.GetFileInfo(pathimage)
            dimg = Val(infoReader.Length)
        End If

        Try
            Dim bugger = (MedGuiR.MainGrid.CurrentRow.Cells(5).Value)
        Catch
            MsgBox("Select a Rom from List", MsgBoxStyle.Exclamation + vbOKOnly)
            Exit Sub
        End Try

        If UpdateServer.StartsWith("https://") Then
            httpGetFileSize()
        Else
            webimagelenght = 1
        End If

        If webimagelenght <= 0 Then
            If Directory.Exists(MedExtra & "Scraped\" & MedGuiR.MainGrid.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.MainGrid.CurrentRow.Cells(0).Value())) Then
                SearchScrape()
            Else
                If MedGuiR.CheckBox2.Checked = False Then
                    EmptyBoxart(MedGuiR.PictureBox1) : MsgBox("No BoxArt Available!", vbOKOnly + vbInformation) : Exit Sub
                End If
            End If
        End If

        Dim cover As String

        If dimg < webimagelenght Then
            Try
                cover = MedGuiR.MainGrid.CurrentRow.Cells(5).Value() & "/" & rn & ".png"

                If UpdateServer.StartsWith("https://") Then
                    My.Computer.Network.DownloadFile(UpdateServer & "/MedGuiR/BoxArt/" & cover, MedExtra & "BoxArt/" & cover, "", "", True, 1000, True)
                ElseIf UpdateServer.StartsWith("ftp://") Then
                    FTPDownloadFile(MedExtra & "BoxArt/" & cover, UpdateServer & "/MedGuiR/BoxArt/" & cover, "anonymous", "anonymous")
                End If
            Catch ex As Exception
                If Directory.Exists(MedExtra & "Scraped\" & MedGuiR.MainGrid.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.MainGrid.CurrentRow.Cells(0).Value())) Then
                    SearchScrape()
                Else
                    If File.Exists(MedExtra & "BoxArt/" & cover) Then File.Delete(MedExtra & "BoxArt/" & cover)
                    EmptyBoxart(MedGuiR.PictureBox1)
                End If
            End Try
            Specific_Info()
        End If
    End Sub

    Public Sub httpGetFileSize()
        ServicePointManager.SecurityProtocol = DirectCast(TypeTls, SecurityProtocolType)

        Try

            Dim myFtpWebRequest As WebRequest
            myFtpWebRequest = WebRequest.Create(UpdateServer & "/MedGuiR/BoxArt/" & MedGuiR.MainGrid.CurrentRow.Cells(5).Value() & "/" & rn & ".png")
            Dim myFtpWebResponse As HttpWebResponse
            myFtpWebResponse = myFtpWebRequest.GetResponse()
            webimagelenght = myFtpWebResponse.ContentLength
            myFtpWebResponse.Close()
        Catch ex As WebException
            If MedGuiR.CheckBox2.Checked = False Then
                If ex.Message.Contains("404") Then
                    MessageBox.Show("File unavailable")
                Else
                    MessageBox.Show(ex.Message)
                End If
                If (ex.Response IsNot Nothing) Then
                    Dim hr As HttpWebResponse = DirectCast(ex.Response, HttpWebResponse)
                End If
            End If
        End Try
    End Sub

    Private Sub SearchScrape()

        For Each foundFile As String In My.Computer.FileSystem.GetFiles(
    MedExtra & "Scraped\" & MedGuiR.MainGrid.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.MainGrid.CurrentRow.Cells(0).Value()))
            If foundFile.Contains("tfront") Then MedGuiR.PictureBox1.Load(foundFile) : pathimage = foundFile : Exit Sub
        Next

        Try
            EmptyBoxart(MedGuiR.PictureBox1)
        Catch
            MedGuiR.PictureBox1.Image = Nothing
        End Try
    End Sub

    Public Function EmptyBoxart(boxart As PictureBox)
        real_name = MedGuiR.MainGrid.CurrentRow.Cells(5).Value()
        detect_icon()
        Dim PathEmptyBox As String = MedExtra & "Resource\Logos\" & gif & ".png"
        If File.Exists(PathEmptyBox) Then
            Select Case gif
                Case "apple2", "cdplay", "fds", "gb", "gba", "gbc", "gg", "nes", "ngp", "ngpc",
                 "pce", "pcfx", "psx", "snes", "wswan", "wswanc" ', "ss"
                    boxart.BackColor = Color.White
                Case Else
                    boxart.BackColor = Color.Black
            End Select
            boxart.Load(PathEmptyBox)
        Else
            boxart.Image = My.Resources.NoPr
        End If
    End Function

    Public Sub psx_version()
        Try
            Select Case True
                Case UCase(rn.Contains("SCUS")), UCase(rn.Contains("SLUS"))
                    'MedGuiR.DataGridView1.CurrentRow.Cells(2).Value() = "(USA)"
                    country = "(USA)"
                Case UCase(rn.Contains("SCES")), UCase(rn.Contains("SLES"))
                    'MedGuiR.DataGridView1.CurrentRow.Cells(2).Value() = "(EUR)"
                    country = "(EUR)"
                Case UCase(rn.Contains("SCPS")), UCase(rn.Contains("SCPM")), UCase(rn.Contains("SLPS")), UCase(rn.Contains("SLPM")), UCase(rn.Contains("SIPS"))
                    'MedGuiR.DataGridView1.CurrentRow.Cells(2).Value() = "(JAP)"
                    country = "(JAP)"
            End Select
        Catch
        End Try
    End Sub

End Module