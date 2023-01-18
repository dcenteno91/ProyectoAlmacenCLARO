using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WebAlmacen.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }
        public int IdMarca { get; set; }
        public int IdModelo { get; set; }
        public int IdAlmacen { get; set; }
        public string Serie { get; set; }        
        [Display(Name = "Precio C$")]
        public decimal Precio { get; set; }
        public Int64 Existencia { get; set; }
    }
}