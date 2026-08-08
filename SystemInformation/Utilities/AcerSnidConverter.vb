Imports System.Globalization

Namespace Utilities

    ''' <summary>
    ''' Provides methods for converting Acer and Gateway hardware serial numbers into 11-digit SNID values.
    ''' </summary>
    Friend NotInheritable Class AcerSnidConverter

#Region " Constants "

        ''' <summary>
        ''' The default fallback string returned when SNID calculation fails or the serial number is invalid.
        ''' </summary>
        Friend Const DefaultFallbackSnid As String = "Unknown"

        ''' <summary>
        ''' Specifies the minimum required character length for a serial number to be valid for SNID extraction.
        ''' </summary>
        Private Const MinimumSerialLength As Integer = 20

        ''' <summary>
        ''' Specifies the starting zero-based character index for the first explicit SNID text segment.
        ''' </summary>
        Private Const Part1Index As Integer = 10

        ''' <summary>
        ''' Specifies the character count of the first explicit SNID text segment.
        ''' </summary>
        Private Const Part1Length As Integer = 3

        ''' <summary>
        ''' Specifies the starting zero-based character index for the hexadecimal segment.
        ''' </summary>
        Private Const HexPartIndex As Integer = 13

        ''' <summary>
        ''' Specifies the character count of the hexadecimal segment.
        ''' </summary>
        Private Const HexPartLength As Integer = 5

        ''' <summary>
        ''' Specifies the starting zero-based character index for the second explicit SNID text segment.
        ''' </summary>
        Private Const Part2Index As Integer = 18

        ''' <summary>
        ''' Specifies the character count of the second explicit SNID text segment.
        ''' </summary>
        Private Const Part2Length As Integer = 1

        ''' <summary>
        ''' Specifies the zero-based character index for the Base-36 encoded character.
        ''' </summary>
        Private Const Base36CharIndex As Integer = 19

        ''' <summary>
        ''' Specifies the target string length for padding the parsed hexadecimal decimal value with leading zeros.
        ''' </summary>
        Private Const HexPaddedWidth As Integer = 6
#End Region ' Constants

        ''' <summary>
        ''' Prevents a default instance of the <see cref="AcerSnidConverter"/> class from being created.
        ''' </summary>
        Private Sub New()
        End Sub

        ''' <summary>
        ''' Calculates the SNID from a hardware serial number.
        ''' </summary>
        ''' <param name="serialNumber">The hardware serial number to process.</param>
        ''' <returns>
        ''' The calculated SNID string if successful; otherwise, returns <see cref="DefaultFallbackSnid"/> ("N/A") 
        ''' if <paramref name="serialNumber"/> is null, too short, or an exception occurs.
        ''' </returns>
        Friend Shared Function GenerateSnid(serialNumber As String) As String
            If String.IsNullOrWhiteSpace(serialNumber) OrElse serialNumber.Length < MinimumSerialLength Then
                Return DefaultFallbackSnid
            End If

            Try
                Dim part1 As String = serialNumber.Substring(Part1Index, Part1Length)
                Dim hexPart As String = serialNumber.Substring(HexPartIndex, HexPartLength)
                Dim part2 As String = serialNumber.Substring(Part2Index, Part2Length)
                Dim base36Char As Char = serialNumber(Base36CharIndex)

                Dim parsedDecimal As Integer = Integer.Parse(hexPart, NumberStyles.HexNumber, CultureInfo.InvariantCulture)
                Dim formattedDecimal As String = parsedDecimal.ToString($"D{HexPaddedWidth}", CultureInfo.InvariantCulture)

                Dim charValue As Integer = ConvertBase36CharToDecimal(base36Char)

                Return $"{part1}{formattedDecimal}{part2}{charValue}"

            Catch ex As Exception
                Debug.WriteLine($"Failed to generate SNID: {ex.Message}")
                Return DefaultFallbackSnid
            End Try
        End Function

        ''' <summary>
        ''' Converts an alphanumeric Base-36 character into its corresponding decimal value.
        ''' </summary>
        ''' <param name="c">The character to convert ('0'-'9' or 'A'-'Z').</param>
        ''' <returns>
        ''' An integer value between 0 and 35 where '0'-'9' evaluate to 0-9 and 'A'-'Z' evaluate to 10-35.
        ''' </returns>
        Private Shared Function ConvertBase36CharToDecimal(c As Char) As Integer
            Dim upperChar As Char = Char.ToUpperInvariant(c)

            If Char.IsDigit(upperChar) Then
                Return AscW(upperChar) - AscW("0"c)
            ElseIf upperChar >= "A"c AndAlso upperChar <= "Z"c Then
                Return (AscW(upperChar) - AscW("A"c)) + 10
            End If

            Return 0
        End Function
    End Class
End Namespace