Imports System.Runtime.CompilerServices
Imports System.Globalization

Namespace Interpretes.ErrorModelState
    Public Module JFErrorModelStateExtensor
        ''' <summary>
        ''' Devuelve un array que contiene el objeto y el mensaje de error del campo que no es válido segun el Modelo
        ''' </summary>
        ''' <param name="model"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()> _
        Public Function ToJSONErrors(model As ModelStateDictionary) As IEnumerable(Of JFErrorModelStateItem)
            Return model.Where(Function(err) err.Value.Errors.Any()) _
                        .Select(Function(err) New JFErrorModelStateItem With {
                                    .ID_Object = err.Key,
                                    .MSG_Error = CType(err.Value.Errors(0), ModelError).ErrorMessage
                                }).ToArray
        End Function
    End Module
End Namespace
