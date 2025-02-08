using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidy;
using CapaDato;
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
    }

}
