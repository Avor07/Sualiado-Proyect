using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sualiado.data.Controllers
{
    public class CerrarController : Controller
    {
        // GET: Cerrar

        public ActionResult Cerrar()
        {
            Session.Contents.RemoveAll();
            return RedirectToAction("Index", "Home");
        }
    }
}