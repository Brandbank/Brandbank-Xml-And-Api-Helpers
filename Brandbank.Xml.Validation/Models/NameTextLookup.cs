using System.Collections.Generic;

namespace Brandbank.Xml.Validation.Models
{
    public class NameTextLookup
    {
        public string BaseTypeId { get; set; }
        public string ItemTypeId { get; set; }
        public string ItemTypeDescription { get; set; }
        public string LookupId { get; set; }
        public string NameTypeId { get; set; }
        public string NameTextValue { get; set; }
        public string NameTextText { get; set; }
        public string LookupTextValue { get; set; }
        public int Occurrances { get; set; }
        public IEnumerable<IdValue> Tags { get; set; }
    }
}
