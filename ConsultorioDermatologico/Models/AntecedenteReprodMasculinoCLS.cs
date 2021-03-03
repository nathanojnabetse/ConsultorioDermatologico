using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultorioDermatologico.Models
{
    /// <summary>
    /// Modelo para registrar los antecedentes reproductivos masculinos
    /// </summary>
    public class AntecedenteReprodMasculinoCLS
    {
        public int? idAntecedenteReprodMasculino { get; set; }
        [Display(Name = "E.T.S.")]
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]
        public string ets { get; set; }
        [Display(Name = "Pareja sexual")]
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]
        public string parejaSexual { get; set; }

    }
}