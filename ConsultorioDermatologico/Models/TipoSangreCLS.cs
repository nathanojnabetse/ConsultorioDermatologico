using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultorioDermatologico.Models
{
    /// <summary>
    /// Modelo para el tipo de sangre del paciente registrado.
    /// </summary>
    public class TipoSangreCLS
    {
        public int idTipoSangre { get; set; }
        [Display(Name = "Grupo sanguíneo")]
        public string sangre { get; set; }
    }
}