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
    ''' Retrieves the aggregated system information model and updates the user interface controls.
    ''' </summary>
    Private Async Sub LoadSystemData()
        Dim info = Await SystemInfoCollector.GetSystemInfoAsync()

        BrandTextBox.Text = info.Brand
        ProductNameTextBox.Text = info.Model
        SerialNumberTextBox.Text = info.SerialNumber
        SnidTextBox.Text = info.Snid
    End Sub

    ''' <summary>
    ''' Terminates the application and returns the exit code to the operating system.
    ''' </summary>
    Private Shared Sub TerminateApplication()
        Environment.Exit(0)
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
