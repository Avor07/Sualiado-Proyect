using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace Sualiado.data.Connection
{
    public class Conexion
    {
        public string TraerCadena(string valor)
        {
            string cadena = System.Configuration.ConfigurationManager.ConnectionStrings[valor].ToString();
            if (string.IsNullOrEmpty(cadena))
            {
                throw new Exception("No se encontro la cadena de conexion");
            }
            return cadena;
        }
        
       
    }
}