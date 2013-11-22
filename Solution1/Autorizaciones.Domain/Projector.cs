
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sigs.AutorizacionesOnline.Models
{
    public class Projector
    {
        AfiliadoService service;
        AutorizacionService autorizacionService;
        ArsDataContext db;
        public Projector()
        {
            this.db = new ArsDataContext();

            autorizacionService = new AutorizacionService(db);
            service = new AfiliadoService(db);
        }


        public dynamic ProyectarAutorizacion(Autorizacion autorizacion)
        {
            //Fill(autorizacion);

            return autorizacion == null ? null : new
            {
                autorizacion.Id,
                FechaServicio = autorizacion.FechaServicio.ToString("dd/MM/yyyy"),
                Afiliado = ProyectarAfiliado(autorizacion.Afiliado),
                autorizacion.TipoAutorizacionId,
                TipoAutorizacion = autorizacion.TipoAutorizacion.Nombre,
                autorizacion.DiagnosticoId,
                Diagnostico = autorizacion.Diagnostico == null ? null : autorizacion.Diagnostico.Descripcion,
                autorizacion.Contacto,
                autorizacion.MontoAprobado,
                autorizacion.Comentario,
                Prestaciones = autorizacion.Prestaciones.Where(p => p.Disponible).Select(p => ProyectarPrestacionAutorizacion(p)).ToList(),
                Estado = autorizacion.Disponible ? "Disponible" : "Cancelada",

                autorizacion.Usuario.Login,
                autorizacion.AccidenteTransito,
                autorizacion.AccidenteLaboral
            };
        }

        public dynamic ProyectarAutorizacion(int autorizacionId)
        {
            var autorizacion = autorizacionService.GetAutorizacionPorId(autorizacionId);

            return ProyectarAutorizacion(autorizacion);
        }

        public dynamic ProyectarAfiliado(Afiliado afiliado)
        {
            var result = afiliado == null ? null : new
            {
                AfiliadoId = afiliado.Id,
                afiliado.NSS,
                Foto = afiliado.Sexo,
                afiliado.NombreCompleto,
                afiliado.Sexo,
                afiliado.Edad
            };

            return result;
        }

        public dynamic ProyectarPrestacionAutorizacion(PrestacionAutorizacion prestacionAutorizacion)
        {
            var result = prestacionAutorizacion == null ? null : new
            {
                prestacionAutorizacion.Id,
                prestacionAutorizacion.PrestacionId,
                prestacionAutorizacion.AutorizacionId,
                prestacionAutorizacion.Cantidad,
                prestacionAutorizacion.Tarifa,
                prestacionAutorizacion.CoPago,
                prestacionAutorizacion.Aprobado,
                prestacionAutorizacion.Prestacion.PorcentajeCobertura,
                prestacionAutorizacion.Prestacion.CoberturaId,
                prestacionAutorizacion.Prestacion.Cobertura.Nombre,
                prestacionAutorizacion.FechaCreacion,
                Usuario = prestacionAutorizacion.Usuario.Login,
            };

            return result;
        }

        public string ProjectMensajeCreacion(Autorizacion autorizacion)
        {
            return string.Format("Autorización creada satisfactoriamente. \n número ({0}) por un monto de ({1} RD$), \n Prestadora (\"{2}\").", autorizacion.Id, autorizacion.MontoAprobado, autorizacion.Prestadora.Nombre == null ? "foo" : autorizacion.Prestadora.Nombre);
        }
    }
}