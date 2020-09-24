using Sualiado.data.Connection;
using Sualiado.data.Models.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Sualiado.data.Models.DAO
{
    public class EntradaDAO
    {
        Conexion con = new Conexion();
        public bool AgregarEntrada(EntradaVO entrada) {
            int cant = 0;
            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SP_INSERTAR_ENTRADA";
                    cmd.Parameters.AddWithValue("@proveedor",int.Parse(entrada.Proveedor));
                    cmd.Parameters.AddWithValue("@producto", int.Parse(entrada.Producto));
                    cmd.Parameters.AddWithValue("@cantidad", int.Parse(entrada.Cantidad));
                    cmd.Parameters.AddWithValue("@persona", entrada.Persona);
                    cmd.Parameters.AddWithValue("@precioC",int.Parse(entrada.PrecioC));
                    cmd.Parameters.AddWithValue("@fecha",DateTime.Parse(entrada.Fecha));
                    cmd.CommandType = CommandType.StoredProcedure;
                    cant = cmd.ExecuteNonQuery();

                }
            }
            return (cant > 0);

        }      
        public DataTable MostrarEntrada() {
            DataTable dt = new DataTable();
            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SP_MOSTRAR_ENTRADA";
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = conn;
                        sda.SelectCommand = cmd;
                        using (dt)
                        {
                            sda.Fill(dt);
                        }
                    }

                }
            }
            return dt;

        }
        public DataTable MostrarEntrada(DateTime date) {
            DataTable dt = new DataTable();
            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SP_BUSCAR_ENTRADA";
                    cmd.Parameters.AddWithValue("@campo",date);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = conn;
                        sda.SelectCommand = cmd;
                        using (dt)
                        {
                            sda.Fill(dt);
                        }
                    }

                }
            }
            return dt;

        }
    }
}