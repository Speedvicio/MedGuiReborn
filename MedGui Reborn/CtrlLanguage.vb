Imports System.IO
Imports System.Reflection

Module CtrlLanguage
    Friend Function getControls(ByRef myCont As Control, Optional ByVal IncludeSubContainer As Boolean = True, Optional ByRef ctr() As Control = Nothing) As Control()
        'Code from http://blogs.dotnethell.it/sandro/Ottenere-lelenco-dei-controlli-di-un-contenitore__13796.aspx

        '***********************************************************************************
        'Func.: getControl (Creazione SB 20080707; Mod.:             )
        'Desc.: Restituisce un array dei controlli contenuti in un determinato contenitore
        '      
        'Par. : myCont          Control rappresentante il contenitore da cui partire a ricercare.
        '[IncludeSubContainer]  Opzionale. Boolean indicante se si vuole procedere in modo ricursivo all'interno
        '                       di tutti i controlli contenitore a partite da myCont.
        '[ctr()]                Opzionale. Array di controlli già esistente.
        'Ret. : Control()       Array di controlli
        '***********************************************************************************
        For Each obj As Control In myCont.Controls
            If obj.Controls.Count > 0 AndAlso IncludeSubContainer Then
                Call getControls(myCont:=obj, ctr:=ctr)
            End If
            If ctr Is Nothing Then
                ReDim ctr(0)
            Else
                ReDim Preserve ctr(UBound(ctr) + 1)
            End If
            ctr(UBound(ctr)) = obj
        Next
        Return ctr

    End Function

    Public Sub getallforms(ByVal sender As Object)
        Dim ctrlText As String
        Dim Forms As New List(Of Form)()
        Dim formType As Type = Type.GetType("System.Windows.Forms.Form")
        For Each t As Type In sender.GetType().Assembly.GetTypes()
            If UCase(t.BaseType.ToString) = "SYSTEM.WINDOWS.FORMS.FORM" Then
                If t.Name IsNot Nothing And t.Name <> "About" And t.Name <> "Message" Then

                    Dim formName As String = t.Name
                    formName = [Assembly].GetEntryAssembly.GetName.Name & "." & formName

                    Dim CountCtr As Integer = 0
                    For Each ctr As Control In getControls(DirectCast([Assembly].GetEntryAssembly.CreateInstance(formName), Form))
                        If TypeOf ctr IsNot TextBox And TypeOf ctr IsNot ComboBox _
                        And TypeOf ctr IsNot NumericUpDown And TypeOf ctr IsNot ToolStrip Then

                            If ctr.Text.Trim <> "" And Len(ctr.Text.Trim) > 3 Then
                                ctrlText = String.Concat(ctrlText, t.Name & "." & ctr.Name & "  :  " & ctr.Text.Trim, vbCrLf)
                                CountCtr += 1
                            End If
                        End If
                    Next
                    If CountCtr > 0 Then ctrlText += "/**-- --**\" & vbCrLf
                End If
            End If
        Next

        Dim file As System.IO.StreamWriter
        file = My.Computer.FileSystem.OpenTextFileWriter(Path.Combine(MedExtra & "Language", "English.txt"), False)
        file.WriteLine(ctrlText)
        file.Close()
    End Sub

    Public Function TranslateAllCtrls(ByVal Language As String)
        Dim LangTxt As String = Path.Combine(MedExtra & "Language", Language & ".txt")
        If File.Exists(LangTxt) = False Then Exit Function

        Using sr As New StreamReader(LangTxt)
            Do Until sr.EndOfStream
                Try
                    Dim sBuf As String = sr.ReadLine
                    Dim BufSplit As String()
                    Dim FormControl As String()

                    BufSplit = sBuf.Split("  :  ")
                    FormControl = BufSplit(0).Split(".")
                    Dim formName As String = FormControl(0)
                    formName = [Assembly].GetEntryAssembly.GetName.Name & "." & formName

                    Dim aForm = DirectCast([Assembly].GetEntryAssembly.CreateInstance(formName), Form)
                    Dim aControl = aForm.Controls.Item(FormControl(1))

                    If aControl IsNot Nothing And BufSplit(4) IsNot Nothing Then
                        aControl.Text = BufSplit(4)
                    End If

                Catch ex As Exception

                End Try
            Loop
        End Using
    End Function
End Module
