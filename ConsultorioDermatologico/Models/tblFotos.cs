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
    
    public partial class tblFotos
    {
        public int idFoto { get; set; }
        public Nullable<int> idEvolucion { get; set; }
        public byte[] foto { get; set; }
        public string nombreFoto { get; set; }
    
        public virtual tblEvolucion tblEvolucion { get; set; }
    }
}
