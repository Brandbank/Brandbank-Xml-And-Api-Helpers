using System.Collections.Generic;

namespace Brandbank.Xml.Models.Coverage
{
    public interface IBrandbankCoverage
    {
        string RetailerId { get; set; }
        IEnumerable<KeyValuePair<string, string>> Gtins { get; set; }
        string Description { get; set; }
        bool OwnLabel { get; set; }
        IEnumerable<KeyValuePair<string, string>> Categories { get; set; }
    }
}