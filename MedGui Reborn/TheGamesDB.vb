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
        Call ClearAllinForm(Panel1)
        Scrape.GetParseXML()
    End Sub

    Sub ClearAllinForm(frmTarget As Panel)
        Dim i, ctrltarget

        For i = 0 To (frmTarget.Controls.Count - 1)
            ctrltarget = frmTarget.Controls(i)
            If TypeOf ctrltarget Is Label Then
                ctrltarget.Text = ""
            End If
        Next i

        RichTextBox1.Clear()
        PictureBox1.Image = Nothing
        PictureBox2.Image = Nothing

    End Sub

    Private Sub TheGamesDB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = gIcon
        F1 = Me
        CenterForm()
        PictureBox3.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\update.png"))
    End Sub

    Private Sub TheGamesDB_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd
        If Me.Width < 750 Then Me.Width = 750
        If Me.Height < 400 Then Me.Height = 400
    End Sub

End Class