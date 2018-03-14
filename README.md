# MedGuiReborn
MedGui Reborn is a frontend (GUI) for Mednafen multi emulator, written in Microsoft Visual Studio Community

**MedGui Reborn** ia a Windows Front-End/Gui for [Mednafen](http://mednafen.fobby.net/) multi-system emulator.

<a href="https://a.fsdn.com/con/app/proj/medguireborn/screenshots/MedGui%20Reborn%20main.jpg/1"><img src="https://a.fsdn.com/con/app/proj/medguireborn/screenshots/MedGui%20Reborn%20main.jpg/1" heigth="200" /></a><br>
<a href="https://a.fsdn.com/con/app/proj/medguireborn/screenshots/2.png/1"><img src="https://a.fsdn.com/con/app/proj/medguireborn/screenshots/2.png/1" heigth="200" /></a><br>
<a href="https://a.fsdn.com/con/app/proj/medguireborn/screenshots/pic2.png/1"><img src="https://a.fsdn.com/con/app/proj/medguireborn/screenshots/pic2.png/1" heigth="200" /></a><br>

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
* Configure by [Medpad](https://github.com/Speedvicio/MedPad) GUI all Input for all module and device supported by Mednafen
* Convert audio and cue file in a format compatible with Mednafen (through Sox converter)
* Playback of cue sheet audio files supported by Mednafen (wav/ogg through Sox converter or by Mednafen itself)
* Automatic/Manual download of available Game Box Art
* Displaying detailed information of Roms in local and/or Internet mode
* Support for Snaps and Title .png image
* Scrape of boxart and info from TheGamesDB
* Support for IRC chat session (through IrcClient.dll by Kobe)
* Auto-ips patch for PSX games
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

### Requirements
* Microsoft .NET Framework 2.0 (all Microsoft OS are supported from XP version)
* All [Mednafen version from 0.9.x.x](https://mednafen.github.io/releases/)

#### First Running
* Extract MedGuiReborn archive into a folder, run the 'MedGuiR.exe' executable and choose your Mednafen directory.
* You can import a single file or a entire folder by pressing icon on top left of GUI, or by set defcustom folder on 'Rom Path' section or by drag & drop file/folder into main grid.

#### Auto Update
* Automatically download and upgrade using the 'Updates button' (blue icon) locatel into 'General' tab
** You can tick 'Force' to update MedGuiR into intermediate version
* You can update also Mednafen to the last version by 'Updates button' (blue icon) locatel into 'General' tab
* Tick 'Auto' to perform a update control at MedGuiR start (it control the last available MedGuiR and Mednafen release)

### Download
You can found the last stable release on:
* [Release](https://github.com/Speedvicio/MedGuiReborn/releases) GitHub section
* [Official MedGui Reborn topic](https://forum.fobby.net/index.php?t=msg&th=924&start=0&)
* [SourceForge](https://sourceforge.net/projects/medguireborn/files/Exe/) page
