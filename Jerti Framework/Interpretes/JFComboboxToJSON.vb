Namespace Interpretes
    Public Class JFComboboxToJSON
        Property Items As IEnumerable
        Property IDItemSelected As String = ""

        Sub New(items As IEnumerable)
            Me.Items = items
        End Sub
    End Class
End Namespace
