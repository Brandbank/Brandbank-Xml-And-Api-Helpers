using Brandbank.Xml.MessageHelpers;
using Brandbank.Xml.Models.Message;
using System;
using System.Collections.Generic;
using Xunit;

namespace Brandbank.Xml.Tests.MessageHelpers
{
    public class ProductTypeWriterExtensionsTests
    {
        private readonly ProductType _productType;
        public ProductTypeWriterExtensionsTests()
        {
            _productType = new ProductType(DateTime.Parse("2016-01-01 12:00:00"));
        }

        [Fact]
        public void ShouldAddIdentity()
        {
            _productType.AddIdentity(new IdentityType("123456789", "AAAA001", new List<string> { "GB" }));
            Assert.NotNull(_productType.Identity);
            Assert.Equal(_productType.Identity.GetGtin(), "123456789");
            Assert.Equal(_productType.Identity.GetTargetMarkets(","), "GB");
            Assert.Equal(_productType.Identity.GetSubscriberCode(), "AAAA001");
        }

        [Fact]
        public void ShouldAddImage()
        {
            _productType.AddImage(new ImageType(1, "http://imageurl.com", 200, 200));
            Assert.NotNull(_productType.Assets.Image);
            Assert.Equal(_productType.Assets.Image[0].Url.Value, "http://imageurl.com");
            Assert.Equal(_productType.Assets.Image[0].Specification.RequestedDimensions.Height, 200);
            Assert.Equal(_productType.Assets.Image[0].Specification.RequestedDimensions.Width, 200);
            Assert.Equal(_productType.Assets.Image[0].ShotTypeId, 1);
        }

        [Fact]
        public void ShouldAddLanguage()
        {
            _productType.AddLanguage(new LanguageType("Product Description", "en-gb"));
            Assert.NotNull(_productType.Data);
            Assert.NotNull(_productType.Data[0].ItemTypeGroup);
            Assert.Equal(_productType.Data[0].Description, "Product Description");
            Assert.Equal(_productType.Data[0].Code, "en-gb");
        }
    }
}
