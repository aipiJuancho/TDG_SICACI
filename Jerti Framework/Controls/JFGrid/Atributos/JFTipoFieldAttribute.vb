Namespace Controls
    <AttributeUsage(AttributeTargets.Property)> _
    Public Class JFTipoFieldAttribute
        Inherits Attribute

        Public Property Tipo As JFControlType

        Public Sub New(Optional tipo As JFControlType = JFControlType.Text)
            Me.Tipo = tipo
        End Sub
    End Class
End Namespace
