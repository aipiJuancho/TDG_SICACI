Imports System.Linq.Expressions

Namespace Controls
    Public Enum jfBSGrid_SelectionMode
        [Single] = 0
        Multiple = 1
        [Nothing] = 2
    End Enum

    Public Class jfBSGrid(Of T)

        Private _htmlHelper As HtmlHelper(Of T)
        Private _id As String

        'Propiedades adicionales de configuración
        Private _pageNum As Integer = 1
        Private _rowPerPage As Integer = 10
        Private _maxRowPerPage As Integer = 100
        Private _rowPrimaryKey As String = ""
        Private _rowSelectionMode As jfBSGrid_SelectionMode = jfBSGrid_SelectionMode.Single
        Private _showRowNumbers As Boolean = True
        Private _showSortingIndicators As Boolean = True
        Private _ajaxLoadData As String = ""

        'Manejo de eventos por parte del JF
        Private _onClick As String = ""
        Private _onCellClick As String = ""

        'Class adicionales para el diseño segun Bootstraps
        Private _dataTableClass As String = "table table-hover table-striped"
        Private _selectedTrClass As String = "warning"

        'Propiedades para definir las COLUMN y los criterios de ORDER
        Private _Columns As New List(Of jfBSGrid_Column)

        Public Sub New(htmlHelper As HtmlHelper(Of T), id As String)
            Me._htmlHelper = htmlHelper
            Me._id = id
        End Sub

#Region "Propiedades de Configuración"
        Public Function PageNum(pagina As Integer) As jfBSGrid(Of T)
            Me._pageNum = pagina
            Return Me
        End Function

        Public Function RowsPerPage(filas_xPagina As Integer) As jfBSGrid(Of T)
            Me._rowPerPage = filas_xPagina
            Return Me
        End Function

        Public Function MaxRowsPerPage(filas_max_xPagina As Integer) As jfBSGrid(Of T)
            Me._maxRowPerPage = filas_max_xPagina
            Return Me
        End Function

        Public Function RowSelectionMode(modo As jfBSGrid_SelectionMode) As jfBSGrid(Of T)
            Me._rowSelectionMode = modo
            Return Me
        End Function

        Public Function ShowRowNumbers(mostrar As Boolean) As jfBSGrid(Of T)
            Me._showRowNumbers = mostrar
            Return Me
        End Function

        Public Function ShowSortingIndicators(mostrar As Boolean) As jfBSGrid(Of T)
            Me._showSortingIndicators = mostrar
            Return Me
        End Function

        Public Function AjaxLoadData(url As String) As jfBSGrid(Of T)
            Me._ajaxLoadData = url
            Return Me
        End Function

        Public Function RowPrimaryKey(column As String) As jfBSGrid(Of T)
            Me._rowPrimaryKey = column
            Return Me
        End Function
#End Region

        Public Function onClick(handler As String) As jfBSGrid(Of T)
            Me._onClick = handler
            Return Me
        End Function

        Public Function onCellClick(handler As String) As jfBSGrid(Of T)
            Me._onCellClick = handler
            Return Me
        End Function

        Public Function dataTableClass(clases As String) As jfBSGrid(Of T)
            Me._dataTableClass = clases
            Return Me
        End Function

        Public Function selectedTrClass(clases As String) As jfBSGrid(Of T)
            Me._selectedTrClass = clases
            Return Me
        End Function

        Public Function AddColumn(Of TProperty)(propiedades As Expression(Of Func(Of T, TProperty))) As jfBSGrid(Of T)
            Dim decode = Me.DecodeMembersField(propiedades)
            Me._Columns.Add(New jfBSGrid_Column(decode))
            Return Me
        End Function

        Public Function RenderHtml(Optional attrAdd As String = "") As MvcHtmlString
            Dim strBuilder As New StringBuilder
            strBuilder.Append(String.Format("<div id=""{0}"" {1}>", Me._id, attrAdd))
            strBuilder.Append("</div>")

            Return MvcHtmlString.Create(strBuilder.ToString)
        End Function

        Public Function GetJavaScript(Optional optGrid As String = "") As MvcHtmlString
            Dim strBuilder As New StringBuilder
            strBuilder.Append(String.Format("$('#{0}').bs_grid(", Me._id))
            strBuilder.Append("{")
            strBuilder.Append(String.Format("pageNum: {0},", Me._pageNum))
            strBuilder.Append(String.Format("rowsPerPage: {0},", Me._rowPerPage))
            strBuilder.Append(String.Format("maxRowsPerPage: {0},", Me._maxRowPerPage))
            strBuilder.Append(String.Format("row_primary_key: '{0}',", Me._rowPrimaryKey))
            strBuilder.Append(String.Format("rowSelectionMode: {0},", If(Me._rowSelectionMode = jfBSGrid_SelectionMode.Single, "'single'", If(Me._rowSelectionMode = jfBSGrid_SelectionMode.Multiple, "'multiple'", "false"))))
            strBuilder.Append("useFilters: false,")
            strBuilder.Append(String.Format("showRowNumbers: {0},", If(Me._showRowNumbers, "true", "false")))
            strBuilder.Append(String.Format("showSortingIndicator: {0},", If(Me._showSortingIndicators, "true", "false")))
            strBuilder.Append("useSortableLists: false,")
            strBuilder.Append(String.Format("ajaxFetchDataURL: '{0}',", Me._ajaxLoadData))

            'Agregamos si existen Handler para los eventos
            If Not String.IsNullOrEmpty(Me._onClick) Then strBuilder.Append(String.Format("onRowClick: {0},", Me._onClick))
            If Not String.IsNullOrEmpty(Me._onCellClick) Then strBuilder.Append(String.Format("onCellClick: {0},", Me._onCellClick))

            'Añadimos las clases adicionales de diseño de la tabla
            strBuilder.Append(String.Format("dataTableClass: '{0}',", Me._dataTableClass))
            strBuilder.Append(String.Format("selectedTrClass: '{0}',", Me._selectedTrClass))

            'Agregamos las columnas
            strBuilder.Append("columns: [")
            For Each column As jfBSGrid_Column In Me._Columns
                strBuilder.Append(column.ToString)
                strBuilder.Append(",")
            Next
            strBuilder.Append("]")

            strBuilder.Append("}")
            strBuilder.Append(");")
            Return MvcHtmlString.Create(strBuilder.ToString)
        End Function


        Private Function DecodeMembersField(Of TProperty)(prop As Expression(Of Func(Of T, TProperty))) As jfBSGrid_DecodeColumn
            Dim jfDecode As New jfBSGrid_DecodeColumn
            Dim modelMetaData As ModelMetadata = modelMetaData.FromLambdaExpression(prop, Me._htmlHelper.ViewData)

            jfDecode.Name = modelMetaData.PropertyName
            jfDecode.Display = modelMetaData.GetDisplayName()
            jfDecode.Visible = Not modelMetaData.AdditionalValues("hideLabel")
            Return jfDecode
        End Function

    End Class
End Namespace
