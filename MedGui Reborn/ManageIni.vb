Imports System.IO

Module ManageIni
    Dim RIni As New Ini
    Dim WIni As New Ini

    'Dim fcvt As System.ComponentModel.TypeConverter = System.ComponentModel.TypeDescriptor.GetConverter(GetType(Font))
    Dim fcvt As New FontConverter

    Public Sub GeneralRMIni()

        Try

            MedGuiR.TextBox4.Text = R_RelPath(RIni.IniRead(MedExtra & "\Mini.ini", "General", "Mednafen_path"))
            Startup_Path = RIni.IniRead(MedExtra & "\Mini.ini", "General", "Startup_Path")
            MedGuiR.ComboBox1.Text = RIni.IniRead(MedExtra & "\Mini.ini", "General", "Dat")

            If (RIni.IniRead(MedExtra & "\Mini.ini", "General", "TextMenu")) = "" Then
                Dim Rtxt = MsgBox("Do you want to start MedGui with the newer text-style top menu or older icon-based menu?" & vbCrLf &
                                  "YES = TEXT MENU (Recommended)" & vbCrLf & "NO = ICON MENU", vbYesNo + MsgBoxStyle.Information, "Select top menu...")
                If Rtxt = MsgBoxResult.No Then
                    MedGuiR.CheckBox23.Checked = False
                Else
                    MedGuiR.CheckBox23.Checked = True
                End If
            Else
                MedGuiR.CheckBox23.CheckState = RIni.IniRead(MedExtra & "\Mini.ini", "General", "TextMenu")
            End If

            'MedGuiR.CheckBox3.CheckState = RIni.IniRead(MedExtra & "\Mini.ini", "General", "Mantain_RomTemp")
            MedGuiR.CheckBox3.CheckState = RIni.IniRead(MedExtra & "\Mini.ini", "General", "PopUP")
            MedGuiR.CheckBox16.CheckState = RIni.IniRead(MedExtra & "\Mini.ini", "General", "Menu_Joypad")
            MedGuiR.ComboBox6.Text = RIni.IniRead(MedExtra & "\Mini.ini", "General", "Joy_Port")
            MedGuiR.TextBox2.Text = RIni.IniRead(MedExtra & "\Mini.ini", "General", "CustomParameter")
            MedGuiR.CheckBox1.CheckState = RIni.IniRead(MedExtra & "\Mini.ini", "General", "Fast_PCE")
            MedGuiR.CheckBox14.CheckState = RIni.IniRead(MedExtra & "\Mini.ini", "General", "Recursive_Scan")
            MedGuiR.CheckBox15.CheckState = RIni.IniRead(MedExtra & "\Mini.ini", "General", "SNES_Faust")
            MedGuiR.CheckBox17.CheckState = RIni.IniRead(MedExtra & "\Mini.ini", "General", "MedBrowser")
            MedGuiR.CheckBox20.CheckState = RIni.IniRead(MedExtra & "\Mini.ini", "General", "AutoUpdate")
            MedGuiR.CheckBox21.CheckState = RIni.IniRead(MedExtra & "\Mini.ini", "General", "AutoScanCD")
            MedGuiR.NetToolStripButton.BackColor = Color.FromName(RIni.IniRead(MedExtra & "\Mini.ini", "General", "AutoConn"))
            MedGuiR.GridToolStripMenuItem.CheckState = RIni.IniRead(MedExtra & "\Mini.ini", "Grid", "Customizable")
            MedGuiR.SetGUI()

            Dim GNA As String = RIni.IniRead(MedExtra & "\Mini.ini", "TGDB API", "NEW_API").Trim
            UpdateServer = RIni.IniRead(MedExtra & "\Mini.ini", "General", "UpdateServer").Trim

            If MedGuiR.NetToolStripButton.BackColor = Color.Red Then
                MedGuiR.AutoConnectToolStripMenuItem.Checked = True
                MedGuiR.ConnectedToolStripMenuItem.BackColor = Color.Green
            Else
                MedGuiR.AutoConnectToolStripMenuItem.Checked = False
                MedGuiR.ConnectedToolStripMenuItem.BackColor = Color.Red
            End If

            If UpdateServer = "" Then
                Test_Server()
            End If

            '//Reference to old legacy TGDBAPI setting
            'If GNA = "" Then
            'GlobalVar.NewAPI = False
            'Else
            'GlobalVar.NewAPI = Val(GNA)
            'End If

            'Dim SizeGui() As String
            'SizeGui = Split(RIni.IniRead(MedExtra & "\Mini.ini", "General", "Gui_Size"), "x")
            'MedGuiR.Size = New System.Drawing.Size(Val(SizeGui(0)), Val(SizeGui(1)))

            forMax = RIni.IniRead(MedExtra & "\Mini.ini", "General", "Form_State")
            MGRH = RIni.IniRead(MedExtra & "\Mini.ini", "General", "MGRH")

            DefBack = Color.FromArgb(RIni.IniRead(MedExtra & "\Mini.ini", "Form Style", "Background_Color"))
            DefFore = Color.FromArgb(RIni.IniRead(MedExtra & "\Mini.ini", "Form Style", "Forecolor_Color"))
        Catch ex As Exception
            MGRWriteLog("ManageIni - GeneralRMini: " & ex.Message)
        Finally
        End Try

    End Sub

    Public Sub UCIRMini()
        Try

            UCInick = RIni.IniRead(MedExtra & "\Mini.ini", "UCI", "UCI_Nick")
            UCIserver = RIni.IniRead(MedExtra & "\Mini.ini", "UCI", "UCI_Server")
            UCIport = RIni.IniRead(MedExtra & "\Mini.ini", "UCI", "UCI_Port")
            UCIchannel = RIni.IniRead(MedExtra & "\Mini.ini", "UCI", "UCI_Channel")
        Catch ex As Exception
            MGRWriteLog("ManageIni - UCI: " & ex.Message)
        Finally
        End Try
    End Sub

    Public Sub NetPlayMini()
        Try

            MedGuiR.ComboBox7.Text = VSTripleDES.DecryptData(RIni.IniRead(MedExtra & "\Mini.ini", "NetPlay", "FTP_Adress"))
            MedGuiR.TextBox25.Text = VSTripleDES.DecryptData(RIni.IniRead(MedExtra & "\Mini.ini", "NetPlay", "Username"))
            MedGuiR.TextBox24.Text = VSTripleDES.DecryptData(RIni.IniRead(MedExtra & "\Mini.ini", "NetPlay", "Password"))
            MedGuiR.TextBox23.Text = VSTripleDES.DecryptData(RIni.IniRead(MedExtra & "\Mini.ini", "NetPlay", "Start_Path"))
            MedGuiR.TextBox21.Text = R_RelPath(RIni.IniRead(MedExtra & "\Mini.ini", "NetPlay", "DownloadedRom"))
            MedGuiR.CheckBox18.CheckState = RIni.IniRead(MedExtra & "\Mini.ini", "NetPlay", "Data_On")

            'Start MedClient
            MedClient.CheckBox4.CheckState = RIni.IniRead(MedExtra & "\Mini.ini", "NetPlay", "Start_MedClient")
            SMedClient = MedClient.CheckBox4.CheckState

            'Set UCI as def client
            D_UCI = RIni.IniRead(MedExtra & "\Mini.ini", "NetPlay", "Use_UCI")

            If D_UCI = True Then
                MedGuiR.CheckBox25.Checked = True
            Else
                MedGuiR.CheckBox25.Checked = False
            End If
        Catch ex As Exception
            MGRWriteLog("ManageIni - NetPlay: " & ex.Message)
        Finally
        End Try
    End Sub

    Public Sub RJoypadMini()
        Try

            JUP = RIni.IniRead(MedExtra & "\Mini.ini", "Joypad Configuration", "UP")
            JDOWN = RIni.IniRead(MedExtra & "\Mini.ini", "Joypad Configuration", "DOWN")
            JLEFT = RIni.IniRead(MedExtra & "\Mini.ini", "Joypad Configuration", "LEFT")
            JRIGHT = RIni.IniRead(MedExtra & "\Mini.ini", "Joypad Configuration", "RIGHT")
            JSELECT = RIni.IniRead(MedExtra & "\Mini.ini", "Joypad Configuration", "SELECT")
            JSTART = RIni.IniRead(MedExtra & "\Mini.ini", "Joypad Configuration", "START")
            JX = RIni.IniRead(MedExtra & "\Mini.ini", "Joypad Configuration", "X")
            JY = RIni.IniRead(MedExtra & "\Mini.ini", "Joypad Configuration", "Y")
            JA = RIni.IniRead(MedExtra & "\Mini.ini", "Joypad Configuration", "A")
            JB = RIni.IniRead(MedExtra & "\Mini.ini", "Joypad Configuration", "B")
            JL = RIni.IniRead(MedExtra & "\Mini.ini", "Joypad Configuration", "L")
            JR = RIni.IniRead(MedExtra & "\Mini.ini", "Joypad Configuration", "R")

            If JUP = "" Then JUP = "0"
            If JDOWN = "" Then JDOWN = "180"
            If JLEFT = "" Then JLEFT = "270"
            If JRIGHT = "" Then JRIGHT = "90"
            If JSELECT = "" Then JSELECT = "64"
            If JSTART = "" Then JSTART = "128"
            If JX = "" Then JX = "4"
            If JY = "" Then JY = "8"
            If JA = "" Then JA = "1"
            If JB = "" Then JB = "2"
            If JL = "" Then JL = "16"
            If JR = "" Then JR = "32"
        Catch ex As Exception
            MGRWriteLog("ManageIni - Joypad Configuration: " & ex.Message)
        Finally
        End Try
    End Sub

    Public Sub GridRMIni()

        Try
            MedGuiR.CheckBox8.CheckState = RIni.IniRead(MedExtra & "\Mini.ini", "Grid", "Resize")
            MedGuiR.CheckBox7.CheckState = RIni.IniRead(MedExtra & "\Mini.ini", "Grid", "Console")
            MedGuiR.CheckBox6.CheckState = RIni.IniRead(MedExtra & "\Mini.ini", "Grid", "Version")
            MedGuiR.CheckBox4.CheckState = RIni.IniRead(MedExtra & "\Mini.ini", "Grid", "Status")
            MedGuiR.CheckBox5.CheckState = RIni.IniRead(MedExtra & "\Mini.ini", "Grid", "System")
            MedGuiR.CheckBox22.CheckState = RIni.IniRead(MedExtra & "\Mini.ini", "Grid", "Fast_Load")
            MedGuiR.ComboBox2.Text = RIni.IniRead(MedExtra & "\Mini.ini", "Grid", "Columns_Order")
        Catch ex As Exception
            MGRWriteLog("ManageIni - GridRMini: " & ex.Message)
        Finally

        End Try

    End Sub

    Public Sub GridRStyle()

        Try

            Dim font1 As Font = CType(fcvt.ConvertFromInvariantString(RIni.IniRead(MedExtra & "\Mini.ini", "Grid Style", "Font_Type")), Font)
            MedGuiR.MainGrid.RowsDefaultCellStyle.Font = font1
            MedGuiR.MainGrid.RowsDefaultCellStyle.ForeColor = Color.FromArgb(RIni.IniRead(MedExtra & "\Mini.ini", "Grid Style", "Normal_Font_Color"))
            MedGuiR.MainGrid.RowsDefaultCellStyle.BackColor = Color.FromArgb(RIni.IniRead(MedExtra & "\Mini.ini", "Grid Style", "Normal_Cell_Color"))
            MedGuiR.MainGrid.RowsDefaultCellStyle.SelectionForeColor = Color.FromArgb(RIni.IniRead(MedExtra & "\Mini.ini", "Grid Style", "Highlight_Font_Color"))
            MedGuiR.MainGrid.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(RIni.IniRead(MedExtra & "\Mini.ini", "Grid Style", "Highlight_Cell_Color"))
            MedGuiR.MainGrid.BackgroundColor = Color.FromArgb(RIni.IniRead(MedExtra & "\Mini.ini", "Grid Style", "Background_Color"))
            MedGuiR.MainGrid.GridColor = Color.FromArgb(RIni.IniRead(MedExtra & "\Mini.ini", "Grid Style", "Grid_Color"))
            MedGuiR.MainGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            MedGuiR.MainGrid.Refresh()

            MedGuiR.FontDialog1.Font = font1
            MedGuiR.FontDialog1.Color = Color.FromArgb(RIni.IniRead(MedExtra & "\Mini.ini", "Grid Style", "Normal_Font_Color"))

            If MedGuiR.CheckBox8.Checked = True Then
                MedGuiR.MainGrid.AutoResizeColumns()
                MedGuiR.ResizeGrid()
            End If
        Catch ex As Exception
            MGRWriteLog("ManageIni - GridRStyle: " & ex.Message)
        Finally

        End Try

    End Sub

    Public Sub DirectoryRMIni()

        Try

            Dim test As String = R_RelPath(RIni.IniRead(MedExtra & "\Mini.ini", "Game Directory", "Default"))
            MedGuiR.TextBox9.Text = R_RelPath(RIni.IniRead(MedExtra & "\Mini.ini", "Game Directory", "Default"))
            If MedGuiR.TextBox9.Text.Trim = "" Or Directory.Exists(MedGuiR.TextBox9.Text.Trim) = False Then
                If Directory.Exists(Path.Combine(Application.StartupPath, "ROMS")) = False Then
                    My.Computer.FileSystem.CreateDirectory(Path.Combine(Application.StartupPath, "ROMS"))
                End If
                MedGuiR.TextBox9.Text = Path.Combine(Application.StartupPath, "ROMS")
            End If
            MedGuiR.TextBox22.Text = R_RelPath(RIni.IniRead(MedExtra & "\Mini.ini", "Game Directory", "Apple2"))
            MedGuiR.TextBox8.Text = R_RelPath(RIni.IniRead(MedExtra & "\Mini.ini", "Game Directory", "Lynx"))
            MedGuiR.TextBox7.Text = R_RelPath(RIni.IniRead(MedExtra & "\Mini.ini", "Game Directory", "GameBoy"))
            MedGuiR.TextBox5.Text = R_RelPath(RIni.IniRead(MedExtra & "\Mini.ini", "Game Directory", "GameBoyAdvance"))
            MedGuiR.TextBox6.Text = R_RelPath(RIni.IniRead(MedExtra & "\Mini.ini", "Game Directory", "NeoGeoPocket"))
            MedGuiR.TextBox11.Text = R_RelPath(RIni.IniRead(MedExtra & "\Mini.ini", "Game Directory", "Famicom"))
            MedGuiR.TextBox10.Text = R_RelPath(RIni.IniRead(MedExtra & "\Mini.ini", "Game Directory", "PCEngine"))
            MedGuiR.TextBox15.Text = R_RelPath(RIni.IniRead(MedExtra & "\Mini.ini", "Game Directory", "PCFX"))
            MedGuiR.TextBox14.Text = R_RelPath(RIni.IniRead(MedExtra & "\Mini.ini", "Game Directory", "GameGear"))
            MedGuiR.TextBox13.Text = R_RelPath(RIni.IniRead(MedExtra & "\Mini.ini", "Game Directory", "Megadrive"))
            MedGuiR.TextBox19.Text = R_RelPath(RIni.IniRead(MedExtra & "\Mini.ini", "Game Directory", "MasterSystem"))
            MedGuiR.TextBox18.Text = R_RelPath(RIni.IniRead(MedExtra & "\Mini.ini", "Game Directory", "Playstation"))
            MedGuiR.TextBox17.Text = R_RelPath(RIni.IniRead(MedExtra & "\Mini.ini", "Game Directory", "SNES"))
            MedGuiR.TextBox20.Text = R_RelPath(RIni.IniRead(MedExtra & "\Mini.ini", "Game Directory", "Saturn"))
            MedGuiR.TextBox12.Text = R_RelPath(RIni.IniRead(MedExtra & "\Mini.ini", "Game Directory", "VirtualBoy"))
            MedGuiR.TextBox16.Text = R_RelPath(RIni.IniRead(MedExtra & "\Mini.ini", "Game Directory", "WonderSwan"))
        Catch ex As Exception
            MGRWriteLog("ManageIni - DirectoryRMini: " & ex.Message)
        Finally
        End Try

    End Sub

    Public Sub RWIni()

        Try
            WIni.IniWrite(MedExtra & "\Mini.ini", "General", "Mednafen_path", W_RelPath(MedGuiR.TextBox4.Text))
            If type_csv = "fav" Then
                WIni.IniWrite(MedExtra & "\Mini.ini", "General", "Startup_Path", "fav")
            Else
                WIni.IniWrite(MedExtra & "\Mini.ini", "General", "Startup_Path", Startup_Path)
            End If
            WIni.IniWrite(MedExtra & "\Mini.ini", "General", "Dat", MedGuiR.ComboBox1.Text)
            'WIni.IniWrite(MedExtra & "\Mini.ini", "General", "Mantain_RomTemp", MedGuiR.CheckBox3.CheckState)
            WIni.IniWrite(MedExtra & "\Mini.ini", "General", "PopUp", MedGuiR.CheckBox3.CheckState)
            WIni.IniWrite(MedExtra & "\Mini.ini", "General", "CustomParameter", MedGuiR.TextBox2.Text)
            WIni.IniWrite(MedExtra & "\Mini.ini", "General", "Fast_PCE", MedGuiR.CheckBox1.CheckState)
            WIni.IniWrite(MedExtra & "\Mini.ini", "General", "Recursive_Scan", MedGuiR.CheckBox14.CheckState)
            WIni.IniWrite(MedExtra & "\Mini.ini", "General", "Menu_Joypad", MedGuiR.CheckBox16.CheckState)
            WIni.IniWrite(MedExtra & "\Mini.ini", "General", "Joy_Port", MedGuiR.ComboBox6.Text)
            WIni.IniWrite(MedExtra & "\Mini.ini", "General", "SNES_Faust", MedGuiR.CheckBox15.CheckState)
            WIni.IniWrite(MedExtra & "\Mini.ini", "General", "MedBrowser", MedGuiR.CheckBox17.CheckState)
            WIni.IniWrite(MedExtra & "\Mini.ini", "General", "AutoUpdate", MedGuiR.CheckBox20.CheckState)
            WIni.IniWrite(MedExtra & "\Mini.ini", "General", "AutoScanCD", MedGuiR.CheckBox21.CheckState)
            WIni.IniWrite(MedExtra & "\Mini.ini", "General", "TextMenu", MedGuiR.CheckBox23.CheckState)
            WIni.IniWrite(MedExtra & "\Mini.ini", "General", "AutoConn", MedGuiR.NetToolStripButton.BackColor.Name)
            WIni.IniWrite(MedExtra & "\Mini.ini", "General", "UpdateServer", UpdateServer)
            'WIni.IniWrite(MedExtra & "\Mini.ini", "General", "Gui_Size", MedGuiR.Size.Width & "x" & MedGuiR.Size.Height)

            If MedGuiR.Width >= Screen.PrimaryScreen.Bounds.Width Then
                forMax = True
            Else
                forMax = False
            End If
            WIni.IniWrite(MedExtra & "\Mini.ini", "General", "Form_State", forMax)
            WIni.IniWrite(MedExtra & "\Mini.ini", "General", "MGRH", MGRH)

            WIni.IniWrite(MedExtra & "\Mini.ini", "NetPlay", "FTP_Adress", VSTripleDES.EncryptData(MedGuiR.ComboBox7.Text))
            WIni.IniWrite(MedExtra & "\Mini.ini", "NetPlay", "Username", VSTripleDES.EncryptData(MedGuiR.TextBox25.Text))
            WIni.IniWrite(MedExtra & "\Mini.ini", "NetPlay", "Password", VSTripleDES.EncryptData(MedGuiR.TextBox24.Text))
            WIni.IniWrite(MedExtra & "\Mini.ini", "NetPlay", "Start_Path", VSTripleDES.EncryptData(MedGuiR.TextBox23.Text))
            WIni.IniWrite(MedExtra & "\Mini.ini", "NetPlay", "DownloadedRom", W_RelPath(MedGuiR.TextBox21.Text))
            WIni.IniWrite(MedExtra & "\Mini.ini", "NetPlay", "Data_On", MedGuiR.CheckBox18.CheckState)
            WIni.IniWrite(MedExtra & "\Mini.ini", "NetPlay", "Use_UCI", MedGuiR.CheckBox25.CheckState)

            WIni.IniWrite(MedExtra & "\Mini.ini", "UCI", "UCI_Nick", UCInick)
            WIni.IniWrite(MedExtra & "\Mini.ini", "UCI", "UCI_Server", UCIserver)
            WIni.IniWrite(MedExtra & "\Mini.ini", "UCI", "UCI_Port", UCIport)
            WIni.IniWrite(MedExtra & "\Mini.ini", "UCI", "UCI_Channel", UCIchannel)

            WIni.IniWrite(MedExtra & "\Mini.ini", "Joypad Configuration", "UP", JUP)
            WIni.IniWrite(MedExtra & "\Mini.ini", "Joypad Configuration", "DOWN", JDOWN)
            WIni.IniWrite(MedExtra & "\Mini.ini", "Joypad Configuration", "LEFT", JLEFT)
            WIni.IniWrite(MedExtra & "\Mini.ini", "Joypad Configuration", "RIGHT", JRIGHT)
            WIni.IniWrite(MedExtra & "\Mini.ini", "Joypad Configuration", "SELECT", JSELECT)
            WIni.IniWrite(MedExtra & "\Mini.ini", "Joypad Configuration", "START", JSTART)
            WIni.IniWrite(MedExtra & "\Mini.ini", "Joypad Configuration", "A", JA)
            WIni.IniWrite(MedExtra & "\Mini.ini", "Joypad Configuration", "B", JB)
            WIni.IniWrite(MedExtra & "\Mini.ini", "Joypad Configuration", "Y", JY)
            WIni.IniWrite(MedExtra & "\Mini.ini", "Joypad Configuration", "X", JX)
            WIni.IniWrite(MedExtra & "\Mini.ini", "Joypad Configuration", "L", JL)
            WIni.IniWrite(MedExtra & "\Mini.ini", "Joypad Configuration", "R", JR)

            WIni.IniWrite(MedExtra & "\Mini.ini", "Grid", "Resize", MedGuiR.CheckBox8.CheckState)
            WIni.IniWrite(MedExtra & "\Mini.ini", "Grid", "Console", MedGuiR.CheckBox7.CheckState)
            WIni.IniWrite(MedExtra & "\Mini.ini", "Grid", "Version", MedGuiR.CheckBox6.CheckState)
            WIni.IniWrite(MedExtra & "\Mini.ini", "Grid", "Status", MedGuiR.CheckBox4.CheckState)
            WIni.IniWrite(MedExtra & "\Mini.ini", "Grid", "System", MedGuiR.CheckBox5.CheckState)
            WIni.IniWrite(MedExtra & "\Mini.ini", "Grid", "Columns_Order", MedGuiR.ComboBox2.Text)
            WIni.IniWrite(MedExtra & "\Mini.ini", "Grid", "Fast_Load", MedGuiR.CheckBox22.CheckState)
            WIni.IniWrite(MedExtra & "\Mini.ini", "Grid", "Customizable", MedGuiR.GridToolStripMenuItem.CheckState)

            If MedGuiR.GridToolStripMenuItem.Checked = True Then
                WIni.IniWrite(MedExtra & "\Mini.ini", "Grid Style", "Font_Type", fcvt.ConvertToInvariantString(MedGuiR.MainGrid.RowsDefaultCellStyle.Font))
                WIni.IniWrite(MedExtra & "\Mini.ini", "Grid Style", "Normal_Font_Color", MedGuiR.MainGrid.RowsDefaultCellStyle.ForeColor.ToArgb)
                WIni.IniWrite(MedExtra & "\Mini.ini", "Grid Style", "Normal_Cell_Color", MedGuiR.MainGrid.RowsDefaultCellStyle.BackColor.ToArgb)
                WIni.IniWrite(MedExtra & "\Mini.ini", "Grid Style", "Highlight_Font_Color", MedGuiR.MainGrid.RowsDefaultCellStyle.SelectionForeColor.ToArgb)
                WIni.IniWrite(MedExtra & "\Mini.ini", "Grid Style", "Highlight_Cell_Color", MedGuiR.MainGrid.RowsDefaultCellStyle.SelectionBackColor.ToArgb)
                WIni.IniWrite(MedExtra & "\Mini.ini", "Grid Style", "Grid_Color", MedGuiR.MainGrid.GridColor.ToArgb)
                WIni.IniWrite(MedExtra & "\Mini.ini", "Grid Style", "Background_Color", MedGuiR.MainGrid.BackgroundColor.ToArgb)
            End If

            WIni.IniWrite(MedExtra & "\Mini.ini", "Form Style", "Background_Color", DefBack.ToArgb)
            WIni.IniWrite(MedExtra & "\Mini.ini", "Form Style", "Forecolor_Color", DefFore.ToArgb)

            WIni.IniWrite(MedExtra & "\Mini.ini", "Game Directory", "Default", W_RelPath(MedGuiR.TextBox9.Text))
            WIni.IniWrite(MedExtra & "\Mini.ini", "Game Directory", "Apple2", W_RelPath(MedGuiR.TextBox22.Text))
            WIni.IniWrite(MedExtra & "\Mini.ini", "Game Directory", "Lynx", W_RelPath(MedGuiR.TextBox8.Text))
            WIni.IniWrite(MedExtra & "\Mini.ini", "Game Directory", "GameBoy", W_RelPath(MedGuiR.TextBox7.Text))
            WIni.IniWrite(MedExtra & "\Mini.ini", "Game Directory", "GameBoyAdvance", W_RelPath(MedGuiR.TextBox5.Text))
            WIni.IniWrite(MedExtra & "\Mini.ini", "Game Directory", "NeoGeoPocket", W_RelPath(MedGuiR.TextBox6.Text))
            WIni.IniWrite(MedExtra & "\Mini.ini", "Game Directory", "Famicom", W_RelPath(MedGuiR.TextBox11.Text))
            WIni.IniWrite(MedExtra & "\Mini.ini", "Game Directory", "PCEngine", W_RelPath(MedGuiR.TextBox10.Text))
            WIni.IniWrite(MedExtra & "\Mini.ini", "Game Directory", "PCFX", W_RelPath(MedGuiR.TextBox15.Text))
            WIni.IniWrite(MedExtra & "\Mini.ini", "Game Directory", "GameGear", W_RelPath(MedGuiR.TextBox14.Text))
            WIni.IniWrite(MedExtra & "\Mini.ini", "Game Directory", "Megadrive", W_RelPath(MedGuiR.TextBox13.Text))
            WIni.IniWrite(MedExtra & "\Mini.ini", "Game Directory", "MasterSystem", W_RelPath(MedGuiR.TextBox19.Text))
            WIni.IniWrite(MedExtra & "\Mini.ini", "Game Directory", "Playstation", W_RelPath(MedGuiR.TextBox18.Text))
            WIni.IniWrite(MedExtra & "\Mini.ini", "Game Directory", "SNES", W_RelPath(MedGuiR.TextBox17.Text))
            WIni.IniWrite(MedExtra & "\Mini.ini", "Game Directory", "Saturn", W_RelPath(MedGuiR.TextBox20.Text))
            WIni.IniWrite(MedExtra & "\Mini.ini", "Game Directory", "VirtualBoy", W_RelPath(MedGuiR.TextBox12.Text))
            WIni.IniWrite(MedExtra & "\Mini.ini", "Game Directory", "WonderSwan", W_RelPath(MedGuiR.TextBox16.Text))
        Catch ex As Exception
            MGRWriteLog("ManageIni - MedGuiR: " & ex.Message)
        End Try
    End Sub

    Public Function W_RelPath(path As String) As String
        If path.StartsWith(Application.StartupPath) Then
            W_RelPath = path.Replace(Application.StartupPath, "..")
        Else
            W_RelPath = path
        End If
    End Function

    Public Function R_RelPath(path As String) As String
        If path.StartsWith("..\") Then
            R_RelPath = path.Replace("..", Application.StartupPath)
        Else
            R_RelPath = path
        End If
    End Function

    Public Sub TGDBIni()
        Try
            WIni.IniWrite(MedExtra & "\Mini.ini", "TGDB API", "NEW_API", TGDBSettings.CheckBox1.CheckState)
        Catch ex As Exception
            MGRWriteLog("ManageIni - TGDB API: " & ex.Message)
        End Try
    End Sub

    Public Sub MedClIniW()
        Try
            WIni.IniWrite(MedExtra & "\Mini.ini", "NetPlay", "Mute_Notify", MedClient.CheckBox3.CheckState)
            WIni.IniWrite(MedExtra & "\Mini.ini", "NetPlay", "Notify_Interval", MedClient.NumericUpDown1.Value)
            WIni.IniWrite(MedExtra & "\Mini.ini", "NetPlay", "Start_MedClient", MedClient.CheckBox4.CheckState)
        Catch ex As Exception
            MGRWriteLog("ManageIni - MedClient: " & ex.Message)
        End Try
    End Sub

    Public Sub RMedClIni()
        Try
            MedClient.CheckBox3.CheckState = RIni.IniRead(MedExtra & "\Mini.ini", "NetPlay", "Mute_Notify")
            MedClient.NumericUpDown1.Value = RIni.IniRead(MedExtra & "\Mini.ini", "NetPlay", "Notify_Interval")
        Catch ex As Exception
            MGRWriteLog("ManageIni - MedClIni: " & ex.Message)
        Finally
        End Try
    End Sub

End Module