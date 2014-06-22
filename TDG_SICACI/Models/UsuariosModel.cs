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
}