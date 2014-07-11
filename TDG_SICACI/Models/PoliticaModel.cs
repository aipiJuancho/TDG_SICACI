using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using JertiFramework.Controls;

namespace TDG_SICACI.Models
{
    public class PoliticaModel
    {
        // TODO: agregar atributo de las propiedades.
        public string nombre { get; set; }
        public string descripcion {get; set;}
        public Array objetivosAsociados{get; set;}
    }
}