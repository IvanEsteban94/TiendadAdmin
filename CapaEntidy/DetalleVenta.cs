using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidy
{
    public class DetalleVenta
    {

        public int IdDetalleCompra { get; set; }
        public int IdCompra { get; set; }
        public int IdProducto { get; set; }
        public Producto oProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }

    }
}
