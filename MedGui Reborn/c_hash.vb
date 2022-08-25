Imports System.IO
Imports System.Security.Cryptography

Module c_hash
    Public filepath, r_md5, r_crc, r_sha As String

    ' specify the path to a file and this routine will calculate your hash
    Public Sub SHA1CalcFile()
        ' open file (as read-only)
        Using reader As New System.IO.FileStream(filepath, IO.FileMode.Open, IO.FileAccess.Read)
            Using md5 As New System.Security.Cryptography.MD5CryptoServiceProvider
                'Using crc As New System.Security.Cryptography
                ' hash contents of this stream
                Dim hash() As Byte = md5.ComputeHash(reader)
                ' return formatted hash
                r_md5 = (ByteArrayToString(hash))
            End Using
        End Using

        'Calculate sha1
        Dim sha As New SHA1CryptoServiceProvider()
        Dim result As Byte() = sha.ComputeHash(IO.File.ReadAllBytes(filepath))
        r_sha = (ByteArrayToString(result))

    End Sub

    ' utility function to convert a byte array into a hex string
    Private Function ByteArrayToString(ByVal arrInput() As Byte) As String

        Dim sb As New System.Text.StringBuilder(arrInput.Length * 2)

        For i As Integer = 0 To arrInput.Length - 1
            sb.Append(arrInput(i).ToString("X2"))
        Next

        Return sb.ToString().ToLower

    End Function

    Public Sub GetCRC32()
        Try
            Dim FS As FileStream = New FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read, 8192)
            Dim CRC32Result As Integer = &HFFFFFFFF
            Dim Buffer(4096) As Byte
            Dim ReadSize As Integer = 4096
            Dim Count As Integer = FS.Read(Buffer, 0, ReadSize)
            Dim CRC32Table(256) As Integer
            Dim DWPolynomial As Integer = &HEDB88320
            Dim DWCRC As Integer
            Dim i As Integer, j As Integer, n As Integer

            'Create CRC32 Table
            For i = 0 To 255
                DWCRC = i
                For j = 8 To 1 Step -1
                    If (DWCRC And 1) Then
                        DWCRC = ((DWCRC And &HFFFFFFFE) \ 2&) And &H7FFFFFFF
                        DWCRC = DWCRC Xor DWPolynomial
                    Else
                        DWCRC = ((DWCRC And &HFFFFFFFE) \ 2&) And &H7FFFFFFF
                    End If
                Next j
                CRC32Table(i) = DWCRC
            Next i

            'Calcualting CRC32 Hash
            Do While (Count > 0)
                For i = 0 To Count - 1
                    n = (CRC32Result And &HFF) Xor Buffer(i)
                    CRC32Result = ((CRC32Result And &HFFFFFF00) \ &H100) And &HFFFFFF
                    CRC32Result = CRC32Result Xor CRC32Table(n)
                Next i
                Count = FS.Read(Buffer, 0, ReadSize)
            Loop
            r_crc = Hex(Not (CRC32Result))
            Select Case Len(r_crc)
                Case Is = 6
                    base_file = "00" & r_crc
                Case Is = 7
                    base_file = "0" & r_crc
                Case Else
                    base_file = r_crc
            End Select
            FS.Dispose()
        Catch ex As Exception

        End Try
    End Sub

End Module