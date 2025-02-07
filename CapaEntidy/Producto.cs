using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CapaEntidy
{
   public  class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdMarca { get; set; }
        public int IdCategoria { get; set; }
        public  Marca oMarca { get; set; }
        public  Categoria oCategoria { get; set; }
        public decimal Precio { get; set; } = 0;
        public int Stock { get; set; }
        public string RutaImagen { get; set; }
        public string NombreImagen { get; set; }
        public bool Activo { get; set; } = true;

    }
}
