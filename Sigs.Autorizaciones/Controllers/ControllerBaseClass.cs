using Sigs.AutorizacionesOnline.Models;
using Sigs.AutorizacionesOnline.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Sigs.AutorizacionesOnline.Controllers
{
    public class ControllerBaseClass : Controller
    {
        public ArsDataContext Contextt;

        public ControllerBaseClass()
        {
            Contextt = new ArsDataContext();
        }


        public Usuario Usuario
        {
            get
            {
                return Contextt.Usuarios.SingleOrDefault(p => p.Login == HttpContext.User.Identity.Name);
            }
        }
    }
}
