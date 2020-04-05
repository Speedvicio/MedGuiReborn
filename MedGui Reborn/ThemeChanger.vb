Module ThemeChanger
    Public DefBack, DefFore As Color, switchTheme As Boolean = False

    Public Sub ChangeControlColors(ByRef MainParent As Control, Preset As String)

        If DefBack = Color.Empty Then
            DefBack = Color.FromKnownColor(KnownColor.Control)
        End If

        If DefFore = Color.Empty Then
            DefFore = Color.FromKnownColor(KnownColor.Black)
        End If

        If DefBack = DefFore Then
            MsgBox("Backgroud and Forecolour has the same value, change one of this to prevent visualization problems.", MsgBoxStyle.Exclamation + vbOKOnly, "Change one colour...")
            Exit Sub
        End If

        If Not MainParent.HasChildren Then Exit Sub

        For Each x As Control In MainParent.Controls()
            Dim en As Boolean = True
            If x.Enabled = False Then
                en = False
                x.Enabled = True
            End If

            Select Case True
                Case TypeOf x Is Label, TypeOf x Is CheckBox, TypeOf x Is Panel, TypeOf x Is LinkLabel,
                     TypeOf x Is GroupBox, TypeOf x Is Button, TypeOf x Is TabControl,
                      TypeOf x Is ToolStrip, TypeOf x Is RadioButton, TypeOf x Is TrackBar, TypeOf x Is SplitContainer
                    If x.HasChildren Then ChangeControlColors(x, Preset)
                    If Not x.Tag = "MeNot" Then
                        Select Case Preset
                            Case "Both", "Contrast", "Reset"
                                MainParent.BackColor = DefBack
                                x.BackColor = DefBack
                                x.ForeColor = DefFore

                                If TypeOf x Is LinkLabel Then
                                    Dim y As LinkLabel = x
                                    y.LinkColor = DefFore
                                End If

                                FlatC(x)
                            Case "Background"
                                x.BackColor = DefBack
                                MainParent.BackColor = DefBack
                                FlatC(x)
                            Case "Forecolor"
                                x.ForeColor = DefFore

                                If TypeOf x Is LinkLabel Then
                                    Dim y As LinkLabel = x
                                    y.LinkColor = DefFore
                                End If
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