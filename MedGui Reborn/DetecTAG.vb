Imports System.IO

Module DetecTAG
    Public AllTags As String

    Sub DetectChipmodule()
        AllTags = ""

        Select Case LCase(Path.GetExtension(percorso))
            Case ".hes"
                'hes()
            Case ".nsf"
                nsf()
            Case ".gbs"
                gbs()
            Case ".spc"
                spc()
            Case ".psf", ".minipsf", ".minigsf", ".ssf", ".minissf"
                psf()
            Case ".bin", ".vgm"
                vg()
            Case Else
                Exit Sub
        End Select

        If String.IsNullOrEmpty(AllTags) = False Then
            AllTags = Replace(AllTags, vbNullChar, "")
            AllTags = Replace(AllTags, "=", ": ").Trim
        End If
        'MedGuiR.Datagrid_filter()
    End Sub

    Sub hes()
        Dim offset As Long
        Dim totalsong As Byte

        Using fs As New FileStream(percorso, FileMode.Open, FileAccess.Read)

            For offset = 20 To 24
                totalsong = totalsong & Convert.ToByte(fs.ReadByte())
            Next offset
            MsgBox(totalsong)
            'AllTags = "Song Name=" & namesong.Trim & vbCrLf & "Total Song=" & totalsong & vbCrLf & "Composer=" & artist.Trim & vbCrLf & "Copyright=" & copyright.Trim

        End Using
    End Sub

    Sub nsf()
        Dim offset As Long
        Dim totalsong As Byte
        Dim namesong As String
        Dim artist As String
        Dim copyright As String

        Using fs As New FileStream(percorso, FileMode.Open, FileAccess.Read)

            For offset = 5 To 200
                fs.Seek(offset, SeekOrigin.Begin)
                Select Case offset
                    Case 6
                        totalsong = (Convert.ToByte(fs.ReadByte()))
                    Case 14 To 45
                        namesong = namesong & Convert.ToChar(fs.ReadByte())
                    Case 46 To 77
                        artist = artist & Convert.ToChar(fs.ReadByte())
                    Case 78 To 109
                        copyright = copyright & Convert.ToChar(fs.ReadByte())
                End Select
            Next offset

            AllTags = "Song Name=" & namesong.Trim & vbCrLf & "Total Song=" & totalsong & vbCrLf & "Composer=" & artist.Trim & vbCrLf & "Copyright=" & copyright.Trim

            Dim xvbtf As String = namesong.Trim
            If xvbtf <> "" Then romname = Replace(xvbtf, Chr(0), "").Trim

        End Using
    End Sub

    Sub gbs()
        Dim offset As Long
        Dim totalsong As Byte
        Dim namesong As String
        Dim artist As String
        Dim copyright As String

        Using fs As New FileStream(percorso, FileMode.Open, FileAccess.Read)

            For offset = 3 To 110
                fs.Seek(offset, SeekOrigin.Begin)
                Select Case offset
                    Case 4
                        totalsong = (Convert.ToByte(fs.ReadByte()))
                    Case 16 To 47
                        namesong = namesong & Convert.ToChar(fs.ReadByte())
                    Case 48 To 78
                        artist = artist & Convert.ToChar(fs.ReadByte())
                    Case 79 To 110
                        copyright = copyright & Convert.ToChar(fs.ReadByte())
                End Select
            Next offset

            AllTags = "Song Name=" & namesong.Trim & vbCrLf & "Total Song=" & totalsong & vbCrLf & "Composer=" & artist.Trim & vbCrLf & "Copyright=" & copyright.Trim

            Dim xvbtf As String = namesong.Trim
            If xvbtf <> "" Then romname = Replace(xvbtf, Chr(0), "").Trim

        End Using
    End Sub

    Sub spc()
        Dim offset As Long
        Dim namesong As String
        Dim gamename As String
        'Dim Rgamename As String
        Dim dumper As String
        Dim comment As String
        Dim artist As String
        Dim datedump As String

        Using fs As New FileStream(percorso, FileMode.Open, FileAccess.Read)

            For offset = 46 To 250 'fs.Length -
                fs.Seek(offset, SeekOrigin.Begin)
                Select Case offset
                    Case 46 To 77
                        namesong = namesong & Convert.ToChar(fs.ReadByte())
                    Case 78 To 109
                        gamename = gamename & Convert.ToChar(fs.ReadByte())
                    Case 110 To 125
                        dumper = dumper & Convert.ToChar(fs.ReadByte())
                    Case 126 To 157
                        comment = comment & Convert.ToChar(fs.ReadByte())
                    Case 158 To 168
                        datedump = datedump & Convert.ToChar(fs.ReadByte())
                    Case 177 To 209
                        artist = artist & Convert.ToChar(fs.ReadByte())
                        'Case 66060 To 66095
                        'Rgamename = Rgamename & Convert.ToChar(fs.ReadByte())
                End Select
            Next offset

            'gamename = Replace(gamename, vbNullChar, "")
            'Rgamename = Replace(Rgamename, vbNullChar, "")

            'If Rgamename.Length > gamename.Length Then
            'gamename = Rgamename
            'End If

            AllTags = "Game Name=" & gamename.Trim & vbCrLf & "Song Name=" & namesong.Trim & vbCrLf & "Composer=" & artist.Trim & vbCrLf & "Dumper=" & dumper.Trim &
                vbCrLf & "Date Dumped=" & datedump.Trim & vbCrLf & "Comment=" & comment.Trim

            Dim xvbtf As String = Replace(gamename.Trim, Chr(0), "") & " - " & Replace(namesong.Trim, Chr(0), "")
            If xvbtf <> " - " Then romname = xvbtf.Trim

        End Using
    End Sub

    Sub psf()
        Using fs As New FileStream(percorso, FileMode.Open, FileAccess.Read)
            Dim start, baseoff, dimension As Long

            If fs.Length < 500 Then
                dimension = fs.Length
            Else
                dimension = 500
            End If

            fs.Seek(-dimension, IO.SeekOrigin.End)
            baseoff = fs.Length - dimension

            For i = 0 To fs.Length - 1
                start = start + 1
                Dim x As String = x & Convert.ToChar(fs.ReadByte())
                If x.Contains("[TAG]") Then Exit For
            Next

            For offset = baseoff + (start - 5) To fs.Length - 1
                fs.Seek(offset, SeekOrigin.Begin)
                AllTags = AllTags & Convert.ToChar(fs.ReadByte())
            Next offset

            AllTags = (Replace(AllTags, vbLf, vbCrLf))

            Dim alltagsclear() As String
            alltagsclear = Split(AllTags, vbCrLf)

            Dim atc As String
            For i As Integer = 0 To alltagsclear.Length - 1
                If alltagsclear(i).Contains("_lib") = False Then
                    atc = atc & StrConv(alltagsclear(i), VbStrConv.ProperCase) & vbCrLf
                End If
            Next
            AllTags = atc.Trim

            Dim title, game As String

            For i = 0 To alltagsclear.Length - 1
                If alltagsclear(i).Contains("game=") Then
                    game = Replace(alltagsclear(i), "game=", "")
                ElseIf alltagsclear(i).Contains("title=") Then
                    title = Replace(alltagsclear(i), "title=", "")
                End If
            Next

            Dim xvbtf As String = game & " - " & title
            If xvbtf <> " - " Then romname = xvbtf.Trim

        End Using
    End Sub

    Sub vg()

        Using fs As New FileStream(percorso, FileMode.Open, FileAccess.Read)
            Dim start, baseoff, dimension As Long

            If fs.Length < 500 Then
                dimension = fs.Length
            Else
                dimension = 500
            End If

            fs.Seek(-dimension, IO.SeekOrigin.End)
            baseoff = fs.Length - dimension

            For i = 0 To fs.Length - 1
                start = start + 1
                Dim x As String = x & Convert.ToChar(fs.ReadByte())
                If x.Contains("Gd3") Then Exit For
            Next

            If start = fs.Length - 1 Then Exit Sub

            For offset = baseoff + (start + 9) To fs.Length - 1
                fs.Seek(offset, SeekOrigin.Begin)
                AllTags = AllTags & Convert.ToChar(fs.ReadByte())
            Next offset

            Dim alltagsclear() As String
            alltagsclear = Split(AllTags.Trim, Chr(0) & Chr(0))

            AllTags = "Game Name=" & alltagsclear(2).Trim & vbCrLf & "Song Name=" & alltagsclear(0).Trim & vbCrLf & "Composer=" & alltagsclear(6).Trim & vbCrLf & "Console=" & alltagsclear(4).Trim & vbCrLf & "Dumper=" & alltagsclear(9).Trim &
                            vbCrLf & "Date Dumped=" & alltagsclear(8).Trim & vbCrLf & "Notes=" & alltagsclear(11).Trim

            'If LCase(Path.GetExtension(percorso)) = ".vgm" Then
            Dim xvbtf As String = alltagsclear(2).Trim & " - " & alltagsclear(0).Trim
            If xvbtf <> " - " Then romname = Replace(xvbtf, Chr(0), "").Trim
            'End If

        End Using
    End Sub

End Module