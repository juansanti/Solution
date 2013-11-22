using Sigs.AutorizacionesOnline.Models.Entities;
using System;
using System.Collections.Generic;
namespace Sigs.AutorizacionesOnline.Models
{
    public interface IAutorizacionService
    {
        Autorizacion GetAutorizacionPorId(int id);
        int GetNewAutoriacionId();
        IEnumerable<Prestacion> GetPrestacionesPDSS();
    }
}
