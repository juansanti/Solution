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

            if (up != null)
            {
                Prestadora = up.Prestadora.Nombre;
            }
            else
            {
                Prestadora = "Admin";
            }
            ViewBag.Usuario = Usuario;
            ViewBag.Prestadora = Prestadora;
            ViewBag.Pagina = "Autorizar";

            return View();
        }

        public HomeController()
        {

        }


    }
}
