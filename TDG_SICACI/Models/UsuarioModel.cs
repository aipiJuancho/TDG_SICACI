using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using JertiFramework.Controls;

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

    public class UsuarioModel 
    {
        //TODO: agregar atributos a las propiedades
        public string id { get; set; }

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
        public int rol { get; set; }
    }

    public class RolModel 
    {
        //TODO: agregar atributos a las propiedades
        public int id {get; set;}
        public string tipo {get; set;}
    }
}