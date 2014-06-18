Namespace Database.Validadores
    Public Class JFContieneAttribute
        Inherits ValidationAttribute

        Private Property ListString As IEnumerable(Of String)

        Sub New(ParamArray values() As String)
            MyBase.New()
            Me.ListString = values
        End Sub

        Public Overrides Function IsValid(value As Object) As Boolean
            If value Is Nothing Then Return True

            Dim str As String = value
            If String.IsNullOrWhiteSpace(str) Then Return True

            'Verificamos que el valor del usuario se encuentre contenido en alguno de los valores posibles
            Return ListString.Contains(str)
        End Function
    End Class
End Namespace
