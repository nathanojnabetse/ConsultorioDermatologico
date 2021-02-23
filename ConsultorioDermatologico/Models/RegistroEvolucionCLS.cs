using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultorioDermatologico.Models
{
    public class RegistroEvolucionCLS
    {
        public EvolucionCLS evolucion { get; set; }
        public List<FotoCLS> listaFotos { get; set; }
    }
}