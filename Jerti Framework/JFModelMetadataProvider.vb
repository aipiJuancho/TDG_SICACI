
Public Class JFModelMetadataProvider
    Inherits DataAnnotationsModelMetadataProvider

    Protected Overrides Function CreateMetadata(attributes As IEnumerable(Of Attribute), containerType As Type, modelAccessor As Func(Of Object), modelType As Type, propertyName As String) As ModelMetadata
        Dim data = MyBase.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName)

        'Añadimos el atributo de 'TIPOFIELD'
        Dim tipoField = attributes.SingleOrDefault(Function(a) GetType(JFTipoFieldAttribute) = a.GetType)
        'Si se encuentra el atributo, lo agregamos al diccionario de valores de los MetaDatos
        If tipoField IsNot Nothing Then
            data.AdditionalValues.Add("TipoField", CType(tipoField, JFTipoFieldAttribute).Tipo)
        Else
            data.AdditionalValues.Add("TipoField", JFControlType.Text)
        End If

        Return data
    End Function
End Class

