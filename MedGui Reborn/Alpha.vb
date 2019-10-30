Public Class Alpha
    Dim nAlpha, alpha As String, ocolor As Color
    Public ncolor As Color

    Private Sub Alpha_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = gIcon
        PictureBox1.BackgroundImage = (New Bitmap(MedExtra & "Resource\Gui\alpha.png"))
        ncolor = Nothing
        ocolor = PictureBox1.BackColor
        TrackBar1.Value = Convert.ToInt32(TextBox1.Text.Substring(0, 2), 16)
        F1 = Me
        CenterForm()
        ColorizeForm()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ncolor = PictureBox1.BackColor
        Me.Close()
    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        nAlpha = (TextBox1.Text.Remove(0, 2))
        alpha = TrackBar1.Value.ToHexString(2) & nAlpha
        TextBox1.Text = alpha
        PictureBox1.BackColor = ColorTranslator.FromHtml("0x" & alpha)
    End Sub

    Private Sub Alpha_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        If ncolor = Nothing Then
            ncolor = ocolor
        End If
    End Sub

End Class