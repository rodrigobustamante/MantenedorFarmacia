using Mantenedor.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;

namespace Mantenedor.Controllers
{
    public class CategoriaController : Controller
    {
        private MantenedorEntities db = new MantenedorEntities();

        public ActionResult Index()
        {
            if (Session["correo"] == null)
            {
                return RedirectToAction("../Home/Welcome");
            }
            else if (Session["cargo"].ToString() == "1" || Session["cargo"].ToString() == "2")
            {
                return View(db.Categoria.ToList());
            }
            else
            {
                return RedirectToAction("../Home/Welcome");
            }
        }

        public ActionResult Create()
        {
            if (Session["correo"] == null)
            {
                return RedirectToAction("../Home/Welcome");
            }
            else if (Session["cargo"].ToString() == "1" || Session["cargo"].ToString() == "2")
            {
                return View();
            }
            else
            {
                return RedirectToAction("../Home/Welcome");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCategoria,NombreCategoria")] Categoria categoria)
        {
            if (Session["correo"] == null)
            {
                return RedirectToAction("../Home/Welcome");
            }
            else if (Session["cargo"].ToString() == "1" || Session["cargo"].ToString() == "2")
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        db.Categoria.Add(categoria);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception)
                    {
                        TempData["error"] = "Ya existe una categoría con este nombre";
                        return View();
                    }
                }
                return View(categoria);
            }
            else
            {
                return RedirectToAction("../Home/Welcome");
            }            
        }

        public ActionResult Edit(int? id)
        {
            if (Session["correo"] == null)
            {
                return RedirectToAction("../Home/Welcome");
            }
            else if (Session["cargo"].ToString() == "1" || Session["cargo"].ToString() == "2")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Categoria categoria = db.Categoria.Find(id);
                if (categoria == null)
                {
                    return HttpNotFound();
                }
                return View(categoria);
            }
            else
            {
                return RedirectToAction("../Home/Welcome");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCategoria,NombreCategoria")] Categoria categoria)
        {
            if (Session["correo"] == null)
            {
                return RedirectToAction("../Home/Welcome");
            }
            else if (Session["cargo"].ToString() == "1" || Session["cargo"].ToString() == "2")
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        db.Entry(categoria).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception)
                    {
                        TempData["error"] = "Ya existe una categoría con este nombre";
                        return View();
                    }
                    
                }
                return View(categoria);
            }
            else
            {
                return RedirectToAction("../Home/Welcome");
            }
        }

        public ActionResult Delete(int? id)
        {
            if (Session["correo"] == null)
            {
                return RedirectToAction("../Home/Welcome");
            }
            else if (Session["cargo"].ToString() == "1" || Session["cargo"].ToString() == "2")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Categoria categoria = db.Categoria.Find(id);
                if (categoria == null)
                {
                    return HttpNotFound();
                }
                return View(categoria);
            }
            else
            {
                return RedirectToAction("../Home/Welcome");
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
            else if (Session["cargo"].ToString() == "1" || Session["cargo"].ToString() == "2")
            {
                Categoria categoria = db.Categoria.Find(id);
                db.Categoria.Remove(categoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("../Home/Welcome");
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
