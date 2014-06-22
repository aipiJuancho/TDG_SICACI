
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

        'Añadimos el atributo de 'HIDE LABEL'
        Dim hideLabel = attributes.SingleOrDefault(Function(a) GetType(JFOcultarEtiquetaAttribute) = a.GetType)
        'Si se encuentra el atributo, lo agregamos al diccionario de valores de los MetaDatos
        If hideLabel IsNot Nothing Then
            data.AdditionalValues.Add("hideLabel", CType(hideLabel, JFOcultarEtiquetaAttribute).OcultarEtiqueta)
        Else
            data.AdditionalValues.Add("hideLabel", False)
        End If

        'Añadimos el atributo de 'MAX CARACTERES'
        Dim maxCaracteres = attributes.SingleOrDefault(Function(a) GetType(JFMaxLenghtAttribute) = a.GetType)
        'Si se encuentra el atributo, lo agregamos al diccionario de valores de los MetaDatos
        If maxCaracteres IsNot Nothing Then
            data.AdditionalValues.Add("maxCaracteres", CType(maxCaracteres, JFMaxLenghtAttribute).MaxLenght)
        Else
            data.AdditionalValues.Add("maxCaracteres", -1)
        End If

        'Añadimos el atributo de "Rejilla in Form"
        Dim rejillaForm = attributes.SingleOrDefault(Function(a) GetType(JFRejillaAttribute) = a.GetType)
        If rejillaForm IsNot Nothing Then
            data.AdditionalValues.Add("rejillaInForms", CType(rejillaForm, JFRejillaAttribute))
        Else
            data.AdditionalValues.Add("rejillaInForms", New JFRejillaAttribute)
        End If

        Return data
    End Function
End Class

