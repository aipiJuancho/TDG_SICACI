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
        public List<Consultar_Valor> valores { get; set; }
        public List<Consultar_Politica> politicas { get; set; }
        public List<Consultar_Versiones> versiones { get; set; }
        public int idVersionSeleccionada { get; set; }


    }
    public class Consultar_Valor
    {
        public string valor { get; set; }
        public string descripcion { get; set; }
    }

    public class Consultar_Politica 
    {
        public string politica { get; set; }
        public string descripcion { get; set; }
        public List<string> Objetivos { get; set; }
    }

    public class Consultar_Versiones
    {
        public int id_Version { get; set; }
        public string usuario { get; set; }
        public string fecha_Version { get; set; }
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
        [JFFile(1, JFFileAttribute.JFFileExtension.Imagen)]
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


        public List<Consultar_Valor> valores { get; set; }
        public List<Consultar_Politica> politicas { get; set; }
        
    }

    public class ModificarOrganizacionModel {
        public string nombre {get; set;}
        public HttpPostedFileBase logo {get; set;}
        public string eslogan {get; set;}
        public string alcance {get; set;}
        public string mision {get; set;}
        public string vision {get; set;}
        public IEnumerable<string> Valor_Texto { get; set; }
        public IEnumerable<string> Valor_Descripcion { get; set; }
        public IEnumerable<string> Politica_Texto { get; set; }
        public IEnumerable<string> Politica_Descripcion { get; set; }
        public IEnumerable<string> Politica_Objetivos { get; set; }
        public IEnumerable<string> Politica_Objetivos_TextPolitica { get; set; }
    }

}