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
            If TypeOf ctrltarget Is LinkLabel Or TypeOf ctrltarget Is Label Then
                ctrltarget.Text = ""
                ctrltarget.tag = ""
            End If
        Next i

        RichTextBox1.Clear()
        PictureBox1.Image = Nothing
        PictureBox2.Image = Nothing

        Label2.Text = "Platform:"
        Label5.Text = "Publisher:"
        Label6.Text = "Developer:"

    End Sub

    Private Sub TheGamesDB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = gIcon
        F1 = Me
        CenterForm()
        PictureBox3.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\update.png"))

        If Val(Environment.OSVersion.Version.ToString) >= 6 Then
            PictureBox3.Enabled = True
        Else
            PictureBox3.Enabled = False
        End If
    End Sub

    Private Sub TheGamesDB_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd
        If Me.Width < 750 Then Me.Width = 750
        If Me.Height < 400 Then Me.Height = 400
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        If NewAPI = True Then
            _link = "https://thegamesdb.net/platform.php?id=" & LinkLabel1.Tag
            open_link()
        End If
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        If NewAPI = True Then
            _link = "https://thegamesdb.net/list_games.php?pub_id=" & LinkLabel2.Tag
            open_link()
        End If
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        If NewAPI = True Then
            _link = "https://thegamesdb.net/list_games.php?dev_id=" & LinkLabel3.Tag
            open_link()
        End If
    End Sub

End Class