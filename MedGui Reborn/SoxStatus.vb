Public Class SoxStatus

    Private Sub SoxStatus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = gIcon
        F1 = Me
        CenterForm()
        Label1.Refresh()
        Me.Refresh()
    End Sub

End Class