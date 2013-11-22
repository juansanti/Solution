using Sigs.AutorizacionesOnline.Models;
using Sigs.AutorizacionesOnline.Models.Entities;
using Sigs.AutorizacionesOnline.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;

namespace Sigs.AutorizacionesOnline.Controllers
{
    public class BuscarController : Controller
    {
        IAutorizacionService service;
        IAfiliadoService afiliadoService;

        public BuscarController(IAutorizacionService autorizacionService, IAfiliadoService afiliadoService)
        {
            this.afiliadoService = afiliadoService;
            this.service = autorizacionService;
        }

        public ActionResult GetPrestaciones()
        {
            var prestaciones = service.GetPrestacionesPDSS();

            var result = prestaciones.Select(p => new
            {
                DetallePlanId = p.Id,
                CoberturaId = p.Cobertura.Id,
                Nombre = string.Format("{0} {1} ({2})", p.Cobertura.SIMON, p.Cobertura.Nombre, p.SubGrupo.Nombre),
                p.Tarifa,
                p.SubGrupoId,
                p.SubGrupo.GrupoId,
                SubGrupo = p.SubGrupo.Nombre,
                PorcentajeCobertura = p.PorcentajeCobertura / 100,
            });

            return new JsonResult()
            {
                Data = result,
                ContentType = "application/json",
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        [HttpGet]
        public string CheckConnection()
        {
            return string.Format("{0}-{1}", "DaTa", "Source!");
        }
    }
}