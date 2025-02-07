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

    }
}
