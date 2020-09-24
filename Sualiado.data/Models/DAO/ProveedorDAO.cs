using Sualiado.data.Connection;
using Sualiado.data.Models.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Sualiado.data.Models.DAO
{
    public class ProveedorDAO
    {
        Conexion con = new Conexion();
        public bool AgregarProveedor(ProveedorVO proVO){
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
                    cmd.CommandText = "SP_AGRAGAR_PROVEEDOR";
                    cmd.Parameters.AddWithValue("@nombre",proVO.Nombre);
                    cmd.Parameters.AddWithValue("@contacto",proVO.Contacto);
                    cmd.Parameters.AddWithValue("@telefono",proVO.Telefono);
                    cmd.Parameters.AddWithValue("@correo",proVO.Correo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cant = cmd.ExecuteNonQuery();

                }
            }
            return (cant > 0);

        }
        public bool ActualizarProveedor(ProveedorVO proVO)
        {
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
                    cmd.CommandText = "SP_ACTUALIZAR_PROVEEDOR";
                    cmd.Parameters.AddWithValue("@id", proVO.Id);
                    cmd.Parameters.AddWithValue("@nombre", proVO.Nombre);
                    cmd.Parameters.AddWithValue("@contacto", proVO.Contacto);
                    cmd.Parameters.AddWithValue("@telefono", proVO.Telefono);
                    cmd.Parameters.AddWithValue("@correo", proVO.Correo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cant = cmd.ExecuteNonQuery();

                }
            }
            return (cant > 0);

        }
        public DataTable MostrarProveedores()
        {
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
                    cmd.CommandText = String.Format("select * FROM Proveedor");
                    cmd.CommandType = CommandType.Text;
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
        public DataTable MostrarProveedores(string refe)
        {
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
                    cmd.CommandText = String.Format("select * FROM Proveedor pr inner join Producto p on p.Idproveedor=pr.Idproveedor where p.referencia='{0}'",refe);
                    cmd.CommandType = CommandType.Text;
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
        public DataTable TraerProveedor(int id)
        {
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
                    cmd.CommandText = "SP_TraerProveedor";
                    cmd.Parameters.AddWithValue("@id", id);
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
        public DataTable BuscarProveedores(string campo)
        {
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
                    cmd.CommandText = "SP_BusquedaProveedor";
                    cmd.Parameters.AddWithValue("@campoBusqueda", campo);
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