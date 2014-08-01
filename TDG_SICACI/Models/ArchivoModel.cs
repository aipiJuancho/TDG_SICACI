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
    public class ArchivoModel
    {
        public int      id          { get; set; }
        
        [Required]
        [Display(Name = "Nombre",Prompt ="Nombre del documento")]
        //[JFMaxLenght(99)]
        [JFRejilla(Grid_Label_PC:3,Grid_Field_PC:9)]
        public string   nombre        { get; set; }

        [Required]
        [Display(Name = "Etiqueta", Prompt = "Etiqueta del documento")]
        //[JFMaxLenght(99)]
        [JFRejilla(Grid_Label_PC:3, Grid_Field_PC:9)]
        public string   etiqueta    { get; set; }

        [Display(Name = "Archivo", Prompt = "Solo archivos *.pdf")]
        [JFTipoField(JFControlType.File)]
        [JFFile(1, JFFileAttribute.JFFileExtension.PDF)]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        public HttpPostedFileBase documento { get; set; } 

        public string   url         { get; set; }
    }

    public class Grid_ArchivoViewModel
    {
        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Display(Name = "Etiqueta")]
        public string etiqueta { get; set; }
    }

    public class Agregar_ArchivoModel
    {
        [JFMaxLenght(16)]
        //[Remote("_validateUser", "Usuario")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [Display(Name = "Nombre", Prompt = "Nombre del Archivo")]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [MinLength(4, ErrorMessageResourceName = "LongitudUser", ErrorMessageResourceType = typeof(ErrorMessages))]
        //[RegularExpression(JFRegExpValidators.UserSystem, ErrorMessageResourceName = "CaracteresUser", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string nombre { get; set; }

        [JFMaxLenght(50)]
        //[Remote("_validateUser", "Usuario")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [Display(Name = "Etiqueta", Prompt = "Etiqueta del Archivo")]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [MinLength(4, ErrorMessageResourceName = "LongitudUser", ErrorMessageResourceType = typeof(ErrorMessages))]
        //[RegularExpression(JFRegExpValidators.UserSystem, ErrorMessageResourceName = "CaracteresUser", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string etiqueta { get; set; }

        [Display(Name = "Archivo", Prompt = "Solo archivos *.pdf")]
        [JFTipoField(JFControlType.File)]
        [JFFile(1, JFFileAttribute.JFFileExtension.PDF)]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        public HttpPostedFileBase documento { get; set; }

    }
}