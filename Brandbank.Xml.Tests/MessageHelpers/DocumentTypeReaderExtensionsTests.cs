using Brandbank.Xml.MessageHelpers;
using Brandbank.Xml.Models.Message;
using Xunit;

namespace Brandbank.Xml.Tests.MessageHelpers
{
    public class DocumentTypeReaderExtensionsTests
    {
        private readonly DocumentType _documentType;

        public DocumentTypeReaderExtensionsTests()
        {
            _documentType = new DocumentType("URL");
        }

        [Fact]
        public void ShouldGetDocuemntUrl()
        {
            Assert.Equal(_documentType.GetDocumentUrl(), "URL");
        }
    }
}
