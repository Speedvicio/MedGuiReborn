Public Class PopUp
    Public PopupPic As String

    Private Sub PictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox1.MouseLeave
        Me.Close()
    End Sub

    Private Sub PopUp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' BackgroundImage = New Bitmap(PopupPic)
        Me.Icon = gIcon
        MedGuiR.TopMost = False
        TopMost = True

        PictureBox1.Load(PopupPic)

        Dim PicYOrig As Integer = PictureBox1.Height
        Dim PicXOrig As Integer = PictureBox1.Width
        Dim proportion As Integer

        Select Case PictureBox1.Height
            Case > 300
                proportion = PictureBox1.Height / 300
                PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
                PictureBox1.Height = (PicYOrig / proportion)
                PictureBox1.Width = (PicXOrig / proportion)
            Case < 300
                proportion = 300 / PictureBox1.Height
                PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
                PictureBox1.Height = (PicYOrig * proportion)
                PictureBox1.Width = (PicXOrig * proportion)
            Case Else
                PictureBox1.SizeMode = PictureBoxSizeMode.AutoSize
        End Select

        Location = New Point(MedGuiR.MousePosition.X - (PictureBox1.Width - 3), MedGuiR.MousePosition.Y - (PictureBox1.Height - 3))

    End Sub

End Class