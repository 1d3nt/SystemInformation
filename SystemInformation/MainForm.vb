Imports SystemInformation.CoreServices.WindowsApiInterop.Methods

''' <author>
''' Sam (ident)
''' Twitter: <see href="https://twitter.com/1d3nt">https://twitter.com/1d3nt</see>
''' GitHub: <see href="https://github.com/1d3nt">https://github.com/1d3nt</see>
''' Email: <see href="mailto:ident@simplecoders.com">ident@simplecoders.com</see>
''' VBForums: <see href="https://www.vbforums.com/member.php?113656-ident">https://www.vbforums.com/member.php?113656-ident</see>
''' ORCID: <see href="https://orcid.org/0009-0007-1363-3308">https://orcid.org/0009-0007-1363-3308</see>
''' </author>
''' <date>07/08/2026</date>
''' <version>1.0.0</version>
''' <license>MIT License - See LICENSE for details</license>
''' <summary>
''' Provides the primary interface and event handling for the System Information application.
''' </summary>
''' <remarks>
''' This class initializes the main form, orchestrates UI events, and routes user actions to dedicated system methods.
''' 
''' This project includes WMI queries and P/Invoke declarations for interacting with Windows API functions.
''' Contributions and enhancements are welcomed to further extend the functionality and improve the integration.
''' 
''' Just a hobby programmer that enjoys P/Invoke and exploring complex interactions with system-level APIs.
''' My mallory x
''' </remarks>
Public Class MainForm

    ''' <summary>
    ''' Manages cancellation tokens for asynchronous system information retrieval operations.
    ''' </summary>
    Private _cancellationTokenSource As CancellationTokenSource

    ''' <summary>
    ''' Asynchronously retrieves hardware identifiers and populates the user interface controls.
    ''' </summary>
    Private Async Sub LoadSystemData()
        _cancellationTokenSource?.Cancel()
        _cancellationTokenSource?.Dispose()
        _cancellationTokenSource = New CancellationTokenSource()

        Try
            Dim info = Await SystemInfoCollector.GetSystemInfoAsync(_cancellationTokenSource.Token)

            BrandTextBox.Text = info.Brand
            ProductNameTextBox.Text = info.Model
            SerialNumberTextBox.Text = info.SerialNumber
            SnidTextBox.Text = info.Snid
        Catch ex As OperationCanceledException
            ' Operation was canceled; suppress UI updates and error dialogs.
        Catch ex As Exception
            DialogService.ShowError($"Failed to load system information: {ex.Message}", "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Terminates the application and returns the exit code to the operating system.
    ''' </summary>
    Private Shared Sub TerminateApplication()
        Application.Exit()
    End Sub

    ''' <summary>
    ''' Handles the form closing event to ensure pending background requests are canceled and resources are cleaned up.
    ''' </summary>
    ''' <param name="e">A <see cref="FormClosingEventArgs"/> that contains the event data.</param>
    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        _cancellationTokenSource?.Cancel()
        _cancellationTokenSource?.Dispose()
        MyBase.OnFormClosing(e)
    End Sub

#Region " UI Handlers "

    ' ReSharper disable once MemberCanBeMadeStatic.Local
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadSystemData()
    End Sub

    ' ReSharper disable once MemberCanBeMadeStatic.Local
    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click
        TerminateApplication()
    End Sub
#End Region ' UI Handlers

End Class
