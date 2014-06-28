Imports System.Linq.Expressions

Namespace Controls
    Public Class JFForm(Of T)
        Implements IHtmlString

        Private ReadOnly _htmlHelper As HtmlHelper(Of T)
        Private Property _titulo As String
        Private Property _IDForm As String
        Private Property _Grupos As New List(Of JFForm_Grupo)
        Private Property _botones As New List(Of JFFormButton)

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

        Public Function AddButton(btn As JFFormButton) As JFForm(Of T)
            Me._botones.Add(btn)
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
            Return jF
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

            Return strBuilder.ToString
        End Function

        Public Function ToHtmlString() As String Implements IHtmlString.ToHtmlString
            Return Me.ToString
        End Function
    End Class
End Namespace
