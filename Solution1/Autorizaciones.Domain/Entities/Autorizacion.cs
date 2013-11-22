using Sigs.AutorizacionesOnline.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Sigs.AutorizacionesOnline.Models
{
    [Table("Autorizaciones")]
    public class Autorizacion : RuleEntity
    {
        [ForeignKey("Prestadora")]
        public int PrestadoraId { get; set; }

        public virtual Prestadora Prestadora { get; set; }

        public DateTime FechaAutorizacion { get; set; }

        public DateTime FechaServicio { get; set; }

        [MaxLength(50)]
        public string Contacto { get; set; }

        public virtual ICollection<PrestacionAutorizacion> Prestaciones { get; set; }

        public int AfiliadoId { get; set; }
        public virtual Afiliado Afiliado { get; set; }

        public int TipoAutorizacionId { get; set; }
        public virtual TipoAutorizacion TipoAutorizacion { get; set; }

        public int? DiagnosticoId { get; set; }
        public Diagnostico Diagnostico { get; set; }

        public bool AccidenteTransito { get; set; }

        public bool AccidenteLaboral { get; set; }

        [MaxLength(300)]
        public string Comentario { get; set; }

        [NotMapped]
        public decimal MontoSolicitado
        {
            get
            {
                return Prestaciones.Where(p => p.Disponible).Sum(p => p.Tarifa);
            }
        }

        [NotMapped]
        public decimal MontoAprobado
        {
            get
            {
                return Prestaciones.Where(p => p.Disponible).Sum(p => p.Aprobado);
            }
        }

        public Autorizacion()
        {
            Prestaciones = new HashSet<PrestacionAutorizacion>();
        }

        public void RechazarTodo()
        {
            foreach (var p in Prestaciones)
            {
                p.Aprobado = 0;
            }
        }

        public static string RuleSetFileName
        {
            get
            {
                //return @"C:\Users\jsanti\Documents\Visual Studio 2012\Projects\Prestamos\Solution\Solution1\Autorizaciones.Domain\Rules\ARS.Autorizaciones.rules";
                return @"C:\Users\jsanti\Documents\Visual Studio 2012\Projects\Prestamos\Solution\Solution1\Autorizaciones.Domain\Rules\ARS.Autorizaciones.rules";
            }
        }
    }
}
