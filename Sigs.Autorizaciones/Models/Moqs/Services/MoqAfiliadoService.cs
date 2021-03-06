﻿using Sigs.AutorizacionesOnline.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sigs.AutorizacionesOnline.Models.Moqs.Services
{
    public class MoqAfiliadoService : IAfiliadoService
    {
        #region IAfiliadoService Members

        public MoqAfiliadoService()
        {

        }

        public Afiliado AfiliadoById(decimal id)
        {
            var afiliado = new Afiliado()
            {
                Nombres = "Maria",
                Apellidos = "Lavandera",
                Id = 334598,
                FechaNacimiento = new DateTime(1969, 4, 15),
                NSS = 198714,
                Sexo = 1,
                Disponible = true
            };

            return afiliado;
        }

        public decimal BalanceMedicamentosAmbuladorios(Afiliado afiliado, Autorizacion autorizacion)
        {
            return 3000;
        }

        #endregion
    }
}