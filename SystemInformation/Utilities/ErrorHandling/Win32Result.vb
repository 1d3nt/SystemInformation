Namespace Utilities.ErrorHandling

    ''' <summary>
    ''' Provides Win32 error code constants and helpers.
    ''' </summary>
    Friend NotInheritable Class Win32Result

        ''' <summary>
        ''' Prevents instantiation of this static utility class.
        ''' </summary>
        Private Sub New()
        End Sub

        ''' <summary>
        ''' The operation completed successfully.
        ''' </summary>
        ''' <remarks>
        ''' See: https://learn.microsoft.com/en-us/windows/win32/debug/system-error-codes--0-499-
        ''' </remarks>
        Friend Const ErrorSuccess As Integer = 0
    End Class
End Namespace