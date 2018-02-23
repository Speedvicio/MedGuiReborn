Public Class TheGamesDB

    Private Sub PictureBox1_DoubleClick(sender As Object, e As EventArgs) Handles PictureBox1.DoubleClick
        Try
            Dim mnb As String
            mnb = Replace(SBoxF, "/thumb", "")
            mnb = Replace(mnb, "tfront_", "front_")
            Process.Start(mnb)
        Catch
        End Try
    End Sub

    Private Sub PictureBox2_DoubleClick(sender As Object, e As EventArgs) Handles PictureBox2.DoubleClick
        Try
            Dim mnb As String
            mnb = Replace(SboxR, "/thumb", "")
            mnb = Replace(mnb, "tback_", "back_")
            Process.Start(mnb)
        Catch
        End Try
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        If My.Computer.Network.IsAvailable = False Then MsgBox("Connections is not Available", vbOKOnly + vbExclamation) : Exit Sub
        ScrapeForce = 3
        Scrape.GetParseXML()
    End Sub

    Private Sub TheGamesDB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PictureBox3.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\update.png"))
    End Sub

    Private Sub TheGamesDB_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd
        If Me.Width < 750 Then Me.Width = 750
        If Me.Height < 400 Then Me.Height = 400
    End Sub

End Class