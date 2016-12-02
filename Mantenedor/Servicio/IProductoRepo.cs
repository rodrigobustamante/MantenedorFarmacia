using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantenedor.Servicio
{
    public interface IProductoRepo
    {
        IEnumerable<ProductoServicio> GetProduct();
        ProductoServicio GetProductID(int id);
        ProductoServicio AddProduct(ProductoServicio cliente);
        void DeleteProduct(int id);
        bool UpdateProduct(ProductoServicio cliente);
    }
}
