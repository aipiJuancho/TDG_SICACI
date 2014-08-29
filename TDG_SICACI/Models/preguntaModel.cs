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

        [JFRejilla(Grid_Label_PC: 5, Grid_Field_PC: 7)]
        [Display(Name = "¿Se adjuntará documento?")]
        [JFTipoField(JFControlType.ComboBox)]
        public string TipoDocumento { get; set; }
    }

    public class PreguntaGidemViewModel : PreguntaViewModel
    {
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [Range(1, 9, ErrorMessageResourceName = "RangoVisual", ErrorMessageResourceType = typeof(ErrorMessages))]
        [Display(Name = "Orden visual")]
        [JFTipoField(JFControlType.Numeric)]
        [JFRejilla(Grid_Label_PC: 5, Grid_Field_PC: 7)]
        public int OrdenVisual { get; set; }
    }

    public class newPreguntaModel
    {
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string TipoPregunta { get; set; }

        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string PreguntaGIDEM { get; set; }

        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        public int ReferenciaA { get; set; }

        public PreguntaViewModel FormPregunta { get; set; }

        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [Range(0, 9, ErrorMessageResourceName = "RangoVisual", ErrorMessageResourceType = typeof(ErrorMessages))]
        public int OrdenVisual { get; set; }
    }

    public class RespuestasViewModel
    {
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string Descripcion { get; set; }

        public string Comentario { get; set; }

        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string EsCorrecta { get; set; }

        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        public int Orden { get; set; }
    }

    public class newPreguntaMultipleModel: newPreguntaModel
    {
        public IEnumerable<RespuestasViewModel> Respuestas { get; set;}
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

        [Display(Name = " ", Prompt = "iso-gidem")]
        public string GIDEM { get; set; }
    }

    public class PreguntaModifiyModel
    {
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [Range(1, 9, ErrorMessageResourceName = "RangoVisual", ErrorMessageResourceType = typeof(ErrorMessages))]
        [Display(Name = "Orden visual")]
        [JFTipoField(JFControlType.Numeric)]
        [JFRejilla(Grid_Label_PC: 5, Grid_Field_PC: 7)]
        public int OrdenVisual { get; set; }

        [JFMaxLenght(200)]
        [JFRejilla(Grid_Label_PC: 5, Grid_Field_PC: 7)]
        [Display(Name = "Pregunta")]
        //[Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string TextoPregunta { get; set; }

        //[JFMaxLenght(100)]
        //[JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        //[Display(Name = "E-Mail", Prompt = "Digite su correo electronico")]
        //[Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        //[RegularExpression(JFRegExpValidators.Email, ErrorMessageResourceName = "Email", ErrorMessageResourceType = typeof(ErrorMessages))]
        //public string email { get; set; }

    }
}