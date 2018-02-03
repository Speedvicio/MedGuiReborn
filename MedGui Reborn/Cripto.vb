Imports System.IO
Imports System.Security.Cryptography
Imports System.Text

Module Cripto

    'Obsolete now use CRC32 Hash in c_hash module
    Public Sub decripta()
        Dim md5 As MD5CryptoServiceProvider = New MD5CryptoServiceProvider
        Dim f As FileStream = New FileStream(percorso, FileMode.Open, FileAccess.Read, FileShare.Read, 8192)
        f.Dispose()
        f = New FileStream(percorso, FileMode.Open, FileAccess.Read, FileShare.Read, 8192)
        md5.ComputeHash(f)
        f.Dispose()
        f.Close()

        Dim ObjFSO As Object = CreateObject("Scripting.FileSystemObject")
        Dim objFile = ObjFSO.GetFile(percorso)
        Dim hash As Byte() = md5.Hash
        Dim buff As StringBuilder = New StringBuilder
        Dim hashByte As Byte
        For Each hashByte In hash
            buff.Append(String.Format("{0:X2}", hashByte))
        Next
        base_file = (buff.ToString())

        RData.LMain()
    End Sub

End Module