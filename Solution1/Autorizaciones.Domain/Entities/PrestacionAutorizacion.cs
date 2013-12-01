using Sigs.AutorizacionesOnline.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Sigs.AutorizacionesOnline.Models
{
    [Table("PrestacionesAutorizaciones")]
    public class PrestacionAutorizacion : RuleEntity
    {
        public int AutorizacionId { get; set; }
        public virtual Autorizacion Autorizacion { get; set; }

        public int PrestacionId { get; set; }
        public virtual Prestacion Prestacion { get; set; }

        public int Cantidad { get; set; }

        public decimal Tarifa { get; set; }

        public decimal Aprobado { get; set; }

        public decimal CoPago { get; set; }

        public static string RuleSetFileName
        {
            get
            {
                //return @"C:\Users\jsanti\Documents\Visual Studio 2012\Projects\Prestamos\Solution\Solution1\Autorizaciones.Domain\Rules\ARS.PrestacionAutorizacion.rules";
                return @"C:\Users\jsanti\Documents\Visual Studio 2012\Projects\Prestamos\Solution\Solution1\Autorizaciones.Domain\Rules\ARS.PrestacionAutorizacion.rules";
            }
        }
    }
}
