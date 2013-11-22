using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Sigs.AutorizacionesOnline.Models.Entities
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Login { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }

        public string Email { get; set; }

        public bool Disponible { get; set; }

        public virtual ICollection<Autorizacion> Autorizaciones { get; set; }

        public virtual ICollection<UsuariosPrestadoras> UsuariosPrestadoras { get; set; }

        public Usuario()
        {
            Autorizaciones = new HashSet<Autorizacion>();
        }
    }
}
