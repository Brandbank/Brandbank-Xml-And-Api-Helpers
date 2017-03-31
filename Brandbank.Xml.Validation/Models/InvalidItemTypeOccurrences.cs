using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brandbank.Xml.Validation.Models
{
    public class InvalidItemTypeOccurrences : InvalidItemType
    {
        public int MaxOccurrances { get; set; }
        public int Occurrances { get; set; }
        public new string GetError()
        {
            return $"ItemType {ItemTypeId} ({ItemTypeDescription}) appears {Occurrances} times on BaseType {ItemBaseTypeId} ({ItemBaseTypeDescription}) but the max occurances is { MaxOccurrances}";
        }
    }
}
