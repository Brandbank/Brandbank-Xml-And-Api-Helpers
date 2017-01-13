using Brandbank.Xml.MessageHelpers;
using System;
using System.Collections.Generic;

namespace Brandbank.Xml.Models.Message
{
    public class BrandbankMessageSummary : IBrandbankMessageSummary
    {
        public bool MessageHadProducts { get; set; }
        public Guid MessageGuid { get; set; }
        public IEnumerable<IBrandbankMessageSummaryProduct> ProductDetails { get; set; }

        public BrandbankMessageSummary() { }

        public BrandbankMessageSummary(MessageType messageType)
        {
            MessageHadProducts = messageType.HasProducts();
            MessageGuid = messageType.GetMessageId();
            ProductDetails = messageType.CreateMessageSummary(pvid => pvid.ToString());
        }
    }
}
