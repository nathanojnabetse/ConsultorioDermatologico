using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultorioDermatologico.Models
{
    /// <summary>
    /// Modelo con la información del contacto de emergencia del paciente
    /// </summary>
    public class ContactoEmergenciaCLS
    {
        public int idContactoEmergencia { get; set; }
        [Required]
        [Display(Name = "Nombres")]
        [StringLength(50, ErrorMessage = "Longitud máxima 50")]
        public string nombreContactoEmergencia { get; set; }
        [Required]
        [Display(Name = "Apellidos")]
        [StringLength(50, ErrorMessage = "Longitud máxima 50")]
        public string apellidoContactoEmergencia { get; set; }
        [Required]
        [Display(Name = "Teléfono")]
        [StringLength(10, ErrorMessage = "Longitud máxima 10")]
        public string telefonoContactoEmergencia { get; set; }
        [Required]
        [Display(Name = "Correo")]
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]
        [EmailAddress(ErrorMessage = "Ingrese un e-mail válido")]
        public string correoContactoEmergencia { get; set; }
    }
}