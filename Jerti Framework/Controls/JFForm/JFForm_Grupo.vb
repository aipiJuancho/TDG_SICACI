Imports JertiFramework.Controls.JFGrid

Namespace Controls
    Public Class JFForm_Grupo
        Implements IHtmlString

        Property _Fields As JFFilaFields
        Protected Property _Options As JFOptionsFields = Nothing
        Protected Property _optionsMultipleSelect As JFMultipleSelect_Options = Nothing
        Property GetJavaScriptField As New StringBuilder

        Private _IsDatePicker As Boolean = False
        ReadOnly Property IsDatePicker As Boolean
            Get
                Return _IsDatePicker
            End Get
        End Property

        Public Sub New(field As JFFilaFields)
            Me._Fields = field
        End Sub

        Public Sub New(field As JFFilaFields, options As JFOptionsFields)
            Me._Fields = field
            Me._Options = options
        End Sub

        Public Sub New(field As JFFilaFields, optMultipleSelect As JFMultipleSelect_Options)
            Me._Fields = field
            Me._optionsMultipleSelect = optMultipleSelect
        End Sub

        Public Overrides Function ToString() As String
            Dim strBuilder As New StringBuilder,
                strControl As String = String.Empty,
                strExtra As New StringBuilder

            'Definimos el Grupo para el formulario segun la documentación de BootStrap 3
            strBuilder.Append("<div  class=""form-group"">")

            'Verificamos si se ha definido ocultar la etiqueta del formulario en el ViewModel
            If Not Me._Fields.OcultarEtiqueta Then
                strBuilder.Append(String.Format("<label for=""{0}"" class=""{2}{3}{4}{5}"">{1}</label>",
                                                Me._Fields.ID,
                                                Me._Fields.Display,
                                                If(_Fields.RejillaInForm.Label_Movil = -1, "", String.Format("col-xs-{0} ", _Fields.RejillaInForm.Label_Movil.ToString)),
                                                If(_Fields.RejillaInForm.Label_Tablet = -1, "", String.Format("col-sm-{0} ", _Fields.RejillaInForm.Label_Tablet.ToString)),
                                                If(_Fields.RejillaInForm.Label_PC = -1, "", String.Format("col-md-{0} ", _Fields.RejillaInForm.Label_PC.ToString)),
                                                If(_Fields.RejillaInForm.Label_Movil = -1 And _Fields.RejillaInForm.Label_Tablet = -1 And _Fields.RejillaInForm.Label_PC = -1, "", "control-label ")))
            Else
                strBuilder.Append(String.Format("<label class=""sr-only {2}{3}{4}{5}"" for=""{0}"">{1}</label>",
                                                Me._Fields.ID,
                                                Me._Fields.Display,
                                                If(_Fields.RejillaInForm.Label_Movil = -1, "", String.Format("col-xs-{0} ", _Fields.RejillaInForm.Label_Movil.ToString)),
                                                If(_Fields.RejillaInForm.Label_Tablet = -1, "", String.Format("col-sm-{0} ", _Fields.RejillaInForm.Label_Tablet.ToString)),
                                                If(_Fields.RejillaInForm.Label_PC = -1, "", String.Format("col-md-{0} ", _Fields.RejillaInForm.Label_PC.ToString)),
                                                If(_Fields.RejillaInForm.Label_Movil = -1 And _Fields.RejillaInForm.Label_Tablet = -1 And _Fields.RejillaInForm.Label_PC = -1, "", "control-label ")))
            End If


            strBuilder.Append(String.Format("<div class=""{0}{1}{2}"">",
                                            If(_Fields.RejillaInForm.Field_Movil = -1, "", String.Format("col-xs-{0} ", _Fields.RejillaInForm.Field_Movil.ToString)),
                                            If(_Fields.RejillaInForm.Field_Tablet = -1, "", String.Format("col-sm-{0} ", _Fields.RejillaInForm.Field_Tablet.ToString)),
                                            If(_Fields.RejillaInForm.Field_PC = -1, "", String.Format("col-md-{0} ", _Fields.RejillaInForm.Field_PC.ToString))))

            'Me._Fields.ValitacionesNative
            Select Case Me._Fields.TypeField
                Case JFControlType.Text
                    If Me._Fields.IsAddButton Then strBuilder.Append("<div class=""input-group"">")

                    strControl = String.Format("<input {2} name=""{0}"" type=""text"" id=""{0}"" class=""form-control"" value=""{4}"" placeholder=""{1}"" {3}>",
                                                Me._Fields.ID,
                                                Me._Fields.MarcaAgua,
                                                String.Join(" ", Me._Fields.Validaciones),
                                                IIf(Me._Fields.MaxCaracteres = -1, "", String.Format("maxlength=""{0}""", Me._Fields.MaxCaracteres)),
                                                Me._Fields.Value)

                    'Validamos si se ha especificado que se cree un boton a la par de control
                    If Me._Fields.IsAddButton Then
                        If Me._Fields.OnlyIconButton Then   'Solo hay que mostrar el icono especificado
                            strExtra.Append("<span class=""input-group-btn"">")
                            strExtra.Append(String.Format("<button id=""btn-{1}"" class=""btn btn-default {0}"" type=""button""></button>",
                                                          Me._Fields.ClassIconButton,
                                                          Me._Fields.ID))
                            strExtra.Append("</span>")
                        End If
                        strExtra.Append("</div>")
                    End If

                Case JFControlType.Password
                    strControl = String.Format("<input {2} name=""{0}"" type=""password"" id=""{0}"" value=""{4}"" class=""form-control"" placeholder=""{1}"" {3}>",
                                                Me._Fields.ID,
                                                Me._Fields.MarcaAgua,
                                                String.Join(" ", Me._Fields.Validaciones),
                                                IIf(Me._Fields.MaxCaracteres = -1, "", String.Format("maxlength=""{0}""", Me._Fields.MaxCaracteres)),
                                                Me._Fields.Value)
                Case JFControlType.File
                    strControl = String.Format("<input name=""{0}"" type=""file"" id=""{0}"" class=""form-control"" accept=""{1}"">",
                                                Me._Fields.ID,
                                                Me._Fields.FileExtensions)
                Case JFControlType.RadioButton
                    Dim builderRadioButtons As New StringBuilder,
                        strSelect As String

                    For Each item In Me._Options.listValues_Items
                        If Not Me._Options.IsInline Then builderRadioButtons.Append("<div class=""radio"">")

                        builderRadioButtons.Append(String.Format("<label class=""{0}"">",
                                                                 If(Me._Options.IsInline, "radio-inline", String.Empty)))

                        'Determinamos si es el item que debe aparecer seleccionado
                        If Not Me._Options.IsEdit Then
                            strSelect = If(item.Selected, " checked=""checked""", String.Empty)
                        Else
                            'Sobreescribimos el item seleccionado, si se ha especificado que es un campo de edicion
                            strSelect = If(item.Value = Me._Fields.Value, " checked=""checked""", String.Empty)
                        End If

                        'Creamos el RadioButton
                        builderRadioButtons.Append(String.Format("<input type=""radio"" id=""{0}"" name=""{0}"" value=""{1}"" {2}>",
                                                                 Me._Fields.ID,
                                                                 item.Value,
                                                                 strSelect))

                        'Agregamos la etiqueta asociada al RadioButton
                        builderRadioButtons.Append(item.Text)
                        builderRadioButtons.Append("</label>")
                        If Not Me._Options.IsInline Then builderRadioButtons.Append("</div>")
                    Next
                    strControl = builderRadioButtons.ToString
                Case JFControlType.ComboBox
                    strControl = buildComboBox()
                Case JFControlType.Numeric
                    strControl = String.Format("<input class=""form-control"" {0} id=""{1}"" name=""{1}"" type=""number"" value=""{2}"">",
                                               String.Join(" ", Me._Fields.Validaciones),
                                               Me._Fields.ID,
                                               Me._Fields.Value)
                Case JFControlType.Fecha
                    strControl = String.Format("<input class=""form-control"" {0} id=""{1}"" name=""{1}"" type=""text"" value=""{2}"">",
                                               String.Join(" ", Me._Fields.Validaciones),
                                               Me._Fields.ID,
                                               If(Me._Options.IsEdit, String.Format("{0:" & Me._Options.Formato & "}", Me._Fields.Value), String.Empty))

                    'Activamos el Script del DatePicker por defecto
                    Me._IsDatePicker = True

                    'Ahora debemos de agregar el codigo JavaScript para asociarlo al calendario de jQueryUI
                    GetJavaScriptField.Append(String.Format("$('#{0}').datepicker(", Me._Fields.ID)) _
                        .Append("{") _
                        .Append(Me._Options.AddOptions) _
                        .Append("});")
                Case JFControlType.Multiline
                    strControl = String.Format("<textarea class=""form-control {4}"" {0} id=""{1}"" name=""{1}"" rows=""{2}"" {5}>{3}</textarea>",
                            String.Join(" ", Me._Fields.Validaciones),
                            Me._Fields.ID,
                            Me._Options.rowsTextArea,
                            If(Me._Options.IsEdit, String.Format("{0:" & Me._Options.Formato & "}", Me._Fields.Value), String.Empty),
                            Me._Options.AddClass,
                            IIf(Me._Fields.MaxCaracteres = -1, "", String.Format("maxlength=""{0}""", Me._Fields.MaxCaracteres)))
                Case JFControlType.MultipleSelect
                    strControl = buildMultipleSelect()
            End Select

            strBuilder.Append(strControl)
            strBuilder.Append(strExtra.ToString)
            strBuilder.Append("</div>")
            strBuilder.Append("</div>")
            Return strBuilder.ToString
        End Function

        Public Function ToHtmlString() As String Implements IHtmlString.ToHtmlString
            Return Me.ToString
        End Function

        Private Function buildComboBox() As String
            Dim builderComboBox As New StringBuilder,
                optComboBox As New StringBuilder

            optComboBox.Append("{")  'Inicializamos los parametros de JavaScript

            'Verificamos si el usuario ha definido que se coloque un icono
            If Me._Fields.IsAddButton Then builderComboBox.Append("<div class=""input-group"">")

            'Consultamos si el usuario definío un conjunto de datos estáticos para el ComboBox
            If Not Me._Options.listValues_Items.Count.Equals(0) AndAlso Me._Options.listValues_Items IsNot Nothing Then
                builderComboBox.Append(String.Format("<select id=""{0}"" name=""{0}"" class=""form-control"" {1}>",
                                                     Me._Fields.ID,
                                                     String.Join(" ", Me._Fields.Validaciones)))

                'Agregamos cada uno de los items pasados por el usuario
                For Each item In Me._Options.listValues_Items
                    If Me._Options.IsEdit AndAlso item.Value = Me._Fields.Value Then
                        builderComboBox.Append(String.Format("<option value=""{0}"" selected=""selected"">{1}</option>",
                                                                                 item.Value,
                                                                                 item.Text))
                    Else
                        builderComboBox.Append(String.Format("<option value=""{0}"" {1}>{2}</option>",
                                                         item.Value,
                                                         If(item.Selected, "selected=""selected""", String.Empty),
                                                         item.Text))
                    End If
                Next
            Else
                'Los elementos del ComboBox se cargarán de forma remota a traves de AJAX del lado del cliente.
                'Primero: Validamos que se haya definido una RUTA para cargar los elementos
                If String.IsNullOrWhiteSpace(Me._Options.loadURL) Then _
                            Throw New ArgumentNullException("No se puede crear el ComboBox para el campo, debido a que no se ha definido el método donde se cargaran los items")

                builderComboBox.Append(String.Format("<select id=""{0}"" name=""{0}"" class=""form-control"" data-jerti-loadItems=""{1}"" data-jerti-loading=""#j-load-{0}"" {2}>",
                                                     Me._Fields.ID,
                                                     Me._Options.loadURL,
                                                     String.Join(" ", Me._Fields.Validaciones)))

                'Determinamos si mostramos la animacion cuando se carga la pagina directamente
                Dim displayLoading = If(Me._Options.loadOnReady, String.Empty, " display: none")

                'Antes de generar el codigo JavaScript para el ComboBox, verificamos si se ha definido algun item por defecto
                If Not String.IsNullOrWhiteSpace(Me._Options.ItemTextDefault) Then _
                    optComboBox.Append(String.Format("firstItem: '{0}'", Me._Options.ItemTextDefault))

                optComboBox.Append("}") 'Finalizamos la inclusión de codigo JavaScript

                'Por ultimo, verificamos si tenemos que generar el Codigo de JavaScript para que se carge automaticamente
                'los itmes delo ComboBox cuando se cargue la pagina.
                If Me._Options.loadOnReady Then _
                    GetJavaScriptField.Append(String.Format("$('#{0}').loadComboBox({1});", Me._Fields.ID, optComboBox.ToString))
            End If

            'Por ultimo, cerramos la etiqueta
            builderComboBox.Append("</select>")

            'Validamos si se ha especificado que se cree un boton a la par de control
            If Me._Fields.IsAddButton Then
                If Me._Fields.OnlyIconButton Then   'Solo hay que mostrar el icono especificado
                    builderComboBox.Append("<span class=""input-group-btn"">")
                    builderComboBox.Append(String.Format("<button id=""btn-{1}"" class=""btn btn-default {0}"" type=""button""></button>",
                                                  Me._Fields.ClassIconButton,
                                                  Me._Fields.ID))
                    builderComboBox.Append("</span>")
                End If
                builderComboBox.Append("</div>")
            End If

            Return builderComboBox.ToString
        End Function

        Private Function buildMultipleSelect() As String
            Dim builderMultiple As New StringBuilder,
                optMultipleSelect As New StringBuilder

            'PASO #1: Establecemos todas las propiedades generales del control
            builderMultiple.Append(String.Format("<select class=""selectpicker {0}"" ", Me._optionsMultipleSelect.AditionalClass))
            If Me._optionsMultipleSelect.IsMultiple Then builderMultiple.Append("multiple ")
            If Not Me._optionsMultipleSelect.MaxElementsSelected.Equals(-1) Then _
                builderMultiple.Append(String.Format("data-max-options=""{0}"" ", Me._optionsMultipleSelect.MaxElementsSelected))
            If Not String.IsNullOrEmpty(Me._optionsMultipleSelect.DataStyle) Then _
                builderMultiple.Append(String.Format("data-style=""{0}"" ", Me._optionsMultipleSelect.DataStyle))
            If Me._optionsMultipleSelect.IsSearch Then builderMultiple.Append("data-live-search=""true"" ")
            If Not String.IsNullOrEmpty(Me._optionsMultipleSelect.Title) Then _
                builderMultiple.Append(String.Format("title=""{0}"" ", Me._optionsMultipleSelect.Title))
            Select Case Me._optionsMultipleSelect.DataFormatSelected
                Case DataFormatSelectedType.Count
                    builderMultiple.Append("data-selected-text-format=""count"" ")
                Case DataFormatSelectedType.Count_MayorA
                    builderMultiple.Append(String.Format("data-selected-text-format=""count>{0}"" ", Me._optionsMultipleSelect.DataFormatSelected_Count))
            End Select
            If Not Me._optionsMultipleSelect.Width.Equals("auto") Then _
                builderMultiple.Append(String.Format("data-width=""{0}"" ", Me._optionsMultipleSelect.Width))
            If Me._optionsMultipleSelect.Disabled Then builderMultiple.Append("disabled ")
            If Not Me._optionsMultipleSelect.Size.Equals("auto") Then _
                builderMultiple.Append(String.Format("data-size=""5"" ", Me._optionsMultipleSelect.Size))
            If Me._optionsMultipleSelect.ShowSubText Then builderMultiple.Append("data-show-subtext=""true"" ")

            builderMultiple.Append(">")

            'PASO #2: Verificamos si el usuario ha definido los GRUPOS o no.
            If Me._optionsMultipleSelect.WithGroups Then
                If Me._optionsMultipleSelect.LoadData.Items.Count.Equals(0) OrElse Me._optionsMultipleSelect.LoadData Is Nothing Then _
                    Throw New ArgumentException("La lista de elementos del control ""MultipleSelect"" se encuentra vacia o no existe.")

                Dim data_headers = Me._optionsMultipleSelect.LoadData.Headers.OrderBy(Function(i) i.Order)
                Dim data_items As IEnumerable(Of JFMultipleSelect_Data_Items)
                For Each iHeader In data_headers
                    'Agregamos las propiedades definidas para cada GRUPO
                    builderMultiple.Append(String.Format("<optgroup label=""{0}"" ", iHeader.Label))
                    If Not iHeader.MaxOptions.Equals(-1) Then _
                        builderMultiple.Append(String.Format("data-max-options=""{0}"" ", iHeader.MaxOptions))
                    If iHeader.Disabled Then builderMultiple.Append("disabled ")
                    builderMultiple.Append(">")

                    'Empezamos agregar cada uno de los elementos que conforman este grupo
                    'Verificamos si debemos de ordenar los elementos que se van agregar en cada grupo
                    If Me._optionsMultipleSelect.LoadData.OrderItems Then
                        data_items = Me._optionsMultipleSelect.LoadData.Items _
                            .Where(Function(i) i.HeaderOrder = iHeader.Order) _
                            .OrderBy(Function(i) i.Label).ToArray()
                    Else
                        data_items = Me._optionsMultipleSelect.LoadData.Items _
                            .Where(Function(i) i.HeaderOrder = iHeader.Order).ToArray()
                    End If

                    For Each iItem In data_items
                        builderMultiple.Append(String.Format("<option value=""{0}"" ", iItem.Value))
                        If Not String.IsNullOrEmpty(iItem.Class) Then _
                            builderMultiple.Append(String.Format("class=""{0}"" ", iItem.Class))
                        If iItem.Disabled Then builderMultiple.Append("disabled=""disabled"" ")
                        If iItem.IsDivider Then builderMultiple.Append("data-divider=""true"" ")
                        If Not String.IsNullOrEmpty(iItem.SubText) Then _
                            builderMultiple.Append(String.Format("data-subtext=""{0}"" ", iItem.SubText))
                        If Not String.IsNullOrEmpty(iItem.DataIcon) Then _
                            builderMultiple.Append(String.Format("data-icon=""{0}"" "))
                        builderMultiple.Append(">")
                        builderMultiple.Append(iItem.Label)
                        builderMultiple.Append("</option>")
                    Next
                    builderMultiple.Append("</optgroup>")
                Next

            End If

            builderMultiple.Append("</select>")
            Return builderMultiple.ToString
        End Function
    End Class
End Namespace
