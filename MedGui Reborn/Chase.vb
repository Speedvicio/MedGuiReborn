Module Chase
    Public F1 As Form

    Public Sub ChaseR()
        Try
            If MedGuiR.Width >= Screen.PrimaryScreen.Bounds.Width Then
                MgrSetting.StartPosition = FormStartPosition.CenterScreen
            Else
                If MgrSetting.Visible = True Then
                    MgrSetting.Location = New Point(MedGuiR.Location.X - MgrSetting.Width - 14, MedGuiR.Location.Y - ((MgrSetting.Height - MedGuiR.Height) / 2))
                End If
            End If
        Catch ex As Exception
            Console.Error.WriteLine("exception: {0}", ex.ToString)
            MGRWriteLog("Chase - ChaseR: " & ex.Message)
        End Try
    End Sub

    Public Sub ChaseL()
        Try
            If MedGuiR.Width >= Screen.PrimaryScreen.Bounds.Width Then
                SoundList.StartPosition = FormStartPosition.CenterScreen
            Else
                If SoundList.Visible = True And SoundList.CheckBox1.Checked = True Then
                    SoundList.Location = New Point(MedGuiR.Location.X + MedGuiR.Width + 14, MedGuiR.Location.Y - ((SoundList.Height - MedGuiR.Height) / 2))
                End If
            End If
        Catch ex As Exception
            Console.Error.WriteLine("exception: {0}", ex.ToString)
            MGRWriteLog("Chase - ChaseL: " & ex.Message)
        End Try
    End Sub

    Public Sub CenterForm()
        Dim fx = MedGuiR.Left + (MedGuiR.Width - F1.Width) \ 2
        Dim fy = MedGuiR.Top + (MedGuiR.Height - F1.Height) \ 2
        F1.Location = New Point(fx, fy)
        F1.BringToFront()
    End Sub

    Public Sub ColorizeForm()
        If DefBack = Color.Empty And DefFore = Color.Empty Then
            ChangeControlColors(F1, "Reset")
        ElseIf DefBack = Color.Empty Then
            ChangeControlColors(F1, "Forecolor")
        ElseIf DefFore = Color.Empty Then
            ChangeControlColors(F1, "Backcolor")
        Else
            ChangeControlColors(F1, "Both")
        End If
    End Sub
End Module