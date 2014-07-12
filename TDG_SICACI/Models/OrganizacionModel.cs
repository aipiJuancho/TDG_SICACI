using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
}