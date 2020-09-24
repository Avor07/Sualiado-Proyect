using Sualiado.data.Models.DAO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sualiado.data.Controllers
{
    public class ReportesController : Controller
    {
        // GET: Reportes

        public ActionResult Reportes()
        {
            if (Session["rol"] != null)
            {
                string usu = Session["id"].ToString();
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

                return RedirectToAction("Login", "Login");
            }
            return View();
        } 
       
        Reportes rep = new Reportes();
        [HttpPost]
        public ActionResult Reportes(string funcion) {
            DataTable reporte= rep.VentasPorMes();
            String[] meses = new string[reporte.Rows.Count];
            String[] ganancia = new string[reporte.Rows.Count];
            for (int i = 0; i < reporte.Rows.Count; i++)
            {
                meses[i] = reporte.Rows[i][1].ToString();
                ganancia[i] = reporte.Rows[i][0].ToString();
            }
            object[] grafica=new object[4];
            grafica[0] = meses; 
            grafica[1] = ganancia;
            DataTable productos = rep.ProductosMasVendidos();
            String[] cantidad = new string[productos.Rows.Count];
            String[] producto = new string[productos.Rows.Count];
            for (int i = 0; i < productos.Rows.Count; i++)
            {
                cantidad[i] = productos.Rows[i][1].ToString();
                producto[i] = productos.Rows[i][0].ToString();
            }
            grafica[2] = cantidad;
            grafica[3] = producto;
            return Json(grafica, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Productos(string reporte)
        {
            DataTable agotado = rep.ProductosAgotados();
            DataTable proximo = rep.ProductosProAgotar();
            object[] productos = new object[3];

            String[] productoA = new string[agotado.Rows.Count];
            String[] productoP = new string[proximo.Rows.Count];
            String[] cantidad = new string[proximo.Rows.Count];
            for (int i = 0; i < agotado.Rows.Count; i++)
            {
                productoA[i] = agotado.Rows[i][2].ToString();
          
            }
            for (int i = 0; i < proximo.Rows.Count; i++)
            {
                productoP[i] = proximo.Rows[i][2].ToString();
                cantidad[i] = proximo.Rows[i][4].ToString();

            }
            productos[0] = productoA;
            productos[1] = productoP;
            productos[2] = cantidad;
            return Json(productos, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Cambios(string funcion) {
            DataTable cambios = rep.CambioEnPrecios();
            string columnas = "";
               
                for (int j = 0; j < cambios.Rows.Count; j++)
                {
                    string temp= string.Format("<tr><td>" + cambios.Rows[j][1].ToString() +"</td>" +
                        "<td>"+cambios.Rows[j][2].ToString()+"</td><td>"+cambios.Rows[j][3].ToString()+"</td>" +
                        "<td>"+cambios.Rows[j][4].ToString()+ "</td>" +
                        "<td>" + cambios.Rows[j][5].ToString() + "</td></tr>");
                    columnas+= temp;
                }
            
            return Json(columnas, JsonRequestBehavior.AllowGet);
        }

    }
}