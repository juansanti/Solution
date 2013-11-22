using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sigs.AutorizacionesOnline.Models
{
    public class AutorizacionValidator
    {
        AfiliadoService afiliadoService;

        Afiliado afiliado;
        Autorizacion autorizacion;

        public ArsDataContext db { get; set; }

        public AutorizacionValidator(ArsDataContext db, Autorizacion autorizacion)
        {
            this.db = db;
            this.afiliadoService = new AfiliadoService(db);

            this.afiliado = db.Afiliados.Single(p => p.Id == autorizacion.AfiliadoId);
            this.autorizacion = autorizacion;
        }

        public ServiceResult ValidarAutorizacion()
        {
            ServiceResult result = new ServiceResult();

            if (autorizacion.AfiliadoId <= 0)
            {
                result.ThrowFail("Afiliado nó valido");
            }

            if (autorizacion.FechaAutorizacion == DateTime.MinValue)
            {
                result.ThrowFail("Favor especificar la fecha de autorización");
            }

            if (autorizacion.Prestaciones.Count == 0)
            {
                result.ThrowFail("No ha agregado ningún servicio a la autorización.");
            }

            if (autorizacion.MontoAprobado == 0)
            {
                result.ThrowFail("El monto aprobado es igual a Cero(0 RD$).");
            }
            return result;
        }

        public ServiceResult ValidarAutorizacionMedicamentos()
        {
            var result = ValidarAutorizacion();

            var balance = 3000;// afiliadoService.BalanceMedicamentosAmbuladorios(afiliado, autorizacion);

            if (autorizacion.MontoAprobado > 3000)
            {
                result.ThrowFail("El monto aprobado no puede ser mayor a 3,000 RD$");
            }
            if (autorizacion.MontoAprobado > balance)
            {
                result.ThrowFail(string.Format("No se puede autorizar {0} a este afiliado. Monto Disponible es {0}", autorizacion.MontoAprobado, balance));
            }

            return result;
        }
    }
}