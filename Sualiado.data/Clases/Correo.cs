using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Mime;
using Sualiado.data.Models.DAO;
using System.Data;
using System.Text.RegularExpressions;

namespace Sualiado.data.Clases
{
    public class Correo
    {
        public Boolean email_bien_escrito(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool EnviarCorreo(string nombre, string ape, string mail, string usuario, string contra)
        {

            MailMessage correo = new MailMessage();
            correo.To.Add(new MailAddress(mail));
            correo.From = new MailAddress("gestorsualiado@gmail.com");
            correo.Subject = "Sualiado";
            //System.Net.Mail.Attachment attachment;
            //attachment = new System.Net.Mail.Attachment("C:/Users/ortiz/Documents/Sualiado/Sa_Ventas/Content/img/bienvenido.png");
            string html = "Buen día Sr(a) " + nombre + " " + ape + "<br/>" +
                " Le notificamos que su cuenta ya ha sido creada y cuando desee puede cambiarle la clave" +
                "<p>Usuario: " + usuario + "</p>"
                + "<p>Contraseña: " + contra + "</p>" +
               "<img  src='cid:imagen' />";

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8, MediaTypeNames.Text.Html);
            LinkedResource img = new LinkedResource(@"C:/Users/ortiz/Documents/Sualiado/Sa_Ventas/Content/img/bienvenido.png", MediaTypeNames.Image.Jpeg);
            img.ContentId = "imagen";
            htmlView.LinkedResources.Add(img);
            correo.AlternateViews.Add(htmlView);
            //correo.Attachments.Add(attachment);
            correo.IsBodyHtml = true;
            bool verificar = false;
            try
            {
                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "gestorsualiado@gmail.com",
                        Password = "0708sabato"
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Send(correo);

                }
                verificar = true;

            }
            catch (Exception)
            {

                verificar = false;
                throw;
            }

            return verificar;

        }
        public bool CorreoRecuperacion(string usuario,string codigo)
        {
            
            PersonaDAO per = new PersonaDAO();
            DataTable info = per.TraerInfo(usuario);
            MailMessage correo = new MailMessage();
            correo.To.Add(new MailAddress(info.Rows[0][6].ToString()));
            correo.From = new MailAddress("gestorsualiado@gmail.com");
            correo.Subject = "Cambio de contraseña";
            string html = "Buen día Sr(a) "+info.Rows[0][1].ToString()+",se ha solicitado un cambio de contraseña, el siguiente codigo es para verificar que si eres tu "+codigo;
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8, MediaTypeNames.Text.Html);
            correo.AlternateViews.Add(htmlView);
            correo.IsBodyHtml = true;
            bool verificar = false;
            try
            {
                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "gestorsualiado@gmail.com",
                        Password = "0708sabato"
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Send(correo);

                }
                verificar = true;

            }
            catch (Exception)
            {

                verificar = false;
                throw;
            }

            return verificar;

        }
    }
}