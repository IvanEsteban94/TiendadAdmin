using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace CapaDato
{
   public  class conexion
    {
        public static string Cn = ConfigurationManager.ConnectionStrings["Cadena"].ToString();
    }
}
