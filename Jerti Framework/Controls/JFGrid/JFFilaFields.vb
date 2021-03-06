﻿Namespace Controls.JFGrid
    Public Class JFFilaFields
        Property ID As String
        Property Display As String
        Property TypeField As JFControlType
        Property NameModel As String
        Property IsPrefix As Boolean

        'Propiedades para el tratamiento de campos Fuertemente Tipados
        Property IsTipado As Boolean = True
        Property Validaciones As IEnumerable(Of String)

        'Propiedad para las tablas de ReadOnly
        Property Value As Object

        'Propiedad para establecer botones como tercera o cuarta columna en la tabal
        Property IsAddButton As Boolean = False
        Property TextButton As String = ""
        Property OnlyIconButton As Boolean = False
        Property ClassIconButton As String = ""

        'Añadimos las nuevas Propiedades para la compatibilidad con BootStraps
        Property OcultarEtiqueta As Boolean
        Property MarcaAgua As String
        Property MaxCaracteres As Integer
        Property RejillaInForm As JFRejillaAttribute

        'Añadimos el nuevo proveedor de validaciones Unobstrive Native
        Property ValitacionesNative As String

        Property FileExtensions As String
    End Class
End Namespace
