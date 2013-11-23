using Sigs.AutorizacionesOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Sigs.AutorizacionesOnline.Controllers
{
    public class SeguridadController : Controller
    {
        public ArsDataContext Contextt { get; set; }

        public SeguridadController()
        {
            Contextt = new ArsDataContext();
        }

        public ActionResult IniciarSesion()
        {
            if (this.HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect("/Home/Index");
            }

            return View();
        }

        [HttpPost]
        public ActionResult IniciarSesion(LoginModel model)
        {
            var usuario = Contextt.Usuarios.SingleOrDefault(p => (p.Login == model.UserName || p.Email == model.UserName) && p.Password == model.Password);

            if (usuario != null)
            {
                FormsAuthentication.SetAuthCookie(usuario.Login, true);
            }
            else
            {
                return Redirect("/Seguridad/IniciarSesion");
            }

            return Redirect("/Home/Index");
        }

        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();

            return Redirect("/");
        }
    }
}
