Imports System.IO

Public Class PerConf

    Private Sub PerConf_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Icon = gIcon
        Refresh_listbox()
        F1 = Me
        CenterForm()
    End Sub

    Private Sub Refresh_listbox()
        ListBox1.Items.Clear()

        For Each perconfsys As String In IO.Directory.GetFiles(MedGuiR.TextBox4.Text & "\", "*.cfg")
            If perconfsys = MedGuiR.TextBox4.Text & "\" & DMedConf & ".cfg" Then
            Else
                ListBox1.Items.Add(IO.Path.GetFileName(perconfsys))
            End If
        Next

        ListBox2.Items.Clear()
        If Directory.Exists(MedGuiR.TextBox4.Text & "\pgconfig\") Then
            For Each txtFile As String In IO.Directory.GetFiles(MedGuiR.TextBox4.Text & "\pgconfig\", "*.cfg")
                ListBox2.Items.Add(IO.Path.GetFileName(txtFile))
            Next
        Else
            Directory.CreateDirectory(MedGuiR.TextBox4.Text & "\pgconfig\")
        End If
    End Sub

    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs) Handles Button7.Click
        set_special_module()

        If ListBox1.SelectedItem = "" Then Exit Sub
        File.Delete(MedGuiR.TextBox4.Text & "\" & ListBox1.SelectedItem)
        File.Delete(MedExtra & "Backup\" & p_c & ".cfg")
        Refresh_listbox()
        KeyAssign.LoadKeyForm()
        'Setting.CheckBox6.Checked = False
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If ListBox2.SelectedItem = "" Then Exit Sub
        File.Delete(MedGuiR.TextBox4.Text & "\pgconfig\" & ListBox2.SelectedItem)
        Refresh_listbox()
        'Setting.CheckBox59.Checked = False
    End Sub

    Private Sub ListBox1_DoubleClick(sender As Object, e As System.EventArgs) Handles ListBox1.DoubleClick
        Try
            Process.Start(MedGuiR.TextBox4.Text & "\" & ListBox1.SelectedItem)
        Catch ex As Exception
            MGRWriteLog("PerConf - " & sender & ": " & ex.Message)
        End Try
    End Sub

    Private Sub ListBox2_DoubleClick(sender As Object, e As System.EventArgs) Handles ListBox2.DoubleClick
        Try
            Process.Start(MedGuiR.TextBox4.Text & "\pgconfig\" & ListBox2.SelectedItem)
        Catch ex As Exception
            MGRWriteLog("PerConf - " & sender & ": " & ex.Message)
        End Try
    End Sub

End Class