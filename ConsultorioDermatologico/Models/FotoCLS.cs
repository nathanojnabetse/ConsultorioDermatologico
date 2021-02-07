using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultorioDermatologico.Models
{
    public class FotoCLS
    {
        public int idFoto { get; set; }
        public int idEvolucion { get; set; }
        public string foto { get; set; } //string para recuperar
        public string nombreFoto { get; set; }
    }
}