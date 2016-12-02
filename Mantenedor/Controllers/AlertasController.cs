using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mantenedor.Controllers
{
    public class AlertasController : Controller
    {
        //
        // GET: /Alertas/

        public ActionResult CorreoExistente()
        {
            return View();
        }

        public ActionResult CategoriaExistente()
        {
            return View();
        }
    }
}
