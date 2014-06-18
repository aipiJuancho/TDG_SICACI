Imports System.Reflection
Imports System.Linq.Expressions
Imports System.Web

Namespace Controls.JFGridReadOnly
    Public Class JFGridReadOnly(Of T)
        Implements IHtmlString

        Private Property _htmlHelper As HtmlHelper(Of T)
        Private Property _titulo As String
        Private ReadOnly _filas As New List(Of JFFilaReadOnly)
        Private Property _Width As Integer
        Private Property _centerAlign As Boolean

        ''' <summary>
        ''' Crea una nueva instancia del JFGridReadOnly para que puede ser utilizada en una coleccion de datos del modelo
        ''' </summary>
        ''' <param name="titulo"></param>
        ''' <remarks></remarks>
        Public Sub New(titulo As String)
            _titulo = titulo
        End Sub

        Public Sub New(htmlHelper As HtmlHelper(Of T), titulo As String)
            _htmlHelper = htmlHelper
            _titulo = titulo
        End Sub

#Region "OVERLOAD PARA AÑADIR UNA NUEVA FILA A LA TABLA"

        ''' <summary>
        ''' Este método se utiliza para agregar filas por medio de un FOR que ocurre cuando el modelo es una colección y no solamente una entidad.
        ''' </summary>
        ''' <param name="helper">El helper fuertemente tipado a la entidad sola que se cargara</param>
        ''' <returns></returns>
        ''' <remarks>ESTE METODO SE UTILIZA SOLAMENTE PARA MODELOS QUE SON COLECCIONES</remarks>
        Public Function AddFieldsFor(helper As HtmlHelper(Of T)) As JFGridReadOnly(Of T)
            Me._htmlHelper = helper
            _filas.Add(New JFFilaReadOnly(New JFFilaFields))
            Return Me
        End Function

        ''' <summary>
        ''' Establece el titulo que aparecera en la fila por medio del nuevo modelo que se ha reescrito
        ''' </summary>
        ''' <typeparam name="TProperty"></typeparam>
        ''' <param name="propiedad">Propiedad de donde se tomara el nombre a mostrar</param>
        ''' <returns></returns>
        ''' <remarks>ESTE METODO SE UTILIZA SOLAMENTE PARA MODELOS QUE SON COLECCIONES</remarks>
        Public Function SetTitulo(Of TProperty)(propiedad As Expression(Of Func(Of T, TProperty))) As JFGridReadOnly(Of T)
            Dim mMetaData As ModelMetadata = ModelMetadata.FromLambdaExpression(propiedad, _htmlHelper.ViewData)
            Dim jF = _filas.Last
            jF._fields.ID = mMetaData.PropertyName
            jF._fields.Display = mMetaData.Model
            Return Me
        End Function

        ''' <summary>
        ''' Establece el titulo que aparecera en la fila por medio del nuevo modelo que se ha reescrito
        ''' </summary>
        ''' <param name="titulo">Texto que deses aparezca como titulo en la tabla</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SetTitulo(titulo As String) As JFGridReadOnly(Of T)
            Dim jF = _filas.Last
            jF._fields.ID = titulo
            jF._fields.Display = titulo
            Return Me
        End Function

        ''' <summary>
        ''' Establece el valor que se mostrara en la segunda columna de la tabla
        ''' </summary>
        ''' <typeparam name="TProperty"></typeparam>
        ''' <param name="propiedad">Propiedad de donse se va a extraer el valor y se establecera en la tabla</param>
        ''' <returns></returns>
        ''' <remarks>ESTE METODO SE UTILIZA SOLAMENTE PARA MODELOS QUE SON COLECCIONES</remarks>
        Public Function SetValue(Of TProperty)(propiedad As Expression(Of Func(Of T, TProperty))) As JFGridReadOnly(Of T)
            Dim mMetaData As ModelMetadata = ModelMetadata.FromLambdaExpression(propiedad, _htmlHelper.ViewData)
            Dim jF = _filas.Last
            jF._fields.Value = mMetaData.Model
            Return Me
        End Function

        Public Function SetValue(value As String) As JFGridReadOnly(Of T)
            Dim jF = _filas.Last
            jF._fields.Value = value
            Return Me
        End Function

        ''' <summary>
        ''' Establece las opciones de la fila que se mostrara en la propiedad
        ''' </summary>
        ''' <param name="opciones">opciones que se van a establecer en la fila</param>
        ''' <returns></returns>
        ''' <remarks>ESTE METODO SE UTILIZA SOLAMENTE PARA MODELOS QUE SON COLECCIONES</remarks>
        Public Function SetOptions(opciones As JFOptionsFieldsReadOnly) As JFGridReadOnly(Of T)
            Dim jF = _filas.Last
            jF._options = opciones
            Return Me
        End Function

        ''' <summary>
        ''' Agrega un nuevo a la tabla sin necesidad de estar fuertemente tipado a una propiedad del modelo.
        ''' </summary>
        ''' <param name="titulo">Titulo que se agregara a la primera columna de la tabla</param>
        ''' <param name="value">Valor que tomara la fila y se mostrara en al segunda columna de la tabla.</param>
        ''' <param name="options">Opciones adicionales para la fila.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function AddField(titulo As String, value As String, options As JFOptionsFieldsReadOnly) As JFGridReadOnly(Of T)
            Dim jF As New JFFilaFields
            jF.ID = titulo
            jF.Display = titulo
            jF.Value = value
            _filas.Add(New JFFilaReadOnly(jF, options))
            Return Me
        End Function

        Public Function AddFieldFor(Of TProperty)(propiedad As Expression(Of Func(Of T, TProperty))) As JFGridReadOnly(Of T)
            Dim jF = Me.GetMembersFields(propiedad)
            _filas.Add(New JFFilaReadOnly(jF))
            Return Me
        End Function

        Public Function AddFieldFor(Of TProperty)(tituloField As String, propiedad As Expression(Of Func(Of T, TProperty))) As JFGridReadOnly(Of T)
            Dim jF = Me.GetMembersFields(propiedad)
            jF.Display = tituloField
            _filas.Add(New JFFilaReadOnly(jF))
            Return Me
        End Function

        Public Function AddFieldFor(Of TProperty)(propiedad As Expression(Of Func(Of T, TProperty)), options As JFOptionsFieldsReadOnly) As JFGridReadOnly(Of T)
            Dim jF = Me.GetMembersFields(propiedad)
            _filas.Add(New JFFilaReadOnly(jF, options))
            Return Me
        End Function

        Public Function AddFieldFor(Of TProperty)(tituloField As String, propiedad As Expression(Of Func(Of T, TProperty)), options As JFOptionsFieldsReadOnly) As JFGridReadOnly(Of T)
            Dim jF = Me.GetMembersFields(propiedad)
            jF.Display = tituloField
            _filas.Add(New JFFilaReadOnly(jF, options))
            Return Me
        End Function
#End Region

        Private Function GetMembersFields(Of TProperty)(propiedad As Expression(Of Func(Of T, TProperty))) As JFFilaFields
            Dim jF As New JFFilaFields
            Dim mMetaData As ModelMetadata = ModelMetadata.FromLambdaExpression(propiedad, _htmlHelper.ViewData)
            jF.IsTipado = True
            jF.Display = mMetaData.GetDisplayName
            jF.ID = mMetaData.PropertyName
            jF.Value = mMetaData.Model

            Return jF
        End Function

        Public Function SetWidth(width As Integer) As JFGridReadOnly(Of T)
            If width <= 0 Then Throw New ArgumentException("El ancho de la tabla tiene que ser mayor a cero")
            Me._Width = width
            Return Me
        End Function

        Public Function SetAlignCenter(center As Boolean) As JFGridReadOnly(Of T)
            Me._centerAlign = center
            Return Me
        End Function

        Public Overrides Function ToString() As String
            Dim strBuilder As New StringBuilder
            Dim sAlign As String = IIf(_centerAlign, " margin: 0 auto;", String.Empty)
            Dim sWidth As String = IIf(Me._Width = 0, String.Empty, String.Format(" style=""width: {0}px;{1}"" ", Me._Width, sAlign))

            'Generamos el contenedor de los objetos
            strBuilder.Append(String.Format("<div{0}>", sWidth))

            'Generamos la barra de titulo
            strBuilder.Append(String.Format("<div class=""j-tbl-ro-titulo"">{0}</div>", Me._titulo))

            strBuilder.Append(String.Format("<table class=""j-tbl-ro"">")) _
                .Append("<tbody>")

            'Vamos a recorrer cada una de las filas que hemos agregado
            For Each f In _filas
                strBuilder.Append(f.ToString)
            Next

            strBuilder.Append("</tbody></table>") _
                .Append("</div>")

            Return strBuilder.ToString
        End Function

        Public Function ToHtmlString() As String Implements IHtmlString.ToHtmlString
            Return Me.ToString
        End Function
    End Class
End Namespace
