using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultorioDermatologico.Models
{
    /// <summary>
    /// Modelo para el registro de la Evolución del tratamiento del paciente
    /// </summary>
    public class EvolucionCLS
    {
        public int idEvolucion { get; set; }
        public int idHistoriaClinica { get; set; }
        public string mapaCorporal { get; set; } //recuperar la cadena de la fotografía del mapa corporal marcado
        public string nombreMapa { get; set; }
        public string extension { get; set; }
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

        //Propiedades disponibles para registros gineco obstetricos
        [Display(Name = "Ciclo")]
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]
        public string ciclo { get; set; }
        [Display(Name = "Última menstruación")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? fechaUltimaMenstruacion { get; set; }
        [Display(Name = "Vida sexual activa")]
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]
        public string vidaSexualActiva { get; set; }
        [Display(Name = "Planificación familiar")]
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]
        public string metodoPlanificacionFamiliar { get; set; }

        public int habilitado { get; set; }
        
        //Propiedades adicionales, usadas para que el admin pueda visualizar las evoluciones desactivadas por un médico
        public int idPaciente { get; set; }
        public string nombresPaciente { get; set; }
        public string cedula { get; set; }
    }
}