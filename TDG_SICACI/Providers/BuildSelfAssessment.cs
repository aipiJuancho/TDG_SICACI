using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TDG_SICACI.Database;

namespace TDG_SICACI.Providers
{
    public class BuildSelfAssessment
    {

        private IEnumerable<SP_GET_NORMA_ISO_MODEL> _tabsHeaders;
        private IEnumerable<SP_GET_NORMA_ISO_MODEL> _contentStructure;
        private StringBuilder _builder;
        private IEnumerable<SP_GET_NORMA_ISO_MODEL> _hijos;
        private IEnumerable<SP_CONSTRUIR_SELF_MODEL> _infoPreguntas;


        public BuildSelfAssessment(IEnumerable<SP_GET_NORMA_ISO_MODEL> headers, IEnumerable<SP_GET_NORMA_ISO_MODEL> content, IEnumerable<SP_CONSTRUIR_SELF_MODEL> self)
        {
            this._tabsHeaders = headers;
            this._contentStructure = content;
            this._infoPreguntas = self;
            this._builder = new StringBuilder();
        }

        public MvcHtmlString Render()
        {
            SP_GET_NORMA_ISO_MODEL modelHeader;
            IEnumerable<SP_GET_NORMA_ISO_MODEL> estructura;

            /*PASO 1: Construimos el TAB segùn los Padres*/
            _builder.Append("<ul class=\"nav nav-tabs\" role=\"tablist\">");
            for (int i = 0; i <= _tabsHeaders.Count() - 1; i++)
            {
                //Verificamos si es el primer elemento, para mostrarlo activo
                if (i.Equals(0))
                    _builder.Append("<li class=\"active\">");
                else
                    _builder.Append("<li>");

                //Añadimos la etiqueta de Link para crear
                modelHeader = _tabsHeaders.ElementAt(i);
                _builder.Append(string.Format("<a href=\"#{0}\" role=\"tab\" data-toggle=\"tab\">{1}</a>",
                    modelHeader.ID_JERARQUIA, modelHeader.DESCRIPCION_JERARQUIA));

                _builder.Append("</li>");
            }
            _builder.Append("</ul>");

            /*PASO 2: Construimos los contenedores de cada uno de los TABS junto con sus elementos internos**/
            _builder.Append("<div class=\"tab-content\">");
            this._builder.Append("<br />");
            for (int i = 0; i <= _tabsHeaders.Count() - 1; i++)
            {
                modelHeader = _tabsHeaders.ElementAt(i);

                //Verificamos si es el primer elemento, para mostrarlo activo
                if (i.Equals(0))
                    _builder.Append(string.Format("<div class=\"tab-pane active\" id=\"{0}\">", modelHeader.ID_JERARQUIA));
                else
                    _builder.Append(string.Format("<div class=\"tab-pane\" id=\"{0}\">", modelHeader.ID_JERARQUIA));

                this.Construir(modelHeader.ID_JERARQUIA.Value);


                _builder.Append("</div>");  //Para finalizar, cerramos este contenido
            }
            _builder.Append("</div>");
            
            return MvcHtmlString.Create(_builder.ToString());
        }

        public void Construir(int padre)
        {
            this._hijos = this._contentStructure.Where(h => h.JERARQUIA_PADRE.Equals(padre));
            if (this._hijos.Count() > 0)
            {
                foreach(SP_GET_NORMA_ISO_MODEL item in this._hijos) {
                    if (item.NIVEL.Equals(1))
                    {
                        this._builder.Append("<div class=\"panel panel-default\">")
                            .Append(string.Format("<div class=\"panel-heading\" style=\"font-size: 18px\">{1} - {0}</div>", item.DESCRIPCION_JERARQUIA, item.ARBOL))
                            .Append("<div class=\"panel-body\">");
                        this.Construir(item.ID_JERARQUIA.Value);
                        this._builder.Append("</div>")
                            .Append("</div>");
                    }
                    else if(item.NIVEL.Equals(2)) {
                        if (item.ES_PREGUNTA.Equals("N"))
                        {  //Eso quiere decir que es un HEADER
                            this._builder.Append(string.Format("<h4>{1} - {0}</h4>", item.DESCRIPCION_JERARQUIA, item.ARBOL));
                            this.Construir(item.ID_JERARQUIA.Value);
                        }
                        else
                        {
                            ConstruirPregunta(item.ID_JERARQUIA.Value);
                        }
                    }
                    else
                    {
                        if (item.ES_PREGUNTA.Equals("N"))   //Eso quiere decir que es un HEADER
                        {
                            switch (item.NIVEL.Value)
                            {
                                case 2:
                                    this._builder.Append(string.Format("<h4>{0}</h4>", item.DESCRIPCION_JERARQUIA));
                                    break;
                                case 3:
                                    this._builder.Append(string.Format("<h5>{0}</h5>", item.DESCRIPCION_JERARQUIA));
                                    break;
                                default:
                                    this._builder.Append(string.Format("<h6>{0}</h6>", item.DESCRIPCION_JERARQUIA));
                                    break;
                            }
                        }
                        else
                            ConstruirPregunta(item.ID_JERARQUIA.Value);

                        //this._builder.Append(string.Format("<h3>{0}</h3>", item.DESCRIPCION_JERARQUIA));
                        this.Construir(item.ID_JERARQUIA.Value);
                    }
                }
            }
        }

        public void ConstruirPregunta(int idJerarquia)
        {
            //STEP 1: Verificamos que si tenga la información de la preguntas asociada
            var infoPregunta = this._infoPreguntas.Where(s => s.ID_JERARQUIA.Value.Equals(idJerarquia) && s.CLASIFICACION.Equals("S")).FirstOrDefault();
            if (infoPregunta != null)   //Verificamos que tenga asociada una Pregunta
            {
                /*STEP 2: Verificamos el Tipo de Pregunta que se debe de crear*/
                switch (infoPregunta.TIPO_PREGUNTA)
                {
                    case "Abierta":
                        Build_PreguntaAbierta(infoPregunta);
                        break;
                    case "Opción múltiple":
                        Build_PreguntaOpcionMultiple(infoPregunta);
                        break;
                    case "Selección múltiple":
                        Build_PreguntaSeleccionMultiple(infoPregunta);
                        break;
                }

                /*STEP 3: Verificamos si la pregunta pide algun documento que se adjunte*/
                if (infoPregunta.ADJUNTAR_DOCUMENTO.Equals("S"))
                {
                    this._builder.Append(string.Format("<div style=\"padding: 10px 20px;\">", (infoPregunta.NIVEL.Value -1) * 20))
                        .Append("<span class=\"label-file-attach\">Por favor adjunte un documento que respalde tu respuesta (Max. 4MB)</span>")
                        .Append(string.Format("<input name=\"{0}\" type=\"file\" data-tipo=\"file\" class=\"form-control input-file-attach\" accept=\"{2}\">", 
                            infoPregunta.ID_PREGUNTA, idJerarquia, 
                            (infoPregunta.TIPO_DOCUMENTO.Equals("PDF") ? "application/pdf" : "image/*")));
                    this._builder.Append("</div>");
                }

                this._builder.Append("</div>");

                

                /*STEP 4: Por ultimo, verificamos si tiene preguntas Adicionales de GIDEM*/
                if (this._infoPreguntas.Where(p => p.ID_JERARQUIA.Value.Equals(infoPregunta.ID_JERARQUIA.Value) && p.CLASIFICACION.Equals("N")).Count() > 0)
                {
                    this._builder.Append(string.Format("<div class=\"panel panel-info\" style=\"margin: 5px {0}px 20px {0}px;\">", (infoPregunta.NIVEL.Value -1) * 25))
                        .Append("<div class=\"panel-heading\" style=\"padding-top: 4px; padding-bottom: 4px;\">Preguntas Adicionales - GIDEM</div>")
                        .Append("<div class=\"panel-body\">");

                    //Empezamos a reconstruir todas las preguntas adicioanles
                    var _pregAdds = this._infoPreguntas.Where(p => p.ID_JERARQUIA.Value.Equals(infoPregunta.ID_JERARQUIA.Value) && p.CLASIFICACION.Equals("N")).OrderBy(p => p.ARBOL).ToArray();
                    foreach (SP_CONSTRUIR_SELF_MODEL preguntaAdd in _pregAdds)
                    {
                        switch (preguntaAdd.TIPO_PREGUNTA)
                        {
                            case "Abierta":
                                Build_PreguntaAbierta(preguntaAdd);
                                this._builder.Append("</div>");
                                break;
                            case "Opción múltiple":
                                Build_PreguntaOpcionMultiple(preguntaAdd);
                                this._builder.Append("</div>");
                                break;
                            case "Selección múltiple":
                                Build_PreguntaSeleccionMultiple(preguntaAdd);
                                this._builder.Append("</div>");
                                break;
                        }

                        if (preguntaAdd.ADJUNTAR_DOCUMENTO.Equals("S"))
                        {
                            this._builder.Append(string.Format("<div style=\"padding: 10px 20px;\">", (preguntaAdd.NIVEL.Value - 1) * 20))
                                .Append("<span class=\"label-file-attach\">Si deseas, puedes adjuntar un documento que respalde tu respuesta (Max. 4MB)</span>")
                                .Append(string.Format("<input name=\"{0}\" type=\"file\" data-tipo=\"file\" class=\"form-control input-file-attach\" accept=\"{2}\">",
                                    preguntaAdd.ID_PREGUNTA, idJerarquia,
                                    (preguntaAdd.TIPO_DOCUMENTO.Equals("PDF") ? "application/pdf" : "image/*")));
                            this._builder.Append("</div>");
                        }

                    }
                    this._builder.Append("</div>")
                        .Append("</div>");

                }
                
            }
        }

        public void Build_PreguntaAbierta(SP_CONSTRUIR_SELF_MODEL pregunta)
        {
            //Definimos el titulo de la pregunta
            if (pregunta.CLASIFICACION.Equals("N")) //Es Pregunta de GIDEM
                this._builder.Append(string.Format("<div class=\"form-group {0}\">", (pregunta.CLASIFICACION.Equals("N") ? "input-group-gidem" : "")));
            else
                this._builder.Append(string.Format("<div class=\"form-group {0}\" style=\"margin-left: {1}px; margin-right: {1}px\">", (pregunta.CLASIFICACION.Equals("N") ? "input-group-gidem" : ""),
                    ((pregunta.NIVEL.Value - 2) > 0 ? ((pregunta.NIVEL.Value - 2) * 20) : 0)));

            this._builder.Append(string.Format("<label for=\"{0}\" class=\"{3}\">{2} - {1}</label>", pregunta.ID_PREGUNTA,
                    pregunta.DESCRIPCION_JERARQUIA, pregunta.ARBOL,
                    (pregunta.CLASIFICACION.Equals("N") ? "input-gidem" : "")));

            this._builder.Append("<div class=\"input-group\">");
            this._builder.Append(string.Format("<input type=\"text\" class=\"form-control {2}\" name=\"{0}\" data-jerarquia=\"{1}\" data-tipo=\"PA\">",
                    pregunta.ID_PREGUNTA, pregunta.ID_JERARQUIA,
                    (pregunta.CLASIFICACION.Equals("N") ? "input-sm" : "")));

            //Comprobamos si la pregunta tiene asociado un comentario de ayuda
            if (!string.IsNullOrEmpty(pregunta.COMENTARIO_PREGUNTA))
                this._builder.Append(string.Format("<span title=\"&lt;div&gt;{0}{2}&lt;/div&gt;\" class=\"input-group-addon glyphicon glyphicon-info-sign icon-help-options tooltip-system {1}\" style=\"top: 0;display: table-cell;\"></span>",
                    pregunta.COMENTARIO_PREGUNTA,
                    (pregunta.CLASIFICACION.Equals("N") ? "input-sm" : ""),
                    (pregunta.LINK_COMENTARIO != null ? string.Format(".&lt;a target='_blank' href='{0}' class='link-comentario' &gt;Ver enlace&lt;/a&gt;", pregunta.LINK_COMENTARIO) : "")));

            this._builder.Append("</div>");
        }

        public void Build_PreguntaOpcionMultiple(SP_CONSTRUIR_SELF_MODEL pregunta)
        {
            //Definimos el titulo de la pregunta
            if (pregunta.CLASIFICACION.Equals("N")) //Es Pregunta de GIDEM
                this._builder.Append(string.Format("<div class=\"form-group {0}\">", (pregunta.CLASIFICACION.Equals("N") ? "input-group-gidem" : "")));
            else
                this._builder.Append(string.Format("<div class=\"form-group {0}\" style=\"margin-left: {1}px; margin-right: {1}px\">", (pregunta.CLASIFICACION.Equals("N") ? "input-group-gidem" : ""),
                    ((pregunta.NIVEL.Value - 2) > 0 ? ((pregunta.NIVEL.Value - 2) * 20) : 0)));

            this._builder.Append("<div>")
                .Append(string.Format("<label for=\"{0}\" class=\"{3}\">{2} - {1}", pregunta.ID_PREGUNTA,
                    pregunta.DESCRIPCION_JERARQUIA, pregunta.ARBOL,
                    (pregunta.CLASIFICACION.Equals("N") ? "input-gidem" : "")))
                .Append("</label>");

            //Comprobamos si la pregunta tiene asociado un comentario de ayuda
            if (!string.IsNullOrEmpty(pregunta.COMENTARIO_PREGUNTA))
                this._builder.Append(string.Format("<span title=\"&lt;div&gt;{0}{1}&lt;/div&gt;\" class=\"glyphicon glyphicon-info-sign icon-help-options tooltip-system\"></span>",
                    pregunta.COMENTARIO_PREGUNTA,
                    (pregunta.LINK_COMENTARIO != null ? string.Format(".&lt;a target='_blank' href='{0}' class='link-comentario' &gt;Ver enlace&lt;/a&gt;", pregunta.LINK_COMENTARIO) : "")));

            this._builder.Append("</div>");


            //Construimos las opciones multiples posibles
            var _opts = this._infoPreguntas.Where(p => p.ID_PREGUNTA.Value.Equals(pregunta.ID_PREGUNTA) && p.CLASIFICACION.Equals("O")).ToArray();

            foreach (SP_CONSTRUIR_SELF_MODEL optPregunta in _opts)
            {
                this._builder.Append(string.Format("<div class=\"radio {0}\">", (pregunta.CLASIFICACION.Equals("N") ? "group-radio-gidem" : "")))
                    .Append(string.Format("<label class=\"{0}\">", (pregunta.CLASIFICACION.Equals("N") ? "input-gidem" : "")))
                    .Append(string.Format("<input type=\"radio\" name=\"{0}\" value=\"{2}\" data-jerarquia=\"{1}\" data-tipo=\"OM\">",
                        optPregunta.ID_PREGUNTA, pregunta.ID_JERARQUIA, optPregunta.ID_TP_MULTIPLE))
                    .Append(optPregunta.DESCRIPCION_JERARQUIA)
                    .Append("</label>");

                //Evaluamos si tiene COMENTARIO esta respuesta
                if (!string.IsNullOrEmpty(optPregunta.COMENTARIO_PREGUNTA))
                    this._builder.Append(string.Format("<span title=\"&lt;div&gt;{0}{1}&lt;/div&gt;\" class=\"glyphicon glyphicon-info-sign icon-help-options tooltip-system\">",
                            optPregunta.COMENTARIO_PREGUNTA,
                    (optPregunta.LINK_COMENTARIO != null ? string.Format(".&lt;a target='_blank' href='{0}' class='link-comentario' &gt;Ver enlace&lt;/a&gt;", optPregunta.LINK_COMENTARIO) : "")))
                        .Append("</span>");

                this._builder.Append("</div>");
            }
        }

        public void Build_PreguntaSeleccionMultiple(SP_CONSTRUIR_SELF_MODEL pregunta)
        {
            //Definimos el titulo de la pregunta
            if (pregunta.CLASIFICACION.Equals("N")) //Es Pregunta de GIDEM
                this._builder.Append(string.Format("<div class=\"form-group {0}\">", (pregunta.CLASIFICACION.Equals("N") ? "input-group-gidem" : "")));
            else
                this._builder.Append(string.Format("<div class=\"form-group {0}\" style=\"margin-left: {1}px; margin-right: {1}px\">", (pregunta.CLASIFICACION.Equals("N") ? "input-group-gidem" : ""),
                    ((pregunta.NIVEL.Value - 2) > 0 ? ((pregunta.NIVEL.Value - 2) * 20) : 0)));

            this._builder.Append("<div>")
                .Append(string.Format("<label for=\"{0}\" class=\"{3}\">{2} - {1}", pregunta.ID_PREGUNTA,
                    pregunta.DESCRIPCION_JERARQUIA, pregunta.ARBOL,
                    (pregunta.CLASIFICACION.Equals("N") ? "input-gidem" : "")))
                .Append("</label>");

            //Comprobamos si la pregunta tiene asociado un comentario de ayuda
            if (!string.IsNullOrEmpty(pregunta.COMENTARIO_PREGUNTA))
                this._builder.Append(string.Format("<span title=\"&lt;div&gt;{0}{1}&lt;/div&gt;\" class=\"glyphicon glyphicon-info-sign icon-help-options tooltip-system\"></span>",
                    pregunta.COMENTARIO_PREGUNTA,
                    (pregunta.LINK_COMENTARIO != null ? string.Format(".&lt;a target='_blank' href='{0}' class='link-comentario' &gt;Ver enlace&lt;/a&gt;", pregunta.LINK_COMENTARIO) : "")));

            this._builder.Append("</div>");


            //Construimos las selecciones multiples posibles
            var _opts = this._infoPreguntas.Where(p => p.ID_PREGUNTA.Value.Equals(pregunta.ID_PREGUNTA) && p.CLASIFICACION.Equals("O")).ToArray();

            foreach (SP_CONSTRUIR_SELF_MODEL optPregunta in _opts)
            {
                this._builder.Append(string.Format("<div class=\"checkbox {0}\">", (pregunta.CLASIFICACION.Equals("N") ? "group-radio-gidem" : "")))
                    .Append(string.Format("<label class=\"{0}\">", (pregunta.CLASIFICACION.Equals("N") ? "input-gidem" : "")))
                    .Append(string.Format("<input type=\"checkbox\" name=\"{0}\" value=\"{2}\" data-jerarquia=\"{1}\" data-tipo=\"SM\">",
                        optPregunta.ID_PREGUNTA, pregunta.ID_JERARQUIA, optPregunta.ID_TP_MULTIPLE))
                    .Append(optPregunta.DESCRIPCION_JERARQUIA)
                    .Append("</label>");

                //Evaluamos si tiene COMENTARIO esta respuesta
                if (!string.IsNullOrEmpty(optPregunta.COMENTARIO_PREGUNTA))
                    this._builder.Append(string.Format("<span title=\"&lt;div&gt;{0}{1}&lt;/div&gt;\" class=\"glyphicon glyphicon-info-sign icon-help-options tooltip-system\">",
                            optPregunta.COMENTARIO_PREGUNTA,
                    (optPregunta.LINK_COMENTARIO != null ? string.Format(".&lt;a target='_blank' href='{0}' class='link-comentario' &gt;Ver enlace&lt;/a&gt;", optPregunta.LINK_COMENTARIO) : "")))
                        .Append("</span>");

                this._builder.Append("</div>");
            }
        }
    }
}