using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultorioDermatologico.Models
{
    public class ReligionCLS
    {
        public int idReligion { get; set; }
        [Display(Name = "Religion")]
        public string nombreReligion { get; set; }
    }
}