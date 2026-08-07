Namespace CoreServices.WindowsApiInterop.Methods

    ''' <summary>
    ''' Provides methods for querying computer system model information using WMI.
    ''' </summary>
    Friend NotInheritable Class SystemModelQuery

        ''' <summary>
        ''' The WMI query string used to retrieve computer system model information.
        ''' </summary>
        Private Const ComputerSystemQuery As String = "SELECT Model FROM Win32_ComputerSystem"

        ''' <summary>
        ''' The WMI property name for the model field in Win32_ComputerSystem.
        ''' </summary>
        Private Const ModelProperty As String = "Model"

        ''' <summary>
        ''' The fallback value returned when the system model cannot be determined.
        ''' </summary>
        Private Const UnknownModel As String = "Unknown"

        ''' <summary>
        ''' Prevents a default instance of the <see cref="SystemModelQuery"/> class from being created.
        ''' </summary>
        Private Sub New()
        End Sub

        ''' <summary>
        ''' Reads the computer system model name using WMI.
        ''' </summary>
        ''' <returns>
        ''' A string containing the computer model name, or "Unknown" if the value cannot be read.
        ''' </returns>
        Friend Shared Function GetModel() As String
            Try
                Using searcher As New ManagementObjectSearcher(ComputerSystemQuery),
                      collection As ManagementObjectCollection = searcher.Get()

                    Dim model As String = ExtractModelFromCollection(collection)

                    If model IsNot Nothing Then
                        Return model.Trim()
                    End If
                End Using

            Catch ex As Exception
                DialogService.ShowError($"Failed to retrieve system model from WMI.{Environment.NewLine}{Environment.NewLine}Error: {ex.Message}",
                                        "WMI Error")
            End Try

            Return UnknownModel
        End Function

        ''' <summary>
        ''' Extracts the first valid model name from a collection of WMI computer system management objects.
        ''' </summary>
        ''' <param name="collection">The <see cref="ManagementObjectCollection"/> containing computer system data from WMI.</param>
        ''' <returns>
        ''' The first non-null, non-whitespace model name found in the collection, or <see langword="Nothing"/> if none is found.
        ''' </returns>
        Private Shared Function ExtractModelFromCollection(collection As ManagementObjectCollection) As String
            Return collection.
                Cast(Of ManagementObject)().
                Select(Function(obj)
                           Using obj
                               Return obj(ModelProperty)?.ToString()
                           End Using
                       End Function).
                FirstOrDefault(Function(value) Not String.IsNullOrWhiteSpace(value))
        End Function
    End Class
End Namespace