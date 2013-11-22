using Sigs.AutorizacionesOnline.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Sigs.AutorizacionesOnline.Models.Entities
{
    [Table("TiposPrestadoras")]
    public class TipoPrestadora : EntityBase
    {
        public string Nombre { get; set; }

        public virtual ICollection<Prestadora> Prestadoras { get; set; }

        public TipoPrestadora()
        {
            Prestadoras = new HashSet<Prestadora>();
        }
    }
}
