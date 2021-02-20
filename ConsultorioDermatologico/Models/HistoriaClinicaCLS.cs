using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultorioDermatologico.Models
{
    public class HistoriaClinicaCLS
    {
        public int idHistoriaClinica { get; set; }
        [Required]
        [Display(Name = "Paciente")]        
        public int idPaciente { get; set; }
        [Required]
        [Display(Name = "Seguro Paciente")]       
        public int idSeguroMedico { get; set; }
        [Required]
        [Display(Name = "Tipo de sangre")]        
        public int idTipoSangre { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(2000, ErrorMessage = "Longitud Máxima 2000")]
        [Display(Name = "Antecedente familiar clínico")]
        public string antecedenteFamiliarClinico { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(2000, ErrorMessage = "Longitud Máxima 2000")]
        [Display(Name = "Antecedente familiar quirúrgico")]
        public string antecedenteFamiliarQuirurgico { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(2000, ErrorMessage = "Longitud Máxima 2000")]
        [Display(Name = "Antecedente personal clínico")]
        public string antecedentePersonalClinico { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(2000, ErrorMessage = "Longitud Máxima 2000")]
        [Display(Name = "Antecedente personal quirúrgico")]
        public string antecedentePersonalQuirurgico { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(2000, ErrorMessage = "Longitud Máxima 2000")]
        [Display(Name = "Antecedente personal alergico")]
        public string antecedentePersonalAlergico { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(2000, ErrorMessage = "Longitud Máxima 2000")]
        [Display(Name = "Antecedente personal de vacunas")]
        public string antecedentePersonalVacunas { get; set; }        
        [Display(Name = "Antecedente gineco obstétrico")]
        public int idAntecedenteGinecoObstetrico { get; set; }
        [Display(Name = "Antecedente reproductivo masculino")]
        public int idAntecedenteReprodMasculino { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Longitud Máxima 100")]
        [Display(Name = "Uso de tabaco")]        
        public string tabaco { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Longitud Máxima 100")]
        [Display(Name = "Uso de alcohol")]
        public string alcohol { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Longitud Máxima 100")]
        [Display(Name = "Uso de otras drogas")]
        public string otrasDrogas { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Longitud Máxima 100")]
        [Display(Name = "Actividad física")]
        public string actividadFisica { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Medicación habitual")]
        [StringLength(1000, ErrorMessage = "Longitud Máxima 1000")]
        public string medicacionHabitual { get; set; }
        public int habilitado { get; set; }
    }
}