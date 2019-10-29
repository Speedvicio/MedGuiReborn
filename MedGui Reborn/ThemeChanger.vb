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
                            Case "Background"
                                x.BackColor = DefBack
                                MainParent.BackColor = DefBack
                            Case "Forecolor"
                                x.ForeColor = DefFore
                            Case "Contrast"
                                MainParent.BackColor = DefBack
                                x.BackColor = DefBack
                                DefFore = Color.FromArgb(DefBack.ToArgb() Xor &HFFFFFF)
                                x.ForeColor = DefFore
                            Case "Reset"
                                DefBack = Color.FromKnownColor(KnownColor.Transparent)
                                DefFore = Color.FromKnownColor(KnownColor.Black)
                                x.BackColor = DefBack
                                x.ForeColor = DefFore
                                MainParent.BackColor = Color.FromKnownColor(KnownColor.Control)
                        End Select

                    End If
            End Select
            x.Enabled = en
        Next

    End Sub
End Module
