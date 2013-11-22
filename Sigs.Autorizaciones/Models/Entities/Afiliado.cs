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

        [Required]
        public DateTime FechaAfiliacion { get; set; }

        public int CotizacionesConsecutivasPDSS { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [NotMapped]
        public int Edad
        {
            get
            {
                return Convert.ToByte(decimal.Floor(((DateTime.Now - FechaNacimiento).Days / 365m)));
            }
        }

        public virtual ICollection<Autorizacion> Autorizaciones { get; set; }

        public int Sexo { get; set; }

        [NotMapped]
        public Sexo SexoAfiliado
        {
            get
            {
                return (Sexo)Sexo;
            }
            set
            {
                Sexo = (int)value;
            }
        }

        [NotMapped]
        public string NombreCompleto
        {
            get
            {
                return string.Format("{0} {1}", Nombres, Apellidos);
            }
        }

        public Afiliado()
        {
            Autorizaciones = new HashSet<Autorizacion>();
        }

        public string Situacion { get; set; }

    }
}
