Imports System.Net

Module BoxArt
    Dim webimagelenght As Integer
    Public rn, pathimage, snap, title As String

    Public Sub Specific_Info()
        Try
            Dim drom As Integer
            Select Case LCase(Right(MedGuiR.DataGridView1.CurrentRow.Cells(4).Value(), 3))
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
                        If base_file = MedGuiR.DataGridView1.CurrentRow.Cells(8).Value Then
                            drom = ArchiveFileInfo.Size
                        End If
                    Next
                Case "cue"
                    Dim FSI1 As New System.IO.FileInfo(Replace(MedGuiR.DataGridView1.CurrentRow.Cells(4).Value(), System.IO.Path.GetExtension(percorso), ".bin"))
                    If (FSI1.Exists) = True Then drom = FSI1.Length.ToString
                Case "ccd"
                    Dim FSI1 As New System.IO.FileInfo(Replace(MedGuiR.DataGridView1.CurrentRow.Cells(4).Value(), System.IO.Path.GetExtension(percorso), ".img"))
                    If (FSI1.Exists) = True Then drom = FSI1.Length.ToString
                Case Else
                    Dim FSI1 As New System.IO.FileInfo(MedGuiR.DataGridView1.CurrentRow.Cells(4).Value())
                    If (FSI1.Exists) = True Then drom = FSI1.Length.ToString
            End Select

            Dim dimension As Integer, size As String
            dimension = ((Decimal.Round(drom) / 1024) * 8).ToString
            If dimension < 1024 Then size = " Kilobit"
            If dimension >= 1024 Then dimension = (dimension / 1024).ToString : size = " Megabit"

            Select Case LCase(System.IO.Path.GetExtension(percorso))
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
                Case ".cue", ".ccd"
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

            rn = Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value())
            MedGuiR.Label1.Text = "Console: " & vbCrLf & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value()
            'MedGuiR.PictureBox2.Image = New Bitmap(icon_console)
            MedGuiR.Label2.Text = "Game Name: " '& vbCrLf & Replace(cleanpsx(rn), "&", "&&")
            MedGuiR.Label47.Text = Replace(cleanpsx(rn), "&", "&&")
            ResizeTextLabel2()
            If MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() = "Sony PlayStation" Then psx_version()
            'If MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() = "Sega Saturn" Then MedGuiR.DataGridView1.CurrentRow.Cells(2).Value() = "(" & v_ss & ")"
            MedGuiR.Label3.Text = "Version: " & Replace(MedGuiR.DataGridView1.CurrentRow.Cells(2).Value(), ".", "")
            MedGuiR.Label4.Text = "No-Intro Status: " & MedGuiR.DataGridView1.CurrentRow.Cells(3).Value()
            MedGuiR.Label5.Text = "Size: " & dimension & size
            pathimage = (MedExtra & "BoxArt\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & rn & ".png")
            snap = (MedExtra & "Snaps\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\CRC_Snaps\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(8).Value()) & ".png")
            title = (MedExtra & "Snaps\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\CRC_Titles\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(8).Value()) & ".png")
            'GC.Collect()
            MedGuiR.PictureBox1.Height = 149
            If System.IO.File.Exists(pathimage) = True Then
                MedGuiR.PictureBox1.Load(pathimage)
            ElseIf System.IO.Directory.Exists(MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value())) Then
                SearchScrape()
            Else
                MedGuiR.PictureBox1.Load(MedExtra & "BoxArt\NoPr.png")
            End If

            If System.IO.File.Exists(snap) = True Then
                MedGuiR.PictureBox1.Height = 97
                MedGuiR.PictureBox5.Load(snap)
            Else
                MedGuiR.PictureBox5.Load(MedExtra & "BoxArt\NoPr.png")
            End If

            If System.IO.File.Exists(title) = True Then
                MedGuiR.PictureBox1.Height = 97
                MedGuiR.PictureBox4.Load(title)
            Else
                MedGuiR.PictureBox4.Load(MedExtra & "BoxArt\NoPr.png")
            End If
        Catch ex As Exception
            If IO.File.Exists(MedExtra & "BoxArt\NoPr.png") Then
                MedGuiR.PictureBox1.Load(MedExtra & "BoxArt\NoPr.png")
            Else
                MedGuiR.PictureBox1.Image = Nothing
            End If
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
        Dim dimg As Integer
        If IO.File.Exists(pathimage) = False Then
            dimg = 0
        Else
            Dim infoReader As System.IO.FileInfo
            infoReader = My.Computer.FileSystem.GetFileInfo(pathimage)
            dimg = Val(infoReader.Length)
        End If

        Try
            Dim bugger = (MedGuiR.DataGridView1.CurrentRow.Cells(5).Value)
        Catch
            MsgBox("Select a Rom from List", MsgBoxStyle.Exclamation + vbOKOnly)
            Exit Sub
        End Try

        httpGetFileSize()

        If webimagelenght <= 0 Then
            If System.IO.Directory.Exists(MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value())) Then
                SearchScrape()
            Else
                MedGuiR.PictureBox1.Load(MedExtra & "BoxArt\NoPr.png") : MsgBox("No BoxArt Available!", vbOKOnly + vbInformation) : Exit Sub
            End If
        End If

        If dimg < webimagelenght Then
            Try
                Dim cover As String
                cover = "http://medguireborn.000webhostapp.com/MedGuiR/BoxArt/" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "/" & rn & ".png"
                My.Computer.Network.DownloadFile(cover, MedExtra & "BoxArt/" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "/" & rn & ".png", "", "", True, 1000, True)
            Catch ex As Exception
                If System.IO.Directory.Exists(MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value())) Then
                    SearchScrape()
                Else
                    MedGuiR.PictureBox1.Load(MedExtra & "BoxArt\NoPr.png")
                End If
            End Try
            Specific_Info()
        End If
    End Sub

    Public Sub httpGetFileSize()

        Try

            Dim myFtpWebRequest As System.Net.WebRequest
            myFtpWebRequest = System.Net.WebRequest.Create("http://medguireborn.000webhostapp.com/MedGuiR/BoxArt/" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "/" & rn & ".png")
            Dim myFtpWebResponse As HttpWebResponse
            myFtpWebResponse = myFtpWebRequest.GetResponse()
            webimagelenght = myFtpWebResponse.ContentLength
            myFtpWebResponse.Close()
        Catch ex As System.Net.WebException

            MessageBox.Show(ex.Message)
            If (ex.Response IsNot Nothing) Then
                Dim hr As System.Net.HttpWebResponse = DirectCast(ex.Response, System.Net.HttpWebResponse)
            End If

        End Try
    End Sub

    Private Sub SearchScrape()
        Try
            MedGuiR.PictureBox1.Load(MedExtra & "BoxArt\NoPr.png")
        Catch
            MedGuiR.PictureBox1.Image = Nothing
        End Try

        For Each foundFile As String In My.Computer.FileSystem.GetFiles(
    MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()))
            If foundFile.Contains("tfront") Then MedGuiR.PictureBox1.Load(foundFile) : pathimage = foundFile
        Next
    End Sub

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