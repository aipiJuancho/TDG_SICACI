Namespace Controls
    <AttributeUsage(AttributeTargets.Property)> _
    Public Class JFMaxLenghtAttribute
        Inherits Attribute

        Public Property MaxLenght As Integer

        Public Sub New(max As Integer)
            Me.MaxLenght = max
        End Sub
    End Class
End Namespace