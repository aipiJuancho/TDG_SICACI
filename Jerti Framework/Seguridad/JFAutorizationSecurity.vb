Imports System.Web.Routing

Namespace Security

    <AttributeUsage(AttributeTargets.Class Or AttributeTargets.Method, AllowMultiple:=False, Inherited:=True)> _
    Public NotInheritable Class JFAllowAnonymousAttribute
        Inherits Attribute
    End Class

    <AttributeUsage(AttributeTargets.Method, AllowMultiple:=False, Inherited:=True)> _
    Public NotInheritable Class JFUnathorizedJSONResult
        Inherits Attribute
    End Class

    Public Class JFAutorizationSecurity
        Inherits AuthorizeAttribute

        Public Overrides Sub OnAuthorization(filterContext As AuthorizationContext)
            Dim skipAuthorization As Boolean = filterContext.ActionDescriptor.IsDefined(GetType(JFAllowAnonymousAttribute), True) OrElse filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(GetType(JFAllowAnonymousAttribute), True)

            If Not skipAuthorization Then
                MyBase.OnAuthorization(filterContext)
            End If
        End Sub

        Protected Overrides Sub HandleUnauthorizedRequest(filterContext As AuthorizationContext)
            If filterContext.HttpContext.User.Identity.IsAuthenticated Then
                Dim jsonResult As Boolean = filterContext.ActionDescriptor.IsDefined(GetType(JFUnathorizedJSONResult), True)

                If Not jsonResult Then
                    Dim routeValues = New RouteValueDictionary()
                    routeValues.Add("controller", "Home")
                    routeValues.Add("action", "NoAutorizado")

                    filterContext.Controller.ViewBag.ErrorMessage = "Lo sentimos, pero usted no tiene permisos para acceder a este modulo."
                    filterContext.Result = New RedirectToRouteResult(routeValues)
                Else
                    Dim dataJSON As New JsonResult With {
                        .JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                        .Data = New With {.msg = "Lo sentimos, pero usted no se encuentra autorizado para llevar acabo esta acción"}
                    }

                    filterContext.Result = dataJSON
                    filterContext.HttpContext.Response.Clear()
                    filterContext.HttpContext.Response.StatusCode = 500
                    filterContext.HttpContext.Response.TrySkipIisCustomErrors = True
                End If

                
            Else
                MyBase.HandleUnauthorizedRequest(filterContext)
            End If
        End Sub

    End Class
End Namespace
