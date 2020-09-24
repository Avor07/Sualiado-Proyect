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
    public class ProveedorController : Controller
    {
        ProveedorDAO proDAO = new ProveedorDAO();
        // GET: Proveedor
        public ActionResult Proveedores()
        {
            if (Session["rol"] != null)
            {
                if (int.Parse(Session["rol"].ToString()) == 2 || int.Parse(Session["rol"].ToString()) == 1)
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
        public ActionResult Proveedores(string cantidad)
        {
            DataTable pro =proDAO.MostrarProveedores();
            string tablaEsquema = "";
            for (int i = 0; i < pro.Rows.Count; i++)
            {
                string temp = string.Format("<tr><td>" + pro.Rows[i][1].ToString() + "</td><td>" + pro.Rows[i][2].ToString() + "</td><td>" + pro.Rows[i][3].ToString() + "</td><td>" + pro.Rows[i][4].ToString() + "</td><td><a data-toggle='modal' data-target='#editar'  id=" + pro.Rows[i][0].ToString() + "  class=" + "'btn btn-linkEdit'" + " onclick='EditarProveedor(this.id)'><em class='flaticon-list'></em></td></</tr>");
                tablaEsquema += temp;
            }
            return Json(tablaEsquema, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Buscar(string campo)
        {
            DataTable pro = proDAO.BuscarProveedores(campo);
            string tablaEsquema = "";
            for (int i = 0; i < pro.Rows.Count; i++)
            {
                string temp = string.Format("<tr><td>" + pro.Rows[i][1].ToString() + "</td><td>" + pro.Rows[i][2].ToString() + "</td><td>" + pro.Rows[i][3].ToString() + "</td><td>" + pro.Rows[i][4].ToString() + "</td><td><a data-toggle='modal' data-target='#editar'  id=" + pro.Rows[i][0].ToString() + "  class=" + "'btn btn-linkEdit'" + " onclick='EditarProveedor(this.id)' ><em class='flaticon-list'></em></td></</tr>");
                tablaEsquema += temp;
            }
            return Json(tablaEsquema, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Actualizar(int id,string nombre, string contacto, string telefono, string correo)
        {
            ProveedorVO proVO = new ProveedorVO(id,nombre, contacto, telefono, correo);
            if (proDAO.ActualizarProveedor(proVO))
            {
                return Json("true", JsonRequestBehavior.AllowGet);
            }
            return Json("false", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Agregar(string nombre,string contacto,string telefono,string correo)
        {
            ProveedorVO proVO = new ProveedorVO(0,nombre,contacto,telefono,correo);
            if (proDAO.AgregarProveedor(proVO))
            {
                return Json("true", JsonRequestBehavior.AllowGet);
            }
            
            return Json("false", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Editar(int id)
        {
            DataTable pro =proDAO.TraerProveedor(id);
            string[] informacion = new string[5];
            for (int i = 0; i < 5; i++)
            {
                informacion[i] = pro.Rows[0][i].ToString(); 
            }

            return Json(informacion, JsonRequestBehavior.AllowGet);
        }
    }
}