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

    public class NewUserViewModel {

        [JFMaxLenght(16)]
        [Remote("_validateUser", "Usuario")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [Display(Name = "Usuario", Prompt = "Usuario o Nickname")]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [MinLength(4, ErrorMessageResourceName = "LongitudUser", ErrorMessageResourceType = typeof(ErrorMessages))]
        [RegularExpression(JFRegExpValidators.UserSystem, ErrorMessageResourceName = "CaracteresUser", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string Usuario { get; set; }

        [JFMaxLenght(16)]
        [JFTipoField(JFControlType.Password)]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [Display(Name = "Contraseña", Prompt = "Digite su contraseña con un minimo de 8 caracteres")]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [MinLength(8, ErrorMessageResourceName = "LongitudPWD", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string Password { get; set; }

        [JFMaxLenght(16)]
        [JFTipoField(JFControlType.Password)]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [Display(Name = "Repetir Contrasña", Prompt = "Repita la contraseña")]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [MinLength(8, ErrorMessageResourceName = "LongitudPWD", ErrorMessageResourceType = typeof(ErrorMessages))]
        [System.Web.Mvc.Compare("Password", ErrorMessageResourceName = "ComparePWD", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string ConfirmPassword { get; set; }

        [JFMaxLenght(50)]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [Display(Name = "Nombres", Prompt = "Digite su primer y segundo nombre")]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [RegularExpression(JFRegExpValidators.OnlyLetrasWithSpace, ErrorMessageResourceName = "OnlyLetras_WSpace", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string Nombres { get; set; }

        [JFMaxLenght(50)]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [Display(Name = "Apellidos", Prompt = "Digite su primer y segundo apellido")]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [RegularExpression(JFRegExpValidators.OnlyLetrasWithSpace, ErrorMessageResourceName = "OnlyLetras_WSpace", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string Apellidos { get; set; }

        [JFMaxLenght(100)]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [Display(Name = "E-Mail", Prompt = "Digite su correo electronico")]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [RegularExpression(JFRegExpValidators.Email, ErrorMessageResourceName = "Email", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string CorreoE { get; set; }

        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.ComboBox)]
        [Display(Name = "Tipo de Usuario")]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        public int Rol { get; set; }
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
}