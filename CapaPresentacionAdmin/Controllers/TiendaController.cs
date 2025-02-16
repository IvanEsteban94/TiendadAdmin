using CapaEntidy;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace CapaPresentacionAdmin.Controllers
{
    public class TiendaController : Controller
    {
        // Llave para guardar el carrito en Session
        private const string CartSessionKey = "Carrito";

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Producto()
        {
            return View();
        }


        public ActionResult Compras()
        {
            return View();
        }
        public ActionResult DetalleCarrito() {
            return View();
        }

        [HttpGet]
        public JsonResult ListarProducto(int idcategoria = 0)
        {
            List<Producto> productos = new CNProducto().Listar();

            if (idcategoria != 0)
            {
                productos = productos.Where(x => x.oCategoria.IdCategoria == idcategoria).ToList();
            }

            return Json(new { data = productos }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarCategoria()
        {
            List<Categoria> categorias = new CNCategoria().Listar();
            return Json(new { data = categorias }, JsonRequestBehavior.AllowGet);
        }

      
    }

}
