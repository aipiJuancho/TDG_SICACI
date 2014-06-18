Imports System.Runtime.CompilerServices
Imports System.Globalization
Imports System.Security
Imports JertiFramework.Security

Public Module JFExtensores
    Private ELVTextInfo As TextInfo = New CultureInfo("es-SV", False).TextInfo

    <Extension()> _
    Public Function ToInitCap(cadena As String) As String
        Return ELVTextInfo.ToTitleCase(cadena)
    End Function

    ''' <summary>
    ''' Convierte una cadena de texto encriptada a un SecureString desencriptado
    ''' </summary>
    ''' <param name="datosEncriptados">El texto encriptado que desea desencriptar y almacenarlo en un SecureString.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()> _
    Public Function ToSecureString(datosEncriptados As String) As SecureString
        Return JFEncriptacion.DesencriptarData(datosEncriptados)
    End Function

    ''' <summary>
    ''' Convierte un string seguro a un string inseguro sin encriptacion
    ''' </summary>
    ''' <param name="stringSeguro">Cadena seguro que se desea convertir a un tipo de datos String</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()> _
    Public Function ToStringInseguro(stringSeguro As SecureString) As String
        Return JFEncriptacion.ToStringInseguro(stringSeguro)
    End Function

    ''' <summary>
    ''' Protege una cadena de texto como parámetro de un método en un controlador para que no admita ataques de inyección SQL. Para ello se codifica la cadena utilizando la clase HTTPUtility del MVC 3.
    ''' </summary>
    ''' <param name="cadena">Cadena que se va a codificar para evitar ataques maliciosos</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()> _
    Public Function EncodeHTML(cadena As String) As String
        Return HttpUtility.HtmlEncode(cadena)
    End Function
End Module
