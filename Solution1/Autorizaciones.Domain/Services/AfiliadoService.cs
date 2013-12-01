using Autorizaciones.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigs.AutorizacionesOnline.Models
{
    public class AfiliadoService : Sigs.AutorizacionesOnline.Models.Services.IAfiliadoService
    {
        private ArsDataContext db;

        public AfiliadoService(ArsDataContext db)
        {
            // TODO: Complete member initialization
            this.db = db;
        }

        public Afiliado AfiliadoById(decimal id)
        {
            return db.Afiliados.SingleOrDefault(p => p.Id == id);
        }

        public AfiliadoService()
        {
            db = new ArsDataContext();
        }

        public decimal BalanceMedicamentosAmbulatorios(Autorizacion a, DateTime fechaAfiliacion, Afiliado afiliado, decimal limite)
        {
            var fechas = StaticHelpers.GetRangoFechasDeFechaServicio(fechaAfiliacion, a.FechaServicio);

            var fechaInicial = fechas[0];
            var fechaFinal = fechas[1];

            var consumido = afiliado
                .Autorizaciones
                .Where(p => p.TipoAutorizacionId == 12 && p.Disponible && p.FechaServicio >= fechaInicial && p.FechaServicio <= fechaFinal)
                .SelectMany(p => p.Prestaciones)
                .Where(p => p.Prestacion.SubGrupoId == "12.1" && p.Disponible)
                .Sum(p => p.Aprobado);

            return limite - consumido;
        }

        public decimal BalanceMedicamentosAltoCosto(Autorizacion a, DateTime fechaAfiliacion, Afiliado afiliado, decimal limite)
        {
            var fechas = StaticHelpers.GetRangoFechasDeFechaServicio(fechaAfiliacion, a.FechaServicio);

            AfiliadoService service = new AfiliadoService(new ArsDataContext());

            var fechaInicial = fechas[0];
            var fechaFinal = fechas[1];

            var consumido = afiliado.Autorizaciones.Where(p => p.TipoAutorizacionId == 12 && p.Disponible && p.FechaServicio >= fechaInicial && p.FechaServicio <= fechaFinal)
                .SelectMany(p => p.Prestaciones)
                .Where(p => p.Prestacion.SubGrupoId == "12.2" && p.Disponible)
                .Sum(p => p.Aprobado);

            return limite - consumido;
        }

        public decimal Balance(int tipoAutorizacionId, string subGrupoId, Autorizacion a, decimal limite)
        {
            var fechas = StaticHelpers.GetRangoFechasDeFechaServicio(a.Afiliado.FechaAfiliacion, a.FechaServicio);

            AfiliadoService service = new AfiliadoService(new ArsDataContext());

            var fechaInicial = fechas[0];
            var fechaFinal = fechas[1];

            var consumido = a.Afiliado
                .Autorizaciones
                .Where(p => (p.TipoAutorizacionId == tipoAutorizacionId) && p.Disponible && p.FechaServicio >= fechaInicial && p.FechaServicio <= fechaFinal)
                .SelectMany(p => p.Prestaciones.Where(q => (subGrupoId == null) || (q.Prestacion.SubGrupoId == subGrupoId) && q.Disponible))
                .Sum(p => p.Aprobado);

            return limite - consumido;
        }
    }
}
