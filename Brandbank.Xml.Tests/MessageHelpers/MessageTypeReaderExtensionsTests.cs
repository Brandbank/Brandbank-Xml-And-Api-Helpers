using Brandbank.Xml.MessageHelpers;
using Brandbank.Xml.Models.Message;
using System;
using Xunit;

namespace Brandbank.Xml.Tests.MessageHelpers
{
    public class MessageTypeReaderExtensionsTests
    {
        private MessageType _message;

        public MessageTypeReaderExtensionsTests()
        {
            _message = new MessageType(Guid.NewGuid(), DateTime.UtcNow);
        }

        [Fact]
        public void ShouldGetMessageId()
        {
            var guid = Guid.NewGuid();
            var message = new MessageType(guid, DateTime.UtcNow);

            Assert.Equal(guid, message.GetMessageId());
        }

        [Fact]
        public void ShouldReturnFalseIfMessageDoesNotHaveProducts()
        {
            Assert.False(_message.HasProducts());
        }

        [Fact]
        public void ShouldReturnTrueIfMessageHasProducts()
        {
            _message.AddProduct(new ProductType(DateTime.Now));
            Assert.True(_message.HasProducts());
        }
    }
}
