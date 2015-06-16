Namespace Controls
    Public Class jfBSGrid_Respond
        Public Property page_num As Integer
        Public Property rows_per_page As Integer
        Public Property sorting As List(Of jfBSGrid_Sort)
        Public Property columns As List(Of jfBSGrid_Respond_Columns)
    End Class

    Public Class jfBSGrid_Respond_Columns
        Public Property Field As String
        Public Property Header As String
        Public Property Visible As String
    End Class
End Namespace
