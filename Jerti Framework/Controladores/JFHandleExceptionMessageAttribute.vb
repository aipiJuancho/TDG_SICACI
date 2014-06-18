Imports System.Net

Namespace Controladores
    Public Class JFHandleExceptionMessageAttribute
        Inherits HandleErrorAttribute

        Public Overrides Sub OnException(filterContext As ExceptionContext)
            'Establecemos el HTTP Result como un error interno en el servidor
            filterContext.HttpContext.Response.StatusCode = HttpStatusCode.InternalServerError

            'Ahora creamos el resultado JSON que vamos a regresar con el MSJ de la excepcion
            Dim dataJSON As New JsonResult With {
                    .JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    .Data = New With {.msg = filterContext.Exception.Message}
                }

            'Retornamos del lado del cliente el contenido del msj de error para que sea interpretado por el Handler y a su vez
            'limpiamos cualquier otro msj que intente entregar el servidor
            filterContext.Result = dataJSON
            filterContext.ExceptionHandled = True
            filterContext.HttpContext.Response.Clear()
            filterContext.HttpContext.Response.StatusCode = 500
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = True
        End Sub
    End Class
End Namespace

