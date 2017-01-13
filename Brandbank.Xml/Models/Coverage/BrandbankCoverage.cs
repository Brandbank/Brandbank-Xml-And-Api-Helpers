using System.Collections.Generic;

namespace Brandbank.Xml.Models.Coverage
{
    public class BrandbankCoverage : IBrandbankCoverage
    {
        public IEnumerable<KeyValuePair<string, string>> Categories { get; set; }
        public string Description { get; set; }
        public IEnumerable<KeyValuePair<string, string>> Gtins { get; set; }
        public bool OwnLabel { get; set; }
        public string RetailerId { get; set; }
    }
}
