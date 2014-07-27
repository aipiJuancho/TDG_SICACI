using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using JertiFramework.Controls;

namespace TDG_SICACI.Models
{
    public class preguntaAbiertaModel : preguntaModel
    {
        public int id { get; set; }

        [Required]
        [Display(Name= "Orden:")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.Text)]
        public int orden { get; set; }

        [Required]
        [Display(Name = "Pregunta:")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.Text)]
        public string texto { get; set; }

        [Required]
        [Display(Name = "Comentario:")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.Text)]
        public string comentario { get; set; }

        [Required]
        [Display(Name = "Categoria:")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 4)]
        [JFTipoField(JFControlType.ComboBox)]
        public string tipo  { get; set; }

        [Required]
        [Display(Name = "Adjuntar archivo:")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.Text)]
        public Boolean adjuntaArchivo { get; set; }

        [Required]
        [Display(Name = "Archivo requerido:")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 4)]
        [JFTipoField(JFControlType.ComboBox)]       
        public string tipoArchivo { get; set; }

        [Required]
        [Display(Name = "Relacionado a:")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 4)]
        [JFTipoField(JFControlType.ComboBox)]
        public string tipoRelacion { get; set; }
        
        //public int numeralRelacion { get; set; }
    }
}