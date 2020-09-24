using Sualiado.data.Connection;
using Sualiado.data.Models.VO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace Sualiado.data.DAO
{

    public class ProductoDAO
    {
        Conexion con = new Conexion();
        IDataReader lector = null;
        DataTable tabla = new DataTable();
        SqlCommand comando = new SqlCommand();


        public DataTable Buscar(String buscar)
        {
            DataTable dt = new DataTable();
            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText ="SP_Busqueda";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@campoBusqueda", buscar);
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
        public DataTable Productos()
        {
            DataTable dt = new DataTable();
            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SP_PRODUCTOS_HABILITADOS";
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
        public DataTable ProductosTop()
        {
            DataTable dt = new DataTable();
            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "Select top(6) * from Producto where estado=1";
                    cmd.CommandType = CommandType.Text;
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
        public DataTable Productos(string campo)
        {
            DataTable dt = new DataTable();
            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SP_BusquedaInventario";
                    cmd.Parameters.AddWithValue("@campoBusqueda", campo);
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
        public DataTable ProductosDeshabilitados(string campo)
        {
            DataTable dt = new DataTable();
            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SP_BusquedaInventarioDes";
                    cmd.Parameters.AddWithValue("@campoBusqueda",campo);
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
        public DataTable ProductosDeshabilitados()
        {
            DataTable dt = new DataTable();
            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SP_PRODUCTOS_DESHABILITADOS";
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
        public DataTable TraerTipos()
        {
            DataTable dt = new DataTable();
            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = String.Format("select * FROM Tipo_Accesorio");
                    cmd.CommandType = CommandType.Text;
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

        public bool AgregarTipo(string des) {
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
                    cmd.CommandText = "AgregarTipo";
                    cmd.Parameters.AddWithValue("@descripcion",des);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cant = cmd.ExecuteNonQuery();

                }
            }
            return (cant>0);
        }
        public String[] ObtenerReferencias()
        {
            int filas = 0;
            IDataReader lector = null;
            String[] referencias = null;
            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {
                DataTable ds = new DataTable();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = String.Format("select count(*) from Producto where Estado=1");
                    cmd.CommandType = CommandType.Text;

                    lector = cmd.ExecuteReader();
                    if (lector.Read())
                    {
                        filas = int.Parse(lector[0].ToString());
                    }
                    lector.Close();
                    referencias = new string[filas];
                    using (var cmd1 = new SqlCommand())
                    {
                        cmd1.Connection = conn;
                        cmd1.CommandText = String.Format("select referencia from Producto where Estado=1");
                        cmd1.CommandType = CommandType.Text;


                        using (var da = new SqlDataAdapter(cmd1))
                        {
                            da.Fill(ds);
                        }
                        for (int i = 0; i < referencias.Length; i++)
                        {
                            referencias[i] = ds.Rows[i][0].ToString();
                        }

                    }
                }

            }

            return referencias;
        }
        


 
        public string TraerTipo(string refe)
        {
            string tipo = "";
            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = String.Format("Select Tipo from Producto where referencia='{0}'", refe);
                    cmd.CommandType = CommandType.Text;
                    lector = cmd.ExecuteReader();
                    if (lector.Read())
                    {


                        tipo = lector[0].ToString();

                    }
                    lector.Close();

                }
            }
            return tipo;
        }
        public bool SubirImagen(string ruta, string refe)
        {

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
                    cmd.CommandText = String.Format("UPDATE Producto SET imagenProducto='{0}' where referencia = '{1}'", ruta, refe);
                    cmd.CommandType = CommandType.Text;
                    cantidad = cmd.ExecuteNonQuery();

                }
            }

            return (cantidad > 0);

        }
        public bool CambiarEstado(string refe,int cambio)
        {

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
                    cmd.CommandText = String.Format("update Producto set Estado={0} where referencia='{1}'",cambio, refe);
                    cmd.CommandType = CommandType.Text;
                    cantidad = cmd.ExecuteNonQuery();


                }
            }

            return (cantidad > 0);

        }
        public bool ActualizarProducto(ProductoVO product,int persona)
        {
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
                    cmd.CommandText = "SP_ACTUALIZAR_PRODUCTO";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@referencia", product.Referencia);
                    cmd.Parameters.AddWithValue("@nombre", product.Nombre);
                    cmd.Parameters.AddWithValue("@precio", int.Parse(product.Precio));
                    cmd.Parameters.AddWithValue("@tipo", int.Parse(product.Tipo));
                    cmd.Parameters.AddWithValue("@proveedor", int.Parse(product.Proveedor));
                    cmd.Parameters.AddWithValue("@persona", persona);
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
        public DataTable Mostrar()
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
                    cmd.CommandText = String.Format("select * FROM Producto where Estado=1");
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
        public DataTable Mostrar(string referencia)
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
                    cmd.CommandText = String.Format("select IdProducto,referencia,NombreProducto,PrecioUnitario,cantidadDisponible,t.nombreTipo,precioCompra from Producto p inner join Tipo_Accesorio t on p.Tipo=t.idTipo where referencia='{0}' and Estado=1", referencia);
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
        public DataTable MostrarDatos(string referencia)
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
                    cmd.CommandText = String.Format("select IdProducto,referencia,NombreProducto,PrecioUnitario,cantidadDisponible,t.nombreTipo,precioCompra from Producto p inner join Tipo_Accesorio t on p.Tipo=t.idTipo where IdProducto='{0}'", referencia);
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

        public int VerificarEstado(string refe)
        {
            int estado = 0;
            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = String.Format("select Estado from Producto where referencia='{0}'", refe);
                    cmd.CommandType = CommandType.Text;
                    lector = cmd.ExecuteReader();
                    if (lector.Read())
                    {
                        estado = int.Parse(lector[0].ToString());
                    }
                    lector.Close();

                }
            }

            return estado;
        }
        public int CantidadProductos()
        {
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
                    cmd.CommandText = String.Format("select count(*) from Producto");
                    cmd.CommandType = CommandType.Text;
                    cantidad = cmd.ExecuteNonQuery();

                }
            }

            return cantidad;
        }
        public bool AgregarProducto(ProductoVO pro)
        {
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
                    cmd.CommandText = "SP_AGRAGAR_PRODUCTO";
                    cmd.Parameters.AddWithValue("@referencia", pro.Referencia);
                    cmd.Parameters.AddWithValue("@nombre", pro.Nombre);
                    cmd.Parameters.AddWithValue("@precio", int.Parse(pro.Precio));
                    cmd.Parameters.AddWithValue("@tipo", int.Parse(pro.Tipo));
                    cmd.Parameters.AddWithValue("@precioCompra", int.Parse(pro.PrecioCompra));
                    cmd.Parameters.AddWithValue("@proveedor", int.Parse(pro.Proveedor));
                    cmd.CommandType = CommandType.StoredProcedure;
                     cantidad = cmd.ExecuteNonQuery();

                }
            }

            return (cantidad > 0);



        }

        public String[] InformacionProducto(string referencia)
        {
            String[] datos = new String[10];
            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = String.Format("SELECT IdProducto,referencia,NombreProducto,PrecioUnitario,p.precioCompra,cantidadDisponible,T.nombreTipo,pr.NombreProveedor,p.IdProveedor,p.tipo FROM Producto p Inner join Tipo_Accesorio T on p.Tipo=t.idTipo INNER JOIN Proveedor pr ON p.IdProveedor=pr.IdProveedor  where referencia='{0}'", referencia);
                    cmd.CommandType = CommandType.Text;
                    lector = cmd.ExecuteReader();
                    if (lector.Read())
                    {
                        for (int i = 0; i < datos.Length; i++)
                        {
                            datos[i] = lector[i].ToString();
                        }
                    }
                    lector.Close();


                }
            }
            return datos;
        }
    }
}