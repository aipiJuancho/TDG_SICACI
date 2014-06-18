Namespace Controls
    Public Class JFTextPage
        Implements IHtmlString

        Private atributos As Object
        Private strBuilder As New StringBuilder

        Public Sub New(htmlAttributes As Object)
            Me.atributos = htmlAttributes

            'Creamos el div principal
            strBuilder.Append(String.Format("<div class=""text-1"" {0}>",
                                            JFOptionsFieldsReadOnly.ConvertDictionaryToStringHTML(atributos)))
        End Sub

        ''' <summary>
        ''' Agrega una nueva linea de texto
        ''' </summary>
        ''' <param name="text">Texto plano y HTML que desea agregar en al linea</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function AddLine(text As String) As JFTextPage
            strBuilder.Append("<div>") _
                .Append(text) _
                .Append("</div>")
            Return Me
        End Function

        Public Function ToHtmlString() As String Implements IHtmlString.ToHtmlString
            strBuilder.Append("</div>")
            Return strBuilder.ToString
        End Function
    End Class
End Namespace
