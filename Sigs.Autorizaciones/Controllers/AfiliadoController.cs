using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Text;
using Sigs.AutorizacionesOnline.Models;
using Sigs.AutorizacionesOnline.Models.Services;
using System.Web.Mvc;

namespace Sigs.AutorizacionesOnline.Controllers
{
    public class AfiliadoController : Controller
    {

        private IAfiliadoService service;

        public AfiliadoController(IAfiliadoService afiliadoService)
        {
            this.service = afiliadoService;
        }

        public ActionResult Get(decimal id)
        {
            Afiliado afiliado = service.AfiliadoById(id);

            if (afiliado == null)
            {

                HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.NotFound);
                throw new HttpResponseException(msg);
            }
            else
            {
                var obj = Projectar(afiliado);

                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        public dynamic Projectar(Afiliado afiliado)
        {
            return new
            {
                afiliado.Id,
                Nombre = afiliado.NombreCompleto,
                afiliado.Edad,
                afiliado.Sexo,
                Foto = "",
                PuedeAutoriar = true
            };
        }
    }
}