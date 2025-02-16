using CapaDato;
using CapaEntidy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CNMARCA
    {
        private CD_MARCA objcapaDato = new CD_MARCA();

        public List<Marca> Listar()
        {
            // Aquí deberías conectar a la base de datos y devolver los usuarios
            return objcapaDato.Listar(); // Simulación de datos
        }

        public bool Registrar(Marca Marca, out string Mensaje)
        {
            Mensaje = ""; // Inicializar la variable Mensaje

            // Verificar si la descripción de la categoría está vacía o nula
            if (string.IsNullOrEmpty(Marca.Descripcion) || string.IsNullOrWhiteSpace(Marca.Descripcion))
            {
                Mensaje = "La descripcion de la Marca no puede ser vacio";
            }

            // Si no hay mensaje de error, proceder con la inserción
            if (string.IsNullOrEmpty(Mensaje))
            {
                return false;
            }
            else
            {
                return objcapaDato.Agregar(Marca, out Mensaje);

            }
        }

        // Editar un usuario existente
        public bool Editar(Marca Marca, out string Mensaje)
        {
            Mensaje = ""; // Inicializar la variable Mensaje

            // Verificar si la descripción de la categoría está vacía o nula
            if (string.IsNullOrEmpty(Marca.Descripcion) || string.IsNullOrWhiteSpace(Marca.Descripcion))
            {
                Mensaje = "La descripcion de la categoria no puede ser vacio";
            }
            // Si no hay mensaje de error, proceder con la inserción
            if (string.IsNullOrEmpty(Mensaje))
            {
                return objcapaDato.Editar(Marca, out Mensaje);
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
       

    }
}
