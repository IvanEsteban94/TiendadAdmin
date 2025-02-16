using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CapaEntidy;
using CapaNegocio; // Importa la capa de negocio

namespace CapaPresentacionAdmin.Controllers
{
    public class CarritoController : Controller
    {
        private CNProducto _productoNegocio = new CNProducto();

        // Acción para mostrar el carrito
        public ActionResult Index()
        {
            var carrito = Session["Carrito"] as List<Producto> ?? new List<Producto>();
            return View(carrito);
        }

        // Acción para agregar un producto al carrito
        public ActionResult Agregar(int id)
        {
            var carrito = Session["Carrito"] as List<Producto> ?? new List<Producto>();

            Producto producto = _productoNegocio.ObtenerProductoPorId(id);
            if (producto != null)
            {
                carrito.Add(producto);
                Session["Carrito"] = carrito;
            }

            return RedirectToAction("Index");
        }

        // Acción para eliminar un producto del carrito
        public ActionResult Eliminar(int id)
        {
            var carrito = Session["Carrito"] as List<Producto>;
            if (carrito != null)
            {
                var producto = carrito.FirstOrDefault(p => p.IdProducto == id);
                if (producto != null)
                {
                    carrito.Remove(producto);
                    Session["Carrito"] = carrito;
                }
            }

            return RedirectToAction("Index");
        }
    }
}
