using Sualiado.data.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Sualiado.data.Models.DAO
{
    public class Reportes
    {
        public DataTable VentasPorMes()
        {

            DataTable ds = new DataTable();
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
                    cmd.CommandText = "SP_VentasPorMes";
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }
            return ds;
        }
        public DataTable ProductosMasVendidos()
        {

            DataTable ds = new DataTable();
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
                    cmd.CommandText = "SP_PRODUCTOS_MAS_VENDIDOS";
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }
            return ds;
        }
        public DataTable ProductosAgotados()
        {

            DataTable ds = new DataTable();
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
                    cmd.CommandText = "SP_PRODUCTOS_AGOTADOS";
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }
            return ds;
        }
        public DataTable CambioEnPrecios()
        {

            DataTable ds = new DataTable();
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
                    cmd.CommandText = "SP_MOSTRAR_HISTORICO";
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }
            return ds;
        }
        public DataTable ProductosProAgotar()
        {

            DataTable ds = new DataTable();
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
                    cmd.CommandText = "SP_PRODUCTOS_PROXIMOS_AGOTAR";
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }
            return ds;
        }
    }
}