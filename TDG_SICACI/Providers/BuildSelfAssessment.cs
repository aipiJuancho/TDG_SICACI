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
                            .Append(string.Format("<div class=\"panel-heading\" style=\"font-size: 18px\">{0}</div>", item.DESCRIPCION_JERARQUIA))
                            .Append("<div class=\"panel-body\">");
                        this.Construir(item.ID_JERARQUIA.Value);
                        this._builder.Append("</div>")
                            .Append("</div>");
                    }
                    else if(item.NIVEL.Equals(2)) {
                        if (item.ES_PREGUNTA.Equals("N"))   //Eso quiere decir que es un HEADER
                            this._builder.Append(string.Format("<h4>{0}</h4>", item.DESCRIPCION_JERARQUIA));
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
                            }
                        }

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
                        //Definimos el titulo de la pregunta
                        this._builder.Append("<div class=\"form-group\">")
                            .Append(string.Format("<label for=\"{0}\">{2} - {1}</label>", infoPregunta.ID_PREGUNTA,
                                infoPregunta.DESCRIPCION_JERARQUIA, infoPregunta.ARBOL));

                        this._builder.Append("<div class=\"input-group\">");
                        this._builder.Append(string.Format("<input type=\"text\" class=\"form-control\" id=\"{0}\">", 
                                infoPregunta.ID_PREGUNTA));

                        //Comprobamos si la pregunta tiene asociado un comentario de ayuda
                        if (!string.IsNullOrEmpty(infoPregunta.COMENTARIO_PREGUNTA))
                            this._builder.Append(string.Format("<span title=\"{0}\" class=\"input-group-addon glyphicon glyphicon-info-sign icon-help-options tooltip-system\" style=\"top: 0;display: table-cell;\"></span>", 
                                infoPregunta.COMENTARIO_PREGUNTA));

                        this._builder.Append("</div>");
                        break;
                    case "Opción múltiple":
                        //Definimos el titulo de la pregunta
                        this._builder.Append("<div class=\"form-group\">")
                            .Append("<div>")
                            .Append(string.Format("<label for=\"{0}\">{2} - {1}", infoPregunta.ID_PREGUNTA,
                                infoPregunta.DESCRIPCION_JERARQUIA, infoPregunta.ARBOL))
                            .Append("</label>");

                        //Comprobamos si la pregunta tiene asociado un comentario de ayuda
                        if (!string.IsNullOrEmpty(infoPregunta.COMENTARIO_PREGUNTA))
                            this._builder.Append(string.Format("<span title=\"{0}\" class=\"glyphicon glyphicon-info-sign icon-help-options tooltip-system\"></span>",
                                infoPregunta.COMENTARIO_PREGUNTA));

                        this._builder.Append("</div>");

                        
                        //Construimos las opciones multiples posibles
                        var _opts = this._infoPreguntas.Where(p => p.ID_PREGUNTA.Value.Equals(infoPregunta.ID_PREGUNTA) && p.CLASIFICACION.Equals("O")).ToArray();

                        foreach (SP_CONSTRUIR_SELF_MODEL optPregunta in _opts)
                        {
                            this._builder.Append("<div class=\"radio\">")
                                .Append("<label>")
                                .Append(string.Format("<input type=\"radio\" name=\"{0}\" value=\"{0}\">",
                                    optPregunta.ID_PREGUNTA))
                                .Append(optPregunta.DESCRIPCION_JERARQUIA)
                                .Append("</label>");

                            //Evaluamos si tiene COMENTARIO esta respuesta
                            if (!string.IsNullOrEmpty(optPregunta.COMENTARIO_PREGUNTA))
                                this._builder.Append(string.Format("<span title=\"{0}\" class=\"glyphicon glyphicon-info-sign icon-help-options tooltip-system\">",
                                        optPregunta.COMENTARIO_PREGUNTA))
                                    .Append("</span>");

                            this._builder.Append("</div>");
                        }
                        break;
                }
                
                this._builder.Append("</div>");

                /*STEP 3: Por ultimo, verificamos si tiene preguntas Adicionales de GIDEM*/
                if (this._infoPreguntas.Where(p => p.ID_JERARQUIA.Value.Equals(infoPregunta.ID_JERARQUIA.Value) && p.CLASIFICACION.Equals("N")).Count() > 0)
                {
                    this._builder.Append(string.Format("<div class=\"panel panel-info\" style=\"margin: 0 {0}px;\">", (infoPregunta.NIVEL.Value -1) * 25))
                        .Append("<div class=\"panel-heading\" style=\"padding-top: 4px; padding-bottom: 4px;\">Preguntas Adicionales - GIDEM</div>")
                        .Append("<div class=\"panel-body\">")
                        .Append("</div>")
                        .Append("</div>");

                }
                
            }
        }
    }
}