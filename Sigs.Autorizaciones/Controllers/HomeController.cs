using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sigs.AutorizacionesOnline.Controllers
{
    public class HomeController : ControllerBaseClass
    {
        [Authorize]
        public ActionResult Index()
        {
            var up = Usuario.UsuariosPrestadoras.FirstOrDefault();

            ViewBag.Usuario = Usuario;
            ViewBag.Prestadora = Usuario.UsuariosPrestadoras.First().Prestadora.Nombre;
            ViewBag.Pagina = "Autorizar";

            return View();
        }

        public HomeController()
        {

        }


    }
}
