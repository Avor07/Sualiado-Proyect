using Sualiado.data.Models.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sualiado.data.Controllers
{
    public class AgendaController : Controller
    {
        // GET: Agenda
        Tarea tarea = new Tarea();
        PersonaDAO per = new PersonaDAO();
        public ActionResult Agenda()
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
        public void CambiarEstado(string id,string estado)
        {
            tarea.CambiarEstado(id,estado);
        }
        public ActionResult MostrarCompletas()
        {
            string tabla = "";
            DataTable tareas = tarea.TareasCompletas();
            for (int i = 0; i < tareas.Rows.Count; i++)
            {
                string temp = "<li class='todo-list-item'><div class='checkbox'><label for='checkbox-1'>" + tareas.Rows[i][1] + " - " + tareas.Rows[i][2] + "</label></div></li>";
                tabla += temp;
            }
            return Json(tabla, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MostrarIncompletas() {
            string tabla = "";
            DataTable tareas = tarea.TareasIncompletas();
            for (int i = 0; i < tareas.Rows.Count; i++)
            {
                string temp = "<li class='todo-list-item'><div class='checkbox'><label for='checkbox-1'>"+tareas.Rows[i][1]+" - "+tareas.Rows[i][2]+"</label></div></li>";
                tabla += temp;
            }
            return Json(tabla, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MostrarEmpleados()
        {
            DataTable personas=per.Personas();
            string options = "";
            for (int i = 0; i < personas.Rows.Count; i++)
            {
                if (personas.Rows[i][7].ToString().Contains("Activo"))
                {
                string temp = "<option value='"+personas.Rows[i][0].ToString()+"'>"+personas.Rows[i][1].ToString()+" "+personas.Rows[i][2].ToString() + "<option>";
                options += temp;

                }
            }   
            return Json(options, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Agregar(string descripcion,int persona)
        {
            if (tarea.AgregarTarea(descripcion, persona, 0))
            {
            return Json("Tarea agregada correctamente", JsonRequestBehavior.AllowGet);
            }
            return Json("No se pudo agregar la tarea", JsonRequestBehavior.AllowGet);
        }
    }
}