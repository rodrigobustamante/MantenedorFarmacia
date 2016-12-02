using Mantenedor.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Security;

namespace Mantenedor.Controllers
{
    public class UsuarioController : Controller
    {
        private MantenedorEntities db = new MantenedorEntities();

        public ActionResult Index()
        {
            if (Session["correo"] == null)
            {
                return RedirectToAction("../Home/Welcome");
            }
            else if(Session["cargo"].ToString() == "1" ||Session["cargo"].ToString() == "2")
            {
                var usuario = db.Usuario.Include(u => u.Cargo).Include(u => u.Ciudad).Include(u => u.Comuna).Include(u => u.Contrato).Include(u => u.Sucursal);
                return View(usuario.ToList());
            }else
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
                ViewBag.IdCargo = new SelectList(db.Cargo, "IdCargo", "NombreCargo");
                ViewBag.IdCiudad = new SelectList(db.Ciudad, "IdCiudad", "NombreCiudad");
                ViewBag.IdComuna = new SelectList(db.Comuna, "IdComuna", "NombreComuna");
                ViewBag.IdContrato = new SelectList(db.Contrato, "IdContrato", "TipoContrato");
                ViewBag.IdSucursal = new SelectList(db.Sucursal, "IdSucursal", "NombreSucursal");
                return View();
            }else
            {
                return RedirectToAction("../Home/Welcome");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUsuario,Nombre,Apellido,Correo,Contrasena,Telefono,Direccion,IdComuna,IdCiudad,IdSucursal,IdCargo,IdContrato")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailAddress m = new MailAddress(usuario.Correo);
                    db.Usuario.Add(usuario);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (System.Exception)
                {
                    ViewBag.IdCargo = new SelectList(db.Cargo, "IdCargo", "NombreCargo", usuario.IdCargo);
                    ViewBag.IdCiudad = new SelectList(db.Ciudad, "IdCiudad", "NombreCiudad", usuario.IdCiudad);
                    ViewBag.IdComuna = new SelectList(db.Comuna, "IdComuna", "NombreComuna", usuario.IdComuna);
                    ViewBag.IdContrato = new SelectList(db.Contrato, "IdContrato", "TipoContrato", usuario.IdContrato);
                    ViewBag.IdSucursal = new SelectList(db.Sucursal, "IdSucursal", "NombreSucursal", usuario.IdSucursal);
                    TempData["error"] = "Correo invalido o ya utilizado por un trabajador";
                    return View();
                }
            }
            ViewBag.IdCargo = new SelectList(db.Cargo, "IdCargo", "NombreCargo", usuario.IdCargo);
            ViewBag.IdCiudad = new SelectList(db.Ciudad, "IdCiudad", "NombreCiudad", usuario.IdCiudad);
            ViewBag.IdComuna = new SelectList(db.Comuna, "IdComuna", "NombreComuna", usuario.IdComuna);
            ViewBag.IdContrato = new SelectList(db.Contrato, "IdContrato", "TipoContrato", usuario.IdContrato);
            ViewBag.IdSucursal = new SelectList(db.Sucursal, "IdSucursal", "NombreSucursal", usuario.IdSucursal);
            return View(usuario);
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
                Usuario usuario = db.Usuario.Find(id);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                ViewBag.IdCargo = new SelectList(db.Cargo, "IdCargo", "NombreCargo", usuario.IdCargo);
                ViewBag.IdCiudad = new SelectList(db.Ciudad, "IdCiudad", "NombreCiudad", usuario.IdCiudad);
                ViewBag.IdComuna = new SelectList(db.Comuna, "IdComuna", "NombreComuna", usuario.IdComuna);
                ViewBag.IdContrato = new SelectList(db.Contrato, "IdContrato", "TipoContrato", usuario.IdContrato);
                ViewBag.IdSucursal = new SelectList(db.Sucursal, "IdSucursal", "NombreSucursal", usuario.IdSucursal);
                return View(usuario);
            }else
            {
                return RedirectToAction("../Home/Welcome");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUsuario,Nombre,Apellido,Correo,Contrasena,Telefono,Direccion,IdComuna,IdCiudad,IdSucursal,IdCargo,IdContrato")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailAddress m = new MailAddress(usuario.Correo);
                    db.Entry(usuario).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (System.Exception)
                {
                    ViewBag.IdCargo = new SelectList(db.Cargo, "IdCargo", "NombreCargo", usuario.IdCargo);
                    ViewBag.IdCiudad = new SelectList(db.Ciudad, "IdCiudad", "NombreCiudad", usuario.IdCiudad);
                    ViewBag.IdComuna = new SelectList(db.Comuna, "IdComuna", "NombreComuna", usuario.IdComuna);
                    ViewBag.IdContrato = new SelectList(db.Contrato, "IdContrato", "TipoContrato", usuario.IdContrato);
                    ViewBag.IdSucursal = new SelectList(db.Sucursal, "IdSucursal", "NombreSucursal", usuario.IdSucursal);
                    TempData["error"] = "Correo invalido o ya utilizado por un trabajador";
                    return View();
                }
            }
            ViewBag.IdCargo = new SelectList(db.Cargo, "IdCargo", "NombreCargo", usuario.IdCargo);
            ViewBag.IdCiudad = new SelectList(db.Ciudad, "IdCiudad", "NombreCiudad", usuario.IdCiudad);
            ViewBag.IdComuna = new SelectList(db.Comuna, "IdComuna", "NombreComuna", usuario.IdComuna);
            ViewBag.IdContrato = new SelectList(db.Contrato, "IdContrato", "TipoContrato", usuario.IdContrato);
            ViewBag.IdSucursal = new SelectList(db.Sucursal, "IdSucursal", "NombreSucursal", usuario.IdSucursal);
            return View(usuario);
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
                Usuario usuario = db.Usuario.Find(id);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                return View(usuario);
            }else
            {
                return RedirectToAction("../Home/Welcome");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            db.Usuario.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Show(int? id)
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
                Usuario usuario = db.Usuario.Find(id);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                return View(usuario);
            }else
            {
                return RedirectToAction("../Home/Welcome");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("../Home/Welcome");
        }

    }
}
