using Brandbank.Xml.Helpers;
using Brandbank.Xml.Models.Message;
using System.Drawing;
using System.IO;

namespace Brandbank.Xml.MessageHelpers
{
    public static class ProductTypeWriterExtensions
    {
        public static void AddIdentity(this ProductType productType, IdentityType identityType)
        {
            productType.Identity = identityType;
        }
        public static void AddImage(this ProductType product, string path, string imageName, int shotTypeId)
        {
            var filePath = Path.Combine(path, imageName);
            if (File.Exists(filePath))
                using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (var jpg = Image.FromStream(file, false, false))
                {
                    var image = new ImageType(shotTypeId, $"File://{imageName}", jpg.Size.Height, jpg.Size.Width);
                    product.AddImage(image);
                }
        }

        public static void AddImage(this ProductType productType, ImageType imageType)
        {
            if (productType.Assets == null)
                productType.Assets = new AssetsType();

            productType.Assets.Image = productType.Assets.Image.ExtendArray(imageType, i => i.ShotTypeId);
        }

        public static void AddDocument(this ProductType productType, DocumentType documentType)
        {
            if (productType.Assets == null)
                productType.Assets = new AssetsType();

            productType.Assets.Document = productType.Assets.Document.ExtendArray(documentType, i => i.Id);
        }

        public static void AddLanguage(this ProductType productType, LanguageType languageType)
        {
            productType.Data = productType.Data.ExtendArray(languageType, l => l.Code);
        }
    }
}
