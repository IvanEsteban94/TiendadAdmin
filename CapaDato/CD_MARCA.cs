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
    public class CD_MARCA
    {
        public List<Marca> Listar()
        {
            List<Marca> Lista = new List<Marca>();
            try
            {
                using (SqlConnection cn = new SqlConnection(conexion.Cn))
                {
                    string sql = "select * from Marca";
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.CommandType = CommandType.Text;
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Marca
                            {
                                IdMarca = Convert.ToInt32(dr["IdMarca"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Active = Convert.ToBoolean(dr["Active"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Lista = new List<Marca>();
                Console.WriteLine("Error al listar Marca: " + ex.Message);
            }

            return Lista;
        }

        public bool Agregar(Marca Marca, out string Mensaje)
        {
            bool respuesta = false;
            int idautogenerado = 0;
            Mensaje = "";

            try
            {
                using (SqlConnection cn = new SqlConnection(conexion.Cn))
                {
                    string sql = "sp_RegistrarMarca";
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Descripcion", Marca.Descripcion);
                    cmd.Parameters.AddWithValue("@Activo", Marca.Active);
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
                Mensaje = "Error al registrar marca: " + ex.Message;
                Console.WriteLine(Mensaje);
            }

            return respuesta;
        }

        public bool Editar(Marca Marca , out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = "";

            try
            {
                using (SqlConnection cn = new SqlConnection(conexion.Cn))
                {
                    string sql = "sp_EditarMarca";
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdMarca", Marca.IdMarca);
                    cmd.Parameters.AddWithValue("@Descripcion", Marca.Descripcion);
                    cmd.Parameters.AddWithValue("@Activo", Marca.Active);
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
                Mensaje = "Error al editar Marca: " + ex.Message;
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
                    string sql = "sp_EliminarMarca";
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdMarca", id);
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
                Mensaje = "Error al eliminar marca: " + ex.Message;
            }

            return respuesta;
        }
        public Marca ObtenerPorId(int id)
        {
            Marca Marca = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(conexion.Cn))
                {
                    string sql = "SELECT * FROM MARCA WHERE IdMarca = @IdMarca";
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@IdMarca", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Marca = new Marca
                            {
                                IdMarca = Convert.ToInt32(dr["IdMarca"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Active = Convert.ToBoolean(dr["Active"])
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener Marca: " + ex.Message);
            }

            return Marca;
        }
    }
}
