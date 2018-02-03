Public Class Downloader
    Private WithEvents W As New Net.WebClient()

    Private Sub W_DownloadProgressChanged(ByVal sender As Object, ByVal e As Net.DownloadProgressChangedEventArgs) Handles W.DownloadProgressChanged
        'Il parametro e contiene alcune informazioni
        'sul progresso del download
        lblStatus.Text = _
            String.Format("Received Bytes: {0} B{3}File Dimension: {1} B{3}Progress: {2:N0}%", _
            e.BytesReceived, e.TotalBytesToReceive, _
            e.ProgressPercentage, Environment.NewLine)
    End Sub

    Private Sub W_DownloadFileCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs) Handles W.DownloadFileCompleted
        'e.Cancelled vale True se il download è stato annullato.
        'e.Error è di tipo Exception e contiene l'eccezione
        '  generata nel caso si sia verificato un errore.
        If e.Cancelled Then
            MessageBox.Show("Download are stopped!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ElseIf e.Error IsNot Nothing Then
            MessageBox.Show("An error occour: " & e.Error.Message, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            MessageBox.Show("Download succesfull!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        btnCancel.Enabled = False
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        'Il metodo CancelAsync cancella il download asincrono
        W.CancelAsync()
        btnCancel.Enabled = False
    End Sub

    Private Sub Downloader_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Inizia il download asincrono
        W.DownloadFileAsync(New Uri("http://digidownload.libero.it/speedvicio/MedGuiR/MedGuiR_v" & Updater.Med_new & ".zip"), MedExtra & "Update\MedGuiR.zip")
        btnCancel.Enabled = True
    End Sub
End Class