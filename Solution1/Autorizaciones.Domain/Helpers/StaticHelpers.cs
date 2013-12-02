using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autorizaciones.Domain.Helpers
{
    public class StaticHelpers
    {

        public static DateTime[] GetRangoAnual(DateTime fechaAfiliacion, DateTime fechaServicio)
        {
            var fechaDesde = new DateTime();
            var fechaHasta = new DateTime();

            if (fechaServicio < fechaAfiliacion)
            {
                throw new Exception("Fecha de servicio es menor a fecha de ingreso de afiliado");
            }
            else
            {
                var fecha = fechaAfiliacion.AddYears(fechaServicio.Year - fechaAfiliacion.Year);

                fechaDesde = fecha > fechaServicio ? fecha.AddYears(-1) : fecha;
                fechaHasta = fechaDesde.AddYears(1);
            }

            return new DateTime[2] { fechaDesde, fechaHasta };
        }

        public static decimal GetAprobado(decimal balance, decimal montoAprobar, ref string rulesApp)
        {
            rulesApp = string.Empty;

            if (montoAprobar > balance)
            {
                if (balance > 0)
                {
                    rulesApp += string.Format("se aprobó balance restante disponible de {0}", balance);
                }
                return balance;
            }
            else
            {
                return montoAprobar;
            }

        }
    }
}
