Imports System.IO
Imports SevenZip

Module UpdateMednafen
    Public LastMednafenClean, LastMednafenFull As String

    Public Sub DetectLastMednafen()

        If Directory.Exists(MedExtra & "Update") = False Then Directory.CreateDirectory(MedExtra & "Update\")
        Dim W As New Net.WebClient
        W.DownloadFile("https://mednafen.github.io/documentation/ChangeLog", MedExtra & "Update\MednafenUpdate.txt")

        If File.Exists(MedExtra & "Update\MednafenUpdate.txt") = False Then
            MsgBox("Unable to detect last Mednafen version", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim fileReader As System.IO.StreamReader
        fileReader =
        My.Computer.FileSystem.OpenTextFileReader(MedExtra & "Update\MednafenUpdate.txt")
        Dim stringReader As String
        stringReader = fileReader.ReadLine()
        stringReader = Replace(stringReader, "--", "")
        stringReader = Replace(stringReader, ":", "")
        LastMednafenFull = stringReader
        stringReader = Replace(stringReader, ".", "")
        LastMednafenClean = Replace(stringReader, "-UNSTABLE", "")
        If Len(LastMednafenClean.Trim) < 5 Then LastMednafenClean = LastMednafenClean & "0"
        LastMednafenClean = Val(LastMednafenClean)
        fileReader.Close()

        Dim m_mu As String
        If Val(vmedClear) < LastMednafenClean Then
            m_mu = MsgBox("Mednafen v" & LastMednafenFull & " is Available to download." & vbCrLf &
                           "Do you want download it?", MsgBoxStyle.YesNo + MsgBoxStyle.Information)
            If m_mu = vbYes Then UpdateLastMednafen()
        Else
            If MedGuiR.AutoUp = False Then MsgBox("Congratulations, your Mednafen version is up to date!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
        End If

    End Sub

    Public Sub UpdateLastMednafen()
        contr_os()

        My.Computer.Network.DownloadFile("https://raw.githubusercontent.com/mednafen/mednafen.github.io/master/releases/files/mednafen-" & LastMednafenFull.Trim & "-win" & c_os & ".zip", MedExtra & "Update\LastMednafen.zip", "", "", True, 500, True)
        SevenZipExtractor.SetLibraryPath(MedExtra & "Plugins\" & sevenzdll)
        Dim szip As SevenZipExtractor = New SevenZipExtractor(MedExtra & "Update\LastMednafen.zip")
        szip.ExtractArchive(MedGuiR.TextBox4.Text)

        Threading.Thread.Sleep(2000)

        File.Delete(MedExtra & "Update\LastMednafen.zip")
        MednafenV()
    End Sub

End Module