using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutorizacionRuleEngine.Tests
{
    public class Rule
    {
        public int RuleID { get; set; }
        public string ObjectProperty { get; set; }
        public string ComparisonOperator { get; set; }
        public string TargetValue { get; set; }

        public Rule(string ObjectProperty, string ComparisonOperator, string TargetValue)
        {
            this.ObjectProperty = ObjectProperty;
            this.ComparisonOperator = ComparisonOperator;
            this.TargetValue = TargetValue;
        }
    }
}
