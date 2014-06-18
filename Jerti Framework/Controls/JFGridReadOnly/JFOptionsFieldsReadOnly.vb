
Namespace Controls.JFGridReadOnly
    Public Class JFOptionsFieldsReadOnly

        Friend Property FormatField As JFFormat = JFFormat.Text
        Friend Property HTMLAttributesDisplay As Object
        Friend Property HTMLAttributesValues As Object

        Public Function SetFormat(formato As JFFormat) As JFOptionsFieldsReadOnly
            Me.FormatField = formato
            Return Me
        End Function

        Public Function SetHTMLAttributesDisplay(attr As Object) As JFOptionsFieldsReadOnly
            Me.HTMLAttributesDisplay = attr
            Return Me
        End Function

        Public Function SetHTMLAttributesValue(attr As Object) As JFOptionsFieldsReadOnly
            Me.HTMLAttributesValues = attr
            Return Me
        End Function

        Public Function getHTMLAttributes_Display() As String
            Return ConvertDictionaryToStringHTML(HTMLAttributesDisplay)
        End Function

        Public Function getHTMLAttributes_Value() As String
            Return ConvertDictionaryToStringHTML(HTMLAttributesValues)
        End Function

        Friend Shared Function ConvertDictionaryToStringHTML(obj As Object) As String
            Dim strBuilder As New StringBuilder

            'Lo primero que debemos hacer es observar si esta definido o no
            If obj Is Nothing Then Return ""

            'Como sabemos que el objeto tiene contenido, vamos a intentar convertir la estrucutra a un diccionario para poder acceder
            'a la clave como a su valor
            Dim _dictionaryDisplay = obj.GetType.GetProperties.ToDictionary(Function(a) a.Name,
                                                                            Function(a) a.GetValue(obj, Nothing))

            'Ahora recorremos todos los atributos definidos por el usuario y los regresamos concatenados
            For Each atributo In _dictionaryDisplay
                strBuilder.Append(String.Format("{0}=""{1}"" ", atributo.Key, atributo.Value.ToString))
            Next

            Return strBuilder.ToString
        End Function
    End Class
End Namespace
