using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultorioDermatologico.Models
{
    public class RegistroPacienteCLS
    {
        public PacienteCLS paciente { get; set; }
        public HistoriaClinicaCLS historiaClinica { get; set; }        
        public ContactoEmergenciaCLS contactoEmergencia{ get;set;}

        public bool chkMayorEdad { get; set; }
    }
}