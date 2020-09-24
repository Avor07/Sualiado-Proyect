using Sualiado.data.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Data;
using Sualiado.data.Models.DAO;

namespace Sualiado.data.Controllers
{
    public class InicioController : Controller
    {
        // GET: Inicio
        public ActionResult Inicio()
        {
            if (Session["rol"] != null)
            {
                ViewBag.perfile = Session["perfile"].ToString();

                ViewBag.nombre = Session["nom"].ToString();
                ViewBag.rol = int.Parse(Session["rol"].ToString());

            }
            else
            {

                return RedirectToActionPermanent("Login", "Login");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Mostrar()
        {
            Reportes rep = new Reportes();
            //DataTable agotado = rep.ProductosAgotados();
            //DataTable proximo = rep.ProductosProAgotar();
            //string recordatorios = "";
            /*if (agotado.Rows.Count != 0)
            {
                recordatorios = "agotados";
            }
            if (proximo.Rows.Count != 0)
            {
                recordatorios += "proximos";

            }*/
            Tarea tarea = new Tarea();
            string[] datos = new string[3];
            string tabla = "";
            DataTable tareas = tarea.Tareas(Session["id"].ToString());
            for (int i = 0; i < tareas.Rows.Count; i++)
            {
                string temp = "<li class='todo-list-item'><div class='checkbox'><input onchange='CambiarEstTarea(this.id)' type='checkbox' value=0 id='" + tareas.Rows[i][0] + "'/><label for='checkbox-1'>" + tareas.Rows[i][1] + " - " + tareas.Rows[i][2] + "</label></div></li>";
                tabla += temp;
            }
            return Json(tabla, JsonRequestBehavior.AllowGet);
        }
    }
}