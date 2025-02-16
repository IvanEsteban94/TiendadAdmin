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
            return objcapaDato.Listar();
        }

        public bool Registrar(Categoria categoria, out string mensaje)
        {
            mensaje = "";

            if (string.IsNullOrEmpty(categoria.Descripcion) || string.IsNullOrWhiteSpace(categoria.Descripcion))
            {
                mensaje = "La descripción de la categoría no puede estar vacía.";
                return false;
            }

            return objcapaDato.Agregar(categoria, out mensaje);
        }

        public bool Editar(Categoria categoria, out string mensaje)
        {
            mensaje = "";

            if (string.IsNullOrEmpty(categoria.Descripcion) || string.IsNullOrWhiteSpace(categoria.Descripcion))
            {
                mensaje = "La descripción de la categoría no puede estar vacía.";
                return false;
            }

            return objcapaDato.Editar(categoria, out mensaje);
        }

        public bool Eliminar(int id, out string mensaje)
        {
            return objcapaDato.Eliminar(id, out mensaje);
        }

        public Categoria ObtenerPorId(int id)
        {
            return objcapaDato.ObtenerPorId(id);
        }
    }
}
