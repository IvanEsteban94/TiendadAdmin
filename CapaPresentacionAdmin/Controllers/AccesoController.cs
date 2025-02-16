using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidy;
using CapaNegocio;

namespace CapaPresentacionAdmin.Controllers
{
    public class AccesoController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CambiarClave()
        {
            return View();
        }

        public ActionResult ReestableceClave()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
            // Registrar los datos en el log
            System.Diagnostics.Debug.WriteLine($"Correo: {correo}, Clave: {clave}");

            // Obtener la lista de usuarios
            var usuarios = new CNUsuario().Listar();
            System.Diagnostics.Debug.WriteLine($"Total Usuarios: {usuarios.Count}");

            // Convertir la clave a SHA256
            string claveHash = CNRecurso.ConvertirSha256(clave);
            System.Diagnostics.Debug.WriteLine($"Clave Hash: {claveHash}");

            // Buscar el usuario por correo y clave
            Usuario oUsuario = usuarios.FirstOrDefault(u => u.Correo == correo && u.Clave == claveHash);

            if (oUsuario == null)
            {
                ViewBag.Error = "Correo o contraseña no correcta";
                return View();
            }

            // Almacenar información del usuario en la sesión
            Session["Usuario"] = oUsuario;

            // Redirigir a la acción Index del controlador Home
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Registrarse()
        {
            return View(new Usuario()
            {
                Nombres = "",
                Apellidos = "",
                Correo = "",
                Clave = "",
                ConfirmarClave = ""
            });
        }

        [HttpPost]
        public ActionResult Registrarse(string nombres, string apellidos, string correo, string clave, string confirmarClave)
        {
            Usuario oUsuario = new Usuario()
            {
                Nombres = nombres,
                Apellidos = apellidos,
                Correo = correo,
                Clave = clave,
                ConfirmarClave = confirmarClave,
                EsAdministrador = false
            };

            if (clave != confirmarClave)
            {
                ViewBag.Error = "Las contraseñas no coinciden";
                return View(oUsuario);
            }

            // Convertir clave a SHA256 antes de registrar
            oUsuario.Clave = CNRecurso.ConvertirSha256(clave);

            bool registroExitoso = new CNUsuario().Registrar(oUsuario);

            if (!registroExitoso)
            {
                ViewBag.Error = "Error al registrar";
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Acceso");
            }
        }
    }
}
