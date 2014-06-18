Friend Class JFCodeExtension
    Friend Shared Function ConvertDictionaryToStringParameters(obj As Object) As String
        Dim strBuilder As New StringBuilder

        'Lo primero que debemos hacer es observar si esta definido o no
        If obj Is Nothing Then Return ""

        'Como sabemos que el objeto tiene contenido, vamos a intentar convertir la estrucutra a un diccionario para poder acceder
        'a la clave como a su valor
        Dim _dictionaryDisplay = obj.GetType.GetProperties.ToDictionary(Function(a) a.Name,
                                                                        Function(a) a.GetValue(obj, Nothing))

        'Ahora recorremos todos los atributos definidos por el usuario y los regresamos concatenados
        For Each atributo In _dictionaryDisplay
            strBuilder.Append(String.Format("{0}:{1},", atributo.Key, atributo.Value.ToString))
        Next
        strBuilder.Remove(strBuilder.ToString.Count - 1, 1)

        Return strBuilder.ToString
    End Function
End Class
