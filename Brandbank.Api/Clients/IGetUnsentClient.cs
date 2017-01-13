using Brandbank.Xml.Models.Message;
using System;
using System.Xml;

namespace Brandbank.Api.Clients
{
    public interface IGetUnsentClient : IDisposable
    {
        IBrandbankMessageSummary AcknowledgeMessage(IBrandbankMessageSummary messageInformation);
        XmlNode GetUnsentProductData();
    }
}
