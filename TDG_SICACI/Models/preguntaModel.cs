using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using JertiFramework.Controls;
using JertiFramework.Database;

namespace TDG_SICACI.Models
{
    public class PreguntaViewModel
    {
        [JFMaxLenght(200)]
        [JFRejilla(Grid_Label_PC: 5, Grid_Field_PC: 7)]
        [Display(Name = "Pregunta", Prompt = "Digite la pregunta que desea mostrar al usuario")]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string TextoPregunta { get; set; }

        [JFMaxLenght(100)]
        [JFRejilla(Grid_Label_PC: 5, Grid_Field_PC: 7)]
        [Display(Name = "Comentario", Prompt = "(Opcional) Digite algun comentario de ayuda para el usuario")]
        public string ComentarioPregunta { get; set; }

        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string TipoPregunta { get; set; }

        [JFRejilla(Grid_Label_PC: 5, Grid_Field_PC: 7)]
        [Display(Name = "¿Se adjuntará documento?")]
        public string TipoDocumento { get; set; }

        [JFRejilla(Grid_Label_PC: 5, Grid_Field_PC: 7)]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        public int AsociadaA { get; set; }
    }

    public class preguntaModel
    {
        //public int id { get; set; }
        [Required]
        [Display(Name = "Orden de pregunta:")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.Multiline)]
        public string orden { get; set; }

        [Required]
        [Display(Name = "Agregue el texto de la pregunta:")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.Multiline)]
        public string texto { get; set; }

        [Required]
        [Display(Name = "Comentario de la pregunta:")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.Multiline)]
        public string comentario { get; set; }

        [Required]
        [Display(Name = "seleccione tipo de pregunta:")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.ComboBox)]
        public string tipo { get; set; }

        [Display(Name = "Seleccione un archivo a subir:")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.File)]
        public Boolean adjuntaArchivo { get; set; }

        [Display(Name = "que tipo de archivo es:??????")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.ComboBox)]
        public string tipoArchivo { get; set; }

        //public string tipoRelacion { get; set; }
        [Display(Name = "A que numeral se relaciona:")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.Text)]
        public int numeralRelacion { get; set; }
    }

    public class Grid_PreguntasViewModel
    {
        [Display(Name = "ID")]
        [JFOcultarEtiqueta(true)]
        public int ID_Jerarquia { get; set; }

        [Display(Name = "Orden")]
        public string Arbol { get; set; }

        [Display(Name = "Pregunta")]
        public string Descripcion_Jerarquia { get; set; }

        [Display(Name = "Tipo Pregunta")]
        public string Tipo_Pregunta { get; set; }

        [Display(Name = "Asociado a")]
        public string Asociado_A { get; set; }
    }
}