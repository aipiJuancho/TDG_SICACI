Imports System.Linq.Expressions

Namespace Controls
    Public Class JFForm(Of T)
        Implements IHtmlString

        Private ReadOnly _htmlHelper As HtmlHelper(Of T)
        Private Property _titulo As String
        Private Property _IDForm As String
        Private Property _Grupos As New List(Of JFForm_Grupo)
        Private Property _botones As New List(Of JFFormButton)
        Private Property _botonDefault As String

        Public Sub New(htmlHelper As HtmlHelper(Of T), idFormulario As String, TituloForm As String)
            Me._htmlHelper = htmlHelper
            Me._IDForm = idFormulario
            Me._titulo = TituloForm
        End Sub

        Public Sub New(htmlHelper As HtmlHelper(Of T), idFormulario As String)
            Me._htmlHelper = htmlHelper
            Me._IDForm = idFormulario
            Me._titulo = String.Empty
        End Sub

        Public Function AddFieldFor(Of TProperty)(propiedad As Expression(Of Func(Of T, TProperty))) As JFForm(Of T)
            Dim jF = Me.GetMembersFields(propiedad)

            'Agregamos la nueva columna a la lista
            Me._Grupos.Add(New JFForm_Grupo(jF))
            Return Me
        End Function

        Public Function AddButton(btn As JFFormButton, Optional isDefault As Boolean = True) As JFForm(Of T)
            Me._botones.Add(btn)
            If isDefault Then
                Me._botonDefault = btn.GetName
            End If
            Return Me
        End Function

        Private Function GetMembersFields(Of TProperty)(propiedad As Expression(Of Func(Of T, TProperty))) As JFFilaFields
            Dim jF As New JFFilaFields
            Dim mMetaData As ModelMetadata = ModelMetadata.FromLambdaExpression(propiedad, _htmlHelper.ViewData)

            jF.IsTipado = True
            jF.ID = mMetaData.PropertyName
            jF.NameModel = mMetaData.PropertyName
            jF.Display = mMetaData.GetDisplayName
            jF.TypeField = mMetaData.AdditionalValues("TipoField")
            jF.IsPrefix = False
            jF.OcultarEtiqueta = mMetaData.AdditionalValues("hideLabel")
            jF.MarcaAgua = mMetaData.Watermark
            jF.MaxCaracteres = mMetaData.AdditionalValues("maxCaracteres")
            jF.RejillaInForm = mMetaData.AdditionalValues("rejillaInForms")

            'Recuperamos el valor del modelo
            jF.Value = mMetaData.Model

            'Determinamos si es control de texto para obtener las validaciones de los DataAnnotations
            Dim dictionaryValidaciones As IDictionary(Of String, Object) = Me._htmlHelper.GetUnobtrusiveValidationAttributes(mMetaData.PropertyName)
            jF.Validaciones = dictionaryValidaciones.Select(Function(a) String.Format("{0}=""{1}""", a.Key, a.Value.ToString)).ToArray
            'jF.ValitacionesNative = MaptoUnobtrusiveNative(dictionaryValidaciones)

            Return jF
        End Function

        Private Function MaptoUnobtrusiveNative(reglas As IDictionary(Of String, Object)) As String
            Dim strBuilder As New StringBuilder
            For Each regla In reglas
                If regla.Key = "data-val-required" Then     'REQUIRED ATTRIBUTE
                    strBuilder.Append("data-rule-required=""true"" ")
                    strBuilder.Append(String.Format("data-msg-required=""{0}"" ", regla.Value.ToString))
                End If
            Next

            Return strBuilder.ToString
        End Function

        Private Function RenderScript() As String
            Dim strBuilder As New StringBuilder
            strBuilder.Append("<script type=""text/javascript"">")
            strBuilder.Append("$(function () {")

            'Activamos al validacion para el formulario
            strBuilder.Append(String.Format("var $form = $('{0}');", Me._IDForm))
            strBuilder.Append(String.Format("$.validator.unobtrusive.parseDynamicContent('{0}');", Me._IDForm))

           'Verificamos si existe un control del tipo FILE
            If Me._Grupos.Where(Function(m) m._Fields.TypeField = JFControlType.File).Count() > 0 Then
                strBuilder.Append("$form.attr('enctype', 'multipart/form-data');")

                'Ahora verificamos si hay un boton establecido por DEFAULT para enviar los datos y hacer los ajustes necesarios
                If Not String.IsNullOrEmpty(Me._botonDefault) Then
                    strBuilder.Append(String.Format("$('#{0}').on('click', function(event) {1}", Me._botonDefault, "{"))
                    strBuilder.Append("event.preventDefault();")
                    strBuilder.Append("$form.sendForm({")
                    strBuilder.Append("overwriteData: new FormData($form.get(0)),")
                    strBuilder.Append("contentType: false,")
                    strBuilder.Append("processData: false")
                    strBuilder.Append("});")
                    strBuilder.Append("});")
                End If
            Else
                'Verificamos si hemos establecido un boton por defecto para mandar los datos
                If Not String.IsNullOrEmpty(Me._botonDefault) Then _
                    strBuilder.Append(String.Format("$('#{0}').on('click', $.handlerSendFormToController);", Me._botonDefault, "{", "}"))
            End If

            'Cerramos el script para finalizarlo
            strBuilder.Append("});")
            strBuilder.Append("</script>")

            Return strBuilder.ToString
            'Return String.Empty
        End Function

        Public Overrides Function ToString() As String
            Dim strBuilder As New StringBuilder

            'Vamos a recorrer cada una de las Grupos que hemos agregado
            For Each f In Me._Grupos
                strBuilder.Append(f.ToString)
            Next

            'Vamos agregar cada uno de los botones que hemos añadido al formulario
            For Each b In Me._botones
                strBuilder.Append(b.ToHtmlString)
            Next

            strBuilder.Append(Me.RenderScript())

            Return strBuilder.ToString
        End Function

        Public Function ToHtmlString() As String Implements IHtmlString.ToHtmlString
            Return Me.ToString
        End Function
    End Class
End Namespace
