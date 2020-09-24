using Sualiado.data.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Sualiado.data.Connection
{
    public class TablaPedido
    {
        DataTable producto = new DataTable();
        ProductoDAO productoDatos = new ProductoDAO();
        int totalCompra;

        public int TotalCompra { get => totalCompra; set => totalCompra = value; }

        public DataTable LlenarTabla(DataTable producto,string referencia,string nombre,string precio,string stock,string cant)
        {
            #region AGREGAR FILAS
            DataRow Row;

            Row = producto.NewRow();

            Row["Referencia"] = referencia;
            Row["Nombre"] = nombre;
            Row["Precio"] = precio;
            Row["stock"] = stock;
            Row["cant"] = cant;
            producto.Rows.Add(Row);
            #endregion

            return producto;

        }
        public DataTable CrearTabla()
        {
            DataTable producto = new DataTable();
         
            DataColumn Col;
            Col = new DataColumn("Referencia", Type.GetType("System.String"));
            producto.Columns.Add(Col);
            Col = new DataColumn("Nombre", Type.GetType("System.String"));
            producto.Columns.Add(Col);
            Col = new DataColumn("Precio", Type.GetType("System.String"));
            producto.Columns.Add(Col);
            Col = new DataColumn("stock", Type.GetType("System.String"));
            producto.Columns.Add(Col);
            Col = new DataColumn("cant", Type.GetType("System.String"));
            producto.Columns.Add(Col);
            return producto;
        }
    }
}