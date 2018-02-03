Module Localization
    Public Sub GetControls(ByVal ctrl As Control)

        For Each c As Control In ctrl.Controls

            MsgBox(String.Format("{0}     {1}", c.GetType().ToString, c.Name & " : " & c.Text))

            If c.Controls.Count > 0 Then
                'aumento il livello di rientro nella finestra immediata
                Debug.Indent()

                GetControls(c)

                'riduco il livello di rientro nella finestra immediata
                Debug.Unindent()
            End If

        Next
    End Sub
End Module
