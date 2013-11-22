using Sigs.AutorizacionesOnline.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigs.AutorizacionesOnline.Models
{
    [Table("UsuariosPrestadoras")]
    public class UsuariosPrestadoras
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Prestadora")]
        public int PrestadoraId { get; set; }

        public virtual Prestadora Prestadora { get; set; }
    }
}
