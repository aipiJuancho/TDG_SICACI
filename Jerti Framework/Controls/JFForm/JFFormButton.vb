Namespace Controls

    Public Class JFFormButton
        Implements IHtmlString

        Public Enum JFTipoBoton
            [Default] = 0
            Primary = 1
            Success = 2
            Info = 3
            Warning = 4
            Danger = 5
            Link = 6
        End Enum

        Private _name As String
        Private _titulo As String
        Private _tipo As String
        Private _enBloque As String
        Private _classAditional As String
        Private _attrAditional As String

        ''' <summary>
        ''' Crea un nuevo boton en el formulario activo
        ''' </summary>
        ''' <param name="name">ID Atributo</param>
        ''' <param name="titulo">Texto que mostrará el boton</param>
        ''' <remarks></remarks>
        Public Sub New(name As String, titulo As String)
            Me._name = name
            Me._titulo = titulo
        End Sub

        ''' <summary>
        ''' Crea un nuevo boton en el formulario activo
        ''' </summary>
        ''' <param name="name">ID Atributo</param>
        ''' <param name="titulo">Texto que mostrará el boton</param>
        ''' <param name="tipo">Estilo del boton segun el Theme del Bootsrap 3.0</param>
        ''' <remarks></remarks>
        Public Sub New(name As String, titulo As String, tipo As JFTipoBoton)
            Me.New(name, titulo)
            Select Case tipo
                Case JFTipoBoton.Default
                    Me._tipo = "btn-default"
                Case JFTipoBoton.Primary
                    Me._tipo = "btn-primary"
                Case JFTipoBoton.Success
                    Me._tipo = "btn-success"
                Case JFTipoBoton.Info
                    Me._tipo = "btn-info"
                Case JFTipoBoton.Warning
                    Me._tipo = "btn-warning"
                Case JFTipoBoton.Danger
                    Me._tipo = "btn-danger"
                Case JFTipoBoton.Link
                    Me._tipo = "btn-link"
            End Select
        End Sub

        ''' <summary>
        ''' Crea un nuevo boton en el formulario activo
        ''' </summary>
        ''' <param name="name">ID Atributo</param>
        ''' <param name="titulo">Texto que mostrará el boton</param>
        ''' <param name="tipo">Estilo del boton segun el Theme del Bootsrap 3.0</param>
        ''' <param name="EnBloque">Define si el boton utilizará todo el ancho disponible o no.</param>
        ''' <remarks></remarks>
        Public Sub New(name As String, titulo As String, tipo As JFTipoBoton, EnBloque As Boolean)
            Me.New(name, titulo, tipo)
            Me._enBloque = If(EnBloque, "btn-block", "")
        End Sub

        ''' <summary>
        ''' Crea un nuevo boton en el formulario activo
        ''' </summary>
        ''' <param name="name">ID Atributo</param>
        ''' <param name="titulo">Texto que mostrará el boton</param>
        ''' <param name="tipo">Estilo del boton segun el Theme del Bootsrap 3.0</param>
        ''' <param name="EnBloque">Define si el boton utilizará todo el ancho disponible o no.</param>
        ''' <param name="classAditional">Clases adicionales que se deseen agregar manualmente al boton.</param>
        ''' <param name="attrAditional">Atributos HTML adicionales que se deseen agregar manualmente al boton</param>
        ''' <remarks></remarks>
        Public Sub New(name As String, titulo As String, tipo As JFTipoBoton, EnBloque As Boolean, Optional classAditional As String = "", Optional attrAditional As String = "")
            Me.New(name, titulo, tipo, EnBloque)
            Me._classAditional = classAditional
            Me._attrAditional = attrAditional
        End Sub

        Public Overrides Function ToString() As String
            Dim strBuilder As New StringBuilder
            strBuilder.Append("<button")
            strBuilder.Append("type=""button"" ")
            strBuilder.Append(String.Format("id=""{0}"" ", Me._name))

            'Agregamos las clases para el boton
            strBuilder.Append(String.Format("class=""btn {0} {1} {2}"" ", Me._tipo, Me._enBloque, Me._classAditional))

            'Agregamos atributos adicionales que haya definido el usuario
            strBuilder.Append(Me._attrAditional)

            'Colocamos el texto que vamos a mostrar en el boton y cerramos la etiqueta del boton.
            strBuilder.Append(">")
            strBuilder.Append(Me._titulo)
            strBuilder.Append("</button>")
            Return strBuilder.ToString
        End Function

        Public Function ToHtmlString() As String Implements IHtmlString.ToHtmlString
            Return Me.ToString
        End Function
    End Class
End Namespace
