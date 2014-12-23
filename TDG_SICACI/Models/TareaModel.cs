using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using JertiFramework.Controls;
using System.Web.Mvc;
using JertiFramework.Database;

namespace TDG_SICACI.Models
{
   
    public class Grid_TareaViewModel
    {
        [JFOcultarEtiqueta(true)]
        public int ID_TAREA { get; set; }

        [JFOcultarEtiqueta(true)]
        public int ID_PROYECTO { get; set; }

        [Display(Name = "Orden")]
        public int ORDEN { get; set; }

        [Display(Name = "Titulo")]
        public string TITULO_TAREA { get; set; }

        [Display(Name = "Responsable de la Ejecución")]
        public string RESPONSABLE_EJECUCION { get; set; }

        [Display(Name = "Fecha prevista de Finalización", Prompt = "column-grid-value-center", ShortName = "column-grid-value-center")]
        public string FECHA_FINALIZACION { get; set; }

        [Display(Name = "Progreso (%)", Prompt = "column-grid-value-center", ShortName = "column-grid-value-center")]
        public string PROGRESO { get; set; }  

    }

    public class Agregar_TareaModel
    {

        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [Range(1, 30, ErrorMessageResourceName = "RangoVisual", ErrorMessageResourceType = typeof(ErrorMessages))]
        [Display(Name = "Orden de Representación")]
        [JFTipoField(JFControlType.Numeric)]
        [JFRejilla(Grid_Label_PC: 4, Grid_Field_PC: 8)]
        public int orden { get; set; }

        [JFMaxLenght(100)]
        [Display(Name = "Titulo", Prompt = "Digite el titulo con el que desea identificar esta tarea")]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [JFRejilla(Grid_Label_PC: 4, Grid_Field_PC: 8)]
        public string titulo { get; set; }

        [JFMaxLenght(1000)]
        [Display(Name = "Descripción", Prompt = "Digite una breve descripción de lo que se va a llevar a cabo en esta tarea")]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [JFRejilla(Grid_Label_PC: 4, Grid_Field_PC: 8)]
        [JFTipoField(JFControlType.Multiline)]
        public string descripcion { get; set; }

        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [Display(Name = "Responsable de la Ejecucion")]
        [JFRejilla(Grid_Label_PC: 4, Grid_Field_PC: 8)]
        [JFTipoField(JFControlType.ComboBox)]
        public string responableEjecucion { get; set; }

        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [JFMaxLenght(500)]
        [Display(Name = "Recursos Asignados")]
        [JFRejilla(Grid_Label_PC: 4, Grid_Field_PC: 8)]
        [JFTipoField(JFControlType.Multiline)]
        public string recursosAsignados { get; set; }

        [Display(Name = "Fecha Prevista de Finalización")]
        [JFRejilla(Grid_Label_PC: 4, Grid_Field_PC: 8)]
        [JFTipoField(JFControlType.Fecha)]
        public DateTime fechaFin { get; set; }

        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [Display(Name = "Progreso de la Tarea (%)")]
        [JFRejilla(Grid_Label_PC: 4, Grid_Field_PC: 8)]
        [JFTipoField(JFControlType.ComboBox)]
        public float progreso { get; set; }

        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [Display(Name = "Personal involucrado")]
        [JFRejilla(Grid_Label_PC: 4, Grid_Field_PC: 8)]
        [JFTipoField(JFControlType.Text)]
        public string personasInvolucradas { get; set; }
    }



    public class comentario
    {
        public string usuario { get; set; }
        public string texto { get; set; }
    }

    public class archivoAdjunto
    {
        public string nombre { get; set; }
        public string url { get; set; }
    }

    public class Modificar_TareaModel
    {
        [Required]
        [Display(Name = "Orden")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.Numeric)]
        public int orden { get; set; }

        [Required]
        [Display(Name = "Titulo de Tarea")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.Text)]
        public string titulo { get; set; }

        [Required]
        [JFMaxLenght(2000)]
        [Display(Name = "Descripcion")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.Multiline)]
        public string descripcion { get; set; }

        [Required]
        [Display(Name = "Responsable de Ejecucion")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.ComboBox)]
        public string responableEjecucion { get; set; }

        [Required]
        [JFMaxLenght(2000)]
        [Display(Name = "Recursos Asignados")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.Multiline)]
        public string recursosAsignados { get; set; }

        [Display(Name = "Fecha fin")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        //[JFTipoField(JFControlType.Fecha)]
        [JFTipoField(JFControlType.Text)]
        public DateTime fechaFin { get; set; }

        [Required]
        [Display(Name = "Progreso")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.ComboBox)]
        public int progreso { get; set; }

        [Required]
        [Display(Name = "Personas involucradas")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.Text)]
        public string personasInvolucradas { get; set; }

        //[Required]
        //[Display(Name = "Agregar Archivo")]
        //[JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        //[JFTipoField(JFControlType.File)]
        //public string agregarArchivo { get; set; }

        //[Required]
        //[JFMaxLenght(2000)]
        //[Display(Name = "Comentario")]
        //[JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        //[JFTipoField(JFControlType.Multiline)]
        //public string comentario { get; set; }


        public List<comentario> comentarios { get; set; }
        public List<archivoAdjunto> archivos { get; set; }

    }

}