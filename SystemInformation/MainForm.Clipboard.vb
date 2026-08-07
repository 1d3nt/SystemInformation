''' <summary>
''' Partial class containing clipboard and copy functionality for the System Information form.
''' </summary>
''' <remarks>
''' This file is separated to maintain single responsibility principle, 
''' isolating copy/clipboard operations from data loading logic in MainForm.vb.
''' </remarks>
Partial Public Class MainForm

#Region " Clipboard Constants "

    ''' <summary>
    ''' Label text for the Brand field when copying to clipboard.
    ''' </summary>
    Private Const ClipboardBrandText As String = "Brand"

    ''' <summary>
    ''' Label text for the Product Name field when copying to clipboard.
    ''' </summary>
    Private Const ClipboardProductNameText As String = "Product Name"

    ''' <summary>
    ''' Label text for the Serial Number field when copying to clipboard.
    ''' </summary>
    Private Const ClipboardSerialNumberText As String = "Serial Number"

    ''' <summary>
    ''' Label text for the SNID field when copying to clipboard.
    ''' </summary>
    Private Const ClipboardSnidText As String = "SNID"

    ''' <summary>
    ''' Message description for copying all system information.
    ''' </summary>
    Private Const AllSystemInfoText As String = "All system information"

    ''' <summary>
    ''' Title for clipboard notification dialogs.
    ''' </summary>
    Private Const ClipboardNotificationTitle As String = "Clipboard"

    ''' <summary>
    ''' Message text shown when content is copied.
    ''' </summary>
    Private Const CopiedNotificationMessage As String = "Copied!"

    ''' <summary>
    ''' Horizontal offset (in pixels) for the tooltip balloon from the control edge.
    ''' </summary>
    Private Const TooltipOffsetX As Integer = 10

    ''' <summary>
    ''' Vertical offset (in pixels) for the tooltip balloon from the control edge.
    ''' </summary>
    Private Const TooltipOffsetY As Integer = -40

    ''' <summary>
    ''' Duration (in milliseconds) that the tooltip balloon remains visible.
    ''' </summary>
    Private Const TooltipDisplayDuration As Integer = 1200

#End Region ' Clipboard Constants

    ''' <summary>
    ''' Gets a collection of all system information fields with their labels and textbox controls.
    ''' </summary>
    ''' <returns>A dictionary mapping field labels to their corresponding textbox controls.</returns>
    Private Function GetSystemInfoFields() As Dictionary(Of String, TextBox)
        Return New Dictionary(Of String, TextBox) From {
            {ClipboardBrandText, BrandTextBox},
            {ClipboardProductNameText, ProductNameTextBox},
            {ClipboardSerialNumberText, SerialNumberTextBox},
            {ClipboardSnidText, SnidTextBox}
        }
    End Function

    ''' <summary>
    ''' Checks if all system information textboxes are empty.
    ''' </summary>
    ''' <returns><see langword="True"/> if all textboxes contain no data; otherwise, <see langword="False"/>.</returns>
    Private Function AreAllFieldsEmpty() As Boolean
        Return GetSystemInfoFields().Values.All(Function(tb) String.IsNullOrWhiteSpace(tb.Text))
    End Function

    ''' <summary>
    ''' Handles the Click event for the Copy All Info button.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">An <see cref="EventArgs"/> containing event data.</param>
    Private Sub CopyAllButton_Click(sender As Object, e As EventArgs) Handles CopyAllButton.Click
        If AreAllFieldsEmpty() Then
            Return
        End If

        Dim builder As New StringBuilder()
        Dim fields = GetSystemInfoFields()

        For Each field In fields
            builder.AppendLine($"{field.Key}: {field.Value.Text}")
        Next

        Dim result As String = builder.ToString().TrimEnd(Environment.NewLine.ToCharArray())

        CopyToClipboard(result, AllSystemInfoText)
    End Sub

    ''' <summary>
    ''' Handles the Click event for individual system information textboxes to copy their value and display a balloon notification.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">An <see cref="EventArgs"/> containing event data.</param>
    Private Sub SystemInfoTextBox_Click(sender As Object, e As EventArgs) Handles BrandTextBox.Click,
                                                                                  ProductNameTextBox.Click,
                                                                                  SerialNumberTextBox.Click,
                                                                                  SnidTextBox.Click
        Dim textBox = TryCast(sender, TextBox)

        If textBox IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(textBox.Text) Then
            Dim fieldLabel As String = GetFieldLabel(textBox)
            CopyToClipboard(textBox.Text, fieldLabel, showBalloon:=True, balloonTarget:=textBox)
        End If
    End Sub

    ''' <summary>
    ''' Resolves the field label for a given textbox control.
    ''' </summary>
    ''' <param name="textBox">The <see cref="TextBox"/> control to evaluate.</param>
    ''' <returns>A string representing the control's field label.</returns>
    Private Function GetFieldLabel(textBox As TextBox) As String
        Dim fields = GetSystemInfoFields()
        Dim match = fields.FirstOrDefault(Function(kvp) kvp.Value Is textBox)

        Return If(match.Key, "Value")
    End Function

    ''' <summary>
    ''' Copies the specified text to the system clipboard and displays a user notification.
    ''' </summary>
    ''' <param name="valueToCopy">The text value to copy to the clipboard.</param>
    ''' <param name="fieldLabel">The descriptive label of the field being copied for notification purposes.</param>
    ''' <param name="showBalloon">Optional. If <see langword="True"/>, shows a balloon tooltip; otherwise shows a dialog. Default is <see langword="False"/>.</param>
    ''' <param name="balloonTarget">Optional. The control to anchor the balloon tooltip to. Only used when <paramref name="showBalloon"/> is <see langword="True"/>.</param>
    Private Sub CopyToClipboard(valueToCopy As String, fieldLabel As String, Optional showBalloon As Boolean = False, Optional balloonTarget As Control = Nothing)
        Clipboard.SetText(valueToCopy)

        If showBalloon AndAlso balloonTarget IsNot Nothing Then
            ShowCopiedBalloon(balloonTarget)
        Else
            DialogService.ShowInformation($"{fieldLabel} copied to clipboard.", ClipboardNotificationTitle)
        End If
    End Sub

    ''' <summary>
    ''' Displays a short-lived "Copied!" balloon tooltip directly above the target control.
    ''' </summary>
    ''' <param name="control">The target control where the tooltip balloon will anchor.</param>
    Private Sub ShowCopiedBalloon(control As Control)
        CopyNotificationToolTip.Hide(control)
        CopyNotificationToolTip.Show(CopiedNotificationMessage, control, TooltipOffsetX, TooltipOffsetY, TooltipDisplayDuration)
    End Sub
End Class
