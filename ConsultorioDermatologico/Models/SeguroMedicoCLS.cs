using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultorioDermatologico.Models
{
    public class SeguroMedicoCLS
    {
        /// <summary>
        /// Propiedades de los seguros médicos diponibles
        /// El tag [Display] es usado para mostrar un nombre en la vista       
        /// </summary>        
        public int idSeguroMedico { get; set; }
        [Required]
        [Display(Name = "Empresa aseguradora")]                
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]        
        public string nombreSeguro { get; set; }
        public int habilitado { get; set; }
    }
}