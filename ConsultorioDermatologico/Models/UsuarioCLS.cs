using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultorioDermatologico.Models
{
    public class UsuarioCLS
    {

        /// <summary>
        /// Propiedades de usuario que puede hacer uso del sistema
        /// Tipos de usuario: ADMINISTRADOR (gestión total del sistema) 
        ///                   MEDICO (gestión de historias clinicas e información del paciente)
        /// El tag [Display] es usado para mostrar un nombre en la vista       
        /// </summary>
        public int idUsuario { get; set; }
        
        [Required]
        [Display(Name ="Nombres")]
        [StringLength(50, ErrorMessage = "Longitud máxima 50")]
        public string nombreUsuario{get; set;}
        
        [Required]
        [Display(Name = "Apellidos")]
        [StringLength(50, ErrorMessage = "Longitud máxima 50")]
        public string apellidoUsuario{get; set;}
        
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
        
    }
}