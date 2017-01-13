using Brandbank.Xml.Models.Message;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Brandbank.Xml.MessageHelpers
{
    public static class MessageTypeReaderExtensions
    {
        public static Guid GetMessageId(this MessageType messageType) => new Guid(messageType.Id);
        public static IEnumerable<ProductType> GetProducts(this MessageType messageType) => messageType.Product ?? new List<ProductType>().ToArray();
        public static bool HasProducts(this MessageType messageType) => messageType.Product != null;
        public static IEnumerable<IBrandbankMessageSummaryProduct> CreateMessageSummary(this MessageType messageType, Func<int, string> idGetter)
        {
            return messageType.GetProducts().Select(p => new BrandbankMessageSummaryProduct
            {
                ExternalId = idGetter(p.GetIdentity().GetPvid()),
                Pvid = p.GetIdentity().GetPvid(),
                Gtin = p.GetIdentity().GetGtin(),
                ProductDescription = p.GetIdentity().GetDiagnosticDescription(),
                UpdateType = p.GetUpdateType()
            });
        }

    }
}
