using Sualiado.data.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sa_Ventas.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        ProductoDAO ProductoDAO = new ProductoDAO();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Buscar(string campo)
        {
            DataTable busqueda = ProductoDAO.Buscar(campo);
            string temp = "";
            string productosHabilitados = "";
            for (int i = 0; i < busqueda.Rows.Count; i++)
            {

                temp = string.Format("<div class='col -lg-4 col-md-6 mb-4'> " +
                    "<div class='card h-100' style='width:350px; height: 300px;'><a href='#'>" +
                    "<img class='card-img-top img-fluid' height='300' width='300' src='" + busqueda.Rows[i][6] + "' alt=''>" +
                    "</a><div class='card-body'>" +
                    " <h4 class='card-title'>" +
                    "<a class='text-muted' style='font-size:16.5px;' href='#'>" + busqueda.Rows[i][2].ToString() + "</a>" +
                    "</h4></div> " +
                    "<div id='star' class='card-footer'>" +
                    "<h5 class='text-dark'>" + busqueda.Rows[i][3] + "</h5>" +
                    "</div></div></div>");
                productosHabilitados += temp;
            }
            return Json(productosHabilitados, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Mostrar(string mostrar)
        {
            DataTable pro = ProductoDAO.ProductosTop();
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
                     "<span>$ " + pro.Rows[i][3].ToString() + "</span></div></div</div></div></div>");
                productosHabilitados += temp;
            }
            return Json(productosHabilitados, JsonRequestBehavior.AllowGet);
        }
    }
}