﻿Imports System.Linq.Expressions

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

        Public Function AddFieldFor(Of TProperty)(propiedad As Expression(Of Func(Of T, TProperty)), textButton As String) As JFForm(Of T)
            Dim jF = Me.GetMembersFields(propiedad)
            jF.IsAddButton = True
            jF.TextButton = textButton

            'Agregamos la nueva columna a la lista
            Me._Grupos.Add(New JFForm_Grupo(jF))
            Return Me
        End Function

        Public Function AddFieldFor(Of TProperty)(propiedad As Expression(Of Func(Of T, TProperty)), textButton As String, classIconButton As String, OnlyIconButton As Boolean) As JFForm(Of T)
            Dim jF = Me.GetMembersFields(propiedad)
            jF.IsAddButton = True
            jF.TextButton = textButton
            jF.ClassIconButton = classIconButton
            jF.OnlyIconButton = OnlyIconButton

            'Agregamos la nueva columna a la lista
            Me._Grupos.Add(New JFForm_Grupo(jF))
            Return Me
        End Function

        Public Function AddFieldFor(Of TProperty)(propiedad As Expression(Of Func(Of T, TProperty)), textButton As String, classIconButton As String, OnlyIconButton As Boolean, options As JFOptionsFields) As JFForm(Of T)
            Dim jF = Me.GetMembersFields(propiedad)
            jF.IsAddButton = True
            jF.TextButton = textButton
            jF.ClassIconButton = classIconButton
            jF.OnlyIconButton = OnlyIconButton

            'Agregamos la nueva columna a la lista
            Me._Grupos.Add(New JFForm_Grupo(jF, options))
            Return Me
        End Function

        Public Function AddFieldFor(Of TProperty)(propiedad As Expression(Of Func(Of T, TProperty)), options As JFOptionsFields) As JFForm(Of T)
            Dim jF = Me.GetMembersFields(propiedad)

            'Agregamos la nueva columna a la lista
            Me._Grupos.Add(New JFForm_Grupo(jF, options))
            Return Me
        End Function

        Public Function AddFieldFor(Of TProperty)(propiedad As Expression(Of Func(Of T, TProperty)), optionsMultipleSelect As JFMultipleSelect_Options) As JFForm(Of T)
            Dim jF = Me.GetMembersFields(propiedad)

            'Agregamos la nueva columna a la lista
            Me._Grupos.Add(New JFForm_Grupo(jF, optionsMultipleSelect))
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
            jF.FileExtensions = mMetaData.AdditionalValues("FileExtension")

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

        Private Function RenderScript(Optional aditionalScript As String = "") As String
            Dim strBuilder As New StringBuilder
            strBuilder.Append("<script type=""text/javascript"">")
            strBuilder.Append("$(function () {")

            'Activamos al validacion para el formulario
            strBuilder.Append(String.Format("var $form = $('{0}');", Me._IDForm))
            strBuilder.Append(String.Format("$.validator.unobtrusive.parseDynamicContent('{0}');", Me._IDForm))

            'Ahora verificamos si alguna fila de las que hemos agregado corresponde a un DatePicker para agregar el script
            If Me._Grupos.Where(Function(f) f.IsDatePicker = True).Count > 0 Then _
                strBuilder.Append(RenderDefaultsDatePickerJavaScript())

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

            'Verificamos si existe un control del tipo MultipleSelect para activarlo de manera automatica
            If Me._Grupos.Where(Function(m) m._Fields.TypeField = JFControlType.MultipleSelect).Count() > 0 Then
                strBuilder.Append("$('.selectpicker').selectpicker();")
            End If

            'Verificamos si alguno de los items MultipleSelect se encuentra en modo edición
            For Each item As JFForm_Grupo In Me._Grupos.Where(Function(m) m._Fields.TypeField = JFControlType.MultipleSelect)
                If item._optionsMultipleSelect.IsEdit AndAlso item._optionsMultipleSelect.ItemsSelected.Count() > 0 Then
                    strBuilder.Append(String.Format("$('#{0}').selectpicker('val', ['{1}']);", item._Fields.ID,
                                                    String.Join("','", item._optionsMultipleSelect.ItemsSelected)))
                End If
            Next

            'Agregamos script que se haya pasado a travez de los parametros de la funcion
            strBuilder.Append(aditionalScript)

            'Cerramos el script para finalizarlo
            strBuilder.Append("});")
            strBuilder.Append("</script>")

            Return strBuilder.ToString
            'Return String.Empty
        End Function

        Public Overrides Function ToString() As String
            Dim strBuilder As New StringBuilder,
                strScriptFields As New StringBuilder

            'Vamos a recorrer cada una de las Grupos que hemos agregado
            For Each f In Me._Grupos
                strBuilder.Append(f.ToString)

                'Añadimos a la lista de script si el item posee script
                strScriptFields.Append(f.GetJavaScriptField)
            Next

            'Vamos agregar cada uno de los botones que hemos añadido al formulario
            For Each b In Me._botones
                strBuilder.Append(b.ToHtmlString)
            Next

            strBuilder.Append(Me.RenderScript(strScriptFields.ToString))

            Return strBuilder.ToString
        End Function

        Private Function RenderDefaultsDatePickerJavaScript() As String
            Dim strBuilder As New StringBuilder

            'Establecemos los valores que tendra los cuadro de dialogo del control DatePicker de jQueryUI
            strBuilder.Append("$.datepicker.regional['es'] = {") _
                .Append("closeText: 'Cerrar',") _
                .Append("prevText: '<Ant',") _
                .Append("nextText: 'Sig>',") _
                .Append("currentText: 'Hoy',") _
                .Append("monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],") _
                .Append("monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],") _
                .Append("dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],") _
                .Append("dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],") _
                .Append("dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],") _
                .Append("weekHeader: 'Sm',") _
                .Append("dateFormat: 'dd/mm/yy',") _
                .Append("firstDay: 0,") _
                .Append("isRTL: false,") _
                .Append("showMonthAfterYear: false,") _
                .Append("yearSuffix: ''") _
                .Append("};") _
                .Append(" $.datepicker.setDefaults($.datepicker.regional['es']);")


            Return strBuilder.ToString
        End Function

        Public Function ToHtmlString() As String Implements IHtmlString.ToHtmlString
            Return Me.ToString
        End Function
    End Class
End Namespace
