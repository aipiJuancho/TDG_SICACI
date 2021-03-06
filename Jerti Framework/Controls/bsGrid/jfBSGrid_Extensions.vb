﻿Imports System.Linq.Expressions

Namespace Controls
    Public Module jfBSGrid_Extensions

        <System.Runtime.CompilerServices.Extension()> _
        Public Function JFBSGrid_Sort(Of T)(data As IQueryable(Of T), parameters As jfBSGrid_Sort) As IQueryable(Of T)
            'Comprobamos que existan datos para poder ordenas
            If (String.IsNullOrWhiteSpace(parameters.field) OrElse String.IsNullOrWhiteSpace(parameters.order)) Then Return data

            Dim param = Expression.Parameter(GetType(T), "i")
            Dim conversion As Expression = Expression.Convert(Expression.Property(param, parameters.field), GetType(Object))
            Dim mySortExpression = Expression.Lambda(Of Func(Of T, Object))(conversion, param)

            Return If(parameters.order.Equals("descending"),
                      data.OrderByDescending(mySortExpression),
                      data.OrderBy(mySortExpression))
        End Function
    End Module
End Namespace
