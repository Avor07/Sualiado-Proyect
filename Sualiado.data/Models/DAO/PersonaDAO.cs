using Sualiado.data.Connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Sualiado.data.Clases;
using System.Diagnostics;
using Sualiado.data.Models.VO;

namespace Sualiado.data.Models.DAO
{

    public class PersonaDAO
    {
        public DataTable Buscar(String buscar)
        {
            DataTable dt = new DataTable();
            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SP_BusquedaPersona";
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
        public bool SubirImagen(string ruta,string id){
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
                    cmd.CommandText = String.Format("UPDATE Persona SET imagen='{0}' where idpersona= '{1}'", ruta,int.Parse(id));
                    cmd.CommandType = CommandType.Text;
                    cantidad = cmd.ExecuteNonQuery();

                }
            }

            return (cantidad > 0);
        }
        public bool CambiarEstado(string usu,int estado)
        {
            DataSet ds = new DataSet();
            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (var cmd = new SqlCommand())
                {

                    string consulta = string.Format("update Persona set Estado={1} where IdPersona={0}", usu,estado);
                    Debug.WriteLine(consulta);
                    cmd.Connection = conn;
                    cmd.CommandText = consulta;
                    cmd.CommandType = CommandType.Text;
                    int cantidad=cmd.ExecuteNonQuery();
                    return (cantidad>0);
                }
            }

        }
        public bool CambiarContraseña(string contra,string usu)
        {
            DataSet ds = new DataSet();
            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (var cmd = new SqlCommand())
                {
                   
                    string consulta = string.Format("Update persona set clave='{0}' where idpersona='{1}'",contra,usu);
                    cmd.Connection = conn;
                    cmd.CommandText = consulta;
                    cmd.CommandType = CommandType.Text;
                    int cantidad = cmd.ExecuteNonQuery();
                    return (cantidad > 0);
                }
            }

        }
        public bool recuperarContraseña(string contra, string id)
        {
            DataSet ds = new DataSet();
            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (var cmd = new SqlCommand())
                {

                    string consulta = string.Format("Update persona set clave='{0}' where username='{1}'", contra,id);
                    cmd.Connection = conn;
                    cmd.CommandText = consulta;
                    cmd.CommandType = CommandType.Text;
                    int cantidad = cmd.ExecuteNonQuery();
                    return (cantidad > 0);
                }
            }

        }
        public bool Verificar(string usu)
        {
            DataSet ds = new DataSet();
            Conexion con = new Conexion();



            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (var cmd = new SqlCommand())
                {

                    string consulta = string.Format("select* from Persona where UserName='{0}'",usu);
                    cmd.Connection = conn;
                    cmd.CommandText = consulta;
                    cmd.CommandType = CommandType.Text;
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                    return (ds.Tables[0].Rows.Count > 0);
                }
            }
        }
        public bool AgregarPersona(PersonaVO persona)
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
                    cmd.CommandText = "SP_INSERTAR_PERSONA";
                    cmd.Parameters.AddWithValue("@nombre", persona.Nombre);
                    cmd.Parameters.AddWithValue("@apellido", persona.Apellido);
                    cmd.Parameters.AddWithValue("@direc", persona.Direccion);
                    cmd.Parameters.AddWithValue("@telefono", persona.Telefono);
                    cmd.Parameters.AddWithValue("@rol", int.Parse(persona.Cargo));
                    cmd.Parameters.AddWithValue("@userN", persona.Usuario);
                    cmd.Parameters.AddWithValue("@clave", persona.Clave);
                    cmd.Parameters.AddWithValue("@correo", persona.Correo);
                    cmd.Parameters.AddWithValue("@genero", persona.Genero);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cant = cmd.ExecuteNonQuery();

                }
            }
            return (cant > 0);
        }
        public bool InsertarPin(string usuario, string pin)
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
                    cmd.CommandText = "SP_INGRESAR_PIN";
                    cmd.Parameters.AddWithValue("@USUARIO",usuario);
                    cmd.Parameters.AddWithValue("@PIN", pin);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cant = cmd.ExecuteNonQuery();

                }
            }
            return (cant > 0);
        }
        public bool Editar(PersonaVO persona)
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
                    cmd.CommandText = "SP_EditarINFO";
                    cmd.Parameters.AddWithValue("@ID",int.Parse(persona.Cargo));
                    cmd.Parameters.AddWithValue("@NOMBRE", persona.Nombre);
                    cmd.Parameters.AddWithValue("@APELLIDO", persona.Apellido);
                    cmd.Parameters.AddWithValue("@DIRECCION", persona.Direccion);
                    cmd.Parameters.AddWithValue("@TELEFONO", persona.Telefono);
                    cmd.Parameters.AddWithValue("@USUARIO", persona.Usuario);
                    cmd.Parameters.AddWithValue("@CORREO", persona.Correo);
                    cmd.Parameters.AddWithValue("@GENERO", persona.Genero);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cant = cmd.ExecuteNonQuery();

                }
            }
            return (cant > 0);
        }
        Conexion con = new Conexion();
        IDataReader lector = null;
        public DataTable TraerRoles()
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

                    string consulta = string.Format("select * From Rol");
                    cmd.Connection = conn;
                    cmd.CommandText = consulta;
                    cmd.CommandType = CommandType.Text;
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                    return ds;
                }
            }
        }
        public string TraerGenero(string usu)
        {
           
            Conexion con = new Conexion();
            string genero = "";


            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (var cmd = new SqlCommand())
                {

                    string consulta = string.Format("select Genero From persona where UserName={0}",usu);
                    cmd.Connection = conn;
                    cmd.CommandText = consulta;
                    cmd.CommandType = CommandType.Text;
                    lector = cmd.ExecuteReader();
                    if (lector.Read())
                    {

                        genero = lector[0].ToString();
                       
                    }
                    else
                    {
                        genero = "";
                    }
                    lector.Close();
                }
            }
            return genero;
        }
        public DataTable TraerInformacion(string usu)
        {

            Conexion con = new Conexion();
                    DataTable ds = new DataTable();


            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (var cmd = new SqlCommand())
                {

                  
                    cmd.Connection = conn;
                    cmd.CommandText = "SP_INFO";
                    cmd.Parameters.AddWithValue("@id",int.Parse(usu));
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }
                    return ds;
        }
        public DataTable TraerInfo(string usu)
        {

            Conexion con = new Conexion();
            DataTable ds = new DataTable();


            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (var cmd = new SqlCommand())
                {


                    cmd.Connection = conn;
                    cmd.CommandText = "Sp_InformacionRecuperacion";
                    cmd.Parameters.AddWithValue("@id",usu);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }
            return ds;
        }
        public string TraerNombre(string usu)
        {

            Conexion con = new Conexion();
            string nom = "";


            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (var cmd = new SqlCommand())
                {

                    string consulta = string.Format("select Nombre From persona where UserName={0}", usu);
                    cmd.Connection = conn;
                    cmd.CommandText = consulta;
                    cmd.CommandType = CommandType.Text;
                    lector = cmd.ExecuteReader();
                    if (lector.Read())
                    {

                        nom = lector[0].ToString();

                    }
                    else
                    {
                        nom = "";
                    }
                    lector.Close();
                }
            }
            return nom;
        }
        public DataTable Personas()
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
                    cmd.CommandText = "SP_PERSONAS";
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                    return ds;
                }
            }
        }

        public bool Verificar(string usu, string clave)
        {
            DataSet ds = new DataSet();
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
                    cmd.CommandText = "SP_INICIO_SESION";
                    cmd.Parameters.AddWithValue("@USER",usu);
                    cmd.Parameters.AddWithValue("@PASS",clave);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                    return (ds.Tables[0].Rows.Count > 0);
                }
            }
        }

        public int ObtenerRol(string usuario)
        {
            int id = 0;
            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = string.Format("select Rol from Persona where UserName='{0}' and estado=1", usuario);
                    cmd.CommandType = CommandType.Text;
                    lector = cmd.ExecuteReader();
                    if (lector.Read())
                    {

                        id = int.Parse(lector[0].ToString());

                    }
                    else
                    {
                        id = 0;
                    }
                    lector.Close();


                }
            }
            return id;
        }
        public int ObtenerId(string usuario)
        {
            int id = 0;
            using (var conn = new SqlConnection(con.TraerCadena("Sualiado")))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = string.Format("select IdPersona from Persona where UserName='{0}'", usuario);
                    cmd.CommandType = CommandType.Text;
                    lector = cmd.ExecuteReader();
                    if (lector.Read())
                    {

                        id = int.Parse(lector[0].ToString());

                    }
                    else
                    {
                        id = 0;
                    }
                    lector.Close();


                }
            }
            return id;
        }
    }
}