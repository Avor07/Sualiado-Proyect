using Sualiado.data.Clases;
using Sualiado.data.Models.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sualiado.data.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            if (Session["rol"] != null)
            {
                return RedirectToAction("Inicio", "Inicio");
            }
            return View();
        }
        PersonaDAO per = new PersonaDAO();
        [HttpPost]
        public ActionResult Login(string usu, string pass)
        {
            if (usu == "" || pass == "")
            {

                return Json("Usuario y contraseña son obligatorios", JsonRequestBehavior.AllowGet);
            }
            else
            {
                string contra = Encryptor.MD5Hash(pass);

                if (per.Verificar(usu, contra))
                {
                    Session["id"] = per.ObtenerId(usu);
                    DataTable info = per.TraerInformacion(Session["id"].ToString());
                    Session["gen"] = per.TraerGenero(usu);
                    Session["rol"] = per.ObtenerRol(usu);
                    Session["nom"] = per.TraerNombre(usu);
                    Session["info"] = info;
                    Session["perfile"] = info.Rows[0][9].ToString();
                    return Json("TRUE", JsonRequestBehavior.AllowGet);

                }
            }
            return Json("Usuario y/o contraseña incorrectos", JsonRequestBehavior.AllowGet);

        }
        public ActionResult ConfirmarContraseña(string contraseña, string confirmacion)
        {
            if (contraseña == confirmacion)
            {
                Contraseña contra = new Contraseña();
                string pass = Encryptor.MD5Hash(contraseña);
                per.recuperarContraseña(pass, Session["usuario"].ToString());
                Session.Contents.RemoveAll();
                return Json("/Login/Login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Las contraseñas deben ser iguales", JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult ConfirmarCodigo(string codigo)
        {
            if (codigo == Session["codigo"].ToString())
            {
                string html = "<h3>Recuperacion de la contrasaeña</h3>" +
                "<div class='row contact_form' action='#'  novalidate='novalidate'>" +
                "<div class='col-md-12 form-group p_star'>" +
                "<input type = 'password' class='form-control' id='contraseñaNueva' name='name' value=''" +
                "placeholder='Nueva contraseña'>" +
                "</div><div class='col-md-12 form-group p_star'>" +
                "<input type = 'password' class='form-control' id='confirmacion' name='name' value=''" +
                "placeholder='Confirmar contraseña'></div><div class='col-md-12 form-group p_star'>" +
                "<label id='mensage'></label>" +
                "<button type ='submit' value ='submit' onclick = 'ConfirmarContraseña()' class='btn_3'>" +
                "Cambiar contraseña</button></div>";
                return Json(html, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("El codigo es erroneo", JsonRequestBehavior.AllowGet);
            }

            //return Json("false", JsonRequestBehavior.AllowGet);
        }
        public ActionResult EnviarCorreo(string usuario)
        {
            Session["usuario"] = usuario;
            if (per.TraerInfo(usuario).Rows.Count != 0)
            {
                string html = "<h3>Te enviamos un correo <br>Por favor ingresa tu codigo</h3>" +
                "<div class='row contact_form' action='#'  novalidate='novalidate'>" +
                "<div class='col-md-12 form-group p_star'>" +
                "<input type = 'number' class='form-control' id='codigo' name='name' value=''" +
                "placeholder='Codigo'></div><div class='col-md-12 form-group p_star'>" +
                "<label id='mensage'></label>" +
                "<button type ='submit' value ='submit' onclick = 'Confirmar()' class='btn_3'>" +
                "Confirmar codigo</button></div>";
                Contraseña contra = new Contraseña();
                string clave = contra.GenerarCodigo();
                Session["codigo"] = clave;
                Correo correo = new Correo();
                correo.CorreoRecuperacion(usuario, clave);
                return Json(html, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("El usuario que ingresaste no existe", JsonRequestBehavior.AllowGet);
            }

            //return Json("false", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Recuperacion(string usuario)
        {
            string html = "<h3>Recuperacion de contraseña! <br>Por favor ingresa tu cedula</h3>" +
                "<div class='row contact_form' action='#'  novalidate='novalidate'>" +
                "<div class='col-md-12 form-group p_star'>" +
                "<input type = 'text' class='form-control' id='usu' name='name' value=''" +
                "placeholder='Cedula'></div><div class='col-md-12 form-group p_star'>" +
                "<label id='mensage'></label>" +
                "<button type ='submit' value ='submit' onclick = 'Codigo()' class='btn_3'>" +
                "Enviar Codigo</button></div>";

            return Json(html, JsonRequestBehavior.AllowGet);
        }
    }
}