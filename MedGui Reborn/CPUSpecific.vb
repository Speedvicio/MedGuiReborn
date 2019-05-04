Imports System.Management
Imports System.Text

Module CPUSpecific

    Public Function GetInfo(stringIn As String) As String

        Dim sbOutput As New StringBuilder(String.Empty)

        Try
            Dim mcInfo As New ManagementClass(stringIn)

            Dim mcInfoCol As ManagementObjectCollection =
               mcInfo.GetInstances()

            Dim pdInfo As PropertyDataCollection = mcInfo.Properties

            For Each objMng As ManagementObject In mcInfoCol

                For Each prop As PropertyData In pdInfo

                    Try

                        sbOutput.AppendLine(prop.Name + ":  " +
                           objMng.Properties(prop.Name).Value)

                    Catch

                    End Try

                Next

                sbOutput.AppendLine()

            Next

        Catch

        End Try

        Return sbOutput.ToString()

    End Function

End Module
