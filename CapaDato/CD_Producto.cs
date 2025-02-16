using CapaEntidy;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Text;

namespace CapaDato
{
  
    public class CD_Producto
    {
        public bool DisminuirStock(int idProducto, int cantidad)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(conexion.Cn))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE PRODUCTO SET Stock = Stock - @Cantidad WHERE IdProducto = @IdProducto AND Stock >= @Cantidad", cn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@IdProducto", idProducto);
                    cmd.Parameters.AddWithValue("@Cantidad", cantidad);

                    cn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al disminuir stock: " + ex.Message);
                return false;
            }
        }

        public List<Producto> Listar()
        {
            List<Producto> Lista = new List<Producto>();
            try
            {
                using (SqlConnection cn = new SqlConnection(conexion.Cn))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("SELECT p.IdProducto, p.Nombre, p.Descripcion, ");
                    sb.AppendLine("m.IdMarca, m.Descripcion AS DesMarca, ");
                    sb.AppendLine("c.IdCategoria, c.Descripcion AS DesCategoria, ");
                    sb.AppendLine("p.Precio, p.Stock, p.RutaImagen, p.NombreImagen, p.Activo ");
                    sb.AppendLine("FROM PRODUCTO p ");
                    sb.AppendLine("INNER JOIN MARCA m ON m.IdMarca = p.IdMarca ");
                    sb.AppendLine("INNER JOIN CATEGORIA c ON c.IdCategoria = p.IdCategoria");


                    SqlCommand cmd = new SqlCommand(sb.ToString(), cn);
                    
                    cmd.CommandType = CommandType.Text;
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Producto
                            {
                                IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                oMarca = new Marca
                                {
                                    IdMarca = Convert.ToInt32(dr["IdMarca"]),
                                    Descripcion = dr["DesMarca"].ToString() // Aquí se asigna la descripción de la marca
                                },
                                oCategoria = new Categoria
                                {
                                    IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                                    Descripcion = dr["DesCategoria"].ToString() // Aquí se asigna la descripción de la categoría
                                },
                                Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-ES")),
                                Stock = Convert.ToInt32(dr["Stock"]),
                                RutaImagen = dr["RutaImagen"].ToString(),
                                NombreImagen = dr["NombreImagen"].ToString(),
                                Activo = Convert.ToBoolean(dr["Activo"])
                            });

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Lista = new List<Producto>();
                Console.WriteLine("Error al listar Producto: " + ex.ToString());
            }

            return Lista;
        }
       
        public int Agregar(Producto producto, out string mensaje)
        {
            mensaje = string.Empty;
            int idGenerado = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(conexion.Cn))
                {
                    SqlCommand cmd = new SqlCommand("usp_RegistrarProducto", cn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("@IdMarca", producto.oMarca.IdMarca);
                    cmd.Parameters.AddWithValue("@IdCategoria", producto.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@Stock", producto.Stock);
                    cmd.Parameters.AddWithValue("@RutaImagen", producto.RutaImagen);
                    cmd.Parameters.AddWithValue("@NombreImagen", producto.NombreImagen);
                    cmd.Parameters.AddWithValue("@Activo", producto.Activo);

                    SqlParameter outputIdParam = new SqlParameter("@IdProducto", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputIdParam);

                    cn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        idGenerado = Convert.ToInt32(outputIdParam.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error al agregar el producto: {ex.Message}";
                // Aquí puedes registrar el error o realizar cualquier otra acción necesaria
                Console.WriteLine(mensaje);
            }

            return idGenerado;
        }


        public bool Editar(Producto producto, out string mensaje)
        {
            bool respuesta = false;
            mensaje = "";

            try
            {
                using (SqlConnection cn = new SqlConnection(conexion.Cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarProducto", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdProducto", producto.IdProducto);
                    cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                   
                    cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@Stock", producto.Stock);
                    cmd.Parameters.AddWithValue("@Activo", producto.Activo);

                    cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cn.Open();
                    cmd.ExecuteNonQuery();

                    int resultado = Convert.ToInt32(cmd.Parameters["@Resultado"].Value);
                    mensaje = cmd.Parameters["@Mensaje"].Value.ToString();

                    respuesta = resultado > 0;
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al editar Producto: " + ex.Message;
            }

            return respuesta;
        }

        public bool Eliminar(int id, out string mensaje)
        {
            bool respuesta = false;
            mensaje = "";

            try
            {
                using (SqlConnection cn = new SqlConnection(conexion.Cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarProducto", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdProducto", id);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cn.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToInt32(cmd.Parameters["@Resultado"].Value) > 0;
                    mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al eliminar Producto: " + ex.Message;
            }

            return respuesta;
        }

        public bool GuardarDatosImagen(Producto producto, out string mensaje)
        {
            bool resultado = false;
            mensaje = "";

            try
            {
                using (SqlConnection cn = new SqlConnection(conexion.Cn))
                {
                    string sql = "UPDATE PRODUCTO SET RutaImagen = @RutaImagen, NombreImagen = @NombreImagen WHERE IdProducto = @IdProducto";
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@RutaImagen", producto.RutaImagen);
                    cmd.Parameters.AddWithValue("@NombreImagen", producto.NombreImagen);
                    cmd.Parameters.AddWithValue("@IdProducto", producto.IdProducto);

                    cn.Open();
                    resultado = cmd.ExecuteNonQuery() > 0;

                    if (!resultado)
                    {
                        mensaje = "No se pudo actualizar la imagen.";
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al guardar imagen: " + ex.Message;
            }

            return resultado;
        }
        public Producto ObtenerDetalle(int idProducto)
        {
            Producto producto = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(conexion.Cn))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("SELECT p.IdProducto, p.Nombre, p.Descripcion, ");
                    sb.AppendLine("m.IdMarca, m.Descripcion AS DesMarca, ");
                    sb.AppendLine("c.IdCategoria, c.Descripcion AS DesCategoria, ");
                    sb.AppendLine("p.Precio, p.Stock, p.RutaImagen, p.NombreImagen, p.Activo ");
                    sb.AppendLine("FROM PRODUCTO p ");
                    sb.AppendLine("INNER JOIN MARCA m ON m.IdMarca = p.IdMarca ");
                    sb.AppendLine("INNER JOIN CATEGORIA c ON c.IdCategoria = p.IdCategoria ");
                    sb.AppendLine("WHERE p.IdProducto = @IdProducto");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), cn);
                    cmd.Parameters.AddWithValue("@IdProducto", idProducto);
                    cmd.CommandType = CommandType.Text;
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            producto = new Producto
                            {
                                IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                oMarca = new Marca
                                {
                                    IdMarca = Convert.ToInt32(dr["IdMarca"]),
                                    Descripcion = dr["DesMarca"].ToString()
                                },
                                oCategoria = new Categoria
                                {
                                    IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                                    Descripcion = dr["DesCategoria"].ToString()
                                },
                                Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-ES")),
                                Stock = Convert.ToInt32(dr["Stock"]),
                                RutaImagen = dr["RutaImagen"].ToString(),
                                NombreImagen = dr["NombreImagen"].ToString(),
                                Activo = Convert.ToBoolean(dr["Activo"])
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                producto = null;
                Console.WriteLine("Error al obtener detalles del Producto: " + ex.ToString());
            }

            return producto;
        }
        public Producto ObtenerProductoPorId(int idProducto)
        {
            Producto producto = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(conexion.Cn))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("SELECT p.IdProducto, p.Nombre, p.Descripcion, ");
                    sb.AppendLine("m.IdMarca, m.Descripcion AS DesMarca, ");
                    sb.AppendLine("c.IdCategoria, c.Descripcion AS DesCategoria, ");
                    sb.AppendLine("p.Precio, p.Stock, p.RutaImagen, p.NombreImagen, p.Activo ");
                    sb.AppendLine("FROM PRODUCTO p ");
                    sb.AppendLine("INNER JOIN MARCA m ON m.IdMarca = p.IdMarca ");
                    sb.AppendLine("INNER JOIN CATEGORIA c ON c.IdCategoria = p.IdCategoria ");
                    sb.AppendLine("WHERE p.IdProducto = @IdProducto");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), cn);
                    cmd.Parameters.AddWithValue("@IdProducto", idProducto);
                    cmd.CommandType = CommandType.Text;
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            producto = new Producto
                            {
                                IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                oMarca = new Marca
                                {
                                    IdMarca = Convert.ToInt32(dr["IdMarca"]),
                                    Descripcion = dr["DesMarca"].ToString()
                                },
                                oCategoria = new Categoria
                                {
                                    IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                                    Descripcion = dr["DesCategoria"].ToString()
                                },
                                Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-ES")),
                                Stock = Convert.ToInt32(dr["Stock"]),
                                RutaImagen = dr["RutaImagen"].ToString(),
                                NombreImagen = dr["NombreImagen"].ToString(),
                                Activo = Convert.ToBoolean(dr["Activo"])
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                producto = null;
                Console.WriteLine("Error al obtener el producto por ID: " + ex.ToString());
            }

            return producto;
        }


    }
}
