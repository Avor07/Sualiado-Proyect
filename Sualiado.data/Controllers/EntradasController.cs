using Sualiado.data.DAO;
using Sualiado.data.Models.DAO;
using Sualiado.data.Models.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sualiado.data.Controllers
{
    public class EntradasController : Controller
    {
        // GET: Entradas
            EntradaDAO entDAO = new EntradaDAO();
        public ActionResult Entrada()
        {
            if (Session["rol"] != null)
            {
                if (int.Parse(Session["rol"].ToString()) == 1)
                {
                    ViewBag.perfile = Session["perfile"].ToString();
                    ViewBag.nombre = Session["nom"].ToString();
                    ViewBag.rol = int.Parse(Session["rol"].ToString());
                }
                else
                {
                    return RedirectToAction("Inicio", "Inicio");
                }
            }
            else
            {

                return RedirectToActionPermanent("Login", "Login");
            }
            return View();
        }


        [HttpPost]
        public ActionResult AgregarEntrada(string proveedor, string producto, string cantidad, string precioC)
        {
            DateTime thisDay = DateTime.Today;
            string fecha=thisDay.ToString();
            EntradaVO entrada = new EntradaVO(proveedor,producto,cantidad,Session["id"].ToString(), precioC,fecha);
            if (entDAO.AgregarEntrada(entrada))
            {
                return Json("true", JsonRequestBehavior.AllowGet);
            }
            return Json("hola", JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditarEntrada(string campo)
        {
            ProductoDAO pro = new ProductoDAO();
            ProveedorDAO provee = new ProveedorDAO();
            DataTable proveedores=provee.MostrarProveedores(campo);
            string[] pv = new string[proveedores.Rows.Count];
            for (int i = 0; i < pv.Length; i++)
            {
                pv[i]= proveedores.Rows[i][0].ToString();
            }
            DataTable produc = pro.Productos(campo);
            string[] producto = new string[produc.Columns.Count];
            for (int i = 0; i < producto.Length; i++)
            {
                producto[i] = produc.Rows[0][i].ToString();
            }
            DateTime thisDay = DateTime.Today;
            object[] respuesta = new object[3];
            respuesta[0] = thisDay.ToString();
            respuesta[1] = producto;
            respuesta[2] = pv;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MostrarEntrada(string mostrar)
        {
            DataTable pro = entDAO.MostrarEntrada();
            string tablaEsquema = "";
            for (int i = 0; i < pro.Rows.Count; i++)
            {
                string temp = string.Format("<tr><td>" + pro.Rows[i][2].ToString() + "</td><td>" + pro.Rows[i][1].ToString() + "</td><td>" + pro.Rows[i][3].ToString() + "</td><td>" + pro.Rows[i][5].ToString() + "</td><td>" + pro.Rows[i][4].ToString() + "</td><td>" + pro.Rows[i][6].ToString() + "</td></</tr>");
                tablaEsquema += temp;
            }
            return Json(tablaEsquema, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Buscar(DateTime mostrar)
        {
            DataTable pro = entDAO.MostrarEntrada(mostrar);
            string tablaEsquema = "";
            for (int i = 0; i < pro.Rows.Count; i++)
            {
                string temp = string.Format("<tr><td>" + pro.Rows[i][2].ToString() + "</td><td>" + pro.Rows[i][1].ToString() + "</td><td>" + pro.Rows[i][3].ToString() + "</td><td>" + pro.Rows[i][5].ToString() + "</td><td>" + pro.Rows[i][4].ToString() + "</td><td>" + pro.Rows[i][6].ToString() + "</td></</tr>");
                tablaEsquema += temp;
            }
            return Json(tablaEsquema, JsonRequestBehavior.AllowGet);
        }
    }
}