using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;
using WebAlmacen.Models;
using WebAlmacen.Tools;

namespace WebAlmacen.Services
{
    public class ProductoService
    {
        private readonly string CadenaConexion = "bA2vzxpWgQOtmPLVSQWzRiM7LruuN/b7TIEbnUkLUaA95ZZl248lyHAAGTkbMXCi2jihsHH4b/dLkKUNw2BXj9y+v9f8IkG//YRSXmEi7XW0JrtC0bOsr/7D2CyAE0enqoAsOH5TyYErkUcLp3rWy71tH8XUqU56zDAzRixVIMivq4uPmB75D7f3nFFbptjs8hw1T76PMJihAlgNRQxfcUvT/h1te4MPsxFIHUL791w=";

        public string ProbarConeccion()
        {            
            try
            {
                SqlConnection connection = new SqlConnection(Encript.Desencriptar(CadenaConexion));
                connection.Open();
                
                return "Exito";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }        

        public List<ProductoDTO> ListarProductos()
        {
            List<ProductoDTO> Productos = new List<ProductoDTO>();

            string query = "SELECT * FROM dbo.fnObtenerProductos()";

            using (SqlConnection connection = new SqlConnection(Encript.Desencriptar(CadenaConexion)))
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ProductoDTO oProductos = new ProductoDTO();
                        oProductos.IdProducto = reader.GetInt32(0);
                        oProductos.Descripcion = reader.GetString(1);
                        oProductos.Marca = reader.GetString(2);
                        oProductos.Modelo = reader.GetString(3);
                        oProductos.Almacen = reader.GetString(4);
                        oProductos.Serie = reader.GetString(5);
                        oProductos.Precio = reader.GetDecimal(6);
                        oProductos.Existencia = reader.GetInt64(7);

                        Productos.Add(oProductos);
                    }

                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return Productos;
        }

        public List<Marcas> ListarMarcas()
        {
            List<Marcas> lstMarcas = new List<Marcas>();

            string query = "SELECT * FROM dbo.Marcas";

            using (SqlConnection connection = new SqlConnection(Encript.Desencriptar(CadenaConexion)))
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Marcas oMarcas = new Marcas();
                        oMarcas.IdMarca = reader.GetInt32(0);
                        oMarcas.Descripcion = reader.GetString(1);

                        lstMarcas.Add(oMarcas);
                    }

                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return lstMarcas;
        }

        public List<Modelos> ListarModelos()
        {
            List<Modelos> lstModelos = new List<Modelos>();

            string query = "SELECT * FROM dbo.Modelos";

            using (SqlConnection connection = new SqlConnection(Encript.Desencriptar(CadenaConexion)))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Modelos oModelos = new Modelos();
                        oModelos.IdModelo = reader.GetInt32(0);
                        oModelos.Descripcion = reader.GetString(1);

                        lstModelos.Add(oModelos);
                    }

                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return lstModelos;
        }

        public List<Almacen> ListarAlmacenes()
        {
            List<Almacen> lstAlmacen = new List<Almacen>();

            string query = "SELECT IdAlmacen, NombreAlmacen FROM dbo.Almacen ORDER BY 2";

            using (SqlConnection connection = new SqlConnection(Encript.Desencriptar(CadenaConexion)))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Almacen oAlmacen = new Almacen();
                        oAlmacen.IdAlmacen = reader.GetInt32(0);
                        oAlmacen.NombreAlmacen = reader.GetString(1);

                        lstAlmacen.Add(oAlmacen);
                    }

                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return lstAlmacen;
        }

        public Producto BuscarProducto(int Id)
        {
            Producto Producto = new Producto();

            string query = "EXEC spObtenerProducto @pId";

            using (SqlConnection connection = new SqlConnection(Encript.Desencriptar(CadenaConexion)))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, connection)
                    {
                        CommandText = "spObtenerProducto",
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@pId", Id);
                    
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Producto.IdProducto = reader.GetInt32(0);
                        Producto.Descripcion = reader.GetString(1);
                        Producto.IdMarca = reader.GetInt32(2);
                        Producto.IdModelo = reader.GetInt32(3);
                        Producto.IdAlmacen = reader.GetInt32(4);
                        Producto.Serie = reader.GetString(5);
                        Producto.Precio = reader.GetDecimal(6);
                        Producto.Existencia = reader.GetInt64(7);
                    }

                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return Producto;
        }

        public string CrearProducto(Producto model)
        {
            string query = "EXEC spGuardarProducto @pDescripcion, @pMarca, @pModelo, @pAlmacen, @pSerie, @pPrecio";

            using (SqlConnection connection = new SqlConnection(Encript.Desencriptar(CadenaConexion)))
            {                
                try
                {
                    SqlCommand command = new SqlCommand(query, connection)
                    {
                        CommandText = "spGuardarProducto",
                        CommandType = CommandType.StoredProcedure
                    };

                    // Anadimos los parametros.
                    command.Parameters.AddWithValue("@pDescripcion", model.Descripcion);
                    command.Parameters.AddWithValue("@pMarca", model.IdMarca);
                    command.Parameters.AddWithValue("@pModelo", model.IdModelo);
                    command.Parameters.AddWithValue("@pAlmacen", model.IdAlmacen);
                    command.Parameters.AddWithValue("@pSerie", model.Serie);
                    command.Parameters.AddWithValue("@pPrecio", model.Precio);

                    connection.Open();
                    command.ExecuteNonQuery();                    
                    connection.Close();

                    return "Ok";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        public string ActualizarProducto(Producto model)
        {
            string query = "EXEC spActualizarProducto @pId, @pDescripcion, @pMarca, @pModelo, @pAlmacen, @pSerie, @pPrecio, @pExistencia";

            using (SqlConnection connection = new SqlConnection(Encript.Desencriptar(CadenaConexion)))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, connection)
                    {
                        CommandText = "spActualizarProducto",
                        CommandType = CommandType.StoredProcedure
                    };

                    // Anadimos los parametros.
                    command.Parameters.AddWithValue("@pId", model.IdProducto);
                    command.Parameters.AddWithValue("@pDescripcion", model.Descripcion);
                    command.Parameters.AddWithValue("@pMarca", model.IdMarca);
                    command.Parameters.AddWithValue("@pModelo", model.IdModelo);
                    command.Parameters.AddWithValue("@pAlmacen", model.IdAlmacen);
                    command.Parameters.AddWithValue("@pSerie", model.Serie);
                    command.Parameters.AddWithValue("@pPrecio", model.Precio);
                    command.Parameters.AddWithValue("@pExistencia", model.Existencia);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                    return "Ok";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        public string EliminarProducto(int id)
        {
            string query = "EXEC spEliminarProducto @pId";

            using (SqlConnection connection = new SqlConnection(Encript.Desencriptar(CadenaConexion)))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, connection)
                    {
                        CommandText = "spEliminarProducto",
                        CommandType = CommandType.StoredProcedure
                    };

                    // Anadimos los parametros.
                    command.Parameters.AddWithValue("@pId", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                    return "Ok";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
    }
}