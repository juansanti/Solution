using Sigs.AutorizacionesOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sigs.AutorizacionesOnline.Controllers
{
    public class ConsultasController : ControllerBaseClass
    {

        public ConsultasController()
        {
           
        }
        [HttpGet]
        public ActionResult Reportes()
        {
            ViewBag.Usuario = Usuario;
            ViewBag.Prestadora = Prestadora;

            return View();
        }

        [HttpPost]
        public ActionResult AutorizacionesDelDia(int usuarioId, DateTime fecha)
        {
            var autorizaciones = Contextt.Autorizaciones;

            if (autorizaciones == null)
            {
                throw new HttpException(404, string.Format("No hay autorizaciones en fecha {0}", fecha.ToShortDateString()));
            }

            var result = autorizaciones.Select(p => Project(p));

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        dynamic Project(Autorizacion a)
        {
            return new
            {
                a.Id,
                AfiliadoId = a.Afiliado.Id,
                a.Afiliado.NombreCompleto,
                a.FechaAutorizacion,
                a.FechaServicio,
                a.MontoAprobado,
                a.MontoSolicitado,
                a.Prestadora.Nombre,
                a.RulesAppliances,
                Prestaciones = a.Prestaciones.Select(p => Project(p))
            };
        }

        dynamic Project(PrestacionAutorizacion p)
        {
            return new
            {
                p.Cantidad,
                p.Tarifa,
                p.CoPago,
                p.Aprobado,
                p.RulesAppliances
            };
        }
    }
}
