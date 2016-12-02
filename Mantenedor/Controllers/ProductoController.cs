using Mantenedor.Models;
using Mantenedor.Servicio;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using System.Net.Http;
using System.Collections.Generic;
using System.Web;
using System;
using System.Net.Http.Headers;

namespace Mantenedor.Controllers
{
    public class ProductoController : Controller
    {
        private MantenedorEntities db = new MantenedorEntities();

        public ActionResult Index()
        {
            if (Session["correo"] == null)
            {
                return RedirectToAction("../Home/Welcome");
            }else
            {
                List<Producto> lst = new List<Producto>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:49353/");

                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("aplication/json"));


                HttpResponseMessage response = client.GetAsync("api/Producto").Result;

                if (response.IsSuccessStatusCode)
                {
                    lst = response.Content.ReadAsAsync<List<Producto>>().Result;
                }

                return View(lst);

                //var producto = db.Producto.Include(u => u.Categoria).Include(u => u.Sucursal);
                //return View(producto.ToList());
            }
            
        }
        
        public ActionResult Create()
        {
            if (Session["correo"] == null)
            {
                return RedirectToAction("../Home/Welcome");
            }
            else
            {
                ViewBag.IdCategoria = new SelectList(db.Categoria, "IdCategoria", "NombreCategoria");
                ViewBag.IdSucursal = new SelectList(db.Sucursal, "IdSucursal", "NombreSucursal");
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProducto,NombreProducto,StockTotal,StockDisponible,Precio,IdCategoria,IdSucursal")] Producto producto)
        {
            if (Session["correo"] == null)
            {
                return RedirectToAction("../Home/Welcome");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    if (producto.StockTotal >= producto.StockDisponible)
                    {
                        db.Producto.Add(producto);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.IdCategoria = new SelectList(db.Categoria, "IdCategoria", "NombreCategoria");
                        ViewBag.IdSucursal = new SelectList(db.Sucursal, "IdSucursal", "NombreSucursal");
                        TempData["error"] = "El stock total no puede ser mayor al stock disponible";
                        return View();
                    }
                }
                ViewBag.IdCategoria = new SelectList(db.Categoria, "IdCategoria", "NombreCategoria", producto.IdCategoria);
                ViewBag.IdSucursal = new SelectList(db.Sucursal, "IdSucursal", "NombreSucursal", producto.IdSucursal);
                return View(producto);
            }
        }
        
        public ActionResult Edit(int? id)
        {
            if (Session["correo"] == null)
            {
                return RedirectToAction("../Home/Welcome");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Producto producto = db.Producto.Find(id);
                if (producto == null)
                {
                    return HttpNotFound();
                }
                ViewBag.IdCategoria = new SelectList(db.Categoria, "IdCategoria", "NombreCategoria", producto.IdCategoria);
                ViewBag.IdSucursal = new SelectList(db.Sucursal, "IdSucursal", "NombreSucursal", producto.IdSucursal);
                return View(producto);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProducto,NombreProducto,StockTotal,StockDisponible,Precio,IdCategoria,IdSucursal")] Producto producto)
        {
            if (Session["correo"] == null)
            {
                return RedirectToAction("../Home/Welcome");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    if (producto.StockTotal >= producto.StockDisponible)
                    {
                        db.Entry(producto).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.IdCategoria = new SelectList(db.Categoria, "IdCategoria", "NombreCategoria");
                        ViewBag.IdSucursal = new SelectList(db.Sucursal, "IdSucursal", "NombreSucursal");
                        TempData["error"] = "El stock total no puede ser mayor al stock disponible";
                        return View();
                    }
                }
                ViewBag.IdCategoria = new SelectList(db.Categoria, "IdCategoria", "NombreCategoria", producto.IdCategoria);
                ViewBag.IdSucursal = new SelectList(db.Sucursal, "IdSucursal", "NombreSucursal", producto.IdSucursal);
                return View(producto);
            }
        }
        
        public ActionResult Delete(int? id)
        {
            if (Session["correo"] == null)
            {
                return RedirectToAction("../Home/Welcome");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Producto producto = db.Producto.Find(id);
                if (producto == null)
                {
                    return HttpNotFound();
                }
                return View(producto);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["correo"] == null)
            {
                return RedirectToAction("../Home/Welcome");
            }
            else
            {
                Producto producto = db.Producto.Find(id);
                db.Producto.Remove(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Show(int? id)
        {
            if (Session["correo"] == null)
            {

                return RedirectToAction("../Home/Welcome");

            } else {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Producto producto= db.Producto.Find(id);
                if (producto == null)
                {
                    return HttpNotFound();
                }

                return View(producto);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Logout()
        {
           FormsAuthentication.SignOut();
           Session.Abandon();
           return RedirectToAction("../Home/Welcome");
        }
    }
}
