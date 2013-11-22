using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Sigs.AutorizacionesOnline.Models.Entities
{
    [Table("Grupos")]
    public class Grupo : EntityBase
    {
        [MaxLength(50)]
        public string Nombre { get; set; }

        public virtual ICollection<SubGrupo> SubGrupos { get; set; }

        public Grupo()
        {
            SubGrupos = new HashSet<SubGrupo>();
        }
    }
}
