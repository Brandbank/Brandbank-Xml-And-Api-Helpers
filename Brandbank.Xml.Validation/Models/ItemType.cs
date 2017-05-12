using System.Collections.Generic;

namespace Brandbank.Xml.Validation.Models
{
    public class ItemType
    {
        public string ItemTypeId { get; set; }
        public string ItemTypeDescription { get; set; }
        public string ItemBaseTypeId { get; set; }
        public string ItemBaseTypeDescription { get; set; }
        public IEnumerable<IdValue> NameTypes { get; set; }
        public IEnumerable<IdValue> LookupTypes { get; set; }
        public IEnumerable<IdValue> TagTypes { get; set; }
        public IEnumerable<TextConstraint> TextConstraints { get; set; }
    }
}
