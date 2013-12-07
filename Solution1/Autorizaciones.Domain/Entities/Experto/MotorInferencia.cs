using Sigs.AutorizacionesOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Sigs.AutorizacionesOnline.Models.Entities.Experto;
using System.Workflow.Activities.Rules;
using Autorizaciones.Domain.Repositories;

namespace Sigs.Expertos
{
    public class MotorInferencia
    {
        /// <summary>
        /// Este método es el encargado de tomar el modelo y aprlicar las reglas.
        /// </summary>
        /// <param name="autorizacion"></param>
        /// <returns></returns>
        public Autorizacion Procesar(Autorizacion autorizacion)
        {
            RuleEngineRepository ruleRepository = new RuleEngineRepository();

            var autorizacionesRuleSet = ruleRepository.GetRuleSetFromFile(Autorizacion.RuleSetFileName);

            //Evalua las reglas de autorizaciones
            AplicarReglas<Autorizacion>(typeof(Autorizacion), autorizacion, autorizacionesRuleSet);

            var prestacionesRuleSet = ruleRepository.GetRuleSetFromFile(PrestacionAutorizacion.RuleSetFileName);

            foreach (var p in autorizacion.Prestaciones)
            {
                //Evalua las reglas de las prestaciones
                AplicarReglas<PrestacionAutorizacion>(typeof(PrestacionAutorizacion), p, prestacionesRuleSet);
            }

            return autorizacion;
        }

        public void AplicarReglas<T>(Type type, T obj, RuleSet ruleSet)
        {
            // Ejecuta las reglas y aplica los cambios correspondientes a la entidad
            RuleValidation validation = new RuleValidation(type, null);

            RuleExecution execution = new RuleExecution(validation, obj);

            ruleSet.Execute(execution);
        }
    }
}