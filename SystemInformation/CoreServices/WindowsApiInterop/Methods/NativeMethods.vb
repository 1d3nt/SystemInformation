Imports System.Runtime.InteropServices
Imports System.Security
Imports System.Text

Namespace CoreServices.WindowsApiInterop.Methods

    ''' <summary>
    ''' Provides methods for interacting with native Windows APIs. 
    ''' This class contains P/Invoke declarations for various functions used for process and token management.
    ''' </summary>
    ''' <remarks>
    ''' The <see cref="NativeMethods"/> class uses the <c>DllImport</c> attribute to define methods that are imported 
    ''' from unmanaged DLLs. These methods are used to interact with the Windows operating system at a low level.
    ''' 
    ''' The <c>SuppressUnmanagedCodeSecurity</c> attribute is applied to this class to improve performance when
    ''' calling unmanaged code. This attribute disables code access security checks for unmanaged code, which
    ''' can reduce overhead in performance-critical applications. Use this attribute with caution, as it bypasses
    ''' some of the security measures provided by the .NET runtime.
    ''' </remarks>
    <SuppressUnmanagedCodeSecurity>
    Friend NotInheritable Class NativeMethods

        ''' <summary>
        ''' Retrieves the type and data for the specified registry value.
        ''' </summary>
        ''' <param name="hkey">
        ''' A handle to an open registry key. The key must have been opened with the KEY_QUERY_VALUE access right.
        ''' This parameter is passed with the <c>[In]</c> attribute.
        ''' </param>
        ''' <param name="lpSubKey">
        ''' The path of a registry key relative to the key specified by the <paramref name="hkey"/> parameter.
        ''' The registry value will be retrieved from this subkey.
        ''' This parameter is passed with the <c>[In]</c> attribute.
        ''' </param>
        ''' <param name="lpValue">
        ''' The name of the registry value. If this parameter is <c>Nothing</c> or an empty string, 
        ''' the function retrieves the type and data for the key's unnamed or default value, if any.
        ''' This parameter is passed with the <c>[In]</c> attribute.
        ''' </param>
        ''' <param name="dwFlags">
        ''' The flags that restrict the data type of value to be queried. If the data type of the value does not 
        ''' match this criteria, the function fails.
        ''' This parameter is passed with the <c>[In]</c> attribute.
        ''' </param>
        ''' <param name="pdwType">
        ''' A pointer to a variable that receives a code indicating the type of data stored in the specified value.
        ''' This parameter can be <c>Nothing</c> if the type is not required.
        ''' This parameter is passed with the <c>[Out]</c> attribute.
        ''' </param>
        ''' <param name="pvData">
        ''' A pointer to a buffer that receives the value's data. 
        ''' This parameter can be <c>Nothing</c> if the data is not required.
        ''' This parameter is passed with the <c>[Out]</c> attribute.
        ''' </param>
        ''' <param name="pcbData">
        ''' A pointer to a variable that specifies the size of the buffer pointed to by the <paramref name="pvData"/> parameter, in bytes. 
        ''' When the function returns, this variable contains the size of data copied to <paramref name="pvData"/>.
        ''' This parameter is passed with the <c>[In, Out]</c> attribute.
        ''' </param>
        ''' <returns>
        ''' If the function succeeds, the return value is <c>ERROR_SUCCESS</c> (0). 
        ''' If the function fails, the return value is a system error code (such as <c>ERROR_FILE_NOT_FOUND</c> or <c>ERROR_MORE_DATA</c>).
        ''' </returns>
        ''' <remarks>
        ''' For more details, refer to the <see href="https://learn.microsoft.com/en-us/windows/win32/api/winreg/nf-winreg-reggetvaluew">RegGetValueW documentation</see> on Microsoft Learn.
        '''
        ''' The function signature in C++ is:
        ''' <code>
        ''' LSTATUS RegGetValueW(
        '''   [in]                HKEY    hkey,
        '''   [in, optional]      LPCWSTR lpSubKey,
        '''   [in, optional]      LPCWSTR lpValue,
        '''   [in, optional]      DWORD   dwFlags,
        '''   [out, optional]     LPDWORD pdwType,
        '''   [out, optional]     PVOID   pvData,
        '''   [in, out, optional] LPDWORD pcbData
        ''' );
        ''' </code>
        ''' </remarks>
        <DllImport(ExternDll.Advapi32, CharSet:=CharSet.Unicode, SetLastError:=True)>
        Friend Shared Function RegGetValue(
            <[In]> hkey As IntPtr,
            <[In]> lpSubKey As String,
            <[In]> lpValue As String,
            <[In]> dwFlags As UInteger,
            <[Out]> ByRef pdwType As UInteger,
            <[Out]> pvData As StringBuilder,
            <[In], [Out]> ByRef pcbData As UInteger
        ) As Integer
        End Function
    End Class
End Namespace
