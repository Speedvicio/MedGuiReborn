Imports System.IO
Imports SevenZip

Module Updater

    Public Sub get_update()

        If Directory.Exists(MedExtra & "Update") = False Then Directory.CreateDirectory(MedExtra & "Update\")

        If File.Exists(MedExtra & "Update\update.txt") Then File.Delete(MedExtra & "Update\update.txt")
        My.Computer.Network.DownloadFile(UpdateServer & "/MedGuiR/update.txt", MedExtra & "Update\update.txt", "anonymous", "anonymous", True, 500, False)
        Dim upd As New StreamReader(MedExtra & "Update\update.txt")

        Try
            Dim Med_old, Med_new As String

            Do
                Med_new = upd.ReadLine
                Med_old = Replace(MedGuiR.Label6.Text, "MedGuiR v.", "")

                If Med_new Is Nothing Or Len(Med_new) > 5 Then
                    MsgBox("Unable to detect/retrieve updated version." & vbCrLf &
                           "Please try again later", vbOKOnly + MsgBoxStyle.Critical, "Unable to update...")
                    upd.Close()
                    If File.Exists(MedExtra & "Update\update.txt") Then File.Delete(MedExtra & "Update\update.txt")
                    Exit Do
                End If

                Dim force As Integer
                If MedGuiR.CheckBox9.Checked = True Then
                    force = 1
                Else
                    force = 0
                End If

                Dim upd_mr As String
                If (Val(Med_old) - force) < Val(Med_new) Then
                    upd_mr = MsgBox("MedGui Reborn v" & Med_new & " is Available to download." & vbCrLf &
                           "Do you want download it?", MsgBoxStyle.YesNo + MsgBoxStyle.Information)

                    If upd_mr = vbYes Then
                        My.Computer.Network.DownloadFile(UpdateServer & "/MedGuiR/MedGuiR_v" & Med_new & ".zip", MedExtra & "Update\MedGuiR.zip", "anonymous", "anonymous", True, 500, True)

                        'Call contr_os()
                        SevenZipExtractor.SetLibraryPath(MedExtra & "Plugins\" & sevenzdll)
                        Dim szip As SevenZipExtractor = New SevenZipExtractor(MedExtra & "Update\MedGuiR.zip")
                        szip.ExtractArchive(MedExtra & "Update")

                        Dim StartTime As Date = Now
                        Do
                            Application.DoEvents()
                        Loop Until (Now - StartTime).TotalMilliseconds > 2000
                        szip.Dispose()

                        OwMedinstR()

                        File.Delete(MedExtra & "Update\MedGuiR.zip")
                        Process.Start(Application.StartupPath & "\MedInstR.exe")

                    ElseIf upd_mr = vbNo Then
                        upd.Close()
                        If File.Exists(MedExtra & "Update\update.txt") Then File.Delete(MedExtra & "Update\update.txt")
                        Exit Sub
                    End If

                ElseIf Val(Med_old) >= Val(Med_new) And MedGuiR.AutoUp = False Then
                    MsgBox("Congratulations, your MedGui Reborn version is up to date!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
                Else
                End If

                upd.Close()
                If File.Exists(MedExtra & "Update\update.txt") Then File.Delete(MedExtra & "Update\update.txt")

            Loop Until Med_new Is Nothing
        Catch exio As IOException
            MGRWriteLog("Updater - get_update: " & exio.Message)
            upd.Close()
        Catch ex As Exception
            MGRWriteLog("Updater - get_update: " & ex.Message)
            upd.Close()
        End Try

    End Sub

    Public Sub get_Datupdate()
        Try
            If Directory.Exists(MedExtra & "Update") Then
            Else
                Directory.CreateDirectory(MedExtra & "Update\")
            End If

            My.Computer.Network.DownloadFile(UpdateServer & "/MedGuiR/DATs.zip", MedExtra & "Update\DATs.zip", "anonymous", "anonymous", True, 500, True)

            Dim infoReader As System.IO.FileInfo
            infoReader = My.Computer.FileSystem.GetFileInfo(MedExtra & "Update\DATs.zip")

            If Dir(MedExtra & "Update\DATs.zip") = "" Or infoReader.Length < 1500000 Then
                MsgBox("No DATs found, please try later.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Update error")
                System.IO.File.Delete(MedExtra & "Update\DATs.zip")
                Exit Sub
            End If

            'Call contr_os()
            SevenZipExtractor.SetLibraryPath(MedExtra & "Plugins\" & sevenzdll)
            Dim szip As SevenZipExtractor = New SevenZipExtractor(MedExtra & "Update\DATs.zip")
            szip.ExtractArchive(MedExtra)
            szip.Dispose()

            System.IO.File.Delete(MedExtra & "Update\DATs.zip")

            MsgBox("All rom database updated!", vbOKOnly + MsgBoxStyle.Information, "DATs updated...")

            If MedGuiR.Text = "MedGui Reborn - Recent Roms" Or MedGuiR.Text = "MedGui Reborn - Favorites Roms" Or MedGuiR.SY.Text.Trim = "" Then
            Else
                MedGuiR.ScanFolder()
            End If
        Catch exio As IOException
            MGRWriteLog("Updater - get_Datupdate: " & exio.Message)
        Catch ex As Exception
            MGRWriteLog("Updater - get_Datupdate: " & ex.Message)
        End Try

    End Sub

    Public Sub get_Modsupdate()

        Try
            If Directory.Exists(MedExtra & "Update") Then
            Else
                Directory.CreateDirectory(MedExtra & "Update\")
            End If

            My.Computer.Network.DownloadFile(ModLand.ModServer & "/allmods.zip", MedExtra & "Update\allmods.zip", "", "", True, 500, True)

            Dim infoReader As System.IO.FileInfo
            infoReader = My.Computer.FileSystem.GetFileInfo(MedExtra & "Update\allmods.zip")

            If Dir(MedExtra & "Update\allmods.zip") = "" Or infoReader.Length < 500 Then
                MsgBox("No ModLand Database found, please try later.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Update error")
                System.IO.File.Delete(MedExtra & "Update\allmods.zip")
                Exit Sub
            End If

            'Call contr_os()
            SevenZipExtractor.SetLibraryPath(MedExtra & "Plugins\" & sevenzdll)
            Dim szip As SevenZipExtractor = New SevenZipExtractor(MedExtra & "Update\allmods.zip")
            szip.ExtractArchive(MedExtra & "Plugins\db\")
            szip.Dispose()

            System.IO.File.Delete(MedExtra & "Update\allmods.zip")
            MsgBox("ModLand DATs Upfated!")
        Catch exio As IOException
            MGRWriteLog("Updater - get_Datupdate: " & exio.Message)
        Catch ex As Exception
            MGRWriteLog("Updater - get_Datupdate: " & ex.Message)
        End Try

    End Sub

    Private Sub OwMedinstR()
        For Each foundFile As String In My.Computer.FileSystem.GetFiles(MedExtra & "Update\")
            If foundFile.Contains("MedInstR") Then
                My.Computer.FileSystem.MoveFile(foundFile, Application.StartupPath & "\MedInstR.exe", True)
                System.IO.File.Delete(foundFile)
                Exit Sub
            End If
        Next
    End Sub

End Module