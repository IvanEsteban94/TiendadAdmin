using CapaEntidy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using BCrypt.Net;

namespace CapaDato
{
    public class CD_Usuario
    {
        public List<Usuario> Listar()
        {
            List<Usuario> Lista = new List<Usuario>();
            try
            {
                using (SqlConnection cn = new SqlConnection(conexion.Cn))
                {
                    string sql = "SELECT IdUsuario, Nombre, Apellido, Correo, Clave, Reestablecer, Activo FROM USUARIO";
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.CommandType = CommandType.Text;
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Usuario
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Nombre = dr["Nombre"].ToString(),
                                Apellido = dr["Apellido"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                Reestablecer = Convert.ToBoolean(dr["Reestablecer"].ToString()),
                                Activo = Convert.ToBoolean(dr["Activo"].ToString())
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Lista = new List<Usuario>();
                Console.WriteLine("Error al listar usuarios: " + ex.Message);
            }

            return Lista;
        }

        public bool Agregar(Usuario usuario)
        {
            bool respuesta = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(conexion.Cn))
                {
                    string sql = "INSERT INTO USUARIO (Nombre, Apellido, Correo, Clave, Reestablecer, Activo) VALUES (@Nombre, @Apellido, @Correo, @Clave, @Reestablecer, @Activo)";
                    SqlCommand cmd = new SqlCommand(sql, cn);

                    // Hashear la clave con BCrypt
                    string claveHasheada = BCrypt.Net.BCrypt.HashPassword(usuario.Clave);

                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                    cmd.Parameters.AddWithValue("@Correo", usuario.Correo);
                    cmd.Parameters.AddWithValue("@Clave", claveHasheada);
                    cmd.Parameters.AddWithValue("@Reestablecer", usuario.Reestablecer);
                    cmd.Parameters.AddWithValue("@Activo", usuario.Activo);

                    cn.Open();
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al agregar usuario: " + ex.Message);
            }

            return respuesta;
        }

        // Método para editar un usuario existente
        public bool Editar(Usuario usuario)
        {
            bool respuesta = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(conexion.Cn))
                {
                    string sql = "UPDATE USUARIO SET Nombre=@Nombre, Apellido=@Apellido, Correo=@Correo, Clave=@Clave, Reestablecer=@Reestablecer, Activo=@Activo WHERE IdUsuario=@IdUsuario";
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                    cmd.Parameters.AddWithValue("@Correo", usuario.Correo);
                    cmd.Parameters.AddWithValue("@Clave", usuario.Clave);
                    cmd.Parameters.AddWithValue("@Reestablecer", usuario.Reestablecer);
                    cmd.Parameters.AddWithValue("@Activo", usuario.Activo);
                    cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                    cn.Open();
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al editar usuario: " + ex.Message);
            }

            return respuesta;
        }

        // Método para eliminar un usuario por ID
        public bool Eliminar(int idUsuario)
        {
            bool respuesta = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(conexion.Cn))
                {
                    string sql = "DELETE FROM USUARIO WHERE IdUsuario = @IdUsuario";
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    cn.Open();
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar usuario: " + ex.Message);
            }

            return respuesta;
        }

        // Método para obtener un usuario por ID
        public Usuario Obtener(int idUsuario)
        {
            Usuario usuario = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(conexion.Cn))
                {
                    string sql = "SELECT IdUsuario, Nombre, Apellido, Correo, Clave, Reestablecer, Activo FROM USUARIO WHERE IdUsuario = @IdUsuario";
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            usuario = new Usuario
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Nombre = dr["Nombre"].ToString(),
                                Apellido = dr["Apellido"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                Reestablecer = Convert.ToBoolean(dr["Reestablecer"].ToString()),
                                Activo = Convert.ToBoolean(dr["Activo"].ToString())
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener usuario: " + ex.Message);
            }

            return usuario;
        }
    }
}
