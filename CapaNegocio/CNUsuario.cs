using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidy;
using CapaDato;
using System.Data.SqlClient;
using System.Data;

namespace CapaNegocio
{
    public class CNUsuario
    {
        private CD_Usuario objcapaDato = new CD_Usuario();

        public List<Usuario> Listar()
        {
            // Aquí deberías conectar a la base de datos y devolver los usuarios
            return objcapaDato.Listar(); // Simulación de datos
        }

        // Registrar un nuevo usuario
        public bool Registrar(Usuario usuario)
        {
            if (usuario == null || string.IsNullOrWhiteSpace(usuario.Nombre) || string.IsNullOrWhiteSpace(usuario.Correo))
            {
                return false;
            }

            try
            {
                return objcapaDato.Agregar(usuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar usuario: {ex.Message}");
                return false;
            }
        }

        // Editar un usuario existente
        public bool Editar(Usuario usuario)
        {
            if (usuario == null || usuario.IdUsuario <= 0 || string.IsNullOrWhiteSpace(usuario.Nombre) || string.IsNullOrWhiteSpace(usuario.Correo))
            {
                return false;
            }

            try
            {
                return objcapaDato.Editar(usuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al editar usuario: {ex.Message}");
                return false;
            }
        }

        // Eliminar un usuario por ID
        public bool Eliminar(int id)
        {
            if (id <= 0)
            {
                return false;
            }

            try
            {
                return objcapaDato.Eliminar(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar usuario: {ex.Message}");
                return false;
            }
        }

        // Obtener un usuario por ID
        public Usuario Obtener(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            try
            {
                return objcapaDato.Obtener(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener usuario: {ex.Message}");
                return null;
            }
        }


        public bool CambiarClave(int idUsuario, string nuevaclave, out string Mensaje)
        {
            return objcapaDato.CambiarClave(idUsuario, nuevaclave, out Mensaje);
        }
        public int ReestableceClave(int idUsuario, string correo, out string Mensaje)
        {
            Mensaje = string.Empty;
            string nuevaclave = CNRecurso.GeneralClave();
            bool resultado = objcapaDato.ReestableceClave(idUsuario, CNRecurso.ConvertirSha256(nuevaclave), out Mensaje);

            if (resultado)
            {
                string asunto = "Contraseña Reestablecida";
                string mensajeCorreo = "<h3>Su cuenta fue creada correctamente</h3></br><p>Su contraseña para acceder es: !clave!</p>";
                mensajeCorreo = mensajeCorreo.Replace("!clave!", nuevaclave);

                bool respuesta = CNRecurso.EnviarCorreo(correo, asunto, mensajeCorreo);

                if (respuesta)
                {
                    return 1; // Devuelve 1 en lugar de true
                }
                else
                {
                    Mensaje = "No se pudo enviar el correo";
                    return 0; // Devuelve 0 en lugar de false
                }
            }
            else
            {
                Mensaje = "No se pudo reestablecer la contraseña";
                return 0; // Devuelve 0 en lugar de false
            }
        }

    }
}
