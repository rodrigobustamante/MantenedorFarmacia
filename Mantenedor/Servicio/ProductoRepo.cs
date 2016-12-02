using Mantenedor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mantenedor.Servicio
{
    public class ProductoRepo : IProductoRepo
    {
        List<ProductoServicio> productos;
        private MantenedorEntities _context;
        public ProductoRepo()
        {
            _context = new MantenedorEntities();
        }

        public IEnumerable<ProductoServicio> GetProduct()
        {
            productos = new List<ProductoServicio>();
            foreach (var p in _context.Producto.ToList())
            {
                ProductoServicio ps = new ProductoServicio();
                ps.IdProducto = p.IdProducto;
                ps.NombreProducto = p.NombreProducto;
                ps.Precio = p.Precio;
                ps.StockDisponible = p.StockDisponible;
                ps.StockTotal = p.StockTotal;
                ps.IdCategoria = p.IdCategoria;
                ps.IdSucursal = p.IdSucursal;
                productos.Add(ps);
            }
            return productos;
        }

        public ProductoServicio GetProductID(int id)
        {
            var ps = _context.Producto.Single(p => p.IdProducto == id);
            ProductoServicio producto = new ProductoServicio();
            producto.IdProducto = ps.IdProducto;
            producto.NombreProducto = ps.NombreProducto;
            producto.StockDisponible = ps.StockDisponible;
            producto.StockTotal = ps.StockTotal;
            producto.Precio = ps.Precio;
            producto.IdCategoria = ps.IdCategoria;
            producto.IdSucursal = ps.IdSucursal;
            return producto;
        }

        public ProductoServicio AddProduct(ProductoServicio producto)
        {
            Models.Producto p = new Models.Producto();
            p.NombreProducto = producto.NombreProducto;
            p.StockDisponible = producto.StockDisponible;
            p.StockTotal = producto.StockTotal;
            p.Precio = producto.Precio;
            p.IdCategoria = producto.IdCategoria;
            p.IdSucursal = producto.IdSucursal;
            _context.Producto.Add(p);
            _context.SaveChanges();
            return producto;
        }

        public void DeleteProduct(int id)
        {
            var p = _context.Producto.First(pro => pro.IdProducto == id);
            _context.Producto.Remove(p);
            _context.SaveChanges();
        }

        public bool UpdateProduct(ProductoServicio producto)
        {
            if (producto == null)
            {
                throw new ArgumentNullException("item");
            }
            Models.Producto p = _context.Producto.Single(pro => pro.IdProducto == producto.IdProducto);
            p.NombreProducto = producto.NombreProducto;
            p.Precio = producto.Precio;
            p.StockDisponible = producto.StockDisponible;
            p.StockTotal = producto.StockTotal;
            p.IdCategoria = producto.IdCategoria;
            p.IdSucursal = producto.IdSucursal;
            _context.SaveChanges();
            return true;
        }

    }
}