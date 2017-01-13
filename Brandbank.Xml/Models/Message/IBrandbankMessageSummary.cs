using System;
using System.Collections.Generic;

namespace Brandbank.Xml.Models.Message
{
    public interface IBrandbankMessageSummary
    {
        bool MessageHadProducts { get; }
        Guid MessageGuid { get; }
        IEnumerable<IBrandbankMessageSummaryProduct> ProductDetails { get; set; }
    }
}
