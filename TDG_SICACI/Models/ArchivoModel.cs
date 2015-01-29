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
        [JFFile(3, JFFileAttribute.JFFileExtension.ImagenYPDF)]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        public HttpPostedFileBase documento { get; set; } 

        public string   url         { get; set; }
    }

    public class Grid_ArchivoViewModel
    {
        [JFOcultarEtiqueta(true)]
        public int ID_FILEGROUP { get; set; }

        [Display(Name = "Nombre del Archivo")]
        public string FILEGROUP_NAME { get; set; }

        [Display(Name = "Etiqueta")]
        public string ETIQUETA { get; set; }

        [Display(Name = "Última versión")]
        public string FECHA_ULTIMA_VERSION { get; set; }

        [Display(Name = "No. Versiones")]
        public int VERSIONES { get; set; }
    }

    public class Grid_VersionFilegroup
    {
        [Display(Name = "# Versión")]
        public int NO_VERSION { get; set; }

        [Display(Name = "Fecha de Creación")]
        public string FECHA_CREACION { get; set; }

        [Display(Name = "Usuario")]
        public string USUARIO { get; set; }

        [Display(Name = "Etiqueta")]
        public string ETIQUETA { get; set; }

    }

    public class Agregar_ArchivoModel
    {
        [JFTipoField(JFControlType.ComboBox)]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [Display(Name = "Nombre", Prompt = "Nombre del Archivo")]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string nombre { get; set; }

        [JFMaxLenght(50)]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [Display(Name = "Etiqueta", Prompt = "Etiqueta del Archivo")]
        [MinLength(4, ErrorMessageResourceName = "LongitudUser", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string etiqueta { get; set; }

        [Display(Name = "Archivo")]
        [JFTipoField(JFControlType.File)]
        [JFFile(4, JFFileAttribute.JFFileExtension.ImagenYPDF)]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        public HttpPostedFileBase documento { get; set; }
    }

    public class New_FileGroupName
    {
        [JFMaxLenght(100)]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [Display(Name = "Nombre", Prompt = "Digite el nombre del archivo que desea crear")]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string nombre { get; set; }
    }

    public class Consultar_ArchivoModel
    {   
        public string nombre { get; set; }
        public string etiqueta { get; set; }
        public List<Archivo_Versionado> archivosVersionados { get; set; }
    }

    public class Archivo_Versionado
    {
        public DateTime fecha { get; set; }
        public string url { get; set; }
    }

    public class Modificar_ArchivoModel 
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

        [Display(Name = "Nueva Versión")]
        [JFTipoField(JFControlType.File)]
        [JFFile(1, JFFileAttribute.JFFileExtension.ImagenYPDF)]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        public HttpPostedFileBase documento { get; set; }

        public List<Archivo_Versionado> archivosVersionados { get; set; }

    }

}