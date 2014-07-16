Imports System.Runtime.CompilerServices
Imports System.Linq.Expressions


Namespace Helpers

    Public Module JFHelper
        ''' <summary>
        ''' Crea una nueva tabla sin ligamento a un FORM, en el cual se pueden agregar filas o controles que se deseen mostrar al usuario.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="helper"></param>
        ''' <param name="titulo">El titulo o etiqueta “Fieldset” que tendrá nuestra formulario.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()> _
        Public Function TableFields(Of T)(helper As HtmlHelper(Of T), titulo As String) As JFGrid(Of T)
            Return New JFGrid(Of T)(helper, titulo)
        End Function

        ''' <summary>
        ''' Crea una nueva tabla sin ligamento a un FORM (no se incluye la instruccion JavaScript para comprobar la validacion de datos)
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="helper"></param>
        ''' <param name="titulo">El titulo o etiqueta “Fieldset” que tendrá nuestra formulario.</param>
        ''' <returns></returns>
        ''' <remarks>Este método se utiliza mas que todo para las vistas parciales cargadas dentro de un Dialog, ya que no requieren de la instrucción para validar los datos ya que se implementa a través de la librería de Jerti.</remarks>
        <Extension()> _
        Public Function TableFieldsFor(Of T)(helper As HtmlHelper(Of T), titulo As String) As JFGrid(Of T)
            Return New JFGrid(Of T)(helper, titulo)
        End Function

        <Extension()> _
        Public Function TableFieldsFor(Of T)(helper As HtmlHelper(Of T), IDForm As String, titulo As String) As JFGrid(Of T)
            Return New JFGrid(Of T)(helper, IDForm, titulo)
        End Function

        <Extension()> _
        Public Function TableFieldsFor(Of T)(helper As HtmlHelper(Of T), IDForm As String, titulo As String, prefix As String) As JFGrid(Of T)
            Return New JFGrid(Of T)(helper, IDForm, titulo, prefix)
        End Function

        <Extension()> _
        Public Function ErrorInformation(helper As HtmlHelper, titulo As String, mensaje As String, classIcon As String, Optional htmlAttributesTitulo As String = "") As MvcHtmlString
            Dim strBuilder As New StringBuilder
            strBuilder.Append("<div class=""jerti-error-information"">") _
                .Append("<div class=""error-information-titulo"">") _
                .Append(String.Format("<span class=""ui-icon {0}"" style=""{1}""></span>", classIcon, htmlAttributesTitulo)) _
                .Append(titulo) _
                .Append("</div>") _
                .Append("<div class=""error-information-msj"">") _
                .Append(mensaje) _
                .Append("</div>") _
                .Append("</div>")
            Return MvcHtmlString.Create(strBuilder.ToString)
        End Function

        ''' <summary>
        ''' Permite a través del Jerti Framework (JF) cargar de una manera sencilla, una vista parcial del proyecto. El JF creara la animación mientras se espera a ser cargada la vista parcial y el mensaje de espera se podrá personalizar.
        ''' </summary>
        ''' <param name="helper"></param>
        ''' <param name="id">Identificador único que recibirá el contenedor (DIV) donde se cargará la vista parcial.</param>
        ''' <param name="URLAction">URL de donde se debe ir a recuperar la vista parcial que se desea cargar.</param>
        ''' <param name="loadOnDocumentLoad">La vista parcial, ¿Deberá cargarse cuando ocurra el evento "document.ready()" del DOM o el usuario definirá su propia lógica para carga la vista parcial?</param>
        ''' <param name="msjLoading">Mensaje de espera que aparecerá en la animación de carga mientras se recupera la vista parcial.</param>
        ''' <param name="parametrosByJS">Parámetros extras que necesite la vista parcial para poder ser cargada. El código se escribe en JavaScript y se agregara al evento “setParametros” del contenedor “DIV” para agregar los parámetros y cargar la vista.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()> _
        Public Function LoadPartialViewAJAX(helper As HtmlHelper, id As String, URLAction As String,
                                            Optional loadOnDocumentLoad As Boolean = True,
                                            Optional msjLoading As String = "Por favor, espere un momento...",
                                            Optional parametrosByJS As String = "") As MvcHtmlString

            Dim strBuilder As New StringBuilder

            'Tenemos q saber si es necesario INYECTAR script para cargar automaticamente la vista parcial cuando ocurra el evento
            'del document.ready().
            If loadOnDocumentLoad Then
                strBuilder.Append("<script type=""text/javascript"">") _
                    .Append("$(function () {")

                'Verificamos si se le van a pasar parametros a la vista parcial por codigo JavaScript
                If Not parametrosByJS.Equals("") Then
                    strBuilder.Append(String.Format("$('#{0}').on('setParametros', function () {1}", id, "{")) _
                        .Append(parametrosByJS) _
                        .Append("});")
                End If

                strBuilder.Append(String.Format("$('#{0}').loadPartialView();", id)) _
                    .Append("});") _
                    .Append("</script>")
            End If

            'Determinamos si aparecerá oculto la ventana de LOADING al momento de crear el objeto
            Dim styleHidden As String = IIf(Not loadOnDocumentLoad, " style=""display: none;""", String.Empty)

            'Crearemos un DIV con el ID especificado y anexaremos la URL al cual hace referencia la carga por medio de AJAX
            strBuilder.Append("<div class=""jerti-container-partialview"">") _
                .Append(String.Format("<div id=""j-load-{1}"" class=""jerti-partialview-loading""{0}>", styleHidden, id)) _
                .Append("<div>") _
                .Append("<div class=""loading-big-icon big-icon""></div>") _
                .Append(String.Format("<div>{0}</div>", msjLoading)) _
                .Append("</div>") _
                .Append("</div>") _
                .Append(String.Format("<div class=""jerti-div-dynamic"" id=""{0}"" data-jerti-partialview=""{1}"" data-jerti-loading=""#j-load-{0}"">", id, URLAction)) _
                .Append("</div>") _
                .Append("</div>")

            Return MvcHtmlString.Create(strBuilder.ToString)
        End Function

        ''' <summary>
        ''' Crea las etiquetas necesarias y el código JavaScript para poder mostrar una vista parcial dentro de un Dialog de jQueryUI.
        ''' </summary>
        ''' <param name="helper"></param>
        ''' <param name="idTrigger">Identificador del control que disparará mostrar el “dialog” en la pagina.</param>
        ''' <param name="name">Nombre único que se va establecer a los controles creados.</param>
        ''' <param name="titulo">Titulo de la ventana del control “dialog”.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()> _
        Public Function loadDialogPartialViewAJAX(helper As HtmlHelper, idTrigger As String, name As String, titulo As String, Optional IsAdd As Boolean = True) As MvcHtmlString
            Return loadDialogPartialViewAJAX(helper, idTrigger, name, titulo, Nothing, IsAdd)
        End Function

        ''' <summary>
        ''' Crea las etiquetas necesarias y el código JavaScript para poder mostrar una vista parcial dentro de un Dialog de jQueryUI.
        ''' </summary>
        ''' <param name="helper"></param>
        ''' <param name="idTrigger">Identificador del control que disparará mostrar el “dialog” en la pagina.</param>
        ''' <param name="name">Nombre único que se va establecer a los controles creados</param>
        ''' <param name="titulo">Titulo de la ventana del control “dialog”.</param>
        ''' <param name="parametros_jQueryUIDialog">Colección de los parámetros propios del control jQueryUI Dialog, el JF se encargara de transformar estos parámetros y se los pasara al momento de carga el Dialog.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()> _
        Public Function loadDialogPartialViewAJAX(helper As HtmlHelper, idTrigger As String, name As String,
                                                  titulo As String, parametros_jQueryUIDialog As Object,
                                                  Optional IsAdd As Boolean = True) As MvcHtmlString
            Dim strBuilder As New StringBuilder

            'Generamos el Script necesario para cargar la vista parcial en un Dialog
            strBuilder.Append("<script type=""text/javascript"">") _
                .Append("$(function () {") _
                .Append(String.Format("$('{0}').attr('data-jerti-dlg-container', '#div-{1}');", idTrigger, name)) _
                .Append(String.Format("$('{0}').dialogPartialView();", idTrigger)) _
                .Append("});") _
                .Append("</script>")

            'Crearemos un DIV con el ID especificado y anexaremos la URL al cual hace referencia la carga por medio de AJAX
            strBuilder.Append("<div class=""jerti-container-partialview hide-div-dialog """) _
                .Append(String.Format("id=""div-{0}"" data-jerti-dlg-id=""#dlg-{0}"" data-jerti-dlg-titulo=""{1}"" data-jerti-dlg-parameters=""{2}"" data-jerti-dlg-trigger=""{3}"" data-jerti-dlg-form=""form-{0}"" data-jerti-dlg-add=""{4}"">",
                                      name, titulo,
                                      JFCodeExtension.ConvertDictionaryToStringParameters(parametros_jQueryUIDialog),
                                      idTrigger, If(IsAdd, 1, 0))) _
                .Append("</div>")

            Return MvcHtmlString.Create(strBuilder.ToString)
        End Function

        ''' <summary>
        ''' Inicia las etiquetas de DIV y FORM que son necesarias para las vistas parciales contenidas dentro de un DIALOG utilizando el JF.
        ''' </summary>
        ''' <param name="helper"></param>
        ''' <param name="url">Dirección donde se mandaran los datos al servidor.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()> _
        Public Function BeginDialogPartialView(helper As HtmlHelper, url As String) As JFDialogPartialView
            Return New JFDialogPartialView(helper, url, Nothing)
        End Function

        ''' <summary>
        ''' Inicia las etiquetas de DIV y FORM que son necesarias para las vistas parciales contenidas dentro de un DIALOG utilizando el JF.
        ''' </summary>
        ''' <param name="helper"></param>
        ''' <param name="url">Dirección donde se mandaran los datos al servidor.</param>
        ''' <param name="htmlAttributesDIV">Diccionario de atributos que se van aplicar al DIV que se va a crear como contenedor de los datos de la vista parcial.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()> _
        Public Function BeginDialogPartialView(helper As HtmlHelper, url As String, htmlAttributesDIV As Object) As JFDialogPartialView
            Return New JFDialogPartialView(helper, url, htmlAttributesDIV)
        End Function

        <Extension()> _
        Public Function TableReadOnlyFor(Of T)(helper As HtmlHelper(Of T), titulo As String) As JFGridReadOnly(Of T)
            Return New JFGridReadOnly(Of T)(helper, titulo)
        End Function

        <Extension()> _
        Public Function ButtonJerti(helper As HtmlHelper, ID As String, text As String) As JFButton
            Return New JFButton(ID, text)
        End Function

        ''' <summary>
        ''' Crea un nuevo titulo en la página. El estilo del titulo estará definido por la clase “tit-legend”.
        ''' </summary>
        ''' <param name="tit">Texto que deseo mostrar como titulo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()> _
        Public Function TituloFieldSet(htmlHelper As HtmlHelper, tit As String) As MvcHtmlString
            Return TituloFieldSet(htmlHelper, tit, New Object())
        End Function

        ''' <summary>
        ''' Crea un nuevo titulo en la página. El estilo del titulo estará definido por la clase “tit-legend”.
        ''' </summary>
        ''' <param name="tit">Texto que deseo mostrar como titulo</param>
        ''' <param name="htmlAtributos">Diccionario de atributos HTML que serán insertados en la creación del objeto.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()> _
        Public Function TituloFieldSet(htmlHelper As HtmlHelper, tit As String, htmlAtributos As Object) As MvcHtmlString
            Dim str As New StringBuilder
            str.Append(String.Format("<h3 class=""tit-legend"" {0}>{1}",
                                     JFOptionsFieldsReadOnly.ConvertDictionaryToStringHTML(htmlAtributos),
                                     tit)) _
                .Append("</h3>")
            Return MvcHtmlString.Create(str.ToString)
        End Function

        ''' <summary>
        ''' Proporciona el estándar para los títulos de la aplicación en una página de texto.
        ''' </summary>
        ''' <param name="htmlHelper"></param>
        ''' <param name="tit">Texto que deseo mostrar como titulo.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()> _
        Public Function TituloHeader(htmlHelper As HtmlHelper, tit As String) As MvcHtmlString
            Return TituloHeader(htmlHelper, tit, New Object())
        End Function

        ''' <summary>
        ''' Proporciona el estándar para los títulos de la aplicación en una página de texto.
        ''' </summary>
        ''' <param name="htmlHelper"></param>
        ''' <param name="tit">Texto que deseo mostrar como titulo.</param>
        ''' <param name="htmlAtributos">Diccionario de atributos HTML que serán insertados en la creación del objeto.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()> _
        Public Function TituloHeader(htmlHelper As HtmlHelper, tit As String, htmlAtributos As Object) As MvcHtmlString
            Dim str As New StringBuilder
            str.Append(String.Format("<h5 class=""titulo-h2"" {0}>{1}",
                                     JFOptionsFieldsReadOnly.ConvertDictionaryToStringHTML(htmlAtributos),
                                     tit)) _
                .Append("</h5>")
            Return MvcHtmlString.Create(str.ToString)
        End Function

        ''' <summary>
        ''' Crea un nueva etiqueta div, donde se podrá agregar el texto de manera ordenada en la pagina.
        ''' </summary>
        ''' <param name="htmlHelper"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()> _
        Public Function TextPage(htmlHelper As HtmlHelper) As JFTextPage
            Return TextPage(htmlHelper, Nothing)
        End Function

        ''' <summary>
        ''' Crea un nueva etiqueta div, donde se podrá agregar el texto de manera ordenada en la pagina.
        ''' </summary>
        ''' <param name="htmlHelper"></param>
        ''' <param name="HTMLatributos">Atributos HTML que se anexaran al div contenedor de todos los textos.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()> _
        Public Function TextPage(htmlHelper As HtmlHelper, HTMLatributos As Object) As JFTextPage
            Return New JFTextPage(HTMLatributos)
        End Function

        ''' <summary>
        ''' Crea un nuevo formulario segun la documentación de BootStrap 3.0
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="helper"></param>
        ''' <param name="idFormulario">ID que se colocarà en el INPUT de la etiqueta HTML</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension()> _
        Public Function FormularioFor(Of T)(helper As HtmlHelper(Of T), idFormulario As String) As JFForm(Of T)
            Return New JFForm(Of T)(helper, idFormulario)
        End Function

        <Extension()> _
        Public Function bootstrapModal(helper As HtmlHelper, id As String, titulo As String, btnTrigger As String, Optional size As JFModalSize = JFModalSize.Default) As MvcHtmlString
            Dim html As New StringBuilder
            If String.IsNullOrWhiteSpace(titulo) Then Throw New ArgumentException("No se ha declarado el titulo de la ventana de dialogo o es una cadena vacia")
            If String.IsNullOrWhiteSpace(id) Then Throw New ArgumentException("No se ha definido un identificador (ID) para la ventana modal")

            'Establecemos ciertas CLASS segun los parametros especificados
            Dim _size As String = If(size = JFModalSize.Default, String.Empty, If(size = JFModalSize.Small, "modal-sm", "modal-lg"))

            'Antes que nada, generamos el SCRIPT necesario para enlazar los controles
            html.Append("<script type=""text/javascript"">") _
                .Append("$(function () {") _
                .Append(String.Format("$('{0}').attr('data-jf-modal', '#{1}');", btnTrigger, id)) _
                .Append(String.Format("$('{0}').bootstrapsDialogPartialView();", btnTrigger)) _
                .Append("});") _
                .Append("</script>")

            html.Append(String.Format("<div class=""modal fade"" id=""{0}"" tabindex=""-1"" role=""dialog"" aria-labelledby=""myModalLabel"" aria-hidden=""true"" data-jf-body=""#body-{0}"" data-jf-trigger=""{1}"">", id, btnTrigger))
            html.Append(String.Format("<div class=""modal-dialog {0}"">", _size))
            html.Append("<div class=""modal-content"">")
            html.Append("<div class=""modal-header"">")
            html.Append("<button type=""button"" class=""close"" data-dismiss=""modal""><span aria-hidden=""true"">&times;</span><span class=""sr-only"">Cerrar</span></button>")
            html.Append(String.Format("<h4 class=""modal-title"" id=""myModalLabel"">{0}</h4>", titulo))
            html.Append("</div>")

            'Construimos el DIV donde se desplegarà el contenido del Modal
            html.Append(String.Format("<div class=""modal-body"" id=""body-{0}"" data-jf-modal=""#{0}"" data-jf-trigger=""{1}"">", id, btnTrigger))
            html.Append("</div>")

            'Construimos la barra de botones o footer del cuadro de dialogo
            html.Append("<div class=""modal-footer"">")
            html.Append("<button type=""button"" class=""btn btn-default"" data-dismiss=""modal"">Cerrar</button>")
            html.Append(String.Format("<button type=""button"" class=""btn btn-primary"" id=""save-{0}"">Guardar Cambios</button>", id))
            html.Append("</div>")

            'Por ultimo, cerramos los contenedores del Dialog
            html.Append("</div>")
            html.Append("</div>")
            html.Append("</div>")

            Return MvcHtmlString.Create(html.ToString)
        End Function
    End Module
End Namespace

