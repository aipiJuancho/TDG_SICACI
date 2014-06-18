Imports System.Web
Imports System.Web.Routing

Namespace Ruteo
    ''' <summary>
    ''' Restringe el mapeo de rutas unicamente a los nombres de controladores especificados en esta lista
    ''' </summary>
    ''' <remarks></remarks>
    Public Class JFControllerListConstraint
        Implements IRouteConstraint

        Public Sub New(ParamArray values As String())
            Me._values = values
        End Sub

        Private _values As String()

        Public Function Match(httpContext As HttpContextBase, route As Route, parameterName As String, values As RouteValueDictionary, routeDirection As RouteDirection) As Boolean Implements IRouteConstraint.Match
            ' Get the value called "parameterName" from the 
            ' RouteValueDictionary called "value"
            Dim value As String = values(parameterName).ToString()

            ' Return true is the list of allowed values contains 
            ' this value.
            Return _values.Contains(value)
        End Function
    End Class
End Namespace
