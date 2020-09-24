using Sualiado.data.Connection;
using Sualiado.data.Models.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Sualiado.data.Models.DAO
{
    public class DañoDAO
    {
        Conexion con = new Conexion();
        public DataTable TraerTipos()
        {
            DataTable dt = new DataTable();
            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SP_Daños";
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
        public DataTable MostrarReportes()
        {
            DataTable dt = new DataTable();
            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SP_REPORTES";
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
        public bool ReportarDaño(DañoVO daño)
        {
            int cant = 0;
            Conexion con = new Conexion();
            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SP_Reportar_Daño";
                    cmd.Parameters.AddWithValue("@referencia",daño.Refe);
                    cmd.Parameters.AddWithValue("@unidadesDesechadas",int.Parse(daño.Unidades));
                    cmd.Parameters.AddWithValue("@daño",int.Parse(daño.Tipo));
                    cmd.Parameters.AddWithValue("@persona",int.Parse(daño.Persona));
                    cmd.Parameters.AddWithValue("@observacion",daño.Observacion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cant = cmd.ExecuteNonQuery();
                }

            }

            return (cant>0);
        }
    }
}