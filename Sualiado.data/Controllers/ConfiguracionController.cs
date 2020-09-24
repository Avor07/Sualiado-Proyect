using Sualiado.data.Clases;
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
    public class ConfiguracionController : Controller
    {
        // GET: Configuracion
        PersonaDAO per = new PersonaDAO();
        public ActionResult Configuracion()
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
            DataTable info = per.TraerInformacion(Session["id"].ToString());
            String[] datos = new string[info.Columns.Count];
            for (int i = 0; i < info.Columns.Count; i++)
            {
                datos[i] = info.Rows[0][i].ToString();
            }
            return Json(datos, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CambiarFoto(HttpPostedFileBase img)
        {
            PersonaDAO personaDAO = new PersonaDAO();
            string ruta="";
            if (img !=null)
            {
                ruta = Server.MapPath("~/perfile/");
                ruta += img.FileName;
                img.SaveAs(ruta);
                personaDAO.SubirImagen("/perfile/" + img.FileName, Session["id"].ToString());
                Session["perfile"] = "/perfile/"+img.FileName;
                return Json("true", JsonRequestBehavior.AllowGet);
            }
            return Json("false", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Editar(string nombre, string apellido, string direccion, string telefono, string usuario, string correo, string genero)
        {
            PersonaVO persona = new PersonaVO(nombre, apellido, direccion, telefono, Session["id"].ToString(), usuario, null, correo, genero);
            if (per.Editar(persona))
            {
                Session["nom"] = nombre;
                return Json("TRUE", JsonRequestBehavior.AllowGet);
            }
            return Json("FALSE", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Contraseña(string contraseña, string nueva, string confirmacion)
        {
            string[] respuesta = new string[2];
            DataTable info = per.TraerInformacion(Session["id"].ToString());
            string clave = Encryptor.MD5Hash(contraseña);
            if (info.Rows[0][8].ToString().Equals(clave))
            {
                if (nueva.Equals(confirmacion))
                {
                    clave = Encryptor.MD5Hash(nueva);
                    if (per.CambiarContraseña(clave, Session["id"].ToString()))
                    {
                        respuesta[0] = "success";
                        respuesta[1] = "se actualizo correctamente tu contraseña";
                        return Json(respuesta, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    respuesta[0] = "error";
                    respuesta[1] = "las contraseñas no son iguales";
                    return Json(respuesta, JsonRequestBehavior.AllowGet);
                }

            }
            else {
                respuesta[0] = "error";
                respuesta[1] = "la contraseña actual es incorrecta";
                return Json(respuesta, JsonRequestBehavior.AllowGet);
            }
          
            return Json("FALSE", JsonRequestBehavior.AllowGet);
        }
    }
}