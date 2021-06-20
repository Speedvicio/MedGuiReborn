Imports System.IO
Imports System.Net

Module Scrape
    Public SBoxF, SboxR As String, ScrapeForce, xmlAttemp As Integer
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
                Case ("Virtual Boy")
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

        If NewAPI = True Then
            xmlAttemp = 8
        Else
            xmlAttemp = 0
        End If

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
            If TGDB_cleanstring.Contains(" ~ ") Then TGDB_cleanstring = Replace(TGDB_cleanstring, " ~ ", "")

            If ScrapeCount = 6 + xmlAttemp Then
                If TGDB_cleanstring.Contains("'") Then TGDB_cleanstring = Replace(TGDB_cleanstring, "'", "")
                If TGDB_cleanstring.Contains(".") Then TGDB_cleanstring = Replace(TGDB_cleanstring, ".", "")
                If TGDB_cleanstring.Contains(": ") Then TGDB_cleanstring = Replace(TGDB_cleanstring, ": ", " ")
                If TGDB_cleanstring.Contains(" II ") Then TGDB_cleanstring = Replace(TGDB_cleanstring, "II", "2")
                If TGDB_cleanstring.Contains(" III") Then TGDB_cleanstring = Replace(TGDB_cleanstring, "III", "3")
                ScrapeCount = 1
            End If

            SoxStatus.Text = "Waiting for Scraping..."
            SoxStatus.Label1.Text = "Downloading..."
            Application.DoEvents()
            SoxStatus.Show()
            'SoxStatus.TopMost = True

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

                Dim infoReader As FileInfo
                Dim OldXML, NewXML As Integer
                Dim DateXML As Date
                infoReader = My.Computer.FileSystem.GetFileInfo(MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & ".xml")
                OldXML = Val(infoReader.Length)
                DateXML = infoReader.CreationTime.ToShortDateString
                infoReader = My.Computer.FileSystem.GetFileInfo(MedExtra & "Scraped\Temp\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & ".xml")
                NewXML = Val(infoReader.Length)

                If OldXML < NewXML Or DateTime.Compare(DateXML, "01/01/2019") < 0 Then
                    File.Delete(MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & ".xml")
                    My.Computer.FileSystem.MoveFile(MedExtra & "Scraped\Temp\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & ".xml",
                         MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & ".xml")
                Else
                    File.Delete(MedExtra & "Scraped\Temp\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & ".xml")
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
            End If

            ReadXml()
        Catch ex As WebException
            If ex.Message.ToString.Contains("403") Then
                MsgBox("You have exceded per month, per ip limit of 1000 request" & vbCrLf &
                                "Try the next month or swap to old TGDB API.", vbInformation + vbOKOnly, "TGDB request limit exceded...")
            End If
            If (ex.Response IsNot Nothing) Then
                Dim hr As HttpWebResponse = DirectCast(ex.Response, HttpWebResponse)
            End If
            SoxStatus.Close()
        End Try
        ReleaseMemory()
    End Sub

    Private Sub TheGamesDb_newapi()
        ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
        '<TheGamesDb newapi>
        MedGuiR.TGDBPlatform()
        Dim Json1 As String = New Net.WebClient().DownloadString("https://api.thegamesdb.net/v1/Games/ByGameName?apikey=" & VSTripleDES.DecryptData("sCIncJ8wu3H2kmUNaEd4r3oxxsji80o2gVZlp+LKd7Zwp4f4wq6P5f23EaIp9NQFVFwko+jbtvULpqijriaQapiPRCpNGjFCiOlRaxOggKCddRhcmQRC4B3et57yNohlyKuW1s5DvXoVm+iRRO2qEpzO4KnDAmADOxChXfGe7QCInElJHwS+qA==") _
           & "&name=" & TGDB_cleanstring.ToString & "&fields=players%2Cpublishers%2Cgenres%2Coverview%2Ccoop&filter%5Bplatform%5D=" & MedGuiR.tgdbCID & "&include=boxart%2Cplatform")
        Threading.Thread.Sleep(1000)
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
        Dim GameName, ReleaseDate, SystemConsole As String
        TGDBXml = MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & ".xml"

        Using reader As New System.Xml.XmlTextReader(TGDBXml)
            Dim W As New Net.WebClient

            TheGamesDB.Show()
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
                            If counTGDB > 1 Then
                                TheGamesDB.Close()
                                TGDBGameSelector.Show()
                                TGDBGameSelector.DataGridView1.Rows.Clear()
                            End If
                        Case "id"
                            If counTGDB > 1 Then GameID = reader.Value.ToString
                        Case "baseImgUrl"
                            BaseUrl = reader.Value
                        Case "original"
                            If counTGDB = 1 Then BaseUrl = reader.Value
                        Case "GameTitle", "game_title"
                            GameName = Replace(reader.Value, "&", "&&")
                            TheGamesDB.Label1.Text = "Game Title: " & GameName
                        Case "Platform", "platform", "name"
                            SystemConsole = ""
                            If counTGDB > 1 And contents = "platform" Then
                                SystemConsole = ReadTGDBList("Platforms", reader.Value.Trim)

                                'If counTGDB > 1 Then
                                TGDBGameSelector.DataGridView1.Rows.Add(GameID, GameName, SystemConsole, ReleaseDate)
                                TGDBGameSelector.DataGridView1.Sort(TGDBGameSelector.DataGridView1.Columns(1), System.ComponentModel.ListSortDirection.Ascending)
                                'End If
                            End If
                            If IsNumeric(reader.Value) Then TheGamesDB.LinkLabel1.Tag = reader.Value
                            TheGamesDB.LinkLabel1.Text = (reader.Value)
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
                            End If
                        Case "Overview", "overview"
                            TheGamesDB.RichTextBox1.Text = (reader.Value)
                        Case "genre", "genres"
                            Dim result As String = ""
                            If counTGDB = 1 Then
                                result = ReadTGDBList("Genres", reader.Value.Trim)
                            Else
                                result = reader.Value
                            End If

                            If Len(TheGamesDB.Label4.Text) <= 7 Then
                                TheGamesDB.Label4.Text = "Genre: " & (result)
                            Else
                                TheGamesDB.Label4.Text = TheGamesDB.Label4.Text & " - " & (result)
                            End If
                        Case "Players", "players"
                            TheGamesDB.Label11.Text = "Players: " & (reader.Value)
                        Case "Publisher", "publishers"
                            Dim result As String = ""
                            If counTGDB = 1 Then
                                If IsNumeric(reader.Value) Then TheGamesDB.LinkLabel2.Tag = reader.Value
                                result = ReadTGDBList("Publishers", reader.Value.Trim)
                            Else
                                result = reader.Value
                            End If
                            TheGamesDB.LinkLabel2.Text = (result)
                        Case "Developer", "developers"
                            Dim result As String = ""
                            If counTGDB = 1 Then
                                If IsNumeric(reader.Value) Then TheGamesDB.LinkLabel3.Tag = reader.Value
                                result = ReadTGDBList("Developers", reader.Value.Trim)
                            Else
                                result = reader.Value
                            End If
                            TheGamesDB.LinkLabel3.Text = (result)
                        Case "Co-op", "coop"
                            TheGamesDB.Label7.Text = "Co-op: " & (reader.Value)
                        Case "boxart"

                            If reader.Value.Contains("boxart/original/back/") Then
                                fBack = reader.Value
                                Dim SIF As String = MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & "\back_" & Path.GetFileName(fBack)

                                If ScrapeForce > 0 Or File.Exists(SIF) = False Then
                                    W.DownloadFile(BaseUrl & fBack, SIF)
                                End If

                            ElseIf reader.Value.Contains("boxart/original/front/") Then
                                fFront = reader.Value
                                Dim SIF As String = MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & "\front_" & Path.GetFileName(fFront)

                                If ScrapeForce > 0 Or File.Exists(SIF) = False Then
                                    W.DownloadFile(BaseUrl & fFront, SIF)
                                End If

                            End If
                        Case "filename"
                            Dim SIF As String
                            If counTGDB = 1 Then
                                If reader.Value.Contains("boxart/back/") Then
                                    fBack = reader.Value
                                    SIF = MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & "\back_" & Path.GetFileName(fBack)

                                    If ScrapeForce > 0 Or File.Exists(SIF) = False Then
                                        W.DownloadFile(BaseUrl & fBack, SIF)
                                    End If

                                    'thumb
                                    tBack = reader.Value
                                    SIF = MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & "\tback_" & Path.GetFileName(tBack)

                                    If ScrapeForce > 0 Or File.Exists(SIF) = False Then
                                        W.DownloadFile(Replace(BaseUrl, "original", "thumb") & tBack, SIF)
                                    End If

                                    SboxR = (MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & "\tback_" & Path.GetFileName(tBack))

                                    Try
                                        TheGamesDB.PictureBox2.Load(SboxR)
                                    Catch
                                        SoxStatus.Close()
                                    End Try

                                ElseIf reader.Value.Contains("boxart/front/") Then
                                    fFront = reader.Value
                                    SIF = MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & "\front_" & Path.GetFileName(fFront)

                                    If ScrapeForce > 0 Or File.Exists(SIF) = False Then
                                        W.DownloadFile(BaseUrl & fFront, SIF)
                                    End If

                                    'thumb
                                    tFront = reader.Value
                                    SIF = MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & "\tfront_" & Path.GetFileName(tFront)

                                    If ScrapeForce > 0 Or File.Exists(SIF) = False Then
                                        W.DownloadFile(Replace(BaseUrl, "original", "thumb") & tFront, SIF)
                                    End If

                                    SBoxF = (MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & "\tfront_" & Path.GetFileName(tFront))

                                    Try
                                        TheGamesDB.PictureBox1.Load(SBoxF)
                                    Catch
                                        SoxStatus.Close()
                                    End Try

                                    If File.Exists(MedExtra & "BoxArt\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & rn & ".png") = False Then
                                        If File.Exists(SBoxF) Then
                                            MedGuiR.PictureBox1.BackColor = DefBack
                                            MedGuiR.PictureBox1.Load(SBoxF)
                                            pathimage = SBoxF
                                        End If
                                    End If
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

                                    If ScrapeForce > 0 Or File.Exists(SIF) = False Then
                                        W.DownloadFile(BaseUrl & tBack, SIF)
                                    End If

                                    SboxR = (MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & "\tback_" & Path.GetFileName(tBack))

                                    Try
                                        TheGamesDB.PictureBox2.Load(SboxR)
                                    Catch
                                        SoxStatus.Close()
                                    End Try

                                Case reader.Value.Contains("boxart/thumb/original/front/")
                                    tFront = reader.Value
                                    Dim SIF As String = MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & "\tfront_" & Path.GetFileName(tFront)

                                    If ScrapeForce > 0 Or File.Exists(SIF) = False Then
                                        W.DownloadFile(BaseUrl & tFront, SIF)
                                    End If

                                    SBoxF = (MedExtra & "Scraped\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.DataGridView1.CurrentRow.Cells(0).Value()) & "\tfront_" & Path.GetFileName(tFront))

                                    Try
                                        TheGamesDB.PictureBox1.Load(SBoxF)
                                    Catch
                                        SoxStatus.Close()
                                    End Try

                                    If File.Exists(MedExtra & "BoxArt\" & MedGuiR.DataGridView1.CurrentRow.Cells(5).Value() & "\" & rn & ".png") = False Then
                                        If File.Exists(SBoxF) Then
                                            MedGuiR.PictureBox1.BackColor = DefBack
                                            MedGuiR.PictureBox1.Load(SBoxF)
                                            pathimage = SBoxF
                                        End If
                                    End If

                            End Select
                        End If
                        AtName = ""
                    End While
                End If
                ScrapeCount = ScrapeCount + 1
            End While
            reader.Close()

        End Using

        TheGamesDB.Focus()

        If ScrapeCount = 6 + xmlAttemp Then
            GetParseXML()
        ElseIf ScrapeCount = 7 + xmlAttemp Then
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

        For i = 0 To 10
            If cleanstring.Contains(" CD" & i) Then
                cleanstring = Replace(cleanstring, " CD" & i, "")
                Exit For
            End If
        Next

        If cleanstring.Contains(", The") Then cleanstring = Replace(cleanstring, ", The", "") : cleanstring = "The " & cleanstring

        cleanpsx = cleanstring
    End Function

    Friend Function CleanRom(Gname As String)
        Dim index As Integer

        If Gname.Contains("(") Or Gname.Contains("[") Then
            Gname = Path.GetFileNameWithoutExtension(Gname)
        End If

        If Gname.Contains("(") Then
            index = Gname.IndexOf("(")
            Gname = Gname.Remove(index - 1)
        End If

        If Gname.Contains("[") Then
            index = Gname.IndexOf("[")
            Gname = Gname.Remove(index - 1)
        End If

        Return Gname.Trim
    End Function

    Public Function RemoveAmpersand(ByVal CleanAmp As String) As String
        RemoveAmpersand = Replace(CleanAmp, " &amp; ", " & ")
    End Function

    Public Function AddAmpersand(ByVal AddAmp As String) As String
        AddAmpersand = Replace(AddAmp, " & ", " %26 ")
    End Function

    Public Function ReadTGDBList(ByVal TypeList As String, id As String)
        Dim oFile As System.IO.File
        Dim oRead As System.IO.StreamReader

        Try
#Disable Warning BC42025 ' L'accesso del membro condiviso, del membro costante, del membro di enumerazione o del tipo nidificato verrà effettuato tramite un'istanza. L'espressione di qualificazione non verrà valutata.
            oRead = oFile.OpenText(MedExtra & "\Plugins\db\TGDB\" & TypeList & ".txt")
#Enable Warning BC42025 ' L'accesso del membro condiviso, del membro costante, del membro di enumerazione o del tipo nidificato verrà effettuato tramite un'istanza. L'espressione di qualificazione non verrà valutata.
            Dim splitoread() As String
            While oRead.Peek <> -1
                splitoread = Split(oRead.ReadLine(), " | ")
                Select Case splitoread(0)
                    Case id
                        Return (splitoread(1).Trim)
                        Exit While
                End Select
            End While
            oRead.Close()
        Catch ex As Exception
            Return ("")
        End Try
#Disable Warning BC42105 ' La funzione 'ReadTGDBList' non restituisce un valore in tutti i percorsi del codice. È possibile che in fase di esecuzione venga restituita un'eccezione dovuta a un riferimento Null quando viene usato il risultato.
    End Function

#Enable Warning BC42105 ' La funzione 'ReadTGDBList' non restituisce un valore in tutti i percorsi del codice. È possibile che in fase di esecuzione venga restituita un'eccezione dovuta a un riferimento Null quando viene usato il risultato.

End Module