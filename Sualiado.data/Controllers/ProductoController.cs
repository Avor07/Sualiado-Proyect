using Sualiado.data.DAO;
using Sualiado.data.Models.DAO;
using Sualiado.data.Models.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Sualiado.data.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        ProductoDAO productoDAO = new ProductoDAO();
        DañoDAO dañ = new DañoDAO();
        public ActionResult Inventario()
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
        public ActionResult AgregarProducto(HttpPostedFileBase img, string refe, string nombre, string precio, string precioCompra, string tipo, string proveedor)
        {
            string ruta = "";
            if (img != null && refe != "" && nombre != "" && precio != "" && precioCompra != "" && tipo != "" && proveedor != "")
            {

                int valor = 0;
                bool comprobacion = int.TryParse(precio, out valor);
                bool comprobacion1 = int.TryParse(precioCompra, out valor);
                if (comprobacion == true && comprobacion1 == true)
                {
                    ProductoVO proVo = new ProductoVO(nombre, refe, precio, null, tipo, null, proveedor, precioCompra);
                    if (productoDAO.AgregarProducto(proVo))
                    {
                        ruta = Server.MapPath("~/Imagenes/");
                        ruta += img.FileName;
                        img.SaveAs(ruta);
                        productoDAO.SubirImagen("/Imagenes/" + img.FileName, refe);
                        return Json("true", JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {

                    return Json("Los campos precio y precio de compra no pueden contener letras", JsonRequestBehavior.AllowGet);
                }



            }
            else
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }

            return Json("llegue aqui pero no hice nada", JsonRequestBehavior.AllowGet);

        }
        public ActionResult Estado(int cambio, string referencia)
        {
            if (cambio == 0)
            {
                if (productoDAO.CambiarEstado(referencia, cambio))
                {
                    return Json("deshabilitado", JsonRequestBehavior.AllowGet);

                }
            }
            else
            {
                if (productoDAO.CambiarEstado(referencia, cambio))
                {
                    return Json("habilitado", JsonRequestBehavior.AllowGet);

                }

            }
            return Json("false", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Actualizar(string refe, string nombre, string precio, string tipo, string proveedor, HttpPostedFileBase img)
        {
            int precioCompra = int.Parse(productoDAO.MostrarDatos(refe).Rows[0][6].ToString());
            if (int.Parse(precio) < precioCompra)
            {
                return Json("falseP", JsonRequestBehavior.AllowGet);

            }
            if (refe == "" || nombre == "" || precio == "" || tipo == "" || proveedor == "")
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
            else
            {
                string ruta = "";
                if (img != null)
                {
                    ruta = Server.MapPath("~/Imagenes/");
                    ruta += img.FileName;
                    img.SaveAs(ruta);
                    productoDAO.SubirImagen("/Imagenes/" + img.FileName, refe);


                }
                ProductoVO proVo = new ProductoVO(nombre, refe, precio, null, tipo, null, proveedor, null);
                productoDAO.ActualizarProducto(proVo, int.Parse(Session["id"].ToString()));
                return Json("true", JsonRequestBehavior.AllowGet);
            }


            //return Json("true", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Editar(string referencia)
        {
            string[] informacion = productoDAO.InformacionProducto(referencia);
            DataTable tipos = productoDAO.TraerTipos();
            string[] tipo = new string[tipos.Rows.Count];
            string[] valor = new string[tipos.Rows.Count];
            for (int i = 0; i < tipos.Rows.Count; i++)
            {
                tipo[i] = tipos.Rows[i][1].ToString();
                valor[i] = tipos.Rows[i][0].ToString();
            }
            ProveedorDAO pro = new ProveedorDAO();
            DataTable proveedores = pro.MostrarProveedores();
            string[] proveedor = new string[proveedores.Rows.Count];
            for (int i = 0; i < proveedores.Rows.Count; i++)
            {
                proveedor[i] = proveedores.Rows[i][1].ToString();
            }
            Object[] resultado = new object[4];
            resultado[0] = informacion;
            resultado[1] = tipo;
            resultado[2] = valor;
            resultado[3] = proveedor;
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Buscar(string campo)
        {
            ProductoDAO producto = new ProductoDAO();
            string TablaEsquema = "";
            string productosHabilitados = "";
            string productosDesHabilitados = "";
            string temp = "";
            DataTable pro = producto.Productos(campo);
            DataTable proD = producto.ProductosDeshabilitados(campo);

            for (int i = 0; i < pro.Rows.Count; i++)
            {

                temp = string.Format("<tr><td><div><img class='{0}' id=" + "'tablaProductos'" + " src ='{1}'/></div></td>" +
                    "<td>" + pro.Rows[i][1].ToString() + "</td>" +
                    "<td>" + pro.Rows[i][2].ToString() + "</td>" +
                    "<td>" + pro.Rows[i][3].ToString() + "</td>" +
                    "<td>" + pro.Rows[i][8].ToString() + "</td>" +
                    "<td><a data-toggle='modal' data-target='#editar'  id=" + pro.Rows[i][1].ToString() + "  class=" + "'btn btn-linkEdit btn-md'" + " onclick='Editar(this)' ><em class='flaticon-list'>&nbsp;</em></a>" +
                    "<button title='Deshabilitar producto' id=" + pro.Rows[i][1].ToString() + "  value='habilitado'  class=" + "'btn btn-linkOcultar btn-md'" + " onclick='{3}' ><em class='flaticon-visibility'>&nbsp;</em> </button>" +
                    "<a data-toggle='modal' data-target='#entrada'  id=" + pro.Rows[i][1].ToString() + "  class=" + "'btn btn-linkComp btn-md'" + " onclick='AgregarEntrada(this)'><em class='flaticon-shopping-cart'>&nbsp;</em>  </a></td></tr>", "img-responsive2", pro.Rows[i][6].ToString(), "Editar(this)", "Estado(this)");
                productosHabilitados += temp;
            }
            for (int i = 0; i < proD.Rows.Count; i++)
            {
                temp = string.Format("<tr><td><div><img class='{0}' id=" + "'tablaProductos'" + " src ='{1}'/></div></td>" +
                   "<td>" + proD.Rows[i][1].ToString() + "</td><td>" + proD.Rows[i][2].ToString() + "</td>" +
                   "<td>" + proD.Rows[i][3].ToString() + "</td><td>" + proD.Rows[i][8].ToString() + "</td>" +
                   "<td><a data-toggle='modal' data-target='#editar'  id=" + proD.Rows[i][1].ToString() + "  class=" + "'btn btn-linkEdit btn-md'" + " onclick='Editar(this)' > <em class='flaticon-list'>&nbsp;</em></a>" +
                   "<button tittle='Habilitar producto' id=" + proD.Rows[i][1].ToString() + " value='deshabilitado'  class=" + "'btn btn-linkVisi btn-md'" + " onclick='{3}' ><em class='flaticon-eye'>&nbsp;</em> </button></td>" +
                   "</tr>", "img-responsive2", proD.Rows[i][6].ToString(), "Editar(this)", "Estado(this)");
                productosDesHabilitados += temp;

            }
            TablaEsquema = "" + productosHabilitados + productosDesHabilitados;
            return Json(TablaEsquema, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Inventario(string cantidad)
        {
            ProductoDAO producto = new ProductoDAO();
            string TablaEsquema = "";
            string productosHabilitados = "";
            string productosDesHabilitados = "";
            string temp = "";
            DataTable pro = producto.Productos();
            DataTable proD = producto.ProductosDeshabilitados();

            for (int i = 0; i < pro.Rows.Count; i++)
            {

                temp = string.Format("<tr><td><div><img class='{0}' id=" + "'tablaProductos'" + " src ='{1}'/></div></td>" +
                   "<td>" + pro.Rows[i][1].ToString() + "</td>" +
                   "<td>" + pro.Rows[i][2].ToString() + "</td>" +
                   "<td>" + pro.Rows[i][3].ToString() + "</td>" +
                   "<td>" + pro.Rows[i][8].ToString() + "</td>" +
                   "<td><a data-toggle='modal' data-target='#editar'  id=" + pro.Rows[i][1].ToString() + "  class=" + "'btn btn-linkEdit btn-md'" + " onclick='Editar(this)' ><em class='flaticon-list'>&nbsp;</em></a>" +
                   "<button title='Deshabilitar producto' id=" + pro.Rows[i][1].ToString() + "  value='habilitado'  class=" + "'btn btn-linkOcultar btn-md'" + " onclick='{3}' ><em class='flaticon-visibility'>&nbsp;</em> </button>" +
                   "<a data-toggle='modal' data-target='#entrada'  id=" + pro.Rows[i][1].ToString() + "  class=" + "'btn btn-linkComp btn-md'" + " onclick='AgregarEntrada(this)'><em class='flaticon-shopping-cart'>&nbsp;</em>  </a></td></tr>", "img-responsive2", pro.Rows[i][6].ToString(), "Editar(this)", "Estado(this)");
                productosHabilitados += temp;
            }
            for (int i = 0; i < proD.Rows.Count; i++)
            {

                temp = string.Format("<tr><td><div><img class='{0}' id=" + "'tablaProductos'" + " src ='{1}'/></div></td>" +
                    "<td>" + proD.Rows[i][1].ToString() + "</td><td>" + proD.Rows[i][2].ToString() + "</td>" +
                    "<td>" + proD.Rows[i][3].ToString() + "</td><td>" + proD.Rows[i][8].ToString() + "</td>" +
                    "<td><a data-toggle='modal' data-target='#editar'  id=" + proD.Rows[i][1].ToString() + "  class=" + "'btn btn-linkEdit btn-md'" + " onclick='Editar(this)' > <em class='flaticon-list'>&nbsp;</em></a>" +
                    "<button tittle='Habilitar producto' id=" + proD.Rows[i][1].ToString() + " value='deshabilitado'  class=" + "'btn btn-linkVisi btn-md'" + " onclick='{3}' ><em class='flaticon-eye'>&nbsp;</em> </button></td>" +
                    "</tr>", "img-responsive2", proD.Rows[i][6].ToString(), "Editar(this)", "Estado(this)");
                productosDesHabilitados += temp;

            }
            TablaEsquema = "" + productosHabilitados + productosDesHabilitados;
            return Json(TablaEsquema, JsonRequestBehavior.AllowGet);
        }
    }
}
