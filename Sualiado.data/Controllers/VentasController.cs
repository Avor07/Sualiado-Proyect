using Sualiado.data.Connection;
using Sualiado.data.DAO;
using Sualiado.data.Models.DAO;
using Sualiado.data.Models.VO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Sa_Ventas.Controllers
{
    public class VentasController : Controller
    {
        // GET: Ventas
        TablaPedido tabla = new TablaPedido();
        PedidoDAO pedidoDAO = new PedidoDAO();
        DataTable resumen = new DataTable();
        bool verificacion = false;
        ProductoDAO producto = new ProductoDAO();
        [HttpGet]
        public ActionResult Ventas()
        {
            if (Session["rol"] != null)
            {
                string usu = Session["id"].ToString();
                if (int.Parse(Session["rol"].ToString()) == 1 || int.Parse(Session["rol"].ToString()) == 2)
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


        [HttpPost]
        public ActionResult Ventas(string cantidad, string campo, string boton, string borrar, string opcion, string[] cantidades)
        {
            string modal = "";
            if (opcion != null && opcion != "")
            {
                resumen = Session["resumen"] as DataTable;
                int cant = 0;
                string productoSin = "";
                verificacion = false;
                string alerta = "";
                if (resumen != null && resumen.Rows.Count != 0)
                {
                    int conteo = resumen.Rows.Count;
                    for (int i = 0; i < conteo; i++)
                    {
                        if (int.Parse(cantidades[i]) > 0)
                        {


                            cant = pedidoDAO.VerificarCantidad(resumen.Rows[i][0].ToString());
                            if (cant < int.Parse(cantidades[i]))
                            {
                                productoSin = resumen.Rows[i][1].ToString();
                                conteo = 0;
                                i = 0;
                                verificacion = false;
                                for (int j = 0; j < resumen.Rows.Count; j++)
                                {
                                    string temp = "<tr><td><label>" + resumen.Rows[j][1].ToString() + "</label> </td> <td> <label>" + resumen.Rows[j][2].ToString() + "</label> </td> <td> <label>" + resumen.Rows[j][3].ToString() + "</label> </td> <td> <div class=" + "'form - inline'" + "><button type=" + "'button'" + " onclick=" + "'Disminuir(this.value)'" + " value=" + j + " class=" + "'btn btn-light btn-sm bg-transparent'" + "> < </button> <input readonly=" + "'readonly'" + " type=" + "'number'" + " style=" + "'outline: none; width: 40px;'" + " value=" + cantidades[j] + " class=" + "'border-0 bg-transparent'" + " id=" + j + " name=" + "'cantidadSolicitada'" + " min=" + "'1'" + "> <button type=" + "'button'" + " onmousedown=" + "'Aumentar(this.value)'" + " value=" + j + " class=" + "'btn btn-light btn-sm bg-transparent'" + "> ></button></div></td><td><button type=" + "'button'" + " onclick=" + "'BorrarProducto(this)'" + " value=" + resumen.Rows[i][0] + " class=" + "'btn btn-outline-danger btn-sm bg-transparent'" + "> x </button> </td></tr>";
                                    modal += temp;
                                }
                                alerta = productoSin;
                                string[] respuesta = new string[2];
                                respuesta[0] = alerta;
                                respuesta[1] = "<input type='hidden' id='unidades' value='falseNS' />" + modal;
                                return Json(respuesta, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                verificacion = true;
                            }
                        }
                        else {
                            for (int j = 0; j < resumen.Rows.Count; j++)
                            {
                                string temp = "<tr><td><label>" + resumen.Rows[j][1].ToString() + "</label> </td> <td> <label>" + resumen.Rows[j][2].ToString() + "</label> </td> <td> <label>" + resumen.Rows[j][3].ToString() + "</label> </td> <td> <div class=" + "'form - inline'" + "><button type=" + "'button'" + " onclick=" + "'Disminuir(this.value)'" + " value=" + j + " class=" + "'btn btn-light btn-sm bg-transparent'" + "> < </button> <input readonly=" + "'readonly'" + " type=" + "'number'" + " style=" + "'outline: none; width: 40px;'" + " value=" + cantidades[j] + " class=" + "'border-0 bg-transparent'" + " id=" + j + " name=" + "'cantidadSolicitada'" + " min=" + "'1'" + "> <button type=" + "'button'" + " onmousedown=" + "'Aumentar(this.value)'" + " value=" + j + " class=" + "'btn btn-light btn-sm bg-transparent'" + "> ></button></div></td><td><button type=" + "'button'" + " onclick=" + "'BorrarProducto(this)'" + " value=" + resumen.Rows[i][0] + " class=" + "'btn btn-outline-danger btn-sm bg-transparent'" + "> x </button> </td></tr>";
                                modal += temp;
                            }
                            alerta = "<div class='alert alert-danger'><a class=" + "'nav-brand'" + ">Los valores no pueden ser negativos</a></div>";
                            string[] respuesta = new string[2];
                            respuesta[0] = alerta;
                            respuesta[1] = "<input type = 'hidden' id = 'unidades' value = 'negativos' /> " + modal;
                            return Json(respuesta, JsonRequestBehavior.AllowGet);

                        }
                    }
                    if (verificacion == true)
                    {
                        if (verificacion == true)
                        {
                            DateTime thisDay = DateTime.Today;

                            pedidoDAO.RegistrarPedido(thisDay, int.Parse(Session["id"].ToString()));
                            for (int i = 0; i < resumen.Rows.Count; i++)
                            {
                                pedidoDAO.RegistrarVenta(int.Parse(cantidades[i]), int.Parse(resumen.Rows[i][2].ToString()), resumen.Rows[i][0].ToString());
                            }
                            resumen = null;
                            Session["resumen"] = null;
                            string[] respuesta = new string[2];
                            respuesta[0] = alerta;
                            respuesta[1] = "<input type = 'hidden' id = 'unidades' value = 'true' /> " + modal;
                            return Json(respuesta, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                else
                {

                    alerta = "<div class='alert alert-danger'><a class=" + "'nav-brand'" + ">No hay productos en el carrito</a></div>";
                    string[] respuesta = new string[2];
                    respuesta[0] = alerta;
                    respuesta[1] = "<input type = 'hidden' id = 'unidades' value = 'false' /> ";
                    return Json(respuesta, JsonRequestBehavior.AllowGet);
                }
            }
            if (borrar != null && borrar != "")
            {

                resumen = Session["resumen"] as DataTable;
                for (int i = resumen.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = resumen.Rows[i];

                    if (dr["Referencia"].ToString() == borrar)
                    {
                        dr.Delete();

                    }


                }
                resumen.AcceptChanges();
                Session["resumen"] = resumen;
                for (int i = 0; i < resumen.Rows.Count; i++)
                {
                    string temp = "<tr><td><label>" + resumen.Rows[i][1].ToString() + "</label> </td> <td> <label>" + resumen.Rows[i][2].ToString() + "</label> </td> <td> <label>" + resumen.Rows[i][3].ToString() + "</label> </td> <td> <div class=" + "'form - inline'" + "><button type=" + "'button'" + " onclick=" + "'Disminuir(this.value)'" + " value=" + i + " class=" + "'btn btn-light btn-sm bg-transparent'" + "> < </button> <input readonly=" + "'readonly'" + " type=" + "'number'" + " style=" + "'outline: none; width: 40px;'" + " value=" + "'1'" + " class=" + "'border-0 bg-transparent'" + " id=" + i + " name=" + "'cantidadSolicitada'" + " min=" + "'1'" + "> <button type=" + "'button'" + " onclick=" + "'Aumentar(this.value)'" + " value=" + i + " class=" + "'btn btn-light btn-sm bg-transparent'" + "> ></button></div></td><td><button type=" + "'button'" + " onclick=" + "'BorrarProducto(this)'" + " value=" + resumen.Rows[i][0] + " class=" + "'btn btn-outline-danger btn-sm bg-transparent'" + "> x </button> </td></tr>";
                    modal += temp;
                }

                return Json(modal, JsonRequestBehavior.AllowGet);

            }

            if (boton != null)
            {

                if (Session["resumen"] == null)
                {
                    resumen = tabla.CrearTabla();
                    DataTable venta = producto.Mostrar(boton);
                    resumen = tabla.LlenarTabla(resumen, venta.Rows[0][1].ToString(), venta.Rows[0][2].ToString(), venta.Rows[0][3].ToString(), venta.Rows[0][4].ToString(), venta.Rows[0][5].ToString());
                    Session["resumen"] = resumen;
                }
                else
                {

                    resumen = Session["resumen"] as DataTable;
                    DataTable product = producto.Mostrar(boton);
                    resumen = tabla.LlenarTabla(resumen, product.Rows[0][1].ToString(),
                        product.Rows[0][2].ToString(), product.Rows[0][3].ToString(),
                        product.Rows[0][4].ToString(), product.Rows[0][5].ToString());


                }
                for (int i = 0; i < resumen.Rows.Count; i++)
                {
                    string temp = "<tr><td><label>" + resumen.Rows[i][1].ToString() + "</label> </td> <td> <label>" + resumen.Rows[i][2].ToString() + "</label> </td> <td> <label>" + resumen.Rows[i][3].ToString() + "</label> </td> <td> <div class=" + "'form - inline'" + "><button type=" + "'button'" + " onclick=" + "'Disminuir(this.value)'" + " value=" + i + " class=" + "'btn btn-light btn-sm bg-transparent'" + "> < </button> <input readonly=" + "'readonly'" + " type=" + "'number'" + " style=" + "'outline: none; width: 40px;'" + " value=" + "'1'" + " class=" + "'border-0 bg-transparent'" + " id=" + i + " name=" + "'cantidadSolicitada'" + " min=" + "'1'" + "> <button type=" + "'button'" + " onclick=" + "'Aumentar(this.value)'" + " value=" + i + " class=" + "'btn btn-light btn-sm bg-transparent'" + "> ></button></div></td><td><button type=" + "'button'" + " onclick=" + "'BorrarProducto(this)'" + " value=" + resumen.Rows[i][0] + " class=" + "'btn btn-outline-danger btn-sm bg-transparent'" + "> x </button> </td></tr>";
                    modal += temp;
                }

                return Json(modal, JsonRequestBehavior.AllowGet);

            }

            string[] referencias = producto.ObtenerReferencias();
            String[] imagenes = null;
            string TablaEsquema = "";
            string tablaImagenes = "";
            string botones = "";
            if (campo != "" && campo != null)
            {
                DataTable busqueda = producto.Buscar(campo);
                imagenes = new string[busqueda.Rows.Count];

                for (int i = 0; i < imagenes.Length; i++)
                {

                    string temp = string.Format("<tr><td><div><img class='{0}' id=" + "'tablaProductos'" + " src ='{1}'/></div></td><td>" + busqueda.Rows[i][1].ToString() + "</td><td>" + busqueda.Rows[i][2].ToString() + "</td><td>" + busqueda.Rows[i][3].ToString() + "</td><td><button id=" + "'btnAgregar'" + " value=" + busqueda.Rows[i][1].ToString() + "  class=" + "'btn btn-linkEdit'" + " onclick='{2}' ><em class='flaticon-add-4'></em></button><br><label class='text-success' id='confirmacion" + busqueda.Rows[i][1].ToString() + "' ></label></td></</tr>", "img-responsive2", busqueda.Rows[i][6].ToString(), "Agregar(this)");
                    tablaImagenes += temp;

                }
                TablaEsquema = "<tbody>" + tablaImagenes + botones + "</tbody>";
                return Json(TablaEsquema, JsonRequestBehavior.AllowGet);
            }

            imagenes = new string[referencias.Length];
            DataTable pro = producto.Productos();


            for (int i = 0; i < imagenes.Length; i++)
            {

                string temp = string.Format("<tr><td><div><img class='{0}' id=" + "'tablaProductos'" + " src ='{1}'/></div></td><td>" + pro.Rows[i][1].ToString() + "</td><td>" + pro.Rows[i][2].ToString() + "</td><td>" + pro.Rows[i][3].ToString() + "</td><td><button id=" + "'btnAgregar'" + " value=" + pro.Rows[i][1].ToString() + "  class=" + "'btn btn-linkEdit'" + " onclick='{2}' ><em class='flaticon-add-4'></em></button><br><label class='text-success' id='confirmacion" + pro.Rows[i][1].ToString() + "' ></label></td></</tr>", "img-responsive2", pro.Rows[i][6].ToString(), "Agregar(this)");
                tablaImagenes += temp;

            }
            TablaEsquema = "<tbody>" + tablaImagenes + "</tbody>";
            return Json(TablaEsquema, JsonRequestBehavior.AllowGet);
        }
    }
}