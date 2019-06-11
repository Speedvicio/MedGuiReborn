Imports System.IO

Module Prescanned
    Public type_csv As String
    Private Declare Function SetProcessWorkingSetSize Lib "kernel32.dll" (ByVal hProcess As IntPtr, ByVal dwMinimumWorkingSetSize As Int32, ByVal dwMaximumWorkingSetSize As Int32) As Int32

    Friend Sub ReleaseMemory()
        Try
            GC.Collect()
            GC.WaitForPendingFinalizers()
            If Environment.OSVersion.Platform = PlatformID.Win32NT Then
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub LoadGridDataInFile()
        Try
            Dim fName As String = ""
            Dim Linecsv As String = ""
            fName = MedExtra & "Scanned\" & type_csv & ".csv"
            Linecsv = File.ReadAllLines(fName).Length

            Dim TextLine As String = ""

            Dim SplitLine() As String

            Dim defreeze As Integer = 0
            If System.IO.File.Exists(fName) = True Then

                Using objReader As New System.IO.StreamReader(fName)
                    Dim cr As Integer
                    cr = 0
                    Do While objReader.Peek() <> -1
                        cr = cr + 1
                        TextLine = objReader.ReadLine()
                        SplitLine = Split(TextLine, "|")

                        If File.Exists(SplitLine(4)) = False And cr - 1 = 0 Then
                            cr = cr - 1
                        Else
                            SplitLine(1) = Nothing
                            MedGuiR.DataGridView1.Rows.Add(SplitLine)
                            real_name = SplitLine(5)
                            detect_icon()
                            MedGuiR.DataGridView1.Rows(cr - 1).Cells(1).Value() = New Bitmap(MedExtra & "Resource\System\" & UCase(gif) & ".gif")
                            MedGuiR.DataGridView1.Rows(cr - 1).Cells(9).Value() = cr - 1

                            If File.Exists(SplitLine(4)) = False Then
                                If MedGuiR.GridToolStripMenuItem.Checked = False Then
                                    MedGuiR.DataGridView1.Rows(cr - 1).DefaultCellStyle.ForeColor = Color.DarkRed
                                    MedGuiR.DataGridView1.Rows(cr - 1).DefaultCellStyle.SelectionForeColor = Color.White
                                    MedGuiR.DataGridView1.Rows(cr - 1).DefaultCellStyle.SelectionBackColor = Color.Black
                                    MedGuiR.DataGridView1.Rows(cr - 1).DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Strikeout)
                                End If
                                MedGuiR.DataGridView1.Rows(cr - 1).DefaultCellStyle.Font = New Font(MedGuiR.DataGridView1.RowsDefaultCellStyle.Font.Name, MedGuiR.DataGridView1.RowsDefaultCellStyle.Font.Size, FontStyle.Strikeout)
                            Else
                                If MedGuiR.GridToolStripMenuItem.Checked = False Then
                                    MedGuiR.DataGridView1.Rows(cr - 1).DefaultCellStyle.ForeColor = Color.Black
                                    MedGuiR.DataGridView1.RowsDefaultCellStyle.BackColor = Color.White
                                    MedGuiR.DataGridView1.Rows(cr - 1).DefaultCellStyle.SelectionForeColor = Color.Black
                                    MedGuiR.DataGridView1.Rows(cr - 1).DefaultCellStyle.SelectionBackColor = Color.PaleGoldenrod
                                    MedGuiR.DataGridView1.Rows(cr - 1).DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
                                End If
                                'MedGuiR.DataGridView1.Rows(cr - 1).DefaultCellStyle.Font = New Font(MedGuiR.DataGridView1.RowsDefaultCellStyle.Font.Name, MedGuiR.DataGridView1.RowsDefaultCellStyle.Font.Size, FontStyle.Regular)
                            End If

                        End If

                        If MedGuiR.FirstStart = False Then
                            MedGuiR.TextBox2.Visible = False
                            MedGuiR.ProgressBar1.Visible = True
                            MedGuiR.ProgressBar1.Maximum = Linecsv
                            MedGuiR.ProgressBar1.PerformStep()
                            MedGuiR.Label95.Text = "Read " & MedGuiR.ProgressBar1.Value & "/" & Linecsv
                            MedGuiR.Label95.Refresh()
                            defreeze += 1
                            If (defreeze Mod 50) = 0 Then Application.DoEvents()
                        End If

                    Loop
                    objReader.Dispose()
                    objReader.Close()
                End Using
            Else
                MsgBox("File Does Not Exist")
            End If
        Catch e As Exception
            MsgBox("The " & UCase(MedGuiR.SY.Text) & ".CSV is damaged or incorrect, please rebuild it!", vbExclamation + vbOKOnly)
        Finally
            MedGuiR.ProgressBar1.Value = 0
            MedGuiR.ProgressBar1.Visible = False
            MedGuiR.Label95.Text = "Custom Setting"
            MedGuiR.TextBox2.Visible = True
        End Try

    End Sub

    Public Sub SaveGridDataInFile()
        If Dir(MedExtra & "Scanned") = "" Then
            Directory.CreateDirectory(MedExtra & "Scanned")
        End If

        Dim I As Integer = 0

        Dim j As Integer = 0

        Dim cellvalue$

        Dim rowLine As String = ""

        Try
            If type_csv = "" Then Exit Sub

            Dim objWriter As New System.IO.StreamWriter(MedExtra & "Scanned\" & type_csv & ".csv", False)

            For j = 0 To (MedGuiR.DataGridView1.Rows.Count - 1)

                For I = 0 To (MedGuiR.DataGridView1.Columns.Count - 2)

                    If Not TypeOf MedGuiR.DataGridView1.CurrentRow.Cells.Item(I).Value Is DBNull Then

                        cellvalue = MedGuiR.DataGridView1.Item(I, j).Value.ToString

                        If cellvalue = "System.Drawing.Bitmap" Then cellvalue = "_image_console_"
                    Else
                        cellvalue = ""
                    End If

                    rowLine = rowLine + cellvalue + "|"

                Next

                objWriter.WriteLine(rowLine)

                rowLine = ""

            Next

            objWriter.Dispose()
            objWriter.Close()
        Catch e As Exception
            'MessageBox.Show("Error occurred while writing to the file." + e.ToString())
        Finally
            FileClose(1)
        End Try

    End Sub

    Public Sub SaveGridDataInRow()

        If Dir(MedExtra & "Scanned") = "" Then
            Directory.CreateDirectory(MedExtra & "Scanned")
        End If

        Dim cellvalue$
        Dim rowLine As String = ""

        Try
            If type_csv = "" Then Exit Sub

            For I = 0 To (MedGuiR.DataGridView1.Columns.Count - 2)
                If Not TypeOf MedGuiR.DataGridView1.CurrentRow.Cells.Item(I).Value Is DBNull Then
                    cellvalue = MedGuiR.DataGridView1.Item(I, MedGuiR.DataGridView1.CurrentRow.Index).Value.ToString
                    If cellvalue = "System.Drawing.Bitmap" Then cellvalue = "_image_console_"
                Else
                    cellvalue = ""
                End If
                rowLine = rowLine + cellvalue + "|"
            Next

            If Dir(MedExtra & "Scanned\" & type_csv & ".csv") <> "" Then
                Dim lineS As String() = IO.File.ReadAllLines(MedExtra & "Scanned\" & type_csv & ".csv")
                For i As Integer = 0 To lineS.Length - 1
                    If lineS(i) = rowLine Then
                        rowLine = ""
                        If type_csv = "fav" Or type_csv = "last" Then Exit Sub 'MsgBox("This Game is already in favorites list.", vbInformation + vbOKOnly)
                    End If
                Next
            End If

            Dim objWriter As New System.IO.StreamWriter(MedExtra & "Scanned\" & type_csv & ".csv", True)
            objWriter.WriteLine(rowLine)
            If type_csv = "fav" Then MsgBox("Game added in favorites list.", vbInformation + vbOKOnly)
            rowLine = ""

            objWriter.Dispose()
            objWriter.Close()
        Catch e As Exception
            'MessageBox.Show("Error occurred while writing to the file." + e.ToString())
        Finally
            FileClose(1)
        End Try

    End Sub

    Public Sub SearchGridDataInRow()
        For Each dr As DataGridViewRow In MedGuiR.DataGridView1.Rows
            If LCase(dr.Cells(0).Value.ToString).Contains(Trim(LCase(MedGuiR.TextBox3.Text))) _
               And LCase(dr.Cells(2).Value.ToString).Contains(Trim(LCase(MedGuiR.regioni))) Then
                dr.Visible = True
            Else
                dr.Visible = False
            End If
        Next
    End Sub

End Module