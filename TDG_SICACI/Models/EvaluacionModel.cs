using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using JertiFramework.Controls;

namespace TDG_SICACI.Models
{
    public class EvaluacionModel
    {
        // TODO: agregar atributo de las propiedades.
        public int revision {get; set;}
        public DateTime fechaCreacion {get; set;}
        public string comentario {get; set;}
        public Array respuestas {get; set;}
        public string idUsuario {get; set;}
    }
}