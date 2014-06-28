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
    Private _tipo As JFTipoBoton

    Public Sub New(name As String, titulo As String, Optional tipo As JFTipoBoton = JFTipoBoton.Default)

    End Sub

    Public Function ToHtmlString() As String Implements IHtmlString.ToHtmlString
        Return Me.ToString
    End Function
End Class
