using Brandbank.Xml.Models.Message;
using Brandbank.Xml.Logger;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Brandbank.Xml.Messages
{
    public class MessageCreatorLogger<T> : IMessageCreator<T>
    {
        readonly ILogger<IMessageCreator<T>> _logger;
        readonly IMessageCreator<T> _messageCreator;

        public MessageCreatorLogger(ILogger<IMessageCreator<T>> logger, IMessageCreator<T> messageCreator)
        {
            _logger = logger;
            _messageCreator = messageCreator;
        }

        public MessageType CreateMessage(Guid messageGuid, IEnumerable<T> products)
        {
            _logger.LogDebug($"Creating Message with Id {messageGuid} and {products.Count()} products");
            try
            {
                var message = _messageCreator.CreateMessage(messageGuid, products);
                _logger.LogDebug($"Created Message with Id {messageGuid} and {products.Count()} products");
                return message;
            }
            catch (Exception)
            {
                _logger.LogError($"Creating Message with Id {messageGuid} and {products.Count()} products failed");
                throw;
            }
        }
    }
}
