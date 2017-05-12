using Brandbank.Xml.Models.Message;
using System;
using System.Collections.Generic;

namespace Brandbank.Xml.Messages
{
    public interface IMessageCreator<in T>
    {
        MessageType CreateMessage(Guid messageGuid, IEnumerable<T> products);
    }
}
