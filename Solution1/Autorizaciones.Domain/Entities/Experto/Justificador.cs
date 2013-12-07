using Sigs.AutorizacionesOnline.Models;
using Sigs.AutorizacionesOnline.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autorizaciones.Domain.Entities.Experto
{
    public class Justificador
    {
        public string adjetivo(Afiliado a)
        {
            int edad = a.Edad;
            SexoAfiliado sexo = a.Sexo == "F" ? SexoAfiliado.Femenino : SexoAfiliado.Masculino;

            string adjetivo = string.Empty;

            if (edad <= 17)
            {
                if (sexo == SexoAfiliado.Femenino)
                {
                    adjetivo = "la niña";
                }
                else
                {
                    adjetivo = "el niño";
                }

            }
            else if (edad <= 35)
            {
                if (sexo == SexoAfiliado.Femenino)
                {
                    adjetivo = "la joven";
                }
                else
                {
                    adjetivo = "el joven";
                }
            }
            else
            {
                if (sexo == SexoAfiliado.Femenino)
                {
                    adjetivo = "la señora";
                }
                else
                {
                    adjetivo = "el señor";
                }
            }

            return adjetivo;
        }

        public dynamic ExtraerResultado(Autorizacion a)
        {
            string resumen = string.Empty;

            List<string> detalle = new List<string>();

            if (a.MontoAprobado == 0)
            {
                return NoCubreNada(a);
            }
            else
            {
                if (a.MontoAprobado == a.MontoSolicitado)
                {
                    return CubreTodo(a);
                }
                else
                {
                    return CubreUnaParte(a);
                }
            }
        }

        public JustificacionResult CubreUnaParte(Autorizacion a)
        {
            JustificacionResult r = new JustificacionResult();

            r.resumen = string.Format("ARS IA, cubre ({0}RD$) de ({1}RD$) Solicitado por {2} {3}, Hoy {4} por concepto servicios de tipo {5} prestados en fecha {6}",
                  a.MontoAprobado,
                  a.MontoSolicitado,
                  adjetivo(a.Afiliado),
                  a.Afiliado.NombreCompleto,
                  DateTime.Now.ToShortDateString(),
                  a.TipoAutorizacion.Nombre,
                  a.FechaServicio.ToShortDateString());

            if (!string.IsNullOrEmpty(a.RulesAppliances))
            {
                r.detalle.Add(a.RulesAppliances);
            }

            var fd = FormatPrestaciones(a.Prestaciones);
            r.detalle.AddRange(fd);

            r.Autorizacion = ProjectarAutorizacionForInsert(a);

            r.Procede = true;
            return r;
        }

        public JustificacionResult NoCubreNada(Autorizacion a)
        {
            JustificacionResult r = new JustificacionResult();
            r.resumen = string.Format("Lo sentimos, no podemos cubrir {0} {1} en ninguno de los servicios solicitados. A continuación mas detalle", adjetivo(a.Afiliado), a.Afiliado.NombreCompleto);

            if (!string.IsNullOrEmpty(a.RulesAppliances))
                r.detalle.Add(a.RulesAppliances);

            var fd = FormatPrestaciones(a.Prestaciones);
            r.detalle.AddRange(fd);
            r.Procede = false;

            return r;
        }

        public JustificacionResult CubreTodo(Autorizacion a)
        {
            JustificacionResult r = new JustificacionResult();
            r.resumen = string.Format("Ars IA, cubre por completo a {0} {1} por los servicios médicos prestados. Solicitud Por un monto de {2}",
                adjetivo(a.Afiliado),
                a.Afiliado.NombreCompleto,
                a.MontoAprobado);

            r.Autorizacion = ProjectarAutorizacionForInsert(a);
            r.Procede = true;

            return r;
        }

        public class JustificacionResult
        {
            public string resumen { get; set; }
            public List<string> detalle { get; set; }
            public dynamic Autorizacion { get; set; }
            public Boolean Procede { get; set; }

            public JustificacionResult()
            {
                detalle = new List<string>();
            }
        }

        public List<string> FormatPrestaciones(IEnumerable<PrestacionAutorizacion> prestaciones)
        {
            var detalle = new List<string>();

            foreach (var p in prestaciones)
            {
                if (!string.IsNullOrEmpty(p.RulesAppliances))
                {
                    detalle.Add(string.Format("({0}) {1} en {5} ({3}) ({4}), ({2})", p.Cantidad, p.Prestacion.Cobertura.Nombre, p.RulesAppliances, p.Cantidad * p.Tarifa, p.Cantidad * p.Aprobado, p.Prestacion.SubGrupo.Nombre));
                }
            }

            return detalle;
        }

        public dynamic ProjectarAutorizacionForInsert(Autorizacion a)
        {
            return new
            {
                a.PrestadoraId,
                a.AfiliadoId,
                a.FechaAutorizacion,
                a.FechaServicio,
                a.TipoAutorizacionId,
                a.UsuarioId,
                a.DiagnosticoId,
                a.Contacto,
                a.Comentario,
                a.AccidenteTransito,
                a.AccidenteLaboral,
                a.RulesAppliances,
                Prestaciones = a.Prestaciones.Select(p => new
                {
                    p.PrestacionId,
                    p.Cantidad,
                    p.Tarifa,
                    p.Aprobado,
                    Simon = p.Prestacion.Cobertura.SIMON,
                    Nombre = p.Prestacion.Cobertura.Nombre,
                    CoPago = p.Tarifa - p.Aprobado,
                    p.RulesAppliances,
                    p.UsuarioId
                })
            };
        }

        public dynamic MostrarAutorizacion(Autorizacion a)
        {
            return new
            {
                a.Id,
                a.MontoAprobado,
                a.MontoSolicitado,
                Prestaciones = a.Prestaciones.Count(),
            };
        }
    }
}
