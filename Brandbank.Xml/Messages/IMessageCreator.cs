using Brandbank.Xml.Models.Message;
using System;
using System.Collections.Generic;

namespace Brandbank.Xml.Messages
{
    public interface IMessageCreator<T>
    {
        MessageType CreateMessage(Guid messageGuid, T product);
        MessageType CreateMessage(Guid messageGuid, IEnumerable<T> products);
    }
}
