Imports System.Globalization

Namespace Database
    Public Class JFDateModelBinder
        Inherits DefaultModelBinder

        Public Overrides Function BindModel(controllerContext As ControllerContext, bindingContext As ModelBindingContext) As Object
            Dim value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName)

            If value IsNot Nothing Then
                Dim fecha As Date
                Dim formato = bindingContext.ModelMetadata.DisplayFormatString
                If Date.TryParseExact(value.AttemptedValue, formato, CultureInfo.GetCultureInfo("es-SV"), DateTimeStyles.None, fecha) Then
                    Return fecha
                Else
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, "La fecha u hora ingresada no es válida ya que no esta escrita en el formato solicitado.")
                End If
            End If

            Return MyBase.BindModel(controllerContext, bindingContext)
        End Function
    End Class
End Namespace
