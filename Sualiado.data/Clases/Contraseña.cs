using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Sualiado.data.Clases
{
    public class Contraseña
    {
        public string GenerarContraseña() {
            Random rdn = new Random();
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890%$#@";
            int longitud = caracteres.Length;
            char letra;
            int longitudContrasenia = 10;
            string contraAleatoria = string.Empty;
            for (int i = 0; i < longitudContrasenia; i++)
            {
                letra = caracteres[rdn.Next(longitud)];
                contraAleatoria += letra.ToString();
            }
          
            return contraAleatoria;
        }
        public string GenerarCodigo()
        {
            Random rdn = new Random();
            string caracteres = "1234567890";
            int longitud = caracteres.Length;
            char letra;
            int longitudContrasenia = 6;
            string contraAleatoria = string.Empty;
            for (int i = 0; i < longitudContrasenia; i++)
            {
                letra = caracteres[rdn.Next(longitud)];
                contraAleatoria += letra.ToString();
            }

            return contraAleatoria;
        }
    }
}