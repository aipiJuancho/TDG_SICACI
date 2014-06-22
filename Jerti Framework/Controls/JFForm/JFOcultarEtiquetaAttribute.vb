Namespace Controls
    <AttributeUsage(AttributeTargets.Property)> _
    Public Class JFOcultarEtiquetaAttribute
        Inherits Attribute

        Public Property OcultarEtiqueta As Boolean

        Public Sub New(Optional ocultar As Boolean = False)
            Me.OcultarEtiqueta = ocultar
        End Sub
    End Class
End Namespace
