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

    public class UsuarioModifiyModel
    {
        [JFMaxLenght(50)]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [Display(Name = "Nombres", Prompt = "Digite su primer y segundo nombre")]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [RegularExpression(JFRegExpValidators.OnlyLetrasWithSpace, ErrorMessageResourceName = "OnlyLetras_WSpace", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string nombre { get; set; }

        [JFMaxLenght(50)]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [Display(Name = "Apellidos", Prompt = "Digite su primer y segundo apellido")]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [RegularExpression(JFRegExpValidators.OnlyLetrasWithSpace, ErrorMessageResourceName = "OnlyLetras_WSpace", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string apellido { get; set; }

        [JFMaxLenght(100)]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [Display(Name = "E-Mail", Prompt = "Digite su correo electronico")]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [RegularExpression(JFRegExpValidators.Email, ErrorMessageResourceName = "Email", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string email { get; set; }

        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.ComboBox)]
        [Display(Name = "Tipo de Usuario")]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        public int rol { get; set; }

        [Display(Name = "Estado", Prompt = "")]
        [JFRejilla(Grid_Label_PC: 3, Grid_Field_PC: 9)]
        [JFTipoField(JFControlType.ComboBox)]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string estado { get; set; }
    }

    public class RolModel 
    {
        //TODO: agregar atributos a las propiedades
        public int id {get; set;}
        public string tipo {get; set;}
    }

    public class ChangePasswordViewModel
    {
        [JFMaxLenght(16)]
        [JFTipoField(JFControlType.Password)]
        [JFRejilla(Grid_Label_PC: 4, Grid_Field_PC: 8)]
        [Display(Name = "Contraseña actual", Prompt = "Digite su contraseña actual")]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [MinLength(8, ErrorMessageResourceName = "LongitudPWD", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string oldPassword { get; set; }

        [JFMaxLenght(16)]
        [JFTipoField(JFControlType.Password)]
        [JFRejilla(Grid_Label_PC: 4, Grid_Field_PC: 8)]
        [Display(Name = "Nueva contraseña", Prompt = "Digite su nueva contraseña")]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [MinLength(8, ErrorMessageResourceName = "LongitudPWD", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string newPassword { get; set; }

        [JFMaxLenght(16)]
        [JFTipoField(JFControlType.Password)]
        [JFRejilla(Grid_Label_PC: 4, Grid_Field_PC: 8)]
        [Display(Name = "Confirmar nueva contraseña", Prompt = "Digite nuevamente su contraseña")]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [MinLength(8, ErrorMessageResourceName = "LongitudPWD", ErrorMessageResourceType = typeof(ErrorMessages))]
        [System.Web.Mvc.Compare("newPassword", ErrorMessageResourceName = "ComparePWD", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string confirmNewPassword { get; set; }
    }

    public class ChangePasswordUserViewModel
    {

        [JFMaxLenght(16)]
        [JFTipoField(JFControlType.Password)]
        [JFRejilla(Grid_Label_PC: 4, Grid_Field_PC: 8)]
        [Display(Name = "Nueva contraseña", Prompt = "Digite su nueva contraseña")]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [MinLength(8, ErrorMessageResourceName = "LongitudPWD", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string newPassword { get; set; }

        [JFMaxLenght(16)]
        [JFTipoField(JFControlType.Password)]
        [JFRejilla(Grid_Label_PC: 4, Grid_Field_PC: 8)]
        [Display(Name = "Confirmar nueva contraseña", Prompt = "Digite nuevamente su contraseña")]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
        [MinLength(8, ErrorMessageResourceName = "LongitudPWD", ErrorMessageResourceType = typeof(ErrorMessages))]
        [System.Web.Mvc.Compare("newPassword", ErrorMessageResourceName = "ComparePWD", ErrorMessageResourceType = typeof(ErrorMessages))]
        public string confirmNewPassword { get; set; }
    }

    public class Grid_LogViewModel
    {
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Tipo")]
        public string TIPO { get; set; }

        [Display(Name = "Fecha Evento")]
        public string FECHA { get; set; }

        [Display(Name = "Usuario")]
        public string USUARIO { get; set; }

        [Display(Name = "Descripción")]
        public string DESCRIPCION { get; set; }

        [Display(Name = "Antes")]
        public string VALOR_ANTERIOR { get; set; }

        [Display(Name = "Despues")]
        public string VALOR_POSTERIOR { get; set; }

    }
}