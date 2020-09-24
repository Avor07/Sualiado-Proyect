using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sualiado.data.Models.VO
{
    public class EntradaVO
    {
        string proveedor, producto, cantidad, persona, precioC, fecha;

        public EntradaVO(string proveedor, string producto, string cantidad, string persona, string precioC, string fecha)
        {
            this.proveedor = proveedor;
            this.producto = producto;
            this.cantidad = cantidad;
            this.persona = persona;
            this.precioC = precioC;
            this.fecha = fecha;
        }

        public string Proveedor { get => proveedor; set => proveedor = value; }
        public string Producto { get => producto; set => producto = value; }
        public string Cantidad { get => cantidad; set => cantidad = value; }
        public string Persona { get => persona; set => persona = value; }
        public string PrecioC { get => precioC; set => precioC = value; }

        public string Fecha { get => fecha; set => fecha = value; }
    }
}