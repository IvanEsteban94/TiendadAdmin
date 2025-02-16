using CapaDato;
using CapaEntidy;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Text;

namespace CapaNegocio
{
    public class CNProducto
    {
        private CD_Producto objcapaDato = new CD_Producto();

        public List<Producto> Listar()
        {
            return objcapaDato.Listar();
        }



        public int Registrar(Producto producto, out string mensaje)
        {
            mensaje = "";

            if (string.IsNullOrWhiteSpace(producto.Nombre))
            {
                mensaje = "El nombre del producto no puede estar vacío.";
                return 0;
            }
            else if (string.IsNullOrWhiteSpace(producto.Descripcion))
            {
                mensaje = "La descripción del producto no puede estar vacía.";
                return 0;
            }
            else if (producto.oMarca == null || producto.oMarca.IdMarca == 0)
            {
                mensaje = "Debe seleccionar una marca válida.";
                return 0;
            }
           
            else if (producto.Precio <= 0)
            {
                mensaje = "Debe ingresar un precio válido para el producto.";
                return 0;
            }
            else if (producto.Stock < 0)
            {
                mensaje = "El stock del producto no puede ser negativo.";
                return 0;
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                // Aquí debes asegurarte de que el método Agregar devuelva un int
                return objcapaDato.Agregar(producto, out mensaje);
            }
            return 0; // Retornar 0 si hay un error
        }


        public bool Editar(Producto producto, out string mensaje)
        {
            mensaje = "";

            if (string.IsNullOrWhiteSpace(producto.Nombre))
            {
                mensaje = "El nombre del producto no puede estar vacío.";
            }
            else if (string.IsNullOrWhiteSpace(producto.Descripcion))
            {
                mensaje = "La descripción del producto no puede estar vacía.";
            }
            else if (producto.oMarca == null || producto.oMarca.IdMarca == 0)
            {
                mensaje = "Debe seleccionar una marca válida.";
            }
            else if (producto.oCategoria == null || producto.oCategoria.IdCategoria == 0)
            {
                mensaje = "Debe seleccionar una categoría válida.";
            }
            else if (producto.Precio <= 0)
            {
                mensaje = "Debe ingresar un precio válido para el producto.";
            }
            else if (producto.Stock < 0)
            {
                mensaje = "El stock del producto no puede ser negativo.";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                return objcapaDato.Editar(producto, out mensaje);
            }
            return false;
        }

        public bool GuardarDatosImagen(Producto producto, out string mensaje)
        {
            return objcapaDato.GuardarDatosImagen(producto, out mensaje);
        }

        public bool Eliminar(int id, out string mensaje)
        {
            return objcapaDato.Eliminar(id, out mensaje);
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
