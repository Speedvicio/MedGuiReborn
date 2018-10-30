Imports System.IO
Imports DiscTools
Imports SevenZip

Module Sbi
    Dim patchname As String

    Public Sub Sbi_Scan()

        If File.Exists(Replace(percorso, Path.GetExtension(percorso), "") & ".sbi") Then Exit Sub

        If File.Exists(MedExtra & "Plugins\db\Sbi_List.txt") = False And My.Computer.Network.IsAvailable = True Then
            My.Computer.Network.DownloadFile(UpdateServer & "/MedGuiR/Sbi_List.txt", MedExtra & "Plugins\db\Sbi_List.txt", "anonymous", "anonymous", True, 500, True)
        ElseIf File.Exists(MedExtra & "Plugins\db\Sbi_List.txt") = False And My.Computer.Network.IsAvailable = False Then
            Exit Sub
        End If

        Try
            Dim oRead As StreamReader
            Dim CDinspector = DiscInspector.ScanPSX(percorso)
            Serial_PSX = CDinspector.Data.SerialNumber

            oRead = File.OpenText(MedExtra & "Plugins\db\Sbi_List.txt")
            While oRead.Peek <> -1
                'If percorso.Contains(oRead.ReadLine()) Then
                'Or percorso.Contains(oRead.ReadLine())
                Dim ComparedSerial As String = oRead.ReadLine().Trim
                If Serial_PSX.Contains(ComparedSerial) Then
                    patchname = ComparedSerial

                    Dim mx As String = MsgBox("Your PSX game needs a LaserLock patch to work properly" & vbCrLf &
                                            "Do you want to download and/or apply it?", vbYesNo + vbInformation)

                    If mx = vbYes Then get_SbiPatch() 'Else Exit Sub
                    Exit While
                End If

            End While

            oRead.Dispose()
            oRead.Close()
        Catch ex As Exception
            MGRWriteLog("Sbi - Sbi_Scan:" & ex.Message)
        End Try

    End Sub

    Public Sub get_SbiPatch()
        Try
            If Directory.Exists(MedExtra & "Patch\Psx\Sbi") Then
            Else
                Directory.CreateDirectory(MedExtra & "Patch\Psx\Sbi")
            End If

            If File.Exists(MedExtra & "Patch\Psx\Sbi\" & patchname & ".7z") Then GoTo PatchSkip

            My.Computer.Network.DownloadFile(UpdateServer & "/MedGuiR/Patch/Psx/Sbi/" & "[" & patchname & "].7z", MedExtra & "Patch\Psx\Sbi\" & patchname & ".7z", "anonymous", "anonymous", True, 500, True)

            Dim infoReader As FileInfo
            infoReader = My.Computer.FileSystem.GetFileInfo(MedExtra & "Patch\Psx\Sbi\" & patchname & ".7z")

            If Dir(MedExtra & "Patch\Psx\Sbi\" & patchname & ".7z") = "" Or infoReader.Length < 100 Then
                MsgBox("No " & patchname & " patch found, please try later.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Download error")
                System.IO.File.Delete(MedExtra & "Patch\Psx\Sbi\" & patchname & ".7z")
                Exit Sub
            End If

PatchSkip:

            'Call contr_os()
            SevenZipExtractor.SetLibraryPath(MedExtra & "Plugins\" & sevenzdll)
            Dim szip As SevenZipExtractor = New SevenZipExtractor(MedExtra & "Patch\Psx\Sbi\" & patchname & ".7z")
            szip.ExtractArchive(MedExtra & "Patch\Psx\Sbi\")
            szip.Dispose()

            For Each foundFile As String In My.Computer.FileSystem.GetFiles(
                MedExtra & "Patch\Psx\Sbi\", FileIO.SearchOption.SearchTopLevelOnly, "*.sbi")
                My.Computer.FileSystem.MoveFile(foundFile, Replace(percorso, Path.GetExtension(percorso), "") & ".sbi", True)
                Exit For
            Next

            MsgBox("All done, patch applied!", vbOKOnly + vbInformation)
        Catch exio As IOException
            MGRWriteLog("Sbi - get_SbiPatch:" & exio.Message)
        Catch ex As Exception
            MGRWriteLog("Sbi - get_SbiPatch:" & ex.Message)
        End Try

    End Sub

    Public Sub GBAMemory()
        Try
            Dim tmem, tsize, trtc, esav As String
            Dim fmem As Boolean = False
            esav = MedGuiR.TextBox4.Text & "\sav\" & Path.GetFileNameWithoutExtension(MedGuiR.DataGridView1.CurrentRow.Cells(4).Value()) & ".type"
            If File.Exists(esav) Then Exit Sub

            If File.Exists(MedExtra & "Plugins\db\GBAmemtype.txt") = False And My.Computer.Network.IsAvailable = True Then
                My.Computer.Network.DownloadFile(UpdateServer & "/MedGuiR/GBAmemtype.txt", MedExtra & "Plugins\db\GBAmemtype.txt", "anonymous", "anonymous", True, 500, True)
            ElseIf File.Exists(MedExtra & "Plugins\db\GBAmemtype.txt") = False And My.Computer.Network.IsAvailable = False Then
                Exit Sub
            End If

            If MedGuiR.DataGridView1.CurrentRow.Cells(4).Value().Contains("Pokemon -") Then
                tmem = "flash"
                tsize = "128"
                trtc = "rtc"
                fmem = True
            Else
                Dim oRead As StreamReader
                Try

                    oRead = File.OpenText(MedExtra & "Plugins\db\GBAmemtype.txt")
                    While oRead.Peek <> -1
                        Dim LineMem = oRead.ReadLine
                        If LineMem.Contains(MedGuiR.DataGridView1.CurrentRow.Cells(8).Value()) Then
                            fmem = True

                            Dim splitsave() As String = LineMem.Split("=")
                            For Each substring In splitsave
                                Select Case substring
                                    Case "flash", "eeprom", "sram", "sensor"
                                        tmem = substring.Trim
                                    Case "32", "64", "128", "256"
                                        tsize = substring.Trim
                                    Case "rtc"
                                        trtc = "rtc"
                                End Select
                            Next

                        End If
                    End While

                    oRead.Dispose()
                    oRead.Close()
                Catch ex As Exception
                    MGRWriteLog("Sbi - GBAMemory:" & ex.Message)
                Finally
                    oRead.Dispose()
                    oRead.Close()
                End Try
            End If

            If fmem = True Then
                MsgBox("This GBA game require a special memory backup" & vbCrLf &
                       "I will make one for you", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "Special memory backup...")
                Dim SGBA As StreamWriter
                SGBA = File.CreateText(esav)
                SGBA.WriteLine(tmem & " " & tsize)
                If trtc <> "" Then SGBA.WriteLine("rtc")
                SGBA.Flush()
                SGBA.Close()
                'MsgBox("GBA memory backup Created!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            End If
        Catch
        End Try
    End Sub

End Module