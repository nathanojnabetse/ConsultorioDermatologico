﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultorioDermatologico.Models
{
    public class EvolucionCLS
    {
        public int idEvolucion { get; set; }
        public int idHistoriaClinica { get; set; }
        public string mapaCorporal { get; set; } //recuperar la cadena
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(5000, ErrorMessage = "Longitud Máxima 5000")]
        [Display(Name = "Diagnóstico")]
        public string diagnostico { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(5000, ErrorMessage = "Longitud Máxima 5000")]
        [Display(Name = "Motivo de consulta")]
        public string motivoConsulta { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(5000, ErrorMessage = "Longitud Máxima 5000")]
        [Display(Name = "Exámen físico")]
        public string examenFisico { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(5000, ErrorMessage = "Longitud Máxima 5000")]
        [Display(Name = "Prescripción")]
        public string prescripcion { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(5000, ErrorMessage = "Longitud Máxima 5000")]
        [Display(Name = "Recomendaciones")]
        public string recomendaciones { get; set; }
        [Display(Name = "Fecha de visitas")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fechaVisita { get; set; }
        public int habilitado { get; set; }
    }
}