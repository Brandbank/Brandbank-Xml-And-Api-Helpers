using Brandbank.Xml.Helpers;
using Brandbank.Xml.Models.Message;
using System.Linq;

namespace Brandbank.Xml.MessageHelpers
{
    public static class ProductTypeWriterExtensions
    {
        public static void AddIdentity(this ProductType productType, IdentityType identityType)
        {
            productType.Identity = identityType;
        }

        public static void AddImage(this ProductType productType, ImageType imageType)
        {
            if (productType.Assets == null)
                productType.Assets = new AssetsType();

            productType.Assets.Image = productType.Assets.Image.ExtendArray(imageType, i => i.ShotTypeId);
        }

        public static void AddLanguage(this ProductType productType, LanguageType languageType)
        {
            productType.Data = productType.Data.ExtendArray(languageType, l => l.Code);
        }
    }
}
