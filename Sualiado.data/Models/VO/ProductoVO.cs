using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sualiado.data.Models.VO
{

    public class ProductoVO
    {


        string nombre, referencia,precio, stock, tipo,ruta,proveedor,precioCompra;

        public ProductoVO(string nombre, string referencia, string precio, string stock, string tipo, string ruta,string proveedor,string precioCompra)
        {
            this.nombre = nombre;
            this.referencia = referencia;
            this.precio = precio;
            this.stock = stock;
            this.tipo = tipo;
            this.ruta = ruta;
            this.proveedor = proveedor;
            this.precioCompra = precioCompra;
        }

        public string Nombre { get => nombre; set => nombre = value; }
        public string Referencia { get => referencia; set => referencia = value; }
        public string Precio { get => precio; set => precio = value; }
        public string Stock { get => stock; set => stock = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public string Ruta { get => ruta; set => ruta = value; }
        public string Proveedor { get => proveedor; set => proveedor = value; }
        public string PrecioCompra { get => precioCompra; set => precioCompra = value; }
    }
}