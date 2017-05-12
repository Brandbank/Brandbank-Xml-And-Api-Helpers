using Brandbank.Xml.MessageHelpers;
using Brandbank.Xml.Models.Message;
using Xunit;

namespace Brandbank.Xml.Tests.MessageHelpers
{
    public class ImageTypeReaderExtensionsTests
    {
        private readonly ImageType _imageType;

        public ImageTypeReaderExtensionsTests()
        {
            _imageType = new ImageType(1, "URL", 200, 200)
            {
                Specification = { Filename = "Filename" }
            };
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
