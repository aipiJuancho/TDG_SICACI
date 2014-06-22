Namespace Controls
    <AttributeUsage(AttributeTargets.Property)> _
    Public Class JFRejillaAttribute
        Inherits Attribute

        Public Property Label_Movil As Integer
        Public Property Label_Tablet As Integer
        Public Property Label_PC As Integer
        Public Property Field_Movil As Integer
        Public Property Field_Tablet As Integer
        Public Property Field_PC As Integer

        Public Sub New(Optional Grid_Label_Movil As Integer = -1, Optional Grid_Label_Tablet As Integer = -1,
                       Optional Grid_Label_PC As Integer = -1, Optional Grid_Field_Movil As Integer = -1,
                       Optional Grid_Field_Tablet As Integer = -1, Optional Grid_Field_PC As Integer = -1)
            Me.Label_Movil = Grid_Label_Movil
            Me.Label_Tablet = Grid_Label_Tablet
            Me.Label_PC = Grid_Label_PC
            Me.Field_Movil = Grid_Field_Movil
            Me.Field_Tablet = Grid_Field_Tablet
            Me.Field_PC = Grid_Field_PC
        End Sub
    End Class
End Namespace
