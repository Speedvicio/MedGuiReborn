Imports System.IO
Imports System.Xml

Module Scrape
    Public SBoxF, SboxR As String, ScrapeForce As Integer
    Dim ConsoleID, TGDB_cleanstring, path_temp As String, ScrapeCount As Integer

    Private Sub GetConsoleID()
        ConsoleID = ""
        Try

            Select Case MedGuiR.DataGridView1.CurrentRow.Cells(5).Value()
                Case "Atari - Lynx"
                    ConsoleID = "Atari Lynx"
                Case "Bandai - WonderSwan Color"
                    ConsoleID = "WonderSwan Color"
                Case "Bandai - WonderSwan"
                    ConsoleID = "WonderSwan"
                Case "PC Engine - TurboGrafx 16"
                    ConsoleID = "TurboGrafx 16"
                Case "TurboGrafx 16 (CD)"
                    ConsoleID = "TurboGrafx CD"
                Case "Nintendo - Famicom Disk System"
                    ConsoleID = "Nintendo Entertainment System (NES)"
                Case "Nintendo - Game Boy Advance"
                    ConsoleID = "Nintendo Game Boy Advance"
                Case "Nintendo - Game Boy Color"
                    ConsoleID = "Nintendo Game Boy Color"
                Case "Nintendo - Game Boy"
                    ConsoleID = "Nintendo Game Boy"
                Case "Super Nintendo Entertainment System"
                    ConsoleID = "Super Nintendo (SNES)"
                Case ("Nintendo - Virtual Boy")
                    ConsoleID = "Nintendo Virtual Boy"
                Case "Nintendo Entertainment System"
                    ConsoleID = "Nintendo Entertainment System (NES)"
                Case "Sega - Game Gear"
                    ConsoleID = "Sega Game Gear"
                Case "Sega - Master System - Mark III"
                    ConsoleID = "Sega Master System"
                Case "Sega - Mega Drive - Genesis"
                    If UCase(MedGuiR.DataGridView1.CurrentRow.Cells(2).Value()).ToString.Contains("(US") Or
    UCase(MedGuiR.DataGridView1.CurrentRow.Cells(2).Value()).ToString.Contains("(JA") Then
                        ConsoleID = "Sega Genesis"
                    Else
                        ConsoleID = "Sega Mega Drive"
                    End If
                Case "SNK - Neo Geo Pocket Color"
                    ConsoleID = "Neo Geo Pocket Color"
                Case "SNK - Neo Geo Pocket"
                    ConsoleID = "Neo Geo Pocket"
                Case "TurboGrafx 16 (CD)"
                    ConsoleID = "TurboGrafx 16"
                Case "PC-FX"
                    ConsoleID = ""
                Case "SegaCD/MegaCD"
                    ConsoleID = "Sega CD"
                Case "Sony PlayStation"
                    ConsoleID = "Sony Playstation"
                Case "Sega Saturn"
                    ConsoleID = "Sega Saturn"
            End Select
        Catch
        End Try

    End Sub

    Public Sub GetParseXML()
        path_temp = ""
        Try
            If MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() = "" Then Exit Sub
        Catch
            Exit Sub
        End Try

        GetConsoleID()

        Try
            If Directory.Exists(MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value())) Then
            Else
                Directory.CreateDirectory(MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()))
            End If

            If Directory.Exists(MedExtra & "Scraped\Temp\") Then
            Else
                Directory.CreateDirectory(MedExtra & "Scraped\Temp\")
            End If

            TGDB_cleanstring = Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value())

            'If ConsoleID = "Sony Playstation" Then cleanstring = Trim(cleanpsx(cleanstring))
            Select Case ConsoleID
                Case "Sega Saturn", "Sony Playstation"
                    TGDB_cleanstring = Trim(cleanpsx(TGDB_cleanstring))
            End Select

            If TGDB_cleanstring.Contains(", The") Then TGDB_cleanstring = Replace(TGDB_cleanstring, ", The", "") : TGDB_cleanstring = "The " & TGDB_cleanstring
            If TGDB_cleanstring.Contains("&") Then TGDB_cleanstring = Replace(TGDB_cleanstring, "&", "%26")
            If TGDB_cleanstring.Contains("+") Then TGDB_cleanstring = Replace(TGDB_cleanstring, "+", "%2B")
            If TGDB_cleanstring.Contains(" - ") Then TGDB_cleanstring = Replace(TGDB_cleanstring, " - ", ": ")

            If ScrapeCount = 6 Then
                If TGDB_cleanstring.Contains("'") Then TGDB_cleanstring = Replace(TGDB_cleanstring, "'", "")
                If TGDB_cleanstring.Contains(".") Then TGDB_cleanstring = Replace(TGDB_cleanstring, ".", "")
                If TGDB_cleanstring.Contains(": ") Then TGDB_cleanstring = Replace(TGDB_cleanstring, ": ", " ")
                If TGDB_cleanstring.Contains(" II ") Then TGDB_cleanstring = Replace(TGDB_cleanstring, "II", "2")
                If TGDB_cleanstring.Contains(" III") Then TGDB_cleanstring = Replace(TGDB_cleanstring, "III", "3")
                ScrapeCount = 1
            End If

            SoxStatus.Text = "Waiting for Scraping..."
            SoxStatus.Label1.Text = "Downloading..."
            SoxStatus.Show()
            SoxStatus.TopMost = True

            Dim search As String
            search = "exactname="

            If File.Exists(MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & ".xml") = False Then
                ScrapeForce = 1
            End If

            If File.Exists(MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & ".xml") And ScrapeForce = 3 Then

                If NewAPI = False Then
                    Dim W As New Net.WebClient
                    W.DownloadFile("http://legacy.thegamesdb.net/api/GetGame.php?" & search & TGDB_cleanstring.ToString & "&platform=" & ConsoleID, MedExtra & "Scraped\Temp\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & ".xml")
                Else
                    path_temp = "Temp\"
                    TheGamesDb_newapi()
                End If
                '<TheGamesDb newapi>
                'MedGuiR.TGDBPlatform()
                'Dim Json1 As String = New Net.WebClient().DownloadString("https://api.thegamesdb.net/Games/ByGameName?apikey=" & VSTripleDES.DecryptData("sCIncJ8wu3H2kmUNaEd4r3oxxsji80o2gVZlp+LKd7Zwp4f4wq6P5f23EaIp9NQFVFwko+jbtvULpqijriaQapiPRCpNGjFCiOlRaxOggKCddRhcmQRC4B3et57yNohlyKuW1s5DvXoVm+iRRO2qEpzO4KnDAmADOxChXfGe7QCInElJHwS+qA==") _
                '   & "&name=" & cleanstring.ToString & "&fields=players%2Cpublishers%2Cgenres%2Coverview%2Ccoop&filter%5Bplatform%5D=" & MedGuiR.tgdbCID & "&include=boxart%2Cplatform")
                'Dim str = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(Json1, "Root")

                'Dim File As StreamWriter
                'File = My.Computer.FileSystem.OpenTextFileWriter(MedExtra & "Scraped\Temp\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & ".xml", False)
                'Dim splitXml As String() = Split(str.OuterXml, "<pages>")
                'File.WriteLine(str.OuterXml.Remove(splitXml(0).Length, str.OuterXml.Length - splitXml(0).Length - 7))
                'File.Close()

                Dim infoReader As FileInfo
                Dim OldXML, NewXML As Integer
                infoReader = My.Computer.FileSystem.GetFileInfo(MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & ".xml")
                OldXML = Val(infoReader.Length)
                infoReader = My.Computer.FileSystem.GetFileInfo(MedExtra & "Scraped\Temp\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & ".xml")
                NewXML = Val(infoReader.Length)

                If OldXML < NewXML Then
                    IO.File.Delete(MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & ".xml")
                    My.Computer.FileSystem.MoveFile(MedExtra & "Scraped\Temp\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & ".xml",
                         MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & ".xml")
                Else
                    IO.File.Delete(MedExtra & "Scraped\Temp\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & ".xml")
                End If
            ElseIf ScrapeForce = 0 Then
            ElseIf ScrapeForce = 1 Then

                If NewAPI = False Then
                    Dim W As New Net.WebClient
                    W.DownloadFile("http://legacy.thegamesdb.net/api/GetGame.php?" & search & TGDB_cleanstring.ToString & "&platform=" & ConsoleID, MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & ".xml")
                Else
                    path_temp = MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\"
                    TheGamesDb_newapi()
                End If
                '<TheGamesDb newapi>
                'MedGuiR.TGDBPlatform()
                'Dim Json1 As String = New Net.WebClient().DownloadString("https://api.thegamesdb.net/Games/ByGameName?apikey=" & VSTripleDES.DecryptData("sCIncJ8wu3H2kmUNaEd4r3oxxsji80o2gVZlp+LKd7Zwp4f4wq6P5f23EaIp9NQFVFwko+jbtvULpqijriaQapiPRCpNGjFCiOlRaxOggKCddRhcmQRC4B3et57yNohlyKuW1s5DvXoVm+iRRO2qEpzO4KnDAmADOxChXfGe7QCInElJHwS+qA==") _
                '   & "&name=" & cleanstring.ToString & "&fields=players%2Cpublishers%2Cgenres%2Coverview%2Ccoop&filter%5Bplatform%5D=" & MedGuiR.tgdbCID & "&include=boxart%2Cplatform")
                'Dim str = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(Json1, "Root")

                'Dim File As StreamWriter
                'File = My.Computer.FileSystem.OpenTextFileWriter(MedExtra & "Scraped\Temp\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & ".xml", False)
                'Dim splitXml As String() = Split(str.OuterXml, "<pages>")
                'File.WriteLine(str.OuterXml.Remove(splitXml(0).Length, str.OuterXml.Length - splitXml(0).Length - 7))
                'File.Close()
            End If

            ReadXml()
        Catch ex As System.Net.WebException
            MessageBox.Show(ex.Message)
            If (ex.Response IsNot Nothing) Then
                Dim hr As System.Net.HttpWebResponse = DirectCast(ex.Response, System.Net.HttpWebResponse)
            End If
            SoxStatus.Close()
        End Try
    End Sub

    Private Sub TheGamesDb_newapi()

        '<TheGamesDb newapi>
        MedGuiR.TGDBPlatform()
        Dim Json1 As String = New Net.WebClient().DownloadString("https://api.thegamesdb.net/Games/ByGameName?apikey=" & VSTripleDES.DecryptData("sCIncJ8wu3H2kmUNaEd4r3oxxsji80o2gVZlp+LKd7Zwp4f4wq6P5f23EaIp9NQFVFwko+jbtvULpqijriaQapiPRCpNGjFCiOlRaxOggKCddRhcmQRC4B3et57yNohlyKuW1s5DvXoVm+iRRO2qEpzO4KnDAmADOxChXfGe7QCInElJHwS+qA==") _
           & "&name=" & TGDB_cleanstring.ToString & "&fields=players%2Cpublishers%2Cgenres%2Coverview%2Ccoop&filter%5Bplatform%5D=" & MedGuiR.tgdbCID & "&include=boxart%2Cplatform")
        Dim str = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(Json1, "Root")

        Dim File As StreamWriter
        File = My.Computer.FileSystem.OpenTextFileWriter(MedExtra & "Scraped\" & path_temp & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & ".xml", False)
        Dim splitXml As String() = Split(str.OuterXml, "<pages>")
        File.WriteLine(str.OuterXml.Remove(splitXml(0).Length, str.OuterXml.Length - splitXml(0).Length - 7))
        File.Close()

    End Sub

    Public Sub ReadXml()
        Dim TGDBXml, BaseUrl, tBack, tFront, fBack, fFront, GameID As String
        Dim counTGDB As Integer
        Dim GameName, ReleaseDate As String
        TGDBXml = MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & ".xml"

        Dim reader As New System.Xml.XmlTextReader(TGDBXml)
        Dim W As New Net.WebClient

        If NewAPI = False Then TheGamesDB.Show()
        TheGamesDB.Label4.Text = "Genre: "
        TheGamesDB.PictureBox1.Image = Nothing
        TheGamesDB.PictureBox2.Image = Nothing

        While reader.Read()
            Dim contents As String
            reader.MoveToContent()

            If reader.NodeType = Xml.XmlNodeType.Element Then
                contents = reader.Name
            End If

            If reader.NodeType = Xml.XmlNodeType.Text Then
                Select Case contents
                    Case "count"
                        counTGDB = Val(reader.Value)
                        If counTGDB = 1 Then
                            TheGamesDB.Show()
                        ElseIf counTGDB > 1 Then
                            TGDBGameSelector.Show()
                            TGDBGameSelector.DataGridView1.Rows.Clear()
                        End If
                    Case "id"
                        GameID = reader.Value.ToString
                    Case "baseImgUrl"
                        BaseUrl = reader.Value
                    Case "GameTitle", "game_title"
                        GameName = Replace(reader.Value, "&", "&&")
                        TheGamesDB.Label1.Text = "Game Title: " & GameName
                    Case "Platform", "platform", "name"
                        TheGamesDB.Label2.Text = "Platform: " & (reader.Value)
                    Case "ReleaseDate", "release_date"
                        Dim fdate As String
                        fdate = Replace(reader.Value, "-", "/")
                        If Len(fdate) = 10 Then fdate = fdate Else fdate = "0" & fdate
                        If Len(fdate) = 4 Then
                            TheGamesDB.Label3.Text = "Release Date: " & (fdate)
                        Else
                            Try
                                Dim formatDate() = {"dd/MM/yyyy", "MM/dd/yyyy", "yyyy/MM/dd"}
                                Dim expenddt As Date
                                expenddt = Date.ParseExact(fdate, formatDate, Globalization.DateTimeFormatInfo.InvariantInfo,
    Globalization.DateTimeStyles.None)
                                fdate = expenddt.ToString("dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture)
                            Catch
                            Finally
                                ReleaseDate = fdate
                                TheGamesDB.Label3.Text = "Release Date: " & (fdate)
                            End Try

                            'If counTGDB > 1 Then
                            TGDBGameSelector.DataGridView1.Rows.Add(GameID, GameName, ReleaseDate)
                            TGDBGameSelector.DataGridView1.Sort(TGDBGameSelector.DataGridView1.Columns(1), System.ComponentModel.ListSortDirection.Ascending)
                            'End If
                        End If
                    Case "Overview", "overview"
                        TheGamesDB.RichTextBox1.Text = (reader.Value)
                    Case "genre", "genres"
                        If Len(TheGamesDB.Label4.Text) <= 7 Then
                            TheGamesDB.Label4.Text = "Genre: " & (reader.Value)
                        Else
                            TheGamesDB.Label4.Text = TheGamesDB.Label4.Text & " - " & (reader.Value)
                        End If
                    Case "Players", "players"
                        TheGamesDB.Label11.Text = "Players: " & (reader.Value)
                    Case "Publisher", "publishers"
                        TheGamesDB.Label5.Text = "Publisher: " & (reader.Value)
                    Case "Developer", "developers"
                        TheGamesDB.Label6.Text = "Developer: " & (reader.Value)
                    Case "Co-op", "coop"
                        TheGamesDB.Label7.Text = "Co-op: " & (reader.Value)
                    Case "boxart"

                        If reader.Value.Contains("boxart/original/back/") Then
                            fBack = reader.Value
                            Dim SIF As String = MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & "\back_" & Path.GetFileName(fBack)

                            If File.Exists(SIF) Then
                            ElseIf ScrapeForce > 0 Then
                                W.DownloadFile(BaseUrl & fBack, SIF)
                            End If

                        ElseIf reader.Value.Contains("boxart/original/front/") Then
                            fFront = reader.Value
                            Dim SIF As String = MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & "\front_" & Path.GetFileName(fFront)

                            If File.Exists(SIF) Then
                            ElseIf ScrapeForce > 0 Then
                                W.DownloadFile(BaseUrl & fFront, SIF)
                            End If

                        End If

                End Select

                contents = ""
            End If

            If reader.HasAttributes Then 'If attributes exist
                While reader.MoveToNextAttribute()
                    Dim AtName As String = reader.LocalName
                    'Display attribute name and value.
                    If AtName = "thumb" Then
                        Select Case True
                            Case reader.Value.Contains("boxart/thumb/original/back/")
                                tBack = reader.Value
                                Dim SIF As String = MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & "\tback_" & Path.GetFileName(tBack)

                                If File.Exists(SIF) Then
                                ElseIf ScrapeForce > 0 Then
                                    W.DownloadFile(BaseUrl & tBack, SIF)
                                End If

                                SboxR = (MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & "\tback_" & Path.GetFileName(tBack))

                                Try
                                    TheGamesDB.PictureBox2.Load(SboxR)
                                Catch
                                End Try

                            Case reader.Value.Contains("boxart/thumb/original/front/")
                                tFront = reader.Value
                                Dim SIF As String = MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & "\tfront_" & Path.GetFileName(tFront)

                                If File.Exists(SIF) Then
                                ElseIf ScrapeForce > 0 Then
                                    W.DownloadFile(BaseUrl & tFront, SIF)
                                End If

                                SBoxF = (MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & "\tfront_" & Path.GetFileName(tFront))

                                Try
                                    TheGamesDB.PictureBox1.Load(SBoxF)
                                Catch
                                End Try

                                If File.Exists(MedExtra & "BoxArt\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & rn & ".png") = False Then MedGuiR.PictureBox1.Load(SBoxF) : pathimage = SBoxF

                        End Select
                    End If
                    AtName = ""
                End While
            End If
            ScrapeCount = ScrapeCount + 1
        End While
        reader.Close()

        TheGamesDB.Focus()

        If ScrapeCount = 6 Then
            GetParseXML()
        ElseIf ScrapeCount = 7 Then
            TheGamesDB.Visible = False
            MsgBox("No TheGamesDB compatible rom name or info not Available", vbOKOnly + vbInformation)
            TheGamesDB.Close()
            Try
                File.Delete(MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & ".xml")
                Directory.Delete(MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()))
            Catch
            End Try
        End If
        SoxStatus.Close()
        ScrapeCount = 0
    End Sub

    Public Function cleanpsx(ByVal cleanstring As String) As String
        Dim i1, i2 As Integer

        i1 = cleanstring.IndexOf("[")
        i2 = cleanstring.IndexOf("]")
        While i1 >= 0 And i2 >= 0
            cleanstring = cleanstring.Remove(i1, i2 - i1 + 1)
            i1 = cleanstring.IndexOf("[")
            i2 = cleanstring.IndexOf("]")
        End While
        cleanpsx = cleanstring
    End Function

    Public Function RemoveAmpersand(ByVal CleanAmp As String) As String
        RemoveAmpersand = Replace(CleanAmp, " &amp; ", " & ")
    End Function

    Public Function AddAmpersand(ByVal AddAmp As String) As String
        AddAmpersand = Replace(AddAmp, " & ", " %26 ")
    End Function

End Module