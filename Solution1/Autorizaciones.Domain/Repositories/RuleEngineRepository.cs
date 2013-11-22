using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Workflow.Activities.Rules;
using System.Workflow.ComponentModel.Serialization;
using System.Xml;

namespace Autorizaciones.Domain.Repositories
{
    public class RuleEngineRepository
    {

        public RuleSet GetRuleSetFromFile(string fileName)
        {
            WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();

            XmlTextReader rulesReader = new XmlTextReader(fileName);

            var ruleSet = (RuleSet)serializer.Deserialize(rulesReader);
            rulesReader.Close();

            return ruleSet;
        }
    }
}
