using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamenP.Models
{
    public class producto
    {
        public int Id_Producto { get; set; }
        public string Nombre { get; set; }
        public int Codigo { get; set; }
        public string Medida { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
      
    }
}
