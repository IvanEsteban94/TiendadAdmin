using CapaEntidy;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDato
{
    public class CD_CATEGORIA
    {
        public List<Categoria> Listar()
        {
            List<Categoria> Lista = new List<Categoria>();
            try
            {
                using (SqlConnection cn = new SqlConnection(conexion.Cn))
                {
                    string sql = "select * from CATEGORIA";
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.CommandType = CommandType.Text;
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Categoria
                            {
                                IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Active = Convert.ToBoolean(dr["Active"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Lista = new List<Categoria>();
                Console.WriteLine("Error al listar Categoria: " + ex.Message);
            }

            return Lista;
        }

        public bool Agregar(Categoria categoria, out string Mensaje)
        {
            bool respuesta = false;
            int idautogenerado = 0;
            Mensaje = "";

            try
            {
                using (SqlConnection cn = new SqlConnection(conexion.Cn))
                {
                    string sql = "sp_RegistrarCategoria";
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Descripcion", categoria.Descripcion);
                    cmd.Parameters.AddWithValue("@Activo", categoria.Active);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cn.Open();
                    cmd.ExecuteNonQuery();

                    idautogenerado = Convert.ToInt32(cmd.Parameters["@Resultado"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();

                    respuesta = idautogenerado > 0;
                }
            }
            catch (Exception ex)
            {
                Mensaje = "Error al registrar categoría: " + ex.Message;
                Console.WriteLine(Mensaje);
            }

            return respuesta;
        }

        public bool Editar(Categoria categoria, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = "";

            try
            {
                using (SqlConnection cn = new SqlConnection(conexion.Cn))
                {
                    string sql = "sp_EditarCategoria";
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdCategoria", categoria.IdCategoria);
                    cmd.Parameters.AddWithValue("@Descripcion", categoria.Descripcion);
                    cmd.Parameters.AddWithValue("@Activo", categoria.Active);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cn.Open();
                    cmd.ExecuteNonQuery();

                    int resultado = Convert.ToInt32(cmd.Parameters["@Resultado"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();

                    respuesta = resultado > 0;
                }
            }
            catch (Exception ex)
            {
                Mensaje = "Error al editar categoría: " + ex.Message;
                Console.WriteLine(Mensaje);
            }

            return respuesta;
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = "";

            try
            {
                using (SqlConnection cn = new SqlConnection(conexion.Cn))
                {
                    string sql = "sp_EliminarCategoria";
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdCategoria", id);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cn.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = "Error al eliminar categoría: " + ex.Message;
            }

            return respuesta;
        }
        public Categoria ObtenerPorId(int id)
        {
            Categoria categoria = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(conexion.Cn))
                {
                    string sql = "SELECT * FROM CATEGORIA WHERE IdCategoria = @IdCategoria";
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@IdCategoria", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            categoria = new Categoria
                            {
                                IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Active = Convert.ToBoolean(dr["Active"])
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener categoría: " + ex.Message);
            }

            return categoria;
        }
       
    }
}
