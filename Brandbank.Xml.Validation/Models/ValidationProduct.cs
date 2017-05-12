using System.Collections.Generic;
using System.Linq;

namespace Brandbank.Xml.Validation.Models
{
    public class ValidationProduct
    {
        public string ProductCodes { get; set; }
        public IEnumerable<Language> Languages { get; set; }
        public IEnumerable<Image> Images { get; set; }

        public IEnumerable<int> GetImageShotsWithDuplicates()
        {
            if (Images == null)
                return Enumerable.Empty<int>();

            return Images.GroupBy(i => i.ShotTypeId)
                        .Where(g => g.Count() > 1)
                        .Select(k => k.Key);
        }
    }
}
