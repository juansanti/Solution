using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sigs.AutorizacionesOnline.Models.Entities
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }

        public bool Disponible { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

        public EntityBase()
        {
            Disponible = true;
            FechaCreacion = DateTime.Now;
        }
    }

    public class RuleEntity : EntityBase
    {
        public string RulesAppliances { get; set; }

        public void pushRuleAppliance(string appliance)
        {
            if (string.IsNullOrEmpty(RulesAppliances))
            {
                RulesAppliances = string.Empty;
            }

            this.RulesAppliances = string.Format("{0} \n {1}", RulesAppliances, appliance);
        }
    }

}