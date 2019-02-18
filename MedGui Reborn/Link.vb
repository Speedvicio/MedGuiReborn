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
            MsgBox("No Internet connection Available", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Public Sub select_link()
        Select Case MedGuiR.WS.Text
            Case "Cdromance"
                _link = "https://cdromance.com/?s=" & MedGuiR.TextBox35.Text
            Case "CoolROM"
                _link = "http://coolrom.com/search?q=" & MedGuiR.TextBox35.Text & "&submit"
            Case "DOPEROMS"
                _link = "http://www.doperoms.com/search.php?s=" & MedGuiR.TextBox35.Text & "&method=ROM"
            Case "EagleForce"
                _link = "http://eagleforces.tistory.com/search/" & MedGuiR.TextBox35.Text
            Case "EMUPARADISE"
                _link = "http://www.emuparadise.org/roms/search.php?query=" & MedGuiR.TextBox35.Text
            Case "Free-ISO"
                _link = "http://free-iso.org/search/?q=" & MedGuiR.TextBox35.Text & "+&t=0#"
            Case "NITROROMS"
                _link = "http://nitroroms.com/search/All/" & MedGuiR.TextBox35.Text & "/page-1"
            Case "Planet Emu"
                _link = "http://www.planetemu.net/?section=recherche&recherche=" & MedGuiR.TextBox35.Text & "&type=Tous%20les%20mots&rubrique=roms"
            Case "Rom Find"
                _link = "http://www.romfind.com/game_search?q=" & MedGuiR.TextBox35.Text
            Case "ROM World"
                _link = "http://www.rom-world.com/search/?q=" & MedGuiR.TextBox35.Text & "&submit"
            Case "Rom Hustler"
                _link = "http://romhustler.net/roms/search/?q=" & MedGuiR.TextBox35.Text
            Case "ROMULATION"
                _link = "https://www.romulation.net/roms/search?query=" & MedGuiR.TextBox35.Text
            Case "Roms Universe"
                _link = "http://www.romsuniverse.com/search.php?q=" & MedGuiR.TextBox35.Text
            Case "RomsMode"
                _link = "http://romsmode.com/search?name=" & MedGuiR.TextBox35.Text
            Case "SnesOrama"
                _link = "http://snesorama.us/ROMS/?s=%2A&q=" & MedGuiR.TextBox35.Text
            Case "Viim's Lair"
                _link = "http://vimm.net/vault/?p=list&search=" & MedGuiR.TextBox35.Text & "&submitButton=Go"
            Case "WoWroMs"
                _link = "https://wowroms.com/en/roms/list?search=" & MedGuiR.TextBox35.Text
            Case "Mednafen Bios"
                '_link = "ftp://anonymous@speedvicio.ddns.net/update/firmware.zip"
                DownExtractBios()
            Case "MedGui BoxArt Pack"
                _link = "https://mega.nz/#!Z2IiWBhC!wovHeRzvXLe8OtFTnIODf4nOxPmKevq0pwdPffGFJlA"
            Case "MedGui Snaps Pack"
                _link = "http://www16.zippyshare.com/v/WTvJcbYe/file.html"
            Case "Hacks", "Translations"
                SelectHack()
                'Case "Test"
                '_link = "ftp://anonymous@speedvicio.ddns.net/update/Test/test.rar"
        End Select
    End Sub

    Private Sub SelectHack()
        Try
            Dim webSystem As String
            Select Case MedGuiR.DataGridView1.CurrentRow.Cells(5).Value()
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
            _link = "http://www.romhacking.net/?page=" & LCase(MedGuiR.WS.Text) & "&genre=&platform=" & webSystem & "&status=&languageid=12&perpage=20&title=" & MedGuiR.TextBox35.Text & "&author=&transsearch=Go"
        Catch
        End Try
    End Sub

    Public Sub DownExtractBios()
        Try
            If UpdateServer = "" Then Test_Server()
            My.Computer.Network.DownloadFile(UpdateServer & "/MedGuiR/firmware.zip", MedExtra & "Update\firmware.zip", "anonymous", "anonymous", True, 1000, True)
            SevenZip.SevenZipExtractor.SetLibraryPath(MedExtra & "Plugins\7z.dll")
            Dim szip As SevenZip.SevenZipExtractor = New SevenZip.SevenZipExtractor(MedExtra & "Update\firmware.zip")
            szip.ExtractArchive(MedGuiR.TextBox4.Text)
            Threading.Thread.Sleep(1000)
            IO.File.Delete(MedExtra & "Update\firmware.zip")
            MsgBox("Firmware extracted on Mednafen path!", vbOKOnly + MsgBoxStyle.Information)
        Catch
        End Try
    End Sub

End Module