Namespace CoreServices.WmiInterop.Methods

    ''' <summary>
    ''' Provides methods for querying BIOS serial number information using WMI.
    ''' </summary>
    Friend NotInheritable Class SystemSerialNumberQuery

        ''' <summary>
        ''' The WMI query string used to retrieve BIOS information.
        ''' </summary>
        Private Const BiosQuery As String = "SELECT SerialNumber FROM Win32_BIOS"

        ''' <summary>
        ''' The WMI property name for the serial number field in Win32_BIOS.
        ''' </summary>
        Private Const SerialNumberProperty As String = "SerialNumber"

        ''' <summary>
        ''' The fallback value returned when the serial number cannot be determined.
        ''' </summary>
        Private Const UnknownSerialNumber As String = "Unknown"

        ''' <summary>
        ''' Prevents a default instance of the <see cref="SystemSerialNumberQuery"/> class from being created.
        ''' </summary>
        Private Sub New()
        End Sub

        ''' <summary>
        ''' Reads the system BIOS serial number using WMI.
        ''' </summary>
        ''' <returns>
        ''' A string containing the system serial number, or "Unknown" if the value cannot be read.
        ''' </returns>
        Friend Shared Function GetSerialNumber() As String
            Try
                Using searcher As New ManagementObjectSearcher(BiosQuery),
                    collection As ManagementObjectCollection = searcher.Get()

                    Return ExtractSerialNumberFromCollection(collection)
                End Using

            Catch ex As Exception
                Return UnknownSerialNumber
            End Try
        End Function

        ''' <summary>
        ''' Extracts the first valid serial number from a collection of WMI BIOS management objects.
        ''' </summary>
        ''' <param name="collection">The <see cref="ManagementObjectCollection"/> containing BIOS data from WMI.</param>
        ''' <returns>
        ''' The trimmed serial number if found; otherwise, <see cref="UnknownSerialNumber"/> ("Unknown") 
        ''' if <paramref name="collection"/> is null, empty, or contains no valid serial number.
        ''' </returns>
        Private Shared Function ExtractSerialNumberFromCollection(collection As ManagementObjectCollection) As String
            If collection Is Nothing Then Return UnknownSerialNumber

            For Each queryObj As ManagementObject In collection
                Using queryObj
                    Dim serialValue = queryObj(SerialNumberProperty)?.ToString()
                    If Not String.IsNullOrWhiteSpace(serialValue) Then
                        Return serialValue.Trim()
                    End If
                End Using
            Next

            Return UnknownSerialNumber
        End Function
    End Class
End Namespace