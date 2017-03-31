using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brandbank.Xml.Validation.Models
{
    public class InvalidItemType : ItemType
    {
        public string ProductDescription { get; set; }

        public string GetError()
        {
            return $"ItemType {ItemTypeId} ({ItemTypeDescription}) is invalid on BaseType {ItemBaseTypeId} ({ItemBaseTypeDescription})";
        }
    }
}
