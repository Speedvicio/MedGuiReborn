Imports SevenZip

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
            MedGuiR.LoadRomToolStripButton.Image = (New Bitmap(MedExtra & "Resource\Gui\rom.png"))
            MedGuiR.NetToolStripButton.Image = (New Bitmap(MedExtra & "Resource\Gui\net.png"))
            MedGuiR.ServerToolStripButton.Image = (New Bitmap(MedExtra & "Resource\Gui\netplay.png"))
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
            MedGuiR.RecentToolStripButton1.Image = (New Bitmap(MedExtra & "Resource\Gui\recent.png"))
            MedGuiR.FoldeRomToolStripButton.Image = (New Bitmap(MedExtra & "Resource\Gui\folder.png"))
            MedGuiR.Button11.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\arecord.png"))
            MedGuiR.Button12.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\mrecord.png"))
            MedGuiR.Button43.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\download.png"))
            MedGuiR.Button52.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\download.png"))
            MedGuiR.FindToolStripButton.Image = (New Bitmap(MedExtra & "Resource\Gui\find.png"))
            MedGuiR.RebuildToolStripButton.Image = (New Bitmap(MedExtra & "Resource\Gui\update.png"))
            MedGuiR.FlagToolStripSplitButton.Image = (New Bitmap(MedExtra & "Resource\Flags\world.png"))
            MedGuiR.WORLDToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Flags\world.png"))
            MedGuiR.EUToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Flags\eu.png"))
            MedGuiR.USToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Flags\us.png"))
            MedGuiR.JPToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Flags\jp.png"))
            MedGuiR.PDToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Flags\pd.png"))
            MedGuiR.MUSICToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\modland.png"))
            MedGuiR.IRCToolStripButton.Image = (New Bitmap(MedExtra & "Resource\Gui\irc.png"))
            MedGuiR.Button33.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\palette.png"))
            MedGuiR.Button50.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\audio.png"))
            MedGuiR.Button51.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\joypad.png"))
            MedGuiR.Button54.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\folder.png"))
            MedGuiR.Button55.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\update.png"))
            MedGuiR.Button56.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\update.png"))
            MedGuiR.Button57.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\help.png"))

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

            IsoSelector.Button11.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\dtl.png"))
            IsoSelector.Button1.BackgroundImage = (New Bitmap(MedExtra & "Resource\System\SS.gif"))
            IsoSelector.Button2.BackgroundImage = (New Bitmap(MedExtra & "Resource\System\PCECD.gif"))
            IsoSelector.Button3.BackgroundImage = (New Bitmap(MedExtra & "Resource\System\PCFX.gif"))
            IsoSelector.Button4.BackgroundImage = (New Bitmap(MedExtra & "Resource\System\MDCD.gif"))
            IsoSelector.Button5.BackgroundImage = (New Bitmap(MedExtra & "Resource\System\PSX.gif"))
            IsoSelector.Button6.BackgroundImage = (New Bitmap(MedExtra & "Resource\System\cdplay.gif"))

            MPCG.Button1.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\folder.png"))
            MPCG.Button2.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\add.png"))
            MPCG.Button3.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\delete.png"))
            MPCG.Button4.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\converter.png"))
            MPCG.Button5.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\cd_rom.png"))

            MedGuiR.Button37.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\target.png"))
            MedGuiR.Button41.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\folder.png"))
            MedGuiR.Button36.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\buggy.png"))
            MedGuiR.Button39.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\delete.png"))

            MedGuiR.PictureBox3.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\update.png"))
            MedGuiR.Button2.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\buggy.png"))
            MedGuiR.Button20.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\save.png"))
            MedGuiR.Button21.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\update.png"))
            MedGuiR.Button22.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\info.png"))
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
            MedGuiR.Button42.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\find.png"))
            MedGuiR.Button47.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\delete.png"))
            MedGuiR.Button3.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\download.png"))
            MedGuiR.Button44.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\dat.png"))
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
            MedGuiR.NetPlayToolStripMenuItem1.Image = (New Bitmap(MedExtra & "Resource\Gui\netplay.png"))
            MedGuiR.NickToolStripTextBox1.Image = (New Bitmap(MedExtra & "Resource\Gui\info.png"))
            MedGuiR.ServerToolStripButton.Image = (New Bitmap(MedExtra & "Resource\Gui\netplay.png"))
            MedGuiR.OnlineToolStripMenuItem1.Image = (New Bitmap(MedExtra & "Resource\Gui\net.png"))
            MedGuiR.IPSToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\patch.png"))
            MedGuiR.RIPSToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\delete.png"))
            MedGuiR.MedPadToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\medpad.ico"))
            MedGuiR.AddToFavoritesToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\favourite.png"))
            MedGuiR.RemoveFromFavoritesToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\delete.png"))
            MedGuiR.AdvancedSettingToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\setting.png"))
            MedGuiR.AddShortuctToDesktopToolStripMenuItem.Image = (New Bitmap(MedExtra & "Resource\Gui\buggy.png"))
            MedGuiR.ImportFromFile.Image = (New Bitmap(MedExtra & "Resource\Gui\cover.png"))
            MedGuiR.ResetToolStripMenuItem1.Image = (New Bitmap(MedExtra & "Resource\Gui\update.png"))
            MedGuiR.mMetroMed.Image = (New Bitmap(MedExtra & "Resource\Gui\MetroMed.png"))

            Dim flip As Image
            flip = (New Bitmap(MedExtra & "Resource\Gui\next.png"))
            flip.RotateFlip(RotateFlipType.Rotate180FlipNone)
            MedGuiR.PreviousToolStripMenuItem.Image = flip

            MDM.Button1.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\folder.png"))

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

            MedBrowser.Button3.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\net.png"))
        Catch ex As Exception
            'MsgBox("Missing Resource image on Resource folder", MsgBoxStyle.Exclamation)
            Test_Server()
            If My.Computer.Network.IsAvailable = True Then MissingResource()
        End Try
    End Sub

    Public Sub MissingResource()
        Try
            If My.Computer.Network.IsAvailable = True Then
                My.Computer.Network.DownloadFile(UpdateServer & "/MedGuiR/Resource.zip", MedExtra & "Update\Resource.zip", "anonymous", "anonymous", True, 500, True)
                SevenZipExtractor.SetLibraryPath(MedExtra & "Plugins\7z.dll")
                Dim szip As SevenZipExtractor = New SevenZipExtractor(MedExtra & "Update\Resource.zip")
                szip.ExtractArchive(Application.StartupPath)

                Threading.Thread.Sleep(1000)

                IO.File.Delete(MedExtra & "Update\Resource.zip")
                Read_Resource()
            Else
                Message.Label1.Text = "Resource file missing, download full MedGui Reborn package from here:" & vbCrLf
                Message.LinkLabel1.Text = "https://img.shields.io/sourceforge/dt/medguireborn.svg" & vbCrLf
                Message.ShowDialog()
            End If
        Catch
        End Try
    End Sub

End Module