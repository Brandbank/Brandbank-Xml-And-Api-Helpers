using Brandbank.Xml.MessageHelpers;
using Brandbank.Xml.Models.Message;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Brandbank.Xml.Tests.MessageHelpers
{
    public class IdentityTypeWriterExtensionsTests
    {
        private readonly IdentityType _identityType;
        public IdentityTypeWriterExtensionsTests()
        {
            _identityType = new IdentityType("1234", "1234", new List<string> { "GB" });
        }

        [Fact]
        public void ShouldAddProductCode()
        {
            _identityType.AddProductCode("scheme", "123456");

            var productCode = _identityType.ProductCodes.First(pc => pc.Scheme.Equals("scheme"));

            Assert.Equal(productCode.Scheme, "scheme");
            Assert.Equal(productCode.Value, "123456");
        }

        [Fact]
        public void ShouldAddDiagnosticDescription()
        {
            _identityType.AddDiagnosticDescription("Diagnostic Description");
            Assert.Equal(_identityType.GetDiagnosticDescription(), "Diagnostic Description");
        }

        [Fact]
        public void ShouldAddDefaultLanguage()
        {
            _identityType.AddDefaultLanguage("en-gb");
            Assert.Equal(_identityType.GetDefaultLanguage(), "en-gb");
        }

    }
}
