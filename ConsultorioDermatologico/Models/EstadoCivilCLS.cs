using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultorioDermatologico.Models
{
    /// <summary>
    /// Modelo para indicar el estado civil del paciente
    /// </summary>
    public class EstadoCivilCLS
    {
        public int idEstadoCivil { get; set; }
        [Display(Name = "Estado Civil")]
        public string nombreEstadoCivil { get; set; }
    }
}