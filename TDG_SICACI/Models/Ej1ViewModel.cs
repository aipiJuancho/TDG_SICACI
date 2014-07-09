using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using JertiFramework.Controls;

namespace TDG_SICACI.Models
{
    //http://www.asp.net/mvc/tutorials/mvc-music-store/mvc-music-store-part-6
    public class Ej1ViewModel
    {
        [Required]
        [JFRejilla(Grid_Label_PC: 6, Grid_Field_PC: 6)]
        [JFMaxLenght(20)]
        [RegularExpression("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$", ErrorMessage= "El correo no es valido")]
        [Display(Name="Digite la pregunta", Prompt="Por favor, solo correos validos")]
        [JFOcultarEtiqueta(true)]
        public string Pregunta { get; set; }

        [Required]
        [JFRejilla(Grid_Label_PC: 6, Grid_Field_PC: 6)]
        [Display(Name = "Digite la respuesta", Prompt="Solo caracteres alfanumericos")]
        public string Respuesta { get; set; }

        [Display(Name = "Archivo", Prompt = "Solo archivos *.jpg, *.png")]
        [JFTipoField(JFControlType.File)]
        public HttpPostedFileBase Archivo { get; set; }
        
        [Display(Name = "Archivo", Prompt = "Solo archivos *.jpg, *.png")]
        [JFTipoField(JFControlType.File)]
        [JFFile(1, JFFileAttribute.JFFileExtension.PDF)]
        public HttpPostedFileBase ArchivoDemo { get; set; } 
    }
}