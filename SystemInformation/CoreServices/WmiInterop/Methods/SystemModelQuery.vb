Namespace CoreServices.WmiInterop.Methods

    ''' <summary>
    ''' Provides methods for querying computer system model information using WMI.
    ''' </summary>
    Friend NotInheritable Class SystemModelQuery

        ''' <summary>
        ''' The default fallback string returned when the system model cannot be retrieved or is empty.
        ''' </summary>
        Private Const DefaultFallbackModel As String = "Unknown"

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

                    Return ExtractModelFromCollection(collection)
                End Using

            Catch ex As Exception
                Return UnknownModel
            End Try
        End Function

        ''' <summary>
        ''' Extracts the system model string from a WMI management object collection, ensuring proper disposal of unmanaged COM objects.
        ''' </summary>
        ''' <param name="collection">The <see cref="ManagementObjectCollection"/> returned by the WMI query.</param>
        ''' <returns>
        ''' The trimmed model name if found; otherwise, <see cref="DefaultFallbackModel"/> ("Unknown") 
        ''' if <paramref name="collection"/> is null, empty, or contains no valid model value.
        ''' </returns>
        Public Shared Function ExtractModelFromCollection(collection As ManagementObjectCollection) As String
            If collection Is Nothing Then Return DefaultFallbackModel

            For Each queryObj As ManagementObject In collection
                Using queryObj
                    Dim modelValue = queryObj(ModelProperty)?.ToString()
                    If Not String.IsNullOrWhiteSpace(modelValue) Then
                        Return modelValue.Trim()
                    End If
                End Using
            Next

            Return DefaultFallbackModel
        End Function
    End Class
End Namespace