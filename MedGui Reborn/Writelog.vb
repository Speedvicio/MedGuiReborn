Imports System.IO

Module Writelog
    Private filePath As String
    Private fileStream As FileStream
    Private streamWriter As StreamWriter

    Public Sub LogOpenFile()
        Dim strPath As String
        strPath = Application.StartupPath & "\MGRerr.log"
        If System.IO.File.Exists(strPath) Then
            fileStream = New FileStream(strPath, FileMode.Append, FileAccess.Write)
        Else
            fileStream = New FileStream(strPath, FileMode.Create, FileAccess.Write)
        End If
        streamWriter = New StreamWriter(fileStream)
    End Sub

    Public Sub MGRWriteLog(ByVal strComments As String)
        LogOpenFile()
        streamWriter.WriteLine(strComments)
        CloseFile()
    End Sub

    Public Sub CloseFile()
        streamWriter.Close()
        fileStream.Close()
    End Sub

End Module