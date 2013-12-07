using Sigs.AutorizacionesOnline.Models.Entities;
using Sigs.AutorizacionesOnline.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigs.AutorizacionesOnline.Models
{
    public class AutorizacionService : IAutorizacionService
    {
        private IAfiliadoService afiliadoService;

        private ArsDataContext db;

        public AutorizacionService(ArsDataContext db)
        {
            this.db = db;
        }

        public Autorizacion GetAutorizacionPorId(int id)
        {
            throw new NotImplementedException();
        }

        public int GetNewAutoriacionId()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Prestacion> GetPrestacionesPDSS()
        {
            return db.Prestaciones;
        }

        public void Crear(Autorizacion autorizacion)
        {
            db.Autorizaciones.Add(autorizacion);

            db.SaveChanges();
        }


    }
}
