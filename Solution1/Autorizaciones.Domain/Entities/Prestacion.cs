using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Sigs.AutorizacionesOnline.Models.Entities
{
    [Table("Prestaciones")]
    public class Prestacion : EntityBase
    {
        public object Tarifa { get; set; }

        public decimal PorcentajeCobertura { get; set; }

        public string SubGrupoId { get; set; }

        [ForeignKey("SubGrupoId")]
        public virtual SubGrupo SubGrupo { get; set; }

        public int CoberturaId { get; set; }
        public virtual Cobertura Cobertura { get; set; }

        public string Sexo { get; set; }

        [NotMapped]
        public SexoPrestaciones SexoPrestacion
        {
            get
            {
                return (SexoPrestaciones)Enum.Parse(typeof(SexoPrestaciones), Sexo.ToString());
            }
            set
            {
                Sexo = value.ToString();
            }
        }
    }
}
