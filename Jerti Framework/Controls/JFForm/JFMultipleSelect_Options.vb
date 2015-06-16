
Namespace Controls
    Public Enum DataFormatSelectedType
        Values = 0
        Count = 1
        Count_MayorA = 2
    End Enum

    Public Class JFMultipleSelect_Options

#Region "Propiedades"
        Private _isMultiple As Boolean = True
        ReadOnly Property IsMultiple As Boolean
            Get
                Return _isMultiple
            End Get
        End Property

        Private _WithGroups As Boolean = True
        ReadOnly Property WithGroups As Boolean
            Get
                Return _WithGroups
            End Get
        End Property

        Private _MaxElementsSelected As Integer = -1
        ReadOnly Property MaxElementsSelected As Integer
            Get
                Return _MaxElementsSelected
            End Get
        End Property

        Private _dataStyle As String = String.Empty
        ReadOnly Property DataStyle As String
            Get
                Return _dataStyle
            End Get
        End Property

        Private _IsSearchLive As Boolean = False
        ReadOnly Property IsSearch As Boolean
            Get
                Return _IsSearchLive
            End Get
        End Property

        Private _Title As String = String.Empty
        ReadOnly Property Title As String
            Get
                Return _Title
            End Get
        End Property

        Private _dataFormatSelected As DataFormatSelectedType = DataFormatSelectedType.Values
        ReadOnly Property DataFormatSelected As DataFormatSelectedType
            Get
                Return _dataFormatSelected
            End Get
        End Property

        Private _dataFormatSelectedCount As Integer = 3
        ReadOnly Property DataFormatSelected_Count As Integer
            Get
                Return _dataFormatSelectedCount
            End Get
        End Property

        Private _width As String = "auto"
        ReadOnly Property Width As String
            Get
                Return _width
            End Get
        End Property

        Private _disabled As String = False
        ReadOnly Property Disabled As Boolean
            Get
                Return _disabled
            End Get
        End Property

        Private _size As String = "auto"
        ReadOnly Property Size As String
            Get
                Return _size
            End Get
        End Property

        Private _loadData As JFMultipleSelect_Data = New JFMultipleSelect_Data
        ReadOnly Property LoadData() As JFMultipleSelect_Data
            Get
                Return _loadData
            End Get
        End Property

        Private _aditionalClass As String = String.Empty
        ReadOnly Property AditionalClass As String
            Get
                Return _aditionalClass
            End Get
        End Property

        Private _showSubText As Boolean = False
        ReadOnly Property ShowSubText As Boolean
            Get
                Return _showSubText
            End Get
        End Property

        Private _itemsSelected As IEnumerable(Of String) = New String() {}
        ReadOnly Property ItemsSelected As IEnumerable(Of String)
            Get
                Return _itemsSelected
            End Get
        End Property

        Private _IsEdit As Boolean = False
        ReadOnly Property IsEdit As Boolean
            Get
                Return _IsEdit
            End Get
        End Property
#End Region

#Region "Funciones"

        Public Function Set_IsMultiple(value As Boolean) As JFMultipleSelect_Options
            Me._isMultiple = value
            Return Me
        End Function

        Public Function Set_WithGroups(value As Boolean) As JFMultipleSelect_Options
            Me._WithGroups = value
            Return Me
        End Function

        Public Function Set_MaxElementsSelected(value As Integer) As JFMultipleSelect_Options
            If value <= 0 Then
                Throw New ArgumentException("La cantidad de elementos maximo permitido debe ser mayor a cero.")
            End If

            Me._MaxElementsSelected = value
            Return Me
        End Function

        Public Function Set_DataStyle(value As String) As JFMultipleSelect_Options
            Me._dataStyle = value
            Return Me
        End Function

        Public Function Set_IsSearchLive(value As Boolean) As JFMultipleSelect_Options
            Me._IsSearchLive = value
            Return Me
        End Function

        Public Function Set_Title(value As String) As JFMultipleSelect_Options
            Me._Title = value
            Return Me
        End Function

        Public Function Set_DataFormatSelected(value As DataFormatSelectedType) As JFMultipleSelect_Options
            Me._dataFormatSelected = value
            Return Me
        End Function

        Public Function Set_DataFormatSelected(value As DataFormatSelectedType, cantidad As Integer) As JFMultipleSelect_Options
            If value <= 0 Then
                Throw New ArgumentException("La cantidad de elementos debe ser mayor a cero.")
            End If

            Me._dataFormatSelected = value
            Me._dataFormatSelectedCount = cantidad

            Return Me
        End Function

        Public Function Set_Width(value As String) As JFMultipleSelect_Options
            Me._width = value
            Return Me
        End Function

        Public Function Set_Disabled(value As Boolean) As JFMultipleSelect_Options
            Me._disabled = value
            Return Me
        End Function

        Public Function Set_Size(value As String) As JFMultipleSelect_Options
            Me._size = value
            Return Me
        End Function

        Public Function LoadItems(data As JFMultipleSelect_Data) As JFMultipleSelect_Options
            Me._loadData = data
            Return Me
        End Function

        Public Function AddClass(value As String) As JFMultipleSelect_Options
            Me._aditionalClass = value
            Return Me
        End Function

        Public Function Set_ShowSubText(value As Boolean) As JFMultipleSelect_Options
            Me._showSubText = value
            Return Me
        End Function

        Public Function Set_ItemsSelected(items As IEnumerable(Of String)) As JFMultipleSelect_Options
            Me._itemsSelected = items
            Return Me
        End Function

        Public Function Set_IsEdit(value As Boolean) As JFMultipleSelect_Options
            Me._IsEdit = value
            Return Me
        End Function

#End Region
    End Class
End Namespace
