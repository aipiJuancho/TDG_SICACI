using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using JertiFramework.Controls;

namespace TDG_SICACI.Models
{
    public class EvaluacionModel
    {
        // TODO: agregar atributo de las propiedades.
        public int revision {get; set;}
        public DateTime fechaCreacion {get; set;}
        public string comentario {get; set;}
        public Array respuestas {get; set;}
        public string idUsuario {get; set;}
    }

    public class Grid_EvaluacionViewModel
    {
        [Display(Name = "Revisión", Prompt = "column-grid-value-center column-grid-value-middle", ShortName = "column-grid-value-center")]
        public int revision { get; set; }

        [Display(Name = "Fecha de creación", Prompt = "column-grid-value-center", ShortName = "column-grid-value-center column-grid-value-last-revision")]
        public string fechaCreacion { get; set; }

        [Display(Name = "Comentario")]
        public string comentario { get; set; }

        [Display(Name = "Usuario", ShortName= "column-grid-value-name")]
        public string idUsuario { get; set; }

        [Display(Name = "Estado", Prompt = "column-grid-value-center", ShortName = "column-grid-value-center")]
        public string estado { get; set; }

        [Display(Name = "Fecha de Revisión", Prompt = "column-grid-value-center", ShortName = "column-grid-value-last-revision column-grid-value-center")]
        public string fechaRevision { get; set; }
    }

    public class Agregar_EvaluacionModel
    {
        [JFMaxLenght(500)]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [Display(Name = "Comentario", Prompt = "Comentario General de la Evaluación")]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [MinLength(4, ErrorMessageResourceName = "LongitudUser", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string comentario { get; set; }
    }

    public class Consultar_EvaluacionModel
    {
        public int revision { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string comentario { get; set; }
        public string idUsuario { get; set; }
    }

    public class Modificar_EvaluacionModel
    {
        [JFMaxLenght(500)]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [Display(Name = "Comentario", Prompt = "Comentario General de la Evaluación")]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [MinLength(4, ErrorMessageResourceName = "LongitudUser", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string comentario { get; set; }
    }

    public class Responses_SelfAssessment
    {
        public IEnumerable<HttpPostedFileBase> Archivo { get; set; }
        public IEnumerable<string> InfoArchivo { get; set; }
        public string tipo { get; set; }
        public IEnumerable<int> ID_Pregunta { get; set; }
        public IEnumerable<int> ID_Respuesta { get; set; }
        public IEnumerable<string> Respuesta { get; set; }
        public IEnumerable<string> TipoPregunta { get; set; }
    }

}