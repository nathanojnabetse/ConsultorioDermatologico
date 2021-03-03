using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultorioDermatologico.Models
{
    /// Modelo para registrar los antecedentes gineco obstetricos.
    public class AntecedenteGinecoObstetricoCLS
    {
        public int? idAntecedenteGinecoObstetrico { get; set; }
        [Display(Name = "Menarquia")]
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]
        public string menarquia { get; set; }
        [Display(Name = "Ciclo")]
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]
        public string ciclo { get; set; }
        [Display(Name = "Última menstruación")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? fechaUltimaMenstruacion { get; set; }
        [Display(Name = "Gestas")]
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]
        public string gestas { get; set; }
        [Display(Name = "Partos")]
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]
        public string partos { get; set; }
        [Display(Name = "Cesárea")]
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]
        public string cesarea { get; set; }
        [Display(Name = "Abortos")]
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]
        public string abortos { get; set; }
        [Display(Name = "Hijos Vivos")]
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]
        public string hijosVivos { get; set; }
        [Display(Name = "Hijos Muertos")]
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]
        public string hijosMuertos { get; set; }
        [Display(Name = "Vida sexual activa")]
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]
        public string vidaSexualActiva { get; set; }
        [Display(Name = "Planificación familiar")]
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]
        public string metodoPlanificacionFamiliar { get; set; }
}
}