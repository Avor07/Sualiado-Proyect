using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sualiado.data.Models.VO
{
    public class DañoVO
    {
        string refe, tipo, unidades,persona,observacion;

        public DañoVO(string refe, string tipo, string unidades,string persona,string observacion)
        {
            this.refe = refe;
            this.tipo = tipo;
            this.unidades = unidades;
            this.Persona = persona;
            this.Observacion = observacion;
        }

        public string Refe { get => refe; set => refe = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public string Unidades { get => unidades; set => unidades = value; }
        public string Persona { get => persona; set => persona = value; }
        public string Observacion { get => observacion; set => observacion = value; }
    }
}