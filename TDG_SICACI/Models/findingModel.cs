using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using JertiFramework.Controls;
using System.Web.Mvc;
using JertiFramework.Database;

namespace TDG_SICACI.Models {
    public class Grid_FindingViewModel
    {
        [Display(Name = "Id")]
        [JFOcultarEtiqueta(true)]
        public int ID { get; set; }

        [Display(Name = "Finding/Hallazgo")]
        public string COMENTARIO { get; set; }

        [Display(Name = "Tipo", Prompt = "column-grid-value-center", ShortName = "column-grid-value-center")]
        public string TIPO_NOCONFORMIDAD { get; set; }

        [Display(Name = "Tipo de Acción", Prompt = "column-grid-value-center", ShortName = "column-grid-value-center" )]
        public string TIPO_CORRECION { get; set; } // Inmediata | Sostenible 

        [Display(Name = "Fecha Limite", Prompt = "column-grid-value-center", ShortName = "column-grid-value-center")]
        public string FECHA_LIMITE { get; set; }

        [Display(Name = "Estado", Prompt = "column-grid-value-center", ShortName = "column-grid-value-center")]
        public string ESTADO { get; set; } // Resuelto | Pendiente 

    }

    public class Agregar_FindingModel
    {
        [Required]
        [Display(Name = "Tipo de no conformidad")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.ComboBox)]
        public int tipoNoConformidad { get; set; }

        [Required]
        [JFMaxLenght(2000)]
        [Display(Name = "Comentario del finding")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.Multiline)]
        public string comentario { get; set; }

        //public string tipoRelacion { get; set; }

        [Required]
        [Display(Name = "Relacionado a")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.ComboBox)]
        //[JFTipoField(JFControlType.Text)]
        public int numeralRelacion { get; set; }

        [Required]
        [Display(Name = "Tipo de accion correctiva")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.RadioButton)]
        //[JFTipoField(JFControlType.Text)]
        public string tipoCorreccion { get; set; }

        [JFMaxLenght(2000)]
        [Display(Name = "Accion correctiva sugerida")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.Multiline)]
        public string accionCorrectivaSugerida { get; set; }

        [Display(Name = "Fecha limite sugerida")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.Fecha)]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public DateTime? fechaLimiteSugerida { get; set; }
    }

    public class Consultar_FindingModel
    {
        public int ID { get; set; }

        public string TIPO_NOCONFORMIDAD { get; set; }

        public string COMENTARIO { get; set; }

        public int NO_RELACION { get; set; }

        public string TIPO_CORRECION { get; set; }

        public string ACCION_CORRECTIVA_SUGERIDA { get; set; }

        public string FECHA_LIMITE { get; set; }

        public string USUARIO { get; set; }

        public string FECHA_CREACION { get; set; }

        public string ESTADO { get; set; }
    }

    public class Modificar_FindingModel
    {

        public int id { get; set; }

        [Required]
        [Display(Name = "Tipo de no conformidad")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.ComboBox)]
        public string tipoNoConformidad { get; set; }

        [Required]
        [Display(Name = "Comentario del finding")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        //[JFTipoField(JFControlType.Multiline)]
        [JFTipoField(JFControlType.Text)]
        public string comentario { get; set; }

        //public string tipoRelacion { get; set; }

        [Required]
        [Display(Name = "Relacionado a")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.ComboBox)]
        //[JFTipoField(JFControlType.Text)]
        public int numeralRelacion { get; set; }

        [Required]
        [Display(Name = "Tipo de accion correctiva")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.RadioButton)]
        //[JFTipoField(JFControlType.Text)]
        public string tipoCorreccion { get; set; }

        [Display(Name = "Accion correctiva sugerida")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        //[JFTipoField(JFControlType.Multiline)]
        [JFTipoField(JFControlType.Text)]
        public string accionCorrectivaSugerida { get; set; }

        [Display(Name = "Fecha limite sugerida")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        //[JFTipoField(JFControlType.Fecha)]
        [JFTipoField(JFControlType.Text)]
        public DateTime fechaLimiteSugerida { get; set; }
    }

}