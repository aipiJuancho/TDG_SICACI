Namespace Controls
    Public Class JFDialogPartialView
        Implements IDisposable

        Private _htmlHelper As htmlhelper

        Public Sub New(helper As HtmlHelper, urlSend As String, htmlAttributes As Object)
            Dim strBuilder As New StringBuilder
            Me._htmlHelper = helper
            strBuilder.Append(String.Format("<div {0}>", JFOptionsFieldsReadOnly.ConvertDictionaryToStringHTML(htmlAttributes))) _
                .Append(String.Format("<form action=""{0}"">", urlSend))

            'Establecemos el contexto de formulario en el ViewContext
            Me._htmlHelper.ViewContext.FormContext = New FormContext()

            'Escribimos el codigo HTML y JavaScript en la salida
            Me._htmlHelper.ViewContext.Writer.Write(strBuilder.ToString)
        End Sub


#Region "IDisposable Support"
        Private disposedValue As Boolean ' Para detectar llamadas redundantes

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    Me._htmlHelper.ViewContext.Writer.Write("</form></div>")
                End If
            End If
            Me.disposedValue = True
        End Sub

        ' Visual Basic agregó este código para implementar correctamente el modelo descartable.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
End Namespace
