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
    public class findingModel
    {
        //[Required]
        //[Display(Name = "Usuario", Prompt = "Username o Nickname")]
        //[JFMaxLenght(16)]
        //[JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        //public string UserName { get; set; }

        //[Required]
        //[Display(Name = "Contraseña", Prompt="Contraseña")]
        //[JFMaxLenght(16)]
        //[JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        //[JFTipoField(JFControlType.Password)]
        //public string Password { get; set; }

        public int id { get; set; }

        [Required]
        [Display(Name = "Tipo de no conformidad")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 4)]
        [JFTipoField(JFControlType.ComboBox)]//TODO: PREGUNTARLE A JUANCHO...
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
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 4)]
        [JFTipoField(JFControlType.ComboBox)]
        //[JFTipoField(JFControlType.Text)]
        public int numeralRelacion { get; set; }

        [Required]
        [Display(Name = "Tipo de accion correctiva")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.RadioButton)]
        //[JFTipoField(JFControlType.Text)]
        public Boolean tipoCorreccion { get; set; }

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





    public class Grid_FindingViewModel
    {
        [Display(Name = "Id")]
        public int id { get; set; }

        [Display(Name = "Comentario")]
        public string comentario { get; set; }

        [Display(Name = "Tipo de no conformidad")]
        public string tipoNoConformidad { get; set; }

        [Display(Name = "Numeral de la norma relacionado")]
        public int numeralRelacionado { get; set; } 

        [Display(Name = "Tipo de accion correctiva")]
        public string tipoCorreccion { get; set; } // Inmediata | Sostenible 

        [Display(Name = "Fecha limite sugerida")]
        public DateTime fechaLimiteSugerida { get; set; }

        [Display(Name = "Estado")]
        public String estado { get; set; } // Resuelto | Pendiente 

    }



}