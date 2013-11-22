using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Sigs.AutorizacionesOnline.Models.Entities
{
    [Table("TiposAutorizaciones")]
    public class TipoAutorizacion : EntityBase
    {
        [MaxLength(50)]
        public string Nombre { get; set; }

        public virtual ICollection<Autorizacion> Autorizaciones { get; set; }

        public TipoAutorizacion()
        {
            Autorizaciones = new HashSet<Autorizacion>();
        }

    }
}
