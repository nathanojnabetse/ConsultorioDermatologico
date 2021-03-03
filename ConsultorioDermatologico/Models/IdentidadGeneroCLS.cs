using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultorioDermatologico.Models
{
    /// <summary>
    /// Modelo para registrar Identidad de género con la que el paciente se siente identificado
    /// </summary>
    public class IdentidadGeneroCLS
    {
        public int idIdetidadGenero { get; set; }
        [Display(Name = "Identidad de género")]
        public string nombreIdentidadGenero { get; set; }
    }
}