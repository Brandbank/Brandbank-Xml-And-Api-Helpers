using Brandbank.Xml.MessageHelpers;
using Brandbank.Xml.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Brandbank.Xml.Tests.MessageHelpers
{
    public class ProductTypeReaderExtensionsTests
    {
        private ProductType _productType;
        public ProductTypeReaderExtensionsTests()
        {
            _productType = new ProductType(DateTime.Parse("2016-01-01 12:00:00"));
            _productType.AddIdentity(new IdentityType("123456789", "AAAA001", new List<string> { "GB" }));
            _productType.AddImage(new ImageType(1, "http://imageurl.com", 200, 200));
            _productType.AddImage(new ImageType(1, "http://imageurl.com", 500, 500));
            _productType.AddImage(new ImageType(2, "http://imageurl.com", 200, 200));
            _productType.AddImage(new ImageType(3, "http://imageurl.com", 200, 200));
            _productType.AddImage(new ImageType(4, "http://imageurl.com", 200, 200));
            _productType.AddLanguage(new LanguageType("Product Description", "en-gb"));
        }

        [Fact]
        public void ShouldHaveAddOrUpdateUpdateType()
        {
            Assert.Equal(_productType.UpdateType, UpdateTypeType.AddOrUpdate);
        }

        [Fact]
        public void ShouldHaveVerionsDateTimeSet()
        {
            Assert.Equal(_productType.VersionDateTime, DateTime.Parse("2016-01-01 12:00:00"));
        }

        [Fact]
        public void ShouldReturnImages()
        {
            var images = _productType.GetImages();
            Assert.NotNull(images);
            Assert.Equal(images.Count(), 5);
        }

        [Fact]
        public void ShouldReturnImagesWhenNoImagesExist()
        {
            var productType = new ProductType(DateTime.Parse("2016-01-01 12:00:00"));
            var images = productType.GetImages(new[] { 1, 4 });
            Assert.NotNull(images);
            Assert.Equal(images.Count(), 0);
        }

        [Fact]
        public void ShouldReturnImagesWhenShotTypesSpecified()
        {
            var images = _productType.GetImages(new[] { 1, 4 });

            Assert.Equal(images.ElementAt(0).GetShopTypeId(), 1);
            Assert.Equal(images.ElementAt(1).GetShopTypeId(), 1);
            Assert.Equal(images.ElementAt(2).GetShopTypeId(), 4);
            Assert.Equal(images.Count(), 3);
        }

        [Fact]
        public void ShouldReturnIdentity()
        {
            Assert.NotNull(_productType.GetIdentity());
        }

        [Fact]
        public void ShouldReturnLanguages()
        {
            Assert.NotNull(_productType.GetLanguages());
        }

        [Theory]
        [InlineData("en-gb")]
        [InlineData("fr")]
        public void ShouldReturnLanguage(string language)
        {
            Assert.NotNull(_productType.GetLanguage(language));
        }

        [Fact]
        public void ShouldReturnUpdateType()
        {
            Assert.NotNull(_productType.GetUpdateType());
        }

        [Fact]
        public void ShouldReturnTrueIfUpdateTypeIsAddOrUpdate()
        {
            Assert.True(_productType.IsAddOrUpdate());
        }

        
    }
}
