using Sigs.AutorizacionesOnline.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;

namespace Sigs.AutorizacionesOnline.Models
{
    public class ArsDataContext : DbContext
    {
        public DbSet<Afiliado> Afiliados { get; set; }
        public DbSet<Autorizacion> Autorizaciones { get; set; }

        public DbSet<Cobertura> Coberturas { get; set; }
        public DbSet<Diagnostico> Diagnosticos { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<SubGrupo> SubGrupos { get; set; }
        public DbSet<Prestacion> Prestaciones { get; set; }
        public DbSet<PrestacionAutorizacion> PrestacionesAutorizaciones { get; set; }
        public DbSet<TipoAutorizacion> TiposAutorizaciones { get; set; }
        public DbSet<TipoPrestadora> TiposPrestadoras { get; set; }
        public DbSet<UsuariosPrestadoras> UsuariosPrestadoras { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Prestadora> Prestadoras { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }


    }
}
