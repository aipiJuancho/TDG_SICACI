Imports System.Net
Imports JertiFramework.Interpretes.ErrorModelState

Namespace Controladores

    Public Class JFValidarModelAttribute
        Inherits ActionFilterAttribute

        Public Overrides Sub OnActionExecuting(filterContext As ActionExecutingContext)
            'Validamos si el modelo con los datos es valido
            If Not filterContext.Controller.ViewData.ModelState.IsValid Then
                'Al no ser valido, construimos la estructura JSON que vamos a devolver con los errores
                Dim dataJSON As New JsonResult

                'New With {.success = False, .modelErrors = ErrorItemModelState.GetErrorsModelState(ModelState)}
                'Construimos el JSON que vamos a devolver
                dataJSON.Data = New With {.success = False, .modelErrors = filterContext.Controller.ViewData.ModelState.ToJSONErrors}
                dataJSON.JsonRequestBehavior = JsonRequestBehavior.AllowGet

                'Establecemos el msj de error en la solicitud HTTP
                'filterContext.HttpContext.Response.StatusCode = HttpStatusCode.InternalServerError
                filterContext.Result = dataJSON
            Else
                'Si el modelo es valido, lo pasamos al Metodo del Controlador
                MyBase.OnActionExecuting(filterContext)
            End If
        End Sub
    End Class
End Namespace
