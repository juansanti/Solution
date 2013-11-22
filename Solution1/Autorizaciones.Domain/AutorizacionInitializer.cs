using Sigs.AutorizacionesOnline.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sigs.AutorizacionesOnline.Models
{
    public class AutorizacionInitializer
    {
        Usuario Usuario;

        public AutorizacionInitializer(Usuario usuario)
        {
            Usuario = usuario;
        }

        public AutorizacionInitializer() { }

        private void InitializeAutorizacion(Autorizacion autorizacion)
        {
            autorizacion.PrestadoraId = this.Usuario.UsuariosPrestadoras.First().PrestadoraId;
            autorizacion.UsuarioId = Usuario.Id;

            foreach (var p in autorizacion.Prestaciones)
            {
                p.UsuarioId = Usuario.Id;
                p.FechaCreacion = DateTime.Now;
            }
        }

        public void InitializeAutorizacionMedicamentos(Autorizacion autorizacion)
        {
            InitializeAutorizacion(autorizacion);

            autorizacion.FechaAutorizacion = DateTime.Now;
            autorizacion.FechaServicio = DateTime.Now;

            autorizacion.TipoAutorizacionId = 12;
        }
    }
}