Module Link
    Public _link As String

    Public Sub open_link()
        Try
            If MedGuiR.CheckBox17.Checked = False Then
                Process.Start(_link)
            Else
                MedBrowser.Show()
                MedBrowser.WebBrowser1.Navigate(_link)
            End If
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Exclamation, "Error")
            'MsgBox("No Internet connection Available", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Public Sub select_link()
        Select Case MedGuiR.WS.Text
            Case "Cdromance"
                _link = "https://cdromance.com/?s=" & MedGuiR.TextBox35.Text
            Case "CoolROM"
                _link = "https://coolrom.com/search?q=" & MedGuiR.TextBox35.Text & "&submit"
            Case "DOMS"
                _link = "https://www.downloadroms.io/search/?q=" & MedGuiR.TextBox35.Text
            Case "EagleForces"
                _link = "https://eagleforces.tistory.com/search/" & MedGuiR.TextBox35.Text
            Case "Edge Emulation"
                _link = "https://edgeemu.net/results.php?q=" & MedGuiR.TextBox35.Text & "&system=all"
            Case "EMUPARADISE"
                _link = "https://www.emuparadise.org/roms/search.php?query=" & MedGuiR.TextBox35.Text
            Case "Free-ISO"
                _link = "https://free-iso.org/search/?q=" & MedGuiR.TextBox35.Text & "+&t=0#"
            Case "GamulatoR"
                _link = "https://www.gamulator.com/search?search_term_string=" & MedGuiR.TextBox35.Text
            Case "Planet Emu"
                _link = "https://www.planetemu.net/?section=recherche&recherche=" & MedGuiR.TextBox35.Text & "&type=Tous%20les%20mots&rubrique=roms"
            Case "RETRO!"
                _link = "https://www.retrostic.com/search?search_term_string=" & MedGuiR.TextBox35.Text
            Case "Rom Find"
                _link = "https://www.romfind.com/game_search?q=" & MedGuiR.TextBox35.Text
            Case "Rom Hustler"
                _link = "https://romhustler.net/roms/search/?q=" & MedGuiR.TextBox35.Text
            Case "ROMULATION"
                _link = "https://www.romulation.net/roms/search?query=" & MedGuiR.TextBox35.Text
            Case "Viim's Lair"
                _link = "https://vimm.net/vault/?p=list&q=" & MedGuiR.TextBox35.Text & "&submitButton=Go"
            Case "WoWroMs"
                _link = "https://wowroms.com/en/roms/list?search=" & MedGuiR.TextBox35.Text
            Case "Mednafen Bios"
                '_link = "ftp://anonymous@speedvicio.ddns.net/update/firmware.zip"
                DownExtractBios()
            Case "MedGui BoxArt Pack"
                _link = "https://mega.nz/folder/ZiwVSb5K#Gb0UE3Gh3bfQAJV7wXAQ2A"
            Case "MedGui Snaps Pack"
                _link = "https://mega.nz/folder/57x1nLDa#sRBobh_R0g2P7I__KeG5ig"
            Case "Hacks", "Translations"
                SelectHack()
                'Case "Test"
                '_link = "ftp://anonymous@speedvicio.ddns.net/update/Test/test.rar"
        End Select
    End Sub

    Private Sub SelectHack()
        Try
            Dim webSystem As String
            Select Case MedGuiR.MainGrid.CurrentRow.Cells(5).Value()
                Case "Bandai - WonderSwan", "Bandai - WonderSwan Color"
                    webSystem = "21"
                Case "SNK - Neo Geo Pocket", "SNK - Neo Geo Pocket Color"
                    webSystem = "16"
                Case "Nintendo - Game Boy Advance"
                    webSystem = "10"
                Case "Nintendo Entertainment System"
                    webSystem = "1"
                Case "Nintendo - Game Boy", "Nintendo - Game Boy Color"
                    webSystem = "8"
                Case "Virtual Boy"
                    webSystem = "32"
                Case "Sega - Game Gear"
                    webSystem = "12"
                Case "Sega - Master System - Mark III"
                    webSystem = "22"
                Case "Sega - Mega Drive - Genesis"
                    webSystem = "11"
                Case "Super Nintendo Entertainment System"
                    webSystem = "9"
                Case "Sega Saturn"
                    webSystem = "13"
                Case "Nintendo - Famicom Disk System"
                    webSystem = "7"
                Case "PC Engine - TurboGrafx 16"
                    webSystem = "45"
                Case "PC-FX"
                    webSystem = "6"
                Case "Sony PlayStation"
                    webSystem = "17"
                Case Else
                    webSystem = ""
            End Select

            If MedGuiR.CheckBox10.Checked = False Then webSystem = ""
            If MedGuiR.WS.Text = "Hacks" Then
                _link = "https://www.romhacking.net/?page=" & LCase(MedGuiR.WS.Text) & "&category=&platform=" & webSystem & "&game=&perpage=20&order=&title=" & MedGuiR.TextBox35.Text & "&dir=&hacksearch=Go"
            ElseIf MedGuiR.WS.Text = "Translations" Then
                _link = "https://www.romhacking.net/?page=" & LCase(MedGuiR.WS.Text) & "&status=&platform=" & webSystem & "&languageid=&order=&perpage=20&dir=&title=" & MedGuiR.TextBox35.Text & "&transsearch=Go"
            End If
        Catch
        End Try
    End Sub

    Public Sub DownExtractBios()
        Try
            If UpdateServer = "" Then Test_Server()

            If UpdateServer.StartsWith("https://") Then
                My.Computer.Network.DownloadFile(UpdateServer & "/MedGuiR/firmware.zip", MedExtra & "Update\firmware.zip", "anonymous", "anonymous", True, 1000, True)
            ElseIf UpdateServer.StartsWith("ftp://") Then
                FTPDownloadFile(MedExtra & "Update\firmware.zip", UpdateServer & "/MedGuiR/firmware.zip", "anonymous", "anonymous")
            End If

            'SevenZip.SevenZipExtractor.SetLibraryPath(MedExtra & "Plugins\7z.dll")
            'Dim szip As SevenZip.SevenZipExtractor = New SevenZip.SevenZipExtractor(MedExtra & "Update\firmware.zip")
            'szip.ExtractArchive(MedGuiR.TextBox4.Text)
            'SoxStatus.Text = "Waiting for extraction..."
            'SoxStatus.Label1.Text = "..."
            'SoxStatus.Show()

            DecompressArchive(MedExtra & "Update\firmware.zip", MedGuiR.TextBox4.Text)

            Threading.Thread.Sleep(1000)
            'szip.Dispose()
            'SoxStatus.Close()

            IO.File.Delete(MedExtra & "Update\firmware.zip")
            MsgBox("Firmware extracted on Default Mednafen path!", vbOKOnly + MsgBoxStyle.Information)
        Catch
            MsgBox("unexpected error while Download/extract", vbOKOnly + MsgBoxStyle.Critical)
            SoxStatus.Close()
        End Try
    End Sub

End Module