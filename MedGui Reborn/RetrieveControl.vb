Module RetrieveControl
    Friend Function getControls(ByRef myCont As Control, Optional ByVal IncludeSubContainer As Boolean = True, Optional ByRef ctr() As Control = Nothing) As Control()
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
End Module
