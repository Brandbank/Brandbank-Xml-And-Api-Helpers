using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brandbank.Xml.Validation.Models
{
    public class IdValue
    {
        public string Id { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }

        override
        public string ToString()
        {
            var result = Id;
            if (Value != null)
                result += $" ({Value})";
            return result;
        }
    }

    public class IdValueComparer : IEqualityComparer<IdValue>
    {
        public bool Equals(IdValue x, IdValue y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(IdValue obj)
        {
            if (obj.Id == null)
                return 0;
            return obj.Id.GetHashCode();
        }
    }
}
