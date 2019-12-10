Public Class ErLog

    Private Sub ErLog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = gIcon
        F1 = Me
        CenterForm()
        ColorizeForm()

        'RichTextBox1.SelectionStart = RichTextBox1.Text.Length
        RichTextBox1.ScrollToCaret()
    End Sub

End Class