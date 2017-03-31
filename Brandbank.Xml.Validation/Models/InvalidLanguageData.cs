using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brandbank.Xml.Validation.Models
{
    public class InvalidLanguageData
    {
        public string ProductCode { get; set; }
        public string LanguageCode { get; set; }
        public IEnumerable<InvalidItemType> InvalidItemTypes { get; set; }
        public IEnumerable<ValidationItemType> InvalidNameAndLookUps { get; set; }
        public IEnumerable<InvalidItemTypeOccurrences> InvalidItemTypeOccurances { get; set; }

        public IEnumerable<string> GetErrors()
        {
            var occurrancesErrors = InvalidItemTypeOccurances.Select(iito => iito.GetError());
            var itemTypeErrors = InvalidItemTypes.Select(iit => iit.GetError());
            var nameLookupErrors = InvalidNameAndLookUps.SelectMany(it => it.GetErrors());
            var productErrorHeader = $"Language: {LanguageCode} has the following errors:";
            var headerList = new List<string> { productErrorHeader };
            if (itemTypeErrors.Any() || nameLookupErrors.Any() || occurrancesErrors.Any())
            {
                return headerList
                                .Concat(occurrancesErrors)
                                .Concat(itemTypeErrors)
                                .Concat(nameLookupErrors);
            }
            else
            {
                return Enumerable.Empty<string>();
            }
        }
    }
}
