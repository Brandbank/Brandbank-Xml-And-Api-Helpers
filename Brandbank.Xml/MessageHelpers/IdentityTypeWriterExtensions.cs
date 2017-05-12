using Brandbank.Xml.Helpers;
using Brandbank.Xml.Models.Message;

namespace Brandbank.Xml.MessageHelpers
{
    public static class IdentityTypeWriterExtensions
    {
        public static void AddProductCode(this IdentityType identityType, string scheme, string code)
        {
            identityType.ProductCodes = identityType.ProductCodes.ExtendArray(new ProductCodeType
            {
                Scheme = scheme,
                Value = code
            }, pc => pc.Scheme);
        }

        public static void AddDiagnosticDescription(this IdentityType identityType, string diagnosticDescription)
        {
            if (identityType.DiagnosticDescription == null)
                identityType.DiagnosticDescription = new DiagnosticDescriptionType();

            identityType.DiagnosticDescription.Value = diagnosticDescription;
        }

        public static void AddDefaultLanguage(this IdentityType identityType, string defaultLanguage)
        {
            identityType.DefaultLanguage = defaultLanguage;
        }
    }
}
