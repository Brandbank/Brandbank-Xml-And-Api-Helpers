using System;
using Brandbank.Xml.Models.Message;

namespace Brandbank.Api
{
    public interface IGetUnsentDownloader
    {
        void GetUnsent(Func<MessageType, IBrandbankMessageSummary> productProcessor);
    }
}