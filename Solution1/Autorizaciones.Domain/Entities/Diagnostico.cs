using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Sigs.AutorizacionesOnline.Models.Entities
{
    [Table("Diagnosticos")]
    public class Diagnostico : EntityBase
    {
        [MaxLength(50)]
        public string Descripcion { get; set; }

        public virtual ICollection<Autorizacion> Autorizaciones { get; set; }

        public Diagnostico()
        {
            Autorizaciones = new HashSet<Autorizacion>();
        }
    }
}
