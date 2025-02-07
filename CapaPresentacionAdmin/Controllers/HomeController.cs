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
            return Json(new { data=oLista} ,JsonRequestBehavior.AllowGet);
        }
    }

}