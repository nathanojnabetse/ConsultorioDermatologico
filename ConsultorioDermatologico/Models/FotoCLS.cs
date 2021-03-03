using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultorioDermatologico.Models
{
    /// <summary>
    /// Modelo con la información de las fotografías almacenadas
    /// </summary>
    public class FotoCLS
    {
        public int idFoto { get; set; }
        public int idEvolucion { get; set; }
        public string foto { get; set; } //string para recuperar
        public string nombreFoto { get; set; }
        public string extension { get; set; }
    }
}