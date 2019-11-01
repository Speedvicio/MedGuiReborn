Module ThemeChanger
    Public DefBack, DefFore As Color

    Public Sub ChangeControlColors(ByRef MainParent As Control, Preset As String)

        If Not MainParent.HasChildren Then Exit Sub

        For Each x As Control In MainParent.Controls()
            Dim en As Boolean = True
            If x.Enabled = False Then
                en = False
                x.Enabled = True
            End If

            Select Case True
                Case TypeOf x Is Label, TypeOf x Is CheckBox, TypeOf x Is Panel,
                     TypeOf x Is GroupBox, TypeOf x Is Button, TypeOf x Is TabControl,
                      TypeOf x Is ToolStrip, TypeOf x Is RadioButton, TypeOf x Is TrackBar
                    If x.HasChildren Then ChangeControlColors(x, Preset)
                    If Not x.Tag = "MeNot" Then
                        Select Case Preset
                            Case "Both"
                                MainParent.BackColor = DefBack
                                x.BackColor = DefBack
                                x.ForeColor = DefFore
                                FlatC(x)
                            Case "Background"
                                x.BackColor = DefBack
                                MainParent.BackColor = DefBack
                                FlatC(x)
                            Case "Forecolor"
                                x.ForeColor = DefFore
                            Case "Contrast"
                                MainParent.BackColor = DefBack
                                x.BackColor = DefBack
                                DefFore = Color.FromArgb(DefBack.ToArgb() Xor &HFFFFFF)
                                x.ForeColor = DefFore
                                FlatC(x)
                            Case "Reset"
                                DefBack = Color.FromKnownColor(KnownColor.Control)
                                DefFore = Color.FromKnownColor(KnownColor.Black)
                                x.BackColor = DefBack
                                x.ForeColor = DefFore
                                MainParent.BackColor = Color.FromKnownColor(KnownColor.Control)
                                FlatC(x)
                        End Select
                    End If

            End Select
            x.Enabled = en
        Next

        If MainParent Is MedGuiR Then
            Dim actualtab As Integer = MedGuiR.TabControl1.SelectedIndex
            MedGuiR.TabControl1.SelectedIndex = 0
            MedGuiR.TabControl1.SelectedIndex = 1
            MedGuiR.TabControl1.SelectedIndex = actualtab
        End If

        MainParent.Refresh()
        'Application.DoEvents()
    End Sub

    Public Sub FlatC(x As Control)

        If TypeOf x Is Button Then
            If DefBack.ToArgb = -986896 And DefFore.ToArgb = -16777216 Then
                DirectCast(x, Button).FlatStyle = FlatStyle.Standard
            Else
                DirectCast(x, Button).FlatStyle = FlatStyle.Flat
            End If
        ElseIf TypeOf x Is GroupBox Then
            If DefBack.ToArgb = -986896 And DefFore.ToArgb = -16777216 Then
                DirectCast(x, GroupBox).FlatStyle = FlatStyle.Standard
            Else
                DirectCast(x, GroupBox).FlatStyle = FlatStyle.Flat
            End If
        End If
    End Sub

End Module