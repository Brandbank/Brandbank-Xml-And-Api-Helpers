using Brandbank.Xml.MessageHelpers;
using Brandbank.Xml.Models.Message;
using Brandbank.Xml.Products;
using System;
using System.Collections.Generic;

namespace Brandbank.Xml.Messages
{
    public class MessageCreator<T> : IMessageCreator<T>
    {
        private readonly IProductConverter<T> _productConverter;

        public MessageCreator(IProductConverter<T> productConverter)
        {
            _productConverter = productConverter;
        }

        public MessageType CreateMessage(Guid messageGuid, T product)
        {
            return CreateMessage(messageGuid, new[] { product });
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
