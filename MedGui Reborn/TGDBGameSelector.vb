Imports System.Net

Public Class TGDBGameSelector

    Private Sub TGDBGameSelector_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = gIcon
        F1 = Me
        CenterForm()
    End Sub

    Private Sub DataGridView1_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentDoubleClick
        Try
            Dim di As IO.DirectoryInfo = New IO.DirectoryInfo(MedExtra & "Scraped\" & MedGuiR.MainGrid.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.MainGrid.CurrentRow.Cells(0).Value()))
            For Each File As IO.FileInfo In di.GetFiles()
                If File.Extension = ".jpg" Then
                    File.Delete()
                End If
            Next
        Catch
        End Try

        Try
            '<TheGamesDb newapi>
            ServicePointManager.SecurityProtocol = DirectCast(TypeTls, SecurityProtocolType)
            Dim Json1 As String = New Net.WebClient().DownloadString("https://api.thegamesdb.net/v1/Games/ByGameID?apikey=" & VSTripleDES.DecryptData("7ODuw2U0FD3nvKr3RilRqcnsINRd3XtakTHLFMAfKzNiIPGgpAyK1FwQz+bzqgx4zEZNq5fgowiOV6uBxgCHQOF8BaLgIVCINK22Edn9l8/VvogDaiOmvxfvhyC+XE0ebLwcGATfC5cs3/nYsABf8BklVmDvRGxboiPiwWPCYgoHcMRzx+cykQ==") _
         & "&id=" & DataGridView1.CurrentRow.Cells(0).Value() & "&fields=players%2Cpublishers%2Cgenres%2Coverview%2Ccoop&filter&include=boxart%2Cplatform")
            Threading.Thread.Sleep(1000)
            Dim str = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(Json1, "Root")

            Dim File As IO.StreamWriter
            File = My.Computer.FileSystem.OpenTextFileWriter(MedExtra & "Scraped\" & MedGuiR.MainGrid.CurrentRow.Cells(5).Value() & "\" & Trim(MedGuiR.MainGrid.CurrentRow.Cells(0).Value()) & ".xml", False)
            Dim splitXml As String() = Split(str.OuterXml, "<pages>")
            File.WriteLine(str.OuterXml.Remove(splitXml(0).Length, str.OuterXml.Length - splitXml(0).Length - 7))
            File.Close()

            DataGridView1.Rows.Clear()
            Scrape.ReadXml()
        Catch ex As System.Net.WebException
            MessageBox.Show(ex.Message)
            If (ex.Response IsNot Nothing) Then
                Dim hr As System.Net.HttpWebResponse = DirectCast(ex.Response, System.Net.HttpWebResponse)
            End If
        Finally
            Me.Close()
        End Try
    End Sub

End Class