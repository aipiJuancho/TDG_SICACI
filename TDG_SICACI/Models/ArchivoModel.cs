using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using JertiFramework.Controls;

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
       
        public string   url         { get; set; }
    }
}