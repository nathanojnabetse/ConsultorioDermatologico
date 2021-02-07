using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultorioDermatologico.Models
{
    public class IdentidadGeneroCLS
    {
        // <summary>
        /// Identidad de género con la que el paciente se siente identificado
        /// Identidades de genero: Femenino, Masculino, trans - Femenino, Trans - masculino, No sabe / No contesta
        /// El tag [Display] es usado para mostrar un nombre en la vista       
        /// </summary>
        public int idIdetidadGenero { get; set; }
        [Display(Name = "Identidad de género")]
        public string nombreIdentidadGenero { get; set; }
    }
}