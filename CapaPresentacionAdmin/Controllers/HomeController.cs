using CapaEntidy;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        [HttpGet]
        public JsonResult ListaUsuario()
        {
            List<Usuario> oLista = new List<Usuario>();
            oLista = new CNUsuario().Listar();
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
            {
                return Json(new { success = false, message = "Datos inválidos." });
            }

            bool respuesta = new CNUsuario().Registrar(usuario);

            return Json(new
            {
                success = respuesta,
                message = respuesta ? "Usuario agregado correctamente." : "Error al agregar usuario."
            });
        }

        [HttpGet]
        public ActionResult EditarUsuario(int id)
        {
            Usuario usuario = new CNUsuario().Obtener(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return PartialView(usuario);
        }

        [HttpPost]
        public JsonResult EditarUsuario(Usuario usuario)
        {
            if (usuario == null || usuario.IdUsuario == 0)
            {
                return Json(new { success = false, message = "Datos inválidos." });
            }

            bool respuesta = new CNUsuario().Editar(usuario);

            return Json(new
            {
                success = respuesta,
                message = respuesta ? "Usuario actualizado correctamente." : "Error al actualizar usuario."
            });
        }

        [HttpPost]
        public JsonResult EliminarUsuario(int id)
        {
            if (id <= 0)
            {
                return Json(new { success = false, message = "ID inválido." });
            }

            bool respuesta = new CNUsuario().Eliminar(id);

            return Json(new
            {
                success = respuesta,
                message = respuesta ? "Usuario eliminado correctamente." : "Error al eliminar usuario."
            });
        }
    }
}
