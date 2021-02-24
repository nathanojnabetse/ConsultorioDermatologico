//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsultorioDermatologico.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblEvolucion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblEvolucion()
        {
            this.tblFotos = new HashSet<tblFotos>();
        }
    
        public int idEvolucion { get; set; }
        public Nullable<int> idHistoriaClinica { get; set; }
        public byte[] mapaCorporal { get; set; }
        public string nombreMapa { get; set; }
        public string diagnostico { get; set; }
        public string motivoConsulta { get; set; }
        public string examenFisico { get; set; }
        public string prescripcion { get; set; }
        public string recomendaciones { get; set; }
        public Nullable<System.DateTime> fechaVisita { get; set; }
        public Nullable<int> habilitado { get; set; }
    
        public virtual tblHistoriaClinica tblHistoriaClinica { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblFotos> tblFotos { get; set; }
    }
}
