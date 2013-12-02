using Autorizaciones.Domain.Helpers;
using Sigs.AutorizacionesOnline.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Sigs.AutorizacionesOnline.Models
{
    [Table("Afiliado")]
    public class Afiliado : EntityBase
    {
        [MaxLength(50)]
        public string Nombres { get; set; }

        [MaxLength(50)]
        public string Apellidos { get; set; }

        public int NSS { get; set; }

        public DateTime FechaAfiliacion { get; set; }

        public int CotizacionesConsecutivasPDSS { get; set; }

        public virtual ICollection<Autorizacion> Autorizaciones { get; set; }

        public string Sexo { get; set; }

        public decimal ConsumidoSubGrupo(DateTime fechaServicio, SubGrupo subgrupo)
        {
            var fechas = StaticHelpers.GetRangoAnual(this.FechaAfiliacion, fechaServicio);
            var fechaInicial = fechas[0];
            var fechaFinal = fechas[1];

            var consumido = Autorizaciones.Where(p => p.FechaServicio >= fechaInicial);

            return 0;
        }

        [NotMapped]
        public string NombreCompleto
        {
            get
            {
                return string.Format("{0} {1}", Nombres, Apellidos);
            }
        }

        public DateTime FechaNacimiento { get; set; }

        [NotMapped]
        public int Edad
        {
            get
            {
                var years = (DateTime.Now.Date - FechaNacimiento.Date).TotalDays / 365;

                return Convert.ToInt32(decimal.Floor(decimal.Parse(years.ToString())));
            }
        }


    }
}
