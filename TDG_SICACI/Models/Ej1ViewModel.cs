using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TDG_SICACI.Models
{
    public class Ej1ViewModel
    {
        [Required]
        public string Pregunta { get; set; }

        [Required]
        public string Respuesta { get; set; }
    }
}