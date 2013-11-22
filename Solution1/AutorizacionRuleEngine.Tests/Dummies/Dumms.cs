using Sigs.AutorizacionesOnline.Models;
using Sigs.AutorizacionesOnline.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutorizacionRuleEngine.Tests.Dummies
{
    public class Dumms
    {
        public Afiliado PedroGarcia
        {
            get
            {
                return new Afiliado()
                {
                    Id = 1,
                    Nombres = "Pedro",
                    Apellidos = "Garcia",
                    FechaNacimiento = new DateTime(1958, 5, 10),
                    NSS = 3541,
                    Sexo = "M",
                    FechaAfiliacion = new DateTime(2011, 3, 25)
                };
            }
        }

        public Prestadora Cedimat
        {
            get
            {
                return new Prestadora()
                {
                    Id = 1,
                    Nombre = "Patronado de Centros de la plaza de la Salud",
                    CodigoSisalril = 2525
                };
            }
        }

        public TipoAutorizacion PrevencionYPromocion
        {
            get
            {
                return new TipoAutorizacion()
                {
                    Id = 1,
                    Nombre = "Prevencion y Promocion",
                };
            }
        }

        public Cobertura CONSULTA_MEDICINA_ESPECIALIZADA
        {
            get
            {
                return new Cobertura()
                {
                    Id = 2467,
                    Nombre = "CONSULTA MEDICINA ESPECIALIZADA"
                };
            }
        }

        public Cobertura CONTROL_DE_PLACA_DENTAL
        {
            get
            {
                return new Cobertura()
                {
                    Id = 411,
                    Nombre = "CONTROL DE PLACA DENTAL NCOC"
                };
            }
        }

        public Prestacion CONSULTA_MEDICINA_ESPECIALIZADA_PYP
        {
            get
            {
                return new Prestacion()
                    {
                        Id = 13,
                        Cobertura = CONSULTA_MEDICINA_ESPECIALIZADA,
                        SubGrupoId = SubGrupoPlanificación_Familiar.Id,
                        SubGrupo = SubGrupoPlanificación_Familiar
                    };
            }
        }

        public Prestacion CONTROL_DE_PLACA_DENTAL_NCOC
        {
            get
            {
                return new Prestacion()
                {
                    Id = 14,
                    Cobertura = CONTROL_DE_PLACA_DENTAL,
                    SubGrupo = new SubGrupo()
                    {
                        Id = "3.2",
                        Nombre = "ServiciosOdontologicos",
                        Grupo = new Grupo()
                        {
                            Id = 3,
                            Nombre = "Servicios Odontologicos"
                        }
                    }
                };
            }
        }

        public Autorizacion AutorizacionConConsultaEspecializadaPYP
        {
            get
            {
                return new Autorizacion()
                {
                    Id = 55,
                    Afiliado = PedroGarcia,
                    FechaAutorizacion = new DateTime(2013, 10, 11, 8, 35, 0),
                    FechaServicio = new DateTime(2013, 10, 11),
                    Prestadora = Cedimat,
                    TipoAutorizacion = PrevencionYPromocion,
                    Prestaciones = new List<PrestacionAutorizacion>() 
                    {
                        new PrestacionAutorizacion()
                        {
                            Prestacion = CONSULTA_MEDICINA_ESPECIALIZADA_PYP,
                            Tarifa = 500
                        }
                    }
                };
            }
        }

        public Autorizacion AutorizacionConServiciosOdontologicos
        {
            get
            {
                return new Autorizacion()
                {
                    Id = 55,
                    Afiliado = PedroGarcia,
                    FechaAutorizacion = new DateTime(2013, 10, 11, 8, 35, 0),
                    FechaServicio = new DateTime(2013, 10, 11),
                    Prestadora = Cedimat,
                    TipoAutorizacion = PrevencionYPromocion,
                    Prestaciones = new List<PrestacionAutorizacion>() 
                    {
                        new PrestacionAutorizacion()
                        {
                            Prestacion = CONTROL_DE_PLACA_DENTAL_NCOC,
                            Tarifa = 1000
                        }
                    }
                };
            }
        }


        public Autorizacion AutorizacionMedicamentos
        {
            get
            {
                return new Autorizacion()
                {
                    Id = 55,
                    Afiliado = PedroGarcia,
                    FechaAutorizacion = new DateTime(2013, 10, 11, 8, 35, 0),
                    FechaServicio = new DateTime(2013, 10, 11),
                    Prestadora = Cedimat,
                    TipoAutorizacion = PrevencionYPromocion,
                    Prestaciones = new List<PrestacionAutorizacion>() 
                    {
                        new PrestacionAutorizacion()
                        {
                            Prestacion = CONSULTA_MEDICINA_ESPECIALIZADA_PYP,
                            Tarifa = 500
                        }
                    }
                };
            }
        }

        public Grupo GrupoPYP
        {
            get
            {
                return new Grupo()
                {
                    Id = 1,
                    Nombre = "Prevencion y Promocion"
                };
            }
        }

        public SubGrupo SubGrupoPlanificación_Familiar
        {
            get
            {
                return new SubGrupo()
                {
                    Id = "1.5",
                    Nombre = "Planificación Familiar",
                    Grupo = GrupoPYP,
                    GrupoId = GrupoPYP.Id
                };
            }
        }

    }
}
