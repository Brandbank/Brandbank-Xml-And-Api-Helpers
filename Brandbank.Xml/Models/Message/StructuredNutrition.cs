using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brandbank.Xml.Models.Message
{
    public class StructuredNutrition
    {
        public IEnumerable<ValueGroupDefinition> ValueGroupDefinitions { get; set; }
        public IEnumerable<StructuredNutrient> StructuredNutrients { get; set; }
    }

    public class ValueGroupDefinition
    {
        public int Id { get; set; }
        public string AmountHeader { get; set; }
        public KeyValuePair<int, decimal> AmountTotal { get; set; }
        public IEnumerable<string> ReferenceIntakeHeaders { get; set; }
    }

    public class StructuredNutrient
    {
        public int Id { get; set; }
        public int UnitId { get; set; }
        public IEnumerable<ValueGroup> ValueGroups { get; set; }
    }

    public class ValueGroup
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public IEnumerable<decimal> ReferenceIntakeValue { get; set; }
    }
}
