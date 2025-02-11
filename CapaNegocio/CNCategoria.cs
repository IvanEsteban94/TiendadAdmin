using CapaDato;
using CapaEntidy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CNCategoria
    {
        private CD_CATEGORIA objcapaDato = new CD_CATEGORIA();

        public List<Categoria> Listar()
        {
            // Aquí deberías conectar a la base de datos y devolver los usuarios
            return objcapaDato.Listar(); // Simulación de datos
        }

        public bool Registrar(Categoria Categoria, out string Mensaje)
        {
            Mensaje = ""; // Inicializar la variable Mensaje

            // Verificar si la descripción de la categoría está vacía o nula
            if (string.IsNullOrEmpty(Categoria.Descripcion) || string.IsNullOrWhiteSpace(Categoria.Descripcion))
            {
                Mensaje = "La descripcion de la categoria no puede ser vacio";
            }

            // Si no hay mensaje de error, proceder con la inserción
            if (string.IsNullOrEmpty(Mensaje))
            {
                return objcapaDato.Agregar(Categoria, out Mensaje);
            }
            else
            {
                return false; // Retornar false en caso de error
            }
        }

        // Editar un usuario existente
        public bool Editar(Categoria Categoria, out string Mensaje)
        {
            Mensaje = ""; // Inicializar la variable Mensaje

            // Verificar si la descripción de la categoría está vacía o nula
            if (string.IsNullOrEmpty(Categoria.Descripcion) || string.IsNullOrWhiteSpace(Categoria.Descripcion))
            {
                Mensaje = "La descripcion de la categoria no puede ser vacio";
            }
            // Si no hay mensaje de error, proceder con la inserción
            if (string.IsNullOrEmpty(Mensaje))
            {
                return objcapaDato.Editar(Categoria, out Mensaje);
            }
            else
            {
                return false; // Retornar false en caso de error
            }
        }

        // Eliminar un usuario por ID
        public bool Eliminar(int id, out string Mensaje)
        {

            return objcapaDato.Eliminar(id, out Mensaje);
        }
        public Categoria ObtenerPorId(int id)
        {
            // Return the category that matches the provided ID
            return objcapaDato.ObtenerPorId(id);
        }


    }
}
