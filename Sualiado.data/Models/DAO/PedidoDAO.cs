using Sualiado.data.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Sualiado.data.Models.DAO
{
    public class PedidoDAO
    {
        int cantidadRegistro = 0;
        Conexion con = new Conexion();
        IDataReader lector = null;
        int cantidad = 0;
        public bool RegistrarPedido(DateTime fecha, int idPersona)
        {
            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (var cmd = new SqlCommand())
                {

                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_INSERTAR_PEDIDO";
                    cmd.Parameters.AddWithValue("@FECHA", fecha);
                    cmd.Parameters.AddWithValue("@OBSERVACION", "");
                    cmd.Parameters.AddWithValue("@IDPERSONA", idPersona);
                    try
                    {
                        cantidadRegistro = cmd.ExecuteNonQuery();

                    }
                    catch (SqlException)
                    {
                        cantidadRegistro = 0;

                    }

                }
            }
            return (cantidadRegistro > 0);
        }
        public int VerificarCantidad(string referencia)
        {
            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = String.Format("select  cantidadDisponible from Producto where Referencia='{0}'", referencia);
                    cmd.CommandType = CommandType.Text;

                    lector = cmd.ExecuteReader();
                    if (lector.Read())
                    {
                        cantidad = int.Parse(lector[0].ToString());
                    }
                    lector.Close();
                }
            }
            return cantidad;

        }

        public bool RegistrarVenta(int cantidad, int precio, string referencia)
        {

            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (var cmd = new SqlCommand())
                {
                    cantidadRegistro = 0;
                    Conexion con = new Conexion();



                    cmd.Connection = conn;
                    cmd.CommandText = "SP_INSERTAR_DETALLE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CANTIDAD", cantidad);
                    cmd.Parameters.AddWithValue("@PRECIO", precio);
                    cmd.Parameters.AddWithValue("@REFERENCIA", referencia);
                    try
                    {
                        cantidadRegistro = cmd.ExecuteNonQuery();

                    }
                    catch (Exception SqlExeption)
                    {
                        if (SqlExeption.Message.Contains("UPDATE"))
                        {
                            cantidadRegistro = 0;
                        }

                    }
                }
            }
            return (cantidadRegistro > 0);
        }
    }
}