Imports System.Security
Imports System.Security.Cryptography
Imports System.Runtime.InteropServices
Imports System.Text

Namespace Security
    Public Class JFEncriptacion
        Private Shared entropy As Byte() = Encoding.Unicode.GetBytes("PZ1hfV0jvl3c5Ju74lcU")

        ''' <summary>
        ''' Funcion que encripta los datos en el archivo de recursos del sistema
        ''' </summary>
        ''' <param name="input">String el cual se desea encriptar</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function EncriptarData(input As SecureString) As String
            Dim encData As Byte() = ProtectedData.Protect(Encoding.Unicode.GetBytes(ToStringInseguro(input)), entropy, DataProtectionScope.LocalMachine)
            Return Convert.ToBase64String(encData)
        End Function

        ''' <summary>
        ''' Funcion para desencriptar los datos en el archivo de recursos del sistema
        ''' </summary>
        ''' <param name="datosencriptados"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function DesencriptarData(datosencriptados As String) As SecureString
            Try
                Dim decData As Byte() = ProtectedData.Unprotect(Convert.FromBase64String(datosencriptados), entropy, DataProtectionScope.LocalMachine)
                Return ToStringSeguro(Encoding.Unicode.GetString(decData))
            Catch ex As Exception
                Return New SecureString
            End Try
        End Function

        Public Shared Function ToStringSeguro(input As String) As SecureString
            Dim secure As New SecureString
            For Each c As Char In input
                secure.AppendChar(c)
            Next
            secure.MakeReadOnly()
            Return secure
        End Function

        Public Shared Function ToStringInseguro(input As SecureString) As String
            Dim retVal As String = String.Empty
            Dim ptr As IntPtr = Marshal.SecureStringToBSTR(input)
            Try
                retVal = Marshal.PtrToStringBSTR(ptr)

            Finally
                Marshal.ZeroFreeBSTR(ptr)
            End Try
            Return retVal
        End Function
    End Class
End Namespace
