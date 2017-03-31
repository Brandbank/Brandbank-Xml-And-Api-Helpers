using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brandbank.Xml.Validation.Models
{
    public class Language
    {
        public string Code { get; set; }
        public IEnumerable<ValidationItemType> ItemTypes { get; set; }
    }
}
