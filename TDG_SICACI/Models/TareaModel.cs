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
        [Display(Name = "Orden")]
        public int orden { get; set; }

        [Display(Name = "Titulo")]
        public string titulo { get; set; }

        [Display(Name = "Responsable")]
        public string responable { get; set; }

        [Display(Name = "Fecha Finalizacion")]
        public DateTime fechaFinalizacion { get; set; }

        [Display(Name = "Progreso %")]
        public float progreso { get; set; }  

    }

    public class Agregar_TareaModel
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

        [Required]
        [Display(Name = "Agregar Archivo")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.File)]
        public string agregarArchivo  { get; set; }

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