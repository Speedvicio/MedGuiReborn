# MedGui Reborn

**MedGui Reborn** ia a Windows Front-End/Gui for [Mednafen](http://mednafen.fobby.net/) multi-system emulator.
<p align="center">
<a href="https://a.fsdn.com/con/app/proj/medguireborn/screenshots/MedGui%20Reborn%20main.jpg/1"><img src="https://a.fsdn.com/con/app/proj/medguireborn/screenshots/MedGui%20Reborn%20main.jpg/1" heigth="200" /></a><br><br>
<a href="https://a.fsdn.com/con/app/proj/medguireborn/screenshots/2.jpg/1"><img src="https://a.fsdn.com/con/app/proj/medguireborn/screenshots/2.jpg/1" heigth="200" /></a><br><br>
<a href="https://a.fsdn.com/con/app/proj/medguireborn/screenshots/pic2.jpg/1"><img src="https://a.fsdn.com/con/app/proj/medguireborn/screenshots/pic2.jpg/1" heigth="200" /></a><br><br>
</p>
### Video Tuttorial

A video tutorial is available on [YouTube](https://www.youtube.com/playlist?list=PL6SV3kdlUgnECXxQzrIbCrbzo01sA1K60), watch it to learn how to use MedGui Reborn.

### Other Link
For Bugs/Suggestions/Feature Requests or simply to request help, you can refer to: 
* [Discord Server](https://discord.gg/hDpSjMb)
* [Official MedGui Reborn topic](https://forum.fobby.net/index.php?t=msg&th=924&start=0&)

### Features
* Modern "Metro" style by [MetroMed GUI](https://github.com/Speedvicio/MetroMed) appendix
* Scanning of rom with comparization with NoIntro Dats
* Support for compressed files and multi archive in. rar, .7zip (via SevenZipSharp.dll Library)
* Mount on the fly cd based games in compressed zip/cfs/ciso format by [Pismo File Mount](https://pismotec.com/download/archive/pfmap-192-win.exe)
* Configure by [Medpad](https://github.com/Speedvicio/MedPad) GUI all Input for all module and device supported by Mednafen
* Convert audio and cue file in a format compatible with Mednafen (through Sox converter)
* Playback of cue sheet audio files supported by Mednafen (wav/ogg through Sox converter or by Mednafen itself)
* Automatic/Manual download of available Game Box Art
* Experimental cheat manager
* Displaying detailed information of Roms in local and/or Internet mode
* Support for Snaps and Title .png image
* Scrape of boxart and info from TheGamesDB
* Support for IRC chat session (through IrcClient.dll by Kobe)
* Auto-ips patch for PSX games
* Auto-bps patch
* Auto Cartige Backup type for GBA games
* Automatic detecttion of real name for Saturn and PSX games
* Palettes maker for gb/gbc roms
* Snaps Manager and Renamer
* Custom DAT maker in standard ctrlMame format
* Scrape game music soundtrack from ModLand server
* Navigate into GUI menu with a directinput compatible joypad
* Net Client with list of opened netplay session by user 
* Create Desktop Shortcut of Game with icons (if boxart available)
* Convert on the fly vgm/vgz/gbs to make it playable by Mednafen
* Any game utility
* Customizable grid and GUI aspect
* Convert ecm/pbp/chd file into generic iso format

### Requirements
* Microsoft .NET Framework 2.0 (all Microsoft OS are supported from XP version)
* All [Mednafen version from 0.9.x.x](https://mednafen.github.io/releases/)

#### First Running
* Extract MedGuiReborn archive into a folder, run the 'MedGuiR.exe' executable and choose your Mednafen directory.
* You can import a single file or a entire folder by pressing icon on top left of GUI, or by set def/custom folder on 'Rom Path' section or by drag & drop file/folder into main grid.

#### Auto Update
* Automatically download and upgrade using the 'Updates button' (blue icon) located into 'General' tab
** You can tick 'Force' to update MedGuiR into intermediate version
* You can update also Mednafen to the last version by 'Updates button' (blue icon) located into 'General' tab
* Tick 'Auto' to perform a update control at MedGuiR start (it control the last available MedGuiR and Mednafen release)

### Download
You can found the last stable release on:
* [Release](https://github.com/Speedvicio/MedGuiReborn/releases) GitHub section
* [Official MedGui Reborn topic](https://forum.fobby.net/index.php?t=msg&th=924&start=0&)
* [SourceForge](https://sourceforge.net/projects/medguireborn/files/Exe/) page

### MedGui Reborn in addiction use this external exe/library:
* [SevenZipSharp.dll](https://github.com/phillipp/SevenZipSharp) for the support at compressed archive
* [7z.dll](http://www.7-zip.org/download.html) for the support at compressed archive
* [NoIntro DATs](http://datomatic.no-intro.org/?page=download) for get real name of the rom
* A modified version of [IrcClient.dll](https://github.com/cshivers/IrcClient-csharp) for IRC Chat support
* [TheGamesDB](http://thegamesdb.net/) API for Boxart Scraping
* [SoX](http://sox.sourceforge.net/) for convert/play wav<>ogg file
* [FMod.dll](https://www.fmod.com/) to play mod in About screen
* [CoreAudioApi](https://msdn.microsoft.com/en-us/library/windows/desktop/dd370802(v=vs.85).aspx) to manage volume peak for left and right channel on About screen (only on Vista and upper OS)
* [Multimedia PeakMeter Control](https://www.codeproject.com/Articles/26357/Multimedia-PeakMeter-Control) dll to visualize volume peak on About screen
* [flips.exe](https://www.smwcentral.net/?p=section&a=details&id=11474) for support at .bps patch file
* [copstation.exe](https://github.com/PSP-Tools/copstation) to unpack pbp file format
* [unecm.exe](https://web.archive.org/web/20130504220128/http://www.neillcorlett.com/cmdpack) to unpack ecm file format
* [DiscTools.dll](https://github.com/Asnivor/DiscTools) to detect PSX serial number and PC-FX cue/ccd file
* ps1titles_us_eu_jp.txt from CaptainCPS-X for get real PSX Name
* [vgmPlay.exe](https://github.com/TheDeadFish/vgmPlay-vgmConv) to convert vgm/vgz file in bin format
* [Modland](http://ftp.modland.com/) server for get all chip music
* [An FTP client library for .NET 2.0](https://github.com/conficient/ftpclient) for perform all ftp job on MedClient session
* [Mico and Sico](https://sourceforge.net/projects/micosico/?source=directory) to convert png to ico
* A modified version of [GBS2GB](http://www.angelfire.com/nc/ugetab/) to convert gbs in gb format
* Superb console icon set by [Yoshi-kun](http://yspixel.jpn.org/icon/index.html)
* [Json.NET](https://www.newtonsoft.com/json) to manage and convert json into xml
