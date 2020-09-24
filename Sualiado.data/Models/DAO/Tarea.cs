using Sualiado.data.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Sualiado.data.Models.DAO
{
    public class Tarea
    {
        public bool CambiarEstado(string id,string estado) {

            int cantidad = 0;
            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SP_ACTUALIZAR_TAREA";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id",id);
                    cmd.Parameters.AddWithValue("@estado",estado);
                    try
                    {
                        cantidad = cmd.ExecuteNonQuery();

                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }
            }


            return (cantidad > 0);
        }
        Conexion con = new Conexion();
        public DataTable Tareas(string id)
        {
            DataTable dt = new DataTable();
            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SP_TAREAS";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = conn;
                        sda.SelectCommand = cmd;
                        using (sda)
                        {
                            sda.Fill(dt);
                        }
                    }
                }

            }

            return dt;
        }
        public DataTable TareasCompletas()
        {
            DataTable dt = new DataTable();
            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SP_MOSTRARTAREASCOMPLETAS";
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = conn;
                        sda.SelectCommand = cmd;
                        using (sda)
                        {
                            sda.Fill(dt);
                        }
                    }
                }

            }

            return dt;
        }
        public DataTable TareasIncompletas()
        {
            DataTable dt = new DataTable();
            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SP_MOSTRARTAREAS";
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = conn;
                        sda.SelectCommand = cmd;
                        using (sda)
                        {
                            sda.Fill(dt);
                        }
                    }
                }

            }

            return dt;
        }
        public bool AgregarTarea(string tarea,int id,int estado) {
            int cantidad = 0;
            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SP_AGRAGAR_TAREA";
                    cmd.Parameters.AddWithValue("@DESCRIPCION",tarea);
                    cmd.Parameters.AddWithValue("@PERSONA",id);
                    cmd.Parameters.AddWithValue("@ESTADO",estado);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cantidad = cmd.ExecuteNonQuery();

                }
            }

            return (cantidad > 0);

        }
    }
}