using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultorioDermatologico.Models
{
    /// <summary>
    /// Modelo para registrar la lateralidad del paciente
    /// </summary>
    public class LateralidadCLS
    {
        public int idLateralidad { get; set; }
        [Display(Name = "Lateralidad")]
        public string nombreLateralidad { get; set; }
    }
}