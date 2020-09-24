using Sualiado.data.Clases;
using Sualiado.data.DAO;
using Sualiado.data.Models.DAO;
using Sualiado.data.Models.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sualiado.data.Controllers
{
    public class DañosController : Controller
    {
        // GET: Daños
        
        

        public ActionResult ReporteDaños()
        {

            if (Session["rol"] != null)
            {
                if (int.Parse(Session["rol"].ToString()) == 3 || int.Parse(Session["rol"].ToString()) == 1)
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
        public ActionResult Mostrar(string cantidad)
        {
            ProductoDAO producto = new ProductoDAO();
            string productosHabilitados = "";
            string temp = "";
            DataTable pro = producto.Productos();

            for (int i = 0; i < pro.Rows.Count; i++)
            {

                temp = string.Format("<tr><td><div style='width:100px; height:100px;'><img class='{0}' id=" + "'tablaProductos'" + " src ='{1}'/></div></td>" +
                    "<td>" + pro.Rows[i][1].ToString() + "</td>" +
                    "<td>" + pro.Rows[i][2].ToString() + "</td>" +
                    "<td><a title='Reportar' data-toggle='modal' data-target='#reporte'  id=" + pro.Rows[i][1].ToString() + "  class=" + "'btn btn-outline-secondary btn-sm'" + " onclick='MostrarCampos(this)'><em class='flaticon-report'></em></td></tr>", "img-responsive", pro.Rows[i][6].ToString(), "Editar(this)");
                productosHabilitados += temp;
            }
            return Json(productosHabilitados, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TraerCampos(string referencia)
        {
            ProductoDAO producto = new ProductoDAO();
            DañoDAO dañ = new DañoDAO();
            String[] produc = producto.InformacionProducto(referencia);
            DataTable tipos = dañ.TraerTipos();
            String[] id = new String[tipos.Rows.Count];
            String[] descripcion = new String[tipos.Rows.Count];
            for (int i = 0; i < id.Length; i++)
            {
                id[i] = tipos.Rows[i][0].ToString();
                descripcion[i] = tipos.Rows[i][1].ToString();
            }
            Object[] respuesta = new Object[3];
            respuesta[0] = produc;
            respuesta[1] = id;
            respuesta[2] = descripcion;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Reportar(string referencia, string tipo, string observacion, string cantidad)
        {
            DañoDAO dañ = new DañoDAO();
            DañoVO dañoVO = new DañoVO(referencia, tipo, cantidad, Session["id"].ToString(), observacion);
            if (dañ.ReportarDaño(dañoVO))
            {
                return Json("TRUE", JsonRequestBehavior.AllowGet);

            }
            return Json("FALSE", JsonRequestBehavior.AllowGet);

        }
        public ActionResult MostrarReporte(string referencia)
        {
            DañoDAO dañ = new DañoDAO();
            DataTable reportes=dañ.MostrarReportes();
            string[,] respuesta = new string[reportes.Columns.Count,reportes.Rows.Count];
            for (int i = 0; i < reportes.Columns.Count; i++)
            {
                for (int j = 0; j < reportes.Rows.Count; j++)
                {
                    respuesta[i,j] = reportes.Rows[j][i].ToString();
                }
            }
            string descripcion= "";
            for (int i = 0; i < reportes.Rows.Count; i++)
            {
            string temp = string.Format("<tr>" +
                "<td>" + reportes.Rows[i][1].ToString() + "</td>" +
                   "<td>" + reportes.Rows[i][2].ToString() + "</td>" +
                   "<td>" + reportes.Rows[i][3].ToString() + "</td>" +
                   "<td>" + reportes.Rows[i][4].ToString() + "</td>" +
                   "<td>" + reportes.Rows[i][5].ToString() + "</td>");
           descripcion += temp;

            }
            string respuesta1= " <table id='mitabla1' class='table table-responsive-lg'>" +
                "<thead><tr><th scope='col'>Unidades desechadas</th>" +
                "<th scope='col'>Tipo de daño</th>" +
                "<th scope='col'>Referencia</th>" +
                "<th scope='col'>Persona</th>" +
                "<th scope='col'>Observacion</th>" +
                "</tr></thead><tbody>" + descripcion+ "</tbody></table>";
            return Json(respuesta1, JsonRequestBehavior.AllowGet);

        }
    }
}
