Namespace Controls
    Public Class JFMultipleSelect_Data
        Property Headers As IEnumerable(Of JFMultipleSelect_Data_Headers)
        Property Items As IEnumerable(Of JFMultipleSelect_Data_Items)
        Property OrderItems As Boolean = False
    End Class

    Public Class JFMultipleSelect_Data_Headers
        Property Label As String
        Property MaxOptions As Integer = -1
        Property Disabled As Boolean = False
        Property Order As Integer
    End Class

    Public Class JFMultipleSelect_Data_Items
        Property Label As String
        Property Value As String
        Property [Class] As String
        Property Disabled As Boolean
        Property IsDivider As Boolean = False
        Property SubText As String
        Property DataIcon As String
        Property HeaderOrder As Integer
    End Class
End Namespace
