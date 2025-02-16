using CapaEntidy;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Newtonsoft.Json;

namespace CapaPresentacionAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Usuario()
        {
            return View();
        }

        public ActionResult Categoria()
        {
           
            return View();
        }

        public ActionResult Marca()
        {
            
            return View();
        }

        public ActionResult Producto()
        {
            
            return View();
        }

        

        // ---------------------------- USUARIOS ----------------------------
        [HttpGet]
        public JsonResult ListaUsuario()
        {
            List<Usuario> oLista = new CNUsuario().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AgregarUsuario()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult AgregarUsuario(Usuario usuario)
        {
            if (usuario == null)
                return Json(new { success = false, message = "Datos inválidos." });

            bool respuesta = new CNUsuario().Registrar(usuario);
            return Json(new { success = respuesta, message = respuesta ? "Usuario agregado correctamente." : "Error al agregar usuario." });
        }

        [HttpGet]
        public ActionResult EditarUsuario(int id)
        {
            Usuario usuario = new CNUsuario().Obtener(id);
            if (usuario == null) return HttpNotFound();
            return PartialView(usuario);
        }

        [HttpPost]
        public JsonResult EditarUsuario(Usuario usuario)
        {
            if (usuario == null || usuario.IdUsuario == 0)
                return Json(new { success = false, message = "Datos inválidos." });

            bool respuesta = new CNUsuario().Editar(usuario);
            return Json(new { success = respuesta, message = respuesta ? "Usuario actualizado correctamente." : "Error al actualizar usuario." });
        }

        [HttpPost]
        public JsonResult EliminarUsuario(int id)
        {
            if (id <= 0)
                return Json(new { success = false, message = "ID inválido." });

            bool respuesta = new CNUsuario().Eliminar(id);
            return Json(new { success = respuesta, message = respuesta ? "Usuario eliminado correctamente." : "Error al eliminar usuario." });
        }
     
        [HttpGet]
        public JsonResult ListaCategoria()
        {
            List<Categoria> oLista = new CNCategoria().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult AgregarCategoria()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult AgregarCategoria(Categoria categoria)
        {
            object resultado;
            string mensaje = string.Empty;
            if (categoria.IdCategoria == 0)
            {
                resultado = new CNCategoria().Registrar(categoria, out mensaje);

            }
            else
            {
                resultado = new CNCategoria().Editar(categoria, out mensaje);



            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult EditarCategoria(int id)
        {
            // Get the category by ID from the business logic layer
            Categoria categoria = new CNCategoria().ObtenerPorId(id);

            // Return the category to the partial view for editing
            return PartialView(categoria);
        }

        [HttpPost]
        public JsonResult EditarCategoria(Categoria categoria)
        {
            object resultado;
            string mensaje = string.Empty;

            // Check if the category ID is valid for editing
            if (categoria.IdCategoria > 0)
            {
                resultado = new CNCategoria().Editar(categoria, out mensaje);
            }
            else
            {
                mensaje = "Invalid category ID";
                resultado = false;
            }

            // Return the result of the editing operation
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        public JsonResult EliminarCategoria(int id)
        {
            bool repuesta = false;
            string mensaje = string.Empty;
            repuesta = new CNCategoria().Eliminar(id, out mensaje);
            return Json(new { resultado = repuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListaMarca()
        {
            List<Marca> oLista = new CNMARCA().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult AgregarMarca()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult GuardarMarca(Marca marca)
        {
            object resultado;
            string mensaje = string.Empty;
            if (marca.IdMarca == 0)
            {
                resultado = new CNMARCA().Registrar(marca, out mensaje);

            }
            else
            {
                resultado = new CNMARCA().Editar(marca, out mensaje);



            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult EditarMarca(Marca marca)
        {
            object resultado;
            string mensaje = string.Empty;

            // Check if the category ID is valid for editing
            if (marca.IdMarca > 0)
            {
                resultado = new CNMARCA().Editar(marca, out mensaje);
            }
            else
            {
                mensaje = "Invalid marca ID";
                resultado = false;
            }

            // Return the result of the editing operation
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        public JsonResult EliminarMarca(int id)
        {
            bool repuesta = false;
            string mensaje = string.Empty;
            repuesta = new CNMARCA().Eliminar(id, out mensaje);
            return Json(new { resultado = repuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListaProducto()
        {
            List<Producto> oLista = new CNProducto().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AgregarProducto()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult GuardarProducto(Producto producto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (producto.IdProducto == 0)
            {
                resultado = new CNProducto().Registrar(producto, out mensaje);
            }
            else
            {
                resultado = new CNProducto().Editar(producto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
    
        [HttpPost]
        public JsonResult EditarProducto(Producto producto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (producto.IdProducto > 0)
            {
                resultado = new CNProducto().Editar(producto, out mensaje);
            }
            else
            {
                mensaje = "Invalid producto ID";
                resultado = false;
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarProducto(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;
            respuesta = new CNProducto().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
    }


}

 

