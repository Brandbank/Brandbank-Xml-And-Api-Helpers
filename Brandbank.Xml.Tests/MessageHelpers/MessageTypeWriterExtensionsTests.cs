using Brandbank.Xml.MessageHelpers;
using Brandbank.Xml.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Brandbank.Xml.Tests.MessageHelpers
{
    public class MessageTypeWriterExtensionsTests
    {
        private readonly MessageType _message;
        public MessageTypeWriterExtensionsTests()
        {
            _message = new MessageType(Guid.NewGuid(), DateTime.UtcNow);
        }

        [Fact]
        public void ShouldCreateNewMessage()
        {
            var guid = Guid.NewGuid();
            var message = new MessageType(guid, DateTime.UtcNow);

            Assert.Equal(guid, message.GetMessageId());
        }

        [Fact]
        public void ShouldAddNewProductIfFirstProduct()
        {
            _message.AddProduct(new ProductType(DateTime.UtcNow));
            Assert.Equal(_message.Product.Length, 1);
        }

        [Fact]
        public void ShouldAddNewProductIfProductsAlreadyExist()
        {
            _message.AddProduct(new ProductType(DateTime.UtcNow));
            _message.AddProduct(new ProductType(DateTime.UtcNow));
            Assert.Equal(_message.Product.Length, 2);
        }

        [Fact]
        public void ShouldCreateAMessageWithAProduct()
        {
            var language = new LanguageType("Product Description", "en-gb");
            language.AddMemo(1, "Memo 1");
            language.AddMemo(2, "Memo 2");

            language.AddStatement(3, new List<int> { 1, 2, 3 });
            language.AddStatement(4, new List<int> { 4, 5, 6 });

            language.AddLongText(5, new List<string> { "1", "2", "3" });
            language.AddLongText(6, new List<string> { "4", "5", "6" });

            language.AddNameLookup(7, new Dictionary<int, int> { { 1, 10 }, { 2, 20 } });
            language.AddNameLookup(8, new Dictionary<int, int> { { 3, 30 }, { 4, 40 } });

            language.AddNameText(9, new Dictionary<int, string> { { 1, "10" } });
            language.AddNameText(10, new Dictionary<int, string> { { 2, "20" } });

            var identity = new IdentityType("12345678", "AAAA001", new List<string> { "GB" });
            var image = new ImageType(1, "URL", 200, 200);

            var product = new ProductType(DateTime.UtcNow);
            product.AddLanguage(language);
            product.AddIdentity(identity);
            product.AddImage(image);

            var message = new MessageType(Guid.NewGuid(), DateTime.UtcNow);
            message.AddProduct(product);

            Assert.Equal(message.GetProducts().FirstOrDefault().GetLanguage("en-gb").GetMemo("1"), "Memo 1");
        }
    }
}
