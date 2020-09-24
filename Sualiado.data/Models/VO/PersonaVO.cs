using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sualiado.data.Models.VO
{
    public class PersonaVO
    {
        string nombre, apellido, direccion, telefono, cargo, usuario, clave, correo, estado,genero;

        public PersonaVO(string nombre, string apellido, string direccion, string telefono, string cargo, string usuario, string clave, string correo,string genero)
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Direccion = direccion;
            this.Telefono = telefono;
            this.Cargo = cargo;
            this.Usuario = usuario;
            this.Clave = clave;
            this.Correo = correo;
            this.Genero = genero;
        }

        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Cargo { get => cargo; set => cargo = value; }
        public string Usuario { get => usuario; set => usuario = value; }
        public string Clave { get => clave; set => clave = value; }
        public string Correo { get => correo; set => correo = value; }
        public string Estado { get => estado; set => estado = value; }
        public string Genero { get => genero; set => genero = value; }
    }
}