using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultorioDermatologico.Models
{
    /// <summary>
    /// Modelo para la orientación sexual con la que el paciente se siente identificado   
    /// </summary>
    public class OrientacionSexualCLS
    {
        public int idOrientacionSexual { get; set; }
        [Display(Name = "Orientacion Sexual")]
        public string nombreOrientacionSexual { get; set; }
    }
}