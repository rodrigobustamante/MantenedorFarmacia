using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mantenedor.Servicio;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MantenedorTests.Model
{
    public class ProductoMemoria : IProductoRepo
    {
        private List<ProductoServicio> _db = new List<ProductoServicio>();
        public IEnumerable<ProductoServicio> ProductosAll()
        {
            return _db.ToList();
        }

        public void AgregarProductos(ProductoServicio p)
        {
            _db.Add(p);
        }
    }
}
