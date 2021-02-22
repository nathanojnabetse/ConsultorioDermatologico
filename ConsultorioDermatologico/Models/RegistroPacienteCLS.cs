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
        public AntecedenteGinecoObstetricoCLS antecedenteGinecoObstetrico { get; set; }
        public AntecedenteReprodMasculinoCLS antecedenteReprodMasculino { get; set; }

        //Propiedad adicional En caso de encontrar una cedula repetida
        public string mensajeError { get; set; }
    }
}