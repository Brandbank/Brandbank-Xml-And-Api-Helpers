using Brandbank.Xml.MessageHelpers;
using Brandbank.Xml.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Brandbank.Xml.Tests.MessageHelpers
{
    public class ImageTypeReaderExtensionsTests
    {
        private ImageType _imageType;

        public ImageTypeReaderExtensionsTests()
        {
            _imageType = new ImageType(1, "URL", 200, 200);
            _imageType.Specification.Filename = "Filename";
        }

        [Fact]
        public void ShouldGetFileName()
        {
            Assert.Equal(_imageType.GetFileName(), "Filename");
        }

        [Fact]
        public void ShouldGetUrl()
        {
            Assert.Equal(_imageType.GetUrl(), "URL");
        }

        [Fact]
        public void ShouldGetShopTypeOId()
        {
            Assert.Equal(_imageType.GetShopTypeId(), 1);
        }
    }
}
