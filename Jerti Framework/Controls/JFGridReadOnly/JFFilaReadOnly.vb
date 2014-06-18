
Namespace Controls.JFGridReadOnly
    Public Class JFFilaReadOnly

        Friend Property _fields As JFFilaFields
        Friend Property _options As JFOptionsFieldsReadOnly

        Public Sub New(fields As JFFilaFields)
            _fields = fields
            _options = New JFOptionsFieldsReadOnly
        End Sub

        Public Sub New(fields As JFFilaFields, options As JFOptionsFieldsReadOnly)
            _fields = fields
            _options = options
        End Sub

        Public Overrides Function ToString() As String
            Dim strBuilder As New StringBuilder,
                strValue As String

            'Creamos la fila
            strBuilder.Append(String.Format("<tr id=""row-{0}"" class=""j-fila-ro"">", _fields.ID))

            'Generamos la etiqueta del campo
            strBuilder.Append(String.Format("<td class=""j-cell-display-ro"" {1}>{0}</td>", _fields.Display, _options.getHTMLAttributes_Display))

            'Verificamos si tenemos que formatear la fecha
            Select Case _options.FormatField
                Case JFFormat.Date
                    strValue = Date.Parse(_fields.Value).ToString("dd/MM/yyyy")
                Case JFFormat.DateTime
                    strValue = DateTime.Parse(_fields.Value).ToString("dd/MMM/yyyy HH:mm tt")
                Case JFFormat.Moneda
                    strValue = Decimal.Parse(_fields.Value).ToString("{0:C}")
                Case JFFormat.Hora
                    strValue = DateTime.Parse(_fields.Value).ToString("HH:mm tt")
                Case Else
                    strValue = _fields.Value
            End Select

            'Generamos ahora el valor del campo
            strBuilder.Append(String.Format("<td class=""j-cell-value-ro"" {1}>{0}</td>", strValue, _options.getHTMLAttributes_Value))

            'Cerramos la etiqueta de la fila
            strBuilder.Append("</tr>")

            Return strBuilder.ToString
        End Function
    End Class
End Namespace
