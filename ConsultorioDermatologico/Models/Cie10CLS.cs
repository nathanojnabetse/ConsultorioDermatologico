using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultorioDermatologico.Models
{
    /// <summary>
    /// Modelo para listar la lista del Código Internacional de Enfermedades
    /// </summary>
    public class Cie10CLS
    {
        public int idCie { get; set; }
        [Display(Name = "Código CIE")]
        public string codigo { get; set; }
        [Display(Name = "Enfermedad")]
        public string enfermedad { get; set; }
        public string capitulo { get; set; }
        public string titulo { get; set; }
    }
}