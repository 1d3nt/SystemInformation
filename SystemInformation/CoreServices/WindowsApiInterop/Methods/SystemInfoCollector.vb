Imports SystemInformation.CoreServices.WindowsApiInterop.Methods.Classes
Imports SystemInformation.Utilities

Namespace CoreServices.WindowsApiInterop.Methods

    ''' <summary>
    ''' Orchestrates the retrieval of system data by invoking individual API query classes.
    ''' </summary>
    Friend NotInheritable Class SystemInfoCollector

        ''' <summary>
        ''' Prevents a default instance of the <see cref="SystemInfoCollector"/> class from being created.
        ''' </summary>
        Private Sub New()
        End Sub

        ''' <summary>
        ''' Asynchronously retrieves hardware system information including brand, model, serial number, and calculated SNID.
        ''' </summary>
        ''' <returns>
        ''' A <see cref="Task(Of SystemInfoModel)"/> representing the asynchronous operation, containing the gathered system details.
        ''' </returns>
        Friend Shared Async Function GetSystemInfoAsync() As Task(Of SystemInfoModel)
            Dim brandTask As Task(Of String) = Task.Run(Function() SystemManufacturerQuery.GetBrand())
            Dim modelTask As Task(Of String) = Task.Run(Function() SystemModelQuery.GetModel())
            Dim serialNumberTask As Task(Of String) = Task.Run(Function() SystemSerialNumberQuery.GetSerialNumber())

            Await Task.WhenAll(brandTask, modelTask, serialNumberTask)

            Dim serialNumber As String = serialNumberTask.Result
            Dim snid As String = AcerSnidConverter.GenerateSnid(serialNumber)

            Return New SystemInfoModel(brandTask.Result, modelTask.Result, serialNumber, snid)
        End Function
    End Class
End Namespace
