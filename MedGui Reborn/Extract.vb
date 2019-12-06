Imports System.IO
Imports SevenZip

Module Extract
    Public SevenZCounter As Integer
    Public c_os, sevenzdll As String
    Public MedExtra, T_MedExtra, TempFolder, decrunch_size, o_ext As String, stopzip, checkpismo As Boolean

    Public Sub extract_7z()
        'Call contr_os()
        'Dim szip As SevenZipExtractor = New SevenZipExtractor(percorso)

        Dim dimarch As New System.IO.FileInfo(percorso)
        Console.WriteLine(dimarch.Exists)
        Dim dimension As Integer
        dimension = (Decimal.Round(dimarch.Length.ToString) / (1024 * 1024))
        Dim msgdim As String

        If dimension > 100 Then
            msgdim = MsgBox("The selected filer are " & dimension & " MB, the extraction might take a long time" & vbCrLf &
"Do you want to continue the operation?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation)
            If msgdim = vbNo Then
                stopscan = True
                MedGuiR.TextBox1.Text = ""
                Exit Sub
            End If
        End If

        DecompressArchive(percorso, MedExtra & "RomTemp")

        'szip.ExtractArchive(MedExtra & "RomTemp")
        'SoxStatus.Text = "Waiting for extraction..."
        'SoxStatus.Label1.Text = "..."
        'SoxStatus.Show()
        'szip.Dispose()
        'SoxStatus.Close()

        'TempFolder = "RomTemp"
        T_MedExtra = MedExtra
        scansiona()

    End Sub

    Public Sub contr_os()

        Dim arch = System.Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE", EnvironmentVariableTarget.Machine)
        If arch = "AMD64" Then
            c_os = "64"
            'sevenzdll = "7z64.dll"
            sevenzdll = "7z.dll"
        Else
            c_os = "32"
            sevenzdll = "7z.dll"
        End If

        'If IntPtr.Size = 8 Then
        'c_os = "64"
        'sevenzdll = "7z64.dll"
        'ElseIf IntPtr.Size = 4 Then
        'c_os = "32"
        'sevenzdll = "7z.dll"
        'End If

        SevenZipExtractor.SetLibraryPath(MedExtra & "Plugins\" & sevenzdll)
    End Sub

    Public Sub ex_Server()
        Dim p_serv As String
        If c_os = "32" And IO.File.Exists(MedExtra & "NetPlay\" & "cyggcc_s-1.dll") = False Then
            p_serv = MedExtra & "NetPlay\" & "server_32.zip"
        ElseIf c_os = "64" And IO.File.Exists(MedExtra & "NetPlay\" & "cyggcc_s-seh-1.dll") = False Then
            p_serv = MedExtra & "NetPlay\" & "server_64.zip"
        Else
            Exit Sub
        End If

        DecompressArchive(p_serv, MedExtra & "NetPlay")

        'Dim szip As SevenZipExtractor = New SevenZipExtractor(p_serv)
        'szip.ExtractArchive(MedExtra & "NetPlay")
        'SoxStatus.Text = "Waiting for extraction..."
        'SoxStatus.Label1.Text = "..."
        'SoxStatus.Show()
        'szip.Dispose()
        'SoxStatus.Close()
    End Sub

    Public Sub ClearFile()
        'If MedGuiR.CheckBox3.Checked = True Then Exit Sub

        Dim fil, files() As String
        Dim f As FileInfo

        Try
            files = Directory.GetFiles(MedExtra & "RomTemp")
            For Each fil In files
                f = New FileInfo(fil)
                f.Delete()
            Next
            'If Dir(MedExtra & "RomTemp") <> "" Then
            If (System.IO.Directory.GetDirectories(MedExtra & "RomTemp").Length) > 0 Then
                'My.Computer.FileSystem.DeleteDirectory(MedExtra & "RomTemp/", FileIO.DeleteDirectoryOption.DeleteAllContents, FileIO.RecycleOption.DeletePermanently)
                System.IO.Directory.Delete(MedExtra & "RomTemp/", True)
            End If

            Shell("pfm unmount", AppWinStyle.Hide, True)
        Catch ex As Exception
            If Dir(MedExtra & "RomTemp\*.*") <> "" Then
                MsgBox("File " & fil & " is die hard, please remove it manually or delete RomTemp folder.", MsgBoxStyle.OkCancel + MsgBoxStyle.Critical)
                Process.Start(MedExtra & "RomTemp")
                End
            End If
        End Try

    End Sub

    Public Sub scan_compressed()
        'stopscan = True
        'Call contr_os()
        o_ext = ext
        scan_ext_compressed()
        'estensione()
        'LMain()

    End Sub

    Public Sub scan_ext_compressed()
        SevenZipExtractor.SetLibraryPath(MedExtra & "Plugins\" & sevenzdll)
        Try
            Dim szip As SevenZipExtractor = New SevenZipExtractor(percorso)
            'If szip.ArchiveFileData.Count > 1 And SevenZCounter = 0 And stopiso = False Then
            'ClearFile()
            'extract_7z()
            'szip.Dispose()
            'SevenZCounter = 1
            'MedGuiR.SY.Text = ""
            'Exit Sub
            'End If

            fileTXT = ""
            For Each ArchiveFileInfo In szip.ArchiveFileData
                If ArchiveFileInfo.IsDirectory Then
                    If checkpismo = False Then
                        consoles = ""
                        ext = ""
                    Else
                        If LCase(Path.GetExtension(percorso)) = ".zip" Then
                            MountPismo()
                            RecuScan()
                        End If
                    End If
                    Continue For
                End If
                ext = LCase(Path.GetExtension(ArchiveFileInfo.FileName))
                If ext = "" Or ext Is Nothing Then
                    Continue For
                Else
                    If Path.GetDirectoryName(ArchiveFileInfo.FileName) <> "" Then
                        Continue For
                    End If
                End If
                Select Case LCase(ext)
                    Case ".iso", ".m3u", ".toc", ".cue", ".ccd"
                        If checkpismo = False Then
                            consoles = ""
                            ext = ""
                            Exit Sub
                        Else
                            If LCase(Path.GetExtension(percorso)) = ".zip" Then
                                MountPismo()
                                RecuScan()
                                Exit Sub
                            End If
                        End If
                    Case ".ecm", ".pbp", ".zip", ".rar", ".7z"
                        consoles = ""
                        ext = ""
                        Exit Sub
                    Case ".bin", ".img"
                        If ArchiveFileInfo.Size > 10485760 Then

                            If checkpismo = False Then
                                consoles = ""
                                ext = ""
                                Exit Sub
                            Else
                                If LCase(Path.GetExtension(percorso)) = ".zip" Then
                                    MountPismo()
                                    RecuScan()
                                    Exit Sub
                                End If
                            End If
                        Else
                            fileTXT = ""
                        End If
                    Case ".mai"
                        consoles = "apple2"
                        LMain()
                        Exit Sub
                    Case Else
                        fileTXT = ""
                End Select
                'Dim o
                'Dim oi
                'o = Right(ArchiveFileInfo.FileName, 4)
                'oi = o.indexof(".")
                'If oi >= 0 Then
                'ext = (o.substring(oi))
                'End If
                base_file = Hex(ArchiveFileInfo.Crc)

                Select Case Trim(Len(base_file))
                    Case 6
                        base_file = "00" & base_file.Trim
                    Case 7
                        base_file = "0" & base_file.Trim
                End Select

                If stopzip = True Then Exit For

                decrunch_size = ArchiveFileInfo.Size
                'szip.Dispose()
                romname = Path.GetFileNameWithoutExtension(ArchiveFileInfo.FileName)
                LMain()
            Next
            szip.Dispose()
        Catch ex As Exception
            If ext = ".rar" Then
                MsgBox("This file could be compressed in RAR5 format." &
                       vbCrLf & "SevenZipSharp supports until RAR4 compression", vbOKOnly + vbCritical, "SevenZipSharp error..")
            Else
                MsgBox(ex.Message.ToString, vbOKOnly + vbCritical, "SevenZipSharp error..")
            End If
        End Try
    End Sub

    Public Sub simple_extract()

        'MedGuiR.CheckBox3.Checked = False
        ClearFile()

        Dim szip As SevenZipExtractor = New SevenZipExtractor(percorso)

        Select Case LCase(MedGuiR.DataGridView1.CurrentRow.Cells(7).Value)
            Case ".psf", ".minipsf", ".minigsf", ".ssf", ".minissf"
                MedGuiR.DataGridView1.Rows.Clear()
                extract_7z()
                szip.Dispose()
                MedGuiR.Datagrid_filter()
            Case ".rsn"
                'If LCase(MedGuiR.DataGridView1.CurrentRow.Cells(7).Value) = ".rsn" Then

                DecompressArchive(percorso, MedExtra & "RomTemp")

                'szip.ExtractArchive(MedExtra & "RomTemp")
                'SoxStatus.Text = "Waiting for extraction..."
                'SoxStatus.Label1.Text = "..."
                'SoxStatus.Show()
                'szip.Dispose()
                'SoxStatus.Close()
            Case Else

                For Each ArchiveFileInfo In szip.ArchiveFileData
                    base_file = Hex(ArchiveFileInfo.Crc)

                    Select Case Trim(Len(base_file))
                        Case 6
                            base_file = "00" & base_file.Trim
                        Case 7
                            base_file = "0" & base_file.Trim
                    End Select

                    If base_file = MedGuiR.DataGridView1.CurrentRow.Cells(8).Value Then
                        Dim fs As FileStream = File.OpenWrite(Path.Combine(MedExtra & "RomTemp\", ArchiveFileInfo.FileName))
                        szip.ExtractFile(ArchiveFileInfo.FileName, fs)
                        fs.Close()
                        Exit For
                    End If
                Next

                szip.Dispose()
                Dim fileEntries As String() = Directory.GetFiles(MedExtra & "RomTemp")
                For Each fileName As String In fileEntries
                    percorso = fileName
                Next
        End Select 'If

    End Sub

    Public Function DecompressArchive(archive As String, final_path As String)
        SevenZipExtractor.SetLibraryPath(MedExtra & "Plugins\7z.dll")
        Dim szip As SevenZipExtractor = New SevenZipExtractor(archive)
        SoxStatus.Text = "Waiting for extraction..."
        SoxStatus.Label1.Text = "..."
        SoxStatus.Show()

        szip.ExtractArchive(final_path)

        szip.Dispose()
        SoxStatus.Close()
    End Function

    Public Sub Pismo()

        If Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Classes\PismoFileMount", "pfmapi", Nothing) Is Nothing Then
            checkpismo = False
        Else
            checkpismo = True
        End If

    End Sub

    Public Sub MountPismo()
        'checkpismo = False
        Shell("pfm unmount", AppWinStyle.Hide, True)
        Shell("pfm mount -i " & Chr(34) & percorso & Chr(34), AppWinStyle.Hide, True)
        Dim cleanpath As String = System.Text.RegularExpressions.Regex.Replace(Path.GetFileName(percorso), "[^0-9a-zA-Z-._ ]+", "")
        TempFolder = Path.Combine("C:\Volumes", cleanpath)
        T_MedExtra = Nothing
    End Sub

End Module