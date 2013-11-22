using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Sigs.AutorizacionesOnline.Models.Entities
{
    [Table("SubGrupos")]
    public class SubGrupo
    {
        [Key]
        public string Id { get; set; }

        [MaxLength(50)]
        public string Nombre { get; set; }

        public int GrupoId { get; set; }
        public virtual Grupo Grupo { get; set; }

        public virtual ICollection<Prestacion> Prestaciones { get; set; }

        public SubGrupo()
        {
            Prestaciones = new HashSet<Prestacion>();
        }
    }
}
