
Namespace Controls.JFGrid
    Public Class JFOptionsFields
        Property MaxLength As String
        Property MarcaDeAgua As String
        Property Width As Integer

        Private _IsInline As Boolean = False
        ReadOnly Property IsInline As Boolean
            Get
                Return Me._IsInline
            End Get
        End Property

        Private _Isedit As Boolean = False
        ReadOnly Property IsEdit As Boolean
            Get
                Return _Isedit
            End Get
        End Property

        Private _addClass As String = ""
        ReadOnly Property AddClass As String
            Get
                Return _addClass
            End Get
        End Property

        Private _formateo As String = ""
        ReadOnly Property Formato As String
            Get
                Return _formateo
            End Get
        End Property

        Private _itemTextDefault As String = ""
        ReadOnly Property ItemTextDefault As String
            Get
                Return _itemTextDefault
            End Get
        End Property

        Property listValues_Items As IEnumerable(Of SelectListItem) = New SelectListItem() {}
        Property loadURL As String

        'Propiedades especificas para el tipo de ComboBox
        Property loadOnReady As Boolean

        'Propiedad especificas para el Automplete
        Property minLenghtAC As Integer

        'Propiedades especificas del Textarea
        Private _rowsTextArea As Integer = 2
        ReadOnly Property rowsTextArea As Integer
            Get
                Return _rowsTextArea
            End Get
        End Property


        Private _optionsAditional As String = ""
        ReadOnly Property AddOptions As String
            Get
                Return _optionsAditional
            End Get
        End Property


        Public Function SetMaxLength(max As Integer) As JFOptionsFields
            If max <= 0 Then Throw New ArgumentException("El valor maximo debe ser mayor a cero")
            Me.MaxLength = max
            Return Me
        End Function

        Public Function SetMarcaDeAgua(watermark As String) As JFOptionsFields
            Me.MarcaDeAgua = watermark
            Return Me
        End Function

        Public Function SetWidth(ancho As Integer) As JFOptionsFields
            Me.Width = ancho
            Return Me
        End Function

        Public Function loadItems(items As IEnumerable(Of SelectListItem)) As JFOptionsFields
            Me.listValues_Items = items
            Return Me
        End Function

        Public Function setSource(url As String, Optional loadOnReady As Boolean = True) As JFOptionsFields
            Me.loadURL = url
            Me.loadOnReady = loadOnReady
            Return Me
        End Function

        Public Function SetMinLengthToAutoComplete(minimo As Integer) As JFOptionsFields
            Me.minLenghtAC = minimo
            Return Me
        End Function

        Public Function SetItemDefault(texto As String) As JFOptionsFields
            Me._itemTextDefault = texto
            Return Me
        End Function

        Public Function SetRowsTextArea(rows As Integer) As JFOptionsFields
            Me._rowsTextArea = rows
            Return Me
        End Function

        ''' <summary>
        ''' Establece en el control que actualmente se encuentra en modo “Edición”, por lo tanto deberá recuperar el valor que va en el modelo y ponerlo en el control HTML como valor inicial.
        ''' </summary>
        ''' <param name="editando">Si es “True”, el control se marca como “Modo Edición”, de lo contrario (valor por defecto) el control no recupera el valor pasado del modelo.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SetIsEdit(editando As Boolean) As JFOptionsFields
            Me._Isedit = editando
            Return Me
        End Function

        ''' <summary>
        ''' Permite aplicar formato al valor del modelo que se va a recuperar para su presentación en el control. Por ejemplo, es útil para definir el formato en que quiero que aparezca una fecha recuperada del modelo.
        ''' </summary>
        ''' <param name="formato">Formato que se va aplicar al valor del modelo.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SetFormato(formato As String) As JFOptionsFields
            Me._formateo = formato
            Return Me
        End Function

        ''' <summary>
        ''' Permite establecer opciones de personalización de los controles jQuery que utiliza el framework. Por ejemplo establecer MinDate y MaxDate en un DatePicker, u opciones adicionales en un TimePicker.
        ''' </summary>
        ''' <param name="opciones">Opciones que se agregaran al código JavaScript del Widget que se desea editar. Escriba las opciones como si estuviera escribiendo JavaScript en los parámetros del control jQuery.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SetOptions(opciones As String) As JFOptionsFields
            Me._optionsAditional = opciones
            Return Me
        End Function

        Public Function SetIsInline(inline As Boolean) As JFOptionsFields
            Me._IsInline = inline
            Return Me
        End Function

        Public Function SetClassHTML(clase As String) As JFOptionsFields
            Me._addClass = clase
            Return Me
        End Function

        Public Overrides Function ToString() As String
            Dim strBuilder As New StringBuilder

            'Armamos cada una de las propiedades que hayamos establecido, aunque siempre se evalua si se establecio o no
            If Not String.IsNullOrWhiteSpace(Me.MaxLength) Then strBuilder.Append(String.Format(" maxlength=""{0}""", Me.MaxLength))
            If Not String.IsNullOrWhiteSpace(Me.MarcaDeAgua) Then strBuilder.Append(String.Format(" placeholder=""{0}""", Me.MarcaDeAgua))
            If Me.Width > 0 Then strBuilder.Append(String.Format(" style=""width:{0}px""", Me.Width))
            Return strBuilder.ToString
        End Function

    End Class
End Namespace
