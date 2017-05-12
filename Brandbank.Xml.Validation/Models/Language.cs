using System.Collections.Generic;

namespace Brandbank.Xml.Validation.Models
{
    public class Language
    {
        public string Code { get; set; }
        public IEnumerable<ValidationItemType> ItemTypes { get; set; }
    }
}
