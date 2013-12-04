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
            ViewBag.Pagina = "Consulta";

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

        [HttpPost]
        public ActionResult Autorizaciones(int? autorizacionId, int? carnet, DateTime? fechaAutorizacionDesde,
            DateTime? fechaAutorizacionHasta, DateTime? fechaServicioDesde, DateTime? fechaServicioHasta)
        {
            bool hasFiltred = false;

            var prestadoraId = Usuario.UsuariosPrestadoras.First().PrestadoraId;

            IQueryable<Autorizacion> query = Contextt.Autorizaciones.Where(p => p.PrestadoraId == prestadoraId);

            if (autorizacionId.HasValue)
            {
                query = query.Where(p => p.Id == autorizacionId);
                hasFiltred = true;
            }
            else
            {
                if (carnet.HasValue)
                {
                    query = query.Where(p => p.Afiliado.Id == carnet);
                    hasFiltred = true;
                }

                if (fechaAutorizacionDesde.HasValue && fechaAutorizacionHasta.HasValue)
                {
                    var hasta = fechaAutorizacionHasta.Value.AddDays(1).AddSeconds(-1);
                    query = query.Where(p => p.FechaAutorizacion >= fechaAutorizacionDesde.Value && p.FechaAutorizacion <= hasta);
                    hasFiltred = true;
                }

                if (fechaServicioDesde.HasValue && fechaServicioHasta.HasValue)
                {
                    var hasta = fechaServicioHasta.Value.AddDays(1).AddSeconds(-1);
                    query = query.Where(p => p.FechaServicio >= fechaServicioDesde.Value && p.FechaServicio <= hasta);
                    hasFiltred = true;
                }
            }

            if (!hasFiltred)
            {
                var hoy = DateTime.Now.Date;
                var ahora = DateTime.Now;
                query = query.Where(p => p.FechaAutorizacion >= hoy && p.FechaAutorizacion <= ahora);
            }

            return Json(query.ToList().Select(p => Project(p)), JsonRequestBehavior.AllowGet);
        }

        dynamic Project(Autorizacion a)
        {
            return new
            {
                Numero = a.Id,
                Carne = a.Afiliado.Id,
                Cantidad = a.Prestaciones.Count,
                TipoAutorizacion = a.TipoAutorizacion.Nombre,
                Solicitado = a.MontoSolicitado,
                Aprobado = a.MontoAprobado,
                a.Afiliado.NombreCompleto,
                FechaAutorizacion = a.FechaAutorizacion.ToShortDateString(),
                FechaServicio = a.FechaServicio.ToShortDateString(),
                a.MontoAprobado,
                a.MontoSolicitado,
                a.Prestadora.Nombre,
                a.Disponible,
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
