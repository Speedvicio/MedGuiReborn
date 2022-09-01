Imports System.IO
Imports DiscTools

Public Class IsoSelector
    Dim cdconsoletype As String
    Dim cd_button As Integer

    Private Sub IsoSelector_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Icon = gIcon
        Read_Resource()
        If Val(vmedClear) >= 9440 Then
            If LCase(ext) = ".m3u" Then
                NumericUpDown1.Enabled = True
                M3UIndex()
            Else
                NumericUpDown1.Enabled = False
            End If
        Else
            MedGuiR.M3UDisk = ""
            Label1.Visible = False
            NumericUpDown1.Visible = False
        End If

        If detect_module("ss.enable") = True Then
            Button1.Visible = True
            Button1.Enabled = True
        Else
            Button1.Visible = False
            Button1.Enabled = False
        End If

        UNI.Items.Clear()
        For Each drive In IO.DriveInfo.GetDrives()

            If drive.DriveType = IO.DriveType.CDRom Then
                UNI.Items.Add(Replace(drive.ToString(), "\", ""))
            End If

        Next

        F1 = Me
        CenterForm()
        ColorizeForm()
        'Dim driveInfo As System.IO.DriveInfo() = _
        'System.IO.DriveInfo.GetDrives()
        'For Each d As System.IO.DriveInfo In driveInfo
        'Try
        'UNI.Items.Add(Mid(d.Name.ToString, 1, 2))
        'Catch ex As Exception
        ' ignora gli errori dovuti a dischi “non hard disk”
        ' es. A:\ oppure lettori CD/DVD e unità non pronte
        ' (senza disco)
        'End Try
        'Next

    End Sub

    Public Sub detectcdtype()

        'Dim DiscToolV As String = (System.Diagnostics.FileVersionInfo.GetVersionInfo(Application.StartupPath & "\DiscTools.dll").FileVersion)
        'DiscToolV = Replace(DiscToolV, ".", "")

        Dim CDinspector As DiscInspector

        Try
            'If Val(DiscToolV) > 1025 Then
            'CDinspector = DiscInspector.ScanDiscQuickNoCorrection(percorso)
            'Else
            CDinspector = DiscInspector.ScanDiscQuick(percorso)
            'End If
            cdconsoletype = CDinspector.DiscTypeString
        Catch
            cdconsoletype = Nothing
        End Try

        'Dim regioncd As String = CDinspector.Data.AreaCodes

        If cdconsoletype Is Nothing Or cdconsoletype = "UnknownFormat" Then
            unrecognizedwithAsni()
        End If

        Select Case True
            Case LCase(cdconsoletype).Contains("segasaturn"), cdconsoletype.Contains("SegaSaturn")
                cd_button = 4
                startIsoCd()
            Case LCase(cdconsoletype).Contains("pc engine"), cdconsoletype.Contains("TurboCD"), cdconsoletype.Contains("PCEngineCD")
                cd_button = 0
                startIsoCd()
            Case LCase(cdconsoletype).Contains("sony computer"), cdconsoletype.Contains("SonyPSX")
                cd_button = 3
                startIsoCd()
            Case cdconsoletype.Contains("PC-FX"), cdconsoletype.Contains("PCFX")
                cd_button = 1
                startIsoCd()
            Case cdconsoletype.Contains("AudioCD")
                cd_button = 5
                startIsoCd()
            Case Else
                Me.ShowDialog()
        End Select
    End Sub

    Public Sub DetectM3U()

        Dim righe As String() = File.ReadAllLines(percorso)

        For i = 0 To 10
            If LCase(righe(i)).Contains("\") Then
                percorso = righe(i)
                Exit For
            ElseIf Trim(righe(i)) <> "" Then
                percorso = Path.Combine(Path.GetDirectoryName(percorso), righe(i))
                Exit For
            End If
        Next

        detectcdtype()
    End Sub

    Private Sub unrecognizedwithAsni()

        Dim offset As Long
        Dim rvimage As String

        Select Case LCase(Path.GetExtension(percorso))
            'If LCase(Path.GetExtension(n_psx)) = ".cue" Then
            Case ".cue" ', ".toc"
                Dim righe As String() = File.ReadAllLines(percorso)
                Dim result As String

                For i = 0 To 10
                    If LCase(righe(i)).Contains(" binary") Then
                        result = righe(i)
                        Exit For
                    End If
                Next

                Dim SPosition As Integer
                Dim word2 As String

                If result.Contains(Chr(34)) Then
                    SPosition = result.IndexOf(Chr(34)) + 1
                    word2 = result.Substring(SPosition, result.IndexOf(Chr(34), SPosition) - SPosition)
                Else
                    SPosition = result.IndexOf("E ") + 2
                    word2 = result.Substring(SPosition, result.IndexOf(" B", SPosition) - SPosition).Trim
                End If
                rvimage = Replace(percorso, Path.GetFileName(percorso), "") & word2
            Case ".ccd"
                rvimage = Replace(percorso, ".ccd", "") & ".img"
            Case Else
                Exit Sub
        End Select

        If File.Exists(rvimage) = False Then
            If skipother = False Then
                MsgBox("Bad or corrupted " & LCase(Path.GetExtension(percorso)) & " file." & vbCrLf &
                   "Open " & percorso & " with a text editor and fix the reference to the binary file", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly,
                   "Bad or copputed " & LCase(Path.GetExtension(percorso)) & "...")
                Process.Start("notepad.exe", percorso.ToString)
                stopscan = True
            End If
            Exit Sub
        End If

        Using fs As New FileStream(rvimage, FileMode.Open, FileAccess.Read)

            For offset = 0 To 9500
                cdconsoletype = cdconsoletype & Convert.ToChar(fs.ReadByte())
            Next offset
        End Using

    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Dim mnt_iso As OpenFileDialog = New OpenFileDialog()
        Dim dtl_iso As String
        mnt_iso.Title = "Select rom"
        mnt_iso.Filter = "All supported format (*.mds,*.mdx,*.b5t,*.b6t,*.bwt,*.cue,*.ccd,*.isz,*.nrg,*.cdi,*.iso,*.ecm,*.chd)|*.mds;*.mdx;*.b5t;*.b6t;*.bwt;*.cue;*.ccd;*.isz;*.nrg;*.cdi;*.iso;*.ecm;*.chd|All file (*.*)|*.*"
        mnt_iso.RestoreDirectory = True
        If mnt_iso.ShowDialog() = DialogResult.OK Then
            dtl_iso = mnt_iso.FileName
            tProcess = "DTLite.exe"
            If c_os = "64" Then
                wDir = "C:PROGRAMFILES(x86)\DAEMON Tools Lite"
            ElseIf c_os = "32" Then
                wDir = "C:PROGRAMFILES\DAEMON Tools Lite"
            End If
            Arg = "-mount dt, 0," & Chr(34) & dtl_iso & Chr(34)
            StartProcess()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        cd_button = 0
        startIsoCd()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        cd_button = 1
        startIsoCd()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        cd_button = 2
        startIsoCd()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        cd_button = 3
        startIsoCd()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        cd_button = 4
        startIsoCd()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        cd_button = 5
        startIsoCd()
    End Sub

    Public Sub startIsoCd()

        Select Case cd_button
            Case 0
                consoles = "pce"
                gif = "pcecd"
                real_name = "TurboGrafx 16 (CD)"
            Case 1
                consoles = "pcfx"
                gif = "pcfx"
                real_name = "PC-FX"
            Case 2
                consoles = "md"
                gif = "mdcd"
                real_name = "SegaCD/MegaCD"
            Case 3
                consoles = "psx"
                gif = "psx"
                real_name = "Sony PlayStation"
                If LCase(ext) <> ".m3u" Then
                    Change_PSX()
                    'Sbi_Scan()
                End If
            Case 4
                consoles = "ss"
                gif = "ss"
                real_name = "Sega Saturn"
                If LCase(ext) <> ".m3u" Then ReadIsoSaturn()
            Case 5
                consoles = "cdplay"
                gif = "cdplay"
                real_name = "Audio CD"
        End Select

        If NumericUpDown1.Enabled = False Then MedGuiR.M3UDisk = "" Else MedGuiR.M3UDisk = " -which_medium " & (NumericUpDown1.Value - 1)

        fileTXT = MedExtra & "DATs\" & MedGuiR.ComboBox1.Text & "\CUE.dat"
        If CheckBox1.Checked = True And UNI.Text <> "" Then MedGuiR.LoadCD = " -force_module " & consoles & " -physcd" : MedGuiR.TextBox1.Text = UNI.Text : MedGuiR.StartStatic_emu() Else MedGuiR.LoadCD = "" : UNI.Text = ""
        'stopscan = True
        Me.Close()
    End Sub

    Private Sub IsoSelector_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        stopscan = True
    End Sub

    Private Sub M3UIndex()
        NumericUpDown1.Maximum = File.ReadAllLines(MedGuiR.TextBox1.Text).Length
    End Sub

End Class