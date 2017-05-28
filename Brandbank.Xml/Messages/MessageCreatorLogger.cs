using Brandbank.Xml.Logging;
using Brandbank.Xml.Models.Message;
using System;
using System.Collections.Generic;

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

        public MessageType CreateMessage(Guid messageGuid, T product)
        {
            _logger.LogDebug($"Creating brandbank message [{messageGuid}]");
            try
            {
                var message = _messageCreator.CreateMessage(messageGuid, product);
                _logger.LogDebug($"Created brandbank message [{messageGuid}]");
                return message;
            }
            catch (Exception e)
            {
                _logger.LogError($"Creating brandbank message [{messageGuid}] failed: {e.Message}");
                throw;
            }
        }

        public MessageType CreateMessage(Guid messageGuid, IEnumerable<T> products)
        {
            _logger.LogDebug($"Creating brandbank message with multiple products [{messageGuid}]");
            try
            {
                var message = _messageCreator.CreateMessage(messageGuid, products);
                _logger.LogDebug($"Created brandbank message with multiple products [{messageGuid}]");
                return message;
            }
            catch (Exception e)
            {
                _logger.LogError($"Creating brandbank message with multiple products [{messageGuid}] failed: {e.Message}");
                throw;
            }
        }
    }
}
