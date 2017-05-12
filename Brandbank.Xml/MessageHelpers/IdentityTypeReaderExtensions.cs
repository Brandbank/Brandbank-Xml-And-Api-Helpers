using Brandbank.Xml.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Brandbank.Xml.MessageHelpers
{
    public static class IdentityTypeReaderExtensions
    {
        public static string GetProductCode(this IdentityType identityType, string scheme)
        {
            if (identityType.ProductCodes.Any())
            {
                var productCode = identityType.ProductCodes.FirstOrDefault(pc => pc.Scheme.Equals(scheme, StringComparison.CurrentCultureIgnoreCase));
                return productCode == null ? string.Empty : productCode.Value;
            }
            return string.Empty;
        }

        public static int GetPvid(this IdentityType identityType)
        {
            int pvid;
            if (!int.TryParse(identityType.GetProductCode("BRANDBANK:PVID"), out pvid)) return pvid;
            return pvid;
        }

        public static string GetGtin(this IdentityType identityType, bool padTo14Digits = false)
        {
            var gtin = identityType.GetProductCode("GTIN");
            return padTo14Digits ? gtin.PadLeft(14, '0') : gtin;
        }
        
        public static string GetDiagnosticDescription(this IdentityType identityType)
        {
            if (identityType.DiagnosticDescription == null)
                identityType.DiagnosticDescription = new DiagnosticDescriptionType();

            return identityType.DiagnosticDescription.Value ?? string.Empty;
        }

        public static string GetDefaultLanguage(this IdentityType identityType)
        {
            return identityType.DefaultLanguage ?? string.Empty;
        }

        public static string GetSubscriberCode(this IdentityType identityType)
        {
            if (identityType.Subscription == null)
                identityType.Subscription = new SubscriptionType();

            return identityType.Subscription.Code ?? string.Empty;
        }

        public static IEnumerable<string> GetTargetMarkets(this IdentityType identityType)
        {
            if (identityType.TargetMarkets == null)
                identityType.TargetMarkets = Enumerable.Empty<TargetMarketType>().ToArray();

            return identityType.TargetMarkets.Select(tm => tm.Code);
        }

        public static string GetTargetMarkets(this IdentityType identityType, string delimeter)
        {
            return string.Join(delimeter, identityType.GetTargetMarkets());
        }

    }
}
