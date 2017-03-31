using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Brandbank.Xml.Validation.Models
{
    public class ValidationItemNameType : ItemNameType
    {
        public string RegEx { get; set; }
        public string RegExErrorMessage { get; set; }

        public bool PassesRegex(string value)
        {
            var regex = new Regex(RegEx);
            return regex.IsMatch(value);
        }
    }
}
