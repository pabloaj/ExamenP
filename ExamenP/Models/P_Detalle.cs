//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExamenP.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class P_Detalle
    {
        public int ID_detalle { get; set; }
        public Nullable<int> Factura_id { get; set; }
        public Nullable<int> Producto_id { get; set; }
        public Nullable<int> cantidad { get; set; }
    
        public virtual P_Facturas P_Facturas { get; set; }
        public virtual P_Productos P_Productos { get; set; }
    }
}
