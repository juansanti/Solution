using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Sigs.AutorizacionesOnline.Models.Entities
{
    [Table("Coberturas")]
    public class Cobertura : EntityBase
    {
        public int Id { get; set; }
        public int TipoCoberturaId { get; set; }
        public string Nombre { get; set; }
        public string CUPS { get; set; }

        public bool NivelAtencion1 { get; set; }
        public bool NivelAtencion2 { get; set; }
        public bool NivelAtencion3 { get; set; }

        public bool PDSS { get; set; }

        public int? SIMON { get; set; }
        
        [NotMapped]
        public TipoCobertura TipoCobertura
        {
            get
            {
                return (TipoCobertura)TipoCoberturaId;
            }
            set
            {
                TipoCoberturaId = Convert.ToInt32(value);
            }
        }

        public virtual ICollection<Prestacion> Prestaciones { get; set; }
    }
}
