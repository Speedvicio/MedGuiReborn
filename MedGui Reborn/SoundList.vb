Public Class SoundList
    Dim prandom As Boolean

    Private Sub ListBox1_DoubleClick(sender As Object, e As EventArgs) Handles ListBox1.DoubleClick
        MedGuiR.StartEmu()
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        RichTextBox2.Clear()
        If ListBox1.SelectedItem = "" Then Exit Sub
        percorso = MedExtra & "\RomTemp\" & ListBox1.SelectedItem
        MedGuiR.TextBox1.Text = percorso
        DetectChipmodule()
        RichTextBox2.Text = AllTags
        If AllTags <> "" Then ChipTAG.RichTextBox1.Text = AllTags
    End Sub

    Private Sub RichTextBox1_LinkClicked(sender As Object, e As LinkClickedEventArgs) Handles RichTextBox1.LinkClicked
        Dim l As New Process
        l = Process.Start(e.LinkText)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        tProcess = "mednafen"
        KillProcess()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If Button4.BackColor = SystemColors.Control Then
            Button4.BackColor = Color.Red
            prandom = True
        Else
            Button4.BackColor = SystemColors.Control
            prandom = False
        End If
    End Sub

    Private Sub Israndom()
        Dim rnd As New Random
        Dim randomIndex As Integer = rnd.Next(0, ListBox1.Items.Count - 1)
        ListBox1.SelectedItem = ListBox1.Items(randomIndex)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If ListBox1.SelectedItem = "" Then Exit Sub
        If prandom = True Then
            Israndom()
        Else
            Try
                If ListBox1.SelectedIndex = ListBox1.Items.Count - 1 Then
                    ListBox1.SelectedIndex = 0
                Else
                    ListBox1.SelectedIndex = ListBox1.SelectedIndex + 1
                End If
            Catch ex As Exception

            End Try
        End If

        MedGuiR.StartEmu()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ListBox1.SelectedItem = "" Then Exit Sub
        If prandom = True Then
            Israndom()
        Else
            Try
                If ListBox1.SelectedIndex = 0 Then
                    ListBox1.SelectedIndex = ListBox1.Items.Count - 1
                Else
                    ListBox1.SelectedIndex = ListBox1.SelectedIndex - 1
                End If
            Catch ex As Exception

            End Try
        End If

        MedGuiR.StartEmu()
    End Sub

    Private Sub SoundList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = gIcon
        Read_Resource()
        F1 = Me
        CenterForm()
        'Me.TopMost = True
    End Sub

    Private Sub SoundList_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        ClearFile()
    End Sub

    Private Sub ListBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ListBox1.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                e.SuppressKeyPress = True
        End Select
    End Sub

    Private Sub ListBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles ListBox1.KeyUp
        Select Case e.KeyCode
            Case Keys.Enter
                MedGuiR.StartEmu()
        End Select
    End Sub

End Class