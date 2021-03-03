using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultorioDermatologico.Models
{
    /// <summary>
    /// Modelo para los seguros médicos diponibles
    /// </summary>        
    public class SeguroMedicoCLS
    {      
        public int idSeguroMedico { get; set; }
        [Required]
        [Display(Name = "Empresa aseguradora")]                
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]        
        public string nombreSeguro { get; set; }
        public int habilitado { get; set; }
    }
}