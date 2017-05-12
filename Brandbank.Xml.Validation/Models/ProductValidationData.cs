using System.Collections.Generic;

namespace Brandbank.Xml.Validation.Models
{
    public class ProductValidationData
    {
        public List<string> ItemBaseTypes { get; set; }
        public List<ValidationItemType> ItemTypes { get; set; }
        public List<ValidationItemNameType> ItemNameTypes { get; set; }
        public List<ValidationItemLookupType> ItemLookupTypes { get; set; }
        public List<string> TagTypes { get; set; }
    }

}
