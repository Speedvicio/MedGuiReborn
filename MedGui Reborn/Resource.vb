﻿Imports System.Resources

Module Resource

    Public Sub Read_Resource()
        Try

            'For Each ctrl As Control In Me.Controls = True
            'Dim myarray1, MyArray2 As String
            'myarray1 = ctrl.Name
            'MyArray2 = ctrl.Text
            'If ctrl.Text <> "" Then MsgBox(Me.Name & " == " & myarray1 & " = " & MyArray2)
            'Next

            MedGuiR.ModLandToolStripButton.Image = (New Bitmap(MedExtra & "Resource\Gui\modland.png"))
            MedGuiR.DownloadMusicModuleToolStripMenuItem.Image = MedGuiR.ModLandToolStripButton.Image
            MedGuiR.LoadRomToolStripButton.Image = (New Bitmap(MedExtra & "Resource\Gui\rom.png"))
            MedGuiR.OpenFileToolStripMenuItem.Image = MedGuiR.LoadRomToolStripButton.Image
            MedGuiR.NetToolStripButton.Image = (New Bitmap(MedExtra & "Resource\Gui\net.png"))
            MedGuiR.ConnectAsClientToolStripMenuItem.Image = MedGuiR.NetToolStripButton.Image
            MedGuiR.ServerToolStripButton.Image = (New Bitmap(MedExtra & "Resource\Gui\netplay.png"))
            MedGuiR.ServerToolStripMenuItem.Image = MedGuiR.ServerToolStripButton.Image
            MedGuiR.LoadCDToolStripButton.Image = (New Bitmap(MedExtra & "Resource\Gui\cd_rom.png"))
            MedGuiR.CheckBox1.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\fast.png"))
            MedGuiR.CheckBox15.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\faust.png"))
            MedGuiR.Button6.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\error.png"))
            MedGuiR.Button1.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\youtube.png"))
            MedGuiR.Button7.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\wikipedia.png"))
            MedGuiR.Button8.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\thegamesdb.png"))
            MedGuiR.Button9.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\gamesdbase.png"))
            MedGuiR.Button4.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\target.png"))
            MedGuiR.Button35.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\scrape.png"))
            MedGuiR.FavouritesToolStripButton.Image = (New Bitmap(MedExtra & "Resource\Gui\favourite.png"))
            MedGuiR.OpenFavouritesToolStripMenuItem.Image = MedGuiR.FavouritesToolStripButton.Image
            MedGuiR.RecentToolStripButton1.Image = (New Bitmap(MedExtra & "Resource\Gui\recent.png"))
            MedGuiR.RecentsToolStripMenuItem.Image = MedGuiR.RecentToolStripButton1.Image
            MedGuiR.FoldeRomToolStripButton.Image = (New Bitmap(MedExtra & "Resource\Gui\folder.png"))
            MedGuiR.OpenFolderToolStripMenuItem.Image = MedGuiR.FoldeRomToolStripButton.Image
            MedGuiR.Button11.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\arecord.png"))
            MedGuiR.Button12.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\mrecord.png"))
            MedGuiR.Button43.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\download.png"))
            MedGuiR.Button52.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\download.png"))
            MedGuiR.FindToolStripButton.Image = (New Bitmap(MedExtra & "Resource\Gui\find.png"))
            MedGuiR.RebuildToolStripButton.Image = (New Bitmap(MedExtra & "Resource\Gui\update.png"))
            MedGuiR.FlagToolStripSplitButton.Image = (New Bitmap(MedExtra & "Resource\Flags\world.png"))
            MedGuiR.FilterToolStripMenuItem.Image = MedGuiR.FlagToolStripSplitButton.Image
            MedGuiR.WORLDToolStripMenuItem1.Image = (New Bitmap(MedExtra & "Resource\Flags\world.png"))
            MedGuiR.EUToolStripMenuItem1.Image = (New Bitmap(MedExtra & "Resource\Flags\eu.png"))
            MedGuiR.USToolStripMenuItem1.Image = (New Bitmap(MedExtra & "Resource\Flags\us.png"))
            MedGuiR.JPToolStripMenuItem1.Image = (New Bitmap(MedExtra & "Resource\Flags\jp.png"))
            MedGuiR.PDToolStripMenuItem1.Image = (New Bitmap(MedExtra & "Resource\Flags\pd.png"))
            MedGuiR.MUSICToolStripMenuItem1.Image = (New Bitmap(MedExtra & "Resource\Gui\modland.png"))
            MedGuiR.WORLDToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Flags\world.png"))
            MedGuiR.EUToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Flags\eu.png"))
            MedGuiR.USToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Flags\us.png"))
            MedGuiR.JPToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Flags\jp.png"))
            MedGuiR.PDToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Flags\pd.png"))
            MedGuiR.MUSICToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\modland.png"))
            MedGuiR.IRCToolStripButton.Image = (New Bitmap(MedExtra & "Resource\Gui\irc.png"))
            MedGuiR.OpenIRCToolStripMenuItem.Image = MedGuiR.IRCToolStripButton.Image
            MedGuiR.Button33.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\palette.png"))
            MedGuiR.Button50.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\audio.png"))
            MedGuiR.Button51.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\joypad.png"))
            MedGuiR.Button54.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\folder.png"))
            MedGuiR.Button55.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\update.png"))
            MedGuiR.Button56.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\update.png"))
            MedGuiR.Button57.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\help.png"))
            MedGuiR.Button60.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\mia.ico"))
            MedGuiR.Button61.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\add.png"))
            MedGuiR.PictureBox7.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\update.png"))
            MedGuiR.Button22.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\csv.png"))

            MgrSetting.Button2.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\folder.png"))
            MgrSetting.Button1.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\folder.png"))
            MgrSetting.Button4.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\rom.png"))
            MgrSetting.Button5.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\save.png"))
            MgrSetting.Button6.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\rom.png"))
            MgrSetting.Button7.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\rom.png"))
            MgrSetting.Button8.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\rom.png"))
            MgrSetting.Button9.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\rom.png"))
            MgrSetting.Button10.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\rom.png"))
            MgrSetting.Button11.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\rom.png"))
            MgrSetting.Button12.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\rom.png"))
            MgrSetting.Button13.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\rom.png"))
            MgrSetting.Button28.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\rom.png"))
            MgrSetting.Button29.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\rom.png"))
            MgrSetting.Button14.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\help.png"))
            MgrSetting.Button15.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\help.png"))
            MgrSetting.Button18.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\setting.png"))
            MgrSetting.Button19.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\keyboard.png"))
            MgrSetting.Button22.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\rom.png"))
            MgrSetting.Button21.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\rom.png"))
            MgrSetting.Button42.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\rom.png"))
            MgrSetting.Button43.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\rom.png"))
            MgrSetting.Button44.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\rom.png"))
            MgrSetting.Button16.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\rom.png"))
            MgrSetting.Button17.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\rom.png"))
            MgrSetting.Button23.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\rom.png"))
            MgrSetting.Button24.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\rom.png"))
            MgrSetting.Button25.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\rom.png"))
            MgrSetting.Button26.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\rom.png"))

            IsoSelector.Button11.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\dtl.png"))
            IsoSelector.Button1.BackgroundImage = (New Bitmap(MedExtra & "Resource\System\SS.gif"))
            IsoSelector.Button2.BackgroundImage = (New Bitmap(MedExtra & "Resource\System\PCECD.gif"))
            IsoSelector.Button3.BackgroundImage = (New Bitmap(MedExtra & "Resource\System\PCFX.gif"))
            IsoSelector.Button4.BackgroundImage = (New Bitmap(MedExtra & "Resource\System\MDCD.gif"))
            IsoSelector.Button5.BackgroundImage = (New Bitmap(MedExtra & "Resource\System\PSX.gif"))
            IsoSelector.Button6.BackgroundImage = (New Bitmap(MedExtra & "Resource\System\cdplay.gif"))

            MPCG.Button1.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\folder.png"))
            MPCG.Button2.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\add.png"))
            MedGuiR.SaveCustomPlaylistToolStripMenuItem.Image = MPCG.Button2.BackgroundImage
            MPCG.Button3.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\delete.png"))
            MPCG.Button4.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\converter.png"))
            MPCG.Button5.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\cd_rom.png"))

            MedGuiR.Button37.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\target.png"))
            MedGuiR.Button41.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\folder.png"))
            MedGuiR.Button36.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\buggy.png"))
            MedGuiR.Button39.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\delete.png"))

            MedGuiR.PictureBox3.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\update.png"))
            'MedGuiR.RescanToolStripMenuItem.Image = MedGuiR.PictureBox3.BackgroundImage
            MedGuiR.Button2.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\buggy.png"))
            MedGuiR.Button20.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\save.png"))
            MedGuiR.Button21.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\update.png"))
            MedGuiR.Button30.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\folder.png"))
            MedGuiR.Button31.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\folder.png"))
            MedGuiR.Button5.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\folder.png"))
            MedGuiR.Button10.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\folder.png"))
            MedGuiR.Button13.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\folder.png"))
            MedGuiR.Button14.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\folder.png"))
            MedGuiR.Button15.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\folder.png"))
            MedGuiR.Button16.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\folder.png"))
            MedGuiR.Button17.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\folder.png"))
            MedGuiR.Button18.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\folder.png"))
            MedGuiR.Button19.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\folder.png"))
            MedGuiR.Button23.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\folder.png"))
            MedGuiR.Button24.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\folder.png"))
            MedGuiR.Button25.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\folder.png"))
            MedGuiR.Button26.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\folder.png"))
            MedGuiR.Button27.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\folder.png"))
            MedGuiR.Button28.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\folder.png"))
            MedGuiR.Button49.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\folder.png"))
            MedGuiR.Button58.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\folder.png"))
            MedGuiR.Button42.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\find.png"))
            'MedGuiR.RapidGameSearchToolStripMenuItem.Image = MedGuiR.Button42.BackgroundImage
            MedGuiR.Button47.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\delete.png"))
            MedGuiR.Button3.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\download.png"))
            MedGuiR.Button44.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\dat.png"))
            MedGuiR.ManageFileToolStripMenuItem.Image = MedGuiR.Button44.BackgroundImage
            MedGuiR.ADVManageToolStripMenuItem.Image = MedGuiR.Button44.BackgroundImage
            MedGuiR.ConvertAudioToToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\converter.png"))
            MedGuiR.ConvertFolderAudioToToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\converter.png"))
            MedGuiR.AddAudioFileToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\audio.png"))
            MedGuiR.PlayToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\play.png"))
            MedGuiR.StopToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\stop.png"))
            MedGuiR.NextToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\next.png"))
            MedGuiR.OggToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\ogg.png"))
            MedGuiR.OggToolStripMenuItem1.Image = (New Bitmap(MedExtra & "Resource\Gui\ogg.png"))
            MedGuiR.WavToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\wav.png"))
            MedGuiR.WavToolStripMenuItem1.Image = (New Bitmap(MedExtra & "Resource\Gui\wav.png"))
            MedGuiR.MultimediaToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\player.png"))
            MedGuiR.DeleteAfterConversionToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\delete.png"))

            MedGuiR.StartGameToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\play.png"))
            MedGuiR.NetPlayToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\netplay.png"))
            MedGuiR.NickToolStripTextBox1.Image = (New Bitmap(MedExtra & "Resource\Gui\info.png"))
            MedGuiR.ServerToolStripButton.Image = (New Bitmap(MedExtra & "Resource\Gui\netplay.png"))
            MedGuiR.OnlineToolStripMenuItem1.Image = (New Bitmap(MedExtra & "Resource\Gui\net.png"))
            MedGuiR.BCKPToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\save.png"))
            MedGuiR.IPSToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\patch.png"))
            MedGuiR.RIPSToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\delete.png"))
            MedGuiR.RSBIToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\delete.png"))
            MedGuiR.MedPadToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\medpad.ico"))
            MedGuiR.ControllerToolStripMenuItem.Image = MedGuiR.MedPadToolStripMenuItem.Image
            MedGuiR.RenameEntryStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\save.png"))
            MedGuiR.AddToFavoritesToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\favourite.png"))
            MedGuiR.RemoveFromFavoritesToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\delete.png"))
            MedGuiR.CleanEntriesMenuItem1.Image = (New Bitmap(MedExtra & "Resource\Gui\clean.png"))
            MedGuiR.AdvancedSettingToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\setting.png"))
            MedGuiR.EmulatorToolStripMenuItem.Image = MedGuiR.AdvancedSettingToolStripMenuItem.Image
            MedGuiR.AddShortuctToDesktopToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\buggy.png"))
            MedGuiR.AboutToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\info.png"))
            MedGuiR.ImportFromFile.Image = (New Bitmap(MedExtra & "Resource\Gui\cover.png"))
            MedGuiR.FormToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\theme.png"))
            MedGuiR.ResetToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\update.png"))
            MedGuiR.CheatToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\cheat.png"))
            MedGuiR.TestPCToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\rom.png"))
            MedGuiR.mMetroMed.Image = (New Bitmap(MedExtra & "Resource\Gui\MetroMed.png"))

            Dim flip As Image
            flip = (New Bitmap(MedExtra & "Resource\Gui\next.png"))
            flip.RotateFlip(RotateFlipType.Rotate180FlipNone)
            MedGuiR.PreviousToolStripMenuItem.Image = flip

            MDM.Button1.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\folder.png"))
            MDM.Button4.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\dat.png"))

            CPM.Button5.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\save.png"))
            CPM.Button6.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\delete.png"))
            CPM.Button7.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\update.png"))
            CPM.Button16.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\help.png"))

            PerConf.Button7.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\delete.png"))
            PerConf.Button1.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\delete.png"))

            KeyAssign.Button1.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\delete.png"))

            SoundList.Button1.BackgroundImage = flip
            SoundList.Button2.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\stop.png"))
            SoundList.Button3.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\next.png"))
            SoundList.Button4.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\random.png"))

            ModLand.Button1.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\find.png"))
            ModLand.Button2.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\delete.png"))
            ModLand.Button3.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\target.png"))
            ModLand.PictureBox1.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\switch.png"))
            ModLand.PictureBox2.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\update.png"))

            TGDBSettings.PictureBox1.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\update.png"))
            TGDBSettings.PictureBox2.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\update.png"))
            TGDBSettings.PictureBox3.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\update.png"))
            TGDBSettings.PictureBox4.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\update.png"))

            MedClient.Button3.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\help.png"))

            MedBrowser.Button3.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\net.png"))
        Catch ex As Exception
            MissingResource("")
        End Try
    End Sub

    Public Sub MissingResource(resm As String)
        Message.Text = "Missing Resource(s)"
        resm += vbCrLf & vbCrLf
        Message.Label1.Text = "Essential resource(s) missing:" & vbCrLf & resm &
        "Download the full MedGui Reborn package from here:"
        Message.LinkLabel1.Text = "https://github.com/Speedvicio/MedGuiReborn/releases" & vbCrLf
        Message.ShowDialog()
        MedGuiR.ResetAll = True
        MedGuiR.Close()
    End Sub

End Module