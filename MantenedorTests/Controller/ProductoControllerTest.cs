using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Mantenedor.Servicio;
using Mantenedor.Controllers;
using Mantenedor.Models;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web;
using System.Security.Principal;

namespace MantenedorTests.Controller
{
    /// <summary>
    /// Descripción resumida de CategoriaController
    /// </summary>
    [TestClass]
    public class ProductoControllerTest
    {
        private static TestsController GetHomeController(IProductoRepo repositorio)
        {
            TestsController controller = new TestsController(repositorio);

            controller.ControllerContext = new ControllerContext()
            {
                Controller = controller,
                RequestContext = new RequestContext(new MockHttpContext(), new RouteData())
            };
            return controller;
        }
        private class MockHttpContext : HttpContextBase
        {
            private readonly IPrincipal _user = new GenericPrincipal(
                     new GenericIdentity("someUser"), null /* roles */);

            public override IPrincipal User
            {
                get
                {
                    return _user;
                }
                set
                {
                    base.User = value;
                }
            }
        }

        [TestMethod]
        public void ListadoProductos()
        {
            Producto p1 = new Producto();
            p1.IdProducto = 1;
            p1.NombreProducto = "Banana boat";
            p1.StockDisponible = 10;
            p1.StockTotal = 15;
            p1.Precio = 2500;
            p1.IdSucursal = 1;
            p1.IdCategoria = 1;
            EntityProducto repositorio = new EntityProducto();
            repositorio.AddProducto(p1);
            var controller = GetHomeController(repositorio);
            var resultado = controller.Todos();
            var model = (IEnumerable<Producto>)resultado.ViewData.Model;
            CollectionAssert.Contains(model.ToList(), p1);
        }
    }
}
