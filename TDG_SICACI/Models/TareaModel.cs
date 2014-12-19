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

    public class Modificar_TareaModel
    {

        [Required]
        [Display(Name = "Orden")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.Numeric)]
        public int orden { get; set; }

        [Required]
        [Display(Name = "Nombre de Tarea")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.Text)]
        public string nombre { get; set; }

        [Required]
        [Display(Name = "Responsable de Ejecucion")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.ComboBox)]
        public string responableEjecucion { get; set; }

        [Required]
        [Display(Name = "Responsable de Aprobacion")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.ComboBox)]
        public string responableAprobacion { get; set; }

        [Required]
        [Display(Name = "Objetivos asociados")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.Text)]  
        public string objetivosAsociados { get; set; }//TODO: no editable

        [Required]
        [Display(Name = "Findings asociados")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.Text)]  //TODO: cambiar el tipo de control al otro volado 
        public string findingsAsociados { get; set; }//TODO: no editable


        [Display(Name = "Fecha de inicio")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        //[JFTipoField(JFControlType.Fecha)]
        [JFTipoField(JFControlType.Text)]
        public DateTime fechaInicio { get; set; }

        [Required]
        [Display(Name = "Estado de Aprobacion")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.ComboBox)]
        public string aprobacion { get; set; }
    }

}