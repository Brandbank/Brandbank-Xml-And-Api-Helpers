using Brandbank.Xml.MessageHelpers;
using Brandbank.Xml.Models.Message;
using System.Collections.Generic;
using Xunit;

namespace Brandbank.Xml.Tests.MessageHelpers
{
    public class IdentityTypeReaderExtensionsTests
    {
        private readonly IdentityType _identityType;
        public IdentityTypeReaderExtensionsTests()
        {
            _identityType = new IdentityType("1234", "sub code", new List<string> { "GB", "IE" });
        }

        [Fact]
        public void ShouldReturnProductPvid()
        {
            _identityType.AddProductCode("BRANDBANK:PVID", "1111");
            Assert.Equal(_identityType.GetPvid(), 1111);
        }

        [Fact]
        public void ShouldReturnProductGtin()
        {
            Assert.Equal(_identityType.GetGtin(), "1234");
        }

        [Fact]
        public void ShouldReturnGetProductCode()
        {
            _identityType.AddProductCode("GLN", "9377000097127");
            Assert.Equal(_identityType.GetProductCode("GLN"), "9377000097127");
        }

        [Fact]
        public void ShouldReturnProductGtinPaddedTo14Digits()
        {
            Assert.Equal(_identityType.GetGtin(true), "00000000001234");
        }

        [Fact]
        public void ShouldReturnEmptyStringIfProductCodeDoesNotExist()
        {
            Assert.Equal(_identityType.GetProductCode("Scheme does not exist"), string.Empty);
        }

        [Fact]
        public void ShouldReturnDiagnosticDescription()
        {
            _identityType.AddDiagnosticDescription("Diagnostic Description");
            Assert.Equal(_identityType.GetDiagnosticDescription(), "Diagnostic Description");
        }

        [Fact]
        public void ShouldEmptyStringIfDiagnosticDescriptionIsNull()
        {
            _identityType.AddDiagnosticDescription(null);
            Assert.Equal(_identityType.GetDiagnosticDescription(), string.Empty);
        }

        [Fact]
        public void ShouldReturnDefaultLanguage()
        {
            _identityType.AddDefaultLanguage("en");
            Assert.Equal(_identityType.GetDefaultLanguage(), "en");
        }

        [Fact]
        public void ShouldReturnEmptyStringIfDefaultLanguageIsNull()
        {
            _identityType.AddDefaultLanguage(null);
            Assert.Equal(_identityType.GetDefaultLanguage(), string.Empty);
        }

        [Fact]
        public void ShouldReturnProductSubscriberCode()
        {
            var code = _identityType.GetSubscriberCode();
            Assert.Equal(code, "sub code");
        }

        [Fact]
        public void ShouldReturnEmptyStringIfProductSubscriberCodeIsNull()
        {
            var identityType = new IdentityType("1234", null, new List<string> { "GB" });
            Assert.Equal(identityType.GetSubscriberCode(), string.Empty);
        }

        [Fact]
        public void ShouldReturnDelimitedStringOfTargetMarkets()
        {
            Assert.Equal(_identityType.GetTargetMarkets("|"), "GB|IE");
        }

        [Fact]
        public void ShouldReturnListOfTargetMarkets()
        {
            Assert.Equal(_identityType.GetTargetMarkets(), new List<string> { "GB", "IE" });
        }

        [Fact]
        public void ShouldReturnEmptyStringIfTargetMarketsIsNull()
        {
            var identityType = new IdentityType();
            Assert.Equal(identityType.GetTargetMarkets("|"), string.Empty);
        }
    }
}


