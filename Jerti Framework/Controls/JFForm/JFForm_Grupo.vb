Imports JertiFramework.Controls.JFGrid

Namespace Controls
    Public Class JFForm_Grupo
        Implements IHtmlString

        Property _Fields As JFFilaFields
        Protected Property _Options As JFOptionsFields = Nothing
        Property GetJavaScriptField As New StringBuilder

        Public Sub New(field As JFFilaFields)
            Me._Fields = field
        End Sub

        Public Sub New(field As JFFilaFields, options As JFOptionsFields)
            Me._Fields = field
            Me._Options = options
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
                        strSelect = If(item.Selected, " checked=""checked""", String.Empty)
                        'strSelect = If(item.Value = Me._Fields.Value, " checked=""checked""", String.Empty)

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
                    builderComboBox.Append(String.Format("<option value=""{0}"" {1}>{2}</option>",
                                                         item.Value,
                                                         If(item.Selected, "selected=""selected""", String.Empty),
                                                         item.Text))
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
    End Class
End Namespace
