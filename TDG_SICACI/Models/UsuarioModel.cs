using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using JertiFramework.Controls;
using System.Web.Mvc;

namespace TDG_SICACI.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Usuario", Prompt = "Username o Nickname")]
        [JFMaxLenght(16)]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Contraseña", Prompt="Contraseña")]
        [JFMaxLenght(16)]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.Password)]
        public string Password { get; set; }
    }

    public class Grid_UserViewModel
    {
        [Display(Name = "Usuario")]
        public string usuario { get; set; }

        [Display(Name = "Nombres")]
        public string nombres { get; set; }

        [Display(Name = "Apellidos")]
        public string apellidos { get; set; }

        [Display(Name = "Correo Electronico")]
        public string email { get; set; }

        [Display(Name = "Tipo de Rol")]
        public string rol { get; set; }

        [Display(Name = "Estado")]
        public string estado { get; set; }
    }

    public class UsuarioModel 
    {
        //TODO: agregar atributos a las propiedades
//        public string id { get; set; }

        [Required]
        [Display(Name = "Usuario", Prompt = "Username o Nickname")]
        [JFMaxLenght(16)]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        public string usuario { get; set; }

        [Required]
        [Display(Name = "Nombre", Prompt = "")]
        [JFMaxLenght(16)]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        public string nombre { get; set; }

        [Required]
        [Display(Name = "Apellido", Prompt = "")]
        [JFMaxLenght(16)]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        public string apellido { get; set; }

        [Required]
        [Display(Name = "Email", Prompt = "")]
        [RegularExpression("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$", ErrorMessage = "El correo no es valido")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        public string email { get; set; }

        [Required]
        [Display(Name = "Contraseña", Prompt = "Contraseña")]
        [JFMaxLenght(16)]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.Password)]
        public string password { get; set; }

        [Required]
        [Display(Name = "Rol", Prompt = "")]
        [JFMaxLenght(16)]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        public string rol { get; set; }

        [Required]
        [Display(Name = "Activo", Prompt = "")]
        public string estado { get; set; }
    }

    public class UsuarioModifiyModel
    {
        [Required]
        [Display(Name = "Nombre", Prompt = "")]
        [JFMaxLenght(16)]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        public string nombre { get; set; }

        [Required]
        [Display(Name = "Apellido", Prompt = "")]
        [JFMaxLenght(16)]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        public string apellido { get; set; }

        [Required]
        [Display(Name = "Email", Prompt = "")]
        [RegularExpression("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$", ErrorMessage = "El correo no es valido")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        public string email { get; set; }

        [Required]
        [Display(Name = "Rol", Prompt = "")]
        [JFMaxLenght(16)]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.ComboBox)]
        public int rol { get; set; }

        [Required]
        [Display(Name = "Estado", Prompt = "")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.ComboBox)]
        public string estado { get; set; }
    }

    public class RolModel 
    {
        //TODO: agregar atributos a las propiedades
        public int id {get; set;}
        public string tipo {get; set;}
    }

    public class jfBSGrid_User_ViewModel
    {
        [Display(Name= "Usuario")]
        [JFOcultarEtiqueta(false)]
        public string usuario { get; set; }

        [Display(Name = "Nombres de Usuario")]
        public string nombres { get; set; }

        [Display(Name = "Apellidos de Usuario")]
        public string apellidos { get; set; }
    }
}