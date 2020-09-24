using Sualiado.data.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sualiado.data.Controllers
{
    public class TiendaController : Controller
    {
        // GET: Tienda
        public ActionResult Tienda()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Mostrar(string pagina)
        {
            ProductoDAO productoDAO = new ProductoDAO();
            DataTable pro = productoDAO.Productos();
            string temp = "";
            string productosHabilitados = "";
            for (int i = 0; i < pro.Rows.Count; i++)
            {

                temp = string.Format("<div class='col-xl-4 col-lg-4 col-md-6 col-sm-6'>" +
                     "<div class='single-popular-items mb-50 text-center'>" +
                     "<div class='popular-img'>" +
                     "<img src = '" + pro.Rows[i][6].ToString() + "' alt=''>" +
                     "<div class='favorit-items'>" +
                     "<span class='flaticon-heart'></span>" +
                     "</div></div>" +
                     "<div class='popular-caption'>" +
                     "<h3>" + pro.Rows[i][2].ToString() + "</a></h3>" +
                     "<span>$ " + pro.Rows[i][3].ToString() + "</span></div></div</div></div></div></div></div>");
                productosHabilitados += temp;
            }
            return Json(productosHabilitados, JsonRequestBehavior.AllowGet);
        }
    }
}