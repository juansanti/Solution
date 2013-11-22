using Sigs.AutorizacionesOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Sigs.AutorizacionesOnline.Models.Entities.Experto;
using Sigs.AutorizacionesOnline.Models.Repositories;
using Sigs.AutorizacionesOnline.Models.Entities.Experto.Reglas;

namespace Sigs.Expertos
{
    public class MotorInferencia
    {
        IReglaRepository reglasRepository;
        IAutorizacionRepository autorizacionRepository;
        IConsumidoService repository;


        public MotorInferencia(IReglaRepository reglasRepository, IAutorizacionRepository autorizacionRepository, IConsumidoService repository)
        {
            this.reglasRepository = reglasRepository;
            this.autorizacionRepository = autorizacionRepository;
            this.repository = repository;
        }

        public string Procesar(Autorizacion autorizacion)
        {
            var prestadora = autorizacion.Prestadora;

            var grupos = autorizacion.Prestaciones.Select(p => p.Prestacion.SubGrupo.Grupo);

            var subGrupos = autorizacion.Prestaciones.Select(p => p.Prestacion.SubGrupo);

            var prestaciones = autorizacion.Prestaciones;

            var coberturas = autorizacion.Prestaciones.Select(p => p.Prestacion.Cobertura);

            var afiliado = autorizacion.Afiliado;

            var reglas = reglasRepository.ReglasVigentes.OrderBy(p => p.TipoRegla);
            return "";
        }

        public string ProcesarReglasDeCantidad(Autorizacion autorizacion, IEnumerable<Regla> reglas)
        {
            foreach (var r in reglas)
            {
                switch (r.EntidadAfectada)
                {
                    case TipoEntidadAfectada.Cobertura:
                        {
                            //Verificar si alguna de las coberturas de la autorizacion esta en regla antes de validarla
                            break;
                        }
                    case TipoEntidadAfectada.Prestacion:
                        {
                            //Verificar si alguna de las prestaciones de la autorizacion esta en regla antes de validarla
                            break;
                        }
                    case TipoEntidadAfectada.SubGrupo:
                        { break; }
                    case TipoEntidadAfectada.Grupo:
                        { break; }
                    case TipoEntidadAfectada.Sexo:
                        { break; }
                    case TipoEntidadAfectada.Edad:
                        { break; }
                    case TipoEntidadAfectada.Afiliado:
                        { break; }
                    case TipoEntidadAfectada.Prestadora:
                        { break; }

                }

            }
            return "";
        }

        public bool PuedeAutoriar(Afiliado a, decimal balance, out string Motivos)
        {
            StringBuilder bldr = new StringBuilder();

            var puedeAutorizar = true;

            if (!a.Disponible)
            {
                bldr.Append(string.Format("No se puede continuar con la autorizacion, el afiliado se encuentra Activo en nuestra institución"));
                Motivos = bldr.ToString();
                return false;
            }
            else
            {
                if (balance <= 0)
                {
                    bldr.Append("No se puede continuar con la autorización, el afiliado no tiene balance disponible");
                    puedeAutorizar = false;
                }
            }

            Motivos = bldr.ToString();

            return puedeAutorizar;
        }
    }
}