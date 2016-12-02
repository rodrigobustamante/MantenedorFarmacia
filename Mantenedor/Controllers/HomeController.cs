using Mantenedor.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Mantenedor.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuario user)
        {
            if (ModelState.IsValid)
            {
                Usuario miUser = user.Existe(user);
                if (miUser != null)
                {
                    Session["correo"] = miUser.Correo;
                    Session["nombre"] = miUser.Nombre;
                    Session["apellido"] = miUser.Apellido;
                    Session["cargo"] = miUser.IdCargo;
                    return RedirectToAction("Welcome");
                }else{
                    ModelState.AddModelError("error", "Usuario o contraseña incorrecta");
                }
            }
            return View();
        }

        public ActionResult Welcome()
        {
            if (Session["correo"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
            if (Session["correo"] == null)
            {
                return RedirectToAction("Login"); ;
            }else{
                FormsAuthentication.SignOut();
                Session.Abandon();
                return RedirectToAction("Login");
            }
        }
    }
}
