Imports System.Linq.Expressions
Imports System.Reflection
Imports System.Web

Namespace Controls.JFGrid
    Public Class JFGrid(Of T)
        Implements IHtmlString

        Private ReadOnly _htmlHelper As HtmlHelper(Of T)
        Private ReadOnly _filas As New List(Of JFFila)
        Private Property _titulo As String
        Private Property _widthTable As Integer = 0
        Private Property _botones As New List(Of JFButton)
        Private Property _IDDefaultButtonSend As String
        Private Property _IDForm As String
        Private Property _aditionalScript As New StringBuilder
        Private Property _HtmlAttributes As String = ""
        Private Property _prefixRows As String = ""
        Private Property _sinValidacionScript As Boolean = False

        Public Sub New(htmlHelper As HtmlHelper(Of T), titulo As String)
            Me.New(htmlHelper, String.Empty, titulo)
            Me._sinValidacionScript = True
        End Sub

        Public Sub New(htmlHelper As HtmlHelper(Of T), IDForm As String, titulo As String)
            'If String.IsNullOrWhiteSpace(titulo) Then Throw New ArgumentException("El parametro de titulo no puede estar en blanco o con espacios en blanco")
            Me._htmlHelper = htmlHelper
            Me._titulo = titulo
            Me._IDForm = IDForm
        End Sub

        Public Sub New(htmlHelper As HtmlHelper(Of T), IDForm As String, titulo As String, prefix As String)
            Me.New(htmlHelper, IDForm, titulo)
            Me._prefixRows = prefix
        End Sub

        ''' <summary>
        ''' Agrega una nueva fila con los parámetros especificados del control que se desea crear.
        ''' </summary>
        ''' <param name="name">Identificador único que va a recibir el control HTML para poder ser llamado a través de JavaScript</param>
        ''' <param name="label">Etiqueta que se colocara como la descripción del campo en la tabla de controles</param>
        ''' <param name="tipo">El tipo de control que será esta nueva fila</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function AddField(name As String, label As String, tipo As JFControlType) As JFGrid(Of T)
            Dim jControl As New JFFilaFields
            jControl.Display = label
            jControl.ID = name
            jControl.IsPrefix = False
            jControl.IsTipado = False
            jControl.NameModel = name
            jControl.TypeField = tipo
            jControl.Validaciones = New String() {}

            'Agregamos la nueva columna a la lista
            _filas.Add(New JFFila(jControl))
            Return Me
        End Function

        ''' <summary>
        ''' Agrega una nueva fila con los parámetros especificados del control que se desea crear y podemos adicionar opciones extras.
        ''' </summary>
        ''' <param name="name">Identificador único que va a recibir el control HTML para poder ser llamado a través de JavaScript</param>
        ''' <param name="label">Etiqueta que se colocara como la descripción del campo en la tabla de controles</param>
        ''' <param name="tipo">El tipo de control que será esta nueva fila</param>
        ''' <param name="options">Opciones adicionales para el tipo de control que hemos especificado en la fila</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function AddField(name As String, label As String, tipo As JFControlType, options As JFOptionsFields) As JFGrid(Of T)
            Dim jControl As New JFFilaFields
            jControl.Display = label
            jControl.ID = name
            jControl.IsPrefix = False
            jControl.IsTipado = False
            jControl.NameModel = name
            jControl.TypeField = tipo
            jControl.Validaciones = New String() {}

            'Agregamos la nueva columna a la lista
            _filas.Add(New JFFila(jControl, options))
            Return Me
        End Function

        Public Function AddField(name As String, label As String, tipo As JFControlType, value As Object, options As JFOptionsFields) As JFGrid(Of T)
            Dim jControl As New JFFilaFields
            jControl.Display = label
            jControl.ID = name
            jControl.IsPrefix = False
            jControl.IsTipado = False
            jControl.NameModel = name
            jControl.TypeField = tipo
            jControl.Value = value
            jControl.Validaciones = New String() {}

            'Agregamos la nueva columna a la lista
            _filas.Add(New JFFila(jControl, options))
            Return Me
        End Function

        ''' <summary>
        ''' Agrega una nueva fila con los parámetros especificados del control que se desea crear y podemos adicionar opciones extras.
        ''' </summary>
        ''' <param name="name">Identificador único que va a recibir el control HTML para poder ser llamado a través de JavaScript</param>
        ''' <param name="label">Etiqueta que se colocara como la descripción del campo en la tabla de controles</param>
        ''' <param name="tipo">El tipo de control que será esta nueva fila</param>
        ''' <param name="options">Opciones adicionales para el tipo de control que hemos especificado en la fila</param>
        ''' <param name="textButton">Texto que se colocara en el botón que se agregara al lado derecho del control de entrada.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function AddField(name As String, label As String, tipo As JFControlType, textButton As String, options As JFOptionsFields) As JFGrid(Of T)
            Dim jControl As New JFFilaFields
            jControl.Display = label
            jControl.ID = name
            jControl.IsPrefix = False
            jControl.IsTipado = False
            jControl.NameModel = name
            jControl.TypeField = tipo
            jControl.IsAddButton = True
            jControl.TextButton = textButton
            jControl.Validaciones = New String() {}

            'Agregamos la nueva columna a la lista
            _filas.Add(New JFFila(jControl, options))
            Return Me
        End Function


        Public Function AddFieldFor(Of TProperty)(propiedad As Expression(Of Func(Of T, TProperty))) As JFGrid(Of T)
            Dim jF = Me.GetMembersFields(propiedad)

            'Agregamos la nueva columna a la lista
            _filas.Add(New JFFila(jF))
            Return Me
        End Function

        Public Function AddFieldFor(Of TProperty)(propiedad As Expression(Of Func(Of T, TProperty)), options As JFOptionsFields) As JFGrid(Of T)
            Dim jF = Me.GetMembersFields(propiedad)

            'Agregamos la nueva columna a la lista
            _filas.Add(New JFFila(jF, options))
            Return Me
        End Function

        Public Function SetHTMLAttributes(attr As Object) As JFGrid(Of T)
            Me._HtmlAttributes = ConvertDictionaryToStringHTML(attr)
            Return Me
        End Function

        Private Function GetMembersFields(Of TProperty)(propiedad As Expression(Of Func(Of T, TProperty))) As JFFilaFields
            Dim jF As New JFFilaFields
            Dim mMetaData As ModelMetadata = ModelMetadata.FromLambdaExpression(propiedad, _htmlHelper.ViewData)


            jF.IsTipado = True
            jF.ID = If(String.IsNullOrWhiteSpace(Me._prefixRows), mMetaData.PropertyName, String.Format("{0}-{1}", _prefixRows, mMetaData.PropertyName))
            jF.NameModel = mMetaData.PropertyName
            jF.Display = mMetaData.GetDisplayName
            jF.TypeField = mMetaData.AdditionalValues("TipoField")
            jF.IsPrefix = If(String.IsNullOrWhiteSpace(Me._prefixRows), False, True)

            'Recuperamos el valor del modelo
            jF.Value = mMetaData.Model

            'Determinamos si es control de texto para obtener las validaciones de los DataAnnotations
            'Determinamos si es control de texto para obtener las validaciones de los DataAnnotations
            Dim dictionaryValidaciones As IDictionary(Of String, Object) = Me._htmlHelper.GetUnobtrusiveValidationAttributes(mMetaData.PropertyName)
            jF.Validaciones = dictionaryValidaciones.Select(Function(a) String.Format("{0}=""{1}""", a.Key, a.Value.ToString)).ToArray
            Return jF
        End Function

        Public Function SetTableWidth(width As Integer) As JFGrid(Of T)
            If width <= 0 Then Throw New ArgumentException("El ancho de la tabla tiene que ser mayor a cero")
            Me._widthTable = width
            Return Me
        End Function

        Public Function AddButtonDefault(boton As JFButton) As JFGrid(Of T)
            Me._IDDefaultButtonSend = boton._ID
            Me._botones.Add(boton.SetIDForm(Me._IDForm))
            Return Me
        End Function

        Public Function AddJavaScript(script As String) As JFGrid(Of T)
            Me._aditionalScript.Append(script)
            Return Me
        End Function

        Public Function RenderScript(Optional aditionalScript As String = "") As String
            Dim strBuilder As New StringBuilder
            strBuilder.Append("<script type=""text/javascript"">")
            strBuilder.Append("$(function () {")


            'Verificamos si debemos aplicar la validacion al contenido o no
            If Not Me._sinValidacionScript Then
                strBuilder.Append(String.Format("var $form = $('{0}');", Me._IDForm))

                'Activamos al validacion para el formulario
                strBuilder.Append(String.Format("$.validator.unobtrusive.parseDynamicContent('{0}');", Me._IDForm))
            End If

            'Ahora verificamos si alguna fila de las que hemos agregado corresponde a un DatePicker para agregar el script
            If Me._filas.Where(Function(f) f.IsDatePicker = True).Count > 0 Then _
                strBuilder.Append(RenderDefaultsDatePickerJavaScript())

            'Ahora verificamos si alguna fila de las que hemos agregado corresponde a un TimePicker para agregar el script de idioma
            If Me._filas.Where(Function(f) f.IsTimePicker = True).Count > 0 Then _
                strBuilder.Append(RenderDefaultsTimePickerJavaScript())

            'Verificamos si hemos establecido un boton por defecto para mandar los datos
            If Not String.IsNullOrEmpty(Me._IDDefaultButtonSend) Then _
                strBuilder.Append(String.Format("$('#{0}').on('click', $.handlerSendFormToController);", Me._IDDefaultButtonSend, "{"))

            'Agregamos al script de la tabla, script adicional que se haya especificado en la propiedad
            strBuilder.Append(_aditionalScript.ToString)

            'Agregamos script que se haya pasado a travez de los parametros de la funcion
            strBuilder.Append(aditionalScript)

            'Cerramos el script para finalizarlo
            strBuilder.Append("});")
            strBuilder.Append("</script>")

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

        Private Function RenderDefaultsTimePickerJavaScript() As String
            Dim strBuilder As New StringBuilder

            'Establecemos los valores que tendra los cuadro de dialogo del control DatePicker de jQueryUI
            strBuilder.Append("$.timepicker.regional['es'] = {") _
                .Append("timeOnlyTitle: 'Elegir una hora',") _
                .Append("timeText: 'Hora',") _
                .Append("hourText: 'Horas',") _
                .Append("minuteText: 'Minutos',") _
                .Append("secondText: 'Segundos',") _
                .Append("millisecText: 'Milisegundos',") _
                .Append("timezoneText: 'Huso horario',") _
                .Append("currentText: 'Ahora',") _
                .Append("closeText: 'Cerrar',") _
                .Append("timeFormat: 'hh:mm tt',") _
                .Append("amNames: ['a.m.', 'AM', 'A'],") _
                .Append("pmNames: ['p.m.', 'PM', 'P'],") _
                .Append("ampm: true") _
                .Append("};") _
                .Append("$.timepicker.setDefaults($.timepicker.regional['es']);")

            Return strBuilder.ToString
        End Function

        Public Overrides Function ToString() As String
            Dim strBuilder As New StringBuilder,
                strScriptFields As New StringBuilder,
                widthControl As String

            'Comprobamos si se ha dado un tamaño para la tabla
            widthControl = If(Me._widthTable.Equals(0), "100%", String.Format("{0}px", Me._widthTable))

            strBuilder.Append(String.Format("<fieldset {0}><legend>", _HtmlAttributes)) _
                .Append(Me._titulo) _
                .Append(String.Format("</legend><table class=""content-form"" style=""width: {0};"">", widthControl)) _
                .Append("<tbody>")

            'Vamos a recorrer cada una de las filas que hemos agregado
            For Each f In _filas
                strBuilder.Append(f.ToString)

                'Añadimos a la lista de script si el item posee script
                strScriptFields.Append(f.GetJavaScriptField)
            Next

            strBuilder.Append("</tbody></table>")

            'Ahora verificamos si se agrega algun panel de botones o no al formulario
            If Me._botones.Count > 0 Then
                strBuilder.Append(String.Format("<div class=""j-panel-buttons"" style=""width: {0};"">", widthControl)) _
                    .Append("<div class=""j-bar"">")

                'Recorremos cada uno de los botones
                For Each btn In Me._botones
                    strBuilder.Append(btn.ToString)
                Next

                'Cerramos la etiqueta de los paneles que acabamos de agregar
                strBuilder.Append("</div>") _
                    .Append("</div>")
            End If

            strBuilder.Append("</fieldset>")

            'Por ultimo, renderizamos el Script de los datos
            strBuilder.Append(Me.RenderScript(strScriptFields.ToString))

            Return strBuilder.ToString
        End Function

        Public Function ToHtmlString() As String Implements IHtmlString.ToHtmlString
            Return Me.ToString
        End Function

        Private Function ConvertDictionaryToStringHTML(obj As Object) As String
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
