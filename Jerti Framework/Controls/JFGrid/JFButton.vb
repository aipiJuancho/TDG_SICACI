Imports System.Web

Namespace Controls.JFGrid
    Public Class JFButton
        Implements IHtmlString

        Property _ID As String
        Private Property _IDForm As String
        Private Property _Value As String
        Private Property _Icon As String
        Private Property _AditionalClass As String
        Private Property _url As String = ""

        Public Sub New(name As String, value As String)
            Me.New(name, value, String.Empty)
        End Sub

        Public Sub New(name As String, value As String, IDForm As String)
            If String.IsNullOrWhiteSpace(name) Then Throw New ArgumentException("El nombre del boton no puede ser nulo")
            Me._ID = name
            Me._Value = value
            Me._IDForm = IDForm
        End Sub

        Public Function SetIcon(classIcon As String) As JFButton
            If String.IsNullOrWhiteSpace(classIcon) Then Throw New ArgumentException("El nombre de la clase que contiene el icono no puede ser nulo")
            Me._Icon = classIcon
            Return Me
        End Function

        Public Function AddClass(nameClass As String) As JFButton
            If String.IsNullOrWhiteSpace(nameClass) Then Throw New ArgumentException("El nombre de la clase no puede ser nulo")
            Me._AditionalClass = nameClass
            Return Me
        End Function

        Public Function SetIDForm(ID As String) As JFButton
            If String.IsNullOrWhiteSpace(ID) Then Throw New ArgumentException("El nombre del ID del FORM no puede ser nulo")
            Me._IDForm = ID
            Return Me
        End Function

        Public Function SetURL(url As String) As JFButton
            Me._url = url
            Return Me
        End Function

        Public Overrides Function ToString() As String
            Dim strBuilder As New StringBuilder
            strBuilder.Append(String.Format("<button id=""{0}"" data-jerti-form=""{2}"" class=""jerti-button {1}""  data-jerti-url=""{3}"">", Me._ID, Me._AditionalClass, Me._IDForm, Me._url))
            strBuilder.Append(String.Format("<span class=""icon {0}""></span>", Me._Icon))
            strBuilder.Append(String.Format("<span>{0}</span>", Me._Value))
            strBuilder.Append("</button>")
            Return strBuilder.ToString
        End Function

        Public Function ToHtmlString() As String Implements IHtmlString.ToHtmlString
            Return Me.ToString
        End Function
    End Class
End Namespace
