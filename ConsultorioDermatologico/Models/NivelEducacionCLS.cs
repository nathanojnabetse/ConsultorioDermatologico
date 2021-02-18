using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultorioDermatologico.Models
{
    public class NivelEducacionCLS
    {
        public int idNivelEducacion { get; set; }
        [Display(Name = "Nivel de educación ")]
        public string nombreNivelEducacion { get; set; }
    }
}
