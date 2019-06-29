using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamenP.Models
{
    public class Cliente
    {
        public int Id_Cliente { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Nit { get; set; }
    }
}