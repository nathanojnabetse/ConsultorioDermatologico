using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultorioDermatologico.Models
{
    public class Cie10CLS
    {
        public int idCie { get; set; }
        [Display(Name = "Código CIE")]
        public string codigo { get; set; }
        [Display(Name = "enfermedad")]
        public string enfermedad { get; set; }
    }
}