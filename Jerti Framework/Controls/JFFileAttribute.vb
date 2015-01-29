Namespace Controls
    Public Class JFFileAttribute
        Inherits ValidationAttribute

        Public Enum JFFileExtension
            Imagen = 0
            PDF = 1
            ImagenYPDF = 2
        End Enum

        Public Property _TipoArchivo As JFFileExtension
        Private Property _Tamaño As Integer
        Private _extensiones As String()
        Private _maxLenght As Integer

        Public Sub New(tamañoArchivoMB As Integer, Extension As JFFileExtension)
            MyBase.New()
            Me._TipoArchivo = Extension
            Me._Tamaño = tamañoArchivoMB

            'Definimos las extensiones permitidas por el validador
            Select Case Me._TipoArchivo
                Case JFFileExtension.Imagen
                    _extensiones = New String() {".jpg", ".png"}
                Case JFFileExtension.PDF
                    _extensiones = New String() {".pdf"}
                Case JFFileExtension.ImagenYPDF
                    _extensiones = New String() {".jpg", ".png", ".pdf"}
            End Select

            'Establecemos la longitud maxima del archivo
            Me._maxLenght = (1024 * 1024) * Me._Tamaño
        End Sub

        Public Overrides Function IsValid(value As Object) As Boolean
            Dim file = CType(value, HttpPostedFileBase)

            If file Is Nothing Then
                Return False
            ElseIf Not Me._extensiones.Contains(file.FileName.Substring(file.FileName.LastIndexOf("."))) Then
                ErrorMessage = "Solo se permiten archivos con extensión: " + String.Join(", ", Me._extensiones)
                Return False
            ElseIf file.ContentLength > Me._maxLenght Then
                ErrorMessage = "El archivo es demasiado grande. El tamaño maximo permitido es " + (Me._maxLenght / 1024).ToString + "kb"
                Return False
            Else
                Return True
            End If
        End Function
    End Class
End Namespace
