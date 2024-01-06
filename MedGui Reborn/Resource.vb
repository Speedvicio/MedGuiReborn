Module Resource

    Private Function Find_Bitmap(ByVal path As String) As Bitmap
        If IO.File.Exists(path) Then
            Return New Bitmap(path)
        Else
            MissingResource(path)
            Throw New Exception("Missing essential resource")
            Return Nothing
        End If
    End Function

    Private AlreadyReadResources As Boolean = False
    Private AlreadyReadResourcesReturnValue As Boolean = False

    Public Function Read_Resource() As Boolean
        If AlreadyReadResources Then Return AlreadyReadResourcesReturnValue

        Try

            'For Each ctrl As Control In Me.Controls = True
            'Dim myarray1, MyArray2 As String
            'myarray1 = ctrl.Name
            'MyArray2 = ctrl.Text
            'If ctrl.Text <> "" Then MsgBox(Me.Name & " == " & myarray1 & " = " & MyArray2)
            'Next

            MedGuiR.ModLandToolStripButton.Image = Find_Bitmap(MedExtra & "Resource\Gui\modland.png")
            MedGuiR.DownloadMusicModuleToolStripMenuItem.Image = MedGuiR.ModLandToolStripButton.Image
            MedGuiR.LoadRomToolStripButton.Image = Find_Bitmap(MedExtra & "Resource\Gui\rom.png")
            MedGuiR.OpenFileToolStripMenuItem.Image = MedGuiR.LoadRomToolStripButton.Image
            MedGuiR.NetToolStripButton.Image = Find_Bitmap(MedExtra & "Resource\Gui\net.png")
            MedGuiR.ConnectAsClientToolStripMenuItem.Image = MedGuiR.NetToolStripButton.Image
            MedGuiR.ServerToolStripButton.Image = Find_Bitmap(MedExtra & "Resource\Gui\netplay.png")
            MedGuiR.ServerToolStripMenuItem.Image = MedGuiR.ServerToolStripButton.Image
            MedGuiR.LoadCDToolStripButton.Image = Find_Bitmap(MedExtra & "Resource\Gui\cd_rom.png")
            MedGuiR.CheckBox1.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\fast.png")
            MedGuiR.CheckBox15.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\faust.png")
            MedGuiR.Button6.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\error.png")
            MedGuiR.Button1.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\youtube.png")
            MedGuiR.Button7.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\wikipedia.png")
            MedGuiR.Button8.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\thegamesdb.png")
            MedGuiR.Button9.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\gamesdbase.png")
            MedGuiR.Button4.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\target.png")
            MedGuiR.Button35.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\scrape.png")
            MedGuiR.FavouritesToolStripButton.Image = Find_Bitmap(MedExtra & "Resource\Gui\favourite.png")
            MedGuiR.OpenFavouritesToolStripMenuItem.Image = MedGuiR.FavouritesToolStripButton.Image
            MedGuiR.RecentToolStripButton1.Image = Find_Bitmap(MedExtra & "Resource\Gui\recent.png")
            MedGuiR.RecentsToolStripMenuItem.Image = MedGuiR.RecentToolStripButton1.Image
            MedGuiR.FoldeRomToolStripButton.Image = Find_Bitmap(MedExtra & "Resource\Gui\folder.png")
            MedGuiR.OpenFolderToolStripMenuItem.Image = MedGuiR.FoldeRomToolStripButton.Image
            MedGuiR.Button11.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\arecord.png")
            MedGuiR.Button12.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\mrecord.png")
            MedGuiR.Button43.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\download.png")
            MedGuiR.Button52.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\download.png")
            MedGuiR.FindToolStripButton.Image = Find_Bitmap(MedExtra & "Resource\Gui\find.png")
            MedGuiR.RebuildToolStripButton.Image = Find_Bitmap(MedExtra & "Resource\Gui\update.png")
            MedGuiR.FlagToolStripSplitButton.Image = Find_Bitmap(MedExtra & "Resource\Flags\world.png")
            MedGuiR.FilterToolStripMenuItem.Image = MedGuiR.FlagToolStripSplitButton.Image
            MedGuiR.WORLDToolStripMenuItem1.Image = Find_Bitmap(MedExtra & "Resource\Flags\world.png")
            MedGuiR.EUToolStripMenuItem1.Image = Find_Bitmap(MedExtra & "Resource\Flags\eu.png")
            MedGuiR.USToolStripMenuItem1.Image = Find_Bitmap(MedExtra & "Resource\Flags\us.png")
            MedGuiR.JPToolStripMenuItem1.Image = Find_Bitmap(MedExtra & "Resource\Flags\jp.png")
            MedGuiR.PDToolStripMenuItem1.Image = Find_Bitmap(MedExtra & "Resource\Flags\pd.png")
            MedGuiR.MUSICToolStripMenuItem1.Image = Find_Bitmap(MedExtra & "Resource\Gui\modland.png")
            MedGuiR.WORLDToolStripMenuItem.Image = Find_Bitmap(MedExtra & "Resource\Flags\world.png")
            MedGuiR.EUToolStripMenuItem.Image = Find_Bitmap(MedExtra & "Resource\Flags\eu.png")
            MedGuiR.USToolStripMenuItem.Image = Find_Bitmap(MedExtra & "Resource\Flags\us.png")
            MedGuiR.JPToolStripMenuItem.Image = Find_Bitmap(MedExtra & "Resource\Flags\jp.png")
            MedGuiR.PDToolStripMenuItem.Image = Find_Bitmap(MedExtra & "Resource\Flags\pd.png")
            MedGuiR.MUSICToolStripMenuItem.Image = Find_Bitmap(MedExtra & "Resource\Gui\modland.png")
            MedGuiR.IRCToolStripButton.Image = Find_Bitmap(MedExtra & "Resource\Gui\irc.png")
            MedGuiR.OpenIRCToolStripMenuItem.Image = MedGuiR.IRCToolStripButton.Image
            MedGuiR.Button33.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\palette.png")
            MedGuiR.Button50.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\audio.png")
            MedGuiR.Button51.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\joypad.png")
            MedGuiR.Button54.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\folder.png")
            MedGuiR.Button55.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\update.png")
            MedGuiR.Button56.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\update.png")
            MedGuiR.Button57.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\help.png")
            MedGuiR.Button60.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\mia.ico")
            MedGuiR.PictureBox7.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\update.png")
            MedGuiR.Button22.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\csv.png")

            MgrSetting.Button2.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\folder.png")
            MgrSetting.Button1.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\folder.png")
            MgrSetting.Button4.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\rom.png")
            MgrSetting.Button5.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\save.png")
            MgrSetting.Button6.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\rom.png")
            MgrSetting.Button7.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\rom.png")
            MgrSetting.Button8.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\rom.png")
            MgrSetting.Button9.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\rom.png")
            MgrSetting.Button10.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\rom.png")
            MgrSetting.Button11.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\rom.png")
            MgrSetting.Button12.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\rom.png")
            MgrSetting.Button13.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\rom.png")
            MgrSetting.Button28.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\rom.png")
            MgrSetting.Button29.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\rom.png")
            MgrSetting.Button14.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\help.png")
            MgrSetting.Button15.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\help.png")
            MgrSetting.Button18.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\setting.png")
            MgrSetting.Button19.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\keyboard.png")
            MgrSetting.Button22.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\rom.png")
            MgrSetting.Button21.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\rom.png")
            MgrSetting.Button42.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\rom.png")
            MgrSetting.Button43.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\rom.png")
            MgrSetting.Button44.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\rom.png")
            MgrSetting.Button16.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\rom.png")
            MgrSetting.Button17.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\rom.png")
            MgrSetting.Button23.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\rom.png")
            MgrSetting.Button24.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\rom.png")
            MgrSetting.Button25.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\rom.png")
            MgrSetting.Button26.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\rom.png")

            IsoSelector.Button11.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\dtl.png")
            IsoSelector.Button1.BackgroundImage = Find_Bitmap(MedExtra & "Resource\System\SS.gif")
            IsoSelector.Button2.BackgroundImage = Find_Bitmap(MedExtra & "Resource\System\PCECD.gif")
            IsoSelector.Button3.BackgroundImage = Find_Bitmap(MedExtra & "Resource\System\PCFX.gif")
            IsoSelector.Button4.BackgroundImage = Find_Bitmap(MedExtra & "Resource\System\MDCD.gif")
            IsoSelector.Button5.BackgroundImage = Find_Bitmap(MedExtra & "Resource\System\PSX.gif")
            IsoSelector.Button6.BackgroundImage = Find_Bitmap(MedExtra & "Resource\System\cdplay.gif")

            MPCG.Button1.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\folder.png")
            MPCG.Button2.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\add.png")
            MedGuiR.SaveCustomPlaylistToolStripMenuItem.Image = MPCG.Button2.BackgroundImage
            MPCG.Button3.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\delete.png")
            MPCG.Button4.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\converter.png")
            MPCG.Button5.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\cd_rom.png")

            MedGuiR.Button37.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\target.png")
            MedGuiR.Button41.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\folder.png")
            MedGuiR.Button36.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\buggy.png")
            MedGuiR.Button39.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\delete.png")

            MedGuiR.PictureBox3.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\update.png")
            'MedGuiR.RescanToolStripMenuItem.Image = MedGuiR.PictureBox3.BackgroundImage
            MedGuiR.Button2.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\buggy.png")
            MedGuiR.Button20.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\save.png")
            MedGuiR.Button21.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\update.png")
            MedGuiR.Button30.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\folder.png")
            MedGuiR.Button31.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\folder.png")
            MedGuiR.Button5.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\folder.png")
            MedGuiR.Button10.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\folder.png")
            MedGuiR.Button13.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\folder.png")
            MedGuiR.Button14.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\folder.png")
            MedGuiR.Button15.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\folder.png")
            MedGuiR.Button16.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\folder.png")
            MedGuiR.Button17.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\folder.png")
            MedGuiR.Button18.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\folder.png")
            MedGuiR.Button19.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\folder.png")
            MedGuiR.Button23.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\folder.png")
            MedGuiR.Button24.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\folder.png")
            MedGuiR.Button25.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\folder.png")
            MedGuiR.Button26.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\folder.png")
            MedGuiR.Button27.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\folder.png")
            MedGuiR.Button28.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\folder.png")
            MedGuiR.Button49.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\folder.png")
            MedGuiR.Button58.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\folder.png")
            MedGuiR.Button42.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\find.png")
            'MedGuiR.RapidGameSearchToolStripMenuItem.Image = MedGuiR.Button42.BackgroundImage
            MedGuiR.Button47.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\delete.png")
            MedGuiR.Button3.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\download.png")
            MedGuiR.Button44.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\dat.png")
            MedGuiR.ManageFileToolStripMenuItem.Image = MedGuiR.Button44.BackgroundImage
            MedGuiR.ADVManageToolStripMenuItem.Image = MedGuiR.Button44.BackgroundImage
            MedGuiR.ConvertAudioToToolStripMenuItem.Image = Find_Bitmap(MedExtra & "Resource\Gui\converter.png")
            MedGuiR.ConvertFolderAudioToToolStripMenuItem.Image = Find_Bitmap(MedExtra & "Resource\Gui\converter.png")
            MedGuiR.AddAudioFileToolStripMenuItem.Image = Find_Bitmap(MedExtra & "Resource\Gui\audio.png")
            MedGuiR.PlayToolStripMenuItem.Image = Find_Bitmap(MedExtra & "Resource\Gui\play.png")
            MedGuiR.StopToolStripMenuItem.Image = Find_Bitmap(MedExtra & "Resource\Gui\stop.png")
            MedGuiR.NextToolStripMenuItem.Image = Find_Bitmap(MedExtra & "Resource\Gui\next.png")
            MedGuiR.OggToolStripMenuItem.Image = Find_Bitmap(MedExtra & "Resource\Gui\ogg.png")
            MedGuiR.OggToolStripMenuItem1.Image = Find_Bitmap(MedExtra & "Resource\Gui\ogg.png")
            MedGuiR.WavToolStripMenuItem.Image = Find_Bitmap(MedExtra & "Resource\Gui\wav.png")
            MedGuiR.WavToolStripMenuItem1.Image = Find_Bitmap(MedExtra & "Resource\Gui\wav.png")
            MedGuiR.MultimediaToolStripMenuItem.Image = Find_Bitmap(MedExtra & "Resource\Gui\player.png")
            MedGuiR.DeleteAfterConversionToolStripMenuItem.Image = Find_Bitmap(MedExtra & "Resource\Gui\delete.png")

            MedGuiR.StartGameToolStripMenuItem.Image = Find_Bitmap(MedExtra & "Resource\Gui\play.png")
            MedGuiR.NetPlayToolStripMenuItem.Image = Find_Bitmap(MedExtra & "Resource\Gui\netplay.png")
            MedGuiR.NickToolStripTextBox1.Image = Find_Bitmap(MedExtra & "Resource\Gui\info.png")
            MedGuiR.ServerToolStripButton.Image = Find_Bitmap(MedExtra & "Resource\Gui\netplay.png")
            MedGuiR.OnlineToolStripMenuItem1.Image = Find_Bitmap(MedExtra & "Resource\Gui\net.png")
            MedGuiR.BCKPToolStripMenuItem.Image = Find_Bitmap(MedExtra & "Resource\Gui\save.png")
            MedGuiR.IPSToolStripMenuItem.Image = Find_Bitmap(MedExtra & "Resource\Gui\patch.png")
            MedGuiR.RIPSToolStripMenuItem.Image = Find_Bitmap(MedExtra & "Resource\Gui\delete.png")
            MedGuiR.RSBIToolStripMenuItem.Image = Find_Bitmap(MedExtra & "Resource\Gui\delete.png")
            MedGuiR.MedPadToolStripMenuItem.Image = Find_Bitmap(MedExtra & "Resource\Gui\medpad.ico")
            MedGuiR.ControllerToolStripMenuItem.Image = MedGuiR.MedPadToolStripMenuItem.Image
            MedGuiR.RenameEntryStripMenuItem.Image = Find_Bitmap(MedExtra & "Resource\Gui\save.png")
            MedGuiR.AddToFavoritesToolStripMenuItem.Image = Find_Bitmap(MedExtra & "Resource\Gui\favourite.png")
            MedGuiR.RemoveFromFavoritesToolStripMenuItem.Image = Find_Bitmap(MedExtra & "Resource\Gui\delete.png")
            MedGuiR.CleanEntriesMenuItem1.Image = Find_Bitmap(MedExtra & "Resource\Gui\clean.png")
            MedGuiR.AdvancedSettingToolStripMenuItem.Image = Find_Bitmap(MedExtra & "Resource\Gui\setting.png")
            MedGuiR.EmulatorToolStripMenuItem.Image = MedGuiR.AdvancedSettingToolStripMenuItem.Image
            MedGuiR.AddShortuctToDesktopToolStripMenuItem.Image = Find_Bitmap(MedExtra & "Resource\Gui\buggy.png")
            MedGuiR.AboutToolStripMenuItem.Image = Find_Bitmap(MedExtra & "Resource\Gui\info.png")
            MedGuiR.ImportFromFile.Image = Find_Bitmap(MedExtra & "Resource\Gui\cover.png")
            MedGuiR.FormToolStripMenuItem.Image = Find_Bitmap(MedExtra & "Resource\Gui\theme.png")
            MedGuiR.ResetToolStripMenuItem.Image = Find_Bitmap(MedExtra & "Resource\Gui\update.png")
            MedGuiR.CheatToolStripMenuItem.Image = Find_Bitmap(MedExtra & "Resource\Gui\cheat.png")
            MedGuiR.TestPCToolStripMenuItem.Image = Find_Bitmap(MedExtra & "Resource\Gui\rom.png")
            MedGuiR.mMetroMed.Image = Find_Bitmap(MedExtra & "Resource\Gui\MetroMed.png")

            Dim flip As Image
            flip = Find_Bitmap(MedExtra & "Resource\Gui\next.png")
            flip.RotateFlip(RotateFlipType.Rotate180FlipNone)
            MedGuiR.PreviousToolStripMenuItem.Image = flip

            MDM.Button1.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\folder.png")

            CPM.Button5.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\save.png")
            CPM.Button6.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\delete.png")
            CPM.Button7.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\update.png")
            CPM.Button16.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\help.png")

            PerConf.Button7.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\delete.png")
            PerConf.Button1.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\delete.png")

            KeyAssign.Button1.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\delete.png")

            SoundList.Button1.BackgroundImage = flip
            SoundList.Button2.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\stop.png")
            SoundList.Button3.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\next.png")
            SoundList.Button4.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\random.png")

            ModLand.Button1.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\find.png")
            ModLand.Button2.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\delete.png")
            ModLand.Button3.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\target.png")
            ModLand.PictureBox1.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\switch.png")
            ModLand.PictureBox2.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\update.png")

            TGDBSettings.PictureBox1.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\update.png")
            TGDBSettings.PictureBox2.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\update.png")
            TGDBSettings.PictureBox3.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\update.png")
            TGDBSettings.PictureBox4.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\update.png")

            MedClient.Button3.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\help.png")

            MedBrowser.Button3.BackgroundImage = Find_Bitmap(MedExtra & "Resource\Gui\net.png")

            AlreadyReadResourcesReturnValue = True
            AlreadyReadResources = True
            Return True
        Catch ex As Exception
            ' Only show the message box again if we haven't already
            If Not ex.Message = "Missing essential resource" Then
                MissingResource()
            End If
        End Try

        AlreadyReadResourcesReturnValue = False
        AlreadyReadResources = True
        Return False
    End Function

    Public Sub MissingResource(Optional ByVal name As String = "")
        If name.Length > 0 Then
            Message.Label1.Text = "Essential resource " & ControlChars.Quote & name & ControlChars.Quote & " missing! Download the full MedGui Reborn package from here:"
        Else
            Message.Label1.Text = "Essential resource(s) missing! Download the full MedGui Reborn package from here:"
        End If
        Message.LinkLabel1.Text = "https://github.com/Speedvicio/MedGuiReborn/releases" & vbCrLf
        Message.ShowDialog()
        MedGuiR.ResetAll = True

        MedGuiR.Close()
    End Sub

End Module