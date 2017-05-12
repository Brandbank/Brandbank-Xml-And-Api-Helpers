using System.Collections.Generic;
using System.Linq;

namespace Brandbank.Xml.Validation.Models
{
    public class InvalidProductData
    {
        public string ProductCode { get; set; }
        public IEnumerable<int> DuplicateImageTypes { get; set; }
        public IEnumerable<InvalidLanguageData> InvalidLanguageData { get; set; }

        public IEnumerable<string> GetErrors()
        {
            var productErrorHeader = $"Product: {ProductCode} has the following errors:";
            var languageDataErrors = InvalidLanguageData.SelectMany(ild => ild.GetErrors());
            var imageDuplicateErrors = GetDuplicateImageTypeErrors();
            var headerList = new List<string> { productErrorHeader };
            if (languageDataErrors.Any() || imageDuplicateErrors.Any())
            {
                return headerList.Concat(imageDuplicateErrors)
                                 .Concat(languageDataErrors);
            }
            else
            {
                return new List<string>();
            }
        }

        public IEnumerable<string> GetDuplicateImageTypeErrors()
        {
            return DuplicateImageTypes.Select(i => $"Image Shot Type {i} has been duplicated");
        }
    }
}
