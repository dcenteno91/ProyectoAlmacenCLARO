using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WebAlmacen.Models
{
    public class ProductoDTO
    {
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Almacen { get; set; }
        public string Serie { get; set; }

        [Display(Name = "Precio C$")]
        public decimal Precio { get; set; }

        public decimal Existencia { get; set; }
    }
}