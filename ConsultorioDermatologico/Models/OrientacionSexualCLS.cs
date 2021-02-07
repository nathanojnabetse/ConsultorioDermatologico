using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultorioDermatologico.Models
{
    public class OrientacionSexualCLS
    {
        // <summary>
        /// Orientación sexual con la que el paciente se siente identificado
        /// Orientaciones sexuales: Lesbiana, Gay, Bisexual, Heterosexual, No sabe / No responde
        /// El tag [Display] es usado para mostrar un nombre en la vista       
        /// </summary>
        public int idOrientacionSexual { get; set; }
        [Display(Name = "Orientacion Sexual")]
        public string nombreOrientacionSexual { get; set; }
    }
}