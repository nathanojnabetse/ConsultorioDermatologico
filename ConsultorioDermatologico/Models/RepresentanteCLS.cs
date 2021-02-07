using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultorioDermatologico.Models
{
    public class RepresentanteCLS
    {
        // <summary>
        /// Propiedades del representante del paciente (En caso de ser menor de 18 años)
        /// El tag [Display] es usado para mostrar un nombre en la vista       
        /// </summary>
        public int idRepresentante { get; set; }
        [Required]
        [Display(Name ="Nombres")]                
        [StringLength(50, ErrorMessage = "Longitud máxima 50")]
        public string nombreRepresentante { get; set; }
        [Required]
        [Display(Name = "Apellidos")]
        [StringLength(50, ErrorMessage = "Longitud máxima 50")]
        public string apellidoRepresentante { get; set; }
        [Required]
        [Display(Name = "Teléfono")]
        [StringLength(10, ErrorMessage = "Longitud máxima 10")]
        public string telefonoRepresentante { get; set; }
        [Required]
        [Display(Name = "Correo")]
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]
        [EmailAddress(ErrorMessage = "Ingrese un e-mail válido")]
        public string correoRepresentante { get; set; }
}
}