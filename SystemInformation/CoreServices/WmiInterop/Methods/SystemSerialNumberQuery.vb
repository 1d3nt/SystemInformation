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

                    Dim serialNumber As String = ExtractSerialNumberFromCollection(collection)

                    If serialNumber IsNot Nothing Then
                        Return serialNumber.Trim()
                    End If
                End Using

            Catch ex As Exception
                DialogService.ShowError($"Failed to retrieve system serial number from WMI.{Environment.NewLine}{Environment.NewLine}Error: {ex.Message}",
                                        "WMI Error")
            End Try

            Return UnknownSerialNumber
        End Function

        ''' <summary>
        ''' Extracts the first valid serial number from a collection of WMI BIOS management objects.
        ''' </summary>
        ''' <param name="collection">The <see cref="ManagementObjectCollection"/> containing BIOS data from WMI.</param>
        ''' <returns>
        ''' The first non-null, non-whitespace serial number found in the collection, or <see langword="Nothing"/> if none is found.
        ''' </returns>
        ''' <remarks>
        ''' This method iterates through the collection using LINQ and properly disposes of each <see cref="ManagementObject"/> 
        ''' after accessing its properties. It filters out null or whitespace values to ensure only valid serial numbers are returned.
        ''' </remarks>
        Private Shared Function ExtractSerialNumberFromCollection(collection As ManagementObjectCollection) As String
            Return collection.
                Cast(Of ManagementObject)().
                Select(Function(obj)
                           Using obj
                               Return obj(SerialNumberProperty)?.ToString()
                           End Using
                       End Function).
                FirstOrDefault(Function(value) Not String.IsNullOrWhiteSpace(value))
        End Function
    End Class
End Namespace