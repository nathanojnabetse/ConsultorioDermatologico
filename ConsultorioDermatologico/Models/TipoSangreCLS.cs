using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultorioDermatologico.Models
{
    public class TipoSangreCLS
    {
        public int idTipoSangre { get; set; }
        [Display(Name = "Grupo sanguíneo")]
        public string sangre { get; set; }
    }
}