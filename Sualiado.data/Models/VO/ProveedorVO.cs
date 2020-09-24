using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sualiado.data.Models.VO
{
    public class ProveedorVO
    {
        string nombre, contacto, telefono, correo;
        int id=0;

        public ProveedorVO(int id,string nombre, string contacto, string telefono, string correo)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Contacto = contacto;
            this.Telefono = telefono;
            this.Correo = correo;
        }

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Contacto { get => contacto; set => contacto = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Correo { get => correo; set => correo = value; }
    }
}