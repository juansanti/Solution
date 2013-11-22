using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sigs.AutorizacionesOnline.Models.Entities.Experto.Reglas
{
    [Table("Reglas")]
    public class Regla : EntityBase
    {
        public byte TipoRegla { get; set; }

        [NotMapped]
        public TipoRegla Tipo
        {
            get
            {
                return (TipoRegla)TopeDias;
            }
            set
            {
                TopeDias = (int)value;
            }
        }

        public int Entidad { get; set; }

        /// <summary>
        /// Poner 0 si afecta a todas las entidades
        /// </summary>
        public string EntidadId { get; set; }

        [NotMapped]
        public TipoEntidadAfectada EntidadAfectada
        {
            get
            {
                return (TipoEntidadAfectada)Entidad;
            }
            set
            {
                Entidad = (int)value;
            }
        }

        public string Valor { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaCaducidad { get; set; }

        /// <summary>
        /// Rango de tiempo entre tope. (No se utiliza para Reglas del tipo MontoCubierto, el valor debe ser 0)
        /// </summary>
        public int TopeDias { get; set; }


    }
}