using Sualiado.data.Clases;
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
    public class EmpleadoController : Controller
    {
        // GET: Empleado
        PersonaDAO per = new PersonaDAO();
        public ActionResult Empleado()
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
        public ActionResult Agregar(string nombre, string apellido, string genero, string cargo, string cedula, string direccion, string telefono, string correo)
        {
            if (nombre.Equals("") || apellido.Equals("") || genero.Equals("seleccionar") || cargo.Equals("seleccionar") || cedula.Equals("") || direccion.Equals("") || telefono.Equals("") || correo.Equals(""))
            {
                return Json("Todos los campos son obligatorios", JsonRequestBehavior.AllowGet);
            }
            Contraseña contra = new Contraseña();
            string clave = contra.GenerarContraseña();
            string pass = Encryptor.MD5Hash(clave);
            if (genero.ToUpper() != "SELECCIONAR")
            {
                PersonaVO perVO = new PersonaVO(nombre, apellido, direccion, telefono, cargo, cedula, pass, correo, genero);
                if (per.Verificar(perVO.Usuario))
                {
                    return Json("El numero de cedula ya se encuentra en la base de datos", JsonRequestBehavior.AllowGet);
                }
                Correo email = new Correo();
                if (email.email_bien_escrito(correo))
                {
                    if (email.EnviarCorreo(perVO.Nombre, perVO.Apellido, perVO.Correo, perVO.Usuario,clave))
                    {
                        per.AgregarPersona(perVO);
                        return Json("El empleado fue agregado", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("No se pudo agregar al empleado, revisa por favor que el correo sea correcto", JsonRequestBehavior.AllowGet);

                }
            }

            return Json("false", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Buscar(string campo) {
            per.Buscar(campo);
            DataTable personas = per.Buscar(campo); 
            string tablaEsquema = "";
            for (int i = 0; i < personas.Rows.Count; i++)
            {
                string boton = "";
                string p = personas.Rows[i][7].ToString();
                if (p.Equals("Activo"))
                {
                    boton = "<button title='Deshabilitar persona' id=" + personas.Rows[i][0].ToString() + "  value='habilitado'  class=" + "'btn btn-linkEdit btn-sm'" + " onclick='CambiarEstado(this.id,this.value)' ><em class='flaticon-eye'></em></button>";
                }
                else
                {

                    boton = "<button title='Habilitar persona' id=" + personas.Rows[i][0].ToString() + "  value='deshabilitado'  class=" + "'btn btn-linkOcultar btn-sm'" + " onclick='CambiarEstado(this.id,this.value)' ><em class='flaticon-visibility'></em></button>";

                }
                string temp = "<tr><td>" + personas.Rows[i][0].ToString() + "</td>" +
                    "<td>" + personas.Rows[i][1].ToString() + "</td>" +
                    "<td>" + personas.Rows[i][2].ToString() + "</td>" +
                    "<td>" + personas.Rows[i][3].ToString() + "</td>" +
                    "<td>" + personas.Rows[i][4].ToString() + "</td>" +
                    "<td>" + personas.Rows[i][5].ToString() + "</td>" +
                    "<td>" + personas.Rows[i][6].ToString() + "</td>" +
                    "<td>" + personas.Rows[i][7].ToString() + "</td>" +
                    "<td>" + boton + "</td></tr>";
                tablaEsquema += temp;
            }
            return Json(tablaEsquema, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CambiarEstado(string id,string estado){
            if (estado.Equals("habilitado"))
            {
                if (per.CambiarEstado(id, 0))
                {
                    return Json("true", JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json("false", JsonRequestBehavior.AllowGet);
                }
            }
            if (estado.Equals("deshabilitado"))
            {
                if (per.CambiarEstado(id, 1))
                {
                    return Json("true", JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json("false", JsonRequestBehavior.AllowGet);
                }
            }
            return Json("false", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Mostrar(string pagina)
        {
            DataTable personas = per.Personas();
            string tablaEsquema = "";
            for (int i = 0; i < personas.Rows.Count; i++)
            {
                string boton = "";
                string p = personas.Rows[i][7].ToString();
                if (p.Equals("Activo"))
                {
                    boton = "<button title='Deshabilitar persona' id=" + personas.Rows[i][0].ToString() + "  value='habilitado'  class=" + "'btn btn-linkEdit btn-sm'" + " onclick='CambiarEstado(this.id,this.value)' ><em class='flaticon-eye'></em></button>";
                }
                else
                {

                    boton = "<button title='Habilitar persona' id=" + personas.Rows[i][0].ToString() + "  value='deshabilitado'  class=" + "'btn btn-linkOcultar btn-sm'" + " onclick='CambiarEstado(this.id,this.value)' ><em class='flaticon-visibility'></em></button>";

                }
                string temp = "<tr><td>" + personas.Rows[i][0].ToString() + "</td>" +
                    "<td>" + personas.Rows[i][1].ToString() + "</td>" +
                    "<td>" + personas.Rows[i][2].ToString() + "</td>" +
                    "<td>" + personas.Rows[i][3].ToString() + "</td>" +
                    "<td>" + personas.Rows[i][4].ToString() + "</td>" +
                    "<td>" + personas.Rows[i][5].ToString() + "</td>" +
                    "<td>" + personas.Rows[i][6].ToString() + "</td>" +
                    "<td>" + personas.Rows[i][7].ToString() + "</td>" +
                    "<td>" + boton + "</td></tr>";
                tablaEsquema += temp;
            }
            return Json(tablaEsquema, JsonRequestBehavior.AllowGet);
        }
    }
}