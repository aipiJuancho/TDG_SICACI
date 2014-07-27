Imports System.Linq.Expressions

Namespace Controls
    Public Class jfBSGrid_Column
        Private _prop As jfBSGrid_DecodeColumn

        Public Sub New(propiedades As jfBSGrid_DecodeColumn)
            Me._prop = propiedades
        End Sub

        Public Overrides Function ToString() As String
            Dim strBuilder As New StringBuilder
            strBuilder.Append("{")
            strBuilder.Append(String.Format("field: ""{0}"", ", Me._prop.Name))
            strBuilder.Append(String.Format("header: ""{0}"", ", Me._prop.Display))
            strBuilder.Append(String.Format("visible: ""{0}""", If(Me._prop.Visible, "yes", "no")))
            strBuilder.Append("}")

            Return strBuilder.ToString
        End Function
    End Class
End Namespace
