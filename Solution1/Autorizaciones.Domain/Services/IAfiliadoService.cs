using System;
namespace Sigs.AutorizacionesOnline.Models.Services
{
    public interface IAfiliadoService
    {
        Afiliado AfiliadoById(decimal id);
        //decimal BalanceMedicamentosAmbuladorios(global::Sigs.AutorizacionesOnline.Models.Afiliado afiliado, global::Sigs.AutorizacionesOnline.Models.Autorizacion autorizacion);
        decimal Balance(int tipoAutorizacionId, string subGrupoId, Autorizacion a, decimal limite);
    }
}
