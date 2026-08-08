Namespace CoreServices.WindowsApiInterop.Methods

    ''' <summary>
    ''' Responsible solely for retrieving the system manufacturer (Brand Name) 
    ''' via native Windows Registry APIs.
    ''' </summary>
    Friend NotInheritable Class SystemManufacturerQuery

#Region " Constants "

        ''' <summary>
        ''' The registry subkey path relative to <c>HKEY_LOCAL_MACHINE</c> containing system BIOS details.
        ''' </summary>
        Private Const BiosRegistryKeyPath As String = "HARDWARE\DESCRIPTION\System\BIOS"

        ''' <summary>
        ''' The registry value name corresponding to the hardware manufacturer.
        ''' </summary>
        Private Const SystemManufacturerValueName As String = "SystemManufacturer"

        ''' <summary>
        ''' The default buffer capacity (in characters) for retrieving registry string values.
        ''' </summary>
        ''' <remarks>
        ''' 1024 characters is sufficient for typical manufacturer names which rarely exceed 100 characters.
        ''' </remarks>
        Private Const BufferCapacity As Integer = 1024

        ''' <summary>
        ''' The fallback value returned when the manufacturer cannot be determined.
        ''' </summary>
        Private Const UnknownManufacturer As String = "Unknown"
#End Region ' Constants

        ''' <summary>
        ''' Prevents a default instance of the <see cref="SystemManufacturerQuery"/> class from being created.
        ''' </summary>
        Private Sub New()
        End Sub

        ''' <summary>
        ''' Retrieves the system manufacturer name directly from the registry.
        ''' </summary>
        ''' <returns>
        ''' The system manufacturer string (for example, "Dell Inc.", "ASUSTeK COMPUTER INC."), 
        ''' or "Unknown" if the registry query fails.
        ''' </returns>
        ''' <remarks>
        ''' This call is synchronous as reading this local registry key completes in under a millisecond, 
        ''' making asynchronous task wrappers unnecessary.
        ''' </remarks>
        Friend Shared Function GetBrand() As String
            Dim buffer As New StringBuilder(BufferCapacity)
            Dim bufferSize = CType(buffer.Capacity * NativeMethods.BytesPerChar, UInteger)
            Dim type As UInteger = 0

            Dim status As Integer = NativeMethods.RegGetValue(
                NativeMethods.HkeyLocalMachine,
                BiosRegistryKeyPath,
                SystemManufacturerValueName,
                NativeMethods.RrfRtRegSz,
                type,
                buffer,
                bufferSize)

            If status = Win32Result.ErrorSuccess Then
                Return buffer.ToString().Trim()
            End If

            Return UnknownManufacturer
        End Function
    End Class
End Namespace