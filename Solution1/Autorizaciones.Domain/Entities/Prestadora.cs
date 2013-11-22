using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Autorizaciones.Domain.Entities;

namespace Sigs.AutorizacionesOnline.Models.Entities
{
    [Table("Prestadoras")]
    public class Prestadora
    {
        public int Id { get; set; }

        public int CodigoSisalril { get; set; }

        [MaxLength(50)]
        public string Nombre { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int TipoPrestadoraId { get; set; }
        public virtual TipoPrestadora TipoPrestadora { get; set; }

        public virtual ICollection<UsuariosPrestadoras> UsuariosPrestadoras { get; set; }
        public virtual ICollection<Autorizacion> Autorizaciones { get; set; }

        public bool Disponible { get; set; }

        public Prestadora()
        {
            UsuariosPrestadoras = new HashSet<UsuariosPrestadoras>();
            Autorizaciones = new HashSet<Autorizacion>();
        }
    }
}
