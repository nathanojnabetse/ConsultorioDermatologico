using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultorioDermatologico.Models
{
    public class AntecedenteGinecoObstetricoCLS
    {
        /// <summary>
        /// Propiedades de los antecedentes gineco obstetricos de un paciente femenino
        /// El tag [Display] es usado para mostrar un nombre en la vista       
        /// </summary>   
        public int? idAntecedenteGinecoObstetrico { get; set; }
        //[Required]
        [Display(Name = "Menarquia")]
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]
        public string menarquia { get; set; }
        //[Required]
        [Display(Name = "Ciclo")]
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]
        public string ciclo { get; set; }
        //[Required]
        [Display(Name = "Última menstruación")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? fechaUltimaMenstruacion { get; set; }
        //[Required]
        [Display(Name = "Gestas")]
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]
        public string gestas { get; set; }
        //[Required]
        [Display(Name = "Partos")]
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]
        public string partos { get; set; }
        //[Required]
        [Display(Name = "Cesárea")]
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]
        public string cesarea { get; set; }
        //[Required]
        [Display(Name = "Abortos")]
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]
        public string abortos { get; set; }
        //[Required]
        [Display(Name = "Hijos Vivos")]
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]
        public string hijosVivos { get; set; }
        //[Required]
        [Display(Name = "Hijos Muertos")]
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]
        public string hijosMuertos { get; set; }
        //[Required]
        [Display(Name = "Vida sexual activa")]
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]
        public string vidaSexualActiva { get; set; }
        //[Required]
        [Display(Name = "Planificación familiar")]
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]
        public string metodoPlanificacionFamiliar { get; set; }
}
}