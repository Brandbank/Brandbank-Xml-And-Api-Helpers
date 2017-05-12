using Brandbank.Xml.Helpers;
using Brandbank.Xml.Models.Message;

namespace Brandbank.Xml.MessageHelpers
{
    public static class MessageTypeWriterExtensions
    {
        public static void AddProduct(this MessageType messageType, ProductType productType)
        {
            messageType.Product = messageType.Product.ExtendArray(productType, p => p.VersionDateTime);
        }
    }
}
