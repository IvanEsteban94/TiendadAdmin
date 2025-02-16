using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidy
{
   public  class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public string ConfirmarContrasena { get; set; }
        public bool EsAdministrador { get; set; }
        public bool Activo { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public bool Reestablecer { get; set; }
        public string Clave { get; set; }
        public string ConfirmarClave { get; set; }
    }
}
