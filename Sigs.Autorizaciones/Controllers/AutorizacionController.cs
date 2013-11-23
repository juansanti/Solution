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
using Sigs.AutorizacionesOnline.Models;
using System.Web.Mvc;
using Sigs.AutorizacionesOnline.Models.Entities;
using Sigs.Expertos;
using Autorizaciones.Domain.Entities.Experto;

namespace Sigs.AutorizacionesOnline.Controllers
{
    public class AutorizacionController : ControllerBaseClass
    {
        public ActionResult Solicitar(Autorizacion autorizacion)
        {
            Fill(autorizacion);

            MotorInferencia engine = new MotorInferencia();
            Justificador jf = new Justificador();

            var a = engine.Procesar(autorizacion);

            var result = jf.ExtraerResultado(a);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Guardar(Autorizacion autorizacion)
        {
            AutorizacionService service = new AutorizacionService(new ArsDataContext());

            Init(autorizacion);

            service.Crear(autorizacion);

            if (autorizacion.Id > 0)
            {
                Justificador jf = new Justificador();
                return Json(jf.MostrarAutorizacion(autorizacion), JsonRequestBehavior.AllowGet);
            }
            else
            {
                throw new InvalidOperationException("No se pudo crear la autorizacion");
            }
        }

        void Init(Autorizacion a)
        {
            a.FechaCreacion = DateTime.Now;
            a.FechaAutorizacion = DateTime.Now;
            a.UsuarioId = this.Usuario.Id;

            foreach (var p in a.Prestaciones)
            {
                p.FechaCreacion = DateTime.Now;
                p.UsuarioId = this.Usuario.Id;
            }
        }

        void Fill(Autorizacion a)
        {
            ArsDataContext db = new ArsDataContext();
            a.Afiliado = db.Afiliados.Single(p => p.Id == a.AfiliadoId);
            a.TipoAutorizacion = db.TiposAutorizaciones.Single(p => p.Id == a.TipoAutorizacionId);
            //a.Diagnostico = db.Diagnosticos.Single(p => p.Id == a.DiagnosticoId);
            a.Prestadora = db.Prestadoras.Single(p => p.Id == a.PrestadoraId);

            foreach (var pr in a.Prestaciones)
            {
                pr.Prestacion = db.Prestaciones.Single(p => p.Id == pr.PrestacionId);
                pr.Autorizacion = a;
            }
        }
    }
}