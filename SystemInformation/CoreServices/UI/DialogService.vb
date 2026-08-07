Namespace CoreServices.UI

    ''' <summary>
    ''' Provides centralized helper methods for displaying standard system message boxes.
    ''' </summary>
    Friend NotInheritable Class DialogService

        ''' <summary>
        ''' Prevents a default instance of the <see cref="DialogService"/> class from being created.
        ''' </summary>
        Private Sub New()
        End Sub

        ''' <summary>
        ''' Displays a standard error message box to the user.
        ''' </summary>
        ''' <param name="message">The main error details to display.</param>
        ''' <param name="title">The window title of the error dialog.</param>
        Friend Shared Sub ShowError(message As String, title As String)
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Sub

        ''' <summary>
        ''' Displays a standard informational message box to the user.
        ''' </summary>
        ''' <param name="message">The informational message to display.</param>
        ''' <param name="title">The window title of the information dialog.</param>
        Friend Shared Sub ShowInformation(message As String, title As String)
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Sub
    End Class
End Namespace