Imports JertiFramework.Controls.JFGrid

Namespace Controls
    Public Class JFForm_Grupo
        Implements IHtmlString

        Protected Property _Fields As JFFilaFields
        Protected Property _Options As JFOptionsFields = Nothing

        Public Sub New(field As JFFilaFields)
            Me._Fields = field
        End Sub

        Public Sub New(field As JFFilaFields, options As JFOptionsFields)
            Me._Fields = field
            Me._Options = options
        End Sub

        Public Overrides Function ToString() As String
            Dim strBuilder As New StringBuilder,
                strControl As String = String.Empty

            'Definimos el Grupo para el formulario segun la documentación de BootStrap 3
            strBuilder.Append("<div  class=""form-group"">")

            'Verificamos si se ha definido ocultar la etiqueta del formulario en el ViewModel
            If Not Me._Fields.OcultarEtiqueta Then
                strBuilder.Append(String.Format("<label for=""{0}"" class=""{2}{3}{4}{5}"">{1}</label>",
                                                Me._Fields.ID,
                                                Me._Fields.Display,
                                                If(_Fields.RejillaInForm.Label_Movil = -1, "", String.Format("col-xs-{0} ", _Fields.RejillaInForm.Label_Movil.ToString)),
                                                If(_Fields.RejillaInForm.Label_Tablet = -1, "", String.Format("col-sm-{0} ", _Fields.RejillaInForm.Label_Tablet.ToString)),
                                                If(_Fields.RejillaInForm.Label_PC = -1, "", String.Format("col-md-{0} ", _Fields.RejillaInForm.Label_PC.ToString)),
                                                If(_Fields.RejillaInForm.Label_Movil = -1 And _Fields.RejillaInForm.Label_Tablet = -1 And _Fields.RejillaInForm.Label_PC = -1, "", "control-label ")))
            Else
                strBuilder.Append(String.Format("<label class=""sr-only {2}{3}{4}{5}"" for=""{0}"">{1}</label>",
                                                Me._Fields.ID,
                                                Me._Fields.Display,
                                                If(_Fields.RejillaInForm.Label_Movil = -1, "", String.Format("col-xs-{0} ", _Fields.RejillaInForm.Label_Movil.ToString)),
                                                If(_Fields.RejillaInForm.Label_Tablet = -1, "", String.Format("col-sm-{0} ", _Fields.RejillaInForm.Label_Tablet.ToString)),
                                                If(_Fields.RejillaInForm.Label_PC = -1, "", String.Format("col-md-{0} ", _Fields.RejillaInForm.Label_PC.ToString)),
                                                If(_Fields.RejillaInForm.Label_Movil = -1 And _Fields.RejillaInForm.Label_Tablet = -1 And _Fields.RejillaInForm.Label_PC = -1, "", "control-label ")))
            End If


            strBuilder.Append(String.Format("<div class=""{0}{1}{2}"">",
                                            If(_Fields.RejillaInForm.Field_Movil = -1, "", String.Format("col-xs-{0} ", _Fields.RejillaInForm.Field_Movil.ToString)),
                                            If(_Fields.RejillaInForm.Field_Tablet = -1, "", String.Format("col-sm-{0} ", _Fields.RejillaInForm.Field_Tablet.ToString)),
                                            If(_Fields.RejillaInForm.Field_PC = -1, "", String.Format("col-md-{0} ", _Fields.RejillaInForm.Field_PC.ToString))))

            Select Case Me._Fields.TypeField
                Case JFControlType.Text
                    strControl = String.Format("<input type=""text"" id=""{0}"" class=""form-control"" placeholder=""{1}"" {3} {2}>",
                                                Me._Fields.ID,
                                                Me._Fields.MarcaAgua,
                                                String.Join(" ", Me._Fields.Validaciones),
                                                IIf(Me._Fields.MaxCaracteres = -1, "", String.Format("maxlength=""{0}""", Me._Fields.MaxCaracteres)))

                Case JFControlType.Password
                    strControl = String.Format("<input type=""password"" id=""{0}"" class=""form-control"" placeholder=""{1}"" {3} {2}>",
                                                Me._Fields.ID,
                                                Me._Fields.MarcaAgua,
                                                String.Join(" ", Me._Fields.Validaciones),
                                                IIf(Me._Fields.MaxCaracteres = -1, "", String.Format("maxlength=""{0}""", Me._Fields.MaxCaracteres)))
            End Select

            strBuilder.Append(strControl)
            strBuilder.Append("</div>")
            strBuilder.Append("</div>")
            Return strBuilder.ToString
        End Function

        Public Function ToHtmlString() As String Implements IHtmlString.ToHtmlString
            Return Me.ToString
        End Function
    End Class
End Namespace
