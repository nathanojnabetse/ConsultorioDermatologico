using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultorioDermatologico.Models
{
    /// <summary>
    /// Modelo para el registro de evolución de un paciente
    /// Se relaciona con los datos de evolución y las posibles fotos a cargar
    /// </summary>
    public class RegistroEvolucionCLS
    {
        public EvolucionCLS evolucion { get; set; }
        public FotoCLS foto1 { get; set; }
        public FotoCLS foto2 { get; set; }
        public FotoCLS foto3 { get; set; }
        public FotoCLS foto4 { get; set; }
        public FotoCLS foto5 { get; set; }
        public FotoCLS foto6 { get; set; }
    }
}