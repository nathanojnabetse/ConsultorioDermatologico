﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultorioDermatologico.Models
{
    public class PacienteCLS
    {
        public int idPaciente { get; set; }
        //[Required]
        [Display(Name = "Nombres")]
        [StringLength(50, ErrorMessage = "Longitud máxima 50")]
        public string nombres { get; set; }
        [Required]
        [Display(Name = "Apellidos")]
        [StringLength(50, ErrorMessage = "Longitud máxima 50")]
        public string apellidos { get; set; }
        [Required]
        [Display(Name = "Cédula")]
        [StringLength(10, ErrorMessage = "Longitud máxima 10")]
        public string cedula { get; set; }
        [Required]
        [Display(Name = "Fecha Nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fechaNacimiento { get; set; }        
        [Display(Name = "Representante")]        
        public int? idRepresentante { get; set; }
        [Required]
        [Display(Name = "Identidad género")]
        public int idIdentidadGenero { get; set; }
        [Required]
        [Display(Name = "Orientacion sexual")]
        public int idOrientacionSexual { get; set; }
        [Required]
        [Display(Name = "Ciudad nacimiento")]
        [StringLength(50, ErrorMessage = "Longitud máxima 50")]
        public string ciudadNacimiento { get; set; }
        [Required]
        [Display(Name = "Ciudad residencia")]
        [StringLength(50, ErrorMessage = "Longitud máxima 50")]
        public string ciudadResidencia { get; set; }
        [Required]
        [Display(Name = "Ocupación")]
        [StringLength(50, ErrorMessage = "Longitud máxima 50")]
        public string ocupacion { get; set; }
        [Required]
        [Display(Name = "Profesión")]
        [StringLength(50, ErrorMessage = "Longitud máxima 50")]
        public string profesion { get; set; }
        [Required]
        [Display(Name = "Tipo discapacidad")]
        [StringLength(50, ErrorMessage = "Longitud máxima 50")]        
        public string tipoDiscapacidad { get; set; }
        [Required]
        [Display(Name = "% Discapacidad")]
        public int porcentajeDiscapacidad { get; set; }
        [Required]
        [Display(Name = "Estado civil")]
        [StringLength(50, ErrorMessage = "Longitud máxima 50")]
        public string estadoCivil { get; set; }
        [Required]
        [Display(Name = "Lateralidad")]
        [StringLength(50, ErrorMessage = "Longitud máxima 50")]
        public string lateralidad { get; set; }
        [Required]
        [Display(Name = "Nivel educación")]
        [StringLength(50, ErrorMessage = "Longitud máxima 50")]
        public string nivelEducacion { get; set; }
        [Required]
        [Display(Name = "Dirección")]
        [StringLength(500, ErrorMessage = "Longitud máxima 500")]
        public string direccion { get; set; }
        [Required]
        [Display(Name = "Teléfono celular")]
        [StringLength(10, ErrorMessage = "Longitud máxima 10")]
        public string telefonoPersonal { get; set; }
        [Required]
        [Display(Name = "Teléfono residencial")]
        [StringLength(10, ErrorMessage = "Longitud máxima 10")]
        public string telefonoResidencial { get; set; }
        [Required]
        [Display(Name = "Correo Electrónico")]
        [StringLength(100, ErrorMessage = "Longitud máxima 100")]
        [EmailAddress(ErrorMessage = "Ingrese un email valido")]
        public string correoElectronico { get; set; }
        [Required]
        [Display(Name = "Religión")]
        [StringLength(50, ErrorMessage = "Longitud máxima 50")]
        public string religion { get; set; }        
        [Required]
        [Display(Name = "Contacto de emergencia")]        
        public int idContactoEmergencia { get; set; }
        public int habilitado { get; set; }
    }
}