using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sigs.AutorizacionesOnline.Models.Entities.Experto.Reglas
{
    public class ResultadoAplicacionReglas
    {
        public List<string> Problemas { get; set; }

        public bool EstaEnCumplimiento
        {
            get
            {
                return Problemas.Count == 0;
            }
        }

        public List<Regla> ReglasIncumplidas { get; set; }
    }
}