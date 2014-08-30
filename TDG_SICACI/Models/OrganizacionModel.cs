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
    public class OrganizacionModel
    {
        //TODO: agregar atributos a las propiedades
        public string nombre {get; set;}
        public string logo {get; set;}
        public string eslogan {get; set;}
        public string alcance {get; set;}
        public string mision {get; set;}
        public string vision {get; set;}
        public Array valores {get; set;}
        public Array politicas {get; set;}
    }

    public class Consultar_OrganizacionModel
    {
        public string nombre { get; set; }
        public string logo { get; set; }
        public string eslogan { get; set; }
        public string alcance { get; set; }
        public string mision { get; set; }
        public string vision { get; set; }
        public Array valores { get; set; }
        public Array politicas { get; set; }
    }

    public class Modificar_organizacionModel 
    {
        [JFMaxLenght(100)]
        //[Remote("_validateUser", "Usuario")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [Display(Name = "Nombre", Prompt = "Nombre de la organización")]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [MinLength(4, ErrorMessageResourceName = "LongitudUser", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string nombre { get; set; }

        [Display(Name = "Logo", Prompt = "Solo archivos *.jpg .png")]
        [JFTipoField(JFControlType.File)]
        [JFFile(1, JFFileAttribute.JFFileExtension.PDF)]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        public HttpPostedFileBase logo { get; set; }


        [JFMaxLenght(200)]
        //[Remote("_validateUser", "Usuario")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [Display(Name = "Eslogan", Prompt = "Eslogan de la organización")]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [MinLength(4, ErrorMessageResourceName = "LongitudUser", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string eslogan { get; set; }

        [JFMaxLenght(500)]
        //[Remote("_validateUser", "Usuario")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [Display(Name = "Alcance", Prompt = "Alcance de la organización")]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [MinLength(4, ErrorMessageResourceName = "LongitudUser", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string alcance { get; set; }

        [JFMaxLenght(500)]
        //[Remote("_validateUser", "Usuario")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [Display(Name = "Misión", Prompt = "Misión de la organización")]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [MinLength(4, ErrorMessageResourceName = "LongitudUser", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string mision { get; set; }

        [JFMaxLenght(500)]
        //[Remote("_validateUser", "Usuario")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [Display(Name = "Visión", Prompt = "Visión de la organización")]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [MinLength(4, ErrorMessageResourceName = "LongitudUser", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string vision { get; set; }

        public Array valores { get; set; }
        
        public Array politicas { get; set; }
    }

}