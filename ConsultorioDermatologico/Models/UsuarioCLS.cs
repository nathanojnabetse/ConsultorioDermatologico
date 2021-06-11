using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultorioDermatologico.Models
{
    /// <summary>
    /// Modelo para el usuario que puede hacer uso del sistema.
    /// </summary>
    public class UsuarioCLS
    {
        public int idUsuario { get; set; }
        
        [Required]
        [Display(Name ="Nombre del usuario")]
        [StringLength(50, ErrorMessage = "Longitud máxima 50")]
        public string nombreUsuario{get; set;}

        [Required]
        [Display(Name = "Apellidos")]
        [StringLength(50, ErrorMessage = "Longitud máxima 50")]
        public string apellidoUsuario { get; set; }

        [Required]
        [Display(Name = "Cédula")]
        [StringLength(10, ErrorMessage = "Longitud máxima 10")]
        public string cedulaUsuario { get; set; }

        [Required]
        [Display(Name = "Rol")]
        [StringLength(50, ErrorMessage = "Longitud máxima 50")]
        public string rolUsuario {get; set;}
        
        [Required]
        [Display(Name = "Alias")]
        [StringLength(50, ErrorMessage = "Longitud máxima 50")]
        public string aliasUsuario { get; set; }
        
        [Required]
        [Display(Name = "Contraseña")]
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]
        public string contraseñaUsuario {get; set;}
        
        [Required]
        [Display(Name = "Correo")]
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]
        [EmailAddress(ErrorMessage = "Ingrese un e-mail válido")]
        public string correoUsuario {get; set;}

        [Display(Name = "Código MSP")]
        [StringLength(50, ErrorMessage = "Longitud máxima 50")]
        public string codigoMSP { get; set; }
    }
}