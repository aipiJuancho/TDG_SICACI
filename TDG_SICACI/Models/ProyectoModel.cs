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
        [JFOcultarEtiqueta(true)]
        public int ID { get; set; }

        [Display(Name = "Nombre del Proyecto")]
        public string NOMBRE_PROYECTO { get; set; }

        [Display(Name = "Resp. de Ejecución")]
        public string RESP_EJECUCION { get; set; }

        [Display(Name = "Fecha de Inicio", Prompt = "column-grid-value-center", ShortName = "column-grid-value-center")]
        public string FECHA_INICIO { get; set; }

        [Display(Name = "Fecha de Finalización", Prompt = "column-grid-value-center", ShortName = "column-grid-value-center")]
        public string FECHA_FINALIZACION { get; set; }

        [Display(Name = "Estado del Proyecto", Prompt = "column-grid-value-center", ShortName = "column-grid-value-center")]
        public string ESTADO_PROYECTO { get; set; }  

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

        [Required]
        [Display(Name = "Findings asociados")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.Text)]  //TODO: cambiar el tipo de control al otro volado 
        public string findingsAsociados { get; set; }//TODO: no editable


        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [Display(Name = "Fecha de inicio")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.Fecha)]
        public DateTime fechaInicio { get; set; }

        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [Display(Name = "Estado de Aprobación")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.ComboBox)]
        public string aprobacion { get; set; }
    }

}