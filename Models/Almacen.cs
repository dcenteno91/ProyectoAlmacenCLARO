using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAlmacen.Models
{
    public class Almacen
    {
        public int IdAlmacen { get; set; }
        public string NombreAlmacen { get; set; }
        public string Direccion { get; set;}
        public string Estado { get; set;}
        public DateTime FechaRegistro { get; set;}
        public DateTime FechaModificacion { get; set;}
    }
}