Public Class PopUp
    Public PopupPic As String

    Private Sub PictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox1.MouseLeave
        'Me.Close()
    End Sub

    Private Sub PopUp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MedGuiR.ToolTip1.Active = False
        Me.Icon = gIcon

        Me.BringToFront()

        Dim bArt As New Bitmap(PopupPic)

        Dim PicYOrig As Integer = bArt.Height
        Dim PicXOrig As Integer = bArt.Width
        Dim proportion As Double

        Select Case bArt.Height
            Case > 300
                proportion = bArt.Height / 300
                PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
                PictureBox1.Height = (PicYOrig / proportion)
                PictureBox1.Width = (PicXOrig / proportion)
            Case < 300
                proportion = 300 / bArt.Height
                PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
                PictureBox1.Height = (PicYOrig * proportion)
                PictureBox1.Width = (PicXOrig * proportion)
            Case Else
                PictureBox1.SizeMode = PictureBoxSizeMode.AutoSize
        End Select

        PictureBox1.Load(PopupPic)

        F1 = Me
        CenterForm()

        Dim ScreenPos As Point = MedGuiR.TabControl1.PointToScreen(MedGuiR.PictureBox1.Location)

        Dim nloc As Double

        If Me.Location.X + PictureBox1.Width > ScreenPos.X Then
            nloc = Me.Location.X + PictureBox1.Width - ScreenPos.X
            Location = New Point(Me.Location.X - nloc, Me.Location.Y)
        End If

    End Sub

    Private Sub PopUp_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        MedGuiR.ToolTip1.Active = True
    End Sub

End Class