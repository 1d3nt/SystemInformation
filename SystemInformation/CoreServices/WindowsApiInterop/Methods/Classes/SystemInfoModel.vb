Namespace CoreServices.WindowsApiInterop.Methods.Classes

    ''' <summary>
    ''' Holds the system information retrieved from native Windows API and WMI queries.
    ''' </summary>
    Friend NotInheritable Class SystemInfoModel

        ''' <summary>
        ''' Gets the hardware manufacturer (Brand Name).
        ''' </summary>
        Friend ReadOnly Property Brand As String

        ''' <summary>
        ''' Gets the computer system model name.
        ''' </summary>
        Friend ReadOnly Property Model As String

        ''' <summary>
        ''' Gets the system BIOS serial number.
        ''' </summary>
        Friend ReadOnly Property SerialNumber As String

        ''' <summary>
        ''' Gets the calculated 11-digit SNID (Serial Number ID).
        ''' </summary>
        Friend ReadOnly Property Snid As String

        ''' <summary>
        ''' Initializes a new instance of the <see cref="SystemInfoModel"/> class.
        ''' </summary>
        ''' <param name="brand">The system manufacturer or brand name.</param>
        ''' <param name="model">The computer system model name.</param>
        ''' <param name="serialNumber">The system BIOS serial number.</param>
        ''' <param name="snid">The calculated 11-digit SNID.</param>
        Friend Sub New(brand As String, model As String, serialNumber As String, snid As String)
            Me.Brand = brand
            Me.Model = model
            Me.SerialNumber = serialNumber
            Me.Snid = snid
        End Sub
    End Class
End Namespace