Namespace Security

    <AttributeUsage(AttributeTargets.Class Or AttributeTargets.Method, AllowMultiple:=False, Inherited:=True)> _
    Public NotInheritable Class JFAllowAnonymousAttribute
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

    End Class
End Namespace
