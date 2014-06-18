Namespace Interpretes.NotifySystem
    Public Class JFNotifySystemMessage
        Property Titulo As String
        Property Mensaje As String
        Property Icono As String
        Property Permanente As Boolean
        Property Tiempo As Integer
        Property Show As Boolean

        Public Sub New(msj As String,
                       Optional titulo As String = "¡Se ha detectado un error!",
                       Optional icono As String = "ERR_EXCEPTION",
                       Optional permanente As Boolean = True,
                       Optional tiempo As Integer = 10000,
                       Optional show As Boolean = True)
            Me.Titulo = titulo
            Me.Mensaje = msj
            Me.Icono = icono
            Me.Permanente = permanente
            Me.Tiempo = tiempo
            Me.Show = show
        End Sub

    End Class
End Namespace
