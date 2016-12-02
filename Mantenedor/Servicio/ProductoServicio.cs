using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mantenedor.Servicio
{
    public class ProductoServicio
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int StockDisponible { get; set; }
        public int StockTotal { get; set; }
        public int Precio { get; set; }
        public int IdCategoria { get; set; }
        public int IdSucursal { get; set; }


    }
}