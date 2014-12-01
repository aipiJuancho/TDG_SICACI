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
   
    public class Grid_ProyectoViewModel
    {
        [Display(Name = "Id")]
        public int id { get; set; }

        [Display(Name = "Nombre de Proyecto")]
        public string nombre { get; set; }

        [Display(Name = "Responsable de Ejecucion")]
        public string responableEjecucion { get; set; }

      //[Display(Name = "Objetivos Asociados")]
      //public string objetivosAsociados { get; set; } 

        [Display(Name = "Fecha de Inicio")]
        public DateTime fechaInicio { get; set; } 

        [Display(Name = "Fecha Finalizacion")]
        public DateTime fechaFinalizacion { get; set; }

        [Display(Name = "Progreso %")]
        public float progreso { get; set; }  

    }

    public class Agregar_ProyectoModel
    {
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [Display(Name = "Nombre de Proyecto")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.Text)]
        [JFMaxLenght(500)]
        public string nombre { get; set; }

        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [Display(Name = "Responsable de Ejecucion")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.ComboBox)]
        public string responableEjecucion { get; set; }

        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [Display(Name = "Responsable de Aprobacion")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.ComboBox)]
        public string responableAprobacion { get; set; }

        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [Display(Name = "Objetivos asociados")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.MultipleSelect)] 
        public string objetivosAsociados { get; set; }

        [Display(Name = "Findings asociados")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.Text)]  //TODO: cambiar el tipo de control al otro volado 
        public string findingsAsociados { get; set; }

        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [Display(Name = "Fecha de inicio")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.Fecha)]
        public DateTime fechaInicio { get; set; }

        //[Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [Display(Name = "Estado de Aprobacion")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.ComboBox)]
        public string aprobacion { get; set; }
    }



    public class Consultar_ProyectoModel
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string responableEjecucion { get; set; }
        public string responableAprobacion { get; set; }
        public string objetivosAsociados { get; set; }
        public string findingsAsociados { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFinalizacion { get; set; }
        public float progreso { get; set; }  
        public string aprobacion { get; set; }
    }

    public class Modificar_ProyectoModel
    {

        public int id { get; set; } //TODO: no editable

        [Required]
        [Display(Name = "Nombre de Proyecto")]
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