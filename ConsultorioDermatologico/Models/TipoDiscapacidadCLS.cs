using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultorioDermatologico.Models
{
    /// <summary>
    /// Modelo para el tipo de discapacidad del paciente registrado
    /// </summary>
    public class TipoDiscapacidadCLS
    {
        public int idTipoDiscapacidad { get; set; }
        [Display(Name = "Tipo de discapacidad")]
        public string tipo { get; set; }
    }
}