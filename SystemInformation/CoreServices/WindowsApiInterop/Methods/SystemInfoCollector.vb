Imports SystemInformation.CoreServices.WindowsApiInterop.Methods.Classes
Imports SystemInformation.CoreServices.WmiInterop.Methods
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
        ''' <param name="cancellationToken">An optional token to monitor for cancellation requests.</param>
        ''' <returns>
        ''' A <see cref="Task(Of SystemInfoModel)"/> representing the asynchronous operation, containing the gathered system details.
        ''' </returns>
        ''' <remarks>
        ''' Note: WMI and Registry APIs are inherently synchronous. Operations are offloaded to background threads 
        ''' via <c>Task.Run</c> to prevent blocking the UI thread during data retrieval.
        ''' </remarks>
        Friend Shared Async Function GetSystemInfoAsync(Optional cancellationToken As CancellationToken = Nothing) As Task(Of SystemInfoModel)
            cancellationToken.ThrowIfCancellationRequested()

            Dim brandTask As Task(Of String) = Task.Run(Function() SystemManufacturerQuery.GetBrand(), cancellationToken)
            Dim modelTask As Task(Of String) = Task.Run(Function() SystemModelQuery.GetModel(), cancellationToken)
            Dim serialNumberTask As Task(Of String) = Task.Run(Function() SystemSerialNumberQuery.GetSerialNumber(), cancellationToken)

            Await Task.WhenAll(brandTask, modelTask, serialNumberTask).ConfigureAwait(False)

            cancellationToken.ThrowIfCancellationRequested()

            Dim serialNumber As String = serialNumberTask.Result
            Dim snid As String = AcerSnidConverter.GenerateSnid(serialNumber)

            Return New SystemInfoModel(brandTask.Result, modelTask.Result, serialNumber, snid)
        End Function
    End Class
End Namespace
