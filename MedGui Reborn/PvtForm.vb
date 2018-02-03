Module PvtForm
    Public WithEvents PVTForm As Form
    Public WithEvents PVTButton As Button
    Public WithEvents PVTRtb As RichTextBox
    Public WithEvents PVTTxt As TextBox

    Public Sub CreatePVTForm() 'Questa sub potrà essere richiamata da qualsiasi form
        PVTForm = New Form
        PVTButton = New Button
        PVTRtb = New RichTextBox
        PVTTxt = New TextBox

        PVTForm.Size = New Size(400, 400)
        PVTRtb.Location = New Size(10, 10)
        PVTRtb.Size = New Size(370, 320)
        PVTTxt.Location = New Size(10, 335)
        PVTTxt.Size = New Size(300, 22)
        PVTButton.Location = New Size(330, 333)
        PVTButton.Size = New Size(44, 22)
        PVTButton.Text = "&Send"
        PVTForm.Controls.Add(PVTRtb)
        PVTForm.Controls.Add(PVTTxt)
        PVTForm.Controls.Add(PVTButton)
        PVTForm.Show()
    End Sub

    Public Sub PVTButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PVTButton.Click 'Qui ti ho fatto un esempio di gestione dell'evento Click del NuovoButton
        MsgBox("ciao")
    End Sub

End Module