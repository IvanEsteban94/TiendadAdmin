using CapaDato;
using CapaEntidy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CapaNegocio
{
    public class CNCarrito
    {
        private List<CarritoItem> carrito = new List<CarritoItem>(); // Lista para manejar el carrito de compras
        private CD_Producto datosProducto = new CD_Producto(); // Acceso a la capa de datos de productos

        // Agregar producto al carrito
        public bool AgregarAlCarrito(int idProducto, int cantidad)
        {
            Producto producto = datosProducto.ObtenerProductoPorId(idProducto);

            if (producto != null && producto.Stock >= cantidad) // Verifica stock disponible
            {
                var itemExistente = carrito.FirstOrDefault(p => p.Producto.IdProducto == idProducto);

                if (itemExistente != null)
                {
                    itemExistente.Cantidad += cantidad;
                }
                else
                {
                    carrito.Add(new CarritoItem { Producto = producto, Cantidad = cantidad });
                }
                return true;
            }
            return false; // Producto sin stock suficiente
        }

        // Visualizar el carrito de compras
        public List<CarritoItem> ObtenerCarrito()
        {
            return carrito;
        }

      
    public class CarritoItem
    {
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
    }
}
}
