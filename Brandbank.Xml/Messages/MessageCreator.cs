using Brandbank.Xml.MessageHelpers;
using Brandbank.Xml.Models.Message;
using Brandbank.Xml.ProductConverter;
using System;
using System.Collections.Generic;

namespace Brandbank.Xml.Messages
{
    public class MessageCreator<T> : IMessageCreator<T>
    {
        private readonly IProductConverter<T> _productConverter;

        public MessageCreator(IProductConverter<T> productConverter)
        {
            if (productConverter == null)
                throw new ArgumentNullException(nameof(productConverter));

            _productConverter = productConverter;
        }

        public MessageType CreateMessage(Guid messageGuid, IEnumerable<T> products)
        {
            var message = new MessageType(messageGuid, DateTime.UtcNow);
            foreach (var product in products)
                message.AddProduct(_productConverter.Convert(product));

            return message;
        }
    }
}
