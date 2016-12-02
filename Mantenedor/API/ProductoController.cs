using Mantenedor.Servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections;

namespace Mantenedor.API
{
    public class ProductoController : ApiController
    {
        private static readonly IProductoRepo repo = new ProductoRepo();
        public HttpResponseMessage Get()
        {
            List<ProductoServicio> productosList = repo.GetProduct().ToList();
            return Request.CreateResponse<List<ProductoServicio>>(HttpStatusCode.OK, productosList);
        }

        public IHttpActionResult Get(int id)
        {
            var producto = repo.GetProductID(id);
            return Ok(producto);
        }

        public HttpResponseMessage Post([FromBody]ProductoServicio producto)
        {
            producto.IdProducto = repo.GetProduct().Select(p => p.IdProducto).Max() + 1;
            repo.AddProduct(producto);
            var response = Request.CreateResponse(HttpStatusCode.Created, producto);
            var uri = Url.Link("DefaultApi", new { id = producto.IdProducto });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void Put(int id, [FromBody] ProductoServicio producto)
        {
            producto.IdProducto = id;
            if (!repo.UpdateProduct(producto))
                NotFound();
            repo.UpdateProduct(producto);
        }

        public void Delete(int id)
        {
            repo.DeleteProduct(id);
        }
    }
}
