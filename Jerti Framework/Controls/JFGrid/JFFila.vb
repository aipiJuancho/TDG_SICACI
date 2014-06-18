Imports System.Linq.Expressions
Imports System.Reflection

Namespace Controls.JFGrid
    Public Class JFFila
        Implements IHtmlString

        Private _IsDatePicker As Boolean = False
        Private _IsTimePicker As Boolean = False

        Protected Property _Fields As JFFilaFields
        Protected Property _Options As JFOptionsFields = Nothing
        Property GetJavaScriptField As New StringBuilder

        ReadOnly Property IsDatePicker As Boolean
            Get
                Return _IsDatePicker
            End Get
        End Property

        ReadOnly Property IsTimePicker As Boolean
            Get
                Return _IsTimePicker
            End Get
        End Property

        Public Sub New(fields As JFFilaFields)
            Me._Fields = fields
        End Sub

        Public Sub New(fields As JFFilaFields, options As JFOptionsFields)
            Me.New(fields)
            Me._Options = options
        End Sub

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

        Public Overrides Function ToString() As String
            Dim strBuilder As New StringBuilder,
                strControl As String = String.Empty,
                extendedPropertys As String = String.Empty,
                aditionalCells As New StringBuilder,
                classReadOnly As String = String.Empty

            'Creamos la fila
            strBuilder.Append("<tr class=""j-fila"">")

            'Generamos la estructura del Label o etiqueta del campo
            strBuilder.Append("<td class=""j-lbl-cell"">") _
                .Append(String.Format("<div class=""j-cont-label {0}"">", classReadOnly)) _
                .Append(String.Format("<label for=""{0}"">{1}</label>", Me._Fields.ID, Me._Fields.Display)) _
                .Append("</div>") _
                .Append("</td>")

            'Preparamos las Propiedades Extendidas que pueda tener el control
            If Me._Options IsNot Nothing Then extendedPropertys = Me._Options.ToString

            'Creamos la etiqueta del control dependiendo del tipo especificado
            Select Case Me._Fields.TypeField
                Case JFControlType.Text
                    strControl = String.Format("<input {0}{2} id=""{1}"" name=""{1}"" data-jerti-prefix=""{4}"" data-jerti-name=""{3}"" type=""text"" value=""{5}"">",
                                               String.Join(" ", Me._Fields.Validaciones),
                                               Me._Fields.ID,
                                               extendedPropertys,
                                               Me._Fields.NameModel,
                                               Me._Fields.IsPrefix,
                                               If(Me._Options.IsEdit, String.Format("{0:" & Me._Options.Formato & "}", Me._Fields.Value), String.Empty))

                    'Verificamos si se va a crear un boton al lado izquierdo del control, si es asi añadimos la nueva celda
                    If Me._Fields.IsAddButton Then
                        aditionalCells.Append("<td class=""j-button-cell"">") _
                            .Append(String.Format("<button id=""btn-{0}"" class=""jerti-button"">", Me._Fields.ID)) _
                            .Append(String.Format("<span>{0}</span>", Me._Fields.TextButton)) _
                            .Append("</button>") _
                            .Append("</div>") _
                            .Append("</td>")
                    End If
                Case JFControlType.Password
                    strControl = String.Format("<input {0}{2} id=""{1}"" name=""{1}"" data-jerti-prefix=""{4}"" data-jerti-name=""{3}"" type=""password"" value=""{5}"">",
                                               String.Join(" ", Me._Fields.Validaciones),
                                               Me._Fields.ID,
                                               extendedPropertys,
                                               Me._Fields.NameModel,
                                               Me._Fields.IsPrefix,
                                               If(Me._Options.IsEdit, String.Format("{0:" & Me._Options.Formato & "}", Me._Fields.Value), String.Empty))
                Case JFControlType.ReadOnly
                    strControl = String.Format("<label id=""value-{0}"" data-jerti-prefix=""{2}"" data-jerti-name=""{1}"" style=""margin: 2px 1px;"">{3}</label>",
                                               Me._Fields.ID,
                                               Me._Fields.NameModel,
                                               Me._Fields.IsPrefix,
                                               If(Me._Options.IsEdit, String.Format("{0:" & Me._Options.Formato & "}", Me._Fields.Value), String.Empty))

                Case JFControlType.ComboBox
                    Dim builderComboBox As New StringBuilder,
                        optComboBox As New StringBuilder

                    'Iniciamos los parametros
                    optComboBox.Append("{")

                    'Consultamos si el usuario definío un conjunto de datos estáticos para el ComboBox
                    If Not Me._Options.listValues_Items.Count.Equals(0) AndAlso Me._Options.listValues_Items IsNot Nothing Then
                        'Creamos el objeto SELECT
                        builderComboBox.Append(String.Format("<select id=""{0}"" name=""{0}"" {1}>",
                                                             Me._Fields.ID,
                                                             String.Join(" ", Me._Fields.Validaciones)))

                        'Ahora vamos agregar uno por uno cada uno de los items que el usuario paso
                        For Each opt In Me._Options.listValues_Items
                            If Me._Options.IsEdit AndAlso opt.Value = Me._Fields.Value Then
                                builderComboBox.Append(String.Format("<option value=""{0}"" selected=""selected"">{1}</option>", opt.Value, opt.Text))
                            Else
                                builderComboBox.Append(String.Format("<option value=""{0}"">{1}</option>", opt.Value, opt.Text))
                            End If
                        Next
                    Else
                        'Significa que los datos se van a cargar de manera remota, por lo tanto verificamos que se haya definido
                        'un metodo que sea el Source para cargar los items del ComboBox, si no esta definido lanzamos una excepcion
                        If String.IsNullOrWhiteSpace(Me._Options.loadURL) Then _
                            Throw New ArgumentNullException("No se puede crear el ComboBox para el campo, debido a que no se ha definido el método donde se cargaran los items")

                        'Creamos el objeto SELECT
                        builderComboBox.Append(String.Format("<select id=""{0}"" name=""{0}"" data-jerti-loadItems=""{2}"" data-jerti-loading=""#j-load-{0}"" {1}>",
                                                             Me._Fields.ID,
                                                             String.Join(" ", Me._Fields.Validaciones),
                                                             Me._Options.loadURL))

                        'Determinamos si mostramos la animacion cuando se carga la pagina directamente
                        Dim displayLoading = If(Me._Options.loadOnReady_ComboBox, String.Empty, " display: none")

                        'Definimos la nueva celda donde llevara la animacion cuando se estan cargando los datos desde el servidor
                        aditionalCells.Append("<td class=""j-loading-cell"" style=""width: 25px; padding-left: 10px;"">")
                        aditionalCells.Append(String.Format("<div class=""medium-icon loading-medium-icon"" id=""j-load-{0}"" style=""{1}"">", Me._Fields.ID, displayLoading))
                        aditionalCells.Append("</div>")
                        aditionalCells.Append("</td>")

                        'Antes de generar el codigo JavaScript para el ComboBox, verificamos si se ha definido algun item por defecto
                        If Not String.IsNullOrWhiteSpace(Me._Options.ItemTextDefault) Then _
                            optComboBox.Append(String.Format("firstItem: '{0}'", Me._Options.ItemTextDefault))

                        'Cerramos las opciones de los parametros JavaScript del ComboBox
                        optComboBox.Append("}")

                        'Por ultimo, verificamos si tenemos que generar el Codigo de JavaScript para que se carge automaticamente
                        'los itmes delo ComboBox cuando se cargue la pagina.
                        If Me._Options.loadOnReady_ComboBox Then _
                            GetJavaScriptField.Append(String.Format("$('#{0}').loadComboBox({1});", Me._Fields.ID, optComboBox.ToString))
                    End If

                    'Cerramos el BuilderCombobox
                    builderComboBox.Append("</select>")

                    'Retornamos el DropDown que generamos
                    strControl = builderComboBox.ToString

                Case JFControlType.RadioButton
                    Dim builderRadioButtons As New StringBuilder,
                        strSelect As String

                    'Creamos al contenedor de cada uno de los botones
                    builderRadioButtons.Append("<div class=""j-container-radiobuttons"">")

                    'Ahora vamos a recorrer el listado de items que nos ha pasado el usuario
                    For Each item In Me._Options.listValues_Items
                        builderRadioButtons.Append("<span>")

                        'Determinamos si este item sera seleccionado
                        If Not Me._Options.IsEdit Then
                            strSelect = If(item.Selected, " checked=""checked""", String.Empty)
                        Else
                            'Sobreescribimos el item seleccionado, si se ha especificado que es un campo de edicion
                            strSelect = If(item.Value = Me._Fields.Value, " checked=""checked""", String.Empty)
                        End If

                        'Creamos el item
                        builderRadioButtons.Append(String.Format("<input{0} id=""{1}"" name=""{1}"" type=""radio"" value=""{2}"">",
                                                                 strSelect, Me._Fields.ID, item.Value))

                        'Agregamos la eqtiqueta de texto al item
                        builderRadioButtons.Append(item.Text)
                        builderRadioButtons.Append("</span>")
                    Next

                    'Cerramos el contenedor de los radio
                    builderRadioButtons.Append("</div>")

                    strControl = builderRadioButtons.ToString

                Case JFControlType.Fecha
                    strControl = String.Format("<input {0}{2} id=""{1}"" name=""{1}"" type=""text"" value=""{3}"">",
                                               String.Join(" ", Me._Fields.Validaciones),
                                               Me._Fields.ID,
                                               extendedPropertys,
                                               If(Me._Options.IsEdit, String.Format("{0:" & Me._Options.Formato & "}", Me._Fields.Value), String.Empty))

                    'Activamos el Script del DatePicker por defecto
                    Me._IsDatePicker = True

                    'Ahora debemos de agregar el codigo JavaScript para asociarlo al calendario de jQueryUI
                    GetJavaScriptField.Append(String.Format("$('#{0}').datepicker(", Me._Fields.ID)) _
                        .Append("{") _
                        .Append(Me._Options.AddOptions) _
                        .Append("});")

                Case JFControlType.AutocompleteWithCache
                    Dim builderAC As New StringBuilder

                    'Primero vamos a crear el elemento SELECT que sera nuestro ComboBox que tendra todos nuestros datos, con la
                    'diferencia que estara oculto
                    builderAC.Append(String.Format("<input {3} id=""{0}"" type=""hidden"" name=""{0}"" data-jerti-input=""#j-ac-extend-{0}"" data-jerti-loadItems=""{1}"" data-jerti-loading=""#j-load-{0}"" data-jerti-minparam=""{2}"">",
                                                   Me._Fields.ID, Me._Options.loadURL, Me._Options.minLenghtAC, String.Join(" ", Me._Fields.Validaciones)))

                    'Definimos la nueva celda donde llevara la animacion cuando se estan cargando los datos desde el servidor
                    aditionalCells.Append("<td class=""j-loading-cell"" style=""width: 25px; padding-left: 10px;"">")
                    aditionalCells.Append(String.Format("<div class=""medium-icon loading-medium-icon"" id=""j-load-{0}"">", Me._Fields.ID))
                    aditionalCells.Append("</div>")
                    aditionalCells.Append("</td>")

                    'Asignamos el codigo JavaScript para cargar los datos en el ComboBox
                    GetJavaScriptField.Append(String.Format("$('#{0}').loadArrayAutoComplete();", Me._Fields.ID))

                    'Ahora crearemos el anexo del control para realizar el AutoComplete
                    builderAC.Append("<span class=""j-container-autocomplete-combobox"">") _
                        .Append(String.Format("<input {0} id=""j-ac-extend-{1}"" value=""{3}"" disabled=""disabled"" data-jerti-parent=""#{1}"" {2}>",
                                              String.Empty,
                                              Me._Fields.ID,
                                              extendedPropertys,
                                              If(Me._Options.IsEdit, String.Format("{0:" & Me._Options.Formato & "}", Me._Fields.Value), String.Empty))) _
                        .Append("</span>")

                    'Ahora creamos el link que tendra el aspecto de boton
                    'builderAC.Append(String.Format("<button id=""j-ac-button-{0}"" class=""j-btn-autocomplete ui-state-default""><span class=""icon ui-icon ui-icon-triangle-1-s"">H</span></button>", Me._Fields.ID))

                    strControl = builderAC.ToString
                Case JFControlType.Autocomplete
                    Dim builderAC As New StringBuilder

                    'Creamos el control INPUT que sera el encargado de almacenar el verdadero valor que enviaremos. Este estara oculto
                    builderAC.Append(String.Format("<input {1} id=""{0}"" type=""hidden"" name=""{0}"" data-jerti-input=""#j-ac-extend-{0}"">",
                                                   Me._Fields.ID, String.Join(" ", Me._Fields.Validaciones)))

                    'Definimos la nueva celda donde llevara la animacion cuando se estan cargando los datos desde el servidor
                    aditionalCells.Append("<td class=""j-loading-cell"" style=""width: 25px; padding-left: 10px;"">")
                    aditionalCells.Append(String.Format("<div class=""medium-icon loading-medium-icon"" style=""display: none;"" id=""j-load-{0}"">", Me._Fields.ID))
                    aditionalCells.Append("</div>")
                    aditionalCells.Append("</td>")

                    'Ahora crearemos el anexo del control para realizar el AutoComplete
                    builderAC.Append("<span class=""j-container-autocomplete-combobox"">") _
                        .Append(String.Format("<input id=""j-ac-extend-{1}"" value=""{3}"" data-jerti-minparam=""{4}"" data-jerti-parent=""#{1}"" data-jerti-loadItems=""{0}"" data-jerti-loading=""#j-load-{1}"" {2}>",
                                              Me._Options.loadURL,
                                              Me._Fields.ID,
                                              extendedPropertys,
                                              If(Me._Options.IsEdit, String.Format("{0:" & Me._Options.Formato & "}", Me._Fields.Value), String.Empty),
                                              Me._Options.minLenghtAC)) _
                        .Append("</span>")

                    'Asignamos el codigo JavaScript para cargar los datos en el ComboBox
                    GetJavaScriptField.Append(String.Format("$('#j-ac-extend-{0}').autocompletado();", Me._Fields.ID))

                    strControl = builderAC.ToString
                Case JFControlType.Multiline
                    strControl = String.Format("<textarea {0}{2} id=""{1}"" data-jerti-prefix=""{5}"" data-jerti-name=""{4}"" name=""{1}"" rows=""{3}"">{6}</textarea>",
                                               String.Join(" ", Me._Fields.Validaciones),
                                               Me._Fields.ID,
                                               extendedPropertys,
                                               Me._Options.rowsTextArea,
                                               Me._Fields.NameModel,
                                               Me._Fields.IsPrefix,
                                               If(Me._Options.IsEdit, String.Format("{0:" & Me._Options.Formato & "}", Me._Fields.Value), String.Empty))

                Case JFControlType.Numeric
                    strControl = String.Format("<input {0}{2} id=""{1}"" name=""{1}"" type=""number"" value=""{3}"">",
                                               String.Join(" ", Me._Fields.Validaciones),
                                               Me._Fields.ID,
                                               extendedPropertys,
                                               If(Me._Options.IsEdit, String.Format("{0:" & Me._Options.Formato & "}", Me._Fields.Value), String.Empty))

                Case JFControlType.Tiempo
                    strControl = String.Format("<input {0}{2} id=""{1}"" name=""{1}"" type=""text"" value=""{3}"">",
                                               String.Join(" ", Me._Fields.Validaciones),
                                               Me._Fields.ID,
                                               extendedPropertys,
                                               If(Me._Options.IsEdit, String.Format("{0:" & Me._Options.Formato & "}", Me._Fields.Value), String.Empty))

                    'Activamos el Script del DatePicker por defecto
                    Me._IsTimePicker = True

                    'Ahora debemos de agregar el codigo JavaScript para asociarlo al TimePicker de jQueryUI
                    GetJavaScriptField.Append(String.Format("$('#{0}').timepicker(", Me._Fields.ID)) _
                        .Append("{") _
                        .Append(Me._Options.AddOptions) _
                        .Append("});")
            End Select

            'Verificamos si la fila ha sido marcada como "READ-ONLY"
            If Me._Fields.TypeField = JFControlType.ReadOnly Then
                classReadOnly = "j-lbl-textbox"
            End If

            'Ahora pasamos a generar el tipo campo que servira para Ingreso del usuario
            strBuilder.Append("<td class=""j-ctl-cell"">") _
                .Append(String.Format("<div class=""j-cont-field {0}"">", classReadOnly)) _
                .Append(strControl)

            If Not Me._Fields.TypeField = JFControlType.ReadOnly Then
                'Ahora creamos la etiqueta SPAN donde se muestran los msj de error devueltos por el controlador, o validaciones en el cliente
                strBuilder.Append(String.Format("<span class=""field-validation-valid"" data-valmsg-for=""{0}"" data-valmsg-replace=""true""></span>", Me._Fields.ID)) _
                    .Append("</div>") _
                    .Append("</td>")
            End If
            
            'Añadimos las filas Adicionales que se han introducido
            strBuilder.Append(aditionalCells.ToString)

            'Cerramos la etiqueta de la fila
            strBuilder.Append("</tr>")

            Return strBuilder.ToString
        End Function

        Public Function ToHtmlString() As String Implements IHtmlString.ToHtmlString
            Return Me.ToString
        End Function
    End Class
End Namespace
