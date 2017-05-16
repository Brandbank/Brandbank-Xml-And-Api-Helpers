using Brandbank.Xml.Helpers;
using Brandbank.Xml.Models.Message;
using System.Collections.Generic;
using System.Linq;

namespace Brandbank.Xml.MessageHelpers
{
    public static class ProductTypeReaderExtensions
    {
        public static IEnumerable<DocumentType> GetDocuments(this ProductType productType)
        {
            return productType.Assets
                .NewIfNull()
                .Document
                .NewIfNull()
                .OrderBy(i => i.Id)
                .ThenBy(i => i.MimeType);
        }

        public static IEnumerable<ImageType> GetImages(this ProductType productType)
        {
            return productType.Assets
                .NewIfNull()
                .Image
                .NewIfNull()
                .OrderBy(i => i.Id)
                .ThenBy(i => i.MimeType);
        }

        public static IEnumerable<ImageType> GetImages(this ProductType productType, params int[] shotTypeIds)
        {
            return productType
                .GetImages()
                .Where(i => shotTypeIds.Contains(i.GetShopTypeId()));
        }

        public static IdentityType GetIdentity(this ProductType productType)
        {
            return productType.Identity.NewIfNull();
        }

        public static IEnumerable<LanguageType> GetLanguages(this ProductType productType)
        {
            return productType.Data.NewIfNull();
        }

        public static LanguageType GetLanguage(this ProductType productType, string languageCode)
        {
            var languageType = productType.GetLanguages().FirstOrDefault(l => l.Code.ToLowerInvariant() == languageCode.ToLowerInvariant());
            return languageType ?? productType.GetLanguages().FirstOrDefault() ?? new LanguageType("", languageCode);
        }

        public static string GetUpdateType(this ProductType productType)
        {
            return productType.UpdateType.ToString();
        }
        
        public static bool IsAddOrUpdate(this ProductType productType)
        {
            return productType.UpdateType == UpdateTypeType.AddOrUpdate;
        }

    }
}
